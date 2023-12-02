Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class InsDesembolso_frmMediosPagoRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmMediosPagoRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
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

                Dim strNroContrato As String = Request.QueryString("cc")
                Dim strCodInsDesembolso As String = Request.QueryString("cid")
                Dim strCodAgrupacion As String = Request.QueryString("ca")
                Dim strCodGrupoHtml As String = Request.QueryString("cah")
                Dim strCodigoMonedaAgrupacion As String = Request.QueryString("cma")
                Dim strCodProveedor As String = Request.QueryString("pro")

                Dim strcu As String = Request.QueryString("cu")
                Dim strAccion As String = Request.QueryString("acc")  'Flag si es Editable
                'Inicio IBK - AAE
                Dim strCodMonCont As String = Request.QueryString("cmc")
                'Fin IBK

                hddcu.Value = strcu
                hddAccion.Value = strAccion
                'Inicio IBK - AAE - Cargo estado de ejecución
                hddCodEstadoEjecucion.Value = "01"
                hddCodMonedaContrato.Value = strCodMonCont
                'Fin IBK
                hddCodigoContrato.Value = strNroContrato
                hddCodigoInsDesembolso.Value = strCodInsDesembolso
                hddCodigoAgrupacion.Value = strCodAgrupacion
                hddCodigoGrupoHtml.Value = strCodGrupoHtml
                hddCodigoMonedaAgrupacion.Value = strCodigoMonedaAgrupacion
                hddCodProveedor.Value = strCodProveedor

                txtNota.Value = strNroContrato

                'Combos
                GCCUtilitario.CargarComboMoneda(cmbMonedaCuenta)
                GCCUtilitario.CargarComboMoneda(cmbMonedaPend)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbMedio, GCCConstante.C_TABLAGENERICA_Tipo_MedioPago)
                ' GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_Tipo_Cuentas)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbPagoComision, GCCConstante.C_TABLAGENERICA_Tipo_Pago_Comision)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)

                'CargaMedio
                CargaMedioPago(strNroContrato, strCodInsDesembolso, strCodAgrupacion, strCodigoMonedaAgrupacion, strCodProveedor)

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
    ''' Graba Cargo
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    ''' Inicio IBK - AAE - Agrego CodEstadoPago
    <WebMethod()> _
    Public Shared Function GrabaMedioPago(ByVal pstrCodContrato As String, _
                                            ByVal pstrCodInsDesembolso As String, _
                                            ByVal pstrCodAgrupacion As String, _
                                            ByVal pstrCodMoneda As String, _
                                            ByVal pstrMedio As String, _
                                            ByVal pstrMonedaCuenta As String, _
                                            ByVal pstrTipoCuenta As String, _
                                            ByVal pstrNroCuenta As String, _
                                            ByVal pstrCuentaBancaria As String, _
                                            ByVal pstrPendiente As String, _
                                            ByVal pstrMonedaPend As String, _
                                            ByVal pstrNota As String, _
                                            ByVal pstrEmisora As String, _
                                            ByVal pstrReceptora As String, _
                                            ByVal pstrNroDocumento As String, _
                                            ByVal pstrTipoDocumento As String, _
                                            ByVal pstrRazonSocial As String, _
                                            ByVal pstrPagoComision As String, _
                                            ByVal pstrMontoComision As String, _
                                            ByVal pstrCodProveedor As String, _
                                            ByVal pstrCodEjecucionPago As String, _
                                            ByVal pstrCargoNoDom As String, _
                                            ByVal pstrCtaCargoNoDom As String, _
                                            ByVal pstrAbonoNoDom As String, _
                                            ByVal pstrCtaAbonoNoDom As String) As String

        Try
            Dim objEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago

            With objEGCC_InsDesembolsoPago
                .Codmedioabono = GCCUtilitario.NullableString(pstrMedio)
                .Codmonedacuenta = GCCUtilitario.NullableString(pstrMonedaCuenta)
                .Codtipocuenta = GCCUtilitario.NullableString(pstrTipoCuenta)
                .Numero_cuenta = GCCUtilitario.NullableString(pstrNroCuenta.Trim())
                .Numcuentainterbancaria = GCCUtilitario.NullableString(pstrCuentaBancaria)
                .Pendiente = GCCUtilitario.NullableString(pstrPendiente)
                .Nota = GCCUtilitario.NullableString(pstrNota)
                .Emisora = GCCUtilitario.NullableString(pstrEmisora)
                .Receptora = GCCUtilitario.NullableString(pstrReceptora)
                .Codmonedapago = GCCUtilitario.NullableString(pstrCodMoneda)
                .Codproveedor = GCCUtilitario.NullableString(pstrCodProveedor)
                .Codmonedapendiente = GCCUtilitario.NullableString(pstrMonedaPend)
                .Codpagocomision = GCCUtilitario.NullableString(pstrPagoComision)
                .Comentario = ""
                .Adjunto = ""
                .Importecomision = GCCUtilitario.CheckDecimal(pstrMontoComision)
                .Codtipodocumento = GCCUtilitario.NullableString(pstrTipoDocumento)
                .Numerodocumento = GCCUtilitario.NullableString(pstrNroDocumento)
                .Razonsocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .Audestadologico = 1
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrCodInsDesembolso)
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Codagrupacion = GCCUtilitario.NullableString(pstrCodAgrupacion)
                .Codmonedaagrupacion = GCCUtilitario.NullableString(pstrCodMoneda)
                'Inicio IBK - AAE - Agrego Estado Medio de Pago}
                .CodEstadoEjecucionPago = GCCUtilitario.NullableString(pstrCodEjecucionPago)
                .CargoNoDom = GCCUtilitario.CheckDecimal(pstrCargoNoDom)
                .AbonoNoDom = GCCUtilitario.CheckDecimal(pstrAbonoNoDom)
                .CtaAbonoNoDom = GCCUtilitario.NullableString(pstrCtaAbonoNoDom)
                .CtaCargoNoDom = GCCUtilitario.NullableString(pstrCtaCargoNoDom)
                'Fin IBK    
            End With

            Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
            objLInstruccionDesembolsoTx.InsertarInsDesembolsoMedioPago(GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoPago))

            'Actualiza Listado
            ListaCargoAbono(pstrCodContrato, pstrCodInsDesembolso)

            Return "0"

        Catch ex As Exception
            Return "1"
        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="argFCDTIPOCUENTA"></param>
    ''' <param name="argFCDCODMONEDA"></param>
    ''' <param name="argFCDCODTIENDA"></param>
    ''' <param name="argFCDCODCATEGORIA"></param>
    ''' <param name="argFCDNUMCUENTA"></param>
    ''' <param name="pCodUnico"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Cambio la validación de cuentas
    <WebMethod()> _
  Public Shared Function ValidaCuentaST(ByVal argFCDTIPOCUENTA As String, _
                                        ByVal argFCDCODMONEDA As String, _
                                        ByVal argFCDCODTIENDA As String, _
                                        ByVal argFCDCODCATEGORIA As String, _
                                        ByVal argFCDNUMCUENTA As String, _
                                        ByVal pCodUnico As String, _
                                        ByVal pstrCodAgrupacion As String, _
                                        ByVal pstrCodProveedor As String, _
                                        ByVal pstrCodSolicitudCredito As String, _
                                        ByVal pstrCodMedioPago As String, _
                                        ByVal pstrTipoDocAgAd As String, _
                                        ByVal pstrNroDocAgAd As String) As String
        ', _
        'ByVal argFCDTIPOCUENTA2 As String, _
        'ByVal argFCDCODMONEDA2 As String, _
        'ByVal argFCDCODTIENDA2 As String, _
        'ByVal argFCDCODCATEGORIA2 As String, _
        'ByVal argFCDNUMCUENTA2 As String, _
        '                                ByVal pCodUnico As String

        Dim sTldDatosTran As String = String.Empty
        Dim sMensaje As String = ""
        Dim oLWebService As New LWebService
        'Inicio IBK - AAE - Defino Variables
        Dim tx As New LInstruccionDesembolsoNTx
        Dim strCU As String = pCodUnico
        Dim strTipoDoc As String
        Dim strNroDoc As String
        Dim argsUsuarioTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("USER_TLD")
        Dim argsAgenciaTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("AGENCIA_TLD")
        Dim flagError As Boolean = False

        'Dim sTldDatosTran2 As String
        Try
            'Inicio IBK - AAE - Obtnego CU según sea cliente o proveedor
            If ((pstrCodAgrupacion.Trim = "07") Or (pstrCodAgrupacion.Trim = "08") Or (pstrCodAgrupacion.Trim = "09") Or (pstrCodAgrupacion.Trim = "10") Or (pstrCodAgrupacion.Trim = "11") Or (pstrCodAgrupacion.Trim = "12") Or (pstrCodAgrupacion.Trim = "14")) Then
                strCU = pCodUnico
            Else
                'IBK - Si el medio de pago es cuenta lo obtengo, si es Pago agente de aduana, voy contra la info del agente de aduana
                If pstrCodMedioPago = "002" Then
                    Dim table As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(tx.obtenerInfoProveedor(pstrCodProveedor))
                    If (table.Rows.Count > 0) Then
                        strTipoDoc = table.Rows.Item(0).Item("CodigoTipoDocumento").ToString.Trim
                        strNroDoc = table.Rows.Item(0).Item("NumeroDocumento").ToString.Trim
                    Else
                        flagError = True
                    End If
                Else
                    strTipoDoc = pstrTipoDocAgAd
                    strNroDoc = pstrNroDocAgAd
                End If

                If Not flagError Then
                    Dim strMensaje As String
                    Dim strDatos As String
                    Dim strCUDummy As String = "0000000000"
                    Dim argsTrama As String = String.Concat(New String() {"20000", strCUDummy, strTipoDoc, strNroDoc, "000"})
                    If tx.callProgramaHost(argsTrama, argsUsuarioTld, argsAgenciaTld, "WIOR002", "ObtenerCU", strMensaje, strDatos) Then
                        strCU = Strings.Trim(Strings.Mid(strDatos, &H4F, 10))
                    Else
                        flagError = True
                    End If
                End If
            End If
            If flagError Then
                Return "1|El código único no es válido"
            End If
            'Fin IBK
            Dim strUlrws As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsFCDCuenta")

            sTldDatosTran = oLWebService.fstrConsultarCuenta(argFCDTIPOCUENTA, argFCDCODMONEDA, argFCDCODTIENDA, argFCDCODCATEGORIA, argFCDNUMCUENTA, strUlrws)
            Dim resultado1 As String = ""
            'Dim resultado2 As String = ""
            Dim resultadoSTServicio As String

            resultadoSTServicio = "1|(FCDO003)Función six_fastpartner retornar un Error: -10"

            If sTldDatosTran.Trim <> resultadoSTServicio.Trim Then

                Dim strTrans As String() = sTldDatosTran.Split(New Char() {"|"c}) 'sTldDatosTran.Split("|")
                If strTrans(0).ToString = "0" Then

                    If argFCDTIPOCUENTA = "IM" Then
                        resultado1 = "0|" + strTrans(16).ToString   '-- CORRIENTE 
                        If strTrans(16).ToString() <> strCU Then
                            resultado1 = "1|" + strTrans(16).ToString()
                        End If
                    Else
                        resultado1 = "0|" + strTrans(17).ToString   '-- AHORROS
                        If strTrans(17).ToString() <> strCU Then
                            resultado1 = "1|" + strTrans(17).ToString()
                        End If
                    End If

                Else
                    resultado1 = "1|" + strTrans(1).ToString
                End If


                'If argFCDNUMCUENTA2 <> "" Then
                '    sTldDatosTran2 = oLWebService.fstrConsultarCuenta(argFCDTIPOCUENTA2, argFCDCODMONEDA2, argFCDCODTIENDA2, argFCDCODCATEGORIA2, argFCDNUMCUENTA2, strUlrws)

                '    Dim strTrans2 As String() = sTldDatosTran2.Split(New Char() {"|"c}) ' sTldDatosTran2.Split("|")
                '    If strTrans2(0).ToString = "0" Then
                '        If argFCDTIPOCUENTA = "IM" Then
                '            resultado2 = "0|" + strTrans2(16).ToString   '-- CORRIENTE
                '            If strTrans2(16).ToString() <> pCodUnico Then
                '                resultado2 = "1|" + strTrans2(16).ToString()
                '            End If
                '        Else
                '            resultado2 = "0|" + strTrans2(17).ToString   '-- AHORROS
                '            If strTrans2(17).ToString() <> pCodUnico Then
                '                resultado2 = "1|" + strTrans2(17).ToString()
                '            End If
                '        End If

                '    Else
                '        resultado2 = "1|" + strTrans2(1).ToString
                '    End If
                '    'Else
                '    '    resultado2 = resultado1
                'End If


                'If argFCDNUMCUENTA2 <> "" Then

                '    If Left(resultado1, 1) = Left(resultado2, 1) Then
                '        If Left(resultado1, 1) = "1" And Left(resultado2, 1) = "1" Then
                '            Return "1|La Primera cuenta está errada, por favor verifique"
                '        Else
                '            Return resultado1
                '        End If

                '    Else
                '        If Left(resultado1, 1) = "1" Then
                '            Return "1|La Primera cuenta está errada, por favor verifique"
                '        End If
                '        If Left(resultado2, 1) = "1" Then
                '            Return "1|La Segunda cuenta está errada, por favor verifique"
                '        End If
                '        'Return "1|Cuenta Errada"
                '    End If
                'Else

                If Left(resultado1, 1) = "1" Then
                    Return "1|La cuenta está errada, por favor verifique"
                Else
                    Return resultado1
                End If
                ' End If

            Else
                Return "1|No hay conexión con el servidor para la validación de las cuentas"

            End If
        Catch ex As Exception
            'Throw ex
            Return "1|" + ex.ToString
        End Try

        Return "1|No se pudo validar la cuenta ST"

    End Function


#End Region

#Region "Métodos"

    Public Shared Sub ListaCargoAbono(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String)

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strEGCC_InsDesembolsoAgrupacion As String
            With objEGCC_InsDesembolsoAgrupacion
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoAgrupacion)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono(strEGCC_InsDesembolsoAgrupacion))
            HttpContext.Current.Session("DTB_LISTACARGOABONO") = dtInstruccionDesembolso

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub CargaMedioPago(ByVal pstrNroContrato As String, _
                                        ByVal pstrCodInsDesembolso As String, _
                                        ByVal pstrCodAgrupacion As String, _
                                        ByVal pstrCodigoMonedaAgrupacion As String, _
                                        ByVal pstrCodProveedor As String)

        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

        'Inicializa Objeto
        Dim objEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        Dim strEGCC_InsDesembolsoPago As String
        With objEGCC_InsDesembolsoPago
            .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
            .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrCodInsDesembolso)
            .Codagrupacion = GCCUtilitario.NullableString(pstrCodAgrupacion)
            .Codproveedor = GCCUtilitario.NullableString(pstrCodProveedor)
            .Codmonedaagrupacion = GCCUtilitario.NullableString(pstrCodigoMonedaAgrupacion)
        End With
        strEGCC_InsDesembolsoPago = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoPago)

        'Ejecuta Consulta
        Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPagoGet(strEGCC_InsDesembolsoPago))

        If dtInstruccionDesembolso.Rows.Count > 0 Then

            GCCUtilitario.SeleccionaCombo(cmbMedio, dtInstruccionDesembolso.Rows(0).Item("CodMedioAbono").ToString.Trim)
            hddCodigoMedioPago.Value = dtInstruccionDesembolso.Rows(0).Item("CodMedioAbono").ToString

            GCCUtilitario.SeleccionaCombo(cmbMonedaCuenta, dtInstruccionDesembolso.Rows(0).Item("CodMonedaCuenta").ToString.Trim)
            GCCUtilitario.SeleccionaCombo(cmbTipoCuenta, dtInstruccionDesembolso.Rows(0).Item("CodTipoCuenta").ToString.Trim)
            txtNroCuenta.Value = dtInstruccionDesembolso.Rows(0).Item("Numero_Cuenta").ToString

            txtCuentaBancaria.Value = dtInstruccionDesembolso.Rows(0).Item("NumCuentaInterbancaria").ToString

            txtPendiente.Value = dtInstruccionDesembolso.Rows(0).Item("Pendiente").ToString
            GCCUtilitario.SeleccionaCombo(cmbMonedaPend, dtInstruccionDesembolso.Rows(0).Item("CodMonedaPendiente").ToString.Trim)
            txtNota.Value = dtInstruccionDesembolso.Rows(0).Item("Nota").ToString
            txtEmisora.Value = dtInstruccionDesembolso.Rows(0).Item("Emisora").ToString
            txtReceptora.Value = dtInstruccionDesembolso.Rows(0).Item("Receptora").ToString

            txtNroDocumento.Value = dtInstruccionDesembolso.Rows(0).Item("NumeroDocumento").ToString
            GCCUtilitario.SeleccionaCombo(cmbTipoDocumento, dtInstruccionDesembolso.Rows(0).Item("CodTipoDocumento").ToString.Trim)
            txtRazonSocial.Value = dtInstruccionDesembolso.Rows(0).Item("RazonSocial").ToString

            GCCUtilitario.SeleccionaCombo(cmbPagoComision, dtInstruccionDesembolso.Rows(0).Item("CodPagoComision").ToString.Trim)
            txtMontoComision.Value = dtInstruccionDesembolso.Rows(0).Item("ImporteComision").ToString
            'Inicio IBK - AAE
            hddCodEstadoEjecucion.Value = dtInstruccionDesembolso.Rows(0).Item("CodEstadoEjecucionPago").ToString.Trim
            txtAbonoNoDom.Value = dtInstruccionDesembolso.Rows(0).Item("MontoAbonoNoDom").ToString.Trim
            txtCargoNoDom.Value = dtInstruccionDesembolso.Rows(0).Item("MontoCargoNoDom").ToString.Trim
            txtCtaAbonoNoDom.Value = dtInstruccionDesembolso.Rows(0).Item("CuentaAbonoNoDom").ToString.Trim
            txtCtaCargoNoDom.Value = dtInstruccionDesembolso.Rows(0).Item("CuentaCargoNoDom").ToString.Trim
            'Fin IBK
        Else
            'Inicio IBK - AAE - Si es SUNAT Cargo info por defecto
            If (hddCodigoAgrupacion.Value.Trim = "01" Or hddCodigoAgrupacion.Value.Trim = "03" Or hddCodigoAgrupacion.Value.Trim = "04" Or hddCodigoAgrupacion.Value.Trim = "06" Or hddCodigoAgrupacion.Value.Trim = "15") Then
                Dim dtContables As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.obtenerContablesSUNAT(hddCodigoAgrupacion.Value))
                If dtContables.Rows.Count > 0 Then
                    GCCUtilitario.SeleccionaCombo(cmbMedio, "007")
                    hddCodigoMedioPago.Value = "007"

                    txtPendiente.Value = dtContables.Rows(0).Item("ContableNoDom").ToString.Trim

                    txtPendiente.Value = dtContables.Rows(0).Item("Contable").ToString.Trim


                    GCCUtilitario.SeleccionaCombo(cmbMonedaPend, hddCodMonedaContrato.Value)
                    txtEmisora.Value = dtContables.Rows(0).Item("Emisora").ToString.Trim
                    txtReceptora.Value = dtContables.Rows(0).Item("Receptora").ToString.Trim
                End If
            End If
        End If
        ' Inicio IBK - AAE - Cargos valores por default
        CargarMediosPagoDefault("005")
        CargarMediosPagoDefault("004")
        CargarMediosPagoDefault("003")


    End Sub


    Public Sub CargarMediosPagoDefault(ByVal pstrCodMedioPago As String)
        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
            Dim strResultado As String = "1|"
            If (pstrCodMedioPago = "005" Or pstrCodMedioPago = "004" Or pstrCodMedioPago = "003") Then
                Dim dtContables As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.obtenerContablesMedioPago(pstrCodMedioPago))
                If dtContables.Rows.Count > 0 Then
                    If pstrCodMedioPago = "005" Then
                        hddPendiente05.Value = dtContables.Rows(0).Item("Contable").ToString.Trim
                        hddEmisora05.Value = dtContables.Rows(0).Item("Emisora").ToString.Trim
                        hddReceptora05.Value = dtContables.Rows(0).Item("Receptora").ToString.Trim
                    End If
                    If pstrCodMedioPago = "004" Then
                        hddPendiente04.Value = dtContables.Rows(0).Item("Contable").ToString.Trim
                        hddEmisora04.Value = dtContables.Rows(0).Item("Emisora").ToString.Trim
                        hddReceptora04.Value = dtContables.Rows(0).Item("Receptora").ToString.Trim
                    End If
                    If pstrCodMedioPago = "003" Then
                        hddPendiente03.Value = dtContables.Rows(0).Item("Contable").ToString.Trim
                        hddEmisora03.Value = dtContables.Rows(0).Item("Emisora").ToString.Trim
                        hddReceptora03.Value = dtContables.Rows(0).Item("Receptora").ToString.Trim
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
