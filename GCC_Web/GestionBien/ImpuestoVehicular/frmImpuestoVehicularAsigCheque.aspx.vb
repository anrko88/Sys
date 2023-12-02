Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_ImpuestoVehicular_frmImpuestoVehicularAsigCheque
    Inherits GCCBase
    Dim objLog As New GCCLog("frmImpuestoVehicularAsigCheque.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 30/11/2012
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
                'txtFechaPago.Value = Now.Date.ToShortDateString()
                'Inicio IBK - AAE
                txtNroLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("NroLote")), "", Request.QueryString("NroLote"))
                'Fin IbK

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
    ''' Asigna Cheque
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 04/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function AsignarCheque(ByVal pstrLote As String, _
                                            ByVal pstrNroCheque As String, _
                                            ByVal pstrFechaPago As String) As String

        Try
            Dim blnResult As Boolean = False
            pstrLote = pstrLote.Trim.PadLeft(8, "0"c)

            'Inicializa Objeto
            Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
            Dim oEMultaVehicular As New EImpuestovehicular


            'Dim strEImpuestomunicipal As String

            'With oEMultaVehicular
            '    .Codsolcredito = Nothing
            '    .Codunico = Nothing
            '    .RazonSocialNombre = Nothing
            '    .CodigoTipoDocumento = Nothing
            '    .NumeroDocumento = Nothing
            '    .AnioFabricacion = Nothing
            '    .CodNroLote = GCCUtilitario.NullableString(pstrLote)
            '    .Periodo = Nothing
            '    .Placa = Nothing
            '    .FechaInscripcionIni = Nothing
            '    .FechaInscripcionFin = Nothing
            '    .EstadoCobro = Nothing
            '    .EstadoPago = Nothing

            'End With

            'strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestovehicular)(oEMultaVehicular)

            ''Ejecuta Consulta

            'Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarImpuestoVehicular(999, _
            '                                                                                        1, _
            '                                                                                        "FecModificacion", _
            '                                                                                        "desc", _
            '                                                                                        strEImpuestomunicipal))

            ''Verifica si el Lote Existe
            'If dtImpuestoVehicular.Rows.Count > 0 Then

            '    Dim strChequeExiste As String = dtImpuestoVehicular.Rows(0).Item("NroCheque").ToString
            '    If Not strChequeExiste.Trim.Equals("") Then
            '        Return "3"
            '    Else
            oEMultaVehicular = New EImpuestovehicular
            Dim objLCobroNTx As New LCobroNTx
            Dim strFechaCobro As String = ""
            Dim strMensaje As String()
            Dim strMensajeResultado As String
            strFechaCobro = Convert.ToDateTime(Convert.ToDateTime(GCCUtilitario.CheckDateString(pstrFechaPago, "yyyy-MM-dd"))).AddDays(GCCConstante.C_DIAS_HABILES_COBRO_IMPUESTO_VEHICULAR).ToString("yyyy-MM-dd")
            strMensaje = objLCobroNTx.ValidarFeriado(strFechaCobro).Split(Convert.ToChar("*"))
            strMensajeResultado = strMensaje(1)
            If strMensajeResultado <> "" Then
                strFechaCobro = strMensaje(0)
            End If

            'For i As Integer = 0 To GCCConstante.C_DIAS_HABILES_COBRO_IMPUESTO_VEHICULAR
            '    If i = 0 Then
            '        dtmFechaCobro = GCCUtilitario.CheckDateString(pstrFechaPago, "yyyy-MM-dd")
            '    Else
            '        dtmFechaCobro = Convert.ToDateTime(dtmFechaCobro).ToString("yyyy-MM-dd")
            '    End If
            '    strMensaje = objLCobroNTx.ValidarFeriado(dtmFechaCobro).Split(Convert.ToChar("*"))
            '    strMensajeResultado = strMensaje(1)
            '    If strMensajeResultado = "" Then
            '        If i = GCCConstante.C_DIAS_HABILES_COBRO_IMPUESTO_VEHICULAR Then
            '            Exit For
            '        Else
            '            dtmFechaCobro = Convert.ToDateTime(dtmFechaCobro).AddDays(1).ToString("yyyy-MM-dd")
            '        End If

            '    Else
            '        dtmFechaCobro = strMensaje(0)
            '        If i <> 0 Then
            '            i = i - 1
            '        End If

            '    End If
            'Next
            strFechaCobro = Convert.ToDateTime(strFechaCobro).ToString("yyyyMMdd")
            With oEMultaVehicular
                .CodNroLote = GCCUtilitario.NullableString(pstrLote)
                .Nrocheque = GCCUtilitario.NullableString(pstrNroCheque)
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Feccobro = GCCUtilitario.StringToDateTime(strFechaCobro)
                .Fecpago = GCCUtilitario.StringToDateTime(pstrFechaPago)
                'Inicio IBK - AAE
                .CantDias = GCCConstante.C_DIAS_HABILES_COBRO_IMPUESTO_VEHICULAR
                'Fin IBK
            End With

            'Ejecuta Transaccion                
            Dim objLMultaVehicularTx As New LImpuestoVehicularTX
            blnResult = objLMultaVehicularTx.AsignarChequeImpuestoVehicular(GCCUtilitario.SerializeObject(oEMultaVehicular))

            'Inicio IBK - AAE - Se comenta alerta
            'Enviar Alerta
            'psEnviarAlerta(pstrLote)
            ' Fin IBK

            '    End If

            'Else
            ''No existe el LOTE
            'Return "2"
            'End If



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
    ''' Enviar Alerta
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - SCA
    ''' Fecha de Creación  : 16/01/2013
    ''' </remarks>
    Private Shared Sub psEnviarAlerta(ByVal pstrLote As String)
        Dim objEGCC_Alertas As EGCC_Alertas = Nothing
        Dim objLAlertaImpVehicularNTx As New LImpuestoVehicularNTX
        Dim dtAlertaImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLAlertaImpVehicularNTx.ListarAltertaImpuestoVehicular(pstrLote, "ImpuestoVehicular"))

        Dim objGCCBase As GCCBase = Nothing
        Dim bolResultado As Boolean = False

        For Each dr As DataRow In dtAlertaImpuestoVehicular.Rows
            objEGCC_Alertas = New EGCC_Alertas
            With objEGCC_Alertas
                .AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()
                .NroLote = pstrLote.ToString()

                .FechaActual = Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString()
                .RazonSocial = dr.Item("ClienteRazonSocial").ToString()
                .Direccion = dr.Item("ClienteDomicilioLegal").ToString()
                .Departamento = dr.Item("Departamento").ToString()
                .Provincia = dr.Item("Provincia").ToString()
                .Distrito = dr.Item("Distrito").ToString()
                .Periodo = dr.Item("Periodo").ToString()
                .NumContrato = dr.Item("CodSolicitudCredito").ToString()
                .FechaPago = dr.Item("FechaPago").ToString()
                .SimMoneda = dr.Item("SimMoneda").ToString()
                .FechaCobro = dr.Item("FechaCobro").ToString()
                .Correo = dr.Item("CorreoCliente").ToString()
                '------ Por Confirmar ---------
                .TipoCambio = GCCUtilitario.CheckDecimal(dr.Item("TipoCambio")).ToString("#,###,##0.00")
                .Monto = GCCUtilitario.CheckDecimal(dr.Item("Monto")).ToString("#,###,##0.00")
                .Comision = GCCUtilitario.CheckDecimal(dr.Item("Comision")).ToString("#,###,##0.00")
                .Igv = GCCUtilitario.CheckDecimal(dr.Item("IGV")).ToString("#,###,##0.00")
                .Importe = (CDec(.Monto) + CDec(.Comision) + CDec(.Igv)).ToString("#,###,##0.00")
                .RutaWeb = HttpContext.Current.Server.MapPath("../..").ToString()
            End With

            objGCCBase = New GCCBase()
            bolResultado = objGCCBase.EnviarMailAlertas("MailImpuestoVehicular", objEGCC_Alertas)

        Next
    End Sub
    <WebMethod()> _
  Public Shared Function ValidaLotesCheque(ByVal pstrLote As String) As String
        Try
            Dim blnResult As Boolean = False
            pstrLote = pstrLote.Trim.PadLeft(8, "0"c)

            Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
            Dim oEMultaVehicular As New EImpuestovehicular


            Dim strEImpuestomunicipal As String

            With oEMultaVehicular
                .Codsolcredito = Nothing
                .Codunico = Nothing
                .RazonSocialNombre = Nothing
                .CodigoTipoDocumento = Nothing
                .NumeroDocumento = Nothing
                .AnioFabricacion = Nothing
                .CodNroLote = GCCUtilitario.NullableString(pstrLote)
                .Periodo = Nothing
                .Placa = Nothing
                .FechaInscripcionIni = Nothing
                .FechaInscripcionFin = Nothing
                .EstadoCobro = Nothing
                .EstadoPago = Nothing

            End With

            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestovehicular)(oEMultaVehicular)

            'Ejecuta Consulta

            Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarImpuestoVehicular(999, _
                                                                                                    1, _
                                                                                                    "FecModificacion", _
                                                                                                    "desc", _
                                                                                                    strEImpuestomunicipal))

            If dtImpuestoVehicular.Rows.Count > 0 Then
                Dim strChequeExiste As String = dtImpuestoVehicular.Rows(0).Item("NroCheque").ToString
                If Not String.IsNullOrEmpty(strChequeExiste) Then
                    Return "3" ' tiene cheque
                Else
                    Return "0" 'No tiene
                End If
            Else
                Return "2" 'No existe el LOTE
            End If

        Catch ex As Exception
            Return "-1" 'Error
        End Try
    End Function

    <WebMethod()> _
   Public Shared Function ObtieneDatosLote(ByVal pstrLote As String) As EImpuestomunicipal
        Try

            Dim dtImpuestoVehicular As New DataTable
            Dim oImpuestoVehicular As New EImpuestomunicipal
            Dim objLImpuestoVehicularNTx As New LImpuestoVehicularNTX
            dtImpuestoVehicular = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoVehicularNTx.GetImpuestoVehicular(pstrLote.Trim.PadLeft(8, "0"c)))

            If dtImpuestoVehicular.Rows.Count > 0 Then
                'oImpuestoVehicular.Municipalidad = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString()), "Sin Municipalidad", dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString())
                oImpuestoVehicular.Total = GCCUtilitario.CheckDecimal(dtImpuestoVehicular.Rows(0).Item("Total").ToString())
                oImpuestoVehicular.FechacobroStr = dtImpuestoVehicular.Rows(0).Item("FechaRegistro").ToString()
            End If

            Return oImpuestoVehicular
        Catch ex As Exception
            Dim oImpuestoVehicular As New EImpuestomunicipal
            'oESolicitudcredito.MsgError = "No se pudo cargar los datos del contrato."
            Return oImpuestoVehicular
        End Try
    End Function

#End Region

End Class
