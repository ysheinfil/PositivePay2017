Imports BankFileLibrary
Imports System.IO

Public Class CitiFacility
    Implements IFacility

    Private _amount As Decimal
    Public Property Amount() As Decimal Implements IFacility.Amount
        Get
            Return _amount
        End Get
        Set(value As Decimal)
            _amount = value
        End Set
    End Property

    Private _bankNumber As String
    Public Property BankNumber() As String
        Get
            Return _bankNumber
        End Get
        Set(ByVal value As String)
            _bankNumber = value
        End Set
    End Property

    Public Property IssueDate As String Implements IFacility.IssueDate
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property IssueType As Char Implements IFacility.IssueType
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Char)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AccountNumber As String Implements IFacility.AccountNumber
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property CheckNumber As Integer Implements IFacility.CheckNumber
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Payee As String Implements IFacility.Payee
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Function CreateBankFile(fileName As String) As Integer Implements IFacility.CreateBankFile
        Dim fi As FileInfo

        fi = New FileInfo(fileName)

        If Not fi.Exists Then
            Throw New FileNotFoundException()
        End If
        Return 0
    End Function
End Class
