Imports BankFileLibrary

Friend Class BoAFacility
    Inherits AbstractFacility

    Public Sub New(facility As String)
        MyBase.New(facility)
        BankName = "Bank of America"
    End Sub

    Public Overrides Function CreateBankFile(fileName As String) As Integer
        Throw New NotImplementedException()
    End Function
End Class
