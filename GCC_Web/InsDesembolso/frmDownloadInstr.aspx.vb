Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data.SqlClient

Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Imports GCC.Entity
Imports GCC.LogicWS

Imports OfficeOpenXml.OfficeProperties
Imports System.Text
Imports System.Drawing
Partial Class InsDesembolso_frmDownloadInstr
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
        Dim CodSolicitudCredito As String = IIf(String.IsNullOrEmpty(Request.QueryString("CodSolicitudCredito")), "", Request.QueryString("CodSolicitudCredito"))
        Dim CodInstruccionDesembolso As String = IIf(String.IsNullOrEmpty(Request.QueryString("CodInstruccionDesembolso")), "", Request.QueryString("CodInstruccionDesembolso"))
        Dim CodCorrelativo As String = IIf(String.IsNullOrEmpty(Request.QueryString("CodCorrelativo")), "", Request.QueryString("CodCorrelativo"))
        'Ejecuta Consulta
        Dim dtInstruccionDesembolso As DataSet = GCCUtilitario.DeserializeObject(Of DataSet) _
                                                    (objLInstruccionDesembolsoNTx.ListaAgrupacionVoucher(CodSolicitudCredito, CodInstruccionDesembolso, CodCorrelativo))
        'Valida si existe
        Exportar(dtInstruccionDesembolso)
    End Sub
    Sub Exportar(ByVal DSResumen As DataSet)
        Dim strNombreArchivo As String
        strNombreArchivo = "Voucher_" + Now.ToLongTimeString.Replace(":", "_").Replace(".", "") + ".xlsx"

        Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../temp/" + strNombreArchivo))
        Dim template As New FileInfo(HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/InstruccionDesembolso.xlsx"))
        Dim col As Integer = 2
        Dim row As Integer = 17
        Dim RowData As String

        If Not Directory.Exists(HttpContext.Current.Server.MapPath("../temp")) Then
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../temp"))
        End If
        If File.Exists(newFile.ToString()) Then
            File.Delete(newFile.ToString())
        End If

        Dim pck As New ExcelPackage(newFile, template)
        Dim worksheet As ExcelWorksheet = pck.Workbook.Worksheets("Voucher")

        Dim dtTabla As DataTable = DSResumen.Tables(0)
        'Header
        worksheet.Cells(9, 3).Value = dtTabla.Rows(0).Item("FechaDesembolso").ToString()
        worksheet.Cells(10, 3).Value = dtTabla.Rows(0).Item("RazonSocial").ToString()
        worksheet.Cells(11, 3).Value = dtTabla.Rows(0).Item("Moneda").ToString()

        worksheet.Cells(9, 6).Value = dtTabla.Rows(0).Item("ValorNeto").ToString()
        worksheet.Cells(10, 6).Value = IIf(String.IsNullOrEmpty(dtTabla.Rows(0).Item("MedioAbono").ToString()), "", dtTabla.Rows(0).Item("MedioAbono").ToString())
        worksheet.Cells(11, 6).Value = IIf(String.IsNullOrEmpty(dtTabla.Rows(0).Item("NroCuenta").ToString()), "", dtTabla.Rows(0).Item("NroCuenta").ToString())
        Dim dtTabla1 As DataTable = DSResumen.Tables(1)
        'Detalle
        For Each rw As DataRow In dtTabla1.Rows
            For Each cl As DataColumn In dtTabla1.Columns
                If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                    RowData = rw(cl.ColumnName).ToString()
                    worksheet.Cells(row, col).Value = RowData.Trim().ToString()
                End If
                col += 1
            Next
            worksheet.SelectedRange(row, 2, row, 7).Style.Border.BorderAround(ExcelBorderStyle.Thin)
            worksheet.SelectedRange(row, 2, row, 7).Style.Border.Left.Style = ExcelBorderStyle.Thin
            worksheet.SelectedRange(row, 2, row, 7).Style.Border.Right.Style = ExcelBorderStyle.Thin

            row += 1
            col = 2
        Next
        'Descuento

        row += 2
        worksheet.Cells(row, 6).Value = "Descuentos"
        worksheet.Cells(row, 7).Value = "Importe"

        worksheet.SelectedRange(row, 6, row, 7).Style.Border.BorderAround(ExcelBorderStyle.Thin)
        worksheet.SelectedRange(row, 6, row, 7).Style.Border.Left.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 6, row, 7).Style.Border.Right.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 6, row, 7).Style.Fill.PatternType = ExcelFillStyle.Solid
        worksheet.SelectedRange(row, 6, row, 7).Style.Fill.BackgroundColor.SetColor(Color.Green)
        worksheet.SelectedRange(row, 6, row, 7).Style.Font.Color.SetColor(Color.White)

        row += 1
        Dim dtTabla2 As DataTable = DSResumen.Tables(2)

        worksheet.Cells(row, 6).Value = "Reembolso Cliente"
        worksheet.Cells(row, 7).Value = dtTabla2.Rows(0).Item("Adelanto").ToString()


        worksheet.SelectedRange(row, 6, row, 7).Style.Border.BorderAround(ExcelBorderStyle.Thin)
        worksheet.SelectedRange(row, 6, row, 7).Style.Border.Left.Style = ExcelBorderStyle.Thin
        worksheet.SelectedRange(row, 6, row, 7).Style.Border.Right.Style = ExcelBorderStyle.Thin

        pck.Save()
        Response.Redirect("../temp/" + strNombreArchivo, False)
    End Sub
End Class
