Public Class KeyBankRecord
    Inherits GenericBankRecord

    Private _void As Char
    ''' <summary>
    ''' "C" means VOID
    ''' </summary>
    ''' <returns></returns>
    Public Property Void As Char
        Get
            Return _void
        End Get
        Set(value As Char)
            If value.ToString.ToUpper.Equals("C") Then
                _void = "C"
            Else
                _void = Space(1)
            End If
        End Set
    End Property

    Private _payeeLine2 As String
    Public Property PayeeLine2 As String
        Get
            Return _payeeLine2
        End Get
        Set(value As String)
            _payeeLine2 = value
        End Set
    End Property

    Sub New()
        _payeeLine2 = String.Empty
    End Sub
End Class
