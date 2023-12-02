Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Cotizacion_frmMotivoRechazo
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCotizacionListado.aspx.vb")

#Region "Eventos"

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                'Fecha Hoy
                Dim dtFecha As Date = Now
                Me.txtFecha.Value = dtFecha.ToString("dd/MM/yyyy")

                Dim strCodigoCotizacion As String = Request.QueryString("hddCodigoCotizacion")
                Me.hddCodigoCotizacion.Value = strCodigoCotizacion

                'CargaCombo
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoMotivo, GCCConstante.C_TABLAGENERICA_MOTIVO_RECHAZO_COTIZACION)

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

            Dim objEGcc_SeguimientoCotizacion As New EGcc_seguimientocotizacion
            Dim objLCotizacionTx As New LCotizacionTx
            Dim strEGcc_SeguimientoCotizacion As String

            'Sube Archivo
            Dim strArchivo As String = Me.txtAdjunto.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtAdjunto, False, "COTIZACION_RECHAZO", Nothing)
            End If

            Dim objESeguimiento As New EGcc_seguimientocotizacion
            With objESeguimiento
                .Codigocotizacion = Me.hddCodigoCotizacion.Value
                '.CodigoTipoEstado = 
                .Fechacambioestado = GCCUtilitario.StringToDateTime(Me.txtFecha.Value)
                .Codigotipomotivo = Me.cmbTipoMotivo.Value
                .Archivoadjunto = ViewState("RutaArchivoAdj") 'Me.txtAdjunto.FileName
                .Comentario = Me.txtComentario.Value
                '.FechaRegistro = 
                .Usuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audestadologico = 1
                '.AudFechaRegistro = 	
                '.AudFechaModificacion = 	
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            strEGcc_SeguimientoCotizacion = GCCUtilitario.SerializeObject(Of EGcc_seguimientocotizacion)(objESeguimiento)
            Dim booResultado As Boolean = objLCotizacionTx.CotizacionRechazar(strEGcc_SeguimientoCotizacion)

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

#End Region

End Class
