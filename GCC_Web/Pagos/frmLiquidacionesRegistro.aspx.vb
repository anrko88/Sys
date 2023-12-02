Imports System.Net
Imports System.Diagnostics
Imports System.IO
Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data.SqlClient

Imports GCC.Entity
Imports GCC.LogicWS


Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.OfficeProperties
Imports System.Text
Imports System.Drawing


Partial Class Pagos_frmLiquidacionesRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmLiquidacionesRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 26/12/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Dim strCodSolicitudCredito As String = Request.QueryString("hddCodSolicitudCredito")
                Dim strCodigoLiquidacion As String = Request.QueryString("hddCodigoLiquidacion")

                GCCUtilitario.CargarComboMoneda(Me.cmbCodMonedaCargo)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoLiquidacion, GCCConstante.C_TABLAGENERICA_ESTADO_LIQUIDACION)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoLiquidacion, GCCConstante.C_TABLAGENERICA_TIPO_LIQUIDACION)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCronograma, GCCConstante.C_TABLAGENERICA_TIPO_CRONOGRAMA)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoGracia, GCCConstante.C_TABLAGENERICA_TIPO_GRACIA)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbPeriodicidad, GCCConstante.C_TABLAGENERICA_PERIOCIDAD)

                If strCodSolicitudCredito Is Nothing Then

                    GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbFrecuenciaPago, GCCConstante.C_TABLAGENERICA_FRECUENCIA_PAGO, GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_MENSUAL)

                    'Eventos
                    txtNroContrato.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imgBsqContrato.UniqueID + "').click();return false;}} else {return true}; ")

                    Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_NUEVO
                    Me.hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO

                    Me.txtFechaValor.Value = Date.Today.ToString("dd/MM/yyyy")
                    Me.txtFechaProceso.Value = Date.Today.ToString("dd/MM/yyyy")

                    Me.hddPorcIGV.Value = GCCUtilitario.ObtenerPorcIGV()
                Else
                    Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_EDITAR
                    Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                    'Datos Generales
                    ObtieneDatosContrato(strCodSolicitudCredito)
                    ObtieneDatosLiquidacion(strCodSolicitudCredito, strCodigoLiquidacion)
                End If
                hddPerfil.Value = GCCSession.PerfilUsuario

            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.ShowLoad(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowLoad("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Consulta Datos Contrato
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 25/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ConsultaContrato(ByVal strCodSolicitudCredito As String) As ESolicitudcredito
        '-------------------------------------------
        ' Consulta Contrato
        '-------------------------------------------
        Try
            Dim oESolicitudcredito As New ESolicitudcredito

            'Variables
            Dim objLContratoNTx As New LContratoNTx

            'Ejecuta Consulta
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLContratoNTx.RetornarContrato(strCodSolicitudCredito))

            'Valida si existe
            If dtContrato.Rows.Count > 0 Then

                oESolicitudcredito.Codsolicitudcredito = dtContrato.Rows(0).Item("CodSolicitudCredito").ToString
                oESolicitudcredito.Codunico = dtContrato.Rows(0).Item("CodUnico").ToString
                oESolicitudcredito.ClienteRazonSocial = dtContrato.Rows(0).Item("NombreSubPrestatario").ToString
                oESolicitudcredito.NombreTipoPersona = dtContrato.Rows(0).Item("NombreTipoPersona").ToString
                oESolicitudcredito.NombreTipoDocIdentificacion = dtContrato.Rows(0).Item("NombreTipoDocIdentificacion").ToString
                oESolicitudcredito.NroDocIdentificacion = dtContrato.Rows(0).Item("NroDocIdentificacion").ToString
                oESolicitudcredito.SubTipoContrato = dtContrato.Rows(0).Item("SubTipoContrato").ToString
                oESolicitudcredito.NombreMonedaAPP = dtContrato.Rows(0).Item("NombreMonedaAPP").ToString
                oESolicitudcredito.Codmoneda = dtContrato.Rows(0).Item("CodMoneda").ToString
                oESolicitudcredito.NombreEjecutivoLeasing = dtContrato.Rows(0).Item("NombreEjecutivoLeasing").ToString

                oESolicitudcredito.CodError = 0
            Else
                oESolicitudcredito.CodError = 1
                oESolicitudcredito.MsgError = "El contrato ingresado no existe"
            End If
            Return oESolicitudcredito

        Catch ex As Exception
            Dim oESolicitudcredito As New ESolicitudcredito
            oESolicitudcredito.CodError = 1
            oESolicitudcredito.MsgError = "No se pudo cargar los datos del contrato."
            Return oESolicitudcredito
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function ObtenerOperacionExtornable(ByVal pstrCodOperacionActiva As String) As ECreditoRecuperacion

        'Variables
        Dim objLPagosNTx As New LPagosNTx

        Try
            'Inicializa Objeto
            Dim objEGCC_PagoCuotas As New EGCC_PagoCuotas
            Dim strEGCC_PagoCuotas As String
            With objEGCC_PagoCuotas
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodOperacionActiva)
                .CUCliente = ""
                .RazonSocial = ""
                .NroAutorizacion = ""
                .FechaPagoInicio = ""
                .FechaPagoFin = ""
                .TipoContrato = ""
                .CodigoMoneda = ""
            End With


            'Retorno
            Dim oECreditoRecuperacion As New ECreditoRecuperacion

            'Buscar pagos ingresados
            objEGCC_PagoCuotas.CodigoEstado = "I"
            strEGCC_PagoCuotas = GCCUtilitario.SerializeObject(objEGCC_PagoCuotas)
            Dim dtPagoCuotas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoCuotas(1, 1, "NumSecRecuperacion", "DESC", strEGCC_PagoCuotas))

            If dtPagoCuotas.Rows.Count > 0 Then
                oECreditoRecuperacion.CodError = 1
                oECreditoRecuperacion.MsgError = "Exiten pagos de cuotas ingresados para este crédito. No es posible extornar."
                Return oECreditoRecuperacion
            End If

            'Buscar pagos enviados a host
            objEGCC_PagoCuotas.CodigoEstado = "C"
            strEGCC_PagoCuotas = GCCUtilitario.SerializeObject(objEGCC_PagoCuotas)
            dtPagoCuotas = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoCuotas(1, 1, "NumSecRecuperacion", "DESC", strEGCC_PagoCuotas))

            If dtPagoCuotas.Rows.Count > 0 Then
                oECreditoRecuperacion.CodError = 1
                oECreditoRecuperacion.MsgError = "Exiten pagos por procesar para este crédito. No es posible extornar."
                Return oECreditoRecuperacion
            End If

            'Buscar pagos ejecutados
            objEGCC_PagoCuotas.CodigoEstado = "H"
            strEGCC_PagoCuotas = GCCUtilitario.SerializeObject(objEGCC_PagoCuotas)
            dtPagoCuotas = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoCuotas(1, 1, "NumSecRecuperacion", "DESC", strEGCC_PagoCuotas))

            If dtPagoCuotas.Rows.Count = 0 Then
                oECreditoRecuperacion.CodError = 1
                oECreditoRecuperacion.MsgError = "El crédito no tiene pagos ejecutados. No es posible extornar."
                Return oECreditoRecuperacion
            Else
                oECreditoRecuperacion.CodError = 0
                oECreditoRecuperacion.CodOperacionActiva = dtPagoCuotas.Rows(0).Item("CodSolicitudCredito").ToString.Trim
                oECreditoRecuperacion.NumSecRecuperacion = dtPagoCuotas.Rows(0).Item("NumSecRecuperacion").ToString.Trim
                Return oECreditoRecuperacion
            End If

        Catch ex As Exception
            Dim oECreditoRecuperacion As New ECreditoRecuperacion
            oECreditoRecuperacion.CodError = 1
            oECreditoRecuperacion.MsgError = "Ocurrio un error al obtener la operacion extornable. No se realizo ningun cambio."
            Return oECreditoRecuperacion
        End Try

    End Function

    'IBK RPR 25/12/2012
    Private Sub ObtieneDatosContrato(ByVal strCodSolicitudCredito As String)
        Try
            'Variables
            Dim objLContratoNTx As New LContratoNTx

            'Ejecuta Consulta
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLContratoNTx.ObtenerContrato(strCodSolicitudCredito))

            'Valida si existe
            If dtContrato.Rows.Count > 0 Then

                txtNroContrato.Value = dtContrato.Rows(0).Item("CodSolicitudCredito").ToString
                hddCodSolicitudCredito.Value = dtContrato.Rows(0).Item("CodSolicitudCredito").ToString
                txtCuCliente.Value = dtContrato.Rows(0).Item("CodUnico").ToString
                txtRazonSocial.Value = dtContrato.Rows(0).Item("NombreSubPrestatario").ToString
                txtTipoPersona.Value = dtContrato.Rows(0).Item("NombreTipoPersona").ToString
                txtTipoDocumento.Value = dtContrato.Rows(0).Item("NombreTipoDocIdentificacion").ToString
                txtNumeroDocumento.Value = dtContrato.Rows(0).Item("NroDocIdentificacion").ToString
                txtTipoContrato.Value = dtContrato.Rows(0).Item("SubTipoContrato").ToString
                txtMoneda.Value = dtContrato.Rows(0).Item("NombreMonedaAPP").ToString
                hddCodMonedaContrato.Value = dtContrato.Rows(0).Item("CodMoneda").ToString
                txtEjecutivoLeasing.Value = dtContrato.Rows(0).Item("NombreEjecutivoLeasing").ToString

                txtNombreBanca.Value = dtContrato.Rows(0).Item("NombreBanca").ToString
                txtClasificacionBien.Value = dtContrato.Rows(0).Item("ClasificacionBien").ToString
                txtPorcenTasaActiva.Value = dtContrato.Rows(0).Item("PorcenTasaActiva").ToString()
                txtEstadoContrato.Value = dtContrato.Rows(0).Item("EstadoContrato").ToString
                txtNombreEstadoOperacionActiva.Value = dtContrato.Rows(0).Item("NombreEstadoOperacionActiva").ToString

                txtMonedaLiquidacion.Value = txtMoneda.Value
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'IBK RPR 26/12/2012
    Private Sub ObtieneDatosLiquidacion(ByVal strCodSolicitudCredito As String, ByVal strCodigoLiquidacion As String)

        Try
            'Variables
            Dim objLPagosNTx As New LPagosNTx

            'Inicializa Objeto
            Dim objEGCC_Liquidacion As New EGCC_Liquidacion
            With objEGCC_Liquidacion
                .CodigoLiquidacion = strCodigoLiquidacion
            End With
            Dim strEGCC_Liquidacion As String = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)

            'Ejecuta Consulta
            Dim dtLiquidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ObtenerLiquidacion(strEGCC_Liquidacion))

            If dtLiquidacion.Rows.Count > 0 Then
                txtFechaValor.Value = GCCUtilitario.CheckDateString(dtLiquidacion.Rows(0).Item("FechaValorLiquidacion"), "C")
                txtFechaProceso.Value = GCCUtilitario.CheckDateString(dtLiquidacion.Rows(0).Item("FechaProcesoLiquidacion"), "C")
                txtCodigoLiquidacion.Value = dtLiquidacion.Rows(0).Item("CodigoLiquidacion").ToString.Trim
                GCCUtilitario.SeleccionaCombo(cmbTipoCambio, dtLiquidacion.Rows(0).Item("CodTipoCambio").ToString.Trim)
                Dim strTipoCambio As String = dtLiquidacion.Rows(0).Item("TipoCambio").ToString.Trim
                If (strTipoCambio <> "") Then txtTipoCambio.Value = Val(strTipoCambio).ToString("##0.000")

                Dim strTipoLiquidacion As String = dtLiquidacion.Rows(0).Item("TipoLiquidacion").ToString.Trim
                GCCUtilitario.SeleccionaCombo(cmbEstadoLiquidacion, dtLiquidacion.Rows(0).Item("EstadoLiquidacion").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoLiquidacion, strTipoLiquidacion)

                Dim strFlagAdenda As String = "N"
                If (dtLiquidacion.Rows(0).Item("FlagAdendaContrato").ToString.Trim = "S") Then
                    strFlagAdenda = "S"
                End If
                cbFlagAdenda.Checked = IIf(strFlagAdenda = "S", True, False)

                hddPorcIGV.Value = dtLiquidacion.Rows(0).Item("PorcIGV").ToString.Trim

                'Datos Cronograma
                If (strTipoLiquidacion = "R") Then
                    GCCUtilitario.SeleccionaCombo(cmbTipoCronograma, dtLiquidacion.Rows(0).Item("TipoCronograma").ToString.Trim)
                    GCCUtilitario.SeleccionaCombo(cmbPeriodicidad, dtLiquidacion.Rows(0).Item("Periodicidad").ToString.Trim)
                    GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbFrecuenciaPago, GCCConstante.C_TABLAGENERICA_FRECUENCIA_PAGO, cmbPeriodicidad.Value)
                    GCCUtilitario.SeleccionaCombo(cmbFrecuenciaPago, dtLiquidacion.Rows(0).Item("FrecuenciaPago").ToString.Trim)
                    GCCUtilitario.SeleccionaCombo(cmbTipoGracia, dtLiquidacion.Rows(0).Item("TipoGracia").ToString.Trim)
                    txtNroCuotas.Value = dtLiquidacion.Rows(0).Item("NroCuotas").ToString.Trim
                    txtPlazoGracia.Value = dtLiquidacion.Rows(0).Item("PlazoGracia").ToString.Trim
                    txtAmortizacion.Value = Val(dtLiquidacion.Rows(0).Item("AmortizacionCapital").ToString.Trim).ToString("###,###,###,##0.00")
                    txtFechaPrimerVencimiento.Value = Convert.ToDateTime(dtLiquidacion.Rows(0).Item("FechaPrimerVencimiento").ToString.Trim).ToString("dd/MM/yyyy")
                End If

                'Via de Cobranza
                hddTipoViaCobranza.Value = dtLiquidacion.Rows(0).Item("TipoViaCobranza").ToString.Trim

                'Estado del Pago
                'txtFechaRecuperacion.Value = GCCUtilitario.CheckDateString(dtPagoCuotas.Rows(0).Item("FechaRecuperacion").ToString.Trim, "C")
                'GCCUtilitario.SeleccionaCombo(cmbEstadoRecuperacion, dtPagoCuotas.Rows(0).Item("EstadoRecuperacion").ToString.Trim)
                'hddEstadoRecuperacion.Value = dtLiquidacion.Rows(0).Item("EstadoRecuperacion").ToString.Trim
                'txtMotivo.Value = dtPagoCuotas.Rows(0).Item("MotivoAnulacionExtorno").ToString.Trim

                'Cargo en Cuenta
                GCCUtilitario.SeleccionaCombo(cmbCodMonedaCargo, dtLiquidacion.Rows(0).Item("CodMonedaCargo").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoCuenta, dtLiquidacion.Rows(0).Item("TipoCuenta").ToString.Trim)

                Dim strNroCuenta As String = dtLiquidacion.Rows(0).Item("NroCuenta").ToString.Trim
                cmbNroCuenta.Items.Add(New ListItem(GCCUtilitario.formateaNroCuenta(strNroCuenta), strNroCuenta))
                cmbNroCuenta.Value = strNroCuenta

                'Identificacion por Ventanilla
                'txtCodOperacionGINA.Value = dtPagoCuotas.Rows(0).Item("CodOperacionGINA").ToString.Trim
                'txtFechaProcesoPago.Value = GCCUtilitario.CheckDateString(dtPagoCuotas.Rows(0).Item("FechaProcesoPago").ToString.Trim, "C")
                'txtCodTerminalPago.Value = dtPagoCuotas.Rows(0).Item("CodTerminalPago").ToString.Trim
                'GCCUtilitario.SeleccionaCombo(cmbTiendaPago, dtPagoCuotas.Rows(0).Item("CodTiendaPago").ToString.Trim)
                'txtCodUsuarioPago.Value = dtPagoCuotas.Rows(0).Item("CodUsuarioPago").ToString.Trim
                'txtCodModoPago.Value = dtPagoCuotas.Rows(0).Item("CodModoPago").ToString.Trim
                'txtCodModoPago2.Value = dtPagoCuotas.Rows(0).Item("CodModoPago2").ToString.Trim

                'Flag Cuenta Propia
                hddFlagCuentaPropia.Value = dtLiquidacion.Rows(0).Item("FlagCuentaPropia").ToString
                If hddFlagCuentaPropia.Value = "" Then
                    hddFlagCuentaPropia.Value = "S"
                ElseIf hddFlagCuentaPropia.Value = "N" Then
                    txtCUClienteCargo.Value = dtLiquidacion.Rows(0).Item("CodUnicoClienteCargo").ToString.Trim
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    <WebMethod()> _
    Public Shared Function ObtenerPagoCuotasTotales(ByVal pstrCodSolicitudCredito As String, ByVal pstrFechaValor As String) As EGCC_PagoCuotas
        'Variables
        Dim objPagosNTx As New LPagosNTx

        Try

            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodSolicitudCredito)
                .TipoRubroFinanciamiento = "000"
                .CodIfi = "9999"
                .TipoRecuperacion = "R"
                .FechaValorRecuperacion = CDate(pstrFechaValor)
                .CodUsuario = GCCSession.CodigoUsuario
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtTotales As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerPagoCuotasTotales(strECreditoRecuperacion))

            'Inicializa Objeto
            Dim objEPagoCuotas As New EGCC_PagoCuotas
            If dtTotales.Rows.Count <> 1 Then
                objEPagoCuotas.CodError = 1
                objEPagoCuotas.MsgError = "Error al obtener totales del nro de contrato."
                Return objEPagoCuotas
            End If

            With objEPagoCuotas
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodSolicitudCredito)
                .NroCuotasxPagar = dtTotales.Rows(0).Item("NroCuotasxPagar")
                .NroCuotasVencidas = dtTotales.Rows(0).Item("NroCuotasVencidas")
                .NroPagosCuotasxProcesar = dtTotales.Rows(0).Item("NroPagosCuotasxProcesar")
                .NroConceptoPendiente = dtTotales.Rows(0).Item("NroPagosCuotasxProcesar")
                .MontoDesembolsado = dtTotales.Rows(0).Item("MontoDesembolsado")
                .MontoRecuperado = dtTotales.Rows(0).Item("MontoRecuperado")
            End With

            objEPagoCuotas.CodError = 0
            Return objEPagoCuotas

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function ObtenerLiquidacion(ByVal pstrCodigoLiquidacion As String, _
                                              ByVal pstrCodSolicitudCredito As String, _
                                              ByVal pstrTipoLiquidacion As String, _
                                              ByVal pstrCodMoneda As String, _
                                              ByVal pstrFechaValor As String, _
                                              ByVal pstrFechaProceso As String, _
                                              ByVal pstrCodTipoCambio As String, _
                                              ByVal pstrTipoCambio As String, _
                                              ByVal pstrTea As String, _
                                              ByVal pstrAmortizacionCapital As String, _
                                              ByVal pstrAmortizacionSeguro As String, _
                                              ByVal pstrTipoCronograma As String, _
                                              ByVal pstrNroCuotas As String, _
                                              ByVal pstrPeriodicidad As String, _
                                              ByVal pstrFrecuenciaPago As String, _
                                              ByVal pstrPlazoGracia As String, _
                                              ByVal pstrTipoGracia As String, _
                                              ByVal pstrFechavence As String, _
                                              ByVal pstrFlagOperacion As String) As JQGridJsonResponse()
        'Variables
        Dim objPagosTx As New LPagosTx

        Try

            'Inicializa Objeto
            Dim objECreditoRecuperacion As New EGCC_Liquidacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodigoLiquidacion = GCCUtilitario.NullableString(pstrCodigoLiquidacion)
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodSolicitudCredito)
                .TipoLiquidacion = pstrTipoLiquidacion
                .FechaValor = pstrFechaValor
                .FechaProceso = pstrFechaProceso
                .CodTipoCambio = pstrCodTipoCambio
                .TipoCambio = Val(pstrTipoCambio)
                .CodUsuario = GCCSession.CodigoUsuario
                .FlagOperacion = pstrFlagOperacion
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtDetalleLiquidacion As DataSet = GCCUtilitario.DeserializeObject(Of DataSet)(objPagosTx.ProcesarLiquidacion(strECreditoRecuperacion))


            Dim i As Integer

            If pstrTipoLiquidacion = "R" And (pstrFlagOperacion = "L" Or pstrFlagOperacion = "S") Then

                Dim saldoCapitalPendiente As Decimal = Val(dtDetalleLiquidacion.Tables(6).Rows(0).Item("SaldoCapitalPendiente").ToString)
                Dim interesCorrido As Decimal = Val(dtDetalleLiquidacion.Tables(6).Rows(0).Item("InteresCorrido").ToString)
                Dim interesCorridoSeguro As Decimal = Val(dtDetalleLiquidacion.Tables(6).Rows(0).Item("interesCorridoSeguro").ToString)

                Dim objInput As New lpcCronograma.QuotationInput
                Dim objQry As New lpcCronograma.clsCronograma
                Dim objTbl As New DataTable

                With objECreditoRecuperacion

                    .CodMoneda = GCCUtilitario.NullableString(pstrCodMoneda)

                    '.Precioventa = GCCUtilitario.StringToDecimal(pstrPrecioVenta)
                    '.Valorventaigv = GCCUtilitario.StringToDecimal(pstrMontoIGV)
                    .SaldoCapitalPendiente = saldoCapitalPendiente
                    '.Importecuotainicial = GCCUtilitario.StringToDecimal(pstrCuotaInicial)
                    '.Cuotainicialporc = GCCUtilitario.StringToDecimal(pstrCuotaInicialPorc)
                    '.Riesgoneto = GCCUtilitario.StringToDecimal(pstrRiesgoNeto)

                    .Codigotipocronograma = GCCUtilitario.NullableString(pstrTipoCronograma)
                    .Numerocuotas = GCCUtilitario.StringToInteger(pstrNroCuotas)
                    .Codigoperiodicidad = GCCUtilitario.NullableString(pstrPeriodicidad)
                    .Codigofrecuenciapago = GCCUtilitario.NullableString(pstrFrecuenciaPago)
                    .Plazograciacuota = GCCUtilitario.StringToInteger(pstrPlazoGracia)
                    .Codigotipograciacuota = GCCUtilitario.NullableString(pstrTipoGracia)
                    .FechaPrimerVencimiento = GCCUtilitario.StringToDateTime(pstrFechavence)
                    .Teaporc = GCCUtilitario.StringToDecimal(pstrTea)

                    .AmortizacionCapital = Val(pstrAmortizacionCapital)
                    .AmortizacionSeguro = Val(pstrAmortizacionSeguro)

                    .InteresCorrido = interesCorrido
                    .InteresCorridoSeguro = interesCorridoSeguro

                    '.Codigobientiposeguro = GCCUtilitario.NullableString(pstrTipoBienSeguro)
                    '.Bienimporteprima = 0 'GCCUtilitario.StringToDecimal(pstrImportePrimaSeguroBien)
                    '.Biennrocuotasfinanciar = 0 'GCCUtilitario.StringToInteger(pstrNumCuotasfinanciadas)

                    '.Fechaingreso = GCCUtilitario.StringToDateTime(pstrFechaIngreso)
                    '.FechaOfertaValida = GCCUtilitario.StringToDateTime(pstrFechaOfertaValida)
                    '.Periododisponible = GCCUtilitario.StringToInteger(pstrPeriodoDisponibilidad)
                    '.Fechamaxactivacion = GCCUtilitario.StringToDateTime(pstrFechaMaxActivacion)

                    '.Audestadologico = 1
                    '.Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    '.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                End With

                Dim numCuotasAnteriores As Integer = dtDetalleLiquidacion.Tables(5).Rows.Count

                If fGenerarQuotationInput(objInput, objECreditoRecuperacion) Then
                    objTbl = objQry.fGenerateQuoteDs(objInput)

                    'Setea en Entidad
                    PreparaCronograma(dtDetalleLiquidacion.Tables(5), objTbl, objECreditoRecuperacion, True)
                End If

                'Setea en Sesion
                'HttpContext.Current.Session("DTB_CRONOGRAMA") = objListECronograma

                For i = numCuotasAnteriores To dtDetalleLiquidacion.Tables(5).Rows.Count - 1
                    Dim objCuota As New EGcc_cotizacioncronograma
                    objCuota.Numerocuota = dtDetalleLiquidacion.Tables(5).Rows(i).Item("NumCuotaCalendario")
                    objCuota.Codigocotizacion = pstrCodigoLiquidacion
                    Dim strFechaVencimiento As String = dtDetalleLiquidacion.Tables(5).Rows(i).Item("FechaVencimientoCuota").ToString
                    objCuota.SFechavencimiento = strFechaVencimiento
                    objCuota.Cantdiascuota = dtDetalleLiquidacion.Tables(5).Rows(i).Item("CantDiasCuota")
                    objCuota.Montosaldoadeudado = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoSaldoAdeudado")
                    objCuota.Montointeresbien = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoInteres")
                    objCuota.Montoprincipalbien = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoPrincipal")
                    objCuota.Montototalcuota = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoTotalPago")
                    objCuota.Saldoseguro = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoSaldoSeguro")
                    objCuota.Principalsegurobien = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoPrincipalSeguro")
                    objCuota.Interessegurobien = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoInteresSeguro")
                    objCuota.Montocuotasegurobien = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoTotalSeguro")
                    objCuota.SaldoSeguroDes = 0
                    objCuota.PrincipalSeguroDes = 0
                    objCuota.CuotaSeguroDes = 0
                    objCuota.Montototalcuotaigv = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoTotalIGV")
                    objCuota.Totalapagar = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoTotalPagar")
                    objCuota.Montocuotasegurobien = dtDetalleLiquidacion.Tables(5).Rows(i).Item("MontoTotalSeguro")
                    objCuota.Audusuarioregistro = GCCSession.CodigoUsuario
                    objCuota.Audusuariomodificacion = pstrCodSolicitudCredito

                    objPagosTx.TMPInsertarCronograma(GCCUtilitario.SerializeObject(objCuota))
                Next
            End If

            'Resultado
            Dim listado(dtDetalleLiquidacion.Tables.Count) As JQGridJsonResponse

            For i = 0 To dtDetalleLiquidacion.Tables.Count - 1
                Dim dtTabla As DataTable = dtDetalleLiquidacion.Tables(i)
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                listado(i) = objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, dtTabla.Rows.Count, dtTabla)
            Next
            Return listado

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function GrabarLiquidacion(ByVal pstrCodOperacionActiva As String, _
                                               ByVal pstrCodigoLiquidacion As String, _
                                               ByVal pstrTipoLiquidacion As String, _
                                               ByVal pstrFechaValor As String, _
                                               ByVal pstrFechaProceso As String, _
                                               ByVal pstrCodTipoCambio As String, _
                                               ByVal pstrTipoCambio As String, _
                                               ByVal pstrPorcIGV As String, _
                                               ByVal pstrTipoCronograma As String, _
                                               ByVal pstrNroCuotas As String, _
                                               ByVal pstrPeriodicidad As String, _
                                               ByVal pstrFrecuenciaPago As String, _
                                               ByVal pstrPlazoGracia As String, _
                                               ByVal pstrTipoGracia As String, _
                                               ByVal pstrFechaPrimerVencimiento As String, _
                                               ByVal pstrAmortizacionCapital As String, _
                                               ByVal pstrValorNeto As String, _
                                               ByVal pstrMontoIGV As String, _
                                               ByVal pstrMontoTotal As String, _
                                               ByVal pstrFlagAdenda As Boolean, _
                                               ByVal pstrTipoViaCobranza As String, _
                                               ByVal pstrFlagCuentaPropia As String, _
                                               ByVal pstrCodUnicoClienteCargo As String, _
                                               ByVal pstrCodMonedaCargo As String, _
                                               ByVal pstrTipoCuenta As String, _
                                               ByVal pstrNroCuenta As String) As EGCC_Liquidacion
        'Variables
        Dim objPagosTx As New LPagosTx

        Try

            'Inicializa Objeto
            Dim objECreditoRecuperacion As New EGCC_Liquidacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodOperacionActiva)
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .TipoLiquidacion = pstrTipoLiquidacion
                .FechaValor = pstrFechaValor
                .FechaProceso = pstrFechaProceso
                .CodTipoCambio = pstrCodTipoCambio
                .TipoCambio = Val(pstrTipoCambio)

                .PorcIGV = pstrPorcIGV
                .TipoCronograma = pstrTipoCronograma
                .NroCuotas = Val(pstrNroCuotas)
                .Periodicidad = pstrPeriodicidad
                .FrecuenciaPago = pstrFrecuenciaPago
                .PlazoGracia = Val(pstrPlazoGracia)
                .TipoGracia = pstrTipoGracia
                .FechaPrimerVencimiento = Convert.ToDateTime(pstrFechaPrimerVencimiento)
                .AmortizacionCapital = Val(pstrAmortizacionCapital)

                .ValorNeto = Val(pstrValorNeto)
                .MontoIGV = Val(pstrMontoIGV)
                .MontoTotal = Val(pstrMontoTotal)
                .CodUsuario = GCCSession.CodigoUsuario

                .FlagAdenda = IIf(pstrFlagAdenda, "S", "N")
                .TipoViaCobranza = pstrTipoViaCobranza
                .FlagCuentaPropia = pstrFlagCuentaPropia
                .CodUnicoClienteCargo = pstrCodUnicoClienteCargo
                .CodMonedaCargo = pstrCodMonedaCargo
                .TipoCuenta = pstrTipoCuenta
                .NroCuenta = pstrNroCuenta

                .FlagOperacion = "G"
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtDetalleLiquidacion As DataTable
            dtDetalleLiquidacion = GCCUtilitario.DeserializeObject(Of DataSet)(objPagosTx.ProcesarLiquidacion(strECreditoRecuperacion)).Tables(0)

            If dtDetalleLiquidacion.Rows.Count = 1 Then
                objECreditoRecuperacion.CodError = 0
                objECreditoRecuperacion.CodigoLiquidacion = dtDetalleLiquidacion.Rows(0).Item("CodigoLiquidacion").ToString()
            Else
                objECreditoRecuperacion.CodError = 1
                objECreditoRecuperacion.MsgError = "Error al ingresar la liquidacion"
            End If

            Return objECreditoRecuperacion

        Catch ex As Exception
            Return Nothing
        Finally
            objPagosTx = Nothing
        End Try

    End Function

    <WebMethod()> _
   Public Shared Function ObtenerCuotaAtrasadaComision(ByVal pstrCodOperacionActiva As String, _
                                                       ByVal pstrCodigoLiquidacion As String, _
                                                       ByVal pstrNumCuotaCalendario As String _
                                                   ) As JQGridJsonResponse
        'Variables
        Dim objPagosNTx As New LPagosNTx

        Try

            'Inicializa Objeto
            Dim objECreditoRecuperacion As New EGCC_Liquidacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodOperacionActiva)
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .NumCuotaCalendario = Val(pstrNumCuotaCalendario)
                .CodUsuario = GCCSession.CodigoUsuario
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtDetalleCuotas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerCuotaAtrasadaComision(strECreditoRecuperacion))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, dtDetalleCuotas.Rows.Count, dtDetalleCuotas)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function ObtenerTipoCambio(ByVal strCodMoneda As String, _
                                               ByVal strFecha As String, _
                                               ByVal strTipoModalidadCambio As String) As EMonedatipocambio
        Dim oLwsTipoCambioNtx As New LUtilNTX
        Dim oEMonedaTipoCambio As New EMonedatipocambio

        Dim odtbDatos As New DataTable

        Try
            odtbDatos = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTipoCambioNtx.ObtenerTipoCambio(strCodMoneda, strTipoModalidadCambio, CDate(strFecha).ToString("yyyyMMdd")))

            If odtbDatos.Rows.Count = 0 Then
                oEMonedaTipoCambio.CodError = "1"
                oEMonedaTipoCambio.Montovalorventa = 0
                oEMonedaTipoCambio.Montovalorcompra = 0
            Else
                oEMonedaTipoCambio.CodError = "0"
                oEMonedaTipoCambio.Montovalorventa = odtbDatos.Rows(0).Item("MontoValorVenta").ToString
                oEMonedaTipoCambio.Montovalorcompra = odtbDatos.Rows(0).Item("MontoValorCompra").ToString
            End If

            Return oEMonedaTipoCambio
        Catch ex As Exception
            oEMonedaTipoCambio.CodError = "1"
            Return oEMonedaTipoCambio
        Finally
            'oLwsTipoCambioNtx = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Listado de las n Siguientes Cuotas
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 31/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ObtenerProximasCuotas(ByVal pstrCodSolicitudCredito As String, _
                                                 ByVal pstrFechaValor As String, _
                                                 ByVal pstrNroCuotas As String) As JQGridJsonResponse
        'Variables
        Dim objPagosNTx As New LPagosNTx

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodSolicitudCredito)
                .TipoRubroFinanciamiento = "000"
                .CodIfi = "9999"
                .TipoRecuperacion = "R"
                .FechaValorRecuperacion = CDate(pstrFechaValor)
                .CodUsuario = GCCSession.CodigoUsuario.ToString
            End With
            Dim strECreditoRecuperacion As String = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtProximasCuotas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerProximasCuotas(strECreditoRecuperacion, Integer.Parse(pstrNroCuotas)))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, dtProximasCuotas.Rows.Count, dtProximasCuotas)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Listado de Comisiones del Pago
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 27/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ObtenerDetalleComisiones(ByVal pstrCodSolicitudCredito As String, _
                                                       ByVal pstrNumSecRecuperacion As String _
                                                       ) As JQGridJsonResponse
        'Variables
        Dim objPagosNTx As New LPagosNTx

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodSolicitudCredito)
                .TipoRubroFinanciamiento = "000"
                .CodIfi = "9999"
                .TipoRecuperacion = "R"
                .NumSecRecuperacion = GCCUtilitario.StringToInteger(pstrNumSecRecuperacion)
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtDetalleComisiones As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerDetalleComisiones(strECreditoRecuperacion))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, dtDetalleComisiones.Rows.Count, dtDetalleComisiones)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Listado de Comisiones del Pago
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 03/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ObtenerProximasComisiones(ByVal pstrCodSolicitudCredito As String) As JQGridJsonResponse
        'Variables
        Dim objPagosNTx As New LPagosNTx

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodSolicitudCredito)
                .CodUsuario = GCCSession.CodigoUsuario.ToString
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ejecuta Consulta
            Dim dtDetalleComisiones As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerProximasComisiones(strECreditoRecuperacion))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, dtDetalleComisiones.Rows.Count, dtDetalleComisiones)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

    <WebMethod()> _
    Public Shared Function AnularLiquidacion(ByVal pstrCodigoLiquidacion As String, _
                                             ByVal pstrMotivoAnulacionExtorno As String) As EGCC_Liquidacion

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New EGCC_Liquidacion

            With objECreditoRecuperacion
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .MotivoAnulacionExtorno = pstrMotivoAnulacionExtorno
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            Dim strECreditoRecuperacion As String

            'Cambiar estado a Anulado
            objECreditoRecuperacion.EstadoLiquidacion = "A"
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)
            objPagosTx.ActualizarEstadoLiquidacion(strECreditoRecuperacion)
            objECreditoRecuperacion.CodError = 0
            Return objECreditoRecuperacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function DevolverLiquidacion(ByVal pstrCodigoLiquidacion As String, _
                                               ByVal pstrMotivoAnulacionExtorno As String) As EGCC_Liquidacion

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New EGCC_Liquidacion

            With objECreditoRecuperacion
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .MotivoAnulacionExtorno = pstrMotivoAnulacionExtorno
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            Dim strECreditoRecuperacion As String

            'Cambiar estado a Anulado
            objECreditoRecuperacion.EstadoLiquidacion = "I"
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)
            objPagosTx.ActualizarEstadoLiquidacion(strECreditoRecuperacion)
            objECreditoRecuperacion.CodError = 0
            Return objECreditoRecuperacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function ExtornarLiquidacion(ByVal pstrCodigoLiquidacion As String, _
                                               ByVal pstrMotivoAnulacionExtorno As String) As EGCC_Liquidacion

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objEGCC_Liquidacion As New EGCC_Liquidacion

            With objEGCC_Liquidacion
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .MotivoAnulacionExtorno = pstrMotivoAnulacionExtorno
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            Dim strEGCC_Liquidacion As String = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)

            'Ejecuta Consulta
            Dim dtLiquidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerLiquidacion(strEGCC_Liquidacion))

            'Carga en Cuenta
            If (dtLiquidacion.Rows(0).Item("TipoViaCobranza").ToString = "C") Then
                Dim pstrRetorno As String = ""
                CargaEnCuentas(pstrCodigoLiquidacion, "S", pstrRetorno)
            End If

            'Ejecutar Liquidacion
            objEGCC_Liquidacion.EstadoLiquidacion = "E"
            strEGCC_Liquidacion = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)
            objPagosTx.ActualizarEstadoLiquidacion(strEGCC_Liquidacion)
            objEGCC_Liquidacion.CodError = 0
            Return objEGCC_Liquidacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function EnviarLiquidacion(ByVal pstrCodigoLiquidacion As String) As EGCC_Liquidacion

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New EGCC_Liquidacion

            With objECreditoRecuperacion
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            Dim strECreditoRecuperacion As String

            'Cambiar estado a Anulado
            objECreditoRecuperacion.EstadoLiquidacion = "C"
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)
            objPagosTx.ActualizarEstadoLiquidacion(strECreditoRecuperacion)
            objECreditoRecuperacion.CodError = 0
            Return objECreditoRecuperacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function EjecutarLiquidacion(ByVal pstrCodigoLiquidacion As String) As EGCC_Liquidacion

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objEGCC_Liquidacion As New EGCC_Liquidacion

            With objEGCC_Liquidacion
                .CodigoLiquidacion = pstrCodigoLiquidacion
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            Dim strEGCC_Liquidacion As String = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)

            'Ejecuta Consulta
            Dim dtLiquidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerLiquidacion(strEGCC_Liquidacion))

            'Carga en Cuenta
            If (dtLiquidacion.Rows(0).Item("TipoViaCobranza").ToString = "C") Then
                Dim pstrRetorno As String = ""
                CargaEnCuentas(pstrCodigoLiquidacion, "N", pstrRetorno)
            End If

            'Ejecutar Liquidacion
            objEGCC_Liquidacion.EstadoLiquidacion = "H"
            strEGCC_Liquidacion = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)
            objPagosTx.ActualizarEstadoLiquidacion(strEGCC_Liquidacion)
            objEGCC_Liquidacion.CodError = 0
            Return objEGCC_Liquidacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function ExtornarPagoCuotas(ByVal pstrCodOperacionActiva As String, _
                                                ByVal pstrTipoRecuperacion As String, _
                                                ByVal pstrNumSecRecuperacion As String, _
                                                ByVal pstrCodAutorizacionRecuperacion As String, _
                                                ByVal pstrEstadoRecuperacion As String, _
                                                ByVal pstrFechaRecuperacion As String, _
                                                ByVal pstrTipoViaCobranza As String, _
                                                ByVal pstrFlagCuentaPropia As String, _
                                                ByVal pstrCodUnicoClienteCargo As String, _
                                                ByVal pstrCodMonedaCargo As String, _
                                                ByVal pstrTipoCuenta As String, _
                                                ByVal pstrNroCuenta As String) As ECreditoRecuperacion


        If pstrFlagCuentaPropia <> "N" Then pstrFlagCuentaPropia = "S"
        If pstrFlagCuentaPropia = "S" Then pstrCodUnicoClienteCargo = ""

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion

            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodOperacionActiva)
                .TipoRubroFinanciamiento = "000"
                .CodIfi = "9999"
                .TipoRecuperacion = pstrTipoRecuperacion
                .NumSecRecuperacion = pstrNumSecRecuperacion
                .CodAutorizacionRecuperacion = pstrCodAutorizacionRecuperacion
                .CodUsuario = GCCSession.CodigoUsuario()
            End With

            'Fecha de cierre para batch
            Dim odtbFechaCierre As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ObtenerFechaCierre("CO"))
            Dim strFechaHoy As String = odtbFechaCierre.Rows(0).Item("FechaHoy").ToString()

            'Validar fecha del pago
            'pstrFechaRecuperacion = CDate(pstrFechaRecuperacion).ToString("yyyyMMdd")
            'If pstrFechaRecuperacion < strFechaHoy Then
            'objECreditoRecuperacion.CodError = 1
            'objECreditoRecuperacion.MsgError = "No es posible anular un pago ya procesado. No se realizó ningún cambio."
            'Return objECreditoRecuperacion
            'End If

            Dim strECreditoRecuperacion As String = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Si es pago en ventanilla con estado C (enviado a Host) enviar Trama de anulacion a Transactor
            If pstrTipoViaCobranza = "V" And pstrEstadoRecuperacion = "C" Then

                Dim strTramaTransactor As String = objPagosNTx.ObtenerTramaAutorizacionPagosVentanilla(strECreditoRecuperacion, "D")
                Dim strTramaRespuesta As String = ""
                Dim bResultado As Boolean = objPagosTx.TransaccionGINA(strTramaTransactor, strTramaRespuesta)

                If bResultado = False Then
                    objECreditoRecuperacion.CodError = 1
                    objECreditoRecuperacion.MsgError = strTramaRespuesta
                    Return objECreditoRecuperacion
                End If
            End If

            'Cambiar estado a Extornado
            objECreditoRecuperacion.EstadoRecuperacion = "E"
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)
            objPagosTx.ActualizarEstadoPagoCuotas(strECreditoRecuperacion)
            objECreditoRecuperacion.CodError = 0
            Return objECreditoRecuperacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        Dim objContratoNTx As New LContratoNTx
        Dim TableCuerpo As DataTable
        Dim TableHeader As DataTable
        Dim TableCronograma As DataTable
        Dim DSResumen As DataSet
        Dim DSDetalle As DataSet
        Try
            TableCuerpo = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcel(txtNroContrato.Value, Now.ToShortDateString.ToString()))
            TableHeader = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelHeader(txtNroContrato.Value))
            TableCronograma = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarDatosCronogramaPostSituacionCreditoExcel(txtNroContrato.Value, GCCSession.CodigoUsuario))
            DSResumen = GCCUtilitario.DeserializeObject(Of DataSet)(objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelResumen(txtNroContrato.Value, GCCSession.CodigoUsuario))
            DSDetalle = GCCUtilitario.DeserializeObject(Of DataSet)(objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelDetalle(txtNroContrato.Value, GCCSession.CodigoUsuario))
            Exportar(TableCuerpo, TableHeader, DSResumen, DSDetalle, TableCronograma)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    <WebMethod()> _
  Public Shared Function ActualizarTMPLiquidacion(ByVal pstrValorNeto As String, _
                                              ByVal pstrValorIGV As String, _
                                              ByVal pstrCodOperacionActiva As String, _
                                              ByVal pstrNumSecRecuperacion As String, _
                                              ByVal pstrTipoRecuperacion As String, _
                                              ByVal pstrCodUsuario As String, _
                                              ByVal pstrAplicacion As String) As String

        'Variables
        Dim objPagosTx As New LPagosTx
        Try


            'Inicializa Objeto
            Dim objEGCC_Liquidacion As New EGCC_Liquidacion
            Dim strEGCC_Liquidacion As String
            With objEGCC_Liquidacion
                .ValorNeto = pstrValorNeto
                .MontoIGV = pstrValorIGV
                .CodOperacionActiva = pstrCodOperacionActiva
                .NumSecRecuperacion = pstrNumSecRecuperacion
                .TipoRecuperacion = pstrTipoRecuperacion
                .CodUsuario = GCCSession.CodigoUsuario
                .Aplicacion = pstrAplicacion
            End With
            strEGCC_Liquidacion = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)
            Dim blnRet As String
            blnRet = objPagosTx.ActualizarTMPLiquidacion(strEGCC_Liquidacion)
            Return blnRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    <WebMethod()> _
 Public Shared Function ActualizarTMPLiquidacionAplicacion(ByVal pstrCodOperacionActiva As String, _
                                             ByVal pstrNumSecRecuperacion As String, _
                                             ByVal pstrTipoRecuperacion As String, _
                                             ByVal pstrCodUsuario As String, _
                                             ByVal pstrAplicacion As String) As String

        'Variables
        Dim objPagosTx As New LPagosTx
        Try


            'Inicializa Objeto
            Dim objEGCC_Liquidacion As New EGCC_Liquidacion
            Dim strEGCC_Liquidacion As String
            With objEGCC_Liquidacion
                .CodOperacionActiva = pstrCodOperacionActiva
                .NumSecRecuperacion = pstrNumSecRecuperacion
                .TipoRecuperacion = pstrTipoRecuperacion
                .CodUsuario = GCCSession.CodigoUsuario
                .Aplicacion = pstrAplicacion
            End With
            strEGCC_Liquidacion = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)
            Dim blnRet As String
            blnRet = objPagosTx.ActualizarTMPLiquidacionAplicacion(strEGCC_Liquidacion)
            Return blnRet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#Region "Ejecucion Pago Cuotas"

    <WebMethod()> _
    Public Shared Function fObtenerCuentasXCo_unico(ByVal argCo_usuario As String, _
                                            ByVal argCo_unico As String, _
                                            ByVal argTi_cuenta As GCCConstante.eTipoCuenta, _
                                            ByVal argMarca As String) As String
        Dim objSRMR047 As Object
        Dim refOut As String = ""
        Dim intRes As Integer
        Try
            objSRMR047 = CreateObject("bseSRMR047.SRMR047")
            With objSRMR047
                .Marca = argMarca
                .CUserId = argCo_usuario.ToLower
                .Cod_unico = argCo_unico
                .Tipo_cuenta = CType(argTi_cuenta, String)
                intRes = .fEjecutarSRMR047(refOut)

                If intRes = 0 Then
                    Return refOut
                Else
                    Throw New Exception("Error de comunicación con el Host " & CStr(intRes))
                End If

            End With

        Catch ex As Exception
            Throw ex
        Finally
            objSRMR047 = Nothing
        End Try
    End Function


    <WebMethod()> _
    Public Shared Function ObtenerCuentas(ByVal pstrCUCliente As String, _
                                   ByVal pstrCodMonedaCargo As String, _
                                   ByVal pstrTipoCuenta As String) As String()

        Dim argsUsuarioTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("USER_TLD")

        Dim strrta As String = ""

        If Val(pstrTipoCuenta) = GCCConstante.eTipoCuenta.Cuenta_corriente Then
            Return saldoCuenta(pstrCUCliente, "IM", pstrCodMonedaCargo, strrta)
        Else
            Return saldoCuenta(pstrCUCliente, "ST", pstrCodMonedaCargo, strrta)
        End If

    End Function


    ''' <summary>
    ''' Ejecuta Pago de Cuotas 
    ''' </summary>
    <WebMethod()> _
    Public Shared Function EjecutaPagoCuotas(ByVal pstrCodOperacionActiva As String, _
                                                ByVal pstrFechaValorRecuperacion As String, _
                                                ByVal pstrTipoViaCobranza As String, _
                                                ByVal pstrFlagCuentaPropia As String, _
                                                ByVal pstrCodUnicoClienteCargo As String, _
                                                ByVal pstrCodMonedaCargo As String, _
                                                ByVal pstrTipoCuenta As String, _
                                                ByVal pstrNroCuenta As String, _
                                                ByVal pstrCodigoMovimientoBasilea As String) As ECreditoRecuperacion

        If pstrFlagCuentaPropia <> "N" Then pstrFlagCuentaPropia = "S"
        If pstrFlagCuentaPropia = "S" Then pstrCodUnicoClienteCargo = ""

        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion
            Dim strECreditoRecuperacion As String
            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodOperacionActiva)
                .TipoRubroFinanciamiento = "000"
                .CodIfi = "9999"
                .TipoRecuperacion = "R"

                .CodUsuario = GCCSession.CodigoUsuario()
                .FechaValorRecuperacion = CDate(pstrFechaValorRecuperacion)
                .TipoViaCobranza = pstrTipoViaCobranza

                .CodMonedaCargo = pstrCodMonedaCargo
                .TipoCuenta = pstrTipoCuenta
                .NroCuenta = pstrNroCuenta

                .EstadoRecuperacion = "I"
                .ConceptoAdministrativo = ""
                .CodigoMovimientoBasilea = pstrCodigoMovimientoBasilea
                .CodUnicoClienteCargo = pstrCodUnicoClienteCargo
                .FlagCuentaPropia = pstrFlagCuentaPropia

                .TipoExtorno = ""
                .TipoPrepago = ""
                .TipoAplicacionPrepago = ""
                .TipoPrelacion = "N"
            End With
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Ingresa Pago de Cuotas
            Dim dtRetornoIngresoPago As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosTx.IngresarPagoCuotas(strECreditoRecuperacion))

            If dtRetornoIngresoPago.Rows.Count <> 1 Then
                objECreditoRecuperacion.CodError = 1
                objECreditoRecuperacion.MsgError = "Error al ingresar el pago de cuotas."
                Return objECreditoRecuperacion
            End If

            objECreditoRecuperacion.NumSecRecuperacion = dtRetornoIngresoPago.Rows(0).Item("NumSecRecuperacion")
            objECreditoRecuperacion.CodAutorizacionRecuperacion = dtRetornoIngresoPago.Rows(0).Item("CodAutorizacionRecuperacion").ToString.Trim
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            If pstrTipoViaCobranza = "V" Then
                Dim strTramaTransactor As String = objPagosNTx.ObtenerTramaAutorizacionPagosVentanilla(strECreditoRecuperacion, "A")
                Dim strTramaRespuesta As String = ""
                Dim bResultado As Boolean = objPagosTx.TransaccionGINA(strTramaTransactor, strTramaRespuesta)

                If bResultado = False Then
                    objECreditoRecuperacion.CodError = 1
                    objECreditoRecuperacion.MsgError = strTramaRespuesta
                    Return objECreditoRecuperacion
                End If
            End If

            'Cambiar estado a enviado a Host
            objECreditoRecuperacion.EstadoRecuperacion = "C"
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)
            objPagosTx.ActualizarEstadoPagoCuotas(strECreditoRecuperacion)

            objECreditoRecuperacion.CodError = 0
            Return objECreditoRecuperacion

        Catch ex As Exception
            Return Nothing
        End Try


        'Dim str As String
        'Dim tx As New LInstruccionDesembolsoNTx
        'Dim table As New DataTable
        'Dim strRta As String = ""
        'Dim strRetorno As String = ""
        'Dim numEstado As Integer = 0
        'Dim strFlagLPC As String = "0"
        'Dim pFlag As String = "1"
        'Dim strRetFinWIO As String

        'Try
        '    Dim enumerator As IEnumerator
        '    Dim str10 As String = "Inicio a ejecutar el desembolso."
        '    table = GCCUtilitario.DeserializeObject(Of DataTable)(tx.getCargosCuentaInsDes(pstrNroContrato, pstrNroInstruccion))
        '    Dim flagTieneSaldo As Boolean = True
        '    Try
        '        enumerator = table.Rows.GetEnumerator
        '        Do While enumerator.MoveNext
        '            Dim strTipoCta As String
        '            Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
        '            Dim pstrCU As String = current.Item("CodUnico").ToString
        '            Dim pstrCodMoneda As String = current.Item("CodMonedaCuenta").ToString
        '            Dim strNroCta As String = current.Item("Numero_Cuenta").ToString
        '            Dim pstrCodTienda As String = strNroCta.Substring(0, 3)
        '            Dim pstrNroCta As String = strNroCta.Substring(3, 10)
        '            If (current.Item("CodTipoCuenta").ToString.Trim = "01") Then
        '                strTipoCta = "IM"
        '            Else
        '                strTipoCta = "ST"
        '            End If
        '            Dim numTotalAbonos As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoTotalAbonos").ToString)
        '            Dim numNetoCargo As Decimal = Decimal.Subtract(GCCUtilitario.CheckDecimal(current.Item("MontoTotalCargos").ToString), numTotalAbonos)
        '            Dim dblNetoCargo As Double = Convert.ToDouble(numNetoCargo)
        '            Dim numSaldo As Double = 0
        '            If (Decimal.Compare(numNetoCargo, Decimal.Zero) > 0) Then
        '                numSaldo = Pagos_frmLiquidacionesRegistro.saldoCuenta(pstrCU, strTipoCta, pstrCodMoneda, pstrCodTienda, pstrNroCta, strRta)
        '            End If
        '            If ((Decimal.Compare(numNetoCargo, Decimal.Zero) > 0) And (numSaldo < dblNetoCargo)) Then
        '                numEstado = 1
        '                flagTieneSaldo = False
        '                strRetorno = (pstrCodTienda & "-" & pstrNroCta)
        '            End If
        '            If (strRta <> "") Then
        '                numEstado = 1
        '                flagTieneSaldo = False
        '                strRetorno = strRta
        '            End If
        '        Loop
        '    Finally
        '        If TypeOf enumerator Is IDisposable Then
        '            TryCast(enumerator, IDisposable).Dispose()
        '        End If
        '    End Try
        '    If flagTieneSaldo Then
        '        Dim strError As String = ""
        '        numEstado = 2
        '        Pagos_frmLiquidacionesRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "009", strFlagLPC)
        '        numEstado = Pagos_frmLiquidacionesRegistro.CargaEnCuentas(pstrNroContrato, pstrNroInstruccion, strError)
        '        Select Case numEstado
        '            Case -1
        '                Return ("2|" & strError)
        '            Case -2
        '                str10 = ("1|" & strError)
        '                Pagos_frmLiquidacionesRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strFlagLPC)
        '                Return str10
        '            Case 3
        '                Dim numRetPC As Integer
        '                strFlagLPC = "1"
        '                If (pstrEnvioLPC <> "1") Then
        '                    numRetPC = Pagos_frmLiquidacionesRegistro.EjecutarDesembolsoLPC(pFlag, pstrNroInstruccion, GCCSession.CodigoUsuario)
        '                Else
        '                    numRetPC = 0
        '                End If
        '                If (numRetPC <> 0) Then
        '                    strFlagLPC = "0"
        '                    Pagos_frmLiquidacionesRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strFlagLPC)
        '                    Return "1|Error en LPC"
        '                End If
        '                strFlagLPC = "1"
        '                Pagos_frmLiquidacionesRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "006", strFlagLPC)
        '                'Finalizo WIO
        '                strRetFinWIO = FinalizaWIO(pstrNroContrato, pstrNroInstruccion)
        '                Dim strRetWio As String() = strRetFinWIO.Split("|")
        '                If strRetWio(0) = "0" Then
        '                    Return "0|Desembolso Ejecutado Corretamente"
        '                Else
        '                    Return strRetFinWIO
        '                End If
        '        End Select
        '        Pagos_frmLiquidacionesRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strFlagLPC)
        '        Return ("1|Error en ejecucion: " & numEstado.ToString & " - " & strError)
        '    End If 'flagTieneSaldo Then
        '    str = (numEstado.ToString & "|No hay saldo disponible en la cuenta de cargo " & strRetorno)
        'Catch exception1 As Exception
        '    str = (numEstado.ToString & "|No se pudo finalizar la Ejecución de Desembolso. (" & exception1.ToString & ")")
        '    Return str
        'End Try
        'Return str
    End Function

    ''' <summary>
    ''' CargaEnCuentas 
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Reingeniería de la función
    <WebMethod()> _
    Public Shared Function CargaEnCuentas(ByVal pstrCodigoLiquidacion As String, ByVal pstrExtorno As String, ByRef strError As String) As Integer
        Dim num As Integer
        Dim num2 As Integer = 2
        Dim str As String = ""
        Dim pstrTrama As String = ""
        Try
            Dim tx2 As New LInstruccionDesembolsoNTx
            Dim pstrUrlws As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsFCDDesembolso")
            Dim strUsoWS As String = GCCUtilitario.fstrObtieneKeyWebConfig("usoWS_Desembolso")
            Dim pstrCodTran As String = GCCUtilitario.fstrObtieneKeyWebConfig("COD_TRAN_FCDO")
            Dim argsUsuarioTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("USER_TLD")
            Dim argsAgenciaTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("AGENCIA_TLD")
            Dim flagError As Boolean = False
            Dim pstrExisteTCambio As String = ""
            Dim pstrMonedaDocumento As String = ""
            Dim strMonedaContrato As String = ""
            Dim pstrTipoCambio As String = ""
            Dim pstrImportePagar As String = ""

            'Inicializa Objeto
            Dim objPagosNTx As New LPagosNTx
            Dim objEGCC_Liquidacion As New EGCC_Liquidacion

            With objEGCC_Liquidacion
                .CodigoLiquidacion = pstrCodigoLiquidacion
            End With

            Dim strEGCC_Liquidacion As String = GCCUtilitario.SerializeObject(objEGCC_Liquidacion)

            'Ejecuta Consulta
            Dim dtLiquidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosNTx.ObtenerLiquidacion(strEGCC_Liquidacion))

            If ((Not dtLiquidacion Is Nothing) AndAlso (dtLiquidacion.Rows.Count > 0)) Then

                Dim current As DataRow = dtLiquidacion.Rows(0)

                Dim pstrNroContrato As String = current.Item("CodOperacionActiva").ToString
                Dim strEstadoLiquidacion As String = current.Item("EstadoLiquidacion").ToString

                Dim flagAbono As Boolean
                Dim strCodTipoOperacion As String = ""
                Dim strCodAgrupacion As String = ""
                'Si Liquidacion se va a ejecutar, se realiza un cargo en cuenta
                If (strEstadoLiquidacion = "C") Then
                    flagAbono = False
                    strCodTipoOperacion = "002"
                    strCodAgrupacion = "10"
                Else 'Si se va a extornar, se hace un abono
                    flagAbono = True
                    strCodTipoOperacion = "001"
                    strCodAgrupacion = "07"
                End If

                Dim nbrMontoAbono As Decimal = Convert.ToDecimal(current.Item("MontoTotal").ToString)

                Dim strCodMedioAbono As String = "002" ' Cuenta - TBL222

                Dim strCodMonedaPago As String
                Dim numMontoAbono As Decimal
                Dim numMontoIGV As Decimal
                Dim numMontoAbonoLog As Decimal
                Dim numMontoAbonoSAdel As Decimal

                Dim strCodMonedaDoc As String = current.Item("CodMoneda").ToString 'CodMonedaDocumento
                Dim codMonedaAgrupacion As String = current.Item("CodMoneda").ToString 'CodMonedaAgrupacion
                strMonedaContrato = current.Item("CodMoneda").ToString 'CodMonedaContrato
                Dim numTCSunat As Decimal = GCCUtilitario.CheckDecimal(current.Item("TipoCambio").ToString) 'TCSunat
                Dim nbrTCAgrp As Decimal = GCCUtilitario.CheckDecimal(current.Item("TipoCambio").ToString) 'TC
                Dim nbrTCPrefVta As Decimal = GCCUtilitario.CheckDecimal(current.Item("TipoCambio").ToString) 'TCPrefVtaDia
                Dim nbrTCPrefCmp As Decimal = GCCUtilitario.CheckDecimal(current.Item("TipoCambio").ToString) 'TCPrefCmpDia
                'Dim nbrMontoMonContrato As Decimal = GCCUtilitario.CheckDecimal(current.Item("ImporteMonContrato").ToString)
                'Dim nbrMontoAdelantoContrato As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoAdelantoMonContrato").ToString)
                Dim nbrMontoPago As Decimal = GCCUtilitario.CheckDecimal(current.Item("ValorNeto").ToString) 'MontoTotalPago
                Dim numMontoAdelanto As Decimal = 0 'GCCUtilitario.CheckDecimal(current.Item("MontoAdelanto").ToString)
                Dim numIGVCargo As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoIGV").ToString) 'ImporteIgv

                Dim blnMonedasCoiciden As Boolean = False

                'Inicio AAE - Chequeo que la agrupación NO tenga notas de credito
                Dim blnTieneNotasCred As Boolean = False

                'If (strCodMedioAbono = "002" Or strCodMedioAbono = "006") Then
                strCodMonedaPago = current.Item("CodMonedaCargo").ToString
                If strMonedaContrato = strCodMonedaPago And strCodMonedaPago = codMonedaAgrupacion Then
                    blnMonedasCoiciden = True
                Else
                    blnMonedasCoiciden = False
                End If
                'End If

                If (strCodMedioAbono = "002" Or strCodMedioAbono = "006") And blnMonedasCoiciden And (nbrMontoAbono <> 0) Then

                    Dim strCU As String
                    Dim strMensajeWIO As String
                    Dim strDatosWIO As String
                    Dim flagTengoCU As Boolean = True

                    strCU = current.Item("CodUnicoClienteCargo").ToString

                    'Si es Cargo
                    If (strCodTipoOperacion = "002") Then
                        flagAbono = False
                        If (codMonedaAgrupacion <> strCodMonedaPago) Then
                            numMontoIGV = Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numIGVCargo, flagAbono)
                            numMontoAbono = Decimal.Add(Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono), numIGVCargo)
                        Else
                            numMontoIGV = numIGVCargo
                            numMontoAbono = Decimal.Add(nbrMontoPago, numIGVCargo)
                        End If
                        numMontoAbonoLog = Decimal.Subtract(numMontoAbono, numMontoIGV)
                    Else ' (strCodTipoOperacion = "002") Then
                        flagAbono = True
                        If (strCodAgrupacion = "02") Then
                            numMontoAbonoSAdel = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                            If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                numMontoAbono = Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numMontoAbonoSAdel, flagAbono)
                            Else
                                numMontoAbono = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                            End If
                        End If
                        If ((strCodAgrupacion = "01") Or (strCodAgrupacion = "03") Or (strCodAgrupacion = "04") Or (strCodAgrupacion = "06")) Then
                            If ((codMonedaAgrupacion <> strCodMonedaPago) And (codMonedaAgrupacion = "002")) Then
                                numMontoAbono = Decimal.Multiply(nbrMontoPago, numTCSunat)
                            Else
                                numMontoAbono = Decimal.Add(nbrMontoPago, numIGVCargo)
                            End If
                        End If
                        If ((strCodAgrupacion = "05") Or (strCodAgrupacion = "07") Or (strCodAgrupacion = "13")) Then
                            If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                numMontoAbono = Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono)
                            Else
                                numMontoAbono = Decimal.Add(nbrMontoPago, numIGVCargo)
                            End If
                        End If
                        numMontoAbonoLog = numMontoAbono
                    End If ' (strCodTipoOperacion = "002") Then
                    If flagTengoCU Then
                        Dim strCodRetorno As String
                        Dim strResultado As String
                        Dim strResCarga As String
                        'Dim pstrNumeroInterno As String = ("00" & pstrNroInstruccion)
                        Dim pstrNumeroInterno As String = ("00" & pstrNroContrato)
                        Dim pintTipoCuenta As String = current.Item("TipoCuenta").ToString
                        Dim pintTipoMoneda As String = strCodMonedaPago
                        Dim pstrNroOficina As String = current.Item("NroCuenta").ToString
                        Dim pstrCodigoUnico As String = strCU
                        Dim pstrNroCuenta As String = current.Item("NroCuenta").ToString
                        Dim numAbono As Decimal = numMontoAbono
                        pstrNroOficina = pstrNroOficina.Substring(0, 3)
                        pstrNroCuenta = pstrNroCuenta.Substring(3, 10)
                        pstrExisteTCambio = ""
                        pstrMonedaDocumento = ""
                        pstrTipoCambio = ""
                        pstrImportePagar = ""
                        Dim strTrama As String = Pagos_frmLiquidacionesRegistro.pArmaTramaDesembolso(pstrNumeroInterno, pintTipoCuenta, pintTipoMoneda, pstrNroOficina, pstrCodigoUnico, pstrNroCuenta, numMontoAbono, pstrExisteTCambio, pstrMonedaDocumento, pstrTipoCambio, pstrImportePagar, strCodAgrupacion, pstrCodTran, pstrExtorno, pstrTrama)
                        If (strUsoWS = "SI") Then
                            strResCarga = tx2.fstrConsultarDesembolso(strTrama, pstrUrlws, strCodRetorno, strResultado)
                        Else
                            If tx2.callProgramaHost(strTrama, argsUsuarioTld, argsAgenciaTld, "FCDO04", "EjecutarDesembolso", strMensajeWIO, strDatosWIO) Then
                                If (strMensajeWIO.Substring(0, 2) = "00") Then
                                    strResCarga = "1|Se desembolso correctamente"
                                    strResultado = "Se desembolso correctamente"
                                    strCodRetorno = "00"
                                Else
                                    strResCarga = ("0|" & strMensajeWIO)
                                    strCodRetorno = strDatosWIO.Substring(0, 2)
                                    strResultado = strMensajeWIO.Substring(0, &HFE)
                                End If
                            Else
                                strDatosWIO = ("0|" & strMensajeWIO)
                                strCodRetorno = strDatosWIO.Substring(0, 2)
                                strResultado = strMensajeWIO.Substring(0, &HFE)
                            End If 'tx2.callProgramaHost(strTrama, a....
                        End If ' (strUsoWS = "SI") Then
                        'Pagos_frmLiquidacionesRegistro.LogEnvioDesembolso(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, pintTipoMoneda, current.Item("CodProveedor").ToString, pstrTrama, strCodRetorno, strResultado)
                        Dim strArray As String() = strResCarga.Split(New Char() {"|"c})
                        If (strArray(0).ToString <> "0") Then
                            str = (str & strArray(1).ToString & Environment.NewLine)
                            flagError = True
                            'Pagos_frmLiquidacionesRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "03", strCodMonedaPago, numMontoAbonoLog, numMontoIGV)
                        Else
                            'Pagos_frmLiquidacionesRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "02", strCodMonedaPago, numMontoAbonoLog, numMontoIGV)
                        End If

                    End If 'If flagTengoCU Then

                Else 'If (current.Item("CodMedioAbono").ToString = "002") Then
                    If ((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02")) Then

                        'Si es Cargo
                        If (strCodTipoOperacion = "002") Then
                            flagAbono = False
                            If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                numMontoIGV = Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numIGVCargo, flagAbono)
                                numMontoAbono = Decimal.Add(Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono), numIGVCargo)
                            Else
                                numMontoIGV = numIGVCargo
                                numMontoAbono = Decimal.Add(nbrMontoPago, numIGVCargo)
                            End If
                            numMontoAbonoLog = Decimal.Subtract(numMontoAbono, numMontoIGV)
                        Else ' (strCodTipoOperacion = "002") Then
                            flagAbono = True
                            If (strCodAgrupacion = "02") Then
                                numMontoAbonoSAdel = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                                If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                    numMontoAbono = Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numMontoAbonoSAdel, flagAbono)
                                Else
                                    numMontoAbono = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                                End If
                            End If
                            If ((strCodAgrupacion = "01") Or (strCodAgrupacion = "03") Or (strCodAgrupacion = "04") Or (strCodAgrupacion = "06") Or (strCodAgrupacion = "15")) Then
                                If ((codMonedaAgrupacion <> strCodMonedaPago) And (codMonedaAgrupacion = "002")) Then
                                    numMontoAbono = Decimal.Multiply(nbrMontoPago, numTCSunat)
                                Else
                                    If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                        numMontoAbono = nbrMontoPago / numTCSunat
                                    Else
                                        numMontoAbono = nbrMontoPago
                                    End If
                                End If
                            End If
                            If ((strCodAgrupacion = "05") Or (strCodAgrupacion = "07") Or (strCodAgrupacion = "13")) Then
                                If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                    numMontoAbono = Pagos_frmLiquidacionesRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono)
                                Else
                                    numMontoAbono = nbrMontoPago
                                End If
                            End If
                            numMontoAbonoLog = numMontoAbono
                        End If ' (strCodTipoOperacion = "002") Then
                    End If '((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02")) Then
                End If ''If (current.Item("CodMedioAbono").ToString = "002") Then

            End If '((Not table Is Nothing) AndAlso (table.Rows.Count > 0)) Then
            strError = str
            If flagError Then
                num2 = -1
            Else
                num2 = 3
            End If
            num = num2
        Catch exception1 As Exception
            num2 = -2
            str = (str & exception1.ToString)
            strError = str
            num = num2
            Return num
        End Try
        Return num

    End Function

    ''' <summary>
    ''' Arma Trama Pagos
    ''' </summary>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Se corrige Trama
    Protected Shared Function pArmaTramaDesembolso(ByVal pstrNumeroInterno As String, _
                                            ByVal pintTipoCuenta As String, _
                                            ByVal pintTipoMoneda As String, _
                                            ByVal pstrNroOficina As String, _
                                            ByVal pstrCodigoUnico As String, _
                                            ByVal pstrNroCuenta As String, _
                                            ByVal pdecMonto As Decimal, _
                                            ByVal pstrExisteTCambio As String, _
                                            ByVal pstrMonedaDocumento As String, _
                                            ByVal pstrTipoCambio As String, _
                                            ByVal pstrImportePagar As String, _
                                            ByVal pstrCodAgrupacion As String, _
                                            ByVal pstrCodTran As String, _
                                            ByVal pstrExtorno As String, _
                                            ByRef pstrTrama As String) As String
        'StrGobal
        Dim strTrama As String = ""

        Try
            'Variables
            Dim chrPad As Char = " "c
            Dim chrPadCero As Char = "0"c
            Dim strFechaActualDia As String = DateTime.Now.ToString("dd")
            Dim strFechaActualMes As String = DateTime.Now.ToString("MM")
            Dim strFechaActualAnio As String = DateTime.Now.ToString("yy")

            'Parametros Globales
            Dim strNumeroInterno As String = pstrNumeroInterno
            Dim strCodigoUnico As String = pstrCodigoUnico

            'Parametros Cargo
            Dim str55 As String = ""
            Dim strTipoCuentaCargo As String = ""
            Dim strTipoMonedaCargo As String = ""
            Dim strNroOficinaCargo As String = ""
            Dim strNroCuentaCargo As String = ""
            Dim strMontoCargo As String = ""
            'Dim strMontoCargo As Decimal = ""
            Dim strCategoriaCuentaCargo As String = ""

            'Parametros Abono
            Dim str54 As String = ""
            Dim strTipoCuentaAbono As String = ""
            Dim strTipoMonedaAbono As String = ""
            Dim strNroOficinaAbono As String = ""
            Dim strNroCuentaAbono As String = ""
            Dim strMontoAbono As String = ""
            'Dim strMontoAbono As Decimal = ""
            Dim strCategoriaCuentaAbono As String = ""

            Dim codigoTienda As String = ""

            'Valida si es CARGO o ABONO
            If pstrCodAgrupacion.Trim.Equals("08") Or _
                pstrCodAgrupacion.Trim.Equals("09") Or _
                pstrCodAgrupacion.Trim.Equals("10") Or _
                pstrCodAgrupacion.Trim.Equals("11") Or _
                pstrCodAgrupacion.Trim.Equals("12") Or _
                pstrCodAgrupacion.Trim.Equals("14") Then


                strNroOficinaCargo = pstrNroOficina
                strNroCuentaCargo = pstrNroCuenta
                str55 = "03"
                'Valida TipoCuenta
                If pintTipoCuenta = GCCConstante.C_TIPOCUENTA_CORRIENTE Then
                    strTipoCuentaCargo = GCCConstante.C_CODCTA_CORRIENTE
                    strCategoriaCuentaCargo = "0001"
                ElseIf pintTipoCuenta = GCCConstante.C_TIPOCUENTA_AHORROS Then
                    strTipoCuentaCargo = GCCConstante.C_CODCTA_AHORROS
                    strCategoriaCuentaCargo = "0002"
                End If

                'Valida TipoMoneda
                If pintTipoMoneda = GCCConstante.C_TIPOMONEDA_SOLES Then
                    strTipoMonedaCargo = GCCConstante.C_TX_MONEDA_SOLES
                ElseIf pintTipoMoneda = GCCConstante.C_TIPOMONEDA_DOLARES Then
                    strTipoMonedaCargo = GCCConstante.C_TX_MONEDA_DOLARES
                End If

                'Valida Monto
                'strMontoCargo = pdecMonto.ToString
                'strMontoCargo = Replace(strMontoCargo, ".", "")
                'strMontoCargo = Replace(strMontoCargo, ",", "")
                strMontoCargo = Strings.Replace(Strings.Replace(Convert.ToInt32(Math.Round(Decimal.Multiply(pdecMonto, 100))).ToString, ".", "", 1, -1, CompareMethod.Binary), ",", "", 1, -1, CompareMethod.Binary)

            ElseIf pstrCodAgrupacion.Trim.Equals("02") Or pstrCodAgrupacion.Trim.Equals("07") Then
                str54 = "03"
                strNroOficinaAbono = pstrNroOficina
                strNroCuentaAbono = pstrNroCuenta

                'Valida TipoCuenta
                If pintTipoCuenta = GCCConstante.C_TIPOCUENTA_CORRIENTE Then
                    strTipoCuentaAbono = GCCConstante.C_CODCTA_CORRIENTE
                    strCategoriaCuentaAbono = "0001"
                ElseIf pintTipoCuenta = GCCConstante.C_TIPOCUENTA_AHORROS Then
                    strTipoCuentaAbono = GCCConstante.C_CODCTA_AHORROS
                    strCategoriaCuentaAbono = "0002"
                End If

                'Valida TipoMoneda
                If pintTipoMoneda = GCCConstante.C_TIPOMONEDA_SOLES Then
                    strTipoMonedaAbono = GCCConstante.C_TX_MONEDA_SOLES
                ElseIf pintTipoMoneda = GCCConstante.C_TIPOMONEDA_DOLARES Then
                    strTipoMonedaAbono = GCCConstante.C_TX_MONEDA_DOLARES
                End If

                'Valida Monto
                'strMontoAbono = pdecMonto.ToString
                'strMontoAbono = Replace(strMontoAbono, ".", "")
                'strMontoAbono = Replace(strMontoAbono, ",", "")
                strMontoAbono = Strings.Replace(Strings.Replace(Convert.ToInt32(Math.Round(Decimal.Multiply(pdecMonto, 100))).ToString, ".", "", 1, -1, CompareMethod.Binary), ",", "", 1, -1, CompareMethod.Binary)

            End If


            'FCD-DATOS-LINK
            Dim FCD_CODRET As String = strPreparaDato("00", 2, chrPad)
            Dim FCD_COD_TRAN As String = strPreparaDato(pstrCodTran, 4, chrPad)
            Dim FCD_PROGRAMA As String = strPreparaDato("FCDO04", 8, chrPad)
            Dim FCD_USUARIO As String = strPreparaDato("USERFCD", 8, chrPad)
            strTrama = String.Concat(strTrama, FCD_CODRET, FCD_COD_TRAN, FCD_PROGRAMA, FCD_USUARIO)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_CODRET, "|", FCD_COD_TRAN, "|", FCD_PROGRAMA, "|", FCD_USUARIO, "|"})
            If (GCCSession.CodigoTienda.Trim = "") Then
                codigoTienda = "100"
            Else
                codigoTienda = GCCSession.CodigoTienda
            End If

            'INPUT GENERICO
            Dim FCD_COMM_CD_NU_DOC As String = strPreparaDato(strNumeroInterno, 10, chrPad)
            Dim FCD_COMM_CD_TRAN_CODE As String = strPreparaDato("", 3, chrPad)
            Dim FCD_COMM_CD_FECPRO_SS As String = strPreparaDato("21", 2, chrPad)
            Dim FCD_COMM_CD_FECPRO_YY As String = strPreparaDato(strFechaActualAnio, 2, chrPad)
            Dim FCD_COMM_CD_FECPRO_MM As String = strPreparaDato(strFechaActualMes, 2, chrPad)
            Dim FCD_COMM_CD_FECPRO_DD As String = strPreparaDato(strFechaActualDia, 2, chrPad)
            Dim FCD_COMM_CD_REG_EMPLEADO As String = strPreparaDatoPadRight(GCCSession.CodigoUsuario, 8, chrPad)
            Dim FCD_COMM_CD_TIENDA_ORIGEN As String = strPreparaDato(codigoTienda, 3, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_NU_DOC, FCD_COMM_CD_TRAN_CODE, FCD_COMM_CD_FECPRO_SS, FCD_COMM_CD_FECPRO_YY, FCD_COMM_CD_FECPRO_MM, FCD_COMM_CD_FECPRO_DD, FCD_COMM_CD_REG_EMPLEADO, FCD_COMM_CD_TIENDA_ORIGEN)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_NU_DOC, "|", FCD_COMM_CD_TRAN_CODE, "|", FCD_COMM_CD_FECPRO_SS, "|", FCD_COMM_CD_FECPRO_YY, "|", FCD_COMM_CD_FECPRO_MM, "|", FCD_COMM_CD_FECPRO_DD, "|", FCD_COMM_CD_REG_EMPLEADO, "|", FCD_COMM_CD_TIENDA_ORIGEN, "|"})
            'INPUT PARA ABONO

            Dim FCD_COMM_CD_TIP_CTA_CR As String = strPreparaDato(strTipoCuentaAbono, 2, chrPad)
            Dim FCD_COMM_CD_CR_CTL1 As String = strPreparaDato(str54, 2, chrPad)
            Dim FCD_COMM_CD_CR_CTL2 As String = strPreparaDato(strTipoMonedaAbono, 3, chrPad)
            Dim FCD_COMM_CD_CR_CTL3 As String = strPreparaDato(strNroOficinaAbono, 3, chrPad)
            Dim FCD_COMM_CD_CR_CTL4 As String = strPreparaDato(strCategoriaCuentaAbono, 4, chrPad)
            Dim FCD_COMM_CD_CR_ACCT As String = strPreparaDato(strNroCuentaAbono, 10, chrPad)
            Dim FCD_COMM_CD_CR_FILL As String = strPreparaDato("", 3, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_TIP_CTA_CR, FCD_COMM_CD_CR_CTL1, FCD_COMM_CD_CR_CTL2, FCD_COMM_CD_CR_CTL3, FCD_COMM_CD_CR_CTL4, FCD_COMM_CD_CR_ACCT, FCD_COMM_CD_CR_FILL)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_TIP_CTA_CR, "|", FCD_COMM_CD_CR_CTL1, "|", FCD_COMM_CD_CR_CTL2, "|", FCD_COMM_CD_CR_CTL3, "|", FCD_COMM_CD_CR_CTL4, "|", FCD_COMM_CD_CR_ACCT, "|", FCD_COMM_CD_CR_FILL, "|"})

            'INPUT PARA DEBITO (CARGO)
            Dim FCD_COMM_CD_TIP_CTA_DB As String = strPreparaDato(strTipoCuentaCargo, 2, chrPad)
            Dim FCD_COMM_CD_DB_CTL1 As String = strPreparaDato(str55, 2, chrPad)
            Dim FCD_COMM_CD_DB_CTL2 As String = strPreparaDato(strTipoMonedaCargo, 3, chrPad)
            Dim FCD_COMM_CD_DB_CTL3 As String = strPreparaDato(strNroOficinaCargo, 3, chrPad)
            Dim FCD_COMM_CD_DB_CTL4 As String = strPreparaDato(strCategoriaCuentaCargo, 4, chrPad)
            Dim FCD_COMM_CD_DB_ACCT As String = strPreparaDato(strNroCuentaCargo, 10, chrPad)
            Dim FCD_COMM_CD_DB_FILL As String = strPreparaDato("", 3, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_TIP_CTA_DB, FCD_COMM_CD_DB_CTL1, FCD_COMM_CD_DB_CTL2, FCD_COMM_CD_DB_CTL3, FCD_COMM_CD_DB_CTL4, FCD_COMM_CD_DB_ACCT, FCD_COMM_CD_DB_FILL)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_TIP_CTA_DB, "|", FCD_COMM_CD_DB_CTL1, "|", FCD_COMM_CD_DB_CTL2, "|", FCD_COMM_CD_DB_CTL3, "|", FCD_COMM_CD_DB_CTL4, "|", FCD_COMM_CD_DB_ACCT, "|", FCD_COMM_CD_DB_FILL, "|"})

            'INPUT GENERICO
            Dim FCD_COMM_CD_EXTORNO As String = strPreparaDato(pstrExtorno, 1, chrPad)
            Dim FCD_COMM_CD_SHORT_DESC As String = strPreparaDatoPadRight("PR  CARG CPR", 15, chrPad)
            Dim FCD_COMM_CD_AMOUNT_CR As String = strPreparaDato(strMontoAbono, 15, chrPadCero) 'ABONO
            Dim FCD_COMM_CD_AMOUNT_DB As String = strPreparaDato(strMontoCargo, 15, chrPadCero) 'CARGO
            Dim FCD_COMM_CD_COBRO_FORZOSO As String = strPreparaDato("N", 1, chrPad)
            Dim FCD_COMM_CD_COBRO_PARCIAL As String = strPreparaDato("N", 1, chrPad)
            Dim FCD_COMM_CD_CODUNI As String = strPreparaDato(strCodigoUnico, 10, chrPad)

            Dim FCD_COMM_CTA_FLG_OC As String = strPreparaDato(pstrExisteTCambio, 1, chrPad)
            Dim FCD_COMM_CTA_MON_CF As String = strPreparaDato(pstrMonedaDocumento, 3, chrPad)
            Dim FCD_COMM_CTA_CLA_TC As String = strPreparaDato("", 2, chrPad)
            Dim FCD_COMM_CTA_TC_CF As String = strPreparaDato(pstrTipoCambio, 15, chrPad)
            Dim FCD_COMM_CTA_IMP_EQUIV As String = strPreparaDato(pstrImportePagar, 15, chrPad)

            Dim FCD_COMM_CD_FILLER As String = strPreparaDato("", 520, chrPad) 'strPreparaDato("", 561, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_EXTORNO, FCD_COMM_CD_SHORT_DESC, FCD_COMM_CD_AMOUNT_CR, FCD_COMM_CD_AMOUNT_DB, FCD_COMM_CD_COBRO_FORZOSO, FCD_COMM_CD_COBRO_PARCIAL, FCD_COMM_CD_CODUNI, FCD_COMM_CTA_FLG_OC, FCD_COMM_CTA_MON_CF, FCD_COMM_CTA_CLA_TC, FCD_COMM_CTA_TC_CF, FCD_COMM_CTA_IMP_EQUIV, FCD_COMM_CD_FILLER)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_EXTORNO, "|", FCD_COMM_CD_SHORT_DESC, "|", FCD_COMM_CD_AMOUNT_CR, "|", FCD_COMM_CD_AMOUNT_DB, "|", FCD_COMM_CD_COBRO_FORZOSO, "|", FCD_COMM_CD_COBRO_PARCIAL, "|", FCD_COMM_CD_CODUNI, "|", FCD_COMM_CTA_FLG_OC, "|", FCD_COMM_CTA_MON_CF, "|", FCD_COMM_CTA_CLA_TC, "|", FCD_COMM_CTA_TC_CF, "|", FCD_COMM_CTA_IMP_EQUIV, "|", FCD_COMM_CD_FILLER, "|"})
            'COMMAREA
            Dim FCD_LENGTH_COMMAREA As String = strPreparaDato("00920", 5, chrPad)
            strTrama = String.Concat(strTrama, FCD_LENGTH_COMMAREA)
            pstrTrama = (pstrTrama & FCD_LENGTH_COMMAREA & "|")
            'INPUT DE ERROR
            Dim FCD_COD_RET_TOLD As String = strPreparaDato("", 2, chrPad)
            Dim FCD_COD_RET_O As String = strPreparaDato("", 2, chrPad)
            Dim FCD_MSG_ERROR As String = strPreparaDato("", 40, chrPad)
            'INPUT SALIDA DE DATOS
            Dim FCD_FC04_NRO_DOCUMENTO As String = strPreparaDato("", 10, chrPad)
            Dim FCD_FC04_IMPORTE_DESEMB As String = strPreparaDato("", 15, chrPad)
            Dim FCD_FC04_FLG_EXTORNO As String = strPreparaDato("", 1, chrPad)
            Dim FILLER As String = strPreparaDato("", 198, chrPad)
            strTrama = String.Concat(strTrama, FCD_COD_RET_TOLD, FCD_COD_RET_O, FCD_MSG_ERROR, FCD_FC04_NRO_DOCUMENTO, FCD_FC04_IMPORTE_DESEMB, FCD_FC04_FLG_EXTORNO, FILLER)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COD_RET_TOLD, "|", FCD_COD_RET_O, "|", FCD_MSG_ERROR, "|", FCD_FC04_NRO_DOCUMENTO, "|", FCD_FC04_IMPORTE_DESEMB, "|", FCD_FC04_FLG_EXTORNO, "|", FILLER})

            Return strTrama

        Catch ex As Exception
            Dim intTamanioTrama As Integer = strTrama.Length
            strTrama = String.Concat(strTrama, strPreparaDato("", (1001 - intTamanioTrama), " "c))
            Return strTrama
        End Try

    End Function

    ''' <summary>
    ''' Prepara Dato para tramas
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Function strPreparaDato(ByVal pstrDato As String, ByVal pintTamano As Integer, ByVal pchrCompletar As Char) As String
        Dim strDato As String = ""

        If pstrDato Is Nothing Then
            pstrDato = ""
        End If
        pstrDato = Mid(pstrDato, 1, pintTamano)
        strDato = pstrDato.PadLeft(pintTamano, pchrCompletar)

        Return strDato
    End Function

    Private Shared Function strPreparaDatoPadRight(ByVal pstrDato As String, ByVal pintTamano As Integer, ByVal pchrCompletar As Char) As String
        Dim strDato As String = ""

        If pstrDato Is Nothing Then
            pstrDato = ""
        End If
        pstrDato = Mid(pstrDato, 1, pintTamano)
        strDato = pstrDato.PadRight(pintTamano, pchrCompletar)

        Return strDato
    End Function

    Private Shared Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer
        Dim numRet As Integer
        Try
            Dim tx As New LInstruccionDesembolsoTx
            Try
                numRet = tx.EjecutarDesembolsoLPC(pFlag, pCodInstDesembolso, pRegUsuario)
            Catch exception1 As SqlException
                Throw New Exception(exception1.ToString)
            End Try
        Catch exception3 As Exception
            Throw exception3
        End Try
        Return numRet
    End Function

    Private Shared Function actualizarEstadoEjecucionPago(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCodAgrupacion As String, ByVal pstrTipoMoneda As String, ByVal pstrCodProveedor As String, ByVal strEstadoEjecucion As String, ByVal strCodMonedaPago As String, ByVal decMonto As Decimal, ByVal decMontoIGVCargo As Decimal) As Boolean
        Dim blnRet As Boolean
        Try
            Dim tx As New LInstruccionDesembolsoTx
            Dim pago As New EGCC_InsDesembolsoPago
            With pago
                .Codsolicitudcredito = pstrNroContrato
                .Codinstrucciondesembolso = pstrNroInstruccion
                .Codmonedaagrupacion = pstrTipoMoneda
                .Codagrupacion = pstrCodAgrupacion
                .Codproveedor = pstrCodProveedor
                .CodEstadoEjecucionPago = strEstadoEjecucion
                .CodMonedaCargoAbono = strCodMonedaPago
                .MontoCargoAbono = decMonto
                .MontoIGVCargo = decMontoIGVCargo
            End With
            Dim strObj As String = GCCUtilitario.SerializeObject(Of EGCC_InsDesembolsoPago)(pago)
            blnRet = tx.actualizarEstadoEjecucionPago(strObj)
        Catch exception1 As Exception
            Throw exception1
        End Try
        Return blnRet
    End Function

    Private Shared Function tipoCambio(ByVal CodMonedaAgrupacion As String, ByVal CodMonedaMedioPago As String, ByVal nbrTCAgrp As Decimal, ByVal nbrTCPrefVta As Decimal, ByVal nbrTCPrefCmp As Decimal, ByVal nbrMontoPago As Decimal, ByVal blnEsAbono As Boolean) As Decimal
        Dim numTCCmp As Decimal
        Dim numTCVta As Decimal
        Dim num2 As New Decimal
        Dim decRet As New Decimal
        If blnEsAbono Then
            numTCVta = nbrTCPrefCmp
            numTCCmp = nbrTCPrefVta
        Else
            numTCVta = nbrTCPrefVta
            numTCCmp = nbrTCPrefCmp
        End If
        If (CodMonedaAgrupacion = "002") Then
            If (Decimal.Compare(nbrTCAgrp, Decimal.Zero) = 0) Then
                decRet = Decimal.Multiply(nbrMontoPago, numTCVta)
            Else
                decRet = Decimal.Multiply(nbrMontoPago, nbrTCAgrp)
            End If
        ElseIf (Decimal.Compare(nbrTCAgrp, Decimal.Zero) = 0) Then
            decRet = Decimal.Divide(nbrMontoPago, numTCCmp)
        Else
            decRet = Decimal.Divide(nbrMontoPago, nbrTCAgrp)
        End If
        Return Math.Round(decRet, 2)
    End Function

    Private Shared Function saldoCuenta(ByVal pstrCU As String, ByVal pstrCodTipoCta As String, ByVal pstrCodMoneda As String, ByRef strRta As String) As String()
        Try
            Dim ListaCuentas As New List(Of String)
            ListaCuentas.Add(0)

            Dim prefijoMoneda As String

            Dim strDatos As String
            Dim strMensaje As String
            Dim strCodMoneda As String

            Dim strTipoCta As String
            Dim tx As New LInstruccionDesembolsoNTx
            Dim argsUsuarioTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("USER_TLD")
            Dim argsAgenciaTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("AGENCIA_TLD")
            Dim numSaldoRet As Double = 0
            If (pstrCodTipoCta = "ST") Then
                strTipoCta = "2"
            Else
                strTipoCta = "1"
            End If
            If (pstrCodMoneda = "002") Then
                prefijoMoneda = "US$"
                strCodMoneda = "010"
            Else
                prefijoMoneda = "S/."
                strCodMoneda = "001"
            End If
            Dim strCU As String
            If pstrCU.Length = 8 Then
                strCU = "00" + pstrCU
            Else
                strCU = pstrCU
            End If
            If tx.callProgramaHost((strCU & strTipoCta), argsUsuarioTld, argsAgenciaTld, "LPCO015", "SaldoCuenta", strMensaje, strDatos) Then
                Dim nbrCantCtas As Integer = Convert.ToInt16(strDatos.Substring(13, 1))
                Dim startIndex As Integer = 14
                Dim num8 As Integer = (nbrCantCtas - 1)
                Dim i As Integer = 0
                Do While (i <= num8)
                    Dim saldo As Double
                    Dim situacion As String
                    Dim tipoCuenta As String
                    Dim strMoneda As String = strDatos.Substring(startIndex, 3)
                    startIndex = (startIndex + 3)
                    Dim strTienda As String = strDatos.Substring(startIndex, 3)
                    startIndex = (startIndex + 3)
                    Dim NroCta As String = strDatos.Substring(startIndex, &H16)
                    startIndex = (startIndex + &H16)
                    If (strDatos.Substring((startIndex + 1), 15).Trim.Length > 0) Then
                        Dim nbr_saldo As Int64 = Convert.ToInt64(strDatos.Substring((startIndex + 1), 15))
                        If (strDatos.Substring(startIndex, 1) = "+") Then
                            saldo = (CDbl(nbr_saldo) / 100)
                        Else
                            saldo = (-1 * (CDbl(nbr_saldo) / 100))
                        End If
                    Else
                        saldo = 0
                    End If
                    startIndex = (startIndex + &H10)
                    If (i = (nbrCantCtas - 1)) Then
                        If ((startIndex + 30) > strDatos.Length) Then
                            tipoCuenta = strDatos.Substring(startIndex, (strDatos.Length - startIndex))
                            situacion = ""
                        Else
                            tipoCuenta = strDatos.Substring(startIndex, 30)
                            startIndex = (startIndex + 30)
                            If ((startIndex + 30) > strDatos.Length) Then
                                situacion = strDatos.Substring(startIndex, (strDatos.Length - startIndex))
                            Else
                                situacion = strDatos.Substring(startIndex, 30)
                            End If
                        End If
                    Else
                        tipoCuenta = strDatos.Substring(startIndex, 30)
                        startIndex = (startIndex + 30)
                        situacion = strDatos.Substring(startIndex, 30)
                        startIndex = (startIndex + 30)
                    End If
                    If (strMoneda = strCodMoneda) Then
                        If pstrCodTipoCta = "ST" Then NroCta = NroCta.Substring(4)
                        ListaCuentas.Add(strTienda & "-" & NroCta)
                        ListaCuentas.Add(prefijoMoneda & saldo.ToString("############.00"))
                    End If
                    i += 1
                Loop
                strRta = ""
            End If
            strRta = strMensaje

            ListaCuentas.Item(0) = ((ListaCuentas.Count - 1) / 2).ToString
            Return ListaCuentas.ToArray

        Catch exception1 As Exception
            Throw exception1
        End Try

    End Function

    Private Shared Function LogEnvioDesembolso(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCodAgrupacion As String, ByVal pstrTipoMoneda As String, ByVal pstrCodProveedor As String, ByVal strTramaArray As String, ByVal strCodRetorno As String, ByVal strResultado As String) As Boolean
        Dim flag As Boolean
        Dim format As String = "yyyy-MM-dd HH:mm:ss.fff"
        Try
            Dim strArray As String() = strTramaArray.Split(New Char() {"|"c})
            Dim tx As New LInstruccionDesembolsoTx
            Dim ejecucion As New EGCC_LogDesembolsoPagoEjecucion
            With ejecucion
                .CodSolicitudCredito = pstrNroContrato
                .CodInstruccionDesembolso = pstrNroInstruccion
                .CodAgrupacion = pstrCodAgrupacion
                .CodMonedaAgrupacion = pstrTipoMoneda
                .CodProveedor = pstrCodProveedor
                .FechaHora = DateTime.Now.ToString(format)
                .AudFechaRegistro = DateTime.Now.ToString(format)
                .AudFechaModificacion = DateTime.Now.ToString(format)
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
                .AudUsuarioModificacion = GCCSession.CodigoUsuario
                .FCDCODRET = strArray(0)
                .FCDCODTRAN = strArray(1)
                .FCDPROGRAMA = strArray(2)
                .FCDUSUARIO = strArray(3)
                .FCDNUDOC = strArray(4)
                .FCDTRANCODE = strArray(5)
                .FCDFECPROSS = strArray(6)
                .FCDFECPROYY = strArray(7)
                .FCDFECPROMM = strArray(8)
                .FCDFECPRODD = strArray(9)
                .FCDREGEMPLEADO = strArray(10)
                .FCDTIENDAORIGEN = strArray(11)
                .FCDTIPCTACR = strArray(12)
                .FCDCRCTL1 = strArray(13)
                .FCDCRCTL2 = strArray(14)
                .FCDCRCTL3 = strArray(15)
                .FCDCRCTL4 = strArray(&H10)
                .FCDCRACCT = strArray(&H11)
                .FCDCRFILL = strArray(&H12)
                .FCDTIPCTADB = strArray(&H13)
                .FCDDBCTL1 = strArray(20)
                .FCDDBCTL2 = strArray(&H15)
                .FCDDBCTL3 = strArray(&H16)
                .FCDDBCTL4 = strArray(&H17)
                .FCDDBACCT = strArray(&H18)
                .FCDDBFILL = strArray(&H19)
                .FCDEXTORNO = strArray(&H1A)
                .FCDSHORTDESC = strArray(&H1B)
                .FCDAMOUNTCR = strArray(&H1C)
                .FCDAMOUNTDB = strArray(&H1D)
                .FCDCOBROFORZOSO = strArray(30)
                .FCDCOBROPARCIAL = strArray(&H1F)
                .FCDCODUNI = strArray(&H20)
                .FCDCTAFLGOC = strArray(&H21)
                .FCDCTAMONCF = strArray(&H22)
                .FCDCTACLATC = strArray(&H23)
                .FCDCTATCCF = strArray(&H24)
                .FCDCTAIMPEQUIV = strArray(&H25)
                .FILLERINP = strArray(&H26)
                .FCDLENGTHCOMMAREA = strArray(&H27)
                .FCDCODRETTOLD = strArray(40)
                .FCDCODRETO = strArray(&H29)
                .FCDMSGERROR = strArray(&H2A)
                .FCDFC04NRODOCUMENTO = strArray(&H2B)
                .FCDFC04IMPORTEDESEMB = strArray(&H2C)
                .FCDFC04FLGEXTORNO = strArray(&H2D)
                .FILLEROUT = strArray(&H2E)
                .CodRetorno = strCodRetorno
                .Resultado = strResultado
            End With
            Dim str2 As String = GCCUtilitario.SerializeObject(Of EGCC_LogDesembolsoPagoEjecucion)(ejecucion)
            flag = tx.LogEnvioDesembolso(str2)
        Catch exception1 As Exception
            Throw exception1
        End Try
        Return flag
    End Function



    'Fin IBK
#End Region

#Region "Util"

    ''' <summary>
    ''' Total paginas
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region

#Region "Exportar Excel"
    Sub Exportar(ByVal DTable As DataTable, ByVal DtableHeader As DataTable, ByVal DSResumen As DataSet, ByVal DSDetalle As DataSet, ByVal DTCrono As DataTable)
        Dim ruta As String = HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/Cronograma.xlsx")

        Dim strNombreArchivo As String
        strNombreArchivo = "Liquidacion_" + Now.ToLongTimeString.Replace(":", "_").Replace(".", "") + ".xlsx"

        Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../temp/" + strNombreArchivo))
        Dim template As New FileInfo(HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/Cronograma.xlsx"))

        Dim col As Integer = 1
        Dim row As Integer = 18
        Dim RowData As String
        Dim sumaPrincipal As Decimal = 0.0
        Dim sumaInteres As Decimal = 0.0
        Dim sumaCuota As Decimal = 0.0
        Dim sumaPrincipalSeguro As Decimal = 0.0
        Dim sumaInteresSeguro As Decimal = 0.0
        Dim sumaCuotaSeguro As Decimal = 0.0
        Dim sumaIGV As Decimal = 0.0
        Dim sumaTotalPagar As Decimal = 0.0
        Dim x As Integer
        Dim SumaVV As Decimal = 0
        Dim SumaIGVx As Decimal = 0
        Dim SumaTotal As Decimal = 0

        'Variables Suma Final
        Dim SumaVVTotal As Decimal = 0
        Dim SumaIGVxTotal As Decimal = 0
        Dim SumaTotalx As Decimal = 0

        If Not Directory.Exists(HttpContext.Current.Server.MapPath("../temp")) Then
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../temp"))
        End If
        If File.Exists(newFile.ToString()) Then
            File.Delete(newFile.ToString())
        End If

        Dim pck As New ExcelPackage(newFile, template)
        Dim worksheet As ExcelWorksheet = pck.Workbook.Worksheets("LIQUIDACIÓN")

        'Carga Header
        worksheet.Cells(9, 3).Value = DtableHeader.Rows(0).Item("CodSolicitudCredito").ToString()
        worksheet.Cells(9, 9).Value = DtableHeader.Rows(0).Item("CantPlazoTotal")
        worksheet.Cells(10, 3).Value = DtableHeader.Rows(0).Item("Categoria").ToString()
        worksheet.Cells(10, 9).Value = DtableHeader.Rows(0).Item("CantPlazoGracia")
        worksheet.Cells(11, 3).Value = DtableHeader.Rows(0).Item("NombreProductoFinanciero").ToString()
        worksheet.Cells(11, 9).Value = DtableHeader.Rows(0).Item("MontoFinanciamientoCronograma")
        worksheet.Cells(12, 9).Value = IIf(String.IsNullOrEmpty(txtCodigoLiquidacion.Value), "", txtCodigoLiquidacion.Value)
        worksheet.Cells(12, 3).Value = DtableHeader.Rows(0).Item("NombreSubproductoFinanciero").ToString()
        worksheet.Cells(13, 3).Value = DtableHeader.Rows(0).Item("NombreMonedaAPP").ToString()
        worksheet.Cells(14, 3).Value = DtableHeader.Rows(0).Item("NombreSubprestatario").ToString()
        'Carga Cuerpo Cronograma
        For Each i As DataRow In DTable.Rows
            sumaPrincipal = sumaPrincipal + CDbl(IIf(String.IsNullOrEmpty(i.Item("Principal").ToString()), 0, i.Item("Principal").ToString()))
            sumaInteres = sumaInteres + CDbl(IIf(String.IsNullOrEmpty(i.Item("Interes").ToString()), 0, i.Item("Interes").ToString()))
            sumaCuota = sumaCuota + CDbl(IIf(String.IsNullOrEmpty(i.Item("Cuota").ToString()), 0, i.Item("Cuota").ToString()))
            sumaPrincipalSeguro = sumaPrincipalSeguro + CDbl(IIf(String.IsNullOrEmpty(i.Item("Principal_Seguro").ToString()), 0, i.Item("Principal_Seguro").ToString()))
            sumaCuotaSeguro = sumaCuotaSeguro + CDbl(IIf(String.IsNullOrEmpty(i.Item("Interes_Seguro").ToString()), 0, i.Item("Interes_Seguro").ToString()))
            sumaInteresSeguro = sumaInteresSeguro + CDbl(IIf(String.IsNullOrEmpty(i.Item("Cuota_Seguro").ToString()), 0, i.Item("Cuota_Seguro").ToString()))
            sumaIGV = sumaIGV + CDbl(IIf(String.IsNullOrEmpty(i.Item("IGV").ToString()), 0, i.Item("IGV").ToString()))
            sumaTotalPagar = sumaTotalPagar + CDbl(IIf(String.IsNullOrEmpty(i.Item("Total_Pagar").ToString()), 0, i.Item("Total_Pagar").ToString()))
        Next
        For Each rw As DataRow In DTable.Rows
            For Each cl As DataColumn In DTable.Columns
                If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                    RowData = rw(cl.ColumnName).ToString()
                    worksheet.Cells(row, col).Value = rw(cl.ColumnName)
                    worksheet.SelectedRange(row, 1, row, 16).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    worksheet.SelectedRange(row, 4, row, 14).Style.Numberformat.Format = "###,###,##0.00"
                    worksheet.SelectedRange(row, 5, row, 14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
                End If
                col += 1
            Next
            If (String.IsNullOrEmpty(worksheet.Cells(row, 1).Value)) Then
                worksheet.SelectedRange(row, 1, row, 16).Style.Fill.PatternType = ExcelFillStyle.Solid
                worksheet.SelectedRange(row, 1, row, 16).Style.Fill.BackgroundColor.SetColor(Color.LightYellow)
                worksheet.SelectedRange(row, 1, row, 16).Value = ""
            End If
            row += 1
            col = 1
        Next
        'Calcula Totales
        row += 2
        worksheet.Cells(row, 5).Value = "Totales"
        worksheet.Cells(row, 6).Value = sumaPrincipal
        worksheet.Cells(row, 7).Value = sumaInteres
        worksheet.Cells(row, 8).Value = sumaCuota
        worksheet.Cells(row, 10).Value = sumaPrincipalSeguro
        worksheet.Cells(row, 11).Value = sumaCuotaSeguro
        worksheet.Cells(row, 12).Value = sumaInteresSeguro
        worksheet.Cells(row, 13).Value = sumaIGV
        worksheet.Cells(row, 14).Value = sumaTotalPagar
        worksheet.SelectedRange(row, 5, row, 14).Style.Font.Bold = True
        row += 2
        worksheet.SelectedRange(row, 1, row, 16).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 1, row, 16).Style.Fill.BackgroundColor.SetColor(Color.Black)
        row += 2
        worksheet.Cells(row, 2).Value = "Cronograma Nuevo"
        worksheet.SelectedRange(row, 2, row, 2).Style.Font.Bold = True
        worksheet.SelectedRange(row, 2, row, 2).Style.Font.Size = 16
        row += 2
        worksheet.Cells(row, 1).Value = "Nro"
        worksheet.Cells(row, 2).Value = "Vencimiento"
        worksheet.Cells(row, 3).Value = "Dias"
        worksheet.Cells(row, 4).Value = "TEA"
        worksheet.Cells(row, 5).Value = "Saldo Adeudado"
        worksheet.Cells(row, 6).Value = "Principal"
        worksheet.Cells(row, 7).Value = "Interes"
        worksheet.Cells(row, 8).Value = "Cuota"
        worksheet.Cells(row, 9).Value = "Saldo Seguro"
        worksheet.Cells(row, 10).Value = "Principal Seguro"
        worksheet.Cells(row, 11).Value = "Interes Seguro"
        worksheet.Cells(row, 12).Value = "Cuota Seguro"
        worksheet.Cells(row, 13).Value = "IGV"
        worksheet.Cells(row, 14).Value = "Total a Pagar"
        worksheet.Cells(row, 15).Value = "Estado"
        worksheet.Cells(row, 16).Value = "Fecha Cancelacion"

        worksheet.SelectedRange(row, 1, row, 16).Style.Border.BorderAround(ExcelBorderStyle.Thin)
        worksheet.SelectedRange(row, 1, row, 16).Style.Border.Left.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 1, row, 16).Style.Border.Right.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 1, row, 16).Style.Font.Bold = True
        worksheet.SelectedRange(row, 1, row, 16).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        row += 2
        For Each rw As DataRow In DTCrono.Rows
            For Each cl As DataColumn In DTCrono.Columns
                If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                    RowData = rw(cl.ColumnName).ToString()
                    worksheet.Cells(row, col).Value = rw(cl.ColumnName)
                    worksheet.SelectedRange(row, 1, row, 16).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    worksheet.SelectedRange(row, 4, row, 14).Style.Numberformat.Format = "###,###,##0.00"
                    worksheet.SelectedRange(row, 5, row, 14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
                End If
                col += 1
            Next
            row += 1
            col = 1
        Next

        row += 2
        worksheet.SelectedRange(row, 1, row, 16).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 1, row, 16).Style.Fill.BackgroundColor.SetColor(Color.Black)


        row += 4
        col = 2
        'Crea Cabecera
        worksheet.Cells(row, 2).Value = "Tipo Bien: " + txtClasificacionBien.Value
        worksheet.Cells(row, 2).Style.Font.Color.SetColor(Color.Blue)
        worksheet.SelectedRange(row, 2, row, 2).Style.Font.UnderLine = True
        row += 2
        'Inicio Detalle Cuotas Atrasadas
        For xdt As Integer = 0 To DSDetalle.Tables.Count - 1
            Dim dtTabla As DataTable = DSDetalle.Tables(xdt)
            dtTabla.Columns.RemoveAt(0)
            row += 2
            worksheet.SelectedRange(row, 2, row, 2).Style.Fill.PatternType = ExcelFillStyle.Solid
            worksheet.SelectedRange(row, 2, row, 2).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
            worksheet.SelectedRange(row, 2, row, 2).Style.Font.Color.SetColor(Color.White)

            worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
            worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
            worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin

            worksheet.Cells(row, 2).Value = "Liquidación al"

            worksheet.Cells(row, 3).Value = Now.ToShortDateString.ToString()
            worksheet.Cells(row, 3).Style.Font.Color.SetColor(Color.Red)
            worksheet.Cells(row, 3).Style.Font.Bold = True
            worksheet.SelectedRange(row, 4, row, 6).Merge = True


            worksheet.SelectedRange(row, 4, row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
            worksheet.SelectedRange(row, 4, row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)

            worksheet.SelectedRange(row, 4, row, 6).Style.Font.Color.SetColor(Color.White)
            worksheet.SelectedRange(row, 4, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            worksheet.Cells(row, 4).Value = "Pendiente"

            row += 1
            worksheet.SelectedRange(row, 2, row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
            worksheet.SelectedRange(row, 2, row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)

            worksheet.SelectedRange(row, 2, row, 6).Style.Font.Color.SetColor(Color.White)
            worksheet.SelectedRange(row, 2, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

            worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
            worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
            worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin

            worksheet.Cells(row, 2).Value = "Concepto"
            worksheet.Cells(row, 3).Value = "Dias"
            worksheet.Cells(row, 4).Value = "VV"
            worksheet.Cells(row, 5).Value = "IGV"
            worksheet.Cells(row, 6).Value = "Total"
            row += 1

            For Each rw As DataRow In dtTabla.Rows
                For Each cl As DataColumn In dtTabla.Columns
                    If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                        RowData = rw(cl.ColumnName).ToString()
                        worksheet.Cells(row, col).Value = RowData.Trim().ToString()
                        worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
                        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        worksheet.SelectedRange(row, 2, row, 3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        worksheet.SelectedRange(row, 2, row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
                        worksheet.SelectedRange(row, 4, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
                    End If
                    col += 1
                Next
                row += 1
                col = 2
            Next
            worksheet.Cells(row, 2).Value = "Total Cuota"
            worksheet.SelectedRange(row, 2, row, 2).Style.Font.Bold = True

            For Each i As DataRow In dtTabla.Rows
                SumaVV = SumaVV + CDbl(IIf(String.IsNullOrEmpty(i.Item("VV").ToString()), 0, i.Item("VV").ToString()))
                SumaIGVx = SumaIGVx + CDbl(IIf(String.IsNullOrEmpty(i.Item("IGV").ToString()), 0, i.Item("IGV").ToString()))
                SumaTotal = SumaTotal + CDbl(IIf(String.IsNullOrEmpty(i.Item("Total").ToString()), 0, i.Item("Total").ToString()))

                worksheet.Cells(row, 4).Value = SumaVV
                worksheet.Cells(row, 5).Value = SumaIGVx
                worksheet.Cells(row, 6).Value = SumaTotal

            Next


            'Guarda Totales

            SumaVVTotal += SumaVV
            SumaIGVxTotal += SumaIGVx
            SumaTotalx += SumaTotal

            worksheet.SelectedRange(row, 4, row, 6).Style.Font.Bold = True
            worksheet.SelectedRange(row, 4, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right

            row += 1
            worksheet.SelectedRange(row, 2, row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
            worksheet.SelectedRange(row, 2, row, 6).Style.Fill.BackgroundColor.SetColor(Color.White)
            worksheet.SelectedRange(row, 2, row, 6).Merge = True
            row += 1

            SumaVV = 0
            SumaIGVx = 0
            SumaTotal = 0
        Next
        'Fin Detalle Cuotas Atrasadas
        'Inicio Tabla Resumen
        x = 0
        row += 2
        worksheet.SelectedRange(row, 2, row, 2).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 2, row, 2).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
        worksheet.SelectedRange(row, 2, row, 2).Style.Font.Color.SetColor(Color.White)

        worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin

        worksheet.Cells(row, 2).Value = "Liquidación al"

        worksheet.Cells(row, 3).Value = Now.ToShortDateString.ToString()
        worksheet.Cells(row, 3).Style.Font.Color.SetColor(Color.Red)
        worksheet.Cells(row, 3).Style.Font.Bold = True
        worksheet.SelectedRange(row, 4, row, 6).Merge = True


        worksheet.SelectedRange(row, 4, row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 4, row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)

        worksheet.SelectedRange(row, 4, row, 6).Style.Font.Color.SetColor(Color.White)
        worksheet.SelectedRange(row, 4, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        worksheet.Cells(row, 4).Value = "Pendiente"

        row += 1
        worksheet.SelectedRange(row, 2, row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 2, row, 6).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)

        worksheet.SelectedRange(row, 2, row, 6).Style.Font.Color.SetColor(Color.White)
        worksheet.SelectedRange(row, 2, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

        worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin

        worksheet.Cells(row, 2).Value = "Concepto"
        worksheet.Cells(row, 3).Value = "Dias"
        worksheet.Cells(row, 4).Value = "VV"
        worksheet.Cells(row, 5).Value = "IGV"
        worksheet.Cells(row, 6).Value = "Total"


        row = row + 1

        For x = 0 To DSResumen.Tables.Count - 1
            Dim dtTabla As DataTable = DSResumen.Tables(x)
            For Each rw As DataRow In dtTabla.Rows
                For Each cl As DataColumn In dtTabla.Columns
                    If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                        RowData = rw(cl.ColumnName).ToString()
                        worksheet.Cells(row, col).Value = RowData.Trim().ToString()
                        worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
                        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
                        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
                        worksheet.SelectedRange(row, 2, row, 3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        worksheet.SelectedRange(row, 2, row, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
                        worksheet.SelectedRange(row, 4, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
                    End If
                    col += 1
                Next
                row += 1
                col = 2
            Next
            worksheet.Cells(row, 2).Value = IIf(x = 0, "Total Opción de Compra", "Total Comisiones")
            worksheet.SelectedRange(row, 2, row, 2).Style.Font.Bold = True

            For Each i As DataRow In dtTabla.Rows
                SumaVV = SumaVV + CDbl(IIf(String.IsNullOrEmpty(i.Item("VV").ToString()), 0, i.Item("VV").ToString()))
                SumaIGVx = SumaIGVx + CDbl(IIf(String.IsNullOrEmpty(i.Item("IGV").ToString()), 0, i.Item("IGV").ToString()))
                SumaTotal = SumaTotal + CDbl(IIf(String.IsNullOrEmpty(i.Item("Total").ToString()), 0, i.Item("Total").ToString()))

                worksheet.Cells(row, 4).Value = SumaVV
                worksheet.Cells(row, 5).Value = SumaIGVx
                worksheet.Cells(row, 6).Value = SumaTotal


            Next

            'Guarda Totales
            SumaVVTotal += SumaVV
            SumaIGVxTotal += SumaIGVx
            SumaTotalx += SumaTotal
            worksheet.SelectedRange(row, 4, row, 6).Style.Font.Bold = True
            worksheet.SelectedRange(row, 4, row, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right

            row += 1
            worksheet.SelectedRange(row, 2, row, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
            worksheet.SelectedRange(row, 2, row, 6).Style.Fill.BackgroundColor.SetColor(Color.White)
            worksheet.SelectedRange(row, 2, row, 6).Merge = True
            row += 1

            SumaVV = 0
            SumaIGVx = 0
            SumaTotal = 0
        Next
        'Fin Tabla Resumen
        row += 1
        worksheet.SelectedRange(row, 2, row, 3).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 2, row, 3).Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue)
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.BorderAround(ExcelBorderStyle.Thin)
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Left.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 2, row, 6).Style.Border.Right.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 2, row, 3).Style.Font.Color.SetColor(Color.White)
        worksheet.SelectedRange(row, 4, row, 6).Style.Font.Bold = True
        worksheet.SelectedRange(row, 2, row, 3).Merge = True

        worksheet.Cells(row, 2).Value = "Total a Pagar USD"

        worksheet.Cells(row, 4).Value = SumaVVTotal
        worksheet.Cells(row, 5).Value = SumaIGVxTotal
        worksheet.Cells(row, 6).Value = SumaTotalx


        pck.Save()
        Response.Redirect("../temp/" + strNombreArchivo, False)
        'File.Delete(newFile.ToString())

    End Sub

#End Region

#Region "Documentos"

    ''' <summary>
    ''' Listado de Documentos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoInsDesembolsoDocumento(ByVal pPageSize As Integer, _
                                                         ByVal pCurrentPage As Integer, _
                                                         ByVal pSortColumn As String, _
                                                         ByVal pSortOrder As String, _
                                                         ByVal pstrCodInstruccion As String, _
                                                         ByVal pstrCodContrato As String _
                                                       ) As JQGridJsonResponse

        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

        Try

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoDoc As New EGCC_InsDesembolsoDoc
            Dim strEGCC_InsDesembolsoDoc As String
            With objEGCC_InsDesembolsoDoc
                .CodInstruccionDesembolso = GCCUtilitario.NullableString(pstrCodInstruccion)
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodContrato)
            End With
            strEGCC_InsDesembolsoDoc = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoDoc)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInstruccionDesembolsoDoc(pPageSize, _
                                                                                                                                                       pCurrentPage, _
                                                                                                                                                       pSortColumn, _
                                                                                                                                                       pSortOrder, _
                                                                                                                                                       strEGCC_InsDesembolsoDoc))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCotizacion.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(dtCotizacion.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCotizacion)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Eliminar DocumentoCometario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaDocumentoComentario(ByVal pstrCodInstruccion As String, _
                                                      ByVal pstrCodContrato As String, _
                                                      ByVal pstrCodigoDocumento As String _
                                                       ) As String

        ''Variables
        Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx

        Try

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoDoc As New EGCC_InsDesembolsoDoc
            Dim strEGCC_InsDesembolsoDoc As String
            With objEGCC_InsDesembolsoDoc
                .CodInstruccionDesembolso = GCCUtilitario.NullableString(pstrCodInstruccion)
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Codigodocumento = GCCUtilitario.NullableString(pstrCodigoDocumento)
                .Audestadologico = 0
            End With
            strEGCC_InsDesembolsoDoc = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoDoc)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objLInstruccionDesembolsoTx.EliminarInstruccionDesembolsoDoc(strEGCC_InsDesembolsoDoc)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

    Public Shared Function fGenerarQuotationInput(ByRef argInput As lpcCronograma.QuotationInput, ByVal pobjEGcc_cotizacion As EGCC_Liquidacion) As Boolean
        Try

            'Valida Tipo Cronograma
            Dim strTipoCronograma As String = pobjEGcc_cotizacion.Codigotipocronograma
            Dim strTipoCronogramaLcp As String = ""
            If strTipoCronograma.Trim().Equals(GCCConstante.C_CODIGO_TIPO_CRONOGRAMA_CAPITAL_CONSTANTE) Then
                strTipoCronogramaLcp = lpcCronograma.QuotationEnums.Enum_Tp_cronograma.Capital_constante
            Else
                strTipoCronogramaLcp = lpcCronograma.QuotationEnums.Enum_Tp_cronograma.Cuota_constante
            End If

            'Valida Periodicidad
            Dim strTipoPeriodicidad As String = pobjEGcc_cotizacion.Codigoperiodicidad
            Dim strTipoPeriodicidadLcp As String = ""
            If strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_ANUAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Anual
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_MENSUAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Mensual
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_SEMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Semestral
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_TRIMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Trimestral
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_BIMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Bimestral
            End If

            'Valida FrecuenciaPago
            Dim strFrecuenciaPago As String = pobjEGcc_cotizacion.Codigofrecuenciapago
            Dim strFrecuenciaPagoLcp As Integer = 0
            If strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_FIJA) Then
                strFrecuenciaPagoLcp = 1
            End If

            'Valida Cantidad Dias
            Dim intCantDias As Integer = 0
            If strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_VARIABLE) Then
                intCantDias = 360
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_VARIABLE) Then
                intCantDias = 60
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_VARIABLE) Then
                intCantDias = 30
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_VARIABLE) Then
                intCantDias = 180
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_VARIABLE) Then
                intCantDias = 90
            End If

            'Setea Parametros para el Cronograma
            With argInput
                .Tp_cronograma = strTipoCronogramaLcp
                .Im_prestamo = pobjEGcc_cotizacion.SaldoCapitalPendiente - pobjEGcc_cotizacion.AmortizacionCapital + pobjEGcc_cotizacion.InteresCorrido 'TODO: pobjEGcc_cotizacion.Riesgoneto
                .De_tea = pobjEGcc_cotizacion.Teaporc
                .De_plazo = pobjEGcc_cotizacion.Numerocuotas

                'If Not pobjEGcc_cotizacion.Fechamaxactivacion.HasValue Then
                .Dt_desembolso = pobjEGcc_cotizacion.FechaValor
                'Else
                '.Dt_desembolso = pobjEGcc_cotizacion.Fechamaxactivacion
                'End If

                .Dt_primer_vcmto = pobjEGcc_cotizacion.FechaPrimerVencimiento

                '.Il_cuota_doble = CBool(Me.selIl_cuota_doble.SelectedValue)
                .Tp_frecuencia = strTipoPeriodicidadLcp
                .Il_fijo = strFrecuenciaPagoLcp
                .De_plazo_gracia = pobjEGcc_cotizacion.Plazograciacuota
                .CostoFondo = 0 'TODO: Validar GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Costofondoporc)
                .PorcCuotaInicial = 0 'TODO: Validar GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Cuotainicialporc)
                '.PrecioVenta = 900 'TODO: Validar pobjEGcc_cotizacion.Precioventa
                '.ValorIgv = 100 'TODO: Validar pobjEGcc_cotizacion.Valorventaigv ' OJO :: Monto del IGV
                .Moneda = pobjEGcc_cotizacion.CodMoneda
                '.RiesgoNeto = pobjEGcc_cotizacion.SaldoCapitalPendiente 'TODO: pobjEGcc_cotizacion.Riesgoneto
                .MostrarTEACartas = 1 'pobjEGcc_cotizacion.Mostrarteacartas
                .MostrarOpcCompras = 1 'IIf(Me.chkMostrarOpcion.Checked = True, 1, 0) => no hay campo
                .MostrarComisionAct = 1 'pobjEGcc_cotizacion.Mostrarmontocomision
                .MostrarEstructuracionCartas = 1 'IIf(Me.chkMostrarComEstructuracion.Checked = True, 1, 0) => no hay campo
                .ImporteCuotaIni = pobjEGcc_cotizacion.AmortizacionCapital 'GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecuotainicial)
                .Observacion = "" 'pobjEGcc_cotizacion.Otrascomisiones

                'If Not pobjEGcc_cotizacion.FechaOfertaValida.HasValue Then
                .FechaValidez = Now
                'Else
                '.FechaValidez = pobjEGcc_cotizacion.FechaOfertaValida
                'End If


                '.TipoBien = pobjEGcc_cotizacion.Codigoclasificacionbien 'Verificar
                '.OpcCompra = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importeopcioncompra)
                '.ComActivacion = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecomisionactivacion)
                '.ComEstruc = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecomisionestructuracion)

                '.TipoSeguro = TODO pobjEGcc_cotizacion.Codigobientiposeguro
                .Im_seguro = pobjEGcc_cotizacion.SaldoSeguroPendiente + pobjEGcc_cotizacion.InteresCorridoSeguro 'GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Bienimporteprima)
                .De_plazo_Seg = GCCUtilitario.CheckInt(pobjEGcc_cotizacion.Numerocuotas)

                'If pobjEGcc_cotizacion.Codigodesgravamentiposeguro <> 0 Then
                .SeguroDes = 0 'GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Desgravamenimporteprima)
                .De_plazo_Seg_Des = 0 'GCCUtilitario.CheckInt(pobjEGcc_cotizacion.Desgravamennrocuotasfinanciar)
                'End If

                'IBK - RPH se agrega el tipo de Gracia
                .TipoGracia = pobjEGcc_cotizacion.Codigotipograciacuota
                'Fin

            End With
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function PreparaCronograma(ByRef dtCalendarioNuevo As DataTable, ByVal pdtbCronograma As DataTable, ByVal pobjEGcc_cotizacion As EGCC_Liquidacion, ByVal booValidaPrimeraFila As Boolean) As ListEGcc_cotizacioncronograma

        Try
            'Declara Variables
            Dim objCotizacionNTx As New LCotizacionNTx
            Dim objListECronograma As New ListEGcc_cotizacioncronograma

            'Valida Cuota Inicial
            Dim decCuotaInicial As Decimal = 0 'TODO GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecuotainicial)
            Dim blnMuestraFila As Boolean = True

            Dim NumCuotaCalendario As Integer

            If (dtCalendarioNuevo.Rows.Count = 0) Then
                NumCuotaCalendario = 1
            Else
                NumCuotaCalendario = dtCalendarioNuevo.Rows(dtCalendarioNuevo.Rows.Count - 1).Item("NumCuotaCalendario") + 1
            End If

            'Valida si existe
            Dim first As Boolean = True

            If pdtbCronograma.Rows.Count > 0 Then

                For Each oRow As DataRow In pdtbCronograma.Rows

                    Dim objECronograma As DataRow = dtCalendarioNuevo.NewRow()

                    With objECronograma
                        .Item("NumCuotaCalendario") = NumCuotaCalendario
                        .Item("FechaVencimientoCuota") = IIf(first, pobjEGcc_cotizacion.FechaValor.ToString("dd/MM/yyyy"), GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString()).ToString("dd/MM/yyyy"))
                        .Item("CantDiasCuota") = GCCUtilitario.StringToInteger(oRow.Item("Nu_dias").ToString())
                        .Item("MontoSaldoAdeudado") = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString())
                        .Item("MontoInteres") = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString())
                        .Item("MontoPrincipal") = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString())
                        .Item("MontoTotalPago") = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString())
                        .Item("MontoSaldoSeguro") = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString())
                        .Item("MontoInteresSeguro") = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString())
                        .Item("MontoPrincipalSeguro") = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString())
                        .Item("MontoTotalSeguro") = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString())

                        '.SaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString())
                        '.InteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString())
                        '.PrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString())
                        '.CuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString())

                        .Item("MontoTotalPagar") = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString())
                        .Item("MontoTotalIGV") = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString())
                        .Item("Estado") = "Pendiente"

                        'Mostrar
                        '.SFechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                        '.SMontosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SMontointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SMontoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SMontototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SSaldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SInteressegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SPrincipalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SMontocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SSaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SInteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SPrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SCuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.SMontototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString()).ToString(GCCConstante.C_FormatMiles)
                        '.STotalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString()).ToString(GCCConstante.C_FormatMiles)

                        '.Audestadologico = 1
                        '.AudFechaRegistro = 	
                        '.AudFechaModificacion = 	
                        '.Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        '.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                    End With

                    first = False

                    If objECronograma.Item("MontoTotalPagar") > 0 Then
                        dtCalendarioNuevo.Rows.Add(objECronograma)
                        NumCuotaCalendario = NumCuotaCalendario + 1
                    End If
                Next

                Return objListECronograma

            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class

