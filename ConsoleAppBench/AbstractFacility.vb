Imports System.Configuration
Imports BankFileLibrary
Public MustInherit Class AbstractFacility
    Implements IFacility



    Public Sub New(facility As String)
        _facilityName = facility
    End Sub

    Dim _facilityName As String

    Public Property FacilityName As String Implements IFacility.FacilityName
        Get
            Return _facilityName
        End Get
        Set(value As String)
            _facilityName = value
        End Set
    End Property

    Dim _bankName As String
    Public Property BankName As String Implements IFacility.BankName
        Get
            Return _bankName
        End Get
        Set(value As String)
            _bankName = value
        End Set
    End Property

    Dim _accountNumber As String
    Public Property AccountNumber As String Implements IFacility.AccountNumber
        Get
            Return _accountNumber
        End Get
        Set(value As String)
            _accountNumber = value
        End Set
    End Property

    Dim _dateString As String
    ''' <summary>
    ''' Tends to come in the form of dd-MMM-YYYY. We'll convert it in the Set 
    ''' to convert it to YYYYMMDD
    ''' </summary>
    ''' <returns></returns>
    Public Property DateString As String Implements IFacility.DateString
        Get
            Return _dateString
        End Get
        Set(value As String)
            _dateString = Utilities.YYYMMDDString(value)
        End Set
    End Property

    Dim _outputFileName As String
    Public Property OutputFileName As String Implements IFacility.OutputFileName
        Get
            If String.IsNullOrEmpty(_outputFileName) Then
                SetOutputToCsv()
            End If
            Return _outputFileName
        End Get
        Set(value As String)
            _outputFileName = value
        End Set
    End Property

    Public Sub SetOutputToCsv()

        '    _outputFileName = ConfigurationManager.AppSettings("BankUploadFolder") + "\_" +
        '                FacilityName + "_" + BankName + "_PayPositive_" +
        '                Utilities.TimeStampForFileName(DateTime.Now) + ".csv"

        '        _outputFileName = "C:\Users\ykenner\Desktop\BankUploadFolder" & "\" & Utilities.TimeStampForFileName(DateTime.Now)
        _outputFileName = My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Utilities.TimeStampForFileName(DateTime.Now) & ".txt"

    End Sub

    Public MustOverride Function CreateBankFile(fileName As String) As Integer Implements IFacility.CreateBankFile

End Class
