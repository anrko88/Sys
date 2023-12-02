

#Region "Clase Transaccional"

Public Class LBienTx

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function fblnModificarBien(ByVal pESolicitudCreditoEstructura As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarBien(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function fblnModificarBienDesembolso(ByVal pESolicitudCreditoEstructura As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarBienDesembolso(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function fblnModificarVehiculo(ByVal pESolicitudCreditoEstructura As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarVehiculo(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function fblnModificarMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarMaquinaria(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 16/10/2012
    ''' </remarks>
    Public Function fblnModificarDetalleMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarDetalleMaquinaria(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    Public Function fblnModificarOtros(ByVal pESolicitudCreditoEstructura As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarOtros(pESolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 05/10/2012
    ''' </remarks>
    Public Function fblnModificarRRPPBienContrato(ByVal pESolicitudCredito As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarRRPPBienContrato(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pCodSolicitudCredito">Codigo de Contrato</param>    
    ''' <param name="pSecFinanciamiento">Codigo del Bien</param>    
    ''' <param name="pComentarioBaja">Comentario de baja</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 08/10/2012
    ''' </remarks>
    Public Function fblnDehabilitarBien(ByVal pCodSolicitudCredito As String, ByVal pSecFinanciamiento As String, ByVal pComentarioBaja As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnDehabilitarBien(pCodSolicitudCredito, pSecFinanciamiento, pComentarioBaja)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Codigo de documento</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 12/10/2012
    ''' </remarks>
    Public Function fblnContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnContratoDocumentoIns(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar BienContrato Documento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Listado de Objeto BienContrato(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    Public Function ModificarBienContratoDocumento(ByVal pECotizacionDocumento As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.ModificarBienContratoDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar BienContrato Documento
    ''' </summary>
    ''' <param name="pEInafectacion">Listado de Objeto BienContrato(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnInsertarInafectacion(ByVal pEInafectacion As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnInsertarInafectacion(pEInafectacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar BienContrato Documento
    ''' </summary>
    ''' <param name="pEInafectacion">Listado de Objeto BienContrato(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnModificarInafectacion(ByVal pEInafectacion As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarInafectacion(pEInafectacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar BienContrato Documento
    ''' </summary>
    ''' <param name="pEInafectacion">Listado de Objeto BienContrato(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnInsertarInscripcionMunicipal(ByVal pEInafectacion As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnInsertarInscripcionMunicipal(pEInafectacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar BienContrato Documento
    ''' </summary>
    ''' <param name="pEInafectacion">Listado de Objeto BienContrato(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnModificarInscripcionMunicipal(ByVal pEInafectacion As String) As Boolean
        Dim objLBienTx As Object = CreateObject("GCC.Logic.LDocBienTx")
        Try
            Return objLBienTx.fblnModificarInscripcionMunicipal(pEInafectacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


#End Region

#Region "Clase No Transaccional"

Public Class LBienNTx

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
                         ByVal pstrSolicitudCreditoEstructura As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListadoBien(pPageSize, _
                                           pCurrentPage, _
                                           pSortColumn, _
                                           pSortOrder, _
                                           pstrSolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoContratoBien(ByVal pPageSize As Integer, _
                      ByVal pCurrentPage As Integer, _
                      ByVal pSortColumn As String, _
                      ByVal pSortOrder As String, _
                      ByVal pstrSolicitudCreditoEstructura As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListadoContratoBien(pPageSize, _
                                           pCurrentPage, _
                                           pSortColumn, _
                                           pSortOrder, _
                                           pstrSolicitudCreditoEstructura)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function ObtenerBien(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerBien(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

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
    Public Function ObtenerDatosBienContrato(ByVal pstrNumeroContrato As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosBienContrato(pstrNumeroContrato)
        Catch ex As Exception
            Throw ex
        End Try

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
    Public Function ListadoBienProveedor(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListadoBienProveedor(pstrNumeroContrato,pCodBien)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene lista de bines por contrato(inmuebles)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 27/09/2012 
    ''' </remarks>
    Public Function ListaBienContratoInmuebles(ByVal pPageSize As Integer, _
                    ByVal pCurrentPage As Integer, _
                    ByVal pSortColumn As String, _
                    ByVal pSortOrder As String, _
                    ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListaBienContratoInmuebles(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListaBienContratoInscripcionResgistral(ByVal pPageSize As Integer, _
                ByVal pCurrentPage As Integer, _
                ByVal pSortColumn As String, _
                ByVal pSortOrder As String, _
                ByVal pStrEBienInscripcion As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListaBienContratoInscripcionResgistral(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pStrEBienInscripcion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene lista de bines por contrato(Vehiculos)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 27/09/2012 
    ''' </remarks>
    Public Function ListaBienContratoVehiculos(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListaBienContratoVehiculos(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Obtiene lista de bines por contrato(Maquinarias, sistemas y otros)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 27/09/2012 
    ''' </remarks>
    Public Function ListadoBienContratoMaquinaria(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListadoBienContratoMaquinariayOtros(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene lista de bines por contrato(sistemas y otros)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 03/10/2012 
    ''' </remarks>
    Public Function ListadoBienContratoSistemas(ByVal pPageSize As Integer, _
                   ByVal pCurrentPage As Integer, _
                   ByVal pSortColumn As String, _
                   ByVal pSortOrder As String, _
                   ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListadoBienContratoSistemas(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodEstadoLogico)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Obtiene los datos del bien
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 28/09/2012 
    ''' </remarks>
    Public Function ObtenerDatosInmueble(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosInmuebles(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Obtiene los datos de maquinaria, sistermas y otros
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 02/10/2012 
    ''' </remarks>
    Public Function ObtenerDatosMaquinarias(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosMaquinarias(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Obtiene los datos de vehiculos
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 02/10/2012 
    ''' </remarks>
    Public Function ObtenerDatosVehiculo(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosVehiculo(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerMaxFilas(ByVal pstrNumeroContrato As String, ByVal pstrEstado As String) As String

        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try

            Return objLBienNTx.ObtenerMaxFilas(pstrNumeroContrato, pstrEstado)

        Catch ex As Exception

            Throw ex

        End Try

    End Function



    ''' <summary>
    ''' Obtiene los datos de sistermas y otros
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 03/10/2012 
    ''' </remarks>
    Public Function ObtenerDatosSistemas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosSistemas(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ObtenerDatosSistemasConsultas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosSistemasConsulta(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListaBienContratoDocumento(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListaBienContratoDocumento(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodbien)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ObtenerBienContratoDocumento
    ''' </summary>
    ''' <returns>Devuelve un registro de cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ObtenerBienContratoDocumento(ByVal pEBienContratoDocumento As String) As String

        Dim objLCotizacionNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try
            Return objLCotizacionNTx.ObtenerBienContratoDocumento(pEBienContratoDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function ValidarDatosPartida(ByVal pEBienSolicitudEstructura As String, ByVal Tipo As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ValidarDatosPartida(pEBienSolicitudEstructura, Tipo)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function ValidarDatosVehiculo(ByVal pEBienSolicitudEstructura As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ValidarDatosVehiculo(pEBienSolicitudEstructura)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function ListaDatosInafectacion(ByVal pCodSolitudCredito As String, ByVal pCodBien As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ListaDatosInafectación(pCodSolitudCredito, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListaBienInscripcionMunicipal(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")

        Try
            Return objLBienNTx.ListaBienInscripcionMunicipal(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodsolicitudcredito, _
                                                   pCodbien)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function ObtenerDatosBienContratoConsulta(ByVal pstrNumeroContrato As String) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosBienContratoConsulta(pstrNumeroContrato)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ObtenerDatosInmueblesConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosInmueblesConsulta(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ObtenerDatosMaquinariasConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosMaquinariasConsultas(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ObtenerDatosVehiculoConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ObtenerDatosVehiculoConsulta(pstrNumeroContrato, pCodBien)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarDetalleBienConsulta(ByVal pstrEBien As String) As String
        ' Public Function ListarDetalleBienConsulta() As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ListarDetalleBienConsulta(pstrEBien)
            'Return objLBienNTx.ListarDetalleBienConsulta()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListaBienContratoInscripcionResgistralReporte(ByVal pstrEBien As String) As String
        ' Public Function ListarDetalleBienConsulta() As String
        Dim objLBienNTx As Object = CreateObject("GCC.Logic.LDocBienNTx")
        Try

            Return objLBienNTx.ListaBienContratoInscripcionResgistralReporte(pstrEBien)
            'Return objLBienNTx.ListarDetalleBienConsulta()
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class

#End Region

