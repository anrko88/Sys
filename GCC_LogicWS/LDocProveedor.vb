
#Region "Clase Transaccional"

Public Class LProveedorTx

#Region "Proveedor"
    ''' <summary>
    ''' Ingresa nuevo Proveedor
    ''' </summary>
    ''' <param name="EProveedor">Listado de Objeto EProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>    
    Public Function fblnInsertarProveedor(ByVal EProveedor As String) As Boolean
        Dim objLProveedorTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLProveedorTx.InsertarProveedor(EProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica Proveedor
    ''' </summary>
    ''' <param name="pEProveedor">Listado de Objeto EProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>
    Public Function fblnModificarProveedor(ByVal pEProveedor As String) As Boolean
        Dim objLProveedorTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLProveedorTx.ModificarProveedor(pEProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Modifica Proveedor
    ''' </summary>
    ''' <param name="pEProveedor">Listado de Objeto EProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>
    Public Function fblnEliminarProveedor(ByVal pCodProveedor As String, ByVal pCodEstadoLogico As String, ByVal pCodUsuario As String) As Boolean
        Dim objLProveedorTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLProveedorTx.EliminarProveedor(pCodProveedor, pCodEstadoLogico, pCodUsuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Contrato Proveedor"
    ''' <summary>
    ''' Ingresa nuevo Contrato Proveedor
    ''' </summary>
    ''' <param name="EContratoProveedor">Listado de Objeto EContratoProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>    
    Public Function fintInsertarContratoProveedor(ByVal EContratoProveedor As String) As Integer
        Dim objLContratoProveedorTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLContratoProveedorTx.InsertarContratoProveedor(EContratoProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica Contrato Proveedor
    ''' </summary>
    ''' <param name="pEContratoProveedor">Listado de Objeto EContratoProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function fblnModificarContratoProveedor(ByVal pEContratoProveedor As String) As Boolean
        Dim objLContratoProveedorTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLContratoProveedorTx.ModificarContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina un objeto Contrato Proveedor, identificandolo por su clave primaria.
    ''' </summary>
    ''' <param name="pEContratoProveedor">Objeto EContratoProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>    
    Public Function fblnEliminarContratoProveedor(ByVal pEContratoProveedor As String) As Boolean
        Dim objLContratoProveedorTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLContratoProveedorTx.fblnEliminarContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica Solicitud Credito
    ''' </summary>
    ''' <param name="pESolicitudCredito">Objeto ESolicitudCredito(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    Public Function ModificaSolicitudDocumentoProveedor(ByVal pESolicitudCredito As String) As Boolean
        Dim objLTemporalTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return objLTemporalTx.ModificaSolicitudDocumentoProveedor(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EnviarCartaDocumentoProveedor(ByVal pEGcc_contratoProveedor As String) As Boolean
        Dim oLDocProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorTx")
        Try
            Return oLDocProveedorNTx.EnviarCartaDocumentoProveedor(pEGcc_contratoProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocProveedorNTx = Nothing
        End Try
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LProveedorNTx

#Region "Proveedor"

    ''' <summary>
    ''' Obtiene los datos del Proveedor
    ''' </summary>
    ''' <param name="pstrCodProveedor">Código Proveedor</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012 
    ''' </remarks>
    Public Function ObtenerProveedor(ByVal pstrCodProveedor As String) As String
        Dim oLDocProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")
        Try
            Return oLDocProveedorNTx.ObtenerProveedor(pstrCodProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocProveedorNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de los proveedores coincidentes con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Public Function ListadoProveedor(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodigoTipoDocumento As String, _
                                     ByVal pNumeroDocumento As String, _
                                     ByVal pRazonSocial As String) As String
        Dim objLProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")

        Try
            Return objLProveedorNTx.ListadoProveedor(pPageSize, _
                                                            pCurrentPage, _
                                                            pSortColumn, _
                                                            pSortOrder, _
                                                            pCodigoTipoDocumento, _
                                                            pNumeroDocumento, _
                                                            pRazonSocial)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de las cuentas del proveedor
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>
    Public Function ListadoProveedorCuenta(ByVal pCodProveedor As String) As String
        Dim objLProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")

        Try
            Return objLProveedorNTx.ListadoProveedorCuenta(pCodProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="pstrCodProveedor">Código Proveedor</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : JJM IBK
    ''' Fecha de Creacion : 16/04/2013
    ''' </remarks>
    Public Function ListaProveedorDocumento(ByVal pstrCodProveedor As String) As String
        Dim oLDocProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")
        Try
            Return oLDocProveedorNTx.ListaProveedorDocumento(pstrCodProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocProveedorNTx = Nothing
        End Try
    End Function
#End Region

#Region "Contacto"

    ''' <summary>
    ''' Listado de los contactos coincidentes con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function ListadoContacto(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodProveedor As String) As String
        Dim objLProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")

        Try
            Return objLProveedorNTx.ListadoContacto(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Contrato Proveedor"

    ''' <summary>
    ''' Listado de los contactos coincidentes con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function ListadoContratoProveedor(ByVal pNumeroContrato As String) As String
        Dim objLProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")

        Try
            Return objLProveedorNTx.ListadoContratoProveedor(pNumeroContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Valida la duplicida del registro de contrato proveedor
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    Public Function ValidarContratoProveedor(ByVal pEContratoProveedor As String) As String
        Dim objLProveedorNTx As Object = CreateObject("GCC.Logic.LDocProveedorNTx")

        Try
            Return objLProveedorNTx.ValidarContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region

