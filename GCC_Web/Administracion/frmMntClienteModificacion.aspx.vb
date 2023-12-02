Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Mantenimiento_frmMntClienteModificacion
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMntClienteModificacion.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                hidCodSup.Value = Request.QueryString("csup")
                hidDir.Value = Request.QueryString("dir")
                hidCodUni.Value = Request.QueryString("cuni")
                txtDireccion.Value = Request.QueryString("dir")
                pCargarContactoPreferente()
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

#End Region

    Private Sub pCargarContactoPreferente()
        Dim oLwsUtilNtx As New LUtilNTX
        Dim dtContacto As New DataTable

        dtContacto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsUtilNtx.ObteberContactoPreferente(hidCodSup.Value))
        If dtContacto.Rows.Count > 0 Then
            txtNombreContactoP.Value = dtContacto.Rows(0).Item("nombre").ToString()
            txtCorreoContactoP.Value = (dtContacto.Rows(0).Item("correo").ToString())
            txtTelefonoContactoP.Value = dtContacto.Rows(0).Item("telefono").ToString()
        End If
       

    End Sub
#Region "WebMethods"
    <WebMethod()> _
    Public Shared Function ModificarSuprestatario(ByVal pCodSuprestatario As String, _
                                         ByVal pDireccion As String, _
                                         ByVal pCodSupContacto As String, _
                                         ByVal pNombre As String, _
                                         ByVal pCorreo As String, _
                                         ByVal pTelefono As String) As String
        Try

            Dim objLContacto As New LUtilTX


            Dim blnResult As Boolean = objLContacto.ActualizarSubprestatario(pCodSuprestatario, pDireccion)

            Dim blnResult2 As Boolean = objLContacto.InsertarContactoPreferente(Convert.ToInt32(pCodSupContacto), pCodSuprestatario, pNombre, pCorreo, pTelefono)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If

            If blnResult2 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

        End Try
    End Function
    <WebMethod()> _
Public Shared Function AsignarContactoPreferente(ByVal pCodSupContacto As String, ByVal pCodSuprestatario As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String
        Try

            Dim objLContacto As New LUtilTX


            Dim blnResult As Boolean = objLContacto.InsertarContactoPreferente(Convert.ToInt32(pCodSupContacto), pCodSuprestatario, pNombre, pCorreo, pTelefono)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

        End Try
    End Function
#End Region

    Protected Sub btnCargarContacto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCargarContacto.Click
        Dim oLwsUtilNtx As New LUtilNTX
        Dim dtContacto As New DataTable

        dtContacto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsUtilNtx.ObteberContactoPreferente(hidCodSup.Value))
        txtNombreContactoP.Value = dtContacto.Rows(0).Item("nombre").ToString()
        txtCorreoContactoP.Value = (dtContacto.Rows(0).Item("correo").ToString())
        txtTelefonoContactoP.Value = dtContacto.Rows(0).Item("telefono").ToString()
    End Sub
End Class
