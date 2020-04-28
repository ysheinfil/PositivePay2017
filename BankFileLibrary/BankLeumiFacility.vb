Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

''' <summary>
''' Uses AmalgamatedBankRecord in Function CreateBankFile()
''' </summary>
Public Class BankLeumiFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of AmalgamatedRecord)

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Leumi"
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim BLRecord As New AmalgamatedRecord

        BLRecord.IssueType = bankRecord.IssueType
        BLRecord.AbaRoutingNumber = bankRecord.AbaRoutingNumber
        BLRecord.AccountNumber = bankRecord.AccountNumber
        BLRecord.CheckNumber = bankRecord.CheckNumber
        BLRecord.Amount = bankRecord.Amount
        BLRecord.IssueDate = bankRecord.IssueDate
        BLRecord.Payee = bankRecord.Payee

        BankRecords.Add(BLRecord)
    End Sub

    ''' <summary>
    ''' Format CSV
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer

        ReadInputFile(fileName)

        Using writer As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                If (bankRecord.IssueType.ToString.ToUpper.Equals("I")) Then

                    writer.Write("IS" + ",")
                Else
                    writer.Write(bankRecord.IssueType + ",")
                End If

                writer.Write(bankRecord.AbaRoutingNumber + ",")
                writer.Write(bankRecord.AccountNumber + ",")
                writer.Write(bankRecord.CheckNumber + ",")
                writer.Write(bankRecord.Amount)
                writer.Write("," + Utilities.MMDDYYString(bankRecord.IssueDate))
                writer.Write("," + bankRecord.Payee)
                writer.WriteLine()
            Next
        End Using
        Return BankRecords.Count
    End Function

End Class
