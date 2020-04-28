Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class ValleyBankFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of GenericBankRecord)

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Valley National Bank"
        BankRecords = New List(Of GenericBankRecord)
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim vbRecord As New GenericBankRecord

        vbRecord.AccountNumber = bankRecord.AccountNumber
        vbRecord.CheckNumber = bankRecord.CheckNumber
        vbRecord.Amount = bankRecord.Amount
        vbRecord.IssueDate = bankRecord.IssueDate
        vbRecord.IssueType = bankRecord.IssueType
        vbRecord.Payee = bankRecord.Payee

        BankRecords.Add(vbRecord)
    End Sub

    ''' <summary>
    ''' Format csv
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer
        ReadInputFile(fileName)

        Using sw As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                sw.Write(bankRecord.AccountNumber + ",")
                sw.Write(bankRecord.CheckNumber + ",")
                sw.Write(bankRecord.IntegerAmount) ' .NET is having trouble writing Amount which is 'Decimal' aka Double together with a comma.
                sw.Write("," + bankRecord.IssueDate + ",")
                sw.Write(bankRecord.IssueType + ",")
                sw.WriteLine(bankRecord.Payee)
            Next
        End Using

        Return bankRecords.Count
    End Function

End Class
