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


Partial Class InsDesembolso_frmInsDesembolsoRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmInsDesembolsoRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Dim strNroContrato As String = Request.QueryString("hddCodigoContrato")
                Dim strCodInsDesembolso As String = Request.QueryString("hddCodigoInsDesembolso")
                Dim strEstadoID As String = Request.QueryString("hddCodigoEstadoID")

                'Consulta EstadoWIO
                ConsultaEstadoWIO(strNroContrato, strCodInsDesembolso, strEstadoID)

                'Datos Generales
                ObtieneDatosContrato(strNroContrato, strCodInsDesembolso)
                ListaCargoAbonoIni(strNroContrato, strCodInsDesembolso)

                hddCodigoInsDesembolso.Value = strCodInsDesembolso

                'Inicio IBK - AAE - Activación Leasing Parcial

                'Limpia Cronograma
                HttpContext.Current.Session("DTB_CRONOGRAMA") = Nothing

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCronograma, GCCConstante.C_TABLAGENERICA_TIPO_CRONOGRAMA)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoGracia, GCCConstante.C_TABLAGENERICA_TIPO_GRACIA)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoBienSeguro, GCCConstante.C_TABLAGENERICA_TIPO_BIEN_SEGURO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoSeguro, GCCConstante.C_TABLAGENERICA_TIPO_SEGURO)

                ObtenerDatosActivacionParcial(strNroContrato, strCodInsDesembolso)
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

    'Inicio IBK - AAE - Activacio LEasing PArcial
    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        'Dim rutalocal As String
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
        Dim table1 As DataTable 'variable para recorrer las tablas
        Dim table2 As DataTable 'variable para recorrer las tablas
        Dim strFecIni As String = ""
        Dim strFecFin As String = ""
        Dim strMoneda As String = "001"
        Dim Flag As String = "N"

        'rutalocal = HttpContext.Current.Server.MapPath("../temp")
        'Dim archivo As String = rutalocal + "\ReporteVentas.xlsm"
        Dim strNroContrato As String = txtNroContrato.Value
        Dim strCodInstDesembolso As String = txtNroInstruccion.Value

        Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../temp/ListaDesembolsos.xlsx"))
        'Dim newFile As New FileInfo(HttpContext.Current.Server.MapPath("../../../../ListaDesembolsos.xls"))
        Dim template As New FileInfo(HttpContext.Current.Server.MapPath("../Util/Plantillas/Reportes/LSG_rptDesemPorNroCredito1.xlsx"))
        Try

            'table = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListaDesembolsos(5000, 1, "fechaDesembolso", "ASC", strNroContrato, strCodInstDesembolso))
            strMoneda = "002" 'dolares
            table1 = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ReporteLeasingEnProceso(strFecIni, strFecFin, strMoneda, strNroContrato, Flag))
            strMoneda = "001" 'soles
            table2 = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ReporteLeasingEnProceso(strFecIni, strFecFin, strMoneda, strNroContrato, Flag))

            If Not Directory.Exists(HttpContext.Current.Server.MapPath("../temp")) Then
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../temp"))
            End If
            If File.Exists(newFile.ToString()) Then
                File.Delete(newFile.ToString())
            End If

            Dim pck As New ExcelPackage(newFile, template)


            Dim worksheet As ExcelWorksheet = pck.Workbook.Worksheets("Contratos DOLARES")
            Dim worksheet2 As ExcelWorksheet = pck.Workbook.Worksheets("Contratos SOLES")
            Dim col As Integer = 1
            Dim row As Integer = 9

            Dim dTotalIGV As Double = 0
            Dim dTotalDesembolso As Double = 0
            Dim dTotalPrecuota As Double = 0
            Dim dTotalPrecuotaCargar As Double = 0
            Dim dTotalIGVDolares As Double = 0
            Dim dTotalIGVSoles As Double = 0
            Dim dTotalGenDesembolso As Double = 0
            Dim dTotalGenIGV As Double = 0

            Dim iFila As Integer = 9
            Dim dCont As Integer = 0
            Dim sOperacion As String
            Dim sOperacionAnt As String = ""
            Dim aux As Integer


            For Each rw As DataRow In table1.Rows

                sOperacion = rw.Item("CodOperacionActiva").ToString.Trim
                If dCont = 0 Then
                    sOperacionAnt = rw.Item("CodOperacionActiva").ToString.Trim
                    iFila = iFila + 1
                    worksheet.Cells(iFila, 12).Value = rw.Item("PorcenTasaActiva").ToString
                    worksheet.Cells(iFila, 19).Value = rw.Item("EjecutivoLeasing").ToString.Trim
                End If

                If sOperacion = sOperacionAnt Then

                    worksheet.Cells(iFila, 2).Value = rw.Item("CodUnico").ToString.Trim
                    worksheet.Cells(iFila, 3).Value = rw.Item("CodOperacionActiva").ToString.Trim
                    worksheet.Cells(iFila, 4).Value = rw.Item("NombreSubprestatario").ToString.Trim
                    worksheet.Cells(iFila, 5).Value = rw.Item("Segmento").ToString.Trim
                    worksheet.Cells(iFila, 6).Value = rw.Item("FechaDesembolso").ToString().Substring(0, 10)
                    worksheet.Cells(iFila, 7).Value = rw.Item("NombreInstitucion").ToString.Trim
                    worksheet.Cells(iFila, 8).Value = rw.Item("TipoBien").ToString.Trim
                    worksheet.Cells(iFila, 9).Value = rw.Item("MontoFinanciado")

                    'EL NUEVO VALOR Neto IGV
                    worksheet.Cells(iFila, 10).Value = rw.Item("MontoIGV")
                    worksheet.Cells(iFila, 11).Value = rw.Item("MontoDesembolso")
                    worksheet.Cells(iFila, 13).Value = rw.Item("DesembolsoDiario")

                    worksheet.Cells(iFila, 14).Value = rw.Item("SaldoDesembolso")
                    worksheet.Cells(iFila, 15).Value = rw.Item("TextoInstruccion1").ToString.Trim
                    worksheet.Cells(iFila, 16).Value = rw.Item("NroDocumento").ToString.Trim
                    worksheet.Cells(iFila, 17).Value = rw.Item("MonedaDocumento").ToString.Trim
                    worksheet.Cells(iFila, 18).Value = rw.Item("TipoCambio").ToString.Trim


                    dTotalDesembolso = dTotalDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))
                    dTotalIGV = dTotalIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                    'Totales por Columna
                    dTotalGenDesembolso = dTotalGenDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))
                    dTotalGenIGV = dTotalGenIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                Else 'else de Moneda
                    iFila = iFila + 1

                    'subtotales
                    worksheet.Cells(iFila, 6).Value = "SUB TOTAL"
                    worksheet.Cells(iFila, 10).Value = dTotalIGV
                    worksheet.Cells(iFila, 11).Value = dTotalDesembolso

                    aux = 2
                    While aux <= 16
                        worksheet.Cells(iFila, aux).Style.Font.Bold = True
                        aux = aux + 1
                    End While

                    dTotalIGV = 0
                    dTotalDesembolso = 0
                    dTotalPrecuota = 0
                    dTotalPrecuotaCargar = 0
                    dTotalIGVDolares = 0
                    dTotalIGVSoles = 0

                    iFila = iFila + 2

                    worksheet.Cells(iFila, 2).Value = rw.Item("CodUnico").ToString.Trim
                    worksheet.Cells(iFila, 3).Value = rw.Item("CodOperacionActiva").ToString.Trim
                    worksheet.Cells(iFila, 4).Value = rw.Item("NombreSubprestatario").ToString.Trim
                    worksheet.Cells(iFila, 5).Value = rw.Item("Segmento").ToString.Trim
                    worksheet.Cells(iFila, 6).Value = rw.Item("FechaDesembolso").ToString().Substring(0, 10)
                    worksheet.Cells(iFila, 7).Value = rw.Item("NombreInstitucion").ToString.Trim
                    worksheet.Cells(iFila, 8).Value = rw.Item("TipoBien").ToString.Trim
                    worksheet.Cells(iFila, 9).Value = rw.Item("MontoFinanciado")

                    'EL NUEVO VALOR Neto IGV
                    worksheet.Cells(iFila, 10).Value = rw.Item("MontoIGV")
                    worksheet.Cells(iFila, 11).Value = rw.Item("MontoDesembolso")
                    worksheet.Cells(iFila, 13).Value = rw.Item("DesembolsoDiario")

                    worksheet.Cells(iFila, 14).Value = rw.Item("SaldoDesembolso")
                    worksheet.Cells(iFila, 15).Value = rw.Item("TextoInstruccion1").ToString.Trim
                    worksheet.Cells(iFila, 16).Value = rw.Item("NroDocumento").ToString.Trim
                    worksheet.Cells(iFila, 17).Value = rw.Item("MonedaDocumento").ToString.Trim
                    worksheet.Cells(iFila, 18).Value = rw.Item("TipoCambio").ToString.Trim

                    worksheet.Cells(iFila, 12).Value = rw.Item("PorcenTasaActiva").ToString.Trim
                    worksheet.Cells(iFila, 19).Value = rw.Item("EjecutivoLeasing").ToString.Trim
                    dTotalIGV = dTotalIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                    dTotalDesembolso = dTotalDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))

                    'Totales por Columna
                    dTotalGenIGV = dTotalGenIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                    dTotalGenDesembolso = dTotalGenDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))
                End If

                sOperacionAnt = rw.Item("CodOperacionActiva").ToString.Trim

                dCont = dCont + 1
                iFila = iFila + 1

            Next

            iFila = iFila + 2

            'SubTotal para el Ultimo registro
            worksheet.Cells(iFila, 6).Value = "TOTAL "
            worksheet.Cells(iFila, 10).Value = dTotalGenIGV
            worksheet.Cells(iFila, 11).Value = dTotalGenDesembolso
            aux = 2
            While aux <= 16
                worksheet.Cells(iFila, aux).Style.Font.Bold = True
                aux = aux + 1
            End While
            iFila = iFila + 2

            col = 1
            row = 9

            dTotalIGV = 0
            dTotalDesembolso = 0
            dTotalPrecuota = 0
            dTotalPrecuotaCargar = 0
            dTotalIGVDolares = 0
            dTotalIGVSoles = 0
            dTotalGenDesembolso = 0
            dTotalGenIGV = 0
            iFila = 9
            dCont = 0

            For Each rw As DataRow In table2.Rows

                sOperacion = rw.Item("CodOperacionActiva").ToString.Trim
                If dCont = 0 Then
                    sOperacionAnt = rw.Item("CodOperacionActiva").ToString.Trim
                    iFila = iFila + 1
                    worksheet2.Cells(iFila, 12).Value = rw.Item("PorcenTasaActiva").ToString
                    worksheet2.Cells(iFila, 19).Value = rw.Item("EjecutivoLeasing").ToString.Trim
                End If

                If sOperacion = sOperacionAnt Then

                    worksheet2.Cells(iFila, 2).Value = rw.Item("CodUnico").ToString.Trim
                    worksheet2.Cells(iFila, 3).Value = rw.Item("CodOperacionActiva").ToString.Trim
                    worksheet2.Cells(iFila, 4).Value = rw.Item("NombreSubprestatario").ToString.Trim
                    worksheet2.Cells(iFila, 5).Value = rw.Item("Segmento").ToString.Trim
                    worksheet2.Cells(iFila, 6).Value = rw.Item("FechaDesembolso").ToString().Substring(0, 10)
                    worksheet2.Cells(iFila, 7).Value = rw.Item("NombreInstitucion").ToString.Trim
                    worksheet2.Cells(iFila, 8).Value = rw.Item("TipoBien").ToString.Trim
                    worksheet2.Cells(iFila, 9).Value = rw.Item("MontoFinanciado")

                    'EL NUEVO VALOR Neto IGV
                    worksheet2.Cells(iFila, 10).Value = rw.Item("MontoIGV").ToString.Trim
                    worksheet2.Cells(iFila, 11).Value = rw.Item("MontoDesembolso")
                    worksheet2.Cells(iFila, 13).Value = rw.Item("DesembolsoDiario")

                    worksheet2.Cells(iFila, 14).Value = rw.Item("SaldoDesembolso")
                    worksheet2.Cells(iFila, 15).Value = rw.Item("TextoInstruccion1").ToString.Trim
                    worksheet2.Cells(iFila, 16).Value = rw.Item("NroDocumento").ToString.Trim
                    worksheet2.Cells(iFila, 17).Value = rw.Item("MonedaDocumento").ToString.Trim
                    worksheet2.Cells(iFila, 18).Value = rw.Item("TipoCambio").ToString.Trim


                    dTotalDesembolso = dTotalDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))
                    dTotalIGV = dTotalIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                    'Totales por Columna
                    dTotalGenDesembolso = dTotalGenDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))
                    dTotalGenIGV = dTotalGenIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                Else 'else de Moneda
                    iFila = iFila + 1

                    'subtotales
                    worksheet2.Cells(iFila, 6).Value = "SUB TOTAL"
                    worksheet2.Cells(iFila, 10).Value = dTotalIGV
                    worksheet2.Cells(iFila, 11).Value = dTotalDesembolso

                    aux = 2
                    While aux <= 16
                        worksheet2.Cells(iFila, aux).Style.Font.Bold = True
                        aux = aux + 1
                    End While

                    dTotalIGV = 0
                    dTotalDesembolso = 0
                    dTotalPrecuota = 0
                    dTotalPrecuotaCargar = 0
                    dTotalIGVDolares = 0
                    dTotalIGVSoles = 0

                    iFila = iFila + 2

                    worksheet2.Cells(iFila, 2).Value = rw.Item("CodUnico").ToString.Trim
                    worksheet2.Cells(iFila, 3).Value = rw.Item("CodOperacionActiva").ToString.Trim
                    worksheet2.Cells(iFila, 4).Value = rw.Item("NombreSubprestatario").ToString.Trim
                    worksheet2.Cells(iFila, 5).Value = rw.Item("Segmento").ToString.Trim
                    worksheet2.Cells(iFila, 6).Value = rw.Item("FechaDesembolso").ToString().Substring(0, 10)
                    worksheet2.Cells(iFila, 7).Value = rw.Item("NombreInstitucion").ToString.Trim
                    worksheet2.Cells(iFila, 8).Value = rw.Item("TipoBien").ToString.Trim
                    worksheet2.Cells(iFila, 9).Value = rw.Item("MontoFinanciado")

                    'EL NUEVO VALOR Neto IGV
                    worksheet2.Cells(iFila, 10).Value = rw.Item("MontoIGV")
                    worksheet2.Cells(iFila, 11).Value = rw.Item("MontoDesembolso")
                    worksheet2.Cells(iFila, 13).Value = rw.Item("DesembolsoDiario")

                    worksheet2.Cells(iFila, 14).Value = rw.Item("SaldoDesembolso")
                    worksheet2.Cells(iFila, 15).Value = rw.Item("TextoInstruccion1").ToString.Trim
                    worksheet2.Cells(iFila, 16).Value = rw.Item("NroDocumento").ToString.Trim
                    worksheet2.Cells(iFila, 17).Value = rw.Item("MonedaDocumento").ToString.Trim
                    worksheet2.Cells(iFila, 18).Value = rw.Item("TipoCambio").ToString.Trim

                    worksheet2.Cells(iFila, 12).Value = rw.Item("PorcenTasaActiva").ToString.Trim
                    worksheet2.Cells(iFila, 19).Value = rw.Item("EjecutivoLeasing").ToString.Trim
                    dTotalIGV = dTotalIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                    dTotalDesembolso = dTotalDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))

                    'Totales por Columna
                    dTotalGenIGV = dTotalGenIGV + IIf(rw.Item("MontoIGV") Is DBNull.Value, 0, rw.Item("MontoIGV"))
                    dTotalGenDesembolso = dTotalGenDesembolso + IIf(rw.Item("MontoDesembolso") Is DBNull.Value, 0, rw.Item("MontoDesembolso"))
                End If

                sOperacionAnt = rw.Item("CodOperacionActiva").ToString.Trim

                dCont = dCont + 1
                iFila = iFila + 1

            Next

            iFila = iFila + 2

            'SubTotal para el Ultimo registro
            worksheet2.Cells(iFila, 6).Value = "TOTAL "
            worksheet2.Cells(iFila, 10).Value = dTotalGenIGV
            worksheet2.Cells(iFila, 11).Value = dTotalGenDesembolso
            aux = 2
            While aux <= 16
                worksheet2.Cells(iFila, aux).Style.Font.Bold = True
                aux = aux + 1
            End While

            iFila = iFila + 2


            'For Each cl As DataColumn In Table.Columns
            '    If cl.ColumnName <> "CodInstruccionDesembolso" Then
            '        If Not String.IsNullOrEmpty(rw(cl.ColumnName)) Then

            '            Dim RowData As String = rw(cl.ColumnName).ToString()
            '            worksheet.Cells(row, col).Value = RowData.Trim().ToString()
            '        End If
            '    End If
            '    col += 1
            'Next
            'row += 1
            'col = 1
            'Next


            pck.Save()
            Response.Redirect("../temp/ListaDesembolsos.xlsx", False)
            'Response.Redirect("../../../../temp/ListaDesembolsos.xls", False)

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub
    'Fin IBK
#End Region

#Region "WebMethods"

    ''' <summary>
    ''' recalcular
    ''' </summary>
    ''' <param name="pstrcodInstruccionDesembolso"></param>
    ''' <param name="pstrcodsolicitudcredito"></param>
    ''' <param name="pstrtcdia"></param>
    ''' <param name="pstrnroticket"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
        Public Shared Function recalcular(ByVal pstrcodInstruccionDesembolso As String, _
                                          ByVal pstrcodsolicitudcredito As String, _
                                          ByVal pstrtcdia As Decimal, _
                                          ByVal pstrnroticket As String) As String

        Try

            Try
                Dim objRecalcularTx As New LInstruccionDesembolsoTx
                Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
                Dim strEGCC_InsDesembolso As String
                With oEGCC_InsDesembolso
                    .Codinstrucciondesembolso = pstrcodInstruccionDesembolso
                    .Codsolicitudcredito = pstrcodsolicitudcredito
                    .tcdia = pstrtcdia
                    'Inicio IBK - AAE - Agrego Usuario
                    .UsuarioRegistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    'Fin IBK
                End With

                strEGCC_InsDesembolso = GCCUtilitario.SerializeObject(oEGCC_InsDesembolso)
                Dim intResult As String = objRecalcularTx.recalcula(strEGCC_InsDesembolso)

                Return intResult
                'If intResult = "0" Then
                '    Return "0"
                'Else
                '    Return "1"
                'End If

            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ListaAgrupacion
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaAgrupacion(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String) As JQGridJsonResponse

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strEGCC_InsDesembolsoAgrupacion As String
            With objEGCC_InsDesembolsoAgrupacion
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoAgrupacion)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoAgrupacion(strEGCC_InsDesembolsoAgrupacion))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 999, dtInstruccionDesembolso)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ListaAgrupacionDocumento
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <param name="pstrCodAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaAgrupacionDocumento(ByVal pstrNroContrato As String, _
                                                     ByVal pstrNroInstruccion As String, _
                                                     ByVal pstrCodAgrupacion As String) As JQGridJsonResponse

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strEGCC_InsDesembolsoAgrupacion As String
            With objEGCC_InsDesembolsoAgrupacion
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
                .Codcorrelativo = GCCUtilitario.NullableString(pstrCodAgrupacion)
            End With
            strEGCC_InsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoAgrupacion)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoDocAgrupacion(strEGCC_InsDesembolsoAgrupacion))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 999, dtInstruccionDesembolso)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ListaMediosPago
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <param name="pstrCodAgrupacion"></param>
    ''' <param name="pstrCodProveedor"></param>
    ''' <param name="pstrCodMonedaAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaMediosPago(ByVal pstrNroContrato As String, _
                                             ByVal pstrNroInstruccion As String, _
                                             ByVal pstrCodAgrupacion As String, _
                                             ByVal pstrCodProveedor As String, _
                                             ByVal pstrCodMonedaAgrupacion As String) As JQGridJsonResponse

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
            Dim strEGCC_InsDesembolsoPago As String
            With objEGCC_InsDesembolsoPago
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
                .Codagrupacion = GCCUtilitario.NullableString(pstrCodAgrupacion)
                .Codproveedor = GCCUtilitario.NullableString(pstrCodProveedor)
                .Codmonedaagrupacion = GCCUtilitario.NullableString(pstrCodMonedaAgrupacion)
            End With
            strEGCC_InsDesembolsoPago = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoPago)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago(strEGCC_InsDesembolsoPago))

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 999, dtInstruccionDesembolso)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ListaCargoAbono
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <param name="pstrTipoGrupo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaCargoAbono(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String, _
                                            ByVal pstrTipoGrupo As String) As JQGridJsonResponse

        Try

            Dim dtbInstruccionDesembolso As DataTable = New DataTable
            Dim objInstruccionDesembolso As Object = HttpContext.Current.Session("DTB_LISTACARGOABONO")
            If Not objInstruccionDesembolso Is Nothing Then
                objInstruccionDesembolso = CType(objInstruccionDesembolso, DataTable)


                Dim dvwfilterInsDesembolso As DataView = objInstruccionDesembolso.DefaultView
                'dvwfilterInsDesembolso.RowFilter = " CODIGOMONEDA = " & stMonedaWIO
                'dtbInstruccionDesembolso = New DataTable
                'dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable

                'Proveedores
                If pstrTipoGrupo.Trim().Equals("B") Then
                    dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION = '02'"
                    dtbInstruccionDesembolso = New DataTable
                    dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable
                End If

                'Clientes
                If pstrTipoGrupo.Trim().Equals("C") Then
                    dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION = '07'"
                    dtbInstruccionDesembolso = New DataTable
                    dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable
                End If

                'SUNAT
                If pstrTipoGrupo.Trim().Equals("D") Then
                    'inicio IBK - AAE - Arego nueva agrupacion
                    'dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION in ('01', '03', '04', '06')"
                    dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION in ('01', '03', '04', '06', '15')"
                    ' fin ibk 
                    dtbInstruccionDesembolso = New DataTable
                    dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable
                End If

                'Diferencia Cambio
                If pstrTipoGrupo.Trim().Equals("E") Then
                    dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION in ('05','13')"
                    dtbInstruccionDesembolso = New DataTable
                    dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable
                End If

                'Cargos
                If pstrTipoGrupo.Trim().Equals("F") Then
                    'Inicio IBK - AAE
                    'dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION in ('08','09','10','11','12')"
                    dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION in ('08','09','10','11','12','14')"
                    'Fin IBK - AAE
                    dtbInstruccionDesembolso = New DataTable
                    dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable
                End If

            End If

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 999, dtbInstruccionDesembolso)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'inicio IBK - AAE
    <WebMethod()> _
    Public Shared Function ListaDesembolsos(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pstrNroContrato As String, _
                                                   ByVal pstrCodInstDesembolso As String) As JQGridJsonResponse

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
            Dim dtDesembolsos As DataTable
            dtDesembolsos = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListaDesembolsos(pPageSize, _
                                                                                                       pCurrentPage, _
                                                                                                       pSortColumn, _
                                                                                                       pSortOrder, _
                                                                                                       pstrNroContrato, _
                                                                                                        pstrCodInstDesembolso))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtDesembolsos.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtDesembolsos.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtDesembolsos.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDesembolsos)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function PaginaCronograma(ByVal pstrPaginaActual As String) As JQGridJsonResponse
        Try

            'Arma Consulta Cronograma
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = HttpContext.Current.Session("DTB_CRONOGRAMA")

            If Not objListECronograma Is Nothing Then

                'Datos Paginacion
                Dim intTotalRegistros As Integer = objListECronograma.Count
                Dim intTotalxPagina As Integer = 50
                Dim intTotalPaginas As Integer = 0
                Dim intPaginaActual As Integer = GCCUtilitario.CheckInt(pstrPaginaActual)

                Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
                If decPaginasTotal <= 1 Then
                    intTotalPaginas = 1
                ElseIf decPaginasTotal <= 2 Then
                    intTotalPaginas = 2
                    'IBK - RPH se aumento el nro de cuotas a 240
                ElseIf decPaginasTotal <= 3 Then
                    intTotalPaginas = 3
                ElseIf decPaginasTotal <= 4 Then
                    intTotalPaginas = 4
                Else
                    intTotalPaginas = 5
                End If
                'Fin 

                'Resize Gronograma Datatable
                Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
                Dim objECronograma As New EGcc_cotizacioncronograma
                Dim intContador As Integer = 0
                For Each objECronograma In objListECronograma
                    If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                        objListECronogramaNuevo.Add(objECronograma)
                    End If
                    intContador = intContador + 1
                Next

                'Devuelve
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

            Else
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                Return objJQGridJsonResponse.JQGridJsonResponseError("Consulta Vacia")
            End If

        Catch ex As Exception
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseError(ex.Message)
        End Try

    End Function

    <WebMethod()> _
   Public Shared Function ListadoCronogramaActivacion(ByVal pstrNroContrato As String, _
                                                      ByVal pstrInstruccionDesembolso As String, _
                                                      ByVal pstrCuotaInicial As String _
                                                      ) As JQGridJsonResponse

        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

        Try


            'Ejecuta Consulta
            Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.CronogramaActivacionGet(pstrNroContrato, pstrInstruccionDesembolso))
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = PreparaCronograma(dtCronograma, pstrNroContrato, pstrInstruccionDesembolso, pstrCuotaInicial, False)

            HttpContext.Current.Session("DTB_CRONOGRAMA") = objListECronograma

            'Datos Paginacion
            Dim intTotalRegistros As Integer = objListECronograma.Count
            Dim intTotalxPagina As Integer = 50
            Dim intTotalPaginas As Integer = 0
            Dim intPaginaActual As Integer = 1

            Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
            If decPaginasTotal <= 1 Then
                intTotalPaginas = 1
            ElseIf decPaginasTotal <= 2 Then
                intTotalPaginas = 2
                'IBK - RPH se aumento el nro de cuotas a 240
            ElseIf decPaginasTotal <= 3 Then
                intTotalPaginas = 3
            ElseIf decPaginasTotal <= 4 Then
                intTotalPaginas = 4
            Else
                intTotalPaginas = 5
            End If
            'Fin

            'Resize Gronograma Datatable
            Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
            Dim objECronograma As New EGcc_cotizacioncronograma
            Dim intContador As Integer = 0
            For Each objECronograma In objListECronograma
                If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                    objListECronogramaNuevo.Add(objECronograma)
                End If
                intContador = intContador + 1
            Next

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' GuardarCotizacion (Insert y Edit)
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GeneraCronograma(ByVal pstrNroContrato As String, _
                                            ByVal pstrCodInstrDesembolso As String, _
                                            ByVal pstrTipoPersona As String, _
                                            ByVal pstrPrecioVenta As String, _
                                            ByVal pstrMontoIGV As String, _
                                            ByVal pstrValorVenta As String, _
                                            ByVal pstrCuotaInicial As String, _
                                            ByVal pstrRiesgoNeto As String, _
 _
                                            ByVal pstrTipoCronograma As String, _
                                            ByVal pstrNroCuotas As String, _
                                            ByVal pstrPeriodicidad As String, _
                                            ByVal pstrFrecuenciaPago As String, _
                                            ByVal pstrPlazoGracia As String, _
                                            ByVal pstrTipoGracia As String, _
                                            ByVal pstrFechaActivacion As String, _
                                            ByVal pstrFechavence As String, _
 _
                                            ByVal pstrTea As String, _
 _
                                            ByVal pstrTipoBienSeguro As String, _
                                            ByVal pstrImportePrimaSeguroBien As String, _
                                            ByVal pstrNumCuotasfinanciadas As String, _
 _
                                            ByVal pstrTipoSeguro As String, _
                                            ByVal pstrImportePrimaDesgravamen As String, _
                                            ByVal pstrNumCuotaFinanciar As String _
                                            ) As JQGridJsonResponse

        Try

            Dim objEGCC_InsDesembolsoActivacion As New EGCC_InsDesembolsoActivacion
            Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx

            'Inicia Valores
            With objEGCC_InsDesembolsoActivacion

                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrNroContrato)
                .CodInstruccionDesembolso = GCCUtilitario.NullableString(pstrCodInstrDesembolso)
                .Codigotipopersona = GCCUtilitario.NullableString(pstrTipoPersona)

                .Precioventa = GCCUtilitario.StringToDecimal(pstrPrecioVenta)
                .Valorventaigv = GCCUtilitario.StringToDecimal(pstrMontoIGV)
                .Valorventa = GCCUtilitario.StringToDecimal(pstrValorVenta)
                .Importecuotainicial = GCCUtilitario.StringToDecimal(pstrCuotaInicial)
                .Riesgoneto = GCCUtilitario.StringToDecimal(pstrRiesgoNeto)

                .Codigotipocronograma = GCCUtilitario.NullableString(pstrTipoCronograma)
                .Numerocuotas = GCCUtilitario.StringToInteger(pstrNroCuotas)
                .Codigoperiodicidad = GCCUtilitario.NullableString(pstrPeriodicidad)
                .Codigofrecuenciapago = GCCUtilitario.NullableString(pstrFrecuenciaPago)
                .Plazograciacuota = GCCUtilitario.StringToInteger(pstrPlazoGracia)
                .Codigotipograciacuota = GCCUtilitario.NullableString(pstrTipoGracia)
                .Fechamaxactivacion = GCCUtilitario.StringToDateTime(pstrFechaActivacion)
                .Fechaprimervencimiento = GCCUtilitario.StringToDateTime(pstrFechavence)

                .Teaporc = GCCUtilitario.StringToDecimal(pstrTea)

                .Codigobientiposeguro = GCCUtilitario.NullableString(pstrTipoBienSeguro)
                .Bienimporteprima = GCCUtilitario.StringToDecimal(pstrImportePrimaSeguroBien)
                .Biennrocuotasfinanciar = GCCUtilitario.StringToInteger(pstrNumCuotasfinanciadas)

                .Codigodesgravamentiposeguro = GCCUtilitario.NullableString(pstrTipoSeguro)
                .Desgravamenimporteprima = GCCUtilitario.StringToDecimal(pstrImportePrimaDesgravamen)
                .Desgravamennrocuotasfinanciar = GCCUtilitario.StringToInteger(pstrNumCuotaFinanciar)

            End With

            'Arma Consulta Cronograma
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            Dim objInput As New lpcCronograma.QuotationInput
            Dim objQry As New lpcCronograma.clsCronograma
            Dim objTbl As New DataTable

            If fGenerarQuotationInput(objInput, objEGCC_InsDesembolsoActivacion) Then
                objTbl = objQry.fGenerateQuoteDs(objInput)

                'Setea en Entidad
                objListECronograma = PreparaCronograma(objTbl, objEGCC_InsDesembolsoActivacion.CodSolicitudCredito, pstrCuotaInicial, objEGCC_InsDesembolsoActivacion.CodInstruccionDesembolso, True)
            End If

            'Setea en Sesion
            HttpContext.Current.Session("DTB_CRONOGRAMA") = objListECronograma

            'Datos Paginacion
            Dim intTotalRegistros As Integer = objListECronograma.Count
            Dim intTotalxPagina As Integer = 50
            Dim intTotalPaginas As Integer = 0
            Dim intPaginaActual As Integer = 1

            Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
            If decPaginasTotal <= 1 Then
                intTotalPaginas = 1
            ElseIf decPaginasTotal <= 2 Then
                intTotalPaginas = 2
                'IBK - RPH se aumento el nro de cuotas a 240
            ElseIf decPaginasTotal <= 3 Then
                intTotalPaginas = 3
            ElseIf decPaginasTotal <= 4 Then
                intTotalPaginas = 4
            Else
                intTotalPaginas = 5
            End If
            'Fin

            'Resize Gronograma Datatable
            Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
            Dim objECronograma As New EGcc_cotizacioncronograma
            Dim intContador As Integer = 0
            For Each objECronograma In objListECronograma
                If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                    objListECronogramaNuevo.Add(objECronograma)
                End If
                intContador = intContador + 1
            Next

            'Actualizo el cronograma y la info de activación
            Dim strCronograma As String = ""
            Dim strEGCC_InsDesembolsoActivacion As String
            strCronograma = GCCUtilitario.SerializeObject(objListECronograma)
            strEGCC_InsDesembolsoActivacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoActivacion)
            objLInstruccionDesembolsoTx.ActualizaCronogramaActivacion(strEGCC_InsDesembolsoActivacion, strCronograma)
            'Devuelve
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

        Catch ex As Exception
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseError(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' ValidaEjecucion
    ''' </summary>
    ''' <param name="pCodigoSolicitudCredito"></param>
    ''' <param name="pCodigoInstruccionDesembolso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ValidaEjecucionActParcial(ByVal pCodigoSolicitudCredito As String, _
                                           ByVal pCodigoInstruccionDesembolso As String) As String

        Try
            Dim objInstruccionDesembolsoNTX As New LInstruccionDesembolsoNTx    'LCheckListTx
            Dim oEGInstruccionDesembolso As New EGCC_InsDesembolso
            Dim strEGInstruccionDesembolso As String
            Dim strFlagCheclPrecuota As String = GCCUtilitario.fstrObtieneKeyWebConfig("ActParcialChequeaPrecuotas")
            With oEGInstruccionDesembolso
                .Codsolicitudcredito = pCodigoSolicitudCredito
                .Codinstrucciondesembolso = pCodigoInstruccionDesembolso
            End With


            strEGInstruccionDesembolso = GCCUtilitario.SerializeObject(oEGInstruccionDesembolso)
            Dim strResult As String = objInstruccionDesembolsoNTX.ValidaEjecucionInstruccionActParcial(strEGInstruccionDesembolso, strFlagCheclPrecuota)
            Return strResult

            'If intResult = 0 Then
            '    Return "0"
            'Else
            '    Return "1"
            'End If
        Catch ex As Exception
            Return "1|" + ex.ToString
            'Throw ex
        End Try
    End Function

    'Fin IBK

    ''' <summary>
    ''' EliminaInstrucionDesembolsoGrupo
    ''' </summary>
    ''' <param name="pCodSolicitudCredito"></param>
    ''' <param name="pCodInstruccionDesembolso"></param>
    ''' <param name="pCodTipoOperacion"></param>
    ''' <param name="pCodAgrupacion"></param>
    ''' <param name="pCodProveedor"></param>
    ''' <param name="pCodMonedaPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function EliminaInstrucionDesembolsoGrupo(ByVal pCodSolicitudCredito As String, _
                                                            ByVal pCodInstruccionDesembolso As String, _
                                                            ByVal pCodTipoOperacion As String, _
                                                            ByVal pCodAgrupacion As String, _
                                                            ByVal pCodProveedor As String, _
                                                            ByVal pCodMonedaPago As String) As String

        Try
            Dim objInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
            Dim oEGccInsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strInsDesembolsoAgrupacion As String
            With oEGccInsDesembolsoAgrupacion
                .Codsolicitudcredito = pCodSolicitudCredito
                .Codinstrucciondesembolso = pCodInstruccionDesembolso
                .Codtipooperacion = pCodTipoOperacion
                .Codagrupacion = pCodAgrupacion
                .Codproveedor = pCodProveedor
                .Codmonedapago = pCodMonedaPago
            End With

            strInsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(oEGccInsDesembolsoAgrupacion)
            Dim intResult As Boolean = objInstruccionDesembolsoTx.EliminarInsDesembolsoAgrupacion(strInsDesembolsoAgrupacion)

            ListaCargoAbonoIni(pCodSolicitudCredito, pCodInstruccionDesembolso)

            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' CargaDatosID
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function CargaDatosID(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String) As String

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolso As New EGCC_InsDesembolso
            Dim strEGCC_InsDesembolso As String
            With objEGCC_InsDesembolso
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableStringCombo(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolso = GCCUtilitario.SerializeObject(objEGCC_InsDesembolso)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolso("1", _
                                                                                                                                                       1, _
                                                                                                                                                       "CodInstruccionDesembolso", _
                                                                                                                                                       "ASC", _
                                                                                                                                                       strEGCC_InsDesembolso))

            'Datos
            Dim strMontos As String = ""
            If dtInstruccionDesembolso.Rows.Count > 0 Then
                strMontos = dtInstruccionDesembolso.Rows(0).Item("Totaldesembolsado").ToString + "|" + dtInstruccionDesembolso.Rows(0).Item("TotalAbono").ToString + "|" + dtInstruccionDesembolso.Rows(0).Item("TotalCargo").ToString
            Else
                strMontos = "0|0|0"
            End If
            Return strMontos
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ActualizaInsDesembolsoEstado
    ''' </summary>
    ''' <param name="pCodSolicitudCredito"></param>
    ''' <param name="pCodInstruccionDesembolso"></param>
    ''' <param name="pCodEstado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Agrego flagLPC
    <WebMethod()> _
    Public Shared Function ActualizaInsDesembolsoEstado(ByVal pCodSolicitudCredito As String, _
                                                            ByVal pCodInstruccionDesembolso As String, _
                                                            ByVal pCodEstado As String, _
                                                            ByVal pflagLPC As String) As String

        Try
            Dim objInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
            Dim oEGccEGCC_InsDesembolso As New EGCC_InsDesembolso
            Dim strInsDesembolso As String
            With oEGccEGCC_InsDesembolso
                .Codsolicitudcredito = pCodSolicitudCredito
                .Codinstrucciondesembolso = pCodInstruccionDesembolso
                .Codestadoinstruccion = pCodEstado
                .FlagLPC = Convert.ToInt32(pflagLPC)
            End With

            strInsDesembolso = GCCUtilitario.SerializeObject(oEGccEGCC_InsDesembolso)
            Dim intResult As Boolean = objInstruccionDesembolsoTx.ActualizarInsDesembolsoEstado(strInsDesembolso)

            If intResult = 0 Then
                Return "0|No se pudo actualizar el estado de la Instrucción de Desembolso"
            Else
                Return "1|Se actualizó el estado de la Instruccion de Desembolso correctamente"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ValidaEjecucion
    ''' </summary>
    ''' <param name="pCodigoSolicitudCredito"></param>
    ''' <param name="pCodigoInstruccionDesembolso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ValidaEjecucion(ByVal pCodigoSolicitudCredito As String, _
                                           ByVal pCodigoInstruccionDesembolso As String) As String

        Try
            Dim objInstruccionDesembolsoNTX As New LInstruccionDesembolsoNTx    'LCheckListTx
            Dim oEGInstruccionDesembolso As New EGCC_InsDesembolso
            Dim strEGInstruccionDesembolso As String
            With oEGInstruccionDesembolso
                .Codsolicitudcredito = pCodigoSolicitudCredito
                .Codinstrucciondesembolso = pCodigoInstruccionDesembolso
            End With


            strEGInstruccionDesembolso = GCCUtilitario.SerializeObject(oEGInstruccionDesembolso)
            Dim intResult As String = objInstruccionDesembolsoNTX.ValidaEjecucionInstruccion(strEGInstruccionDesembolso)
            Return intResult

            'If intResult = 0 Then
            '    Return "0"
            'Else
            '    Return "1"
            'End If
        Catch ex As Exception
            Return "1|" + ex.ToString
            'Throw ex
        End Try
    End Function
   
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Generar Input
    ''' </summary>
    ''' <param name="argInput"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function fGenerarQuotationInput(ByRef argInput As lpcCronograma.QuotationInput, ByVal pobjEGCC_InsDesembolsoActivacion As EGCC_InsDesembolsoActivacion) As Boolean
        Try

            'Valida Tipo Cronograma
            Dim strTipoCronograma As String = pobjEGCC_InsDesembolsoActivacion.Codigotipocronograma
            Dim strTipoCronogramaLcp As String = ""
            If strTipoCronograma.Trim().Equals(GCCConstante.C_CODIGO_TIPO_CRONOGRAMA_CAPITAL_CONSTANTE) Then
                strTipoCronogramaLcp = lpcCronograma.QuotationEnums.Enum_Tp_cronograma.Capital_constante
            Else
                strTipoCronogramaLcp = lpcCronograma.QuotationEnums.Enum_Tp_cronograma.Cuota_constante
            End If

            'Valida Periodicidad
            Dim strTipoPeriodicidad As String = pobjEGCC_InsDesembolsoActivacion.Codigoperiodicidad
            Dim strTipoPeriodicidadLcp As String = ""
            If strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_ANUAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Anual
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_MENSUAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Mensual
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_SEMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Semestral
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_TRIMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Trimestral
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_BIMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Bimestral
            End If

            'Valida FrecuenciaPago
            Dim strFrecuenciaPago As String = pobjEGCC_InsDesembolsoActivacion.Codigofrecuenciapago
            Dim strFrecuenciaPagoLcp As Integer = 0
            If strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_FIJA) Then
                strFrecuenciaPagoLcp = 1
            End If

            'Valida Cantidad Dias
            Dim intCantDias As Integer = 0
            If strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_VARIABLE) Then
                intCantDias = 360
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_VARIABLE) Then
                intCantDias = 60
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_VARIABLE) Then
                intCantDias = 30
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_VARIABLE) Then
                intCantDias = 180
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_VARIABLE) Then
                intCantDias = 90
            End If

            'Setea Parametros para el Cronograma
            With argInput
                .Tp_cronograma = strTipoCronogramaLcp
                .Im_prestamo = pobjEGCC_InsDesembolsoActivacion.Riesgoneto
                .De_tea = pobjEGCC_InsDesembolsoActivacion.Teaporc
                .De_plazo = pobjEGCC_InsDesembolsoActivacion.Numerocuotas

                If Not pobjEGCC_InsDesembolsoActivacion.Fechamaxactivacion.HasValue Then
                    .Dt_desembolso = Now
                    .fechas = 1
                Else
                    .Dt_desembolso = pobjEGCC_InsDesembolsoActivacion.Fechamaxactivacion
                End If
                If Not pobjEGCC_InsDesembolsoActivacion.Fechaprimervencimiento.HasValue Then
                    Dim dtmFecActivacion As DateTime = Now

                    If pobjEGCC_InsDesembolsoActivacion.Fechamaxactivacion.HasValue Then
                        dtmFecActivacion = pobjEGCC_InsDesembolsoActivacion.Fechamaxactivacion
                    End If

                    .Dt_primer_vcmto = dtmFecActivacion.AddDays(intCantDias)
                    .fechas = 1
                Else
                    .Dt_primer_vcmto = pobjEGCC_InsDesembolsoActivacion.Fechaprimervencimiento
                End If

                '.Il_cuota_doble = CBool(Me.selIl_cuota_doble.SelectedValue)
                .Tp_frecuencia = strTipoPeriodicidadLcp
                .Il_fijo = strFrecuenciaPagoLcp
                .De_plazo_gracia = pobjEGCC_InsDesembolsoActivacion.Plazograciacuota
                '.CostoFondo = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Costofondoporc)
                '.PorcCuotaInicial = GCCUtilitario.CheckDecimal(pobjEGCC_InsDesembolsoActivacion.Cuotainicialporc)
                .PrecioVenta = pobjEGCC_InsDesembolsoActivacion.Precioventa
                .ValorIgv = pobjEGCC_InsDesembolsoActivacion.Valorventaigv ' OJO :: Monto del IGV
                '.Moneda = pobjEGcc_cotizacion.Codigomoneda
                .RiesgoNeto = pobjEGCC_InsDesembolsoActivacion.Riesgoneto
                '.MostrarTEACartas = pobjEGcc_cotizacion.Mostrarteacartas
                '.MostrarOpcCompras = 1 'IIf(Me.chkMostrarOpcion.Checked = True, 1, 0) => no hay campo
                '.MostrarComisionAct = pobjEGcc_cotizacion.Mostrarmontocomision
                '.MostrarEstructuracionCartas = 1 'IIf(Me.chkMostrarComEstructuracion.Checked = True, 1, 0) => no hay campo
                .ImporteCuotaIni = GCCUtilitario.CheckDecimal(pobjEGCC_InsDesembolsoActivacion.Importecuotainicial)
                '.Observacion = pobjEGcc_cotizacion.Otrascomisiones

                'If Not pobjEGcc_cotizacion.FechaOfertaValida.HasValue Then
                .FechaValidez = Now
                ' Else
                '.FechaValidez = pobjEGcc_cotizacion.FechaOfertaValida
                'End If


                '.TipoBien = pobjEGcc_cotizacion.Codigoclasificacionbien 'Verificar
                '.OpcCompra = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importeopcioncompra)
                '.ComActivacion = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecomisionactivacion)
                '.ComEstruc = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecomisionestructuracion)

                .TipoSeguro = pobjEGCC_InsDesembolsoActivacion.Codigobientiposeguro
                .Im_seguro = GCCUtilitario.CheckDecimal(pobjEGCC_InsDesembolsoActivacion.Bienimporteprima)
                .De_plazo_Seg = GCCUtilitario.CheckInt(pobjEGCC_InsDesembolsoActivacion.Biennrocuotasfinanciar)

                'If pobjEGcc_cotizacion.Codigodesgravamentiposeguro <> 0 Then
                .SeguroDes = GCCUtilitario.CheckDecimal(pobjEGCC_InsDesembolsoActivacion.Desgravamenimporteprima)
                .De_plazo_Seg_Des = GCCUtilitario.CheckInt(pobjEGCC_InsDesembolsoActivacion.Desgravamennrocuotasfinanciar)
                'End If

                'IBK - RPH se agrega el tipo de Gracia
                .TipoGracia = pobjEGCC_InsDesembolsoActivacion.Codigotipograciacuota
                'Fin

            End With
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Prepara Cronograma para insertar
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Shared Function PreparaCronograma(ByVal pdtbCronograma As DataTable, ByVal pstrCodigoContrato As String, ByVal pstrCuotaInicial As String, ByVal pstrInstruccionDesembolso As String, ByVal booValidaPrimeraFila As Boolean) As ListEGcc_cotizacioncronograma

        Try
            'Declara Variables
            Dim objCotizacionNTx As New LCotizacionNTx
            Dim objListECronograma As New ListEGcc_cotizacioncronograma

            'Valida Cuota Inicial
            Dim intContadorCronograma As Integer = 0
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(pstrCuotaInicial)
            Dim blnMuestraFila As Boolean = True

            'Valida si existe
            If pdtbCronograma.Rows.Count > 0 Then

                For Each oRow As DataRow In pdtbCronograma.Rows

                    If Not decCuotaInicial > 0 And intContadorCronograma = 0 And booValidaPrimeraFila Then
                        blnMuestraFila = False
                    Else
                        blnMuestraFila = True
                    End If

                    If blnMuestraFila Then

                        Dim objECronograma As New EGcc_cotizacioncronograma
                        With objECronograma
                            .Numerocuota = GCCUtilitario.CheckInt(oRow.Item("Nu_cuota").ToString())
                            .Codigocotizacion = pstrCodigoContrato
                            .Versioncotizacion = "1"
                            .Fechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .Cantdiascuota = GCCUtilitario.StringToInteger(oRow.Item("Nu_dias").ToString())
                            .Montosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString())
                            .Montointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString())
                            .Montoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString())
                            .Montototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString())
                            .Saldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString())
                            .Interessegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString())
                            .Principalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString())
                            .Montocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString())

                            .SaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString())
                            .InteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString())
                            .PrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString())
                            .CuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString())

                            .Totalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString())
                            .Montototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString())

                            'Mostrar
                            .SFechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .SMontosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteressegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SCuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .STotalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString()).ToString(GCCConstante.C_FormatMiles)

                            .Audestadologico = 1
                            '.AudFechaRegistro = 	
                            '.AudFechaModificacion = 	
                            .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                            .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        End With
                        objListECronograma.Add(objECronograma)

                    End If

                    intContadorCronograma = intContadorCronograma + 1

                Next

                Return objListECronograma

            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Private Sub ObtieneDatosContrato(ByVal strNroContrato As String, ByVal strNroInstruccion As String)

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolso As New EGCC_InsDesembolso
            Dim strEGCC_InsDesembolso As String
            With objEGCC_InsDesembolso
                .Codsolicitudcredito = GCCUtilitario.NullableString(strNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableStringCombo(strNroInstruccion)
            End With
            strEGCC_InsDesembolso = GCCUtilitario.SerializeObject(objEGCC_InsDesembolso)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolso("1", _
                                                                                                                                                       1, _
                                                                                                                                                       "CodInstruccionDesembolso", _
                                                                                                                                                       "ASC", _
                                                                                                                                                       strEGCC_InsDesembolso))
            'Inicio IBK - AAE Agrego Tabla
            Dim table2 As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoTotales(GCCUtilitario.NullableString(strNroContrato), GCCUtilitario.NullableStringCombo(strNroInstruccion)))

            'Valida si existe
            If dtInstruccionDesembolso.Rows.Count > 0 Then

                txtNroContrato.Value = dtInstruccionDesembolso.Rows(0).Item("CodSolicitudCredito").ToString
                txtCuCliente.Value = dtInstruccionDesembolso.Rows(0).Item("CodUnico").ToString
                txtRazonSocial.Value = dtInstruccionDesembolso.Rows(0).Item("ClienteRazonSocial").ToString
                txtTipoContrato.Value = dtInstruccionDesembolso.Rows(0).Item("TipoContrato").ToString
                txtMoneda.Value = dtInstruccionDesembolso.Rows(0).Item("NombreMoneda").ToString
                txtNroInstruccion.Value = dtInstruccionDesembolso.Rows(0).Item("CodInstruccionDesembolso").ToString
                txtEstado.Value = dtInstruccionDesembolso.Rows(0).Item("NombreEstado").ToString
                hddCodMonedaContrato.Value = dtInstruccionDesembolso.Rows(0).Item("CodMoneda").ToString

                txtTotalDesembolsado.Value = dtInstruccionDesembolso.Rows(0).Item("Totaldesembolsado").ToString
                txtTotalPagos.Value = dtInstruccionDesembolso.Rows(0).Item("TotalAbono").ToString
                txtTotalCargos.Value = dtInstruccionDesembolso.Rows(0).Item("TotalCargo").ToString

                hddCodCotizacion.Value = dtInstruccionDesembolso.Rows(0).Item("CodigoCotizacion").ToString

                txtTcDia.Value = dtInstruccionDesembolso.Rows(0).Item("tcdia").ToString
                txtTcTicket.Value = "0.00"

                hddCodEstadoInstruccion.Value = dtInstruccionDesembolso.Rows(0).Item("CodEstadoInstruccion").ToString

                txtValorVenta.Value = dtInstruccionDesembolso.Rows(0).Item("ValorVenta").ToString
                txtPrecioventa.Value = dtInstruccionDesembolso.Rows(0).Item("PrecioVenta").ToString
                txtIgv.Value = dtInstruccionDesembolso.Rows(0).Item("ValorVentaIGV").ToString
                hddActivacion.Value = GCCUtilitario.CheckInt(dtInstruccionDesembolso.Rows(0).Item("FlagActivacionLeasing"))
                'Inicio IBK - AAE
                hddFlagEnvioLPC.Value = dtInstruccionDesembolso.Rows.Item(0).Item("FlagLPC").ToString
                hddCodigoSubtipoContrato.Value = dtInstruccionDesembolso.Rows.Item(0).Item("CodigoSubTipoContrato").ToString.Trim
                'Fin IBK

            End If
            'Inicio IBK - AAE
            If table2.Rows.Count > 0 Then
                txtTotProv.Value = table2.Rows.Item(0).Item("TotProv").ToString
                txtTotDtoSUNAT.Value = table2.Rows.Item(0).Item("TotSUNAT").ToString
                txtTotSUNAT.Value = table2.Rows.Item(0).Item("TotSUNAT").ToString
                txtTotAdelantosProv.Value = table2.Rows.Item(0).Item("TotAdelantos").ToString
                txtTotAdelantos.Value = table2.Rows.Item(0).Item("TotAdelantos").ToString
                txtTotDUAS.Value = table2.Rows.Item(0).Item("TotDUAS").ToString
                txtTotDif.Value = table2.Rows.Item(0).Item("TotDifTC").ToString
            End If
            'Fin IBK

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub ListaCargoAbonoIni(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String)

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strEGCC_InsDesembolsoAgrupacion As String
            With objEGCC_InsDesembolsoAgrupacion
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoAgrupacion)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono(strEGCC_InsDesembolsoAgrupacion))
            HttpContext.Current.Session("DTB_LISTACARGOABONO") = dtInstruccionDesembolso

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    'Inicio IBK - AAE - Obtengo info para activación parcial
    Public Sub ObtenerDatosActivacionParcial(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String)

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolso As New EGCC_InsDesembolso
            Dim strEGCC_InsDesembolso As String
            With objEGCC_InsDesembolso
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableStringCombo(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolso = GCCUtilitario.SerializeObject(objEGCC_InsDesembolso)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoActParcial(strEGCC_InsDesembolso))
            'Valida si existe
            If dtInstruccionDesembolso.Rows.Count > 0 Then
                Me.hddTipoPersona.Value = dtInstruccionDesembolso.Rows(0).Item("TipoPersona").ToString
                Me.txtPrecioVentaCro.Value = dtInstruccionDesembolso.Rows(0).Item("Precioventa").ToString
                Me.txtMontoIGVCro.Value = dtInstruccionDesembolso.Rows(0).Item("Valorventaigv").ToString
                Me.txtValorVentaCro.Value = dtInstruccionDesembolso.Rows(0).Item("Valorventa").ToString
                Me.txtCuotaInicialCro.Value = dtInstruccionDesembolso.Rows(0).Item("Importecuotainicial").ToString
                Me.txtRiesgoNetoCro.Value = dtInstruccionDesembolso.Rows(0).Item("Riesgoneto").ToString
                Me.txtTEA.Value = dtInstruccionDesembolso.Rows(0).Item("TEA").ToString

                GCCUtilitario.CargarComboValorGenerico(Me.cmbPeriodicidad, GCCConstante.C_TABLAGENERICA_PERIOCIDAD)
                GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbFrecuenciaPago, GCCConstante.C_TABLAGENERICA_FRECUENCIA_PAGO, dtInstruccionDesembolso.Rows(0).Item("Codigoperiodicidad").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoCronograma, dtInstruccionDesembolso.Rows(0).Item("Codigotipocronograma").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbPeriodicidad, dtInstruccionDesembolso.Rows(0).Item("Codigoperiodicidad").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbFrecuenciaPago, dtInstruccionDesembolso.Rows(0).Item("Codigofrecuenciapago").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoGracia, dtInstruccionDesembolso.Rows(0).Item("Codigotipograciacuota").ToString.Trim)
                Me.txtNroCuotas.Value = dtInstruccionDesembolso.Rows(0).Item("Numerocuotas").ToString.Trim
                Me.txtPlazoGracia.Value = dtInstruccionDesembolso.Rows(0).Item("Plazograciacuota").ToString.Trim
                Me.txtFechaMaxActivacion.Value = GCCUtilitario.CheckDateString(dtInstruccionDesembolso.Rows(0).Item("FechaActivacion").ToString.Trim, "C")
                Me.txtFechavence.Value = GCCUtilitario.CheckDateString(dtInstruccionDesembolso.Rows(0).Item("Fechaprimervencimiento").ToString.Trim, "C")
                GCCUtilitario.SeleccionaCombo(cmbTipoBienSeguro, dtInstruccionDesembolso.Rows(0).Item("Codigobientiposeguro").ToString.Trim)
                Me.txtImportePrimaSeguroBien.Value = dtInstruccionDesembolso.Rows(0).Item("Bienimporteprima").ToString.Trim
                Me.txtNumCuotasfinanciadas.Value = dtInstruccionDesembolso.Rows(0).Item("Biennrocuotasfinanciar").ToString.Trim

                GCCUtilitario.SeleccionaCombo(cmbTipoSeguro, dtInstruccionDesembolso.Rows(0).Item("Codigodesgravamentiposeguro").ToString.Trim)
                Me.txtImportePrimaDesgravamen.Value = dtInstruccionDesembolso.Rows(0).Item("Desgravamenimporteprima").ToString.Trim
                Me.txtNumCuotaFinanciar.Value = dtInstruccionDesembolso.Rows(0).Item("Desgravamennrocuotasfinanciar").ToString.Trim
                Me.txtCuotaIniContrato.Value = dtInstruccionDesembolso.Rows(0).Item("CuotaInicialContrato").ToString.Trim

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Documentos"

    ''' <summary>
    ''' Listado de Documentos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoInsDesembolsoDocumento(ByVal pPageSize As Integer, _
                                                         ByVal pCurrentPage As Integer, _
                                                         ByVal pSortColumn As String, _
                                                         ByVal pSortOrder As String, _
                                                         ByVal pstrCodInstruccion As String, _
                                                         ByVal pstrCodContrato As String _
                                                       ) As JQGridJsonResponse

        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

        Try

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoDoc As New EGCC_InsDesembolsoDoc
            Dim strEGCC_InsDesembolsoDoc As String
            With objEGCC_InsDesembolsoDoc
                .CodInstruccionDesembolso = GCCUtilitario.NullableString(pstrCodInstruccion)
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodContrato)
            End With
            strEGCC_InsDesembolsoDoc = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoDoc)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInstruccionDesembolsoDoc(pPageSize, _
                                                                                                                                                       pCurrentPage, _
                                                                                                                                                       pSortColumn, _
                                                                                                                                                       pSortOrder, _
                                                                                                                                                       strEGCC_InsDesembolsoDoc))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCotizacion.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(dtCotizacion.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCotizacion)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Eliminar DocumentoCometario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaDocumentoComentario(ByVal pstrCodInstruccion As String, _
                                                      ByVal pstrCodContrato As String, _
                                                      ByVal pstrCodigoDocumento As String _
                                                       ) As String

        ''Variables
        Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx

        Try

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoDoc As New EGCC_InsDesembolsoDoc
            Dim strEGCC_InsDesembolsoDoc As String
            With objEGCC_InsDesembolsoDoc
                .CodInstruccionDesembolso = GCCUtilitario.NullableString(pstrCodInstruccion)
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Codigodocumento = GCCUtilitario.NullableString(pstrCodigoDocumento)
                .Audestadologico = 0
            End With
            strEGCC_InsDesembolsoDoc = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoDoc)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objLInstruccionDesembolsoTx.EliminarInstruccionDesembolsoDoc(strEGCC_InsDesembolsoDoc)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Registro WIO"

    ''' <summary>
    ''' Registrar un nuevo WIO y Cambiar estado del Contrato
    ''' </summary>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EnviaWIO(ByVal pstrCodContrato As String, ByVal pstrCodInstruccion As String, ByVal pstrTotDifTC As String, ByVal pstrActivacion As String) As String

        Dim strResp As String = ""
        'Inicio IBk - AAE 
        Dim strRespArr As String()
        'Fin IBK
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Dim blnActivacionSinDocs As Boolean = False
        Dim strDocumentos As String = ""
        Dim pdecTotDifTC As Decimal
        Try

            'Prepara documentos
            Dim oLwsDesembolsoNTx As New LDesembolsoNTx
            Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
            Dim odtbListaDoc As New DataTable
            With oEContratoEstructDoc
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
            End With
            odtbListaDoc = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDesembolsoNTx.ListarContratoEstructDoc(999, 1, "codsolicitudcredito", "asc", GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc)))
            If odtbListaDoc IsNot Nothing Then
                If odtbListaDoc.Rows.Count > 0 Then
                    Dim intContador As Integer = 0
                    For Each dr As DataRow In odtbListaDoc.Rows

                        Dim strCodInstruccion As String = dr("CodInstruccionDesembolso").ToString
                        If (strCodInstruccion.Trim.Equals(pstrCodInstruccion.Trim)) Then

                            'pCodProveedor + "$" + pTipoDocumento + "$" + fn_util_trim(pCodigoDocumento) + "$" + pFechaEmision + "$" + pMoneda + "$" + pMonto;
                            Dim strGrupo As String = ""
                            Dim strCodProveedor As String = dr("CodProveedor").ToString
                            Dim strTipoDocumento As String = dr("TipoDocumento").ToString
                            'inicio IBK - Se corrige error de WIO
                            'Dim strNroDocumento As String = dr("NumeroDocumentoProveedor").ToString
                            Dim strNroDocumento As String = dr("NroDocumento").ToString
                            Dim strFecEmision As String = dr("StringFechaEmisionCorta").ToString
                            Dim strMoneda As String = dr("MonedaOriginal").ToString
                            Dim strMonto As String = dr("TotalConvertido").ToString
                            strGrupo = strCodProveedor.Trim + "$" + strTipoDocumento.Trim + "$" + strNroDocumento.Trim + "$" + strFecEmision.Trim + "$" + strMoneda.Trim + "$" + strMonto.Trim

                            If intContador = 0 Then
                                strDocumentos = strGrupo + "|"
                            ElseIf intContador = odtbListaDoc.Rows.Count - 1 Then
                                strDocumentos = strDocumentos + strGrupo
                            Else
                                strDocumentos = strDocumentos + strGrupo + "|"
                            End If

                            intContador = intContador + 1

                        End If

                    Next
                End If
            End If
            If pstrActivacion.Trim() = "1" And strDocumentos.Length = 0 Then
                blnActivacionSinDocs = True
            End If
            If Not blnActivacionSinDocs Then
                strDocumentos = strDocumentos.Trim.Substring(0, strDocumentos.Length - 1)
            End If

            'Prepara Llamado a Metodo WIO
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(pstrCodContrato))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows

                        'Dim pstrCodContrato As String = dr("").ToString
                        Dim pstrCodUnico As String = dr("CODUNICO").ToString
                        Dim pstrNroLinea As String = dr("NROLINEA").ToString
                        Dim pstrProducto As String = dr("CODPRODUCTOFINANCIEROACTIVO").ToString
                        'Dim pstrChekeados As String = dr("").ToString
                        Dim pstrMonedaContrato As String = dr("CODMONEDA").ToString
                        Dim pstrTipoCambioDia As String = ObtenerTipoCambio(IIf(pstrMonedaContrato = GCCConstante.C_COD_MONEDA_SOLES, GCCConstante.C_COD_MONEDA_DOLARES, pstrMonedaContrato), GCCUtilitario.ToStringyyyyMMdd(Today.ToString), "SBS")
                        Dim pstrMontoFinanciado As String = dr("PRECIOVENTA").ToString
                        Dim pstrMontoIgv As String = dr("VALORVENTAIGV").ToString
                        Dim pstrImporteInicial As String = dr("IMPORTECUOTAINICIAL").ToString
                        Dim pstrCapitalFinanciado As String = dr("VALORVENTA").ToString
                        Dim pstrRiesgoAsumido As String = dr("RIESGONETO").ToString
                        Dim pstrNroCuotas As String = dr("NUMEROCUOTAS").ToString
                        Dim pstrPeriocidad As String = dr("CODIGOFRECUENCIAPAGO").ToString
                        Dim pstrImporteOpCompra As String = dr("IMPORTEOPCIONCOMPRA").ToString
                        Dim pstrGastoActivacion As String = dr("IMPORTECOMISIONACTIVACION").ToString
                        Dim pstrPolizaSeguro As String = dr("CODIGOBIENTIPOSEGURO").ToString
                        Dim pstrCodProcedenciaCotizacion As String = dr("CODIGOPROCEDENCIA").ToString
                        Dim pstrTipoDesembolso As String = dr("CODIGOSUBTIPOCONTRATO").ToString
                        Dim pstrNroIOAsociado As String = IIf(dr("NUMEROINSTRUCCION") = 0, "", dr("NUMEROINSTRUCCION"))
                        Dim pstrCostoFondos As String = dr("COSTOFONDOPORC").ToString
                        Dim pstrTasa As String = dr("TEAPORC").ToString
                        Dim pstrSpread As String = dr("SPREADPORC").ToString
                        'Inicio IBK - AAE
                        pdecTotDifTC = GCCUtilitario.CheckDecimal(pstrTotDifTC)
                        'Fin IBK
                        strResp = RegistrarWIO(pstrCodContrato, _
                                                pstrCodUnico, _
                                                pstrNroLinea, _
                                                pstrProducto, _
                                                strDocumentos, _
                                                pstrMonedaContrato, _
                                                pstrTipoCambioDia, _
                                                pstrMontoFinanciado, _
                                                pstrMontoIgv, _
                                                pstrImporteInicial, _
                                                pstrCapitalFinanciado, _
                                                pstrRiesgoAsumido, _
                                                pstrNroCuotas, _
                                                pstrPeriocidad, _
                                                pstrImporteOpCompra, _
                                                pstrGastoActivacion, _
                                                pstrPolizaSeguro, _
                                                pstrCodProcedenciaCotizacion, _
                                                pstrTipoDesembolso, _
                                                pstrNroIOAsociado, _
                                                pstrCostoFondos, _
                                                pstrTasa, _
                                                pstrSpread, _
                                                pdecTotDifTC, _
                                                blnActivacionSinDocs)


                    Next
                End If
            End If
            'Inicio IBK - AAE
            Dim FlagLPC As String = "0"
            strRespArr = strResp.Split("|")
            If strRespArr(0) = "0" Then
                'Actualiza Estado WIO
                ActualizaInsDesembolsoEstado(pstrCodContrato, pstrCodInstruccion, GCCConstante.C_ESTADO_INSDESEMBOLSO_WIO, FlagLPC)
            End If
            'Fin IBK
        Catch ex As Exception
            strResp = String.Concat("1|", ex.Message)
        End Try

        Return strResp

    End Function

    ''' <summary>
    ''' Registrar un nuevo WIO y Cambiar estado del Contrato
    ''' </summary>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <param name="pstrChekeados">Todos los Chekeados</param>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RegistrarWIO(ByVal pstrCodContrato As String, _
                                       ByVal pstrCodUnico As String, _
                                       ByVal pstrNroLinea As String, _
                                       ByVal pstrProducto As String, _
                                       ByVal pstrChekeados As String, _
                                       ByVal pstrMonedaContrato As String, _
                                       ByVal pstrTipoCambioDia As String, _
                                       ByVal pstrMontoFinanciado As String, _
                                       ByVal pstrMontoIgv As String, _
                                       ByVal pstrImporteInicial As String, _
                                       ByVal pstrCapitalFinanciado As String, _
                                       ByVal pstrRiesgoAsumido As String, _
                                       ByVal pstrNroCuotas As String, _
                                       ByVal pstrPeriocidad As String, _
                                       ByVal pstrImporteOpCompra As String, _
                                       ByVal pstrGastoActivacion As String, _
                                       ByVal pstrPolizaSeguro As String, _
                                       ByVal pstrCodProcedenciaCotizacion As String, _
                                       ByVal pstrTipoDesembolso As String, _
                                       ByVal pstrNroIOAsociado As String, _
                                       ByVal pstrCostoFondos As String, _
                                       ByVal pstrTasa As String, _
                                       ByVal pstrSpread As String, _
                                       ByVal pdecTotDifTC As Decimal, _
                                       ByVal pblnActivaSinDocs As Boolean _
                                ) As String

        Dim oESolCredEstructDoc As ESolicitudcreditoestructuradoc
        Dim oLwsDesembolso As New LDesembolsoTx
        Dim oLwsDesembolsoNtx As New LDesembolsoNTx
        Dim strResp As String = ""
        Try




            Dim decTotalWio As Decimal = 0
            Dim decTotalGrilla As Decimal = 0

            Dim decTcVenta As Decimal = pstrTipoCambioDia.Split("$")(0)
            Dim decTcCompra As Decimal = pstrTipoCambioDia.Split("$")(1)

            Dim pChkTotal As String() = pstrChekeados.TrimEnd("|").Split("|")
            Dim pValores As String()
            Dim odtbContratoEstruct As New DataTable
            Dim listaContratoEstruct As New ArrayList

            Dim flagVerificaBienes As Boolean
            flagVerificaBienes = True

            'VERIFICA EN EL LISTADO DE DOCUMENTOS CUAL TIENE VIENES ASOCIADOS

            Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
            Dim odtbLista As New DataTable

            With oEContratoEstructDoc
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
            End With
            odtbLista = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDesembolsoNtx.ListarContratoEstructDoc(999, _
                                                                           1, _
                                                                           "CodSolicitudCredito", _
                                                                           "asc", _
                                                                           GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc) _
                                                                           ))
            If pblnActivaSinDocs = False Then
                For x As Integer = 0 To pChkTotal.Length - 1
                    pValores = pChkTotal(x).Split("$")
                    oESolCredEstructDoc = New ESolicitudcreditoestructuradoc
                    With oESolCredEstructDoc
                        .Codsolicitudcredito = pstrCodContrato
                        .CodProveedor = pValores(0)
                        .Tipodocumento = pValores(1)
                        .Nrodocumento = pValores(2)
                        .StringFechaEmision = pValores(3)
                        .Estadodocumento = GCCConstante.eEstadoDoc.Desembolsado

                        'If pstrMonedaContrato = GCCConstante.C_CODMON_SOLES Then
                        '    If pValores(4) = GCCConstante.C_CODMON_DOLARES Then
                        '        decTotalGrilla = (GCCUtilitario.CheckDecimal(pValores(5)) * GCCUtilitario.CheckDecimal(decTcCompra))
                        '    Else
                        '        decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                        '    End If
                        'Else
                        '    If pValores(4) = GCCConstante.C_CODMON_SOLES Then
                        '        decTotalGrilla = (GCCUtilitario.CheckDecimal(pValores(5)) / GCCUtilitario.CheckDecimal(decTcVenta))
                        '    Else
                        '        decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                        '    End If
                        'End If

                        decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                        If pValores(1).Trim = "09" Or pValores(1).Trim = "97" Or pValores(1).Trim = "87" Then
                            decTotalWio = decTotalWio - decTotalGrilla
                        Else
                            decTotalWio = decTotalWio + decTotalGrilla
                        End If
                    End With
                    listaContratoEstruct.Add(oESolCredEstructDoc)

                    'Dim strNumeroDocumento As String = ""
                    Dim strfechaemision As String = ""
                    For Each oRow As DataRow In odtbLista.Rows

                        strfechaemision = GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim())

                        If (oRow.Item("CodSolicitudCredito").ToString().Trim().Equals(pstrCodContrato.Trim()) _
                            And oRow.Item("TipoDocumento").ToString().Trim().Equals(pValores(1).Trim()) _
                            And oRow.Item("NroDocumento").ToString().Trim().Equals(pValores(2).Trim()) _
                            And oRow.Item("CodProveedor").ToString().Trim().Equals(pValores(0).Trim()) _
                            And GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim()).Equals(pValores(3).Trim())) Then

                            If (GCCUtilitario.CheckInt(oRow.Item("contadorbienes").ToString()) <= 0) Then
                                flagVerificaBienes = False
                                'strNumeroDocumento = strNumeroDocumento + oRow.Item("NroDocumento").ToString().Trim() + "|"
                            End If
                        End If

                    Next


                Next
                'Inicio IBK - AAE
                decTotalWio = decTotalWio + pdecTotDifTC

                'Fin IBK - AAE
            Else
                For Each oRow As DataRow In odtbLista.Rows
                    oESolCredEstructDoc = New ESolicitudcreditoestructuradoc
                    With oESolCredEstructDoc
                        .Codsolicitudcredito = pstrCodContrato
                        .CodProveedor = oRow.Item("CodProveedor").ToString().Trim()
                        .Tipodocumento = oRow.Item("TipoDocumento").ToString().Trim()
                        .Nrodocumento = oRow.Item("NroDocumento").ToString().Trim()
                        .StringFechaEmision = GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim())
                        .Estadodocumento = GCCConstante.eEstadoDoc.Desembolsado
                    End With
                    listaContratoEstruct.Add(oESolCredEstructDoc)
                Next
                decTotalWio = 0.01
            End If ' pblnActivaSinDocs = False Then

            If flagVerificaBienes = True Then

                odtbContratoEstruct = GCCUtilitario.ToDataTable(listaContratoEstruct)
                Dim xmlContratoEstruct As String = GCCUtilitario.fstrConvertirDataTableAXML(odtbContratoEstruct)

                'Obtener Cuenta Cargo
                Dim odtbCtaCargoLeasing As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDesembolsoNtx.ObtenerCtaLeasingWioSel(pstrCodContrato))
                Dim xmlCtaContratoLeasing As String = GCCUtilitario.fstrConvertirDataTableAXML(odtbCtaCargoLeasing)

                'Obtener Leasing para enviar a WIO
                Dim odtbBienleasing As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDesembolsoNtx.ObtenerBienLeasingWIO(xmlContratoEstruct))
                Dim xmlBienleasing As String = GCCUtilitario.fstrConvertirDataTableAXML(odtbBienleasing)

                'Declaracion de Objetos donde se serializan 
                Dim strEClienteWIO As String = ""
                Dim strEInstruccionWio As String = ""
                Dim strELineaWio As String = ""
                Dim strECaracteristicaWio As String = ""
                Dim strTasaLineaWio As String = ""
                Dim strSeguimientoWio As String = ""
                Dim strCodTiendaRm As String = ""
                pCargarDatosWIO(pstrCodContrato, _
                                pstrCodUnico, _
                                pstrProducto, _
                                pstrMonedaContrato, _
                                decTotalWio, _
                                decTcVenta.ToString, _
                                pstrMontoFinanciado, _
                                pstrMontoIgv, _
                                pstrImporteInicial, _
                                pstrCapitalFinanciado, _
                                pstrRiesgoAsumido, _
                                pstrNroCuotas, _
                                pstrPeriocidad.Trim, _
                                pstrImporteOpCompra, _
                                pstrGastoActivacion, _
                                pstrPolizaSeguro.Trim, _
                                pstrCodProcedenciaCotizacion, _
                                pstrTipoDesembolso, _
                                pstrNroIOAsociado, _
                                strCodTiendaRm, _
                                strEClienteWIO, strEInstruccionWio, strECaracteristicaWio, strSeguimientoWio, pstrCostoFondos)
                Dim strEsActivacion As String = "0"
                If pblnActivaSinDocs Then
                    strEsActivacion = "1"
                End If
                Dim strNroIO As String = oLwsDesembolso.RegistrarWIO(strEClienteWIO, _
                                             strEInstruccionWio, _
                                             strECaracteristicaWio, _
                                             strSeguimientoWio, _
                                             xmlCtaContratoLeasing, _
                                             xmlBienleasing, _
                                             xmlContratoEstruct, _
                                             pstrCodContrato, _
                                             pstrNroLinea, _
                                             strEsActivacion)


                pCargarDatosLinea(pstrNroLinea, pstrMonedaContrato, strCodTiendaRm, strNroIO, strELineaWio, strTasaLineaWio, pstrTasa, pstrSpread)
                oLwsDesembolso.RegistrarLineaWIO(strELineaWio, strTasaLineaWio, strNroIO)

                strResp = String.Concat("0|", strNroIO)
            Else
                strResp = String.Concat("1|", "Debe asociar todos los documentos a un bien")
            End If  'fin

            Return strResp

        Catch ex As Exception
            Return String.Concat("1|", ex.Message)
        End Try

    End Function

    Private Shared Sub pCargarDatosWIO(ByVal pstrCodContrato As String, _
                                       ByVal pstrCodUnico As String, _
                                       ByVal pstrProducto As String, _
                                       ByVal pstrMonedaContrato As String, _
                                       ByVal pImporte As Decimal, _
                                       ByVal pstrTipocambio As String, _
                                       ByVal pstrMontoFinanciado As String, _
                                       ByVal pstrMontoIgv As String, _
                                       ByVal pstrImporteInicial As String, _
                                       ByVal pstrCapitalFinanciado As String, _
                                       ByVal pstrRiesgoAsumido As String, _
                                       ByVal pstrNroCuotas As String, _
                                       ByVal pstrPeriocidad As String, _
                                       ByVal pstrImporteOpCompra As String, _
                                       ByVal pstrGastoActivacion As String, _
                                       ByVal pstrPolizaSeguro As String, _
                                       ByVal pstrCodProcedenciaCotizacion As String, _
                                       ByVal pstrTipoDesembolso As String, _
                                       ByVal pstrNroIOAsociado As String, _
                                       ByRef pstrCodTiendaRm As String, _
                                       ByRef pEClienteWIO As String, ByRef pEInstruccionWio As String, _
                                       ByRef pECaracteristicaWio As String, ByRef pESeguimientoWio As String, _
                                       ByRef pstrCostoFondos As String)
        Dim oLWebservice As New LWebService
        Dim secuencia As Integer = 1
        Try
            Dim strCodUnico As String = pstrCodUnico
            Dim strError As String = ""
            Dim oEClienteRM As New EClienteRM
            oEClienteRM = GCCUtilitario.fObtenerDatosRMCliente(1, strCodUnico, "2", "0", strError)

            If oEClienteRM Is Nothing Then
                Throw New Exception(strError)
                'oEClienteRM.Codigounico = strCodUnico
            End If
            Dim strUsuariosACLS As String = ObtMiembrosGpoLogico(GCCUtilitario.fstrObtieneKeyWebConfig("NeumonicoACLS"), _
                                                                 GCCUtilitario.fstrObtieneKeyWebConfig("wsUltimusWBC"))
            Dim oEClienteWIO As New F15.Entity.ECLIENTE
            With oEClienteWIO
                .Codigounico = GCCUtilitario.fFormatoCodCliente(oEClienteRM.Codigounico, 10)
                .Razonsocial = oEClienteRM.Razonsocialcliente
                .Ciiu = GCCUtilitario.CheckInt(oEClienteRM.Ciiu)
                .Banca = oEClienteRM.Banca
                .Codigoejecutivo = oEClienteRM.Codigoejecutivo
                .Codigotienda = oEClienteRM.Codigotienda
                .Nombretienda = oEClienteRM.Nombretienda
                .Ratingempresa = GCCUtilitario.CheckDecimal(oEClienteRM.Ratingempresa)
                .Fecharatingempresa = Now
                .Segmento = IIf(oEClienteRM.Segmento = "1", "1", "0")
                .Fechaibk = Now
                .Clasificacionsbs = oEClienteRM.Clasificacionsbs
                .Fechasbs = Now
                .Clasificacionfeve = oEClienteRM.Clasificacionfeve
                .Fechafeve = Now
                .Numerodocumento = oEClienteRM.Numerodocumento
                .Nombreejecutivo = oEClienteRM.Nombreejecutivo
                .Nombregrupo = oEClienteRM.Nombregrupo
                .Codigogrupo = GCCUtilitario.CheckInt(oEClienteRM.Codigogrupo)
                .Codigotipodocumento = fObtenerCodigoTipoDoc(oEClienteRM.Codigotipodocumento)
                .CODIGOUSUARIOCREACION = GCCSession.CodigoUsuario
                .CODIGOUSUARIOMODIFICACION = GCCSession.CodigoUsuario
                .Zona = "Zona 1"
            End With
            pstrCodTiendaRm = oEClienteWIO.Codigotienda

            If Not String.IsNullOrEmpty(pstrNroIOAsociado) Then
                secuencia = oLWebservice.fintObtenerSecuenciaLs(pstrNroIOAsociado.PadLeft(18, "0"))
            End If

            'INSTRUCCION OPERATIVA WIO
            Dim pintForOperacion As Integer = 0
            Dim oEInstOperativa As New F15.Entity.EINSTRUCCIONOPERATIVA
            With oEInstOperativa
                .Numeroinstruccion = 0
                .Codigounico = oEClienteWIO.Codigounico
                .Codigoproducto = fObtenerProductoWio(pstrProducto, pintForOperacion)
                .Codigoformaoperacion = pintForOperacion
                .CODIGOUSUARIOCREACION = strUsuariosACLS
                .Secuencia = secuencia
                .Codigomoneda = fObtenerCodigoMonedaWio(pstrMonedaContrato)
                .Contratomarco = 0
                .Importebase = pImporte
                .Importefinal = .Importebase
                .Tipocambio = GCCUtilitario.CheckDecimal(pstrTipocambio)
                .CODIGOWORKFLOW = 0
                .ESTACIONULTIMUS = F15.Entity.EConstante.C_vRECEPCION_SOLICITUD
                .Codigoestado = 221
                .Modalidad = fObtenerProcedenciaWio(pstrCodProcedenciaCotizacion.Trim)
                .TipoDesembolso = fObtenerTipoDesembolsoWio(pstrTipoDesembolso)
                .ActivaOP = 0
                .Numerooperacionasociada = IIf(secuencia = 1, "", pstrNroIOAsociado)
                .Numerooperacion = pstrCodContrato
                .NumeroregistroN1 = pstrCodContrato
                .Aplicacostofondo = 1
                .Valorcostofondo = pstrCostoFondos
            End With

            'Caracteristicas Leasing
            Dim oECaractOperLeasing As New F15.Entity.ECARACTERISTICAOPERACIONLEASING
            With oECaractOperLeasing
                .NumeroInstruccion = 0
                .CodMoneda = oEInstOperativa.Codigomoneda
                .CapitalFinanciado = GCCUtilitario.CheckDecimal(pstrCapitalFinanciado)
                .IGV = pstrMontoIgv
                .MontoFinanciado = pstrMontoFinanciado
                .CuotaInicial = pstrImporteInicial
                .RiesgoAsumido = pstrRiesgoAsumido
                .NumeroCuotas = pstrNroCuotas
                .Periodicidad = fObtenerPeriocidadWio(pstrPeriocidad)
                .PeriodicidadGracia = 0
                .OpcionCompra = pstrImporteOpCompra
                .Gastos = pstrGastoActivacion
                .PolizaSeguro = fObtenerPolizaSeguroWio(pstrPolizaSeguro)
                .EspecificacionesDesembolso = ""
            End With
            pECaracteristicaWio = GCCUtilitario.SerializeObject(Of F15.Entity.ECARACTERISTICAOPERACIONLEASING)(oECaractOperLeasing)

            'SEGUIMEINTO
            Dim oESeguimiento As New F15.Entity.ESEGUIMIENTO
            With oESeguimiento
                .Numeroinstruccion = 0
                .Codigoseguimiento = 0
                .Fechainicio = CType(Now, Date)
                .Fechainicioproceso = CType(Now, Date)
                .Fechafin = CType(Now, Date)
                .Codigousuarioregistro = GCCSession.CodigoUsuario
                .Vigenciausuario = 1
                .Nivel = 0
                .Codigoaccion = F15.Entity.EConstante.eAccion.nGuardar
                .Codigoestacion = F15.Entity.EConstante.C_nRECEPCION_SOLICITUD
                .Secuencia = 1
                .NombreUsuario = GCCSession.NombreUsuario
                .Observacion = "Se envia desde " & GCC.Entity.EConstante.C_NOMBRE_APLICATIVO & " en Desembolso."
                .CodigoRol = GCCSession.PerfilUsuario
            End With

            pEClienteWIO = GCCUtilitario.SerializeObject(Of F15.Entity.ECLIENTE)(oEClienteWIO)
            pEInstruccionWio = GCCUtilitario.SerializeObject(Of F15.Entity.EINSTRUCCIONOPERATIVA)(oEInstOperativa)
            pESeguimientoWio = GCCUtilitario.SerializeObject(Of F15.Entity.ESEGUIMIENTO)(oESeguimiento)

        Catch ex As Exception
            Throw ex
        Finally
            oLWebservice = Nothing
        End Try
    End Sub

    Private Shared Sub pCargarDatosLinea(ByVal pstrNroLinea As String, _
                                  ByVal pstrMonedaContrato As String, _
                                  ByVal pstrCodTiendaRm As String, _
                                  ByVal pstrNroInstruccion As String, _
                                  ByRef pELineaWio As String, _
                                  ByRef pTasaLineaWio As String, _
                                  ByRef pstrTasa As String, _
                                  ByRef pstrSpread As String)
        Dim oLWebservice As New LWebService
        Try
            Dim stMonedaWIO As String = fObtenerCodigoMonedaWio(pstrMonedaContrato)

            'LINEAS
            Dim odtbLODet As DataTable = GCCUtilitario.DeserializeObject2(Of DataTable)(oLWebservice.fObtenerDatosLineaOP(pstrNroLinea))

            If odtbLODet.Rows.Count > 0 Then
                Dim oELineaCredito As New F15.Entity.ELINEACREDITO
                With oELineaCredito
                    .Numeroinstruccion = pstrNroInstruccion
                    .Codigotipocredito = 87
                    .Nuevalineaoperacion = 0
                    .Numerolineaoperacion = pstrNroLinea
                    .NroLinea = pstrNroLinea

                    .Riesgomaximocliente = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("RIESGOMAXIMOCLIENTE").ToString)
                    .Riesgomaximogrupo = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("RIESGOMAXIMOGRUPO").ToString)

                    .RiesgomaximoclienteOri = .Riesgomaximocliente
                    .RiesgomaximogrupoOri = .Riesgomaximogrupo

                    .Montoaprobado = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("MONTOAPROBADO").ToString)
                    .Saldodisponible = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("SALDOANTESOPERACION").ToString)
                    .Saldoreservado = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("SALDORESERVADO").ToString)
                    .Saldolocacion = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("MONTORECIBIDO").ToString)
                    .Codigoestado = GCCUtilitario.CheckInt(odtbLODet.Rows(0).Item("CODIGOESTADO").ToString)
                    .CodigoTienda = pstrCodTiendaRm

                    .Fechavencimiento = GCCUtilitario.CheckDate(odtbLODet.Rows(0).Item("FECHAVENCIMIENTO").ToString)
                End With

                'Obtener Tasa
                Dim odtbTasa As DataTable = GCCUtilitario.DeserializeObject2(Of DataTable)(oLWebservice.fObtenerTasasLineas("0", pstrNroLinea))
                Dim tblMoneda As New DataTable
                Dim tblTipo As New DataTable
                Dim tblSubtipo As New DataTable
                For Each DR As DataRow In odtbTasa.Rows
                    tblMoneda = GCCUtilitario.DeserializeObject(Of DataTable)(oLWebservice.fObtenerParamDomWio(7, CType(DR("CODIGOMONEDA"), Integer), 1))
                    tblTipo = GCCUtilitario.DeserializeObject(Of DataTable)(oLWebservice.fObtenerParamDomWio(32, CType(DR("CODIGOTIPOTASA"), Integer), 1))
                    tblSubtipo = GCCUtilitario.DeserializeObject(Of DataTable)(oLWebservice.fObtenerParamDomWio(33, CType(DR("CODIGOSUBTIPOTASA"), Integer), 1))
                    DR("CODIGOMONEDA") = tblMoneda.Rows(0).Item("CODIGO").ToString
                    DR("MONEDA") = tblMoneda.Rows(0).Item("DESCRIPCION").ToString
                    DR("CODIGOTIPOTASA") = tblTipo.Rows(0).Item("CODIGO").ToString
                    DR("TIPOTASA") = tblTipo.Rows(0).Item("DESCRIPCION").ToString
                    DR("CODIGOSUBTIPOTASA") = tblSubtipo.Rows(0).Item("CODIGO").ToString
                    DR("SUBTIPOTASA") = tblSubtipo.Rows(0).Item("DESCRIPCION").ToString

                    tblMoneda = Nothing
                    tblTipo = Nothing
                    tblSubtipo = Nothing
                Next

                Dim dvwfilterTasaComisionWIO As DataView = odtbTasa.DefaultView
                dvwfilterTasaComisionWIO.RowFilter = " CODIGOMONEDA = " & stMonedaWIO
                odtbTasa = New DataTable
                odtbTasa = dvwfilterTasaComisionWIO.ToTable

                If odtbTasa.Rows.Count = 0 Then

                    '-------------------------------------
                    'INICIO :: IF
                    '-------------------------------------
                    Dim FilaTasaComision As DataRow = odtbTasa.NewRow

                    '******************************
                    'Tasa
                    '******************************
                    FilaTasaComision("CODIGOTIPOTASA") = "226"
                    FilaTasaComision("TIPOTASA") = "T.I.R."
                    FilaTasaComision("CODIGOSUBTIPOTASA") = "237"
                    FilaTasaComision("SUBTIPOTASA") = "Tasa"

                    If stMonedaWIO = "95" Then
                        FilaTasaComision("CODIGOMONEDA") = "95"
                        FilaTasaComision("MONEDA") = "Nuevos Soles"

                    ElseIf stMonedaWIO = "96" Then
                        FilaTasaComision("CODIGOMONEDA") = "96"
                        FilaTasaComision("MONEDA") = "Dólares USA"
                    End If

                    FilaTasaComision("VALORLINEAS") = "-0.0100"
                    FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrTasa).ToString(GCCConstante.C_FormatMiles)
                    FilaTasaComision("CODIGOTASACOMISION") = "1"

                    'Aumenta FILA
                    odtbTasa.Rows.Add(FilaTasaComision)

                    '******************************
                    'Spread Mínimo
                    '******************************
                    FilaTasaComision = odtbTasa.NewRow
                    FilaTasaComision("CODIGOTIPOTASA") = "227"
                    FilaTasaComision("TIPOTASA") = "Spread Mínimo"
                    FilaTasaComision("CODIGOSUBTIPOTASA") = "238"
                    FilaTasaComision("SUBTIPOTASA") = "Spread"

                    If stMonedaWIO = "95" Then
                        FilaTasaComision("CODIGOMONEDA") = "95"
                        FilaTasaComision("MONEDA") = "Nuevos Soles"

                    ElseIf stMonedaWIO = "96" Then
                        FilaTasaComision("CODIGOMONEDA") = "96"
                        FilaTasaComision("MONEDA") = "Dólares USA"
                    End If

                    FilaTasaComision("VALORLINEAS") = "-0.0100"
                    FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrSpread).ToString(GCCConstante.C_FormatMiles)
                    FilaTasaComision("CODIGOTASACOMISION") = "2"

                    'Aumenta FILA
                    odtbTasa.Rows.Add(FilaTasaComision)

                    '-------------------------------------
                    'FIN :: IF
                    '-------------------------------------

                Else

                    '-------------------------------------
                    'INICIO :: ELSE
                    '-------------------------------------

                    'Filtro
                    dvwfilterTasaComisionWIO.RowFilter = ""
                    dvwfilterTasaComisionWIO.RowFilter = "CODIGOSUBTIPOTASA=237 AND CODIGOTIPOTASA=226"


                    If dvwfilterTasaComisionWIO.Count = 0 Then
                        Dim FilaTasaComision As DataRow = odtbTasa.NewRow
                        FilaTasaComision("CODIGOTIPOTASA") = "226"
                        FilaTasaComision("TIPOTASA") = "T.I.R."
                        FilaTasaComision("CODIGOSUBTIPOTASA") = "237"
                        FilaTasaComision("SUBTIPOTASA") = "Tasa"

                        If stMonedaWIO = "95" Then
                            FilaTasaComision("CODIGOMONEDA") = "95"
                            FilaTasaComision("MONEDA") = "Nuevos Soles"

                        ElseIf stMonedaWIO = "96" Then
                            FilaTasaComision("CODIGOMONEDA") = "96"
                            FilaTasaComision("MONEDA") = "Dólares USA"
                        End If

                        FilaTasaComision("VALORLINEAS") = "-0.0100"
                        FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrTasa).ToString(GCCConstante.C_FormatMiles)
                        FilaTasaComision("CODIGOTASACOMISION") = "1"

                        odtbTasa.Rows.Add(FilaTasaComision)

                    Else

                        For Each oRow As DataRow In odtbTasa.Rows
                            If oRow.Item("CODIGOSUBTIPOTASA").ToString().Trim().Equals("237") And oRow.Item("CODIGOTIPOTASA").ToString().Trim().Equals("226") Then
                                oRow.Item("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrTasa).ToString(GCCConstante.C_FormatMiles)
                            End If
                        Next
                        odtbTasa.AcceptChanges()

                    End If


                    'Filtro
                    dvwfilterTasaComisionWIO.RowFilter = ""
                    dvwfilterTasaComisionWIO.RowFilter = "CODIGOSUBTIPOTASA=238 AND CODIGOTIPOTASA=227"

                    If dvwfilterTasaComisionWIO.Count = 0 Then
                        Dim FilaTasaComision As DataRow = odtbTasa.NewRow
                        FilaTasaComision("CODIGOTIPOTASA") = "227"
                        FilaTasaComision("TIPOTASA") = "Spread Mínimo"
                        FilaTasaComision("CODIGOSUBTIPOTASA") = "238"
                        FilaTasaComision("SUBTIPOTASA") = "Spread"

                        If stMonedaWIO = "95" Then
                            FilaTasaComision("CODIGOMONEDA") = "95"
                            FilaTasaComision("MONEDA") = "Nuevos Soles"
                        ElseIf stMonedaWIO = "96" Then
                            FilaTasaComision("CODIGOMONEDA") = "96"
                            FilaTasaComision("MONEDA") = "Dólares USA"
                        End If

                        FilaTasaComision("VALORLINEAS") = "-0.0100"
                        FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrSpread).ToString(GCCConstante.C_FormatMiles)
                        FilaTasaComision("CODIGOTASACOMISION") = "2"
                        odtbTasa.Rows.Add(FilaTasaComision)

                    Else

                        For Each oRow As DataRow In odtbTasa.Rows
                            If oRow.Item("CODIGOSUBTIPOTASA").ToString().Trim().Equals("238") And oRow.Item("CODIGOTIPOTASA").ToString().Trim().Equals("227") Then
                                oRow.Item("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrSpread).ToString(GCCConstante.C_FormatMiles)
                            End If
                        Next
                        odtbTasa.AcceptChanges()

                    End If

                    '-------------------------------------
                    'FIN :: ELSE
                    '-------------------------------------

                End If

                pELineaWio = GCCUtilitario.SerializeObject(Of F15.Entity.ELINEACREDITO)(oELineaCredito)
                pTasaLineaWio = GCCUtilitario.fstrConvertirDataTableAXML(odtbTasa)
            Else
                pELineaWio = Nothing
                pTasaLineaWio = Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLWebservice = Nothing
        End Try
    End Sub

    Private Shared Function ObtMiembrosGpoLogico(ByVal pstrGroupName As String, _
                                                 ByVal sURLwsUltimus As String) As String

        Dim ObjStructGroupMembers() As wsUltimus.structGroupMembers = Nothing
        Dim strUserName As String = ""
        Dim strUsuarios As String = ""
        Dim strDominioUsuario As String = GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
        Dim objUltimus As New LWebService
        Dim bok As Boolean = False
        Try
            bok = objUltimus.fblnObtenerMemberGroup(pstrGroupName, ObjStructGroupMembers, sURLwsUltimus, "")
            If ObjStructGroupMembers IsNot Nothing Then
                For intIndex As Int32 = 0 To ObjStructGroupMembers.Length - 1
                    strUserName = ObjStructGroupMembers(intIndex).strMemberName.ToUpper
                    strUserName = strUserName.Replace(strDominioUsuario.ToUpper & "/", "").Trim()
                    strUsuarios += strUserName + ","
                Next
            End If

            strUsuarios = strUsuarios.TrimEnd(",")
        Catch ex As Exception
            Throw ex
        Finally
            objUltimus = Nothing
        End Try
        Return strUsuarios
    End Function

    Private Shared Function fObtenerCodigoTipoDoc(ByVal pstrTipoDoc As String) As Integer
        Dim intTipoDoc As Integer = 0
        Select Case pstrTipoDoc
            Case GCC.Entity.EConstante.C_TRX_TIPDOC_DNI
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_DNI
            Case GCC.Entity.EConstante.C_TRX_TIPDOC_CE
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_CE
            Case GCC.Entity.EConstante.C_TRX_TIPDOC_PA
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_PAS
            Case Else
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_RUC
        End Select

        Return intTipoDoc
    End Function

    Private Shared Function fObtenerCodigoMonedaWio(ByVal pstrCodigoMoneda As String) As Integer
        Select Case pstrCodigoMoneda
            Case GCCConstante.C_CODMON_SOLES
                Return 95
            Case GCCConstante.C_CODMON_DOLARES
                Return 96
        End Select
    End Function

    Private Shared Function fObtenerProductoWio(ByVal pstrCodigoProducto As String, ByRef pintForOperacion As Integer) As Integer
        Select Case pstrCodigoProducto
            Case GCCConstante.C_CODLPC_PROD_LEASING
                pintForOperacion = 26
                Return 11
            Case GCCConstante.C_CODLPC_PROD_LEASEBACK
                pintForOperacion = 64
                Return 19
        End Select
    End Function

    Private Shared Function fObtenerPeriocidadWio(ByVal pstrCodigoPeriocidad As String) As Integer
        Select Case pstrCodigoPeriocidad.Trim
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_FIJA
                Return 456
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_VARIABLE
                Return 816
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_FIJA
                Return 151
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_VARIABLE
                Return 813
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_FIJA
                Return 150
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_VARIABLE
                Return 812
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_FIJA
                Return 455
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_VARIABLE
                Return 815
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_FIJA
                Return 152
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_VARIABLE
                Return 814
            Case Else
                Return 457
        End Select
    End Function

    Private Shared Function fObtenerPolizaSeguroWio(ByVal pstrCodigoPoliza As String) As Integer
        Select Case pstrCodigoPoliza
            Case "001"
                Return 490
            Case "002"
                Return 489
        End Select
    End Function

    Private Shared Function fObtenerProcedenciaWio(ByVal pstrCodigoProcedencia As String) As Integer
        Select Case pstrCodigoProcedencia
            Case "001"
                Return 219
            Case "002"
                Return 220
        End Select
    End Function

    Private Shared Function fObtenerTipoDesembolsoWio(ByVal psrtTipoDesembolso As String) As Integer
        Select Case psrtTipoDesembolso
            Case "001"
                Return 181
            Case "002"
                Return 180
        End Select
    End Function

    Private Shared Function ObtenerTipoCambio(ByVal strMonedaBusq As String, _
                                               ByVal strFecha As String, _
                                               ByVal strTipoModalidaCambio As String) As String
        Dim oLwsTipoCambioNtx As New LUtilNTX
        Dim odtbDatos As New DataTable
        Dim strResult As String = ""
        Try
            odtbDatos = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTipoCambioNtx.ObtenerTipoCambio(strMonedaBusq, strTipoModalidaCambio, strFecha))
            If odtbDatos.Rows.Count = 0 Then
                strResult = String.Concat("0$0")
            Else
                strResult = String.Concat(odtbDatos.Rows(0).Item("MontoValorVenta").ToString, "$", odtbDatos.Rows(0).Item("MontoValorCompra").ToString)
            End If
            odtbDatos = Nothing

            Return strResult
        Catch ex As Exception
            Return "0"
        Finally
            oLwsTipoCambioNtx = Nothing
        End Try
    End Function

#End Region

#Region "Ejecucion ID"

    ''' <summary>
    ''' EjecutaInsDesembolso 
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Modifico la ejeccución del desembolso
    <WebMethod()> _
    Public Shared Function EjecutaInsDesembolso(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrEnvioLPC As String, ByVal pstrReintentar As String) As String

        'Dim strResultado As String

        'Try
        '    strResultado = "1|Se Ejecutó la Instrucción de Desembolso Correctamente."
        '    'strResultado = CargaEnCuentas(pstrNroContrato, pstrNroInstruccion)
        '    Dim arrResultado As String() = strResultado.Split("|")

        '    If Not arrResultado(0).ToString = "1" Then
        '        ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, GCCConstante.C_ESTADO_INSDESEMBOLSO_APROBADA)
        '    End If

        '    Return strResultado

        'Catch ex As Exception
        '    strResultado = "1|No se pudo realizar la Ejecución de Desembolso. (" + ex.Message + ")"
        '    Return strResultado
        'End Try

        Dim str As String
        Dim tx As New LInstruccionDesembolsoNTx
        Dim table As New DataTable
        Dim strRta As String = ""
        Dim strRetorno As String = ""
        Dim numEstado As Integer = 0
        Dim strFlagLPC As String = "0"
        Dim pFlag As String = "1"
        Dim strRetFinWIO As String

        Try
            Dim enumerator As IEnumerator
            Dim str10 As String = "Inicio a ejecutar el desembolso."
            table = GCCUtilitario.DeserializeObject(Of DataTable)(tx.getCargosCuentaInsDes(pstrNroContrato, pstrNroInstruccion))
            Dim flagTieneSaldo As Boolean = True
            Try
                enumerator = table.Rows.GetEnumerator
                Do While enumerator.MoveNext
                    Dim strTipoCta As String
                    Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                    Dim pstrCU As String = current.Item("CodUnico").ToString
                    Dim pstrCodMoneda As String = current.Item("CodMonedaCuenta").ToString
                    Dim strNroCta As String = current.Item("Numero_Cuenta").ToString
                    Dim pstrCodTienda As String = strNroCta.Substring(0, 3)
                    Dim pstrNroCta As String = strNroCta.Substring(3, 10)
                    If (current.Item("CodTipoCuenta").ToString.Trim = "01") Then
                        strTipoCta = "IM"
                    Else
                        strTipoCta = "ST"
                    End If
                    Dim numTotalAbonos As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoTotalAbonos").ToString)
                    numTotalAbonos = numTotalAbonos - numTotalAbonos * 0.00005 'itf
                    Dim numNetoCargo As Decimal = Decimal.Subtract((GCCUtilitario.CheckDecimal(current.Item("MontoTotalCargos").ToString) * 1.00005), numTotalAbonos)
                    Dim dblNetoCargo As Double = Convert.ToDouble(numNetoCargo)
                    Dim numSaldo As Double = 0
                    If (Decimal.Compare(numNetoCargo, Decimal.Zero) > 0) Then
                        numSaldo = InsDesembolso_frmInsDesembolsoRegistro.saldoCuenta(pstrCU, strTipoCta, pstrCodMoneda, pstrCodTienda, pstrNroCta, strRta)
                    End If
                    If ((Decimal.Compare(numNetoCargo, Decimal.Zero) > 0) And (numSaldo < dblNetoCargo)) Then
                        numEstado = 1
                        flagTieneSaldo = False
                        strRetorno = (pstrCodTienda & "-" & pstrNroCta)
                    End If
                    If (strRta <> "") Then
                        numEstado = 1
                        flagTieneSaldo = False
                        strRetorno = strRta
                    End If
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
            If flagTieneSaldo Then
                Dim strError As String = ""
                numEstado = 2
                InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "009", strFlagLPC)
                numEstado = InsDesembolso_frmInsDesembolsoRegistro.CargaEnCuentas(pstrNroContrato, pstrNroInstruccion, strError)
                Select Case numEstado
                    Case -1
                        Return ("2|" & strError)
                    Case -2
                        str10 = ("1|" & strError)
                        InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strFlagLPC)
                        Return str10
                    Case 3
                        Dim numRetPC As Integer
                        strFlagLPC = "1"
                        If (pstrEnvioLPC <> "1") Then
                            numRetPC = InsDesembolso_frmInsDesembolsoRegistro.EjecutarDesembolsoLPC(pFlag, pstrNroInstruccion, GCCSession.CodigoUsuario)
                        Else
                            numRetPC = 0
                        End If
                        If (numRetPC <> 0) Then
                            strFlagLPC = "0"
                            InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strFlagLPC)
                            Return "1|Error en LPC"
                        End If
                        strFlagLPC = "1"
                        InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "006", strFlagLPC)
                        'Finalizo WIO
                        strRetFinWIO = FinalizaWIO(pstrNroContrato, pstrNroInstruccion)
                        Dim strRetWio As String() = strRetFinWIO.Split("|")
                        If strRetWio(0) = "0" Then
                            Return "0|Desembolso Ejecutado Corretamente"
                        Else
                            Return strRetFinWIO
                        End If
                End Select
                InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strFlagLPC)
                Return ("1|Error en ejecucion: " & numEstado.ToString & " - " & strError)
            End If 'flagTieneSaldo Then
            str = (numEstado.ToString & "|No hay saldo disponible en la cuenta de cargo " & strRetorno)
        Catch exception1 As Exception
            str = (numEstado.ToString & "|No se pudo finalizar la Ejecución de Desembolso. (" & exception1.ToString & ")")
            Return str
        End Try
        Return str
    End Function

    ''' <summary>
    ''' CargaEnCuentas 
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Reingeniería de la función
    <WebMethod()> _
    Public Shared Function CargaEnCuentas(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByRef strError As String) As Integer
        Dim num As Integer
        Dim num2 As Integer = 2
        Dim str As String = ""
        Dim pstrTrama As String = ""
        Try
            Dim tx2 As New LInstruccionDesembolsoNTx
            Dim pstrUrlws As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsFCDDesembolso")
            Dim strUsoWS As String = GCCUtilitario.fstrObtieneKeyWebConfig("usoWS_Desembolso")
            Dim pstrCodTran As String = GCCUtilitario.fstrObtieneKeyWebConfig("COD_TRAN_FCDO")
            Dim argsUsuarioTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("USER_TLD")
            Dim argsAgenciaTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("AGENCIA_TLD")
            Dim flagError As Boolean = False
            Dim pstrExisteTCambio As String = ""
            Dim pstrMonedaDocumento As String = ""
            Dim strMonedaContrato As String = ""
            Dim pstrTipoCambio As String = ""
            Dim pstrImportePagar As String = ""
            Dim tx As New LInstruccionDesembolsoNTx
            Dim pago As New EGCC_InsDesembolsoPago
            With pago
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
                .Codagrupacion = "0"
                .Codproveedor = "0"
                .Codmonedaagrupacion = "0"
            End With
            Dim pEInsDesembolsoPago As String = GCCUtilitario.SerializeObject(Of EGCC_InsDesembolsoPago)(pago)
            Dim table As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(tx.ListadoInsDesembolsoMedioPago2(pEInsDesembolsoPago))
            If ((Not table Is Nothing) AndAlso (table.Rows.Count > 0)) Then
                Dim enumerator As IEnumerator
                Try
                    enumerator = table.Rows.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim flagAbono As Boolean
                        Dim strCodMonedaPago As String
                        Dim numMontoAbono As Decimal
                        Dim numMontoIGV As Decimal
                        Dim numMontoAbonoLog As Decimal
                        Dim numMontoAbonoSAdel As Decimal
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        Dim strCodMonedaDoc As String = current.Item("CodMonedaDocumento").ToString
                        Dim codMonedaAgrupacion As String = current.Item("CodMonedaAgrupacion").ToString
                        strMonedaContrato = current.Item("CodMonedaContrato").ToString
                        Dim numTCSunat As Decimal = GCCUtilitario.CheckDecimal(current.Item("TCSunat").ToString)
                        Dim nbrTCAgrp As Decimal = GCCUtilitario.CheckDecimal(current.Item("TC").ToString)
                        Dim nbrTCPrefVta As Decimal = GCCUtilitario.CheckDecimal(current.Item("TCPrefVtaDia").ToString)
                        Dim nbrTCPrefCmp As Decimal = GCCUtilitario.CheckDecimal(current.Item("TCPrefCmpDia").ToString)
                        Dim nbrMontoMonContrato As Decimal = GCCUtilitario.CheckDecimal(current.Item("ImporteMonContrato").ToString)
                        Dim nbrMontoAdelantoContrato As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoAdelantoMonContrato").ToString)
                        Dim nbrMontoPago As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoTotalPago").ToString)
                        Dim numMontoAdelanto As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoAdelanto").ToString)
                        Dim numIGVCargo As Decimal = GCCUtilitario.CheckDecimal(current.Item("ImporteIgv").ToString)
                        Dim strCodAgrupacion As String = current.Item("CodAgrupacion").ToString.Trim
                        Dim strCodTipoOperacion As String = current.Item("CodTipoOperacion").ToString
                        Dim blnMonedasCoiciden As Boolean = False
                        Dim strTipoDoc As String
                        Dim strNroDoc As String
                        'Inicio AAE - Chequeo que la agrupación NO tenga notas de credito
                        Dim blnTieneNotasCred As Boolean = False
                        Dim nbrMontoAbono As Decimal = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                        If strCodAgrupacion.Trim = "02" Then
                            blnTieneNotasCred = tx.TieneNotasCredito(pstrNroContrato, pstrNroInstruccion, strCodAgrupacion.Trim, GCCUtilitario.CheckInt(current.Item("CodCorrelativo")).ToString.Trim)
                        End If

                        If ((current.Item("CodMedioAbono").ToString = "002") Or (current.Item("CodMedioAbono").ToString = "006")) Then
                            strCodMonedaPago = current.Item("CodMonedaCuenta").ToString
                            If strMonedaContrato = strCodMonedaPago And strCodMonedaPago = codMonedaAgrupacion Then
                                blnMonedasCoiciden = True
                            Else
                                blnMonedasCoiciden = False
                            End If
                        Else
                            strCodMonedaPago = current.Item("CodMonedaPendiente").ToString
                        End If

                        If ((current.Item("CodMedioAbono").ToString = "002") Or (current.Item("CodMedioAbono").ToString = "006")) And blnMonedasCoiciden And (Not blnTieneNotasCred) And (nbrMontoAbono <> 0) Then
                            'Si no esta ejecutado
                            If ((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04")) Then
                                Dim strCU As String
                                Dim strMensajeWIO As String
                                Dim strDatosWIO As String
                                Dim flagTengoCU As Boolean = True
                                If ((strCodAgrupacion = "07") Or (strCodAgrupacion = "08") Or (strCodAgrupacion = "09") Or (strCodAgrupacion = "10") Or (strCodAgrupacion = "11") Or (strCodAgrupacion = "12") Or (strCodAgrupacion = "14")) Then
                                    strCU = current.Item("CUCliente").ToString
                                Else
                                    strCU = "0000000000"
                                    If (current.Item("CodMedioAbono").ToString = "002") Then
                                        strTipoDoc = current.Item("CodigoTipoDocumentoProv").ToString.Trim
                                        strNroDoc = current.Item("NumeroDocumentoProv").ToString.Trim
                                    Else
                                        strTipoDoc = current.Item("CodigoTipoDocumentoAgAduana").ToString.Trim
                                        strNroDoc = current.Item("CodigoNumeroDocumentoAgAduana").ToString.Trim
                                    End If
                                    Dim argsTrama As String = String.Concat(New String() {"20000", strCU, strTipoDoc, strNroDoc, "000"})
                                    If tx2.callProgramaHost(argsTrama, argsUsuarioTld, argsAgenciaTld, "WIOR002", "ObtenerCU", strMensajeWIO, strDatosWIO) Then
                                        strCU = Strings.Trim(Strings.Mid(strDatosWIO, &H4F, 10))
                                        If strCU.Length < 8 Then
                                            flagTengoCU = False
                                        End If
                                    Else
                                        flagTengoCU = False
                                    End If
                                End If '((strCodAgrupacion = "07") Or (str...
                                'Si es Cargo
                                If (strCodTipoOperacion = "002") Then
                                    flagAbono = False
                                    If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                        numMontoIGV = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numIGVCargo, flagAbono)
                                        numMontoAbono = Decimal.Add(InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono), numIGVCargo)
                                    Else
                                        numMontoIGV = numIGVCargo
                                        numMontoAbono = Decimal.Add(nbrMontoPago, numIGVCargo)
                                    End If
                                    numMontoAbonoLog = Decimal.Subtract(numMontoAbono, numMontoIGV)
                                Else ' (strCodTipoOperacion = "002") Then
                                    flagAbono = True
                                    If (strCodAgrupacion = "02") Then
                                        numMontoAbonoSAdel = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                                        If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                            numMontoAbono = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numMontoAbonoSAdel, flagAbono)
                                        Else
                                            numMontoAbono = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                                        End If
                                    End If
                                    If ((strCodAgrupacion = "01") Or (strCodAgrupacion = "03") Or (strCodAgrupacion = "04") Or (strCodAgrupacion = "06")) Then
                                        If ((codMonedaAgrupacion <> strCodMonedaPago) And (codMonedaAgrupacion = "002")) Then
                                            numMontoAbono = Decimal.Multiply(nbrMontoPago, numTCSunat)
                                        Else
                                            numMontoAbono = nbrMontoPago
                                        End If
                                    End If
                                    If ((strCodAgrupacion = "05") Or (strCodAgrupacion = "07") Or (strCodAgrupacion = "13")) Then
                                        If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                            numMontoAbono = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono)
                                        Else
                                            numMontoAbono = nbrMontoPago
                                        End If
                                    End If
                                    numMontoAbonoLog = numMontoAbono
                                End If ' (strCodTipoOperacion = "002") Then
                                If flagTengoCU Then
                                    Dim strCodRetorno As String
                                    Dim strResultado As String
                                    Dim strResCarga As String
                                    'Dim pstrNumeroInterno As String = ("00" & pstrNroInstruccion)
                                    Dim pstrNumeroInterno As String = ("00" & pstrNroContrato)
                                    Dim pintTipoCuenta As String = current.Item("Codtipocuenta").ToString
                                    Dim pintTipoMoneda As String = strCodMonedaPago
                                    Dim pstrNroOficina As String = current.Item("Numero_cuenta").ToString
                                    Dim pstrCodigoUnico As String = strCU
                                    Dim pstrNroCuenta As String = current.Item("Numero_cuenta").ToString
                                    Dim numAbono As Decimal = numMontoAbono
                                    pstrNroOficina = pstrNroOficina.Substring(0, 3)
                                    pstrNroCuenta = pstrNroCuenta.Substring(3, 10)
                                    pstrExisteTCambio = ""
                                    pstrMonedaDocumento = ""
                                    pstrTipoCambio = ""
                                    pstrImportePagar = ""
                                    Dim strTrama As String = InsDesembolso_frmInsDesembolsoRegistro.pArmaTramaDesembolso(pstrNumeroInterno, pintTipoCuenta, pintTipoMoneda, pstrNroOficina, pstrCodigoUnico, pstrNroCuenta, numMontoAbono, pstrExisteTCambio, pstrMonedaDocumento, pstrTipoCambio, pstrImportePagar, current.Item("CodAgrupacion").ToString, pstrCodTran, pstrTrama)
                                    If (strUsoWS = "SI") Then
                                        strResCarga = tx2.fstrConsultarDesembolso(strTrama, pstrUrlws, strCodRetorno, strResultado)
                                    Else
                                        If tx2.callProgramaHost(strTrama, argsUsuarioTld, argsAgenciaTld, "FCDO04", "EjecutarDesembolso", strMensajeWIO, strDatosWIO) Then
                                            If (strMensajeWIO.Substring(0, 2) = "00") Then
                                                strResCarga = "1|Se desembolso correctamente"
                                                strResultado = "Se desembolso correctamente"
                                                strCodRetorno = "00"
                                            Else
                                                strResCarga = ("0|" & strMensajeWIO)
                                                strCodRetorno = strDatosWIO.Substring(0, 2)
                                                strResultado = strMensajeWIO.Substring(0, &HFE)
                                            End If
                                        Else
                                            strDatosWIO = ("0|" & strMensajeWIO)
                                            strCodRetorno = strDatosWIO.Substring(0, 2)
                                            strResultado = strMensajeWIO.Substring(0, &HFE)
                                        End If 'tx2.callProgramaHost(strTrama, a....
                                    End If ' (strUsoWS = "SI") Then
                                    InsDesembolso_frmInsDesembolsoRegistro.LogEnvioDesembolso(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, pintTipoMoneda, current.Item("CodProveedor").ToString, pstrTrama, strCodRetorno, strResultado)
                                    Dim strArray As String() = strResCarga.Split(New Char() {"|"c})
                                    If (strArray(0).ToString <> "0") Then
                                        str = (str & strArray(1).ToString & Environment.NewLine)
                                        flagError = True
                                        InsDesembolso_frmInsDesembolsoRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "03", strCodMonedaPago, numMontoAbonoLog, numMontoIGV)
                                    Else
                                        InsDesembolso_frmInsDesembolsoRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "02", strCodMonedaPago, numMontoAbonoLog, numMontoIGV)
                                    End If
                                Else 'If flagTengoCU Then
                                    str = String.Concat(New String() {str, "- El proveedor ", current.Item("NumeroDocumentoProv").ToString, " No tiene CU, no es posible abonarle", Environment.NewLine})
                                    flagError = True
                                    InsDesembolso_frmInsDesembolsoRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "03", strCodMonedaPago, nbrMontoPago, numIGVCargo)
                                End If 'If flagTengoCU Then
                            End If '((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04")) 
                        Else 'If (current.Item("CodMedioAbono").ToString = "002") Then
                            If ((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02")) Then

                                'Si es Cargo
                                If (strCodTipoOperacion = "002") Then
                                    flagAbono = False
                                    If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                        numMontoIGV = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numIGVCargo, flagAbono)
                                        numMontoAbono = Decimal.Add(InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono), numIGVCargo)
                                    Else
                                        numMontoIGV = numIGVCargo
                                        numMontoAbono = Decimal.Add(nbrMontoPago, numIGVCargo)
                                    End If
                                    numMontoAbonoLog = Decimal.Subtract(numMontoAbono, numMontoIGV)
                                Else ' (strCodTipoOperacion = "002") Then
                                    flagAbono = True
                                    If (strCodAgrupacion = "02") Then
                                        numMontoAbonoSAdel = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                                        If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                            numMontoAbono = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numMontoAbonoSAdel, flagAbono)
                                        Else
                                            numMontoAbono = Decimal.Subtract(nbrMontoPago, numMontoAdelanto)
                                        End If
                                    End If
                                    If ((strCodAgrupacion = "01") Or (strCodAgrupacion = "03") Or (strCodAgrupacion = "04") Or (strCodAgrupacion = "06") Or (strCodAgrupacion = "15")) Then
                                        If ((codMonedaAgrupacion <> strCodMonedaPago) And (codMonedaAgrupacion = "002")) Then
                                            numMontoAbono = Decimal.Multiply(nbrMontoPago, numTCSunat)
                                        Else
                                            If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                                numMontoAbono = nbrMontoPago / numTCSunat
                                            Else
                                                numMontoAbono = nbrMontoPago
                                            End If
                                        End If
                                    End If
                                    If ((strCodAgrupacion = "05") Or (strCodAgrupacion = "07") Or (strCodAgrupacion = "13")) Then
                                        If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                            numMontoAbono = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagAbono)
                                        Else
                                            numMontoAbono = nbrMontoPago
                                        End If
                                    End If
                                    numMontoAbonoLog = numMontoAbono
                                End If ' (strCodTipoOperacion = "002") Then
                                InsDesembolso_frmInsDesembolsoRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "04", strCodMonedaPago, numMontoAbonoLog, numMontoIGV)
                            End If '((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02")) Then
                        End If ''If (current.Item("CodMedioAbono").ToString = "002") Then
                    Loop ' While enumerator.MoveNext
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            End If '((Not table Is Nothing) AndAlso (table.Rows.Count > 0)) Then
            strError = str
            If flagError Then
                num2 = -1
            Else
                num2 = 3
            End If
            num = num2
        Catch exception1 As Exception
            num2 = -2
            str = (str & exception1.ToString)
            strError = str
            num = num2
            Return num
        End Try
        Return num

        'Dim strResultado As String = ""

        'Try
        '    Dim oLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
        '    Dim strUrlws As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsFCDDesembolso")
        '    Dim strMensaje As String = "La Ejecución fue grabada correctamente."
        '    Dim blnErrorDesembolso As Boolean = False

        '    'Consulta Medios de Pago
        '    Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
        '    Dim objEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        '    Dim strEGCC_InsDesembolsoPago As String
        '    With objEGCC_InsDesembolsoPago
        '        .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
        '        .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
        '        .Codagrupacion = "0"
        '        .Codproveedor = "0"
        '        .Codmonedaagrupacion = "0"
        '    End With
        '    strEGCC_InsDesembolsoPago = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoPago)
        '    Dim dtMediosPago As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago(strEGCC_InsDesembolsoPago))

        '    If dtMediosPago IsNot Nothing Then
        '        If dtMediosPago.Rows.Count > 0 Then
        '            For Each dr As DataRow In dtMediosPago.Rows


        '                If dr("CodMedioAbono").ToString = "002" Then

        '                    Dim pstrNumeroInterno As String = "00" + pstrNroInstruccion
        '                    Dim pintTipoCuenta As String = dr("Codtipocuenta").ToString
        '                    Dim pintTipoMoneda As String = dr("Codmonedacuenta").ToString
        '                    Dim pstrNroOficina As String = dr("Numero_cuenta").ToString
        '                    Dim pstrCodigoUnico As String = dr("CodUnico").ToString
        '                    Dim pstrNroCuenta As String = dr("Numero_cuenta").ToString
        '                    Dim pdecMonto As Decimal = GCCUtilitario.CheckDecimal(dr("MontoPago").ToString)

        '                    pstrNroOficina = pstrNroOficina.Substring(0, 3)
        '                    pstrNroCuenta = pstrNroCuenta.Substring(3, 10)

        '                    Dim strExisteTCambio As String = ""
        '                    Dim strMonedaDocumento As String = ""
        '                    Dim strTipoCambio As String = ""
        '                    Dim strImportePagar As String = ""

        '                    'Dim strTramaDesembolsoMedio1 As String = pArmaTramaDesembolso(strCodigoOperacion, Me.ddlMedioPago.SelectedValue, Me.ddlTipoMoneda.SelectedValue, strOficina1, Me.hddCodigoUnicoMedio1.Value, strNumCta1, FCDUtilitario.CheckDecimal(Me.txtImporte.Text), strExisteTCambio1, strMonedaDocumento, strTipoCambio1, strImportePagar1)
        '                    Dim strTramaDesembolsoMedio As String = pArmaTramaDesembolso(pstrNumeroInterno, pintTipoCuenta, pintTipoMoneda, pstrNroOficina, pstrCodigoUnico, pstrNroCuenta, pdecMonto, strExisteTCambio, strMonedaDocumento, strTipoCambio, strImportePagar, dr("CodAgrupacion").ToString)


        '                    Dim strTramaDesembolso As String = oLInstruccionDesembolsoNTx.fstrConsultarDesembolso(strTramaDesembolsoMedio, strUrlws)

        '                    Dim strTrans As String() = strTramaDesembolso.Split("|")
        '                    If Not strTrans(0).ToString = "0" Then
        '                        strMensaje = strTrans(1).ToString
        '                        blnErrorDesembolso = True
        '                    End If
        '                End If

        '            Next
        '        End If
        '    End If

        '    If blnErrorDesembolso Then
        '        strResultado = "1|" + strMensaje
        '    Else
        '        strResultado = "0|La Ejecución de Desembolso se ejecutó correctamente"
        '    End If

        '    Return strResultado

        'Catch ex As Exception
        '    strResultado = "1|No se pudo realizar la Ejecución de Desembolso. (" + ex.Message + ")"
        '    Return strResultado
        'End Try

    End Function

    ''' <summary>
    ''' Arma Trama Pagos
    ''' </summary>
    ''' <remarks></remarks>
    ''' Inicio IBK - AAE - Se corrige Trama
    Protected Shared Function pArmaTramaDesembolso(ByVal pstrNumeroInterno As String, _
                                            ByVal pintTipoCuenta As String, _
                                            ByVal pintTipoMoneda As String, _
                                            ByVal pstrNroOficina As String, _
                                            ByVal pstrCodigoUnico As String, _
                                            ByVal pstrNroCuenta As String, _
                                            ByVal pdecMonto As Decimal, _
                                            ByVal pstrExisteTCambio As String, _
                                            ByVal pstrMonedaDocumento As String, _
                                            ByVal pstrTipoCambio As String, _
                                            ByVal pstrImportePagar As String, _
                                            ByVal pstrCodAgrupacion As String, _
                                            ByVal pstrCodTran As String, _
                                            ByRef pstrTrama As String) As String
        'StrGobal
        Dim strTrama As String = ""
        pstrTrama = ""

        Try
            'Variables
            Dim chrPad As Char = " "c
            Dim chrPadCero As Char = "0"c
            Dim strFechaActualDia As String = DateTime.Now.ToString("dd")
            Dim strFechaActualMes As String = DateTime.Now.ToString("MM")
            Dim strFechaActualAnio As String = DateTime.Now.ToString("yy")

            'Parametros Globales
            Dim strNumeroInterno As String = pstrNumeroInterno
            Dim strCodigoUnico As String = pstrCodigoUnico

            'Parametros Cargo
            Dim str55 As String = ""
            Dim strTipoCuentaCargo As String = ""
            Dim strTipoMonedaCargo As String = ""
            Dim strNroOficinaCargo As String = ""
            Dim strNroCuentaCargo As String = ""
            Dim strMontoCargo As String = ""
            'Dim strMontoCargo As Decimal = ""
            Dim strCategoriaCuentaCargo As String = ""

            'Parametros Abono
            Dim str54 As String = ""
            Dim strTipoCuentaAbono As String = ""
            Dim strTipoMonedaAbono As String = ""
            Dim strNroOficinaAbono As String = ""
            Dim strNroCuentaAbono As String = ""
            Dim strMontoAbono As String = ""
            'Dim strMontoAbono As Decimal = ""
            Dim strCategoriaCuentaAbono As String = ""

            Dim codigoTienda As String = ""

            'Valida si es CARGO o ABONO
            If pstrCodAgrupacion.Trim.Equals("08") Or _
                pstrCodAgrupacion.Trim.Equals("09") Or _
                pstrCodAgrupacion.Trim.Equals("10") Or _
                pstrCodAgrupacion.Trim.Equals("11") Or _
                pstrCodAgrupacion.Trim.Equals("12") Or _
                pstrCodAgrupacion.Trim.Equals("14") Then


                strNroOficinaCargo = pstrNroOficina
                strNroCuentaCargo = pstrNroCuenta
                str55 = "03"
                'Valida TipoCuenta
                If pintTipoCuenta = GCCConstante.C_TIPOCUENTA_CORRIENTE Then
                    strTipoCuentaCargo = GCCConstante.C_CODCTA_CORRIENTE
                    strCategoriaCuentaCargo = "0001"
                ElseIf pintTipoCuenta = GCCConstante.C_TIPOCUENTA_AHORROS Then
                    strTipoCuentaCargo = GCCConstante.C_CODCTA_AHORROS
                    strCategoriaCuentaCargo = "0002"
                End If

                'Valida TipoMoneda
                If pintTipoMoneda = GCCConstante.C_TIPOMONEDA_SOLES Then
                    strTipoMonedaCargo = GCCConstante.C_TX_MONEDA_SOLES
                ElseIf pintTipoMoneda = GCCConstante.C_TIPOMONEDA_DOLARES Then
                    strTipoMonedaCargo = GCCConstante.C_TX_MONEDA_DOLARES
                End If

                'Valida Monto
                'strMontoCargo = pdecMonto.ToString
                'strMontoCargo = Replace(strMontoCargo, ".", "")
                'strMontoCargo = Replace(strMontoCargo, ",", "")
                strMontoCargo = Strings.Replace(Strings.Replace(Convert.ToInt32(Math.Round(Decimal.Multiply(pdecMonto, 100))).ToString, ".", "", 1, -1, CompareMethod.Binary), ",", "", 1, -1, CompareMethod.Binary)

            ElseIf pstrCodAgrupacion.Trim.Equals("02") Or pstrCodAgrupacion.Trim.Equals("07") Then
                str54 = "03"
                strNroOficinaAbono = pstrNroOficina
                strNroCuentaAbono = pstrNroCuenta

                'Valida TipoCuenta
                If pintTipoCuenta = GCCConstante.C_TIPOCUENTA_CORRIENTE Then
                    strTipoCuentaAbono = GCCConstante.C_CODCTA_CORRIENTE
                    strCategoriaCuentaAbono = "0001"
                ElseIf pintTipoCuenta = GCCConstante.C_TIPOCUENTA_AHORROS Then
                    strTipoCuentaAbono = GCCConstante.C_CODCTA_AHORROS
                    strCategoriaCuentaAbono = "0002"
                End If

                'Valida TipoMoneda
                If pintTipoMoneda = GCCConstante.C_TIPOMONEDA_SOLES Then
                    strTipoMonedaAbono = GCCConstante.C_TX_MONEDA_SOLES
                ElseIf pintTipoMoneda = GCCConstante.C_TIPOMONEDA_DOLARES Then
                    strTipoMonedaAbono = GCCConstante.C_TX_MONEDA_DOLARES
                End If

                'Valida Monto
                'strMontoAbono = pdecMonto.ToString
                'strMontoAbono = Replace(strMontoAbono, ".", "")
                'strMontoAbono = Replace(strMontoAbono, ",", "")
                strMontoAbono = Strings.Replace(Strings.Replace(Convert.ToInt32(Math.Round(Decimal.Multiply(pdecMonto, 100))).ToString, ".", "", 1, -1, CompareMethod.Binary), ",", "", 1, -1, CompareMethod.Binary)

            End If


            'FCD-DATOS-LINK
            Dim FCD_CODRET As String = strPreparaDato("00", 2, chrPad)
            Dim FCD_COD_TRAN As String = strPreparaDato(pstrCodTran, 4, chrPad)
            Dim FCD_PROGRAMA As String = strPreparaDato("FCDO04", 8, chrPad)
            Dim FCD_USUARIO As String = strPreparaDato("USERFCD", 8, chrPad)
            strTrama = String.Concat(strTrama, FCD_CODRET, FCD_COD_TRAN, FCD_PROGRAMA, FCD_USUARIO)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_CODRET, "|", FCD_COD_TRAN, "|", FCD_PROGRAMA, "|", FCD_USUARIO, "|"})
            If (GCCSession.CodigoTienda.Trim = "") Then
                codigoTienda = "100"
            Else
                codigoTienda = GCCSession.CodigoTienda
            End If

            'INPUT GENERICO
            Dim FCD_COMM_CD_NU_DOC As String = strPreparaDato(strNumeroInterno, 10, chrPad)
            Dim FCD_COMM_CD_TRAN_CODE As String = strPreparaDato("", 3, chrPad)
            Dim FCD_COMM_CD_FECPRO_SS As String = strPreparaDato("21", 2, chrPad)
            Dim FCD_COMM_CD_FECPRO_YY As String = strPreparaDato(strFechaActualAnio, 2, chrPad)
            Dim FCD_COMM_CD_FECPRO_MM As String = strPreparaDato(strFechaActualMes, 2, chrPad)
            Dim FCD_COMM_CD_FECPRO_DD As String = strPreparaDato(strFechaActualDia, 2, chrPad)
            Dim FCD_COMM_CD_REG_EMPLEADO As String = strPreparaDatoPadRight(GCCSession.CodigoUsuario, 8, chrPad)
            Dim FCD_COMM_CD_TIENDA_ORIGEN As String = strPreparaDato(codigoTienda, 3, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_NU_DOC, FCD_COMM_CD_TRAN_CODE, FCD_COMM_CD_FECPRO_SS, FCD_COMM_CD_FECPRO_YY, FCD_COMM_CD_FECPRO_MM, FCD_COMM_CD_FECPRO_DD, FCD_COMM_CD_REG_EMPLEADO, FCD_COMM_CD_TIENDA_ORIGEN)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_NU_DOC, "|", FCD_COMM_CD_TRAN_CODE, "|", FCD_COMM_CD_FECPRO_SS, "|", FCD_COMM_CD_FECPRO_YY, "|", FCD_COMM_CD_FECPRO_MM, "|", FCD_COMM_CD_FECPRO_DD, "|", FCD_COMM_CD_REG_EMPLEADO, "|", FCD_COMM_CD_TIENDA_ORIGEN, "|"})
            'INPUT PARA ABONO

            Dim FCD_COMM_CD_TIP_CTA_CR As String = strPreparaDato(strTipoCuentaAbono, 2, chrPad)
            Dim FCD_COMM_CD_CR_CTL1 As String = strPreparaDato(str54, 2, chrPad)
            Dim FCD_COMM_CD_CR_CTL2 As String = strPreparaDato(strTipoMonedaAbono, 3, chrPad)
            Dim FCD_COMM_CD_CR_CTL3 As String = strPreparaDato(strNroOficinaAbono, 3, chrPad)
            Dim FCD_COMM_CD_CR_CTL4 As String = strPreparaDato(strCategoriaCuentaAbono, 4, chrPad)
            Dim FCD_COMM_CD_CR_ACCT As String = strPreparaDato(strNroCuentaAbono, 10, chrPad)
            Dim FCD_COMM_CD_CR_FILL As String = strPreparaDato("", 3, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_TIP_CTA_CR, FCD_COMM_CD_CR_CTL1, FCD_COMM_CD_CR_CTL2, FCD_COMM_CD_CR_CTL3, FCD_COMM_CD_CR_CTL4, FCD_COMM_CD_CR_ACCT, FCD_COMM_CD_CR_FILL)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_TIP_CTA_CR, "|", FCD_COMM_CD_CR_CTL1, "|", FCD_COMM_CD_CR_CTL2, "|", FCD_COMM_CD_CR_CTL3, "|", FCD_COMM_CD_CR_CTL4, "|", FCD_COMM_CD_CR_ACCT, "|", FCD_COMM_CD_CR_FILL, "|"})

            'INPUT PARA DEBITO (CARGO)
            Dim FCD_COMM_CD_TIP_CTA_DB As String = strPreparaDato(strTipoCuentaCargo, 2, chrPad)
            Dim FCD_COMM_CD_DB_CTL1 As String = strPreparaDato(str55, 2, chrPad)
            Dim FCD_COMM_CD_DB_CTL2 As String = strPreparaDato(strTipoMonedaCargo, 3, chrPad)
            Dim FCD_COMM_CD_DB_CTL3 As String = strPreparaDato(strNroOficinaCargo, 3, chrPad)
            Dim FCD_COMM_CD_DB_CTL4 As String = strPreparaDato(strCategoriaCuentaCargo, 4, chrPad)
            Dim FCD_COMM_CD_DB_ACCT As String = strPreparaDato(strNroCuentaCargo, 10, chrPad)
            Dim FCD_COMM_CD_DB_FILL As String = strPreparaDato("", 3, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_TIP_CTA_DB, FCD_COMM_CD_DB_CTL1, FCD_COMM_CD_DB_CTL2, FCD_COMM_CD_DB_CTL3, FCD_COMM_CD_DB_CTL4, FCD_COMM_CD_DB_ACCT, FCD_COMM_CD_DB_FILL)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_TIP_CTA_DB, "|", FCD_COMM_CD_DB_CTL1, "|", FCD_COMM_CD_DB_CTL2, "|", FCD_COMM_CD_DB_CTL3, "|", FCD_COMM_CD_DB_CTL4, "|", FCD_COMM_CD_DB_ACCT, "|", FCD_COMM_CD_DB_FILL, "|"})

            'INPUT GENERICO
            Dim FCD_COMM_CD_EXTORNO As String = strPreparaDato("N", 1, chrPad)
            Dim FCD_COMM_CD_SHORT_DESC As String = strPreparaDatoPadRight("PR  CARG CPR", 15, chrPad)
            Dim FCD_COMM_CD_AMOUNT_CR As String = strPreparaDato(strMontoAbono, 15, chrPadCero) 'ABONO
            Dim FCD_COMM_CD_AMOUNT_DB As String = strPreparaDato(strMontoCargo, 15, chrPadCero) 'CARGO
            Dim FCD_COMM_CD_COBRO_FORZOSO As String = strPreparaDato("N", 1, chrPad)
            Dim FCD_COMM_CD_COBRO_PARCIAL As String = strPreparaDato("N", 1, chrPad)
            Dim FCD_COMM_CD_CODUNI As String = strPreparaDato(strCodigoUnico, 10, chrPad)

            Dim FCD_COMM_CTA_FLG_OC As String = strPreparaDato(pstrExisteTCambio, 1, chrPad)
            Dim FCD_COMM_CTA_MON_CF As String = strPreparaDato(pstrMonedaDocumento, 3, chrPad)
            Dim FCD_COMM_CTA_CLA_TC As String = strPreparaDato("", 2, chrPad)
            Dim FCD_COMM_CTA_TC_CF As String = strPreparaDato(pstrTipoCambio, 15, chrPad)
            Dim FCD_COMM_CTA_IMP_EQUIV As String = strPreparaDato(pstrImportePagar, 15, chrPad)

            Dim FCD_COMM_CD_FILLER As String = strPreparaDato("", 520, chrPad) 'strPreparaDato("", 561, chrPad)
            strTrama = String.Concat(strTrama, FCD_COMM_CD_EXTORNO, FCD_COMM_CD_SHORT_DESC, FCD_COMM_CD_AMOUNT_CR, FCD_COMM_CD_AMOUNT_DB, FCD_COMM_CD_COBRO_FORZOSO, FCD_COMM_CD_COBRO_PARCIAL, FCD_COMM_CD_CODUNI, FCD_COMM_CTA_FLG_OC, FCD_COMM_CTA_MON_CF, FCD_COMM_CTA_CLA_TC, FCD_COMM_CTA_TC_CF, FCD_COMM_CTA_IMP_EQUIV, FCD_COMM_CD_FILLER)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COMM_CD_EXTORNO, "|", FCD_COMM_CD_SHORT_DESC, "|", FCD_COMM_CD_AMOUNT_CR, "|", FCD_COMM_CD_AMOUNT_DB, "|", FCD_COMM_CD_COBRO_FORZOSO, "|", FCD_COMM_CD_COBRO_PARCIAL, "|", FCD_COMM_CD_CODUNI, "|", FCD_COMM_CTA_FLG_OC, "|", FCD_COMM_CTA_MON_CF, "|", FCD_COMM_CTA_CLA_TC, "|", FCD_COMM_CTA_TC_CF, "|", FCD_COMM_CTA_IMP_EQUIV, "|", FCD_COMM_CD_FILLER, "|"})
            'COMMAREA
            Dim FCD_LENGTH_COMMAREA As String = strPreparaDato("00920", 5, chrPad)
            strTrama = String.Concat(strTrama, FCD_LENGTH_COMMAREA)
            pstrTrama = (pstrTrama & FCD_LENGTH_COMMAREA & "|")
            'INPUT DE ERROR
            Dim FCD_COD_RET_TOLD As String = strPreparaDato("", 2, chrPad)
            Dim FCD_COD_RET_O As String = strPreparaDato("", 2, chrPad)
            Dim FCD_MSG_ERROR As String = strPreparaDato("", 40, chrPad)
            'INPUT SALIDA DE DATOS
            Dim FCD_FC04_NRO_DOCUMENTO As String = strPreparaDato("", 10, chrPad)
            Dim FCD_FC04_IMPORTE_DESEMB As String = strPreparaDato("", 15, chrPad)
            Dim FCD_FC04_FLG_EXTORNO As String = strPreparaDato("", 1, chrPad)
            Dim FILLER As String = strPreparaDato("", 198, chrPad)
            strTrama = String.Concat(strTrama, FCD_COD_RET_TOLD, FCD_COD_RET_O, FCD_MSG_ERROR, FCD_FC04_NRO_DOCUMENTO, FCD_FC04_IMPORTE_DESEMB, FCD_FC04_FLG_EXTORNO, FILLER)
            pstrTrama = String.Concat(New String() {pstrTrama, FCD_COD_RET_TOLD, "|", FCD_COD_RET_O, "|", FCD_MSG_ERROR, "|", FCD_FC04_NRO_DOCUMENTO, "|", FCD_FC04_IMPORTE_DESEMB, "|", FCD_FC04_FLG_EXTORNO, "|", FILLER})

            Return strTrama

        Catch ex As Exception
            Dim intTamanioTrama As Integer = strTrama.Length
            strTrama = String.Concat(strTrama, strPreparaDato("", (1001 - intTamanioTrama), " "c))
            Return strTrama
        End Try

    End Function

    ''' <summary>
    ''' Prepara Dato para tramas
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Function strPreparaDato(ByVal pstrDato As String, ByVal pintTamano As Integer, ByVal pchrCompletar As Char) As String
        Dim strDato As String = ""

        If pstrDato Is Nothing Then
            pstrDato = ""
        End If
        pstrDato = Mid(pstrDato, 1, pintTamano)
        strDato = pstrDato.PadLeft(pintTamano, pchrCompletar)

        Return strDato
    End Function

    Private Shared Function strPreparaDatoPadRight(ByVal pstrDato As String, ByVal pintTamano As Integer, ByVal pchrCompletar As Char) As String
        Dim strDato As String = ""

        If pstrDato Is Nothing Then
            pstrDato = ""
        End If
        pstrDato = Mid(pstrDato, 1, pintTamano)
        strDato = pstrDato.PadRight(pintTamano, pchrCompletar)

        Return strDato
    End Function

    'Inicio IBK - AAE - Método Reintentar ejecutar Desembolso
    <WebMethod()> _
    Public Shared Function ReintentoEjecutaInsDesembolso(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrEnvioLPC As String) As String
        Dim pstrReintentar As String = "1"
        Return InsDesembolso_frmInsDesembolsoRegistro.EjecutaInsDesembolso(pstrNroContrato, pstrNroInstruccion, pstrEnvioLPC, pstrReintentar)
    End Function

    <WebMethod()> _
    Public Shared Function EjecutaAdministrativo(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrEnvioLPC As String) As String
        Dim strRetorno As String
        Try
            Dim strResultado As String
            Dim numRetLPC As Integer
            Dim strEstadoEnvioLPC As String = "0"
            Dim strFlagLPC As String = "1"
            Dim tx2 As New LInstruccionDesembolsoNTx
            Dim tx As New LInstruccionDesembolsoNTx
            Dim pago As New EGCC_InsDesembolsoPago
            Dim strRetFinWIO As String
            With pago
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
                .Codagrupacion = "0"
                .Codproveedor = "0"
                .Codmonedaagrupacion = "0"
            End With
            Dim pEInsDesembolsoPago As String = GCCUtilitario.SerializeObject(Of EGCC_InsDesembolsoPago)(pago)
            Dim table As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(tx.ListadoInsDesembolsoMedioPago2(pEInsDesembolsoPago))
            If ((Not table Is Nothing) AndAlso (table.Rows.Count > 0)) Then
                Dim enumerator As IEnumerator
                Try
                    enumerator = table.Rows.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        ' si no esta en Ejecutado o Administrativo
                        If ((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02") And (current.Item("CodEstadoEjecucionPago").ToString.Trim <> "04")) Then
                            Dim flagEsAbono As Boolean
                            Dim strCodMonedaPago As String
                            Dim numMonto As Decimal
                            Dim numIGVCargo As Decimal
                            Dim numMontoPagoLog As Decimal
                            Dim strMonedaDocumento As String = current.Item("CodMonedaDocumento").ToString
                            Dim codMonedaAgrupacion As String = current.Item("CodMonedaAgrupacion").ToString
                            Dim nbrTCSunat As Decimal = GCCUtilitario.CheckDecimal(current.Item("TCSunat").ToString)
                            Dim nbrTCAgrp As Decimal = GCCUtilitario.CheckDecimal(current.Item("TC").ToString)
                            Dim nbrTCPrefVta As Decimal = GCCUtilitario.CheckDecimal(current.Item("TCPrefVtaDia").ToString)
                            Dim nbrTCPrefCmp As Decimal = GCCUtilitario.CheckDecimal(current.Item("TCPrefCmpDia").ToString)
                            Dim nbrMontoPago As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoTotalPago").ToString)
                            Dim nbrMontoAdelanto As Decimal = GCCUtilitario.CheckDecimal(current.Item("MontoAdelanto").ToString)
                            Dim nbrImporteIGVCargo As Decimal = GCCUtilitario.CheckDecimal(current.Item("ImporteIgv").ToString)
                            Dim strCodAgrupacion As String = current.Item("CodAgrupacion").ToString.Trim
                            Dim strTIpoOper As String = current.Item("CodTipoOperacion").ToString
                            If (current.Item("CodMedioAbono").ToString = "002") Then
                                strCodMonedaPago = current.Item("CodMonedaCuenta").ToString
                            Else
                                strCodMonedaPago = current.Item("CodMonedaPendiente").ToString
                            End If
                            'Si el tipo de operación es un cargo
                            If (strTIpoOper = "002") Then
                                flagEsAbono = False
                                If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                    numIGVCargo = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrImporteIGVCargo, flagEsAbono)
                                    numMonto = Decimal.Add(InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagEsAbono), numIGVCargo)
                                Else
                                    numIGVCargo = nbrImporteIGVCargo
                                    numMonto = Decimal.Add(nbrMontoPago, nbrImporteIGVCargo)
                                End If '(codMonedaAgrupacion <> strCodMonedaPago) Then
                                numMontoPagoLog = Decimal.Subtract(numMonto, numIGVCargo)
                            Else '(strTIpoOper = "002") Then
                                ' si es un abono
                                flagEsAbono = True
                                '(strCodAgrupacion = "02") Then
                                If (strCodAgrupacion = "02") Then
                                    Dim numMontoPagoCta As Decimal = Decimal.Subtract(nbrMontoPago, nbrMontoAdelanto)
                                    If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                        numMonto = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, numMontoPagoCta, flagEsAbono)
                                    Else
                                        numMonto = Decimal.Subtract(nbrMontoPago, nbrMontoAdelanto)
                                    End If
                                End If '(strCodAgrupacion = "02") Then
                                If ((strCodAgrupacion = "01") Or (strCodAgrupacion = "03") Or (strCodAgrupacion = "04") Or (strCodAgrupacion = "06") Or (strCodAgrupacion = "15")) Then
                                    If ((codMonedaAgrupacion <> strCodMonedaPago) And (codMonedaAgrupacion = "002")) Then
                                        numMonto = Decimal.Multiply(nbrMontoPago, nbrTCSunat)
                                    Else
                                        If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                            numMonto = nbrMontoPago / nbrTCAgrp
                                        Else
                                            numMonto = nbrMontoPago
                                        End If 'numMonto = nbrMontoPago
                                    End If '((codMonedaAgrupacion <> strCodMonedaPago) And (codMonedaAgrupacion = "002"))

                                End If '((((strCodAgrupacion = "01") Or (strCodAgrupacion = "03")....
                                If ((strCodAgrupacion = "05") Or (strCodAgrupacion = "13") Or (strCodAgrupacion = "07")) Then
                                    If (codMonedaAgrupacion <> strCodMonedaPago) Then
                                        numMonto = InsDesembolso_frmInsDesembolsoRegistro.tipoCambio(codMonedaAgrupacion, strCodMonedaPago, nbrTCAgrp, nbrTCPrefVta, nbrTCPrefCmp, nbrMontoPago, flagEsAbono)
                                    Else
                                        numMonto = nbrMontoPago
                                    End If
                                End If '((((strCodAgrupacion = "05") Or (strCodAgrupacion = "13")
                                numMontoPagoLog = numMonto
                            End If '(strTIpoOper = "002") Then
                            InsDesembolso_frmInsDesembolsoRegistro.actualizarEstadoEjecucionPago(pstrNroContrato, pstrNroInstruccion, current.Item("CodAgrupacion").ToString, current.Item("CodMonedaAgrupacion").ToString, current.Item("CodProveedor").ToString, "04", strCodMonedaPago, numMontoPagoLog, numIGVCargo)
                        End If '((current.Item("CodEstadoEjecucionPago").ToString.Trim <> "02") And 
                    Loop 'While
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            End If '((Not table Is Nothing) AndAlso 
            'Envio a LPC
            strEstadoEnvioLPC = "1"
            If (pstrEnvioLPC <> "1") Then
                numRetLPC = InsDesembolso_frmInsDesembolsoRegistro.EjecutarDesembolsoLPC(strFlagLPC, pstrNroInstruccion, GCCSession.CodigoUsuario)
            Else
                numRetLPC = 0
            End If
            If (numRetLPC <> 0) Then
                strEstadoEnvioLPC = "0"
                InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "007", strEstadoEnvioLPC)
                strResultado = "1|Error en LPC"
            Else
                strEstadoEnvioLPC = "1"
                InsDesembolso_frmInsDesembolsoRegistro.ActualizaInsDesembolsoEstado(pstrNroContrato, pstrNroInstruccion, "008", strEstadoEnvioLPC)
                'Finalizo WIO
                strRetFinWIO = FinalizaWIO(pstrNroContrato, pstrNroInstruccion)
                Dim strRetWio As String() = strRetFinWIO.Split("|")
                If strRetWio(0) = "0" Then
                    strResultado = "0|Desembolso Ejecutado Corretamente"
                Else
                    strResultado = strRetFinWIO
                End If
                'strResultado = "0|Desembolso Ejecutado Corretamente"
            End If
            strRetorno = strResultado
        Catch exception1 As Exception
            strRetorno = ("1|No se pudo finalizar la Ejecución de Desembolso. (" & exception1.ToString & ")")
            Return strRetorno
        End Try
        Return strRetorno
    End Function

    <WebMethod()> _
   Public Shared Function Anular(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String) As String
        Dim strRetorno As String
        Dim tx As New LInstruccionDesembolsoTx
        Dim pago As New EGCC_InsDesembolso
        Try

            With pago
                .Codsolicitudcredito = pstrNroContrato
                .Codinstrucciondesembolso = pstrNroInstruccion
            End With
            Dim strObj As String = GCCUtilitario.SerializeObject(Of EGCC_InsDesembolso)(pago)
            strRetorno = tx.AnularInstDesembolso(strObj)
            If strRetorno = "0" Then
                strRetorno = strRetorno + "|Anulación Correcta"
            Else
                strRetorno = strRetorno + "|Falló la Anulación"
            End If
            Return strRetorno
        Catch ex As Exception
            Return "1|" + ex.ToString()
        End Try
    End Function

    Private Shared Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer
        Dim numRet As Integer
        Try
            Dim tx As New LInstruccionDesembolsoTx
            Try
                numRet = tx.EjecutarDesembolsoLPC(pFlag, pCodInstDesembolso, pRegUsuario)
            Catch exception1 As SqlException
                Throw New Exception(exception1.ToString)
            End Try
        Catch exception3 As Exception
            Throw exception3
        End Try
        Return numRet
    End Function

    Private Shared Function actualizarEstadoEjecucionPago(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCodAgrupacion As String, ByVal pstrTipoMoneda As String, ByVal pstrCodProveedor As String, ByVal strEstadoEjecucion As String, ByVal strCodMonedaPago As String, ByVal decMonto As Decimal, ByVal decMontoIGVCargo As Decimal) As Boolean
        Dim blnRet As Boolean
        Try
            Dim tx As New LInstruccionDesembolsoTx
            Dim pago As New EGCC_InsDesembolsoPago
            With pago
                .Codsolicitudcredito = pstrNroContrato
                .Codinstrucciondesembolso = pstrNroInstruccion
                .Codmonedaagrupacion = pstrTipoMoneda
                .Codagrupacion = pstrCodAgrupacion
                .Codproveedor = pstrCodProveedor
                .CodEstadoEjecucionPago = strEstadoEjecucion
                .CodMonedaCargoAbono = strCodMonedaPago
                .MontoCargoAbono = decMonto
                .MontoIGVCargo = decMontoIGVCargo
            End With
            Dim strObj As String = GCCUtilitario.SerializeObject(Of EGCC_InsDesembolsoPago)(pago)
            blnRet = tx.actualizarEstadoEjecucionPago(strObj)
        Catch exception1 As Exception
            Throw exception1
        End Try
        Return blnRet
    End Function

    Private Shared Function tipoCambio(ByVal CodMonedaAgrupacion As String, ByVal CodMonedaMedioPago As String, ByVal nbrTCAgrp As Decimal, ByVal nbrTCPrefVta As Decimal, ByVal nbrTCPrefCmp As Decimal, ByVal nbrMontoPago As Decimal, ByVal blnEsAbono As Boolean) As Decimal
        Dim numTCCmp As Decimal
        Dim numTCVta As Decimal
        Dim num2 As New Decimal
        Dim decRet As New Decimal
        If blnEsAbono Then
            numTCVta = nbrTCPrefCmp
            numTCCmp = nbrTCPrefVta
        Else
            numTCVta = nbrTCPrefVta
            numTCCmp = nbrTCPrefCmp
        End If
        If (CodMonedaAgrupacion = "002") Then
            If (Decimal.Compare(nbrTCAgrp, Decimal.Zero) = 0) Then
                decRet = Decimal.Multiply(nbrMontoPago, numTCVta)
            Else
                decRet = Decimal.Multiply(nbrMontoPago, nbrTCAgrp)
            End If
        ElseIf (Decimal.Compare(nbrTCAgrp, Decimal.Zero) = 0) Then
            decRet = Decimal.Divide(nbrMontoPago, numTCCmp)
        Else
            decRet = Decimal.Divide(nbrMontoPago, nbrTCAgrp)
        End If
        Return Math.Round(decRet, 2)
    End Function

    Private Shared Function saldoCuenta(ByVal pstrCU As String, ByVal pstrCodTipoCta As String, ByVal pstrCodMoneda As String, ByVal pstrCodTienda As String, ByVal pstrNroCta As String, ByRef strRta As String) As Double
        Dim num As Double
        Try
            Dim strDatos As String
            Dim strMensaje As String
            Dim strCodMoneda As String
            Dim strCta As String
            Dim strTipoCta As String
            Dim tx As New LInstruccionDesembolsoNTx
            Dim argsUsuarioTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("USER_TLD")
            Dim argsAgenciaTld As String = GCCUtilitario.fstrObtieneKeyWebConfig("AGENCIA_TLD")
            Dim numSaldoRet As Double = 0
            If (pstrCodTipoCta = "ST") Then
                strTipoCta = "2"
                strCta = ("0000" & pstrNroCta)
            Else
                strTipoCta = "1"
                strCta = pstrNroCta
            End If
            If (pstrCodMoneda = "002") Then
                strCodMoneda = "010"
            Else
                strCodMoneda = "001"
            End If
            Dim strCU As String
            If pstrCU.Length = 8 Then
                strCU = "00" + pstrCU
            Else
                strCU = pstrCU
            End If
            If tx.callProgramaHost((strCU & strTipoCta), argsUsuarioTld, argsAgenciaTld, "LPCO015", "SaldoCuenta", strMensaje, strDatos) Then
                Dim nbrCantCtas As Integer = Convert.ToInt16(strDatos.Substring(13, 1))
                Dim startIndex As Integer = 14
                Dim num8 As Integer = (nbrCantCtas - 1)
                Dim i As Integer = 0
                Do While (i <= num8)
                    Dim saldo As Double
                    Dim situacion As String
                    Dim tipoCuenta As String
                    Dim strMoneda As String = strDatos.Substring(startIndex, 3)
                    startIndex = (startIndex + 3)
                    Dim strTienda As String = strDatos.Substring(startIndex, 3)
                    startIndex = (startIndex + 3)
                    Dim NroCta As String = strDatos.Substring(startIndex, &H16)
                    startIndex = (startIndex + &H16)
                    If (strDatos.Substring((startIndex + 1), 15).Trim.Length > 0) Then
                        Dim nbr_saldo As Int64 = Convert.ToInt64(strDatos.Substring((startIndex + 1), 15))
                        If (strDatos.Substring(startIndex, 1) = "+") Then
                            saldo = (CDbl(nbr_saldo) / 100)
                        Else
                            saldo = (-1 * (CDbl(nbr_saldo) / 100))
                        End If
                    Else
                        saldo = 0
                    End If
                    startIndex = (startIndex + &H10)
                    If (i = (nbrCantCtas - 1)) Then
                        If ((startIndex + 30) > strDatos.Length) Then
                            tipoCuenta = strDatos.Substring(startIndex, (strDatos.Length - startIndex))
                            situacion = ""
                        Else
                            tipoCuenta = strDatos.Substring(startIndex, 30)
                            startIndex = (startIndex + 30)
                            If ((startIndex + 30) > strDatos.Length) Then
                                situacion = strDatos.Substring(startIndex, (strDatos.Length - startIndex))
                            Else
                                situacion = strDatos.Substring(startIndex, 30)
                            End If
                        End If
                    Else
                        tipoCuenta = strDatos.Substring(startIndex, 30)
                        startIndex = (startIndex + 30)
                        situacion = strDatos.Substring(startIndex, 30)
                        startIndex = (startIndex + 30)
                    End If
                    If (((strMoneda = strCodMoneda) And (strTienda = pstrCodTienda)) And (NroCta.Trim = strCta.Trim)) Then
                        numSaldoRet = saldo
                    End If
                    i += 1
                Loop
                strRta = ""
                Return numSaldoRet
            End If
            strRta = strMensaje
            num = 0
        Catch exception1 As Exception
            Throw exception1
        End Try
        Return num
    End Function

    Private Shared Function LogEnvioDesembolso(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCodAgrupacion As String, ByVal pstrTipoMoneda As String, ByVal pstrCodProveedor As String, ByVal strTramaArray As String, ByVal strCodRetorno As String, ByVal strResultado As String) As Boolean
        Dim flag As Boolean
        Dim format As String = "yyyy-MM-dd HH:mm:ss.fff"
        Try
            Dim strArray As String() = strTramaArray.Split(New Char() {"|"c})
            Dim tx As New LInstruccionDesembolsoTx
            Dim ejecucion As New EGCC_LogDesembolsoPagoEjecucion
            With ejecucion
                .CodSolicitudCredito = pstrNroContrato
                .CodInstruccionDesembolso = pstrNroInstruccion
                .CodAgrupacion = pstrCodAgrupacion
                .CodMonedaAgrupacion = pstrTipoMoneda
                .CodProveedor = pstrCodProveedor
                .FechaHora = DateTime.Now.ToString(format)
                .AudFechaRegistro = DateTime.Now.ToString(format)
                .AudFechaModificacion = DateTime.Now.ToString(format)
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
                .AudUsuarioModificacion = GCCSession.CodigoUsuario
                .FCDCODRET = strArray(0)
                .FCDCODTRAN = strArray(1)
                .FCDPROGRAMA = strArray(2)
                .FCDUSUARIO = strArray(3)
                .FCDNUDOC = strArray(4)
                .FCDTRANCODE = strArray(5)
                .FCDFECPROSS = strArray(6)
                .FCDFECPROYY = strArray(7)
                .FCDFECPROMM = strArray(8)
                .FCDFECPRODD = strArray(9)
                .FCDREGEMPLEADO = strArray(10)
                .FCDTIENDAORIGEN = strArray(11)
                .FCDTIPCTACR = strArray(12)
                .FCDCRCTL1 = strArray(13)
                .FCDCRCTL2 = strArray(14)
                .FCDCRCTL3 = strArray(15)
                .FCDCRCTL4 = strArray(&H10)
                .FCDCRACCT = strArray(&H11)
                .FCDCRFILL = strArray(&H12)
                .FCDTIPCTADB = strArray(&H13)
                .FCDDBCTL1 = strArray(20)
                .FCDDBCTL2 = strArray(&H15)
                .FCDDBCTL3 = strArray(&H16)
                .FCDDBCTL4 = strArray(&H17)
                .FCDDBACCT = strArray(&H18)
                .FCDDBFILL = strArray(&H19)
                .FCDEXTORNO = strArray(&H1A)
                .FCDSHORTDESC = strArray(&H1B)
                .FCDAMOUNTCR = strArray(&H1C)
                .FCDAMOUNTDB = strArray(&H1D)
                .FCDCOBROFORZOSO = strArray(30)
                .FCDCOBROPARCIAL = strArray(&H1F)
                .FCDCODUNI = strArray(&H20)
                .FCDCTAFLGOC = strArray(&H21)
                .FCDCTAMONCF = strArray(&H22)
                .FCDCTACLATC = strArray(&H23)
                .FCDCTATCCF = strArray(&H24)
                .FCDCTAIMPEQUIV = strArray(&H25)
                .FILLERINP = strArray(&H26)
                .FCDLENGTHCOMMAREA = strArray(&H27)
                .FCDCODRETTOLD = strArray(40)
                .FCDCODRETO = strArray(&H29)
                .FCDMSGERROR = strArray(&H2A)
                .FCDFC04NRODOCUMENTO = strArray(&H2B)
                .FCDFC04IMPORTEDESEMB = strArray(&H2C)
                .FCDFC04FLGEXTORNO = strArray(&H2D)
                .FILLEROUT = strArray(&H2E)
                .CodRetorno = strCodRetorno
                .Resultado = strResultado
            End With
            Dim str2 As String = GCCUtilitario.SerializeObject(Of EGCC_LogDesembolsoPagoEjecucion)(ejecucion)
            flag = tx.LogEnvioDesembolso(str2)
        Catch exception1 As Exception
            Throw exception1
        End Try
        Return flag
    End Function



    'Fin IBK
#End Region

#Region "WebService"
    'Inicio IBK - AAE - Mejoro consulta de WIO
    Public Sub ConsultaEstadoWIO(ByVal strNroContrato As String, ByVal strCodInsDesembolso As String, ByVal strEstadoID As String)

        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
        Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
        Dim pstrURL As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIO")
        Dim pstrProd As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIOArgProducto")
        Dim pstrPaso As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIOArgPaso")

        Dim strUsoFlujoWIO As String = GCCUtilitario.fstrObtieneKeyWebConfig("FlujoWIO")
        Dim strTipoProdFin As String 'Leasing o LeaseBack
        Dim strTipoProdFinPas As String
        Dim strProdWIO As String
        Dim pstrProdLB As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIOArgProductoLeaseBack")
        Dim strWIO As String = ""
        Dim FlagLPC As String = "0"
        Dim blnActualice As Boolean = False

        Try
            strEstadoID = GCCUtilitario.CheckStr(strEstadoID)
            If (strEstadoID.Trim.Equals(GCCConstante.C_ESTADO_INSDESEMBOLSO_WIO) Or strEstadoID.Trim.Equals(GCCConstante.C_ESTADO_INSDESEMBOLSO_PENDEJECUCION)) Then

                Dim table As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.obtenerWIO(strNroContrato, strCodInsDesembolso))
                If (table.Rows.Count > 0) Then
                    strWIO = table.Rows.Item(0).Item("NroInstruccionWIO").ToString
                    strTipoProdFin = table.Rows.Item(0).Item("CodProductoFinancieroActivo").ToString
                    strTipoProdFinPas = table.Rows.Item(0).Item("CodProductoFinancieroPasivo").ToString
                End If
                If strTipoProdFin = GCCConstante.C_CODLPC_PROD_LEASEBACK And strTipoProdFinPas = GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS Then
                    strProdWIO = pstrProdLB
                Else
                    strProdWIO = pstrProd
                End If
                'Dim blnActualizaEstado As String = objLInstruccionDesembolsoNTx.ConsultaEstadoWIO(strNroContrato, strCodInsDesembolso)
                If strUsoFlujoWIO <> "SI" Or objLInstruccionDesembolsoNTx.ConsultaEstadoWIO(strNroContrato, strCodInsDesembolso, pstrURL, strWIO, strProdWIO, pstrPaso) Then
                    ActualizaInsDesembolsoEstado(strNroContrato, strCodInsDesembolso, GCCConstante.C_ESTADO_INSDESEMBOLSO_PENDEJECUCION, FlagLPC)
                    blnActualice = True
                End If
                'Actualiza estado a Pendiente de Ejecucion
                'If blnActualizaEstado Then
                'ActualizaInsDesembolsoEstado(strNroContrato, strCodInsDesembolso, GCCConstante.C_ESTADO_INSDESEMBOLSO_PENDEJECUCION)
                'End If
                'Chequeo anulación de un WIO
                If (blnActualice = False) Then
                    If objLInstruccionDesembolsoNTx.ConsultaAnulacionWIO(pstrURL, strWIO) Then
                        objLInstruccionDesembolsoTx.LiberarInstDesembolso(strNroContrato, strCodInsDesembolso)
                    End If
                End If
            End If




        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function FinalizaWIO(ByVal strNroContrato As String, ByVal strCodInsDesembolso As String) As String
        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx
        Dim pstrURL As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIO")
        Dim pstrProd As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIOArgProducto")
        Dim pstrPaso As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIOArgPaso")
        Dim strTipoProdFin As String 'Leasing o LeaseBack
        Dim strTipoProdFinPas As String
        Dim strProdWIO As String
        Dim pstrProdLB As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsEstadoWIOArgProductoLeaseBack")
        Dim strWIO As String = ""
        Dim strUser As String
        Dim strNom As String
        Dim strRta As String
        Try
            ' Obtengo el nro de WIO a desembolsar
            Dim table As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.obtenerWIO(strNroContrato, strCodInsDesembolso))
            If (table.Rows.Count > 0) Then
                strWIO = table.Rows.Item(0).Item("NroInstruccionWIO").ToString
                strTipoProdFin = table.Rows.Item(0).Item("CodProductoFinancieroActivo").ToString
                strTipoProdFinPas = table.Rows.Item(0).Item("CodProductoFinancieroPasivo").ToString
            End If
            If strTipoProdFin = GCCConstante.C_CODLPC_PROD_LEASEBACK And strTipoProdFinPas = GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS Then
                strProdWIO = pstrProdLB
            Else
                strProdWIO = pstrProd
            End If
            'obtengo parámetros
            strUser = GCCSession.CodigoUsuario.ToString
            strNom = GCCSession.NombreUsuario.ToString
            If strWIO.Trim() = "" Then
                strRta = "0|Leasing desembolsado, por favor finalize el WIO"
            Else
                strRta = objLInstruccionDesembolsoNTx.finalizaWIO(strNroContrato, strCodInsDesembolso, pstrURL, strWIO, strProdWIO, pstrPaso, strUser, strNom)
            End If


            Return strRta
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Util"

    ''' <summary>
    ''' Total paginas
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function



#End Region

End Class
