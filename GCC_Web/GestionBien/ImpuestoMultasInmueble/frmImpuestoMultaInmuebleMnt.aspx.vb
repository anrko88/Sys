Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleMnt
    Inherits GCCBase

    Dim objLog As New GCCLog("frmImpuestoMultaInmuebleMnt.aspx.vb")

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
                'Inicio IBK - AAE 
                hddPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                'Fin IBK
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
                Dim strMunicipalidad As String = Request.QueryString("strMunicipalidad")
                Dim strNroLote As String = Request.QueryString("strNroLote")


                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodUnico.Value = strCodUnico
                Me.hddCodImpuesto.Value = strCodImpuesto
                Me.hddVer.Value = strVer

                Me.txtTotalAutovaluo.Value = strTotalAutovaluo
                Me.txtTotalPredial.Value = strTotalPredial
                Me.txtPeriodo.Value = strPeriodo
                Me.hddMunicipalidad.Value = strMunicipalidad
                Me.txtmuni.Value = strMunicipalidad

                Me.hddNroLote.Value = IIf(String.IsNullOrEmpty(strNroLote), "", strNroLote)
                'Inicio IBK - AAE
                hidTengoLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("codNroLote")), "N", Request.QueryString("codNroLote"))
                'Fin IBK
                'Inicio IBK - AAE
                hidReadOnly.Value = Request.QueryString("ReadOnly")
                'Fin IBK - AAE

                CargaDatosBien(strCodContrato, strCodBien)

                GCCUtilitario.CargarComboValorGenerico(Me.txtEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(Me.txtEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)

                'Carga Datos Editar
                If Not strCodImpuesto Is Nothing Then
                    Me.hddTipoTx.Value = GCCConstante.C_TX_EDITAR
                    CargaEditarImpuesto(strCodContrato, strCodBien, strCodImpuesto)
                End If

                'IBK JJM
                'Carga Codigo Predio 
                If Not strCodUnico Is Nothing And Not strCodContrato Is Nothing Then
                    CargaCodigoPredio(strCodContrato, strCodBien)
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
    ''' 'Inicio IBK - AAE - Agrego parámetros
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
                                            ByVal pstrObservacion As String, _
                                            ByVal pstrMunicipalidad As String, _
                                            ByVal pstrNroLote As String, _
                                            ByVal pCobroAdelantado As String, _
                                             ByVal pTengoLote As String, _
                                             ByVal pNoComision As String) As String

        Try
            Dim objEImpuestomunicipal As New EImpuestomunicipal

            With objEImpuestomunicipal
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.NullableString(pstrCodBien)
                .Secimpuesto = GCCUtilitario.NullableString(pstrCodImpuesto)
                .Codunico = GCCUtilitario.NullableString(pstrCodUnico)
                .FecpagoStr = GCCUtilitario.NullableString(pstrFechaPago)
                .FeclimiteStr = ""
                .Municipalidad = pstrMunicipalidad
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
                .FiscalizacionChk = GCCUtilitario.NullableString(pstrFiscalizacion)
                .Total = GCCUtilitario.StringToDecimal(pstrTotal)
                .PagoCliente = IIf(String.IsNullOrEmpty(GCCUtilitario.NullableString(pstrPagoCliente)), "0", GCCUtilitario.NullableString(pstrPagoCliente))
                .EstadoPago = IIf(String.IsNullOrEmpty(GCCUtilitario.NullableString(pstrEstadoPago)), "0", GCCUtilitario.NullableString(pstrEstadoPago))
                .EstadoCobro = IIf(String.IsNullOrEmpty(GCCUtilitario.NullableString(pstrEstadoCobro)), "", GCCUtilitario.NullableString(pstrEstadoCobro))
                .IGV = 0
                .Total2 = 0

                'Inicio IBK - AAE - Agrego parámetro
                .CobroAdelantado = pCobroAdelantado
                '.Lote = pstrNroLote
                If pTengoLote = "N" Then
                    .Lote = ""
                Else
                    .Lote = pTengoLote
                End If
                .NoComision = pNoComision
                'Fin IBK
                .Estadologico = 1
            End With

            'Ejecuta Transaccion
            Dim objLImpuestoMunicipalTx As New LImpuestoMunicipalTx
            'Inicio IBK - AAE - REtrno un string
            'Dim blnResult As Boolean = False
            Dim strResult As String = ""
            Dim strNumeroDemanda As String = ""
            If pstrTipoTx.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strResult = objLImpuestoMunicipalTx.InsertarImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
                'blnResult = True
            Else
                strResult = objLImpuestoMunicipalTx.ModificarImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
                'blnResult = True
            End If

            'Valida Resultado
            'If blnResult Then
            '    Return "1"
            'Else
            '    Return "0"
            'End If
            Return strResult
            'Fin IBK
        Catch ex As Exception
            Return "1|" + ex.ToString()
        End Try

    End Function
    <WebMethod()> _
    Public Shared Function GetImpuestoTotalesInmueble(ByVal pstrPeriodo As String, _
                                                   ByVal pstrMunicipalidad As String) As EImpuestomunicipal

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Periodo = pstrPeriodo
                .Municipalidad = pstrMunicipalidad
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.GetImpuestoTotalesInmueble(strEImpuestomunicipal))
            If dtImpMunicipal.Rows.Count > 0 Then
                oEImpuestoMunicipal.TotalPredial = GCCUtilitario.CheckDecimal(dtImpMunicipal.Rows(0).Item("TotalPredial").ToString)
                oEImpuestoMunicipal.TotalAutovaluo = GCCUtilitario.CheckDecimal(dtImpMunicipal.Rows(0).Item("TotalAutovaluo").ToString)
            End If
            Return oEImpuestoMunicipal
        Catch ex As Exception
            Return Nothing
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

                Me.txtNroContrato.Value = dtBien.Rows(0).Item("CodSolicitudCredito").ToString
                Me.txtCUCliente.Value = dtBien.Rows(0).Item("CodUnico").ToString
                Me.txtTipoDocumento.Value = dtBien.Rows(0).Item("DesCodigoTipoDocumento").ToString
                Me.txtNroDocumento.Value = dtBien.Rows(0).Item("NumeroDocumento").ToString
                Me.txtRazonSocial.Value = dtBien.Rows(0).Item("ClienteRazonSocial").ToString

                Me.txtDepartamento.Value = dtBien.Rows(0).Item("DesDepartamento").ToString
                Me.txtProvincia.Value = dtBien.Rows(0).Item("DesProvincia").ToString
                Me.txtDistrito.Value = dtBien.Rows(0).Item("DesDistrito").ToString

                Me.txtUbicacion.Value = dtBien.Rows(0).Item("Ubicacion").ToString
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

                Me.txtLote.Value = dtImpuestomunicipal.Rows(0).Item("NroLote").ToString

                Me.txtTotalAutovaluo.Value = dtImpuestomunicipal.Rows(0).Item("TotalAutovaluo").ToString
                Me.txtTotalPredial.Value = dtImpuestomunicipal.Rows(0).Item("TotalPredial").ToString

                Me.txtPeriodo.Value = dtImpuestomunicipal.Rows(0).Item("Periodo").ToString
                Me.txtCodPredio.Value = dtImpuestomunicipal.Rows(0).Item("CodPredio").ToString
                Me.txtAutovaluo.Value = dtImpuestomunicipal.Rows(0).Item("Autovaluo").ToString

                Me.txtImpuestoPredial.Value = dtImpuestomunicipal.Rows(0).Item("ImpuestoPredial").ToString
                Me.txtArbitrio.Value = dtImpuestomunicipal.Rows(0).Item("Arbitrio").ToString
                Me.txtMulta.Value = dtImpuestomunicipal.Rows(0).Item("Multa").ToString

                Me.txtFiscalizacion.Value = dtImpuestomunicipal.Rows(0).Item("Fiscalizacion").ToString
                Me.txtTotal.Value = dtImpuestomunicipal.Rows(0).Item("Total").ToString

                'Me.chkPagoCliente.Value = dtImpuestomunicipal.Rows(0).Item("PagoCliente").ToString
                Dim intPagoCliente As Integer = GCCUtilitario.CheckInt(dtImpuestomunicipal.Rows(0).Item("PagoCliente").ToString)
                Me.hddPagoCliente.Value = intPagoCliente
                If intPagoCliente = 1 Then
                    Me.chkPagoCliente.Checked = True
                Else
                    Me.chkPagoCliente.Checked = False
                End If
                'Inicio IBK - AAE
                If dtImpuestomunicipal.Rows(0).Item("CobroAdelantado").ToString() = "S" Then
                    cbCobroAdelantado.Checked = True
                Else
                    cbCobroAdelantado.Checked = False
                End If
                If dtImpuestomunicipal.Rows(0).Item("NoComision").ToString() = "1" Then
                    cbNoComision.Checked = True
                Else
                    cbNoComision.Checked = False
                End If
                'Fin IBK
                Me.txtFechaPago.Value = dtImpuestomunicipal.Rows(0).Item("FecPago").ToString
                'Me.txtEstadoPago.Value = dtImpuestomunicipal.Rows(0).Item("DesEstadoPago").ToString
                Me.txtFecCobro.Value = dtImpuestomunicipal.Rows(0).Item("FechaCobro").ToString
                'Me.txtEstadoCobro.Value = dtImpuestomunicipal.Rows(0).Item("DesEstadoCobro").ToString

                Me.txtObservacion.Value = dtImpuestomunicipal.Rows(0).Item("Observaciones").ToString

                Me.hddEstadoPago.Value = dtImpuestomunicipal.Rows(0).Item("EstadoPago").ToString
                Me.hddEstadoCobro.Value = dtImpuestomunicipal.Rows(0).Item("EstadoCobro").ToString

                GCCUtilitario.SeleccionaCombo(txtEstadoPago, dtImpuestomunicipal.Rows(0).Item("EstadoPago").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(txtEstadoCobro, dtImpuestomunicipal.Rows(0).Item("EstadoCobro").ToString.Trim)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Get Bien
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : JJM - IBK
    ''' Fecha de Creación  : 28/01/2013
    ''' </remarks>
    Protected Sub CargaCodigoPredio(ByVal pstrCodContrato As String, _
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
            Dim dtBien As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoMunicipalNTx.GetCodigoPredioBien(pstrCodContrato, strCodBien))

            'Valida si existe
            If dtBien.Rows.Count > 0 Then
                Me.txtCodPredio.Value = dtBien.Rows(0).Item("CodigoPredio").ToString
            Else
                Me.txtCodPredio.Value = ""
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
