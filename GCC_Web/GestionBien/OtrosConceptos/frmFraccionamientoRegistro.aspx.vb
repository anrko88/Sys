
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class OtrosConceptos_frmFraccionamientoRegistro
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmFraccionamientoRegistro.aspx.vb")

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
                pCargarQueryString()
                pValoresDefecto()
                pCargarEstadoDefecto()
                ObtenerCobro()
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
    ''' Obtiene valores configurados en cobro
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:12/12/2011</remarks>
    Private Sub pValoresDefecto()
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim odtbParam As DataTable

        Try
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_Configuracion_Cobro))
            Dim dwParam As New DataView(odtbParam)
            For Each dr As DataRow In odtbParam.Rows
                If dr("CODIGO") = GCCConstante.C_CONFIGURACION_COBRO_NROCUOTAFRACCIONAMIENTO Then
                    hidNroCuotas.Value = dr("VALOR2").ToString()
                    txtNroCuotas.Value = dr("VALOR2").ToString()
                End If
                If dr("CODIGO") = GCCConstante.C_CONFIGURACION_COBRO_TASADEFECTO Then hidTasaDefecto.Value = GCCUtilitario.CheckDecimal(dr("VALOR2").ToString())
                If dr("CODIGO") = GCCConstante.C_CONFIGURACION_COBRO_DIASANIO Then hidDiaAnio.Value = GCCUtilitario.CheckDecimal(dr("VALOR2").ToString())
            Next
        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub

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

    End Sub

    ''' <summary>
    ''' Obtener Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Private Sub ObtenerCobro()
        Dim objLCobro As New LCobroNTx
        Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision
        Dim odtbCobro As New DataTable

        Try
            With objECreditoRecuperacionComision
                .CodOperacionActiva = hidCodSolicitudCredito.Value
                .TipoRubroFinanciamiento = hidTipoRubroFinanciamiento.Value
                .CodIfi = hidCodIfi.Value
                .TipoRecuperacion = hidTipoRecuperacion.Value
                .NumSecRecuperacion = GCCUtilitario.CheckInt(hidNumSecRecuperacion.Value)
                .NumSecRecupComi = GCCUtilitario.CheckInt(hidNumSecRecupComi.Value)
                .CodComisionTipo = hidCodComisionTipo.Value
            End With

            odtbCobro = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobro.ObtenerCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision)))
            If odtbCobro IsNot Nothing Then
                If odtbCobro.Rows.Count > 0 Then

                    With odtbCobro.Rows(0)
                        txtImporte.Value = GCCUtilitario.CheckDecimal(.Item("MontoReembolso").ToString).ToString(GCCConstante.C_FormatMiles)
                        hidFechaCobro.Value = GCCUtilitario.CheckDate(.Item("FechaRecuperacion").ToString()).ToString("dd/MM/yyyy")
                        hidCodMoneda.Value = .Item("CodMoneda").ToString()
                        hidTea.Value = .Item("PorcentajeTEA").ToString()
                        txtMoneda.Value = .Item("NombreMoneda").ToString()
                        hidIGV.Value = GCCUtilitario.CheckDecimal(.Item("ValorIGV").ToString())

                        hidFechaActivacion.Value = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbCobro.Rows(0).Item("FechaActivacion").ToString()))
                        hidFechaVencmiento.Value = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbCobro.Rows(0).Item("FechaVencimientoOperacion").ToString()))
                    End With
                End If
            End If


        Catch ex As Exception
            Throw ex
        Finally
            odtbCobro.Dispose()
            objLCobro = Nothing

        End Try
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
            hidEstadoDefecto.Value = GCCUtilitario.Concatenar(GCCConstante.C_ESTADO_COBRO_ENVIADO_A_HOST, "|", dwPara(0).Item("VALOR2").ToString().ToUpper())
        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub

#End Region

#Region "   Web Metodos "

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
            Dim blnResult As Boolean = objLCobroTx.fblnInsertarFraccionarCobro(pListEFraccionarCobro)

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
#End Region

End Class
