Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Formalizacion_CesionContrato_frmCesionContratoRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCesionContratoRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 04/01/2013
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                'Verifica Edicion
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")

                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato

                'Obtiene datos del Contrato
                ListaCesionContrato(strCodContrato)

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

#Region "WebMethods"

    ''' <summary>
    ''' Realiza Cesión
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RealizaCesion(ByVal hddCodContrato As String, _
                                             ByVal hddCodCesionario As String) As String

        ''Variables
        Dim objCesionContratoTx As New LCesionContratoTx
        Dim objCesionarioNTx As New LCesionarioNTx
        Dim strMensajeError As String = "Petición denegada."

        Try

            

            'Consulta Datos del Cesionario
            Dim strCesionario As String = ""
            Dim oCesionario As New EGCC_Cesionario
            oCesionario.Codsolcredito = GCCUtilitario.NullableString(hddCodContrato)
            oCesionario.CodCesionario = GCCUtilitario.NullableString(hddCodCesionario)
            strCesionario = GCCUtilitario.SerializeObject(Of EGCC_Cesionario)(oCesionario)

            Dim strOCesionario As String = objCesionarioNTx.ObtenerCesionario(strCesionario)
            Dim dtbCesionario As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(strOCesionario)

            'Valida si existe            
            Dim strCodTipoDocumento As String = ""
            Dim strNroDocumento As String = ""
            If dtbCesionario.Rows.Count > 0 Then
                strCodTipoDocumento = dtbCesionario.Rows(0).Item("CodigoTipoDocumento").ToString
                strNroDocumento = dtbCesionario.Rows(0).Item("NroDocumento").ToString
            Else
                strMensajeError = "Cesionario no encontrado."
            End If


            'Consulta RM
            Dim intNumConsulta As Integer = 2 '2=Busqueda por Tipo Documento - 1=Por CU
            Dim strCodUnico As String = ""
            Dim strTipoDoc As String = strCodTipoDocumento.Trim()
            Dim strNroRuc As String = strNroDocumento.Trim()
            Dim strMensaje As String = ""
            Dim oEClienteRM As EClienteRM = GCCUtilitario.fObtenerDatosRMCliente(intNumConsulta, strCodUnico, strTipoDoc, strNroRuc, strMensaje)
            strMensajeError = strMensaje

            Dim blnResultado As Boolean = False
            If Not oEClienteRM Is Nothing Then

                'Inicializa Objeto
                Dim oECesionContrato As New EGCC_CesionContrato
                Dim strECesionContrato As String
                With oECesionContrato
                    .NroContrato = GCCUtilitario.NullableString(hddCodContrato)
                    .CodCesionario = GCCUtilitario.CheckInt(hddCodCesionario)
                    .UsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                    .CodSubprestatario = ""
                    .CodUnico = oEClienteRM.Codigounico
                    .RazonSocial = oEClienteRM.Razonsocialcliente
                    .TipoDocumento = oEClienteRM.Codigotipodocumento
                    .NroDocumento = oEClienteRM.Numerodocumento

                End With
                strECesionContrato = GCCUtilitario.SerializeObject(Of EGCC_CesionContrato)(oECesionContrato)

                blnResultado = objCesionContratoTx.RealizarCesionContrato(strECesionContrato)
                'blnResultado = True
            End If

            'Valida Resultado
            If blnResultado Then
                Return "1|Grabado Correcto"
            Else
                Return "0|No se puede asignar al Cesionario. " + strMensajeError
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

#End Region

#Region "Methods"

    ''' <summary>
    ''' GetCesionContrato
    ''' </summary>
    ''' <returns>Listado de CesionContrato</returns>
    ''' <remarks></remarks>
    Public Sub ListaCesionContrato(ByVal pstrNroContrato As String)

        'Variables
        Dim objCesionContratoNTx As New LCesionContratoNTx

        Try

            'Inicializa Objeto
            Dim objECesionContrato As New EGCC_CesionContrato
            Dim strECesionContrato As String
            With objECesionContrato
                .NroContrato = GCCUtilitario.NullableString(pstrNroContrato)
            End With
            strECesionContrato = GCCUtilitario.SerializeObject(Of EGCC_CesionContrato)(objECesionContrato)

            'Ejecuta Consulta
            Dim dtCesionContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCesionContratoNTx.GetCesionContrato(strECesionContrato))

            'Valida si existe
            If dtCesionContrato.Rows.Count > 0 Then

                Me.txtNroContrato.Value = dtCesionContrato.Rows(0).Item("CodSolicitudCredito").ToString
                Me.txtCUCliente.Value = dtCesionContrato.Rows(0).Item("CodUnico").ToString
                Me.txtRazonSocial.Value = dtCesionContrato.Rows(0).Item("ClienteRazonSocial").ToString
                Me.txtTipoDoc.Value = dtCesionContrato.Rows(0).Item("DesTipoDocumento").ToString
                Me.txtNroDocumento.Value = dtCesionContrato.Rows(0).Item("NumeroDocumento").ToString
                Me.txtClasificacionBien.Value = dtCesionContrato.Rows(0).Item("ClasificacionBien").ToString
                Me.txtEstadoContrato.Value = dtCesionContrato.Rows(0).Item("DesEstadoContrato").ToString

                Me.hddCodEstadoContrato.Value = dtCesionContrato.Rows(0).Item("CodigoEstadoContrato").ToString
                Me.hddCodCesionarioPri.Value = dtCesionContrato.Rows(0).Item("CodCesionario").ToString

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
