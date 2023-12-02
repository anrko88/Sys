Imports GCC.Entity

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GCC.UI.GCCSession.CodigoUsuario = Nothing
        GCC.UI.GCCSession.NombreUsuario = Nothing
        GCC.UI.GCCSession.PerfilUsuario = Nothing
        GCC.UI.GCCSession.DescripcionPerfil = Nothing
        Dim retorno As String = Request.QueryString("retorno")
        If String.IsNullOrEmpty(retorno) Then
            Dim mRutaAnt As String
            Dim strAmb As String = fCargarAmbiente()
            If Not Context.Request.UrlReferrer Is Nothing Then
                mRutaAnt = Context.Request.UrlReferrer.PathAndQuery
            Else
                mRutaAnt = ""
            End If

            If String.IsNullOrEmpty(mRutaAnt) Then
                Dim sUser As String = Request.QueryString("arg01")
                Dim sPin As String = Request.QueryString("arg02")
                Dim sDbServer As String = Request.QueryString("arg03")
                Dim sDbName As String = Request.QueryString("arg04")
                Dim sPerfil As String = Request.QueryString("arg05")
                If mRutaAnt = "" Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "AbrirVentana", "fn_CargarVentanaSDA('" & sUser & "','" & sPin & "','" & HttpUtility.UrlEncode(sDbServer) & "','" & HttpUtility.UrlEncode(sDbName) & "','" & sPerfil & "','','" & strAmb & "');", True)
                Else
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "AbrirVentana", "fn_cargarVentana('" & sUser & "','" & sPin & "','" & HttpUtility.UrlEncode(sDbServer) & "','" & HttpUtility.UrlEncode(sDbName) & "','" & sPerfil & "','" & Context.Request.UrlReferrer.PathAndQuery & "','" & strAmb & "');", True)
                End If
            Else
                If mRutaAnt.ToUpper.IndexOf("FRMLOGIN.ASPX") = -1 Then
                    Dim sUser As String = Request.QueryString("arg01")
                    Dim sPin As String = Request.QueryString("arg02")
                    Dim sDbServer As String = Request.QueryString("arg03")
                    Dim sDbName As String = Request.QueryString("arg04")
                    Dim sPerfil As String = Request.QueryString("arg05")

                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "AbrirVentana", "fn_CargarVentanaSDA('" & sUser & "','" & sPin & "','" & HttpUtility.UrlEncode(sDbServer) & "','" & HttpUtility.UrlEncode(sDbName) & "','" & sPerfil & "','" & Context.Request.UrlReferrer.PathAndQuery & "','" & strAmb & "');", True)
                Else
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "AbrirVentana", "fn_cargarVentana('" & Request.QueryString("usu") & "','" & Request.QueryString("rol") & "','" & Context.Request.UrlReferrer.PathAndQuery & "','" & strAmb & "');", True)
                End If
            End If
        Else
            Dim mRutaAnt As String = Request.QueryString("urlant")
            If String.IsNullOrEmpty(mRutaAnt) Then
                Dim mRutaFinal As String = "frmPrincipal.aspx?arg01=" & Request.QueryString("arg01") & "&arg02=" & Request.QueryString("arg02") & "&arg03=" & Request.QueryString("arg03") & "&arg04=" & Request.QueryString("arg04") & "&arg05=" & Request.QueryString("arg05") & "&urlant=" & mRutaAnt
                GCC.UI.GCCUtilitario.pGuardarLogWeb("GCC", "Default.aspx", EConstante.C_SUCESO_INFORMATIVO, "Load", mRutaFinal)
                Response.Redirect(mRutaFinal)
            Else
                If mRutaAnt.ToUpper.IndexOf("FRMLOGIN.ASPX") = -1 Then
                    Dim mRutaFinal As String = "frmPrincipal.aspx?arg01=" & Request.QueryString("arg01") & "&arg02=" & Request.QueryString("arg02") & "&arg03=" & Request.QueryString("arg03") & "&arg04=" & Request.QueryString("arg04") & "&arg05=" & Request.QueryString("arg05") & "&urlant=" & mRutaAnt
                    GCC.UI.GCCUtilitario.pGuardarLogWeb("GCC", "Default.aspx", EConstante.C_SUCESO_INFORMATIVO, "Load", mRutaFinal)
                    Response.Redirect(mRutaFinal)
                Else
                    Response.Redirect("frmPrincipal.aspx?usu=" & Request.QueryString("usu") & "&rol=" & Request.QueryString("rol") & "&urlant=" & Request.QueryString("urlant"))
                End If
            End If
        End If
    End Sub

    Private Function fCargarAmbiente() As String
        Try
            Dim mValor As String = String.Empty
            Select Case GCC.UI.GCCUtilitario.fstrObtieneKeyWebConfig("ArgAmbienteIndex")
                Case 0 : mValor = "PRD"
                Case 1 : mValor = "UAT"
                Case 2 : mValor = "SIT"
                Case 3 : mValor = "DES"
            End Select
            Return mValor
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class