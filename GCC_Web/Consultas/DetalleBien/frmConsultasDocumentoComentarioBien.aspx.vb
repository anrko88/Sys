Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Consultas_frmConsultasDocumentoComentarioBien
    Inherits GCCBase

    Dim objLog As New GCCLog("frmDocumentoComentarioBien.aspx.vb")

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


                            Me.txtNombreArchivo.InnerText = dtDocumentoContrato.Rows(0).Item("NombreArchivo").ToString.Trim
                            Me.txtComentario.Value = dtDocumentoContrato.Rows(0).Item("OBSERVACIONES").ToString.Trim

                            'Pone Datos
                            Dim strArchivo As String = dtDocumentoContrato.Rows(0).Item("ADJUNTO").ToString.Trim
                            hddAdjunto.Value = strArchivo
                            If Not strArchivo.Trim().Equals("") Then
                                hlkArchivo.Visible = True
                                hlkArchivo.NavigateUrl = "~/frmDownload.aspx?nombreArchivo=" + HttpUtility.UrlEncode(strArchivo.Trim())
                                Me.hddArchivo.Value = HttpUtility.UrlEncode(strArchivo.Trim())
                            Else
                                '  lblNoArchivo.Visible = True
                            End If

                        End If



                    End If
                End If
                If hddEdicion.Value <> "" Then
                    txtComentario.Value = hddObservacion.Value
                    txtNombreArchivo.InnerText = hddArchivo.Value
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

    
End Class
