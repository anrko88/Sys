Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmDocumentoComentarioContratoBien
    Inherits GCCBase

    Dim objLog As New GCCLog("frmDocumentoComentarioContratoBien.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                hddCodigoContrato.Value = Request.QueryString("codcontrato")
                hddCodigoDocumento.Value = Request.QueryString("codigo")
                hidSecFinanciamiento.Value = Request.QueryString("scf")
                hddEdicion.Value = Request.QueryString("add")
                hddObservacion.Value = Request.QueryString("obs")
                hddArchivo.Value = Request.QueryString("nomArchivo")
                hidDetalle.Value = Request.QueryString("det")
                hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO
                If Not hddCodigoDocumento.Value Is Nothing Then
                    If Not hddCodigoDocumento.Value.Trim().Equals("") Then
                        'Setea Tipo Transaccion
                        Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                        'Inicializa Objetos
                        Dim objBienNTx As New LBienNTx
                        Dim objEGcc_contratodocumento As New EGcc_contratodocumento
                        Dim strEGcc_contratodocumento As String
                        With objEGcc_contratodocumento
                            .Numerocontrato = GCCUtilitario.NullableString(hddCodigoContrato.Value)
                            .Codigodocumento = hddCodigoDocumento.Value
                        End With

                        'Inicializa Objeto            
                        strEGcc_contratodocumento = GCCUtilitario.SerializeObject(objEGcc_contratodocumento)

                        'Ejecuta Consulta
                        Dim dtDocumentoContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ObtenerBienContratoDocumento(strEGcc_contratodocumento))

                        If dtDocumentoContrato.Rows.Count > 0 Then


                            Me.txtNombreArchivo.Value = dtDocumentoContrato.Rows(0).Item("NombreArchivo").ToString.Trim
                            Me.txtComentario.Value = dtDocumentoContrato.Rows(0).Item("OBSERVACIONES").ToString.Trim

                            'Pone Datos
                            Dim strArchivo As String = dtDocumentoContrato.Rows(0).Item("ADJUNTO").ToString.Trim
                            hddAdjunto.Value = strArchivo
                            If Not strArchivo.Trim().Equals("") Then
                                hlkArchivo.Visible = True
                                hlkArchivo.NavigateUrl = "~/frmDownload.aspx?nombreArchivo=" + HttpUtility.UrlEncode(strArchivo.Trim())
                                Me.hddArchivo.Value = HttpUtility.UrlEncode(strArchivo.Trim())
                            Else
                                lblNoArchivo.Visible = True
                            End If

                        End If



                    End If
                End If
                If hddEdicion.Value <> "" Then
                    txtComentario.Value = hddObservacion.Value
                    txtNombreArchivo.Value = hddArchivo.Value
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
            Dim objLBienTx As New LBienTx
            Dim strEGcc_biendocumento As String

            'Sube Archivo
            Dim strArchivo As String = txtArchivoDocumentos.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "Mant.Bien", Nothing)
            Else
                ' ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
            End If

            'Graba Documentos

            Dim objEGcc_contratodocumento As New EGcc_contratodocumento
            With objEGcc_contratodocumento
                .Numerocontrato = hddCodigoContrato.Value
                .SecFinanciamiento = 0
                .EstadoDocumento = 1
                .Nombrearchivo = Me.txtNombreArchivo.Value
                .Adjunto = ViewState("RutaArchivoAdj") 'Me.txtAdjunto.FileName
                .Observaciones = Me.txtComentario.Value
                .EstadoDocContrato = 1
                .EstadoDocBien = 0
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            'Ejecuta Transaccion
            Dim strTipoTransaccion As String = Me.hddTipoTransaccion.Value
            Dim blnResult As Boolean = False
            If strTipoTransaccion.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strEGcc_biendocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(objEGcc_contratodocumento)
                objLBienTx.fblnContratoDocumentoIns(strEGcc_biendocumento)
                blnResult = True
            Else
                objEGcc_contratodocumento.Codigodocumento = Me.hddCodigoDocumento.Value
                If ViewState("RutaArchivoAdj") Is Nothing Then
                    objEGcc_contratodocumento.Adjunto = hddAdjunto.Value
                End If
                strEGcc_biendocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(objEGcc_contratodocumento)
                objLBienTx.ModificarBienContratoDocumento(strEGcc_biendocumento)
                blnResult = True
            End If

            'Valida Resultado
            If blnResult Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
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
