Imports BankFileLibrary

Public Class BlankFacility
    Inherits AbstractFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "BLANK"
    End Sub

    Public Overrides Function CreateBankFile(fileName As String) As Integer
        Throw New NotImplementedException()
    End Function
End Class
