#Region "Clase Transaccional"

Public Class LDocClienteTx

    ''' <summary>
    ''' Guardar de Verificacion Documento Cliente 
    ''' </summary>
    ''' <param name="pEContrato">Entidad de Contrato para valores del Flag y Fecha de Termino Recep Cliente</param>
    ''' <param name="pEContacto">Entidad Serializada de Contacto para la cotizacion y contrato especifico</param>
    ''' <param name="pstrOpcion">Opcion de Contacto Si es N : Nuevo; M : Modificar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    ''' 
    Public Function GuardarVerificacionCliente(ByVal pEContrato As String, ByVal pEContacto As String, ByVal pstrOpcion As String) As Boolean
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteTx")
        Try
            Return oLDocClienteNTx.fblnGuardarVerificacionCliente(pEContrato, pEContacto, pstrOpcion)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Agregar un Documento/Condicion en Verificacion de Cliente 
    ''' </summary>
    ''' <param name="pstrContratoDoc">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function AgregarDocCondCliente(ByVal pstrContratoDoc As String) As Integer
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteTx")
        Try
            Return oLDocClienteNTx.AgregarDocCondCliente(pstrContratoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar un Documento/Condicion en Verificacion de Cliente de manera Logica
    ''' </summary>
    ''' <param name="pstrContratoDoc">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function EliminarDocCondCliente(ByVal pstrContratoDoc As String) As Boolean
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteTx")
        Try
            Return oLDocClienteNTx.EliminarDocCondCliente(pstrContratoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Envia Carta a Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function EnviarCartaDocumentoCliente(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteTx")
        Try
            Return oLDocClienteNTx.EnviarCartaDocumentoCliente(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LDocClienteNTx
    ''' <summary>
    ''' Obtiene el Contacto de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pstrCodCotizacion">Numero de Cotizacion</param>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012 
    ''' </remarks>
    Public Function ObtenerContacto(ByVal pstrCodCotizacion As String, ByVal pstrCodContrato As String) As String
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteNTx")
        Try
            Return oLDocClienteNTx.ObtenerContacto(pstrCodCotizacion, pstrCodContrato)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado General de Contrato y Cotizacion 
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteNTx")
        Try
            Return oLDocClienteNTx.ListadoContratoCotizacion(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener Datos Contrato y Cotizacion por numero de Contrato
    ''' </summary>
    ''' <param name="pstrNroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 17/05/2012
    ''' </remarks>
    Public Function ObtenerContratoCotizacion(ByVal pstrNroContrato As String) As String
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteNTx")
        Try
            Return oLDocClienteNTx.ObtenerContratoCotizacion(pstrNroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ' Inicio IBK - AAE - 03/10/2012 - Nuevo método para listar cotizaciones en pendiente y evaluación F10
    ''' <summary>
    ''' Listado General de Contrato y Cotizacion para solicitud de docunentos
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 03/10/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacionSolDoc(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteNTx")
        Try
            Return oLDocClienteNTx.ListadoContratoCotizacionSolDoc(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    Public Function ObtenerContratoCotizacion2(ByVal pstrNroContrato As String) As String
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LDocClienteNTx")
        Try
            Return oLDocClienteNTx.ObtenerContratoCotizacion(pstrNroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function
    ' Fin IBK - AAE

End Class

#End Region