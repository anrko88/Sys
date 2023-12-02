
'Imports GCC.LogicWS
Imports GCC.Entity
Imports System.Collections.Generic
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Data
Imports System.IO

Imports Microsoft.VisualBasic
Imports GCC.LogicWS

Namespace GCC.UI

    Public Class GCCUtilitario

#Region " Métodos de Serialización / Deserealización "

        ''' <summary>
        ''' UTF8ByteArrayToString
        ''' </summary>    
        ''' <remarks>
        ''' Para convertir un array de Bytes de valores Unicode (UTF-8 encoded) a una cadena completa
        ''' </remarks>
        Private Shared Function UTF8ByteArrayToString(ByVal characters As Byte()) As String
            Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding()
            Dim constructedString As String = encoding.GetString(characters)

            Return (constructedString)
        End Function

        ''' <summary>
        ''' UTF8ByteArrayToString
        ''' </summary>    
        ''' <remarks>
        ''' Convertir la cadena a un array de Bytes de valores Unicode (UTF-8 encoded) y esta se usa en la deserialización
        ''' </remarks>
        Private Shared Function StringToUTF8ByteArray(ByVal pXmlString As String) As Byte()
            Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding()
            Dim byteArray As Byte() = encoding.GetBytes(pXmlString)

            Return byteArray
        End Function

        ''' <summary>
        ''' fstrConvertirDataTableAXML
        ''' </summary>    
        ''' <remarks>
        ''' Convertit un datatable a un xml para pasarlo como parametro directo hacia la BD. El datatable a convertir
        ''' </remarks>
        Public Shared Function fstrConvertirDataTableAXML(ByVal pdtbBuild As DataTable) As String

            Dim dsBuildSQL As New DataSet()
            Dim sbSQL As StringBuilder
            Dim swSQL As StringWriter
            Dim XMLformat As String
            Try

                sbSQL = New StringBuilder()
                swSQL = New StringWriter(sbSQL)

                dsBuildSQL.Merge(pdtbBuild, True, MissingSchemaAction.AddWithKey)
                dsBuildSQL.Tables(0).TableName = "Table"

                dsBuildSQL.WriteXml(swSQL, XmlWriteMode.IgnoreSchema)
                XMLformat = sbSQL.ToString()
            Catch
                Return String.Empty
            Finally
                dsBuildSQL = Nothing
            End Try
            Return XMLformat
        End Function

        ''' <summary>
        ''' DeserializeArrayList
        ''' </summary>    
        ''' <remarks>
        ''' Para Deserializar un objeto arraylist directamente
        ''' </remarks>
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

        ''' <summary>
        ''' SerializeArrayList
        ''' </summary>    
        ''' <remarks>
        ''' Para Serializar un objeto ArrayList directamente
        ''' </remarks>
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

        ''' <summary>
        ''' SerializeObject
        ''' </summary>    
        ''' <remarks>
        ''' Serealizar un objecto a una cadena XML
        ''' </remarks>
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

        ''' <summary>
        ''' DeserializeObject
        ''' </summary>    
        ''' <remarks>
        ''' Recosntruir un objecto desde una Cadena XML
        ''' </remarks>
        Public Shared Function DeserializeObject(Of T)(ByVal xml As String) As T
            Dim cads() As String = xml.Split("|"c)
            Dim bytes(cads.Length - 1) As Byte

            For i As Integer = 0 To cads.Length - 1
                bytes(i) = Convert.ToByte(cads(i))
            Next

            Return DirectCast(FromBinary(bytes), T)
        End Function

        ''' <summary>
        ''' DeserializeObject
        ''' </summary>    
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' ToBinary
        ''' </summary>    
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' ToDataTable
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function ToDataTable(ByVal alist As ArrayList) As System.Data.DataTable
            Dim dt As New System.Data.DataTable()
            If alist(0) Is Nothing Then
                Throw New FormatException("Arraylist Vacio")
            End If
            dt.TableName = alist(0).[GetType]().Name
            Dim dr As System.Data.DataRow
            Dim propInfo As System.Reflection.PropertyInfo() = alist(0).[GetType]().GetProperties()

            For i As Integer = 0 To propInfo.Length - 1
                Dim colType As Type = propInfo(i).PropertyType

                If (colType.IsGenericType) AndAlso (colType.GetGenericTypeDefinition() Is GetType(Nullable(Of ))) Then
                    colType = colType.GetGenericArguments()(0)
                End If

                dt.Columns.Add(New DataColumn(propInfo(i).Name, colType))
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

        ''' Serealizar un objecto a una cadena XML
        Public Shared Function SerializeObjectXML(Of T)(ByVal obj As T) As String
            Try
                Dim xmlString As String = Nothing
                Dim memoryStream As New System.IO.MemoryStream()
                Dim xs As New System.Xml.Serialization.XmlSerializer(GetType(T))
                Dim xmlTextWriter As New System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.UTF8)
                xs.Serialize(xmlTextWriter, obj)
                memoryStream = DirectCast(xmlTextWriter.BaseStream, System.IO.MemoryStream)
                xmlString = UTF8ByteArrayToString(memoryStream.ToArray())
                Return xmlString
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        ''' Recosntruir un objecto desde una Cadena XML
        Public Shared Function DeserializeObjectXML(Of T)(ByVal xml As String) As T
            Dim xs As New System.Xml.Serialization.XmlSerializer(GetType(T))
            Dim memoryStream As New System.IO.MemoryStream(StringToUTF8ByteArray(xml))
            Dim xmlTextWriter As New System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.UTF8)
            Return DirectCast(xs.Deserialize(memoryStream), T)
        End Function

#End Region

#Region " Métodos de Serialización / Deserealización WBC"
        ''' Para Deserializar un objeto arraylist directamente
        Public Shared Function DeserializeArrayList2(Of T)(ByVal xml As String) As ArrayList
            'Variable ArrayList a retornar

            Dim arrRetorno As New ArrayList

            'Deserializamos el Array XML obtenido para obtener un verdadero ArrayList
            If Not String.IsNullOrEmpty(xml) Then
                Dim arrListado As ArrayList = DeserializeObject2(Of ArrayList)(xml)

                'Verificamos que el arraylist obtenido no sea nulo
                If Not arrListado Is Nothing Then
                    'Verificamos que el arraylist obtenido tenga datos
                    If arrListado.Count > 0 Then
                        Dim oItem As T = Nothing
                        For Each item As String In arrListado
                            'Deserializamos cada item del listado al objeto correspondiente
                            oItem = DeserializeObject2(Of T)(item)
                            'Se agrega el objeto deserializado al arraylist de retorno
                            arrRetorno.Add(oItem)
                        Next
                    End If
                End If
            End If
            Return arrRetorno
        End Function

        ''' Para Serializar un objeto ArrayList directamente
        Public Shared Function SerializeArrayList2(Of T)(ByVal arrArray As ArrayList) As String
            'Declaramos la variable string de retorno
            Dim strXMLRetorno As String = ""

            'Definimos un ArrayList que contendrán los objetos serializados del ArrayList recibido
            Dim arrListado As New ArrayList
            'Definimos la variable temporal que recibirá los objetos serializados para pasarlos
            'al ArrayList de objetos serializados
            Dim strXML As String = ""

            'Verificamos que el ArrayList original no se nulo
            If Not arrArray Is Nothing Then
                'Verificamos que el ArrayList original tenga datos
                If arrArray.Count > 0 Then
                    'Recorremos el ArrayList original, obteniendo los objetos del tipo correspondiente
                    For Each item As T In arrArray
                        'Serializamos el objeto y lo almacenamos en la variable temporal
                        strXML = SerializeObject2(Of T)(item)
                        'Agregamos el objeto serializado al ArrayList de objetos serializados
                        arrListado.Add(strXML)
                    Next

                    'Finalmente serializamos el ArrayList de objetos serializados
                    strXMLRetorno = SerializeObject2(Of ArrayList)(arrListado)
                End If
            End If

            Return strXMLRetorno
        End Function

        ''' Serealizar un objecto a una cadena XML
        Public Shared Function SerializeObject2(Of T)(ByVal obj As T) As String
            Try
                Dim xmlString As String = Nothing
                Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(T))
                Dim xmlTextWriter As System.Xml.XmlTextWriter = New System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.UTF8)
                xs.Serialize(xmlTextWriter, obj)
                memoryStream = CType(xmlTextWriter.BaseStream, System.IO.MemoryStream)
                xmlString = UTF8ByteArrayToString(memoryStream.ToArray())

                Return xmlString
            Catch
                Return String.Empty
            End Try
        End Function

        ''' Recosntruir un objecto desde una Cadena XML
        Public Shared Function DeserializeObject2(Of T)(ByVal xml As String) As T
            Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(T))
            Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream(StringToUTF8ByteArray(xml))

            Return CType(xs.Deserialize(memoryStream), T)
        End Function


#End Region

#Region " Encriptar/Desencriptar "

        ''' <summary>
        ''' EncriptarTexto
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function EncriptarTexto(ByVal EncriptString As String) As String
            Return Convert.ToBase64String(StringToUTF8ByteArray(EncriptString))
        End Function

        ''' <summary>
        ''' DesencriptarTexto
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function DesencriptarTexto(ByVal TextEncripted As String) As String
            Return UTF8ByteArrayToString(Convert.FromBase64String(TextEncripted))
        End Function

#End Region

#Region " Combos "

        ''' <summary>
        ''' pInsertarPrimerItem
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub pInsertarPrimerItem(ByRef pDDlCombo As DropDownList, ByVal pstrTexto As String, ByVal pstrValor As String)
            pDDlCombo.Items.Insert(0, New ListItem(pstrTexto, pstrValor))
        End Sub

        ''' <summary>
        ''' pBorrarItem
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub pBorrarItem(ByRef pDDlCombo As DropDownList, ByVal pintCodProducto As Integer)
            Dim ItemSeleccionado As ListItem = pDDlCombo.Items.FindByValue(pintCodProducto)
            If ItemSeleccionado IsNot Nothing Then
                pDDlCombo.Items.RemoveAt(pDDlCombo.Items.IndexOf(ItemSeleccionado))
            End If
        End Sub

        ''' <summary>
        ''' pCargarDropDownList
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub pCargarDropDownList(ByRef pDDlCombo As DropDownList, ByVal pObjDataSource As Object, ByVal pstrDataTextField As String, ByVal pstrDataValueField As String, ByVal pstrPrimerItemTexto As String, ByVal pstrPrimerItemValor As String)
            pDDlCombo.DataSource = pObjDataSource
            pDDlCombo.DataTextField = pstrDataTextField
            pDDlCombo.DataValueField = pstrDataValueField
            pDDlCombo.DataBind()

            pInsertarPrimerItem(pDDlCombo, pstrPrimerItemTexto, pstrPrimerItemValor)
        End Sub

        ''' <summary>
        ''' CargaValorCombo
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub CargaValorCombo(ByVal page As Page, ByVal type As Type, ByVal pstrNombre As String, ByVal pstrCombo As String, ByVal pstrValor As String)
            page.ClientScript.RegisterStartupScript(type, pstrNombre, "fn_util_SeteaComboServidor('" + pstrCombo + "','" + pstrValor + "');", True)
        End Sub

        Public Shared Sub SeleccionaCombo(ByVal pobjCombo As HtmlSelect, ByVal pstrValor As String)
            For i As Integer = 0 To pobjCombo.Items.Count - 1
                If pobjCombo.Items(i).Value.Trim.Equals(pstrValor) Then
                    pobjCombo.Items(i).Selected = True
                End If
            Next i
        End Sub

#End Region

#Region " Obtener Valor del Web.Config "

        ''' <summary>
        ''' fstrObtieneKeyWebConfig
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function fstrObtieneKeyWebConfig(ByVal strKey As String) As String
            Return ConfigurationManager.AppSettings(strKey)
        End Function

#End Region

#Region " Emitir mensajes "

        ''' <summary>
        ''' Show
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub ShowLoad(ByVal message As String, ByVal CurrentPage As Page)
            Dim strMensaje As String = message.Replace("'", "\'")
            strMensaje = strMensaje.Replace(Microsoft.VisualBasic.vbCr, "\n").Replace(Microsoft.VisualBasic.vbLf, "\n")
            CurrentPage.RegisterStartupScript(Guid.NewGuid().ToString(), String.Format("<script>alert('{0}');</script>", strMensaje))
        End Sub

        ''' <summary>
        ''' Show
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub Show(ByVal message As String, ByVal CurrentPage As Page)
            Dim strMensaje As String = message.Replace("'", "\'")
            strMensaje = strMensaje.Replace(Microsoft.VisualBasic.vbCr, "\n").Replace(Microsoft.VisualBasic.vbLf, "\n")
            CurrentPage.RegisterStartupScript(Guid.NewGuid().ToString(), String.Format("<script>parent.fn_mdl_mensajeError('{0}');</script>", strMensaje))
        End Sub

        ''' <summary>
        ''' ConfirmButton
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub ConfirmButton(ByVal btn As Button, Optional ByVal param As String = "")
            btn.Attributes.Add("onclick", "return confirm(mstrSeguroRealizarAccion);" + param + "")
        End Sub

        ''' <summary>
        ''' ShowMensaje
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub ShowMensaje(ByVal message As String, ByVal CurrentPage As Page)
            Dim strMensaje As String = message.Replace("'", "\'")
            strMensaje = strMensaje.Replace(Microsoft.VisualBasic.vbCr, "\n").Replace(Microsoft.VisualBasic.vbLf, "\n")
            CurrentPage.RegisterStartupScript(Guid.NewGuid().ToString(), String.Format("<script>fn_alert('{0}');</script>", strMensaje))
        End Sub

        ''' <summary>
        ''' ShowError
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub ShowError(ByVal message As String, ByVal CurrentPage As Page)
            Dim strMensaje As String = message.Replace("'", "\'")
            strMensaje = strMensaje.Replace(Microsoft.VisualBasic.vbCr, "\n").Replace(Microsoft.VisualBasic.vbLf, "\n")
            CurrentPage.RegisterStartupScript(Guid.NewGuid().ToString(), String.Format("<script>parent.fn_alertError('{0}');</script>", strMensaje))
        End Sub

        ''' <summary>
        ''' ShowMensaje
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub ShowMensaje(ByVal message As String, ByVal updPanel As UpdatePanel)
            Dim strMensaje As String = message.Replace("'", "\'")
            strMensaje = strMensaje.Replace(Microsoft.VisualBasic.vbCr, "\n").Replace(Microsoft.VisualBasic.vbLf, "\n")
            ScriptManager.RegisterStartupScript(updPanel, updPanel.GetType, Guid.NewGuid.ToString, String.Format("fn_alert('{0}');", strMensaje), True)
        End Sub

        ''' <summary>
        ''' ShowError
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub ShowError(ByVal message As String, ByVal updPanel As UpdatePanel)
            Dim strMensaje As String = message.Replace("'", "\'")
            strMensaje = strMensaje.Replace(Microsoft.VisualBasic.vbCr, "\n").Replace(Microsoft.VisualBasic.vbLf, "\n")
            ScriptManager.RegisterStartupScript(updPanel, updPanel.GetType, Guid.NewGuid.ToString, String.Format("fn_alertError('{0}');", strMensaje), True)
        End Sub
#End Region

#Region " Conversores de Tipos de Datos "

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
        Public Shared Function ToStringyyyyMMdd(ByVal pidtValue As Nullable(Of DateTime)) As String
            Try
                If pidtValue.HasValue Then
                    If (pidtValue.Value = New DateTime(1990, 1, 1)) OrElse (pidtValue.Value = Nothing) Then
                        Return Nothing
                    Else
                        Dim sYYYYMMDD As String

                        sYYYYMMDD = pidtValue.Value.ToString("yyyyMMdd")

                        Return sYYYYMMDD
                    End If
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Devuelve una cadena en formato YYYYMMDD, si es una fecha válida.
        ''' Caso contrario devuelve nothing. 
        ''' Si es el valor inicial de tipo fecha (01/01/1990), tambien devuelve nothing.
        ''' </summary>
        ''' <param name="pidtValue">Dato fecha o Nothing</param>
        ''' <param name="valorDevuelto">Valor devuelto en caso sea una fecha inválida</param>
        ''' <returns>String con un dato fecha en formato YYYYMMDD, nothing si no es fecha</returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 07/05/2012
        ''' </remarks>
        Public Shared Function ToStringyyyyMMdd(ByVal pidtValue As Nullable(Of DateTime), _
                                                ByVal valorDevuelto As String) As String
            Try
                If pidtValue.HasValue Then
                    If (pidtValue.Value = New DateTime(1990, 1, 1)) OrElse (pidtValue.Value = Nothing) Then
                        Return valorDevuelto
                    Else
                        Dim sYYYYMMDD As String

                        sYYYYMMDD = pidtValue.Value.ToString("yyyyMMdd")

                        Return sYYYYMMDD
                    End If
                Else
                    Return valorDevuelto
                End If
            Catch ex As Exception
                Return valorDevuelto
            End Try
        End Function

        ''' <summary>
        ''' Devuelve una cadena en formato YYYYMMDD, si es una fecha válida.
        ''' Caso contrario devuelve nothing. 
        ''' Si es el valor inicial de tipo fecha (01/01/1990), tambien devuelve nothing.
        ''' </summary>
        ''' <param name="pFecha">String con un dato fecha o Nothing</param>
        ''' <param name="valorDevuelto">Valor devuelto en caso sea una fecha inválida</param>
        ''' <returns>String con un dato fecha en formato YYYYMMDD, nothing si no es fecha</returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 07/05/2012
        ''' </remarks>
        Public Shared Function ToStringyyyyMMdd(ByVal pFecha As String, _
                                                ByVal valorDevuelto As String) As String
            Try
                If String.IsNullOrEmpty(pFecha) Then
                    Return valorDevuelto
                Else
                    Return CDate(pFecha).ToString("yyyyMMdd")
                End If
            Catch ex As Exception
                Return valorDevuelto
            End Try
        End Function

        ''' <summary>
        ''' Devuelve una cadena en formato YYYYMMDD, si es una fecha válida.
        ''' Caso contrario devuelve nothing. 
        ''' Si es el valor inicial de tipo fecha (01/01/1990), tambien devuelve nothing.
        ''' </summary>
        ''' <param name="pFecha">String con un dato fecha o Nothing</param>
        ''' <returns>String con un dato fecha en formato YYYYMMDD, nothing si no es fecha</returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 07/05/2012
        ''' </remarks>
        Public Shared Function ToStringyyyyMMdd(ByVal pFecha As String) As String
            Try
                If String.IsNullOrEmpty(pFecha) Then
                    Return Nothing
                Else
                    Return CDate(pFecha).ToString("yyyyMMdd")
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Devuelve una cadena en formato DD/MM/YYYY, si es una fecha válida.
        ''' Caso contrario devuelve vacio. 
        ''' Si es el valor inicial de tipo fecha (01/01/1990), tambien devuelve vacio.
        ''' </summary>
        ''' <param name="pFecha">String con un dato fecha o Nothing</param>
        ''' <returns>String con un dato fecha en formato DD/MM/YYYY, vacio si no es fecha</returns>
        ''' <remarks>
        ''' Creado Por         : TSF - WCR
        ''' Fecha de Creación  : 11/12/2012
        ''' </remarks>
        Public Shared Function fSetearFecha(ByVal pFecha As DateTime) As String
            If pFecha.ToShortDateString = "01/01/1900" Then Return ""
            If pFecha.ToShortDateString = "01/01/0001" Then Return ""
            If pFecha.ToShortDateString = "1/1/0001" Then Return ""
            If pFecha.ToShortDateString = "1/1/1900" Then Return ""
            Return pFecha.ToString("dd/MM/yyyy")
        End Function

        ''' <summary>
        ''' Devuelve un valor Nothing cuando la cadena es de longitud 0 (cero), en otro
        ''' caso devuelve la cadena.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 08/05/2012
        ''' </remarks>
        Public Shared Function NullableString(ByVal value As String) As String
            Dim salida As String

            If (String.IsNullOrEmpty(value)) Then
                salida = Nothing
            Else
                salida = value
            End If

            Return salida
        End Function

        ''' <summary>
        ''' Devuelve un valor Nothing cuando la cadena es de longitud 0 (cero), en otro
        ''' caso devuelve la cadena.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 08/05/2012
        ''' </remarks>
        Public Shared Function NullableStringCombo(ByVal value As String) As String
            Dim salida As String

            If (String.IsNullOrEmpty(value)) Then
                salida = Nothing
            Else
                If value.Trim.Equals("0") Then
                    salida = Nothing
                Else
                    salida = value
                End If

            End If

            Return salida
        End Function

        ''' <summary>
        ''' Convierte un String con un DateTime válido a tipo DateTime.
        ''' Caso contrario devuelve Nothing.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 16/04/2012
        ''' </remarks>
        Public Shared Function StringToDateTime(ByVal value As String) As Nullable(Of DateTime)
            Dim salida As Nullable(Of DateTime)

            Try
                If String.IsNullOrEmpty(value) Then
                    salida = Nothing
                Else
                    salida = DateTime.ParseExact(value, "yyyyMMdd", Nothing)
                End If

                Return salida
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Convierte un String con un Integer válido a tipo Integer.
        ''' Caso contrario devuelve Nothing.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 16/04/2012
        ''' </remarks>
        Public Shared Function StringToInteger(ByVal value As String) As Nullable(Of Integer)
            Dim salida As Nullable(Of Integer)

            Try
                If String.IsNullOrEmpty(value) Then
                    salida = Nothing
                Else
                    If IsNumeric(value) Then
                        salida = CInt(value)
                    Else
                        salida = Nothing
                    End If
                End If

                Return salida
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Convierte un String con un decimal válido a tipo decimal.
        ''' Caso contrario devuelve Nothing.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 16/04/2012
        ''' </remarks>
        Public Shared Function StringToDecimal(ByVal value As String) As Nullable(Of Decimal)
            Dim salida As Nullable(Of Decimal)

            If String.IsNullOrEmpty(value) Then
                salida = Nothing
            Else
                salida = value.Replace(",", "")
                salida = CheckDecimal(value)
            End If

            Return salida
        End Function

        ''' <summary>
        ''' CheckDate
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckDate(ByVal value As Object) As DateTime
            Dim salida As DateTime
            If value Is Nothing Or IsDBNull(value) Then
                salida = Nothing 'DateTime.Now
            ElseIf value.ToString = "" Then
                salida = Nothing 'DateTime.Now
            Else
                salida = Convert.ToDateTime(value)
            End If

            Return salida

        End Function

        ''' <summary>
        ''' CheckStr
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckStr(ByVal value As Object) As String
            Dim salida As String = ""
            If value Is Nothing Or IsDBNull(value) Then
                salida = ""
            Else
                salida = value.ToString()
            End If
            Return salida.Trim()

        End Function

        ''' <summary>
        ''' CheckInt64
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckInt64(ByVal value As Object) As Int64
            Dim salida As Int64 = 0
            If value Is Nothing Or IsDBNull(value) Then
                salida = 0
            Else
                If Convert.ToString(value) = "" Then
                    salida = 0
                Else
                    salida = Convert.ToInt64(value)
                End If
            End If

            Return salida

        End Function

        ''' <summary>
        ''' CheckFloat
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckFloat(ByVal value As Object) As Single
            Dim salida As Integer = 0
            If value Is Nothing Or IsDBNull(value) Then
                salida = 0
            Else
                If Convert.ToString(value) = "" Then
                    salida = 0
                Else
                    salida = Convert.ToInt32(value)
                End If
            End If

            Return salida

        End Function

        ''' <summary>
        ''' CheckInt
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckInt(ByVal value As Object) As Integer
            Dim salida As Integer = 0
            If value Is Nothing Or IsDBNull(value) Then
                salida = 0
            Else
                If Convert.ToString(value) = "" Then
                    salida = 0
                Else
                    salida = Convert.ToInt32(value)
                End If
            End If

            Return salida

        End Function

        ''' <summary>
        ''' CheckDbl
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckDbl(ByVal value As Object) As Double
            Dim salida As Double = 0
            If value Is Nothing Or IsDBNull(value) Then
                salida = 0
            Else
                If Convert.ToString(value) = "" Then
                    salida = 0
                Else
                    salida = Convert.ToDouble(value)
                End If
            End If

            Return salida

        End Function

        ''' <summary>
        ''' CheckDecimal
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckDecimal(ByVal value As Object) As Decimal
            Dim salida As Decimal = 0
            If value Is Nothing Or IsDBNull(value) Then
                salida = 0
            Else
                If Convert.ToString(value) = "" Then
                    salida = 0
                Else
                    salida = Convert.ToDecimal(value)
                End If
            End If

            Return salida

        End Function

        ''' <summary>
        ''' CheckDbl
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckBoolean(ByVal value As Object) As Boolean
            Dim salida As Boolean = False
            If value = Nothing Then
                salida = False
            Else
                If Convert.ToBoolean(value) = "" Then
                    salida = False
                Else
                    salida = Convert.ToDouble(value)
                End If
            End If

            Return salida

        End Function

        ''' <summary>
        ''' Devuelve un valor Nothing cuando la cadena es de longitud 0 (cero), en otro
        ''' caso devuelve la cadena.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - JRC
        ''' Fecha de Creación  : 08/06/2012
        ''' </remarks>
        Public Shared Function CheckDateString(ByVal value As String, ByVal tipo As String) As String

            If value Is Nothing Then
                Return Nothing
            ElseIf (String.IsNullOrEmpty(value)) Then
                Return Nothing
            Else
                Dim strAnio As String = value.Substring(0, 4)
                Dim strMes As String = value.Substring(4, 2)
                Dim strDia As String = value.Substring(6, 2)

                Dim strHora As String = ""
                Dim strMinuto As String = ""
                Dim strSegundo As String = ""

                If value.Length > 8 Then
                    strHora = value.Substring(8, 2)
                    strMinuto = value.Substring(10, 2)
                    strSegundo = value.Substring(12, 2)
                End If

                If tipo.Trim().Equals("C") Then
                    Return strDia + "/" + strMes + "/" + strAnio
                ElseIf tipo.Trim().Equals("M") Then
                    Return strDia + "/" + strMes + "/" + strAnio + " " + strHora + ":" + strMinuto
                ElseIf tipo.Trim().Equals("F") Then
                    Return strDia + "/" + strMes + "/" + strAnio + " " + strHora + ":" + strMinuto + ":" + strSegundo
                Else
                    Return strDia + "/" + strMes + "/" + strAnio
                End If

            End If

        End Function

        ''' <summary>
        ''' Devuelve un valor Nothing cuando la cadena es de longitud 0 (cero), en otro
        ''' caso devuelve la cadena.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - AEP
        ''' Fecha de Creación  : 08/10/2012
        ''' </remarks>
        Public Shared Function CheckDateString2(ByVal value As String, ByVal tipo As String) As String

            If value Is Nothing Then
                Return Nothing
            ElseIf (String.IsNullOrEmpty(value)) Then
                Return Nothing
            Else
                Dim strDia As String = value.Substring(0, 2)
                Dim strMes As String = value.Substring(3, 2)
                Dim strAnio As String = value.Substring(6, 4)

                Dim strHora As String = ""
                Dim strMinuto As String = ""
                Dim strSegundo As String = ""

                If value.Length > 8 Then
                    strHora = value.Substring(8, 2)
                    strMinuto = value.Substring(10, 2)
                    strSegundo = value.Substring(12, 2)
                End If

                If strDia = "01" And strMes = "01" And strAnio = "1900" Then
                    Return ""
                ElseIf tipo.Trim().Equals("C") Then
                    Return strDia + "/" + strMes + "/" + strAnio
                ElseIf tipo.Trim().Equals("M") Then
                    Return strDia + "/" + strMes + "/" + strAnio + " " + strHora + ":" + strMinuto
                ElseIf tipo.Trim().Equals("F") Then
                    Return strDia + "/" + strMes + "/" + strAnio + " " + strHora + ":" + strMinuto + ":" + strSegundo
                Else
                    Return strDia + "/" + strMes + "/" + strAnio
                End If

            End If

        End Function
        ''' <summary>
        ''' CheckDecimal
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function CheckMontoCeroVacio(ByVal value As Decimal) As String
            Dim salida As String = value
            If value = 0 Then
                salida = ""
            End If
            Return salida
        End Function

        ''' <summary>
        ''' ConvierteValorBien
        ''' </summary>
        ''' <param name="MontoValorBien"></param>
        ''' <returns>Decimal</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvierteValorBien(ByVal MontoValorBien As String) As Decimal
            Dim datodecimal() As String
            Dim Dato1 As String = MontoValorBien.ToString().Replace(",", "")
            Dim DatoSinPuntoIzquierda As Long
            Dim DatoSinPuntoDerecha As Decimal

            datodecimal = Split(Dato1, ".")
            DatoSinPuntoIzquierda = CLng(datodecimal(0))
            If datodecimal.Length > 1 Then
                If Not String.IsNullOrEmpty(datodecimal(1)) Then
                    DatoSinPuntoDerecha = 0.0 + (CInt(datodecimal(1)) / 100)
                Else
                    DatoSinPuntoDerecha = 0.0
                End If
            End If
            Return DatoSinPuntoIzquierda + DatoSinPuntoDerecha
        End Function

#End Region

#Region "Números"

        ''' <summary>
        ''' fVerificarCantidadesEnteras
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function fVerificarCantidadesEnteras(ByVal pMonto As Decimal, ByVal pMaxEntero As Integer) As Boolean
            Dim mMonto As String = pMonto.ToString.Split(".")(0).ToString

            If mMonto.Length > pMaxEntero Then Return True

            Return False
        End Function

#End Region

#Region "Concatenar"

        Public Shared Function Concatenar(ByVal ParamArray cadenas() As String) As String
            Dim sb As New System.Text.StringBuilder()
            Dim cad As String
            For Each cad In cadenas
                sb.Append(cad)
            Next
            Return sb.ToString
        End Function

        Public Shared Function ConcatenarSeparador(ByVal pstrSeparador As String, ByVal ParamArray cadenas() As String) As String
            Dim sb As New System.Text.StringBuilder()
            Dim cad As String
            For Each cad In cadenas
                sb.Append(cad)
                sb.Append(pstrSeparador)
            Next

            Return sb.ToString().Substring(0, sb.ToString().Length - 1)
        End Function

        Public Shared Function FiltrarDataTable(ByVal dtOrigen As DataTable, ByVal Campo As String, ByVal Valor As String) As DataTable
            Dim i As Integer
            For i = 0 To dtOrigen.Rows.Count - 1
                If Not dtOrigen.Rows(i).Item(Campo) = Valor Then
                    dtOrigen.Rows.RemoveAt(i)
                End If
            Next
            dtOrigen.AcceptChanges()
            Return dtOrigen
        End Function

        Public Shared Function ArmaComboOpcion(ByVal pstrCodigo As String, ByVal pstrNombre As String) As String
            Dim strOpcion As String = ""
            strOpcion = "<option value='" + pstrCodigo + "'>" + pstrNombre + "</option>"
            Return strOpcion
        End Function

#End Region

#Region "Formatos del Cliente"

        'INICIO IBK RPR
        Public Shared Function formateaNroCuenta(ByVal strNroCuenta As String) As String
            If (strNroCuenta.Length < 13) Then Return strNroCuenta
            Return strNroCuenta.Substring(0, 3) + "-" + strNroCuenta.Substring(3)
        End Function
        'FIN

        ''' <summary>
        ''' fFormatoCodCliente
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Function fFormatoCodCliente(ByVal pCodCliente As String, ByVal pLength As Integer) As String
            Try
                ''Por el Codigo que trae del RM son 14 y instruccion son 10
                Dim mCodCliente As String = String.Empty
                If pCodCliente <> "" Then
                    If pCodCliente.Length > (pLength + 1) Then
                        mCodCliente = pCodCliente.Substring((Len(pCodCliente) - pLength), pLength)
                    Else
                        mCodCliente = pCodCliente.Trim.PadLeft(pLength, "0"c)
                    End If
                End If
                Return mCodCliente
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Crear Web Log"

        ''' <summary>
        ''' pGuardarLogWeb
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub pGuardarLogWeb(ByVal pstrNombAplicativo As String, ByVal pstrFormulario As String, ByVal pintSuceso As Integer, ByVal pstrElmento As String, ByVal pstrMensaje As String)
            Dim oLog As New GCCLog("GCCUtilitario")
            Try
                Dim strRuta As String = fstrObtieneKeyWebConfig("LogRuta")
                Dim intAmbiente As Integer = fstrObtieneKeyWebConfig("ArgAmbienteIndex")
                Dim iRes As Integer = oLog.toWrite(strRuta, pstrNombAplicativo, intAmbiente, pstrElmento, "", pintSuceso, "", "Mensaje del Formulario : " & pstrFormulario, "", pstrMensaje)
            Catch ex As Exception
                'Throw ex
            Finally
                oLog = Nothing
            End Try
        End Sub

#End Region

#Region "Combo ValorGenerico"

        Public Shared Sub CargarComboValorGenerico(ByRef pDDLCombo As HtmlSelect, ByVal pstrTablaGenerica As String)
            Dim oLwsMantenimiento As New LMantenimientoNTX
            Dim odtbParam As DataTable

            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(pstrTablaGenerica))

                pCargarHtmlSelect(pDDLCombo, odtbParam, "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsMantenimiento = Nothing
            End Try
        End Sub

        '10/01/2013 IBK RPR
        Public Shared Sub CargarComboMotivoBasilea(ByRef pDDLCombo As HtmlSelect)
            Dim oLwsMantenimiento As New LMantenimientoNTX
            Dim odtbParam As DataTable

            Try
                Dim oEValorGenerica As New EValorgenerica
                oEValorGenerica.Valor4 = "Pago"
                Dim strEValorGenerica As String = GCCUtilitario.SerializeObject(oEValorGenerica)

                odtbParam = DeserializeObject(Of DataTable)(oLwsMantenimiento.ListadoValorGenericaEspecial(strEValorGenerica, 24))

                pCargarHtmlSelect(pDDLCombo, odtbParam, "Valor1", "Clave1", "[-Seleccione-]", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsMantenimiento = Nothing
            End Try
        End Sub

        '24/04/2013 IBK RPR
        Public Shared Function ObtenerPorcIGV() As Decimal
            Dim oLwsMantenimiento As New LMantenimientoNTX
            Dim odtbParam As DataTable

            Try
                Dim oEValorGenerica As New EValorgenerica
                Dim strEValorGenerica As String = GCCUtilitario.SerializeObject(oEValorGenerica)

                odtbParam = DeserializeObject(Of DataTable)(oLwsMantenimiento.ListadoValorGenericaEspecial(strEValorGenerica, 10))

                If (odtbParam.Rows.Count > 0) Then
                    Return Val(odtbParam.Rows(0).Item(0).ToString)
                Else
                    Return 0.18
                End If

            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsMantenimiento = Nothing
            End Try
        End Function
        'FIN RPR

        Public Shared Sub CargarComboValorGenericoInfraccion(ByRef pDDLCombo As HtmlSelect, ByVal pstrTablaGenerica As String)
            Dim oLwsMantenimiento As New LMantenimientoNTX
            Dim odtbParam As DataTable

            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(pstrTablaGenerica))

                pCargarHtmlSelect(pDDLCombo, odtbParam, "DESCRIPCION", "CODIGO", "---", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsMantenimiento = Nothing
            End Try
        End Sub

        Public Shared Sub CargarComboValorGenericoAnidado(ByRef pDDLCombo As HtmlSelect, ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String)
            Dim oLwsMantenimiento As New LMantenimientoNTX
            Dim odtbParam As New DataTable
            Dim dv As New DataView
            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenericaAnidada(pstrTablaGenerica, pstrCodigo))
                dv = odtbParam.DefaultView
                'dv.RowFilter = "ESTADO = 1 "
                pCargarHtmlSelect(pDDLCombo, dv.ToTable, "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsMantenimiento = Nothing
            End Try
        End Sub

        Public Shared Sub CargarComboMoneda(ByRef pDDLCombo As HtmlSelect)
            Dim oLwsUtil As New LUtilNTX
            Dim odtbParam As New DataTable
            Dim dv As New DataView
            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsUtil.ListarMoneda())
                dv = odtbParam.DefaultView
                'dv.RowFilter = "ESTADO = 1 "
                pCargarHtmlSelect(pDDLCombo, dv.ToTable, "NombreMoneda", "CodMoneda", "[-Seleccione-]", 0)
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsUtil = Nothing
            End Try
        End Sub

        Public Shared Sub CargarComboPais(ByRef pDDLCombo As HtmlSelect)
            Dim oLwsUtil As New LUtilNTX
            Dim odtbParam As New DataTable
            Dim dv As New DataView
            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsUtil.ListarPais())
                dv = odtbParam.DefaultView
                'dv.RowFilter = "ESTADO = 1 "
                pCargarHtmlSelect(pDDLCombo, dv.ToTable, "DescripPais", "CodPais", "[-Seleccione-]", 0)
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsUtil = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' pCargarDropDownList
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub pCargarHtmlSelect(ByRef pDDlCombo As HtmlSelect, ByVal pObjDataSource As Object, ByVal pstrDataTextField As String, ByVal pstrDataValueField As String, ByVal pstrPrimerItemTexto As String, ByVal pstrPrimerItemValor As String)
            pDDlCombo.DataSource = pObjDataSource
            pDDlCombo.DataTextField = pstrDataTextField
            pDDlCombo.DataValueField = pstrDataValueField
            pDDlCombo.DataBind()

            pInsertarPrimerItemHtmlSelect(pDDlCombo, pstrPrimerItemTexto, pstrPrimerItemValor)
        End Sub

        ''' <summary>
        ''' pInsertarPrimerItem
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub pInsertarPrimerItemHtmlSelect(ByRef pDDlCombo As HtmlSelect, ByVal pstrTexto As String, ByVal pstrValor As String)
            pDDlCombo.Items.Insert(0, New ListItem(pstrTexto, pstrValor))
        End Sub

#End Region

#Region "Estados del contrato"

        Public Shared Sub CargarComboEstadosdelContrato(ByRef pDDLCombo As HtmlSelect)
            Dim oLUtilNTX As New LUtilNTX

            Dim odtbParam As New DataTable
            Dim dv As New DataView
            Try
                odtbParam = DeserializeObject(Of DataTable)(oLUtilNTX.ListarEstadosBusquedaContrato())
                dv = odtbParam.DefaultView
                pCargarHtmlSelect(pDDLCombo, dv.ToTable, "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLUtilNTX = Nothing
            End Try
        End Sub

#End Region

#Region "Ubigeo"

        ''' <summary>
        ''' Devuelve el código de ubigeo para el las opciones seleccionadas por el usuario de las
        ''' listas deplegables departamento, provincia, distrito.
        ''' </summary>
        ''' <param name="departamento">Código de departamento, dos dígitos o "0"</param>
        ''' <param name="provincia">Código de provincia, dos dígitos o "0"</param>
        ''' <param name="distrito">Código de distrito, dos dígitos o "0"</param>
        ''' <returns>Código de ubigeo válido o null</returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 22/02/2011
        ''' </remarks>
        Public Shared Function CodigoUbigeo(ByVal departamento As String, _
                                            ByVal provincia As String, _
                                            ByVal distrito As String) As String
            Try
                If (String.IsNullOrEmpty(departamento) Or departamento = "0") Then
                    Return Nothing
                Else
                    If (String.IsNullOrEmpty(provincia) Or provincia = "0") Then
                        Return departamento + "0000"
                    Else
                        If (String.IsNullOrEmpty(distrito) Or distrito = "0") Then
                            Return departamento + provincia + "00"
                        Else
                            Return departamento + provincia + distrito
                        End If
                    End If
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Sub CargarDepartamento(ByRef pDDLCombo As HtmlSelect)
            Dim oLwsUtil As New LUtilNTX
            Dim odtbParam As DataTable

            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsUtil.ListarDepartamento())

                'dv.RowFilter = "ESTADO = 1 "
                pCargarHtmlSelect(pDDLCombo, odtbParam, "nombre", "codigo", "[-Seleccione-]", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsUtil = Nothing
            End Try
        End Sub

        'Inicio JJM IBK
        Public Shared Sub CargarMunicipalidad(ByRef pDDLCombo As HtmlSelect)
            Dim oLwsUtil As New LUtilNTX
            Dim odtbParam As DataTable

            Try
                odtbParam = DeserializeObject(Of DataTable)(oLwsUtil.ListarMunicipalidad())

                'dv.RowFilter = "ESTADO = 1 "
                pCargarHtmlSelect(pDDLCombo, odtbParam, "nombre", "codigo", "[-Seleccione-]", "0")
            Catch ex As Exception
                Throw ex
            Finally
                odtbParam = Nothing
                oLwsUtil = Nothing
            End Try
        End Sub
        'Fin JJM IBK

#End Region

#Region "Consulta RM"

        Public Shared Function fObtenerDatosRMCliente(ByVal pargNumConsulta As Integer, _
                                                      ByVal pstrCodUnico As String, _
                                                      ByVal pstrTipoDoc As String, _
                                                      ByVal pstrNroRuc As String, _
                                                      ByRef sMensaje As String) As EClienteRM
            Dim oLWebService As New LWebService
            Dim sTldDatosTran As String = String.Empty
            Dim strCodProducto As String = "000"
            Dim intTipoDoc As Integer = GCCUtilitario.fObtenerCodTipoDocTrx(pstrTipoDoc)
            Dim strNroRuc As String = IIf(intTipoDoc = GCC.Entity.EConstante.C_TRX_TIPDOC_RUC, pstrNroRuc.Trim.PadLeft(11, "0"c), pstrNroRuc)
            If pargNumConsulta = 1 Then
                strNroRuc = pstrNroRuc.Trim.PadLeft(11, "0"c)
            End If
            Dim oECliente As New EClienteRM

            Try

                Dim strUlrws As String = fstrObtieneKeyWebConfig("wsFCDRM")
                sTldDatosTran = oLWebService.fstrConsultarClienteRM(pargNumConsulta, pstrCodUnico, CStr(intTipoDoc), strNroRuc.ToUpper, strCodProducto, strUlrws)

                Dim strTrans As String() = sTldDatosTran.Split("|")

                If strTrans(0).ToString = "0" Then
                    If strTrans(6).ToString = "00" Then
                        With oECliente
                            .Codigounico = fFormatoCodCliente(strTrans(9).ToString, 10)
                            .Codigotipodocumento = fObtenerCodigoTipoDoc(CheckInt(strTrans(10).ToString.Trim))
                            .Numerodocumento = strTrans(11).ToString
                            .Razonsocialcliente = strTrans(12).ToString + " " + strTrans(42).ToString
                            .Ciiu = strTrans(24).ToString
                            .Codigotienda = strTrans(40).ToString
                            .Nombretienda = strTrans(41).ToString
                            .Banca = strTrans(16).ToString
                            .Codigoejecutivo = strTrans(13).ToString
                            .Nombreejecutivo = strTrans(14).ToString
                            .Codigogrupo = strTrans(17).ToString
                            .Nombregrupo = strTrans(36).ToString
                            .Segmento = strTrans(21).ToString
                            Dim mRatingEmpActual As Decimal = CheckDecimal(Trim(strTrans(20).ToString))
                            If mRatingEmpActual > 0 Then
                                mRatingEmpActual = (mRatingEmpActual / 1000)
                            End If
                            .Ratingempresa = mRatingEmpActual
                            .Clasificacionsbs = strTrans(19).ToString
                            .Clasificacionfeve = strTrans(22).ToString
                            .Direccion = strTrans(44).ToString
                        End With
                    ElseIf strTrans(6).ToString <> "00" And strTrans(8).ToString <> "" Then
                        oECliente = Nothing
                        sMensaje = strTrans(8).ToString
                    Else
                        oECliente = Nothing
                        sMensaje = strTrans(8).ToString
                    End If
                Else
                    oECliente = Nothing
                    sMensaje = strTrans(1).ToString
                End If


            Catch ex As Exception
                Throw ex
            Finally
                oLWebService = Nothing
            End Try

            Return oECliente
        End Function

        Private Shared Function fObtenerCodTipoDocTrx(ByVal pstrTipoDoc As String) As Integer
            Dim intTipoDoc As Integer = 0
            Select Case pstrTipoDoc
                Case GCCConstante.eTipoDocumento.DNI
                    intTipoDoc = GCC.Entity.EConstante.C_TRX_TIPDOC_DNI
                Case GCCConstante.eTipoDocumento.RUC
                    intTipoDoc = GCC.Entity.EConstante.C_TRX_TIPDOC_RUC
                Case GCCConstante.eTipoDocumento.CarnetExtranjeria
                    intTipoDoc = GCC.Entity.EConstante.C_TRX_TIPDOC_CE
                Case GCCConstante.eTipoDocumento.Pasaporte
                    intTipoDoc = GCC.Entity.EConstante.C_TRX_TIPDOC_PA
                Case Else
                    intTipoDoc = GCC.Entity.EConstante.C_TIPODOCUMENTO_OTROS
            End Select
            Return intTipoDoc
        End Function

        Private Shared Function fObtenerCodigoTipoDoc(ByVal pintTipoDoc As Integer) As Integer
            Dim intTipoDoc As Integer = 0
            Select Case pintTipoDoc
                Case GCC.Entity.EConstante.C_TRX_TIPDOC_DNI
                    intTipoDoc = GCCConstante.eTipoDocumento.DNI
                Case GCC.Entity.EConstante.C_TRX_TIPDOC_RUC
                    intTipoDoc = GCCConstante.eTipoDocumento.RUC
                Case GCC.Entity.EConstante.C_TRX_TIPDOC_CE
                    intTipoDoc = GCCConstante.eTipoDocumento.CarnetExtranjeria
                Case GCC.Entity.EConstante.C_TRX_TIPDOC_PA
                    intTipoDoc = GCCConstante.eTipoDocumento.Pasaporte
                Case Else
                    intTipoDoc = GCCConstante.eTipoDocumento.Otros
            End Select
            Return intTipoDoc
        End Function



#End Region

#Region "Tipo de caracteres"

        ''' <summary>
        ''' Convierte una cadena con caracteres unicode no ansi en su equivalente utf8.
        ''' </summary>
        ''' <param name="texto">Cadena con los caracteres a transformar</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 22/02/2011
        ''' </remarks>
        Public Shared Function UnicodeToUtf8(ByVal texto As String) As String
            Dim oDictionary As New Dictionary(Of String, String)

            ' Diccionario con los caracteres no ansi
            ' (unicode, utf8)
            oDictionary.Add("ž", "%9E")
            oDictionary.Add("Ÿ", "%9F")
            oDictionary.Add("À", "%C0")
            oDictionary.Add("Á", "%C1")
            oDictionary.Add("Â", "%C2")
            oDictionary.Add("Ã", "%C3")
            oDictionary.Add("Ä", "%C4")
            oDictionary.Add("Å", "%C5")
            oDictionary.Add("Æ", "%C6")
            oDictionary.Add("È", "%C8")
            oDictionary.Add("É", "%C9")
            oDictionary.Add("Ê", "%CA")
            oDictionary.Add("Ë", "%CB")
            oDictionary.Add("Ì", "%CC")
            oDictionary.Add("Í", "%CD")
            oDictionary.Add("Î", "%CE")
            oDictionary.Add("Ï", "%CF")
            oDictionary.Add("Ñ", "%D1")
            oDictionary.Add("Ò", "%D2")
            oDictionary.Add("Ó", "%D3")
            oDictionary.Add("Ô", "%D4")
            oDictionary.Add("Õ", "%D5")
            oDictionary.Add("Ö", "%D6")
            oDictionary.Add("Ù", "%D9")
            oDictionary.Add("Ú", "%DA")
            oDictionary.Add("Û", "%DB")
            oDictionary.Add("Ü", "%DC")
            oDictionary.Add("Ý", "%DD")
            oDictionary.Add("à", "%E0")
            oDictionary.Add("á", "%E1")
            oDictionary.Add("â", "%E2")
            oDictionary.Add("ã", "%E3")
            oDictionary.Add("ä", "%E4")
            oDictionary.Add("å", "%E5")
            oDictionary.Add("è", "%E8")
            oDictionary.Add("é", "%E9")
            oDictionary.Add("ê", "%EA")
            oDictionary.Add("ë", "%EB")
            oDictionary.Add("ì", "%EC")
            oDictionary.Add("í", "%ED")
            oDictionary.Add("î", "%EE")
            oDictionary.Add("ï", "%EF")
            oDictionary.Add("ñ", "%F1")
            oDictionary.Add("ò", "%F2")
            oDictionary.Add("ó", "%F3")
            oDictionary.Add("ô", "%F4")
            oDictionary.Add("õ", "%F5")
            oDictionary.Add("ö", "%F6")
            oDictionary.Add("ù", "%F9")
            oDictionary.Add("ú", "%FA")
            oDictionary.Add("û", "%FB")
            oDictionary.Add("ü", "%FC")
            oDictionary.Add("ý", "%FD")
            oDictionary.Add("ÿ", "%FF")

            For i As Integer = 0 To texto.Length
                If oDictionary.ContainsKey(texto.Substring(i, 1)) Then
                    texto = texto.Replace(texto.Substring(i, 1), oDictionary.Item(texto.Substring(i, 1)))
                End If
            Next i

            Return texto

        End Function

#End Region

#Region "Manejo de archivos"

        ''' <summary>
        ''' Elimina, si existe el archivo de destino indicado como parametro
        ''' </summary>
        ''' <param name="destino">Parametro o nombre del archivo a modificar</param>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 22/02/2011
        ''' </remarks>
        Public Shared Sub Eliminar(ByVal destino As String)
            Try
                If File.Exists(destino) Then
                    File.Delete(destino)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Exportación a Excel"

        Private Enum EstilosDato
            Texto = 1
            Moneda = 2
            Izquierdo = 3
            Fecha = 4
            Hora = 5
        End Enum

        Public Shared Function fHTMLEstilo() As String
            Dim obj_sb As New StringBuilder
            Dim sw As StringWriter

            obj_sb.Append("<style>")
            obj_sb.Append(".titulo")
            obj_sb.Append("{")
            obj_sb.Append("font-family:arial;")
            obj_sb.Append("font-size:12pt;")
            obj_sb.Append("color:#000000;")
            obj_sb.Append("font-weight:bold;")
            obj_sb.Append("text-align:center")
            obj_sb.Append("}")

            obj_sb.Append(".td-grid-header")
            obj_sb.Append("{")
            obj_sb.Append("background-color: #6ab952;")
            obj_sb.Append("font-family: Arial;")
            obj_sb.Append("font-size: 8pt;")
            obj_sb.Append("color: #ffffff;")
            obj_sb.Append("text-align:center;")
            obj_sb.Append("vertical-align:middle;")
            'obj_sb.Append("height: 20px;")
            'obj_sb.Append("z-index: 10;")
            obj_sb.Append("font-weight:bold")
            obj_sb.Append("}")

            obj_sb.Append(".td-grid-blanco")
            obj_sb.Append("{")
            obj_sb.Append("background-color: #ffffff;")
            obj_sb.Append("height: 15px;")
            'obj_sb.Append("padding: 3px;")
            obj_sb.Append("font-family: Arial;")
            obj_sb.Append("font-size: 8pt;")
            obj_sb.Append("color: #404a55;")
            obj_sb.Append("text-align:center;")
            obj_sb.Append("vertical-align:middle;")
            obj_sb.Append("}")

            obj_sb.Append(".td-grid-gris")
            obj_sb.Append("{")
            obj_sb.Append("background-color: #f0f0f0;")
            obj_sb.Append("height: 15px;")
            'obj_sb.Append("padding: 3px;")
            obj_sb.Append("font-family: Arial;")
            obj_sb.Append("font-size: 8pt;")
            obj_sb.Append("color: #404a55;")
            obj_sb.Append("text-align:center;")
            obj_sb.Append("vertical-align:middle;")
            obj_sb.Append("}")

            obj_sb.Append(".CssText { mso-number-format:\@; }")
            obj_sb.Append(".CssLeft { text-align:left; }")
            obj_sb.Append(".CssCost { mso-number-format:'#,##0.00'; text-align:right;}")
            obj_sb.Append(".CssNum { mso-number-format:'#,##0';}")
            obj_sb.Append(".CssDate { mso-number-format:'dd/mm/yyyy'; }")
            obj_sb.Append(".CssHour { mso-number-format:'hh:mm:ss'; }")

            obj_sb.Append("</style>")

            sw = New StringWriter(obj_sb)
            Return sw.ToString
        End Function

        Public Shared Function fHTMLGeneraTablaTitulo(ByVal pTitulo As String, _
                                                        ByVal pPage As System.Web.UI.Page, _
                                                        ByVal pintColumns As Integer) As String
            Dim sw As StringWriter
            Dim obj_sb As New StringBuilder

            'Dim strStyle As String = "style='font-family:arial;font-size:12pt;color:#000000;font-weight:bold'"

            obj_sb.Append("<table border='0'>" & vbCrLf)
            obj_sb.Append("       <tr><td rowspan='2'></td></tr>")
            obj_sb.Append("       <tr>" & vbCrLf)

            'Dim strRuta As String = fstrObtieneKeyWebConfig("FileServer")
            'Dim strLogo As String = "Interbank.jpg"
            'strRuta = IIf(strRuta.EndsWith("\"), strRuta, strRuta & "\") & strLogo

            'Dim strLogo As String = pPage.Request.PhysicalApplicationPath + "Util/images/Interbank.jpg"
            Dim strPort As String = pPage.Request.Url.Port.ToString()
            strPort = IIf(strPort = "0", "", ":" & strPort & "/")
            Dim strLogo As String = "http://" & pPage.Request.Url.Host & strPort & pPage.ResolveUrl("~/Util/images/Interbank.jpg")

            obj_sb.Append("              <td>" & vbCrLf)
            obj_sb.Append("                     <img src='" & strLogo & "'>" & vbCrLf)
            obj_sb.Append("              </td>" & vbCrLf)

            obj_sb.Append("              <td colspan='" & (pintColumns - 1).ToString() & "' class='titulo'>" & vbCrLf)
            obj_sb.Append("                     " & pTitulo & vbCrLf)
            obj_sb.Append("              </td>" & vbCrLf)
            obj_sb.Append("       </tr>" & vbCrLf)
            obj_sb.Append("</table>" & vbCrLf)

            sw = New StringWriter(obj_sb)
            Return sw.ToString
        End Function

        Public Shared Function fHTMLGeneraTablaCabecera(ByVal pOrderHeaderTableColumnsLabels As ArrayList, _
                                                        ByVal pOrderHeaderTableColumnsText As ArrayList, _
                                                        ByVal pintColumns As Integer) As String
            Dim obj_sb As New StringBuilder
            Dim sw As StringWriter

            obj_sb.Append("<br>")

            obj_sb.Append("<table>")

            obj_sb.Append("<tr>")
            obj_sb.Append("<td>")
            obj_sb.Append("&nbsp;</td>")

            obj_sb.Append("<td align='left' colspan='" & pintColumns.ToString() & "'>")
            For i As Integer = 0 To pOrderHeaderTableColumnsText.Count - 1
                obj_sb.Append("<b>")
                obj_sb.Append(pOrderHeaderTableColumnsLabels(i))
                obj_sb.Append("</b>")
                obj_sb.Append("&nbsp;&nbsp;")
                obj_sb.Append(pOrderHeaderTableColumnsText(i))
                obj_sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
            Next
            obj_sb.Append("</td>")
            obj_sb.Append("</tr>")

            obj_sb.Append("</table>")
            sw = New StringWriter(obj_sb)
            Return sw.ToString
        End Function

        Public Shared Sub pDescargaArchivoPagina(ByVal oPage As System.Web.UI.Page, _
                                                        ByVal oDato As Object, _
                                                        Optional ByVal pstrNombreArchivo As String = "Consulta", _
                                                        Optional ByVal pstrExtension As String = "xls")
            If oDato Is Nothing Then Exit Sub
            Dim strExtension As String = String.Format("attachment; filename={0}.{1}", pstrNombreArchivo, pstrExtension)
            oPage.Response.ClearContent()
            oPage.Response.ClearHeaders()
            oPage.Response.Buffer = True
            'oPage.Response.BufferOutput = True
            oPage.Response.ContentType = "application/octet-stream"
            oPage.Response.AddHeader("content-disposition", strExtension)
            oPage.Response.AddHeader("Content-Length", oDato.Length.ToString)
            If oDato.GetType().Name = "Byte[]" Then
                oPage.Response.BinaryWrite(oDato)
            Else
                oPage.Response.Write(oDato)
            End If
            oPage.Response.Flush()
            'oPage.Response.End()
        End Sub

        Public Shared Function fHTMLGeneraTabla(ByVal dtDetalleConsulta As DataTable, _
                                      ByVal pOrderDataTableColumnsTitles As ArrayList, _
                                      ByVal pOrderDataTableColumnsName As ArrayList, _
                                      Optional ByVal pAlineacionColumna As ArrayList = Nothing, _
                                      Optional ByVal pGuionNumero As Boolean = False) As String

            Dim vColumnsTitleCount As Integer = pOrderDataTableColumnsTitles.Count
            Dim vColumnsNameCount As Integer = pOrderDataTableColumnsName.Count
            Dim obj_sb As New StringBuilder
            Dim sw As StringWriter

            obj_sb.Append("<table cellpadding='0' cellspacing='0'>")
            obj_sb.Append("<tr>")
            obj_sb.Append("<td>")
            obj_sb.Append("&nbsp;&nbsp;</td>")
            obj_sb.Append("<td>")

            obj_sb.Append("<table border='1'>")

            '***************
            'INICIO CABECERA
            '***************
            obj_sb.Append("<tr class='td-grid-header'>")
            For i As Integer = 0 To vColumnsTitleCount - 1
                Dim strColumnText As String = pOrderDataTableColumnsTitles(i)
                obj_sb.Append("<td>&nbsp;")
                obj_sb.Append(strColumnText)
                obj_sb.Append("&nbsp;</td>")
            Next
            obj_sb.Append("</tr>")
            '************
            'FIN CABECERA
            '************

            Dim oRow As DataRow
            Dim flag As Integer = 0
            Dim strClass As String = String.Empty

            For Each oRow In dtDetalleConsulta.Rows
                If flag = 0 Then
                    strClass = "td-grid-blanco"
                    flag = 1
                Else
                    strClass = "td-grid-gris"
                    flag = 0
                End If

                obj_sb.Append("<tr class='" & strClass & "'>")

                For i As Integer = 0 To vColumnsNameCount - 1
                    Dim dcDataTable As DataColumn = dtDetalleConsulta.Columns(pOrderDataTableColumnsName(i))
                    Dim oValue As Object = oRow(pOrderDataTableColumnsName(i))

                    If dcDataTable.DataType Is GetType(Date) Then
                        obj_sb.Append("<td align='" & pAlineacionColumna(i) & "' class='CssDate'>")
                        Dim datValor As Date = CheckDate(oValue)
                        obj_sb.Append(datValor.ToString(GCCConstante.C_FormatoFecha))
                    ElseIf dcDataTable.DataType Is GetType(Decimal) Or dcDataTable.DataType Is GetType(Double) Then
                        obj_sb.Append("<td align='" & pAlineacionColumna(i) & "' class='CssCost'>")
                        If (oValue Is Nothing Or oValue Is DBNull.Value) And pGuionNumero Then
                            obj_sb.Append("-")
                        Else
                            Dim decValor As Decimal = CheckDecimal(oValue)
                            obj_sb.Append(decValor.ToString(GCCConstante.C_FormatMiles2))
                        End If
                    ElseIf dcDataTable.DataType Is GetType(Integer) Then
                        obj_sb.Append("<td align='" & pAlineacionColumna(i) & "' class='CssNum'>")
                        Dim decValor As Integer = CheckInt(oValue)
                        obj_sb.Append(decValor.ToString(GCCConstante.C_FormatMiles0))
                    Else
                        obj_sb.Append("<td align='" & pAlineacionColumna(i) & "' class='CssText'>")
                        Dim strValor As String = CheckStr(oValue)
                        obj_sb.Append(strValor)
                    End If

                    obj_sb.Append("</td>")

                    'Select Case pTipoColumna(i)
                    '    Case TipoDatoColumna.ETexto
                    '        obj_sb.Append("<td align='left'>")
                    '        Dim strValor As String = CheckStr(oValue)
                    '        obj_sb.Append(strValor)
                    '        obj_sb.Append("</td>")
                    '    Case TipoDatoColumna.EDecimal
                    '        obj_sb.Append("<td align='right'>")
                    '        Dim decValor As Decimal = CheckDecimal(oValue)
                    '        obj_sb.Append(decValor.ToString(GCCConstante.C_FormatMiles2))
                    '        obj_sb.Append("</td>")
                    '    Case TipoDatoColumna.EDecimalGuion
                    '        obj_sb.Append("<td align='right'>")
                    '        If oValue Is Nothing Or oValue Is DBNull.Value Then
                    '            obj_sb.Append("-")
                    '        Else
                    '            Dim decValor As Decimal = CheckDecimal(oValue)
                    '            obj_sb.Append(decValor.ToString(GCCConstante.C_FormatMiles2))
                    '        End If
                    '        obj_sb.Append("</td>")
                    '    Case TipoDatoColumna.ENumero
                    '        obj_sb.Append("<td align='center'>")
                    '        Dim intValor As Integer = CheckInt(oValue)
                    '        obj_sb.Append(intValor.ToString(GCCConstante.C_FormatMiles0))
                    '        obj_sb.Append("</td>")
                    '    Case TipoDatoColumna.EFecha
                    '        obj_sb.Append("<td align='center'>")
                    '        Dim datValor As Date = CheckDate(oValue)
                    '        obj_sb.Append(datValor.ToString(GCCConstante.C_FormatoFecha))
                    '        obj_sb.Append("</td>")
                    'End Select
                Next
                obj_sb.Append("</tr>")
            Next

            obj_sb.Append("</table>")

            obj_sb.Append("</td>")
            obj_sb.Append("</tr>")
            obj_sb.Append("</table>")

            sw = New StringWriter(obj_sb)
            Return sw.ToString
        End Function

        Public Shared Function fHTMLGeneraTablaEstilo(ByVal dtDetalleConsulta As DataTable, _
                                                      ByVal pOrderDataTableColumnsTitles As ArrayList, _
                                                      ByVal pOrderDataTableColumnsName As ArrayList, _
                                                      Optional ByVal pColumnStyleWidth As ArrayList = Nothing, _
                                                      Optional ByVal pModo As ArrayList = Nothing) As String

            Dim vColumnsTitleCount As Integer = pOrderDataTableColumnsTitles.Count
            Dim vColumnsNameCount As Integer = pOrderDataTableColumnsName.Count
            Dim obj_sb As New StringBuilder
            Dim sw As StringWriter

            obj_sb.Append("<table cellpadding='0' cellspacing='0'>")
            obj_sb.Append("<tr>")
            obj_sb.Append("<td style='width:5px' width='5px'>")
            obj_sb.Append("&nbsp;</td>")
            obj_sb.Append("<td>")

            obj_sb.Append("<table border='1'>")

            '***************
            'INICIO CABECERA
            '***************
            obj_sb.Append("<tr class='td-grid-header'>")
            For i As Integer = 0 To vColumnsTitleCount - 1
                If pColumnStyleWidth Is Nothing Then
                    obj_sb.Append("<td>&nbsp;")
                Else
                    obj_sb.Append("<td style='width:" & pColumnStyleWidth(i) & "px'>&nbsp;")
                End If
                obj_sb.Append(pOrderDataTableColumnsTitles(i))
                obj_sb.Append("&nbsp;</td>")
            Next
            obj_sb.Append("</tr>")
            '************
            'FIN CABECERA
            '************

            Dim oRow As DataRow
            Dim flag As Integer = 0
            Dim strClass As String = String.Empty

            Dim intContador As Integer = 1
            Dim strContador As String = String.Empty
            Dim strCampo As String = String.Empty
            Dim strCampoRep As String = String.Empty

            For Each oRow In dtDetalleConsulta.Rows

                If flag = 0 Then
                    strClass = "td-grid-blanco"
                    flag = 1
                Else
                    strClass = "td-grid-gris"
                    flag = 0
                End If

                obj_sb.Append("<tr class='" & strClass & "'>")

                If pModo IsNot Nothing Then
                    strCampoRep = String.Empty
                    strContador = String.Empty
                    If strCampo = String.Empty Then
                        strContador = intContador
                        strCampo = oRow(pModo(0))
                        strCampoRep = oRow(pModo(0))
                        strContador = intContador.ToString()
                    End If

                    If strCampo <> oRow(pModo(0)) Then
                        strCampo = oRow(pModo(0))
                        strCampoRep = oRow(pModo(0))
                        intContador = intContador + 1
                        strContador = intContador.ToString()
                    End If
                End If

                For i As Integer = 0 To vColumnsNameCount - 1
                    Dim arrDatos() As String = pOrderDataTableColumnsName(i).ToString().Split("|")
                    Dim strDato As String = String.Empty
                    If arrDatos(0) <> String.Empty Then
                        strDato = oRow(arrDatos(0)).ToString().Trim()
                    End If

                    If arrDatos.Length = 2 Then
                        Select Case CInt(arrDatos(1))
                            Case EstilosDato.Texto : obj_sb.Append("<td class='CssText'>")
                            Case EstilosDato.Moneda : obj_sb.Append("<td class='CssCost'>")
                            Case EstilosDato.Izquierdo : obj_sb.Append("<td class='CssLeft'>")
                            Case EstilosDato.Fecha : obj_sb.Append("<td class='CssDate'>")
                            Case EstilosDato.Hora : obj_sb.Append("<td class='CssHour'>")
                            Case Else : obj_sb.Append("<td>")
                        End Select
                    ElseIf arrDatos.Length = 3 Then
                        obj_sb.Append("<td style='color:" & oRow(arrDatos(2)).ToString().Trim() & "'>")
                    Else
                        obj_sb.Append("<td>")
                    End If

                    If pModo IsNot Nothing Then
                        If i = 0 Then
                            obj_sb.Append(fstrSeteaCampoHtml(strContador))
                        Else
                            If pModo(0) = arrDatos(0) Then
                                obj_sb.Append(fstrSeteaCampoHtml(strCampoRep))
                            Else
                                obj_sb.Append(fstrSeteaCampoHtml(strDato))
                            End If
                        End If
                    Else
                        obj_sb.Append(fstrSeteaCampoHtml(strDato))
                    End If
                    obj_sb.Append("</td>")
                Next
                obj_sb.Append("</tr>")

            Next

            obj_sb.Append("</table>")
            obj_sb.Append("</td>")
            obj_sb.Append("</tr>")
            obj_sb.Append("</table>")

            sw = New StringWriter(obj_sb)
            Return sw.ToString
        End Function

        Private Shared Function fstrSeteaCampoHtml(ByVal pstrDato As String) As String
            Dim strDato As String = "&nbsp;"
            If pstrDato.Trim <> String.Empty Then
                strDato = pstrDato
            End If
            Return strDato
        End Function

#End Region

    End Class

End Namespace