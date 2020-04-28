Imports System.Configuration
Imports System.IO
Imports BankFileLibrary
Imports log4net
Imports Microsoft.VisualBasic.FileIO

Public MustInherit Class AbstractFacility
    Implements IFacility

    Public Sub New(facility As String)
        _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        _facilityName = facility
        SetOutputToCsv()
    End Sub

    Private _facilityName As String
    Public Property FacilityName As String Implements IFacility.FacilityName
        Get
            Return _facilityName
        End Get
        Set(value As String)
            _facilityName = value
        End Set
    End Property

    Private _bankName As String
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

    Private _dateString As String
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


    Private _outputCopyFile As String
    Private _outputFileName As String
    Public Property OutputFileName As String Implements IFacility.OutputFileName
        Get
            Return _outputFileName
        End Get
        Set(value As String)
            _outputFileName = value
        End Set
    End Property

    Private _checks As Integer
    Public Property Checks As Integer Implements IFacility.Checks
        Get
            Return _checks
        End Get
        Set(value As Integer)
            _checks = value
        End Set
    End Property

    Private Shared _log As ILog
    Protected ReadOnly Property Log As ILog
        Get
            Return _log
        End Get
    End Property


    ''' <summary>
    ''' Target folder is whatever is in app.config.
    ''' facilityName_Date.csv
    ''' </summary>
    Protected Sub SetOutputToCsv()
        If String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("BankUploadFolder")) Then
            _outputFileName = "c:\testdata\test_" + FacilityName + "_" + Utilities.TimeStampForFileName(DateTime.Now) + ".csv"
            _outputCopyFile = _outputFileName.Replace(".csv", "_Copy.csv")
        Else
            _outputFileName = ConfigurationManager.AppSettings("BankUploadFolder") + "\" +
                    FacilityName + "_" + Utilities.TimeStampForFileName(DateTime.Now) + ".csv"
            _outputCopyFile = ConfigurationManager.AppSettings("FolderToWatch") + "\processed\output\" +
            FacilityName + "_" + Utilities.TimeStampForFileName(DateTime.Now) + ".csv"
        End If

    End Sub

    ''' <summary>
    ''' Same as SetOutpuToCsv but uses .txt extension
    ''' </summary>
    Protected Sub SetOutputToTxt()
        SetOutputToCsv()
        _outputFileName = _outputFileName.Replace(".csv", ".txt")
        _outputCopyFile = _outputCopyFile.Replace(".csv", ".txt")
    End Sub

    Public Sub CopyOutput() Implements IFacility.CopyOutput
        FileSystem.CopyFile(OutputFileName, _outputCopyFile)
    End Sub

    ''' <summary>
    ''' Declares file as Delimited by commas with text (sometimes) enclosed in quotes
    ''' </summary>
    ''' <param name="inputFile"></param>
    Protected Shared Sub StartOffInputFile(inputFile As TextFieldParser)
        inputFile.TextFieldType = FieldType.Delimited
        inputFile.SetDelimiters(",")
        inputFile.HasFieldsEnclosedInQuotes = True

        ' First line is a header - with field names
        inputFile.ReadFields()
    End Sub


    ''' <summary>
    ''' Reads the following from the SAP File:
    ''' Account #, Amount, Check #, Date and Payee
    ''' </summary>
    ''' <param name="fileName"></param>
    Protected Sub ReadInputFile(fileName As String)
        Dim bankRecord As GenericBankRecord

        Using inputFile As New TextFieldParser(fileName)
            StartOffInputFile(inputFile)
            Try
                While inputFile.EndOfData = False
                    Dim fields = inputFile.ReadFields()
                    bankRecord = New GenericBankRecord With {
                        .AbaRoutingNumber = fields(12),
                        .AccountNumber = fields(0),
                        .Amount = fields(2),
                        .CheckNumber = fields(4),
                        .IssueDate = fields(5),
                        .IssueType = fields(10),
                        .Payee = fields(9)
                    }
                    ProcessFields(bankRecord, fields)
                End While
            Catch ex As Exception
                Log.Error("Reading " + BankName + " Bank Facility", ex)
            End Try
        End Using

    End Sub

    ''' <summary>
    ''' Override in Facility sub classes to handle other fields that are not covered in ReadInputFile
    ''' </summary>
    ''' <param name="bankRecord"></param>
    ''' <param name="fields"></param>
    Protected Overridable Sub ProcessFields(bankRecord As GenericBankRecord, fields() As String)

    End Sub


    Public MustOverride Function CreateBankFile(fileName As String) As Integer Implements IFacility.CreateBankFile
End Class
