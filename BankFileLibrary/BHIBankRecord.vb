Public Class BHIBankRecord
    Inherits GenericBankRecord

    Private _issue As Char

    Public Property Issue As Char
        Get
            Return _issue
        End Get
        Set(value As Char)
            If value.ToString.ToUpper.Equals("V") Then
                _issue = "V"
            Else
                _issue = "0"
            End If
        End Set
    End Property

    Public Property Serial As String
        Get
            Return CheckNumber

        End Get
        Set(value As String)
            CheckNumber = value
        End Set
    End Property

    Public Overrides Property AccountNumber As String
        Get
            If (Not MyBase.AccountNumber.StartsWith("0")) Then
                Return "0" + MyBase.AccountNumber
            Else
                Return MyBase.AccountNumber
            End If
        End Get
        Set(value As String)
            MyBase.AccountNumber = value
        End Set
    End Property
End Class
