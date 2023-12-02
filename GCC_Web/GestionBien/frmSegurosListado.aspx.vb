Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_frmSegurosListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmSegurosListado.aspx.vb")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoValor, GCCConstante.C_TABLAGENERICA_Tipo_Valor)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoSeguro, GCCConstante.C_TABLAGENERICA_Tipo_Seguro_reporte)

            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                If Not IsPostBack Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                End If
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#Region "WebMethods"

    <WebMethod()> _
    Public Shared Function ListarSeguros(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pstrNroContrato As String, _
                                             ByVal pstrNroPoliza As String, _
                                             ByVal pstrCiaSeguros As String, _
                                             ByVal pstrTipoValor As String, _
                                             ByVal pstrFechaIngresoIni As String, _
                                             ByVal pstrFechaIngresoFin As String, _
                                             ByVal pstrTipobien As String, _
                                             ByVal pstrTipoSeguro As String) As JQGridJsonResponse

        'Variables
        Dim objTemporalNTx As New LTemporalNTx

        Try
            'Valida Campos
            If pstrFechaIngresoIni <> "" Then
                pstrFechaIngresoIni = CDate(pstrFechaIngresoIni).ToString("yyyyMMdd")
            End If
            If pstrFechaIngresoFin <> "" Then
                pstrFechaIngresoFin = CDate(pstrFechaIngresoFin).ToString("yyyyMMdd")
            End If

            'Inicializa Objeto
            Dim objEGcc_Seguro As New ESeguro
            Dim strEGcc_Seguro As String
            With objEGcc_Seguro
                .NroContraro = GCCUtilitario.NullableString(pstrNroContrato)
                .NroPoliza = GCCUtilitario.NullableString(pstrNroPoliza)
                .CiaSeguro = GCCUtilitario.NullableString(pstrCiaSeguros)
                .CodigoTipoValor = IIf(pstrTipoValor = "0", "", pstrTipoValor) 'GCCUtilitario.NullableString(pstrTipoValor)
                .FechaInicio = GCCUtilitario.StringToDateTime(pstrFechaIngresoIni)
                .FechaFin = GCCUtilitario.StringToDateTime(pstrFechaIngresoFin)
                .TipoBien = GCCUtilitario.NullableStringCombo(pstrTipobien)
                .CodigoTipoSeguro = IIf(pstrTipoSeguro = "0", "", pstrTipoSeguro) 'GCCUtilitario.NullableStringCombo(pstrTipoSeguro)
            End With

            strEGcc_Seguro = GCCUtilitario.SerializeObject(objEGcc_Seguro)

            'Ejecuta Consulta
            Dim dtSeguros As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTemporalNTx.ListarSeguros(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   GCCUtilitario.SerializeObject(Of ESeguro)(objEGcc_Seguro)))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtSeguros.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtSeguros.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtSeguros.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSeguros)

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
