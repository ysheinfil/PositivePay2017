Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class MTBankFacility
    Inherits AbstractFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "M & T Bank"
        BankRecords = New List(Of MTBankRecord)

    End Sub

    Public Property BankRecords As List(Of MTBankRecord)

    ''' <summary>
    ''' Format: Fixed width
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer

        ReadInputFile(fileName)

        SetOutputToTxt()

        Using writer As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                writer.Write(Utilities.LeftFillZeros(bankRecord.AccountNumber, 15))
                writer.Write(Utilities.LeftFillZeros(bankRecord.CheckNumber, 10))
                writer.Write(Utilities.LeftFillZeros(bankRecord.IntegerAmount, 12))
                writer.Write(Utilities.YYYMMDDString(bankRecord.IssueDate))
                writer.Write("  " + Utilities.LeftJustifiedField(bankRecord.Payee, 100))
                writer.WriteLine()
            Next
        End Using

        Checks = BankRecords.Count
        Return BankRecords.Count
    End Function

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim mtRecord As MTBankRecord

        mtRecord = New MTBankRecord()
        mtRecord.Amount = bankRecord.Amount
        mtRecord.IssueDate = bankRecord.IssueDate
        mtRecord.AccountNumber = bankRecord.AccountNumber
        mtRecord.CheckNumber = bankRecord.CheckNumber
        mtRecord.Payee = bankRecord.Payee

        BankRecords.Add(mtRecord)

    End Sub

End Class
