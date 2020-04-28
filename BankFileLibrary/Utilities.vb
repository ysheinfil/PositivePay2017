Imports System.Text

Public Class Utilities
    ''' <summary>
    ''' Takes Number and returns it as a string, with leading zeros.
    ''' </summary>
    ''' <param name="Number">Value to convert</param>
    ''' <param name="Size">Size of the field</param>
    ''' <returns></returns>
    Public Shared Function IntegerToString(Number As Integer, Size As Integer) As String
        Dim strVal As String
        Dim Retval As String

        strVal = Number.ToString()

        If strVal.Length > Size Then
            Throw New ArgumentOutOfRangeException(NameOf(Number), "Number takes up more than " + Size.ToString + " characters.")
        End If
        Dim filler As StringBuilder
        filler = New StringBuilder()

        filler.Append("0", Size - strVal.Length)

        Retval = filler.ToString + strVal
        Return Retval

    End Function

    ''' <summary>
    ''' Takes Number. Converts to string and removes the decimal and truncates everything after 2 decimal places. Adds leading zeros to fill up to the size parameter.
    ''' e.g. Utilities.DecimalToString(98765.543M, 10) returns 0009876554
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <param name="size"></param>
    ''' <returns></returns>
    Public Shared Function DecimalToString(Number As Decimal, size As Integer) As String
        Dim strVal As String
        Dim Retval As String

        strVal = Math.Truncate(100 * Number).ToString()

        If strVal.Length > size Then
            Throw New ArgumentOutOfRangeException(NameOf(Number), "Number takes up more than " + size.ToString + " characters.")
        End If
        Dim filler As StringBuilder
        filler = New StringBuilder()

        filler.Append("0", size - strVal.Length)

        Retval = filler.ToString + strVal
        Return Retval
    End Function

    ''' <summary>
    ''' If amount does not have a decimal point, it appends amount with '.00' and returns it as a string.
    ''' Otherwise, it returns amount as a string.
    ''' </summary>
    ''' <param name="amount"></param>
    ''' <returns></returns>
    Public Shared Function WithDecimal(amount As Decimal) As String
        Dim TextAmount = amount.ToString
        If Not TextAmount.Contains(".") Then
            TextAmount = TextAmount + ".00"
        End If

        Return TextAmount
    End Function

    Public Shared Function LeftFillZeros(textVal As String, size As Integer) As String
        If textVal.Length > size Then
            Throw New ArgumentOutOfRangeException(NameOf(textVal), " string is too long.")
        End If
        Dim retVal As StringBuilder
        retVal = New StringBuilder()
        retVal.Append("0", size - textVal.Length)
        retVal.Append(textVal)

        Return retVal.ToString
    End Function


    Public Shared Function LeftJustifiedField(textVal As String, size As Integer) As String
        If textVal.Length > size Then
            Throw New ArgumentOutOfRangeException(NameOf(textVal), " string is too long.")
        End If

        Dim retVal As StringBuilder
        retVal = New StringBuilder()
        retVal.Append(textVal)
        retVal.Append(Space(size - textVal.Length))
        Return retVal.ToString
    End Function

    Public Shared Function RightJustifiedField(textVal As String, size As Integer) As String
        If textVal.Length > size Then
            Throw New ArgumentOutOfRangeException(NameOf(textVal), " string is too long.")
        End If

        Dim retVal As StringBuilder
        retVal = New StringBuilder()
        retVal.Append(Space(size - textVal.Length))
        retVal.Append(textVal)
        Return retVal.ToString
    End Function

    Friend Shared Function TimeStampForFileName(thisTime As DateTime) As String
        Dim retVal As New StringBuilder
        retVal.Append(Utilities.YYYMMDDString(thisTime))
        retVal.Append("_")
        If (thisTime.Hour < 10) Then
            retVal.Append("0")
        End If
        retVal.Append(thisTime.Hour.ToString)
        If (thisTime.Minute < 10) Then
            retVal.Append("0")
        End If
        retVal.Append(thisTime.Minute.ToString)
        Return retVal.ToString
    End Function

    ''' <summary>
    ''' Accepts any string that can be parsed by System.DateTime
    ''' and returns YYYYMMDD
    ''' </summary>
    ''' <param name="fDate"></param>
    ''' <returns></returns>
    Public Shared Function YYYMMDDString(fDate As String) As String
        Dim RetVal As String
        Try
            Dim fullDate As Date
            Dim monthPart As String = String.Empty
            Dim dayPart As String = String.Empty

            fullDate = DateTime.Parse(fDate)
            If (fullDate.Month < 10) Then
                monthPart = "0" + fullDate.Month.ToString
            Else
                monthPart = fullDate.Month.ToString
            End If
            If (fullDate.Day < 10) Then
                dayPart = "0" + fullDate.Day.ToString
            Else
                dayPart = fullDate.Day.ToString
            End If
            RetVal = fullDate.Year.ToString + monthPart + dayPart

        Catch fex As FormatException
            RetVal = String.Empty
        End Try

        Return RetVal

    End Function

    Public Shared Function AmericanDate(value As String) As String
        Dim RetVal As String
        Try
            Dim ParsedDate As DateTime = DateTime.Parse(value)
            Dim monthPart As String = String.Empty, dayPart As String = String.Empty

            If (ParsedDate.Month < 10) Then
                monthPart = "0" + ParsedDate.Month.ToString
            Else
                monthPart = ParsedDate.Month.ToString
            End If

            If (ParsedDate.Day < 10) Then
                dayPart = "0" + ParsedDate.Day.ToString
            Else
                dayPart = ParsedDate.Day.ToString
            End If

            RetVal = monthPart + "/" + dayPart + "/" + ParsedDate.Year.ToString
        Catch fex As FormatException
            'LOG AS BAD FORMAT????
            RetVal = String.Empty
        End Try

        Return RetVal
    End Function

    Public Shared Function MMDDYYString(dateString As String) As String
        Dim american As String
        Dim dateParts As List(Of String)

        american = AmericanDate(dateString)
        dateParts = american.Split("/").ToList()
        Return dateParts(0) + dateParts(1) + dateParts(2).Substring(2)

    End Function
End Class
