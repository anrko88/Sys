
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class OtrosConceptos_frmCobroRegistro
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmCobroRegistro.aspx.vb")

#Region "   Eventos     "

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

                If Request.QueryString("Titulo") IsNot Nothing Then
                    divTitulo.InnerText = Request.QueryString("Titulo")
                End If
                If Request.QueryString("SubTitulo") IsNot Nothing Then
                    divSubTitulo.InnerText = Request.QueryString("SubTitulo")
                End If
                hidFechaActual.Value = DateTime.Now.ToString("dd/MM/yyyy")

                pCargarQueryString()
                pInicializaPagina()

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
        If Request.QueryString("fi") IsNot Nothing Then hidFlagIndividual.Value = Request.QueryString("fi")

        If Request.QueryString("fic") IsNot Nothing Then hidFilItem.Value = Request.QueryString("fic")
        If Request.QueryString("fnl") IsNot Nothing Then hidFilNroLote.Value = Request.QueryString("fnl")
        If Request.QueryString("frs") IsNot Nothing Then hidFilRazonSocial.Value = Request.QueryString("frs")
        If Request.QueryString("fnc") IsNot Nothing Then hidFilNroContrato.Value = Request.QueryString("fnc")
        If Request.QueryString("fcu") IsNot Nothing Then hidFilCUCliente.Value = Request.QueryString("fcu")
        If Request.QueryString("fco") IsNot Nothing Then hidFilConcepto.Value = Request.QueryString("fco")
        If Request.QueryString("fec") IsNot Nothing Then hidFilEstadoCobro.Value = Request.QueryString("fec")
        If Request.QueryString("fsn") IsNot Nothing Then hidFilSortName.Value = Request.QueryString("fsn")
        If Request.QueryString("fso") IsNot Nothing Then hidFilSortOrder.Value = Request.QueryString("fso")

        If Request.QueryString("eco") IsNot Nothing Then hidEditarCons.Value = Request.QueryString("eco")
        If Request.QueryString("inr") IsNot Nothing Then hidInstancia.Value = Request.QueryString("inr")


    End Sub

    ''' <summary>
    ''' Inicializa Combos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Protected Sub pInicializaPagina()

        txtComision.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_CalculoTotal(); return false;}} else {return true}; ")
        txtComision.Attributes.Add("onblur", "fn_CalculoTotal();")
        'txtFechaCobro.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_ValidarFecha(); return false;}} else {return true}; ")
        'txtFechaCobro.Attributes.Add("onblur", "fn_SetearEstilo();")
        GCCUtilitario.CargarComboMoneda(cboMoneda)
        pCargarConceptos()
        pCargarEstadoDefecto()

        If hidOpcion.Value.Trim = GCCConstante.C_TX_NUEVO Then
            txtNroContrato.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_buscarContrato(); return false;}} else {return true}; ")
            txtNroContrato.Attributes.Add("onblur", "fn_buscarContrato();")
            txtImporte.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_CalculoComision(); return false;}} else {return true}; ")
            txtImporte.Attributes.Add("onblur", "fn_CalculoComision();")
            'Else            
            '    If hidFlagIndividual.Value = "1" Then
            '        txtImporte.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_CalculoComision(); return false;}} else {return true}; ")
            '        txtImporte.Attributes.Add("onblur", "fn_CalculoComision();")
            '    End If
        End If

    End Sub

    ''' <summary>
    ''' Carga combo de conceptos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Protected Sub pCargarConceptos()
        Dim objLCobro As New LCobroNTx
        Dim odtbConcepto As DataTable
        odtbConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobro.ListadoConcepto(GCCConstante.C_TABLAGENERICA_Concepto))
        Dim dwConcepto As New DataView(odtbConcepto)
        If hidOpcion.Value.Trim = GCCConstante.C_TX_NUEVO Then
            Dim strFiltro As String = String.Empty
            strFiltro = String.Format("CODIGO NOT IN ('{0}','{1}','{2}','{3}')", _
                                      GCCConstante.C_CONCEPTO_IMPUESTO_MUNICIPAL, _
                                      GCCConstante.C_CONCEPTO_IMPUESTO_VEHICULAR, _
                                      GCCConstante.C_CONCEPTO_INFRACCION_TRANSITO, _
                                      GCCConstante.C_CONCEPTO_MULTA_INSCRIPCION)
            dwConcepto.RowFilter = strFiltro
        End If
        GCCUtilitario.pCargarHtmlSelect(cboConcepto, dwConcepto.ToTable(), "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")
    End Sub

    ''' <summary>
    ''' Carga estado cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Sub pCargarEstadoDefecto()
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim odtbParam As DataTable

        Try
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_Estado_Cobro_LPC))
            Dim dwPara As New DataView(odtbParam)
            dwPara.RowFilter = String.Format(" VALOR3 = '1' AND CODIGO = '{0}'", GCCConstante.C_ESTADO_COBRO_ENVIADO_A_HOST)
            txtEstadoCobro.Value = dwPara(0).Item("VALOR2").ToString().ToUpper()
            hidEstadoCobro.Value = GCCConstante.C_ESTADO_COBRO_ENVIADO_A_HOST
        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub

    Public Shared Function pSetearPaginar(ByVal pdrCobros As DataRow) As String
        Dim strResultado As String = String.Empty
        strResultado = GCCUtilitario.Concatenar(pdrCobros("Id").ToString(), "|", _
                                                pdrCobros("CodSolicitudCredito").ToString(), "|", _
                                                pdrCobros("TipoRubroFinanciamiento").ToString(), "|", _
                                                pdrCobros("CodIfi").ToString(), "|", _
                                                pdrCobros("TipoRecuperacion").ToString(), "|", _
                                                pdrCobros("NumSecRecuperacion").ToString(), "|", _
                                                pdrCobros("NumSecRecupComi").ToString(), "|", _
                                                pdrCobros("CodComisionTipo").ToString())
        Return strResultado
    End Function
#End Region

#Region "   Web Metodos "

    ''' <summary>
    ''' Graba Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaCobro(ByVal pstrOpcion As String, _
                                      ByVal pstrNumeroContrato As String, _
                                      ByVal pstrTipoRubroFinanciamiento As String, _
                                      ByVal pstrCodIfi As String, _
                                      ByVal pstrTipoRecuperacion As String, _
                                      ByVal pstrNumSecRecuperacion As String, _
                                      ByVal pstrNumSecRecupComi As String, _
                                      ByVal pstrCodComisionTipo As String, _
                                      ByVal pstrCodigoMoneda As String, _
                                      ByVal pstrImporte As String, _
                                      ByVal pstrComision As String, _
                                      ByVal pstrComisionIGV As String, _
                                      ByVal pstrFechaCobro As String, _
                                      ByVal pstrObservaciones As String, _
                                      ByVal pstrFlagIndividual As String, _
                                      ByVal pstrInstancia As String, _
                                      ByVal pstrNumeroSecuencia As String, _
                                      ByVal pstrImporteIGV As String) As String

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
                .CodMoneda = pstrCodigoMoneda
                .MontoReembolso = GCCUtilitario.StringToDecimal(pstrImporte)
                .MontoComision = GCCUtilitario.StringToDecimal(pstrComision)
                .MontoIGV = GCCUtilitario.StringToDecimal(pstrComisionIGV)
                .FechaCobro = GCCUtilitario.StringToDateTime(pstrFechaCobro)
                .CodUsuario = GCCSession.CodigoUsuario
                .FlagIndividual = pstrFlagIndividual
                If pstrOpcion.Trim = GCCConstante.C_TX_NUEVO Then .FlagIndividual = "1"
                If pstrOpcion.Trim = GCCConstante.C_TX_NUEVO Then .IndicadorRegistro = GCCUtilitario.CheckInt64(pstrInstancia)
                If pstrOpcion.Trim = GCCConstante.C_TX_NUEVO Then .EstadoCobro = GCCConstante.C_ESTADO_COBRO_ENVIADO_A_HOST
                .Observaciones = pstrObservaciones
                .NumeroSecuencia = GCCUtilitario.CheckInt(pstrNumeroSecuencia)
                .MontoIGVReembolso = GCCUtilitario.ConvierteValorBien(pstrImporteIGV)
            End With

            'Ejecuta Transaccion
            Dim objLCobroTx As New LCobroTx

            Dim blnResult As Boolean = False
            If pstrOpcion.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                blnResult = objLCobroTx.fblnInsertarCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision))
            Else
                blnResult = objLCobroTx.fblnModificarCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision))
            End If

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
    ''' Graba Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function PaginarRegistro(ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pstrCUCliente As String, _
                                           ByVal pstrRazonSocial As String, _
                                           ByVal pstrNroContrato As String, _
                                           ByVal pstrNroLote As String, _
                                           ByVal pstrCodigoConcepto As String, _
                                           ByVal pstrEstadoCobro As String, _
                                           ByVal pstrItem As String, _
                                           ByVal pstrNumSecRecuperacion As String, _
                                           ByVal pstrNumSecRecupComi As String, _
                                           ByVal pstrInstancia As String) As ECreditoRecuperacionComision

        Try
            Dim strECreditoRecuperacionComision As String = String.Empty
            Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision

            With objECreditoRecuperacionComision
                .CodOperacionActiva = pstrNroContrato
                .CodComisionTipo = pstrCodigoConcepto
                .CodUsuario = pstrCUCliente
                .RazonSocial = pstrRazonSocial
                .NumeroLote = pstrNroLote
                .EstadoCobro = pstrEstadoCobro
                .Item = GCCUtilitario.CheckInt(pstrItem)
                .NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
                .NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
                .IndicadorRegistro = GCCUtilitario.CheckInt64(pstrInstancia)
            End With

            'Ejecuta Transaccion
            Dim objLCobroNTx As New LCobroNTx
            strECreditoRecuperacionComision = GCCUtilitario.SerializeObject(Of ECreditoRecuperacionComision)(objECreditoRecuperacionComision)

            Dim dtbCobro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobroNTx.ListadoCobroPaginar(pSortColumn, _
                                                                                                                        pSortOrder, _
                                                                                                                        strECreditoRecuperacionComision))
            Dim strAnterior As String = String.Empty
            Dim strActual As String = String.Empty
            Dim strSiguiente As String = String.Empty
            Dim intTotalRegistros As Integer = 0
            If dtbCobro IsNot Nothing Then
                If dtbCobro.Rows.Count > 0 Then
                    For Each dr As DataRow In dtbCobro.Rows
                        intTotalRegistros = GCCUtilitario.CheckInt(dr("TotalRegistros"))
                        If GCCUtilitario.CheckInt(dr("Id")) = GCCUtilitario.CheckInt(pstrItem) - 1 Then strAnterior = pSetearPaginar(dr)
                        If GCCUtilitario.CheckInt(dr("Id")) = GCCUtilitario.CheckInt(pstrItem) Then strActual = pSetearPaginar(dr)
                        If GCCUtilitario.CheckInt(dr("Id")) = GCCUtilitario.CheckInt(pstrItem) + 1 Then strSiguiente = pSetearPaginar(dr)
                    Next
                End If
                With objECreditoRecuperacionComision
                    .Anterior = strAnterior
                    .Actual = strActual
                    .Siguiente = strSiguiente
                    .TotalRegistros = intTotalRegistros
                End With
            End If

            Return objECreditoRecuperacionComision
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Obtener Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ObtenerCobro(ByVal pstrNroContrato As String, _
                                   ByVal pstrTipoRubroFinanciamiento As String, _
                                   ByVal pstrCodIfi As String, _
                                   ByVal pstrTipoRecuperacion As String, _
                                   ByVal pstrNumSecRecuperacion As String, _
                                   ByVal pstrNumSecRecupComi As String, _
                                   ByVal pstrCodComisionTipo As String) As ECreditoRecuperacionComision
        Dim objLCobro As New LCobroNTx
        Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision
        Dim odtbCobro As New DataTable

        Try
            With objECreditoRecuperacionComision
                .CodOperacionActiva = pstrNroContrato
                .TipoRubroFinanciamiento = pstrTipoRubroFinanciamiento
                .CodIfi = pstrCodIfi
                .TipoRecuperacion = pstrTipoRecuperacion
                .NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
                .NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
                .CodComisionTipo = pstrCodComisionTipo
            End With

            odtbCobro = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobro.ObtenerCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision)))
            If odtbCobro IsNot Nothing Then
                If odtbCobro.Rows.Count > 0 Then

                    With objECreditoRecuperacionComision
                        .CodOperacionActiva = odtbCobro.Rows(0).Item("CodSolicitudCredito").ToString
                        .TipoDocumento = odtbCobro.Rows(0).Item("TipoDocumento").ToString
                        .NumeroDocumento = odtbCobro.Rows(0).Item("NumeroDocumento").ToString
                        .EstadoContrato = odtbCobro.Rows(0).Item("EstadoContrato").ToString
                        .RazonSocial = odtbCobro.Rows(0).Item("ClienteRazonSocial").ToString
                        .CodComisionTipo = odtbCobro.Rows(0).Item("CodComisionTipo").ToString
                        .CodMoneda = odtbCobro.Rows(0).Item("CodMoneda").ToString
                        .MontoReembolso = odtbCobro.Rows(0).Item("MontoReembolso").ToString
                        .MontoComision = odtbCobro.Rows(0).Item("MontoComision").ToString
                        .MontoIGV = odtbCobro.Rows(0).Item("MontoIGV").ToString
                        .Total = odtbCobro.Rows(0).Item("Total").ToString()
                        .StringFechaCobro = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbCobro.Rows(0).Item("FechaRecuperacion").ToString()))
                        .Observaciones = odtbCobro.Rows(0).Item("Observaciones").ToString
                        .EstadoCobro = odtbCobro.Rows(0).Item("EstadoCobro").ToString
                        .EstadoRecuperacion = odtbCobro.Rows(0).Item("EstadoRecuperacion").ToString
                        .FlagIndividual = odtbCobro.Rows(0).Item("FlagIndividual").ToString
                        .StringFechaPago = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbCobro.Rows(0).Item("FecPago").ToString()))
                        .StringFechaActivacion = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbCobro.Rows(0).Item("FechaActivacion").ToString()))
                        .StringFechaVencimientoOperacion = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbCobro.Rows(0).Item("FechaVencimientoOperacion").ToString()))
                        .CantidadFraccionar = odtbCobro.Rows(0).Item("CantidadFraccion").ToString
                        .PorcenComision = odtbCobro.Rows(0).Item("PorcentajeComisionSC").ToString
                        .NumeroSecuencia = odtbCobro.Rows(0).Item("NumeroSecuencia").ToString
                        .MontoIGVReembolso = odtbCobro.Rows(0).Item("montoigvreembolso").ToString
                    End With

                End If
            End If

            Return objECreditoRecuperacionComision
        Catch ex As Exception
            Throw ex
        Finally
            odtbCobro.Dispose()
            objLCobro = Nothing
            'objECreditoRecuperacionComision = Nothing
        End Try
    End Function
#End Region

End Class
