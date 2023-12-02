Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Formalizacion_frmContratoVer
    Inherits GCCBase


    ReadOnly _objLog As New GCCLog("frmContratoVer.aspx.vb")

    Protected strNroContrato As String
    Protected strCotizacion As String
    Protected strEstado As String
    Protected strCantidadBienes As String
    Protected strDescripcionBien As String
    Protected strRuc As String
    Protected strCliente As String
    Protected strNumeroCta As String
    Protected strTitulo As String

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
            _objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                _objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Call InicializarListas()
                Call InicializarParametros()

                Dim codSolicitudCredito As String
                codSolicitudCredito = Request.QueryString("hddCodigo")
                hddCodigoContrato.Value = codSolicitudCredito

                Call LeerContrato(codSolicitudCredito)

                Call LeerDatosCliente(codSolicitudCredito)

                If hddCodigoCotizacion.Value <> "" Then
                    Call LeerCotizacion(hddCodigoCotizacion.Value)
                End If

                Call LeerTarifas(hddCodProductoFinancieroActivo.Value, _
                                 hddCodMoneda.Value)

                Call ValoresPredeterminados()
            End If

        Catch ex As ApplicationException
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"

#Region "Generales"

    ''' <summary>
    ''' Lee los parametros predefinidos y valores ppredeterminado válidos para el contrato.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub InicializarParametros()
        hddMensajeCorreo.Value = GCCConstante.C_MENSAJE_CORREO_CONTRATO

        ' Se lee la fecha del servidor para evitar problemas con la configuración del equipo host del cliente.
        hddFechaActual.Value = Now.ToString("dd/MM/yyyy")
    End Sub

    ''' <summary>
    ''' Inicializa el contenido de los Select (html controls) y dropdwonlist con los datos de sus respectivas tablas genericas.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub InicializarListas()
        GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionContrato, GCCConstante.C_TABLAGENERICA_CLASIFICACION_CONTRATO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoCivil, GCCConstante.C_TABLAGENERICA_ESTADO_CIVIL)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumentoConyuge, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
    End Sub

#End Region

#Region "Contrato"

    ''' <summary>
    ''' Lista todos los documentos del contrato existentes del cliente para el actual contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodigo">Código del contrato</param>
    ''' <param name="pFlagFiltro"></param>
    ''' <param name="pFlagEnvioCarta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pCodigo As String, _
                                                   ByVal pFlagFiltro As Integer, _
                                                   ByVal pFlagEnvioCarta As Integer, _
                                                   ByVal pFields As String) As JQGridJsonResponse
        Dim objCondicionAdicionalNTx As New LCheckListNTx
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim strEGccContratodocumento As String
        With oEGccContratodocumento
            .Numerocontrato = GCCUtilitario.NullableString(pCodigo)
            .Flagfiltro = GCCUtilitario.CheckInt(pFlagFiltro)
            .Flagcartaenvio = GCCUtilitario.CheckInt(pFlagEnvioCarta)
        End With
        strEGccContratodocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEGccContratodocumento)
        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCondicionAdicionalNTx.ContratoDocumentoSel(pPageSize, _
                                                                                                                                            pCurrentPage, _
                                                                                                                                            pSortColumn, _
                                                                                                                                            pSortOrder, _
                                                                                                                                            strEGccContratodocumento))
        'Número total de páginas
        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtCondicionAdicional.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtCondicionAdicional, _
                                                               pFields)
    End Function

    ''' <summary>
    ''' Devuelve los datos del cliente cuyos datos correponden con el código de contrato enviado.
    ''' </summary>
    ''' <param name="codSolicitudCredito">Código del contrato.</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerDatosCliente(ByVal codSolicitudCredito As String)
        Dim objContratoNTx As New LContratoNTx
        Dim oLwsDocClienteNtx As New LDocClienteNTx

        Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(codSolicitudCredito))
        Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(codSolicitudCredito))

        For Each oRow As DataRow In dtCliente.Rows
            ' Datos del cliente
            If Not oRow.Item("CodUnico") Is DBNull.Value Then
                txtCodUnico.Value = oRow.Item("CodUnico").ToString().Trim
            End If

            If Not odtbDatos.Rows(0).Item("NOMBRECLIENTE") Is DBNull.Value Then
                txtRazonSocial.Value = odtbDatos.Rows(0).Item("NOMBRECLIENTE").ToString().Trim
            End If

            If Not oRow.Item("CodigoTipoPersona") Is DBNull.Value Then
                hddCodigoTipoPersona.Value = oRow.Item("CodigoTipoPersona").ToString().Trim
            End If

            If Not odtbDatos.Rows(0).Item("NOMBRETIPOPERSONA") Is DBNull.Value Then
                txtTipoPersona.Value = odtbDatos.Rows(0).Item("NOMBRETIPOPERSONA").ToString().Trim
            End If

            If Not oRow.Item("Direccion") Is DBNull.Value Then
                txtaDomicilioCliente.Value = oRow.Item("Direccion").ToString().Trim
            End If
            If hddCodigoEstadoCivil.Value = "" Then
                If Not oRow.Item("CodigoEstadoCivil") Is DBNull.Value Then
                    hddCodigoEstadoCivil.Value = oRow.Item("CodigoEstadoCivil").ToString().Trim
                End If
            End If
            If Not oRow.Item("CodDocIdentificacionTipo") Is DBNull.Value Then
                hddTipoDocumento.Value = oRow.Item("CodDocIdentificacionTipo").ToString().Trim
            End If
            ' Datos del conyuge
            If Not oRow.Item("NombreConyuge") Is DBNull.Value Then
                txtNombreConyuge.Value = oRow.Item("NombreConyuge").ToString().Trim
            End If
            If Not oRow.Item("CodigoTipoDocConyuge") Is DBNull.Value Then
                hddTipoDocumentoConyuge.Value = oRow.Item("CodigoTipoDocConyuge").ToString().Trim
            End If
            If Not oRow.Item("NumeroDocumentoConyuge") Is DBNull.Value Then
                txtnumerodocumento.Value = oRow.Item("NumeroDocumentoConyuge").ToString().Trim
            End If
            If Not oRow.Item("DocumentoSeparacion") Is DBNull.Value Then
                hddAdjuntarArchivoDocumentoSeparacion.Value = oRow.Item("DocumentoSeparacion").ToString().Trim
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Lee los datos del tarifario, si el contrato no cuenta con sus propias tarifas.
    ''' </summary>
    ''' <param name="codProductoFinancieroActivo">Código del producto financiero</param>
    ''' <param name="codMoneda">código de la moneda del contrato</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerTarifas(ByVal codProductoFinancieroActivo As String, _
                            ByVal codMoneda As String)
        Dim objContratoNTx As New LContratoNTx

        ' Datos de las tarifas
        Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetTarifarioPredefContrato(codProductoFinancieroActivo, _
                                                                                                                             codMoneda))
        For Each oRow As DataRow In dtTarifas.Rows
            If txtImporteAtrasoPorc.Value = "" AndAlso _
                Not oRow.Item("PorcentajeImportePendiente") Is DBNull.Value Then
                txtImporteAtrasoPorc.Value = Format(CDbl(oRow.Item("PorcentajeImportePendiente")), "#,##0.000")
            End If
            If txtPorcentajeCuota.Value = "" AndAlso _
                Not oRow.Item("PorcentajeCuota") Is DBNull.Value Then
                txtPorcentajeCuota.Value = Format(CDbl(oRow.Item("PorcentajeCuota")), "#,##0.00")
            End If
            If txtdiasVencimiento.Value = "" Then
                txtdiasVencimiento.Value = GCCConstante.CONTRATO_DIAS_VENCIMIENTO.ToString
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Lee los datos del contrato, a partir de su respectivo código y los carga en sus respectivos controles.
    ''' </summary>
    ''' <param name="codSolicitudCredito">Código del contrato</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerContrato(ByVal codSolicitudCredito As String)
        Dim objContratoNTx As New LContratoNTx

        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(codSolicitudCredito))

        For Each oRow As DataRow In dtContrato.Rows
            ' Datos del contrato
            txtNroContrato.Value = codSolicitudCredito
            If Not oRow.Item("TipoRubroFinanciamiento") Is DBNull.Value Then
                hddTipoRubroFinanciamiento.Value = oRow.Item("TipoRubroFinanciamiento").ToString().Trim()
            End If
            If Not oRow.Item("CodigoEstadoContrato") Is DBNull.Value Then
                hddCodigoEstadoContrato.Value = oRow.Item("CodigoEstadoContrato").ToString().Trim()
            End If

            If Not oRow.Item("CodProductoFinancieroActivo") Is DBNull.Value Then
                hddCodProductoFinancieroActivo.Value = oRow.Item("CodProductoFinancieroActivo").ToString().Trim()
            End If
            If Not oRow.Item("CodigoCotizacion") Is DBNull.Value Then
                hddCodigoCotizacion.Value = oRow.Item("CodigoCotizacion").ToString().Trim()
            End If
            If Not oRow.Item("EstadoContrato") Is DBNull.Value Then
                txtEstadoDelContrato.Value = oRow.Item("EstadoContrato").ToString().Trim()
            End If
            If Not oRow.Item("FechaContrato") Is DBNull.Value Then
                txtFechaContrato.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaContrato").ToString(), "C")
            End If
            If Not oRow.Item("FechaMaxDisponible") Is DBNull.Value Then
                txtFechamaxdisp.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaMaxDisponible").ToString(), "C")
            End If
            If Not oRow.Item("FechaMaxActivacion") Is DBNull.Value Then
                txtFechaActivacion.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaMaxActivacion").ToString(), "C")
            End If
            If Not oRow.Item("PeridoDisponible") Is DBNull.Value Then
                txtPeriodoDisponible.Value = oRow.Item("PeridoDisponible").ToString().Trim()
            End If
            If Not oRow.Item("ClasificacionBien") Is DBNull.Value Then
                txtClasificacionDelBien.Value = oRow.Item("ClasificacionBien").ToString().Trim()
            End If
            If Not oRow.Item("Procedencia") Is DBNull.Value Then
                txtProcedencia.Value = oRow.Item("Procedencia").ToString().Trim()
            End If
            If Not oRow.Item("CodMoneda") Is DBNull.Value Then
                hddCodMoneda.Value = oRow.Item("CodMoneda").ToString().Trim()
            End If
            If Not oRow.Item("NombreMoneda") Is DBNull.Value Then
                txtMoneda.Value = oRow.Item("NombreMoneda").ToString().Trim()
            End If
            If Not oRow.Item("MontoFinanciamiento") Is DBNull.Value Then
                txtMontoFinanciado.Value = Format(CDbl(oRow.Item("MontoFinanciamiento")), "#,##0.00")
            End If
            If Not oRow.Item("CodigoClasificacionContrato") Is DBNull.Value Then
                hddCodigoClasificacionContrato.Value = oRow.Item("CodigoClasificacionContrato").ToString()
            End If
            If Not oRow.Item("FechaRegistroPublico") Is DBNull.Value Then
                txtFechaRegistroPublico.Value = GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaRegistroPublico").ToString())
            End If
            If Not oRow.Item("FechaFirmaNotaria") Is DBNull.Value Then
                txtFechaFirmaNotaria.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaFirmaNotaria").ToString(), "C")
                hddFechaFirmaNotaria.Value = GCCUtilitario.CheckDateString(oRow.Item("FechaFirmaNotaria").ToString(), "C")
                chkFirmaNotaria.Checked = True
            End If
            If Not oRow.Item("ArchivoContratoAdjunto") Is DBNull.Value Then
                hddAdjuntarArchivo.Value = oRow.Item("ArchivoContratoAdjunto").ToString().Trim()
            End If

            ' Estado civil del cliente
            If Not oRow.Item("CodigoEstadoCivil") Is DBNull.Value Then
                hddCodigoEstadoCivil.Value = oRow.Item("CodigoEstadoCivil").ToString().Trim
            End If

            ' Datos Otros conceptos
            ' Tasas y comisiones
            If Not oRow.Item("MontoTEAPorc") Is DBNull.Value Then
                txtTea.Value = Format(CDbl(oRow.Item("MontoTEAPorc")), "#,##0.00")
            End If
            If Not oRow.Item("MontoPreCuotaPorc") Is DBNull.Value Then
                txtprecuota.Value = Format(CDbl(oRow.Item("MontoPreCuotaPorc")), "#,##0.00")
            End If
            If Not oRow.Item("OpcionCompra") Is DBNull.Value Then
                txtOpcionCompra.Value = Format(CDbl(oRow.Item("OpcionCompra")), "#,##0.00")
            End If
            If Not oRow.Item("ComisionActivacion") Is DBNull.Value Then
                txtComisionActivacion.Value = Format(CDbl(oRow.Item("ComisionActivacion")), "#,##0.00")
            End If
            If Not oRow.Item("ComisionEstructuracion") Is DBNull.Value Then
                txtComisionEstructuracion.Value = Format(CDbl(oRow.Item("ComisionEstructuracion")), "#,##0.00")
            End If
            If Not oRow.Item("OtrasComisiones") Is DBNull.Value Then
                txtaOtrasComisiones.Value = oRow.Item("OtrasComisiones").ToString().Trim()
            End If

            ' Penalidades
            If Not oRow.Item("ImportePendiente") Is DBNull.Value Then
                txtImporteAtrasoPorc.Value = Format(CDbl(oRow.Item("ImportePendiente")), "#,##0.000")
            End If
            If Not oRow.Item("OtrasPenalidades") Is DBNull.Value Then
                txtaOtrasPenalidades.Value = oRow.Item("OtrasPenalidades").ToString().Trim()
            End If
            If Not oRow.Item("DiasVencimiento") Is DBNull.Value Then
                txtdiasVencimiento.Value = oRow.Item("DiasVencimiento").ToString().Trim()
            End If
            If Not oRow.Item("NombreArchivo") Is DBNull.Value Then
                hddAdjuntarArchivoOtroConcepto.Value = oRow.Item("NombreArchivo").ToString().Trim()
            End If
            If Not oRow.Item("PorcentajeCuota") Is DBNull.Value Then
                txtPorcentajeCuota.Value = Format((oRow.Item("PorcentajeCuota")), "#,##0.00")
            End If
            ' Indica si el contrato a sido modificado.
            If oRow.Item("FlagModificado") Is DBNull.Value Then
                hddFlagModificado.Value = "0"
            Else
                If CBool(oRow.Item("FlagModificado")) Then
                    hddFlagModificado.Value = "1"
                Else
                    hddFlagModificado.Value = "0"
                End If
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Lee los datos de la cotización, a partir de su respectivo código y carga los datos en sus respectivos controles.
    ''' </summary>
    ''' <param name="Codigocotizacion">Código de la cotización</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub LeerCotizacion(ByVal Codigocotizacion As String)
        If Codigocotizacion.Trim <> "" Then
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String
            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(hddCodigoCotizacion.Value)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)

            ' Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            For Each oRow As DataRow In dtCotizacion.Rows
                If Not oRow.Item("CodigoTipoBien") Is DBNull.Value Then
                    hddCodigoTipoBien.Value = oRow.Item("CodigoTipoBien").ToString().Trim
                End If
                If Not oRow.Item("NombreTipoBien") Is DBNull.Value Then
                    txtTipoDeBien.Value = oRow.Item("NombreTipoBien").ToString().Trim
                End If
                If hddCodigoTipoPersona.Value.Trim() = "" Then
                    If Not oRow.Item("CodigoTipoPersona") Is DBNull.Value Then
                        hddCodigoTipoPersona.Value = oRow.Item("CodigoTipoPersona").ToString().Trim
                    End If
                End If
                If txtTipoPersona.Value.Trim() = "" Then
                    If Not oRow.Item("NombreTipopersona") Is DBNull.Value Then
                        txtTipoPersona.Value = oRow.Item("NombreTipopersona").ToString().Trim
                    End If
                End If
                If hddTipoDocumento.Value.Trim() = "" Then
                    If Not oRow.Item("CodigoTipoDocumento") Is DBNull.Value Then
                        hddTipoDocumento.Value = oRow.Item("CodigoTipoDocumento").ToString().Trim
                    End If
                End If
                If txtTipoDocumento.Value.Trim() = "" Then
                    If Not oRow.Item("NombreTipoDocumento") Is DBNull.Value Then
                        txtTipoDocumento.Value = oRow.Item("NombreTipoDocumento").ToString().Trim
                    End If
                End If
                If txtNroDeDocumento.Value.Trim() = "" Then
                    If Not oRow.Item("NumeroDocumento") Is DBNull.Value Then
                        txtNroDeDocumento.Value = oRow.Item("NumeroDocumento").ToString().Trim
                    End If
                End If
                If Not oRow.Item("Codigoestadobien") Is DBNull.Value Then
                    hddCodigoEstadoBien.Value = oRow.Item("Codigoestadobien").ToString().Trim()
                End If
                If Not oRow.Item("Correocontacto") Is DBNull.Value Then
                    hddCorreocontacto.Value = oRow.Item("Correocontacto").ToString().Trim
                End If

                ' Ejecutivo Leasing
                txtEjecutivoLeasing.Value = oRow.Item("NombreEjecutivoleasing").ToString().Trim

            Next oRow
        End If
    End Sub

    ''' <summary>
    ''' Establece algunos valores predeterminados, según el número de contrato.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub ValoresPredeterminados()
        ' Se carga el estado predeterminado sólo si esta en estado 03
        If hddCodigoEstadoContrato.Value = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ELABORADO Then
            If hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_BIEN_INMUEBLE AndAlso _
                hddCodigoTipoBien.Value <> GCCConstante.C_TIPO_BIEN_EMBARCACION Then
                ' Bien inmueble que no sea embarcación pesquera
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_INMUEBLE
            ElseIf hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_BIEN_INMUEBLE AndAlso _
                   hddCodigoTipoBien.Value = GCCConstante.C_TIPO_BIEN_EMBARCACION Then
                ' Bien inmueble que sea embarcación pesquera
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA
            ElseIf hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_INDUSTRIAL OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_OFICINA OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_MOV_TIERRA OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_SISTEMA_PROC_DATOS OrElse _
                hddTipoRubroFinanciamiento.Value = GCCConstante.C_TIPO_RUBRO_FINANC_OTROS Then
                ' Maquinaria que no sea vehículo
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_MUEBLE
            Else
                ' Vehículo
                hddClasifContratoSeleccion.Value = GCCConstante.C_CLASIF_CONTRATO_VEHICULO
            End If
        End If

    End Sub

#End Region

#Region "Datos Notariales"

    ''' <summary>
    ''' Lista los documentos notariales del cliente para el actual contrato, organizados por páginas.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Número de contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                                           ByVal pCurrentPage As Integer, _
                                                           ByVal pSortColumn As String, _
                                                           ByVal pSortOrder As String, _
                                                           ByVal pContrato As String, _
                                                           ByVal pTipoDatoNotarial As String, _
                                                           ByVal pFields As String) As JQGridJsonResponse
        Dim oJQGridJsonResponse As New JQGridJsonResponse

        Try
            Dim objContratoNTx As New LContratoNTx

            Dim dtContratoNotarial As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoNotarialPaginado(pPageSize, _
                                                                                                                                               pCurrentPage, _
                                                                                                                                               pSortColumn, _
                                                                                                                                               pSortOrder, _
                                                                                                                                               pContrato, _
                                                                                                                                               pTipoDatoNotarial))


            'Total de registros a mostrar.
            Dim totalRecords As Integer
            Dim totalCurrent As Integer

            If dtContratoNotarial.Rows.Count = 0 Then
                totalRecords = 0
                totalCurrent = 0
            Else
                totalRecords = Convert.ToInt32(dtContratoNotarial.Rows(0)("RecordCount"))
                totalCurrent = Convert.ToInt32(dtContratoNotarial.Rows(0)("TOTAL_PAGINA"))
            End If
            If pCurrentPage > totalCurrent Then
                pCurrentPage = totalCurrent
            End If

            'Número total de páginas
            Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                         pPageSize)
            Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                                   pCurrentPage, _
                                                                   totalRecords, _
                                                                   dtContratoNotarial, _
                                                                   pFields)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Condiciones Adicionales"

    ''' <summary>
    ''' Devuelve las condiciones adicionales correspondientes para el credito ingresado.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodigo">Número de contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListaCondicionesAdicionales(ByVal pPageSize As Integer, _
                                                       ByVal pCurrentPage As Integer, _
                                                       ByVal pSortColumn As String, _
                                                       ByVal pSortOrder As String, _
                                                       ByVal pCodigo As String, _
                                                       ByVal pFields As String) As JQGridJsonResponse
        Dim objCondicionAdicionalNTx As New LCheckListNTx

        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim strEGccContratodocumento As String

        oEGccContratodocumento.Numerocontrato = GCCUtilitario.NullableString(pCodigo)
        oEGccContratodocumento.Flagfiltro = 2

        strEGccContratodocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEGccContratodocumento)
        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCondicionAdicionalNTx.ContratoDocumentoSel(pPageSize, _
                                                                                                                                            pCurrentPage, _
                                                                                                                                            pSortColumn, _
                                                                                                                                            pSortOrder, _
                                                                                                                                            strEGccContratodocumento))

        'Número total de páginas
        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtCondicionAdicional.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtCondicionAdicional)
    End Function

#End Region

#Region "ContratoProveedor"

    ''' <summary>
    ''' Devuelve los proveedores del contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Número de contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarContratoProveedores(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pContrato As String, _
                                                     ByVal pFields As String) As JQGridJsonResponse
        Try
            Dim objContratoNTx As New LContratoNTx

            Dim dtContratoProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarDistinctContratoProveedores(pPageSize, _
                                                                                                                                                    pCurrentPage, _
                                                                                                                                                    pSortColumn, _
                                                                                                                                                    pSortOrder, _
                                                                                                                                                    pContrato))


            Dim oJQGridJsonResponse As New JQGridJsonResponse

            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtContratoProveedores.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(dtContratoProveedores.Rows(0)("RecordCount"))
            End If
            ' Número total de páginas
            Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                         pPageSize)

            Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                                   pCurrentPage, _
                                                                   totalRecords, _
                                                                   dtContratoProveedores, _
                                                                   pFields)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Bienes"

    ''' <summary>
    ''' Lista los bienes del cliente para el actual contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Número de contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarBienes(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pContrato As String, _
                                        ByVal pFields As String) As JQGridJsonResponse
        Dim objContratoNTx As New LContratoNTx

        Dim dtBienesProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       pContrato, _
                                                                                                                       Nothing))

        'Número total de páginas
        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtBienesProveedor.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtBienesProveedor.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtBienesProveedor.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtBienesProveedor, _
                                                               pFields)
    End Function

#End Region

#Region "Representantes Banco"

    ''' <summary>
    ''' Lista los representantes del cliente para el actual contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato">Número de contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListaRepresentantes(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pNumeroContrato As String, _
                                               ByVal pCodigoTipoRepresentante As String, _
                                               ByVal pFields As String) As JQGridJsonResponse
        Dim objRepresentanteContratoNTx As New LCheckListNTx()
        Dim oEGccRepresentanteContrato As New EGcc_contratorepresentante
        Dim strEGccontratoRepresentante As String

        With oEGccRepresentanteContrato
            .Numerocontrato = GCCUtilitario.NullableString(pNumeroContrato)
            .Codigotiporepresentante = GCCUtilitario.NullableString(pCodigoTipoRepresentante)
        End With

        strEGccontratoRepresentante = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteContrato)
        Dim dtContratoRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(pPageSize, _
                                                                                                                                                       pCurrentPage, _
                                                                                                                                                       pSortColumn, _
                                                                                                                                                       pSortOrder, _
                                                                                                                                                       strEGccontratoRepresentante, _
                                                                                                                                                       Nothing))

        ' Número total de páginas
        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtContratoRepresentante.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtContratoRepresentante.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtContratoRepresentante.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtContratoRepresentante, _
                                                               pFields)

    End Function

#End Region

#End Region

End Class
