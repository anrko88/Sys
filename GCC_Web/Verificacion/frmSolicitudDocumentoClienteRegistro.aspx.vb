Imports System.Web.Services
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Verificacion_frmSolicitudDocumentoClienteRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmSolicitudDocumentoClienteRegistro.aspx.vb")
    Dim mstrNroContrato As String

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not IsPostBack Then
                mstrNroContrato = Request.QueryString("cc")

                GCCUtilitario.CargarComboValorGenerico(cmbCargo, GCCConstante.C_TABLAGENERICA_CARGO)
                hidMensajeCorreo.Value = GCCConstante.C_MENSAJE_CORREO_CLIENTE
                pInicializarControles()

                'Valida Bloqueo
                GestionBloqueo(mstrNroContrato)

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
            txtNumeroContrato.Value = mstrNroContrato
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(txtNumeroContrato.Value))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtNroCotizacion.Value = dr("CODIGOCOTIZACION").ToString
                        txtClasificacionBien.Value = dr("NOMBRECLASIFICACIONBIEN").ToString
                        txtCUCliente.Value = dr("CODUNICO").ToString
                        txtTipoPersona.Value = dr("NOMBRETIPOPERSONA").ToString
                        txtTipoInmueble.Value = dr("NOMBRETIPOBIEN").ToString
                        txtProcedencia.Value = dr("NOMBREPROCEDENCIA").ToString
                        txtNombreCliente.Value = dr("NOMBRECLIENTE").ToString
                        chkTerminoRecepcion.Checked = CBool(dr("FLAGTERMINORECEPDOCUMENTOCLIE").ToString)
                        txtFechaTerminoRecepcion.Value = GCCUtilitario.CheckDateString(dr("FECHATERMINORECEPDOCUMENTOCLIE").ToString, "M")

                        dr = Nothing
                        Exit For
                    Next
                End If
                odtbDatos.Dispose()
            End If

            Dim strContacto As String = oLwsDocClienteNtx.ObtenerContacto(txtNroCotizacion.Value, txtNumeroContrato.Value)
            If Not String.IsNullOrEmpty(strContacto) Then
                Dim oEContacto As EGcc_contacto = GCCUtilitario.DeserializeObject(Of EGcc_contacto)(strContacto)
                txtContacto.Value = oEContacto.Nombre.Trim()
                txtCorreo.Value = oEContacto.Correo.Trim()
                txtTelefonos.Value = oEContacto.Telefono.Trim()
                txtAnexo.Value = oEContacto.Anexo.Trim()
                hidCodContacto.Value = oEContacto.Codigocontacto
                GCCUtilitario.SeleccionaCombo(cmbCargo, oEContacto.CodigoCargo.ToString)

            End If
            GCCUtilitario.CargarComboValorGenerico(cmbCondicionesAdicionales, GCCConstante.C_TABLAGENERICA_CONDICIONES_ADICIONALES)



        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteNtx = Nothing
        End Try
    End Sub

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
            Dim blnNuevoBloqueo As Boolean
            blnNuevoBloqueo = False

            'Pregunta Bloqueo
            With objEBloqueo
                .TipoDocumento = GCCConstante.C_BLOQUEO_DOC_CONTRATO
                .Modulo = GCCConstante.C_BLOQUEO_MODULO_SOLCLIENTE
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
                    .Modulo = GCCConstante.C_BLOQUEO_MODULO_SOLCLIENTE
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
    ''' ListaDocumentosCondiciones
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaDocumentosCondiciones(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pCodContrato As String) As JQGridJsonResponse


        Dim oLwsCheckListNTx As New LCheckListNTx
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim odtbDocumento As New DataTable
        Try
            With oEGccContratodocumento
                .Numerocontrato = GCCUtilitario.NullableString(pCodContrato)
                .Flagfiltro = 0
                .Flagcartaenvio = 0
            End With
            odtbDocumento = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsCheckListNTx.ContratoDocumentoSel(pPageSize, _
                                                                     pCurrentPage, _
                                                                     pSortColumn, _
                                                                     pSortOrder, _
                                                                     GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEGccContratodocumento) _
                                                                     ))

            ' Total de registros a mostrar.

            Dim intTotalCurrent As Int32

            Dim totalRecords As Integer
            If odtbDocumento.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(odtbDocumento.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(odtbDocumento.Rows(0)("TOTAL_PAGINA"))
            End If

            If pCurrentPage > intTotalCurrent Then
                pCurrentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim jqGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = jqGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return jqGridJsonResponse.JQGridJsonResponseDataTable(totalPages, pCurrentPage, totalRecords, odtbDocumento)

        Catch ex As Exception
            Throw ex
        Finally
            odtbDocumento.Dispose()
            oLwsCheckListNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <param name="strFechaTerminoRecepCli"></param>
    ''' <param name="strFlagFechaTerminoRecepCli"></param>
    ''' <param name="strCodigoContacto"></param>
    ''' <param name="strCodigocotizacion"></param>
    ''' <param name="strNumeroContrato"></param>
    ''' <param name="strNombre"></param>
    ''' <param name="strCorreo"></param>
    ''' <param name="strTelefono"></param>
    ''' <param name="strAnexo"></param>
    ''' <param name="strCargo"></param>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Sub Guardar(ByVal strFechaTerminoRecepCli As String, _
                                 ByVal strFlagFechaTerminoRecepCli As String, _
                                 ByVal strCodigoContacto As String, _
                                 ByVal strCodigocotizacion As String, _
                                 ByVal strNumeroContrato As String, _
                                 ByVal strNombre As String, _
                                 ByVal strCorreo As String, _
                                 ByVal strTelefono As String, _
                                 ByVal strAnexo As String, _
                                 ByVal strCargo As String)
        Dim oEContrato As New ESolicitudcredito
        Dim oEContacto As New EGcc_contacto
        Dim oLwsDocClienteTx As New LDocClienteTx
        Try
            Dim strEContrato As String = ""
            If strFlagFechaTerminoRecepCli = "1" Then
                With oEContrato
                    .FechaTerminoRecepDocumentoClie = GCCUtilitario.StringToDateTime(strFechaTerminoRecepCli)
                    .FlagTerminoRecepDocumentoClie = strFlagFechaTerminoRecepCli
                End With
                strEContrato = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(oEContrato)
            End If

            With oEContacto
                .Codigocontacto = GCCUtilitario.StringToInteger(strCodigoContacto)
                .Codigocotizacion = strCodigocotizacion
                .Codsolicitudcredito = strNumeroContrato
                .Nombre = GCCUtilitario.NullableString(strNombre)
                .Correo = GCCUtilitario.NullableString(strCorreo)
                .Telefono = GCCUtilitario.NullableString(strTelefono)
                .Anexo = GCCUtilitario.NullableString(strAnexo)
                .CodigoCargo = GCCUtilitario.NullableString(strCargo)
            End With

            Dim blnResult As Boolean = oLwsDocClienteTx.GuardarVerificacionCliente( _
                                                strEContrato, _
                                                GCCUtilitario.SerializeObject(Of EGcc_contacto)(oEContacto), _
                                                IIf(String.IsNullOrEmpty(strCodigoContacto), "N", "M"))
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteTx = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' AgregarDocCond
    ''' </summary>
    ''' <param name="pstrCodContrato"></param>
    ''' <param name="pstrDocumento"></param>
    ''' <param name="pstrCondicion"></param>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function AgregarDocCond(ByVal pstrCodContrato As String, _
                                  ByVal pstrDocumento As String, _
                                  ByVal pstrCondicion As String) As String
        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim oLwsDocClienteTx As New LDocClienteTx
        Try
            With oEContratoDoc
                .Numerocontrato = pstrCodContrato
                .Nombrearchivo = GCCUtilitario.NullableString(pstrDocumento.ToUpper)
                .Codigoorigencondicion = GCCConstante.C_ORIGENDOC_DOC_CLIENTE
                .Codigotipocondicion = GCCUtilitario.NullableStringCombo(pstrCondicion)
                .Audusuarioregistro = GCCSession.CodigoUsuario
            End With

            Dim intCodigoDocumento As Integer = oLwsDocClienteTx.AgregarDocCondCliente(GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEContratoDoc))
            Return GCCUtilitario.CheckStr(intCodigoDocumento)

        Catch ex As Exception
            Throw ex

        Finally

            oLwsDocClienteTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' EliminarDocCond
    ''' </summary>
    ''' <param name="strCodContratoDoc"></param>
    ''' <param name="pstrCodContrato"></param>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Sub EliminarDocCond(ByVal strCodContratoDoc As String, _
                                      ByVal pstrCodContrato As String)
        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim oLwsDocClienteTx As New LDocClienteTx
        Try
            With oEContratoDoc
                .Codigocontratodocumento = strCodContratoDoc
                .Numerocontrato = pstrCodContrato
                .Audusuarioregistro = GCCSession.CodigoUsuario
            End With

            Dim blnResult As Boolean = oLwsDocClienteTx.EliminarDocCondCliente(GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEContratoDoc))
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteTx = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' EnviaCarta
    ''' </summary>
    ''' <param name="pstrCodContrato"></param>
    ''' <param name="pstrRegistros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function EnviaCarta(ByVal pstrCodContrato As String, _
                                  ByVal pstrRegistros As String) As String
        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim oLwsDocClienteTx As New LDocClienteTx
        Try
            With oEContratoDoc
                .Observaciones = pstrRegistros
                .Numerocontrato = pstrCodContrato.Trim.PadLeft(8, "0"c)
                .Audusuarioregistro = GCCSession.CodigoUsuario
            End With

            Dim blnResult As Boolean = oLwsDocClienteTx.EnviarCartaDocumentoCliente(GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEContratoDoc))
            Return "0|"
        Catch ex As Exception
            Throw ex
            Return GCCUtilitario.Concatenar("1|", ex.Message)
        Finally
            oLwsDocClienteTx = Nothing
        End Try

    End Function

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
