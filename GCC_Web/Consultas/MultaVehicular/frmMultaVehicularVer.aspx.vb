Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_MultaVehicular_frmMultaVehicularVer
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMultaVehicularVer.aspx.vb")

#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 21/01/2013
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
                hddOrigen.Value = Request.QueryString("origen")

                Dim oLwsImpuestoVehicularNtx As New LImpuestoVehicularNTX
                Dim dtImpuesto As DataTable
                hddSecImpuesto.Value = Request.QueryString("codImp")
                tdPlaca.InnerHtml = Request.QueryString("placa")
                tdNroContrato.InnerHtml = Request.QueryString("csc")
                hddCodMunicipalidad.Value = Request.QueryString("codMuni")
                hddFechaTransferencia.Value = Request.QueryString("fechaT")
                hddEstadoPago.Value = Request.QueryString("EstPago")


                Dim strEMultaVehicular As String = String.Empty

                Dim oEMultaVehicular As New EGCC_MultaVehicular

                With oEMultaVehicular
                    .Codsolcredito = String.Empty
                    .Codunico = String.Empty
                    .RazonSocialNombre = String.Empty
                    .CodigoTipoDocumento = String.Empty
                    .NumeroDocumento = String.Empty
                    .CodTipoBien = String.Empty
                    .CodNroLote = String.Empty
                    .CodConcepto = String.Empty
                    .Placa = Request.QueryString("placa")
                    .CodInfraccion = String.Empty
                    .Infraccion = String.Empty
                    .CodMunicipalidad = String.Empty
                    .EstadoCobro = String.Empty
                    .EstadoPago = String.Empty
                    .Secimpuesto = Convert.ToInt32(hddSecImpuesto.Value)
                End With
                strEMultaVehicular = GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)


                dtImpuesto = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsImpuestoVehicularNtx.ObtenerDatosMultaConsulta(strEMultaVehicular))
                hddSecFinanciamiento.Value = dtImpuesto.Rows(0).Item("SecFinanciamiento").ToString()
                hddNumeroContrato.Value = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                tdNroContrato.InnerHtml = dtImpuesto.Rows(0).Item("CodSolicitudCredito").ToString()
                tdCUCliente.InnerHtml = dtImpuesto.Rows(0).Item("CodUnico").ToString()
                tdRazonSocial.InnerHtml = dtImpuesto.Rows(0).Item("RazonSocial").ToString()
                tdPlaca.InnerHtml = dtImpuesto.Rows(0).Item("Placa").ToString()
                tdNroLote.InnerHtml = dtImpuesto.Rows(0).Item("CodNroLote").ToString()
                tdMunicipalidad.InnerHtml = dtImpuesto.Rows(0).Item("Municipalidad").ToString()
                tdMarca.InnerHtml = dtImpuesto.Rows(0).Item("Marca").ToString()
                tdModelo.InnerHtml = dtImpuesto.Rows(0).Item("Modelo").ToString()
                tdMotor.InnerHtml = dtImpuesto.Rows(0).Item("NroMotor").ToString()
                tdNroInfraccion.InnerHtml = dtImpuesto.Rows(0).Item("NroInfraccion").ToString()
                tdFechaInfraccion.InnerHtml = dtImpuesto.Rows(0).Item("FecInfraccion").ToString()
                tdConcepto.InnerHtml = dtImpuesto.Rows(0).Item("Concepto").ToString.Trim()
                'GCCUtilitario.SeleccionaCombo(ddlConcepto, dtImpuesto.Rows(0).Item("CodConcepto").ToString.Trim())
                tdCodigoInfraccion.InnerHtml = dtImpuesto.Rows(0).Item("DesInfraccion").ToString.Trim() & " - " & dtImpuesto.Rows(0).Item("Infraccion").ToString()
                'GCCUtilitario.SeleccionaCombo(ddlCodInfraccion, dtImpuesto.Rows(0).Item("CodInfraccion").ToString.Trim())
                'tdCodigoInfraccion.InnerHtml = dtImpuesto.Rows(0).Item("Infraccion").ToString()
                tdFechaRegistro.InnerHtml = dtImpuesto.Rows(0).Item("FecIngreso").ToString()
                tdFechaRecBanco.InnerHtml = dtImpuesto.Rows(0).Item("FecRecepcionBanco").ToString()
                tdImporte.InnerHtml = Convert.ToDecimal(dtImpuesto.Rows(0).Item("Importe")).ToString("#,###,##0.00")
                tdImporteDescuento.InnerHtml = Convert.ToDecimal(dtImpuesto.Rows(0).Item("ImporteDescuento")).ToString("#,###,##0.00")
                hddCodMunicipalidad.Value = dtImpuesto.Rows(0).Item("CodMunicipalidad").ToString.Trim()
                'ddlMunicipalidad.Value = dtImpuesto.Rows(0).Item("CodMunicipalidad").ToString.Trim()
                'GCCUtilitario.SeleccionaCombo(ddlMunicipalidad, dtImpuesto.Rows(0).Item("CodMunicipalidad").ToString.Trim())
                If dtImpuesto.Rows(0).Item("PagoCliente").ToString() = "1" Then
                    cbPagoCliente.Checked = True
                Else
                    cbPagoCliente.Checked = False
                End If
                tdFechaPago.InnerHtml = dtImpuesto.Rows(0).Item("FecPago").ToString()
                tdEstadoCobro.InnerHtml = dtImpuesto.Rows(0).Item("DesEstadoCobro").ToString.Trim()
                'GCCUtilitario.SeleccionaCombo(ddlEstadoCobro, dtImpuesto.Rows(0).Item("EstadoPago").ToString.Trim())
                tdEstadoPago.InnerHtml = dtImpuesto.Rows(0).Item("DesEstadoPago").ToString.Trim()
                'GCCUtilitario.SeleccionaCombo(ddlEstadoPago, dtImpuesto.Rows(0).Item("EstadoCobro").ToString.Trim())
                tdFechaCobro.InnerHtml = dtImpuesto.Rows(0).Item("FecCobro").ToString()
                txtObservaciones.InnerHtml = dtImpuesto.Rows(0).Item("Observaciones").ToString()
                hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR

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

#Region "   Métodos     "

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function

#End Region
End Class
