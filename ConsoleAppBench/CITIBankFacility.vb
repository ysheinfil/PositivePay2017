Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class CITIBankFacility
    Inherits AbstractFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "CITI Bank"
    End Sub

    ''' <summary>
    ''' Format: CSV:
    ''' Account #, CHeck#, Transaction Type, Issue Date, Amount, Payee
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' 

    Public Overrides Function CreateBankFile(fileName As String) As Integer

        Dim Total As Integer
        Dim FirstUnderscore As Integer
        Dim companyname As String
        Dim bankRecords As List(Of CITIBankRecord)
        Dim iCtr As Integer = 0
        bankRecords = ReadFileIntoArray(fileName)
        SetOutputToCsv()
        Using sw As StreamWriter =
            New StreamWriter(OutputFileName)
            sw.Write("H")
            FirstUnderscore = InStr(fileName, "_")
            companyname = fileName.Substring(0, FirstUnderscore)
            sw.Write(companyname)
            sw.Write(Now)
            sw.Write(InsertBlanks(41))
            For Each bankRecordCITI As CITIBankRecord In bankRecords
                sw.Write(bankRecordCITI.RecordTypeC)
                sw.Write(bankRecordCITI.BankNumber)
                sw.Write(Utilities.IntegerToString(10, Len(bankRecordCITI.AccountNumberC)) & bankRecordCITI.AccountNumberC)
                sw.Write(Space(7))
                sw.Write(bankRecordCITI.VoidCheckIndicators)
                sw.Write(Utilities.IntegerToString(10, Len(bankRecordCITI.CheckNumber)) & bankRecordCITI.CheckNumber)
                sw.Write(bankRecordCITI.CheckAmount)
                sw.Write(Utilities.IntegerToString(10, Len(bankRecordCITI.CheckAmount)) & bankRecordCITI.CheckAmount)
                sw.Write(bankRecordCITI.IssueDate)
                sw.Write(bankRecordCITI.AdditionalData)
                sw.Write(Space(15))
                sw.Write(bankRecordCITI.PayeeInfo)
                iCtr += 1
            Next
            sw.Write("T")
            sw.Write(bankRecords(0).BankNumber)
            sw.Write(Utilities.IntegerToString(10, Len(bankRecords(0).AccountNumberC)) & bankRecords(0).AccountNumberC)
            sw.Write(Space(8))
            sw.Write(Utilities.IntegerToString(10, Len(iCtr)) & iCtr)
            sw.Write(Total)
            sw.Write(Space(38))
        End Using

        Return bankRecords.Count

    End Function

    Private Function InsertBlanks(Amount) As String

        Dim InsertBlanks1 As String = String.Empty
        For i = 0 To Amount
            InsertBlanks1 = InsertBlanks1 & " "
        Next

        InsertBlanks = InsertBlanks1

    End Function

    'Private Function ReadFileIntoArray(fileName As String) As List(Of CITIBankRecord)

    '    Dim currentRow As String()
    '    Dim tfp As New TextFieldParser(fileName)
    '    tfp.TextFieldType = FieldType.Delimited
    '    tfp.SetDelimiters(",")

    '    ' First line is a header - with field names

    '    tfp.ReadFields()
    '    Dim bankRecordCITI As CITIBankRecord
    '    Dim CITIBankRecords As New List(Of CITIBankRecord)
    '    While Not tfp.EndOfData
    '        currentRow = tfp.ReadFields()
    '        bankRecordCITI = New CITIBankRecord With
    '            {
    '            .RecordType = currentRow(0),
    '           .CompanyName = currentRow(0),
    '       .DateofFile = currentRow(0),
    '            .RecordTypeC = currentRow(0),
    '            .BankNumber = currentRow(0),
    '             .AccountNumberC = currentRow(0),
    '            .VoidCheckIndicators = currentRow(0),
    '            .CheckAmount = currentRow(0),
    '            .AdditionalData = currentRow(0),
    '            .PayeeInfo = currentRow(0),
    '            .RecordTypeT = currentRow(0),
    '            .AccountNumberT = currentRow(0),
    '              .CheckIssuedCount = currentRow(0),
    '              .TotalDollarAmount = currentRow(0)
    '        }

    '        CITIBankRecords.Add(bankRecordCITI)

    '    End While
    '    tfp.Close()

    '    Return CITIBankRecords



    'End Function
    Private Function ReadFileIntoArray(fileName As String) As List(Of CITIBankRecord)

        Dim currentRow As String()
        Dim tfp As New TextFieldParser(fileName)
        tfp.TextFieldType = FieldType.Delimited
        tfp.SetDelimiters(",")

        ' First line is a header - with field names

        tfp.ReadFields()
        Dim bankRecordCITI As CITIBankRecord
        Dim CITIBankRecords As New List(Of CITIBankRecord)
        While Not tfp.EndOfData
            currentRow = tfp.ReadFields()
            bankRecordCITI = New CITIBankRecord With
                {
                .BankNumber = currentRow(0),
                .AccountNumberC = currentRow(1),
                .VoidCheckIndicators = currentRow(5),
                .CheckNumber = (2),
                .CheckAmount = currentRow(4),
                .IssueDate = currentRow(3),
                .PayeeInfo = currentRow(6)
            }

            CITIBankRecords.Add(bankRecordCITI)

        End While
        tfp.Close()

        Return CITIBankRecords



    End Function

End Class

