Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class BHIBankFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of BHIBankRecord)

    Public Sub New(facility As String)
    MyBase.New(facility)
        BankName = "BHI Bank"
        BankRecords = New List(Of BHIBankRecord)
    End Sub

    ''' <summary>
    ''' Format: csv
    ''' Issue, Serial, Account, Amount, Date [MMDDYY], Payee
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer

        ReadInputFile(fileName)

        Using sw As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                sw.Write(Utilities.LeftFillZeros("0" + bankRecord.AccountNumber, 15))
                sw.Write(Utilities.LeftFillZeros(bankRecord.Serial, 10))
                sw.Write(Utilities.LeftFillZeros(bankRecord.IntegerAmount.ToString, 12)) ' .NET is having trouble writing Amount which is 'Decimal' aka Double together with a comma.
                sw.Write(Utilities.YYYMMDDString(bankRecord.IssueDate) + "  ")
                sw.WriteLine(bankRecord.Payee)
            Next
        End Using

        Return BankRecords.Count
    End Function

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim bhiRecord As New BHIBankRecord

        bhiRecord.AccountNumber = bankRecord.AccountNumber
        bhiRecord.CheckNumber = bankRecord.CheckNumber
        bhiRecord.Amount = bankRecord.Amount
        bhiRecord.IssueDate = bankRecord.IssueDate
        bhiRecord.Payee = bankRecord.Payee

        BankRecords.Add(bhiRecord)
    End Sub

End Class
