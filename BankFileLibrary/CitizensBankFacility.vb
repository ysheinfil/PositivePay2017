Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class CitizensBankFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of CitizensBankRecord)

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Citizens Bank"
        BankRecords = New List(Of CitizensBankRecord)
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim ctznRecord As New CitizensBankRecord

        ctznRecord.AccountNumber = bankRecord.AccountNumber
        ctznRecord.CheckNumber = bankRecord.CheckNumber
        ctznRecord.Amount = bankRecord.Amount
        ctznRecord.IssueDate = bankRecord.IssueDate
        ctznRecord.RecordType = bankRecord.IssueType ' or Action?
        ctznRecord.Payee = bankRecord.Payee
        ctznRecord.StopReason = String.Empty
        BankRecords.Add(ctznRecord)
    End Sub

    ''' <summary>
    ''' Format: Fixed Length
    ''' Account Number....
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer
        ReadInputFile(fileName)

        Using sw As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord As CitizensBankRecord In BankRecords
                sw.Write(Utilities.LeftJustifiedField(bankRecord.AccountNumber, 10))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.CheckNumber, 10))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.Amount, 10))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.IssueDate, 6))
                sw.Write(bankRecord.RecordType)
                sw.Write(bankRecord.AdditionalData) 'Self managed!
                sw.Write(Utilities.LeftJustifiedField(bankRecord.Payee, 60))
                sw.Write(bankRecord.PayeeAdditionalData) 'Self managed!
                sw.Write(Utilities.LeftJustifiedField(bankRecord.StopReason, 8))
            Next
        End Using

        Return BankRecords.Count
    End Function

End Class
