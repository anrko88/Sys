Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data
Imports System.Web.Services

Partial Class Verificacion_frmPipelineListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPipelineListado.aspx.vb")

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                'Inicio IBK - AAE
                GCCUtilitario.CargarComboValorGenerico(cmbBanca, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
                GCCUtilitario.CargarComboValorGenerico(cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_PIPELINE)
                'Fin IBK - AAE
               
            End If
        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub
    'Inicio IBK - AAE Agrego parámetros de búsqueda
    <WebMethod()> _
    Public Shared Function ListarPipeline(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pCUCliente As String, _
                                                     ByVal pRazonSocialCli As String, _
                                                     ByVal pCodEjecutivo As String, _
                                                     ByVal pCodBanca As String, _
                                                     ByVal pCodEstado As String) As JQGridJsonResponse


        Dim oLwsPipelineNTX As New LPipelineNTX
        Dim oEPipeline As New EGCC_Pipeline
        Dim odtbListado As New DataTable
        Try
            Dim strCodUnico As String = GCCUtilitario.NullableString(pCUCliente)
            If Not strCodUnico Is Nothing Then
                strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
            End If

            With oEPipeline
                .CodUnico = strCodUnico
                .RazonSocial = pRazonSocialCli
                .EjecutivoLeasing = GCCUtilitario.NullableStringCombo(pCodEjecutivo)
                'Inicio IBK - Agrego los parámetros
                .CodigoEstado = GCCUtilitario.NullableStringCombo(pCodEstado)
                .CodBanca = GCCUtilitario.NullableStringCombo(pCodBanca)
                'Fin IBK

            End With



            odtbListado = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsPipelineNTX.ListarPipeline(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           GCCUtilitario.SerializeObject(Of EGCC_Pipeline)(oEPipeline) _
                                                                           ))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            'Dim total2 As Decimal
            If odtbListado.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(odtbListado.Rows(0)("RecordCount"))
                'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
                intTotalCurrent = Convert.ToInt32(odtbListado.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            ' Return JQGridJsonResponse.JQGridJsonResponseClass(totalPages, currentPage, totalRecords, temporals)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, odtbListado)

        Catch ex As Exception
            Throw ex
        Finally
            odtbListado.Dispose()
            oLwsPipelineNTX = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <param name="strCodigoCotizacion"></param>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Sub EliminarPipeline(ByVal strCodigoCotizacion As String)
        Dim oLwsPipelineTX As New LPipelineTX
        Try
            Dim blnResult As Boolean = oLwsPipelineTX.EliminarPipeline(strCodigoCotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            oLwsPipelineTX = Nothing
        End Try
    End Sub

    'Inicio IBK - AAE - Nuevo método para llanzar el reporte
    
    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        'AAE - Agrego variables 
        Dim strCotizaciones As String = HddSelected.Value
        Dim pCUCliente As String = txtCuCliente.Value
        Dim pRazonSocialCli As String = txtRazonsocial.Value
        Dim pCodEjecutivo As String = cmbEjecutivo.Value
        Dim pCodBanca As String = cmbBanca.Value
        Dim pCodEstado As String = cmbEstado.Value

        Dim objUtilNTx As New LUtilNTX

        Dim sbResultado As New StringBuilder("")
        Dim strResultado As String
        'Tablas
        Dim table As DataTable 'variable para recorrer las tablas

        Dim strCot As String()
        Dim strListCodCotiza As String = ""


        Try
            If strCotizaciones = "" Then
                strListCodCotiza = ""
            Else
                strCot = strCotizaciones.Split("|")
                For i As Integer = 0 To strCot.Length - 1
                    strListCodCotiza = strListCodCotiza + "'" + strCot(i).Trim + "', "
                Next
                strListCodCotiza = strListCodCotiza + "''"
            End If
            
            If pCodEjecutivo = "0" Then
                pCodEjecutivo = ""
            End If
            If pCodBanca = "0" Then
                pCodBanca = ""
            End If
            If pCodEstado = "0" Then
                pCodEstado = ""
            End If
            table = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarPipeline2(strListCodCotiza, pCUCliente, pRazonSocialCli, pCodEjecutivo, pCodBanca, pCodEstado))
            sbResultado.Append(generarExcel(table))
            strResultado = sbResultado.ToString

            If Not String.IsNullOrEmpty(strResultado) Then
                Session("stream") = strResultado
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "FrameScript", "window.open('Streamdownload.aspx?archivo=Reporte_Pipeline.xls&aplicacion=application/excel');", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function generarExcel(ByVal tabla As DataTable) As String
        Dim sbCabecera As New StringBuilder("")
        Dim row As DataRow
        Dim nbrJ As Integer
        Dim nbrPorc As Decimal
        Dim FechaAprox As String
        Dim FechaAprob As String
        Try
            nbrJ = 0
            'Header
            sbCabecera.Append("   <?xml version=""1.0""?>                                                                                               " & vbCrLf)
            sbCabecera.Append("   <?mso-application progid=""Excel.Sheet""?>                                                                            " & vbCrLf)
            sbCabecera.Append("   <Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""                                                      " & vbCrLf)
            sbCabecera.Append("    xmlns:o=""urn:schemas-microsoft-com:office:office""                                                                  " & vbCrLf)
            sbCabecera.Append("    xmlns:x=""urn:schemas-microsoft-com:office:excel""                                                                   " & vbCrLf)
            sbCabecera.Append("    xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""                                                            " & vbCrLf)
            sbCabecera.Append("    xmlns:html=""http://www.w3.org/TR/REC-html40"">                                                                      " & vbCrLf)
            sbCabecera.Append("    <DocumentProperties xmlns=""urn:schemas-microsoft-com:office:office"">                                               " & vbCrLf)
            sbCabecera.Append("     <LastAuthor>LOGICA.Americo Estefanell, Andres</LastAuthor>                                                          " & vbCrLf)
            sbCabecera.Append("     <Created>2012-12-13T18:35:11Z</Created>                                                                             " & vbCrLf)
            sbCabecera.Append("     <Version>14.00</Version>                                                                                            " & vbCrLf)
            sbCabecera.Append("    </DocumentProperties>                                                                                                " & vbCrLf)
            sbCabecera.Append("    <OfficeDocumentSettings xmlns=""urn:schemas-microsoft-com:office:office"">                                           " & vbCrLf)
            sbCabecera.Append("     <AllowPNG/>                                                                                                         " & vbCrLf)
            sbCabecera.Append("    </OfficeDocumentSettings>                                                                                            " & vbCrLf)
            sbCabecera.Append("    <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">                                                     " & vbCrLf)
            sbCabecera.Append("     <WindowHeight>6615</WindowHeight>                                                                                   " & vbCrLf)
            sbCabecera.Append("     <WindowWidth>18900</WindowWidth>                                                                                    " & vbCrLf)
            sbCabecera.Append("     <WindowTopX>240</WindowTopX>                                                                                        " & vbCrLf)
            sbCabecera.Append("     <WindowTopY>15</WindowTopY>                                                                                         " & vbCrLf)
            sbCabecera.Append("     <ProtectStructure>False</ProtectStructure>                                                                          " & vbCrLf)
            sbCabecera.Append("     <ProtectWindows>False</ProtectWindows>                                                                              " & vbCrLf)
            sbCabecera.Append("    </ExcelWorkbook>                                                                                                     " & vbCrLf)
            sbCabecera.Append("    <Styles>                                                                                                             " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""Default"" ss:Name=""Normal"">                                                                        " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Vertical=""Bottom""/>                                                                                " & vbCrLf)
            sbCabecera.Append("      <Borders/>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>                             " & vbCrLf)
            sbCabecera.Append("      <Interior/>                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <NumberFormat/>                                                                                                    " & vbCrLf)
            sbCabecera.Append("      <Protection/>                                                                                                      " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s68"">                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom"" ss:WrapText=""1""/>                                     " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Tahoma"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#000000""                                " & vbCrLf)
            sbCabecera.Append("       ss:Bold=""1""/>                                                                                                   " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s69"">                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom"" ss:WrapText=""1""/>                                     " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Tahoma"" x:Family=""Swiss"" ss:Color=""#000000""/>                                             " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s74"">                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>                                                              " & vbCrLf)
            sbCabecera.Append("      <Borders/>                                                                                                         " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s81"">                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>                                     " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                        " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#969696""/>                                                                                           " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                          " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#969696""/>                                                                                           " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                         " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#969696""/>                                                                                           " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""                                           " & vbCrLf)
            sbCabecera.Append("        ss:Color=""#969696""/>                                                                                           " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""8"" ss:Color=""#FFFFFF""                                  " & vbCrLf)
            sbCabecera.Append("       ss:Bold=""1""/>                                                                                                   " & vbCrLf)
            sbCabecera.Append("      <Interior ss:Color=""#003366"" ss:Pattern=""Solid""/>                                                              " & vbCrLf)
            sbCabecera.Append("      <Protection ss:Protected=""0""/>                                                                                   " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s83"">                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                      " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                        " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                       " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""8"" ss:Color=""#000000""/>                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <Style ss:ID=""s84"">                                                                                               " & vbCrLf)
            sbCabecera.Append("      <Borders>                                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                      " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                        " & vbCrLf)
            sbCabecera.Append("       <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>                                       " & vbCrLf)
            sbCabecera.Append("      </Borders>                                                                                                         " & vbCrLf)
            sbCabecera.Append("      <Font ss:FontName=""Arial"" x:Family=""Swiss"" ss:Size=""8"" ss:Color=""#000000""/>                                " & vbCrLf)
            sbCabecera.Append("      <NumberFormat ss:Format=""Standard""/>                                " & vbCrLf)
            sbCabecera.Append("     </Style>                                                                                                            " & vbCrLf)
            sbCabecera.Append("    </Styles>                                                                                                            " & vbCrLf)
            sbCabecera.Append("    <Worksheet ss:Name=""rptPipeline 1 "">                                                                               " & vbCrLf)
            sbCabecera.Append("     <Table>                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""48""/>                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""58.5""/>                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""78.75""/>                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""63""/>                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""30""/>                                                                    " & vbCrLf)
            sbCabecera.Append("      <Column ss:Index=""7"" ss:Width=""54""/>                                                                           " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""35.25""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""36.75""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""59.25""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""70.5""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""47.25""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""63.75""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""67.5""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""42""/>                                                                    " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""69""/>                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""44.25""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""105.75""/>                                                                " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""57.75""/>                                                                 " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""55.5""/>                                                                                        " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""78.75""/>                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""185.25""/>                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""45.75""/>                                                                                       " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""55.5""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""108.75""/>                                                                                      " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""58.5""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      <Column ss:Width=""78""/>                                                                                          " & vbCrLf)
            sbCabecera.Append("      <Column ss:AutoFitWidth=""0"" ss:Width=""61.5""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      <Row ss:Height=""15.75"">                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:MergeAcross=""27"" ss:StyleID=""s68""><Data ss:Type=""String"">REPORTE PIPELINE</Data></Cell>            " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell ss:MergeAcross=""27"" ss:StyleID=""s69""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Row>                                                                                                              " & vbCrLf)
            sbCabecera.Append("       <Cell ss:MergeAcross=""27"" ss:StyleID=""s74""/>                                                                  " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                             " & vbCrLf)
            sbCabecera.Append("      <Row ss:Height=""33.75"">                                                                                          " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Estado</Data></Cell>                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Tipo Documento</Data></Cell>                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Numero de Documento</Data></Cell>                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Nombre del cliente</Data></Cell>                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Banca</Data></Cell>                                             " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Ejecutivo Leasing</Data></Cell>                                 " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Tipo de Leasing</Data></Cell>                                   " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Activo</Data></Cell>                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Seguro</Data></Cell>                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Precio Venta</Data></Cell>                                      " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Monto Desmebolsado</Data></Cell>                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">% Desembolsado</Data></Cell>                                    " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Valor Venta</Data></Cell>                                       " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Riesgo Neto</Data></Cell>                                       " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Moneda</Data></Cell>                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Fecha de Cotización</Data></Cell>                               " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Spread%</Data></Cell>                                           " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Proveedor</Data></Cell>                                         " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Opción de Compra</Data></Cell>                                  " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Cuota Inicial</Data></Cell>                                     " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Com. De Activación</Data></Cell>                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Fecha Aprobación Cotización - Cliente</Data></Cell>             " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">CU Cliente</Data></Cell>                                        " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Estado</Data></Cell>                                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Fecha Aprox Desembolso</Data></Cell>                            " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">% Prox. Desembolso</Data></Cell>                                " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">Motivo De Demora</Data></Cell>                                  " & vbCrLf)
            sbCabecera.Append("       <Cell ss:StyleID=""s81""><Data ss:Type=""String"">No. Leasing</Data></Cell>                                       " & vbCrLf)
            sbCabecera.Append("      </Row>                                                                                                             " & vbCrLf)
            For Each row In tabla.Rows
                'Lineas
                sbCabecera.Append("      <Row ss:AutoFitHeight=""0"" ss:Height=""12.75"">                                                                   " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("EstadoRegistro").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("TipoDocumento").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("NumeroDocumento").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("NombreCliente").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("Banca").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("EjecutivoLeasing").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("TipoLeasing").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("ClasificacionBien").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("TipoSeguro").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("PrecioVenta").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("montoDesembolsado").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                nbrPorc = (GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("montoDesembolsado")) / GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("PrecioVenta"))) * 100
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + nbrPorc.ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("ValorVenta").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("RiesgoNeto").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("Moneda").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("FechaCarta").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + tabla.Rows(nbrJ).Item("Spread").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("DesProveedor").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("OpcionCompra").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("CuotaInicial").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("ComisionActivacion").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                FechaAprob = tabla.Rows(nbrJ).Item("FechaAprobacionCotizacion").ToString.Trim
                If FechaAprob = "01/01/1900" Then
                    FechaAprob = tabla.Rows(nbrJ).Item("FechaRegistroContrato").ToString.Trim
                End If
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + FechaAprob + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("CodigoUnico").ToString.Trim + "</Data></Cell>                                                                                       " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("Estado").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                Select Case GCCUtilitario.CheckStr(tabla.Rows(nbrJ).Item("FechaAproximada").ToString)
                    Case 1 : FechaAprox = MonthName(Now.Date.Month, False)
                    Case 2 : FechaAprox = MonthName(Now.Date.Month + 1, False)
                    Case 3 : FechaAprox = (Now.Year + 1).ToString()
                    Case "" : FechaAprox = ""
                End Select
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + FechaAprox + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s84""><Data ss:Type=""Number"">" + GCCUtilitario.CheckDecimal(tabla.Rows(nbrJ).Item("PorcProximoDesembolso").ToString.Trim).ToString(GCCConstante.C_FormatMiles) + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("Motivo").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("       <Cell ss:StyleID=""s83""><Data ss:Type=""String"">" + tabla.Rows(nbrJ).Item("CodigoContrato").ToString.Trim + "</Data></Cell>                                                                                        " & vbCrLf)
                sbCabecera.Append("      </Row>                                                                                                             " & vbCrLf)
                nbrJ = nbrJ + 1
            Next
            'Final
            sbCabecera.Append("     </Table>                                                                                                            " & vbCrLf)
            sbCabecera.Append("     <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">                                                 " & vbCrLf)
            sbCabecera.Append("      <Selected/>                                                                                                        " & vbCrLf)
            sbCabecera.Append("      <DoNotDisplayGridlines/>                                                                                           " & vbCrLf)
            sbCabecera.Append("      <Panes>                                                                                                            " & vbCrLf)
            sbCabecera.Append("       <Pane>                                                                                                            " & vbCrLf)
            sbCabecera.Append("        <Number>3</Number>                                                                                               " & vbCrLf)
            sbCabecera.Append("        <ActiveRow>4</ActiveRow>                                                                                         " & vbCrLf)
            sbCabecera.Append("       </Pane>                                                                                                           " & vbCrLf)
            sbCabecera.Append("      </Panes>                                                                                                           " & vbCrLf)
            sbCabecera.Append("      <ProtectObjects>False</ProtectObjects>                                                                             " & vbCrLf)
            sbCabecera.Append("      <ProtectScenarios>False</ProtectScenarios>                                                                         " & vbCrLf)
            sbCabecera.Append("     </WorksheetOptions>                                                                                                 " & vbCrLf)
            sbCabecera.Append("    </Worksheet>                                                                                                         " & vbCrLf)
            sbCabecera.Append("   </Workbook>                                                                                                           " & vbCrLf)
            sbCabecera.Append("                                                                                                                         " & vbCrLf)

            Return (sbCabecera.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
