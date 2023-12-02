
'End Class
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_ImpuestoVehicular_frmConsultaImpuestoVehicularRegistroAgregar
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultaImpuestoVehicularRegistroAgregar.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/11/2011
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

                'GCCUtilitario.CargarComboValorGenerico(ddlCuota, GCCConstante.C_TABLAGENERICA_Numero_Cuota)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
                'GCCUtilitario.CargarComboMoneda(ddlMoneda)
                hddOrigen.Value = Request.QueryString("origen")
                hddFechaTransferencia.Value = Request.QueryString("fechaT")

                If Request.QueryString("origen") = "1" Then
                    hddCodigoUnico.Value = Request.QueryString("CodUnico")
                    hddSecFinanciamiento.Value = Request.QueryString("codBien")
                    'txtNroContrato.Value = Request.QueryString("csc")
                    'txtPlaca.Value = Request.QueryString("placa")
                    'txtDescripcionInmueble.Value = Request.QueryString("desc")
                    hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO
                Else
                    Dim oLwsImpuestoVehicularNtx As New LImpuestoVehicularNTX
                    Dim dtImpuesto As DataTable

                    hddSecImpuesto.Value = Request.QueryString("codImp")
                    hddCheque.Value = Request.QueryString("cheque")
                    dtImpuesto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsImpuestoVehicularNtx.ObtenerDatosImpuesto(Request.QueryString("placa"), Convert.ToInt32(hddSecImpuesto.Value)))

                    TDNumeroContrato.InnerText = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString().Trim()
                    TDPlaca.InnerText = dtImpuesto.Rows(0).Item("Placa").ToString().Trim()
                    TDDescripcionBien.InnerText = dtImpuesto.Rows(0).Item("Descripcion").ToString().Trim()
                    TDFechaDeclaracion.InnerText = dtImpuesto.Rows(0).Item("FecDeclaracion").ToString()
                    TDPeriodo.InnerText = dtImpuesto.Rows(0).Item("Periodo").ToString().Trim()
                    TDImporte.InnerText = Convert.ToDecimal(dtImpuesto.Rows(0).Item("Importe")).ToString("#,###,##0.00")
                    TDMoneda.InnerText = dtImpuesto.Rows(0).Item("CMoneda").ToString().Trim()
                    'Dim strCodmoneda As String = dtImpuesto.Rows(0).Item("CodigoMoneda").ToString()
                    'If strCodmoneda = "001" Then
                    '    TDMoneda.InnerText = "SOLES"
                    'Else
                    '    TDMoneda.InnerText = "DOLARES"
                    'End If

                    TDCuota.InnerText = dtImpuesto.Rows(0).Item("CNroCuota").ToString.Trim()

                    'If dtImpuesto.Rows(0).Item("PagoCliente").ToString() = "1" Then
                    '    TDPagoCliente.InnerText = "Si"
                    'Else
                    '    TDPagoCliente.InnerText = "No"
                    'End If

                    TDFechaPago.InnerText = dtImpuesto.Rows(0).Item("FechaPago").ToString()
                    TDEstadoPago.InnerText = dtImpuesto.Rows(0).Item("CEstadoPago").ToString.Trim()
                    TDFechaCobro.InnerText = dtImpuesto.Rows(0).Item("FechaCobro").ToString()
                    TDEstadoCobro.InnerText = dtImpuesto.Rows(0).Item("CEstadoCobro").ToString.Trim()
                    TDObservaciones.InnerText = dtImpuesto.Rows(0).Item("Observaciones").ToString()


                    'dtImpuesto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsImpuestoVehicularNtx.ObtenerDatosImpuesto(Request.QueryString("placa"), Convert.ToInt32(hddSecImpuesto.Value)))
                    'ddlMoneda.Value = dtImpuesto.Rows(0).Item("CodigoMoneda").ToString()
                    'hddSecFinanciamiento.Value = dtImpuesto.Rows(0).Item("SecFinanciamiento").ToString()
                    'TDNumeroContrato.InnerText = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                    ''txtNroContrato.Value = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                    'hddCodigoUnico.Value = dtImpuesto.Rows(0).Item("CodUnico").ToString()
                    'txtPlaca.Value = dtImpuesto.Rows(0).Item("Placa").ToString().Trim()
                    'txtDescripcionInmueble.Value = dtImpuesto.Rows(0).Item("Descripcion").ToString().Trim()
                    'txtFechaDeclaracion.Value = dtImpuesto.Rows(0).Item("FecDeclaracion").ToString()
                    'txtPeriodo.Value = dtImpuesto.Rows(0).Item("Periodo").ToString().Trim()
                    'txtImporte.Value = Convert.ToDecimal(dtImpuesto.Rows(0).Item("Importe")).ToString("#,###,##0.00")
                    'GCCUtilitario.SeleccionaCombo(ddlCuota, dtImpuesto.Rows(0).Item("NroCuota").ToString.Trim())
                    '' hddCodigoUnico.Value = dtImpuesto.Rows(0).Item("Importe").ToString()

                    If dtImpuesto.Rows(0).Item("PagoCliente").ToString() = "1" Then
                        cbPagoCliente.Checked = True
                    Else
                        cbPagoCliente.Checked = False
                    End If
                    'txtFechaPago.Value = dtImpuesto.Rows(0).Item("FechaPago").ToString()
                    'hddEstadoPago.Value = dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim()
                    'hddEstadoCobro.Value = dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim()
                    'GCCUtilitario.SeleccionaCombo(ddlEstadoPago, dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim())
                    'txtFechaCobro.Value = dtImpuesto.Rows(0).Item("FechaCobro").ToString()
                    'GCCUtilitario.SeleccionaCombo(ddlEstadoCobro, dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim())
                    'txtObservaciones.Value = dtImpuesto.Rows(0).Item("Observaciones").ToString()
                    'hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

                End If

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

#Region "Métodos"

    <WebMethod()> _
    Public Shared Function GuardarImpuestoVehicular(ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pCodUnico As String, _
                                         ByVal pFechaDeclaracion As String, _
                                         ByVal pPeriodo As String, _
                                         ByVal pImporte As String, _
                                         ByVal pCuota As String, _
                                         ByVal pPagoCliente As String, _
                                         ByVal pEstadoPago As String, _
                                         ByVal pFechaCobro As String, _
                                         ByVal pEstadoCobro As String, _
                                         ByVal pMoneda As String, _
                                         ByVal pFechaPago As String, _
                                         ByVal pObservaciones As String)

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .Codsolcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .Codunico = pCodUnico
                    .FechaDeclaracion = GCCUtilitario.StringToDateTime(pFechaDeclaracion)
                    .Periodo = pPeriodo
                    .Monto = GCCUtilitario.StringToDecimal(pImporte)
                    .Cuota = pCuota
                    .PagoCliente = pPagoCliente
                    .EstadoPago = pEstadoPago
                    .EstadoCobro = pEstadoCobro
                    .Codigomoneda = pMoneda
                    .Observaciones = pObservaciones
                    .Fecpago = GCCUtilitario.StringToDateTime(pFechaPago)

                    .Feccobro = GCCUtilitario.StringToDateTime(pFechaCobro)


                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            Dim blnResult As Boolean = objLImpuestoVehicular.GrabarImpuestoVehicular(pEImpuestoVehicular)

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
    <WebMethod()> _
    Public Shared Function ModificarImpuestoVehicular(ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pCodImpuesto As String, _
                                         ByVal pCodUnico As String, _
                                         ByVal pFechaDeclaracion As String, _
                                         ByVal pPeriodo As String, _
                                         ByVal pImporte As String, _
                                         ByVal pCuota As String, _
                                         ByVal pPagoCliente As String, _
                                         ByVal pEstadoPago As String, _
                                         ByVal pFechaCobro As String, _
                                         ByVal pEstadoCobro As String, _
                                         ByVal pMoneda As String, _
                                         ByVal pFechaPago As String, _
                                         ByVal pEstado As String, _
                                         ByVal pObservaciones As String)

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .Codsolcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .Secimpuesto = pCodImpuesto
                    .Codunico = pCodUnico
                    .FechaDeclaracion = GCCUtilitario.StringToDateTime(pFechaDeclaracion)
                    .Periodo = pPeriodo
                    .Monto = GCCUtilitario.StringToDecimal(pImporte)
                    .Cuota = pCuota
                    .PagoCliente = pPagoCliente
                    .EstadoPago = pEstadoPago
                    .Estado = pEstado
                    .EstadoCobro = pEstadoCobro
                    .Codigomoneda = pMoneda
                    .Observaciones = pObservaciones
                    .Fecpago = GCCUtilitario.StringToDateTime(pFechaPago)
                    .Feccobro = GCCUtilitario.StringToDateTime(pFechaCobro)


                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            Dim blnResult As Boolean = objLImpuestoVehicular.ModificarImpuestoVehicular(pEImpuestoVehicular)

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
    <WebMethod()> _
    Public Shared Function ValidarCuotaPeriodo(ByVal strCodigoBien As String, _
                                         ByVal strPeriodo As String, ByVal strCodigoContrato As String)

        Try

            Dim objLImpuestoVehicularNTX As New LImpuestoVehicularNTX

            Dim dtValidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoVehicularNTX.ListarCuotasPeriodo(Convert.ToInt32(strCodigoBien), Convert.ToInt32(strPeriodo), strCodigoContrato))
            Dim StrCuotas As String = ""

            If dtValidacion.Rows.Count > 0 Then

                For Each dr As DataRow In dtValidacion.Rows
                    StrCuotas = StrCuotas.Trim() + dr("NroCuota").ToString().Trim() + ","
                Next


                If StrCuotas <> "" Then
                    Return StrCuotas.Substring(0, StrCuotas.Length - 1)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
#End Region
End Class