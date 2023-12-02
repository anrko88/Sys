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
Imports System.Text
Partial Class Pagos_frmGenerarCarta
    Inherits GCCBase
    Dim objLog As New GCCLog("frmGenerarCarta.aspx.vb")
    <WebMethod()> _
   Public Shared Function ObtieneDatosLote(ByVal pstrLote As String) As JQGridJsonResponse
        
        Dim dtImpuestoVehicular As New DataTable
        Dim oImpuestoVehicular As New EImpuestomunicipal
        Dim objLPagosNTx As New LPagosNTx
        dtImpuestoVehicular = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.GetDatosCarta(pstrLote.Trim.PadLeft(8, "0"c)))

        If dtImpuestoVehicular.Rows.Count > 0 Then
            '    oImpuestoVehicular.Municipalidad = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString()), "Sin Municipalidad", dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString())
            '    oImpuestoVehicular.Total = GCCUtilitario.CheckDecimal(dtImpuestoVehicular.Rows(0).Item("ImporteTotal").ToString())
            '    oImpuestoVehicular.FechacobroStr = dtImpuestoVehicular.Rows(0).Item("FechaGeneracion").ToString()
            '    oImpuestoVehicular.Concepto = dtImpuestoVehicular.Rows(0).Item("Concepto").ToString()
            '    'oImpuestoVehicular.Periodo = dtImpuestoVehicular.Rows(0).Item("Concepto").ToString()
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 20, dtImpuestoVehicular)
        End If
    End Function
    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        Dim dtOtroConcepto As New DataTable
        Dim objLPagosNTx As New LPagosNTx
        Dim strRutaExcel As String = ""
        Dim strRutaFisica As String = ""
        Try

            dtOtroConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.GetDatosCartaExcel(txtNroLote.Value.Trim.PadLeft(8, "0"c)))
            If dtOtroConcepto.Rows.Count > 0 Then


                If dtOtroConcepto.Rows(0).Item("concepto") = "Impuesto Vehicular" Then
                    strRutaExcel = "../Util/Plantillas/Reportes/LSG_ImpuestoVehicular.xlsm"
                    'strRutaFisica = "D:\Files\ImpuestoVehicular.xlsm"
                    strRutaFisica = "../temp/ImpuestoVehicular.xlsm"
                ElseIf dtOtroConcepto.Rows(0).Item("concepto") = "Impuesto Municipal" Then
                    strRutaExcel = "../Util/Plantillas/Reportes/LSG_ImpuestoMunicipal.xlsm"
                    'strRutaFisica = "D:\Files\ImpuestoMunicipal.xlsm"
                    strRutaFisica = "../temp/ImpuestoMunicipal.xlsm"
                ElseIf dtOtroConcepto.Rows(0).Item("concepto") = "Infracción de transito" Then
                    strRutaExcel = "../Util/Plantillas/Reportes/LSG_MultaVehicular.xlsm"
                    'strRutaFisica = "D:\Files\MultaVehicular.xlsm"
                    strRutaFisica = "../temp/MultaVehicular.xlsm"
                End If

                Dim newFile As New FileInfo(strRutaFisica)
                Dim template As New FileInfo(HttpContext.Current.Server.MapPath(strRutaExcel))

                If Not Directory.Exists(HttpContext.Current.Server.MapPath("../temp")) Then
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../temp"))
                End If
                If File.Exists(newFile.ToString()) Then
                    File.Delete(newFile.ToString())
                End If

                If Not Directory.Exists(strRutaFisica) Then
                    Directory.CreateDirectory(strRutaFisica)
                End If
                If File.Exists(newFile.ToString()) Then
                    File.Delete(newFile.ToString())
                End If

                Dim pck As New ExcelPackage(newFile, template)
                Dim worksheet As ExcelWorksheet = pck.Workbook.Worksheets("Hoja1")
                Dim col As Integer = 1
                Dim row As Integer = 2

                For Each rw As DataRow In dtOtroConcepto.Rows
                    For Each cl As DataColumn In dtOtroConcepto.Columns()
                        If cl.Ordinal <= 80 Then
                            'If cl.Ordinal <> 2 And cl.Ordinal <> 6 And cl.Ordinal <> 11 And cl.Ordinal <> 12 And cl.Ordinal <> 16 Then
                            'If Not String.IsNullOrEmpty(rw(cl.ColumnName).ToString()) Then
                            Dim RowData As String = rw(cl.ColumnName).ToString()
                            worksheet.Cells(row, col).Value = RowData.Trim().ToString()
                            'End If
                            'End If
                            col += 1
                        End If
                    Next
                    row += 1
                    col = 1
                Next
                pck.Save()


                Response.Redirect(strRutaFisica, False)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub
End Class
