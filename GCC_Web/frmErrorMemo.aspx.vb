
Partial Class frmErrorMemo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim str As String = HttpContext.Current.Request.QueryString("Mensaje")

        Label1.Text = str.ToString()
    End Sub
End Class
