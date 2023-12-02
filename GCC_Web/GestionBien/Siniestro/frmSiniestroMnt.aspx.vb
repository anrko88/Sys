Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_Siniestro_frmSiniestroMnt
    Inherits GCCBase

    Dim objLog As New GCCLog("frmSiniestroMnt.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2011
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

                'Setea Modo Transaccion
                Me.hddTipoTx.Value = GCCConstante.C_TX_NUEVO

                'Verifica Edicion
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                Dim strCodSiniestro As String = Request.QueryString("hddCodSiniestro")
                Dim strCodUnico As String = Request.QueryString("hddCodUnico")
                Dim strVer As String = Request.QueryString("hddVer")


                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodSiniestro.Value = strCodSiniestro
                Me.hddCodUnico.Value = strCodUnico
                Me.hddVer.Value = strVer

                'Carga Combos
                GCCUtilitario.CargarComboMoneda(cmbMonedaIndem)
                GCCUtilitario.CargarComboMoneda(cmbMonedaCuenta)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipo, GCCConstante.C_TABLAGENERICA_Siniestro_Tipo)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbSituacion, GCCConstante.C_TABLAGENERICA_Siniestro_Situacion)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbAplicacion, GCCConstante.C_TABLAGENERICA_Siniestro_Aplicacion)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTransferencia, GCCConstante.C_TABLAGENERICA_Siniestro_Transferencia)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbContrato, GCCConstante.C_TABLAGENERICA_Siniestro_Contrato)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbSeguro, GCCConstante.C_TABLAGENERICA_Siniestro_Seguro)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbOrigenCono, GCCConstante.C_TABLAGENERICA_Siniestro_Origen)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoBien, GCCConstante.C_TABLAGENERICA_Siniestro_EstadoBien)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoPoliza, GCCConstante.C_TABLAGENERICA_Siniestro_TipoPoliza)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbBancoEmite, GCCConstante.C_TABLAGENERICA_Siniestro_BancoEmite)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbAplicaFondo, GCCConstante.C_TABLAGENERICA_Siniestro_AplicaFondo)

                'Fecha Hoy
                Dim dtFecha As Date = Now
                Me.txtFechaSituacion.Value = dtFecha.ToString("dd/MM/yyyy")

                'Carga Datos Editar
                If Not strCodSiniestro Is Nothing Then
                    Me.hddTipoTx.Value = GCCConstante.C_TX_EDITAR
                    CargaEditarSiniestro(strCodContrato, strCodBien, strCodSiniestro)
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

#Region "WebMethods"

    ''' <summary>
    ''' Graba Siniestro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaSiniestro(ByVal pstrTipoTx As String, _
                                            ByVal pstrCodContrato As String, _
                                            ByVal pstrCodBien As String, _
                                            ByVal pstrCodSiniestro As String, _
                                            ByVal pstrNroSiniestro As String, _
                                            ByVal pstrFechaConoBanco As String, _
                                            ByVal pstrFechaConoLeasing As String, _
                                            ByVal pstrFechaSiniestro As String, _
                                            ByVal pstrTipo As String, _
                                            ByVal pstrFechaSituacion As String, _
                                            ByVal pstrSituacion As String, _
                                            ByVal pstrContrato As String, _
                                            ByVal pstrFechaAplicacion As String, _
                                            ByVal pstrAplicacion As String, _
                                            ByVal pstrFechaDescargoMunicipal As String, _
                                            ByVal pstrFechaTransparencia As String, _
                                            ByVal pstrTransferencia As String, _
                                            ByVal pstrOrigenCono As String, _
                                            ByVal pstrSeguro As String, _
                                            ByVal pstrChequeAseg As String, _
                                            ByVal pstrEstadoBien As String, _
                                            ByVal pstrNroPoliza As String, _
                                            ByVal pstrTipoPoliza As String, _
                                            ByVal pstrFechaIndem As String, _
                                            ByVal pstrMonedaIndem As String, _
                                            ByVal pstrMontoIndem As String, _
                                            ByVal pstrBancoEmite As String, _
                                            ByVal pstrAplicaFondo As String, _
                                            ByVal pstrNroCuenta As String, _
                                            ByVal pstrTipoCuenta As String, _
                                            ByVal pstrMonedaCuenta As String, _
                                            ByVal pstrCodUnico As String) As String

        Try
            Dim objESiniestro As New ESiniestro

            With objESiniestro
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.NullableString(pstrCodBien)
                .Secsiniestro = GCCUtilitario.NullableString(pstrCodSiniestro)
                .Codunico = GCCUtilitario.NullableString(pstrCodUnico)
                .Montoindemnizacion = GCCUtilitario.StringToDecimal(pstrMontoIndem)
                .Moneda = GCCUtilitario.NullableString(pstrMonedaIndem)
                .FecSiniestroStr = GCCUtilitario.NullableString(pstrFechaSiniestro)
                .Tipo = GCCUtilitario.NullableStringCombo(pstrTipo)
                .Situacion = GCCUtilitario.NullableStringCombo(pstrSituacion)
                .Aplicacion = GCCUtilitario.NullableStringCombo(pstrAplicacion)
                .Transferencia = GCCUtilitario.NullableStringCombo(pstrTransferencia)
                .Contrato = GCCUtilitario.NullableStringCombo(pstrContrato)
                .Seguro = GCCUtilitario.NullableStringCombo(pstrSeguro)
                .Observaciones = ""
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Origen = GCCUtilitario.NullableStringCombo(pstrOrigenCono)
                .FecSituacionStr = GCCUtilitario.NullableString(pstrFechaSituacion)
                .FecAplicacionStr = GCCUtilitario.NullableString(pstrFechaAplicacion)
                .FecConocimientoStr = GCCUtilitario.NullableString(pstrFechaConoLeasing)
                .FecRecIndemnizacionStr = GCCUtilitario.NullableString(pstrFechaIndem)
                .FecDescargoMunicipalStr = GCCUtilitario.NullableString(pstrFechaDescargoMunicipal)
                .FecTransferenciaStr = GCCUtilitario.NullableString(pstrFechaTransparencia)
                .NroChequeAseguradora = GCCUtilitario.NullableString(pstrChequeAseg)
                .CodEstadoBien = GCCUtilitario.NullableStringCombo(pstrEstadoBien)
                .NroPoliza = GCCUtilitario.NullableString(pstrNroPoliza)
                .CodTipoPoliza = GCCUtilitario.NullableStringCombo(pstrTipoPoliza)
                .CodBancoEmiteCheque = GCCUtilitario.NullableStringCombo(pstrBancoEmite)
                .CodAplicaFondo = GCCUtilitario.NullableStringCombo(pstrAplicaFondo)
                .NroCuenta = GCCUtilitario.NullableString(pstrNroCuenta)
                .CodTipoCuenta = GCCUtilitario.NullableStringCombo(pstrTipoCuenta)
                .CodMonedaCuenta = GCCUtilitario.NullableStringCombo(pstrMonedaCuenta)
                .FecConocimientoBancoStr = GCCUtilitario.NullableString(pstrFechaConoBanco)
                .CodTipoSiniestro = GCCConstante.C_TIPO_SINIESTRO_SINIESTRO
                '.CodDemanda = 
                .NroSiniestro = GCCUtilitario.NullableString(pstrNroSiniestro)
                .EstadoLogico = 1
            End With


            'Ejecuta Transaccion
            Dim objLSiniestroTx As New LSiniestroTx

            Dim blnResult As Boolean = False
            Dim strNumeroSiniestro As String = ""
            If pstrTipoTx.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strNumeroSiniestro = objLSiniestroTx.InsertarSiniestro(GCCUtilitario.SerializeObject(objESiniestro))
                blnResult = True
            Else
                objLSiniestroTx.ModificarSiniestro(GCCUtilitario.SerializeObject(objESiniestro))
                strNumeroSiniestro = pstrCodSiniestro
                blnResult = True
            End If

            'Valida Resultado
            If blnResult Then
                Return "1"
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Get Siniestro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2012
    ''' </remarks>
    Protected Sub CargaEditarSiniestro(ByVal pstrCodContrato As String, ByVal strCodBien As String, ByVal strCodSiniestro As String)

        Try

            Dim objSiniestroNTx As New LSiniestroNTx
            Dim msgError As String = ""

            'Inicializa Objeto
            Dim objESiniestro As New ESiniestro
            Dim strESiniestro As String
            With objESiniestro
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.CheckInt(strCodBien)
                .Secsiniestro = GCCUtilitario.CheckInt(strCodSiniestro)
            End With
            strESiniestro = GCCUtilitario.SerializeObject(objESiniestro)

            'Ejecuta Consulta
            Dim dtSiniestro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestroNTx.GetSiniestro(strESiniestro))

            'Valida si existe
            If dtSiniestro.Rows.Count > 0 Then

                Me.txtNroSiniestro.Value = dtSiniestro.Rows(0).Item("NroSiniestro").ToString
                Me.txtFechaConoBanco.Value = dtSiniestro.Rows(0).Item("FecConocimientoBanco").ToString
                Me.txtFechaConoLeasing.Value = dtSiniestro.Rows(0).Item("FecConocimiento").ToString
                Me.txtFechaSiniestro.Value = dtSiniestro.Rows(0).Item("FecSiniestro").ToString
                Me.txtFechaSituacion.Value = dtSiniestro.Rows(0).Item("FecSituacion").ToString
                Me.txtFechaAplicacion.Value = dtSiniestro.Rows(0).Item("FecAplicacion").ToString

                Me.txtFechaDescargoMunicipal.Value = dtSiniestro.Rows(0).Item("FecDescargoMunicipal").ToString
                Me.txtFechaTransparencia.Value = dtSiniestro.Rows(0).Item("FecTransferencia").ToString
                Me.txtChequeAseg.Value = dtSiniestro.Rows(0).Item("NroChequeAseguradora").ToString
                Me.txtNroPoliza.Value = dtSiniestro.Rows(0).Item("NroPoliza").ToString
                Me.txtFechaIndem.Value = dtSiniestro.Rows(0).Item("FecRecIndemnizacion").ToString
                Me.txtMontoIndem.Value = dtSiniestro.Rows(0).Item("MontoIndemnizacion").ToString
                Me.txtNroCuenta.Value = dtSiniestro.Rows(0).Item("NroCuenta").ToString

                GCCUtilitario.SeleccionaCombo(cmbTipo, dtSiniestro.Rows(0).Item("Tipo").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbSituacion, dtSiniestro.Rows(0).Item("Situacion").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbContrato, dtSiniestro.Rows(0).Item("Contrato").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbAplicacion, dtSiniestro.Rows(0).Item("Aplicacion").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTransferencia, dtSiniestro.Rows(0).Item("Transferencia").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbOrigenCono, dtSiniestro.Rows(0).Item("Origen").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbSeguro, dtSiniestro.Rows(0).Item("Seguro").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbEstadoBien, dtSiniestro.Rows(0).Item("CodEstadoBien").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoPoliza, dtSiniestro.Rows(0).Item("CodTipoPoliza").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbMonedaIndem, dtSiniestro.Rows(0).Item("Moneda").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbBancoEmite, dtSiniestro.Rows(0).Item("CodBancoEmiteCheque").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbAplicaFondo, dtSiniestro.Rows(0).Item("CodAplicaFondo").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbMonedaCuenta, dtSiniestro.Rows(0).Item("CodMonedaCuenta").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoCuenta, dtSiniestro.Rows(0).Item("CodTipoCuenta").ToString.Trim)

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
