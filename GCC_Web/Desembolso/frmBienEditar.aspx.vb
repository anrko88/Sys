Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Desembolso_frmBienEditar
    Inherits GCCBase

    Dim objLog As New GCCLog("frmBienEditar.aspx.vb")

#Region "Eventos"

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
                Me.hddCodclasibien.Value = Request.QueryString("Codclasibien")
                Me.hddCodContrato.Value = Request.QueryString("CodContrato") '"002"
                Me.hddSecFinanciamiento.Value = Request.QueryString("SecFinanciamiento")

                'Mantenimiento de Bienes
                GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbTipoBien, GCCConstante.C_TABLAGENERICA_TIPO_BIEN, Me.hddCodclasibien.Value)
                'Mantenimiento de Vehículo
                GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbTipoBien1, GCCConstante.C_TABLAGENERICA_TIPO_BIEN, Me.hddCodclasibien.Value)
                'Mantenimiento Otros 
                GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbTipoBien2, GCCConstante.C_TABLAGENERICA_TIPO_BIEN, Me.hddCodclasibien.Value)
                GCCUtilitario.CargarDepartamento(ddlDepartamento1)
                GCCUtilitario.CargarDepartamento(ddlDepartamento)
                GCCUtilitario.CargarDepartamento(ddlDepartamento2)
                GCCUtilitario.CargarComboMoneda(ddlMonedaBien)
                GCCUtilitario.CargarComboMoneda(ddlMonedaBien1)
                GCCUtilitario.CargarComboMoneda(ddlMonedaBien2)

                pInicializarControles()
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
        Dim oLwsDocBienNtx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Try
            oESolicitudCreditoEstructura = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerBien(hddCodContrato.Value, hddSecFinanciamiento.Value))
            If oESolicitudCreditoEstructura IsNot Nothing Then
                With oESolicitudCreditoEstructura
                    Me.hddCodContrato.Value = .Codsolicitudcredito
                    'txtEstado.Value = .EstadoContrato
                    txtClasificacionBien0.Value = .ClasificacionBien
                    txtClasificacionBien1.Value = .ClasificacionBien
                    txtClasificacionBien.Value = .ClasificacionBien
                    'txtcu.Value = .CodUnico
                    'txtRazonSocial.Value = .RazonSocial
                    'txtmoneda.Value = .Moneda
                    'hidCodClasificacion.Value = .Tiporubrofinanciamiento
                    'Me.hidCodTipoBien.Value = .Codigotipobien.Trim
                    hidCodTipoBien.Value = .Codigotipobien.Trim()
                    If (Array.IndexOf(GCCConstante.DestinoCredito_Inmueble, .Tiporubrofinanciamiento) <> -1) Then
                        txtCantidad.Value = .Cantidadproducto
                        txtDescripcionDemanda.Value = .DescripcionBien.Trim()
                        txtEstadoBien.Value = .EstadoBien
                        txtUso.Value = .Uso.Trim()
                        txtUbicacion.Value = .Ubicacion.Trim()
                        hidCodDepartamento.Value = .Departamento.Trim()
                        hidCodProvincia.Value = .Provincia.Trim()
                        hidCodDistrito.Value = .Distrito.Trim()
                        ddlMonedaBien.Value = .Monedabien
                        txtValorBien.Value = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferencia.Value = IIf(.FechaTransferencia = "01/01/1900", "", .FechaTransferencia)
                        txtObservaciones.Value = .Comentario.Trim
                        txtOficinaRegistral.Value = .OficinaRegistral.Trim()
                        txtPartidaRegistral.Value = .PartidaRegistral.Trim()
                        ' txtColor.Value = .Color
                        GCCUtilitario.SeleccionaCombo(Me.cmbTipoBien, oESolicitudCreditoEstructura.Codigotipobien.Trim)

                    ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Maquinaria, .Tiporubrofinanciamiento) <> -1 Or Array.IndexOf(GCCConstante.DestinoCredito_Vehiculo, .Tiporubrofinanciamiento) <> -1) Then
                        'If (Me.hddCodclasibien.Value = "006") Then
                        txtCantidad1.Value = .Cantidadproducto
                        txtDescripcionBien1.Value = .DescripcionBien.Trim()
                        txtEstadoBien1.Value = .EstadoBien
                        txtUso1.Value = .Uso.Trim()
                        txtUbicacion1.Value = .Ubicacion.Trim()
                        hidCodDepartamento1.Value = .Departamento.Trim()
                        hidCodProvincia1.Value = .Provincia.Trim()
                        hidCodDistrito1.Value = .Distrito.Trim()
                        ddlMonedaBien1.Value = .Monedabien
                        txtValorBien1.Value = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferencia1.Value = IIf(.FechaTransferencia = "01/01/1900", "", .FechaTransferencia)
                        txtObservaciones1.Value = .Comentario.Trim
                        txtPlacaActual.Value = .PlacaAntigua
                        txtPlacaAnterior.Value = .Placa
                        txtAnio.Value = .Anio.ToString()
                        txtNroSerie.Value = .NroSerie
                        txtNrMotor.Value = .NroMotor
                        txtMarca.Value = .Marca
                        txtModelo.Value = .Modelo
                        txtCarroceria.Value = .Carroceria
                        txtColor1.Value = .Color.Trim
                        txtMedidas.Value = .Medidas

                        GCCUtilitario.SeleccionaCombo(Me.cmbTipoBien1, oESolicitudCreditoEstructura.Codigotipobien.Trim)

                    ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Otros, .Tiporubrofinanciamiento) <> -1) Then
                        txtCantidad2.Value = .Cantidadproducto
                        txtDescripcionBien2.Value = .DescripcionBien.Trim()
                        txtEstadoBien2.Value = .EstadoBien
                        txtUso2.Value = .Uso.Trim()
                        txtUbicacion2.Value = .Ubicacion.Trim()
                        hidCodDepartamento2.Value = .Departamento.Trim()
                        hidCodProvincia2.Value = .Provincia.Trim()
                        hidCodDistrito2.Value = .Distrito.Trim()
                        ddlMonedaBien2.Value = .Monedabien
                        txtValorBien2.Value = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferencia2.Value = IIf(.FechaTransferencia = "01/01/1900", "", .FechaTransferencia)
                        txtObservaciones2.Value = .Comentario.Trim
                        txtPartidaRegistral2.Value = .PartidaRegistral
                        txtOficinaRegistral2.Value = .OficinaRegistral
                        txtMarca2.Value = .Marca
                        txtColor2.Value = .Color
                        txtModelo2.Value = .Modelo
                     
                        GCCUtilitario.SeleccionaCombo(Me.cmbTipoBien2, oESolicitudCreditoEstructura.Codigotipobien.Trim)
                    End If

                End With
            End If

        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocBienNtx = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' GuardarBien
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pSecFinanciamiento"></param>
    ''' <param name="pCodigoTipoBien"></param>
    ''' <param name="pFechaTransferencia"></param>
    ''' <param name="pObservaciones"></param>
    ''' <param name="pColor"></param>
    ''' <param name="pFlag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GuardarBien(ByVal pNumeroContrato As String, _
                                         ByVal pSecFinanciamiento As String, _
                                         ByVal pCodigoTipoBien As String, _
                                         ByVal pFechaTransferencia As String, _
                                         ByVal pObservaciones As String, _
                                         ByVal pColor As String, _
                                         ByVal pCodDistrito As String, _
                                         ByVal pValorBien As String, _
                                         ByVal pPartidaRegistral As String, _
                                         ByVal pOficinaRegistral As String, _
                                         ByVal pCodMoneda As String, _
                                         ByVal pFlag As String) As String
        Try
            Dim objESolicitudCreditoEstructura As New ESolicitudcreditoestructura
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoEstructura As String

            With objESolicitudCreditoEstructura
                .Codsolicitudcredito = pNumeroContrato
                .Secfinanciamiento = pSecFinanciamiento
                .Codigotipobien = pCodigoTipoBien
                .FechaTransferencia = GCCUtilitario.StringToDateTime(pFechaTransferencia)
                .Comentario = pObservaciones
                .Color = pColor
                .Distrito = pCodDistrito
                .ValorBien = pValorBien
                .PartidaRegistral = pPartidaRegistral
                .OficinaRegistral = pOficinaRegistral
                .Monedabien = pCodMoneda
                .Flag = GCCUtilitario.CheckInt(pFlag)
                .Audusuariomodificacion = GCCSession.CodigoUsuario
                .Codigoestadobien = "001"
            End With
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructura)

            Dim blnResult As Boolean = objLBien.fblnModificarBienDesembolso(pESolicitudCreditoEstructura)

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
    Public Shared Function GuardarMaquinaria(ByVal pNumeroContrato As String, _
                                         ByVal pSecFinanciamiento As String, _
                                         ByVal pCodigoTipoBien As String, _
                                         ByVal pFechaTransferencia As String, _
                                         ByVal pObservaciones As String, _
                                         ByVal pColor As String, _
                                         ByVal pCodDistrito As String, _
                                         ByVal pValorBien As String, _
                                         ByVal pPlacaActual As String, _
                                         ByVal pPlacaAnterior As String, _
                                         ByVal pAnioFabricacion As Integer, _
                                         ByVal pNroMotor As String, _
                                         ByVal pCarroceria As String, _
                                         ByVal pMedidas As String, _
                                         ByVal pCodMoneda As String, _
                                         ByVal pFlag As String) As String
        Try
            Dim objESolicitudCreditoEstructura As New ESolicitudcreditoestructura
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoEstructura As String

            With objESolicitudCreditoEstructura
                .Codsolicitudcredito = pNumeroContrato
                .Secfinanciamiento = pSecFinanciamiento
                .Codigotipobien = pCodigoTipoBien
                .FechaTransferencia = GCCUtilitario.StringToDateTime(pFechaTransferencia)
                .Comentario = pObservaciones
                .Color = pColor
                .Distrito = pCodDistrito
                .ValorBien = pValorBien
                .Placa = pPlacaActual
                .PlacaAntigua = pPlacaAnterior
                .Anio = pAnioFabricacion
                .NroMotor = pNroMotor
                .Carroceria = pCarroceria
                .Medidas = pMedidas
                .Monedabien = pCodMoneda
                .Flag = GCCUtilitario.CheckInt(pFlag)
                .Audusuariomodificacion = GCCSession.CodigoUsuario
            End With
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructura)

            Dim blnResult As Boolean = objLBien.fblnModificarMaquinaria(pESolicitudCreditoEstructura)

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
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
#End Region

End Class
