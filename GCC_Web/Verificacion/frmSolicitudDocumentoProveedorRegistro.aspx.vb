Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Verificacion_frmSolicitudDocumentoProveedorRegistro

    Inherits GCCBase

    Dim objLog As New GCCLog("frmSolicitudDocumentoProveedorRegistro.aspx.vb")

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
            pInicializarControles()

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                hidMensajeCorreo.Value = GCCConstante.C_MENSAJE_CORREO_FACTURA_PROVEEDOR
                'GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                'TBL046()
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoProveedor, GCCConstante.C_TABLAGENERICA_NACIONALIDAD)
                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)

                'Valida Bloqueo
                Dim strNroContrato As String = Request.QueryString("cc")
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

    Private Sub ObtenerTipoCambio(ByVal strMoneda As String, ByVal strTipoModalidaCambio As String, ByVal strFecha As String)
        Dim oLwsTipoCambioNtx As New LUtilNTX
        Dim FechaSolcitud As DateTime
        Try
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTipoCambioNtx.ObtenerTipoCambio(strMoneda, strTipoModalidaCambio, strFecha))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        'txtNroCotizacion.Value = dr("MontoValorVenta").ToString
                        hidTipoCompra.Value = dr("MontoValorCompra").ToString
                        hidTipoVenta.Value = dr("MontoValorVenta").ToString
                        hddModalidadTipoCambio.Value = dr("TipoModalidadCambio").ToString
                        FechaSolcitud = GCCUtilitario.StringToDateTime(hddFechaSolicitudCredito.Value)
                        hddFechaSolicitudCredito.Value = FechaSolcitud.ToString("dd/MM/yyyy")
                        dr = Nothing
                        Exit For
                    Next

                End If
                odtbDatos.Dispose()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLwsTipoCambioNtx = Nothing
        End Try
    End Sub

    Private Sub pInicializarControles()
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Try
            hidNumeroContrato.Value = Request.QueryString("cc")
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(hidNumeroContrato.Value))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtNroCotizacion.Value = dr("CODIGOCOTIZACION").ToString
                        txtClasificacionBien.Value = dr("NOMBRECLASIFICACIONBIEN").ToString
                        txtNumContrato.Value = dr("CODSOLICITUDCREDITO").ToString
                        txtCUCliente.Value = dr("CODUNICO").ToString
                        txtTipoPersona.Value = dr("NOMBRETIPOPERSONA").ToString
                        txtTipoInmueble.Value = dr("NOMBRETIPOBIEN").ToString
                        txtProcedencia.Value = dr("NOMBREPROCEDENCIA").ToString
                        txtRazonSocial.Value = dr("NOMBRECLIENTE").ToString
                        'txtValorVenta.Value = dr("VALORVENTA").ToString
                        'txtIgv.Value = dr("VALORVENTAIGV").ToString
                        'txtPrecioVenta.Value = dr("PRECIOVENTA").ToString
                        hidMonedaContrato.Value = dr("CODIGOMONEDA").ToString
                        txtMonedaContrato.Value = dr("NOMBREMONEDA").ToString

                        txtValorVenta.Value = Format(CDbl(dr.Item("VALORVENTA")), GCCConstante.C_FormatMiles)
                        txtIgv.Value = Format(CDbl(dr.Item("VALORVENTAIGV")), GCCConstante.C_FormatMiles)
                        txtPrecioVenta.Value = Format(CDbl(dr.Item("PRECIOVENTA")), GCCConstante.C_FormatMiles)

                        'chkTerminoRecepcion.Checked = GCCUtilitario.CheckBoolean(dr("FLAGTERMINORECEPDOCUMENTOPROV").ToString)

                        Dim dtmFechaTermino As String = GCCUtilitario.CheckDateString(dr("FECHATERMINORECEPDOCUMENTOPROV").ToString, "C")
                        If Not dtmFechaTermino Is Nothing Then
                            If Not dtmFechaTermino.Trim().Equals("") Then
                                txtFechaTerminoRecepcion.Value = dtmFechaTermino
                            End If
                        End If

                        txadescripcionbien.Value = dr("DESCRIPCIONBIEN").ToString
                        '----
                        ' POR CONFIRMAR DE DONDE OBTIENE LA FECHA DEL TIPO DE CAMBIO
                        '----
                        Dim dtmFechaSolicitudCredito As String = GCCUtilitario.ToStringyyyyMMdd(dr("fechasolicitudcredito").ToString, "")
                        'Dim dtmFechaSolicitudCredito As String = GCCUtilitario.ToStringyyyyMMdd(dr("FECHAHOY").ToString, "")
                        If Not dtmFechaSolicitudCredito Is Nothing Then
                            If Not dtmFechaSolicitudCredito.Trim().Equals("") Then
                                hddFechaSolicitudCredito.Value = dtmFechaSolicitudCredito
                            End If
                        End If

                        'ObtenerTipoCambio(dr("CODIGOMONEDA").ToString, "PRF", hddFechaSolicitudCredito.Value)
                        ObtenerTipoCambio(GCCConstante.C_COD_MONEDA_DOLARES, GCCConstante.C_vMODALIDAD_PRF, hddFechaSolicitudCredito.Value)

                        dr = Nothing
                        Exit For
                    Next
                End If
                odtbDatos.Dispose()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteNtx = Nothing
        End Try
    End Sub

    <WebMethod()> _
Public Shared Function EnviaCarta(ByVal pstrCodContrato As String, _
                             ByVal pstrRegistros As String) As String

        Dim pEcontratoproveedor As String
        Dim oEContratoProv As New EGcc_contratoproveedor
        Dim oLwsDocProveedorTx As New LProveedorTx
        Try


            Dim strRegistrosSplit As String() = pstrRegistros.Split(New Char() {"|"c})

            Dim objContratoProveedorList As New ListEContratoProveedor

            If strRegistrosSplit.Length > 0 Then

                Dim objContratoProveedorNTx As New LProveedorNTx
                Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoProveedorNTx.ListadoContratoProveedor(pstrCodContrato))
                Dim dwProveedor As New DataView(dtProveedor)
                Dim oGCCAnexo As New GCC_Anexo

                Dim sbProveedor As New StringBuilder
                For i As Integer = LBound(strRegistrosSplit) To UBound(strRegistrosSplit)
                    oEContratoProv = New EGcc_contratoproveedor

                    oEContratoProv.Codigocontratoproveedor = GCCUtilitario.CheckInt(strRegistrosSplit(i))
                    oEContratoProv.Numerocontrato = pstrCodContrato

                    If oEContratoProv.Codigocontratoproveedor.Value > 0 Then
                        objContratoProveedorList.Add(oEContratoProv)

                        dwProveedor.RowFilter = GCCUtilitario.Concatenar("CodigoContratoProveedor=", oEContratoProv.Codigocontratoproveedor.Value.ToString())
                        If dwProveedor.Count = 1 Then
                            With dwProveedor
                                sbProveedor.Append(GCCUtilitario.Concatenar(oGCCAnexo.CartaProveedor(dwProveedor, pstrCodContrato), "*", dwProveedor(0).Item("Correo").ToString(), ";"))
                            End With
                        End If

                    End If

                Next

                pEcontratoproveedor = GCCUtilitario.SerializeObject(objContratoProveedorList)
                Dim blnResult As Boolean = oLwsDocProveedorTx.EnviarCartaDocumentoProveedor(pEcontratoproveedor)
                If blnResult Then
                    Return GCCUtilitario.Concatenar("0|", sbProveedor.ToString())
                Else
                    Return "1|Se produjo un error al generar la carta"
                End If
            End If

        Catch ex As Exception
            Throw ex
            Return "1|" & ex.ToString
        Finally
            oLwsDocProveedorTx = Nothing
        End Try

        Return "1|Error al Enviar Carta"

    End Function

    <WebMethod()> _
   Public Shared Function GuardarSolicitud(ByVal pstrNumeroContrato As String, _
                                      ByVal pstrFechaTerminoRecepDocProv As String, _
                                      ByVal pstrFlagFechaTerminoRecepDocProv As String, _
                                      ByVal pstrDescripcionBien As String) As String
        Dim oESolicitudCredito As New ESolicitudcredito
        Dim objLSolicitudCredito As New LProveedorTx
        Try

            With oESolicitudCredito
                .Codsolicitudcredito = pstrNumeroContrato
                If pstrFlagFechaTerminoRecepDocProv = "1" Then
                    .FlagTerminoRecepDocumentoProv = 1
                    .FechaTerminoRecepDocumentoProv = GCCUtilitario.StringToDateTime(pstrFechaTerminoRecepDocProv)
                Else
                    .FlagTerminoRecepDocumentoProv = 0
                    .FechaTerminoRecepDocumentoProv = GCCUtilitario.StringToDateTime("1/1/1900")
                End If
                .DescripcionBien = pstrDescripcionBien

            End With

            Dim blnResult As Boolean = objLSolicitudCredito.ModificaSolicitudDocumentoProveedor(GCCUtilitario.SerializeObject(oESolicitudCredito))

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        Finally

        End Try
    End Function

    <WebMethod()> _
    Public Shared Function ListarContratoProveedor(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pNumeroContrato As String) As JQGridJsonResponse
        Dim objContratoProveedorNTx As New LProveedorNTx

        Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoProveedorNTx.ListadoContratoProveedor(pNumeroContrato))

        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 20, dtProveedor)
        '' Número de página actual.
        'Dim currentPage As Integer = pCurrentPage
        '' Total de registros a mostrar.
        'Dim totalRecords As Integer
        'If dtProveedor.Rows.Count = 0 Then
        '    totalRecords = 0
        'Else
        '    totalRecords = Convert.ToInt32(dtProveedor.Rows(0)("RecordCount"))
        '    'totalRecords = Convert.ToInt32(dtProveedor.Rows(0)("total2"))
        'End If

        '' Número total de páginas
        'Dim JQGridJsonResponse As New JQGridJsonResponse
        'Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        'Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtProveedor)

    End Function

    <WebMethod()> _
    Public Shared Function GuardarEditar(ByVal pCodigoContratoProveedor As String, _
                                         ByVal pCodProveedor As String, _
                                         ByVal pCodigoContacto As String, _
                                         ByVal pCodigoTipoProveedor As String, _
                                         ByVal pNumeroContrato As String, _
                                         ByVal pCodigoMoneda As String, _
                                         ByVal pImporte As String, _
                                         ByVal pTipoCambio As String, _
                                         ByVal pMontoTipoCambio As String, _
                                         ByVal pTotalImporte As String, _
                                         ByVal pNombreContacto As String, _
                                         ByVal pDescripcionBien As String, _
                                         ByVal pCorreo As String) As String
        Try
            Dim objEContratoProveedor As New EGcc_contratoproveedor
            Dim objLContratoProveedor As New LProveedorTx
            Dim pEContratoProveedor As String

            With objEContratoProveedor
                .Codigocontratoproveedor = CInt(pCodigoContratoProveedor)
                .Codproveedor = pCodProveedor
                .Codigocontacto = CInt(pCodigoContacto)
                .CodigoTipoProveedor = pCodigoTipoProveedor
                .Numerocontrato = pNumeroContrato
                .Codigomoneda = pCodigoMoneda
                .Importe = GCCUtilitario.StringToDecimal(pImporte)
                .Tipocambio = pTipoCambio
                .MontoTipoCambio = GCCUtilitario.StringToDecimal(pMontoTipoCambio)
                .NombreContacto = pNombreContacto
                .Correo = pCorreo
                .TotalImporte = GCCUtilitario.StringToDecimal(pTotalImporte)
                .DescripcionBien = pDescripcionBien
                .Audusuariomodificacion = GCCSession.CodigoUsuario
            End With
            pEContratoProveedor = GCCUtilitario.SerializeObject(objEContratoProveedor)

            Dim blnResult As Boolean = objLContratoProveedor.fblnModificarContratoProveedor(pEContratoProveedor)

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
    Public Shared Function GuardarNuevo(ByVal pCodigoContratoProveedor As String, _
                                         ByVal pCodProveedor As String, _
                                         ByVal pCodigoContacto As String, _
                                         ByVal pCodigoTipoProveedor As String, _
                                         ByVal pNumeroContrato As String, _
                                         ByVal pCodigoMoneda As String, _
                                         ByVal pImporte As String, _
                                         ByVal pTipoCambio As String, _
                                         ByVal pMontoTipoCambio As String, _
                                         ByVal pTotalImporte As String, _
                                         ByVal pNombreContacto As String, _
                                         ByVal pDescripcionBien As String, _
                                         ByVal pCorreo As String) As String
        Try
            Dim objEContratoProveedor As New EGcc_contratoproveedor
            Dim objLContratoProveedor As New LProveedorTx
            Dim pEContratoProveedor As String

            With objEContratoProveedor
                .Codigocontratoproveedor = GCCUtilitario.StringToInteger(pCodigoContratoProveedor)
                .Codproveedor = pCodProveedor
                .Codigocontacto = GCCUtilitario.StringToInteger(pCodigoContacto)
                .CodigoTipoProveedor = pCodigoTipoProveedor
                .Numerocontrato = pNumeroContrato
                .Codigomoneda = pCodigoMoneda

                .Importe = GCCUtilitario.StringToDecimal(pImporte)
                .Tipocambio = pTipoCambio
                .MontoTipoCambio = GCCUtilitario.StringToDecimal(pMontoTipoCambio)
                .NombreContacto = pNombreContacto
                .Correo = pCorreo
                .TotalImporte = GCCUtilitario.StringToDecimal(pTotalImporte)
                .Audusuarioregistro = GCCSession.CodigoUsuario
                .DescripcionBien = pDescripcionBien
            End With
            pEContratoProveedor = GCCUtilitario.SerializeObject(objEContratoProveedor)

            Dim intResult As Integer = objLContratoProveedor.fintInsertarContratoProveedor(pEContratoProveedor)

            If intResult = 0 Then
                Return "0"
            Else
                Return intResult.ToString()
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function EliminarRegistro(ByVal pCodigoContratoProveedor As String) As String
        Try
            Dim objEContratoProveedor As New EGcc_contratoproveedor
            Dim objLContratoProveedor As New LProveedorTx
            Dim pEContratoProveedor As String

            With objEContratoProveedor
                .Codigocontratoproveedor = CInt(pCodigoContratoProveedor)
                .Audusuariomodificacion = GCCSession.CodigoUsuario
            End With
            pEContratoProveedor = GCCUtilitario.SerializeObject(objEContratoProveedor)

            Dim intResult As Integer = objLContratoProveedor.fblnEliminarContratoProveedor(pEContratoProveedor)

            If intResult = 0 Then
                Return "0"
            Else
                Return intResult.ToString()
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
                .Modulo = GCCConstante.C_BLOQUEO_MODULO_SOLPROVEEDOR
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
                    .Modulo = GCCConstante.C_BLOQUEO_MODULO_SOLPROVEEDOR
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

#End Region

#Region "WebMetods"

    ''' <summary>
    ''' Actualiza Bloqueo
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/07/2012
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

#End Region

End Class
