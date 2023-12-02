Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Reportes_frmConsultaRetenciones
    Inherits GCCBase

    Dim objLog As New GCCLog("frmConsultaRetenciones.aspx.vb")

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
        Try
            Dim strFechaInicial As String = txtFechaInicial.Value
            Dim strFechaFinal As String = txtFechaFinal.Value
            Dim objUtilNTx As New LUtilNTX
            Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarRetenciones(ConvierteFecha(strFechaInicial), ConvierteFecha(strFechaFinal)))
            Dim strResultado As String = ""
            Dim sbResultado As New StringBuilder("")
            sbResultado.Append(generarExcelHeaders())
            sbResultado.Append(generarExcelTitulos(strFechaInicial, strFechaFinal))
            sbResultado.Append(generarExcelFilas(dtRegCompra))
            sbResultado.Append(generarExcelFooter())

            strResultado = sbResultado.ToString
            Session("stream") = strResultado
            If Not strResultado.Trim.Equals("") Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "FrameScript", "window.open('frmRepRetenciones.aspx?archivo=Reporte_Retenciones.xls&aplicacion=application/excel');", True)
            End If

        Catch ex As Exception

        End Try
        

    End Sub
    Protected Sub btnGenerarSunat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarSunat.Click
        Try
            Dim strFechaInicial As String = txtFechaInicial.Value
            Dim strFechaFinal As String = txtFechaFinal.Value
            Dim objUtilNTx As New LUtilNTX
            Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarRetencionesSunat(ConvierteFecha(strFechaInicial), ConvierteFecha(strFechaFinal)))
            Dim strResultado As String = ""
            Dim sbResultado As New StringBuilder("")
            sbResultado.Append(generarExcelHeaders())
            sbResultado.Append(generarExcelTitulos(strFechaInicial, strFechaFinal))
            sbResultado.Append(generarExcelFilas(dtRegCompra))
            sbResultado.Append(generarExcelFooter())

            strResultado = sbResultado.ToString
            Session("stream") = strResultado
            If Not strResultado.Trim.Equals("") Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "FrameScript", "window.open('frmRepRetenciones.aspx?archivo=Reporte_Retenciones.xls&aplicacion=application/excel');", True)
            End If

        Catch ex As Exception

        End Try


    End Sub
    Public Function generarExcelHeaders() As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("      <?xml version=""1.0""?>                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <?mso-application progid=""Excel.Sheet""?>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""                                                                                   " & vbCrLf)
            sbCabecera.Append("       xmlns:o=""urn:schemas-microsoft-com:office:office""                                                                                               " & vbCrLf)
            sbCabecera.Append("       xmlns:x=""urn:schemas-microsoft-com:office:excel""                                                                                                " & vbCrLf)
            sbCabecera.Append("       xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""                                                                                         " & vbCrLf)
            sbCabecera.Append("       xmlns:html=""http://www.w3.org/TR/REC-html40"">                                                                                                   " & vbCrLf)
            sbCabecera.Append("       <DocumentProperties xmlns=""urn:schemas-microsoft-com:office:office"">                                                                            " & vbCrLf)
            sbCabecera.Append("        <Author>Gesfor- Osmos</Author>                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        <LastAuthor>LOGICA.Americo Estefanell, Andres</LastAuthor>                                                                                       " & vbCrLf)
            sbCabecera.Append("        <LastPrinted>2008-01-28T20:46:38Z</LastPrinted>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        <Created>2003-01-24T17:53:05Z</Created>                                                                                                          " & vbCrLf)
            sbCabecera.Append("        <LastSaved>2012-08-29T19:43:23Z</LastSaved>                                                                                                      " & vbCrLf)
            sbCabecera.Append("        <Version>14.00</Version>                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       </DocumentProperties>                                                                                                                             " & vbCrLf)
            sbCabecera.Append("       <OfficeDocumentSettings xmlns=""urn:schemas-microsoft-com:office:office"">                                                                        " & vbCrLf)
            sbCabecera.Append("        <AllowPNG/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       </OfficeDocumentSettings>                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                  " & vbCrLf)
            sbCabecera.Append("        <WindowHeight>6795</WindowHeight>                                                                                                                " & vbCrLf)
            sbCabecera.Append("        <WindowWidth>11580</WindowWidth>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        <WindowTopX>240</WindowTopX>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("        <WindowTopY>105</WindowTopY>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("        <ProtectStructure>False</ProtectStructure>                                                                                                       " & vbCrLf)
            sbCabecera.Append("        <ProtectWindows>False</ProtectWindows>                                                                                                           " & vbCrLf)
            sbCabecera.Append("       </ExcelWorkbook>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Styles>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""Default"" ss:Name=""Normal"">                                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Vertical=""Bottom""/>                                                                                                             " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" ss:Size=""9""/>                                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Interior/>                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("         <NumberFormat/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("         <Protection/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s64"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s65"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s66"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s67"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom""/>                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s68"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s69"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s70"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s71"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s72"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s73"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom""/>                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s74"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s75"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>                                                                  " & vbCrLf)
            sbCabecera.Append("         <Borders>                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                   " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                     " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                      " & vbCrLf)
            sbCabecera.Append("         </Borders>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s76"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>                                                                  " & vbCrLf)
            sbCabecera.Append("         <Borders>                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                   " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                     " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                      " & vbCrLf)
            sbCabecera.Append("         </Borders>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s77"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""dd/mm/yyyy;@""/>                                                                                                      " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s78"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""dd/mm/yyyy;@""/>                                                                                                      " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s79"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,##0.0000""/>                                                                                                        " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s80"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>                                                                  " & vbCrLf)
            sbCabecera.Append("         <Borders>                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                   " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                     " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                      " & vbCrLf)
            sbCabecera.Append("         </Borders>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,##0.0000""/>                                                                                                        " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s81"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom""/>                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s82"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s83"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Borders/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s84"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s85"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,##0.0000""/>                                                                                                        " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s86"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Vertical=""Bottom""/>                                                                                                             " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s87"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Vertical=""Bottom""/>                                                                                                             " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9"" ss:Bold=""1""/>                                                                    " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s88"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom""/>                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""9""/>                                                                                  " & vbCrLf)
            sbCabecera.Append("         <Interior ss:Color=""#FFFF00"" ss:Pattern=""Solid""/>                                                                                           " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""#,###,###,##0.00""/>                                                                                                  " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Style ss:ID=""s92"">                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""12"" ss:Bold=""1""/>                                                                   " & vbCrLf)
            sbCabecera.Append("         <NumberFormat ss:Format=""@""/>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("        </Style>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       </Styles>                                                                                                                                         " & vbCrLf)

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function generarExcelTitulos(ByVal strfini As String, ByVal strFfin As String) As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("       <Worksheet ss:Name=""Retenciones de 6% de IGV"">                                                                                                  " & vbCrLf)
            sbCabecera.Append("        <Names>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <NamedRange ss:Name=""_FilterDatabase""                                                                                                         " & vbCrLf)
            sbCabecera.Append("          ss:RefersTo=""='Retenciones de 6% de IGV'!R9C2:R9C15"" ss:Hidden=""1""/>                                                                       " & vbCrLf)
            sbCabecera.Append("         <NamedRange ss:Name=""Print_Titles""                                                                                                            " & vbCrLf)
            sbCabecera.Append("          ss:RefersTo=""='Retenciones de 6% de IGV'!R1:R9""/>                                                                                            " & vbCrLf)
            sbCabecera.Append("        </Names>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <Table ss:ExpandedColumnCount=""15"" ss:ExpandedRowCount=""12984"" x:FullColumns=""1""                                                           " & vbCrLf)
            sbCabecera.Append("         x:FullRows=""1"" ss:StyleID=""s64"" ss:DefaultColumnWidth=""84""                                                                                " & vbCrLf)
            sbCabecera.Append("         ss:DefaultRowHeight=""12"">                                                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s64"" ss:AutoFitWidth=""0"" ss:Width=""11.25""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s65"" ss:AutoFitWidth=""0"" ss:Width=""53.25""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s65"" ss:AutoFitWidth=""0"" ss:Width=""75""/>                                                                              " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s66"" ss:AutoFitWidth=""0"" ss:Width=""68.25""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s77"" ss:AutoFitWidth=""0"" ss:Width=""68.25""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s67"" ss:AutoFitWidth=""0"" ss:Width=""63.75""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s67"" ss:AutoFitWidth=""0"" ss:Width=""197.25""/>                                                                          " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s66"" ss:AutoFitWidth=""0"" ss:Width=""73.5"" ss:Span=""1""/>                                                              " & vbCrLf)
            sbCabecera.Append("         <Column ss:Index=""10"" ss:StyleID=""s68"" ss:AutoFitWidth=""0"" ss:Width=""83.25""                                                             " & vbCrLf)
            sbCabecera.Append("          ss:Span=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("         <Column ss:Index=""12"" ss:StyleID=""s79"" ss:AutoFitWidth=""0"" ss:Width=""65.25""/>                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s68"" ss:AutoFitWidth=""0"" ss:Width=""78.75""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s69"" ss:AutoFitWidth=""0"" ss:Width=""83.25""/>                                                                           " & vbCrLf)
            sbCabecera.Append("         <Column ss:StyleID=""s64"" ss:AutoFitWidth=""0"" ss:Width=""222.75""/>                                                                          " & vbCrLf)
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""12.75"" ss:Span=""1""/>                                                                                 " & vbCrLf)
            sbCabecera.Append("         <Row ss:Index=""5"" ss:AutoFitHeight=""0"" ss:Height=""9"">                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s70""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s71""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s71""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""15.75"">                                                                                                " & vbCrLf)
            sbCabecera.Append("          <Cell ss:Index=""2"" ss:MergeAcross=""13"" ss:StyleID=""s92""><Data ss:Type=""String"">RETENCIONES DE 6% DEL IGV</Data><NamedCell              " & vbCrLf)
            sbCabecera.Append("            ss:Name=""Print_Titles""/></Cell>                                                                                                            " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""15.75"">                                                                                                " & vbCrLf)
            sbCabecera.Append("          <Cell ss:Index=""2"" ss:MergeAcross=""13"" ss:StyleID=""s92""><Data ss:Type=""String"">DE " + strfini + " AL " + strFfin + "</Data><NamedCell            " & vbCrLf)
            sbCabecera.Append("            ss:Name=""Print_Titles""/></Cell>                                                                                                            " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""13.5"">                                                                                                 " & vbCrLf)
            sbCabecera.Append("          <Cell ss:Index=""4"" ss:StyleID=""s72""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                           " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s78""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s73""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s73""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s72""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s72""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s74""><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                          " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""39"">                                                                                                   " & vbCrLf)
            sbCabecera.Append("          <Cell ss:Index=""2"" ss:StyleID=""s75""><Data ss:Type=""String"">Fecha Emisión </Data><NamedCell                                               " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">Nro Comprobante</Data><NamedCell                                                             " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">Tipo Documento</Data><NamedCell                                                              " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">Fecha Emisión Documento</Data><NamedCell                                                     " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">Nro. Doc.</Data><NamedCell                                                                   " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">Proveedor</Data><NamedCell                                                                   " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">RUC</Data><NamedCell                                                                         " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s75""><Data ss:Type=""String"">Moneda</Data><NamedCell                                                                      " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s76""><Data ss:Type=""String"">Monto</Data><NamedCell                                                                       " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s76""><Data ss:Type=""String"">Retención USD</Data><NamedCell                                                               " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s80""><Data ss:Type=""String"">Tipo Cambio</Data><NamedCell                                                                 " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s76""><Data ss:Type=""String"">Retención S/.</Data><NamedCell                                                               " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s76""><Data ss:Type=""String"">Total a Pagar</Data><NamedCell                                                               " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s76""><Data ss:Type=""String"">Referencia</Data><NamedCell                                                                  " & vbCrLf)
            sbCabecera.Append("            ss:Name=""_FilterDatabase""/><NamedCell ss:Name=""Print_Titles""/></Cell>                                                                    " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function generarExcelFilas(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrJ As Integer

        Try
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = CType((nbrTotal + IIf(tabla.Rows(nbrJ)(11) Is DBNull.Value, 0, tabla.Rows(nbrJ)(11))), Decimal)
                sbCabecera.Append("         <Row>                                                                                                                                           " & vbCrLf)
                sbCabecera.Append("          <Cell ss:Index=""2""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                         " & vbCrLf)
                'sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("FechaEmision") + "</Data></Cell>                                                                                   " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + Trim(tabla.Rows(nbrJ).Item("NumComprobante")) + "</Data></Cell>                                                                         " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + Trim(tabla.Rows(nbrJ).Item("TipoDocumento")) + "</Data></Cell>                                                                         " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("FechaEmision") + "</Data></Cell>                                                                                    " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("NroDocumento") + "</Data></Cell>                                                " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("Proveedor") + "</Data></Cell>                                                                                   " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("RUC") + "</Data></Cell>                                                                                   " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("NombreMoneda") + "</Data></Cell>                                                                                          " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + CType(Trim(tabla.Rows(nbrJ).Item("Monto")), String) + "</Data></Cell>                                                                                           " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + CType(Trim(tabla.Rows(nbrJ).Item("RetencionUS")), String) + "</Data></Cell>                                                                                " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + CType(Trim(tabla.Rows(nbrJ).Item("MontoValorVenta")), String) + "</Data></Cell>                                                                                " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + CType(Trim(tabla.Rows(nbrJ).Item("RetencionSO")), String) + "</Data></Cell>                                                                                " & vbCrLf)
                'sbCabecera.Append("          <Cell><Data ss:Type=""Number"">" + tabla.Rows(nbrJ).Item("Total") + "</Data></Cell>                                                                                " & vbCrLf)
                sbCabecera.Append("          <Cell ss:StyleID=""s88""><Data ss:Type=""String"">" + CType(Trim(tabla.Rows(nbrJ).Item("Total")), String) + "</Data></Cell>                                                                        " & vbCrLf)
                'sbCabecera.Append("          <Cell><Data ss:Type=""Number"">" + tabla.Rows(nbrJ)(12) + "</Data></Cell>                                                                                " & vbCrLf)
                sbCabecera.Append("          <Cell><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("Referencia") + "</Data></Cell>                                                                " & vbCrLf)
                sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)

                nbrJ = nbrJ + 1
            Next row

            sbCabecera.Append("         <Row>                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("          <Cell ss:Index=""4"" ss:StyleID=""s72""/>                                                                                                      " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s78""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s73""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s73""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s72""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s72""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s74""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""12.75"">                                                                                                " & vbCrLf)
            sbCabecera.Append("          <Cell ss:Index=""4"" ss:StyleID=""s72""/>                                                                                                      " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s78""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s73""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s81""><Data ss:Type=""String"">TOTALES SOLES</Data></Cell>                                                                  " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s82""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s82""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s83""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s84""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s85""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s84""><Data ss:Type=""String"">" + CType(nbrTotal, String) + "</Data></Cell>                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s86""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Cell ss:StyleID=""s87""/>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("         </Row>                                                                                                                                          " & vbCrLf)


            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function generarExcelFooter() As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("         <Row ss:AutoFitHeight=""0"" ss:Height=""12.75"" ss:Span=""6441""/>                                                                              " & vbCrLf)
            sbCabecera.Append("        </Table>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                              " & vbCrLf)
            sbCabecera.Append("         <PageSetup>                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("          <Layout x:Orientation=""Landscape""/>                                                                                                          " & vbCrLf)
            sbCabecera.Append("          <Header x:Margin=""0""/>                                                                                                                       " & vbCrLf)
            sbCabecera.Append("          <Footer x:Margin=""0"" x:Data=""&amp;LFecha: &amp;D&amp;CB20600&amp;RPágina &amp;P/&amp;N""/>                                                  " & vbCrLf)
            sbCabecera.Append("         </PageSetup>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("         <Print>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("          <ValidPrinterInfo/>                                                                                                                            " & vbCrLf)
            sbCabecera.Append("          <PaperSizeIndex>9</PaperSizeIndex>                                                                                                             " & vbCrLf)
            sbCabecera.Append("          <Scale>58</Scale>                                                                                                                              " & vbCrLf)
            sbCabecera.Append("          <HorizontalResolution>600</HorizontalResolution>                                                                                               " & vbCrLf)
            sbCabecera.Append("          <VerticalResolution>600</VerticalResolution>                                                                                                   " & vbCrLf)
            sbCabecera.Append("         </Print>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("         <PageBreakZoom>60</PageBreakZoom>                                                                                                               " & vbCrLf)
            sbCabecera.Append("         <Selected/>                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("         <Panes>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("          <Pane>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("           <Number>3</Number>                                                                                                                            " & vbCrLf)
            sbCabecera.Append("           <ActiveRow>17</ActiveRow>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("           <ActiveCol>4</ActiveCol>                                                                                                                      " & vbCrLf)
            sbCabecera.Append("          </Pane>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("         </Panes>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("         <ProtectObjects>False</ProtectObjects>                                                                                                          " & vbCrLf)
            sbCabecera.Append("         <ProtectScenarios>False</ProtectScenarios>                                                                                                      " & vbCrLf)
            sbCabecera.Append("        </WorksheetOptions>                                                                                                                              " & vbCrLf)
            sbCabecera.Append("        <AutoFilter x:Range=""R9C2:R9C15""                                                                                                               " & vbCrLf)
            sbCabecera.Append("         xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                               " & vbCrLf)
            sbCabecera.Append("        </AutoFilter>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       </Worksheet>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      </Workbook>                                                                                                                                        " & vbCrLf)


            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function ConvierteFecha(ByVal StrFecha As String) As String
        Dim datofecha() As String
        Dim Dato1 As String = StrFecha.ToString()
        Dim Dia As String = String.Empty
        Dim Mes As String = String.Empty
        Dim Anio As String = String.Empty
        If Not String.IsNullOrEmpty(StrFecha) Then
            datofecha = Split(Dato1, "/")
            Dia = datofecha(0)
            Mes = datofecha(1)
            Anio = datofecha(2)
        End If
        Return Anio + "" + Mes + "" + Dia

    End Function
End Class
