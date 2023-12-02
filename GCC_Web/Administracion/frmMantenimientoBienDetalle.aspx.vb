Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Administracion_frmMantenimientoBienDetalle
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMantenimientoBienDetalle.aspx.vb")

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
                GCCUtilitario.CargarDepartamento(ddlDepartamentoInmueble)
                GCCUtilitario.CargarMunicipalidad(ddlMunicipalidadInmueble) 'IBK JJM
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBien, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                GCCUtilitario.CargarComboMoneda(ddlMonedaBien)
                GCCUtilitario.CargarComboValorGenerico(ddlNotariaInmueble, GCCConstante.C_TABLAGENERICA_NOTARIA_PUBLICA)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionRRPPInmueble, GCCConstante.C_TABLAGENERICA_Estado_Inscripción_RRPP)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoMunicipalInmueble, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                GCCUtilitario.CargarComboValorGenerico(ddlEstaRegistroBienInmueble, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                'GCCUtilitario.CargarComboValorGenerico(ddlPropiedadInmueble, GCCConstante.C_TABLAGENERICA_Estado_Transferencia)

                'Maquinarias
                GCCUtilitario.CargarDepartamento(ddlDepartamentoMaquinaria)
                GCCUtilitario.CargarComboMoneda(ddlMonedaMaquinaria)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienMaquinaria, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoRegistroBienMaquinaria, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                'GCCUtilitario.CargarComboValorGenerico(ddlTipoCarroceriaMaquinaria, GCCConstante.C_TABLAGENERICA_Tipo_Carroceria)
                'Vehiculos
                GCCUtilitario.CargarDepartamento(ddlDepartamentoVehiculo)
                GCCUtilitario.CargarComboValorGenerico(ddlTraccionVehivulo, GCCConstante.C_TABLAGENERICA_Traccion)
                GCCUtilitario.CargarComboValorGenerico(ddlTransmisionVehivulo, GCCConstante.C_TABLAGENERICA_Transmision)
                GCCUtilitario.CargarComboValorGenerico(ddlTipoMotorVehivulo, GCCConstante.C_TABLAGENERICA_Tipo_Motor)
                'GCCUtilitario.CargarComboValorGenerico(ddlCombustibleVehivulo, GCCConstante.C_TABLAGENERICA_Tipo_Combustible)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienVehiculo, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                GCCUtilitario.CargarComboMoneda(ddlMonedaVehiculo)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoMunicipalVehiculo, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionRRPPVehiculo, GCCConstante.C_TABLAGENERICA_Estado_Inscripción_RRPP)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoRegistroBienVehiculo, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                ' GCCUtilitario.CargarComboValorGenerico(ddlTipoCarroceriaVehiculo, GCCConstante.C_TABLAGENERICA_Tipo_Carroceria)
                GCCUtilitario.CargarComboValorGenerico(ddlClaseVehivulo, GCCConstante.C_TABLAGENERICA_Clase)
                'sistemas y otros
                GCCUtilitario.CargarDepartamento(ddlDepartamentoOtros)
                GCCUtilitario.CargarComboMoneda(ddlMonedaOtros)
                GCCUtilitario.CargarComboValorGenerico(ddlBienOtros, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoRegistroBienOtros, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
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
                    txtNumeroContrato.Value = .Codsolicitudcredito
                    txtEstado.Value = .EstadoContrato
                    txtclasificacion.Value = .ClasificacionBien
                    txtTipoBien.Value = .TipoBien
                    txtmoneda.Value = .Moneda
                    txtEjecutivo.Value = .EjecutivoBanca
                    txtBanca.Value = .Banca
                    txtKardex.Value = .Kardex
                    txtObservacionContrato.Value = .ObservacionContrato
                    txtcu.Value = .CodUnico
                    txtRazonSocial.Value = .RazonSocial
                    txtTipoDocumento.Value = .TipoDocumento
                    txtNumDocumento.Value = .NumeroDocumento
                    hidCodClasificacion.Value = .Tiporubrofinanciamiento
                    hidCodTipoBien.Value = .Codigotipobien.Trim
                End With
            End If


            If (Array.IndexOf(GCCConstante.DestinoCredito_Inmueble, hidCodClasificacion.Value) <> -1) Then
                'DATOS DEL INMUEBLE
                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraInmueble = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosInmueble(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraInmueble IsNot Nothing Then
                    With oESolicitudCreditoEstructuraInmueble
                        hidCodDepartamentoInmueble.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                        hidCodProvinciaInmueble.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                        hidCodDistritoInmueble.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                        hidCodMunicipalidadInmueble.Value = IIf(.Municipalidad = "", "", .Municipalidad.Trim) 'IBK JJM
                        txtUsoInmueble.Value = IIf(.Uso = "", "", .Uso.Trim)
                        txtUbicacionInmueble.Value = IIf(.Ubicacion = "", "", .Ubicacion.Trim)
                        txtCantidadInmueble.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim)
                        txtDescripcionInmueble.Value = IIf(.Comentario = "", "", .Comentario.Trim)
                        hidEstadoBien.Value = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlEstaRegistroBienInmueble.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        hidCodTipoBien.Value = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        hidMonedaBien.Value = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorInmueble.Value = .MontoValorBien.ToString()
                        txtFechaInscripcionMunicipalInmueble.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString(), "C")
                        txtFechaInscripcionRegistralInmueble.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString(), "C")
                        txtOficinaRegistralInmueble.Value = IIf(.OficinaRegistral = "", "", .OficinaRegistral.Trim)
                        ddlNotariaInmueble.Value = IIf(.CodigoNotaria = "", "", .CodigoNotaria.Trim)
                        ddlEstadoInscripcionRRPPInmueble.Value = IIf(.CodEstadoInscripcionRrPp = "", "", .CodEstadoInscripcionRrPp.Trim)
                        ddlEstadoMunicipalInmueble.Value = IIf(.CodEstadoMunicipal = "", "", .CodEstadoMunicipal.Trim)
                        txtFechaTransferenciaInmueble.Value = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisicionInmueble.Value = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtFechaBajaInmueble.Value = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtObservacionesInmueble.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)
                        txtCodigoPredio.Value = IIf(.CodigoPredio = "", "", .CodigoPredio.Trim)

                        txtFechaProbableObra.Value = GCCUtilitario.CheckDateString2(.FechaProbableFinObra.ToString.Trim, "C")
                        txtFechaRealObra.Value = GCCUtilitario.CheckDateString2(.FechaRealFinObra.ToString.Trim, "C")
                        txtFechaInscripcionMunicipalInmueble.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString.Trim, "C")
                        ddlEstadoMunicipalInmueble.Value = .CodEstadoMunicipal.Trim()
                        txtFechaEnvioNotaria.Value = GCCUtilitario.CheckDateString2(.FechaEnvioNotaria.ToString.Trim, "C")
                        txtFechaPropiedad.Value = GCCUtilitario.CheckDateString2(.FechaPropiedad.ToString.Trim, "C")
                        txtFechaInscripcionRegistralInmueble.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString.Trim, "C")
                        ddlEstadoInscripcionRRPPInmueble.Value = .CodEstadoInscripcionRrPp.Trim()
                        txtOficinaRegistralInmueble.Value = .OficinaRegistral

                        'IBK - RPH
                        hidTotal.Value = ""
                        hidTotal.Value = .PrecioTotal
                        GCCUtilitario.SeleccionaCombo(ddlMunicipalidadInmueble, IIf(.Municipalidad = "", "", .Municipalidad.Trim))
                        'Fin
                    End With
                End If
            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Maquinaria, hidCodClasificacion.Value) <> -1) Then
                'DATOS DE LA MAQUINARIA
                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraMaquinaria = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosMaquinarias(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraMaquinaria IsNot Nothing Then
                    With oESolicitudCreditoEstructuraMaquinaria
                        hidDepartamentoMaquinaria.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                        hidProvinciaMaquinaria.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                        hidDistritoMaquinaria.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtUsoMaquinaria.Value = IIf(.Uso = "", "", .Uso.Trim)
                        txtPlacaActualMaquinaria.Value = IIf(.Placa = "", "", .Placa.Trim)
                        txtPlacaAnteriorMaquinaria.Value = IIf(.PlacaAntigua = "", "", .PlacaAntigua.ToString().Trim)
                        txtDireccionMaquinaria.Value = IIf(.Ubicacion = "", "", .Ubicacion.ToString().Trim)
                        txtAnioTransferenciaMaquinaria.Value = IIf(.Anio = 0, "", Convert.ToInt32(.Anio).ToString().Trim())
                        txtSerieMaquinaria.Value = IIf(.NroSerie = "", "", .NroSerie.ToString().Trim())
                        txtMotorMaquinaria.Value = IIf(.NroMotor = "", "", .NroMotor.ToString().Trim())
                        txtMarcaMaquinaria.Value = IIf(.Marca = "", "", .Marca.ToString().Trim())
                        txtModeloMaquinaria.Value = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                        txtColorMaquinaria.Value = IIf(.Color = "", "", .Color.ToString().Trim())
                        txtCarroceriaMaquinaria.Value = IIf(.CodTipoCarroceria = "", "", .CodTipoCarroceria.ToString().Trim())
                        txtMedidasMaquinaria.Value = IIf(.Medidas = "", "", .Medidas.ToString().Trim())
                        txtModeloMaquinaria.Value = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                        txtCantidadMaquinaria.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                        txtDescripcionMaquinaria.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                        hidEstadoBienMaquinaria.Value = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlEstadoRegistroBienMaquinaria.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim())
                        hidTipoBienMaquinaria.Value = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        hidMonedaMaquinaria.Value = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorBienMaquinaria.Value = .MontoValorBien.ToString()
                        txtFechaTransferenciaMaquinaria.Value = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisicionMaquinaria.Value = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtFechaBajaMaquinaria.Value = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtObservacionesMaquinaria.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)

                        'IBK - RPH
                        hidTotal.Value = ""
                        hidTotal.Value = .PrecioTotal
                        'Fin

                    End With
                End If


            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Vehiculo, hidCodClasificacion.Value) <> -1) Then
                'DATOS DE VEHICULO

                'IBK-JJM
                pObtieneRegistroMaximo()
                'IBK-Fin
                oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculo(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraVehiculo IsNot Nothing Then
                    With oESolicitudCreditoEstructuraVehiculo
                        hidDepartamentoVehiculo.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                        hidProvinciaVehiculo.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                        hidDistritoVehiculo.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtDireccionVehiculo.Value = IIf(.Ubicacion = "", "", .Ubicacion.Trim)
                        txtUsoVehiculo.Value = IIf(.Uso = "", "", .Uso.Trim)
                        ddlEstadoRegistroBienVehiculo.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        ddlTransmisionVehivulo.Value = IIf(.CodTransmision = "", "", .CodTransmision.Trim)
                        ddlTraccionVehivulo.Value = IIf(.CodTraccion = "", "", .CodTraccion.Trim)
                        ddlTipoMotorVehivulo.Value = IIf(.CodTipoMotor = "", "", .CodTipoMotor.Trim)

                        ''ddlTipoBienVehiculo = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        'ddlTipoBien = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)


                        ddlClaseVehivulo.Value = IIf(.CodClase = "", "", .CodClase.Trim)
                        txtCombustibleVehiculo.Value = IIf(.CodCombustible = "", "", .CodCombustible.Trim)
                        txtPotenciaMotorVehivulo.Value = IIf(.CodPotenciaMotor = "", "", .CodPotenciaMotor.Trim)
                        txtCilindrosVehivulo.Value = IIf(.Cilindros = "", "", .Cilindros.ToString().Trim)
                        txtLongitudVehivulo.Value = IIf(.Longitud = "", "", .Longitud.Trim)
                        txtPesoNetoVehivulo.Value = .PesoNeto.ToString()
                        txtMarcaVehivulo.Value = IIf(.Marca = "", "", .Marca.Trim)
                        txtCargaUtilVehivulo.Value = .CargaUtil.ToString()
                        txtPesoBrutoVehivulo.Value = .PesoBruto.ToString()
                        txtAsientosVehivulo.Value = IIf(.Asientos = 0, "", GCCUtilitario.CheckInt(.Asientos))
                        txtEjesVehivulo.Value = IIf(.Ejes = 0, "", GCCUtilitario.CheckInt(.Ejes))
                        txtRuedasVehivulo.Value = IIf(.Ruedas = 0, "", GCCUtilitario.CheckInt(.Ruedas))
                        txtPasajerosVehivulo.Value = IIf(.Pasajeros = 0, "", GCCUtilitario.CheckInt(.Pasajeros))
                        txtAnchoVehivulo.Value = .Ancho.ToString()
                        txtPuertasVehivulo.Value = IIf(.Puertas = 0, "", GCCUtilitario.CheckInt(.Puertas))
                        txtAltoVehivulo.Value = .Alto.ToString()
                        txtFormulaRodanteVehivulo.Value = IIf(.FormulaRodante = "", "", .FormulaRodante.Trim)
                        txtCantidadVehivulo.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                        txtDescripcionVehivulo.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                        hidEstadoBienVehiculo.Value = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        hidTipoBienVehiculo.Value = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        hidMonedaVehiculo.Value = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorVehivulo.Value = .MontoValorBien.ToString() ' * dec.ToString()
                        txtFechaTransferenciaVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisionVehiculo.Value = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtObservacionesVehivulo.Value = IIf(.ObservacionBien = "", "", .ObservacionBien.Trim)
                        txtPlacaActualVehivulo.Value = IIf(.PlacaAntigua = "", "", .PlacaAntigua.Trim)
                        txtPlacaAnteriorVehivulo.Value = IIf(.Placa = "", "", .Placa.Trim)
                        txtAnioVehivulo.Value = IIf(.Anio = 0, "", Convert.ToInt32(.Anio))
                        txtNroSerieVehivulo.Value = IIf(.NroSerie = "", "", .NroSerie.Trim)
                        txtNroMotorVehivulo.Value = IIf(.NroMotor = "", "", .NroMotor.Trim)
                        txtModeloVehivulo.Value = IIf(.Modelo = "", "", .Modelo.Trim)
                        txtColorVehivulo.Value = IIf(.Color = "", "", .Color.Trim)
                        txtCarroceriaVehiculo.Value = IIf(.Carroceria = "", "", .Carroceria.Trim)
                        txtMedidasVehivulo.Value = IIf(.Medidas = "", "", .Medidas.ToString().Trim())
                        txtClaseVehiculo.Value = IIf(.Clase = "", "", .Clase.ToString().Trim())
                        hidCodClaseVehivulo.Value = IIf(.CodClase = "", "", .CodClase.Trim)
                        txtCilindrajeVehivulo.Value = IIf(.Cilindraje = "", "", .Cilindraje.Trim())
                        ddlEstadoMunicipalVehiculo.Value = IIf(.CodEstadoMunicipal = "", "", .CodEstadoMunicipal.Trim())
                        ddlEstadoInscripcionRRPPVehiculo.Value = IIf(.CodEstadoInscripcionRrPp = "", "", .CodEstadoInscripcionRrPp.Trim())
                        txtFechaEnvioSATVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaEnvioSat.ToString(), "C")
                        txtFechaEmisionTarjetaVehiculo.Value = GCCUtilitario.CheckDateString2(.FechaEmisionTarjeta.ToString(), "C")
                        txtFechaInscripcionMunicipalVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString(), "C")
                        txtFechaTransferenciaVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaEnvioNotariaVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaEnvioNotaria.ToString(), "C")
                        txtFechaBajaVehiculo.Value = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
                        txtFechaPropiedadVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaPropiedad.ToString(), "C")
                        txtFechaInscripcionRegistralVehivulo.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString(), "C")

                        'IBK - RPH
                        hidTotal.Value = ""
                        hidTotal.Value = .PrecioTotal
                        'fin


                        If .FlagInafectacion.Trim() = "1" Then
                            cbInafectacion.Checked = True
                        Else
                            cbInafectacion.Checked = False
                        End If

                        If .FlagPagoImpuestos.Trim() = "1" Then
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
                oESolicitudCreditoEstructuraSistema = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosSistemas(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraSistema IsNot Nothing Then
                    With oESolicitudCreditoEstructuraSistema
                        hidCodDepartamentoOtros.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                        hidCodProvinciaOtros.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                        hidCodDistritoOtros.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtSerieOtros.Value = IIf(.NroSerie = "", "", .NroSerie.ToString().Trim())
                        txtMotorOtros.Value = IIf(.NroMotor = "", "", .NroMotor.ToString().Trim())
                        txtMarcaOtros.Value = IIf(.Marca = "", "", .Marca.ToString().Trim())
                        txtModeloOtros.Value = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                        txtColorOtros.Value = IIf(.Color = "", "", .Color.ToString().Trim())
                        txtUbicacionOtros.Value = IIf(.Ubicacion = "", "", .Ubicacion.ToString().Trim())
                        txtPartidaRegistralOtros.Value = IIf(.PartidaRegistral = "", "", .PartidaRegistral.ToString().Trim())
                        txtOficinaRegistralOtros.Value = IIf(.OficinaRegistral = "", "", .OficinaRegistral.ToString().Trim())
                        txtCantidadOtros.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                        txtDescripcionOtros.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                        txtUsoOtros.Value = IIf(.Uso = "", "", .Uso.ToString().Trim())
                        ddlBienOtros.Value = IIf(.EstadoBien = "", "", .EstadoBien.Trim)
                        ddlEstadoRegistroBienOtros.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        hidTipoBienOtros.Value = IIf(.Codigotipobien = "", "", .Codigotipobien.Trim)
                        ddlMonedaOtros.Value = IIf(.Monedabien = "", "", .Monedabien.Trim)
                        txtValorBienOtros.Value = .MontoValorBien.ToString("#,###,##0.00")
                        txtFechaTransferenciaOtros.Value = GCCUtilitario.CheckDateString2(.FechaTransferencia.ToString(), "C")
                        txtFechaAdquisicionOtros.Value = GCCUtilitario.CheckDateString2(.FechaAdquisicion.ToString(), "C")
                        txtFechaBajaOtros.Value = GCCUtilitario.CheckDateString2(.FechaBaja.ToString(), "C")
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
                .ValorBien = GCCUtilitario.ConvierteValorBien(pValorBien.ToString())
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
                .ValorBien = GCCUtilitario.ConvierteValorBien(pValorBien.ToString())
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
    <WebMethod()> _
    Public Shared Function GuardarDetalleMaquinaria(ByVal pNumeroContrato As String, _
                                         ByVal pSecFinanciamiento As String, _
                                         ByVal pCodigoTipoBien As String, _
                                         ByVal pCodDepartamento As String, _
                                         ByVal pCodProvincia As String, _
                                         ByVal pCodDistrito As String, _
                                         ByVal pUbicacion As String, _
                                         ByVal pUso As String, _
                                         ByVal pPlacaActual As String, _
                                         ByVal pPlacaAnterior As String, _
                                         ByVal pAnioFabricacion As Integer, _
                                         ByVal pNroSerie As String, _
                                         ByVal pNroMotor As String, _
                                         ByVal pMedidas As String, _
                                         ByVal pMarca As String, _
                                         ByVal pModelo As String, _
                                         ByVal pColor As String, _
                                         ByVal pCarroceria As String, _
                                         ByVal pCantidad As String, _
                                         ByVal pDescripcion As String, _
                                         ByVal pCodEstadoBien As String, _
                                         ByVal pCodMoneda As String, _
                                         ByVal pValorBien As String, _
                                         ByVal pFechaBaja As String, _
                                         ByVal pFechaTransferencia As String, _
                                         ByVal pFechaAdquisicion As String, _
                                         ByVal pObservaciones As String, _
                                         ByVal pFlagOrigen As String, _
                                         ByVal pCodEstado As String) As String
        Try
            Dim objESolicitudCreditoEstructura As New ESolicitudcreditoestructura
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoEstructura As String


            'pAnioFabricacion = IIf(pAnioFabricacion, DBNull.Value, 0, pAnioFabricacion)

            With objESolicitudCreditoEstructura
                .Codsolicitudcredito = pNumeroContrato
                .Secfinanciamiento = pSecFinanciamiento
                .Codigotipobien = pCodigoTipoBien
                .FechaTransferencia = GCCUtilitario.StringToDateTime(pFechaTransferencia)
                .Color = pColor
                .Uso = pUso
                .ObservacionBien = pObservaciones
                .Departamento = pCodDepartamento
                .Provincia = pCodProvincia
                .Distrito = pCodDistrito
                .ValorBien = GCCUtilitario.ConvierteValorBien(pValorBien.ToString())
                .Placa = pPlacaActual
                .PlacaAntigua = pPlacaAnterior
                .Anio = GCCUtilitario.StringToInteger(pAnioFabricacion)
                .NroMotor = pNroMotor
                .Carroceria = pCarroceria
                .Medidas = pMedidas
                .Monedabien = pCodMoneda
                .Ubicacion = pUbicacion
                .Cantidadproducto = Convert.ToInt32(pCantidad)
                .Comentario = pDescripcion
                .EstadoBien = pCodEstadoBien
                .FechaBaja = GCCUtilitario.StringToDateTime(pFechaBaja)
                .FechaAdquisicion = GCCUtilitario.StringToDateTime(pFechaAdquisicion)
                .Flag_origen = GCCUtilitario.StringToInteger(pFlagOrigen)
                .Codigoestadobien = pCodEstado
                .NroSerie = pNroSerie
                .Marca = pMarca
                .Modelo = pModelo
                .Carroceria = pCarroceria
                .Audusuariomodificacion = GCCSession.CodigoUsuario
            End With
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructura)

            Dim blnResult As Boolean = objLBien.fblnModificarDetalleMaquinaria(pESolicitudCreditoEstructura)

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
    Public Shared Function GuardarBienDetalle(ByVal pNumeroContrato As String, _
                                         ByVal pSecFinanciamiento As String, _
                                         ByVal pCodDepartamento As String, _
                                         ByVal pCodProvincia As String, _
                                         ByVal pCodDistrito As String, _
                                         ByVal pUbicacion As String, _
                                         ByVal pUso As String, _
                                         ByVal pCantidad As String, _
                                         ByVal pDescripcion As String, _
                                         ByVal pCodEstadoBien As String, _
                                         ByVal pCodigoTipoBien As String, _
                                         ByVal pCodMoneda As String, _
                                         ByVal pValorBien As String, _
                                         ByVal pFechaTransferencia As String, _
                                         ByVal pFechaAdquisicion As String, _
                                         ByVal pCodigoPredio As String, _
                                         ByVal pFlagOrigen As Integer, _
                                         ByVal pCodEstado As String, _
                                         ByVal pObservaciones As String, _
                                         ByVal pFechaProbableFinObra As String, _
                                         ByVal pFechaRealFinObra As String, _
                                         ByVal pFechaInscripcionMunicipal As String, _
                                         ByVal pFechaEnvioNotaria As String, _
                                         ByVal pFechaPropiedad As String, _
                                         ByVal pFechaBaja As String, _
                                         ByVal pFechaInscripcionRegistral As String, _
                                         ByVal pOficinaRegistral As String, _
                                         ByVal pCodigoNotaria As String, _
                                         ByVal pCodEstadoInscripcionRrpp As String, _
                                         ByVal pCodEstadoMunicipal As String, _
                                         ByVal pMunicipalidad As String)

        Try
            Dim objESolicitudCreditoEstructura As New ESolicitudcreditoestructura
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoEstructura As String

            With objESolicitudCreditoEstructura
                .Codsolicitudcredito = pNumeroContrato
                .Secfinanciamiento = pSecFinanciamiento
                .Departamento = pCodDepartamento
                .Provincia = pCodProvincia
                .Distrito = pCodDistrito
                .Ubicacion = pUbicacion
                .Uso = pUso
                .Cantidadproducto = pCantidad
                .Comentario = pDescripcion
                .EstadoBien = pCodEstadoBien
                .Codigotipobien = pCodigoTipoBien
                .Monedabien = pCodMoneda
                .ValorBien = GCCUtilitario.ConvierteValorBien(pValorBien.ToString())
                .FechaTransferencia = GCCUtilitario.StringToDateTime(pFechaTransferencia)
                .FechaAdquisicion = GCCUtilitario.StringToDateTime(pFechaAdquisicion)
                .FechaBaja = GCCUtilitario.StringToDateTime(pFechaBaja)
                .CodigoPredio = pCodigoPredio
                .Flag_origen = pFlagOrigen
                .Codigoestadobien = pCodEstado
                .ObservacionBien = pObservaciones
                .FechaProbableFinObra = GCCUtilitario.StringToDateTime(pFechaProbableFinObra)
                .FechaRealFinObra = GCCUtilitario.StringToDateTime(pFechaRealFinObra)
                .FechaInscripcionMunicipal = GCCUtilitario.StringToDateTime(pFechaInscripcionMunicipal)
                .FechaEnvioNotaria = GCCUtilitario.StringToDateTime(pFechaEnvioNotaria)
                .FechaPropiedad = GCCUtilitario.StringToDateTime(pFechaPropiedad)
                .FechaInscripcionRegistral = GCCUtilitario.StringToDateTime(pFechaInscripcionRegistral)
                .OficinaRegistral = pOficinaRegistral
                .CodigoNotaria = pCodigoNotaria
                .CodEstadoInscripcionRrPp = pCodEstadoInscripcionRrpp
                .CodEstadoMunicipal = pCodEstadoMunicipal
                .Audusuariomodificacion = GCCSession.CodigoUsuario
                .Municipalidad = pMunicipalidad 'IBK JJM
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
        Public Shared Function GuardarVehiculoDetalle(ByVal pNumeroContrato As String, _
                                             ByVal pSecFinanciamiento As String, _
                                            ByVal pTipoBien As String, _
                                            ByVal pFechaTransferencia As String, _
                                            ByVal pColor As String, _
                                            ByVal pObservaciones As String, _
                                            ByVal pCodDepartamento As String, _
                                            ByVal pCodProvincia As String, _
                                            ByVal pCodDistrito As String, _
                                            ByVal pValorBien As String, _
                                            ByVal pPlacaActual As String, _
                                            ByVal pPlacaAnterior As String, _
                                            ByVal pNroMotor As String, _
                                            ByVal pTipoCarroceria As String, _
                                            ByVal pMedidas As String, _
                                            ByVal pCodMoneda As String, _
                                            ByVal pUbicacion As String, _
                                            ByVal pUso As String, _
                                            ByVal pCantidad As String, _
                                            ByVal pDescripcion As String, _
                                            ByVal pCodEstadoBien As String, _
                                            ByVal pFechaAdquisicion As String, _
                                            ByVal pFechaBaja As String, _
                                            ByVal pCodPredio As String, _
                                            ByVal pCodEstado As String, _
                                            ByVal pTransmision As String, _
                                            ByVal pTraccion As String, _
                                            ByVal pTipoMotor As String, _
                                            ByVal pPotenciaMotor As String, _
                                            ByVal pCombustible As String, _
                                            ByVal pCilindros As String, _
                                            ByVal pLongitud As String, _
                                            ByVal pPasajeros As String, _
                                            ByVal pAsientos As String, _
                                            ByVal pEjes As String, _
                                            ByVal pPuertas As String, _
                                            ByVal pRuedas As String, _
                                            ByVal pFormulaRodante As String, _
                                            ByVal pNroSerie As String, _
                                            ByVal pMarca As String, _
                                            ByVal pModelo As String, _
                                            ByVal pCarroceria As String, _
                                            ByVal pCodClase As String, _
                                            ByVal pClase As String, _
                                            ByVal pCilindraje As String, _
                                            ByVal pFechaEnvioSat As String, _
                                            ByVal pFechaInscripcionMunicipal As String, _
                                            ByVal pFechaEnvioNotaria As String, _
                                            ByVal pFechaPropiedad As String, _
                                            ByVal pFechaInscripcionRegistral As String, _
                                            ByVal pFechaEmisionTarjeta As String, _
                                            ByVal pEstadoInscripcionMunicipal As String, _
                                            ByVal pEstadoInscripcionRrpp As String, _
                                            ByVal pPesoNeto As String, _
                                            ByVal pCargaUtil As String, _
                                            ByVal pPesoBruto As String, _
                                            ByVal pAncho As String, _
                                            ByVal pAlto As String, _
                                            ByVal pAnioFabricacion As String, _
                                            ByVal pFlagOrigen As String, _
                                            ByVal pCodImpuesto As String, _
                                            ByVal pCodInafectacion As String) As String
        'Inicio IBK - AAE - comento método pFecEnvioRRRPP
        'ByVal pCodInafectacion As String, ByVal pFechaEnvioRrpp As String)

        Try
            Dim objESolicitudCreditoEstructura As New ESolicitudcreditoestructura
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoEstructura As String

            With objESolicitudCreditoEstructura
                .Codsolicitudcredito = pNumeroContrato
                .Secfinanciamiento = pSecFinanciamiento
                .Codigotipobien = pTipoBien
                .FechaTransferencia = GCCUtilitario.StringToDateTime(pFechaTransferencia)
                .Color = pColor
                .ObservacionBien = pObservaciones
                .Departamento = pCodDepartamento
                .Provincia = pCodProvincia
                .Distrito = pCodDistrito
                .ValorBien = GCCUtilitario.ConvierteValorBien(pValorBien.ToString())
                .PlacaAntigua = pPlacaActual
                .Placa = pPlacaAnterior
                .Anio = IIf(String.IsNullOrEmpty(pAnioFabricacion), 0, CType(pAnioFabricacion, Integer))
                .NroMotor = pNroMotor
                'carroceria no hay
                .Carroceria = pTipoCarroceria
                .Medidas = pMedidas
                .Monedabien = pCodMoneda
                .Ubicacion = pUbicacion
                .Uso = pUso
                .Cantidadproducto = GCCUtilitario.StringToInteger(pCantidad)
                .Comentario = pDescripcion
                .EstadoBien = pCodEstadoBien
                .FechaAdquisicion = GCCUtilitario.StringToDateTime(pFechaAdquisicion)
                .FechaBaja = GCCUtilitario.StringToDateTime(pFechaBaja)
                .Flag_origen = GCCUtilitario.StringToInteger(pFlagOrigen)
                .Codigoestadobien = pCodEstado
                .CodTransmision = pTransmision
                .CodTraccion = pTraccion
                .CodTipoMotor = pTipoMotor
                .CodPotenciaMotor = pPotenciaMotor
                .CodCombustible = pCombustible
                .Cilindros = pCilindros
                .Longitud = pLongitud
                .Pasajeros = GCCUtilitario.StringToInteger(pPasajeros)
                .PesoNeto = pPesoNeto
                .CargaUtil = pCargaUtil
                .PesoBruto = pPesoBruto
                .Asientos = pAsientos
                .Ejes = GCCUtilitario.StringToInteger(pEjes)
                .Ancho = pAncho
                .Puertas = GCCUtilitario.StringToInteger(pPuertas)
                .Alto = pAlto
                .Ruedas = GCCUtilitario.StringToInteger(pRuedas)
                .FormulaRodante = pFormulaRodante
                .NroSerie = pNroSerie
                .Marca = pMarca
                .Modelo = pModelo
                .Carroceria = pCarroceria
                .CodClase = pCodClase
                .Clase = pClase
                .Cilindraje = pCilindraje
                .FechaEnvioSat = GCCUtilitario.StringToDateTime(pFechaEnvioSat)
                .FechaInscripcionMunicipal = GCCUtilitario.StringToDateTime(pFechaInscripcionMunicipal)
                .FechaEnvioNotaria = GCCUtilitario.StringToDateTime(pFechaEnvioNotaria)
                'Inicio IBK - EL parámetro no se encuentra en aspx y js
                'Falta Fecha envio rrpp
                '.FechaEnvioRrPp = GCCUtilitario.StringToDateTime(pFechaEnvioRrpp)
                .FechaEnvioRrPp = Nothing
                'Fin IBK
                .FechaEmisionTarjeta = GCCUtilitario.StringToDateTime(pFechaEmisionTarjeta)
                .FechaPropiedad = GCCUtilitario.StringToDateTime(pFechaPropiedad)
                .FechaInscripcionRegistral = GCCUtilitario.StringToDateTime(pFechaInscripcionRegistral)

                .Codusuario = GCCSession.CodigoUsuario
                .CodEstadoMunicipal = pEstadoInscripcionMunicipal
                .CodEstadoInscripcionRrPp = pEstadoInscripcionRrpp
                .Audusuariomodificacion = GCCSession.CodigoUsuario
                .CodPagoImpuesto = pCodImpuesto
                .CodInafectacion = pCodInafectacion

            End With
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructura)

            Dim blnResult As Boolean = objLBien.fblnModificarVehiculo(pESolicitudCreditoEstructura)

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
    Public Shared Function GuardarOtrosDetalle(ByVal pNumeroContrato As String, _
                                         ByVal pSecFinanciamiento As String, _
                                         ByVal pCodDistrito As String, _
                                        ByVal pCodDepartamento As String, _
                                        ByVal pCodProvincia As String, _
                                        ByVal pUso As String, _
                                        ByVal pNroSerie As String, _
                                        ByVal pNroMotor As String, _
                                        ByVal pColor As String, _
                                        ByVal pUbicacion As String, _
                                        ByVal pMarca As String, _
                                        ByVal pModelo As String, _
                                        ByVal pPartidaRegistral As String, _
                                        ByVal pOficinaRegistral As String, _
                                        ByVal pCantidad As String, _
                                        ByVal pDescripcion As String, _
                                        ByVal pCodEstadoBien As String, _
                                        ByVal pTipoBien As String, _
                                        ByVal pCodMoneda As String, _
                                        ByVal pValorBien As String, _
                                        ByVal pFechaTransferencia As String, _
                                        ByVal pFechaAdquisicion As String, _
                                        ByVal pFechaBaja As String, _
                                        ByVal pObservaciones As String, _
                                        ByVal pFlagOrigen As String, _
                                        ByVal pCodEstado As String)

        Try
            Dim objESolicitudCreditoEstructura As New ESolicitudcreditoestructura
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoEstructura As String

            With objESolicitudCreditoEstructura
                .Codsolicitudcredito = pNumeroContrato
                .Secfinanciamiento = pSecFinanciamiento
                .Departamento = pCodDepartamento
                .Provincia = pCodProvincia
                .Distrito = pCodDistrito
                .Ubicacion = pUbicacion
                .Uso = pUso
                .Cantidadproducto = GCCUtilitario.StringToInteger(pCantidad)
                .Comentario = pDescripcion
                .Color = pColor
                .EstadoBien = pCodEstadoBien
                .Codigotipobien = pTipoBien
                .PartidaRegistral = pPartidaRegistral
                .OficinaRegistral = pOficinaRegistral
                .TipoBien = pTipoBien
                .Marca = pMarca
                .NroSerie = pNroSerie
                .NroMotor = pNroMotor
                .Modelo = pModelo
                .Monedabien = pCodMoneda
                .ValorBien = GCCUtilitario.ConvierteValorBien(pValorBien.ToString())
                .FechaBaja = GCCUtilitario.StringToDateTime(pFechaBaja)
                .FechaTransferencia = GCCUtilitario.StringToDateTime(pFechaTransferencia)
                .FechaAdquisicion = GCCUtilitario.StringToDateTime(pFechaAdquisicion)
                .Flag_origen = GCCUtilitario.StringToInteger(pFlagOrigen)
                .Codigoestadobien = pCodEstado
                .ObservacionBien = pObservaciones
                .Audusuariomodificacion = GCCSession.CodigoUsuario
            End With
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructura)

            Dim blnResult As Boolean = objLBien.fblnModificarOtros(pESolicitudCreditoEstructura)

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
    ''' <summary>
    ''' Eliminar DocumentoCometario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaDocumentoComentario(ByVal pstrCodigoContrato As String, _
                                                         ByVal pstrCodigoDocumento As String, _
                                                         ByVal pstrSecFinanciamiento As String _
                                                       ) As String

        ''Variables
        Dim objLBienTx As New LBienTx
        Dim strEGcc_biendocumento As String
        Try

            'Inicializa Objeto
            Dim objEGcc_contratodocumento As New EGcc_contratodocumento
            With objEGcc_contratodocumento
                .Numerocontrato = pstrCodigoContrato
                .Codigodocumento = pstrCodigoDocumento
                .SecFinanciamiento = pstrSecFinanciamiento
                .EstadoDocumento = 2
                .EstadoDocContrato = 0
                .EstadoDocBien = 1
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            strEGcc_biendocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(objEGcc_contratodocumento)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objLBienTx.ModificarBienContratoDocumento(strEGcc_biendocumento)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

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
    <WebMethod()> _
    Public Shared Function EliminaInafectacion(ByVal pstrCodigoContrato As String, _
                                                         ByVal pstrCodigoInafectacion As String, _
                                                         ByVal pstrCodigoBien As String _
                                                       ) As String

        ''Variables
        Dim objLBienTx As New LBienTx
        Dim strEGcc_biendocumento As String
        Try

            Dim objESolicitudCreditoCarc As New ESolicitudcreditoestructuracarac
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoCarc As String

            If objESolicitudCreditoCarc IsNot Nothing Then
                With objESolicitudCreditoCarc
                    .CodInafectacion = pstrCodigoInafectacion
                    .Codsolicitudcredito = pstrCodigoContrato
                    .Secfinanciamiento = pstrCodigoBien
                    .EstadoInafectacion = "2"
                End With
            End If
            pESolicitudCreditoCarc = GCCUtilitario.SerializeObject(objESolicitudCreditoCarc)

            Dim blnResult As Boolean = objLBien.fblnModificarInafectacion(pESolicitudCreditoCarc)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    <WebMethod()> _
    Public Shared Function EliminaInscripcion(ByVal pstrCodigoContrato As String, _
                                                         ByVal pstrCodigoInscripcion As String, _
                                                         ByVal pstrCodigoBien As String _
                                                       ) As String

        ''Variables
        Dim objLBienTx As New LBienTx
        Dim strEGcc_biendocumento As String
        Try

            Dim objESolicitudCreditoCarc As New ESolicitudcreditoestructuracarac
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoCarc As String

            If objESolicitudCreditoCarc IsNot Nothing Then
                With objESolicitudCreditoCarc
                    .CodigoInscripcion = pstrCodigoInscripcion
                    .Codsolicitudcredito = pstrCodigoContrato
                    .Secfinanciamiento = pstrCodigoBien
                    .EstadoInscripcion = "2"
                End With
            End If
            pESolicitudCreditoCarc = GCCUtilitario.SerializeObject(objESolicitudCreditoCarc)

            Dim blnResult As Boolean = objLBien.fblnModificarInscripcionMunicipal(pESolicitudCreditoCarc)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function

#End Region
End Class
