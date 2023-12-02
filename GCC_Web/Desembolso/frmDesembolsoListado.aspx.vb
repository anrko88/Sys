Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data
Imports System.Web.Services


Partial Class Desembolso_frmDesembolsoListado
    Inherits GCCBase

    Dim _objLog As New GCCLog("frmDesembolsoListado.aspx.vb")

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
            _objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                _objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                Call InicializarListas()
            End If

        Catch ex As ApplicationException
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"

    Private Sub InicializarListas()
        GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
        'GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
        GCCUtilitario.CargarComboEstadosdelContrato(Me.cmbEstado)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacion, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionContrato, GCCConstante.C_TABLAGENERICA_SUB_TIPO_CONTRATO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbBanca, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
        GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)
    End Sub

    <WebMethod()> _
    Public Shared Function BuscarDocumentos(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pContrato As String, _
                                           ByVal pCuCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pClasificacion As String, _
                                           ByVal pMoneda As String, _
                                           ByVal pCodigoSubTipoContrato As String, _
                                           ByVal pEstado As String, _
                                           ByVal pEjecutivo As String, _
                                           ByVal pBanca As String) As JQGridJsonResponse
        Dim objContratoNTx As New LContratoNTx

        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratosDesembolso(pPageSize, _
                                                                                                                    pCurrentPage, _
                                                                                                                    pSortColumn, _
                                                                                                                    pSortOrder, _
                                                                                                                    pContrato, _
                                                                                                                    pCuCliente, _
                                                                                                                    pRazonSocial, _
                                                                                                                    pEjecutivo, _
                                                                                                                    pEstado, _
                                                                                                                    pClasificacion, _
                                                                                                                    pCodigoSubTipoContrato, _
                                                                                                                    pBanca, _
                                                                                                                    pMoneda))

        Dim oJQGridJsonResponse As New JQGridJsonResponse
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtContrato.Rows.Count = 0 Then
            totalRecords = 0
        Else
            totalRecords = Convert.ToInt32(dtContrato.Rows(0)("RecordCount"))
        End If
        ' Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)

        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, pCurrentPage, totalRecords, dtContrato)

    End Function

    <WebMethod()> _
    Public Shared Function EnviarCarta(ByVal pCodigoContrato As String) As String
        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim objEGCC_Carta As New EGcc_carta
        Dim pSolicitudCredito As String
        Dim pEGCC_Carta As String

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        objEGCC_Carta.Codsolicitudcredito = pCodigoContrato
        objEGCC_Carta.Usuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        objEGCC_Carta.Numerocarta = Nothing

        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
        pEGCC_Carta = GCCUtilitario.SerializeObject(objEGCC_Carta)

        Dim blnResult As Boolean = objContratoTx.EnviarCarta(pSolicitudCredito, _
                                                             pEGCC_Carta)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

#End Region

End Class
