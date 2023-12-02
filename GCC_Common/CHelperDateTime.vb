Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class CHelperDateTime
    Public Enum DateInterval
        Second
        Minute
        Hour
        Day
        Week
        Month
        Quarter
        Year
    End Enum

    Public Shared Function DateDiff(ByVal Interval As DateInterval, ByVal StartDate As System.DateTime, ByVal EndDate As System.DateTime) As Long
        Dim lngDateDiffValue As Long = 0
        Dim TS As New System.TimeSpan(EndDate.Ticks - StartDate.Ticks)
        Select Case Interval
            Case DateInterval.Day
                lngDateDiffValue = CLng(TS.Days)
                Exit Select
            Case DateInterval.Hour
                lngDateDiffValue = CLng(TS.TotalHours)
                Exit Select
            Case DateInterval.Minute
                lngDateDiffValue = CLng(TS.TotalMinutes)
                Exit Select
            Case DateInterval.Month
                lngDateDiffValue = CLng(TS.Days / 30)
                Exit Select
            Case DateInterval.Quarter
                lngDateDiffValue = CLng((TS.Days / 30) / 3)
                Exit Select
            Case DateInterval.Second
                lngDateDiffValue = CLng(TS.TotalSeconds)
                Exit Select
            Case DateInterval.Week
                lngDateDiffValue = CLng(TS.Days / 7)
                Exit Select
            Case DateInterval.Year
                lngDateDiffValue = CLng(TS.Days / 365)
                Exit Select
        End Select
        Return (lngDateDiffValue)
    End Function

    Public Shared Function ConvertToDateTime(ByVal valueFecha As String) As DateTime
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("es-PE", True)
        Dim dFecha As DateTime = Convert.ToDateTime(valueFecha, format)

        Return dFecha
    End Function

    Public Shared Function FormatFecha(ByVal vFecha As String) As String
        Dim fecha As String = ""
        fecha = vFecha.Replace("-", "")
        If vFecha <> "" Then
            fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4)
        End If
        Return fecha
    End Function

    ' Pasa un DateTime de C# a DD/MM/YYYY
    ' no valen DateTime nulos (simplemente porque no hay DateTime nulos)
    Public Shared Function GetDateTimeAsDDMMYYYY(ByVal pidtValue As DateTime) As String
        Dim sDDMMYYYY As String, sDay As String, sMonth As String, sYear As String

        sDay = Convert.ToString(pidtValue.Day)
        sDay = sDay.PadLeft(2, Convert.ToChar("0"))

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sDDMMYYYY = CFunciones.fConcatenar(sDay, "/", sMonth, "/", sYear)

        Return (sDDMMYYYY)
    End Function

    ' Pasa un DateTime de C# a DD/MM/YYYY HH:MM:SS
    ' no valen DateTime nulos (simplemente porque no hay DateTime nulos)
    Public Shared Function GetDateTimeAsDDMMYYYY_HHMMSS(ByVal pidtValue As DateTime) As String
        Dim sDDMMYYYY_HHMMSS As String, sDay As String, sMonth As String, sYear As String, sHour As String, sMinute As String, _
         sSecond As String

        sDay = Convert.ToString(pidtValue.Day)
        sDay = sDay.PadLeft(2, Convert.ToChar("0"))

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sHour = Convert.ToString(pidtValue.Hour)
        sHour = sHour.PadLeft(2, Convert.ToChar("0"))

        sMinute = Convert.ToString(pidtValue.Minute)
        sMinute = sMinute.PadLeft(2, Convert.ToChar("0"))

        sSecond = Convert.ToString(pidtValue.Second)
        sSecond = sSecond.PadLeft(2, Convert.ToChar("0"))

        sDDMMYYYY_HHMMSS = CFunciones.fConcatenar(sDay, "/", sMonth, "/", sYear, " ", sHour, ":", sMinute, ":", sSecond)

        Return (sDDMMYYYY_HHMMSS)
    End Function

    ' Pasa un DateTime de C# a MM/DD/YYYY HH:MM:SS
    ' no valen DateTime nulos (simplemente porque no hay DateTime nulos)
    Public Shared Function GetDateTimeAsMMDDYYYY_HHMMSS(ByVal pidtValue As DateTime) As String
        Dim sMMDDYYYY_HHMMSS As String, sDay As String, sMonth As String, sYear As String, sHour As String, sMinute As String, _
         sSecond As String

        sDay = Convert.ToString(pidtValue.Day)
        sDay = sDay.PadLeft(2, Convert.ToChar("0"))

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sHour = Convert.ToString(pidtValue.Hour)
        sHour = sHour.PadLeft(2, Convert.ToChar("0"))

        sMinute = Convert.ToString(pidtValue.Minute)
        sMinute = sMinute.PadLeft(2, Convert.ToChar("0"))

        sSecond = Convert.ToString(pidtValue.Second)
        sSecond = sSecond.PadLeft(2, Convert.ToChar("0"))

        sMMDDYYYY_HHMMSS = CFunciones.fConcatenar(sMonth, "/", sDay, "/", sYear, " ", sHour, ":", sMinute, ":", sSecond)

        Return (sMMDDYYYY_HHMMSS)
    End Function

    ' Pasa un DateTime de C# a MM/DD/YYYY
    ' no valen DateTime nulos (simplemente porque no hay DateTime nulos)
    Public Shared Function GetDateTimeAsMMDDYYYY(ByVal pidtValue As DateTime) As String
        Dim sMMDDYYYY As String, sDay As String, sMonth As String, sYear As String

        sDay = Convert.ToString(pidtValue.Day)
        sDay = sDay.PadLeft(2, Convert.ToChar("0"))

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sMMDDYYYY = CFunciones.fConcatenar(sMonth, "/", sDay, "/", sYear)

        Return (sMMDDYYYY)
    End Function

    ' Pasa un DateTime de C# a MM/YYYY
    ' no valen DateTime nulos (simplemente porque no hay DateTime nulos)
    Public Shared Function GetDateTimeAsMMYYYY(ByVal pidtValue As DateTime) As String
        Dim sMMYYYY As String, sMonth As String, sYear As String

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sMMYYYY = CFunciones.fConcatenar(sMonth, "/", sYear)

        Return (sMMYYYY)
    End Function

    ' Pasa un DateTime de C# a YYYYMM
    ' no valen DateTime nulos (simplemente porque no hay DateTime nulos)
    Public Shared Function GetDateTimeAsYYYYMM(ByVal pidtValue As DateTime) As String
        Dim sYYYYMM As String, sMonth As String, sYear As String

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sYYYYMM = CFunciones.fConcatenar(sYear + sMonth)

        Return (sYYYYMM)
    End Function

    ' Pasa un string DD/MM/YYYY a DateTime de C#
    ' sólo vale con strings con el formato DD/MM/YYYY, no se mandan horas ni minutos ni segundos
    Public Shared Function GetDDMMYYYYAsDateTime(ByVal pisValue As String) As DateTime
        Dim sDay As String, sMonth As String, sYear As String

        If pisValue.Length <> 10 Then
            Throw New Exception(CFunciones.fConcatenar("La cadena ingresada [", pisValue, "] no tiene el formato correcto DD/MM/YYYY"))
        End If

        sDay = pisValue.Substring(0, 2)
        sMonth = pisValue.Substring(3, 2)
        sYear = pisValue.Substring(6, 4)

        Dim dtValue As New DateTime(Integer.Parse(sYear), Integer.Parse(sMonth), Integer.Parse(sDay))

        Return (dtValue)
    End Function

    ' Recibe un DateTime con valores en hora, minutos y segundos y lo trunca a 00:00:00
    Public Shared Function TruncDateTime(ByVal pidtValue As DateTime) As DateTime
        Dim iDay As Integer, iMonth As Integer, iYear As Integer

        iDay = pidtValue.Day
        iMonth = pidtValue.Month
        iYear = pidtValue.Year

        Dim dtValue As New DateTime(iYear, iMonth, iDay)

        Return (dtValue)

    End Function

    ''' <summary>
    ''' Devuelve una cadena en formato YYYYMMDD, si es una fecha válida.
    ''' Caso contrario devuelve nothing. 
    ''' Si es el valor inicial de tipo fecha (01/01/1990), tambien devuelve nothing.
    ''' </summary>
    ''' <param name="pidtValue">Dato fecha o Nothing</param>
    ''' <returns>String con un dato fecha en formato YYYYMMDD, nothing si no es fecha</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Shared Function FormatDateTimeAsYYYYMMDD(ByVal pidtValue As Nullable(Of DateTime)) As String
        If pidtValue.HasValue Then
            If (pidtValue = New DateTime(1990, 1, 1)) OrElse (pidtValue = Nothing) Then
                Return Nothing
            Else
                Dim sYYYYMMDD As String

                sYYYYMMDD = pidtValue.Value.ToString("yyyyMMdd")

                Return sYYYYMMDD
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function FormatDateTimeAsYYYYMMDD(ByVal pidtValue As DateTime) As String
        Dim sYYYYMMDD As String, sDay As String, sMonth As String, sYear As String

        sDay = Convert.ToString(pidtValue.Day)
        sDay = sDay.PadLeft(2, Convert.ToChar("0"))

        sMonth = Convert.ToString(pidtValue.Month)
        sMonth = sMonth.PadLeft(2, Convert.ToChar("0"))

        sYear = Convert.ToString(pidtValue.Year)

        sYYYYMMDD = CFunciones.fConcatenar(sYear, sMonth, sDay)

        Return (sYYYYMMDD)
    End Function

    Public Shared Function GetFormatMMSS(ByVal cantidad As Int64) As String
        Dim result As String
        If cantidad > 60 Then
            Dim intMinutos As Int64 = Convert.ToInt64(CFunciones.CheckDbl(cantidad / 60, 2))
            Dim intSegundos As Int64 = Convert.ToInt64(cantidad Mod 60)
            result = [String].Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"))
        Else
            result = [String].Format("00:{0}", cantidad.ToString("00"))
        End If
        Return result
    End Function

    Public Shared Function GetFormatHHMMSS(ByVal cantidad As Int64) As String
        Dim result As String
        Dim intHoras As Int64 = 0
        Dim intSegundos As Int64 = 0
        If cantidad >= 3600 Then
            intHoras = Convert.ToInt64(CFunciones.CheckDbl(cantidad / 3600, 2))
            intSegundos = Convert.ToInt64(cantidad Mod 3600)
            result = GetFormatMMSS(CFunciones.CheckInt64(intSegundos))
        Else
            result = GetFormatMMSS(CFunciones.CheckInt64(cantidad))
        End If
        result = CFunciones.fConcatenar(intHoras.ToString("00"), ":", result)
        Return result
    End Function

    Public Shared Function GetFormatHHMMSS24AsHHMMSSAMPM(ByVal isHora As String) As String
        Dim sRetorno As String = ""
        Dim arrDatos As String() = isHora.Split(Convert.ToChar(":"))
        If arrDatos.Length > 1 Then
            If arrDatos.Length > 2 Then
                Dim iHoras As Int64 = CFunciones.CheckInt64(CFunciones.CheckDbl(arrDatos(0), 2))
                Dim iMinutos As Int64 = CFunciones.CheckInt64(CFunciones.CheckDbl(arrDatos(1), 2))
                Dim iSegundos As Int64 = CFunciones.CheckInt64(CFunciones.CheckDbl(arrDatos(2), 2))
                If iHoras > 12 Then
                    iHoras = iHoras - 12
                    sRetorno = [String].Format("{0}:{1}:{2} p.m.", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"))
                Else
                    sRetorno = [String].Format("{0}:{1}:{2} a.m.", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"))
                End If
            Else
                sRetorno = GetFormatMMSSAsHHMMSS(isHora)
            End If
        Else
            sRetorno = isHora
        End If
        Return sRetorno
    End Function

    Public Shared Function GetFormatMMSSAsHHMMSS(ByVal isHora As String) As String
        Dim sRetorno As String = ""
        Dim arrDatos As String() = isHora.Split(Convert.ToChar(":"))
        If arrDatos.Length > 1 Then
            If arrDatos.Length > 2 Then
                sRetorno = isHora
            Else
                Dim iMinutos As Int64 = CFunciones.CheckInt64(CFunciones.CheckDbl(arrDatos(0), 2))
                Dim iSegundos As Int64 = CFunciones.CheckInt64(CFunciones.CheckDbl(arrDatos(1), 2))
                If iMinutos >= 60 Then
                    Dim iHoras As Int64 = Convert.ToInt64(CFunciones.CheckDbl(iMinutos / 60, 2))
                    iMinutos = Convert.ToInt64(iMinutos Mod 60)
                    sRetorno = [String].Format("{0}:{1}:{2}", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"))
                Else
                    sRetorno = CFunciones.fConcatenar("00:", [String].Format("{0}:{1}"), iMinutos.ToString("00"), iSegundos.ToString("00"))
                End If
            End If
        Else
            sRetorno = isHora
        End If
        Return sRetorno
    End Function
End Class
'End Namespace