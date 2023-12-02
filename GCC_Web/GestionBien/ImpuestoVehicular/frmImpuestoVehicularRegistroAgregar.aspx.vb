Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_ImpuestoVehicular_frmImpuestoVehicularRegistroAgregar
    Inherits GCCBase
    Dim objLog As New GCCLog("frmImpuestoVehicularRegistroAgregar.aspx.vb")
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
                'Inicio IBK - AAE 
                Dim oESolicitudCreditoEstructuraVehiculo As New ESolicitudcreditoestructura
                Dim oLwsDocBienNtx As New LBienNTx

               
                hddPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                'Fin IBK
                GCCUtilitario.CargarComboValorGenerico(ddlCuota, GCCConstante.C_TABLAGENERICA_Numero_Cuota)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
                GCCUtilitario.CargarComboMoneda(ddlMoneda)
                hddOrigen.Value = Request.QueryString("origen")
                hddFechaTransferencia.Value = Request.QueryString("fechaT")
                hddNroLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("NroLote")), "", Request.QueryString("NroLote"))
                txtPeriodo.Value = IIf(String.IsNullOrEmpty(Request.QueryString("Periodo")), "", Request.QueryString("Periodo"))
                'Inicio IBK - AAE
                hidTengoLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("codNroLote")), "N", Request.QueryString("codNroLote"))
                'Fin IBK
                'Inicio IBK - AAE
                hidReadOnly.Value = Request.QueryString("ReadOnly")
                'Fin IBK - AAE
                If Request.QueryString("origen") = "1" Then
                    hddCodigoUnico.Value = Request.QueryString("CodUnico")
                    hddSecFinanciamiento.Value = Request.QueryString("codBien")
                    txtNroContrato.Value = Request.QueryString("csc")
                    txtPlaca.Value = Request.QueryString("placa")
                    txtDescripcionInmueble.Value = Request.QueryString("desc")
                    'Inicio IBK - AAE
                    txtFechaDeclaracion.Value = Date.Now.ToString("dd/MM/yyyy")
                    oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculo(Request.QueryString("csc"), Convert.ToInt32(Request.QueryString("codBien"))))

                    txtFechaRRPP.Value = GCCUtilitario.CheckDateString2(oESolicitudCreditoEstructuraVehiculo.FechaInscripcionRegistral.ToString(), "C")
                    txtFechaMuni.Value = GCCUtilitario.CheckDateString2(oESolicitudCreditoEstructuraVehiculo.FechaInscripcionMunicipal.ToString(), "C")

                    'Fin IBK
                    hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO
                Else
                    Dim oLwsImpuestoVehicularNtx As New LImpuestoVehicularNTX
                    Dim dtImpuesto As DataTable
                    hddSecImpuesto.Value = Request.QueryString("codImp")
                    hddCheque.Value = Request.QueryString("cheque")
                    dtImpuesto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsImpuestoVehicularNtx.ObtenerDatosImpuesto(Request.QueryString("placa"), Convert.ToInt32(hddSecImpuesto.Value)))
                    ddlMoneda.Value = dtImpuesto.Rows(0).Item("CodigoMoneda").ToString()
                    hddSecFinanciamiento.Value = dtImpuesto.Rows(0).Item("SecFinanciamiento").ToString()
                    txtNroContrato.Value = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                    hddCodigoUnico.Value = dtImpuesto.Rows(0).Item("CodUnico").ToString()
                    txtPlaca.Value = dtImpuesto.Rows(0).Item("Placa").ToString().Trim()
                    txtDescripcionInmueble.Value = dtImpuesto.Rows(0).Item("Descripcion").ToString().Trim()
                    txtFechaDeclaracion.Value = dtImpuesto.Rows(0).Item("FecDeclaracion").ToString()
                    txtPeriodo.Value = dtImpuesto.Rows(0).Item("Periodo").ToString().Trim()
                    txtImporte.Value = Convert.ToDecimal(dtImpuesto.Rows(0).Item("Importe")).ToString("#,###,##0.00")
                    GCCUtilitario.SeleccionaCombo(ddlCuota, dtImpuesto.Rows(0).Item("NroCuota").ToString.Trim())
                    ' hddCodigoUnico.Value = dtImpuesto.Rows(0).Item("Importe").ToString()

                    If dtImpuesto.Rows(0).Item("PagoCliente").ToString() = "1" Then
                        cbPagoCliente.Checked = True
                    Else
                        cbPagoCliente.Checked = False
                    End If
                    'Inicio IBK - AAE
                    If dtImpuesto.Rows(0).Item("CobroAdelantado").ToString() = "S" Then
                        cbCobroAdelantado.Checked = True
                    Else
                        cbCobroAdelantado.Checked = False
                    End If
                    If dtImpuesto.Rows(0).Item("NoComision").ToString() = "1" Then
                        cbNoComision.Checked = True
                    Else
                        cbNoComision.Checked = False
                    End If
                    oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculo(txtNroContrato.Value, Convert.ToInt32(hddSecFinanciamiento.Value)))

                    txtFechaRRPP.Value = GCCUtilitario.CheckDateString2(oESolicitudCreditoEstructuraVehiculo.FechaInscripcionRegistral.ToString(), "C")
                    txtFechaMuni.Value = GCCUtilitario.CheckDateString2(oESolicitudCreditoEstructuraVehiculo.FechaInscripcionMunicipal.ToString(), "C")

                    'Fin IBK
                    txtFechaPago.Value = dtImpuesto.Rows(0).Item("FechaPago").ToString()
                    hddEstadoPago.Value = dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim()
                    hddEstadoCobro.Value = dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim()
                    GCCUtilitario.SeleccionaCombo(ddlEstadoPago, dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim())
                    txtFechaCobro.Value = dtImpuesto.Rows(0).Item("FechaCobro").ToString()
                    GCCUtilitario.SeleccionaCombo(ddlEstadoCobro, dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim())
                    txtObservaciones.Value = dtImpuesto.Rows(0).Item("Observaciones").ToString()
                    hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR
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
    'Inicio IBK - AAE - Agrego parámetros
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
                                         ByVal pObservaciones As String, _
                                         ByVal pNroLote As String, _
                                         ByVal pCobroAdelantado As String, _
                                         ByVal pTengoLote As String, _
                                         ByVal pNoComision As String) As String

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
                    'Inicio IBK - AAE - Agrego parámetro
                    .CobroAdelantado = pCobroAdelantado
                    '.CodNroLote = IIf(String.IsNullOrEmpty(pNroLote.Trim), "", pNroLote.Trim.PadLeft(8, "0"c))
                    If pTengoLote = "N" Then
                        .CodNroLote = ""
                    Else
                        .CodNroLote = pTengoLote
                    End If
                    .NoComision = pNoComision
                    'Fin IBK

                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)
            'Inicio IBK - AAE - REtrno un string
            'Dim blnResult As Boolean = objLImpuestoVehicular.GrabarImpuestoVehicular(pEImpuestoVehicular)
            Dim strResult As String = objLImpuestoVehicular.GrabarImpuestoVehicular(pEImpuestoVehicular)

            'If blnResult Then
            '    Return "0"
            'Else
            '    Return "1"
            'End If
            Return strResult
            'Fin IBK
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    'Inicio IBK - AAE - Agrego parámetros
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
                                         ByVal pObservaciones As String, _
                                         ByVal pNroLote As String, _
                                         ByVal pCobroAdelantado As String, _
                                         ByVal pNoComision As String) As String

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
                    'Inicio IBK - AAE - Agrego parámetro
                    .CobroAdelantado = pCobroAdelantado
                    .NoComision = pNoComision
                    'Fin IBK
                    .CodNroLote = IIf(String.IsNullOrEmpty(pNroLote.Trim), "", pNroLote.Trim.PadLeft(8, "0"c))
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            'Inicio IBK - AAE - REtrno un string
            'Dim blnResult As Boolean = objLImpuestoVehicular.ModificarImpuestoVehicular(pEImpuestoVehicular)
            Dim strResult As String = objLImpuestoVehicular.ModificarImpuestoVehicular(pEImpuestoVehicular)
            'If blnResult Then
            '    Return "0"
            'Else
            '    Return "1"
            'End If
            Return strResult
            'Fin IBK
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function ValidarCuotaPeriodo(ByVal strCodigoBien As String, _
                                         ByVal strPeriodo As String, ByVal strCodigoContrato As String) As String

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
