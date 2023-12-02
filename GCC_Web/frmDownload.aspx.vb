Imports GCC.UI

Partial Class frmDownload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If (Request.QueryString.HasKeys()) Then
                Dim nombreArchivo As String = Request.QueryString("nombreArchivo")
                Dim ruta As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + nombreArchivo
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(ruta)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(HttpUtility.UrlDecode(file.FullName))
                    Response.Flush()
                    Response.Close()
                Else
                    Dim scriptString As String
                    scriptString = " alert('El archivo no existe en la ruta.');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ConfirmScript", scriptString, True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "cerrarventana", "window.close();", True)
                End If
            End If

        Catch ex As Exception
            GCCUtilitario.Show(Server.HtmlEncode(ex.Message), Me)
        End Try
    End Sub

End Class
