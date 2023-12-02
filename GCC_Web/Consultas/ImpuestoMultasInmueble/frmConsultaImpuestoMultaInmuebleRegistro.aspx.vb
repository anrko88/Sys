Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Consultas_ImpuestoMultasInmueble_frmConsultaImpuestoMultaInmuebleRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmConsultaImpuestoMultaInmuebleListado.aspx.vb")

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

                'Setea Modo Transaccion
                Me.hddTipoTx.Value = GCCConstante.C_TX_NUEVO
                Me.hddAbreEditarAuto.Value = "N"


                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                Dim strCodImpuesto As String = Request.QueryString("hddCodImpuesto")

                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodImpuesto.Value = strCodImpuesto


                'Carga Datos Editar
                If Not strCodImpuesto Is Nothing Then
                    Me.hddAbreEditarAuto.Value = "S"
                End If

                'Carga Departamento
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)

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
    ''' Lista Bienes para ImpuestoMunicipal
    ''' </summary>
    ''' <returns>Listado de Bienes</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipalBienes(ByVal pPageSize As Integer, _
                                                         ByVal pCurrentPage As Integer, _
                                                         ByVal pSortColumn As String, _
                                                         ByVal pSortOrder As String, _
                                                         ByVal pstrDepartamento As String, _
                                                         ByVal pstrProvincia As String, _
                                                         ByVal pstrDistrito As String, _
                                                         ByVal pstrUbicacion As String) As JQGridJsonResponse

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
                .Ubicacion = GCCUtilitario.NullableString(pstrUbicacion)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipalBienes(pPageSize, _
                                                                                                                                                   pCurrentPage, _
                                                                                                                                                   pSortColumn, _
                                                                                                                                                   pSortOrder, _
                                                                                                                                                   strEImpuestomunicipal))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtBienes.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtBienes.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtBienes.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienes)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' ListaImpuestoMunicipal
    ''' </summary>
    ''' <returns>Listado de Impuestos y Multas Municipales</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipal(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pstrCodContrato As String, _
                                                     ByVal pstrCodBien As String, _
                                                     ByVal pstrTieneLote As String) As JQGridJsonResponse

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx

        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Departamento = Nothing
                .Provincia = Nothing
                .Distrito = Nothing
                .NroContrato = Nothing
                .RazonSocial = Nothing
                .TipoDocumento = Nothing
                .NroDocumento = Nothing
                .Periodo = 0
                .Lote = Nothing
                .EstadoPago = Nothing
                .EstadoCobro = Nothing

                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.NullableString(pstrCodBien)
                .TieneLote = GCCUtilitario.NullableString(pstrTieneLote)
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

    ''' <summary>
    ''' Lista Bien para ImpuestoMunicipal Editar
    ''' </summary>
    ''' <returns>Listado de Bienes</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipalBienEditar(ByVal pPageSize As Integer, _
                                                         ByVal pCurrentPage As Integer, _
                                                         ByVal pSortColumn As String, _
                                                         ByVal pSortOrder As String, _
                                                         ByVal pstrCodContrato As String, _
                                                         ByVal pstrCodBien As String) As JQGridJsonResponse

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx

        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Secfinanciamiento = GCCUtilitario.CheckInt(pstrCodBien)
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipalBienes(pPageSize, _
                                                                                                                                                   pCurrentPage, _
                                                                                                                                                   pSortColumn, _
                                                                                                                                                   pSortOrder, _
                                                                                                                                                   strEImpuestomunicipal))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtBienes.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtBienes.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtBienes.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienes)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' EliminarImpuestoMunicipal
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function EliminarImpuestoMunicipal(ByVal pstrCodigosImpuestos As String) As String

        'Variables
        Dim objLImpuestoMunicipalTx As New LImpuestoMunicipalTx
        Dim blnResult As Boolean = False

        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .CodigosImpuestos = pstrCodigosImpuestos
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)
            objLImpuestoMunicipalTx.EliminarImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
            blnResult = True

            'Valida Resultado
            If blnResult Then
                Return "1"
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

    ''' <summary>
    ''' GeneraLote
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GeneraLote(ByVal pstrCodigosImpuestos As String) As String

        'Variables
        Dim objLImpuestoMunicipalTx As New LImpuestoMunicipalTx
        Dim blnResult As Boolean = False

        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .CodigosImpuestos = pstrCodigosImpuestos
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)
            Dim strNroLote As String = objLImpuestoMunicipalTx.AsignarLoteImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
            blnResult = True

            'Valida Resultado
            If blnResult Then
                Return strNroLote
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

#End Region

#Region "Metodos"

    ''' <summary>
    ''' TotalPaginas
    ''' </summary>
    ''' <param name="total"></param>
    ''' <param name="pPageSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region

End Class
