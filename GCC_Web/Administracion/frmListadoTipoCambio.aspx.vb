Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Administracion_frmListadoTipoCambio
    Inherits GCCBase
    Dim objLog As New GCCLog("frmListadoTipoCambio.aspx.vb")

#Region "Eventos"

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
                GCCUtilitario.CargarComboValorGenerico(cmbTipoModalidad, GCCConstante.C_TABLAGENERICA_Modalidad_TipoCambio)
                Me.txtFechaIni.Value = Now.ToString("dd/MM/yyyy")
                Me.txtFechaFin.Value = Now.ToString("dd/MM/yyyy")

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

#End Region

#Region "WebMétodos"
    <WebMethod()> _
    Public Shared Function BuscarTipoCambio(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pCodigoTipoModalidad As String, _
                                             ByVal pFechaInicioVigencia As String, _
                                             ByVal pFechaFinalVigencia As String) As JQGridJsonResponse

        Dim objTipoCambioNTx As New LTipoCambioNTx
        Dim ETipoCambio As New EMonedatipocambio
        With ETipoCambio
            .Tipomodalidadcambio = pCodigoTipoModalidad
            .Fechainiciovigencia = GCCUtilitario.NullableString(ConvierteFecha(pFechaInicioVigencia))
            .Fechafinalvigencia = GCCUtilitario.NullableString(ConvierteFecha(pFechaFinalVigencia))
            .Tipooperacion = "2"
        End With

        Dim dtTipoCambio As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTipoCambioNTx.ListaTipoCambio(pPageSize, _
                                                                                                                    pCurrentPage, _
                                                                                                                    pSortColumn, _
                                                                                                                    pSortOrder, _
                                                                                                                    GCCUtilitario.SerializeObject(Of EMonedatipocambio)(ETipoCambio)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtTipoCambio.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtTipoCambio.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtTipoCambio.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtTipoCambio)

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

#End Region
End Class
