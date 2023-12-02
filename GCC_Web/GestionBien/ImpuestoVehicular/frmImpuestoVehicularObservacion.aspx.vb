Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_ImpuestoVehicular_frmImpuestoVehicularObservacion
    'Inherits System.Web.UI.Page
    Inherits GCCBase

    Dim objLog As New GCCLog("frmImpuestoVehicularObservacion.aspx.vb")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
            'pInicializarControles()

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            If Not Page.IsPostBack Then

                Dim strFechaRegistro As String = Request.QueryString("Fecha")
                Dim strObservacion As String = Request.QueryString("Obs")
                'Dim Opcion As String = Request.QueryString("Add")
                'Me.hddCodigoContrato.Value = strCodigoContrato
                'Me.hddCodigoSecFinanciamiento.Value = strSecFinanciamiento
                'hidOpcion.Value = Opcion
                'If Opcion <> "" Then
                '    InicializarCampos()
                'Else
                txtComentario.Value = strObservacion
                '    HidFecha.Value = Me.txtFecha.Value
                'End If
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
