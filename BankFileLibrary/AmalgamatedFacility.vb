Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class AmalgamatedFacility
    Inherits AbstractFacility

    Public Property BankRecords As List(Of AmalgamatedRecord)

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Amalgamated"
    End Sub

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim ARecord As New AmalgamatedRecord

        ARecord.Amount = bankRecord.Amount
        ARecord.IssueDate = bankRecord.IssueDate
        ARecord.AbaRoutingNumber = bankRecord.AbaRoutingNumber
        ARecord.AccountNumber = bankRecord.AccountNumber
        ARecord.CheckNumber = bankRecord.CheckNumber
        ARecord.IssueType = bankRecord.IssueType
        ARecord.IssueAction = bankRecord.IssueAction
        ARecord.Payee = bankRecord.Payee

        BankRecords.Add(ARecord)
    End Sub

    ''' <summary>
    ''' File format is:
    ''' AMOUNT, ISSUE DATE, ABA/TRC, ACCOUNT, CHECK#, ISSUE TYPE, , PAYEE
    ''' Also used by PopularBankFacility(Banco Popular)
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Overrides Function CreateBankFile(fileName As String) As Integer
        Dim BankRecords As List(Of AmalgamatedRecord)

        BankRecords = ReadInputFile(fileName)

        SetOutputToTxt()

        Using writer As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                writer.Write(bankRecord.Amount)
                writer.Write("," + Utilities.YYYMMDDString(bankRecord.IssueDate) + ",")
                writer.Write(bankRecord.AbaRoutingNumber + ",")
                writer.Write(bankRecord.AccountNumber + ",")
                writer.Write(bankRecord.CheckNumber + ",")
                writer.Write(bankRecord.IssueType + ",,")
                writer.Write(bankRecord.IssueAction + ",")
                writer.Write(bankRecord.Payee)
                writer.WriteLine()
            Next
        End Using

        Checks = BankRecords.Count
        Return BankRecords.Count
    End Function

    Protected Function ReadInputFile(fileName As String) As List(Of AmalgamatedRecord)
        Dim bankRecord As AmalgamatedRecord
        Dim bankRecords As List(Of AmalgamatedRecord)
        Dim inputFile As New TextFieldParser(fileName)
        inputFile.Delimiters = New String() {","}
        inputFile.TextFieldType = FieldType.Delimited

        bankRecords = New List(Of AmalgamatedRecord)

        inputFile.ReadFields()

        While inputFile.EndOfData = False
            Dim fields = inputFile.ReadFields()
            bankRecord = New AmalgamatedRecord With {
                .AbaRoutingNumber = fields(0), 'May be hard coded as opposed to 'per record'
                .AccountNumber = fields(1), 'May be hard coded as opposed to 'per record'
                .Amount = fields(3),
                .CheckNumber = fields(2),
                .IssueAction = fields(6),
                .IssueDate = fields(4),
                .IssueType = fields(5),
                .Payee = fields(9)
            }
            bankRecords.Add(bankRecord)
        End While
        inputFile.Close()
        Return bankRecords
    End Function
End Class
