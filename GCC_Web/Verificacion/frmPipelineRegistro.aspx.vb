Imports System.Web.Services
Imports System.Data
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Verificacion_frmPipelineRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPipelineRegistro.aspx.vb")
    Dim mstrCodigoCotizacion As String
    Dim mstrCodEstado As String

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not IsPostBack Then
                mstrCodigoCotizacion = Request.QueryString("CodCotizacion")
                mstrCodEstado = Request.QueryString("CodEstado")
                GCCUtilitario.CargarComboValorGenerico(cmbMotivoDemora, GCCConstante.C_TABLAGENERICA_MOTIVO_PIPELINE)
                GCCUtilitario.CargarComboValorGenerico(cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_PIPELINE)
                pInicializarControles()

                'Valida Bloqueo
                ' GestionBloqueo(mstrNroContrato)

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

#Region "Métodos"

    Private Sub pInicializarControles()


        Dim oLwsPipelineNtx As New LPipelineNTX
        Dim oEPipeline As New EGCC_Pipeline
        Dim odtbDatos As New DataTable
        oEPipeline.Codigocotizacion = mstrCodigoCotizacion
        Try
            txtNroCotizacion.Value = mstrCodigoCotizacion
            odtbDatos = GCCUtilitario.DeserializeObject(Of DataTable)( _
                              oLwsPipelineNtx.ListarPipeline(10, _
                                                                1, _
                                                                "CodigoCotizacion", _
                                                                "", _
                                                                GCCUtilitario.SerializeObject(Of EGCC_Pipeline)(oEPipeline) _
                                                                          ))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtNroCotizacion.Value = dr("CodigoCotizacion").ToString
                        txtCUCliente.Value = dr("codUnico").ToString
                        txtNumeroContrato.Value = dr("CodSolicitudCredito").ToString
                        txtEjecutivoBanca.Value = dr("EjecutivoBanca").ToString
                        txtRazonsocial.Value = dr("RazonSocialNombre").ToString
                        txtEjecutivoLeasing.Value = dr("EjecutivoLeasing").ToString
                        cmbMotivoDemora.Value = dr("codigomotivopipeline").ToString
                        'Inicio IBK - AAE - 22/10/2012 - Se agregan campos de moneda, montos leasing y monto desembolasado
                        TxtMoneda.Value = dr("codMoneda").ToString
                        TxtMontoDesembolsado.Value = dr("montoDesembolsado").ToString
                        TxtMontoLeasing.Value = dr("montoLeasing").ToString
                        TxtComentario.Value = dr("comentario").ToString
                        txtTipo.Value = dr("tipoLeasing").ToString
                        txtClasificacionDelBien.Value = dr("clasificacionBien").ToString.Trim
                        txtTipoDeBien.Value = dr("tipoBien").ToString.Trim
                        txtBanca.Value = dr("banca").ToString.Trim
                        txtPrecioVenta.Value = dr("precioVenta").ToString.Trim
                        txtRiesgoNeto.Value = dr("RiesgoNeto").ToString.Trim
                        ' Fin IBK - AAE
                        If dr("codigoestadopipeline").ToString = "" Then
                            Select Case mstrCodEstado
                                Case GCCConstante.CODIGO_ESTADO_PIPELIN_EVALUACION_C : mstrCodEstado = GCCConstante.CODIGO_ESTADO_PIPELIN_EVALUACION
                                Case GCCConstante.CODIGO_ESTADO_PIPELIN_RIESGOS_C : mstrCodEstado = GCCConstante.CODIGO_ESTADO_PIPELIN_RIESGOS
                                Case GCCConstante.CODIGO_ESTADO_PIPELIN_PORDESEMBOLSAR_C : mstrCodEstado = GCCConstante.CODIGO_ESTADO_PIPELIN_PORDESEMBOLSAR
                            End Select
                        Else
                            mstrCodEstado = dr("codigoestadopipeline").ToString()
                        End If
                        cmbEstado.Value = mstrCodEstado.ToString
                        'If mstrCodEstado = "003" Then
                        '    dv_desembolso.Visible = True
                        'Else
                        '    dv_desembolso.Visible = False
                        'End If
                        Dim decAnterior As String = GCCUtilitario.CheckDecimal(dr("PorcentajeAnterior").ToString()).ToString(GCCConstante.C_FormatMiles)
                        Dim decActual As String = GCCUtilitario.CheckDecimal(dr("PorcentajeMesActual").ToString()).ToString(GCCConstante.C_FormatMiles)
                        Dim decSiguienteMeses As String = GCCUtilitario.CheckDecimal(dr("PorcentajeMesSiguiente").ToString()).ToString(GCCConstante.C_FormatMiles)
                        Dim decSiguienteAnios As String = GCCUtilitario.CheckDecimal(dr("PorcentajeAnioSiguiente").ToString()).ToString(GCCConstante.C_FormatMiles)
                         'Inicio IBK - AAE - 22/10/2012 - Si el estado es por desembolsar el valor anterior es % desembolsado
                        If decAnterior = ".00" And mstrCodEstado = GCCConstante.CODIGO_ESTADO_PIPELIN_PORDESEMBOLSAR Then
                            decAnterior = GCCUtilitario.CheckDecimal(dr("porcDesembolsado").ToString()).ToString(GCCConstante.C_FormatMiles)
                        End If
                        ' Fin IBK - AAE
					
                        txtAnterior.Value = IIf(decAnterior = ".00", "", decAnterior).ToString()
                        txtActual.Value = IIf(decActual = ".00", "", decActual).ToString()
                        txtsiguienteMeses.Value = IIf(decSiguienteMeses = ".00", "", decSiguienteMeses).ToString()
                        txtsiguienteAnios.Value = IIf(decSiguienteAnios = ".00", "", decSiguienteAnios).ToString()
                        dr = Nothing
                        Exit For
                    Next
                End If
                odtbDatos.Dispose()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            oLwsPipelineNtx = Nothing
        End Try
    End Sub
#End Region

#Region "WebMetods"
		' Inicio IBK - AAE - 22/10/2012 - Se agrega comentario al salvar el Pipeline
		
    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <param name="strCodigoCotizacion"></param>
    ''' <param name="strCodigoContrato"></param>
    ''' <param name="strCodigoMotivo"></param>
    ''' <param name="strCodigoEstado"></param>
    ''' <param name="strPorcentajeAnterior"></param>
    ''' <param name="strPorcentajeMesActual"></param>
    ''' <param name="strPorcentajeMesSiguiente"></param>
    ''' <param name="strPorcentajeAnioSiguiente"></param>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Sub GuardarPipeline(ByVal strCodigoCotizacion As String, _
                                 ByVal strCodigoContrato As String, _
                                 ByVal strCodigoMotivo As String, _
                                 ByVal strCodigoEstado As String, _
                                 ByVal strPorcentajeAnterior As String, _
                                 ByVal strPorcentajeMesActual As String, _
                                 ByVal strPorcentajeMesSiguiente As String, _
                                 ByVal strPorcentajeAnioSiguiente As String, _
                                 ByVal strComentario As String) 
                                 
        Dim oEPipeline As New EGCC_Pipeline
        Dim oLwsPipelineTX As New LPipelineTX
        Try
            If strCodigoContrato = "" Then
                strCodigoContrato = Nothing
            End If
            With oEPipeline
                .Codigocotizacion = strCodigoCotizacion
                .CodigoSolicitud = strCodigoContrato
                .CodigoMotivo = strCodigoMotivo
                .CodigoEstado = strCodigoEstado
                .PorcentajeAnterior = GCCUtilitario.StringToDecimal(strPorcentajeAnterior)
                .PorcentajeMesActual = GCCUtilitario.StringToDecimal(strPorcentajeMesActual)
                .PorcentajeMesSiguiente = GCCUtilitario.StringToDecimal(strPorcentajeMesSiguiente)
                .PorcentajeAnioSiguiente = GCCUtilitario.StringToDecimal(strPorcentajeAnioSiguiente)
                .Comentario = strComentario
            End With

            Dim blnResult As Boolean = oLwsPipelineTX.GrabarPipeline(GCCUtilitario.SerializeObject(Of EGCC_Pipeline)(oEPipeline))
        Catch ex As Exception
            Throw ex
        Finally
            oLwsPipelineTX = Nothing
        End Try
    End Sub
     ' Fin IBK - AAE
#End Region

End Class
