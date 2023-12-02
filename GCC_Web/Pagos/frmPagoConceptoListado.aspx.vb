Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Pagos_frmPagoConceptoListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPagoConceptoListado.aspx.vb")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página frmPagoConceptoListado", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoContrato, GCCConstante.C_TABLAGENERICA_SUB_TIPO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_RECUPERACION)
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
#Region "WebMethods"
    ''' <summary>
    ''' ListaPagoConcepto
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pstrNroContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaPagoConcepto(ByVal pPageSize As Integer, _
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
            Dim objEGCC_PagoConcepto As New EGcc_otroconcepto
            'Dim strEGCC_PagoConcepto As String
            With objEGCC_PagoConcepto
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .CodUnico = GCCUtilitario.NullableString(pstrCuCliente)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .NumSecuenciaAutorizacion = GCCUtilitario.NullableString(pstrNroAutorizacion)
                .FechaRegistroInicio = GCCUtilitario.NullableString(pstrFechaPagoIni)
                .FechaRegistroFin = GCCUtilitario.NullableString(pstrFechaPagoFin)
                .Codigoestadopago = GCCUtilitario.NullableString(pstrEstado)
                .Codigomoneda = GCCUtilitario.NullableString(pstrMoneda)
                .TipoContrato = GCCUtilitario.NullableString(pstrTipoContrato)
            End With


            'Ejecuta Consulta
            Dim dtPagoConcepto As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoPagoConcepto(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       GCCUtilitario.SerializeObject(Of EGcc_otroconcepto)(objEGCC_PagoConcepto)))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtPagoConcepto.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtPagoConcepto.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtPagoConcepto.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtPagoConcepto)

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
