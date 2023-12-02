
Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class SituacionCredito_frmSituacionCreditoRegistro
    Inherits GCCBase
    ReadOnly _objLog As New GCCLog("frmSituacionCreditoRegistro.aspx.vb")
    Protected strNroContrato As String
    Protected strCotizacion As String
    Protected strEstado As String
    Protected strCantidadBienes As String
    Protected strDescripcionBien As String
    Protected strRuc As String
    Protected strCliente As String
    Protected strNumeroCta As String
    Protected strTitulo As String

#Region "Eventos"
    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            _objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                _objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                Call InicializarParametros()

                Dim codSolicitudCredito As String
                codSolicitudCredito = Request.QueryString("hddCodigo")
                hddCodigoContrato.Value = codSolicitudCredito

                Call LeerContrato(codSolicitudCredito)

                Call LeerDatosCliente(codSolicitudCredito)

                If hddCodigoCotizacion.Value <> "" Then
                    Call LeerCotizacion(hddCodigoCotizacion.Value)
                End If

                Call LeerTarifas(hddCodProductoFinancieroActivo.Value, _
                                 hddCodMoneda.Value)

                Call ValoresPredeterminados()
            End If

        Catch ex As ApplicationException
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"

#Region "Generales"

    ''' <summary>
    ''' Lee los parametros predefinidos y valores ppredeterminado válidos para el contrato.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub InicializarParametros()


        hddMensajeCorreo.Value = GCCConstante.C_MENSAJE_CORREO_CONTRATO

        ' Se lee la fecha del servidor para evitar problemas con la configuración del equipo host del cliente.
        hddFechaActual.Value = Now.ToString("dd/MM/yyyy")
    End Sub

#End Region

#Region "Contrato"

    ''' <summary>
    ''' Devuelve todos los documentos del contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodigo">Código del contrato.</param>
    ''' <param name="pFlagFiltro"></param>
    ''' <param name="pFlagEnvioCarta"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
        Public Shared Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                                       ByVal pCurrentPage As Integer, _
                                                       ByVal pSortColumn As String, _
                                                       ByVal pSortOrder As String, _
                                                       ByVal pCodigo As String, _
                                                       ByVal pFlagFiltro As Integer, _
                                                       ByVal pFlagEnvioCarta As Integer, _
                                                       ByVal pFields As String) As JQGridJsonResponse
        Dim objCondicionAdicionalNTx As New LCheckListNTx
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim strEGccContratodocumento As String

        With oEGccContratodocumento
            .Numerocontrato = GCCUtilitario.NullableString(pCodigo)
            .Flagfiltro = GCCUtilitario.CheckInt(pFlagFiltro)
            .Flagcartaenvio = GCCUtilitario.CheckInt(pFlagEnvioCarta)
        End With
        strEGccContratodocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEGccContratodocumento)
        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCondicionAdicionalNTx.ContratoDocumentoSel(pPageSize, _
                                                                                                                                           pCurrentPage, _
                                                                                                                                           pSortColumn, _
                                                                                                                                           pSortOrder, _
                                                                                                                                           strEGccContratodocumento))
        Dim oJQGridJsonResponse As New JQGridJsonResponse
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtCondicionAdicional.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtCondicionAdicional, _
                                                               pFields)

    End Function

    ''' <summary>
    ''' Guarda los cambios a los datos del contrato, sin cambiar su estado.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <param name="pClasificacionContrato"></param>
    ''' <param name="pCodigoEstadoContrato"></param>
    ''' <param name="pFechaRegistroPublico"></param>
    ''' <param name="pFechaFirmaNotaria">Fecha de la firma en notaria</param>
    ''' <param name="pCodigoEstadoCivil">Estado civil del cliente.</param>
    ''' <param name="pNombreConyuge">Nombre del conyugue, si el cliente es casado.</param>
    ''' <param name="pTipoDocumentoConyuge">Tipo de documento de identidad del conyugue.</param>
    ''' <param name="pnumerodocumento">Número de documento de identidad del conyugue, si el cliente es casado.</param>
    ''' <param name="pImporteAtrasoPorc">Porcentaje del importe pendiente de pago, por día de atraso.</param>
    ''' <param name="pOtrasPenalidades">Descripción de otras penalidades.</param>
    ''' <param name="pdiasVencimiento">Días desde el vencimiento a la fecha de pago.</param>
    ''' <param name="pPorcentajeCuota">Porcentaje de la cuota.</param>
    ''' <param name="pModificado"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarContratoActualizar(ByVal pCodigoContrato As String, _
                                                     ByVal pClasificacionContrato As String, _
                                                     ByVal pCodigoEstadoContrato As String, _
                                                     ByVal pFechaRegistroPublico As String, _
                                                     ByVal pFechaFirmaNotaria As String, _
                                                     ByVal pCodigoEstadoCivil As String, _
                                                     ByVal pNombreConyuge As String, _
                                                     ByVal pTipoDocumentoConyuge As String, _
                                                     ByVal pnumerodocumento As String, _
                                                     ByVal pImporteAtrasoPorc As String, _
                                                     ByVal pOtrasPenalidades As String, _
                                                     ByVal pdiasVencimiento As String, _
                                                     ByVal pPorcentajeCuota As String, _
                                                     ByVal pClienteRazonSocial As String, _
                                                     ByVal pClienteDomicilioLegal As String, _
                                                     ByVal pModificado As Boolean) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            Dim objEGccContratootroconcepto As New EGcc_contratootroconcepto
            Dim pEGccContratootroconcepto As String

            ' Datos del contrato
            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato

            objESolicitudCredito.CodigoClasificacionContrato = pClasificacionContrato

            If (pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEDECARTA OrElse _
                pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ENVIADOCLIENTE OrElse _
                pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA OrElse _
                pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEENVIO) AndAlso _
                pModificado Then
                ' objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ENVIADOCLIENTE
                objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEDECARTA
            Else
                objESolicitudCredito.Codigoestadocontrato = GCCUtilitario.NullableString(pCodigoEstadoContrato)
            End If

            objESolicitudCredito.FechaRegistroPublico = GCCUtilitario.StringToDateTime(pFechaRegistroPublico)
            objESolicitudCredito.Fechafirmanotaria = GCCUtilitario.StringToDateTime(pFechaFirmaNotaria)
            objESolicitudCredito.CodigoEstadoCivil = GCCUtilitario.NullableString(pCodigoEstadoCivil)
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            ' Datos del Cónyuge
            objESolicitudCredito.Nombreconyuge = GCCUtilitario.NullableString(pNombreConyuge)
            objESolicitudCredito.Codigotipodocconyuge = GCCUtilitario.NullableString(pTipoDocumentoConyuge)
            objESolicitudCredito.Numerodocumentoconyuge = GCCUtilitario.NullableString(pnumerodocumento)

            ' Tasas y Comisiones
            objEGccContratootroconcepto.Numerocontrato = pCodigoContrato

            ' Penalidades
            objEGccContratootroconcepto.Importependiente = GCCUtilitario.StringToDecimal(pImporteAtrasoPorc)
            objEGccContratootroconcepto.Otraspenalidades = GCCUtilitario.NullableString(pOtrasPenalidades)
            objEGccContratootroconcepto.Diasvencimiento = GCCUtilitario.StringToInteger(pdiasVencimiento)
            objEGccContratootroconcepto.Porcentajecuota = GCCUtilitario.StringToDecimal(pPorcentajeCuota)
            objEGccContratootroconcepto.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            ' Guardar si hubo algún cambio
            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.Modificado = False 'pModificado
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            objESolicitudCredito.ClienteRazonSocial = GCCUtilitario.NullableString(pClienteRazonSocial)
            objESolicitudCredito.ClienteDomicilioLegal = GCCUtilitario.NullableString(pClienteDomicilioLegal)

            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
            pEGccContratootroconcepto = GCCUtilitario.SerializeObject(objEGccContratootroconcepto)


            Dim blnResult As Boolean = objContratoTx.fblnModificarContrato(pESolicitudCredito, _
                                                                           pEGccContratootroconcepto)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Guarda los datos del contrato y actualiza el estado del contrato al siguiente.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <param name="pClasificacionContrato"></param>
    ''' <param name="pFechaRegistroPublico"></param>
    ''' <param name="pFechaFirmaNotaria">Fecha de la firma en notaria</param>
    ''' <param name="pCodigoEstadoCivil">Estado civil del cliente.</param>
    ''' <param name="pNombreConyuge">Nombre del conyugue, si el cliente es casado.</param>
    ''' <param name="pTipoDocumentoConyuge">Tipo de documento de identidad del conyugue.</param>
    ''' <param name="pnumerodocumento">Número de documento de identidad del conyugue, si el cliente es casado.</param>
    ''' <param name="pImporteAtrasoPorc">Porcentaje del importe pendiente de pago, por día de atraso.</param>
    ''' <param name="pOtrasPenalidades">Descripción de otras penalidades.</param>
    ''' <param name="pdiasVencimiento">Días desde el vencimiento a la fecha de pago.</param>
    ''' <param name="pPorcentajeCuota">Porcentaje de la cuota.</param>
    ''' <param name="pCodigoEstadoContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarYEnviar(ByVal pCodigoContrato As String, _
                                          ByVal pClasificacionContrato As String, _
                                          ByVal pFechaRegistroPublico As String, _
                                          ByVal pFechaFirmaNotaria As String, _
                                          ByVal pCodigoEstadoCivil As String, _
                                          ByVal pNombreConyuge As String, _
                                          ByVal pTipoDocumentoConyuge As String, _
                                          ByVal pnumerodocumento As String, _
                                          ByVal pImporteAtrasoPorc As String, _
                                          ByVal pOtrasPenalidades As String, _
                                          ByVal pdiasVencimiento As String, _
                                          ByVal pPorcentajeCuota As String, _
                                          ByVal pClienteRazonSocial As String, _
                                          ByVal pClienteDomicilioLegal As String, _
                                          ByVal pCodigoEstadoContrato As String) As String
        Try
            ' Si esta en estado elaborado, pasa al estado pendiente de carta
            If pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ELABORADO Then
                Dim objESolicitudCredito As New ESolicitudcredito
                Dim objEGcc_contratootroconcepto As New EGcc_contratootroconcepto
                Dim objContratoTx As New LContratoTx
                Dim pESolicitudCredito As String
                Dim pEGcc_contratootroconcepto As String

                ' Datos del contrato
                objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
                objESolicitudCredito.CodigoClasificacionContrato = pClasificacionContrato
                objESolicitudCredito.FechaRegistroPublico = GCCUtilitario.StringToDateTime(pFechaRegistroPublico)
                objESolicitudCredito.Fechafirmanotaria = GCCUtilitario.StringToDateTime(pFechaFirmaNotaria)
                objESolicitudCredito.CodigoEstadoCivil = GCCUtilitario.NullableString(pCodigoEstadoCivil)

                ' Datos del Cónyuge
                objESolicitudCredito.Nombreconyuge = GCCUtilitario.NullableString(pNombreConyuge)
                objESolicitudCredito.Codigotipodocconyuge = GCCUtilitario.NullableString(pTipoDocumentoConyuge)
                objESolicitudCredito.Numerodocumentoconyuge = GCCUtilitario.NullableString(pnumerodocumento)
                objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEDECARTA
                objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                ' Tasas y Comisiones
                objEGcc_contratootroconcepto.Numerocontrato = pCodigoContrato

                ' Penalidades
                objEGcc_contratootroconcepto.Importependiente = GCCUtilitario.StringToDecimal(pImporteAtrasoPorc)
                objEGcc_contratootroconcepto.Otraspenalidades = GCCUtilitario.NullableString(pOtrasPenalidades)
                objEGcc_contratootroconcepto.Diasvencimiento = GCCUtilitario.StringToInteger(pdiasVencimiento)
                objEGcc_contratootroconcepto.Porcentajecuota = GCCUtilitario.StringToDecimal(pPorcentajeCuota)
                objEGcc_contratootroconcepto.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                objESolicitudCredito.ClienteRazonSocial = GCCUtilitario.NullableString(pClienteRazonSocial)
                objESolicitudCredito.ClienteDomicilioLegal = GCCUtilitario.NullableString(pClienteDomicilioLegal)

                pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
                pEGcc_contratootroconcepto = GCCUtilitario.SerializeObject(objEGcc_contratootroconcepto)

                Dim blnResult As Boolean = objContratoTx.ContratoGuardarYEnviar(pESolicitudCredito, _
                                                                                pEGcc_contratootroconcepto)

                If blnResult Then
                    Return "0"
                Else
                    Return "1"
                End If
                ' Si esta en estado pendiente de firma, debe poder enviar la carta si hay una fecha de firma en notaria
            ElseIf pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA Then
                ' La fecha de firma en notaria es un dato válido.
                If (Not String.IsNullOrEmpty(pFechaFirmaNotaria)) Then
                    Dim objESolicitudCredito As New ESolicitudcredito
                    Dim objEGcc_contratootroconcepto As New EGcc_contratootroconcepto
                    Dim objContratoTx As New LContratoTx
                    Dim pESolicitudCredito As String
                    Dim pEGcc_contratootroconcepto As String

                    ' Datos del contrato
                    objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
                    objESolicitudCredito.Clasificacioncontrato = pClasificacionContrato
                    objESolicitudCredito.FechaRegistroPublico = GCCUtilitario.StringToDateTime(pFechaRegistroPublico)
                    objESolicitudCredito.Fechafirmanotaria = GCCUtilitario.StringToDateTime(pFechaFirmaNotaria)

                    ' Datos del Cónyuge
                    objESolicitudCredito.Nombreconyuge = GCCUtilitario.NullableString(pNombreConyuge)
                    objESolicitudCredito.Codigotipodocconyuge = GCCUtilitario.NullableString(pTipoDocumentoConyuge)
                    objESolicitudCredito.Numerodocumentoconyuge = GCCUtilitario.NullableString(pnumerodocumento)
                    objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_FORMALIZADO
                    objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                    ' Tasas y Comisiones
                    objEGcc_contratootroconcepto.Numerocontrato = pCodigoContrato
                    ' Penalidades
                    objEGcc_contratootroconcepto.Importependiente = GCCUtilitario.StringToDecimal(pImporteAtrasoPorc)
                    objEGcc_contratootroconcepto.Otraspenalidades = GCCUtilitario.NullableString(pOtrasPenalidades)
                    objEGcc_contratootroconcepto.Diasvencimiento = GCCUtilitario.StringToInteger(pdiasVencimiento)
                    objEGcc_contratootroconcepto.Porcentajecuota = GCCUtilitario.StringToDecimal(pPorcentajeCuota)
                    objEGcc_contratootroconcepto.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                    pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
                    pEGcc_contratootroconcepto = GCCUtilitario.SerializeObject(objEGcc_contratootroconcepto)

                    Dim blnResult As Boolean = objContratoTx.ContratoGuardarYEnviar(pESolicitudCredito, _
                                                                                    pEGcc_contratootroconcepto)

                    If blnResult Then
                        Return "0"
                    Else
                        Return "1"
                    End If
                Else
                    Throw New Exception("Debe seleccionar la fecha de firma en notaría.")
                End If

            ElseIf pCodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_FORMALIZADO Then
                ' La fecha de registros públicos es un dato válido.
                If (Not String.IsNullOrEmpty(pFechaRegistroPublico)) Then
                    Dim objESolicitudCredito As New ESolicitudcredito
                    Dim objEGcc_contratootroconcepto As New EGcc_contratootroconcepto
                    Dim objContratoTx As New LContratoTx
                    Dim pESolicitudCredito As String
                    Dim pEGcc_contratootroconcepto As String

                    ' Datos del contrato
                    objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
                    objESolicitudCredito.Clasificacioncontrato = pClasificacionContrato
                    objESolicitudCredito.FechaRegistroPublico = GCCUtilitario.StringToDateTime(pFechaRegistroPublico)
                    objESolicitudCredito.Fechafirmanotaria = GCCUtilitario.StringToDateTime(pFechaFirmaNotaria)
                    ' Datos del Cónyuge
                    objESolicitudCredito.Nombreconyuge = GCCUtilitario.NullableString(pNombreConyuge)
                    objESolicitudCredito.Codigotipodocconyuge = GCCUtilitario.NullableString(pTipoDocumentoConyuge)
                    objESolicitudCredito.Numerodocumentoconyuge = GCCUtilitario.NullableString(pnumerodocumento)
                    objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_VIGENTE
                    objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                    ' Tasas y Comisiones
                    objEGcc_contratootroconcepto.Numerocontrato = pCodigoContrato
                    ' Penalidades
                    objEGcc_contratootroconcepto.Importependiente = GCCUtilitario.StringToDecimal(pImporteAtrasoPorc)
                    objEGcc_contratootroconcepto.Otraspenalidades = GCCUtilitario.NullableString(pOtrasPenalidades)
                    objEGcc_contratootroconcepto.Diasvencimiento = GCCUtilitario.StringToInteger(pdiasVencimiento)
                    objEGcc_contratootroconcepto.Porcentajecuota = GCCUtilitario.StringToDecimal(pPorcentajeCuota)
                    objEGcc_contratootroconcepto.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                    pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
                    pEGcc_contratootroconcepto = GCCUtilitario.SerializeObject(objEGcc_contratootroconcepto)

                    Dim blnResult As Boolean = objContratoTx.ContratoGuardarYEnviar(pESolicitudCredito, _
                                                                                    pEGcc_contratootroconcepto)

                    If blnResult Then
                        Return "0"
                    Else
                        Return "1"
                    End If
                Else
                    Throw New Exception("Debe ingresar una fecha de registros públicos.")
                End If
            Else
                Throw New Exception("No puede guardar y enviar el contrato para el estado actual.")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Devuelve los datos del cliente cuyos datos correponden con el código de contrato enviado.
    ''' </summary>
    ''' <param name="codSolicitudCredito">Código del contrato</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerDatosCliente(ByVal codSolicitudCredito As String)
        Dim objContratoNTx As New LContratoNTx
        Dim oLwsDocClienteNtx As New LDocClienteNTx

        Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(codSolicitudCredito))
        Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(codSolicitudCredito))

        For Each oRow As DataRow In dtCliente.Rows
            ' Datos del cliente
            If Not oRow.Item("CodUnico") Is DBNull.Value Then
                txtCodUnico.InnerHtml = oRow.Item("CodUnico").ToString().Trim
            End If
            'If Not oRow.Item("NombreSubprestatario") Is DBNull.Value Then
            '    txtRazonSocial.Value = oRow.Item("NombreSubprestatario").ToString().Trim
            'End If

            If Not odtbDatos.Rows(0).Item("NOMBRECLIENTE") Is DBNull.Value Then
                txtRazonSocial.InnerHtml = odtbDatos.Rows(0).Item("NOMBRECLIENTE").ToString().Trim
            End If


            If Not odtbDatos.Rows(0).Item("NOMBRETIPOPERSONA") Is DBNull.Value Then
                txtTipoPersona.InnerHtml = odtbDatos.Rows(0).Item("NOMBRETIPOPERSONA").ToString().Trim
            End If

            If Not odtbDatos.Rows(0).Item("DOMICILIOCLIENTE") Is DBNull.Value Then
                txtaDomicilioCliente.Value = odtbDatos.Rows(0).Item("DOMICILIOCLIENTE").ToString().Trim
            End If
            If hddCodigoEstadoCivil.Value = "" Then
                If Not oRow.Item("CodigoEstadoCivil") Is DBNull.Value Then
                    hddCodigoEstadoCivil.Value = oRow.Item("CodigoEstadoCivil").ToString().Trim
                End If
            End If
            If Not oRow.Item("DocIdentificacion") Is DBNull.Value Then
                txtTipoDocumento.InnerHtml = oRow.Item("DocIdentificacion").ToString().Trim
            End If
            If Not oRow.Item("numDocIdentificacion") Is DBNull.Value Then
                txtNroDeDocumento.InnerHtml = oRow.Item("numDocIdentificacion").ToString().Trim
            End If
            ' Datos del conyuge
            'If Not oRow.Item("NombreConyuge") Is DBNull.Value Then
            '    txtNombreConyuge.Value = oRow.Item("NombreConyuge").ToString().Trim
            'End If
            'If Not oRow.Item("CodigoTipoDocConyuge") Is DBNull.Value Then
            '    hddTipoDocumentoConyuge.Value = oRow.Item("CodigoTipoDocConyuge").ToString().Trim
            'End If
            'If Not oRow.Item("NumeroDocumentoConyuge") Is DBNull.Value Then
            '    txtNroDeDocumento.InnerText = oRow.Item("NumeroDocumentoConyuge").ToString().Trim
            'End If
            'If Not oRow.Item("DocumentoSeparacion") Is DBNull.Value Then
            '    hddAdjuntarArchivoDocumentoSeparacion.Value = oRow.Item("DocumentoSeparacion").ToString().Trim
            'End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Lee los datos del tarifario, si el contrato no cuenta con sus propias tarifas.
    ''' </summary>
    ''' <param name="codProductoFinancieroActivo">Código del producto financiero.</param>
    ''' <param name="codMoneda">Código de la moneda.</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerTarifas(ByVal codProductoFinancieroActivo As String, _
                            ByVal codMoneda As String)
        Dim objContratoNTx As New LContratoNTx

        ' Datos de las tarifas
        Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetTarifarioPredefContrato(codProductoFinancieroActivo, _
                                                                                                                             codMoneda))
        For Each oRow As DataRow In dtTarifas.Rows
            If txtImporteAtrasoPorc.InnerText = "" AndAlso _
                Not oRow.Item("PorcentajeImportePendiente") Is DBNull.Value Then
                'txtImporteAtrasoPorc.Value = Format(CDbl(oRow.Item("PorcentajeImportePendiente")), "#,##0.000")
                txtImporteAtrasoPorc.InnerText = Format(CDbl(oRow.Item("PorcentajeImportePendiente")), "#,###0.000")
            End If
            If txtPorcentajeCuota.InnerText = "" AndAlso _
                Not oRow.Item("PorcentajeCuota") Is DBNull.Value Then
                txtPorcentajeCuota.InnerText = Format(CDbl(oRow.Item("PorcentajeCuota")), "#,##0.00")
            End If
            If txtdiasVencimiento.InnerText = "" Then
                txtdiasVencimiento.InnerText = GCCConstante.CONTRATO_DIAS_VENCIMIENTO.ToString
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Lee los datos del contrato, a partir de su respectivo código y los carga en sus respectivos controles.
    ''' </summary>
    ''' <param name="codSolicitudCredito">Código del contrato</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerContrato(ByVal codSolicitudCredito As String)
        Dim objContratoNTx As New LContratoNTx

        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarDatosContratoSituacionCredito(codSolicitudCredito))

        For Each oRow As DataRow In dtContrato.Rows

            hddUbigeoUbicacion.Value = RTrim(oRow("UBIGEO").ToString)



            ' Datos del contrato
            txtNroContrato.InnerText = codSolicitudCredito
            hddFlagRetorno.Value = oRow.Item("FlagRetorno").ToString().Trim()
            If Not oRow.Item("TipoRubroFinanciamiento") Is DBNull.Value Then
                hddTipoRubroFinanciamiento.Value = oRow.Item("TipoRubroFinanciamiento").ToString().Trim()
            End If
            If Not oRow.Item("CodigoEstadoContrato") Is DBNull.Value Then
                hddCodigoEstadoContrato.Value = oRow.Item("CodigoEstadoContrato").ToString().Trim()
            End If

            'If Not oRow.Item("CodProductoFinancieroActivo") Is DBNull.Value Then
            '    hddCodProductoFinancieroActivo.Value = oRow.Item("CodProductoFinancieroActivo").ToString().Trim()
            'End If
            If Not oRow.Item("CodigoCotizacion") Is DBNull.Value Then
                hddCodigoCotizacion.Value = oRow.Item("CodigoCotizacion").ToString().Trim()
            End If
            If Not oRow.Item("EstadoContrato") Is DBNull.Value Then
                txtEstadoDelContrato.InnerText = oRow.Item("EstadoContrato").ToString().Trim()
            End If
            If Not oRow.Item("FechaContrato") Is DBNull.Value Then
                txtFechaContrato.InnerText = GCCUtilitario.CheckDateString(oRow.Item("FechaContrato").ToString(), "C")
            End If
            If Not oRow.Item("FechaMaxDisponible") Is DBNull.Value Then
                txtFechamaxdisp.InnerText = GCCUtilitario.CheckDateString(oRow.Item("FechaMaxDisponible").ToString(), "C")
            End If
            If Not oRow.Item("FechaMaxActivacion") Is DBNull.Value Then
                txtFechaActivacion.InnerText = GCCUtilitario.CheckDateString(oRow.Item("FechaMaxActivacion").ToString(), "C")
            End If
            If Not oRow.Item("PeridoDisponible") Is DBNull.Value Then
                txtPeriodoDisponible.InnerText = oRow.Item("PeridoDisponible").ToString().Trim()
            End If
            If Not oRow.Item("ClasificacionBien") Is DBNull.Value Then
                txtClasificacionDelBien.InnerText = oRow.Item("ClasificacionBien").ToString().Trim()
            End If
            If Not oRow.Item("Procedencia") Is DBNull.Value Then
                txtProcedencia.InnerText = oRow.Item("Procedencia").ToString().Trim()
            End If
            'If Not oRow.Item("CodMoneda") Is DBNull.Value Then
            '    hddCodMoneda.Value = oRow.Item("CodMoneda").ToString().Trim()
            'End If
            If Not oRow.Item("NombreMoneda") Is DBNull.Value Then
                txtMoneda.InnerText = oRow.Item("NombreMonedaAPP").ToString().Trim()
            End If
            If Not oRow.Item("MontoFinanciamiento") Is DBNull.Value Then
                txtMontoFinanciado.InnerText = Format(CDbl(oRow.Item("MontoFinanciamiento")), "#,##0.00")
            End If
            If Not oRow.Item("CodigoClasificacionContrato") Is DBNull.Value Then
                cmbClasificacionContrato.InnerText = oRow.Item("CodigoClasificacionContrato").ToString()
            End If
            If Not oRow.Item("FechaRegistroPublico") Is DBNull.Value Then
                txtFechaRegistroPublico.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaRegistroPublico").ToString(), "C")
                chkRegistroPublico.Checked = True
            End If

            If Not oRow.Item("FechaFirmaNotaria") Is DBNull.Value Then
                txtFechaFirmaNotaria.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaFirmaNotaria").ToString(), "C")
                hddFechaFirmaNotaria.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaFirmaNotaria").ToString(), "C")
                chkFirmaNotaria.Checked = True
            End If

            If Not oRow.Item("ArchivoContratoAdjunto") Is DBNull.Value Then
                hddAdjuntarArchivo.Value = oRow.Item("ArchivoContratoAdjunto").ToString().Trim()
            End If

            ' Estado civil del cliente
            If Not oRow.Item("CodigoEstadoCivil") Is DBNull.Value Then
                hddCodigoEstadoCivil.Value = oRow.Item("CodigoEstadoCivil").ToString().Trim
            End If

            ' Datos del bien que estan en el contrato
            If Not oRow.Item("Uso") Is DBNull.Value Then
                'hddUso.Value = oRow.Item("Uso").ToString().Trim()
                txtUsoInmueble.InnerText = oRow.Item("Uso").ToString().Trim()
                txtUsoBienMaquina.InnerText = oRow.Item("Uso").ToString().Trim()
                txtUsoDatosOtros.InnerText = oRow.Item("Uso").ToString().Trim()
            End If
            If Not oRow.Item("Ubicacion") Is DBNull.Value Then
                'hddUbicacion.Value = oRow.Item("Ubicacion").ToString().Trim()
                txtUbicacionInmueble.InnerText = oRow.Item("Ubicacion").ToString().Trim()
                txtUbicacionBienMaquina.InnerText = oRow.Item("Ubicacion").ToString().Trim()
                txtUbicacionDatosOtros.InnerText = oRow.Item("Ubicacion").ToString().Trim()
            End If

            'Situacion de Credito

            If Not oRow.Item("Segmento") Is DBNull.Value Then
                txtSegmento.InnerText = oRow.Item("Segmento").ToString()
            End If
            If Not oRow.Item("NombreSectorista") Is DBNull.Value Then
                txtSectorista.InnerText = oRow.Item("NombreSectorista").ToString()
            End If
            If Not oRow.Item("FechaDesembolso") Is DBNull.Value Then
                txtFechaDesembolso.InnerText = oRow.Item("FechaDesembolso").ToString()
            End If
            If Not oRow.Item("FechaVencimientoCuota") Is DBNull.Value Then
                txtFechaVctoCuota.InnerText = oRow.Item("FechaVencimientoCuota").ToString()
            End If
            If Not oRow.Item("FechaVencimientoOperacion") Is DBNull.Value Then
                txtFechaVctoCredito.InnerText = oRow.Item("FechaVencimientoOperacion").ToString()
            End If


            ' Datos Otros conceptos
            ' Tasas y comisiones
            If Not oRow.Item("MontoTEAPorc") Is DBNull.Value Then
                txtTea.InnerText = Format(CDbl(oRow.Item("MontoTEAPorc")), "#,##0.00")
            End If
            If Not oRow.Item("MontoPreCuotaPorc") Is DBNull.Value Then
                txtprecuota.InnerText = Format(CDbl(oRow.Item("MontoPreCuotaPorc")), "#,##0.00")
            End If
            If Not oRow.Item("OpcionCompra") Is DBNull.Value Then
                txtOpcionCompra.InnerText = Format(CDbl(oRow.Item("OpcionCompra")), "#,##0.00")
            End If
            If Not oRow.Item("ComisionActivacion") Is DBNull.Value Then
                txtComisionActivacion.InnerText = Format(CDbl(oRow.Item("ComisionActivacion")), "#,##0.00")
            End If
            If Not oRow.Item("ComisionEstructuracion") Is DBNull.Value Then
                txtComisionEstructuracion.InnerText = Format(CDbl(oRow.Item("ComisionEstructuracion")), "#,##0.00")
            End If
            If Not oRow.Item("OtrasComisiones") Is DBNull.Value Then
                txtaOtrasComisiones.Value = oRow.Item("OtrasComisiones").ToString().Trim()
            End If

            ' Penalidades
            If Not oRow.Item("ImportePendiente") Is DBNull.Value Then
                txtImporteAtrasoPorc.InnerText = Format(CDbl(oRow.Item("ImportePendiente")), "#,##0.000")
            End If
            If Not oRow.Item("OtrasPenalidades") Is DBNull.Value Then
                txtaOtrasPenalidades.Value = oRow.Item("OtrasPenalidades").ToString().Trim()
            End If
            If Not oRow.Item("DiasVencimiento") Is DBNull.Value Then
                txtdiasVencimiento.InnerText = oRow.Item("DiasVencimiento").ToString().Trim() + " Dias"
            End If
            If Not oRow.Item("NombreArchivo") Is DBNull.Value Then
                hddAdjuntarArchivoOtroConcepto.Value = oRow.Item("NombreArchivo").ToString().Trim()
            End If
            If Not oRow.Item("PorcentajeCuota") Is DBNull.Value Then
                txtPorcentajeCuota.InnerText = Format((oRow.Item("PorcentajeCuota")), "#,##0.00")
            End If
            ' Indica si el contrato a sido modificado.
            If oRow.Item("FlagModificado") Is DBNull.Value Then
                hddFlagModificado.Value = "0"
            Else
                If CBool(oRow.Item("FlagModificado")) Then
                    hddFlagModificado.Value = "1"
                Else
                    hddFlagModificado.Value = "0"
                End If
            End If
        Next oRow

        

        ' Se carga la lista de departamentos de adenda sólo si esta en estado 07
        'If hddCodigoEstadoContrato.Value = GCCConstante.C_CODIGO_ESTADO_CONTRATO_FORMALIZADO OrElse _
        '    hddCodigoEstadoContrato.Value = GCCConstante.C_CODIGO_ESTADO_CONTRATO_VIGENTE Then
        '    GCCUtilitario.CargarDepartamento(Me.cmbDepartamentoAdenda)
        'End If
    End Sub

    ''' <summary>
    ''' Lee los datos de la cotización, a partir de su respectivo código y carga los datos en sus respectivos controles.
    ''' </summary>
    ''' <param name="Codigocotizacion">Código de la cotización</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerCotizacion(ByVal Codigocotizacion As String)
        If Codigocotizacion.Trim <> "" Then
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String
            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(hddCodigoCotizacion.Value)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)

            ' Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))

            '********************************
            'DATOS GENERALES :: CRONOGRAMA
            '********************************

            txtTipoCronograma.InnerText = dtCotizacion.Rows(0).Item("nombretipocronograma").ToString.Trim
            txtPeriodicidad.InnerText = dtCotizacion.Rows(0).Item("nombreperiodicidad").ToString.Trim
            txtFrecuenciaPago.InnerText = dtCotizacion.Rows(0).Item("nombrefrecuenciapago").ToString.Trim
            txtTipoGracia.InnerText = dtCotizacion.Rows(0).Item("nombretipograciacuota").ToString.Trim
            Me.txtNroCuotasCronograma.InnerText = dtCotizacion.Rows(0).Item("Numerocuotas").ToString.Trim
            Me.txtPlazoGracia.InnerText = dtCotizacion.Rows(0).Item("Plazograciacuota").ToString.Trim
            Me.txtFechavence.InnerText = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechaprimervencimiento").ToString.Trim, "C")

            For Each oRow As DataRow In dtCotizacion.Rows


                Me.hddCodigoCotizacion.Value = oRow.Item("CodigoCotizacion").ToString
                Me.hddVersionCotizacion.Value = oRow.Item("versionCotizacion").ToString

                If Not oRow.Item("CodigoTipoBien") Is DBNull.Value Then
                    hddCodigoTipoBien.Value = oRow.Item("CodigoTipoBien").ToString().Trim
                End If
                If Not oRow.Item("NombreTipoBien") Is DBNull.Value Then
                    txtTipoDeBien.InnerText = oRow.Item("NombreTipoBien").ToString().Trim
                End If
                If hddCodigoTipoPersona.Value.Trim() = "" Then
                    If Not oRow.Item("CodigoTipoPersona") Is DBNull.Value Then
                        hddCodigoTipoPersona.Value = oRow.Item("CodigoTipoPersona").ToString().Trim
                    End If
                End If
                If txtTipoPersona.InnerHtml.Trim() = "" Then
                    If Not oRow.Item("NombreTipopersona") Is DBNull.Value Then
                        txtTipoPersona.InnerHtml = oRow.Item("NombreTipopersona").ToString().Trim
                    End If
                End If
                If hddTipoDocumento.Value.Trim() = "" Then
                    If Not oRow.Item("CodigoTipoDocumento") Is DBNull.Value Then
                        hddTipoDocumento.Value = oRow.Item("CodigoTipoDocumento").ToString().Trim
                    End If
                End If
                If txtTipoDocumento.InnerHtml.Trim() = "" Then
                    If Not oRow.Item("NombreTipoDocumento") Is DBNull.Value Then
                        txtTipoDocumento.InnerHtml = oRow.Item("NombreTipoDocumento").ToString().Trim
                    End If
                End If
                If txtNroDeDocumento.InnerHtml.Trim() = "" Then
                    If Not oRow.Item("NumeroDocumento") Is DBNull.Value Then
                        txtNroDeDocumento.InnerHtml = oRow.Item("NumeroDocumento").ToString().Trim
                    End If
                End If
                If Not oRow.Item("Codigoestadobien") Is DBNull.Value Then
                    hddCodigoEstadoBien.Value = oRow.Item("Codigoestadobien").ToString().Trim()
                End If
                If Not oRow.Item("Correocontacto") Is DBNull.Value Then
                    hddCorreocontacto.Value = oRow.Item("Correocontacto").ToString().Trim
                End If

                ' Ejecutivo Leasing
                txtEjecutivoLeasing.InnerText = oRow.Item("NombreEjecutivoleasing").ToString().Trim

            Next oRow
        End If
    End Sub

    ''' <summary>
    ''' Establece algunos valores predeterminados, según el número de contrato.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub ValoresPredeterminados()
        ' Se carga el estado predeterminado sólo si esta en estado 03
        If hddCodigoEstadoContrato.Value = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ELABORADO Then
            If hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_BIEN_INMUEBLE AndAlso _
                hddCodigoTipoBien.Value <> GCCConstante.C_TIPO_BIEN_EMBARCACION Then
                ' Bien inmueble que no sea embarcación pesquera
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_INMUEBLE
            ElseIf hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_BIEN_INMUEBLE AndAlso _
                   hddCodigoTipoBien.Value = GCCConstante.C_TIPO_BIEN_EMBARCACION Then
                ' Bien inmueble que sea embarcación pesquera
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA
            ElseIf hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_INDUSTRIAL OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_OFICINA OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_MOV_TIERRA OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_SISTEMA_PROC_DATOS OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_OTROS Then
                ' Maquinaria que no sea vehículo
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_MUEBLE
            Else
                ' Vehículo
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_VEHICULO
            End If
        End If

    End Sub

    ''' <summary>
    ''' Actualiza el estado del contrato a aprobado.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function Aprobar(ByVal pCodigoContrato As String, _
                                   ByVal pClienteRazonSocial As String, _
                                   ByVal pClienteDomicilioLegal As String) As String
        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pSolicitudCredito As String

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.ClienteRazonSocial = pClienteRazonSocial
        objESolicitudCredito.ClienteDomicilioLegal = pClienteDomicilioLegal

        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEENVIO  'GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        Dim blnResult As Boolean = objContratoTx.Aprobar(pSolicitudCredito)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Envia el contrato a la notaria.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EnviaNotaria(ByVal pCodigoContrato As String, _
                                        ByVal pClienteRazonSocial As String, _
                                        ByVal pClienteDomicilioLegal As String) As String
        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pSolicitudCredito As String
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Dim objCotizacionNTx As New LCotizacionNTx
        Dim strEGcc_cotizacion As String
        Dim objEGcc_cotizacion As New EGcc_cotizacion

        Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(pCodigoContrato))
        Dim mbool As Boolean
        Dim oBase As New GCCBase
        Dim strCorreo As String

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        objESolicitudCredito.ClienteRazonSocial = pClienteRazonSocial
        objESolicitudCredito.ClienteDomicilioLegal = pClienteDomicilioLegal

        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
        Dim strRutaServidor As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
        If odtbDatos.Rows.Count > 0 Then
            For Each dr As DataRow In odtbDatos.Rows

                With objEGcc_cotizacion
                    .Codigocotizacion = GCCUtilitario.NullableString(dr("CODIGOCOTIZACION").ToString)
                End With
                strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

                'Ejecuta Consulta
                Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))

                strCorreo = dtCotizacion.Rows(0).Item("Correocontacto").ToString

                mbool = oBase.EnviarMail("", strCorreo, strRutaServidor + dr("ARCHIVOCONTRATOADJUNTO").ToString, "MailContratoNotaria", pCodigoContrato, _
                                                 "", dr("NOMBRECLIENTE").ToString, "", _
                                                 "", _
                                                 "", _
                                                 "", "", "", "")
            Next
        End If

        Dim blnResult As Boolean = objContratoTx.Aprobar(pSolicitudCredito)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Se formaliza el contrato (estado formalizado), ingresando la fecha de firma en notaria.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato</param>
    ''' <param name="pFechaFirmaNotaria">Fecha en la cual el contrato se registro en notaria.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function Formalizado(ByVal pCodigoContrato As String, _
                                       ByVal pFechaFirmaNotaria As String, _
                                       ByVal pClienteRazonSocial As String, _
                                       ByVal pClienteDomicilioLegal As String) As String
        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pSolicitudCredito As String
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(pCodigoContrato))
        Dim strCorreo As String
        strCorreo = ""
        Dim oBase As New GCCBase

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_FORMALIZADO  'GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        objESolicitudCredito.Fechafirmanotaria = GCCUtilitario.StringToDateTime(pFechaFirmaNotaria) ' GCCUtilitario.CheckDate(pFechaFirmaNotaria)
        objESolicitudCredito.SFechafirmanotaria = pFechaFirmaNotaria

        objESolicitudCredito.ClienteRazonSocial = pClienteRazonSocial
        objESolicitudCredito.ClienteDomicilioLegal = pClienteDomicilioLegal

        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        Dim mbool As Boolean
        If odtbDatos.Rows.Count > 0 Then
            For Each dr As DataRow In odtbDatos.Rows
                Dim pstrProdFinanActivo As String = dr("CODPRODUCTOFINANCIEROACTIVO").ToString
                Dim pstrProdFinanPasivo As String = dr("CODPRODUCTOFINANCIEROPASIVO").ToString
                Dim pstrTipoContrato As String = ""
                Dim PstrTipoSubContrato As String = ""
                Dim pstrSimbolo As String = ""
                If dr("CODIGOMONEDA").ToString = "001" Then
                    pstrSimbolo = "S/."
                Else
                    pstrSimbolo = "$"
                End If

                If dr("CODIGOSUBTIPOCONTRATO").ToString().Trim() = "001" Then
                    PstrTipoSubContrato = "Directo"
                ElseIf dr("CODIGOSUBTIPOCONTRATO").ToString().Trim() = "002" Then
                    PstrTipoSubContrato = "Parcial"
                End If

                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING_PAS) Then
                    pstrTipoContrato = IIf(GCCConstante.C_CODGCC_PROD_LEASING = "LD", "LEASING", "").ToString()
                End If
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS) Then
                    pstrTipoContrato = GCCConstante.C_DESGCC_PROD_LEASEBACK
                End If
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION_PAS) Then
                    pstrTipoContrato = GCCConstante.C_DESGCC_PROD_IMPORTACION
                End If
                mbool = oBase.EnviarMail("", strCorreo, "", "MailContratoFirmado", pCodigoContrato, _
                                         "", dr("NOMBRECLIENTE").ToString, "", _
                                         dr("NOMBRECLASIFICACIONBIEN").ToString, _
                                         dr("NOMBRETIPOBIEN").ToString, _
                                         pstrTipoContrato + " " + UCase(PstrTipoSubContrato), pstrSimbolo + " " + Format(CDbl(dr("PRECIOVENTA").ToString()), "#,##0.00"), "", "")
            Next
        End If


        Dim blnResult As Boolean = objContratoTx.Aprobar(pSolicitudCredito)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Anular el contrato.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function Anular(ByVal pCodigoContrato As String) As String
        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pSolicitudCredito As String

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ANULADO
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        Dim blnResult As Boolean = objContratoTx.Aprobar(pSolicitudCredito)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

#End Region

#Region "Datos Notariales"

    ''' <summary>
    ''' Listar los documentos notariales (dato notarial o adenda).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código del contrato</param>
    ''' <param name="pTipoDatoNotarial"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarDatosNotariales(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pContrato As String, _
                                                 ByVal pTipoDatoNotarial As String) As JQGridJsonResponse

        Dim objContratoNTx As New LContratoNTx
        Dim oJQGridJsonResponse As New JQGridJsonResponse

        Dim dtContratoNotarial As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoNotarial(pContrato, _
                                                                                                                                   pTipoDatoNotarial))


        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtContratoNotarial.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtContratoNotarial.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtContratoNotarial.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtContratoNotarial)
    End Function

    ''' <summary>
    ''' Listar los documentos notariales (dato notarial o adenda), organizándolos en páginas.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código del contrato.</param>
    ''' <param name="pTipoDatoNotarial"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                                           ByVal pCurrentPage As Integer, _
                                                           ByVal pSortColumn As String, _
                                                           ByVal pSortOrder As String, _
                                                           ByVal pContrato As String, _
                                                           ByVal pTipoDatoNotarial As String, _
                                                           ByVal pFields As String) As JQGridJsonResponse
        Dim oJQGridJsonResponse As New JQGridJsonResponse

        Try
            Dim objContratoNTx As New LContratoNTx

            Dim dtContratoNotarial As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoNotarialPaginado(pPageSize, _
                                                                                                                                               pCurrentPage, _
                                                                                                                                               pSortColumn, _
                                                                                                                                               pSortOrder, _
                                                                                                                                               pContrato, _
                                                                                                                                               pTipoDatoNotarial))


            'Total de registros a mostrar.
            Dim totalRecords As Integer
            Dim totalCurrent As Integer

            If dtContratoNotarial.Rows.Count = 0 Then
                totalRecords = 0
                totalCurrent = 0
            Else
                totalRecords = Convert.ToInt32(dtContratoNotarial.Rows(0)("RecordCount"))
                totalCurrent = Convert.ToInt32(dtContratoNotarial.Rows(0)("TOTAL_PAGINA"))
            End If
            If pCurrentPage > totalCurrent Then
                pCurrentPage = totalCurrent
            End If

            'Número total de páginas
            Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                         pPageSize)
            Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                                   pCurrentPage, _
                                                                   totalRecords, _
                                                                   dtContratoNotarial, _
                                                                   pFields)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Ingresa un nuevo dato notarial al contrato.
    ''' </summary>
    ''' <param name="strNroContrato">Código del contrato.</param>
    ''' <param name="strCodigoNotaria">Código de la notaria.</param>
    ''' <param name="strDepartamento">Código del departamento.</param>
    ''' <param name="strProvincia">Código de la provincia.</param>
    ''' <param name="strDistrito">Código del distrito.</param>
    ''' <param name="strCodigoTipoMinuta">Código del tipo de minuta.</param>
    ''' <param name="strKardex">Número de kardex.</param>
    ''' <param name="strFecha">Fecha del registro del dato notarial.</param>
    ''' <param name="strObservacionesNotariales">Observaciones adicionales.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarDatosNotarialesNuevo(ByVal strNroContrato As String, _
                                                       ByVal strCodigoNotaria As String, _
                                                       ByVal strDepartamento As String, _
                                                       ByVal strProvincia As String, _
                                                       ByVal strDistrito As String, _
                                                       ByVal strCodigoTipoMinuta As String, _
                                                       ByVal strKardex As String, _
                                                       ByVal strFecha As String, _
                                                       ByVal strObservacionesNotariales As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objEGCC_ContratoNotarial As New EGcc_contratonotarial
            Dim pEGCC_ContratoNotarial As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objEGCC_ContratoNotarial
                .Codigoorigenadenda = GCCConstante.ORIGEN_ADENDA_DatoNotarial
                .Numerocontrato = strNroContrato
                .Codigonotaria = strCodigoNotaria
                .Codigoubigeo = GCCUtilitario.CodigoUbigeo(strDepartamento, strProvincia, strDistrito)
                .Codigotipominuta = GCCUtilitario.NullableString(strCodigoTipoMinuta)
                .Kardex = GCCUtilitario.NullableString(strKardex)
                .Fecha = GCCUtilitario.StringToDateTime(strFecha)
                .Observacion = GCCUtilitario.NullableString(strObservacionesNotariales)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pEGCC_ContratoNotarial = GCCUtilitario.SerializeObject(objEGCC_ContratoNotarial)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim intResult As Integer = objContratoTx.fintInsertarContratoNotarial(pESolicitudCredito, _
                                                                                  pEGCC_ContratoNotarial)

            If intResult > 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos del documento notarial.
    ''' </summary>
    ''' <param name="intCodigoNotarial">Número identificatorio del dato notarial.</param>
    ''' <param name="strNroContrato">Código del contrato.</param>
    ''' <param name="strCodigoNotaria">Código de la notaria.</param>
    ''' <param name="strDepartamento">Código del departamento.</param>
    ''' <param name="strProvincia">Código de la provincia.</param>
    ''' <param name="strDistrito">Código del distrito.</param>
    ''' <param name="strCodigoTipoMinuta"></param>
    ''' <param name="strKardex">Número de kardex.</param>
    ''' <param name="strFecha">Fecha del registro del dato notarial.</param>
    ''' <param name="strObservacionesNotariales">Observaciones adicionales.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarDatosNotarialesEditar(ByVal intCodigoNotarial As Integer, _
                                                        ByVal strNroContrato As String, _
                                                        ByVal strCodigoNotaria As String, _
                                                        ByVal strDepartamento As String, _
                                                        ByVal strProvincia As String, _
                                                        ByVal strDistrito As String, _
                                                        ByVal strCodigoTipoMinuta As String, _
                                                        ByVal strKardex As String, _
                                                        ByVal strFecha As String, _
                                                        ByVal strObservacionesNotariales As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objEGCC_ContratoNotarial As New EGcc_contratonotarial
            Dim pEGCC_ContratoNotarial As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            objEGCC_ContratoNotarial.Codigonotarial = intCodigoNotarial
            objEGCC_ContratoNotarial.Numerocontrato = strNroContrato
            objEGCC_ContratoNotarial.Codigonotaria = strCodigoNotaria
            objEGCC_ContratoNotarial.Codigoubigeo = GCCUtilitario.CodigoUbigeo(strDepartamento, strProvincia, strDistrito)
            objEGCC_ContratoNotarial.Codigotipominuta = GCCUtilitario.NullableString(strCodigoTipoMinuta)
            objEGCC_ContratoNotarial.Kardex = GCCUtilitario.NullableString(strKardex)
            objEGCC_ContratoNotarial.Fecha = GCCUtilitario.StringToDateTime(strFecha)
            objEGCC_ContratoNotarial.Observacion = GCCUtilitario.NullableString(strObservacionesNotariales)
            objEGCC_ContratoNotarial.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pEGCC_ContratoNotarial = GCCUtilitario.SerializeObject(objEGCC_ContratoNotarial)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnModificarContratoNotarial(pESolicitudCredito, _
                                                                                   pEGCC_ContratoNotarial)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina los documentos notariales indicados.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <param name="pCodigosEliminar">Lista de documento notariales.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function DatosNotarialesEliminar(ByVal pCodigoContrato As String, _
                                                   ByVal pCodigosEliminar As String) As String
        Dim objContratoTx As New LContratoTx

        Dim oListEGccContratonotarial As New ListEGcc_contratonotarial
        Dim pListEGccContratonotarial As String

        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pESolicitudCredito As String

        Dim vCodigosEliminar As String() = pCodigosEliminar.Split(New Char() {"|"c})

        For i As Integer = 0 To vCodigosEliminar.Length - 1
            Dim oEGccContratonotarial As New EGcc_contratonotarial

            oEGccContratonotarial.Numerocontrato = pCodigoContrato
            oEGccContratonotarial.Codigonotarial = CInt(vCodigosEliminar(i))
            oEGccContratonotarial.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            oListEGccContratonotarial.Add(oEGccContratonotarial)
        Next i

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Modificado = True
        objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        pListEGccContratonotarial = GCCUtilitario.SerializeObject(oListEGccContratonotarial)
        pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        Dim blnResult As Boolean = objContratoTx.fblnEliminarContratoNotarial(pESolicitudCredito, _
                                                                              pListEGccContratonotarial)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Indica si existe entre los documentos notariales una minuta de tipo arrendamiento finaciero.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ExisteArrendamientoFinanciero(ByVal pCodigoContrato As String) As String
        Dim blnResult As Boolean = False

        Dim objContratoNTx As New LContratoNTx

        Dim dtContratoNotarial As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoNotarial(pCodigoContrato, _
                                                                                                                                   GCCConstante.ORIGEN_ADENDA_DatoNotarial))

        For Each oRow As DataRow In dtContratoNotarial.Rows
            If Not oRow.Item("Minuta") Is DBNull.Value Then
                If oRow.Item("Minuta").ToString().Trim().ToUpper().StartsWith("ARRENDAMIENTO FINANCIERO") Then
                    blnResult = True
                End If
            End If
        Next oRow

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

#End Region

#Region "Condiciones Adicionales"

    ''' <summary>
    ''' Devuelve las condiciones adicionales correspondientes para el credito ingresado.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodigo"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListaCondicionesAdicionales(ByVal pPageSize As Integer, _
                                                       ByVal pCurrentPage As Integer, _
                                                       ByVal pSortColumn As String, _
                                                       ByVal pSortOrder As String, _
                                                       ByVal pCodigo As String, _
                                                       ByVal pFields As String) As JQGridJsonResponse
        Dim objCondicionAdicionalNTx As New LCheckListNTx

        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim strEGccContratodocumento As String

        oEGccContratodocumento.Numerocontrato = GCCUtilitario.NullableString(pCodigo)
        oEGccContratodocumento.Flagfiltro = 2

        strEGccContratodocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEGccContratodocumento)
        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCondicionAdicionalNTx.ContratoDocumentoSel(pPageSize, _
                                                                                                                                            pCurrentPage, _
                                                                                                                                            pSortColumn, _
                                                                                                                                            pSortOrder, _
                                                                                                                                            strEGccContratodocumento))

        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtCondicionAdicional.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtCondicionAdicional, _
                                                               pFields)
    End Function

#End Region

#Region "Adenda"
    ''' <summary>
    ''' Ingresa una nueva adenda al contrato.
    ''' </summary>
    ''' <param name="strNroContrato">Código del contrato.</param>
    ''' <param name="strFechaAdenda">Fecha de registro de la adenda.</param>
    ''' <param name="strFechaEscrituraPub">Fecha de registro de la escritura pública.</param>
    ''' <param name="strAdjuntarAdenda">Nombre del archivo de la adenda, incluyendo su ruta parcial.</param>
    ''' <param name="strMotivo"></param>
    ''' <param name="strPorCuentaDe">Quien originó la adenda, el cliente o el banco.</param>
    ''' <param name="strNotariaAdenda">Código de la notaria.</param>
    ''' <param name="strKardexAdenda">Número de kardex.</param>
    ''' <param name="strDepartamentoAdenda">Código del departamento.</param>
    ''' <param name="strProvinciaAdenda">Código de la provincia.</param>
    ''' <param name="strDistritoAdenda">Código del distrito.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarAdendaNuevo(ByVal strNroContrato As String, _
                                              ByVal strFechaAdenda As String, _
                                              ByVal strFechaEscrituraPub As String, _
                                              ByVal strAdjuntarAdenda As String, _
                                              ByVal strMotivo As String, _
                                              ByVal strPorCuentaDe As String, _
                                              ByVal strNotariaAdenda As String, _
                                              ByVal strKardexAdenda As String, _
                                              ByVal strDepartamentoAdenda As String, _
                                              ByVal strProvinciaAdenda As String, _
                                              ByVal strDistritoAdenda As String) As String
        Try
            Dim objEGCC_ContratoNotarial As New EGcc_contratonotarial
            Dim objContratoTx As New LContratoTx
            Dim pEGCC_ContratoNotarial As String

            With objEGCC_ContratoNotarial
                .Codigoorigenadenda = GCCConstante.ORIGEN_ADENDA_Adenda
                .Numerocontrato = GCCUtilitario.NullableString(strNroContrato)
                .Fecha = GCCUtilitario.StringToDateTime(strFechaAdenda)
                .FechaEscrituraPublica = GCCUtilitario.StringToDateTime(strFechaEscrituraPub)
                .Nombrearchivo = HttpUtility.UrlDecode(GCCUtilitario.NullableString(strAdjuntarAdenda))
                .Motivo = GCCUtilitario.NullableString(strMotivo)
                .CodigoPorCuenta = GCCUtilitario.NullableString(strPorCuentaDe)
                .Codigonotaria = GCCUtilitario.NullableString(strNotariaAdenda)
                .Kardex = GCCUtilitario.NullableString(strKardexAdenda)
                .Codigoubigeo = GCCUtilitario.CodigoUbigeo(strDepartamentoAdenda, _
                                                           strProvinciaAdenda, _
                                                           strDistritoAdenda)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            pEGCC_ContratoNotarial = GCCUtilitario.SerializeObject(objEGCC_ContratoNotarial)
            Dim intResult As Integer = objContratoTx.fintInsertarAdenda(pEGCC_ContratoNotarial)

            If intResult > 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Actualiza los datos de la adenda.
    ''' </summary>
    ''' <param name="intCodigoNotarial">Número identificatorio de la adenda.</param>
    ''' <param name="strNroContrato">Código del contrato.</param>
    ''' <param name="strFechaAdenda">Fecha de registro de la adenda.</param>
    ''' <param name="strFechaEscrituraPub">Fecha de registro de la escritura pública.</param>
    ''' <param name="strAdjuntarAdenda">Nombre del archivo de la adenda, incluyendo su ruta parcial.</param>
    ''' <param name="strMotivo"></param>
    ''' <param name="strPorCuentaDe">Quien originó la adenda, el cliente o el banco.</param>
    ''' <param name="strNotariaAdenda">Código de la notaria.</param>
    ''' <param name="strKardexAdenda">Número de kardex.</param>
    ''' <param name="strDepartamentoAdenda">Código del departamento.</param>
    ''' <param name="strProvinciaAdenda">Código de la provincia.</param>
    ''' <param name="strDistritoAdenda">Código del distrito.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarAdendaEditar(ByVal intCodigoNotarial As Integer, _
                                               ByVal strNroContrato As String, _
                                               ByVal strFechaAdenda As String, _
                                               ByVal strFechaEscrituraPub As String, _
                                               ByVal strAdjuntarAdenda As String, _
                                               ByVal strMotivo As String, _
                                               ByVal strPorCuentaDe As String, _
                                               ByVal strNotariaAdenda As String, _
                                               ByVal strKardexAdenda As String, _
                                               ByVal strDepartamentoAdenda As String, _
                                               ByVal strProvinciaAdenda As String, _
                                               ByVal strDistritoAdenda As String) As String

        Try
            Dim objEGCC_ContratoNotarial As New EGcc_contratonotarial
            Dim objContratoTx As New LContratoTx
            Dim pEGCC_ContratoNotarial As String

            objEGCC_ContratoNotarial.Numerocontrato = strNroContrato
            objEGCC_ContratoNotarial.Codigonotarial = intCodigoNotarial
            objEGCC_ContratoNotarial.Fecha = GCCUtilitario.StringToDateTime(strFechaAdenda)
            objEGCC_ContratoNotarial.FechaEscrituraPublica = GCCUtilitario.StringToDateTime(strFechaEscrituraPub)
            objEGCC_ContratoNotarial.Nombrearchivo = HttpUtility.UrlDecode(GCCUtilitario.NullableString(strAdjuntarAdenda))
            objEGCC_ContratoNotarial.Motivo = GCCUtilitario.NullableString(strMotivo)
            objEGCC_ContratoNotarial.CodigoPorCuenta = GCCUtilitario.NullableString(strPorCuentaDe)
            objEGCC_ContratoNotarial.Codigonotaria = GCCUtilitario.NullableString(strNotariaAdenda)
            objEGCC_ContratoNotarial.Kardex = GCCUtilitario.NullableString(strKardexAdenda)
            objEGCC_ContratoNotarial.Codigoubigeo = GCCUtilitario.CodigoUbigeo(strDepartamentoAdenda, _
                                                                               strProvinciaAdenda, _
                                                                               strDistritoAdenda)
            objEGCC_ContratoNotarial.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pEGCC_ContratoNotarial = GCCUtilitario.SerializeObject(objEGCC_ContratoNotarial)
            Dim blnResult As Boolean = objContratoTx.fblnModificarAdenda(pEGCC_ContratoNotarial)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Elimina las adendas indicadas.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <param name="pCodigosEliminar">Relación de documentos adenda a eliminar.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function AdendaEliminar(ByVal pCodigoContrato As String, _
                                          ByVal pCodigosEliminar As String) As String
        Dim objContratoTx As New LContratoTx

        Dim oListEGccContratonotarial As New ListEGcc_contratonotarial
        Dim pListEGccContratonotarial As String

        Dim vCodigosEliminar As String() = pCodigosEliminar.Split(New Char() {"|"c})

        For i As Integer = 0 To vCodigosEliminar.Length - 1
            Dim oEGccContratonotarial As New EGcc_contratonotarial

            oEGccContratonotarial.Numerocontrato = pCodigoContrato
            oEGccContratonotarial.Codigonotarial = CInt(vCodigosEliminar(i))
            oEGccContratonotarial.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            oListEGccContratonotarial.Add(oEGccContratonotarial)
        Next i

        pListEGccContratonotarial = GCCUtilitario.SerializeObject(oListEGccContratonotarial)

        Dim blnResult As Boolean = objContratoTx.fblnEliminarAdenda(pListEGccContratonotarial)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

#End Region

#Region "ContratoProveedor"

    ''' <summary>
    ''' Devuelve los proveedores del contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarContratoProveedores(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pContrato As String, _
                                                     ByVal pFields As String) As JQGridJsonResponse
        Try
            Dim objContratoNTx As New LContratoNTx

            Dim dtContratoProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarDistinctContratoProveedores(pPageSize, _
                                                                                                                                                    pCurrentPage, _
                                                                                                                                                    pSortColumn, _
                                                                                                                                                    pSortOrder, _
                                                                                                                                                    pContrato))


            Dim oJQGridJsonResponse As New JQGridJsonResponse
            'Total de registros a mostrar.
            Dim totalRecords As Integer
            Dim totalCurrent As Integer

            If dtContratoProveedores.Rows.Count = 0 Then
                totalRecords = 0
                totalCurrent = 0
            Else
                totalRecords = Convert.ToInt32(dtContratoProveedores.Rows(0)("RecordCount"))
                totalCurrent = Convert.ToInt32(dtContratoProveedores.Rows(0)("TOTAL_PAGINA"))
            End If
            If pCurrentPage > totalCurrent Then
                pCurrentPage = totalCurrent
            End If

            'Número total de páginas
            Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                         pPageSize)
            Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                                   pCurrentPage, _
                                                                   totalRecords, _
                                                                   dtContratoProveedores, _
                                                                   pFields)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Bienes"

    ''' <summary>
    ''' Listar de bienes vinculados al contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarBienes(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pContrato As String, _
                                        ByVal pFields As String) As JQGridJsonResponse
        Dim objContratoNTx As New LContratoNTx

        Dim dtBienesProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       pContrato, _
                                                                                                                       Nothing))


        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtBienesProveedor.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtBienesProveedor.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtBienesProveedor.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtBienesProveedor, _
                                                               pFields)
    End Function
    ''' <summary>
    ''' Ingresa un nuevo bien inmueble al contrato.
    ''' </summary>
    ''' <param name="strNroContrato">Código del contrato.</param>
    ''' <param name="strTipoRubroFinanciamiento">Código de clasificación del bien.</param>
    ''' <param name="strUsoInmueble">Uso al que será destinado el bien.</param>
    ''' <param name="strUbicacionInmueble">Dirección fisica del bien.</param>
    ''' <param name="strDescripcionInmueble">Breve descricpción y caracterititcas del bien.</param>
    ''' <param name="strEstadoBienInmueble">Código del nivel de uso del bien.</param>
    ''' <param name="intCantidadInmueble">CAntidad de bienes inmuebles adquiridos</param>
    ''' <param name="strDepartamentoInmueble">Código del departamento.</param>
    ''' <param name="strProvinciaInmueble">Código de la provincia.</param>
    ''' <param name="strDistritoInmueble">Código del distrito.</param>
    ''' <param name="strPartidaRegistralInmueble">Partida registral.</param>
    ''' <param name="strOficinaRegistralInmueble">Oficina registral.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarBienInmuebleNuevo(ByVal strNroContrato As String, _
                                                    ByVal strTipoRubroFinanciamiento As String, _
                                                    ByVal strUsoInmueble As String, _
                                                    ByVal strUbicacionInmueble As String, _
                                                    ByVal strDescripcionInmueble As String, _
                                                    ByVal strEstadoBienInmueble As String, _
                                                    ByVal intCantidadInmueble As String, _
                                                    ByVal strDepartamentoInmueble As String, _
                                                    ByVal strProvinciaInmueble As String, _
                                                    ByVal strDistritoInmueble As String, _
                                                    ByVal strPartidaRegistralInmueble As String, _
                                                    ByVal strOficinaRegistralInmueble As String, _
                                                    ByVal strCodigoPredioInmueble As String, _
                                                    ByVal intFlagOrigen As Integer, _
                                                    ByVal strCodTipoBien As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudcreditoestructura As New ESolicitudcreditoestructura
            Dim objESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

            Dim pESolicitudcreditoestructura As String
            Dim pESolicitudcreditoestructuracarac As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objESolicitudcreditoestructura
                .Codsolicitudcredito = strNroContrato
                .Codproveedor = Nothing
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Uso = GCCUtilitario.NullableString(strUsoInmueble)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacionInmueble)
                .Codigoestadobien = GCCUtilitario.NullableString(strEstadoBienInmueble)
                .Cantidadproducto = GCCUtilitario.StringToInteger(intCantidadInmueble)
                ' Inicio IBK - AAE - Agrego el código de usuario
                .Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Fin IBK
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Flag_origen = intFlagOrigen
                'IBK - RPH
                .Codigotipobien = strCodTipoBien
                'Fin
            End With

            With objESolicitudCreditoEstructuraCarac
                .Codsolicitudcredito = strNroContrato
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Comentario = GCCUtilitario.NullableString(strDescripcionInmueble)
                .Departamento = GCCUtilitario.NullableString(strDepartamentoInmueble)
                .Provincia = GCCUtilitario.NullableString(strProvinciaInmueble)
                .Distrito = GCCUtilitario.NullableString(strDistritoInmueble)
                .PartidaRegistral = GCCUtilitario.NullableString(strPartidaRegistralInmueble)
                .OficinaRegistral = GCCUtilitario.NullableString(strOficinaRegistralInmueble)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .CodigoPredio = GCCUtilitario.NullableString(strCodigoPredioInmueble)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructura = GCCUtilitario.SerializeObject(objESolicitudcreditoestructura)
            pESolicitudcreditoestructuracarac = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarac)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnInsertarBien(pESolicitudCredito, _
                                                                      pESolicitudcreditoestructura, _
                                                                      pESolicitudcreditoestructuracarac)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Actualiza los datos del documento notarial.
    ''' </summary>
    ''' <param name="strNroContrato">Código del contrato.</param>
    ''' <param name="intSecFinanciamiento">Número identificatorio del bien.</param>
    ''' <param name="strTipoRubroFinanciamiento">Código de clasificación del bien.</param>
    ''' <param name="strUsoInmueble">Uso al que será destinado el bien.</param>
    ''' <param name="strUbicacionInmueble">Dirección fisica del bien.</param>
    ''' <param name="strDescripcionInmueble">Breve descricpción y caracterititcas del bien.</param>
    ''' <param name="strEstadoBienInmueble">Código del nivel de uso del bien.</param>
    ''' <param name="intCantidadInmueble">CAntidad de bienes inmuebles adquiridos</param>
    ''' <param name="strDepartamentoInmueble">Código del departamento.</param>
    ''' <param name="strProvinciaInmueble">Código de la provincia.</param>
    ''' <param name="strDistritoInmueble">Código del distrito.</param>
    ''' <param name="strPartidaRegistralInmueble">Partida registral.</param>
    ''' <param name="strOficinaRegistralInmueble">Oficina registral.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarBienInmuebleEditar(ByVal strNroContrato As String, _
                                                     ByVal intSecFinanciamiento As String, _
                                                     ByVal strTipoRubroFinanciamiento As String, _
                                                     ByVal strUsoInmueble As String, _
                                                     ByVal strUbicacionInmueble As String, _
                                                     ByVal strDescripcionInmueble As String, _
                                                     ByVal strEstadoBienInmueble As String, _
                                                     ByVal intCantidadInmueble As String, _
                                                     ByVal strDepartamentoInmueble As String, _
                                                     ByVal strProvinciaInmueble As String, _
                                                     ByVal strDistritoInmueble As String, _
                                                     ByVal strPartidaRegistralInmueble As String, _
                                                     ByVal strOficinaRegistralInmueble As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudcreditoestructura As New ESolicitudcreditoestructura
            Dim objESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

            Dim pESolicitudcreditoestructura As String
            Dim pESolicitudcreditoestructuracarac As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objESolicitudcreditoestructura
                .Codsolicitudcredito = strNroContrato
                .Secfinanciamiento = GCCUtilitario.StringToInteger(intSecFinanciamiento)
                .Codproveedor = Nothing
                ' Inicio IBK - AAE - Agrego el código de usuario
                .Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Fin IBK
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Uso = GCCUtilitario.NullableString(strUsoInmueble)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacionInmueble)
                .Codigoestadobien = GCCUtilitario.NullableString(strEstadoBienInmueble)
                .Cantidadproducto = GCCUtilitario.StringToInteger(intCantidadInmueble)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            With objESolicitudCreditoEstructuraCarac
                .Codsolicitudcredito = strNroContrato
                .Secfinanciamiento = GCCUtilitario.StringToInteger(intSecFinanciamiento)
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Comentario = GCCUtilitario.NullableString(strDescripcionInmueble)
                .Departamento = GCCUtilitario.NullableString(strDepartamentoInmueble)
                .Provincia = GCCUtilitario.NullableString(strProvinciaInmueble)
                .Distrito = GCCUtilitario.NullableString(strDistritoInmueble)
                .PartidaRegistral = GCCUtilitario.NullableString(strPartidaRegistralInmueble)
                .OficinaRegistral = GCCUtilitario.NullableString(strOficinaRegistralInmueble)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructura = GCCUtilitario.SerializeObject(objESolicitudcreditoestructura)
            pESolicitudcreditoestructuracarac = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarac)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnModificarBien(pESolicitudCredito, _
                                                                       pESolicitudcreditoestructura, _
                                                                       pESolicitudcreditoestructuracarac)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Elimina los bienes indicadas.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <param name="pBienesEliminar">Lista de bienes a eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BienesEliminar(ByVal pCodigoContrato As String, _
                                          ByVal pBienesEliminar As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim oListESolicitudcreditoestructura As New ListESolicitudcreditoestructura
            Dim pESolicitudcreditoestructuraList As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            Dim vBienesEliminar As String() = pBienesEliminar.Split(New Char() {"|"c})

            For i As Integer = 0 To vBienesEliminar.Length - 1
                Dim oESolicitudcreditoestructura As New ESolicitudcreditoestructura

                oESolicitudcreditoestructura.Codsolicitudcredito = pCodigoContrato
                oESolicitudcreditoestructura.Secfinanciamiento = CInt(vBienesEliminar(i))
                oESolicitudcreditoestructura.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                oListESolicitudcreditoestructura.Add(oESolicitudcreditoestructura)
            Next i

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructuraList = GCCUtilitario.SerializeObject(oListESolicitudcreditoestructura)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnEliminarBien(pESolicitudCredito, _
                                                                      pESolicitudcreditoestructuraList)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            If ex.ToString().ToUpper().Contains("THE DELETE STATEMENT CONFLICTED WITH THE REFERENCE CONSTRAINT") Then
                Return "1|Existen documentos relacionados al bien.<br />No se puede eliminar."
            Else
                Throw ex
            End If
        End Try
    End Function
    ''' <summary>
    ''' Ingresa un nuevo bien inmueble al contrato.
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="strTipoRubroFinanciamiento"></param>
    ''' <param name="strSerieMotorMaquina"></param>
    ''' <param name="strNumeroMotorMaquina"></param>
    ''' <param name="strFabricacionMaquina"></param>
    ''' <param name="strMarcaMaquina"></param>
    ''' <param name="strModeloMaquina"></param>
    ''' <param name="strTipoCarroceriaMaquina"></param>
    ''' <param name="strDescripcionAutoMaquina"></param>
    ''' <param name="strEstadobienMaquina"></param>
    ''' <param name="strPlacaMaquina"></param>
    ''' <param name="strMedidasMaquina"></param>
    ''' <param name="intCantidadMaquina"></param>
    ''' <param name="strUsoBienMaquina"></param>
    ''' <param name="strUbicacionBienMaquina"></param>
    ''' <param name="strDepartamentoMaquinaria"></param>
    ''' <param name="strProvinciaMaquinaria"></param>
    ''' <param name="strDistritoMaquinaria"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarMaquinaNuevo(ByVal strNroContrato As String, _
                                               ByVal strTipoRubroFinanciamiento As String, _
                                               ByVal strSerieMotorMaquina As String, _
                                               ByVal strNumeroMotorMaquina As String, _
                                               ByVal strFabricacionMaquina As String, _
                                               ByVal strMarcaMaquina As String, _
                                               ByVal strModeloMaquina As String, _
                                               ByVal strTipoCarroceriaMaquina As String, _
                                               ByVal strDescripcionAutoMaquina As String, _
                                               ByVal strEstadobienMaquina As String, _
                                               ByVal strPlacaMaquina As String, _
                                               ByVal strMedidasMaquina As String, _
                                               ByVal intCantidadMaquina As String, _
                                               ByVal strUsoBienMaquina As String, _
                                               ByVal strUbicacionBienMaquina As String, _
                                               ByVal strDepartamentoMaquinaria As String, _
                                               ByVal strProvinciaMaquinaria As String, _
                                               ByVal strDistritoMaquinaria As String, _
                                               ByVal intFlagOrigen As Integer, _
                                               ByVal strColor As String, _
                                               ByVal strCodTipoBien As String) As String 'Agrego color en Contrato
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudcreditoestructura As New ESolicitudcreditoestructura
            Dim objESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

            Dim pESolicitudcreditoestructura As String
            Dim pESolicitudcreditoestructuracarac As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objESolicitudcreditoestructura
                .Codsolicitudcredito = strNroContrato
                .Codproveedor = Nothing
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Uso = GCCUtilitario.NullableString(strUsoBienMaquina)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacionBienMaquina)
                .Cantidadproducto = GCCUtilitario.StringToInteger(intCantidadMaquina)
                If strEstadobienMaquina = "0" Then
                    .Codigoestadobien = Nothing
                Else
                    .Codigoestadobien = GCCUtilitario.NullableString(strEstadobienMaquina)
                End If
                ' Inicio IBK - AAE - Agrego el código de usuario
                .Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Fin IBK
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Flag_origen = intFlagOrigen
                .AudUsuarioCreacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                'IBK - RPH
                .Codigotipobien = strCodTipoBien
                'Fin
            End With

            With objESolicitudCreditoEstructuraCarac
                .Codsolicitudcredito = strNroContrato
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Nroserie = GCCUtilitario.NullableString(strSerieMotorMaquina)
                .Nromotor = GCCUtilitario.NullableString(strNumeroMotorMaquina)
                .Anio = GCCUtilitario.StringToInteger(strFabricacionMaquina)
                .Marca = GCCUtilitario.NullableString(strMarcaMaquina)
                .Modelo = GCCUtilitario.NullableString(strModeloMaquina)
                .Carroceria = GCCUtilitario.NullableString(strTipoCarroceriaMaquina)
                .Comentario = GCCUtilitario.NullableString(strDescripcionAutoMaquina)
                .Placa = GCCUtilitario.NullableString(strPlacaMaquina)
                .Medidas = GCCUtilitario.NullableString(strMedidasMaquina)
                If strDepartamentoMaquinaria = "0" Then
                    .Departamento = Nothing
                Else
                    .Departamento = GCCUtilitario.NullableString(strDepartamentoMaquinaria)
                End If
                If strProvinciaMaquinaria = "0" Then
                    .Provincia = Nothing
                Else
                    .Provincia = GCCUtilitario.NullableString(strProvinciaMaquinaria)
                End If
                If strDistritoMaquinaria = "0" Then
                    .Distrito = Nothing
                Else
                    .Distrito = GCCUtilitario.NullableString(strDistritoMaquinaria)
                End If
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                'IBK - RPH
                .Color = strColor

            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructura = GCCUtilitario.SerializeObject(objESolicitudcreditoestructura)
            pESolicitudcreditoestructuracarac = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarac)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnInsertarBien(pESolicitudCredito, _
                                                                      pESolicitudcreditoestructura, _
                                                                      pESolicitudcreditoestructuracarac)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GuardarMaquinaEditar
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="intSecFinanciamiento"></param>
    ''' <param name="strTipoRubroFinanciamiento"></param>
    ''' <param name="strSerieMotorMaquina"></param>
    ''' <param name="strNumeroMotorMaquina"></param>
    ''' <param name="strFabricacionMaquina"></param>
    ''' <param name="strMarcaMaquina"></param>
    ''' <param name="strModeloMaquina"></param>
    ''' <param name="strTipoCarroceriaMaquina"></param>
    ''' <param name="strDescripcionAutoMaquina"></param>
    ''' <param name="strEstadobienMaquina"></param>
    ''' <param name="strPlacaMaquina"></param>
    ''' <param name="strMedidasMaquina"></param>
    ''' <param name="intCantidadMaquina"></param>
    ''' <param name="strUsoBienMaquina"></param>
    ''' <param name="strUbicacionBienMaquina"></param>
    ''' <param name="strDepartamentoMaquinaria"></param>
    ''' <param name="strProvinciaMaquinaria"></param>
    ''' <param name="strDistritoMaquinaria"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarMaquinaEditar(ByVal strNroContrato As String, _
                                                ByVal intSecFinanciamiento As String, _
                                                ByVal strTipoRubroFinanciamiento As String, _
                                                ByVal strSerieMotorMaquina As String, _
                                                ByVal strNumeroMotorMaquina As String, _
                                                ByVal strFabricacionMaquina As String, _
                                                ByVal strMarcaMaquina As String, _
                                                ByVal strModeloMaquina As String, _
                                                ByVal strTipoCarroceriaMaquina As String, _
                                                ByVal strDescripcionAutoMaquina As String, _
                                                ByVal strEstadobienMaquina As String, _
                                                ByVal strPlacaMaquina As String, _
                                                ByVal strMedidasMaquina As String, _
                                                ByVal intCantidadMaquina As String, _
                                                ByVal strUsoBienMaquina As String, _
                                                ByVal strUbicacionBienMaquina As String, _
                                                ByVal strDepartamentoMaquinaria As String, _
                                                ByVal strProvinciaMaquinaria As String, _
                                                ByVal strDistritoMaquinaria As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudcreditoestructura As New ESolicitudcreditoestructura
            Dim objESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

            Dim pESolicitudcreditoestructura As String
            Dim pESolicitudcreditoestructuracarac As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objESolicitudcreditoestructura
                .Codsolicitudcredito = strNroContrato
                .Secfinanciamiento = GCCUtilitario.StringToInteger(intSecFinanciamiento)
                .Codproveedor = Nothing
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Uso = GCCUtilitario.NullableString(strUsoBienMaquina)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacionBienMaquina)
                .Cantidadproducto = GCCUtilitario.StringToInteger(intCantidadMaquina)
                If strEstadobienMaquina = "0" Then
                    .Codigoestadobien = Nothing
                Else
                    .Codigoestadobien = GCCUtilitario.NullableString(strEstadobienMaquina)
                End If
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Inicio IBK - AAE - Agrego el código de usuario
                .Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Fin IBK
            End With

            With objESolicitudCreditoEstructuraCarac
                .Codsolicitudcredito = strNroContrato
                .Secfinanciamiento = GCCUtilitario.StringToInteger(intSecFinanciamiento)
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Nroserie = GCCUtilitario.NullableString(strSerieMotorMaquina)
                .Nromotor = GCCUtilitario.NullableString(strNumeroMotorMaquina)
                .Anio = GCCUtilitario.StringToInteger(strFabricacionMaquina)
                .Marca = GCCUtilitario.NullableString(strMarcaMaquina)
                .Modelo = GCCUtilitario.NullableString(strModeloMaquina)
                .Carroceria = GCCUtilitario.NullableString(strTipoCarroceriaMaquina)
                .Comentario = GCCUtilitario.NullableString(strDescripcionAutoMaquina)
                .Placa = GCCUtilitario.NullableString(strPlacaMaquina)
                .Medidas = GCCUtilitario.NullableString(strMedidasMaquina)
                If strDepartamentoMaquinaria = "0" Then
                    .Departamento = Nothing
                Else
                    .Departamento = GCCUtilitario.NullableString(strDepartamentoMaquinaria)
                End If
                If strProvinciaMaquinaria = "0" Then
                    .Provincia = Nothing
                Else
                    .Provincia = GCCUtilitario.NullableString(strProvinciaMaquinaria)
                End If
                If strDistritoMaquinaria = "0" Then
                    .Distrito = Nothing
                Else
                    .Distrito = GCCUtilitario.NullableString(strDistritoMaquinaria)
                End If
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructura = GCCUtilitario.SerializeObject(objESolicitudcreditoestructura)
            pESolicitudcreditoestructuracarac = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarac)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnModificarBien(pESolicitudCredito, _
                                                                       pESolicitudcreditoestructura, _
                                                                       pESolicitudcreditoestructuracarac)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GuardarDatosOtrosNuevo
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="strTipoRubroFinanciamiento"></param>
    ''' <param name="strUsoDatosOtros"></param>
    ''' <param name="strUbicacionDatosOtros"></param>
    ''' <param name="strDescripcionDatosOtros"></param>
    ''' <param name="strMarcaDatosOtros"></param>
    ''' <param name="strModeloDatosOtros"></param>
    ''' <param name="intCantidadDatosOtros"></param>
    ''' <param name="strPartidaRegistralDatosOtros"></param>
    ''' <param name="strOficinaRegistralDatosOtros"></param>
    ''' <param name="strDepartamentoDatosOtros"></param>
    ''' <param name="strProvinciaDatosOtros"></param>
    ''' <param name="strDistritoDatosOtros"></param>
    ''' <param name="strEstadoBienInmueble"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarDatosOtrosNuevo(ByVal strNroContrato As String, _
                                                  ByVal strTipoRubroFinanciamiento As String, _
                                                  ByVal strUsoDatosOtros As String, _
                                                  ByVal strUbicacionDatosOtros As String, _
                                                  ByVal strDescripcionDatosOtros As String, _
                                                  ByVal strMarcaDatosOtros As String, _
                                                  ByVal strModeloDatosOtros As String, _
                                                  ByVal intCantidadDatosOtros As String, _
                                                  ByVal strPartidaRegistralDatosOtros As String, _
                                                  ByVal strOficinaRegistralDatosOtros As String, _
                                                  ByVal strDepartamentoDatosOtros As String, _
                                                  ByVal strProvinciaDatosOtros As String, _
                                                  ByVal strDistritoDatosOtros As String, _
                                                  ByVal strEstadoBienInmueble As String, _
                                                  ByVal intFlagOrigen As Integer, _
                                                  ByVal strCodTipoBien As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudcreditoestructura As New ESolicitudcreditoestructura
            Dim objESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

            Dim pESolicitudcreditoestructura As String
            Dim pESolicitudcreditoestructuracarac As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objESolicitudcreditoestructura
                .Codsolicitudcredito = strNroContrato
                .Codproveedor = Nothing
                ' Inicio IBK - AAE - Agrego el código de usuario
                .Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Fin IBK
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Codigoestadobien = GCCUtilitario.NullableString(strEstadoBienInmueble)
                .Uso = GCCUtilitario.NullableString(strUsoDatosOtros)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacionDatosOtros)
                .Cantidadproducto = GCCUtilitario.StringToInteger(intCantidadDatosOtros)

                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Flag_origen = intFlagOrigen

                'IBK - RPH
                .Codigotipobien = strCodTipoBien
                'Fin
            End With

            With objESolicitudCreditoEstructuraCarac
                .Codsolicitudcredito = strNroContrato
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento

                .Marca = GCCUtilitario.NullableString(strMarcaDatosOtros)
                .Modelo = GCCUtilitario.NullableString(strModeloDatosOtros)
                .Comentario = GCCUtilitario.NullableString(strDescripcionDatosOtros)
                .PartidaRegistral = GCCUtilitario.NullableString(strPartidaRegistralDatosOtros)
                .OficinaRegistral = GCCUtilitario.NullableString(strOficinaRegistralDatosOtros)
                If strDepartamentoDatosOtros = "0" Then
                    .Departamento = Nothing
                Else
                    .Departamento = GCCUtilitario.NullableString(strDepartamentoDatosOtros)
                End If
                If strProvinciaDatosOtros = "0" Then
                    .Provincia = Nothing
                Else
                    .Provincia = GCCUtilitario.NullableString(strProvinciaDatosOtros)
                End If
                If strDistritoDatosOtros = "0" Then
                    .Distrito = Nothing
                Else
                    .Distrito = GCCUtilitario.NullableString(strDistritoDatosOtros)
                End If
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructura = GCCUtilitario.SerializeObject(objESolicitudcreditoestructura)
            pESolicitudcreditoestructuracarac = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarac)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnInsertarBien(pESolicitudCredito, _
                                                                      pESolicitudcreditoestructura, _
                                                                      pESolicitudcreditoestructuracarac)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GuardarDatosOtrosEditar
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="intSecFinanciamiento"></param>
    ''' <param name="strTipoRubroFinanciamiento"></param>
    ''' <param name="strUsoDatosOtros"></param>
    ''' <param name="strUbicacionDatosOtros"></param>
    ''' <param name="strDescripcionDatosOtros"></param>
    ''' <param name="strMarcaDatosOtros"></param>
    ''' <param name="strModeloDatosOtros"></param>
    ''' <param name="intCantidadDatosOtros"></param>
    ''' <param name="strPartidaRegistralDatosOtros"></param>
    ''' <param name="strOficinaRegistralDatosOtros"></param>
    ''' <param name="strDepartamentoDatosOtros"></param>
    ''' <param name="strProvinciaDatosOtros"></param>
    ''' <param name="strDistritoDatosOtros"></param>
    ''' <param name="strEstadoBienInmueble"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarDatosOtrosEditar(ByVal strNroContrato As String, _
                                                   ByVal intSecFinanciamiento As String, _
                                                   ByVal strTipoRubroFinanciamiento As String, _
                                                   ByVal strUsoDatosOtros As String, _
                                                   ByVal strUbicacionDatosOtros As String, _
                                                   ByVal strDescripcionDatosOtros As String, _
                                                   ByVal strMarcaDatosOtros As String, _
                                                   ByVal strModeloDatosOtros As String, _
                                                   ByVal intCantidadDatosOtros As String, _
                                                   ByVal strPartidaRegistralDatosOtros As String, _
                                                   ByVal strOficinaRegistralDatosOtros As String, _
                                                   ByVal strDepartamentoDatosOtros As String, _
                                                   ByVal strProvinciaDatosOtros As String, _
                                                   ByVal strDistritoDatosOtros As String, _
                                                   ByVal strEstadoBienInmueble As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudcreditoestructura As New ESolicitudcreditoestructura
            Dim objESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

            Dim pESolicitudcreditoestructura As String
            Dim pESolicitudcreditoestructuracarac As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objESolicitudcreditoestructura
                .Codsolicitudcredito = strNroContrato
                .Secfinanciamiento = GCCUtilitario.StringToInteger(intSecFinanciamiento)
                .Codproveedor = Nothing
                ' Inicio IBK - AAE - Agrego el código de usuario
                .Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                ' Fin IBK
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento
                .Codigoestadobien = GCCUtilitario.NullableString(strEstadoBienInmueble)

                .Uso = GCCUtilitario.NullableString(strUsoDatosOtros)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacionDatosOtros)
                .Cantidadproducto = GCCUtilitario.StringToInteger(intCantidadDatosOtros)

                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            With objESolicitudCreditoEstructuraCarac
                .Codsolicitudcredito = strNroContrato
                .Secfinanciamiento = GCCUtilitario.StringToInteger(intSecFinanciamiento)
                .Tiporubrofinanciamiento = strTipoRubroFinanciamiento

                .Marca = GCCUtilitario.NullableString(strMarcaDatosOtros)
                .Modelo = GCCUtilitario.NullableString(strModeloDatosOtros)
                .Comentario = GCCUtilitario.NullableString(strDescripcionDatosOtros)
                .PartidaRegistral = GCCUtilitario.NullableString(strPartidaRegistralDatosOtros)
                .OficinaRegistral = GCCUtilitario.NullableString(strOficinaRegistralDatosOtros)
                If strDepartamentoDatosOtros = "0" Then
                    .Departamento = Nothing
                Else
                    .Departamento = GCCUtilitario.NullableString(strDepartamentoDatosOtros)
                End If
                If strProvinciaDatosOtros = "0" Then
                    .Provincia = Nothing
                Else
                    .Provincia = GCCUtilitario.NullableString(strProvinciaDatosOtros)
                End If
                If strDistritoDatosOtros = "0" Then
                    .Distrito = Nothing
                Else
                    .Distrito = GCCUtilitario.NullableString(strDistritoDatosOtros)
                End If
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pESolicitudcreditoestructura = GCCUtilitario.SerializeObject(objESolicitudcreditoestructura)
            pESolicitudcreditoestructuracarac = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarac)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objContratoTx.fblnModificarBien(pESolicitudCredito, _
                                                                       pESolicitudcreditoestructura, _
                                                                       pESolicitudcreditoestructuracarac)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Anexos"
    ''' <summary>
    ''' GuardarContratoYGenerarAnexos
    ''' </summary>
    ''' <param name="pCodigoContrato"></param>
    ''' <param name="pCodigoCotizacion"></param>
    ''' <param name="pClasificacionContrato"></param>
    ''' <param name="pCodigoEstadoContrato"></param>
    ''' <param name="pFechaRegistroPublico"></param>
    ''' <param name="pFechaFirmaNotaria"></param>
    ''' <param name="pCodigoEstadoCivil"></param>
    ''' <param name="pNombreConyuge"></param>
    ''' <param name="pTipoDocumentoConyuge"></param>
    ''' <param name="pnumerodocumento"></param>
    ''' <param name="pImporteAtrasoPorc"></param>
    ''' <param name="pOtrasPenalidades"></param>
    ''' <param name="pdiasVencimiento"></param>
    ''' <param name="pPorcentajeCuota"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarContratoYGenerarAnexos(ByVal pCodigoContrato As String, _
                                                         ByVal pCodigoCotizacion As String, _
                                                         ByVal pClasificacionContrato As String, _
                                                         ByVal pCodigoEstadoContrato As String, _
                                                         ByVal pFechaRegistroPublico As String, _
                                                         ByVal pFechaFirmaNotaria As String, _
                                                         ByVal pCodigoEstadoCivil As String, _
                                                         ByVal pNombreConyuge As String, _
                                                         ByVal pTipoDocumentoConyuge As String, _
                                                         ByVal pnumerodocumento As String, _
                                                         ByVal pImporteAtrasoPorc As String, _
                                                         ByVal pOtrasPenalidades As String, _
                                                         ByVal pdiasVencimiento As String, _
                                                         ByVal pPorcentajeCuota As String, _
                                                         ByVal pClienteRazonSocial As String, _
                                                         ByVal pClienteDomicilioLegal As String) As String
        Dim oGccAnexo As New GCC_Anexo

        Try
            Dim objContratoTx As New LContratoTx

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            Dim objEGccContratootroconcepto As New EGcc_contratootroconcepto
            Dim pEGccContratootroconcepto As String

            Dim nombreArchivoAnexos As String

            ' Datos del contrato
            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.CodigoClasificacionContrato = pClasificacionContrato
            objESolicitudCredito.Codigoestadocontrato = pCodigoEstadoContrato
            objESolicitudCredito.FechaRegistroPublico = GCCUtilitario.StringToDateTime(pFechaRegistroPublico)
            objESolicitudCredito.Fechafirmanotaria = GCCUtilitario.StringToDateTime(pFechaFirmaNotaria)
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            ' Datos del Cónyuge
            objESolicitudCredito.Nombreconyuge = GCCUtilitario.NullableString(pNombreConyuge)
            objESolicitudCredito.Codigotipodocconyuge = GCCUtilitario.NullableString(pTipoDocumentoConyuge)
            objESolicitudCredito.Numerodocumentoconyuge = GCCUtilitario.NullableString(pnumerodocumento)

            ' Indicar que se genero el contrato
            If pCodigoEstadoContrato = "03" Then
                objESolicitudCredito.Modificado = False
            Else
                objESolicitudCredito.Modificado = True
            End If

            objESolicitudCredito.ClienteRazonSocial = GCCUtilitario.NullableString(pClienteRazonSocial)
            objESolicitudCredito.ClienteDomicilioLegal = GCCUtilitario.NullableString(pClienteDomicilioLegal)


            ' Tasas y Comisiones
            objEGccContratootroconcepto.Numerocontrato = pCodigoContrato

            ' Tasas y Comisiones
            objEGccContratootroconcepto.Numerocontrato = pCodigoContrato

            ' Penalidades
            objEGccContratootroconcepto.Importependiente = GCCUtilitario.StringToDecimal(pImporteAtrasoPorc)
            objEGccContratootroconcepto.Otraspenalidades = GCCUtilitario.NullableString(pOtrasPenalidades)
            objEGccContratootroconcepto.Diasvencimiento = GCCUtilitario.StringToInteger(pdiasVencimiento)
            objEGccContratootroconcepto.Porcentajecuota = GCCUtilitario.StringToDecimal(pPorcentajeCuota)
            objEGccContratootroconcepto.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)


            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
            pEGccContratootroconcepto = GCCUtilitario.SerializeObject(objEGccContratootroconcepto)

            Dim blnResult As Boolean = objContratoTx.fblnModificarContrato(pESolicitudCredito, _
                                                                           pEGccContratootroconcepto)

            If Not blnResult Then
                Throw New Exception("Ocurrió un error al actualizar el contrato.")
            End If

            nombreArchivoAnexos = oGccAnexo.Generar(pCodigoContrato, _
                                                     pCodigoCotizacion)

            Return "0|" + nombreArchivoAnexos
        Catch ex As Exception
            Return "1|" + ex.ToString()
        End Try
    End Function
    ''' <summary>
    ''' GenerarAnexos
    ''' </summary>
    ''' <param name="pCodigoContrato"></param>
    ''' <param name="pCodigoCotizacion"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GenerarAnexos(ByVal pCodigoContrato As String, _
                                         ByVal pCodigoCotizacion As String) As String
        Dim objContratoNTx As New LContratoNTx
        Dim oGCC_Anexo As New GCC_Anexo

        ' Clasificación del producto financiero, leasing, leaseback.
        Dim codProductoFinancieroActivo As String = ""
        ' Clasificación del bien.
        Dim tipoRubroFinanciamiento As String = ""
        ' Nacional o importado.
        Dim procedencia As String = ""
        ' Cesión de Posición Contractual, documentos del contrato.
        Dim cpc As String = ""
        Dim subtipoDeContrato As String = ""
        Dim nombreArchivoAnexos As String = ""
        Dim CodigoClasificacionContrato As String = ""

        Try
            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))

            For Each oRow As DataRow In dtContrato.Rows
                If Not oRow.Item("CodProductoFinancieroActivo") Is DBNull.Value Then
                    codProductoFinancieroActivo = oRow.Item("CodProductoFinancieroActivo").ToString()
                Else
                    Throw New Exception("Se requiere el definir el tipo de contrato del crédito.")
                End If
                If Not oRow.Item("TipoRubroFinanciamiento") Is DBNull.Value Then
                    tipoRubroFinanciamiento = oRow.Item("TipoRubroFinanciamiento").ToString()
                Else
                    Throw New Exception("Se requiere el definir la clasificación del bien.")
                End If
                ' Cesión de Posición Contractual
                If Not oRow.Item("CPC") Is DBNull.Value Then
                    cpc = oRow.Item("CPC").ToString()
                End If
                ' Nacional o importado
                If Not oRow.Item("CodigoProcedencia") Is DBNull.Value Then
                    procedencia = oRow.Item("CodigoProcedencia").ToString()
                End If
                ' Subtipo de contrato, total o parcial
                If Not oRow.Item("CodigoSubTipoContrato") Is DBNull.Value Then
                    subtipoDeContrato = oRow.Item("CodigoSubTipoContrato").ToString()
                End If
                ' Si el bien inmueble es embarcación
                If Not oRow.Item("CodigoClasificacionContrato") Is DBNull.Value Then
                    CodigoClasificacionContrato = oRow.Item("CodigoClasificacionContrato").ToString()
                End If
            Next oRow

            ' Selecciona una de las quince plantillas.
            ' Vehículo
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, tipoRubroFinanciamiento) <> -1) _
                And cpc = "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirectoVehicular(pCodigoContrato, _
                                                                         pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, tipoRubroFinanciamiento) <> -1) _
                And cpc <> "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirectoVehicularConCesion(pCodigoContrato, _
                                                                                  pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
                And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, tipoRubroFinanciamiento) <> -1) _
                And cpc = "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeaseBackVehicular(pCodigoContrato, _
                                                                    pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
                And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, tipoRubroFinanciamiento) <> -1) _
                And cpc <> "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeaseBackVehicularConCesion(pCodigoContrato, _
                                                                             pCodigoCotizacion)
            End If

            ' Mueble
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc = "" _
                And procedencia = GCCConstante.PROCEDENCIA_LOCAL _
                And subtipoDeContrato = GCCConstante.SUBTIPO_TOTAL Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirectoMueble(pCodigoContrato, _
                                                                      pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc = "" _
                And procedencia = GCCConstante.PROCEDENCIA_LOCAL _
                And subtipoDeContrato <> GCCConstante.SUBTIPO_TOTAL Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirectoMueblePagoParcial(pCodigoContrato, _
                                                                                 pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc <> "" _
                And procedencia <> GCCConstante.PROCEDENCIA_IMPORTACION Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirecto_MuebleConCesion(pCodigoContrato, _
                                                                                pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc = "" _
                And procedencia <> GCCConstante.PROCEDENCIA_IMPORTACION Then
                nombreArchivoAnexos = oGCC_Anexo.LeaseBackMueble(pCodigoContrato, _
                                                                 pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc <> "" _
                And procedencia <> GCCConstante.PROCEDENCIA_IMPORTACION Then
                nombreArchivoAnexos = oGCC_Anexo.LeaseBackMuebleConCesion(pCodigoContrato, _
                                                                          pCodigoCotizacion)
            End If
            ' Mueble - importación
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc = "" _
                And procedencia = GCCConstante.PROCEDENCIA_IMPORTACION Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirecto_MuebleImportacion(pCodigoContrato, _
                                                                                  pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Mueble, tipoRubroFinanciamiento) <> -1) _
                And cpc <> "" _
                And procedencia = GCCConstante.PROCEDENCIA_IMPORTACION Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirecto_MuebleImportacionConCesion(pCodigoContrato, _
                                                                                           pCodigoCotizacion)
            End If

            ' Inmueble
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Inmueble, tipoRubroFinanciamiento) <> -1) _
                And CodigoClasificacionContrato <> GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
                And cpc = "" Then

                nombreArchivoAnexos = oGCC_Anexo.LeasingDirectoInmueble(pCodigoContrato, _
                                                                        pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Inmueble, tipoRubroFinanciamiento) <> -1) _
                And CodigoClasificacionContrato <> GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
                And cpc <> "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingInmuebleConCesion(pCodigoContrato, _
                                                                          pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Inmueble, tipoRubroFinanciamiento) <> -1) _
                And CodigoClasificacionContrato = GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
                And cpc = "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingDirectoEmbarcacion(pCodigoContrato, _
                                                                           pCodigoCotizacion)
            End If
            If (codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
                And (Array.IndexOf(GCCConstante.Anexo_Inmueble, tipoRubroFinanciamiento) <> -1) _
                And CodigoClasificacionContrato = GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
                And cpc <> "" Then
                nombreArchivoAnexos = oGCC_Anexo.LeasingEmbarcacionConCesion(pCodigoContrato, _
                                                                             pCodigoCotizacion)
            End If

            Return "0|" + nombreArchivoAnexos
        Catch ex As Exception
            Return "1|" + ex.ToString()
        End Try
    End Function

#End Region

#Region "Representantes Banco"

    ''' <summary>
    ''' Lista los representantes del cliente para el actual contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListaRepresentantes(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pNumeroContrato As String, _
                                               ByVal pCodigoTipoRepresentante As String, _
                                               ByVal pFields As String) As JQGridJsonResponse

        Try
            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteContrato As New EGcc_contratorepresentante
            Dim strEGccontratoRepresentante As String

            With oEGccRepresentanteContrato
                .Numerocontrato = GCCUtilitario.NullableString(pNumeroContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(pCodigoTipoRepresentante)
            End With

            strEGccontratoRepresentante = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteContrato)
            Dim dtContratoRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(pPageSize, _
                                                                                                                                                           pCurrentPage, _
                                                                                                                                                           pSortColumn, _
                                                                                                                                                           pSortOrder, _
                                                                                                                                                           strEGccontratoRepresentante, _
                                                                                                                                                           Nothing))
            'Número total de páginas
            Dim oJQGridJsonResponse As New JQGridJsonResponse
            'Total de registros a mostrar.
            Dim totalRecords As Integer
            Dim totalCurrent As Integer

            If dtContratoRepresentante.Rows.Count = 0 Then
                totalRecords = 0
                totalCurrent = 0
            Else
                totalRecords = Convert.ToInt32(dtContratoRepresentante.Rows(0)("RecordCount"))
                totalCurrent = Convert.ToInt32(dtContratoRepresentante.Rows(0)("TOTAL_PAGINA"))
            End If
            If pCurrentPage > totalCurrent Then
                pCurrentPage = totalCurrent
            End If

            'Número total de páginas
            Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                         pPageSize)
            Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                                   pCurrentPage, _
                                                                   totalRecords, _
                                                                   dtContratoRepresentante, _
                                                                   pFields)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Representantes Cliente"
    ''' <summary>
    ''' RepresentanteEliminar
    ''' </summary>
    ''' <param name="pCodigoContrato"></param>
    ''' <param name="pCodigoTipoRepresentante"></param>
    ''' <param name="pRepresentantesEliminar"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RepresentanteEliminar(ByVal pCodigoContrato As String, _
                                                 ByVal pCodigoTipoRepresentante As String, _
                                                 ByVal pRepresentantesEliminar As String) As String
        Dim objCheckListTx As New LCheckListTx

        Dim oListEGcc_contratorepresentante As New ListEGcc_contratorepresentante
        Dim pListEGcc_contratorepresentante As String

        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pESolicitudCredito As String

        Dim vRepresentantesEliminar As String() = pRepresentantesEliminar.Split(New Char() {"|"c})

        For i As Integer = 0 To vRepresentantesEliminar.Length - 1
            Dim oEGcc_contratorepresentante As New EGcc_contratorepresentante
            oEGcc_contratorepresentante.Numerocontrato = pCodigoContrato
            oEGcc_contratorepresentante.Codigotiporepresentante = pCodigoTipoRepresentante
            oEGcc_contratorepresentante.Codigorepresentante = CInt(vRepresentantesEliminar(i))
            oEGcc_contratorepresentante.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            oListEGcc_contratorepresentante.Add(oEGcc_contratorepresentante)
        Next i

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Modificado = True
        objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        pListEGcc_contratorepresentante = GCCUtilitario.SerializeObject(oListEGcc_contratorepresentante)
        pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        Dim blnResult As Boolean = objCheckListTx.RepresentanteContratoListDel(pESolicitudCredito, _
                                                                               pListEGcc_contratorepresentante)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

#End Region

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <param name="pstrCodigoContrato">Código del Contrato</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/09/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GestionFlujo(ByVal pstrCodigoContrato As String) As String

        Try
            Dim objLUtilTX As New LUtilTX
            objLUtilTX.fblnGestionFlujo(pstrCodigoContrato, GCCConstante.C_BLOQUEO_MODULO_CONTRATO, GCCUtilitario.NullableString(GCCSession.CodigoUsuario))
            Return "0"
        Catch ex As Exception
            Return "1"
        End Try

    End Function


#Region "Cronograma"
    <WebMethod()> _
     Public Shared Function PaginaCronograma(ByVal pstrPaginaActual As String) As JQGridJsonResponse
        Try

            'Arma Consulta Cronograma
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = HttpContext.Current.Session("DTB_CRONOGRAMA")

            If Not objListECronograma Is Nothing Then

                'Datos Paginacion
                Dim intTotalRegistros As Integer = objListECronograma.Count
                Dim intTotalxPagina As Integer = 50
                Dim intTotalPaginas As Integer = 0
                Dim intPaginaActual As Integer = GCCUtilitario.CheckInt(pstrPaginaActual)

                Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
                If decPaginasTotal <= 1 Then
                    intTotalPaginas = 1
                ElseIf decPaginasTotal <= 2 Then
                    intTotalPaginas = 2
                Else
                    intTotalPaginas = 3
                End If

                'Resize Gronograma Datatable
                Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
                Dim objECronograma As New EGcc_cotizacioncronograma
                Dim intContador As Integer = 0
                For Each objECronograma In objListECronograma
                    If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                        objListECronogramaNuevo.Add(objECronograma)
                    End If
                    intContador = intContador + 1
                Next

                'Devuelve
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

            Else
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                Return objJQGridJsonResponse.JQGridJsonResponseError("Consulta Vacia")
            End If

        Catch ex As Exception
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseError(ex.Message)
        End Try

    End Function



    ''' <summary>
    ''' Prepara Cronograma para insertar
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Shared Function PreparaCronograma(ByVal pdtbCronograma As DataTable, ByVal pstrCodigoCotizacion As String, ByVal pstrVersionCotizacion As String, ByVal pobjEGcc_cotizacion As EGcc_cotizacion, ByVal booValidaPrimeraFila As Boolean) As ListEGcc_cotizacioncronograma

        Try
            'Declara Variables
            Dim objCotizacionNTx As New LCotizacionNTx
            Dim objListECronograma As New ListEGcc_cotizacioncronograma

            'Valida Cuota Inicial
            Dim intContadorCronograma As Integer = 0
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecuotainicial)
            Dim blnMuestraFila As Boolean = True

            'Valida si existe
            If pdtbCronograma.Rows.Count > 0 Then

                For Each oRow As DataRow In pdtbCronograma.Rows

                    If Not decCuotaInicial > 0 And intContadorCronograma = 0 And booValidaPrimeraFila Then
                        blnMuestraFila = False
                    Else
                        blnMuestraFila = True
                    End If

                    If blnMuestraFila Then

                        Dim objECronograma As New EGcc_cotizacioncronograma
                        With objECronograma
                            .Numerocuota = GCCUtilitario.CheckInt(oRow.Item("Nu_cuota").ToString())
                            .Codigocotizacion = pstrCodigoCotizacion
                            .Versioncotizacion = pstrVersionCotizacion
                            .Fechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .Cantdiascuota = GCCUtilitario.StringToInteger(oRow.Item("Nu_dias").ToString())
                            .Montosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString())
                            .Montointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString())
                            .Montoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString())
                            .Montototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString())
                            .Saldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString())
                            .Interessegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString())
                            .Principalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString())
                            .Montocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString())

                            .SaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString())
                            .InteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString())
                            .PrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString())
                            .CuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString())

                            .Totalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString())
                            .Montototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString())

                            'Mostrar
                            .SFechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .SMontosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteressegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SCuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .STotalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString()).ToString(GCCConstante.C_FormatMiles)

                            .Audestadologico = 1
                            '.AudFechaRegistro = 	
                            '.AudFechaModificacion = 	
                            .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                            .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        End With
                        objListECronograma.Add(objECronograma)

                    End If

                    intContadorCronograma = intContadorCronograma + 1

                Next

                Return objListECronograma

            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Listado de Cronograma de Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 28/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoCotizacionCronograma(ByVal pstrNroCotizacion As String, _
                                                       ByVal pstrVersionCotizacion As String _
                                                       ) As JQGridJsonResponse

        'Variables
        Dim objCotizacionNTx As New LCotizacionNTx

        Try
            'Inicializa Objeto
            Dim objECotizacion As New EGcc_cotizacion
            Dim strECotizacion As String
            With objECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrNroCotizacion)
                .Versioncotizacion = GCCUtilitario.StringToInteger(pstrVersionCotizacion)
            End With
            strECotizacion = GCCUtilitario.SerializeObject(objECotizacion)

            'Ejecuta Consulta
            Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.CotizacionCronogramaGet(strECotizacion))
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = PreparaCronograma(dtCronograma, pstrNroCotizacion, pstrVersionCotizacion, objECotizacion, False)

            HttpContext.Current.Session("DTB_CRONOGRAMA") = objListECronograma

            'Datos Paginacion
            Dim intTotalRegistros As Integer = objListECronograma.Count
            Dim intTotalxPagina As Integer = 50
            Dim intTotalPaginas As Integer = 0
            Dim intPaginaActual As Integer = 1

            Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
            If decPaginasTotal <= 1 Then
                intTotalPaginas = 1
            ElseIf decPaginasTotal <= 2 Then
                intTotalPaginas = 2
            Else
                intTotalPaginas = 3
            End If

            'Resize Gronograma Datatable
            Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
            Dim objECronograma As New EGcc_cotizacioncronograma
            Dim intContador As Integer = 0
            For Each objECronograma In objListECronograma
                If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                    objListECronogramaNuevo.Add(objECronograma)
                End If
                intContador = intContador + 1
            Next

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region

#End Region

End Class
