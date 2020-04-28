Namespace BankFileCreator

    Public Class FacilityFactory
        Private Shared log As log4net.ILog

        Public Shared Function ProcessFile(ByVal fileName As String) As Integer
            log = log4net.LogManager.GetLogger(GetType(BankFileCreator.FacilityFactory))

            Dim RetVal As Integer

            RetVal = 0
            log.Info("ProcessFile is returning " + RetVal)
            Return RetVal
        End Function


    End Class
End Namespace

