Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmImpuestoMultaInmuebleListado.aspx.vb")
    Dim strNroContrato As String

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

                GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoDoc, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)
                'Inicio JJM IBK
                'GCCUtilitario.CargarMunicipalidad(Me.cmbMunicipalidad)
                'Fin JJm IBK
                GCCUtilitario.CargarComboValorGenerico(Me.cmdEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(Me.cmdEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
                'Inicio IBK - AAE
                txtLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("NroLote")), "", Request.QueryString("NroLote"))
                'Fin IBK
                
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
    ''' ListaImpuestoMunicipal
    ''' </summary>
    ''' <returns>Listado de Impuestos y Multas Municipales</returns>
    ''' <remarks> 'JJM IBK
    ''' Se agrego el Filtro municipalidad
    ''' pstrMunicipalidad
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipal(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pstrDepartamento As String, _
                                                     ByVal pstrProvincia As String, _
                                                     ByVal pstrDistrito As String, _
                                                     ByVal pstrNroContrato As String, _
                                                     ByVal pstrRazonSocial As String, _
                                                     ByVal pstrTipoDoc As String, _
                                                     ByVal pstrDocumento As String, _
                                                     ByVal pstrPeriodo As String, _
                                                     ByVal txtLote As String, _
                                                     ByVal pstrEstadoPago As String, _
                                                     ByVal pstrEstadoCobro As String, _
                                                     ByVal pstrMunicipalidad As String) As JQGridJsonResponse

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx

        Try
           
            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Departamento = GCCUtilitario.NullableStringCombo(pstrDepartamento)
                .Provincia = GCCUtilitario.NullableStringCombo(pstrProvincia)
                .Distrito = GCCUtilitario.NullableStringCombo(pstrDistrito)
                .NroContrato = GCCUtilitario.NullableString(pstrNroContrato)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .TipoDocumento = GCCUtilitario.NullableStringCombo(pstrTipoDoc)
                .NroDocumento = GCCUtilitario.NullableString(pstrDocumento)
                .Periodo = GCCUtilitario.NullableString(pstrPeriodo)
                .Lote = GCCUtilitario.NullableString(txtLote)
                .EstadoPago = GCCUtilitario.NullableStringCombo(pstrEstadoPago)
                .EstadoCobro = GCCUtilitario.NullableStringCombo(pstrEstadoCobro)
                .Municipalidad = GCCUtilitario.NullableStringCombo(pstrMunicipalidad)

            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipal(pPageSize, _
                                                                                                                                           pCurrentPage, _
                                                                                                                                           pSortColumn, _
                                                                                                                                           pSortOrder, _
                                                                                                                                           strEImpuestomunicipal))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtImpMunicipal.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtImpMunicipal.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtImpMunicipal.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpMunicipal)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Inicio IBK - AAE - 13/02/2013
    <WebMethod()> _
   Public Shared Function CheckLote(ByVal pNroLote As String) As String
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim flag As String = GCCConstante.C_LOTE_IMP_MUNICIPAL ' Para impuesto inmueble el flag es 2
        Dim res As String = objImpuestoVehicularTx.CheckLote(pNroLote, flag)
        Return res
    End Function
    'Fin IBK
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


