
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_ImpuestoVehicular_frmConsultasImpuestoVehicular
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultasImpuestoVehicular.aspx.vb")
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

                GCCUtilitario.CargarComboValorGenerico(ddlTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
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

#Region "Métodos"
    <WebMethod()> _
    Public Shared Function ListaImpuestoVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pTipoDocumento As String, _
                                           ByVal pNumeroDocumento As String, _
                                           ByVal pPlaca As String, _
                                           ByVal pFechaInscripcionIni As String, _
                                           ByVal pFechaInscripcionFin As String, _
                                           ByVal pEstadoCobro As String, _
                                           ByVal pEstadoPago As String, _
                                           ByVal pAnioFabricacion As String, _
                                           ByVal pPeriodo As String, _
                                           ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            
        Dim strCodUnico As String = GCCUtilitario.NullableString(pCUCliente)
        If Not strCodUnico Is Nothing Then
            strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
        End If

        With oEImpuestoVehicular
            .Codsolcredito = pNumeroContraro
            .Codunico = strCodUnico
            .RazonSocialNombre = pRazonSocial
            .CodigoTipoDocumento = pTipoDocumento
            .NumeroDocumento = pNumeroDocumento
            .Placa = pPlaca
            .FechaInscripcionIni = GCCUtilitario.StringToDateTime(pFechaInscripcionIni)
            .FechaInscripcionFin = GCCUtilitario.StringToDateTime(pFechaInscripcionFin)
            .EstadoCobro = pEstadoCobro
            .EstadoPago = pEstadoPago
            .AnioFabricacion = pAnioFabricacion
            .Periodo = pPeriodo
            .CodNroLote = pNroLote
        End With


        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarImpuestoVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of EImpuestovehicular)(oEImpuestoVehicular)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtImpuestoVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpuestoVehicular)

    End Function
#End Region
End Class