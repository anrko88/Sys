Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary


#Region "Clase Transaccional"

Public Class LCotizacionTx
    Inherits LUtilitario

    ''' <summary>
    ''' Ingresa nueva Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>    
    Public Function InsertarCotizacion(ByVal pECotizacion As String, ByVal pEListCronograma As String) As String
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.InsertarCotizacion(pECotizacion, pEListCronograma)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificarCotizacion(ByVal pECotizacion As String, ByVal pEListCronograma As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.ModificarCotizacion(pECotizacion, pEListCronograma)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'IBK - RPH
    Public Function RegistrarRutaCronograma(ByVal pECotizacion As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.RegistrarRutaCronograma(pECotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin

    ''' <summary>
    ''' Modifica envio Carta
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function ModificarCotizacionCarta(ByVal pECotizacion As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.ModificarCotizacionCarta(pECotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Aprueba Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function CotizacionAprobar(ByVal pECotizacion As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.CotizacionAprobar(pECotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Rechazar Cotizacion
    ''' </summary>
    ''' <param name="pECotizacionSeguimiento">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function CotizacionRechazar(ByVal pECotizacionSeguimiento As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.CotizacionRechazar(pECotizacionSeguimiento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Ingresar Cotizacion Documento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function InsertarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.InsertarCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar Cotizacion Documento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ModificarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.ModificarCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Cotizacion
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function EliminarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.EliminarCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar Cotizacion Estado
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/07/2012
    ''' </remarks>
    Public Function ModificarCotizacionEstado(ByVal pECotizacionDocumento As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.ModificarCotizacionEstado(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "WebService GCC"

    ''' <summary>
    ''' ModificarEstadoCotizacionWS
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion">Numero Cotizacion</param>
    ''' <param name="pstrCodigoEstado">Codigo Estado a Actualizar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Public Function ModificarEstadoCotizacionWS(ByVal pstrNumeroCotizacion As String, ByVal pstrCodUnico As String, ByVal pstrCodigoEstado As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.ModificarEstadoCotizacionWS(pstrNumeroCotizacion, pstrCodUnico, pstrCodigoEstado)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' ModificarCotizacionWS
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Public Function ModificarCotizacionWS(ByVal pECotizacion As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Dim odtbCotizacion As New DataTable

        'Dim oEGccContratodocumento As New EGcc_contratodocumento

        Try
            'If Not String.IsNullOrEmpty(pECotizacion) Then
            'odtbCotizacion = DeserializeObject2(Of DataTable)(pECotizacion)
            'End If
            'odtbCotizacion.


            objLCotizacionTx.ModificarCotizacionWS(pECotizacion)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionTx = Nothing
            odtbCotizacion.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' ConsultarCotizacionWS
    ''' </summary>
    ''' <param name="pECodigoCotizacion"></param>
    ''' <param name="pECodigoUnico"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultarCotizacionWS(ByVal pECodigoCotizacion As String, ByVal pECodigoUnico As String) As Boolean
        Dim objLCotizacionTx As Object = CreateObject("GCC.Logic.LCotizacionTx")
        Try
            Return objLCotizacionTx.ConsultarCotizacionWS(pECodigoCotizacion, pECodigoUnico)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionTx = Nothing
        End Try
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LCotizacionNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Listado de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoCotizacion(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pstrEGcc_cotizacion As String) As String

        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ListadoCotizacion(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pstrEGcc_cotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un registro de cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function GetCotizacion(ByVal pstrEGcc_cotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.GetCotizacion(pstrEGcc_cotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener SubPrestatario
    ''' </summary>
    ''' <returns>Devuelve un registro de cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ObtenerSubPrestatario(ByVal pstrCodSuprestatario As String, ByVal pstrCodUnico As String) As String

        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ObtenerSubPrestatario(pstrCodSuprestatario, pstrCodUnico)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pECotizacionDocumento As String) As String

        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ListadoCotizacionDocumento(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener CotizacionDocumento
    ''' </summary>
    ''' <returns>Devuelve un registro de cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionDocumento(ByVal pECotizacionDocumento As String) As String

        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ObtenerCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado General de Cotizacion Version
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 23/05/2012
    ''' </remarks>
    Public Function ListadoCotizacionVersion(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pECotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ListadoCotizacionVersion(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado del Seguimeinto de una Cotizacion especifica
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 23/05/2012
    ''' </remarks>
    Public Function ListadoSeguimientoCotizacion(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pECotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ListadoSeguimientoCotizacion(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener Version de una cotizacion especifica
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionVersion(ByVal pECotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ObtenerCotizacionVersion(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener un listado del cronograma por version de una cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionCronograma(ByVal pECotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ObtenerCotizacionCronograma(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener un listado actual del cronograma 
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion">numero de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Public Function ObtenerCronogramaActual(ByVal pstrNumeroCotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.ObtenerCronogramaActual(pstrNumeroCotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener un listado del cronograma por version de una cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function CotizacionCronogramaGet(ByVal pECotizacion As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.CotizacionCronogramaGet(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    ' INICIO AAE - 14/09/2012 - Función que obtiene el codigo de usuario del ejecutivo leasing de la Cot
    Public Function GetCodUsuarioEjecutivo(ByVal pstrNroCotizacion As String, ByVal pnbrEsCotizacion As Integer) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Try
            Return objLCotizacionNTx.GetCodUsuarioEjecutivo(pstrNroCotizacion, pnbrEsCotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    'Función que retorna una tabla con los codigos de usuario de los administradores
    Public Function GetCodUsuarioAdministradoresLeasing() As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")

        Try
            Return objLCotizacionNTx.GetCodUsuarioAdministradoresLeasing()
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    'Función que retorna una tabla con los codigos de usuario de los Supervisores Leasing Comercial
    Public Function GetCodUsuarioAdministradoresComercial() As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")

        Try
            Return objLCotizacionNTx.GetCodUsuarioAdministradoresComercial()
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function

    Public Function GetCodUsuarioEjecutivoWS(ByVal pstrNroCotizacion As String, ByVal pnbrEsCotizacion As Integer) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Dim odtb As New DataTable
        Dim strCodigos As String = ""
        Try
            odtb = DeserializeObject(Of DataTable)(objLCotizacionNTx.GetCodUsuarioEjecutivo(pstrNroCotizacion, pnbrEsCotizacion))
            If odtb.Rows.Count > 0 Then
                strCodigos = odtb.Rows(0).Item(0).ToString().Trim()
            End If
            Return strCodigos
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
        End Try
    End Function
    'Fin AAE
    'IBK - RPH
    Public Function ListadoCliente(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pRazonSocial As String) As String

        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")

        Try
            Return objLCotizacionNTx.ListadoCliente(pPageSize, _
                                                    pCurrentPage, _
                                                    pSortColumn, _
                                                    pSortOrder, _
                                                    pRazonSocial)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Fin RPH


#Region "WebService GCC"
    ''' <summary>
    ''' Obtener Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un registro de cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Public Function ConsultarCotizacion(ByVal pstrNumeroCotizacion As String, ByVal pstrCodigoUnico As String) As String
        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LCotizacionNTx")
        Dim odtb As New DataTable
        Try
            odtb = DeserializeObject(Of DataTable)(objLCotizacionNTx.ConsultaCotizacion(pstrNumeroCotizacion, pstrCodigoUnico))



            Return SerializeObject2(Of DataTable)(odtb)
        Catch ex As Exception
            Throw ex
        Finally
            objLCotizacionNTx = Nothing
            odtb.Dispose()
        End Try
    End Function
#End Region
End Class

#End Region

#Region "Serializacion"
Public Class LUtilitario
    ''' <summary>
    ''' DeserializeObject
    ''' </summary>    
    ''' <remarks>
    ''' Recosntruir un objecto desde una Cadena XML
    ''' </remarks>
    Protected Function DeserializeObject(Of T)(ByVal xml As String) As T
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
    Private Function FromBinary(ByVal buffer() As Byte) As Object
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
    ''' fstrConvertirDataTableAXML
    ''' </summary>    
    ''' <remarks>
    ''' Convertit un datatable a un xml para pasarlo como parametro directo hacia la BD. El datatable a convertir
    ''' </remarks>
    Protected Function ConvertirDataTableAXML(ByVal pdtbBuild As DataTable) As String

        Dim dsBuildSQL As New DataSet()
        Dim sbSQL As Text.StringBuilder
        Dim swSQL As StringWriter
        Dim XMLformat As String
        Try

            sbSQL = New Text.StringBuilder()
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

    ''' Serealizar un objecto a una cadena XML
    Protected Function SerializeObject2(Of T)(ByVal obj As T) As String
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
    Protected Function DeserializeObject2(Of T)(ByVal xml As String) As T
        Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(T))
        Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream(StringToUTF8ByteArray(xml))

        Return CType(xs.Deserialize(memoryStream), T)
    End Function

    ''' Para convertir un array de Bytes de valores Unicode (UTF-8 encoded) a una cadena completa
    Private Function UTF8ByteArrayToString(ByVal characters As Byte()) As String
        Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding()
        Dim constructedString As String = encoding.GetString(characters)

        Return (constructedString)
    End Function

    ''' Convertir la cadena a un array de Bytes de valores Unicode (UTF-8 encoded) y esta se usa en la deserialización
    Private Function StringToUTF8ByteArray(ByVal pXmlString As String) As Byte()
        Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding()
        Dim byteArray As Byte() = encoding.GetBytes(pXmlString)

        Return byteArray
    End Function
End Class
#End Region