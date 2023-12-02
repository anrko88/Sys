Imports GCC.UI
Imports GCC.LogicWS

Partial Class frmLogin
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmLogin.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then
                Me.txtUsuario.Text = "B11797"
                pCargarPerfilesActivos()
            End If
        Catch ex As Exception
            Label1.text = String.Concat(ex.Message, " - TRACE ", ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Evento al hacer clic en el boton Ingresar (Hide)
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub btnIngresarHide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIngresarHide.Click
        Try
            GCCSession.CodigoUsuario = txtUsuario.Text
            GCCSession.PerfilUsuario = ddlPerfil.SelectedValue

            Response.Redirect("~/Default.aspx?usu=" & Me.txtUsuario.Text.Trim.ToUpper & "&rol=" & Me.ddlPerfil.SelectedValue.Trim.ToUpper, False)

            'If Not txtClave.Text.Trim.Equals("Leasing01") Then
            '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "scriptSalir", "alert('La clave ingresada no es correcta.');", True)
            'Else
            '    Response.Redirect("~/Default.aspx?usu=" & Me.txtUsuario.Text.Trim.ToUpper & "&rol=" & Me.ddlPerfil.SelectedValue.Trim.ToUpper, False)
            'End If

        Catch ex As Exception
            objLog.escribe("FATAL", ex.Message, "btnIngresarHide_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"
    Private Sub pCargarPerfilesActivos()
        Dim oLwsConfiguracion As New LConfiguracion
        Try
            Dim odtbPerfil As Data.DataTable = GCCUtilitario.DeserializeObject(Of Data.DataTable)(oLwsConfiguracion.ListarRolActivo())
            GCCUtilitario.pCargarDropDownList(ddlPerfil, odtbPerfil, "NOMBRE", "CodigoSda", "[-Seleccione-]", 0)
            ddlPerfil.SelectedValue = 1
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

End Class
