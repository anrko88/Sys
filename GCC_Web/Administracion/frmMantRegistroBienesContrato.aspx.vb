Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmMantRegistroBienesContrato
    Inherits GCCBase

    Dim objLog As New GCCLog("frmMantRegistroBienesContrato.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            If Not Page.IsPostBack Then

			
				'Inicio JJM
                If Not String.IsNullOrEmpty(Request.QueryString("csc")) Then
                    hidNumeroContrato.Value = Request.QueryString("csc")
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("csf")) Then
                    hidSecFinanciamiento.Value = Request.QueryString("csf")
                End If


                ''IBK - RPH
                'If Not String.IsNullOrEmpty(Request.QueryString("tipobien")) Then

                'End If
                ''Fin


                'hidNumeroContrato.Value = Request.QueryString("csc")
                'hidSecFinanciamiento.Value = Request.QueryString("csf")
                'Fin JJM
				
                'hidNumeroContrato.Value = Request.QueryString("csc")
                'hidSecFinanciamiento.Value = Request.QueryString("csf")

                'Inmuebles
                GCCUtilitario.CargarDepartamento(ddlDepartamentoInmueble)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBien, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)


                'Maquinarias
                GCCUtilitario.CargarDepartamento(cmbDepartamentoMaquinaria)
                GCCUtilitario.CargarComboValorGenerico(cmbEstadobienMaquina, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                'GCCUtilitario.CargarComboValorGenerico(ddlTipoCarroceriaMaquinaria, GCCConstante.C_TABLAGENERICA_Tipo_Carroceria)

                'Vehiculos
                GCCUtilitario.CargarDepartamento(ddlDepartamentoVehiculo)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienVehiculo, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
                'GCCUtilitario.CargarComboValorGenerico(ddlTipoCarroceriaMaquinaria, GCCConstante.C_TABLAGENERICA_Tipo_Carroceria)

                'Otros
                GCCUtilitario.CargarDepartamento(ddlDepartamentoDatosOtros)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoDatosOtros, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)

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
                    hidCodClasificacion.Value = .Tiporubrofinanciamiento
                    hidCodTipoBien.Value = .Codigotipobien.Trim
                End With
            End If

            'DATOS DEL INMUEBLE
            If (Array.IndexOf(GCCConstante.DestinoCredito_Inmueble, hidCodClasificacion.Value) <> -1) Then
			If Not String.IsNullOrEmpty(hidNumeroContrato.Value) And (hidSecFinanciamiento.Value <> "0") Then
                oESolicitudCreditoEstructuraInmueble = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosInmueble(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                If oESolicitudCreditoEstructuraInmueble IsNot Nothing Then
                    With oESolicitudCreditoEstructuraInmueble
                        hidCodDepartamentoInmueble.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                        hidCodProvinciaInmueble.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                        hidCodDistritoInmueble.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                        txtUsoInmueble.Value = IIf(.Uso = "", "", .Uso.Trim)
                        txtUbicacionInmueble.Value = IIf(.Ubicacion = "", "", .Ubicacion.Trim)
                        txtCantidadInmueble.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim)
                        txtDescripcionInmueble.Value = IIf(.Comentario = "", "", .Comentario.Trim)
                        hidEstadoBien.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        txtPartidaRegistralInmueble.Value = IIf(.PartidaRegistral = "", "", .PartidaRegistral.Trim)
                        txtOficinaRegistralInmueble.Value = IIf(.OficinaRegistral = "", "", .OficinaRegistral.Trim)
                        txtCodigoPredioInmueble.Value = IIf(.CodigoPredio = "", "", .CodigoPredio.Trim)
                    End With
                    End If
                End If

            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Maquinaria, hidCodClasificacion.Value) <> -1) Then

                ''DATOS DE LA MAQUINARIA

                Dim objBienNTx As New LBienNTx


                Dim dtBienesContratoMaqOtros As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoBienContratoMaquinaria(10000, _
                                                                                                                    1, _
                                                                                                                    "CodSolicitudCredito", _
                                                                                                                    "asc", _
                                                                                                                    hidNumeroContrato.Value, _
                                                                                                                    "100"))



                'IBK JJM Inicio
                If Not String.IsNullOrEmpty(hidNumeroContrato.Value) And (hidSecFinanciamiento.Value <> "0") Then
                    oESolicitudCreditoEstructuraMaquinaria = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosMaquinarias(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))


                    If oESolicitudCreditoEstructuraMaquinaria IsNot Nothing Then
                        With oESolicitudCreditoEstructuraMaquinaria

                            hidDepartamentoMaquinaria.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                            hidProvinciaMaquinaria.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                            hidDistritoMaquinaria.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                            txtUsoMaquina.Value = IIf(.Uso = "", "", .Uso.Trim)
                            txtPlacaMaquina.Value = IIf(.PlacaAntigua = "", "", .PlacaAntigua.Trim)
                            txtFabricacionMaquina.Value = IIf(.Anio = 0, "", Convert.ToInt32(.Anio).ToString().Trim())
                            txtSerieMotorMaquina.Value = IIf(.NroSerie = "", "", .NroSerie.ToString().Trim())
                            txtNumeroMotorMaquina.Value = IIf(.NroMotor = "", "", .NroMotor.ToString().Trim())
                            txtMarcaMaquina.Value = IIf(.Marca = "", "", .Marca.ToString().Trim())
                            txtModeloMaquina.Value = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                            txtMedidasMaquina.Value = IIf(.Medidas = "", "", .Medidas.ToString().Trim())
                            txtCantidadMaquina.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                            hidEstadoBienMaquinaria.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                            txtDescripcionAutoMaquina.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                            txtTipoCarroceriaMaquina.Value = IIf(.CodTipoCarroceria = "", "", .CodTipoCarroceria.ToString().Trim())
                            txtUbicacionBienMaquina.Value = IIf(.Ubicacion = "", "", .Ubicacion.Trim)

                        End With
                    End If
                End If

            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Vehiculo, hidCodClasificacion.Value) <> -1) Then
                ''DATOS DE VEHICULO
                '''oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculo(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))

                'If Not String.IsNullOrEmpty(hidNumeroContrato.Value) And (hidSecFinanciamiento.Value <> "0") Then
                '    With oESolicitudCreditoEstructuraVehiculo
                '        hidDepartamentoVehiculo.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                '        hidProvinciaVehiculo.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                '        hidDistritoVehiculo.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                '        txtUsoVehiculo.Value = IIf(.Uso = "", "", .Uso.Trim)
                '        txtMarcaVehiculo.Value = IIf(.Marca = "", "", .Marca.Trim)
                '        txtCantidadVehiculo.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                '        txtDescripcionVehiculo.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                '        hidEstadoBienVehiculo.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                '        txtPlacaVehiculo.Value = IIf(.PlacaAntigua = "", "", .PlacaAntigua.Trim)
                '        txtAnioVehiculo.Value = IIf(.Anio = 0, "", Convert.ToInt32(.Anio))
                '        txtSerieVehiculo.Value = IIf(.NroSerie = "", "", .NroSerie.Trim)
                '        txtMotorVehiculo.Value = IIf(.NroMotor = "", "", .NroMotor.Trim)
                '        txtModeloVehiculo.Value = IIf(.Modelo = "", "", .Modelo.Trim)
                '        txtMedidasVehiculo.Value = IIf(.Medidas = "", "", .Medidas.Trim)
                '        txtCarroceriaVehiculo.Value = IIf(.Carroceria = "", "", .Carroceria.Trim)
                '        txtUbicacionVehiculo.Value = IIf(.Ubicacion = "", "", .Ubicacion.Trim)


                '    End With
                'End If
                If Not String.IsNullOrEmpty(hidNumeroContrato.Value) And (hidSecFinanciamiento.Value <> "0") Then
                    oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculo(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                    If oESolicitudCreditoEstructuraVehiculo IsNot Nothing Then
                        With oESolicitudCreditoEstructuraVehiculo
                            hidDepartamentoVehiculo.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                            hidProvinciaVehiculo.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                            hidDistritoVehiculo.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                            txtUsoVehiculo.Value = IIf(.Uso = "", "", .Uso.Trim)
                            txtMarcaVehiculo.Value = IIf(.Marca = "", "", .Marca.Trim)
                            txtCantidadVehiculo.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                            txtDescripcionVehiculo.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                            hidEstadoBienVehiculo.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                            txtPlacaVehiculo.Value = IIf(.PlacaAntigua = "", "", .PlacaAntigua.Trim)
                            txtAnioVehiculo.Value = IIf(.Anio = 0, "", Convert.ToInt32(.Anio))
                            txtSerieVehiculo.Value = IIf(.NroSerie = "", "", .NroSerie.Trim)
                            txtMotorVehiculo.Value = IIf(.NroMotor = "", "", .NroMotor.Trim)
                            txtModeloVehiculo.Value = IIf(.Modelo = "", "", .Modelo.Trim)
                            txtMedidasVehiculo.Value = IIf(.Medidas = "", "", .Medidas.Trim)
                            txtCarroceriaVehiculo.Value = IIf(.Carroceria = "", "", .Carroceria.Trim)
                            txtUbicacionVehiculo.Value = IIf(.Ubicacion = "", "", .Ubicacion.Trim)

                            'IBK - RPH
                            txtColorVehiculo.Value = IIf(.Color = "", "", .Color.ToString.Trim())
                            'Fin
                        End With
                    End If
                End If
            ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Otros, hidCodClasificacion.Value) <> -1) Then

                ''DATOS DE SISTEMA Y OTROS

                If Not String.IsNullOrEmpty(hidNumeroContrato.Value) And (hidSecFinanciamiento.Value <> "0") Then
                    oESolicitudCreditoEstructuraSistema = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosSistemas(hidNumeroContrato.Value, Convert.ToInt32(hidSecFinanciamiento.Value)))
                    If oESolicitudCreditoEstructuraSistema IsNot Nothing Then
                        With oESolicitudCreditoEstructuraSistema
                            hidDepartamentoOtros.Value = IIf(.Departamento = "", "", .Departamento.Trim)
                            hidProvinciaOtros.Value = IIf(.Provincia = "", "", .Provincia.Trim)
                            hidDistritoOtros.Value = IIf(.Distrito = "", "", .Distrito.Trim)
                            txtUsoDatosOtros.Value = IIf(.Uso = "", "", .Uso.Trim)
                            txtMarcaDatosOtros.Value = IIf(.Marca = "", "", .Marca.ToString().Trim())
                            txtModeloDatosOtros.Value = IIf(.Modelo = "", "", .Modelo.ToString().Trim())
                            txtUbicacionDatosOtros.Value = IIf(.Ubicacion = "", "", .Ubicacion.ToString().Trim())
                            txtPartidaRegistralDatosOtros.Value = IIf(.PartidaRegistral = "", "", .PartidaRegistral.ToString().Trim())
                            txtOficinaRegistralDatosOtros.Value = IIf(.OficinaRegistral = "", "", .OficinaRegistral.ToString().Trim())
                            txtCantidadDatosOtros.Value = IIf(.Cantidadproducto = "", "", .Cantidadproducto.ToString().Trim())
                            txtDescripcionDatosOtros.Value = IIf(.Comentario = "", "", .Comentario.ToString().Trim())
                            hidEstadoBienOtros.Value = IIf(.Codigoestadobien = "", "", .Codigoestadobien.Trim)
                        End With
                    End If
                End If
            End If







        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocBienNtx = Nothing
        End Try
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        objLog.escribe("DEBUG", "Metodo Load de la página", "btnGrabar_Click")
        Try

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If




        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGrabar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "'))", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGrabar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub
    <WebMethod()> _
    Public Shared Function ValidarDatosMaquinaria(ByVal strSerie As String, ByVal strNumContrato As String)

        Try
            Dim objBienNTx As New LBienNTx
            Dim strNroSerie As String
            Dim oESolicitudCreditoEstructuraMaquinaria As New ESolicitudcreditoestructura

            Dim dtBienesContratoMaqOtros As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoBienContratoMaquinaria(10000, _
                                                                                                                1, _
                                                                                                                "CodSolicitudCredito", _
                                                                                                                "asc", _
                                                                                                                strNumContrato, _
                                                                                                                "100"))
            For i As Integer = 0 To dtBienesContratoMaqOtros.Rows.Count - 1

                If strSerie.Trim() = dtBienesContratoMaqOtros.Rows(i)("NroSerie").ToString().Trim() Then
                    strNroSerie = (dtBienesContratoMaqOtros.Rows(i)("NroSerie").ToString().Trim())
                    Exit For
                End If
            Next

            If strNroSerie <> "" Then
                Return strNroSerie
            Else
                Return ""
            End If

        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
      

    <WebMethod()> _
    Public Shared Function ValidarDatosVehiculo(ByVal strSerie As String, _
                                         ByVal strMotor As String, _
                                         ByVal strPlaca As String, _
                                         ByVal strPlacaAntigua As String)

        Try
            Dim objESolicitudCreditoEstructuraCarc As New ESolicitudcreditoestructuracarac
            Dim objLBienNTX As New LBienNTx
            Dim pESolicitudCreditoEstructura As String

            If objESolicitudCreditoEstructuraCarc IsNot Nothing Then
                With objESolicitudCreditoEstructuraCarc
                    .Nroserie = strSerie.ToString()
                    .Nromotor = strMotor.ToString()
                    .Placa = strPlaca.ToString()
                    .Placaantigua = strPlacaAntigua.ToString()
                End With
            End If
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarc)

            Dim dtValidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLBienNTX.ValidarDatosVehiculo(pESolicitudCreditoEstructura))
            Dim StrMensaje As String
            StrMensaje = dtValidacion.Rows(0)("Mensaje").ToString()
            If dtValidacion.Rows.Count > 0 Then
                If StrMensaje <> "" Then
                    Return StrMensaje
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
End Class
