Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class InsDesembolso_frmInsDesembolsoListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmInsDesembolsoListado.aspx.vb")

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
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoContrato, GCCConstante.C_TABLAGENERICA_SUB_TIPO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_InsDesembolso_Estado)
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
    ''' ListaInsDesembolso
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pstrNroContrato"></param>
    ''' <param name="pstrCuCliente"></param>
    ''' <param name="pstrRazonSocial"></param>
    ''' <param name="pstrNroInstruccion"></param>
    ''' <param name="pstrFechaIngresoIni"></param>
    ''' <param name="pstrFechaIngresoFin"></param>
    ''' <param name="pstrTipoContrato"></param>
    ''' <param name="pstrEstado"></param>
    ''' <param name="pstrMoneda"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaInsDesembolso(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pstrNroContrato As String, _
                                             ByVal pstrCuCliente As String, _
                                             ByVal pstrRazonSocial As String, _
                                             ByVal pstrNroInstruccion As String, _
                                             ByVal pstrFechaIngresoIni As String, _
                                             ByVal pstrFechaIngresoFin As String, _
                                             ByVal pstrTipoContrato As String, _
                                             ByVal pstrEstado As String, _
                                             ByVal pstrMoneda As String, _
                                             ByVal pstrNroWIO As String) As JQGridJsonResponse

        'Variables
        Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

        Try

            'Valida Campos
            If pstrFechaIngresoIni <> "" Then
                pstrFechaIngresoIni = CDate(pstrFechaIngresoIni).ToString("yyyyMMdd")
            End If
            If pstrFechaIngresoFin <> "" Then
                pstrFechaIngresoFin = CDate(pstrFechaIngresoFin).ToString("yyyyMMdd")
            End If
            Dim strCodUnico As String = GCCUtilitario.NullableString(pstrCuCliente)
            If Not strCodUnico Is Nothing Then
                strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
            End If

            'Inicializa Objeto
            Dim objEGCC_InsDesembolso As New EGCC_InsDesembolso
            Dim strEGCC_InsDesembolso As String
            With objEGCC_InsDesembolso
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .CUCliente = strCodUnico
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .Codinstrucciondesembolso = GCCUtilitario.NullableStringCombo(pstrNroInstruccion)
                .FechaInicio = GCCUtilitario.NullableString(pstrFechaIngresoIni)
                .FechaFin = GCCUtilitario.NullableString(pstrFechaIngresoFin)
                .TipoContrato = GCCUtilitario.NullableStringCombo(pstrTipoContrato)
                .CodigoEstado = GCCUtilitario.NullableStringCombo(pstrEstado)
                .CodigoMoneda = GCCUtilitario.NullableStringCombo(pstrMoneda)
                'Inicio IBK - AAE
                .NroWIO = GCCUtilitario.NullableStringCombo(pstrNroWIO)
                'Fin IBK

            End With
            strEGCC_InsDesembolso = GCCUtilitario.SerializeObject(objEGCC_InsDesembolso)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolso(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       strEGCC_InsDesembolso))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtInstruccionDesembolso.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtInstruccionDesembolso.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtInstruccionDesembolso.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtInstruccionDesembolso)

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
