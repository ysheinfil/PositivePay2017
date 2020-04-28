Public Interface IBankRecord
    Property Amount As Decimal
    Property IssueDate As String
    ''' <summary>
    ''' Normal Or Void. Some Banks allow blank for normal.
    ''' </summary>
    ''' <returns></returns>
    Property IssueType As Char
    Property AccountNumber As String
    Property CheckNumber As String
    Property Payee As String
    Property RoutingNumber As String
End Interface
