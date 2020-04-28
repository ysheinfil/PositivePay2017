Imports System.Text.RegularExpressions


Public Class TDBankRecord
    Inherits GenericBankRecord

    Private _transactionType As Char
    ''' <summary>
    ''' Default is 'I'
    ''' </summary>
    ''' <returns></returns>
    Public Property TransactionType As Char
        Get
            Return _transactionType
        End Get
        Set(value As Char)
            Dim sValue = value.ToString()
            If String.IsNullOrWhiteSpace(sValue) Or sValue.Equals(vbNullChar) Then
                _transactionType = "I"
            Else
                _transactionType = value
            End If
        End Set
    End Property

    Public Overrides Property IssueDate As String
        Get
            Return MyBase.IssueDate
        End Get
        Set(value As String)
            If (Regex.IsMatch(value, "^\d{1,2}/\d{1,2}/\\d{4}$")) Then
                _issueDate = value
            Else
                _issueDate = Utilities.AmericanDate(value)
            End If

        End Set
    End Property

End Class
