Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Administracion_frmMantenimientoBienRegistro
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMantenimientoBienRegistro.aspx.vb")

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
                hidNumeroContrato.Value = Request.QueryString("csc")
                hidOp.Value = Request.QueryString("co")
                hidSecFinanciamiento.Value = Request.QueryString("csf")
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


#Region " Metodos "
    Private Sub pInicializarControles()
        Dim oLwsDocBienNtx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Try
            oESolicitudCreditoEstructura = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerBien(hidNumeroContrato.Value, hidSecFinanciamiento.Value))
            If oESolicitudCreditoEstructura IsNot Nothing Then
                With oESolicitudCreditoEstructura
                    txtNumeroContrato.Value = .Codsolicitudcredito
                    txtEstado.Value = .EstadoContrato
                    txtclasificacion.Value = .ClasificacionBien
                    txtcu.Value = .CodUnico
                    txtRazonSocial.Value = .RazonSocial
                    txtmoneda.Value = .Moneda
                    hidCodClasificacion.Value = .Tiporubrofinanciamiento
                    hidCodTipoBien.Value = .Codigotipobien.Trim
                    If (Array.IndexOf(GCCConstante.DestinoCredito_Inmueble, .Tiporubrofinanciamiento) <> -1) Then
                        txtCantidad.Value = .Cantidadproducto
                        txtDescripcionDemanda.Value = .DescripcionBien.Trim()
                        txtEstadoBien.Value = .EstadoBien.Trim()
                        txtUso.Value = .Uso.Trim()
                        txtUbicacion.Value = .Ubicacion.Trim()
                        hidCodDepartamento.Value = .Departamento.Trim()
                        hidCodProvincia.Value = .Provincia.Trim()
                        'txtColor1.Value = .Color.Trim()
                        hidCodDistrito.Value = .Distrito.Trim()
                        ddlMonedaBien.Value = .Monedabien.Trim()
                        txtValorBien.Value = .MontoValorBien.ToString '("#,###,##0.00")
                        txtFechaTransferencia.Value = .FechaTransferencia
                        txtObservaciones.Value = .Comentario.Trim
                        txtOficinaRegistral.Value = .OficinaRegistral.Trim()
                        txtPartidaRegistral.Value = .PartidaRegistral.Trim()
                    ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Maquinaria, .Tiporubrofinanciamiento) <> -1) Then
                        txtCantidad1.Value = .Cantidadproducto
                        txtDescripcionBien1.Value = .DescripcionBien.ToString.Trim()
                        txtEstadoBien1.Value = .EstadoBien
                        txtUso1.Value = .Uso
                        txtUbicacion1.Value = .Ubicacion
                        hidCodDepartamento1.Value = .Departamento.Trim()
                        hidCodProvincia1.Value = .Provincia.Trim()
                        hidCodDistrito1.Value = .Distrito.Trim()
                        ddlMonedaBien1.Value = .Monedabien
                        txtValorBien1.Value = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferencia1.Value = .FechaTransferencia
                        txtObservaciones1.Value = .Comentario.Trim
                        txtPlacaActual.Value = .Placa.Trim()
                        txtPlacaAnterior.Value = .PlacaAntigua.Trim()
                        txtAnio.Value = IIf(.Anio = 0, "", .Anio).ToString()
                        txtNroSerie.Value = .NroSerie.Trim()
                        txtNrMotor.Value = .NroMotor.Trim()
                        txtMarca.Value = .Marca.Trim()
                        txtModelo.Value = .Modelo.Trim()
                        txtCarroceria.Value = .Carroceria.Trim()
                        txtColor.Value = .Color.Trim
                        txtMedidas.Value = .Medidas.Trim()
                    ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Otros, .Tiporubrofinanciamiento) <> -1) Then
                        txtCantidad2.Value = .Cantidadproducto
                        txtDescripcionBien2.Value = .DescripcionBien.Trim()
                        txtEstadoBien2.Value = .EstadoBien.Trim()
                        txtUso2.Value = .Uso.Trim()
                        txtUbicacion2.Value = .Ubicacion.Trim()
                        hidCodDepartamento2.Value = .Departamento.Trim()
                        hidCodProvincia2.Value = .Provincia.Trim()
                        hidCodDistrito2.Value = .Distrito.Trim()
                        ddlMonedaBien2.Value = .Monedabien
                        txtValorBien2.Value = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferencia2.Value = .FechaTransferencia
                        txtObservaciones2.Value = .Comentario.Trim
                        txtPartidaRegistral2.Value = .PartidaRegistral.Trim()
                        txtOficinaRegistral2.Value = .OficinaRegistral.Trim()
                        txtColor2.Value = .Color.Trim()
                        txtMarca2.Value = .Marca.Trim()
                        txtModelo2.Value = .Modelo.Trim()
                    End If

                End With
            End If

        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocBienNtx = Nothing
        End Try
    End Sub

    <WebMethod()> _
    Public Shared Function ListarBienProveedor(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pNumeroContrato As String, _
                                                     ByVal pSecFinanciamiento As String) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx

        Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoBienProveedor(pNumeroContrato, pSecFinanciamiento))

        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 10, dtProveedor)

    End Function

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
                .FechaTransferencia = CDate(GCCUtilitario.StringToDateTime(pFechaTransferencia)).ToString("yyyy-MM-dd")
                .Comentario = pObservaciones
                .Color = pColor
                .Distrito = pCodDistrito
                .ValorBien = pValorBien
                .PartidaRegistral = pPartidaRegistral
                .OficinaRegistral = pOficinaRegistral
                .Monedabien = pCodMoneda
                .Flag = GCCUtilitario.CheckInt(pFlag)
                .Audusuariomodificacion = GCCSession.CodigoUsuario
            End With
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructura)

            Dim blnResult As Boolean = objLBien.fblnModificarBien(pESolicitudCreditoEstructura)

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
                .FechaTransferencia = CDate(GCCUtilitario.StringToDateTime(pFechaTransferencia)).ToString("yyyy-MM-dd")
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
