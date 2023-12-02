

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
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 14/06/2012
''' </remarks>
<Guid("953FAE00-BD20-4c2e-8E18-2F9377430304") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocBienTx")> _
Public Class LDocBienTx
    Inherits ServicedComponent
    Implements IDocBienTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LDocBienTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarBien(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IDocBienTx.fblnModificarBien
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarBien(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarBienDesembolso(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IDocBienTx.fblnModificarBienDesembolso
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarBienDesembolso(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 15/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarVehiculo(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IDocBienTx.fblnModificarVehiculo
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarVehiculo(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : AEP
    ''' Fecha de Creación  : 19/07/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IDocBienTx.fblnModificarMaquinaria
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarMaquinaria(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function
    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : AEP
    ''' Fecha de Creación  : 16/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarDetalleMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IDocBienTx.fblnModificarDetalleMaquinaria
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarDetalleMaquinaria(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function
    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarOtros(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IDocBienTx.fblnModificarDetalleOtros
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarOtros(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function
#End Region

    <AutoComplete(True)> _
    Public Function fblnModificarRRPPBienContrato(ByVal pESolicitudCredito As String) As Boolean Implements IDocBienTx.fblnModificarRRPPBienContrato
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarRRPPBienContrato(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function
    <AutoComplete(True)> _
  Public Function fblnDehabilitarBien(ByVal CodSolicitudCredito As String, ByVal SecFinanciamiento As String, ByVal ComentarioBaja As String) As Boolean Implements IDocBienTx.fblnDehabilitarBien
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnDehabilitarBien(CodSolicitudCredito, SecFinanciamiento, ComentarioBaja)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function
    <AutoComplete(True)> _
    Public Function fblnContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean Implements IDocBienTx.ContratoDocumentoIns
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.ContratoDocumentoIns(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function
    ''' <summary>
    ''' Modificar BienContratoDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarBienContratoDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements IDocBienTx.ModificarBienContratoDocumento
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.ModificarBienContratoDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try
        Return blnResultado
    End Function
    <AutoComplete(True)> _
    Public Function fblnInsertarInafectacion(ByVal pEInafectacion As String) As Boolean Implements IDocBienTx.fblnInsertarInafectacion
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnInsertarInafectacion(pEInafectacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try
        Return blnResultado
    End Function
    <AutoComplete(True)> _
   Public Function fblnModificarInafectacion(ByVal pEInafectacion As String) As Boolean Implements IDocBienTx.fblnModificarInafectacion
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarInafectacion(pEInafectacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try
        Return blnResultado
    End Function

    <AutoComplete(True)> _
    Public Function fblnInsertarInscripcionMunicipal(ByVal pEInafectacion As String) As Boolean Implements IDocBienTx.fblnInsertarInscripcionMunicipal
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnInsertarInscripcionMunicipal(pEInafectacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try
        Return blnResultado
    End Function
    <AutoComplete(True)> _
   Public Function fblnModificarInscripcionMunicipal(ByVal pEInafectacion As String) As Boolean Implements IDocBienTx.fblnModificarInscripcionMunicipal
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificarInscripcionMunicipal(pEInafectacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try
        Return blnResultado
    End Function
End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LDocProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 14/06/2012
''' </remarks>
<Guid("4AD135EC-FF06-4bad-9DB8-40E6AAF60F20") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocBienNTx")> _
Public Class LDocBienNTx
    Inherits ServicedComponent
    Implements IDocBienNTx




#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LDocBienNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Devuelve el conjunto de bienes hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 14/06/2012
    ''' </remarks>
    Public Function ListadoBien(ByVal pPageSize As Integer, _
                         ByVal pCurrentPage As Integer, _
                         ByVal pSortColumn As String, _
                         ByVal pSortOrder As String, _
                         ByVal pstrSolicitudCreditoEstructura As String) As String Implements IDocBienNTx.ListadoBien
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListadoBien(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pstrSolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Devuelve la lista de contratos (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/09/2012
    ''' </remarks>
    Public Function ListadoContratoBien(ByVal pPageSize As Integer, _
                         ByVal pCurrentPage As Integer, _
                         ByVal pSortColumn As String, _
                         ByVal pSortOrder As String, _
                         ByVal pstrSolicitudCreditoEstructura As String) As String Implements IDocBienNTx.ListadoContratoBien
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListadoContratoBien(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pstrSolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene los datos del bien
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 15/06/2012 
    ''' </remarks>
    Public Function ObtenerBien(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerBien
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerBien(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene lista de proveedores asociado al bien
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 15/06/2012 
    ''' </remarks>
    Public Function ListadoBienProveedor(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ListadoBienProveedor
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListadoBienProveedor(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' ObtenerDatosBienContrato
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 27/09/2012 
    ''' </remarks>
    ''' 
    Public Function ObtenerDatosBienContrato(ByVal pstrNumeroContrato As String) As String Implements IDocBienNTx.ObtenerDatosBienContrato
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosBienContrato(pstrNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListaBienContratoInmuebles(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IDocBienNTx.ListaBienContratoInmuebles
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListaBienContratoInmuebles(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListaBienContratoInscripcionResgistral(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pStrEBienInscripcion As String) As String Implements IDocBienNTx.ListaBienContratoInscripcionResgistral
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListaBienContratoInscripcionResgistral(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pStrEBienInscripcion)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListaBienContratoInscripcionResgistralReporte(ByVal pStrEBienInscripcion As String) As String Implements IDocBienNTx.ListaBienContratoInscripcionResgistralReporte
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListaBienContratoInscripcionResgistralReporte(pStrEBienInscripcion)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListaBienContratoVehiculos(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IDocBienNTx.ListaBienContratoVehiculos
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListaBienContratoVehiculos(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListadoBienContratoMaquinariayOtros(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IDocBienNTx.ListadoBienContratoMaquinariayOtros
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListadoBienContratoMaquinariayOtros(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListadoBienContratoSistemas(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IDocBienNTx.ListadoBienContratoSistemasyOtros
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListadoBienContratoSistemas(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
    ''' <summary>
    ''' ObtenerDatosInmuebles
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 28/09/2012 
    ''' </remarks>
    ''' 
    Public Function ObtenerDatosInmuebles(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosInmuebles
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosInmuebles(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
    ''' <summary>
    ''' ObtenerDatosMaquinarias
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 02/10/2012 
    ''' </remarks>
    ''' 
    Public Function ObtenerDatosMaquinarias(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosMaquinarias
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosMaquinarias(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
      'IBK - JJM Inicio
    Public Function ObtenerMaxFilas(ByVal pstrNumeroContrato As String, ByVal pstrEstado As String) As String Implements IDocBienNTx.ObtenerMaxFilas
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerMaxFilas(pstrNumeroContrato, pstrEstado)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
    ''' <summary>
    ''' ObtenerDatosVehiculo
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 02/10/2012 
    ''' </remarks>
    ''' 
    Public Function ObtenerDatosVehiculo(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosVehiculo
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosVehiculo(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' ObtenerDatosSistemas
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 03/10/2012 
    ''' </remarks>
    ''' 
    Public Function ObtenerDatosSistemas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosSistemas
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosSistemas(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
    Public Function ObtenerDatosSistemasConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosSistemasConsulta
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosSistemasConsulta(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function


    Public Function ListaBienContratoDocumento(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String Implements IDocBienNTx.ListaBienContratoDocumento
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListaBienContratoDocumento(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodbien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Get de BienContratoDocuemento
    ''' </summary>
    ''' <returns>Devuelve un registro de Cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ObtenerBienContratoDocumento(ByVal pECotizacionDocumento As String) As String Implements IDocBienNTx.BienContratoDocumento


        Dim objDBienContratoDocumentoNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienContratoDocumentoNTx = New DBienNTx
            strResultado = objDBienContratoDocumentoNTx.ObtenerBienContratoDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienContratoDocumentoNTx.Dispose()
        End Try

        Return strResultado

    End Function

    Public Function ValidarDatosVehiculo(ByVal pEBienSolicitudEstructura As String) As String Implements IDocBienNTx.ValidarDatosVehiculo
        Dim objDBienContratoDocumentoNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienContratoDocumentoNTx = New DBienNTx
            strResultado = objDBienContratoDocumentoNTx.ValidarDatosVehiculo(pEBienSolicitudEstructura)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienContratoDocumentoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListaDatosInafectación(ByVal pCodSolitudCredito As String, ByVal pCodBien As String) As String Implements IDocBienNTx.ListaDatosInafectación
        Dim objDBienContratoDocumentoNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienContratoDocumentoNTx = New DBienNTx
            strResultado = objDBienContratoDocumentoNTx.ListaDatosInafectación(pCodSolitudCredito, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienContratoDocumentoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListaBienInscripcionMunicipal(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String Implements IDocBienNTx.ListaBienInscripcionMunicipal
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListaBienInscripcionMunicipal(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodbien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
    Public Function ValidarDatosPartida(ByVal pEBienSolicitudEstructura As String, ByVal Tipo As String) As String Implements IDocBienNTx.ValidarDatosPartida
        Dim objDBienContratoDocumentoNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienContratoDocumentoNTx = New DBienNTx
            strResultado = objDBienContratoDocumentoNTx.ValidarDatosPartida(pEBienSolicitudEstructura, Tipo)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienContratoDocumentoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ObtenerDatosBienContratoConsulta(ByVal pstrNumeroContrato As String) As String Implements IDocBienNTx.ObtenerDatosBienContratoConsulta
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosBienContratoConsulta(pstrNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ' Consultas

    Public Function ObtenerDatosInmueblesConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosInmueblesConsulta
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosInmueblesConsulta(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ObtenerDatosMaquinariasConsultas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosMaquinariasConsulta
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosMaquinariasConsultas(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ObtenerDatosVehiculoConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IDocBienNTx.ObtenerDatosVehiculoConsulta
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ObtenerDatosVehiculoConsulta(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListarDetalleBienConsulta(ByVal pstrEBien As String) As String Implements IDocBienNTx.ListarDetalleBienConsulta
        Dim objDBienNTx As DBienNTx = Nothing
        Dim strResultado As String

        Try
            objDBienNTx = New DBienNTx
            strResultado = objDBienNTx.ListarDetalleBienConsulta(pstrEBien)
        Catch ex As Exception
            Throw ex
        Finally
            objDBienNTx.Dispose()
        End Try

        Return strResultado
    End Function
#End Region



End Class

#End Region
