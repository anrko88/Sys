Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_frmGestionBienDocRegistro
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

                'Setea Transacccion
                Me.hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO

                'Verifica Edicion
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                Dim strCodRelacionado As String = Request.QueryString("hddCodRelacionado")
                Dim strCodTipo As String = Request.QueryString("hddCodTipo")
                Dim strCodigoDocumento As String = Request.QueryString("hddCodDocumento")

                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodRelacionado.Value = strCodRelacionado
                Me.hddCodTipo.Value = strCodTipo

                'Edita Documento
                If Not strCodigoDocumento Is Nothing Then
                    If Not strCodigoDocumento.Trim().Equals("") Then

                        'Setea Tipo Transaccion
                        Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                        'Inicializa Objetos
                        Dim objLGestionBienDocNTx As New LGestionBienDocNTx
                        Dim objEGestionBienDoc As New EGCC_GestionBienDoc
                        Dim strEGestionBienDoc As String
                        With objEGestionBienDoc
                            .CodSolicitudCredito = GCCUtilitario.NullableString(strCodContrato)
                            .SecFinanciamiento = GCCUtilitario.CheckInt(strCodBien)
                            .CodRelacionado = GCCUtilitario.NullableString(strCodRelacionado)
                            .CodTipoModulo = GCCUtilitario.NullableString(strCodTipo)
                            .Codigodocumento = GCCUtilitario.CheckInt(strCodigoDocumento)
                        End With

                        'Inicializa Objeto            
                        strEGestionBienDoc = GCCUtilitario.SerializeObject(objEGestionBienDoc)

                        'Ejecuta Consulta
                        Dim dtDocumento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLGestionBienDocNTx.ObtenerGestionBienDoc(strEGestionBienDoc))

                        'Pone Datos del Documento
                        If dtDocumento.Rows.Count > 0 Then

                            Me.hddCodigoDocumento.Value = strCodigoDocumento
                            Me.txtNombre.Value = dtDocumento.Rows(0).Item("NombreArchivo").ToString.Trim
                            Me.txtComentario.Value = dtDocumento.Rows(0).Item("Comentario").ToString.Trim

                            'Pone Datos
                            Dim strArchivo As String = dtDocumento.Rows(0).Item("RutaArchivo").ToString.Trim
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
            Dim objLGestionBienDocTx As New LGestionBienDocTx
            Dim strEGestionBienDoc As String

            'Sube Archivo
            Dim strArchivo As String = Me.txtAdjunto.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtAdjunto, False, "INSDESEMBOLSO", Nothing)
            Else
                ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
            End If

            'Graba Documentos
            Dim objEGestionBienDoc As New EGCC_GestionBienDoc
            With objEGestionBienDoc
                .CodSolicitudCredito = Me.hddCodContrato.Value
                .SecFinanciamiento = Me.hddCodBien.Value
                .CodRelacionado = Me.hddCodRelacionado.Value
                .CodTipoModulo = Me.hddCodTipo.Value

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
            Dim strMensaje As String = ""
            Dim strNumeroCotizacion As String = ""
            If strTipoTransaccion.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strEGestionBienDoc = GCCUtilitario.SerializeObject(Of EGCC_GestionBienDoc)(objEGestionBienDoc)
                objLGestionBienDocTx.InsertarGestionBienDoc(strEGestionBienDoc)
                blnResult = True
                strMensaje = "El Documento/Comentario fué grabado correctamente."
            Else
                objEGestionBienDoc.Codigodocumento = Me.hddCodigoDocumento.value
                strEGestionBienDoc = GCCUtilitario.SerializeObject(Of EGCC_GestionBienDoc)(objEGestionBienDoc)
                objLGestionBienDocTx.ModificarGestionBienDoc(strEGestionBienDoc)
                blnResult = True
                strMensaje = "El Documento/Comentario fué actualizado correctamente."
            End If

            'Valida Resultado
            If blnResult Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListadoDoc('" + strMensaje + "');", True)
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
