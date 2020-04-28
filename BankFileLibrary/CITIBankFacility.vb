Imports System.IO
Imports BankFileLibrary

Friend Class CITIBankFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of CITIBankRecord)
    Public Property BankNumber As String

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "CitiBank"
        BankRecords = New List(Of CITIBankRecord)

        SetBankNumber()
    End Sub

    ''' <summary>
    ''' Look at FacilityName to determine the Bank Number
    ''' Facilities and codes are 'Hard-Coded'
    ''' Throws 'KeyNotFoundException' if not listed.
    ''' </summary>
    Private Sub SetBankNumber()
        Select Case FacilityName
            Case "New York Metro - NYB", "NEXT ONE"
                BankNumber = "001"
            Case "Connecticut - CCT"
                BankNumber = "002"
            Case "Maryland - CMD"
                BankNumber = "007"
            Case "DC - CDC"
                BankNumber = "008"
            Case "Nevada - CNV"
                BankNumber = "011"
            Case "CA/Nevada - CCA"
                BankNumber = "013"
            Case "Illinois - CIL"
                BankNumber = "014"
            Case "Florida - FSB"
                BankNumber = "016"
            Case "New Jersey - CNJ"
                BankNumber = "030"
            Case "Florida - FNA"
                BankNumber = "040"
            Case "Texas - CTX"
                BankNumber = "041"
            Case Else
                Throw New KeyNotFoundException("Facility not listed.")
        End Select
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim citiRecord As New CITIBankRecord

        citiRecord.AccountNumber = bankRecord.AccountNumber
        citiRecord.IssueVoid = fields(8)
        citiRecord.CheckNumber = bankRecord.CheckNumber
        citiRecord.Amount = bankRecord.Amount
        citiRecord.IssueDate = bankRecord.IssueDate
        citiRecord.AdditionalData = String.Empty
        citiRecord.Payee = bankRecord.Payee

        BankRecords.Add(citiRecord)
    End Sub


    Public Overrides Function CreateBankFile(fileName As String) As Integer
        Dim checkIssuedCount As Integer
        Dim totalDollars As Decimal

        ReadInputFile(fileName)

        Using sw As StreamWriter = New StreamWriter(OutputFileName)
            ' Header Record
            sw.Write("H" + Utilities.LeftJustifiedField(FacilityName, 30) + Utilities.YYYMMDDString(DateTime.Today))
            sw.WriteLine()

            checkIssuedCount = 0
            totalDollars = 0.0

            For Each bankRecord In BankRecords
                sw.Write("D" + BankNumber + Utilities.RightJustifiedField(AccountNumber, 10) + Space(7))
                sw.Write(bankRecord.IssueVoid + Utilities.RightJustifiedField(bankRecord.CheckNumber, 10))
                sw.Write(Utilities.RightJustifiedField(bankRecord.IntegerAmount, 10))
                sw.Write(Utilities.YYYMMDDString(bankRecord.IssueDate) + Utilities.LeftJustifiedField(bankRecord.AdditionalData, 15) + Space(15))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.Payee, 80))
                sw.WriteLineAsync()
                If (Not bankRecord.IssueVoid.ToUpper().Equals("V")) Then
                    totalDollars += bankRecord.Amount
                    checkIssuedCount += 1
                End If
            Next

            ' Trailer / Totals Record
            sw.Write("T" + BankNumber + Utilities.LeftJustifiedField(FacilityName, 30))
            sw.Write(Utilities.RightJustifiedField(AccountNumber, 10) + Space(8))
            sw.Write(Utilities.LeftFillZeros(checkIssuedCount, 10))
            sw.Write(Utilities.DecimalToString(totalDollars, 10) + Space(38))
            sw.WriteLine()

        End Using

        Return BankRecords.Count
    End Function
End Class


