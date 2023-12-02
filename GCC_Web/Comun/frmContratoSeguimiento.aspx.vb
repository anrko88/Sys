Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Comun_frmContratoSeguimiento
    'Inherits System.Web.UI.Page
    Inherits GCCBase

    Dim objLog As New GCCLog("frmContratoSeguimiento.aspx.vb")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
            'pInicializarControles()

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoMotivo, GCCConstante.C_TABLAGENERICA_TIPO_OBSERVACION_CHECKLIST)
                Dim strCodigoContrato As String = Request.QueryString("hddCodigoContrato")
                Dim strEstado As String = Request.QueryString("hddCodigoEstadoContrato")

                Me.hddCodigoContrato.Value = strCodigoContrato
                Me.hddCodigoEstadoContrato.Value = strEstado


                Me.txtFecha.Value = Now.ToString("dd/MM/yyyy")
                HidFecha.Value = Me.txtFecha.Value
                'TBL046()
                'GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoProveedor, GCCConstante.C_TABLAGENERICA_NACIONALIDAD)
                'GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)

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
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        objLog.escribe("DEBUG", "Metodo Load de la página", "btnGrabar_Click")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            Dim objEGcc_seguimientocontrato As New EGcc_seguimientocontrato  ' EGcc_seguimientocontrato
            Dim objLContratoTx As New LContratoTx ' LCotizacionTx
            Dim oLwsDocClienteNtx As New LDocClienteNTx
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(hddCodigoContrato.Value))
            Dim strEGcc_SeguimientoContrato As String
            Dim oBase As New GCCBase
            Dim strCorreo As String
            strCorreo = ""
            Dim objESeguimientoContrato As New EGcc_seguimientocontrato

            'Sube Archivo al file server
            Dim strArchivo As String = Me.txtArchivoDocumentos.FileName

            'Sube Archivo
            'Dim strArchivo As String = Me.txtAdjunto.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "SEGUIMIENTO", Nothing)
            Else
                ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
            End If

            With objESeguimientoContrato
                .Codsolicitudcredito = Me.hddCodigoContrato.Value
                .CodigoMotivoRechazo = Me.cmbTipoMotivo.Value
                .Observacion = Me.txtComentario.Value

                ' GCCTS_JRC_20120220-Se necesita el Codigo de Usuari de Registro para el Seguimiento de Contrato 
                ' .Codigoestadocontrato = Me.hddCodigoEstadoContrato.Value
                .Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_INGRESADO

                .Adjunto = ViewState("RutaArchivoAdj")
                'Dim sfecha As String
                'sfecha = Year(CDate(Me.txtFecha.Value)) & "" & Right("00" & Month(CDate(Me.txtFecha.Value)), 2) & "" & Right("00" & Day(CDate(Me.txtFecha.Value)), 2)

                .SFechacambioestado = GCCUtilitario.CheckDate(Me.HidFecha.Value)
                .Usuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                '.Fechacambioestado = GCCUtilitario.StringToDateTime(Me.txtFecha.Value)
                '.Codigotipomotivo = Me.cmbTipoMotivo.Value
                '.Archivoadjunto = Me.txtAdjunto.FileName
                '.Comentario = Me.txtComentario.Value
                '.FechaRegistro = 
                '.Usuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                '.Audestadologico = 1
                '.AudFechaRegistro = 	
                '.AudFechaModificacion = 	
                '.Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                '.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                'GCCTS_JRC_20120304-Seguimiento Datos acionales
                .CodigoUsuario = GCCSession.CodigoUsuario.ToString()
                .NombreUsuario = GCCSession.NombreUsuario.ToString()
                .PerfilUsuario = GCCSession.DescripcionPerfil.ToString()

            End With

            strEGcc_SeguimientoContrato = GCCUtilitario.SerializeObject(Of EGcc_seguimientocontrato)(objESeguimientoContrato)
            Dim booResultado As Boolean = objLContratoTx.InsertaSeguimientoContrato(strEGcc_SeguimientoContrato)

            HidFecha.Value = ""

            Dim objSolicitudcreditoTx As New LContratoTx
            Dim oEGcSolicitudcredito As New ESolicitudcredito
            Dim strEGcSolicitudcredito As String
            With oEGcSolicitudcredito
                .Codsolicitudcredito = GCCUtilitario.NullableString(Me.hddCodigoContrato.Value)
                .FlagEnvioLegal = 0
                .Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_INGRESADO

            End With

            strEGcSolicitudcredito = GCCUtilitario.SerializeObject(oEGcSolicitudcredito)

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
                    mbool = oBase.EnviarMail("", strCorreo, "", "MailChecklistLegal", hddCodigoContrato.Value, _
                                             "", dr("NOMBRECLIENTE").ToString, "", _
                                             dr("NOMBRECLASIFICACIONBIEN").ToString, _
                                             dr("NOMBRETIPOBIEN").ToString, _
                                             pstrTipoContrato + " " + UCase(PstrTipoSubContrato), pstrSimbolo + " " + Format(CDbl(dr("PRECIOVENTA").ToString()), "#,##0.00"), cmbTipoMotivo.Items(cmbTipoMotivo.SelectedIndex).Text, txtComentario.Value)
                Next
            End If

            Dim intResult As Boolean = objSolicitudcreditoTx.GestionComercialEnviarUpd(strEGcSolicitudcredito)

            If mbool = False Then
                Throw New Exception("No se logró enviar el correo. Inténtelo mas tarde.")
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGrabar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "'))", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGrabar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub


End Class
