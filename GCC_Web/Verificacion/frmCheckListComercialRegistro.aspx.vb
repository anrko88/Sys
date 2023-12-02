Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports GCC.Entity.EGcc_contratocuenta

Partial Class Verificacion_frmCheckListComercialRegistro

    Inherits GCCBase

    Dim objLog As New GCCLog("frmCheckListComercialRegistro.aspx.vb")
    Public Const C_ORIGENDOC_CHECKLIST_COMERCIAL As String = "002"
    Dim strNroContrato As String

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

                strNroContrato = Request.QueryString("cc")
                hddconsOrigenCondicion.value = GCCConstante.C_ORIGENDOC_CHECKLIST_COMERCIAL
                pInicializarControles()
                'AAE - 11/09/2012 - Cargo el perfil
                hddPerfil.Value = GCCSession.PerfilUsuario
                'FIN AAE
                'txtNumeroCotizacion.Value = "0000000001"
                'txtCUCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnRmCliente.UniqueID + "').click();return false;}} else {return true}; ")

                'GCCUtilitario.CargarComboValorGenerico(Me.cmbCondicionesAdicionales, GCCConstante.C_TABLAGENERICA_CONDICIONES_ADICIONALES)
                ''Agregar metodo Cragra combos
                GCCUtilitario.CargarComboValorGenerico(Me.cmbcontratofirma, GCCConstante.C_TABLAGENERICA_FIRMA_EN)
                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda1)
                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda2)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta1, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta2, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamentoUbicacion)

                Dim objDContratoCuentaNTx As New LContratoNTx
                Dim dtContratoCuenta As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objDContratoCuentaNTx.RetornarContratoCuentas(strNroContrato))

                Dim contador As Integer = 1
                For Each oRow As DataRow In dtContratoCuenta.Rows
                    If contador = 1 Then
                        If Not oRow.Item("Cuenta") Is DBNull.Value Then
                            txtNumeroCuenta1.Value = oRow.Item("Cuenta").ToString().Trim()
                        End If
                        If Not oRow.Item("CodigoTipoCuenta") Is DBNull.Value Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "ComboNumeroCuenta1", "fn_util_SeteaComboServidor('cmbTipoCuenta1','" + oRow.Item("CodigoTipoCuenta").ToString.Trim + "');", True)
                        End If

                        If Not oRow.Item("CodigoMoneda") Is DBNull.Value Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "ComboMoneda", "fn_util_SeteaComboServidor('cmbMoneda1','" + oRow.Item("CodigoMoneda").ToString.Trim + "');", True)
                        End If
                    End If
                    If contador = 2 Then
                        If Not oRow.Item("Cuenta") Is DBNull.Value Then
                            txtNumeroCuenta2.Value = oRow.Item("Cuenta").ToString().Trim()
                        End If
                        If Not oRow.Item("CodigoTipoCuenta") Is DBNull.Value Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "ComboNumeroCuenta2", "fn_util_SeteaComboServidor('cmbTipoCuenta2','" + oRow.Item("CodigoTipoCuenta").ToString.Trim + "');", True)
                        End If

                        If Not oRow.Item("CodigoMoneda") Is DBNull.Value Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "ComboMoneda2", "fn_util_SeteaComboServidor('cmbMoneda2','" + oRow.Item("CodigoMoneda").ToString.Trim + "');", True)
                        End If
                    End If
                    contador = contador + 1
                Next

                'Valida Bloqueo
                GestionBloqueo(strNroContrato)

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
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Try
            txtNumeroContrato.Value = strNroContrato
            'Inicio IBK - AAE - 11/02/2013 - Clono método de búsqueda
            'Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(txtNumeroContrato.Value))
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion2(txtNumeroContrato.Value))
            'Fin IBK
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtNumeroCotizacion.Value = dr("CODIGOCOTIZACION").ToString
                        txtclasificacionBien.Value = dr("NOMBRECLASIFICACIONBIEN").ToString
                        txtCuCliente.Value = dr("CODUNICO").ToString
                        hidCodUnico.Value = dr("CODUNICO").ToString
                        txtTipoPersona.Value = dr("NOMBRETIPOPERSONA").ToString
                        txtTipoInmueble.Value = dr("NOMBRETIPOBIEN").ToString
                        txtProcedencia.Value = dr("NOMBREPROCEDENCIA").ToString
                        txtNombreRazonSocial.Value = dr("NOMBRECLIENTE").ToString
                        txtCuCliente.Value = dr("CODUNICO").ToString
                        txtMonedaContrato.Value = dr("CODMONEDA").ToString
                        If (dr("USO").ToString) <> "" Then
                            txtUso.Value = dr("USO").ToString
                        Else
                            txtUso.Value = "Giro propio del negocio"
                        End If
                        If (dr("UBICACION").ToString) <> "" Then
                            txtUbicacion.Value = dr("UBICACION").ToString
                            hidUbicacion.Value = dr("UBICACION").ToString
                        Else
                            txtUbicacion.Value = "<Ingresar dirección completa>"
                        End If
                        txtEstadoBien.Value = dr("ESTADODELBIEN").ToString
                        'txtUbicacion.Value = dr("codigolugarfirmacontrato").ToString

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "cmbcontratofirma", "fn_util_SeteaComboServidor('cmbcontratofirma','" + RTrim(dr("CODIGOLUGARFIRMACONTRATO").ToString) + "');", True)
                        hddFirmaen.Value = RTrim(dr("CODIGOLUGARFIRMACONTRATO").ToString)
                        'chkTerminoRecepcion.Checked = CBool(dr("FLAGTERMINORECEPDOCUMENTOCLIE").ToString)
                        'txtFechaTerminoRecepcion.Value = dr("FECHATERMINORECEPDOCUMENTOCLIE").ToString

                        hddUbigeoUbicacion.Value = RTrim(dr("UBIGEO").ToString)
                        'Dim sDepartamento As String
                        'sDepartamento = Mid(hddUbigeoUbicacion.Value, 1, 2)

                        hddFlagRetorno.value = RTrim(dr("FLAGRETORNO").ToString)
                        'cmbDepartamentoUbicacion.Value = sDepartamento

                        'Inicio IBK - AAE - 11/02/2013 - Cargo info extra de la cotización
                        Dim strFechaActivacion As String
                        Dim strFechaDisponibilidad As String
                        hidTipoCronograma.Value = RTrim(dr("CODIGOFRECUENCIAPAGO"))
                        If IsDBNull(dr("FECHAMAXACTIVACION")) Then
                            strFechaActivacion = ""
                        Else
                            strFechaActivacion = RTrim(dr("FECHAMAXACTIVACION"))
                        End If
                        If IsDBNull(dr("FECHAOFERTAVALIDA")) Then
                            strFechaDisponibilidad = ""
                        Else
                            strFechaDisponibilidad = RTrim(dr("FECHAOFERTAVALIDA"))
                        End If

                        txtDirCliente.Value = RTrim(dr("DOMICILIOCLIENTE"))
                        txtFechaMaxActivacion.Value = strFechaActivacion 'GCCUtilitario.CheckDateString(strFechaActivacion, "C")
                        txtFechaDisponibilidad.Value = strFechaDisponibilidad 'GCCUtilitario.CheckDateString(strFechaDisponibilidad, "C")
                        txtOpcionCompraMonto.Value = RTrim(dr("IMPORTEOPCIONCOMPRA"))
                        hidOpcionCompra.Value = RTrim(dr("IMPORTEOPCIONCOMPRA"))
                        txtComisionActivacionMonto.Value = RTrim(dr("IMPORTECOMISIONACTIVACION"))
                        hidComActivacion.Value = RTrim(dr("IMPORTECOMISIONACTIVACION"))
                        txtComisionEstructuracionMonto.Value = RTrim(dr("IMPORTECOMISIONESTRUCTURACION"))
                        hidComestructuracion.Value = RTrim(dr("IMPORTECOMISIONESTRUCTURACION"))
                        'Lineas 
                        Dim sbResultado As New StringBuilder
                        Try
                            Dim strCodUnico As String = dr("CODUNICO").ToString.Trim
                            Dim objListELinea As New ListELinea
                            sbResultado = ListaLineas(strCodUnico, objListELinea)
                            'Combo
                            cmbLinea.DataSource = objListELinea
                            cmbLinea.DataTextField = "ClaveCL"
                            cmbLinea.DataValueField = "ClaveCL"
                            cmbLinea.DataBind()
                            GCCUtilitario.pInsertarPrimerItemHtmlSelect(cmbLinea, "[-Seleccione-]", "0")
                            GCCUtilitario.SeleccionaCombo(cmbLinea, dr("NUMEROLINEA").ToString.Trim)
                        Catch ex As Exception
                            GCCUtilitario.pInsertarPrimerItemHtmlSelect(cmbLinea, "No se puede conectar con Lineas", "0")
                            'msgError = "No se puede conectar con Lineas"
                        End Try
                        'Fin IBK - AAE

                        dr = Nothing
                        Exit For
                    Next
                End If
                odtbDatos.Dispose()
            End If


            Dim objContratoProveedorNTx As New LProveedorNTx

            Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoProveedorNTx.ListadoContratoProveedor(txtNumeroContrato.Value))
            'Dim totalRecords As Integer

            If dtProveedor.Rows.Count = 0 Then
                hddTotalRegistrosProveedor.Value = "0"
            Else
                hddTotalRegistrosProveedor.Value = dtProveedor.Rows.Count.ToString
            End If

            'hddTotalRegistrosProveedor.Value = totalRecords
            'Dim objJQGridJsonResponse As New JQGridJsonResponse
            'Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 10, dtProveedor)


            'Dim strContacto As String = oLwsDocClienteNtx.ObtenerContacto(txtNroCotizacion.Value, txtNumeroContrato.Value)
            'If Not String.IsNullOrEmpty(strContacto) Then
            'Dim oEContacto As EGcc_contacto = GCCUtilitario.DeserializeObject(Of EGcc_contacto)(strContacto)
            'txtContacto.Value = oEContacto.Nombre
            'txtCorreo.Value = oEContacto.Correo
            'txtTelefonos.Value = oEContacto.Telefono
            'txtAnexo.Value = oEContacto.Anexo
            'hidCodContacto.Value = oEContacto.Codigocontacto
            'End If
            'GCCUtilitario.CargarComboValorGenerico(cmbCondicionesAdicionales, GCCConstante.C_TABLAGENERICA_CONDICIONES_ADICIONALES)
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteNtx = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Actualiza Bloquero
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ActualizaBloqueo(ByVal pstrCodBloqueo As String) As String

        Try
            Dim objEBloqueo As New EGCC_Bloqueo
            With objEBloqueo
                .CodigoBloqueo = GCCUtilitario.CheckInt(pstrCodBloqueo)
                .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .NombreUsuario = GCCUtilitario.NullableString(GCCSession.NombreUsuario)
                .Activo = "1"
            End With
            Dim objLUtilTX As New LUtilTX
            objLUtilTX.ModificarBloqueo(GCCUtilitario.SerializeObject(objEBloqueo))

            Return "0"
        Catch ex As Exception
            Return "1"
        End Try

    End Function

    'Inicio IBK - AAE - 11/02/2013 - Funcion que lista líneas de cotizacipon
    Public Shared Function ListaLineas(ByVal pstrCodUnico As String, ByRef objListELinea As ListELinea) As StringBuilder

        '-------------------------------------------
        ' Variables para Consulta
        '-------------------------------------------
        Dim strMensaje As String = "No se encontraron Lineas para el Cliente"
        Dim sbResultado As New StringBuilder
        sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "- Seleccione -"))

        'Parametros
        Dim strCodigoUnico As String = pstrCodUnico
        Dim strCodigoProducto As String = GCCConstante.C_CODIGO_LEASING_LINEAS

        Try

            'Consulta lineas
            Dim objLWebService As New LWebService
            Dim strLineas As String = objLWebService.fObtenerLineaOP(strCodigoUnico.Trim.PadLeft(14, "0"), GCCConstante.eTipoOperacion.Linea, GCCUtilitario.CheckInt(strCodigoProducto))
            Dim strOperaciones As String = objLWebService.fObtenerLineaOP(strCodigoUnico.Trim.PadLeft(14, "0"), GCCConstante.eTipoOperacion.Operacion, GCCUtilitario.CheckInt(strCodigoProducto))

            'Lineas
            If Not strLineas Is Nothing Then
                If Not strLineas.Trim().Equals("") Then
                    Dim odtbLineas As DataTable = GCCUtilitario.DeserializeObjectXML(Of DataTable)(strLineas)
                    If Not odtbLineas Is Nothing Then
                        If odtbLineas.Rows.Count > 0 Then
                            For Each oRow As DataRow In odtbLineas.Rows
                                Dim objELinea As New ELinea
                                With objELinea
                                    .CodigoLineaOperacion = oRow.Item("CODIGOLINEAOPERACION").ToString()
                                    .NumeroLinea = oRow.Item("NUMEROLINEA").ToString()
                                    .ClaveCL = oRow.Item("CLAVECL").ToString()
                                    .RazonSocial = oRow.Item("RAZONSOCIAL").ToString()
                                End With
                                If Not objELinea.ClaveCL.Trim.Equals("") Then
                                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(objELinea.ClaveCL, objELinea.ClaveCL))
                                    objListELinea.Add(objELinea)
                                End If
                            Next oRow
                        End If
                    End If
                End If
            End If

            'Operaciones
            If Not strOperaciones Is Nothing Then
                If Not strOperaciones.Trim().Equals("") Then
                    Dim odtbOperaciones As DataTable = GCCUtilitario.DeserializeObjectXML(Of DataTable)(strOperaciones)
                    If Not odtbOperaciones Is Nothing Then
                        If odtbOperaciones.Rows.Count > 0 Then
                            For Each oRow As DataRow In odtbOperaciones.Rows
                                Dim objELineaOp As New ELinea
                                With objELineaOp
                                    .CodigoLineaOperacion = oRow.Item("CODIGOLINEAOPERACION").ToString()
                                    .NumeroLinea = oRow.Item("NUMEROLINEA").ToString()
                                    .ClaveCL = oRow.Item("CLAVECL").ToString()
                                    .RazonSocial = oRow.Item("RAZONSOCIAL").ToString()
                                End With
                                If Not objELineaOp.ClaveCL.Trim.Equals("") Then
                                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(objELineaOp.ClaveCL, objELineaOp.ClaveCL))
                                    objListELinea.Add(objELineaOp)
                                End If
                            Next oRow
                        End If
                    End If
                End If
            End If

            Return sbResultado

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Fin IBK
#End Region

#Region "Web Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaCondicionesAdicionales(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pCodigo As String, _
                                                      ByVal pFlagFiltro As Integer, _
                                                      ByVal pFlagEnvioCarta As Integer) As JQGridJsonResponse

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
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        'Dim total2 As Decimal
        If dtCondicionAdicional.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
            'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
            intTotalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        ' Return JQGridJsonResponse.JQGridJsonResponseClass(totalPages, currentPage, totalRecords, temporals)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCondicionAdicional)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pfirma"></param>
    ''' <param name="pUbigeo"></param>
    ''' <param name="pTipoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function Listarepresentantes(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pfirma As String, _
                                                   ByVal pUbigeo As String, _
                                                   ByVal pTipoRepresentante As String, _
                                                   ByVal pNumeroContrato As String) As JQGridJsonResponse

        Dim objRepresentantesNTx As New LCheckListNTx()
        Dim oEGccRepresentante As New EGcc_representante
        Dim strEGcRepresentante As String
        With oEGccRepresentante

            .Codigoubigeo = GCCUtilitario.NullableString(pUbigeo)
            .Codigotiporepresentante = GCCUtilitario.NullableString(pTipoRepresentante)
        End With
        strEGcRepresentante = GCCUtilitario.SerializeObject(Of EGcc_representante)(oEGccRepresentante)
        Dim dtRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentantesNTx.RepresentantesSel(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   pfirma.ToString.PadLeft("3", "0"), _
                                                                                                                   strEGcRepresentante, _
                                                                                                                   pNumeroContrato))
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtRepresentante.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtRepresentante.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtRepresentante.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtRepresentante)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoTipoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaRepresentantesContrato(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pNumeroContrato As String, _
                                                   ByVal pCodigoTipoRepresentante As String, _
                                                   ByVal pfirma As String) As JQGridJsonResponse

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
                                                                                                                   strEGccontratoRepresentante, pfirma))
        '' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtContratoRepresentante.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtContratoRepresentante.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtContratoRepresentante.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtContratoRepresentante)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoTipoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaRepresentantesContratoTotal(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pNumeroContrato As String, _
                                                   ByVal pCodigoTipoRepresentante As String, _
                                                   ByVal pfirma As String) As Integer

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
                                                                                                                   strEGccontratoRepresentante, pfirma))
        '' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtContratoRepresentante.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtContratoRepresentante.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtContratoRepresentante.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        Return totalRecords
    End Function

    ''' <summary>
    ''' InsertaRepresentanteContrato
    ''' </summary>
    ''' <param name="pCodigoRepresentante"></param>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoTipoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function InsertaRepresentanteContrato(ByVal pCodigoRepresentante As Integer, _
                                                        ByVal pNumeroContrato As String, _
                                                        ByVal pCodigoTipoRepresentante As String) As String

        Try
            Dim objRepresentanteContratoTx As New LCheckListTx
            Dim oEGccRepresentanteContrato As New EGcc_contratorepresentante
            Dim strEGccontratoRepresentante As String
            With oEGccRepresentanteContrato
                .Codigorepresentante = pCodigoRepresentante
                .Numerocontrato = GCCUtilitario.NullableString(pNumeroContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(pCodigoTipoRepresentante)
                .Audestadologico = 1
                .Audfecharegistro = Now
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With


            strEGccontratoRepresentante = GCCUtilitario.SerializeObject(oEGccRepresentanteContrato)
            Dim intResult As Integer = objRepresentanteContratoTx.RepresentanteContratoIns(strEGccontratoRepresentante)


            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' EliminaRepresentanteContratoItem
    ''' </summary>
    ''' <param name="pCodigoRepresentante"></param>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoTipoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function EliminaRepresentanteContratoItem(ByVal pCodigoRepresentante As Integer, _
                                                    ByVal pNumeroContrato As String, _
                                                    ByVal pCodigoTipoRepresentante As String) As String

        Try
            Dim objRepresentanteEliminaTx As New LCheckListTx
            Dim oEGccRepresentanteContrato As New EGcc_contratorepresentante
            Dim strEGccontratoRepresentante As String
            With oEGccRepresentanteContrato
                .Codigorepresentante = pCodigoRepresentante
                .Numerocontrato = GCCUtilitario.NullableString(pNumeroContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(pCodigoTipoRepresentante)
                .Audestadologico = 1
                .Audfecharegistro = Now
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            strEGccontratoRepresentante = GCCUtilitario.SerializeObject(oEGccRepresentanteContrato)
            Dim intResult As Boolean = objRepresentanteEliminaTx.RepresentanteContratoItemDel(strEGccontratoRepresentante)

            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' EliminaRepresentanteContrato
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoTipoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Function EliminaRepresentanteContrato(ByVal pNumeroContrato As String, _
                                                       ByVal pCodigoTipoRepresentante As String) As String

        Try
            Dim objRepresentanteEliminaTx As New LCheckListTx
            Dim oEGccRepresentanteContrato As New EGcc_contratorepresentante
            Dim strEGccontratoRepresentante As String
            With oEGccRepresentanteContrato

                .Numerocontrato = GCCUtilitario.NullableString(pNumeroContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(pCodigoTipoRepresentante)
                .Audestadologico = 1
                .Audfecharegistro = Now
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With


            strEGccontratoRepresentante = GCCUtilitario.SerializeObject(oEGccRepresentanteContrato)
            Dim intResult As Boolean = objRepresentanteEliminaTx.RepresentanteContratoDel(strEGccontratoRepresentante)


            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' ActualizarSolicitudCredito
    ''' </summary>
    ''' <param name="strCodigo"></param>
    ''' <param name="strUso"></param>
    ''' <param name="strUbicacion"></param>
    ''' <param name="strNumeroCuenta1"></param>
    ''' <param name="strCodigoTipoCuenta1"></param>
    ''' <param name="strCodigoMoneda1"></param>
    ''' <param name="strNumeroCuenta2"></param>
    ''' <param name="strCodigoTipoCuenta2"></param>
    ''' <param name="strCodigoMoneda2"></param>
    ''' <param name="strcontratofirma"></param>
    ''' <param name="strUbigeofirma"></param>
    ''' <param name="strEnvioCorreo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Function ActualizarSolicitudCredito(ByVal strCodigo As String, _
                                        ByVal strUso As String, _
                                        ByVal strUbicacion As String, _
                                        ByVal strNumeroCuenta1 As String, _
                                        ByVal strCodigoTipoCuenta1 As String, _
                                        ByVal strCodigoMoneda1 As String, _
                                        ByVal strNumeroCuenta2 As String, _
                                        ByVal strCodigoTipoCuenta2 As String, _
                                        ByVal strCodigoMoneda2 As String, _
                                        ByVal strcontratofirma As String, _
                                        ByVal strUbigeofirma As String, _
                                        ByVal strUbigeoUbicacionBien As String, _
                                        ByVal strEnvioCorreo As String, _
                                        ByVal strRazonSocial As String, _
                                        ByVal strDireccionCliente As String, _
                                        ByVal strNroLinea As String, _
                                        ByVal strFechaActivacion As String, _
                                        ByVal strFechaDisponibilidad As String, _
                                        ByVal strOpcionCompra As String, _
                                        ByVal strComActivacion As String, _
                                        ByVal strComEstructuracion As String, _
                                        ByVal hidOpcionCompra As String, _
                                        ByVal hidComActivacion As String, _
                                        ByVal hidComEstructuracion As String) As String

        Try
            Dim objActualizaSolicitudCreditoTx As New LContratoTx
            Dim oLwsDocClienteNtx As New LDocClienteNTx
            'Inicio IBK - AAE
            Dim nbrOpcionCompra As Decimal = -1
            Dim nbrComActivacion As Decimal = -1
            Dim nbrComEstructuracion As Decimal = -1
            Dim strFechaAct As String
            Dim strFechaDisp As String
            If String.IsNullOrEmpty(strFechaActivacion) Then
                strFechaAct = strFechaActivacion
            Else
                strFechaAct = strFechaActivacion.Substring(6, 4) + strFechaActivacion.Substring(3, 2) + strFechaActivacion.Substring(0, 2)
            End If
            If String.IsNullOrEmpty(strFechaDisponibilidad) Then
                strFechaDisp = strFechaDisponibilidad
            Else
                strFechaDisp = strFechaDisponibilidad.Substring(6, 4) + strFechaDisponibilidad.Substring(3, 2) + strFechaDisponibilidad.Substring(0, 2)
            End If
            'Fin IBK
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(strCodigo))
            'Inicio IBK - AAE - 11/02/2013 - agrego a la cotizacion
            Dim oECotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String
            With oECotizacion
                .CodigoContrato = GCCUtilitario.NullableString(strCodigo)
                .NombreCliente = GCCUtilitario.NullableString(strRazonSocial)
                .DireccionCliente = GCCUtilitario.NullableString(strDireccionCliente)
                If strNroLinea.Trim().Equals("") Or strNroLinea.Trim().Equals("null") Then
                    strNroLinea = "0"
                End If
                .Numerolinea = GCCUtilitario.NullableString(strNroLinea)
                .Fechamaxactivacion = GCCUtilitario.StringToDateTime(strFechaAct)
                .FechaOfertaValida = GCCUtilitario.StringToDateTime(strFechaDisp)
                'Si son iguales no debo modificar la opción de compra ni comisiones
                If strOpcionCompra = hidOpcionCompra Then
                    nbrOpcionCompra = -1
                Else
                    nbrOpcionCompra = GCCUtilitario.StringToDecimal(strOpcionCompra)
                End If
                If strComActivacion = hidComActivacion Then
                    nbrComActivacion = -1
                Else
                    nbrComActivacion = GCCUtilitario.StringToDecimal(strComActivacion)
                End If
                If strComEstructuracion = hidComEstructuracion Then
                    nbrComEstructuracion = -1
                Else
                    nbrComEstructuracion = GCCUtilitario.StringToDecimal(strComEstructuracion)
                End If


                .Importeopcioncompra = nbrOpcionCompra
                .Importecomisionactivacion = nbrComActivacion
                .Importecomisionestructuracion = nbrComEstructuracion
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(oECotizacion)
            'Fin IBK
            Dim oESolicitudCreditoBien As New ESolicitudcredito

            Dim strESolicitudCreditoBien As String
            Dim strCorreo As String
            strCorreo = ""
            Dim oBase As New GCCBase
            With oESolicitudCreditoBien
                .Codsolicitudcredito = GCCUtilitario.NullableString(strCodigo)
                .Uso = GCCUtilitario.NullableString(strUso)
                .Ubicacion = GCCUtilitario.NullableString(strUbicacion)
                .codigolugarfirmacontrato = GCCUtilitario.NullableString(strcontratofirma)
                .codigoubigeolugarfirma = GCCUtilitario.NullableString(strUbigeofirma)
                .Ubigeo = GCCUtilitario.NullableString(strUbigeoUbicacionBien)
            End With

            'Solicitud Credito bien
            strESolicitudCreditoBien = GCCUtilitario.SerializeObject(oESolicitudCreditoBien)



            Dim objListEGcc_contratoCuenta As New ListEGcc_contratoCuenta
            Dim objEGcc_contratocuenta As New EGcc_contratocuenta
            Dim strListEGcc_contratoCuenta As String

            With objEGcc_contratocuenta
                .Codsolicitudcredito = GCCUtilitario.NullableString(strCodigo)
                .Codigotipocuenta = GCCUtilitario.NullableString(strCodigoTipoCuenta1)
                .Codigomoneda = GCCUtilitario.NullableString(strCodigoMoneda1)
                .Cuenta = GCCUtilitario.NullableString(strNumeroCuenta1)
                objListEGcc_contratoCuenta.Add(objEGcc_contratocuenta)
            End With

            objEGcc_contratocuenta = New EGcc_contratocuenta
            With objEGcc_contratocuenta

                .Codsolicitudcredito = GCCUtilitario.NullableString(strCodigo)
                .Codigotipocuenta = GCCUtilitario.NullableString(strCodigoTipoCuenta2)
                .Codigomoneda = GCCUtilitario.NullableString(strCodigoMoneda2)
                .Cuenta = GCCUtilitario.NullableString(strNumeroCuenta2)

                objListEGcc_contratoCuenta.Add(objEGcc_contratocuenta)
            End With

            strListEGcc_contratoCuenta = GCCUtilitario.SerializeObject(Of ListEGcc_contratoCuenta)(objListEGcc_contratoCuenta)
            Dim mbool As Boolean
            If odtbDatos.Rows.Count > 0 Then
                For Each dr As DataRow In odtbDatos.Rows
                    Dim pstrProdFinanActivo As String = dr("CODPRODUCTOFINANCIEROACTIVO").ToString
                    Dim pstrProdFinanPasivo As String = dr("CODPRODUCTOFINANCIEROPASIVO").ToString
                    Dim pstrTipoContrato As String = ""
                    Dim PstrTipoSubContrato As String = ""
                    Dim pstrSimbolo As String = ""
                    If dr("CODIGOMONEDA").ToString = "001" Then
                        pstrSimbolo = "S/."
                    Else
                        pstrSimbolo = "$"
                    End If
                    If dr("CODIGOSUBTIPOCONTRATO").ToString().Trim() = "001" Then
                        PstrTipoSubContrato = "Directo"
                    ElseIf dr("CODIGOSUBTIPOCONTRATO").ToString().Trim() = "002" Then
                        PstrTipoSubContrato = "Parcial"
                    End If

                    If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING_PAS) Then
                        pstrTipoContrato = IIf(GCCConstante.C_CODGCC_PROD_LEASING = "LD", "LEASING", "").ToString()
                    End If
                    If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS) Then
                        pstrTipoContrato = GCCConstante.C_DESGCC_PROD_LEASEBACK
                    End If
                    If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION_PAS) Then
                        pstrTipoContrato = GCCConstante.C_DESGCC_PROD_IMPORTACION
                    End If
                    'Inicio AAE - comento envío de mails hasta que se envíe a legal, no solo al guardar
                    If strEnvioCorreo = "SI" Then
                        mbool = oBase.EnviarMail("", strCorreo, "", "MailChecklistComercial", strCodigo, _
                                                 "", dr("NOMBRECLIENTE").ToString, "", _
                                                 dr("NOMBRECLASIFICACIONBIEN").ToString, _
                                                 dr("NOMBRETIPOBIEN").ToString, _
                                                 pstrTipoContrato + " " + UCase(PstrTipoSubContrato), pstrSimbolo + " " + Format(CDbl(dr("PRECIOVENTA").ToString()), "#,##0.00"), "", "")
                    Else
                        mbool = True
                    End If
                    'FIN AAE
                Next
            End If

            Dim intResult As Boolean
            'Inicio IBK - AAE - 11/02/2013 - Actualizo la info
            'intResult = objActualizaSolicitudCreditoTx.CheckLisComercialUpd(strESolicitudCreditoBien, strListEGcc_contratoCuenta)
            intResult = objActualizaSolicitudCreditoTx.CheckLisComercialUpd2(strESolicitudCreditoBien, strListEGcc_contratoCuenta, strEGcc_cotizacion)


            If mbool = False Then
                Throw New Exception("No se logró enviar el correo. Inténtelo mas tarde.")
            End If

            If intResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' EliminaContratoDocumento
    ''' </summary>
    ''' <param name="intContratoDocumento"></param>
    ''' <param name="strnumeroContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Function EliminaContratoDocumento(ByVal intContratoDocumento As Integer, _
                                                            ByVal strnumeroContrato As String) As String

        Try
            Dim objDocumentoContratoEliminaTx As New LContratoTx
            Dim oEGcDocumentoContrato As New EGcc_contratodocumento
            Dim strEGcDocumentoContrato As String
            With oEGcDocumentoContrato
                .Codigocontratodocumento = GCCUtilitario.CheckInt(intContratoDocumento)
                .Numerocontrato = GCCUtilitario.NullableString(strnumeroContrato)

            End With


            strEGcDocumentoContrato = GCCUtilitario.SerializeObject(oEGcDocumentoContrato)
            Dim intResult As Boolean = objDocumentoContratoEliminaTx.EliminaContratoDocumento(strEGcDocumentoContrato)


            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ActualizarGestionComercialEnviar
    ''' </summary>
    ''' <param name="strCodSolicitudCredito"></param>
    ''' <param name="strFlagTerminoRecepDocumentoClie"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
  Public Shared Function ActualizarGestionComercialEnviar(ByVal strCodSolicitudCredito As String, _
                                                           ByVal intFlagEnvioLegal As Integer) As String
        Try
            Dim objSolicitudcreditoTx As New LContratoTx
            Dim oEGcSolicitudcredito As New ESolicitudcredito
            Dim strEGcSolicitudcredito As String
            With oEGcSolicitudcredito
                .Codsolicitudcredito = GCCUtilitario.NullableString(strCodSolicitudCredito)
                .FlagEnvioLegal = intFlagEnvioLegal
                .Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_VERIFICADO
                ' GCCTS_JRC_20120220-Se necesita el Codigo de Usuari de Registro para el Seguimiento de Contrato 
                .AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                'GCCTS_JRC_20120304-Seguimiento Datos acionales
                .CodigoUsuario = GCCSession.CodigoUsuario.ToString()
                .NombreUsuario = GCCSession.NombreUsuario.ToString()
                .PerfilUsuario = GCCSession.DescripcionPerfil.ToString()

            End With

            strEGcSolicitudcredito = GCCUtilitario.SerializeObject(oEGcSolicitudcredito)
            Dim intResult As Boolean = objSolicitudcreditoTx.GestionComercialEnviarUpd(strEGcSolicitudcredito)
            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' ValidaCuentaSt
    ''' </summary>
    ''' <param name="argFCDTIPOCUENTA"></param>
    ''' <param name="argFCDCODMONEDA"></param>
    ''' <param name="argFCDCODTIENDA"></param>
    ''' <param name="argFCDCODCATEGORIA"></param>
    ''' <param name="argFCDNUMCUENTA"></param>
    ''' <param name="strUlrws"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ValidaCuentaST(ByVal argFCDTIPOCUENTA As String, _
                                          ByVal argFCDCODMONEDA As String, _
                                          ByVal argFCDCODTIENDA As String, _
                                          ByVal argFCDCODCATEGORIA As String, _
                                          ByVal argFCDNUMCUENTA As String, _
                                          ByVal argFCDTIPOCUENTA2 As String, _
                                          ByVal argFCDCODMONEDA2 As String, _
                                          ByVal argFCDCODTIENDA2 As String, _
                                          ByVal argFCDCODCATEGORIA2 As String, _
                                          ByVal argFCDNUMCUENTA2 As String, _
                                          ByVal pCodUnico As String) As String
        Dim sTldDatosTran As String = String.Empty
        Dim sMensaje As String = ""
        Dim oLWebService As New LWebService
        Dim sTldDatosTran2 As String
        Try
            Dim strUlrws As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsFCDCuenta")

            sTldDatosTran = oLWebService.fstrConsultarCuenta(argFCDTIPOCUENTA, argFCDCODMONEDA, argFCDCODTIENDA, argFCDCODCATEGORIA, argFCDNUMCUENTA, strUlrws)
            Dim resultado1 As String = ""
            Dim resultado2 As String = ""
            Dim resultadoSTServicio As String
            resultadoSTServicio = "1|(FCDO003)Función six_fastpartner retornar un Error: -10"
            If sTldDatosTran.Trim <> resultadoSTServicio.Trim Then

                Dim strTrans As String() = sTldDatosTran.Split(New Char() {"|"c})
                'sTldDatosTran.Split("|")
                If strTrans(0).ToString = "0" Then
                    If argFCDTIPOCUENTA = "IM" Then
                        resultado1 = "0|" + strTrans(16).ToString   '-- CORRIENTE
                        If strTrans(16).ToString() <> pCodUnico Then
                            resultado1 = "1|" + strTrans(16).ToString()
                        End If
                    Else
                        resultado1 = "0|" + strTrans(17).ToString   '-- AHORROS
                        If strTrans(17).ToString() <> pCodUnico Then
                            resultado1 = "1|" + strTrans(17).ToString()
                        End If
                    End If
                Else
                    resultado1 = "1|" + strTrans(1).ToString
                End If


                If argFCDNUMCUENTA2 <> "" Then
                    sTldDatosTran2 = oLWebService.fstrConsultarCuenta(argFCDTIPOCUENTA2, argFCDCODMONEDA2, argFCDCODTIENDA2, argFCDCODCATEGORIA2, argFCDNUMCUENTA2, strUlrws)

                    Dim strTrans2 As String() = sTldDatosTran2.Split(New Char() {"|"c}) ' sTldDatosTran2.Split("|")
                    If strTrans2(0).ToString = "0" Then
                        If argFCDTIPOCUENTA2 = "IM" Then
                            'If argFCDTIPOCUENTA = "IM" Then
                            resultado2 = "0|" + strTrans2(16).ToString   '-- CORRIENTE
                            If strTrans2(16).ToString() <> pCodUnico Then
                                resultado2 = "1|" + strTrans2(16).ToString()
                            End If

                        Else
                            resultado2 = "0|" + strTrans2(17).ToString   '-- AHORROS
                            If strTrans2(17).ToString() <> pCodUnico Then
                                resultado2 = "1|" + strTrans2(17).ToString()
                            End If

                        End If

                    Else
                        resultado2 = "1|" + strTrans2(1).ToString
                    End If
                Else

                End If




                If argFCDNUMCUENTA2 <> "" Then

                    If Left(resultado1, 1) = Left(resultado2, 1) Then
                        If Left(resultado1, 1) = "1" And Left(resultado2, 1) = "1" Then
                            Return "1|La primera cuenta está errada, por favor verifique"
                        Else
                            Return resultado1
                        End If
                    Else
                        If Left(resultado1, 1) = "1" Then
                            Return "1|La primera cuenta está errada, por favor verifique"
                        End If
                        If Left(resultado2, 1) = "1" Then
                            Return "1|La segunda cuenta está errada, por favor verifique"
                        End If
                        'Return "1|Cuenta Errada"
                    End If
                Else
                    If Left(resultado1, 1) = "1" Then
                        Return "1|La primera cuenta está errada, por favor verifique"
                    Else
                        Return resultado1
                    End If
                End If
            Else
                Return "1|No hay conexión con el servidor para la validación de las cuentas"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestión Bloquero
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/07/2012
    ''' </remarks>
    Protected Sub GestionBloqueo(ByVal strCodigoContrato As String)

        Try
            'Variables
            Dim objEBloqueo As New EGCC_Bloqueo
            Dim blnNuevoBloqueo As New Boolean
            blnNuevoBloqueo = False

            'Pregunta Bloqueo
            With objEBloqueo
                .TipoDocumento = GCCConstante.C_BLOQUEO_DOC_CONTRATO
                .Modulo = GCCConstante.C_BLOQUEO_MODULO_CHECKCOMERCIAL
                .NumeroDocumento = strCodigoContrato
                .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            Dim objLUtilNTX As New LUtilNTX
            Dim dtBloqueo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTX.ObtenerBloqueo(GCCUtilitario.SerializeObject(objEBloqueo)))

            'Valida Bloqueo Existente
            If Not dtBloqueo Is Nothing Then
                If dtBloqueo.Rows.Count > 0 Then

                    Dim strUsuarioBloqueo As String = dtBloqueo.Rows(0).Item("CodigoUsuario").ToString
                    If strUsuarioBloqueo.Trim().Equals(GCCUtilitario.NullableString(GCCSession.CodigoUsuario)) Then
                        Me.hddBloqueoExistente.Value = "0"
                    Else
                        Me.hddBloqueoExistente.Value = "1"
                        Me.hddBloqueoCodigo.Value = dtBloqueo.Rows(0).Item("CodigoBloqueo").ToString
                        Me.hddBloqueoCodUsuario.Value = dtBloqueo.Rows(0).Item("CodigoUsuario").ToString
                        Me.hddBloqueoNomUsuario.Value = dtBloqueo.Rows(0).Item("NombreUsuario").ToString
                        Me.hddBloqueoFecha.Value = dtBloqueo.Rows(0).Item("FechaInicio").ToString
                    End If

                Else
                    blnNuevoBloqueo = True
                End If
            Else
                blnNuevoBloqueo = True
            End If

            'Ingresa Nuevo Bloqueo
            If blnNuevoBloqueo Then
                Me.hddBloqueoExistente.Value = "0"
                With objEBloqueo
                    .TipoDocumento = GCCConstante.C_BLOQUEO_DOC_CONTRATO
                    .Modulo = GCCConstante.C_BLOQUEO_MODULO_CHECKCOMERCIAL
                    .NumeroDocumento = strCodigoContrato
                    .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    .NombreUsuario = GCCUtilitario.NullableString(GCCSession.NombreUsuario)
                    .Activo = "1"
                End With
                Dim objLUtilTX As New LUtilTX
                objLUtilTX.InsertarBloqueo(GCCUtilitario.SerializeObject(objEBloqueo))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <param name="pstrCodigoContrato">Código del Contrato</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/09/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GestionFlujo(ByVal pstrCodigoContrato As String) As String

        Try
            Dim objLUtilTX As New LUtilTX
            objLUtilTX.fblnGestionFlujo(pstrCodigoContrato, GCCConstante.C_BLOQUEO_MODULO_CHECKCOMERCIAL, GCCUtilitario.NullableString(GCCSession.CodigoUsuario))
            Return "0"
        Catch ex As Exception
            Return "1"
        End Try

    End Function

#End Region

End Class
