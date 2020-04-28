Public Interface IFacility
    Property FacilityName As String
    Property BankName As String
    Property AccountNumber As String
    Property DateString As String
    Property OutputFileName As String
    Property Checks As Integer
    Function CreateBankFile(ByVal fileName As String) As Integer
    Sub CopyOutput()
End Interface

