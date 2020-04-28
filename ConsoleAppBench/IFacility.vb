Public Interface IFacility
    Property FacilityName As String
    Property BankName As String
    Property AccountNumber As String
    Property DateString As String
    Property OutputFileName As String
    Function CreateBankFile(ByVal fileName As String) As Integer
End Interface

