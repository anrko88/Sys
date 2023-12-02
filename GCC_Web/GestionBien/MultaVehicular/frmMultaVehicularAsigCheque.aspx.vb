Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_MultaVehicular_frmMultaVehicularAsigCheque
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMultaVehicularAsigCheque.aspx.vb")
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
                '   txtFechaPago.Value = Now.Date.ToShortDateString()
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
    ''' Fecha de Creación  : 30/11/2012
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
            Dim oEMultaVehicular As New EGCC_MultaVehicular


            Dim strEImpuestomunicipal As String

            With oEMultaVehicular
                .Codsolcredito = Nothing
                .Codunico = Nothing
                .RazonSocialNombre = Nothing
                .CodigoTipoDocumento = Nothing
                .NumeroDocumento = Nothing
                .CodTipoBien = Nothing
                .CodNroLote = GCCUtilitario.NullableString(pstrLote)
                .CodConcepto = Nothing
                .Placa = Nothing
                .CodInfraccion = Nothing
                .Infraccion = Nothing
                .CodMunicipalidad = Nothing
                .EstadoCobro = Nothing
                .EstadoPago = Nothing

            End With

            'strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)

            ''Ejecuta Consulta

            'Dim dtMultaVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarMultaVehicular(999, _
            '                                                                                        1, _
            '                                                                                        "FecModificacion", _
            '                                                                                        "desc", _
            '                                                                                        strEImpuestomunicipal))

            ''Verifica si el Lote Existe
            'If dtMultaVehicular.Rows.Count > 0 Then

            '    Dim strChequeExiste As String = dtMultaVehicular.Rows(0).Item("NroCheque").ToString
            '    If Not strChequeExiste.Trim.Equals("") Then
            '        Return "3"
            '    Else
            oEMultaVehicular = New EGCC_MultaVehicular
            Dim objLCobroNTx As New LCobroNTx
            Dim dtmFechaCobro As String = ""

            Dim strMensaje As String()
            Dim strMensajeResultado As String

            dtmFechaCobro = Convert.ToDateTime(Convert.ToDateTime(GCCUtilitario.CheckDateString(pstrFechaPago, "yyyy-MM-dd"))).AddDays(GCCConstante.C_DIAS_HABILES_COBRO_MULTA_VEHICULAR).ToString("yyyy-MM-dd")
            strMensaje = objLCobroNTx.ValidarFeriado(dtmFechaCobro).Split(Convert.ToChar("*"))
            strMensajeResultado = strMensaje(1)
            If strMensajeResultado <> "" Then
                dtmFechaCobro = strMensaje(0)
            End If

            'For i As Integer = 0 To GCCConstante.C_DIAS_HABILES_COBRO_MULTA_VEHICULAR
            '    If i = 0 Then
            '        dtmFechaCobro = GCCUtilitario.CheckDateString(pstrFechaPago, "yyyy-MM-dd")
            '    Else
            '        dtmFechaCobro = Convert.ToDateTime(dtmFechaCobro).ToString("yyyy-MM-dd")
            '    End If
            '    strMensaje = objLCobroNTx.ValidarFeriado(dtmFechaCobro).Split(Convert.ToChar("*"))
            '    strMensajeResultado = strMensaje(1)
            '    If strMensajeResultado = "" Then
            '        If i = GCCConstante.C_DIAS_HABILES_COBRO_MULTA_VEHICULAR Then
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
            dtmFechaCobro = Convert.ToDateTime(dtmFechaCobro).ToString("yyyMMdd")
            With oEMultaVehicular
                .CodNroLote = GCCUtilitario.NullableString(pstrLote)
                .Nrocheque = GCCUtilitario.NullableString(pstrNroCheque)
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Feccobro = GCCUtilitario.StringToDateTime(dtmFechaCobro)
                .Fecpago = GCCUtilitario.StringToDateTime(pstrFechaPago)
                .CantDias = GCCConstante.C_DIAS_HABILES_COBRO_MULTA_VEHICULAR
            End With

            'Ejecuta Transaccion                
            Dim objLMultaVehicularTx As New LImpuestoVehicularTX
            blnResult = objLMultaVehicularTx.AsignarChequeMultaVehicular(GCCUtilitario.SerializeObject(oEMultaVehicular))

            'Inicio IBK - AAE - Comento envío de alertas
            'Enviar Alerta
            'psEnviarAlerta(pstrLote)
            'Fin IBK

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
        Dim objEGCC_Alertas As New EGCC_Alertas
        Dim objLAlertaImpVehicularNTx As New LImpuestoVehicularNTX
        Dim dtAlertaImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLAlertaImpVehicularNTx.ListarAltertaImpuestoVehicular(pstrLote, "InformeMultas"))

        Dim objGCCBase As GCCBase = Nothing
        Dim bolResultado As Boolean = False
        Dim strTablaDetalle As StringBuilder = Nothing
        Dim strCunia As String = "XX"
        Dim decImporteTotal As Decimal = 0
        Dim strObjeto As String = "01/01/0001"

        For Each dr As DataRow In dtAlertaImpuestoVehicular.Rows
            With objEGCC_Alertas
                If strCunia <> GCCUtilitario.CheckStr(dr.Item("NumContrato")) Then

                    If strCunia <> "XX" Then
                        strTablaDetalle.Append("</table>")
                        objEGCC_Alertas.TablaDetalle = strTablaDetalle.ToString()
                        objEGCC_Alertas.Importe = decImporteTotal.ToString("#,###,##0.00")
                        objGCCBase = New GCCBase()
                        bolResultado = objGCCBase.EnviarMailAlertas("MailMultasVehicular", objEGCC_Alertas)
                        strTablaDetalle = Nothing
                    End If
                    .AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()
                    .Periodo = GCCUtilitario.CheckStr(dr.Item("Periodo"))
                    .NroLote = pstrLote.ToString()

                    .FechaActual = Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString()
                    .RazonSocial = GCCUtilitario.CheckStr(dr.Item("ClienteRazonSocial"))
                    .Direccion = GCCUtilitario.CheckStr(dr.Item("ClienteDomicilioLegal"))
                    .Departamento = GCCUtilitario.CheckStr(dr.Item("Departamento"))
                    .Provincia = GCCUtilitario.CheckStr(dr.Item("Provincia"))
                    .Distrito = GCCUtilitario.CheckStr(dr.Item("Distrito"))
                    .NumContrato = GCCUtilitario.CheckStr(dr.Item("NumContrato"))
                    .FechaPago = GCCUtilitario.CheckStr(dr.Item("FechaPago"))
                    .SimMoneda = GCCUtilitario.CheckStr(dr.Item("SimMoneda"))
                    .RutaWeb = HttpContext.Current.Server.MapPath("../..").ToString()
                    .Periodo = CDate(dr.Item("Periodo").ToString()).Year.ToString()
                    .FechaCobro = GCCUtilitario.CheckStr(dr.Item("FechaCobro"))
                    .Correo = dr.Item("CorreoCliente").ToString()

                    '------ Detalle de las Multas ---------
                    strTablaDetalle = New StringBuilder
                    strTablaDetalle.Append("<table class='tabDetalleMultas'>")
                    strTablaDetalle.Append("<tr><td style='text-align:center;'>N° de<br/>Infracción</td> <td style='text-align:center;'>Fecha Infracción</td> <td style='text-align:center;'>Tipo de<br/>Infracción</td> <td style='text-align:center;'>N° de Placa</td> <td style='text-align:center;'>Importe</td> <td style='text-align:center;'>Comisión</td> <td style='text-align:center;'>IGV</td> <td style='text-align:center;'>Importe Total<br/>a Pagar (*)</td></tr>")
                    strCunia = GCCUtilitario.CheckStr(dr.Item("NumContrato"))
                    decImporteTotal = 0
                End If
                .NroInfraccion = dr.Item("NroInfraccion").ToString()
                .FecInfraccion = dr.Item("FecInfraccion").ToString()
                .TipoInfraccion = dr.Item("TipoInfraccion").ToString()
                .NroPlaca = dr.Item("NroPlaca").ToString()

                .ImporteMulta = GCCUtilitario.CheckDecimal(dr.Item("ImporteMulta")).ToString("#,###,##0.00")
                .Comision = GCCUtilitario.CheckDecimal(dr.Item("Comision")).ToString("#,###,##0.00")
                .Igv = GCCUtilitario.CheckDecimal(dr.Item("Igv")).ToString("#,###,##0.00")
                strTablaDetalle.Append(String.Format("<tr><td style='text-align:right;'>{0}</td><td style='text-align:center;'>{1}</td><td>{2}</td><td style='text-align:center;'>{3}</td><td style='text-align:right;'>{4}</td><td style='text-align:right;'>{5}</td><td style='text-align:right;'>{6}</td><td style='text-align:right;'>{7}</td></tr>", _
                                                     .NroInfraccion.ToString(), _
                                                     .FecInfraccion.ToString(), _
                                                     .TipoInfraccion.ToString(), _
                                                     .NroPlaca.ToString(), _
                                                     .ImporteMulta.ToString(), _
                                                     .Comision.ToString(), _
                                                     .Igv.ToString(), _
                                                     (CDec(.ImporteMulta) + CDec(.Comision) + CDec(.Igv)).ToString("#,###,##0.00")))
                decImporteTotal = decImporteTotal + CDec(.ImporteMulta) + CDec(.Comision) + CDec(.Igv)
            End With
        Next


        If decImporteTotal <> 0 Then
            strTablaDetalle.Append("</table>")
            objEGCC_Alertas.TablaDetalle = strTablaDetalle.ToString()
            objEGCC_Alertas.Importe = decImporteTotal.ToString("#,###,##0.00")
            objGCCBase = New GCCBase()
            bolResultado = objGCCBase.EnviarMailAlertas("MailMultasVehicular", objEGCC_Alertas)
        End If

    End Sub
    'Inicio IBK
    <WebMethod()> _
 Public Shared Function ValidaLotesCheque(ByVal pstrLote As String) As String
        Try
            Dim blnResult As Boolean = False
            pstrLote = pstrLote.Trim.PadLeft(8, "0"c)

            Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
            Dim oEMultaVehicular As New EGCC_MultaVehicular


            Dim strEImpuestomunicipal As String

            With oEMultaVehicular
                .Codsolcredito = Nothing
                .Codunico = Nothing
                .RazonSocialNombre = Nothing
                .CodigoTipoDocumento = Nothing
                .NumeroDocumento = Nothing
                .CodTipoBien = Nothing
                .CodNroLote = GCCUtilitario.NullableString(pstrLote)
                .CodConcepto = Nothing
                .Placa = Nothing
                .CodInfraccion = Nothing
                .Infraccion = Nothing
                .CodMunicipalidad = Nothing
                .EstadoCobro = Nothing
                .EstadoPago = Nothing

            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)

            'Ejecuta Consulta
            Dim dtMultaVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarMultaVehicular(999, _
                                                                                                     1, _
                                                                                                     "FecModificacion", _
                                                                                                     "desc", _
                                                                                                     strEImpuestomunicipal))
            If dtMultaVehicular.Rows.Count > 0 Then

                Dim strChequeExiste As String = dtMultaVehicular.Rows(0).Item("NroCheque").ToString
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
            dtImpuestoVehicular = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoVehicularNTx.GetImpuestoMultas(pstrLote.Trim.PadLeft(8, "0"c)))

            If dtImpuestoVehicular.Rows.Count > 0 Then
                oImpuestoVehicular.Municipalidad = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString()), "Sin Municipalidad", dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString())
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
