Imports System.Web.Services
Imports System.Data
Imports GCC.UI
Imports GCC.LogicWS
Imports GCC.Entity

Partial Class Cotizacion_frmCotizacionVer
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmCotizacionVer.aspx.vb")
    Shared mstrCodCotizacion As String
    Shared mstrVersion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mstrCodCotizacion = Request.QueryString("cc")
                mstrVersion = Request.QueryString("cv")
                Me.hddCodigoCotizacion.Value = mstrCodCotizacion
                Me.hddVersionCotizacion.Value = mstrVersion
                CargaEditarCotizacion()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargaEditarCotizacion()
        Dim oLwsCotizacionNTx As New LCotizacionNTx
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            'Inicializa Objeto
            Dim oECotizacion As New EGcc_cotizacion
            With oECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(mstrCodCotizacion)
                .Versioncotizacion = GCCUtilitario.NullableString(mstrVersion)
            End With
            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                                            oLwsCotizacionNTx.ObtenerCotizacionVersion(GCCUtilitario.SerializeObject(oECotizacion)))

            'Valida si existe
            If dtCotizacion.Rows.Count > 0 Then

                '*****************************
                'CABECERA
                '*****************************
                txtNumeroCotizacion.Text = dtCotizacion.Rows(0).Item("Codigocotizacion").ToString
                lblEstado.Text = dtCotizacion.Rows(0).Item("Nombreestadocotizacion").ToString
                chkGeneraCarta.Checked = CBool(GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("Generarcarta")))
                txtCUCliente.Text = dtCotizacion.Rows(0).Item("CODIGOUNICO").ToString
                txtNombreCliente.Text = dtCotizacion.Rows(0).Item("NombreCliente").ToString
                lblTipoPersona.Text = dtCotizacion.Rows(0).Item("NombreTipopersona").ToString
                hddTipoPersona.Value = dtCotizacion.Rows(0).Item("CODIGOTIPOPERSONA").ToString
                lblTipoDocumento.Text = dtCotizacion.Rows(0).Item("NombreTipoDocumento").ToString
                lblNumeroDocumento.Text = dtCotizacion.Rows(0).Item("NumeroDocumento").ToString
                chkLinea.Checked = CBool(GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("FlagLinea")))
                lblLinea.Text = dtCotizacion.Rows(0).Item("Numerolinea").ToString
                lblEjecutivoBanca.Text = dtCotizacion.Rows(0).Item("DesEjecutivoBanca").ToString
                lblBanca.Text = dtCotizacion.Rows(0).Item("NombreBanca").ToString
                lblZonal.Text = dtCotizacion.Rows(0).Item("DesZonal").ToString
                lblEjecutivoLeasing.Text = dtCotizacion.Rows(0).Item("NombreEjecutivoleasing").ToString
                lblCorreo.Text = dtCotizacion.Rows(0).Item("Correocontacto").ToString
                lblContacto.Text = dtCotizacion.Rows(0).Item("Nombrecontacto").ToString

                '********************************
                'DATOS GENERALES :: COTIZACION
                '********************************
                Dim pstrProdFinanActivo As String = dtCotizacion.Rows(0).Item("Codproductofinancieroactivo").ToString
                Dim pstrProdFinanPasivo As String = dtCotizacion.Rows(0).Item("Codproductofinancieropasivo").ToString
                Dim pstrTipoContrato As String = ""
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING_PAS) Then
                    pstrTipoContrato = GCCConstante.C_DESGCC_PROD_LEASING
                End If
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS) Then
                    pstrTipoContrato = GCCConstante.C_DESGCC_PROD_LEASEBACK
                End If
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION_PAS) Then
                    pstrTipoContrato = GCCConstante.C_DESGCC_PROD_IMPORTACION
                End If
                lblTipoContrato.Text = pstrTipoContrato

                lblMoneda.Text = dtCotizacion.Rows(0).Item("NombreMoneda").ToString
                lblProcedencia.Text = dtCotizacion.Rows(0).Item("NombreProcedencia").ToString
                lblClasificacionBien.Text = dtCotizacion.Rows(0).Item("NombreClasificacionbien").ToString
                lblTipoBien.Text = dtCotizacion.Rows(0).Item("NombreTipoBien").ToString
                lblEstadoBien.Text = dtCotizacion.Rows(0).Item("NombreEstadobien").ToString
                lblPrecioVenta.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Precioventa").ToString).ToString(GCCConstante.C_FormatMiles)
                lblMontoIGV.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Valorventaigv").ToString).ToString(GCCConstante.C_FormatMiles)
                lblValorVenta.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Valorventa").ToString).ToString(GCCConstante.C_FormatMiles)
                lblCuotaInicial.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString).ToString(GCCConstante.C_FormatMiles)
                lblCuotaIncialPorc.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Cuotainicialporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblRiesgoNeto.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Riesgoneto").ToString).ToString(GCCConstante.C_FormatMiles)

                '********************************
                'DATOS GENERALES :: CRONOGRAMA
                '********************************
                lblTipoCronograma.Text = dtCotizacion.Rows(0).Item("NombreTipocronograma").ToString
                lblNroCuotas.Text = dtCotizacion.Rows(0).Item("Numerocuotas").ToString
                lblPeriocidad.Text = dtCotizacion.Rows(0).Item("NombrePeriodicidad").ToString
                lblFrecuenciaPago.Text = dtCotizacion.Rows(0).Item("NombreFrecuenciapago").ToString
                lblPlazoGracia.Text = dtCotizacion.Rows(0).Item("Plazograciacuota").ToString
                lblTipoGracia.Text = dtCotizacion.Rows(0).Item("NombreTipograciacuota").ToString
                lblFechaMaxActivacion.Text = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechamaxactivacion").ToString.Trim, "C")
                lblFechaVence.Text = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechaprimervencimiento").ToString.Trim, "C")

                '********************************
                'DATOS GENERALES :: TASA
                '********************************
                lblTEA.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Teaporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblCostoFondos.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Costofondoporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblSpread.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Spreadporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblPreCuota.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Precuotaporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblPlazpGraciaPreCuota.Text = dtCotizacion.Rows(0).Item("Plazograciaprecuota").ToString

                '********************************
                'DATOS GENERALES :: COMISION
                '********************************                
                lblOpcionCompraPorc.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Opcioncompraporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblOpcionCompraMonto.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importeopcioncompra").ToString).ToString(GCCConstante.C_FormatMiles)
                lblComisionActivacionProc.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Comisionactivacionporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblComisionActivacionMonto.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecomisionactivacion").ToString).ToString(GCCConstante.C_FormatMiles)
                lblComisionEstructuracionPorc.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Comisionestructuracionporc").ToString).ToString(GCCConstante.C_FormatMiles)
                lblComisionEstructuracionMonto.Text = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecomisionestructuracion").ToString).ToString(GCCConstante.C_FormatMiles)

                '********************************
                'DATOS GENERALES :: SEGURO BIEN
                '********************************                
                lblTipoBienSeguro.Text = dtCotizacion.Rows(0).Item("NombreBientiposeguro").ToString
                lblImportePrimaSeguroBien.Text = dtCotizacion.Rows(0).Item("Bienimporteprima").ToString
                lblNumCuotasfinanciadas.Text = dtCotizacion.Rows(0).Item("Biennrocuotasfinanciar").ToString

                '********************************
                'DATOS GENERALES :: SEGURO DEGRA
                '********************************                
                lblTipoSeguro.Text = dtCotizacion.Rows(0).Item("NombreDesgravamentiposeguro").ToString
                lblImportePrimaDesgravamen.Text = dtCotizacion.Rows(0).Item("Desgravamenimporteprima").ToString
                lblNumCuotaFinanciar.Text = dtCotizacion.Rows(0).Item("Desgravamennrocuotasfinanciar").ToString

                '********************************
                'OPCIONES
                '********************************   
                lblFechaIngreso.Text = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechaingreso").ToString.Trim, "C")
                'rblMostrarTea.SelectedValue = dtCotizacion.Rows(0).Item("Mostrarteacartas").ToString
                lblfechaOfertaValida.Text = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("FechaOfertaValida").ToString.Trim, "C")
                'rdbMostrarComision.SelectedValue = dtCotizacion.Rows(0).Item("Mostrarmontocomision").ToString

                If Not dtCotizacion.Rows(0).Item("Periododisponible").ToString.Equals("") Then
                    lblPeriodoDisponibilidad.Text = dtCotizacion.Rows(0).Item("Periododisponible").ToString + " Días"
                End If

                txaOtrasComisiones.Value = dtCotizacion.Rows(0).Item("Otrascomisiones").ToString
                txtProveedores.Value = dtCotizacion.Rows(0).Item("DescripcionProveedor").ToString

                Dim intMostrarteacartas As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("Mostrarteacartas"))
                Dim intMostrarmontocomision As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("Mostrarmontocomision"))

                Me.hddMostrarTea.Value = intMostrarteacartas
                Me.hddMostrarComision.Value = intMostrarmontocomision

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

    <WebMethod()> _
    Public Shared Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pstrCodCotizacion As String) As JQGridJsonResponse

        Dim oLwsCotizacionNTx As New LCotizacionNTx
        Try
            'Inicializa Objeto
            Dim oECotizacionDocumento As New EGcc_cotizaciondocumento

            With oECotizacionDocumento
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodCotizacion)
            End With

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                                    oLwsCotizacionNTx.ListadoCotizacionDocumento(pPageSize, _
                                                                                               pCurrentPage, _
                                                                                               pSortColumn, _
                                                                                               pSortOrder, _
                                                                                               GCCUtilitario.SerializeObject(Of EGcc_cotizaciondocumento)(oECotizacionDocumento)) _
                                            )

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCotizacion.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(dtCotizacion.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

    ''' <summary>
    ''' Listado de Cronograma de Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 28/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoCotizacionCronograma(ByVal pstrNroCotizacion As String, _
                                                       ByVal pstrVersionCotizacion As String, _
                                                       ByVal pstrPaginaActual As String _
                                                       ) As JQGridJsonResponse

        'Variables
        Dim objCotizacionNTx As New LCotizacionNTx

        Try
            'Inicializa Objeto
            Dim objECotizacion As New EGcc_cotizacion
            Dim strECotizacion As String
            With objECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrNroCotizacion)
                .Versioncotizacion = GCCUtilitario.StringToInteger(pstrVersionCotizacion)
            End With
            strECotizacion = GCCUtilitario.SerializeObject(objECotizacion)

            'Ejecuta Consulta
            Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.CotizacionCronogramaGet(strECotizacion))
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = PreparaCronograma(dtCronograma, pstrNroCotizacion, pstrVersionCotizacion, objECotizacion, False)

            'Datos Paginacion
            Dim intTotalRegistros As Integer = objListECronograma.Count
            Dim intTotalxPagina As Integer = 50
            Dim intTotalPaginas As Integer = 0
            Dim intPaginaActual As Integer = GCCUtilitario.CheckInt(pstrPaginaActual)

            Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
            If decPaginasTotal <= 1 Then
                intTotalPaginas = 1
            ElseIf decPaginasTotal <= 2 Then
                intTotalPaginas = 2
            Else
                intTotalPaginas = 3
            End If

            'Resize Gronograma Datatable
            Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
            Dim objECronograma As New EGcc_cotizacioncronograma
            Dim intContador As Integer = 0
            For Each objECronograma In objListECronograma
                If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                    objListECronogramaNuevo.Add(objECronograma)
                End If
                intContador = intContador + 1
            Next

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Prepara Cronograma para insertar
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Shared Function PreparaCronograma(ByVal pdtbCronograma As DataTable, ByVal pstrCodigoCotizacion As String, ByVal pstrVersionCotizacion As String, ByVal pobjEGcc_cotizacion As EGcc_cotizacion, ByVal booValidaPrimeraFila As Boolean) As ListEGcc_cotizacioncronograma

        Try
            'Declara Variables
            Dim objCotizacionNTx As New LCotizacionNTx
            Dim objListECronograma As New ListEGcc_cotizacioncronograma

            'Valida Cuota Inicial
            Dim intContadorCronograma As Integer = 0
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecuotainicial)
            Dim blnMuestraFila As Boolean = True

            'Valida si existe
            If pdtbCronograma.Rows.Count > 0 Then

                For Each oRow As DataRow In pdtbCronograma.Rows

                    If Not decCuotaInicial > 0 And intContadorCronograma = 0 And booValidaPrimeraFila Then
                        blnMuestraFila = False
                    Else
                        blnMuestraFila = True
                    End If

                    If blnMuestraFila Then

                        Dim objECronograma As New EGcc_cotizacioncronograma
                        With objECronograma
                            .Numerocuota = GCCUtilitario.CheckInt(oRow.Item("Nu_cuota").ToString())
                            .Codigocotizacion = pstrCodigoCotizacion
                            .Versioncotizacion = pstrVersionCotizacion
                            .Fechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .Cantdiascuota = GCCUtilitario.StringToInteger(oRow.Item("Nu_dias").ToString())
                            .Montosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString())
                            .Montointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString())
                            .Montoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString())
                            .Montototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString())
                            .Saldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString())
                            .Interessegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString())
                            .Principalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString())
                            .Montocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString())

                            .SaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString())
                            .InteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString())
                            .PrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString())
                            .CuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString())

                            .Totalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString())
                            .Montototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString())

                            'Mostrar
                            .SFechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .SMontosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteressegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SCuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .STotalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString()).ToString(GCCConstante.C_FormatMiles)

                            .Audestadologico = 1
                            '.AudFechaRegistro = 	
                            '.AudFechaModificacion = 	
                            .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                            .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        End With
                        objListECronograma.Add(objECronograma)

                    End If

                    intContadorCronograma = intContadorCronograma + 1

                Next

                Return objListECronograma

            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
