Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Pagos_frmPagoCuotasListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPagoCuotasListado.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 18/12/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página frmPagoCuotasListado", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            Dim strOp As String = Request.QueryString("op")

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoContrato, GCCConstante.C_TABLAGENERICA_SUB_TIPO_CONTRATO)

                If strOp Is Nothing Then
                    GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_RECUPERACION)
                Else
                    Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_EXTORNO
                    Me.hddTipoTransaccion.Value = "E"
                    Me.cmbEstado.Items.Add(New ListItem("EXTORNADO", "E"))
                End If

                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)
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

#End Region

#Region "WebMethods"
    ''' <summary>
    ''' ListaPagoCuotas
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrCuCliente"></param>
    ''' <param name="pstrRazonSocial"></param>
    ''' <param name="pstrNroAutorizacion"></param>
    ''' <param name="pstrFechaPagoIni"></param>
    ''' <param name="pstrFechaPagoFin"></param>
    ''' <param name="pstrTipoContrato"></param>
    ''' <param name="pstrEstado"></param>
    ''' <param name="pstrMoneda"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaPagoCuotas(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pstrNroContrato As String, _
                                             ByVal pstrCuCliente As String, _
                                             ByVal pstrRazonSocial As String, _
                                             ByVal pstrNroAutorizacion As String, _
                                             ByVal pstrFechaPagoIni As String, _
                                             ByVal pstrFechaPagoFin As String, _
                                             ByVal pstrTipoContrato As String, _
                                             ByVal pstrEstado As String, _
                                             ByVal pstrMoneda As String) As JQGridJsonResponse


        'Variables
        Dim objLPagosNTx As New LPagosNTx

        Try

            'Valida Campos
            If pstrFechaPagoIni <> "" Then
                pstrFechaPagoIni = CDate(pstrFechaPagoIni).ToString("yyyyMMdd")
            End If
            If pstrFechaPagoFin <> "" Then
                pstrFechaPagoFin = CDate(pstrFechaPagoFin).ToString("yyyyMMdd")
            End If

            'Inicializa Objeto
            Dim objEGCC_PagoCuotas As New EGCC_PagoCuotas
            Dim strEGCC_PagoCuotas As String
            With objEGCC_PagoCuotas
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .CUCliente = GCCUtilitario.NullableString(pstrCuCliente)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .NroAutorizacion = GCCUtilitario.NullableStringCombo(pstrNroAutorizacion)
                .FechaPagoInicio = GCCUtilitario.NullableString(pstrFechaPagoIni)
                .FechaPagoFin = GCCUtilitario.NullableString(pstrFechaPagoFin)
                .TipoContrato = GCCUtilitario.NullableStringCombo(pstrTipoContrato)
                .CodigoEstado = GCCUtilitario.NullableStringCombo(pstrEstado)
                .CodigoMoneda = GCCUtilitario.NullableStringCombo(pstrMoneda)
            End With
            strEGCC_PagoCuotas = GCCUtilitario.SerializeObject(objEGCC_PagoCuotas)

            'Ejecuta Consulta
            Dim dtPagoCuotas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoCuotas(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       strEGCC_PagoCuotas))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtPagoCuotas.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtPagoCuotas.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtPagoCuotas.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtPagoCuotas)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Metodos"

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region

End Class
