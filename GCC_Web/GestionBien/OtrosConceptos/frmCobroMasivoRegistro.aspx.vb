Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_OtrosConceptos_frmCobroMasivoRegistro
    Inherits GCCBase
    Dim objLog As New GCCLog("frmCobroMasivoRegistro.aspx.vb")


#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
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
                pCargarQueryString()
                pCargarInstancia()
                InicializaCombos()
                pValoresDefecto()
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

#Region "   Web Métodos "
    ''' <summary>
    ''' Devuelve el conjunto de cobros asociados a contratos hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pstrCUCliente">Código único cliente</param>
    ''' <param name="pstrRazonSocial">Razón social</param>
    ''' <param name="pstrNroContrato">Número de contrato</param>
    ''' <param name="pstrNroLote">Número de lote</param>
    ''' <param name="pstrCodigoConcepto">Código de concepto</param>
    ''' <param name="pstrEstadoCobro">Código tipo de documento</param>
    ''' <param name="pstrFlagIndividual">Flag individual</param>
    ''' <param name="pstrFlagRegistro">Flag registro</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarCobros(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pstrCUCliente As String, _
                                           ByVal pstrRazonSocial As String, _
                                           ByVal pstrNroContrato As String, _
                                           ByVal pstrNroLote As String, _
                                           ByVal pstrCodigoConcepto As String, _
                                           ByVal pstrEstadoCobro As String, _
                                           ByVal pstrFlagIndividual As String, _
                                           ByVal pstrFlagRegistro As String, _
                                           ByVal pstrInstancia As String) As JQGridJsonResponse
        Dim objCobroNTx As New LCobroNTx
        Dim strECreditoRecuperacionComision As String = String.Empty
        Dim objCreditoRecuperacionComision As New ECreditoRecuperacionComision

        With objCreditoRecuperacionComision
            .CodOperacionActiva = pstrNroContrato
            .RazonSocial = pstrRazonSocial
            .NumeroLote = pstrNroLote
            .CodComisionTipo = pstrCodigoConcepto
            .CodigoUnico = pstrCUCliente
            .EstadoCobro = pstrEstadoCobro
            .FlagIndividual = pstrFlagIndividual
            .FlagRegistro = pstrFlagRegistro
            .IndicadorRegistro = GCCUtilitario.CheckInt64(pstrInstancia)
        End With

        strECreditoRecuperacionComision = GCCUtilitario.SerializeObject(Of ECreditoRecuperacionComision)(objCreditoRecuperacionComision)

        Dim dtbCobro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.ListadoCobro(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            strECreditoRecuperacionComision))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtbCobro.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtbCobro.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtbCobro.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtbCobro)

    End Function

    ''' <summary>
    ''' Elimina Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminarCobro(ByVal pstrNumeroContrato As String, _
                                      ByVal pstrTipoRubroFinanciamiento As String, _
                                      ByVal pstrCodIfi As String, _
                                      ByVal pstrTipoRecuperacion As String, _
                                      ByVal pstrNumSecRecuperacion As String, _
                                      ByVal pstrNumSecRecupComi As String, _
                                      ByVal pstrCodComisionTipo As String) As String

        Try
            Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision

            With objECreditoRecuperacionComision
                .CodOperacionActiva = pstrNumeroContrato
                .TipoRubroFinanciamiento = pstrTipoRubroFinanciamiento
                .CodIfi = pstrCodIfi
                .TipoRecuperacion = pstrTipoRecuperacion
                .NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
                .NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
                .CodComisionTipo = pstrCodComisionTipo
            End With

            'Ejecuta Transaccion
            Dim objLCobroTx As New LCobroTx

            Dim blnResult As Boolean = False
            blnResult = objLCobroTx.fblnEliminarCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision))
            

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
    ''' Devuelve el conjunto de cobros asociados a contratos hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pstrNroContrato">Número de contrato</param>
    ''' <param name="pstrCodigoConcepto">Razón social</param>
    ''' <param name="pstrNumSecRecuperacion">Número de contrato</param>
    ''' <param name="pstrNumSecRecupComi">Número de lote</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 06/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarCobroEditar(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pstrNroContrato As String, _
                                           ByVal pstrCodigoConcepto As String, _
                                           ByVal pstrNumSecRecuperacion As String, _
                                           ByVal pstrNumSecRecupComi As String) As JQGridJsonResponse
        Dim objCobroNTx As New LCobroNTx
        Dim strECreditoRecuperacionComision As String = String.Empty
        Dim objCreditoRecuperacionComision As New ECreditoRecuperacionComision

        With objCreditoRecuperacionComision
            .CodOperacionActiva = pstrNroContrato
            '.NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
            '.NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
            '.CodComisionTipo = pstrCodigoConcepto
        End With

        strECreditoRecuperacionComision = GCCUtilitario.SerializeObject(Of ECreditoRecuperacionComision)(objCreditoRecuperacionComision)

        Dim dtbCobro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.ListadoCobro(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            strECreditoRecuperacionComision))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtbCobro.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtbCobro.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtbCobro.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtbCobro)

    End Function

    ''' <summary>
    ''' Listar Fraccionamiento
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoFraccionamiento(ByVal pstrNumeroContrato As String, _
                                      ByVal pstrTipoRubroFinanciamiento As String, _
                                      ByVal pstrCodIfi As String, _
                                      ByVal pstrTipoRecuperacion As String, _
                                      ByVal pstrNumSecRecuperacion As String, _
                                      ByVal pstrNumSecRecupComi As String, _
                                      ByVal pstrCodComisionTipo As String) As JQGridJsonResponse

        Dim objCobroNTx As New LCobroNTx
        Dim objEFraccionarCobro As New EGCC_FraccionarCobro

        With objEFraccionarCobro
            .CodOperacionActiva = pstrNumeroContrato
            .TipoRubroFinanciamiento = pstrTipoRubroFinanciamiento
            .CodIfi = pstrCodIfi
            .TipoRecuperacion = pstrTipoRecuperacion
            .NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
            .NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
            .CodComisionTipo = pstrCodComisionTipo
        End With

        Dim strEFraccionarCobro As String = GCCUtilitario.SerializeObject(Of EGCC_FraccionarCobro)(objEFraccionarCobro)
        Dim dtbFraccionamiento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.ListadoFraccionamiento(strEFraccionarCobro))

        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 20, dtbFraccionamiento)

    End Function

    ''' <summary>
    ''' Graba Fraccionamiento
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaFraccionamiento(ByVal pstrNumeroContrato As String, _
                                              ByVal pstrTipoRubroFinanciamiento As String, _
                                              ByVal pstrCodIfi As String, _
                                              ByVal pstrTipoRecuperacion As String, _
                                              ByVal pstrNumSecRecuperacion As String, _
                                              ByVal pstrNumSecRecupComi As String, _
                                              ByVal pstrCodComisionTipo As String, _
                                              ByVal pstrRegistro As String) As String

        Try

            Dim oListEFraccionarCobro As New ListEGCC_FraccionarCobro
            Dim pListEFraccionarCobro As String = String.Empty

            Dim arrFraccionar As String() = pstrRegistro.Split(New Char() {"|"c})

            For i As Integer = 0 To arrFraccionar.Length - 1
                If arrFraccionar(i) <> "" Then
                    Dim arrDatos As String() = arrFraccionar(i).Split(New Char() {"*"c})

                    Dim oEFraccionar As New EGCC_FraccionarCobro
                    With oEFraccionar
                        .CodOperacionActiva = pstrNumeroContrato
                        .TipoRubroFinanciamiento = pstrTipoRubroFinanciamiento
                        .CodIfi = pstrCodIfi
                        .TipoRecuperacion = pstrTipoRecuperacion
                        .NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
                        .NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
                        .CodComisionTipo = pstrCodComisionTipo
                        .NroCuota = GCCUtilitario.CheckInt(arrDatos(0))
                        .FechaCobro = GCCUtilitario.CheckDate(arrDatos(1))
                        .MontoFraccionar = GCCUtilitario.CheckDecimal(arrDatos(2))
                        .MontoComision = GCCUtilitario.CheckDecimal(arrDatos(3))
                        .MontoIGVComision = GCCUtilitario.CheckDecimal(arrDatos(4))
                        .MontoInteres = GCCUtilitario.CheckDecimal(arrDatos(5))
                        .MontoTotal = GCCUtilitario.CheckDecimal(arrDatos(6))
                        .CodigoEstado = arrDatos(7)
                        .Dias = GCCUtilitario.CheckInt(arrDatos(8))
                        .UsuarioRegistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    End With
                    oListEFraccionarCobro.Add(oEFraccionar)
                End If
            Next i

            pListEFraccionarCobro = GCCUtilitario.SerializeObject(oListEFraccionarCobro)

            'Ejecuta Transaccion
            Dim objLCobroTx As New LCobroTx
            Dim blnResult As Boolean = objLCobroTx.fblnModificarFraccionarCobro(pListEFraccionarCobro)

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
    ''' Enviar Carta
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - SCA
    ''' Fecha de Creación  : 15/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Sub EnviarCarta(ByVal pstrRazonSocial As String, _
                                  ByVal pstrDireccion As String, _
                                  ByVal pstrDepartamento As String, _
                                  ByVal pstrProvincia As String, _
                                  ByVal pstrDistrito As String, _
                                  ByVal pstrConcepto As String, _
                                  ByVal pstrNumContrato As String, _
                                  ByVal pstrFechaPago As String, _
                                  ByVal pstrSimMoneda As String, _
                                  ByVal pstrImporte As String, _
                                  ByVal pstrTipoCambio As String, _
                                  ByVal pstrMonto As String, _
                                  ByVal pstrComision As String, _
                                  ByVal pstrIgv As String, _
                                  ByVal pstrFechaCobro As String, _
                                  ByVal pstrCodComisionTipo As String, _
                                  ByVal pstrNroLote As String, _
                                  ByVal pstrCorreoCliente As String)

        Dim objEGCC_Alertas As New EGCC_Alertas
        'Dim objLAlertaImpVehicularNTx As New LImpuestoVehicularNTX
        'Dim dtAlertaImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLAlertaImpVehicularNTx.ListarAltertaImpuestoVehicular(pstrLote, "ImpuestoVehicular"))

        Dim objGCCBase As GCCBase = Nothing
        'Dim bolResultado As Boolean = False

        'For Each dr As DataRow In dtAlertaImpuestoVehicular.Rows

        With objEGCC_Alertas
            .AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()
            .NroLote = pstrNroLote.ToString()
            .TipoConcepto = pstrCodComisionTipo.ToString() 'Guarda el tipo de Concepto

            .FechaActual = Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString()
            .RazonSocial = pstrRazonSocial.ToString()
            .Direccion = pstrDireccion.ToString()
            .Departamento = pstrDepartamento.ToString()
            .Provincia = pstrProvincia.ToString()
            .Distrito = pstrDistrito.ToString()
            .Periodo = pstrConcepto.ToString() ' se reutiliza para el Concepto
            .NumContrato = pstrNumContrato.ToString()
            '.FechaPago = pstrFechaPago.ToString()
            .FechaPago = IIf(String.IsNullOrEmpty(pstrFechaPago), pstrFechaCobro, pstrFechaPago).ToString()
            .FechaPago = IIf(pstrFechaPago = "-", pstrFechaCobro, pstrFechaPago).ToString()
            .SimMoneda = pstrSimMoneda.ToString()
            .FechaCobro = pstrFechaCobro.ToString()
            .Correo = pstrCorreoCliente.ToString()
            '------ Por Confirmar ---------
            .TipoCambio = pstrTipoCambio.ToString() 'CDec(pstrTipoCambio).ToString("#,###,##0.00")
            .Monto = pstrMonto.ToString() 'CDec(pstrMonto).ToString("#,###,##0.00")
            .Comision = pstrComision.ToString() 'CDec(pstrComision).ToString("#,###,##0.00")
            .Igv = pstrIgv.ToString() 'CDec(pstrIgv).ToString("#,###,##0.00")
            .Importe = pstrImporte.ToString() 'CDec(pstrImporte).ToString("#,###,##0.00") '(CDec(.Monto) + CDec(.Comision) + CDec(.Igv)).ToString() 'CDec(pstrImporte).ToString("#,###,##0.00")
            .RutaWeb = HttpContext.Current.Server.MapPath("../..").ToString()
        End With

        objGCCBase = New GCCBase()
        Dim bolResultado As Boolean = objGCCBase.EnviarMailAlertas("MailConceptoCobros", objEGCC_Alertas)
        'Next

    End Sub


#End Region

#Region "   Métodos     "

    ''' <summary>
    ''' Función que Carga lo parametros de la url
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:27/10/2011</remarks>
    Private Sub pCargarQueryString()
        If Request.QueryString("co") IsNot Nothing Then hidOpcion.Value = Request.QueryString("co")
        If Request.QueryString("csc") IsNot Nothing Then hidCodSolicitudCredito.Value = Request.QueryString("csc")
        If Request.QueryString("trf") IsNot Nothing Then hidTipoRubroFinanciamiento.Value = Request.QueryString("trf")
        If Request.QueryString("ci") IsNot Nothing Then hidCodIfi.Value = Request.QueryString("ci")
        If Request.QueryString("tre") IsNot Nothing Then hidTipoRecuperacion.Value = Request.QueryString("tre")
        If Request.QueryString("nsr") IsNot Nothing Then hidNumSecRecuperacion.Value = Request.QueryString("nsr")
        If Request.QueryString("nsrc") IsNot Nothing Then hidNumSecRecupComi.Value = Request.QueryString("nsrc")
        If Request.QueryString("cct") IsNot Nothing Then hidCodComisionTipo.Value = Request.QueryString("cct")
        If Request.QueryString("ere") IsNot Nothing Then hidEstadoRecuperacion.Value = Request.QueryString("ere")
    End Sub

    ''' <summary>
    ''' Inicializa Combos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Protected Sub InicializaCombos()
        'pCargarEstado()
        'pCargarConceptos()
    End Sub

    '''' <summary>
    '''' Carga combo de conceptos
    '''' </summary>    
    '''' <remarks>
    '''' Creado Por         : TSF - WCR
    '''' Fecha de Creación  : 27/11/2012
    '''' </remarks>
    'Protected Sub pCargarConceptos()
    '    Dim objLCobro As New LCobroNTx
    '    Dim odtbConcepto As DataTable
    '    odtbConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobro.ListadoConcepto(GCCConstante.C_TABLAGENERICA_Concepto))
    '    GCCUtilitario.pCargarHtmlSelect(cmbConcepto, odtbConcepto, "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")
    'End Sub

    ''' <summary>
    ''' Devuelve instancia para el registro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/12/2012
    ''' </remarks>
    Public Sub pCargarInstancia()
        Dim objLCobro As New LCobroNTx
        Try
            If hidOpcion.Value = GCCConstante.C_TX_NUEVO Then
                hidInstancia.Value = objLCobro.InstanciaRegistro(GCCSession.CodigoUsuario)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objLCobro = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene valores configurados en cobro
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:12/12/2011</remarks>
    Private Sub pValoresDefecto()
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim odtbParam As DataTable

        Try
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_Configuracion_Cobro))
            For Each dr As DataRow In odtbParam.Rows
                If dr("CODIGO") = GCCConstante.C_CONFIGURACION_COBRO_TASADEFECTO Then hidTasaDefecto.Value = GCCUtilitario.CheckDecimal(dr("VALOR2").ToString())
                If dr("CODIGO") = GCCConstante.C_CONFIGURACION_COBRO_DIASANIO Then hidDiaAnio.Value = GCCUtilitario.CheckDecimal(dr("VALOR2").ToString())
            Next

            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_Impuestos_LPC))
            For Each dr As DataRow In odtbParam.Rows
                If dr("CODIGO") = GCCConstante.C_IMPUESTOS_LPC_IGV Then
                    hidIGV.Value = GCCUtilitario.CheckDecimal(dr("VALOR3").ToString())
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub
#End Region

End Class

