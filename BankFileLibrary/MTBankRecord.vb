Public Class MTBankRecord
    Inherits GenericBankRecord

    Public Property TransactionId As String
        Get
            Return "0"
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property
    Public Property SerialNumber As String
        Get
            Return CheckNumber
        End Get
        Set(value As String)
            CheckNumber = value
        End Set
    End Property
End Class
