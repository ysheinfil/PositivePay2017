
Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class FacilityFactory

    Public Shared Log As log4net.ILog

    Public Shared Function ProcessFile(ByVal fullFileName As String) As IFacility
        Log.Debug("Parsing filename to determine facility and bank...")
        Dim bankName As String = Nothing
        Dim acctNo As String = Nothing
        Dim facilityName As String
        Dim facility As IFacility

        ParseFileName(fullFileName, bankName, acctNo, facilityName)

        Select Case bankName.ToLower
            Case "amalgamated"
                facility = New AmalgamatedFacility(facilityName)
            Case "banco popular"
                facility = New PopularBankFacility(facilityName)
                facility.BankName = "Banco Popular"
            Case "bank of america"
                facility = New BoAFacility(facilityName)

            Case "bhi"
                facility = New BHIBankFacility(facilityName)
            Case "citibank", "citi" ' or "citi bank"
                facility = New CITIBankFacility(facilityName)

            Case "citizens bank"
                facility = New CitizensBankFacility(facilityName)

            Case "huntington"
                facility = New HuntingtonBankFacility(facilityName)

            Case "leumi"
                facility = New BankLeumiFacility(facilityName)

            Case "key bank"
                facility = New KeyBankFacility(facilityName)

            Case "metropolitan"
                facility = New MTBankFacility(facilityName)
                facility.BankName = "Metropolitan"

            Case "mt bank long"
                facility = New MTBankFacilityLongAcct(facilityName)

            Case "m&t"
                facility = New MTBankFacility(facilityName)

            Case "td bank"
                facility = New TDBankFacility(facilityName)

            Case "valley"
                facility = New ValleyBankFacility(facilityName)

            Case Else
                facility = New BlankFacility("BLANK")
        End Select

        Log.Debug("Log determined to be " + facility.BankName)
        facility.FacilityName = facilityName
        facility.AccountNumber = acctNo

        Try
            facility.Checks = facility.CreateBankFile(fullFileName)
            facility.CopyOutput()

        Catch ex As Exception
            Log.Fatal(ex.Message)
        End Try

        Return facility
    End Function


    Public Shared Sub ParseFileName(fullFileName As String, ByRef bankName As String, ByRef acctNo As String, ByRef facilityName As String)
        Dim facilityBankParts As String()
        Dim filenameParts As String() = Nothing
        Dim fileName = fullFileName.Substring(fullFileName.LastIndexOf("\") + 1)
        filenameParts = fileName.Split("_")
        facilityName = filenameParts(0)
        facilityBankParts = filenameParts(1).Split("-")
        Log.Info("Facility:" + facilityName)

        If (facilityBankParts.Count > 2) Then
            Log.Info("Facility Code: " + facilityBankParts(0))
            bankName = facilityBankParts(facilityBankParts.Length - 2)
            acctNo = facilityBankParts(facilityBankParts.Length - 1)
        Else
            bankName = facilityBankParts(0)
            acctNo = facilityBankParts(1)
        End If
        Log.Info("Bank: " + bankName)
    End Sub
End Class

