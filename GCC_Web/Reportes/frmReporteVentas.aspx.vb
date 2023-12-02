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
Imports System.Text
Partial Class Reportes_frmReporteVentas
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepRegVenta.aspx.vb")
    Dim oDocument As Object
    Dim ruta As String = HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/LSG_rptRegVentasNuevo.xlsm")
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

        Dim strFechaIni As String = txtFechaInicial.Value
        Dim strFechaFin As String = txtFechaFinal.Value
        Dim strFlag As String = "1"

        'Dim rutalocal As String
        Dim objUtilNTx As New LUtilNTX
        Dim table As DataTable 'variable para recorrer las tablas
        'rutalocal = HttpContext.Current.Server.MapPath("../temp")
        'Dim archivo As String = rutalocal + "\ReporteVentas.xlsm"


        Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../temp/ReporteVentas.xlsm"))
        Dim template As New FileInfo(HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/LSG_rptRegVentasNuevo.xlsm"))
        Try

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListaRegistroVentas(strFlag, strFechaIni, strFechaFin))

            If Not Directory.Exists(HttpContext.Current.Server.MapPath("../temp")) Then
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../temp"))
            End If
            If File.Exists(newFile.ToString()) Then
                File.Delete(newFile.ToString())
            End If

            Dim pck As New ExcelPackage(newFile, template)
            Dim worksheet As ExcelWorksheet = pck.Workbook.Worksheets("Registro de Ventas")
            Dim col As Integer = 1
            Dim row As Integer = 13

            For Each rw As DataRow In table.Rows
                For Each cl As DataColumn In table.Columns
                    If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then
                        Dim RowData As String = rw(cl.ColumnName).ToString()
                        worksheet.Cells(row, col).Value = RowData.Trim().ToString()
                    End If
                    col += 1
                Next
                row += 1
                col = 1
            Next
            pck.Save()
            Response.Redirect("../temp/ReporteVentas.xlsm", False)
            '"C:\Files\LSG_rptRegVentasNuevo.xlt"
            'Dim Ap As Object
            'Dim Wb As Object
            'Ap = Server.CreateObject("Excel.Application")
            'Wb = Ap.workbooks.Open(ruta)
            'Ap.run("Main", ConvertToRecordset(table), 2002, CType(strFechaIni, Date).ToShortDateString.ToString(), CType(strFechaFin, Date).ToShortDateString.ToString(), User.Identity.Name.ToString())
            'Wb.Parent.Visible = False
            'Ap.ActiveSheet.SaveAs(archivo)

            'Wb.Close()
            'Ap.Quit()
            'Wb = Nothing
            'Ap = Nothing
            'Err.Clear()
            'OpenXls(rutaUsuario)
            'Response.Clear()
            'Response.Buffer = True
            'Response.AddHeader("Content-disposition", "attachment; filename=ReporteVentas1.xlsm")
            'Response.ContentType = "application/vnd.ms-excel.sheet.macroEnabled.12"
            ''Response.Redirect(archivo, True)
            'Response.TransmitFile(archivo)
            'Response.End()
            'FileOpen(0, archivo, OpenMode.Append, OpenAccess.Read, OpenShare.Default, 1)
            'File.Open(archivo, FileMode.Open, FileAccess.ReadWrite)
            'Wb.Parent.Visible = True

            'fnFileOpen(rutaUsuario)
            'If Not Directory.Exists(rutaUsuario) Then
            '    Directory.CreateDirectory(rutaUsuario)
            'End If
            'rutaUsuario = rutaUsuario + "\ReporteVentas.xlsm"
            'File.Copy(archivo, rutaUsuario, True)
            'Dim rutaUsuario As String = "\\10.10.82.151\Compartido"
            '"C:\Documents and Settings\" + My.User.Name.ToString() + "\Mis documentos\Compartido"
            '"\\oracle11g\SDA\IIS\GCC\temp\macros"
            'Server.MapPath("wwwroot")

            'File.Delete(archivo)
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

    'Private Function fnFileOpen(ByVal FilePath As String) As Boolean
    '    'This function checks to see if the given file is open

    '    Dim fs As System.IO.FileStream

    '    Dim blnFileOpen As Boolean = True

    '    Try

    '        ' If Open() succeeds, then we know the file is not currently in use.

    '        fs = System.IO.File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)

    '        blnFileOpen = False

    '        'fs.Close()

    '    Catch ex As Exception

    '        ' any "file already in use" code here

    '    End Try

    '    fnFileOpen = blnFileOpen
    'End Function

    'Private Function AbrirGuardar() As Boolean

    '    Dim OExcel As Object
    '    Dim OSheet As Object
    '    'Dim ruta As String
    '    OExcel = CreateObject("Excel.Application")
    '    OExcel.Workbooks.Add()
    '    OExcel.ActiveSheet.Cells(2, 2) = "contenido del text"
    '    OExcel.ActiveSheet.Cells(3, 4) = "contenido del label"

    '    'ruta = App.Path & "\Archivo-fecha" & Format(Date, "dd-mm-yy") & _
    '    '"-hora" & Format(Time, "hh-mm-ss") & ".xls"

    '    ' guarda
    '    OExcel.ActiveSheet.SaveAs("\\oracle11g\SDA\TMP\GCC\Prueba.xls")
    '    'cierra el Exc.
    '    OExcel.Quit()

    '    OSheet = Nothing
    '    OExcel = Nothing
    '    AbrirGuardar = True
    '    Exit Function
    '    On Error Resume Next
    '    OSheet = Nothing
    '    OExcel = Nothing
    '    Err.Clear()
    'End Function

    'Private Sub GuardarArchivo(ByVal file__1 As HttpPostedFile)
    '    ' Se carga la ruta física de la carpeta temp del sitio
    '    Dim ruta As String = Server.MapPath("~/temp")

    '    ' Si el directorio no existe, crearlo
    '    If Not Directory.Exists(ruta) Then
    '        Directory.CreateDirectory(ruta)
    '    End If

    '    Dim archivo As String = [String].Format("{0}\{1}", ruta, file__1.FileName)

    '    ' Verificar que el archivo no exista
    '    If File.Exists(archivo) Then
    '        'MensajeError([String].Format("Ya existe una imagen con nombre""{0}"".", file__1.FileName))
    '    Else
    '        file__1.SaveAs(archivo)
    '    End If
    'End Sub
End Class
