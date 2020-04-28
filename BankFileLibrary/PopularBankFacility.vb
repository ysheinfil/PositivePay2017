Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO
''' <summary>
''' Similar to Amalgamated
'''     The fields are the same
'''     Assuming the input is the same
'''     The output is in a different order
''' </summary>
Public Class PopularBankFacility
    Inherits AbstractFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Popular"
        BankRecords = New List(Of GenericBankRecord)

    End Sub

    Public Property BankRecords As List(Of GenericBankRecord)

    Public Overrides Function CreateBankFile(fileName As String) As Integer
        ReadInputFile(fileName)

        Using writer As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                writer.Write(bankRecord.Amount)
                writer.Write("," + Utilities.YYYMMDDString(bankRecord.IssueDate) + ",")
                writer.Write(bankRecord.CheckNumber + ",")
                writer.Write(bankRecord.Payee)
                writer.WriteLine()
            Next
        End Using

        Return BankRecords.Count
    End Function


    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim popRecord As GenericBankRecord

        popRecord = New GenericBankRecord()
        popRecord.Amount = bankRecord.Amount
        popRecord.IssueDate = bankRecord.IssueDate
        popRecord.AccountNumber = bankRecord.AccountNumber
        popRecord.CheckNumber = bankRecord.CheckNumber
        popRecord.Payee = bankRecord.Payee

        BankRecords.Add(popRecord)
    End Sub
End Class
