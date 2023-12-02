
#Region "Clase Transaccional"

Public Class LDesembolsoTx

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function InsertarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
        Dim objDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objDesembolsoNTx.InsertarContratoEstructDoc(pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Modificar el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function ModificarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
        Dim objDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objDesembolsoNTx.ModificarContratoEstructDoc(pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar el Documento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 06/06/2012
    ''' </remarks>
    Public Function EliminarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
        Dim objDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objDesembolsoNTx.EliminarContratoEstructDoc(pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Insertar DesembolsoBien
    ''' </summary>
    ''' <param name="pEContratoEstructDocDet">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function InsertarDesembolsoBien(ByVal pEContratoEstructDocDet As String) As Boolean
        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.InsertarContratoEstructDocDet(pEContratoEstructDocDet)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar DesembolsoBien
    ''' </summary>
    ''' <param name="pEContratoEstructDocDet">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function EliminarDesembolsoBien(ByVal pEContratoEstructDocDet As String) As Boolean
        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.EliminarContratoEstructDocDet(pEContratoEstructDocDet)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Generar WIO - En desembolso (Formalizacion)
    ''' </summary>
    ''' <param name="pstrClienteWIO">Entidad Serializado del Cliente WIO</param>
    ''' <param name="pstrInstruccionWIO">Entidad serializada de la Instruccion WIO</param>    
    ''' <param name="pstrCaracteristicasWIO">Entidad serializada de la caracteristicas WIO</param>
    ''' <param name="pstrSeguimientoWIO">Entidad del segimiento WIO</param>
    ''' <param name="pXmlEContratoEstructDoc">Datatable serializado de COntrato Estructu doc</param>
    ''' <param name="pstrNroContrato">Numero de COntrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    Public Function RegistrarWIO(ByVal pstrClienteWIO As String, _
                                 ByVal pstrInstruccionWIO As String, _
                                 ByVal pstrCaracteristicasWIO As String, _
                                 ByVal pstrSeguimientoWIO As String, _
                                 ByVal xmlCtaContratoLeasing As String, _
                                 ByVal xmlBienleasing As String, _
                                 ByVal pXmlEContratoEstructDoc As String, _
                                 ByVal pstrNroContrato As String, _
                                 ByVal pstrNroLinea As String, _
                                 ByVal pstrEsActivacion As String) As String
        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.RegistrarWIO(pstrClienteWIO, _
                                                pstrInstruccionWIO, _
                                                pstrCaracteristicasWIO, _
                                                pstrSeguimientoWIO, _
                                                xmlCtaContratoLeasing, _
                                                xmlBienleasing, _
                                                pXmlEContratoEstructDoc, _
                                                pstrNroContrato, _
                                                pstrNroLinea, _
                                                pstrEsActivacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Registra LINEA Y TASA WIO - En desembolso (Formalizacion)
    ''' </summary>
    ''' <param name="pstrLineaOPWIO">Entidad Serializada de la Linea de credito WIO</param>
    ''' <param name="pstrTasaComisionWIO">Datatable serializado en Xml WIO</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 27/06/2012
    ''' </remarks>
    Public Function RegistrarLineaWIO(ByVal pstrLineaOPWIO As String, _
                                    ByVal pstrTasaComisionWIO As String, _
                                    ByVal pstrNroInstruccion As String) As Boolean

        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.RegistrarLineaWIO(pstrLineaOPWIO, pstrTasaComisionWIO, pstrNroInstruccion)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' ActualizarIGVDesembolso
    ''' </summary>
    ''' <param name="strNumContrato">Numero de Contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/09/2012
    ''' </remarks>
    Public Function ActualizarIGVDesembolso(ByVal strNumContrato As String) As Integer

        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.ActualizarIGVDesembolso(strNumContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoTx = Nothing
        End Try
    End Function

    ' Inicio IBK
    ''' <summary>
    ''' Insertar Relacion
    ''' </summary>
    ''' <param name="strArrayDocs">Arreglo de tipodoc;nrodoc;codprov;fecha(char8)|....|.... </param>
    ''' <param name="nbrArraySize">Tamaño del arreglo</param>
    ''' <returns></returns>
    ''' <remarks> El arreglo esta separado por ;
    ''' Creado Por         : IBK - AAE
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function AgregarRelacion(ByVal strNroContrato As String, _
                                                   ByVal strSecBien As String, _
                                                   ByVal strArrayDocs As String, _
                                                   ByVal nbrArraySize As Integer) As Boolean
        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.AgregarRelacion(strNroContrato, strSecBien, strArrayDocs, nbrArraySize)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' Fin IBK


#Region "WebService GCC"
    ''' <summary>
    ''' ModificaEstadoDocumentoWS
    ''' </summary>
    ''' <param name="strEGcc_desembolso"></param>
    ''' <param name="pTipoAprobacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModificaEstadoDocumentoWS(ByVal strEGcc_desembolso As String, ByVal pTipoAprobacion As String) As Boolean
        Dim objLDesembolsoTx As Object = CreateObject("GCC.Logic.LDesembolsoTx")
        Try
            Return objLDesembolsoTx.ModificaEstadoDocumentoWS(strEGcc_desembolso, pTipoAprobacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoTx = Nothing
        End Try
    End Function
#End Region



End Class

#End Region

#Region "Clase No Transaccional"

Public Class LDesembolsoNTx

    ''' <summary>
    ''' Listado de Bienes
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoDocumentoBienes(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEContratoEstructDoc As String) As String

        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListadoContratoEstructDocDet(pPageSize, _
                                                                   pCurrentPage, _
                                                                   pSortColumn, _
                                                                   pSortOrder, _
                                                                   pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Valida existencia de documento
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ValidaContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String

        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ValidaContratoEstructDoc(pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListarContratoEstructDoc(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListarContratoEstructDoc(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 07/03/2013
    ''' </remarks>
    Public Function ListarContratoEstructDocConsulta(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListarContratoEstructDocConsulta(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListarContratoEstructDocAsociar(ByVal pPageSize As Integer, _
                                                    ByVal pCurrentPage As Integer, _
                                                    ByVal pSortColumn As String, _
                                                    ByVal pSortOrder As String, _
                                                    ByVal pEContratoEstructDoc As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListarContratoEstructDocAsociar(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el dato especifico de los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ObtenerContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ObtenerContratoEstructDoc(pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Estructura para insertar en WIO los bienes
    ''' </summary>
    ''' <param name="pXmlEContratoEstructDoc"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 20/06/2012
    ''' </remarks>
    Public Function ObtenerBienLeasingWIO(ByVal pXmlEContratoEstructDoc As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ObtenerBienLeasingWIO(pXmlEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener las Cuentas de un contrato para WIO
    ''' </summary>
    ''' <param name="pstrNroContrato">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 20/06/2012
    ''' </remarks>
    Public Function ObtenerCtaLeasingWioSel(ByVal pstrNroContrato As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ObtenerCtaLeasingWioSel(pstrNroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los bienes
    ''' </summary>
    ''' <param name="pstrNroDocumento"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 27/06/2012
    ''' </remarks>
    Public Function ObtenerAgenteRetencion(ByVal pstrNroDocumento As String) As Integer
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ObtenerAgenteRetencion(pstrNroDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ' INICIO IBK
    ''' <summary>
    ''' Lista bienes del contrato
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaBienesContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNumeroContrato As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListaBienesContrato(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista documentos del contrato
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNumeroContrato As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListaDocumentosContrato(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function
    
    ''' <summary>
    ''' Lista Relaciones del contrato
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaRelacionesContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNumeroContrato As String) As String
        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListaRelacionesContrato(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLDesembolsoNTx = Nothing
        End Try
    End Function
    'FIN IBK
End Class

#End Region

