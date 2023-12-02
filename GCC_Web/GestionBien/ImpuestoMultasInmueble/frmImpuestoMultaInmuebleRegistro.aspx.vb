Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmImpuestoMultaInmuebleListado.aspx.vb")

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
                Dim strNroLote As String = IIf(String.IsNullOrEmpty(Request.QueryString("NroLote")), "", Request.QueryString("NroLote"))
                Dim strMunicipalidad As String = IIf(String.IsNullOrEmpty(Request.QueryString("Municipalidad")), "", Request.QueryString("Municipalidad"))
                Dim strPeriodo As String = IIf(String.IsNullOrEmpty(Request.QueryString("Periodo")), "", Request.QueryString("Periodo"))

                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodImpuesto.Value = strCodImpuesto
                Me.hddNroLote.Value = strNroLote
                Me.hddMunicipalidadBien.Value = strMunicipalidad
                Me.hddPeriodoEdita.Value = strPeriodo

                'Carga Datos Editar
                If Not String.IsNullOrEmpty(strCodImpuesto) Then
                    Me.hddAbreEditarAuto.Value = "S"
                End If
                'If Not String.IsNullOrEmpty(hddNroLote.Value) Then
                '    txtNroLoteCarga.Value = hddNroLote.Value
                'Else
                '    txtNroLoteCarga.Value = GeneraLoteLoad("", "", hddNroLote.Value, "", "0.00", "0.00", "0")
                'End If
                'Carga Departamento
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)
                'Inicio IBK
                'GCCUtilitario.CargarMunicipalidad(Me.cmbMunicipalidad)
                'Fin IBK
                If Not String.IsNullOrEmpty(strMunicipalidad) Then
                    ListadoMunicipalidadPaginado(strMunicipalidad, "") 'JJM IBK
                    '                    GCCUtilitario.SeleccionaCombo(cmbMunicipalidad, strMunicipalidad)
                End If
                hddPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                'Inicio IBK - AAE - Agrego info de lote
                If Not String.IsNullOrEmpty(hddNroLote.Value) Then
                    txtNroLoteCarga.Value = hddNroLote.Value
                    hidTengoLote.Value = hddNroLote.Value
                    'Obtengo info del lote
                    Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
                    Dim flag As String = GCCConstante.C_LOTE_IMP_MUNICIPAL
                    Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(hddNroLote.Value, flag))
                    hidCodEstadoLote.Value = dtImpuestoVehicular.Rows(0).Item("CodEstadoLote").ToString()
                    txtEstadoLote.Value = dtImpuestoVehicular.Rows(0).Item("DescCodEstadoLote").ToString()
                    txtMontoCheque.Value = dtImpuestoVehicular.Rows(0).Item("MontoCheque").ToString()
                    txtTotalLote.Value = dtImpuestoVehicular.Rows(0).Item("TotalLote").ToString()
                    txtDevuelto.Value = dtImpuestoVehicular.Rows(0).Item("DevueltoLote").ToString()
                    txtReembolsar.Value = dtImpuestoVehicular.Rows(0).Item("ReembolsoLote").ToString()
                    txtTotalAutovaluo.Value = dtImpuestoVehicular.Rows(0).Item("TotalAutovaluo").ToString()
                    txtTotalPredial.Value = dtImpuestoVehicular.Rows(0).Item("TotalPredial").ToString()
                    txtPeriodo.Value = dtImpuestoVehicular.Rows(0).Item("Periodo").ToString()
                    If String.IsNullOrEmpty(strMunicipalidad) Then
                        ListadoMunicipalidadPaginado(dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString().Trim(), "") 'JJM IBK
                        '                    GCCUtilitario.SeleccionaCombo(cmbMunicipalidad, strMunicipalidad)
                    End If
                    Me.hddTipoTx.Value = GCCConstante.C_TX_EDITAR
                Else
                    txtNroLoteCarga.Value = GenerarLoteLoad2("", hddNroLote.Value)
                    hidTengoLote.Value = "N"
                    txtEstadoLote.Value = "Ingresado"
                    txtMontoCheque.Value = "0"
                    txtTotalLote.Value = "0"
                    txtDevuelto.Value = "0"
                    txtReembolsar.Value = "0"
                End If
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
    ''' Lista Bienes para ImpuestoMunicipal
    ''' </summary>
    ''' <returns>Listado de Bienes</returns>
    ''' <remarks></remarks>
    ''' Inicio IBK
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipalBienes(ByVal pPageSize As Integer, _
                                                         ByVal pCurrentPage As Integer, _
                                                         ByVal pSortColumn As String, _
                                                         ByVal pSortOrder As String, _
                                                         ByVal pstrDepartamento As String, _
                                                         ByVal pstrProvincia As String, _
                                                         ByVal pstrDistrito As String, _
                                                         ByVal pstrUbicacion As String, _
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
                .Ubicacion = GCCUtilitario.NullableString(pstrUbicacion)
                'Inicio IBK
                .Municipalidad = GCCUtilitario.NullableStringCombo(pstrMunicipalidad)
                'Fin IBK
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
    ''' Inicio IBK - AAE - Agrego paráemtro
    <WebMethod()> _
    Public Shared Function EliminarImpuestoMunicipal(ByVal pstrCodigosImpuestos As String, ByVal pstrNroLote As String) As String

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
                .Lote = GCCUtilitario.NullableString(pstrNroLote)
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
    Public Shared Function GeneraLote(ByVal pstrCodigosImpuestos As String, ByVal pRegeneraLote As String, ByVal pNroLote As String, ByVal pMunicipalidad As String, _
                                      ByVal pTotalAutovaluo As String, ByVal pTotalPredial As String, ByVal pPeriodo As String) As String

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
                .TieneLote = pRegeneraLote
                .Lote = pNroLote

                .Municipalidad = pMunicipalidad
                .TotalAutovaluo = GCCUtilitario.ConvierteValorBien(pTotalAutovaluo)
                .TotalPredial = GCCUtilitario.ConvierteValorBien(pTotalPredial)
                .Periodo = pPeriodo

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
    'Inicio IBK - AAE
    <WebMethod()> _
   Public Shared Function ReGenerarLote(ByVal strNroLote As String) As String

        Try

            Dim objLImpuestoVehicular As New LImpuestoVehicularTX


            Dim strNroLoteGenerado As String = objLImpuestoVehicular.ReGenerarLote(strNroLote)

            Return strNroLoteGenerado

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    <WebMethod()> _
   Public Shared Function ObtenerInfoLote(ByVal pNroLote As String) As JQGridJsonResponse

        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            
        Dim flag As String = GCCConstante.C_LOTE_IMP_MUNICIPAL
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(pNroLote, flag))


        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtImpuestoVehicular)

    End Function
    'Fin IBK


    ''' <summary>
    ''' ListaImpuestoMunicipalxLote
    ''' </summary>
    ''' <returns>Listado de Impuestos y Multas Municipales</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipalxLotes(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pstrCodContrato As String, _
                                                     ByVal pstrCodBien As String, _
                                                     ByVal pstrTieneLote As String, _
                                                     ByVal pstrNroLote As String) As JQGridJsonResponse

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
                .Lote = pstrNroLote
                .EstadoPago = Nothing
                .EstadoCobro = Nothing

                '.Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                '.Secfinanciamiento = GCCUtilitario.NullableString(pstrCodBien)
                '.TieneLote = GCCUtilitario.NullableString(pstrTieneLote)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipalxLote(pPageSize, _
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
    <WebMethod()> _
    Public Shared Function GetImpuestoTotalesInmueble(ByVal pstrPeriodo As String, _
                                                ByVal pstrMunicipalidad As String) As EImpuestomunicipal

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Periodo = pstrPeriodo
                .Municipalidad = pstrMunicipalidad
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.GetImpuestoTotalesInmueble(strEImpuestomunicipal))
            If dtImpMunicipal.Rows.Count > 0 Then
                oEImpuestoMunicipal.TotalPredial = dtImpMunicipal.Rows(0).Item("TotalPredial").ToString
                oEImpuestoMunicipal.TotalAutovaluo = dtImpMunicipal.Rows(0).Item("TotalAutovaluo").ToString
            End If
            Return oEImpuestoMunicipal
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
  Public Shared Function EliminarLote(ByVal pLote As String) As String

        Try

            Dim objLImpuestoMunicipal As New LImpuestoMunicipalTx


            Dim blnResult As Boolean = objLImpuestoMunicipal.EliminarLoteImpuestoMunicipal(pLote)


            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    'Inicio IBK - AAE - Agrego funcion
    <WebMethod()> _
    Public Shared Function AnularLote(ByVal pLote As String) As String

        Try

            Dim objLMultaVehicular As New LImpuestoVehicularTX


            Dim strResult As String = objLMultaVehicular.AnularLote(pLote)


            Return strResult
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    'Fin IBK
    ''' <summary>
    ''' descuentoImpuestoMunicipal
    ''' Creacion : 19/02/2013
    ''' Creador : JJM IBK
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function DescuentoLoteImpuestoMunicipal(ByVal pNroLote As String, ByVal pDescuento As Decimal) As String

        'Variables        
        Dim pUsuarioModificacion As String = GCCSession.CodigoUsuario

        Try

            'Inicializa Objeto

            Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
            'Ejecuta Consulta
            'Inicio IBK - AAE
            'Dim strRes As String = objLImpuestoMunicipalNTx.DescuentoLoteImpuestoMunicipal(pNroLote, pUsuarioModificacion, pDescuento)
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.DescuentoLoteImpuestoMunicipal(pNroLote, pUsuarioModificacion, pDescuento))
            Dim resultado As String = String.Empty

            If dtImpMunicipal.Rows.Count > 0 Then
                resultado = dtImpMunicipal.Rows(0).Item("Resultado").ToString
            End If

            'Valida Resultado

            Return IIf(String.IsNullOrEmpty(resultado), "0", resultado)
            'Return "1"
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    ''' <summary>
    ''' BusquedaRapida
    ''' Creacion : 21/02/2013
    ''' Creador : JJM IBK
    ''' </summary>
    ''' <remarks></remarks>
    ''' Public Shared Function ConsultaMunicipalidad(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As JQGridJsonResponse
    <WebMethod()> _
    Public Shared Function ConsultaMunicipalidad(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As String
        Dim objUtilNTX As New LUtilNTX
        Dim strRes As String = ""

        Dim dtUtil As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTX.ListadoMunicipalidadPaginado(100, 100, "CLAVE1", "desc", _
                                                                                                                    pCodMunicipalidad, _
                                                                                                                    pMunicipalidad))
        If dtUtil.Rows.Count = 1 Then
            strRes = "0|" + dtUtil.Rows(0).Item("CLAVE1").ToString() + "|" + dtUtil.Rows(0).Item("VALOR1").ToString
        Else
            strRes = "1|"
        End If
        'Dim objJQGridJsonResponse As New JQGridJsonResponse
        'Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtUtil)
        ' Número total de páginas
        Return strRes
    End Function
#End Region

#Region "Metodos"
    Public Shared Function GeneraLoteLoad(ByVal pstrCodigosImpuestos As String, ByVal pRegeneraLote As String, ByVal pNroLote As String, ByVal pMunicipalidad As String, _
                                          ByVal pTotalAutovaluo As String, ByVal pTotalPredial As String, ByVal pPeriodo As String) As String

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
                .TieneLote = pRegeneraLote
                .Lote = pNroLote

                .Municipalidad = pMunicipalidad
                .TotalAutovaluo = GCCUtilitario.ConvierteValorBien(pTotalAutovaluo)
                .TotalPredial = GCCUtilitario.ConvierteValorBien(pTotalPredial)
                .Periodo = pPeriodo

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

    Public Shared Function GenerarLoteLoad2(ByVal strCodigoImpuesto As String, _
                                                    ByVal strNroLote As String) As String

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .CodigosImpuesto = strCodigoImpuesto
                    .CodNroLote = strNroLote
                    .RegeneraLote = IIf(String.IsNullOrEmpty(strNroLote), "0", "1")
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)
            'Inicio IBK - AAE - Obtener el siguiente nro de lote para visualizar
            'Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteImpuestoVehicular(pEImpuestoVehicular)
            Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteImpuestoVehicular2(pEImpuestoVehicular)
            'Fin IBK
            Return strNroLoteGenerado.Trim.PadLeft(8, "0"c)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ListadoMunicipalidadPaginado(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As String
        Dim objUtilNTX As New LUtilNTX

        Dim dtUtil As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTX.ListadoMunicipalidadPaginado(10, 1, "CLAVE1", "desc", _
                                                                                                                    pCodMunicipalidad, _
                                                                                                                    pMunicipalidad))

        If dtUtil.Rows.Count > 0 Then
            txtCodMunicipalidad.Value = dtUtil.Rows(0).Item("CLAVE1").ToString
            txtMunicipalidadDesc.Value = dtUtil.Rows(0).Item("VALOR1").ToString
        End If
        Return ""

    End Function
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


    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
#End Region

End Class
