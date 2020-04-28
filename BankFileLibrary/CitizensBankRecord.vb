Public Class CitizensBankRecord
    Inherits GenericBankRecord

    Public Property RecordType As Char
        Get
            Return IssueType
        End Get
        Set(value As Char)
            IssueType = value
        End Set
    End Property

    Private _additionalData As String
    ''' <summary>
    ''' This field is self managed
    ''' </summary>
    ''' <returns></returns>
    Public Property AdditionalData As String
        Get
            If String.IsNullOrEmpty(_additionalData) Then
                Return Space(15)
            Else
                Return _additionalData
            End If
        End Get
        Set(value As String)
            _additionalData = value
        End Set
    End Property

    Private _payeeAdditionalData As String
    ''' <summary>
    ''' THis field is Self Managed
    ''' </summary>
    ''' <returns></returns>
    Public Property PayeeAdditionalData As String
        Get
            If String.IsNullOrEmpty(_payeeAdditionalData) Then
                Return Space(60)
            Else
                Return _payeeAdditionalData
            End If
        End Get
        Set(value As String)
            _payeeAdditionalData = value
        End Set
    End Property

    Private _stopReason As String
    Public Property StopReason() As String
        Get
            Return _stopReason
        End Get
        Set(ByVal value As String)
            _stopReason = value
        End Set
    End Property

End Class
