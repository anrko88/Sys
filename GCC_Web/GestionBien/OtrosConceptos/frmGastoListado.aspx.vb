Imports GCC.Entity
Imports GCC.LogicWS
Imports GCC.UI
Partial Class GestionBien_OtrosConceptos_frmGastoListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmGastoListado.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                'GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
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
