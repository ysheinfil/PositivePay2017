Public Class AmalgamatedRecord
    Inherits GenericBankRecord

    Private _debitCredit As Char
    ''' <summary>
    ''' Optional
    ''' </summary>
    ''' <returns></returns>
    Public Property DebitCredit As Char
        Get
            Return _debitCredit
        End Get
        Set(value As Char)
            Dim ucValue = value.ToString.ToUpper
            If (ucValue.Equals("D") Or ucValue.Equals("C")) Then
                _debitCredit = ucValue
            Else
                _debitCredit = ""
            End If

        End Set
    End Property
End Class
