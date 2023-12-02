Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data.SqlClient

Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Pagos_frmPagoConceptoRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPagoConceptoRegistro.aspx.vb")
#Region "Eventos"
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
                Dim strNumSecRecuperacion As String = Request.QueryString("hddNumSecRecuperacion")

                GCCUtilitario.CargarComboMoneda(Me.cmbCodMoneda)
                GCCUtilitario.CargarComboMoneda(Me.cmbCodMonedaCargo)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoRecuperacion, GCCConstante.C_TABLAGENERICA_ESTADO_RECUPERACION)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbConcepto, GCCConstante.C_TABLAGENERICA_CONCEPTOS)

                If strCodSolicitudCredito Is Nothing Then

                    'Eventos
                    txtNroContrato.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imgBsqContrato.UniqueID + "').click();return false;}} else {return true}; ")

                    Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_NUEVO
                    Me.hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO

                    Me.txtFechaValor.Value = Date.Today.ToString("dd/MM/yyyy")
                Else
                    Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_EDITAR
                    Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                    'Datos Generales
                    ObtieneDatosContrato(strCodSolicitudCredito)
                    ObtieneDatosConceptoPago(strCodSolicitudCredito, strNumSecRecuperacion)
                End If

                hddCodSolicitudCredito.Value = strCodSolicitudCredito
                hddNumSecRecuperacion.Value = strNumSecRecuperacion
                hddPerfilUsuario.Value = GCCSession.PerfilUsuario
                If (GCCSession.CodigoUsuario = "B11797") Then
                    hddPerfilUsuario.Value = "1"
                End If
            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub
#End Region


#Region "WebMétodos"

    <WebMethod()> _
    Public Shared Function ObtenerConceptoEspecifico(ByVal pstrCodContrato As String, _
                                                ByVal pstrSecfinanciamiento As Integer) As EGcc_otroconcepto
        Dim oLwsPagosNTx As New LPagosNTx
        Dim odtbObtener As New DataTable
        Dim oEotroconcepto As New EGcc_otroconcepto
        Try
            With oEotroconcepto
                .Codsolicitudcredito = pstrCodContrato
                .Secfinanciamiento = pstrSecfinanciamiento
            End With
            odtbObtener = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                 oLwsPagosNTx.ObtenerConceptoEspecifico(GCCUtilitario.SerializeObject(Of EGcc_otroconcepto)(oEotroconcepto)))
            If odtbObtener.Rows.Count > 0 Then
                For Each odr As DataRow In odtbObtener.Rows
                    With oEotroconcepto
                        .Importe = GCCUtilitario.CheckDecimal(odr("MontoComision").ToString())
                        .Importeigv = GCCUtilitario.CheckDecimal(odr("MontoIGV").ToString())
                        .Codigootroconcepto2 = IIf(String.IsNullOrEmpty(odr("Clave1").ToString.Trim()), "", odr("Clave1").ToString.Trim())
                    End With
                    Exit For
                Next
            End If
            Return oEotroconcepto
        Catch ex As Exception
            Throw ex
        Finally
            odtbObtener.Dispose()
            oLwsPagosNTx = Nothing
        End Try
    End Function
    <WebMethod()> _
       Public Shared Function ListadoPagoConceptoxNumeroSecuencia(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pstrNroContrato As String, _
                                                ByVal pstrSecfinanciamiento As Integer) As JQGridJsonResponse


        'Variables
        Dim objLPagosNTx As New LPagosNTx

        Try

            'Valida Campos

            'Inicializa Objeto
            Dim objEGCC_PagoConcepto As New EGcc_otroconcepto
            'Dim strEGCC_PagoConcepto As String
            With objEGCC_PagoConcepto
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Secfinanciamiento = pstrSecfinanciamiento
            End With


            'Ejecuta Consulta
            Dim dtPagoConcepto As New DataTable
            dtPagoConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoConceptoxNumeroSecuencia(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       GCCUtilitario.SerializeObject(Of EGcc_otroconcepto)(objEGCC_PagoConcepto)))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtPagoConcepto.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtPagoConcepto.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtPagoConcepto.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtPagoConcepto)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    <WebMethod()> _
      Public Shared Function ListadoPagoConceptoxNumeroSecuenciaTemporal(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pstrNroContrato As String, _
                                               ByVal pstrNumSecuencia As Integer, _
                                               ByVal pstrNumSecuenciaAutorizacion As String) As JQGridJsonResponse


        'Variables
        Dim objLPagosNTx As New LPagosNTx

        Try

            'Valida Campos

            'Inicializa Objeto
            Dim objEGCC_PagoConcepto As New EGcc_otroconcepto
            'Dim strEGCC_PagoConcepto As String
            With objEGCC_PagoConcepto
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Secfinanciamiento = pstrNumSecuencia
                .NumSecuenciaAutorizacion = pstrNumSecuenciaAutorizacion
            End With


            'Ejecuta Consulta
            Dim dtPagoConcepto As New DataTable
            dtPagoConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoConceptoxNumeroSecuenciaTemporal(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       GCCUtilitario.SerializeObject(Of EGcc_otroconcepto)(objEGCC_PagoConcepto)))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtPagoConcepto.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtPagoConcepto.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtPagoConcepto.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtPagoConcepto)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Private Shared Function ObtenerTipoCambio(ByVal strMonedaBusq As String, _
                                               ByVal strFecha As String, _
                                               ByVal strTipoModalidaCambio As String) As String
        Dim oLwsTipoCambioNtx As New LUtilNTX
        Dim odtbDatos As New DataTable
        Dim strResult As String = ""
        Try
            odtbDatos = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTipoCambioNtx.ObtenerTipoCambio(strMonedaBusq, strTipoModalidaCambio, strFecha))
            If odtbDatos.Rows.Count = 0 Then
                strResult = String.Concat("0$0")
            Else
                strResult = String.Concat(odtbDatos.Rows(0).Item("MontoValorVenta").ToString, "$", odtbDatos.Rows(0).Item("MontoValorCompra").ToString)
            End If
            odtbDatos = Nothing

            Return strResult
        Catch ex As Exception
            Return "0"
        Finally
            oLwsTipoCambioNtx = Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function EliminaPagoConcepto(ByVal pstrCodContrato As String) As Boolean
        Try
            Dim oLwsPagosTx As New LPagosTx
            Dim oEGcc_otroconcepto As New EGcc_otroconcepto
            Dim strEliminaotroconcepto As String
            With oEGcc_otroconcepto
                .Codsolicitudcredito = pstrCodContrato
            End With

            strEliminaotroconcepto = GCCUtilitario.SerializeObject(oEGcc_otroconcepto)
            Dim intResult As Boolean = oLwsPagosTx.EliminaPagoConcepto(strEliminaotroconcepto)

            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function GrabaConceptoDetalleTemporal(ByVal pstrNroContrato As String, ByVal pstrCodConcepto As String, _
                                                        ByVal pstrDescConcepto As String, ByVal pdecMonto As String, _
                                                        ByVal pdecMontoIGV As String, ByVal pstrNumSecuencia As Integer, _
                                                        ByVal pstrNumSecuenciaAutorizacion As String) As Boolean
        Try
            Dim oLwsPagosTx As New LPagosTx
            Dim oEGcc_otroconcepto As New EGcc_otroconcepto
            Dim strAgregarotroconcepto As String
            With oEGcc_otroconcepto
                .Codsolicitudcredito = pstrNroContrato
                .Codigootroconcepto2 = pstrCodConcepto
                .Importe = GCCUtilitario.CheckDecimal(pdecMonto)
                .Importeigv = GCCUtilitario.CheckDecimal(pdecMontoIGV)
                .Secfinanciamiento = IIf(Not String.IsNullOrEmpty(pstrNumSecuencia), 0, pstrNumSecuencia)
                .NumSecuenciaAutorizacion = pstrNumSecuenciaAutorizacion
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            strAgregarotroconcepto = GCCUtilitario.SerializeObject(oEGcc_otroconcepto)
            Dim intResult As Boolean = oLwsPagosTx.GrabaConceptoDetalleTemporal(strAgregarotroconcepto)

            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

            Throw ex

        End Try
    End Function
    <WebMethod()> _
   Public Shared Function ActualizaConceptoDetalleTemporal(ByVal pstrNroContrato As String, ByVal pstrCodConcepto As String, _
                                                       ByVal pstrDescConcepto As String, ByVal pdecMonto As String, _
                                                       ByVal pdecMontoIGV As String, ByVal pstrNumSecuencia As Integer, _
                                                        ByVal pstrNumSecuenciaAutorizacion As String) As Boolean
        Try
            Dim oLwsPagosTx As New LPagosTx
            Dim oEGcc_otroconcepto As New EGcc_otroconcepto
            Dim strAgregarotroconcepto As String
            With oEGcc_otroconcepto
                .Codsolicitudcredito = pstrNroContrato
                .Codigootroconcepto2 = pstrCodConcepto
                .Importe = GCCUtilitario.ConvierteValorBien(pdecMonto)
                .Importeigv = GCCUtilitario.ConvierteValorBien(pdecMontoIGV)
                .Secfinanciamiento = pstrNumSecuencia
                .NumSecuenciaAutorizacion = pstrNumSecuenciaAutorizacion
                .CodUsuario = GCCSession.CodigoUsuario
            End With

            strAgregarotroconcepto = GCCUtilitario.SerializeObject(oEGcc_otroconcepto)
            Dim intResult As Boolean = oLwsPagosTx.ActualizaConceptoDetalleTemporal(strAgregarotroconcepto)

            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' Ejecuta Pago de Conceptos 
    ''' </summary>
    <WebMethod()> _
    Public Shared Function EjecutaPagoConcepto(ByVal pstrCodOperacionActiva As String, _
                                                ByVal pstrNumSecRecuperacion As String, _
                                                ByVal pstrCodAutorizacion As String, _
                                                ByVal pstrFechaValorRecuperacion As String, _
                                                ByVal pstrCodMoneda As String, _
                                                ByVal pstrCodComisionTipo As String, _
                                                ByVal pstrMontoReembolso As String, _
                                                ByVal pstrMontoIGVReembolso As String, _
                                                ByVal pstrMontoComision As String, _
                                                ByVal pstrMontoIGV As String, _
                                                ByVal pstrTipoViaCobranza As String, _
                                                ByVal pstrFlagCuentaPropia As String, _
                                                ByVal pstrCodUnicoClienteCargo As String, _
                                                ByVal pstrCodMonedaCargo As String, _
                                                ByVal pstrTipoCuenta As String, _
                                                ByVal pstrNroCuenta As String, _
                                                ByVal pstrFlagNuevo As String) As ECreditoRecuperacion


        'Variables
        Dim objPagosTx As New LPagosTx
        Dim objPagosNTx As New LPagosNTx
        Dim objUtilNTx As New LUtilNTX

        Try
            'Inicializa Objeto
            Dim objECreditoRecuperacion As New ECreditoRecuperacion
            Dim strECreditoRecuperacion As String

            With objECreditoRecuperacion
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrCodOperacionActiva)
                If (pstrFlagNuevo = "1") Then
                    .NumSecRecuperacion = -1
                Else
                    .NumSecRecuperacion = Val(pstrNumSecRecuperacion)
                End If

                .TipoRubroFinanciamiento = "000"
                .CodIfi = "9999"
                .TipoRecuperacion = "C"

                .CodUsuario = GCCSession.CodigoUsuario()
                .FechaValorRecuperacion = CDate(pstrFechaValorRecuperacion)
                .TipoViaCobranza = pstrTipoViaCobranza

                .EstadoRecuperacion = "I"
                .CodigoConcepto = pstrCodComisionTipo
                .CodMoneda = pstrCodMoneda
                .MontoReembolso = Val(pstrMontoReembolso)
                .MontoIGVReembolso = Val(pstrMontoIGVReembolso)
                .MontoComision = Val(pstrMontoComision)
                .MontoIGV = Val(pstrMontoIGV)
                .FlagCuentaPropia = pstrFlagCuentaPropia
                .CodUnicoClienteCargo = pstrCodUnicoClienteCargo

                .CodMonedaCargo = pstrCodMonedaCargo
                .TipoCuenta = pstrTipoCuenta
                .NroCuenta = pstrNroCuenta

                .TipoExtorno = ""
                .TipoPrepago = ""
                .TipoAplicacionPrepago = ""
                .TipoPrelacion = "N"

                'Solo para anular en ventanilla:
                .CodAutorizacionRecuperacion = Val(pstrCodAutorizacion)
            End With

            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            'Validar que Fecha Valor sea >= Fecha Hoy
            'Dim odtbFechaCierre As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ObtenerFechaCierre("CO"))
            'Dim strFechaHoy As String = odtbFechaCierre.Rows(0).Item("FechaHoy").ToString()

            'pstrFechaValorRecuperacion = CDate(pstrFechaValorRecuperacion).ToString("yyyyMMdd")
            'If pstrFechaValorRecuperacion < strFechaHoy Then
            'objECreditoRecuperacion.CodError = 1
            'objECreditoRecuperacion.MsgError = "La fecha valor no puede ser menor que la fecha de proceso actual(" & strFechaHoy & "). No se realizó ningún cambio."
            'Return objECreditoRecuperacion
            'End If

            'Si se modifica concepto se debe anular en ventanilla:
            If pstrFlagNuevo = "0" Then
                Dim strTramaTransactor As String = objPagosNTx.ObtenerTramaAutorizacionPagosVentanilla(strECreditoRecuperacion, "D")
                Dim strTramaRespuesta As String = ""

                Dim numIntentos As Integer = 0

                Dim bResultado As Boolean = False '
                While numIntentos < 3 And bResultado = False
                    numIntentos = numIntentos + 1
                    bResultado = objPagosTx.TransaccionGINA(strTramaTransactor, strTramaRespuesta)
                End While

                'Existe en ventanilla y no se puede anular
                If bResultado = False And strTramaRespuesta.Substring(8, 2) <> "13" Then
                    objECreditoRecuperacion.CodError = 1
                    objECreditoRecuperacion.MsgError = "No se puede modificar el concepto. Error de conexion con host: " + strTramaRespuesta
                    Return objECreditoRecuperacion
                End If
            End If

            'Ingresa Pago de Conceptos
            Dim dtRetornoIngresoPago As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objPagosTx.IngresarPagoConceptos(strECreditoRecuperacion))

            If dtRetornoIngresoPago.Rows.Count <> 1 Then
                objECreditoRecuperacion.CodError = 1
                objECreditoRecuperacion.MsgError = "Error al ingresar el pago de conceptos."
                Return objECreditoRecuperacion
            End If

            objECreditoRecuperacion.NumSecRecuperacion = dtRetornoIngresoPago.Rows(0).Item("NumSecRecuperacion")
            objECreditoRecuperacion.CodAutorizacionRecuperacion = dtRetornoIngresoPago.Rows(0).Item("CodAutorizacionRecuperacion").ToString.Trim
            strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)

            If pstrTipoViaCobranza = "V" Then
                Dim strTramaTransactor As String = objPagosNTx.ObtenerTramaAutorizacionPagosVentanilla(strECreditoRecuperacion, "A")
                Dim strTramaRespuesta As String = ""

                Dim numIntentos As Integer = 0

                Dim bResultado As Boolean = False

                While numIntentos < 3 And bResultado = False
                    numIntentos = numIntentos + 1
                    bResultado = objPagosTx.TransaccionGINA(strTramaTransactor, strTramaRespuesta)
                End While

                If bResultado = False Then
                    objECreditoRecuperacion.CodError = 1
                    objECreditoRecuperacion.MsgError = "Error en Interface Transactor. " & strTramaRespuesta

                    'Cambiar estado a enviado a Error Transaccion
                    objECreditoRecuperacion.EstadoRecuperacion = "X"
                    strECreditoRecuperacion = GCCUtilitario.SerializeObject(objECreditoRecuperacion)
                    objPagosTx.ActualizarEstadoPagoCuotas(strECreditoRecuperacion)
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
    End Function
#End Region

#Region "Metodos"
    Private Sub ObtieneDatosContrato(ByVal strCodSolicitudCredito As String)

        Try
            'Variables
            Dim objLContratoNTx As New LContratoNTx

            'Ejecuta Consulta
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLContratoNTx.ObtenerContrato(strCodSolicitudCredito))

            'Valida si existe
            If dtContrato.Rows.Count > 0 Then

                txtNroContrato.Value = dtContrato.Rows(0).Item("CodSolicitudCredito").ToString
                txtCuCliente.Value = dtContrato.Rows(0).Item("CodUnico").ToString
                txtRazonSocial.Value = dtContrato.Rows(0).Item("NombreSubPrestatario").ToString
                txtTipoPersona.Value = dtContrato.Rows(0).Item("NombreTipoPersona").ToString
                txtTipoDocumento.Value = dtContrato.Rows(0).Item("NombreTipoDocIdentificacion").ToString
                txtNumeroDocumento.Value = dtContrato.Rows(0).Item("NroDocIdentificacion").ToString
                txtTipoContrato.Value = dtContrato.Rows(0).Item("SubTipoContrato").ToString
                txtMoneda.Value = dtContrato.Rows(0).Item("NombreMonedaAPP").ToString
                hddCodMonedaContrato.Value = dtContrato.Rows(0).Item("CodMoneda").ToString
                txtEjecutivoLeasing.Value = dtContrato.Rows(0).Item("NombreEjecutivoLeasing").ToString
                txtEstadoContrato.Value = dtContrato.Rows(0).Item("EstadoContrato").ToString

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub ObtieneDatosConceptoPago(ByVal strCodSolicitudCredito As String, ByVal strSecfinanciamiento As Integer)

        Try
            'Variables
            Dim objLPagoConceptoNTx As New LPagosNTx
            Dim oECreditoRecuperacion As New EGcc_otroconcepto
            With oECreditoRecuperacion
                .Codsolicitudcredito = strCodSolicitudCredito
                .Secfinanciamiento = strSecfinanciamiento
            End With
            'Ejecuta Consulta
            Dim dtConcepto As New DataTable
            dtConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagoConceptoNTx.ListadoPagoConceptoDetalle(GCCUtilitario.SerializeObject(Of EGcc_otroconcepto)(oECreditoRecuperacion)))
            If dtConcepto.Rows.Count > 0 Then
                txtCodAutorizacionRecuperacion.Value = dtConcepto.Rows(0).Item("CodAutorizacionRecuperacion").ToString()
                txtNumSecRecuperacion.Value = dtConcepto.Rows(0).Item("NumSecRecuperacion").ToString()
                txtFechaRecuperacion.Value = GCCUtilitario.CheckDateString(dtConcepto.Rows(0).Item("FechaRecuperacion").ToString(), "C")
                txtFechaValor.Value = GCCUtilitario.CheckDateString(dtConcepto.Rows(0).Item("FechaValorRecuperacion").ToString(), "C")

                txtMontoComision.Value = dtConcepto.Rows(0).Item("MontoComision").ToString()
                txtMontoIGV.Value = dtConcepto.Rows(0).Item("MontoIGV").ToString()
                txtMontoReembolso.Value = dtConcepto.Rows(0).Item("MontoReembolso").ToString()
                txtMontoIGVReembolso.Value = dtConcepto.Rows(0).Item("MontoIGVReembolso").ToString()

                txtReajusteComision.Value = dtConcepto.Rows(0).Item("MontoReajusteComision").ToString()
                txtReajusteIGV.Value = dtConcepto.Rows(0).Item("MontoReajusteIGV").ToString()
                txtReajusteReembolso.Value = dtConcepto.Rows(0).Item("MontoReajusteReembolso").ToString()
                txtReajusteIGVReembolso.Value = dtConcepto.Rows(0).Item("MontoReajusteIGVReembolso").ToString()

                GCCUtilitario.SeleccionaCombo(cmbConcepto, dtConcepto.Rows(0).Item("CodComisionTipo").ToString.Trim)

                GCCUtilitario.SeleccionaCombo(cmbCodMoneda, dtConcepto.Rows(0).Item("CodMoneda").ToString.Trim)

                'Cargo en Cuenta
                GCCUtilitario.SeleccionaCombo(cmbTipoCuenta, dtConcepto.Rows(0).Item("TipoCuenta").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbCodMonedaCargo, dtConcepto.Rows(0).Item("CodMonedaCargo").ToString.Trim)
                Dim strNroCuenta As String = dtConcepto.Rows(0).Item("NroCuenta").ToString.Trim
                cmbNroCuenta.Items.Add(New ListItem(GCCUtilitario.formateaNroCuenta(strNroCuenta), strNroCuenta))
                cmbNroCuenta.Value = strNroCuenta

                'Flag Cuenta Propia
                hddFlagCuentaPropia.Value = dtConcepto.Rows(0).Item("FlagCuentaPropia").ToString
                If hddFlagCuentaPropia.Value = "" Then
                    hddFlagCuentaPropia.Value = "S"
                ElseIf hddFlagCuentaPropia.Value = "N" Then
                    txtCUClienteCargo.Value = dtConcepto.Rows(0).Item("CodUnicoClienteCargo").ToString.Trim
                End If

                'Identificacion por Ventanilla
                txtCodOperacionGINA.Value = dtConcepto.Rows(0).Item("CodOperacionGINA").ToString.Trim
                txtFechaProcesoPago.Value = GCCUtilitario.CheckDateString(dtConcepto.Rows(0).Item("FechaProcesoPago").ToString.Trim, "C")
                txtCodTerminalPago.Value = dtConcepto.Rows(0).Item("CodTerminalPago").ToString.Trim
                GCCUtilitario.SeleccionaCombo(cmbTiendaPago, dtConcepto.Rows(0).Item("CodTiendaPago").ToString.Trim)
                txtCodUsuarioPago.Value = dtConcepto.Rows(0).Item("CodUsuarioPago").ToString.Trim
                txtCodModoPago.Value = dtConcepto.Rows(0).Item("CodModoPago").ToString.Trim
                txtCodModoPago2.Value = dtConcepto.Rows(0).Item("CodModoPago2").ToString.Trim

                GCCUtilitario.SeleccionaCombo(cmbEstadoRecuperacion, dtConcepto.Rows(0).Item("EstadoRecuperacion").ToString.Trim)
                hddEstadoRecuperacion.Value = dtConcepto.Rows(0).Item("EstadoRecuperacion").ToString.Trim
                If hddEstadoRecuperacion.Value = "A" Or hddEstadoRecuperacion.Value = "E" Then
                    txtMotivo.Value = dtConcepto.Rows(0).Item("MotivoAnulacionExtorno").ToString.Trim
                Else
                    txtMotivo.Value = dtConcepto.Rows(0).Item("DescripcionCargo").ToString.Trim
                End If

                hddTipoViaCobranza.Value = dtConcepto.Rows(0).Item("TipoViaCobranza").ToString()

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function
#End Region

End Class
