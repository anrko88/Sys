Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_MultaVehicular_frmMultaVehicularRegistroAgregar
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
                hddPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                'Fin IBK
                GCCUtilitario.CargarComboValorGenericoInfraccion(ddlCodInfraccion, GCCConstante.C_TABLAGENERICA_MultaVehiculAR_Infraccion)
                GCCUtilitario.CargarComboValorGenerico(ddlConcepto, GCCConstante.C_TABLAGENERICA_MultaVehicular_Concepto)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
                GCCUtilitario.CargarMunicipalidad(ddlMunicipalidad)
                hddOrigen.Value = Request.QueryString("origen")

                Dim oLwsImpuestoVehicularNtx As New LImpuestoVehicularNTX
                Dim dtImpuesto As DataTable
                hddSecImpuesto.Value = Request.QueryString("codImp")
                txtPlaca.Value = Request.QueryString("placa")
                txtNroContrato.Value = Request.QueryString("csc")
                hddCodMunicipalidad.Value = Request.QueryString("codMuni")
                hddFechaTransferencia.Value = Request.QueryString("fechaT")
                hddEstadoPago.Value = Request.QueryString("EstPago")
                hddNroLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("strNroLote")), "", Request.QueryString("strNroLote"))
                'If (Not String.IsNullOrEmpty(hddCodMunicipalidad.Value.Trim)) Then
                '    txtMunicipalidadDesc.Value = hddCodMunicipalidad.Value.Trim
                'End If
                GCCUtilitario.SeleccionaCombo(ddlMunicipalidad, IIf(String.IsNullOrEmpty(hddCodMunicipalidad.Value.Trim), "0", hddCodMunicipalidad.Value.Trim))
                'Inicio IBK - AAE
                hidTengoLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("codNroLote")), "N", Request.QueryString("codNroLote"))
                'Fin IBK
                'Inicio IBK - AAE
                hidReadOnly.Value = Request.QueryString("ReadOnly")
                'Fin IBK - AAE
                If hddOrigen.Value = "1" Then
                    dtImpuesto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsImpuestoVehicularNtx.ObtenerDatosMulta(txtPlaca.Value, 0))
                    txtNroContrato.Value = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                    txtCUCliente.Value = dtImpuesto.Rows(0).Item("CodUnico").ToString()
                    txtRazonSocialNombre.Value = dtImpuesto.Rows(0).Item("RazonSocial").ToString()
                    txtPlaca.Value = dtImpuesto.Rows(0).Item("Placa").ToString()
                    txtMunicipalidad.Value = dtImpuesto.Rows(0).Item("Municipalidad").ToString()
                    txtMarca.Value = dtImpuesto.Rows(0).Item("Marca").ToString()
                    txtModelo.Value = dtImpuesto.Rows(0).Item("Modelo").ToString()
                    txtNroMotor.Value = dtImpuesto.Rows(0).Item("NroMotor").ToString()
                    hddSecFinanciamiento.Value = dtImpuesto.Rows(0).Item("SecFinanciamiento").ToString()
                    hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO
                    txtFechaRegistro.Value = Date.Now.ToString("dd/MM/yyyy")
                Else
                    dtImpuesto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsImpuestoVehicularNtx.ObtenerDatosMulta(txtPlaca.Value, Convert.ToInt32(hddSecImpuesto.Value)))
                    hddSecFinanciamiento.Value = dtImpuesto.Rows(0).Item("SecFinanciamiento").ToString()
                    txtNroContrato.Value = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                    txtCUCliente.Value = dtImpuesto.Rows(0).Item("CodUnico").ToString()
                    txtRazonSocialNombre.Value = dtImpuesto.Rows(0).Item("RazonSocial").ToString()
                    txtPlaca.Value = dtImpuesto.Rows(0).Item("Placa").ToString()
                    txtNroLote.Value = dtImpuesto.Rows(0).Item("CodNroLote").ToString()
                    txtMunicipalidad.Value = dtImpuesto.Rows(0).Item("Municipalidad").ToString()
                    txtMarca.Value = dtImpuesto.Rows(0).Item("Marca").ToString()
                    txtModelo.Value = dtImpuesto.Rows(0).Item("Modelo").ToString()
                    txtNroMotor.Value = dtImpuesto.Rows(0).Item("NroMotor").ToString()
                    txtNroInfraccion.Value = dtImpuesto.Rows(0).Item("NroInfraccion").ToString()
                    txtFechaInfraccion.Value = dtImpuesto.Rows(0).Item("FecInfraccion").ToString()
                    ddlConcepto.Value = dtImpuesto.Rows(0).Item("CodConcepto").ToString.Trim()
                    'GCCUtilitario.SeleccionaCombo(ddlConcepto, dtImpuesto.Rows(0).Item("CodConcepto").ToString.Trim())
                    ddlCodInfraccion.Value = dtImpuesto.Rows(0).Item("CodInfraccion").ToString.Trim()
                    'GCCUtilitario.SeleccionaCombo(ddlCodInfraccion, dtImpuesto.Rows(0).Item("CodInfraccion").ToString.Trim())
                    txtCodigoInfraccion.Value = dtImpuesto.Rows(0).Item("Infraccion").ToString()
                    txtFechaRegistro.Value = dtImpuesto.Rows(0).Item("FecIngreso").ToString()
                    txtFechaRecBanco.Value = dtImpuesto.Rows(0).Item("FecRecepcionBanco").ToString()
                    txtImporte.Value = Convert.ToDecimal(dtImpuesto.Rows(0).Item("Importe")).ToString("#,###,##0.00")
                    txtImporteDescuento.Value = Convert.ToDecimal(dtImpuesto.Rows(0).Item("ImporteDescuento")).ToString("#,###,##0.00")
                    hddCodMunicipalidad.Value = dtImpuesto.Rows(0).Item("CodMunicipalidad").ToString.Trim()
                    'ddlMunicipalidad.Value = dtImpuesto.Rows(0).Item("CodMunicipalidad").ToString.Trim()
                    'GCCUtilitario.SeleccionaCombo(ddlMunicipalidad, dtImpuesto.Rows(0).Item("CodMunicipalidad").ToString.Trim())
                    txtFechaNotLeasing.Value = dtImpuesto.Rows(0).Item("FechaNotifLeasing").ToString()
                    txtFecVenc.Value = dtImpuesto.Rows(0).Item("FechaVencimiento").ToString()
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
                    'Fin IBK
                    txtFechaPago.Value = dtImpuesto.Rows(0).Item("FecPago").ToString()
                    ddlEstadoCobro.Value = dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim()
                    'GCCUtilitario.SeleccionaCombo(ddlEstadoCobro, dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim())
                    ddlEstadoPago.Value = dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim()
                    'GCCUtilitario.SeleccionaCombo(ddlEstadoPago, dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim())
                    txtFechaCobro.Value = dtImpuesto.Rows(0).Item("FecCobro").ToString()
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

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
    'Inicio IBK - AAE - Agrego parámetros
    <WebMethod()> _
    Public Shared Function GuardarMultaVehicular(ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pNroInfraccion As String, _
                                         ByVal pFechaInfraccion As String, _
                                         ByVal pCodConcepto As String, _
                                         ByVal pCodInfraccion As String, _
                                         ByVal pInfraccion As String, _
                                         ByVal pFecIngreso As String, _
                                         ByVal pFecRecepcionBanco As String, _
                                         ByVal pImporte As String, _
                                         ByVal pImporteDcto As String, _
                                         ByVal pCodMunicipalidad As String, _
                                         ByVal pPagoCliente As String, _
                                         ByVal pEstadoPago As String, _
                                         ByVal pFechaCobro As String, _
                                         ByVal pEstadoCobro As String, _
                                         ByVal pFechaPago As String, _
                                         ByVal pObservaciones As String, _
                                         ByVal pLote As String, _
                                         ByVal pCobroAdelantado As String, _
                                         ByVal pTengoLote As String, _
                                         ByVal pNoComision As String, _
                                         ByVal pFechaNotLeasing As String) As String

        Try
            Dim objEMultaVehicular As New EGCC_MultaVehicular
            Dim objLMultaVehicular As New LImpuestoVehicularTX
            Dim pEMultaVehicular As String
            Dim decImpDto As Decimal = 0
            If objEMultaVehicular IsNot Nothing Then
                With objEMultaVehicular
                    .Codsolcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .NroInfraccion = pNroInfraccion
                    .FechaInfraccion = GCCUtilitario.StringToDateTime(pFechaInfraccion)
                    .CodConcepto = pCodConcepto
                    .CodInfraccion = pCodInfraccion
                    .Infraccion = pInfraccion
                    .FechaIngreso = GCCUtilitario.StringToDateTime(pFecIngreso)
                    .FechaRecepcionBanco = GCCUtilitario.StringToDateTime(pFecRecepcionBanco)
                    .Monto = GCCUtilitario.StringToDecimal(pImporte)
                    If IsNumeric(pImporteDcto) And Convert.ToDecimal(GCCUtilitario.StringToDecimal(pImporteDcto)) > 0 Then
                        .ImporteDescuento = GCCUtilitario.StringToDecimal(pImporteDcto)
                    Else
                        .ImporteDescuento = GCCUtilitario.StringToDecimal(pImporte)
                    End If

                    .CodMunicipalidad = pCodMunicipalidad
                    .PagoCliente = pPagoCliente
                    .EstadoPago = pEstadoPago
                    .EstadoCobro = pEstadoCobro
                    .Observaciones = pObservaciones
                    .Fecpago = GCCUtilitario.StringToDateTime(pFechaPago)
                    .Feccobro = GCCUtilitario.StringToDateTime(pFechaCobro)
                    'Inicio IBK - AAE - Agrego parámetro
                    .FechaNotificacionLeasing = GCCUtilitario.StringToDateTime(pFechaNotLeasing)
                    .CobroAdelantado = pCobroAdelantado
                    '.CodNroLote = IIf(String.IsNullOrEmpty(pNroLote.Trim), "", pNroLote.Trim.PadLeft(8, "0"c))
                    If pTengoLote = "N" Then
                        .CodNroLote = ""
                    Else
                        .CodNroLote = pTengoLote
                    End If
                    .NoComision = pNoComision
                    '.CodNroLote = pLote
                    'Fin IBK
                End With
            End If
            pEMultaVehicular = GCCUtilitario.SerializeObject(objEMultaVehicular)
            'Inicio IBK - AAE - REtrno un string
            'Dim blnResult As Boolean = objLMultaVehicular.GrabarMultaVehicular(pEMultaVehicular)
            Dim strResult As String = objLMultaVehicular.GrabarMultaVehicular(pEMultaVehicular)
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
    Public Shared Function ModificarMultaVehicular(ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pSecMulta As String, _
                                         ByVal pNroInfraccion As String, _
                                         ByVal pFechaInfraccion As String, _
                                         ByVal pCodConcepto As String, _
                                         ByVal pCodInfraccion As String, _
                                         ByVal pInfraccion As String, _
                                         ByVal pFecIngreso As String, _
                                         ByVal pFecRecepcionBanco As String, _
                                         ByVal pImporte As String, _
                                         ByVal pImporteDcto As String, _
                                         ByVal pCodMunicipalidad As String, _
                                         ByVal pPagoCliente As String, _
                                         ByVal pEstadoPago As String, _
                                         ByVal pFechaCobro As String, _
                                         ByVal pEstadoCobro As String, _
                                         ByVal pFechaPago As String, _
                                         ByVal pObservaciones As String, _
                                         ByVal pNroLote As String, _
                                         ByVal pCobroAdelantado As String, _
                                         ByVal pNoComision As String, _
                                         ByVal pFechaNotLeasing As String) As String

        Try
            Dim objEMultaVehicular As New EGCC_MultaVehicular
            Dim objLMultaVehicular As New LImpuestoVehicularTX
            Dim pEMultaVehicular As String
            If objEMultaVehicular IsNot Nothing Then
                With objEMultaVehicular
                    .Codsolcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .Secimpuesto = pSecMulta
                    .NroInfraccion = pNroInfraccion
                    .FechaInfraccion = GCCUtilitario.StringToDateTime(pFechaInfraccion)
                    .CodConcepto = pCodConcepto
                    .CodInfraccion = pCodInfraccion
                    .Infraccion = pInfraccion
                    .FechaIngreso = GCCUtilitario.StringToDateTime(pFecIngreso)
                    .FechaRecepcionBanco = GCCUtilitario.StringToDateTime(pFecRecepcionBanco)
                    .Monto = GCCUtilitario.StringToDecimal(pImporte)
                    If IsNumeric(pImporteDcto) And Convert.ToDecimal(GCCUtilitario.StringToDecimal(pImporteDcto)) > 0 Then
                        .ImporteDescuento = GCCUtilitario.StringToDecimal(pImporteDcto)
                    Else
                        .ImporteDescuento = GCCUtilitario.StringToDecimal(pImporte)
                    End If
                    '.ImporteDescuento = GCCUtilitario.StringToDecimal(pImporteDcto)
                    .CodMunicipalidad = pCodMunicipalidad
                    .PagoCliente = pPagoCliente
                    .EstadoPago = pEstadoPago
                    .EstadoCobro = pEstadoCobro
                    .Observaciones = pObservaciones
                    .Fecpago = GCCUtilitario.StringToDateTime(pFechaPago)
                    .Feccobro = GCCUtilitario.StringToDateTime(pFechaCobro)
                    'Inicio IBK - AAE - Agrego parámetro
                    .FechaNotificacionLeasing = GCCUtilitario.StringToDateTime(pFechaNotLeasing)
                    .CobroAdelantado = pCobroAdelantado
                    .NoComision = pNoComision
                    'Fin IBK
                    .CodNroLote = pNroLote
                End With
            End If
            pEMultaVehicular = GCCUtilitario.SerializeObject(objEMultaVehicular)
            'Inicio IBK - AAE - REtrno un string
            'Dim blnResult As Boolean = objLMultaVehicular.ModificarMultaVehicular(pEMultaVehicular)
            Dim strResult As String = objLMultaVehicular.ModificarMultaVehicular(pEMultaVehicular)

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
        Public Shared Function ValidarInfraccion(ByVal strCodigoInfraccion As String, ByVal strInfraccion As String, ByVal dtmFechaRecepcion As String) As String

        Try
            Dim objMultaVehicularNTx As New LImpuestoVehicularNTX
            Dim strDescuentos As String

            Dim dtMatrizInfraccion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objMultaVehicularNTx.ListarEscalaInfraccionesMulta())

            'METODO PARA CALCULAR LOS DIAS HABILES

            Dim strFechaInfraccion As String = ""
            Dim strFechaInfraccion2 As String = ""

            Dim objLCobroNTx As New LCobroNTx
            Dim strMensaje As String()
            Dim strMensajeResultado As String
            Dim dtmFechaInfraccion

            If Convert.ToDateTime(dtmFechaRecepcion).ToString("yyyy-MM-dd") > Date.Now.ToString("yyyy-MM-dd") Then
                strDescuentos = ""
            Else
                For i As Integer = 0 To GCCConstante.C_DIAS_HABILES_MULTA_VEHICULAR
                    If i = 0 Then
                        strFechaInfraccion = Convert.ToDateTime(dtmFechaRecepcion).ToString("yyyy-MM-dd")
                    Else
                        strFechaInfraccion = Convert.ToDateTime(strFechaInfraccion).ToString("yyyy-MM-dd")
                    End If
                    strMensaje = objLCobroNTx.ValidarFeriado(strFechaInfraccion).Split(Convert.ToChar("*"))
                    strMensajeResultado = strMensaje(1)
                    If strMensajeResultado = "" Then
                        If i = GCCConstante.C_DIAS_HABILES_MULTA_VEHICULAR Then
                            Exit For
                        Else
                            strFechaInfraccion = Convert.ToDateTime(strFechaInfraccion).AddDays(1).ToString("yyyy-MM-dd")
                        End If

                    Else
                        strFechaInfraccion = strMensaje(0)
                        If i <> 0 Then
                            i = i - 1
                        End If

                    End If
                Next

                dtmFechaInfraccion = Convert.ToDateTime(strFechaInfraccion).ToString("dd/MM/yyyy")

                For Each o As DataRow In dtMatrizInfraccion.Rows

                    If strDescuentos = "" Then

                        If strCodigoInfraccion = o.Item("CodInfraccion").ToString().Trim() Then
                            Dim strCodigos() As String
                            strCodigos = o.Item("codigos").ToString().Trim().Split(Convert.ToChar(","))

                            For i As Integer = 0 To strCodigos.Length - 1

                                If strCodigos(i) = strInfraccion.Trim() Then
                                    'If convert.ToDateTime(dtmFechaRecepcion).ToString("dd/MM/yyyy")>Date.Now.ToString("dd/MM/yyyy")Then
                                    'strDescuentos=""
                                    'Exit For
                                    'End If

                                    If Date.Now.ToString("dd/MM/yyyy") < dtmFechaInfraccion Then
                                        strDescuentos = o.Item("primerdescuento").ToString().Trim()
                                        Exit For
                                    ElseIf Date.Now.ToString("dd/MM/yyyy") = dtmFechaInfraccion Then
                                        strDescuentos = o.Item("segundodescuento").ToString().Trim()
                                        Exit For
                                    ElseIf Date.Now.ToString("dd/MM/yyyy") > dtmFechaInfraccion Then
                                        strDescuentos = ""
                                        Exit For
                                    Else
                                        strDescuentos = ""
                                    End If

                                End If

                            Next

                        End If

                    End If
                Next
            End If
            If strDescuentos = "" Then
                Return ""
            Else
                Return Convert.ToDecimal(strDescuentos).ToString("#,###,##0.00")
            End If

        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    <WebMethod()> _
        Public Shared Function ValidarInfraccionTotal(ByVal strCodigoInfraccion As String, ByVal strInfraccion As String) As String

        Try
            Dim objMultaVehicularNTx As New LImpuestoVehicularNTX
            Dim strDescuentos As String

            Dim dtMatrizInfraccion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objMultaVehicularNTx.ListarEscalaInfraccionesMulta())

            'METODO PARA CALCULAR LOS DIAS HABILES

            Dim strFechaInfraccion As String = ""

            For Each o As DataRow In dtMatrizInfraccion.Rows

                If strDescuentos = "" Then
                    If strCodigoInfraccion = o.Item("CodInfraccion").ToString().Trim() Then
                        Dim strCodigos() As String
                        strCodigos = o.Item("codigos").ToString().Trim().Split(Convert.ToChar(","))

                        For i As Integer = 0 To strCodigos.Length - 1

                            If strCodigos(i) = strInfraccion.Trim() Then
                                strDescuentos = o.Item("MontoTotal").ToString().Trim()
                                Exit For
                            Else
                                strDescuentos = ""
                            End If

                        Next

                    End If
                End If
            Next
            If strDescuentos = "" Then
                Return ""
            Else
                Return Convert.ToDecimal(strDescuentos).ToString("#,###,##0.00")
            End If

        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
#End Region
End Class
