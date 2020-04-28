Imports System.IO
Imports BankFileLibrary
Imports Microsoft.VisualBasic.FileIO

Public Class KeyBankFacility
    Inherits AbstractFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Key Bank"
        BankRecords = New List(Of KeyBankRecord)

    End Sub

    Public Property BankRecords As List(Of KeyBankRecord)

    Public Overrides Function CreateBankFile(fileName As String) As Integer
        Dim sw As StreamWriter
        ReadInputFile(fileName)

        SetOutputToTxt()

        sw = New StreamWriter(OutputFileName)
        Try
            For Each bankRecord In BankRecords
                sw.Write("  " + Utilities.LeftFillZeros(bankRecord.AccountNumber, 15))
                sw.Write(Utilities.LeftFillZeros(bankRecord.CheckNumber, 10))
                sw.Write(Utilities.YYYMMDDString(bankRecord.IssueDate))
                sw.Write(Utilities.DecimalToString(bankRecord.Amount, 10))
                sw.Write(bankRecord.Void)
                sw.Write(Space(15))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.Payee, 75))
                sw.Write(Utilities.LeftJustifiedField(bankRecord.PayeeLine2, 75))
                sw.WriteLine(Space(9))
            Next
        Catch ex As Exception
            Log.Error("Writing bank file", ex)
        Finally
            sw.Close()
        End Try

        Return bankRecords.Count
    End Function

    Protected Overrides Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)
        Dim keyRecord As new KeyBankRecord

        keyRecord.AccountNumber = bankRecord.AccountNumber
        keyRecord.CheckNumber = bankRecord.CheckNumber
        keyRecord.IssueDate = bankRecord.IssueDate
        keyRecord.Amount = bankRecord.Amount
        keyRecord.Payee = bankRecord.Payee
        keyRecord.PayeeLine2 = String.Empty

        If (fields(8).ToString().ToUpper().Equals("C")) Then
            keyRecord.Void = "C"
        Else
            keyRecord.Void = " "
        End If

        BankRecords.Add(keyRecord)
    End Sub

End Class
