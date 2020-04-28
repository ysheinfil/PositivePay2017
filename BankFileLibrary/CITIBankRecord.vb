Public Class CITIBankRecord
    Inherits GenericBankRecord

    Public Property IssueVoid As String

    Private _AdditionalData As String
    Public Property AdditionalData As String
        Get
            If (String.IsNullOrEmpty(_AdditionalData)) Then
                Return String.Empty
            Else
                Return _AdditionalData
            End If
        End Get
        Set(value As String)
            _AdditionalData = value
        End Set
    End Property

End Class


