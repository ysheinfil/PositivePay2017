Imports System.IO

Public Class MTBankFacilityLongAcct
    Inherits MTBankFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "M & T Bank Long Acct"
        BankRecords = New List(Of MTBankRecord)
    End Sub

    Public Overrides Function CreateBankFile(fileName As String) As Integer

        ReadInputFile(fileName)

        Using writer As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                writer.Write(bankRecord.TransactionId)
                writer.Write(Utilities.RightJustifiedField(bankRecord.AccountNumber, 14))
                writer.Write(Utilities.RightJustifiedField(bankRecord.SerialNumber, 10))
                writer.Write(Utilities.LeftFillZeros(bankRecord.IntegerAmount, 12))
                writer.Write(Utilities.YYYMMDDString(bankRecord.IssueDate))
                writer.Write("  " + Utilities.LeftJustifiedField(bankRecord.Payee, 100))
                writer.WriteLine()
            Next
        End Using
        Return BankRecords.Count
    End Function
End Class
