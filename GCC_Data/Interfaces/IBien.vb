Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICartaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("075B993F-B63D-421e-8C19-B3E84D44FB0E")> _
Public Interface IBienTx

    Function fblnInsertar(ByVal pESolicitudCreditoEstructura As String, _
                          ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean

    Function fblnModificar(ByVal pESolicitudCreditoEstructura As String, _
                           ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean

    Function fblnModificarBienDesembolso(ByVal pESolicitudCreditoEstructura As String) As Boolean
    Function fblnModificarBien(ByVal pESolicitudCreditoEstructura As String) As Boolean

    Function fblnModificarMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean
    Function fblnModificarDetalleMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean
    Function fblnModificarDetalleOtros(ByVal pESolicitudCreditoEstructura As String) As Boolean
    Function fblnModificarVehiculo(ByVal pESolicitudCreditoEstructura As String) As Boolean
    Function fblnModificarRRPPBienContrato(ByVal pESolicitudCredito As String) As Boolean

    Function fblnEliminar(ByVal pESolicitudCreditoEstructura As String) As Boolean
    Function fblnDehabilitarBien(ByVal CodSolicitudCredito As String, ByVal SecFinanciamiento As String, ByVal ComentarioBaja As String) As Boolean


    '' <summary>
    ''' Insertar Documentos
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 12/10/2012
    ''' </remarks>
    Function ContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean
    Function ModificarBienContratoDocumento(ByVal pEBienContratoDocumento As String) As Boolean
    Function fblnInsertarInafectacion(ByVal pEInafectacion As String) As Boolean
    Function fblnModificarInafectacion(ByVal pEInafectacion As String) As Boolean

    Function fblnInsertarInscripcionMunicipal(ByVal pEInafectacion As String) As Boolean
    Function fblnModificarInscripcionMunicipal(ByVal pEInafectacion As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase BienNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("7F60D3C9-A041-4740-B7CA-8EC60E71469A")> _
Public Interface IBienNTx

    Function fobjLeer(ByVal pESolicitudCreditoEstructura As String) As String

    Function Lista(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, _
                   ByVal pCodProveedor As String) As String

    Function ListadoBien(ByVal pPageSize As Integer, _
                              ByVal pCurrentPage As Integer, _
                              ByVal pSortColumn As String, _
                              ByVal pSortOrder As String, _
                              ByVal pstrSolicitudCreditoEstructura As String) As String

    Function ObtenerBien(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String

    Function ObtenerDatosBienContrato(ByVal pstrNumeroContrato As String) As String

    Function ListadoBienProveedor(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String

    Function ListadoContratoBien(ByVal pPageSize As Integer, _
                              ByVal pCurrentPage As Integer, _
                              ByVal pSortColumn As String, _
                              ByVal pSortOrder As String, _
                              ByVal pstrSolicitudCreditoEstructura As String) As String

    Function ListaBienContratoInscripcionResgistral(ByVal pPageSize As Integer, _
                  ByVal pCurrentPage As Integer, _
                  ByVal pSortColumn As String, _
                  ByVal pSortOrder As String, _
                  ByVal pCodEstadoLogico As String) As String

    Function ListadoBienContratoMaquinariayOtros(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, _
                   ByVal pCodEstadoLogico As String) As String

    Function ListadoBienContratoSistemasyOtros(ByVal pPageSize As Integer, _
                  ByVal pCurrentPage As Integer, _
                  ByVal pSortColumn As String, _
                  ByVal pSortOrder As String, _
                  ByVal pCodsolicitudcredito As String, _
                  ByVal pCodEstadoLogico As String) As String

    Function ListaBienContratoVehiculos(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, _
                   ByVal pCodEstadoLogico As String) As String

    Function ListaBienContratoInmuebles(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, _
                   ByVal pCodEstadoLogico As String) As String

    Function ObtenerDatosInmuebles(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
    Function ObtenerDatosMaquinarias(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
    Function ObtenerDatosSistemas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
    Function ObtenerDatosVehiculo(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
    Function ObtenerMaxFilas(ByVal pstrNumeroContrato As String, ByVal pstrEstado As String) As String
    Function ListaBienContratoDocumento(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String
    Function BienContratoDocumento(ByVal pECotizacionDocumento As String) As String
    Function ValidarDatosVehiculo(ByVal pEBienSolicitudEstructura As String) As String
    Function ListaDatosInafectación(ByVal pCodSolitudCredito As String, ByVal pCodBien As String) As String
    Function ListaBienInscripcionMunicipal(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String

    Function ValidarDatosPartida(ByVal pEBienSolicitudEstructura As String, ByVal Tipo As String) As String

    Function ObtenerDatosBienContratoConsulta(ByVal pstrNumeroContrato As String) As String

    Function ObtenerDatosInmueblesConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String

    Function ObtenerDatosMaquinariasConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String

    Function ObtenerDatosVehiculoConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String

    Function ObtenerDatosSistemasConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String

    Function ListarDetalleBienConsulta(ByVal pstrEBien As String) As String

    Function ListaBienContratoInscripcionResgistralReporte(ByVal pCodEstadoLogico As String) As String

End Interface

#End Region
