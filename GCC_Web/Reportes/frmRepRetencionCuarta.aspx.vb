Imports System.Net
Imports System.Diagnostics
Imports System.IO

Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports GCC.Entity
Imports GCC.LogicWS


Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.OfficeProperties
Imports System.Text
Imports System.Drawing
Partial Class Reportes_frmRepRetencionCuarta
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepRetencionCuarta.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then

            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub
    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        Dim strNombreArchivo As String
        strNombreArchivo = "RetencionCuarta_" + Now.ToLongTimeString.Replace(":", "_").Replace(".", "") + ".xlsx"

        Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../temp/" + strNombreArchivo))
        Dim template As New FileInfo(HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/LSG_rptRetencionesCuarta.xlsx"))
        Dim pck As New ExcelPackage(newFile, template)
        pck.Save()
        Response.Redirect("../temp/" + strNombreArchivo, False)

    End Sub

End Class
