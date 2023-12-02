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
Partial Class Reportes_frmNotasAbono
    Inherits GCCBase

    Dim objLog As New GCCLog("frmNotasAbono.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtfecha As Date
        Dim format As String = "dd/MM/yyyy"
        Try

            If Not Page.IsPostBack Then
                If DateTime.Now.DayOfWeek = 1 Then
                    dtfecha = DateTime.Now.AddDays(-3)
                Else
                    dtfecha = DateTime.Now.AddDays(-1)
                End If

                txtFechaInicial.Value = dtfecha.ToString(format)
                txtFechaFinal.Value = dtfecha.ToString(format)
            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim strFechaIni As String = txtFechaInicial.Value
        Dim strFechaFin As String = txtFechaFinal.Value


        Dim objUtilNTx As New LUtilNTX
        Dim table As New DataTable 'variable para recorrer las tablas      
        Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../temp/NotasAbono.xlsm"))
        Dim template As New FileInfo(HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/LSG_NotasAbono.xlsm"))
        Try

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarNotasAbono(hddFecInicio.Value, hddFecFin.Value))
            If Not Directory.Exists(HttpContext.Current.Server.MapPath("../temp")) Then
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../temp"))
            End If
            If File.Exists(newFile.ToString()) Then
                File.Delete(newFile.ToString())
            End If

            Dim pck As New ExcelPackage(newFile, template)
            Dim worksheet As ExcelWorksheet = pck.Workbook.Worksheets("Hoja1")
            Dim col As Integer = 1
            Dim row As Integer = 2

            For Each rw As DataRow In table.Rows
                For Each cl As DataColumn In table.Columns()
                    If cl.Ordinal <= 80 Then
                        'If cl.Ordinal <> 2 And cl.Ordinal <> 6 And cl.Ordinal <> 11 And cl.Ordinal <> 12 And cl.Ordinal <> 16 Then
                        If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                            Dim RowData As String = rw(cl.ColumnName).ToString()
                            worksheet.Cells(row, col).Value = RowData.Trim().ToString()
                        End If
                        'End If
                        col += 1
                    End If
                Next
                row += 1
                col = 1
            Next
            pck.Save()
            Response.Redirect("../temp/NotasAbono.xlsm", False)
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub
    'Public Shared Function ConvertToRecordset(ByVal inTable As DataTable) As ADODB.Recordset
    '    Dim result As New ADODB.Recordset()
    '    result.CursorLocation = ADODB.CursorLocationEnum.adUseClient

    '    Dim resultFields As ADODB.Fields = result.Fields
    '    Dim inColumns As System.Data.DataColumnCollection = inTable.Columns

    '    For Each inColumn As DataColumn In inColumns
    '        resultFields.Append(inColumn.ColumnName, TranslateType(inColumn.DataType), inColumn.MaxLength, IIf(inColumn.AllowDBNull, ADODB.FieldAttributeEnum.adFldIsNullable, ADODB.FieldAttributeEnum.adFldUnspecified), Nothing)
    '    Next

    '    result.Open(System.Reflection.Missing.Value, System.Reflection.Missing.Value, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, 0)

    '    For Each dr As DataRow In inTable.Rows
    '        result.AddNew(System.Reflection.Missing.Value, System.Reflection.Missing.Value)

    '        For columnIndex As Integer = 0 To inColumns.Count - 1
    '            resultFields(columnIndex).Value = dr(columnIndex)
    '        Next
    '    Next

    '    Return result
    'End Function

    'Private Shared Function TranslateType(ByVal columnType As Type) As ADODB.DataTypeEnum
    '    Select Case columnType.UnderlyingSystemType.ToString()
    '        Case "System.Boolean"
    '            Return ADODB.DataTypeEnum.adBoolean

    '        Case "System.Byte"
    '            Return ADODB.DataTypeEnum.adUnsignedTinyInt

    '        Case "System.Char"
    '            Return ADODB.DataTypeEnum.adChar

    '        Case "System.DateTime"
    '            Return ADODB.DataTypeEnum.adDate

    '        Case "System.Decimal"
    '            Return ADODB.DataTypeEnum.adCurrency

    '        Case "System.Double"
    '            Return ADODB.DataTypeEnum.adDouble

    '        Case "System.Int16"
    '            Return ADODB.DataTypeEnum.adSmallInt

    '        Case "System.Int32"
    '            Return ADODB.DataTypeEnum.adInteger

    '        Case "System.Int64"
    '            Return ADODB.DataTypeEnum.adBigInt

    '        Case "System.SByte"
    '            Return ADODB.DataTypeEnum.adTinyInt

    '        Case "System.Single"
    '            Return ADODB.DataTypeEnum.adSingle

    '        Case "System.UInt16"
    '            Return ADODB.DataTypeEnum.adUnsignedSmallInt

    '        Case "System.UInt32"
    '            Return ADODB.DataTypeEnum.adUnsignedInt

    '        Case "System.UInt64"
    '            Return ADODB.DataTypeEnum.adUnsignedBigInt

    '        Case "System.String"
    '            Return ADODB.DataTypeEnum.adVarChar
    '    End Select
    'End Function

    'Private Sub OpenXls(ByVal Ruta As String)
    '    Dim ps As New ProcessStartInfo
    '    ps.UseShellExecute = True
    '    ps.FileName = Ruta
    '    Process.Start(ps)
    'End Sub
End Class
