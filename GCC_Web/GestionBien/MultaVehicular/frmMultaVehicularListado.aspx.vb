Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_MultaVehicular_frmMultaVehicularListado
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMultaVehicularListado.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 09/11/2012
    '''  </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            If Not Page.IsPostBack Then

                GCCUtilitario.CargarComboValorGenerico(ddlTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
                GCCUtilitario.CargarComboValorGenerico(ddlConcepto, GCCConstante.C_TABLAGENERICA_MultaVehicular_Concepto)
                GCCUtilitario.CargarComboValorGenericoInfraccion(ddlCodInfraccion, GCCConstante.C_TABLAGENERICA_MultaVehiculAR_Infraccion)
                'GCCUtilitario.CargarMunicipalidad(ddlMunicipalidad)
                'Inicio IBK - AAE
                txtNroLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("NroLote")), "", Request.QueryString("NroLote"))
                'Fin IBK
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
    ''' <summary>
    ''' BusquedaRapida
    ''' Creacion : 21/02/2013
    ''' Creador : JJM IBK
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ConsultaMunicipalidad(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As JQGridJsonResponse
        Dim objUtilNTX As New LUtilNTX

        Dim dtUtil As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTX.ListadoMunicipalidadPaginado(100, 100, "CLAVE1", "desc", _
                                                                                                                    pCodMunicipalidad, _
                                                                                                                    pMunicipalidad))
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtUtil)
        ' Número total de páginas

    End Function
#End Region

#Region "Métodos"
    <WebMethod()> _
    Public Shared Function ListaMultaVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pTipoDocumento As String, _
                                           ByVal pNumeroDocumento As String, _
                                           ByVal pTipoBien As String, _
                                           ByVal pNroLote As String, _
                                           ByVal pConcepto As String, _
                                           ByVal pPlaca As String, _
                                           ByVal pCodInfraccion As String, _
                                           ByVal pInfraccion As String, _
                                           ByVal pCodMunicipalidad As String, _
                                           ByVal pEstadoCobro As String, _
                                           ByVal pEstadoPago As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEMultaVehicular As New EGCC_MultaVehicular

        With oEMultaVehicular
            .Codsolcredito = pNumeroContraro
            .Codunico = pCUCliente
            .RazonSocialNombre = pRazonSocial
            .CodigoTipoDocumento = pTipoDocumento
            .NumeroDocumento = pNumeroDocumento
            .CodTipoBien = pTipoBien
            .CodNroLote = pNroLote
            .CodConcepto = pConcepto
            .Placa = pPlaca
            .CodInfraccion = pCodInfraccion
            .Infraccion = pInfraccion
            .CodMunicipalidad = IIf(String.IsNullOrEmpty(pCodMunicipalidad), "0", pCodMunicipalidad)
            .EstadoCobro = pEstadoCobro
            .EstadoPago = pEstadoPago
            
        End With


        Dim dtMultaVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarMultaVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtMultaVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtMultaVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtMultaVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtMultaVehicular)

    End Function

    'Inicio IBK - AAE - 13/02/2013
    <WebMethod()> _
   Public Shared Function CheckLote(ByVal pNroLote As String) As String
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim flag As String = GCCConstante.C_LOTE_MULT_VEHICULAR ' Para multa vehicular el flag es 1
        Dim res As String = objImpuestoVehicularTx.CheckLote(pNroLote, flag)
        Return res
    End Function
    'Fin IBK

#End Region
End Class
