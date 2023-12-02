
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_Tasacion_frmTasacionAsignacion
    Inherits GCCBase
    Dim objLog As New GCCLog("frmTasacionAsignacion.aspx.vb")

    Dim strNroContrato As String

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - IJM
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
                hddListaContratos.Value = Request.QueryString("sListaContratos")
                txtEjecutivoBanca.Value = Request.QueryString("eb")
                txtFechadesde.Value = Request.QueryString("fd")
                txtFechahasta.Value = Request.QueryString("fh")
                txtPeriodo.Value = Request.QueryString("Per")
                'GCCUtilitario.CargarComboValorGenerico(Me.cmbTasador, "TBL135") 'GCCConstante.C_TABLAGENERICA_Tasador)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTasador, GCCConstante.C_TABLAGENERICA_Tasador)
                cmbTasador.Value = Request.QueryString("stasador")


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



#Region "Web Metodos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCodTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
        Public Shared Function AsignaTasador(ByVal pCodSolicitudcredito As String, _
                                            ByVal pCodTasador As String) As String

        Try
            Dim objObservacionDocumentoInsTx As New LTasadorTx
            Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
            Dim strESolicitudcreditoestructuratasacion As String
            With oEGccSolicitudcreditoestructuratasacion

                .Codsolicitudcredito = pCodSolicitudcredito
                .Codtasador = pCodTasador
                .Usuarioregistro = GCCSession.CodigoUsuario
                .CodEstadoTasacion = "4"
                '.Codigocontratodocumento = GCCUtilitario.CheckInt(strCodigoContratoDocumento)
                '.Numerocontrato = GCCUtilitario.NullableString(strnumeroContrato)
            End With


            strESolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(oEGccSolicitudcreditoestructuratasacion)
            Dim intResult As String = objObservacionDocumentoInsTx.InsertarTasador(strESolicitudcreditoestructuratasacion)

            If intResult = "True" Then
                'Enviar Alerta
                Dim blnRpta As Boolean = psEnviarAlerta(oEGccSolicitudcreditoestructuratasacion)
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function psEnviarAlerta(ByVal obj As ESolicitudcreditoestructuratasacion) As Boolean
        Dim objEGCC_Alertas As New EGCC_Alertas
        Dim objLAlertaImpVehicularNTx As New LImpuestoVehicularNTX
        Dim strNroLote As String = obj.Codsolicitudcredito.ToString()
        Dim blnRpta As Boolean = False
        Try
            Dim dtAlertaImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLAlertaImpVehicularNTx.ListarAltertaImpuestoVehicular(strNroLote, "AsignarTasador"))

            Dim objGCCBase As GCCBase = Nothing
            Dim bolResultado As Boolean = False

            For Each dr As DataRow In dtAlertaImpuestoVehicular.Rows
                With objEGCC_Alertas
                    .AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()
                    .CodTasador = dr.Item("CodTasador").ToString()
                    .FechaActual = Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString()
                    .RazonSocial = dr.Item("ClienteRazonSocial").ToString()
                    .Direccion = dr.Item("ClienteDomicilioLegal").ToString()
                    .Distrito = dr.Item("Distrito").ToString()
                    .NumContrato = dr.Item("CodSolicitudCredito").ToString()
                    .FechaActivacion = dr.Item("fecharegistro").ToString()
                    '------ Por Confirmar ---------
                    .EmpresaTasador = dr.Item("EmpresaTasador").ToString()
                    .Tasador = dr.Item("Tasador").ToString()
                    .Telefono = dr.Item("Telefono").ToString()
                    .Celular = dr.Item("CelularTasador").ToString()
                    .Correo = dr.Item("CorreoTasador").ToString()
                    .RutaWeb = HttpContext.Current.Server.MapPath("../..").ToString()
                End With
                objGCCBase = New GCCBase()
                bolResultado = objGCCBase.EnviarMailAlertas("MailAsignacionTasador", objEGCC_Alertas)
            Next
            blnRpta = True
        Catch ex As Exception

        End Try
        Return blnRpta
    End Function

#End Region

End Class