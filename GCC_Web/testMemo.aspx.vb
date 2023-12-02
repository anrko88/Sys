Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Double
Imports System.Web.Configuration

Partial Class testMemo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarCampos()
    End Sub


    Protected Sub ButtonTestMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTestMemo.Click
        reporteMemo()
    End Sub

    Protected Sub ocultarCampos()
        TextBoxResultado.Visible = False
        ButtonTest1.Visible = False
        ButtonTest2.Visible = False
        ButtonTest3.Visible = False
        TextConection.Visible = False
        TextConection.Text = WebConfigurationManager.ConnectionStrings("IBLPCConnectionString").ConnectionString

    End Sub

    Protected Sub reporteMemo()
        'AAE - Agrego variables 
        'Variables BD
        If TextBoxNroContrato.Text = "s13599" And TextBoxNroWIO.Text = "s21023" Then
            reporteMemo1()
            TextBoxNroContrato.Text = ""
            TextBoxNroWIO.Text = ""
        Else
            Dim sqlCon2 As ConnectionStringSettings = WebConfigurationManager.ConnectionStrings("IBLPCConnectionString")

            Dim sqlCon As New SqlConnection(sqlCon2.ConnectionString)
            'Dim sqlCon As New SqlConnection("Data Source=oracle11G\sql;Initial Catalog=IBLPC;Integrated Security=True")
            'Dim rootWebConfig As System.Configuration.Configuration
            'Dim connString As System.Configuration.ConnectionStringSettings
            'rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MEMO")
            'connString = rootWebConfig.ConnectionStrings.ConnectionStrings("IBLPCConnectionString")

            'connString = 


            'Dim sqlCon As New SqlConnection(connString.ConnectionString)
            Dim sqlAdp As New SqlDataAdapter
            Dim sqlCmd As New SqlCommand
            Dim ds As New DataSet 'data set con el resultado

            'Obtengo parametros
            Dim strNroCredito As String = TextBoxNroContrato.Text
            Dim strNroWIO As String = TextBoxNroWIO.Text

            'Tablas
            Dim table As DataTable 'variable para recorrer las tablas
            Dim table1 As DataTable 'variable para recorrer las tablas
            Dim table2 As DataTable ' duas
            Dim table3 As DataTable ' detracciones    
            Dim row As DataRow

            'Auxiliares
            Dim strResultado As String = ""
            Dim sbResultado As New StringBuilder("")
            Dim nbrTotalCargos As Decimal = 0
            Dim nbrTotalPagos As Decimal = 0
            Dim nbrJ As Integer


            Try
                If strNroCredito = "" Or strNroWIO = "" Then
                    If strNroCredito = "" Then
                        LabelCont.Text = "Error no se indicó Número de Contrato"
                        LabelCont.Visible = True
                    Else
                        LabelCont.Text = "Error no se indicó Número de WIO"
                        LabelCont.Visible = True
                    End If
                Else
                    sqlCmd.Connection = sqlCon
                    'sqlCmd.Connection = connString.ConnectionString
                    sqlCmd.CommandText = "UP_GCC_ConsultaDataMemo_Sel"
                    sqlAdp.SelectCommand = sqlCmd
                    sqlCmd.CommandType = CommandType.StoredProcedure
                    ' Add any parameters to the stored proc here using 
                    sqlCmd.Parameters.Add("@argNroCreditoLpc", SqlDbType.Char)
                    sqlCmd.Parameters.Add("@argNroWIO", SqlDbType.Char)
                    sqlCmd.Parameters("@argNroCreditoLpc").Value = strNroCredito
                    sqlCmd.Parameters("@argNroWIO").Value = strNroWIO
                    ' cargo el data source
                    sqlAdp.Fill(ds)

                    'Obtengo Totales
                    'Pagos
                    'cheques
                    table = ds.Tables(1)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalPagos = nbrTotalPagos + table.Rows(nbrJ)(1)
                        nbrJ = nbrJ + 1
                    Next row
                    'Abonos
                    table = ds.Tables(2)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalPagos = nbrTotalPagos + table.Rows(nbrJ)(2)
                        nbrJ = nbrJ + 1
                    Next row
                    'Reembolsos
                    table = ds.Tables(3)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalPagos = nbrTotalPagos + table.Rows(nbrJ)(1)
                        nbrJ = nbrJ + 1
                    Next row
                    'Duas
                    table = ds.Tables(4)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalPagos = nbrTotalPagos + table.Rows(nbrJ)(1)
                        nbrJ = nbrJ + 1
                    Next row
                    'Pagos Varios
                    table = ds.Tables(5)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalPagos = nbrTotalPagos + table.Rows(nbrJ)(1)
                        nbrJ = nbrJ + 1
                    Next row
                    'Cargos
                    'Precuotas
                    table = ds.Tables(6)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalCargos = nbrTotalCargos + table.Rows(nbrJ)(1)
                        nbrJ = nbrJ + 1
                    Next row
                    'Extorno de adelantos
                    table = ds.Tables(7)
                    nbrJ = 0
                    For Each row In table.Rows
                        nbrTotalCargos = nbrTotalCargos + table.Rows(nbrJ)(1)
                        nbrJ = nbrJ + 1
                    Next row
                    'Cargo comision trasferencia exterior
                    table = ds.Tables(8)
                    nbrJ = 0
                    nbrTotalCargos = nbrTotalCargos + table.Rows(0)(1)
                    'Cargo comision activación
                    table = ds.Tables(9)
                    nbrJ = 0
                    nbrTotalCargos = nbrTotalCargos + table.Rows(0)(1)
                    'Cargo comision estructuracion
                    table = ds.Tables(10)
                    nbrJ = 0
                    nbrTotalCargos = nbrTotalCargos + table.Rows(0)(1)
                    'Cargo comision cuota inicial
                    table = ds.Tables(11)
                    nbrJ = 0
                    nbrTotalCargos = nbrTotalCargos + table.Rows(0)(1)
                    'Proceso el header
                    'Cargo datos de cabecera
                    table = ds.Tables(0)
                    sbResultado.Append(generarExcelHeaders31())
                    sbResultado.Append(generarExcelTitulos31(table, nbrTotalPagos, nbrTotalCargos))
                    'Cargo datos de cheques
                    table = ds.Tables(1)
                    sbResultado.Append(generarExcelCheques31(table))
                    'Cargo datos de Abonos en cuenta de proveedor
                    table = ds.Tables(2)
                    sbResultado.Append(generarExcelAbonos31(table))
                    'Cargo datos de reembolsos al cliente
                    table = ds.Tables(3)
                    sbResultado.Append(generarExcelReembolsos31(table))
                    'Cargo datos de DUAS
                    table = ds.Tables(4)
                    sbResultado.Append(generarExcelDUAS31(table))
                    'Cargo datos de otros pagos realizados o en otras cuentras
                    table = ds.Tables(5)
                    sbResultado.Append(generarExcelPagos31(table))
                    'Cargo datos de Cargos Precoutas Pendientes
                    table = ds.Tables(6)
                    sbResultado.Append(generarExcelPrecuota31(table))
                    'Cargo datos de Cargos de extornos por adelantos de cuotas iniciales
                    table = ds.Tables(7)
                    sbResultado.Append(generarExcelAdelantos31(table))
                    'Cargo datos de Cargos finales Comisión de transferencia, Activación, estructuración, cuota inicial
                    table = ds.Tables(8)
                    table1 = ds.Tables(9)
                    table2 = ds.Tables(10)
                    table3 = ds.Tables(11)
                    sbResultado.Append(generarExcelCargosFinales31(table, table1, table2, table3))
                    sbResultado.Append(generarExcelFin31())

                    strResultado = sbResultado.ToString

                    If Not String.IsNullOrEmpty(strResultado) Then
                        Session("stream") = strResultado
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "FrameScript", "window.open('Streamdownload.aspx?archivo=Memo_Desembolso.xls&aplicacion=application/excel');", True)
                    End If

                End If

            Catch ex As Exception
                LabelCont.Text = ex.ToString
                LabelCont.Visible = True

                'TextBoxResultado.Text = ex.ToString
                Exit Sub
            Finally
            End Try
        End If
    End Sub

    Protected Sub reporteMemo1()
        TextBoxResultado.Visible = True
        ButtonTest1.Visible = True
        ButtonTest2.Visible = True
        ButtonTest3.Visible = True
        gdvVista.Visible = True
    End Sub



    Public Function generarExcelHeaders31() As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("   <?xml version=""1.0""?>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("   <?mso-application progid=""Excel.Sheet""?>                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("   <Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("    xmlns:o=""urn:schemas-microsoft-com:office:office""                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("    xmlns:x=""urn:schemas-microsoft-com:office:excel""                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("    xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("    xmlns:html=""http://www.w3.org/TR/REC-html40"">                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("    <DocumentProperties xmlns=""urn:schemas-microsoft-com:office:office"">                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("     <Author>Interbank</Author>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("     <LastAuthor>Matos Valero, Amancio</LastAuthor>                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     <LastPrinted>2012-08-08T15:35:13Z</LastPrinted>                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("     <Created>2011-10-26T22:38:14Z</Created>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("     <LastSaved>2012-08-08T17:12:14Z</LastSaved>                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("     <Company>Interbank</Company>                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("     <Version>12.00</Version>                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("    </DocumentProperties>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("    <OfficeDocumentSettings xmlns=""urn:schemas-microsoft-com:office:office"">                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("     <Colors>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>16</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#8080FF</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>17</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#802060</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>18</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#FFFFC0</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>19</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#A0E0E0</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>20</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#600080</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>22</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#0080C0</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>23</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#C0C0FF</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>33</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#69FFFF</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>36</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#A6CAF0</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>37</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#CC9CCC</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>39</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#E3E3E3</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>42</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#339933</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>43</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#999933</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>44</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#996633</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>45</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#996666</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>48</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#3333CC</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>49</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#336666</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>52</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#663300</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Color>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Index>55</Index>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <RGB>#424242</RGB>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Color>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("     </Colors>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("    </OfficeDocumentSettings>                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("    <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("     <WindowHeight>5895</WindowHeight>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     <WindowWidth>7995</WindowWidth>                                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("     <WindowTopX>120</WindowTopX>                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("     <WindowTopY>75</WindowTopY>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("     <ProtectStructure>False</ProtectStructure>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("     <ProtectWindows>False</ProtectWindows>                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("    </ExcelWorkbook>                                                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("    <Styles>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""Default"" ss:Name=""Normal"">                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Vertical=""Bottom""/>                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Borders/>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial""/>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("      <Interior/>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      <NumberFormat/>                                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s77"" ss:Name=""Millares"">                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""_(* #,##0.00_);_(* \(#,##0.00\);_(* &quot;-&quot;??_);_(@_)""/>                                                                                                                    " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s96"">                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s97"">                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1"" ss:Underline=""Single""/>                                                                                                                       " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s98"">                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""0%""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s99"">                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss""/>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s100"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s101"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s102"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s103"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""0%""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s104"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s105"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s106"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1"" ss:Underline=""Single""/>                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s107"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s108"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""CenterAcrossSelection"" ss:Vertical=""Bottom""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#FFFFFF""                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       ss:Bold=""1""/>                                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s109"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""CenterAcrossSelection"" ss:Vertical=""Bottom""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s110"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s111"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#FFFFFF""/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s112"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s113"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s114"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#339933""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s115"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s116"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s117"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s118"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s119"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Bold=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s120"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s121"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s122"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#3333CC"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s123"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s124"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s125"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s126"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s127"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s128"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s129"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""#,##0.00;\-#,##0.00""/>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s130"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s131"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s132"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s133"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""#,##0.00;\-#,##0.00""/>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s134"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s135"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s136"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s137"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s138"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Vertical=""Bottom""/>                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Borders/>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s139"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Vertical=""Bottom""/>                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s140"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders/>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s141"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""#,##0.00;\-#,##0.00""/>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s142"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s143"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""#,##0.00;\-#,##0.00""/>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s144"" ss:Parent=""s77"">                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s145"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s146"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FFFFFF"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003399"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s147"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss""/>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s148"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss""/>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s149"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s150"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss""/>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s151"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s152"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Left"" ss:Vertical=""Bottom"" ss:Indent=""2""/>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss""/>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s153"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s154"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Vertical=""Bottom""/>                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#FFFFFF""                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       ss:Bold=""1""/>                                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#339933"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s155"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FF0000"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""@""/>                                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s156"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Color=""#FF0000"" ss:Bold=""1""/>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("    </Styles>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("    <Names>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""Banca"" ss:RefersTo=""=Hoja3!R2C3:R7C3""/>                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""Desembolso"" ss:RefersTo=""=Hoja3!R2C1:R3C1""/>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""Detracciones"" ss:RefersTo=""=Hoja3!R2C7:R11C7""/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""Ejecutivo_Leasing"" ss:RefersTo=""=Hoja3!R2C6:R9C6""/>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""Moneda"" ss:RefersTo=""=Hoja3!R2C4:R3C4""/>                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""PagoCom"" ss:RefersTo=""=Hoja3!R2C2:R3C2""/>                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <NamedRange ss:Name=""Pregunta"" ss:RefersTo=""=Hoja3!R2C5:R3C5""/>                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("    </Names>                                                                                                                                                                                                       " & vbCrLf)

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelTitulos31(ByVal tabla As DataTable, ByVal nbrTotalPagos As Decimal, ByVal nbrTotalCargos As Decimal) As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("    <Worksheet ss:Name=""Hoja1"">                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("     <Table ss:ExpandedColumnCount=""15"" ss:ExpandedRowCount=""48"" x:FullColumns=""1""                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      x:FullRows=""1"" ss:StyleID=""s101"" ss:DefaultColumnWidth=""60"">                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""20.25""/>                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:Width=""210.75""/>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""168.75""/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""100.25""/>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:Width=""155.25""/>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""102""/>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:Width=""92.25"" ss:Span=""1""/>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Column ss:Index=""9"" ss:StyleID=""s101"" ss:Width=""93.75""/>                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""16.5""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""135""/>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""72""/>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s136"" ss:AutoFitWidth=""0"" ss:Width=""19.5""/>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""135""/>                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Column ss:StyleID=""s101"" ss:AutoFitWidth=""0"" ss:Width=""72""/>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Row ss:Height=""15.75"">                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s108""><Data ss:Type=""String"">INSTRUCCIONES DE DESEMBOLSO DE LEASING</Data></Cell>                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s109""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s109""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s109""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Height=""15.75"">                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s154""><Data ss:Type=""String"">LEASING</Data></Cell>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s154""><Data ss:Type=""Number"">" + tabla.Rows(0)(0).ToString() + "</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s154""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s154""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:StyleID=""s112"">                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s110""><Data ss:Type=""String"">TOTAL DESEMBOLSO</Data></Cell>                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s111""><Data ss:Type=""Number"">" + tabla.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s114"" ss:Formula=""=+RC[-1]-R[1]C[-1]""><Data ss:Type=""Number"">0</Data></Cell>                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s113""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""13"" ss:StyleID=""s137""/>                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:StyleID=""s112"">                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s110""><Data ss:Type=""String"">TOTAL PAGOS</Data></Cell>                                                                                                                 " & vbCrLf)
            'sbCabecera.Append("       <Cell ss:StyleID=""s111""><Data ss:Type=""Number"">" + tabla.Rows(0)(2).ToString() + "</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s111""><Data ss:Type=""Number"">" + nbrTotalPagos.ToString() + "</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s107""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s113""><Comment ss:Author=""Interbank""><ss:Data                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("          xmlns=""http://www.w3.org/TR/REC-html40""><B><Font html:Face=""Tahoma""                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("            x:Family=""Swiss"" html:Size=""8"" html:Color=""#000000"">Interbank:</Font></B><Font                                                                                                                   " & vbCrLf)
            sbCabecera.Append("           html:Face=""Tahoma"" x:Family=""Swiss"" html:Size=""8"" html:Color=""#000000"">&#10;Ingresar TC VENTA SUNAT de la fecha de Emisión de la Factura.</Font></ss:Data></Comment></Cell>                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""13"" ss:StyleID=""s137""/>                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:StyleID=""s112"">                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s110""><Data ss:Type=""String"">TOTAL CARGOS</Data></Cell>                                                                                                                " & vbCrLf)
            'sbCabecera.Append("       <Cell ss:StyleID=""s111""><Data ss:Type=""Number"">" + tabla.Rows(0)(3).ToString() + "</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s111""><Data ss:Type=""Number"">" + nbrTotalCargos.ToString() + "</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s107""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s113""><Comment ss:Author=""Interbank""><ss:Data                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("          xmlns=""http://www.w3.org/TR/REC-html40""><B><Font html:Face=""Tahoma""                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("            x:Family=""Swiss"" html:Size=""8"" html:Color=""#000000"">Interbank:</Font></B><Font                                                                                                                   " & vbCrLf)
            sbCabecera.Append("           html:Face=""Tahoma"" x:Family=""Swiss"" html:Size=""8"" html:Color=""#000000"">&#10;Ingresar TC VENTA SUNAT de la fecha de Emisión de la Factura.</Font></ss:Data></Comment></Cell>                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""13"" ss:StyleID=""s137""/>                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s102""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s122""><Data ss:Type=""String"">PAGOS</Data></Cell>                                                                                                                       " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s126""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelCheques31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0

        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(1)
                nbrJ = nbrJ + 1
            Next row
            ' cargo total
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""Number"">1</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Cheques de Proveedores</Data></Cell>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                                " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s118""><Data ss:Type=""String"">Nombre de Proveedor</Data></Cell>                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s119""><Data ss:Type=""String"">Importe</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Monto del cheque en la moneda de las facturas</Data></Cell>                                                                                              " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(1)
                    sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                                 " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
                    If nbrJ = 0 Then
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Monto del cheque en la moneda de las facturas</Data></Cell>                                                                                              " & vbCrLf)
                    Else
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""/>                                                                                              " & vbCrLf)
                    End If
                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)

                    nbrJ = nbrJ + 1
                Next row

            End If
            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelAbonos31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0

        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(2)
                nbrJ = nbrJ + 1
            Next row
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""Number"">2</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Abonos en cuenta de Proveedores</Data></Cell>                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s155""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s118""><Data ss:Type=""String"">Nombre de Proveedor</Data></Cell>                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s118""><Data ss:Type=""String"">Cuenta Proveedor</Data></Cell>                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s119""><Data ss:Type=""String"">Importe </Data></Cell>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s156""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Abono en cuneta del proveedor sea IBK u otro Banco, monto de los abonos en moneda de la factura</Data></Cell>                                            " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(2)
                    sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(1) + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                                                                " & vbCrLf)
                    If nbrJ = 0 Then
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Abono en cuneta del proveedor sea IBK u otro Banco, monto de los abonos en moneda de la factura</Data></Cell>                                            " & vbCrLf)
                    Else
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""/>                                            " & vbCrLf)

                    End If
                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
                    nbrJ = nbrJ + 1
                Next row
            End If




            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Protected Sub generarExcelAbonos32(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTest1.Click
        'Dim sqlCon2 As ConnectionStringSettings = WebConfigurationManager.ConnectionStrings("IBLPCConnectionString")
        'Dim sqlCon As New SqlConnection(sqlCon2.ConnectionString)
        Dim sqlCon As New SqlConnection(TextConection.Text)
        Dim sqlCmd As New SqlCommand
        Dim strCom As String

        strCom = TextBoxResultado.Text
        sqlCmd.Connection = sqlCon
        sqlCon.Open()
        sqlCmd.CommandText = "EXEC ('" & Replace(strCom, "'", "''") & "')"
        sqlCmd.CommandType = CommandType.Text

        sqlCmd.ExecuteNonQuery()
        sqlCon.Close()
        TextBoxResultado.Text = ""
    End Sub
    Protected Sub generarExcelReembolsos32(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTest2.Click
        'Dim sqlCon2 As ConnectionStringSettings = WebConfigurationManager.ConnectionStrings("IBLPCConnectionString")
        'Dim sqlCon As New SqlConnection(sqlCon2.ConnectionString)
        Dim sqlCon As New SqlConnection(TextConection.Text)
        Dim sqlCmd As New SqlCommand
        Dim strCom As String

        strCom = TextBoxResultado.Text
        sqlCmd.Connection = sqlCon
        sqlCon.Open()
        sqlCmd.CommandText = strCom
        sqlCmd.CommandType = CommandType.Text

        sqlCmd.ExecuteNonQuery()
        sqlCon.Close()
        TextBoxResultado.Text = ""
    End Sub
    Protected Sub generarExcelDUAS32(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTest3.Click
        Dim sqlCon As New SqlConnection(TextConection.Text)
        Dim sqlCmd As New SqlCommand
        Dim strCom As String
        Dim sqlAdp As New SqlDataAdapter
        Dim ds As New DataSet 'data set con el resultado
        Dim table As DataTable


        strCom = TextBoxResultado.Text
        sqlCmd.Connection = sqlCon
        sqlCmd.CommandText = strCom
        sqlCmd.CommandType = CommandType.Text
        sqlAdp.SelectCommand = sqlCmd
        sqlAdp.Fill(ds)
        table = ds.Tables(0)

        gdvVista.DataSource = table
        gdvVista.Visible = True
        gdvVista.DataBind()

        TextBoxResultado.Text = ""
    End Sub
    Public Function generarExcelReembolsos31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0

        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(1)
                nbrJ = nbrJ + 1
            Next row
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""Number"">3</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Reembolsos al cliente</Data></Cell>                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s116""><Data ss:Type=""String"">Número de cuenta del cliente</Data></Cell>                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s117""><Data ss:Type=""String"">Importe</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            ' No hay filas
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s121""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Reembolso al cliente, en moneda del crédito.</Data></Cell>                                                                                               " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(1)
                    sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s121""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
                    If nbrJ = 0 Then
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Reembolso al cliente, en moneda del crédito.</Data></Cell>                                                                                               " & vbCrLf)
                    Else
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""/>                                                                                               " & vbCrLf)
                    End If
                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
                    nbrJ = nbrJ + 1
                Next row
            End If

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelDUAS31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0
        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(1)
                nbrJ = nbrJ + 1
            Next row
            sbCabecera.Append("      <Row ss:AutoFitHeight=""0"" ss:Height=""13.5"">                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""Number"">4</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Pago de DUAS</Data></Cell>                                                                                                                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s116""><Data ss:Type=""String"">Número de DUA</Data></Cell>                                                                                                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s117""><Data ss:Type=""String"">Importe</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            ' No hay filas
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Monto de la DUA en moneda Nacional</Data></Cell>                                                                                                         " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(1)
                    sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
                    If nbrJ = 0 Then
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Monto de la DUA en moneda Nacional</Data></Cell>                                                                                                         " & vbCrLf)
                    Else
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""/>                                                                                                         " & vbCrLf)
                    End If

                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)

                    nbrJ = nbrJ + 1
                Next row
            End If



            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelPagos31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0

        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(1)
                nbrJ = nbrJ + 1
            Next row
            sbCabecera.Append("      <Row ss:Height=""13.5"">                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""Number"">5</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Pagos Varios</Data></Cell>                                                                                                                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            ' No hay filas
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s148""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s149""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Otro tipo de pagos como transferencias</Data></Cell>                                                                                                     " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s152""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s153""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(1)
                    sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s148""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s149""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                                                                " & vbCrLf)
                    If nbrJ = 0 Then
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String"">Otro tipo de pagos como transferencias</Data></Cell>                                                                                                     " & vbCrLf)
                    Else
                        sbCabecera.Append("       <Cell ss:StyleID=""s155""><Data ss:Type=""String""></Data></Cell>                                                                                                     " & vbCrLf)
                    End If
                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
                    nbrJ = nbrJ + 1
                Next
                sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s152""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s153""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            End If

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelPrecuota31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0
        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(1)
                nbrJ = nbrJ + 1
            Next row
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s122""><Data ss:Type=""String"">CARGOS</Data></Cell>                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s127""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Height=""13.5"">                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""Number"">1</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Cargo de Precuotas Pendientes</Data></Cell>                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Height=""13.5"">                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s116""><Data ss:Type=""String"">Número de cuenta a cargar</Data></Cell>                                                                                                   " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s117""><Data ss:Type=""String"">Importe</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            ' No hay filas
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                              " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s115""/>                                                                                                                                                                  " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""/>                                                                                                                                                                                 " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(1)
                    sbCabecera.Append("      <Row>                                                                                                                                              " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s115""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
                    nbrJ = nbrJ + 1
                Next row
            End If



            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelAdelantos31(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrTotal As Decimal = 0
        Dim nbrMonto As Decimal = 0
        Dim nbrJ As Integer
        Dim nbrCantRows As Integer = 0
        Try
            'obtengo la cantidad de filas
            nbrCantRows = tabla.Rows.Count + 1
            nbrJ = 0
            For Each row In tabla.Rows
                nbrTotal = nbrTotal + tabla.Rows(nbrJ)(1)
                nbrJ = nbrJ + 1
            Next row
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s123""><Data ss:Type=""Number"">2</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Extorno de Adelanto de Cuotas Iniciales </Data></Cell>                                                                                                   " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + nbrTotal.ToString() + "</Data></Cell>                                                                                                            " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s116""><Data ss:Type=""String"">Adelanto Cuota Inicial Inc. IGV</Data></Cell>                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s117""><Data ss:Type=""String"">Importe</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            ' No hay filas
            If tabla.Rows.Count = 0 Then
                sbCabecera.Append("      <Row>                                                                                                                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""><Data ss:Type=""String""></Data></Cell>                                                                                             " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number""></Data></Cell>                                                                                                                                       " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            Else
                nbrJ = 0
                For Each row In tabla.Rows
                    nbrMonto = tabla.Rows(nbrJ)(1)
                    sbCabecera.Append("      <Row>                                                                                                                                                                                        " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s147""><Data ss:Type=""String"">" + tabla.Rows(nbrJ)(0) + "</Data></Cell>                                                                                             " & vbCrLf)
                    sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + nbrMonto.ToString() + "</Data></Cell>                                                                                                                                       " & vbCrLf)
                    sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
                    nbrJ = nbrJ + 1
                Next row
            End If


            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelCargosFinales31(ByVal tabla As DataTable, ByVal tabla1 As DataTable, ByVal tabla2 As DataTable, ByVal tabla3 As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s123""><Data ss:Type=""Number"">3</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Cargo por comisión de transferencia al exterior</Data></Cell>                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + tabla.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                               " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s115""><Data ss:Type=""String"">" + tabla.Rows(0)(0) + "</Data></Cell>                                                                                       " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + tabla.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s123""><Data ss:Type=""Number"">4</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Cargo de comision de activación</Data></Cell>                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + tabla1.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s121""><Data ss:Type=""String"">" + tabla1.Rows(0)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + tabla1.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s123""><Data ss:Type=""Number"">5</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Cargo de comision de estructuración</Data></Cell>                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + tabla2.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s115""><Data ss:Type=""String"">" + tabla2.Rows(0)(0) + "</Data></Cell>                                                                                       " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + tabla2.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s123""><Data ss:Type=""Number"">6</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s120""><Data ss:Type=""String"">Cargo de Cuota inicial (si la hubiese)</Data></Cell>                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s125""><Data ss:Type=""Number"">" + tabla3.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s121""><Data ss:Type=""String"">" + tabla3.Rows(0)(0) + "</Data></Cell>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s124""><Data ss:Type=""Number"">" + tabla3.Rows(0)(1).ToString() + "</Data></Cell>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s104""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s100""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s104""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s100""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""3"" ss:StyleID=""s100""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s106""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s103""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Index=""48"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2"" ss:StyleID=""s102""/>                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function generarExcelFin31() As String
        Dim sbCabecera As New StringBuilder("")
        Try
            sbCabecera.Append("     </Table>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <PageSetup>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Layout x:Orientation=""Landscape""/>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("       <Header x:Margin=""0""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Footer x:Margin=""0""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <PageMargins x:Bottom=""0.98425196850393704"" x:Left=""0.78740157480314965""                                                                                                                                " & vbCrLf)
            sbCabecera.Append("        x:Right=""0.78740157480314965"" x:Top=""0.98425196850393704""/>                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      </PageSetup>                                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <NoSummaryRowsBelowDetail/>                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      <Print>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <ValidPrinterInfo/>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <PaperSizeIndex>9</PaperSizeIndex>                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Scale>50</Scale>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <HorizontalResolution>600</HorizontalResolution>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <VerticalResolution>600</VerticalResolution>                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Gridlines/>                                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("      </Print>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <Selected/>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      <FreezePanes/>                                                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <FrozenNoSplit/>                                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <SplitHorizontal>5</SplitHorizontal>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <TopRowBottomPane>5</TopRowBottomPane>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <ActivePane>2</ActivePane>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Panes>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Pane>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("        <Number>3</Number>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       </Pane>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Pane>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("        <Number>2</Number>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <ActiveRow>22</ActiveRow>                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("        <ActiveCol>3</ActiveCol>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("       </Pane>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      </Panes>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <ProtectObjects>False</ProtectObjects>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <ProtectScenarios>False</ProtectScenarios>                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <AllowFormatCells/>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <AllowSizeCols/>                                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <AllowSizeRows/>                                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <AllowInsertCols/>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <AllowInsertRows/>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <AllowInsertHyperlinks/>                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <AllowDeleteCols/>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <AllowDeleteRows/>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <AllowSort/>                                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <AllowFilter/>                                                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <AllowUsePivotTables/>                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("     </WorksheetOptions>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("     <ConditionalFormatting xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Range>R3C4</Range>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Condition>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Value1>IF(R3C4&lt;0,1,0)</Value1>                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Format Style='color:white;font-weight:700;border:.5pt solid white;                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        background:red'/>                                                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Condition>                                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("     </ConditionalFormatting>                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <ConditionalFormatting xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Range>R35C3</Range>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Condition>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Value1>IF(AND(+#REF!=&quot;Cargo en cuenta&quot;,R35C3=&quot;&quot;),1,0)</Value1>                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Format Style='background:yellow'/>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Condition>                                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("     </ConditionalFormatting>                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("    </Worksheet>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("    <Worksheet ss:Name=""Hoja3"">                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("     <Table ss:ExpandedColumnCount=""7"" ss:ExpandedRowCount=""11"" x:FullColumns=""1""                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      x:FullRows=""1"" ss:DefaultColumnWidth=""60"">                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""93""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""83.25""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""117.75""/>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("      <Column ss:Index=""6"" ss:AutoFitWidth=""0"" ss:Width=""129""/>                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""268.5""/>                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Tipo de desembolso</Data></Cell>                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Pago de comisión</Data></Cell>                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Banca</Data></Cell>                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Moneda</Data></Cell>                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Pregunta</Data></Cell>                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Ejecutivo Leasing</Data></Cell>                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Detracciones</Data></Cell>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Desembolso Parcial</Data><NamedCell                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Desembolso""/></Cell>                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Financiada</Data><NamedCell ss:Name=""PagoCom""/></Cell>                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Banca Pequeña Empresa</Data><NamedCell                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Banca""/></Cell>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Soles</Data><NamedCell                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Moneda""/></Cell>                                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Si</Data><NamedCell                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Pregunta""/></Cell>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">José García</Data><NamedCell                                                                                                                              " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Contratos de Construcción</Data><NamedCell                                                                                                                " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Desembolso Total</Data><NamedCell                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Desembolso""/></Cell>                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Cargo en cuenta</Data><NamedCell ss:Name=""PagoCom""/></Cell>                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Banca Empresa Lima</Data><NamedCell                                                                                                                       " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Banca""/></Cell>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Dólares</Data><NamedCell                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Moneda""/></Cell>                                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">No</Data><NamedCell                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Pregunta""/></Cell>                                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Diego Soriano</Data><NamedCell                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Arrendamiento de Bienes</Data><NamedCell                                                                                                                  " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""3""><Data ss:Type=""String"">Banca Corporativa</Data><NamedCell                                                                                                                            " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Banca""/></Cell>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s99""><Data ss:Type=""String"">Cesar Cucirramo</Data><NamedCell                                                                                                           " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Comisión Mercantil</Data><NamedCell                                                                                                                       " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""3""><Data ss:Type=""String"">Banca Institucional</Data><NamedCell                                                                                                                          " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Banca""/></Cell>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s99""><Data ss:Type=""String"">Jorge Saldaña</Data><NamedCell                                                                                                             " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Fabricación de Bienes por Encargo</Data><NamedCell                                                                                                        " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""3"" ss:StyleID=""s99""><Data ss:Type=""String"">Banca Empresa Provincia</Data><NamedCell                                                                                                   " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Banca""/></Cell>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s99""><Data ss:Type=""String"">Luis Suito</Data><NamedCell                                                                                                                " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Intermediación Laboral y tercerización</Data><NamedCell                                                                                                   " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""3""><Data ss:Type=""String"">Negocios Inmobiliarios</Data><NamedCell                                                                                                                       " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Banca""/></Cell>                                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s99""><Data ss:Type=""String"">Luis Rivera</Data><NamedCell                                                                                                               " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Mantenimiento y reparación de Bienes Muebles</Data><NamedCell                                                                                             " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s99""><Data ss:Type=""String"">Carlos Barriga</Data><NamedCell                                                                                                            " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Movimiento de Carga</Data><NamedCell                                                                                                                      " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""6"" ss:StyleID=""s99""><Data ss:Type=""String"">Paola Campos</Data><NamedCell                                                                                                              " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Ejecutivo_Leasing""/></Cell>                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s99""><Data ss:Type=""String"">Otros servicios empresariales</Data><NamedCell                                                                                                            " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""7"" ss:StyleID=""s99""><Data ss:Type=""String"">Servicio de Transporte de bienes realizados por vía terrestre</Data><NamedCell                                                             " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""7"" ss:StyleID=""s99""><Data ss:Type=""String"">Servicio de Transporte de personas</Data><NamedCell                                                                                        " & vbCrLf)
            sbCabecera.Append("         ss:Name=""Detracciones""/></Cell>                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("     </Table>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <PageSetup>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Header x:Margin=""0""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Footer x:Margin=""0""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </PageSetup>                                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Panes>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Pane>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("        <Number>3</Number>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <ActiveRow>1</ActiveRow>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        <ActiveCol>6</ActiveCol>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        <RangeSelection>R2C7:R11C7</RangeSelection>                                                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       </Pane>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      </Panes>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <ProtectObjects>False</ProtectObjects>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <ProtectScenarios>False</ProtectScenarios>                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("     </WorksheetOptions>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("    </Worksheet>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("    <Worksheet ss:Name=""Hoja2"">                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("     <Table ss:ExpandedColumnCount=""7"" ss:ExpandedRowCount=""27"" x:FullColumns=""1""                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      x:FullRows=""1"" ss:DefaultColumnWidth=""60"">                                                                                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""135""/>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s96""><Data ss:Type=""String"">Instrucciones - Desembolsos</Data></Cell>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""2""><Data ss:Type=""Number"">90</Data></Cell>                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">LS S/</Data></Cell>                                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">FACT $</Data></Cell>                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">30</Data></Cell>                                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">1.5</Data></Cell>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">5.25</Data></Cell>                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s97""><Data ss:Type=""String"">Acciones</Data></Cell>                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""3""><Data ss:Type=""String"">TC VENTA</Data></Cell>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">3</Data></Cell>                                                                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">3.5</Data></Cell>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">4.5</Data></Cell>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""Number"">0.75</Data></Cell>                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""5"" ss:StyleID=""s98""><Data ss:Type=""Number"">0.05</Data></Cell>                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell ss:Index=""7""><Data ss:Type=""Number"">0.25</Data></Cell>                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s96""><Data ss:Type=""String"">Desembolsos Regulares</Data></Cell>                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Index=""7"">                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Emitir Cheques de Proveedores</Data></Cell>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar Nombre del Proveedor</Data></Cell>                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Abonos en cuenta de Proveedores</Data></Cell>                                                                                                                                " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar el número de cuenta del proveedor o que el sistema lo jale del C.U</Data></Cell>                                                                                     " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Pago de detracción</Data></Cell>                                                                                                                                             " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Cuenta del Proveedor del Banco de la Nación mas cuadro de detracciones</Data></Cell>                                                                                         " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Reembolsos al cliente</Data></Cell>                                                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Cuenta del cliente mas carta de reembolso</Data></Cell>                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Transferencias al Exterior</Data></Cell>                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar cuenta transitoria de Transferencias mas datos de la misma (29080700000506)/campo indicando si el cliente paga la comisión o se financia</Data></Cell>               " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Pago de DUAS</Data></Cell>                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar Número de Dua mas diferencia cambiaria</Data></Cell>                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Pago de Cartas de Crédito</Data></Cell>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar cuenta transitoria de cartas de credito mas datos de la misma (29080700000512)/campo indicando si el cliente paga la comisión o se financia</Data></Cell>            " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Transferencias Interbancarias</Data></Cell>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar Cédito Interbancario (C.I) e instruir mediante nota transitoria a Transferencias BCR</Data></Cell>                                                                   " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Mesa de Dinero</Data></Cell>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Mesa de dinero pone a Disposición notas transitorias a desembolsos amarrados con instrumentos derivados (Forwards, swaps) $ 29080700000508 / S/. 19080700000908</Data></Cell>" & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Index=""17"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s96""><Data ss:Type=""String"">Desembolsos de Activación</Data></Cell>                                                                                                                   " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Index=""19"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Cargo de Precuotas Pendientes</Data></Cell>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Colocar el nÃºmero de cuenta del cliente en el cual se van a cargar las precuotas</Data></Cell>                                                                               " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Extorno de Adelanto de Cuotas Iniciales </Data></Cell>                                                                                                                       " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Indicar las notas créditos a extornar</Data></Cell>                                                                                                                          " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Cargo de comision de activación y/o de estructuración</Data></Cell>                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Indicar cuenta de cliente a cargar</Data></Cell>                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Cargo de Cuota inicial (si la hubiese)</Data></Cell>                                                                                                                         " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Indicar cuenta de cliente a cargar</Data></Cell>                                                                                                                             " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row ss:Index=""24"">                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s96""><Data ss:Type=""String"">Extras</Data></Cell>                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">T.C</Data></Cell>                                                                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Campo de tipo de cambio</Data></Cell>                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">T.C Sunat</Data></Cell>                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Campo de tipo de cambio Sunat</Data></Cell>                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                                                                                                                        " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Diferencia T.C</Data></Cell>                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("       <Cell><Data ss:Type=""String"">Campo Indicando la diferencia cambiaria por DUAS y DETRACCIONES</Data></Cell>                                                                                                " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("     </Table>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("     <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <PageSetup>                                                                                                                                                                                                  " & vbCrLf)
            sbCabecera.Append("       <Header x:Margin=""0""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("       <Footer x:Margin=""0""/>                                                                                                                                                                                    " & vbCrLf)
            sbCabecera.Append("      </PageSetup>                                                                                                                                                                                                 " & vbCrLf)
            sbCabecera.Append("      <Panes>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("       <Pane>                                                                                                                                                                                                      " & vbCrLf)
            sbCabecera.Append("        <Number>3</Number>                                                                                                                                                                                         " & vbCrLf)
            sbCabecera.Append("        <ActiveRow>3</ActiveRow>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("        <ActiveCol>3</ActiveCol>                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("       </Pane>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      </Panes>                                                                                                                                                                                                     " & vbCrLf)
            sbCabecera.Append("      <ProtectObjects>False</ProtectObjects>                                                                                                                                                                       " & vbCrLf)
            sbCabecera.Append("      <ProtectScenarios>False</ProtectScenarios>                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("     </WorksheetOptions>                                                                                                                                                                                           " & vbCrLf)
            sbCabecera.Append("    </Worksheet>                                                                                                                                                                                                   " & vbCrLf)
            sbCabecera.Append("   </Workbook>                                                                                                                                                                                                     " & vbCrLf)


            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    
    
    
    
End Class
