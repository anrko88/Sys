Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Cotizacion_frmDocumentoComentario
    Inherits GCCBase

    Dim objLog As New GCCLog("frmDocumentoComentario.aspx.vb")

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Dim strCodigoCotizacion As String = Request.QueryString("hddCodigoCotizacion")
                Dim strCodigoDocumento As String = Request.QueryString("hddCodigoDocumento")

                Me.hddCodigoCotizacion.Value = strCodigoCotizacion
                Me.hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO

                If Not strCodigoDocumento Is Nothing Then
                    If Not strCodigoDocumento.Trim().Equals("") Then

                        'Setea Tipo Transaccion
                        Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                        'Inicializa Objetos
                        Dim objCotizacionNTx As New LCotizacionNTx
                        Dim objEGcc_cotizaciondocumento As New EGcc_cotizaciondocumento
                        Dim strEGcc_cotizacionDocumento As String
                        With objEGcc_cotizaciondocumento
                            .Codigocotizacion = GCCUtilitario.NullableString(strCodigoCotizacion)
                            .Codigodocumento = strCodigoDocumento
                        End With

                        'Inicializa Objeto            
                        strEGcc_cotizacionDocumento = GCCUtilitario.SerializeObject(objEGcc_cotizaciondocumento)

                        'Ejecuta Consulta
                        Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.ObtenerCotizacionDocumento(strEGcc_cotizacionDocumento))


                        If dtCotizacion.Rows.Count > 0 Then

                            Me.hddCodigoDocumento.Value = strCodigoDocumento
                            Me.txtNombre.Value = dtCotizacion.Rows(0).Item("NombreArchivo").ToString.Trim
                            Me.txtComentario.Value = dtCotizacion.Rows(0).Item("Comentario").ToString.Trim

                            'Pone Datos
                            Dim strArchivo As String = dtCotizacion.Rows(0).Item("RutaArchivo").ToString.Trim
                            If Not strArchivo.Trim().Equals("") Then
                                hlkArchivo.Visible = True
                                hlkArchivo.NavigateUrl = "~/frmDownload.aspx?nombreArchivo=" + HttpUtility.UrlEncode(strArchivo.Trim())
                                Me.hddAdjunto.Value = HttpUtility.UrlEncode(strArchivo.Trim())
                            Else
                                lblNoArchivo.Visible = True
                            End If

                        End If

                    End If
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

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        objLog.escribe("DEBUG", "Metodo Load de la página", "btnGrabar_Click")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            'Instancia Clases
            Dim objLCotizacionTx As New LCotizacionTx
            Dim strEGcc_cotizaciondocumento As String

            'Sube Archivo
            Dim strArchivo As String = Me.txtAdjunto.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtAdjunto, False, "COTIZACION", Nothing)
            Else
                ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
            End If

            'Graba Documentos
            Dim objEGcc_cotizaciondocumento As New EGcc_cotizaciondocumento
            With objEGcc_cotizaciondocumento
                .Codigocotizacion = Me.hddCodigoCotizacion.Value
                '.Codigodocumento = me.hddCodigoDocumento.value
                .Nombrearchivo = Me.txtNombre.Value
                .Rutaarchivo = ViewState("RutaArchivoAdj") 'Me.txtAdjunto.FileName
                .Comentario = Me.txtComentario.Value

                .Audestadologico = 1
                '.AudFechaRegistro = 	
                '.AudFechaModificacion = 	
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            'Ejecuta Transaccion
            Dim strTipoTransaccion As String = Me.hddTipoTransaccion.Value
            Dim blnResult As Boolean = False
            Dim strNumeroCotizacion As String = ""
            If strTipoTransaccion.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strEGcc_cotizaciondocumento = GCCUtilitario.SerializeObject(Of EGcc_cotizaciondocumento)(objEGcc_cotizaciondocumento)
                objLCotizacionTx.InsertarCotizacionDocumento(strEGcc_cotizaciondocumento)
                blnResult = True
            Else
                objEGcc_cotizaciondocumento.Codigodocumento = Me.hddCodigoDocumento.value
                strEGcc_cotizaciondocumento = GCCUtilitario.SerializeObject(Of EGcc_cotizaciondocumento)(objEGcc_cotizaciondocumento)
                objLCotizacionTx.ModificarCotizacionDocumento(strEGcc_cotizaciondocumento)
                blnResult = True
            End If

            'Valida Resultado
            If blnResult Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListadoDoc();", True)
            End If

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
