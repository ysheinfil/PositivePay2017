Imports System.Configuration
Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class TDBankFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of TDBankRecord)

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "TD Bank"
        BankRecords = New List(Of TDBankRecord)
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim tdRecord As New TDBankRecord

        tdRecord.AccountNumber = bankRecord.AccountNumber
        tdRecord.CheckNumber = bankRecord.CheckNumber
        tdRecord.TransactionType = fields(13)
        tdRecord.IssueDate = bankRecord.IssueDate
        tdRecord.Amount = bankRecord.Amount
        tdRecord.Payee = bankRecord.Payee

        BankRecords.Add(tdRecord)
    End Sub
    ''' <summary>
    ''' Format: CSV:
    ''' Account #, CHeck#, Transaction Type, Issue Date, Amount, Payee
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer

        ReadInputFile(fileName)

        SetOutputToTxt()

        Using sw As StreamWriter = New StreamWriter(OutputFileName)

            For Each bankRecord As TDBankRecord In bankRecords
                sw.Write(bankRecord.AccountNumber + ",")
                sw.Write(bankRecord.CheckNumber + ",")
                sw.Write(bankRecord.TransactionType + ",")
                sw.Write(bankRecord.IssueDate + ",")
                sw.Write(Utilities.WithDecimal(bankRecord.Amount)) ' .NET is having trouble writing Amount which is 'Decimal' aka Double together with a comma.
                sw.WriteLine(",""" + bankRecord.Payee + """") ' Payee gets surrounded by double-quotes
            Next
        End Using

        Return bankRecords.Count
    End Function


End Class
