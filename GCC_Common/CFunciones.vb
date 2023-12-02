Imports System
Imports System.Text.RegularExpressions
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Runtime.Serialization.Formatters.Binary

Public Class CFunciones

    Public Shared Function fConcatenar(ByVal ParamArray cadenas() As String) As String
        Dim sb As New System.Text.StringBuilder()
        Dim cad As String
        For Each cad In cadenas
            sb.Append(cad)
        Next
        Return sb.ToString
    End Function

    Public Shared Function CheckStr(ByVal pobjValue As Object) As String
        Dim strSalida As String = String.Empty

        Dim salida As String = ""
        If pobjValue IsNot Nothing OrElse pobjValue IsNot System.DBNull.Value Then
            strSalida = pobjValue.ToString()
        End If
        Return strSalida.Trim()
    End Function

    Public Shared Function CheckInt64(ByVal pobjValue As Object) As Int64
        Dim intSalida As Int64 = 0
        If pobjValue IsNot DBNull.Value OrElse pobjValue IsNot Nothing Then
            Int64.TryParse(pobjValue.ToString, intSalida)
        End If
        Return intSalida
    End Function

    Public Shared Function CheckLong(ByVal pobjValue As Object) As Long
        Dim lngSalida As Long = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Long.TryParse(pobjValue.ToString, lngSalida)
        End If
        Return lngSalida
    End Function

    Public Shared Function CheckShort(ByVal pobjValue As Object) As Short
        Dim shtSalida As Short = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Short.TryParse(pobjValue.ToString, shtSalida)
        End If
        Return shtSalida
    End Function

    Public Shared Function CheckFloat(ByVal pobjValue As Object) As Single
        Dim sglSalida As Single = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Single.TryParse(pobjValue.ToString, sglSalida)
        End If
        Return sglSalida
    End Function

    Public Shared Function CheckInt(ByVal pobjValue As Object) As Integer
        Dim intSalida As Integer = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Integer.TryParse(pobjValue.ToString, intSalida)
        End If
        Return intSalida
    End Function

    Public Shared Function CheckBoolean(ByVal pobjValue As Object) As Boolean
        Dim boolSalida As Boolean = False
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Boolean.TryParse(pobjValue.ToString, boolSalida)
        End If
        Return boolSalida
    End Function

    Public Shared Function CheckDbl(ByVal pobjValue As Object) As Double
        Dim dblSalida As Double = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Double.TryParse(pobjValue.ToString, dblSalida)
        End If
        Return dblSalida
    End Function

    Public Shared Function CheckDecimal(ByVal pobjValue As Object) As Decimal
        Dim decSalida As Decimal = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Decimal.TryParse(pobjValue.ToString, decSalida)
        End If
        Return decSalida
    End Function

    Public Shared Function CheckDbl(ByVal pobjValue As Object, ByVal nroDecimales As Integer) As Double
        Dim dblSalida As Double = 0
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            Double.TryParse(pobjValue.ToString, dblSalida)
        End If
        Return redondearMontos(dblSalida, 2)
    End Function

    Public Shared Function redondearMontos(ByVal value As Double, ByVal nroDecimales As Integer) As Double
        Return Math.Round(value, nroDecimales)
    End Function

    Public Shared Function CheckDate(ByVal pobjValue As Object) As DateTime
        Dim dtmSalida As DateTime
        If pobjValue IsNot DBNull.Value Or pobjValue IsNot Nothing Then
            DateTime.TryParse(pobjValue.ToString, dtmSalida)
        End If
        Return dtmSalida
    End Function

    Public Shared Function CheckDefaultDate(ByVal pobjValue As Object) As DateTime
        Dim dtmSalida As DateTime
        If pobjValue Is DBNull.Value Or pobjValue Is Nothing Then
            dtmSalida = Convert.ToDateTime("01/01/1900")
        Else
            DateTime.TryParse(pobjValue.ToString, dtmSalida)
        End If
        Return dtmSalida
    End Function

    Public Shared Function ComparaNumeroMenorIgualQue(ByVal NumeroMenor As Decimal, ByVal NumeroMayor As Decimal) As Boolean
        If NumeroMenor <= NumeroMayor Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function ComparaNumeroMenorIgualQue(ByVal NumeroMenor As Integer, ByVal NumeroMayor As Integer) As Boolean
        If NumeroMenor <= NumeroMayor Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function ComparaNumeroMayorQue(ByVal NumeroMenor As Decimal, ByVal NumeroMayor As Decimal) As Boolean
        If NumeroMayor > NumeroMenor Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function ComparaNumeroMayorQue(ByVal NumeroMenor As Integer, ByVal NumeroMayor As Integer) As Boolean
        If NumeroMayor > NumeroMenor Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function ObtenerNumerosRandom(ByVal NumeroCaracteres As Integer) As String
        Dim cadena As String = ""
        Dim rando As New Random()
        For i As Integer = 0 To NumeroCaracteres - 1
            cadena = String.Concat(cadena, rando.[Next](9).ToString())
        Next
        Return cadena
    End Function

    Public Shared Function ToDataTable(ByVal alist As ArrayList) As System.Data.DataTable
        Dim dt As New System.Data.DataTable()
        If alist(0) Is Nothing Then
            Throw New FormatException("Arraylist Vacio")
        End If
        dt.TableName = alist(0).[GetType]().Name
        Dim dr As System.Data.DataRow
        Dim propInfo As System.Reflection.PropertyInfo() = alist(0).[GetType]().GetProperties()

        For i As Integer = 0 To propInfo.Length - 1
            Dim ColType As Type = If(Nullable.GetUnderlyingType(propInfo(i).PropertyType), propInfo(i).PropertyType)
            'dt.Columns.Add(propInfo(i).Name, propInfo(i).PropertyType)
            dt.Columns.Add(propInfo(i).Name, ColType)
        Next
        For row As Integer = 0 To alist.Count - 1
            dr = dt.NewRow()
            For i As Integer = 0 To propInfo.Length - 1
                Dim tempObject As Object = alist(row)
                Dim t As Object = propInfo(i).GetValue(tempObject, Nothing)
                If t IsNot Nothing Then
                    dr(i) = t.ToString()
                End If
            Next
            dt.Rows.Add(dr)
        Next
        Return dt
    End Function

#Region " Métodos de Serialización / Deserealización "


    ''' Para Deserializar un objeto arraylist directamente
    Public Shared Function DeserializeArrayList(Of T)(ByVal xml As String) As ArrayList

        'Variable ArrayList a retornar
        Dim arrRetorno As New ArrayList()

        'Deserializamos el Array XML obtenido para obtener un verdadero ArrayList
        If Not xml.Equals("") Then

            Dim arrListado As ArrayList = DeserializeObject(Of ArrayList)(xml)
            'Verificamos que el arraylist obtenido no sea nulo
            If arrListado IsNot Nothing Then
                'Verificamos que el arraylist obtenido tenga datos
                If arrListado.Count > 0 Then

                    Dim oItem As T = Nothing
                    For Each item As String In arrListado
                        'Deserializamos cada item del listado al objeto correspondiente
                        oItem = DeserializeObject(Of T)(item)
                        'Se agrega el objeto deserializado al arraylist de retorno
                        arrRetorno.Add(oItem)
                    Next
                End If
            End If
        End If

        Return arrRetorno

    End Function

    ''' Para Serializar un objeto ArrayList directamente
    Public Shared Function SerializeArrayList(Of T)(ByVal arrArray As ArrayList) As String

        'Declaramos la variable string de retorno
        Dim strXMLRetorno As String = ""

        'Definimos un ArrayList que contendrán los objetos serializados del ArrayList recibido
        Dim arrListado As New ArrayList()

        'Definimos la variable temporal que recibirá los objetos serializados para pasarlos
        'al ArrayList de objetos serializados
        Dim strXML As String = ""

        'Verificamos que el ArrayList original no se nulo
        If arrArray IsNot Nothing Then

            'Verificamos que el ArrayList original tenga datos
            If arrArray.Count > 0 Then

                'Recorremos el ArrayList original, obteniendo los objetos del tipo correspondiente
                For Each item As T In arrArray

                    'Serializamos el objeto y lo almacenamos en la variable temporal
                    strXML = SerializeObject(Of T)(item)

                    'Agregamos el objeto serializado al ArrayList de objetos serializados
                    arrListado.Add(strXML)

                Next

                'Finalmente serializamos el ArrayList de objetos serializados
                strXMLRetorno = SerializeObject(Of ArrayList)(arrListado)

            End If
        End If

        Return strXMLRetorno

    End Function

    ''' Serealizar un objecto a una cadena XML
    Public Shared Function SerializeObject(Of T)(ByVal obj As T) As String

        Dim osb As StringBuilder = Nothing

        Try

            Dim bytes() As Byte = ToBinary(obj)
            osb = New StringBuilder()
            For Each b As Byte In bytes
                osb.Append(b)
                osb.Append("|")
            Next

            osb.Remove(osb.Length - 1, 1)

            Return osb.ToString
        Catch
            Return String.Empty
        Finally
            If Not osb Is Nothing Then
                osb = Nothing
            End If
        End Try

    End Function

    ''' Recosntruir un objecto desde una Cadena XML
    Public Shared Function DeserializeObject(Of T)(ByVal xml As String) As T

        Dim cads() As String = xml.Split("|"c)
        Dim bytes(cads.Length - 1) As Byte

        For i As Integer = 0 To cads.Length - 1
            bytes(i) = Convert.ToByte(cads(i))
        Next

        Return DirectCast(FromBinary(bytes), T)

    End Function

    Private Shared Function FromBinary(ByVal buffer() As Byte) As Object

        Dim serializationStream As MemoryStream = New MemoryStream(buffer)
        Dim formatter As BinaryFormatter = New BinaryFormatter()

        serializationStream.Position = 0
        Dim obj As Object = formatter.Deserialize(serializationStream)

        serializationStream.Dispose()
        serializationStream = Nothing
        formatter = Nothing

        Return obj

    End Function

    Private Shared Function ToBinary(ByVal o As Object) As Byte()

        Dim formatter As System.Runtime.Serialization.Formatters.Binary.BinaryFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim serializationStream As MemoryStream = New MemoryStream()
        formatter.Serialize(serializationStream, o)
        Dim buffer() As Byte = serializationStream.ToArray()
        serializationStream.Dispose()
        serializationStream = Nothing
        formatter = Nothing
        Return buffer

    End Function

#End Region

#Region "Modulo11 para validar RUC"

    ''' <summary>
    ''' Modulo11 para validar RUC
    ''' </summary>
    ''' <param name="cRUC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Modulo11(ByVal cRUC As String) As Boolean
        Try

            Dim dig01 As Integer = CInt(cRUC.Substring(0, 1)) * 5
            Dim dig02 As Integer = CInt(cRUC.Substring(1, 1)) * 4
            Dim dig03 As Integer = CInt(cRUC.Substring(2, 1)) * 3
            Dim dig04 As Integer = CInt(cRUC.Substring(3, 1)) * 2
            Dim dig05 As Integer = CInt(cRUC.Substring(4, 1)) * 7
            Dim dig06 As Integer = CInt(cRUC.Substring(5, 1)) * 6
            Dim dig07 As Integer = CInt(cRUC.Substring(6, 1)) * 5
            Dim dig08 As Integer = CInt(cRUC.Substring(7, 1)) * 4
            Dim dig09 As Integer = CInt(cRUC.Substring(8, 1)) * 3
            Dim dig10 As Integer = CInt(cRUC.Substring(9, 1)) * 2
            Dim dig11 As Integer = CInt(cRUC.Substring(10, 1))

            Dim suma As Integer = dig01 + dig02 + dig03 + dig04 + dig05 + dig06 + dig07 + dig08 + dig09 + dig10
            Dim residuo As Integer = suma Mod 11
            Dim resta As Integer = 11 - residuo

            Dim digChk As Integer
            If resta = 10 Then
                digChk = 0
            ElseIf resta = 11 Then
                digChk = 1
            Else
                digChk = resta
            End If

            If dig11 = digChk Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
