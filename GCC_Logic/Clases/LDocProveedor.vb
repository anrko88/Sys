
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LDocProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("0FFF01CF-6371-4b3f-81A5-F1A6F58DA908") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocProveedorTx")> _
Public Class LDocProveedorTx
    Inherits ServicedComponent
    Implements IDocProveedorTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LDocProveedorTx"
#End Region

#Region "Metodos"

#Region "Proveedor"

    ''' <summary>
    ''' Ingresa nuevo Proveedor
    ''' </summary>
    ''' <param name="pEProveedor">Listado de Objeto EProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarProveedor(ByVal pEProveedor As String) As Boolean Implements IDocProveedorTx.InsertarProveedor
        Dim objDProveedorTx As DProveedorTx = Nothing
        Dim iResultado As Integer

        Try
            objDProveedorTx = New DProveedorTx
            iResultado = objDProveedorTx.InsertarProveedor(pEProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objDProveedorTx.Dispose()
            objDProveedorTx = Nothing
        End Try

        Return iResultado
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
    <AutoComplete(True)> _
    Public Function ModificarProveedor(ByVal pEProveedor As String) As Boolean Implements IDocProveedorTx.ModificarProveedor
        Dim objDProveedorTx As DProveedorTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDProveedorTx = New DProveedorTx
            blnResultado = objDProveedorTx.ModificarProveedor(pEProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objDProveedorTx.Dispose()
            objDProveedorTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#Region "ContratoProveedor"
    ''' <summary>
    ''' Ingresa nueva Contrato Proveedor
    ''' </summary>
    ''' <param name="pEContratoProveedor">Listado de Objeto EContratoProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarContratoProveedor(ByVal pEContratoProveedor As String) As Integer Implements IDocProveedorTx.InsertarContratoProveedor
        Dim objDContratoProveedorTx As DContratoProveedorTx = Nothing
        Dim iResultado As Integer

        Try
            objDContratoProveedorTx = New DContratoProveedorTx
            iResultado = objDContratoProveedorTx.InsertarContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoProveedorTx.Dispose()
            objDContratoProveedorTx = Nothing
        End Try

        Return iResultado
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
    <AutoComplete(True)> _
    Public Function ModificarContratoProveedor(ByVal pEContratoProveedor As String) As Integer Implements IDocProveedorTx.ModificarContratoProveedor
        Dim objDContratoProveedorTx As DContratoProveedorTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoProveedorTx = New DContratoProveedorTx
            blnResultado = objDContratoProveedorTx.ModificaContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoProveedorTx.Dispose()
            objDContratoProveedorTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina Contrato Proveedor
    ''' </summary>
    ''' <param name="pEContratoProveedor">Listado de Objeto EContratoProveedor(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnEliminarContratoProveedor(ByVal pEContratoProveedor As String) As Boolean Implements IDocProveedorTx.fblnEliminarContratoProveedor
        Dim objDContratoProveedorTx As DContratoProveedorTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoProveedorTx = New DContratoProveedorTx
            blnResultado = objDContratoProveedorTx.EliminarContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoProveedorTx.Dispose()
            objDContratoProveedorTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Modifica Solicitud Credito
    ''' </summary>
    ''' <param name="pESolicitudCredito">Objeto ESolicitudCredito(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificaSolicitudDocumentoProveedor(ByVal pESolicitudCredito As String) As Boolean Implements IDocProveedorTx.ModificaSolicitudDocumentoProveedor
        Dim objDSolicitudCreditTx As DSolicitudCreditoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDSolicitudCreditTx = New DSolicitudCreditoTx
            blnResultado = objDSolicitudCreditTx.ModificaSolicitudDocumentoProveedor(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolicitudCreditTx.Dispose()
            objDSolicitudCreditTx = Nothing
        End Try

        Return blnResultado
    End Function

    <AutoComplete(True)> _
    Public Function EnviarCartaDocumentoProveedor(ByVal pEGcc_contratoProveedor As String) As Boolean Implements IDocProveedorTx.EnviarCartaDocumentoProveedor
        Try

            Dim objDContratoProveedorTx As DContratoProveedorTx = Nothing
            Dim blnResultado As Boolean

            Try
                Dim objContratoProveedorList As ListEContratoProveedor = CFunciones.DeserializeObject(Of ListEContratoProveedor)(pEGcc_contratoProveedor)

                objDContratoProveedorTx = New DContratoProveedorTx

                Dim pEContratoProveedor As String
                For Each objContratoProveedor In objContratoProveedorList

                    pEContratoProveedor = CFunciones.SerializeObject(objContratoProveedor)
                    blnResultado = objDContratoProveedorTx.EnviarCartaDocumentoProveedor(pEContratoProveedor)

                Next objContratoProveedor



            Catch ex As Exception
                Throw ex
            Finally
                objDContratoProveedorTx.Dispose()
                objDContratoProveedorTx = Nothing
            End Try

            Return blnResultado

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LDocProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("E6F9ED56-08A2-4d65-9780-D018ABB66BCB") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocProveedorNTx")> _
Public Class LDocProveedorNTx
    Inherits ServicedComponent
    Implements IDocProveedorNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LDocProveedorNTx"
#End Region

#Region "Metodos"

#Region "Proveedor"

    ''' <summary>
    ''' Obtiene datos del Proveedor
    ''' </summary>
    ''' <param name="pstrCodProveedor">Codigo Proveedor</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 05/06/2012 
    ''' </remarks>
    Public Function ObtenerProveedor(ByVal pstrCodProveedor As String) As String Implements IDocProveedorNTx.ObtenerProveedor
        Try
            Using oDProveedorNTx As New DProveedorNTx
                Return oDProveedorNTx.ObtenerProveedor(pstrCodProveedor)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de los proveedores coincidentse con los criterios de búsqueda
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
                                     ByVal pRazonSocial As String) As String Implements IDocProveedorNTx.ListadoProveedor
        Dim objProveedorNTx As DProveedorNTx = Nothing
        Dim strResultado As String

        Try
            objProveedorNTx = New DProveedorNTx
            strResultado = objProveedorNTx.ListadoProveedor(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pCodigoTipoDocumento, _
                                                           pNumeroDocumento, _
                                                           pRazonSocial)
        Catch ex As Exception
            Throw ex
        Finally
            objProveedorNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado las cuentas del proveedor
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Public Function ListadoProveedorCuenta(ByVal pCodProveedor As String) As String Implements IDocProveedorNTx.ListadoProveedorCuenta
        Dim objProveedorNTx As DProveedorNTx = Nothing
        Dim strResultado As String

        Try
            objProveedorNTx = New DProveedorNTx
            strResultado = objProveedorNTx.ListadoProveedorCuenta(pCodProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objProveedorNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#Region "Contacto"
    ''' <summary>
    ''' Listado de los Contactos coincidentse con los criterios de búsqueda
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
                                     ByVal pCodProveedor As String) As String Implements IDocProveedorNTx.ListadoContacto
        Dim objContactoNTx As DContactoNTx = Nothing
        Dim strResultado As String

        Try
            objContactoNTx = New DContactoNTx
            strResultado = objContactoNTx.ListadoContacto(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objContactoNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#Region "Contrato Proveedor"
    ''' <summary>
    ''' Listado de Contrato Proveedor
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function ListadoContratoProveedor(ByVal pNumeroContrato As String) As String Implements IDocProveedorNTx.ListadoContratoProveedor
        Dim objDContratoProveedorNTx As DContratoProveedorNTx = Nothing
        Dim strResultado As String

        Try
            objDContratoProveedorNTx = New DContratoProveedorNTx
            strResultado = objDContratoProveedorNTx.ListadoContratoProveedor(pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoProveedorNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Valida que el registro ingresado no se duplique
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    Public Function ValidarContratoProveedor(ByVal pEContratoProveedor As String) As String Implements IDocProveedorNTx.ValidarContratoProveedor
        Dim objDContratoProveedorNTx As DContratoProveedorNTx = Nothing
        Dim strResultado As String

        Try
            objDContratoProveedorNTx = New DContratoProveedorNTx
            strResultado = objDContratoProveedorNTx.ValidarContratoProveedor(pEContratoProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoProveedorNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region
#End Region

End Class

#End Region
