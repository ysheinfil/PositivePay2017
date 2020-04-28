Imports System.Text.RegularExpressions

Public Class GenericBankRecord
    Implements IBankRecord
    Private _amount As Decimal
    Public Overridable Property Amount() As Decimal Implements IBankRecord.Amount
        Get
            Return _amount
        End Get
        Set(value As Decimal)
            If (value > 0) Then
                _amount = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Removes the decimal from Amount, so if Amount = 123.45, it will give you 12345
    ''' </summary>
    ''' <returns></returns>
    Public Property IntegerAmount As Integer
        Get
            Return Math.Truncate(_amount * 100)
        End Get
        Set(value As Integer)
            _amount = value / 100
        End Set
    End Property

    Protected _issueDate As String
    Public Overridable Property IssueDate() As String Implements IBankRecord.IssueDate
        Get
            Return _issueDate
        End Get
        Set(ByVal value As String)
            _issueDate = value
        End Set
    End Property

    Private _abaRouting As String
    Public Property AbaRoutingNumber() As String Implements IBankRecord.RoutingNumber
        Get
            Return _abaRouting
        End Get
        Set(ByVal value As String)
            _abaRouting = value
        End Set
    End Property

    Private _issueAction As Char
    ''' <summary>
    ''' 'A' for Add, 'D' for Delete
    ''' </summary>
    ''' <returns></returns>
    Public Property IssueAction() As Char
        Get
            Return _issueAction
        End Get
        Set(ByVal value As Char)
            If (value.ToString.ToUpper.Equals("D")) Then
                _issueAction = "D"
            Else
                _issueAction = "A"
            End If
        End Set
    End Property

    Private _issueType As Char
    ''' <summary>
    ''' Precheck is done.
    ''' Default is I.
    ''' </summary>
    ''' <returns></returns>
    Public Property IssueType As Char Implements IBankRecord.IssueType
        Get
            Return _issueType
        End Get
        Set(value As Char)
            If (value.ToString.ToUpper.Equals("V")) Then
                _issueType = "V"
            Else
                _issueType = "I"
            End If
        End Set
    End Property

    Private _accountNumber As String
    ''' <summary>
    ''' Precheck is done.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property AccountNumber As String Implements IBankRecord.AccountNumber
        Get
            Return _accountNumber
        End Get
        Set(value As String)
            If (Regex.IsMatch(value, "^\d+$") And value.Length <= 17) Then
                _accountNumber = value
            End If
        End Set
    End Property

    Private _checkNo As Integer
    ''' <summary>
    ''' Precheck is done.
    ''' </summary>
    ''' <returns></returns>
    Public Property CheckNumber As String Implements IBankRecord.CheckNumber
        Get
            Return _checkNo
        End Get
        Set(value As String)
            If (Regex.IsMatch(value, "^\d+$") And value.Length <= 15) Then
                _checkNo = value
            End If
        End Set
    End Property

    Private _payee As String
    Public Property Payee As String Implements IBankRecord.Payee
        Get
            Return _payee
        End Get
        Set(value As String)
            _payee = value
        End Set
    End Property
End Class
