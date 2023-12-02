Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_frmConsultasBienDetalle
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultasBienDetalle.aspx.vb")

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
                hidFlagOrigen.Value = Request.QueryString("flag")
                HidCodEstado.Value = Request.QueryString("codestado")

                If Not String.IsNullOrEmpty(Request.QueryString("precioventa")) Then
                    Session.Add("PrecioVenta", Request.QueryString("precioventa"))
                End If

                If Not String.IsNullOrEmpty(Session.Item("PrecioVenta")) Then
                    hidPrecioVenta.Value = Session.Item("PrecioVenta")
                Else
                    Session.RemoveAt("PrecioVenta")
                End If

                ' hidTotal.Value = Request.QueryString("total")

                'Response.Write(hidNumeroContrato.Value)
                'Response.Write(hidOp.Value)
                'Response.Write(hidFlagOrigen.Value)
                'Response.Write(HidCodEstado.Value)


                'Inmuebles
                'GCCUtilitario.CargarDepartamento(ddlDepartamentoInmueble)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoBien, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaBien)
                'GCCUtilitario.CargarComboValorGenerico(ddlNotariaInmueble, GCCConstante.C_TABLAGENERICA_NOTARIA_PUBLICA)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionRRPPInmueble, GCCConstante.C_TABLAGENERICA_Estado_Inscripción_RRPP)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoMunicipalInmueble, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstaRegistroBienInmueble, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                'GCCUtilitario.CargarComboValorGenerico(ddlPropiedadInmueble, GCCConstante.C_TABLAGENERICA_Estado_Transferencia)

                'Maquinarias
                'GCCUtilitario.CargarDepartamento(ddlDepartamentoMaquinaria)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaMaquinaria)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienMaquinaria, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoRegistroBienMaquinaria, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                'GCCUtilitario.CargarComboValorGenerico(ddlTipoCarroceriaMaquinaria, GCCConstante.C_TABLAGENERICA_Tipo_Carroceria)
                'Vehiculos
                'GCCUtilitario.CargarDepartamento(ddlDepartamentoVehiculo)
                'GCCUtilitario.CargarComboValorGenerico(ddlTraccionVehivulo, GCCConstante.C_TABLAGENERICA_Traccion)
                'GCCUtilitario.CargarComboValorGenerico(ddlTransmisionVehivulo, GCCConstante.C_TABLAGENERICA_Transmision)
                'GCCUtilitario.CargarComboValorGenerico(ddlTipoMotorVehivulo, GCCConstante.C_TABLAGENERICA_Tipo_Motor)
                'GCCUtilitario.CargarComboValorGenerico(ddlCombustibleVehivulo, GCCConstante.C_TABLAGENERICA_Tipo_Combustible)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienVehiculo, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaVehiculo)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoMunicipalVehiculo, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionRRPPVehiculo, GCCConstante.C_TABLAGENERICA_Estado_Inscripción_RRPP)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoRegistroBienVehiculo, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                ' GCCUtilitario.CargarComboValorGenerico(ddlTipoCarroceriaVehiculo, GCCConstante.C_TABLAGENERICA_Tipo_Carroceria)
                'CCUtilitario.CargarComboValorGenerico(ddlClaseVehivulo, GCCConstante.C_TABLAGENERICA_Clase)
                'sistemas y otros
                'GCCUtilitario.CargarDepartamento(ddlDepartamentoOtros)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaOtros)
                'GCCUtilitario.CargarComboValorGenerico(ddlBienOtros, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoRegistroBienOtros, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                'GCCUtilitario.CargarDepartamento(ddlDepartamento)
                'GCCUtilitario.CargarDepartamento(ddlDepartamento2)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaBien)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaBien1)
                'GCCUtilitario.CargarComboMoneda(ddlMonedaBien2)
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
    'IBK-JJM
    Private Sub pObtieneRegistroMaximo()
        Dim oESolicitudCreditoEstructuraVehiculoNew As New ESolicitudcreditoestructura
        Dim oLwsDocBienNtx As New LBienNTx
        oESolicitudCreditoEstructuraVehiculoNew = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerMaxFilas(hidNumeroContrato.Value, HidCodEstado.Value))
        hidMaxSecFinanciamiento.Value = IIf(String.IsNullOrEmpty(oESolicitudCreditoEstructuraVehiculoNew.Secfinanciamiento), "0", oESolicitudCreditoEstructuraVehiculoNew.Secfinanciamiento)

    End Sub
    'IBK-Fin
    Private Sub pInicializarControles()
        Dim oLwsDocBienNtx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraInmueble As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraMaquinaria As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraVehiculo As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraSistema As New ESolicitudcreditoestructura
        Try
            'DATOS DE CONTRATO Y CLIENTE
            oESolicitudCreditoEstructura = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosBienContrato(hidNumeroContrato.Value))
            If oESolicitudCreditoEstructura IsNot Nothing Then
                With oESolicitudCreditoEstructura
                    txtNumeroContrato.InnerText = .Codsolicitudcredito
                    txtEstado.InnerText = .EstadoContrato
                    txtclasificacion.InnerText = .ClasificacionBien
                    txtTipoBien.InnerText = .TipoBien
                    txtmoneda.InnerText = .Moneda
                    txtEjecutivo.InnerText = .EjecutivoBanca
                    txtBanca.InnerText = .Banca
                    txtKardex.InnerText = .Kardex
                    txtObservacionContrato.Value = .ObservacionContrato
                    txtcu.InnerText = .CodUnico
                    txtRazonSocial.InnerText = .RazonSocial
                    txtTipoDocumento.InnerText = .TipoDocumento
                    txtNumDocumento.InnerText = .NumeroDocumento
                    hidCodClasificacion.Value = .Tiporubrofinanciamiento
                    hidCodTipoBien.Value = .Codigotipobien.Trim
                End With
            End If


            If (Array.IndexOf(GCCConstante.DestinoCredito_Inmueble, hidCodClasificacion.Value) <> -1) Then
                'DATOS DEL INMUEBLE
                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraInmueble = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosInmueblesConsulta(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraInmueble IsNot Nothing Then
                    With oESolicitudCreditoEstructuraInmueble
                        ddlDepartamentoInmueble.InnerText = IIf(.Departamento = "", "", .Departamento.Trim)
                        ddlProvinciaInmueble.InnerText = IIf(.Provincia = "", "", .Provincia.Trim)
                        ddlDistritoInmueble.InnerText = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtUsoInmueble.InnerText = IIf(.Uso = "", "", .Uso.Trim)
                        txtUbicacionInmueble.InnerText = IIf(.Ubicacion = "", "", .Ubicacion.Trim)
                        txtCantidadInmueble.InnerText = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim)
                        txtDescripcionInmueble.Value = IIf(.Comentario = "", "", .Comentario.Trim)
                        ddlEstaRegistroBienInmueble.InnerText = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlEstadoBien.InnerText = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        ddlTipoBien.InnerText = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        ddlMonedaBien.InnerText = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorInmueble.InnerText = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaInscripcionMunicipalInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString(), "C")
                        txtFechaInscripcionRegistralInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString(), "C")
                        txtOficinaRegistralInmueble.InnerText = IIf(.OficinaRegistral = "", "", .OficinaRegistral.Trim)
                        ddlNotariaInmueble.InnerText = IIf(.CodigoNotaria = "", "", .CodigoNotaria.Trim)
                        ddlEstadoInscripcionRRPPInmueble.InnerText = IIf(.CodEstadoInscripcionRrPp = "", "", .CodEstadoInscripcionRrPp.Trim)
                        ddlEstadoMunicipalInmueble.InnerText = IIf(.CodEstadoMunicipal = "", "", .CodEstadoMunicipal.Trim)
                        txtFechaTransferenciaInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisicionInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtFechaBajaInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtObservacionesInmueble.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)
                        txtCodigoPredio.InnerText = IIf(.CodigoPredio = "", "", .CodigoPredio.Trim)

                        txtFechaProbableObra.InnerText = GCCUtilitario.CheckDateString2(.FechaProbableFinObra.ToString.Trim, "C")
                        txtFechaRealObra.InnerText = GCCUtilitario.CheckDateString2(.FechaRealFinObra.ToString.Trim, "C")
                        txtFechaInscripcionMunicipalInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString.Trim, "C")
                        ddlEstadoMunicipalInmueble.InnerText = .CodEstadoMunicipal.Trim()
                        txtFechaEnvioNotaria.InnerText = GCCUtilitario.CheckDateString2(.FechaEnvioNotaria.ToString.Trim, "C")
                        txtFechaPropiedad.InnerText = GCCUtilitario.CheckDateString2(.FechaPropiedad.ToString.Trim, "C")
                        txtFechaInscripcionRegistralInmueble.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString.Trim, "C")
                        ddlEstadoInscripcionRRPPInmueble.InnerText = .CodEstadoInscripcionRrPp.Trim()
                        txtOficinaRegistralInmueble.InnerText = .OficinaRegistral

                        'IBK - RPH
                        'hidTotal.Value = ""
                        'hidTotal.Value = .PrecioTotal
                        'Fin
                    End With
                End If
            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Maquinaria, hidCodClasificacion.Value) <> -1) Then
                'DATOS DE LA MAQUINARIA
                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraMaquinaria = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosMaquinariasConsulta(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraMaquinaria IsNot Nothing Then
                    With oESolicitudCreditoEstructuraMaquinaria
                        ddlDepartamentoMaquinaria.InnerText = IIf(.Departamento = "", "", .Departamento.Trim)
                        ddlProvinciaMaquinaria.InnerText = IIf(.Provincia = "", "", .Provincia.Trim)
                        ddlDistritoMaquinaria.InnerText = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtUsoMaquinaria.InnerText = IIf(.Uso = "", "", .Uso.Trim)
                        txtPlacaActualMaquinaria.InnerText = IIf(.Placa = "", "", .Placa.Trim)
                        txtPlacaAnteriorMaquinaria.InnerText = IIf(.PlacaAntigua = "", "", .PlacaAntigua.ToString().Trim)
                        txtDireccionMaquinaria.InnerText = IIf(.Ubicacion = "", "", .Ubicacion.ToString().Trim)
                        txtAnioTransferenciaMaquinaria.InnerText = IIf(.Anio = 0, "", Convert.ToInt32(.Anio).ToString().Trim())
                        txtSerieMaquinaria.InnerText = IIf(.NroSerie = "", "", .NroSerie.ToString().Trim())
                        txtMotorMaquinaria.InnerText = IIf(.NroMotor = "", "", .NroMotor.ToString().Trim())
                        txtMarcaMaquinaria.InnerText = IIf(.Marca = "", "", .Marca.ToString().Trim())
                        txtModeloMaquinaria.InnerText = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                        txtColorMaquinaria.InnerText = IIf(.Color = "", "", .Color.ToString().Trim())
                        txtCarroceriaMaquinaria.InnerText = IIf(.CodTipoCarroceria = "", "", .CodTipoCarroceria.ToString().Trim())
                        txtMedidasMaquinaria.InnerText = IIf(.Medidas = "", "", .Medidas.ToString().Trim())
                        txtModeloMaquinaria.InnerText = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                        txtCantidadMaquinaria.InnerText = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                        txtDescripcionMaquinaria.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                        ddlEstadoRegistroBienMaquinaria.InnerText = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlEstadoBienMaquinaria.InnerText = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim())
                        ddlTipoBienMaquinaria.InnerText = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        ddlMonedaMaquinaria.InnerText = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorBienMaquinaria.InnerText = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferenciaMaquinaria.InnerText = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisicionMaquinaria.InnerText = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtFechaBajaMaquinaria.InnerText = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtObservacionesMaquinaria.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)

                        'IBK - RPH
                        'hidTotal.Value = ""
                        'hidTotal.Value = .PrecioTotal
                        'Fin

                    End With
                End If


            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Vehiculo, hidCodClasificacion.Value) <> -1) Then
                'DATOS DE VEHICULO

                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculoConsulta(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraVehiculo IsNot Nothing Then
                    With oESolicitudCreditoEstructuraVehiculo
                        ddlDepartamentoVehiculo.InnerText = IIf(.Departamento = "", "", .Departamento.Trim)
                        ddlProvinciaVehiculo.InnerText = IIf(.Provincia = "", "", .Provincia.Trim)
                        ddlDistritoVehiculo.InnerText = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtDireccionVehiculo.InnerText = IIf(.Ubicacion = "", "", .Ubicacion.Trim)
                        txtUsoVehiculo.InnerText = IIf(.Uso = "", "", .Uso.Trim)
                        ddlEstadoRegistroBienVehiculo.InnerText = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        ddlTransmisionVehivulo.InnerText = IIf(.CodTransmision = "", "", .CodTransmision.Trim)
                        ddlTraccionVehivulo.InnerText = IIf(.CodTraccion = "", "", .CodTraccion.Trim)
                        ddlTipoMotorVehivulo.InnerText = IIf(.CodTipoMotor = "", "", .CodTipoMotor.Trim)

                        ''ddlTipoBienVehiculo = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        'ddlTipoBien = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)


                        ddlClaseVehivulo.InnerText = IIf(.CodClase = "", "", .CodClase.Trim)
                        txtCombustibleVehiculo.InnerText = IIf(.CodCombustible = "", "", .CodCombustible.Trim)
                        txtPotenciaMotorVehivulo.InnerText = IIf(.CodPotenciaMotor = "", "", .CodPotenciaMotor.Trim)
                        txtCilindrosVehivulo.InnerText = IIf(.Cilindros = "", "", .Cilindros.ToString().Trim)
                        txtLongitudVehivulo.InnerText = IIf(.Longitud = "", "", .Longitud.Trim)
                        txtPesoNetoVehivulo.InnerText = .PesoNeto.ToString()
                        txtMarcaVehivulo.InnerText = IIf(.Marca = "", "", .Marca.Trim)
                        txtCargaUtilVehivulo.InnerText = .CargaUtil.ToString()
                        txtPesoBrutoVehivulo.InnerText = .PesoBruto.ToString()
                        txtAsientosVehivulo.InnerText = IIf(.Asientos = 0, "", GCCUtilitario.CheckInt(.Asientos))
                        txtEjesVehivulo.InnerText = IIf(.Ejes = 0, "", GCCUtilitario.CheckInt(.Ejes))
                        txtRuedasVehivulo.InnerText = IIf(.Ruedas = 0, "", GCCUtilitario.CheckInt(.Ruedas))
                        txtPasajerosVehivulo.InnerText = IIf(.Pasajeros = 0, "", GCCUtilitario.CheckInt(.Pasajeros))
                        txtAnchoVehivulo.InnerText = .Ancho.ToString()
                        txtPuertasVehivulo.InnerText = IIf(.Puertas = 0, "", GCCUtilitario.CheckInt(.Puertas))
                        txtAltoVehivulo.InnerText = .Alto.ToString()
                        txtFormulaRodanteVehivulo.InnerText = IIf(.FormulaRodante = "", "", .FormulaRodante.Trim)
                        txtCantidadVehivulo.InnerText = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                        txtDescripcionVehivulo.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                        ddlEstadoBienVehiculo.InnerText = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlTipoBienVehiculo.InnerText = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        ddlMonedaVehiculo.InnerText = IIf(.MonedabienNombre = "", "", .MonedabienNombre.Trim)
                        txtValorVehivulo.InnerText = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferenciaVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisionVehiculo.InnerText = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtObservacionesVehivulo.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)
                        txtPlacaActualVehivulo.InnerText = IIf(.PlacaAntigua = "", "", .PlacaAntigua.Trim)
                        txtPlacaAnteriorVehivulo.InnerText = IIf(.Placa = "", "", .Placa.Trim)
                        txtAnioVehivulo.InnerText = IIf(.Anio = 0, "", Convert.ToInt32(.Anio))
                        txtNroSerieVehivulo.InnerText = IIf(.NroSerie = "", "", .NroSerie.Trim)
                        txtNroMotorVehivulo.InnerText = IIf(.NroMotor = "", "", .NroMotor.Trim)
                        txtModeloVehivulo.InnerText = IIf(.Modelo = "", "", .Modelo.Trim)
                        txtColorVehivulo.InnerText = IIf(.Color = "", "", .Color.Trim)
                        txtCarroceriaVehiculo.InnerText = IIf(.Carroceria = "", "", .Carroceria.Trim)
                        txtMedidasVehivulo.InnerText = IIf(.Medidas = "", "", .Medidas.ToString().Trim())
                        'txtClaseVehiculo.InnerText = IIf(.Clase = "", "", .Clase.ToString().Trim())
                        ddlClaseVehivulo.InnerText = IIf(.CodClase = "", "", .CodClase.Trim)
                        txtCilindrajeVehivulo.InnerText = IIf(.Cilindraje = "", "", .Cilindraje.Trim())
                        ddlEstadoMunicipalVehiculo.InnerText = IIf(.CodEstadoMunicipal = "", "", .CodEstadoMunicipal.Trim())
                        ddlEstadoInscripcionRRPPVehiculo.InnerText = IIf(.CodEstadoInscripcionRrPp = "", "", .CodEstadoInscripcionRrPp.Trim())
                        txtFechaEnvioSATVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaEnvioSat.ToString(), "C")
                        txtFechaEmisionTarjetaVehiculo.InnerText = GCCUtilitario.CheckDateString2(.FechaEmisionTarjeta.ToString(), "C")
                        txtFechaInscripcionMunicipalVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString(), "C")
                        txtFechaTransferenciaVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaEnvioNotariaVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaEnvioNotaria.ToString(), "C")
                        txtFechaBajaVehiculo.InnerText = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtFechaPropiedadVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaPropiedad.ToString(), "C")
                        txtFechaInscripcionRegistralVehivulo.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString(), "C")

                        'IBK - RPH
                        hidTotal.Value = ""
                        hidTotal.Value = .PrecioTotal
                        'fin


                        If .CodInafectacion.Trim() = "1" Then
                            cbInafectacion.Checked = True
                        Else
                            cbInafectacion.Checked = False
                        End If

                        If .CodPagoImpuesto.Trim() = "1" Then
                            cbPagoImpuestos.Checked = True
                        Else
                            cbPagoImpuestos.Checked = False
                        End If

                    End With

                End If


            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Otros, hidCodClasificacion.Value) <> -1) Then
                'DATOS DE SISTEMA Y OTROS
                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraSistema = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosSistemasConsultas(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraSistema IsNot Nothing Then
                    With oESolicitudCreditoEstructuraSistema
                        ddlDepartamentoOtros.InnerText = IIf(.DepartamentoNombre = "", "", .DepartamentoNombre.Trim)
                        ddlProvinciaOtros.InnerText = IIf(.ProvinciaNombre = "", "", .ProvinciaNombre.Trim)
                        ddlDistritoOtros.InnerText = IIf(.DistritoNombre = "", "", .DistritoNombre.Trim)
                        txtSerieOtros.InnerText = IIf(.NroSerie = "", "", .NroSerie.ToString().Trim())
                        txtMotorOtros.InnerText = IIf(.NroMotor = "", "", .NroMotor.ToString().Trim())
                        txtMarcaOtros.InnerText = IIf(.Marca = "", "", .Marca.ToString().Trim())
                        txtModeloOtros.InnerText = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                        txtColorOtros.InnerText = IIf(.Color = "", "", .Color.ToString().Trim())
                        txtUbicacionOtros.InnerText = IIf(.Ubicacion = "", "", .Ubicacion.ToString().Trim())
                        txtPartidaRegistralOtros.InnerText = IIf(.PartidaRegistral = "", "", .PartidaRegistral.ToString().Trim())
                        txtOficinaRegistralOtros.InnerText = IIf(.OficinaRegistral = "", "", .OficinaRegistral.ToString().Trim())
                        txtCantidadOtros.InnerText = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                        txtDescripcionOtros.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                        txtUsoOtros.InnerText = IIf(.Uso = "", "", .Uso.ToString().Trim())
                        ddlBienOtros.InnerText = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        ddlEstadoRegistroBienOtros.InnerText = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlTipoBienOtros.InnerText = IIf(.TipoBien = "", "", .TipoBien.Trim)
                        ddlMonedaOtros.InnerText = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorBienOtros.InnerText = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferenciaOtros.InnerText = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisicionOtros.InnerText = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtFechaBajaOtros.InnerText = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtObservacionesOtros.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)

                        hidTotal.Value = ""
                        hidTotal.Value = .PrecioTotal
                    End With
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocBienNtx = Nothing
        End Try
    End Sub

    <WebMethod()> _
        Public Shared Function ListaDocumentos(ByVal pPageSize As Integer, _
                                                          ByVal pCurrentPage As Integer, _
                                                          ByVal pSortColumn As String, _
                                                          ByVal pSortOrder As String, _
                                                          ByVal pCodigoContrato As String, _
                                                          ByVal pCodigoBien As Integer) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx

        Dim dtDocumentos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoDocumento(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   pCodigoContrato, _
                                                                                                                   pCodigoBien.ToString()))
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        'Dim total2 As Decimal
        If dtDocumentos.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtDocumentos.Rows(0)("RecordCount"))
            'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
            intTotalCurrent = Convert.ToInt32(dtDocumentos.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDocumentos)

    End Function
    <WebMethod()> _
        Public Shared Function ListaDocumentosInscripcion(ByVal pPageSize As Integer, _
                                                          ByVal pCurrentPage As Integer, _
                                                          ByVal pSortColumn As String, _
                                                          ByVal pSortOrder As String, _
                                                          ByVal pCodigoContrato As String, _
                                                          ByVal pCodigoBien As Integer) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx

        Dim dtDocumentos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienInscripcionMunicipal(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   pCodigoContrato, _
                                                                                                                   pCodigoBien.ToString()))
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        'Dim total2 As Decimal
        If dtDocumentos.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtDocumentos.Rows(0)("RecordCount"))
            'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
            intTotalCurrent = Convert.ToInt32(dtDocumentos.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDocumentos)

    End Function

    <WebMethod()> _
        Public Shared Function ListaInafectacion(ByVal pCodigoContrato As String, _
                                                 ByVal pCodigoBien As Integer) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx

        Dim dtInafectacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaDatosInafectacion(pCodigoContrato, _
                                                                                                                      pCodigoBien.ToString()))

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(dtInafectacion.Rows.Count, 1)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, 1, dtInafectacion.Rows.Count, dtInafectacion)

    End Function

#End Region
End Class
