Public Class CITIBankRecord
    Inherits GenericBankRecord
    Private Property _RecordType As Char
    Public Property RecordType As Char
        Get
            Return _RecordType
        End Get
        Set(value As Char)
            _RecordType = value
        End Set
    End Property

    Private _CompanyName As String

    Public Property CompanyName As String

        Get
            Return _CompanyName
        End Get
        Set(value As String)
            _CompanyName = value
        End Set
    End Property
    Private _DateofFile As String
    Public Property DateofFile As String

        Get
            Return _DateofFile
        End Get
        Set(value As String)
            _DateofFile = value
        End Set
    End Property

    ' Check Issue Rec
    Private _RecordTypeC As String
    Public Property RecordTypeC As String

        Get
            Return _RecordTypeC
        End Get
        Set(value As String)
            _RecordTypeC = value
        End Set
    End Property
    Private _BankNumber As String
    Public Property BankNumber As String

        Get
            Return _BankNumber
        End Get
        Set(value As String)
            _BankNumber = value
        End Set
    End Property
    Private _AccountNumber As String
    Public Property AccountNumberC As String

        Get
            Return _AccountNumber
        End Get
        Set(value As String)
            _AccountNumber = value
        End Set
    End Property
    Private _VoidCheckIndicators As String
    Public Property VoidCheckIndicators As String

        Get
            Return _VoidCheckIndicators
        End Get
        Set(value As String)
            _VoidCheckIndicators = value
        End Set
    End Property

    Private _CheckAmount As String
    Public Property CheckAmount As String

        Get
            Return _CheckAmount
        End Get
        Set(value As String)
            _CheckAmount = value
        End Set
    End Property
    Private _AdditionalData As String
    Public Property AdditionalData As String

        Get
            Return _AdditionalData
        End Get
        Set(value As String)
            _AdditionalData = value
        End Set
    End Property
    Private _PayeeInfo As String
    Public Property PayeeInfo As String

        Get
            Return _PayeeInfo
        End Get
        Set(value As String)
            _PayeeInfo = value
        End Set
    End Property
    Private _RecordTypeT As String
    Public Property RecordTypeT As String

        Get
            Return _RecordTypeT
        End Get
        Set(value As String)
            _RecordTypeT = value
        End Set
    End Property
    Private _AccountNumberT As String
    Public Property AccountNumberT As String

        Get
            Return _AccountNumberT
        End Get
        Set(value As String)
            _AccountNumberT = value
        End Set
    End Property
    Private _CheckIssuedCount As String
    Public Property CheckIssuedCount As String

        Get
            Return _CheckIssuedCount
        End Get
        Set(value As String)
            _CheckIssuedCount = value
        End Set
    End Property
    Private _TotalDollarAmount As String
    Public Property TotalDollarAmount As String

        Get
            Return _TotalDollarAmount
        End Get
        Set(value As String)
            _TotalDollarAmount = value
        End Set
    End Property
End Class


