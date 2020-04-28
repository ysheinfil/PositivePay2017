Imports System.IO
''' <summary>
''' Similar to Amalgamated
'''     The fields are the same
'''     Assuming the input is the same
'''     The output is in a different order
''' </summary>
Public Class BPOPBankFacility
    Inherits AmalgamatedFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "BPOP"
    End Sub

    Public Overrides Function CreateBankFile(fileName As String) As Integer
        Dim BankRecords As List(Of AmalgamatedRecord)

        BankRecords = ReadInputFile(fileName)

        Using writer As StreamWriter = New StreamWriter(OutputFileName)
            For Each bankRecord In BankRecords
                writer.Write(bankRecord.Amount)
                writer.Write("," + Utilities.YYYMMDDString(bankRecord.IssueDate) + ",")
                writer.Write(bankRecord.CheckNumber + ",")
                writer.Write(bankRecord.Payee + ",")
                writer.Write(bankRecord.AbaRoutingNumber + ",")
                writer.Write(bankRecord.AccountNumber + ",")
                writer.Write(bankRecord.IssueType)
                writer.WriteLine()
            Next
        End Using

        Return BankRecords.Count
    End Function
End Class
