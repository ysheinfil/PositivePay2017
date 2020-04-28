Imports System.Net.Mail

Public Class EmailBench

    Public Shared Sub SendMail()
        Dim mm As New MailMessage("pospayapp@centershealthcare.org", "ysheinfil@centershealthcare.org")
        Dim SMTP As New SmtpClient("192.168.254.220")
        mm.Subject = "New Positive Pay File "
        mm.Body = "THere is a new positive pay file ready for upload."
        SMTP.Send(mm)
    End Sub

End Class
