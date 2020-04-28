Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class HuntingtonBankFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of GenericBankRecord)

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Huntington"
        BankRecords = New List(Of GenericBankRecord)
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim hRecord As New GenericBankRecord

        hRecord.CheckNumber = bankRecord.CheckNumber
        hRecord.Amount = bankRecord.Amount
        hRecord.IssueDate = bankRecord.IssueDate
        hRecord.IssueType = bankRecord.IssueType

        BankRecords.Add(hRecord)

    End Sub

    ''' <summary>
    ''' Format: Fixed Field 
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer

        ReadInputFile(fileName)

        SetOutputToTxt()
        Using sw As StreamWriter = New StreamWriter(OutputFileName)
            'Write header
            sw.WriteLine("H" + AccountNumber)

            For Each bankRecord As GenericBankRecord In BankRecords
                sw.Write("D  " + Utilities.RightJustifiedField(bankRecord.CheckNumber, 10))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.Amount.ToString, 12))
                sw.Write(bankRecord.IssueDate)
                sw.Write(bankRecord.IssueType)
                sw.WriteLine("S")
            Next
        End Using

        Return BankRecords.Count
    End Function

End Class
