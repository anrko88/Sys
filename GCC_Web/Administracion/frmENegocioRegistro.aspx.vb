Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmENegocioRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmENegocioRegistro.aspx.vb")

    Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                Dim strClave1 As String = Request.QueryString("co")

                'Pone Valores
                Me.hddClave1.Value = strClave1

                'Carga Ejecutivo
                Dim oLEjecutivoNTx As New LEjecutivoNTx
                Dim oEjecutivo As New EGCC_Ejecutivo
                With oEjecutivo
                    .ID_Tabla = "TBL146"
                End With
                Dim dtEjecutivoLeasing As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLEjecutivoNTx.ListadoEjecutivo(100, 1, "NombreEjecutivo", "ASC", GCCUtilitario.SerializeObject(oEjecutivo)))

                'Combo
                cmbELeasing.DataSource = dtEjecutivoLeasing
                cmbELeasing.DataTextField = "NombreEjecutivo"
                cmbELeasing.DataValueField = "Codigo"
                cmbELeasing.DataBind()
                GCCUtilitario.pInsertarPrimerItemHtmlSelect(cmbELeasing, "[-Seleccione-]", "0")


                'Edita Documento
                If Not strClave1 Is Nothing Then
                    If Not strClave1.Trim().Equals("0") Then

                        'Setea Tipo Transaccion
                        Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                        'Inicializa Objetos
                        Dim objLEjecutivoNTx As New LEjecutivoNTx

                        'Ejecuta Consulta
                        Dim dtEjecutivo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLEjecutivoNTx.ObtenerEjecutivo("TBL410", GCCUtilitario.NullableString(strClave1)))

                        'Pone Datos del Documento
                        If dtEjecutivo.Rows.Count > 0 Then

                            Me.txtCodigo.Value = dtEjecutivo.Rows(0).Item("CodigoEjecutivo").ToString.Trim
                            Me.txtNombre.Value = dtEjecutivo.Rows(0).Item("NombreEjecutivo").ToString.Trim
                            GCCUtilitario.SeleccionaCombo(cmbELeasing, dtEjecutivo.Rows(0).Item("CodELeasing").ToString.Trim)

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
            Dim objLEjecutivoTx As New LEjecutivoTx
            Dim strEjecutivo As String

            'Graba Documentos
            Dim objEGCC_Ejecutivo As New EGCC_Ejecutivo
            With objEGCC_Ejecutivo
                .ID_Tabla = "TBL410"
                .Codigo = Me.hddClave1.Value
                .CodigoEjecutivo = Me.txtCodigo.Value
                .NombreEjecutivo = Me.txtNombre.Value
                .CodELeasing = Me.cmbELeasing.Value
            End With

            'Ejecuta Transaccion
            Dim strTipoTransaccion As String = Me.hddTipoTransaccion.Value
            Dim blnResult As Boolean = False
            Dim strMensaje As String = ""

            If strTipoTransaccion.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strEjecutivo = GCCUtilitario.SerializeObject(Of EGCC_Ejecutivo)(objEGCC_Ejecutivo)
                objLEjecutivoTx.InsertarEjecutivo(strEjecutivo)
                blnResult = True
                strMensaje = "El Ejecutivo fué grabado correctamente."
            Else
                strEjecutivo = GCCUtilitario.SerializeObject(Of EGCC_Ejecutivo)(objEGCC_Ejecutivo)
                objLEjecutivoTx.ModificarEjecutivo(strEjecutivo)
                blnResult = True
                strMensaje = "El Ejecutivo fué actualizado correctamente."
            End If

            'Valida Resultado
            If blnResult Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "CONFIRMACIÓN", "fn_grabadoOK('" + strMensaje + "');", True)
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
