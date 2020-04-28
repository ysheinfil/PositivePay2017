Imports BankFileLibrary

Module Program

    Sub Main()
        'TryCitiBank()
        'EmailBench.SendMail()
        'PlayWithStringSplit()
        Console.Read()
    End Sub

    Private Sub PlayWithStringSplit()
        Dim parts = "Greet-Balls-of-Fire".Split("-")
        Console.Write("Length - 1 is " + parts(parts.Length - 1))
    End Sub

    Private Sub TryCitiBank()
        Dim facility As CITIBankFacility = New CITIBankFacility("Test Facility")
        facility.BankName = "CitiBank"
        facility.SetOutputToCsv()
        facility.CreateBankFile("*********PUT FILE NAME HERE*********")
    End Sub
End Module
