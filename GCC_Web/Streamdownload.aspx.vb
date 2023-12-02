
Partial Class Streamdownload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strStream As String = Session("stream")
        Dim strArchivo As String = Request.QueryString("archivo")
        Dim strAplicacion As String = Request.QueryString("aplicacion")
        If strStream.Trim <> String.Empty Then

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("Content-disposition", "attachment; filename=" & strArchivo)
            Response.ContentType = strAplicacion
            Response.Write(strStream)
            Response.End()

        End If

    End Sub
End Class
