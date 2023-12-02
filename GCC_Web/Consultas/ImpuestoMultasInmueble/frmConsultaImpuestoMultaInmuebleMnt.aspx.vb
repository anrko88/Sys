Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Consultas_ImpuestoMultasInmueble_frmConsultaImpuestoMultaInmuebleMnt
    Inherits GCCBase

    Dim objLog As New GCCLog("frmConsultaImpuestoMultaInmuebleMnt.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/02/2011
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

                'Datos
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                Dim strCodUnico As String = Request.QueryString("hddCodUnico")
                Dim strCodImpuesto As String = Request.QueryString("hddCodImpuesto")
                Dim strVer As String = Request.QueryString("hddVer")

                Dim strTotalAutovaluo As String = Request.QueryString("txtTotalAutovaluo")
                Dim strTotalPredial As String = Request.QueryString("txtTotalPredial")
                Dim strPeriodo As String = Request.QueryString("txtPeriodo")


                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodUnico.Value = strCodUnico
                Me.hddCodImpuesto.Value = strCodImpuesto
                Me.hddVer.Value = strVer

                Me.txtTotalAutovaluo.InnerText = strTotalAutovaluo
                Me.txtTotalPredial.InnerText = strTotalPredial
                Me.txtPeriodo.InnerText = strPeriodo

                CargaDatosBien(strCodContrato, strCodBien)

                'GCCUtilitario.CargarComboValorGenerico(Me.txtEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                'GCCUtilitario.CargarComboValorGenerico(Me.txtEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)

                'Carga Datos Editar
                If Not strCodImpuesto Is Nothing Then
                    Me.hddTipoTx.Value = GCCConstante.C_TX_EDITAR
                    CargaEditarImpuesto(strCodContrato, strCodBien, strCodImpuesto)
                End If

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
    ''' Graba Impuesto
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaImpuesto(ByVal pstrTipoTx As String, _
                                            ByVal pstrCodContrato As String, _
                                            ByVal pstrCodBien As String, _
                                            ByVal pstrCodImpuesto As String, _
                                            ByVal pstrCodUnico As String, _
                                            ByVal pstrTotalAutovaluo As String, _
                                            ByVal pstrTotalPredial As String, _
                                            ByVal pstrPeriodo As String, _
                                            ByVal pstrCodPredio As String, _
                                            ByVal pstrAutovaluo As String, _
                                            ByVal pstrImpuestoPredial As String, _
                                            ByVal pstrArbitrio As String, _
                                            ByVal pstrMulta As String, _
                                            ByVal pstrFiscalizacion As String, _
                                            ByVal pstrTotal As String, _
                                            ByVal pstrPagoCliente As String, _
                                            ByVal pstrFechaPago As String, _
                                            ByVal pstrEstadoPago As String, _
                                            ByVal pstrFecCobro As String, _
                                            ByVal pstrEstadoCobro As String, _
                                            ByVal pstrObservacion As String) As String

        Try
            Dim objEImpuestomunicipal As New EImpuestomunicipal

            With objEImpuestomunicipal
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.NullableString(pstrCodBien)
                .Secimpuesto = GCCUtilitario.NullableString(pstrCodImpuesto)
                .Codunico = GCCUtilitario.NullableString(pstrCodUnico)
                .FecpagoStr = GCCUtilitario.NullableString(pstrFechaPago)
                .FeclimiteStr = ""
                .Municipalidad = ""
                .Monto = 0
                .Concepto = "001"
                .Observaciones = pstrObservacion
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Comision = 0
                .FechacobroStr = GCCUtilitario.NullableString(pstrFecCobro)
                .Tipodecambio = 0
                .Nrocheque = ""
                .Nropartidaregistral = ""
                .Codigomoneda = ""
                .TotalAutovaluo = GCCUtilitario.StringToDecimal(pstrTotalAutovaluo)
                .TotalPredial = GCCUtilitario.StringToDecimal(pstrTotalPredial)
                .Periodo = GCCUtilitario.NullableString(pstrPeriodo)
                .CodPredio = GCCUtilitario.NullableString(pstrCodPredio)
                .Autovaluo = GCCUtilitario.StringToDecimal(pstrAutovaluo)
                .ImpuestoPredial = GCCUtilitario.StringToDecimal(pstrImpuestoPredial)
                .Arbitrio = GCCUtilitario.StringToDecimal(pstrArbitrio)
                .Multa = GCCUtilitario.StringToDecimal(pstrMulta)
                .Fiscalizacion = GCCUtilitario.StringToDecimal(pstrFiscalizacion)
                .Total = GCCUtilitario.StringToDecimal(pstrTotal)
                .PagoCliente = GCCUtilitario.NullableString(pstrPagoCliente)
                .EstadoPago = GCCUtilitario.NullableString(pstrEstadoPago)
                .EstadoCobro = GCCUtilitario.NullableString(pstrEstadoCobro)
                .IGV = 0
                .Total2 = 0
                .Lote = ""
                .Estadologico = 1
            End With

            'Ejecuta Transaccion
            Dim objLImpuestoMunicipalTx As New LImpuestoMunicipalTx

            Dim blnResult As Boolean = False
            Dim strNumeroDemanda As String = ""
            If pstrTipoTx.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                objLImpuestoMunicipalTx.InsertarImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
                blnResult = True
            Else
                objLImpuestoMunicipalTx.ModificarImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
                blnResult = True
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

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Get Bien
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2012
    ''' </remarks>
    Protected Sub CargaDatosBien(ByVal pstrCodContrato As String, _
                                 ByVal strCodBien As String)

        Try

            Dim objImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
            Dim objSiniestroNTx As New LSiniestroNTx
            Dim msgError As String = ""

            'Inicializa Objeto Demanda
            Dim objEImpuestoMunicipal As New EImpuestomunicipal
            Dim strEImpuestoMunicipal As String
            With objEImpuestoMunicipal
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.CheckInt(strCodBien)
            End With
            strEImpuestoMunicipal = GCCUtilitario.SerializeObject(objEImpuestoMunicipal)

            'Ejecuta Consulta
            Dim dtBien As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoMunicipalNTx.GetImpuestoMunicipalBienes(strEImpuestoMunicipal))

            'Valida si existe
            If dtBien.Rows.Count > 0 Then

                Me.txtNroContrato.InnerText = dtBien.Rows(0).Item("CodSolicitudCredito").ToString
                Me.txtCUCliente.InnerText = dtBien.Rows(0).Item("CodUnico").ToString
                Me.txtTipoDocumento.InnerText = dtBien.Rows(0).Item("DesCodigoTipoDocumento").ToString
                Me.txtNroDocumento.InnerText = dtBien.Rows(0).Item("NumeroDocumento").ToString
                Me.txtRazonSocial.InnerText = dtBien.Rows(0).Item("ClienteRazonSocial").ToString

                Me.txtDepartamento.InnerText = dtBien.Rows(0).Item("DesDepartamento").ToString
                Me.txtProvincia.InnerText = dtBien.Rows(0).Item("DesProvincia").ToString
                Me.txtDistrito.InnerText = dtBien.Rows(0).Item("DesDistrito").ToString

                Me.txtUbicacion.InnerText = dtBien.Rows(0).Item("Ubicacion").ToString
                'Me.txtLote.Value = dtBien.Rows(0).Item("MontoDemanda").ToString

                Me.hddFechaTransferencia.Value = dtBien.Rows(0).Item("FechaTransferencia").ToString

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Carga Editar Impuesto
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2012
    ''' </remarks>
    Protected Sub CargaEditarImpuesto(ByVal pstrCodContrato As String, _
                                     ByVal strCodBien As String, _
                                     ByVal strCodImpuesto As String)

        Try

            Dim objImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
            Dim msgError As String = ""

            'Inicializa Objeto Demanda
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.CheckInt(strCodBien)
                .Secimpuesto = GCCUtilitario.CheckInt(strCodImpuesto)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtImpuestomunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoMunicipalNTx.GetImpuestoMunicipal(strEImpuestomunicipal))

            'Valida si existe
            If dtImpuestomunicipal.Rows.Count > 0 Then

                Me.txtLote.InnerText = dtImpuestomunicipal.Rows(0).Item("NroLote").ToString

                Me.txtTotalAutovaluo.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("TotalAutovaluo")).ToString("###,##0.00")
                Me.txtTotalPredial.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("TotalPredial")).ToString("###,##0.00")

                Me.txtPeriodo.InnerText = dtImpuestomunicipal.Rows(0).Item("Periodo").ToString
                Me.txtCodPredio.InnerText = dtImpuestomunicipal.Rows(0).Item("CodPredio").ToString
                Me.txtAutovaluo.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("Autovaluo")).ToString("###,##0.00")

                Me.txtImpuestoPredial.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("ImpuestoPredial")).ToString("###,##0.00")
                Me.txtArbitrio.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("Arbitrio")).ToString("#,###,##0.00")
                Me.txtMulta.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("Multa")).ToString("#,###,##0.00")

                'Me.txtFiscalizacion.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("Fiscalizacion")).ToString("#,###,##0.00")
                Me.txtFiscalizacion.InnerText = dtImpuestomunicipal.Rows(0).Item("Fiscalizacion")
                Me.txtTotal.InnerText = GCCUtilitario.CheckDecimal(dtImpuestomunicipal.Rows(0).Item("Total")).ToString("#,###,##0.00")

                'Me.chkPagoCliente.Value = dtImpuestomunicipal.Rows(0).Item("PagoCliente").ToString
                Dim intPagoCliente As Integer = GCCUtilitario.CheckInt(dtImpuestomunicipal.Rows(0).Item("PagoCliente").ToString)
                Me.hddPagoCliente.Value = intPagoCliente
                If intPagoCliente = 1 Then
                    Me.chkPagoCliente.Checked = True
                Else
                    Me.chkPagoCliente.Checked = False
                End If

                Me.txtFechaPago.InnerText = dtImpuestomunicipal.Rows(0).Item("FecPago").ToString
                Me.txtFecCobro.InnerText = dtImpuestomunicipal.Rows(0).Item("FechaCobro").ToString

                Me.txtEstadoPago.InnerText = dtImpuestomunicipal.Rows(0).Item("DesEstadoPago").ToString
                Me.txtEstadoCobro.InnerText = dtImpuestomunicipal.Rows(0).Item("DesEstadoCobro").ToString

                Me.txtObservacion.InnerText = dtImpuestomunicipal.Rows(0).Item("Observaciones").ToString

                Me.hddEstadoPago.Value = dtImpuestomunicipal.Rows(0).Item("EstadoPago").ToString
                Me.hddEstadoCobro.Value = dtImpuestomunicipal.Rows(0).Item("EstadoCobro").ToString

                'GCCUtilitario.SeleccionaCombo(txtEstadoPago, dtImpuestomunicipal.Rows(0).Item("EstadoPago").ToString.Trim)
                'GCCUtilitario.SeleccionaCombo(txtEstadoCobro, dtImpuestomunicipal.Rows(0).Item("EstadoCobro").ToString.Trim)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

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

#End Region

End Class
