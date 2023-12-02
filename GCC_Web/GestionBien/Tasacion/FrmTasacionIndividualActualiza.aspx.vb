Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_Tasacion_FrmTasacionIndividualActualiza
    Inherits GCCBase
    Dim objLog As New GCCLog("FrmTasacionIndividualActualiza.aspx.vb")

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

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTasador, GCCConstante.C_TABLAGENERICA_Tasador)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbMotivonoTasacion, GCCConstante.C_TABLAGENERICA_MotivoNoTasacion)

                hddCodSolicitudcredito.Value = Request.QueryString("spCodSolicitudcredito")
                hddSecfinanciamiento.value = Request.QueryString("spSecfinanciamiento")
                hddCodTasador.value = Request.QueryString("spCodTasador")
                hddCodTasacion.Value = Request.QueryString("spCodTasacion")


                txtFechaProxTasacion.Value = Request.QueryString("spFProxtasacion")
                cmbMotivonoTasacion.Value = Request.QueryString("spcodMotivonoTasacion")

                hidGrid.Value = Request.QueryString("spGrid")
                hidID.Value = Request.QueryString("spID")
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
    ''' AsignaTasadorIndividual
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pSecFinanciamiento"></param>
    ''' <param name="pCodtasacion"></param>
    ''' <param name="pCodTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
        Public Shared Function AsignaTasadorIndividual(ByVal pCodSolicitudcredito As String, _
                                                       ByVal pSecFinanciamiento As String, _
                                                       ByVal pCodtasacion As String, _
                                                       ByVal pCodTasador As String, _
                                                       ByVal pFechaProxTasacion As String, _
                                                       ByVal pMotivoNoTasacion As String) As String

        Try
            Dim objAsignaTasadorTx As New LTasadorTx
            Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
            Dim strESolicitudcreditoestructuratasacion As String
            With oEGccSolicitudcreditoestructuratasacion
                .Codsolicitudcredito = pCodSolicitudcredito
                .Secfinanciamiento = GCCUtilitario.CheckInt(pSecFinanciamiento)
                .Codtasacion = pCodtasacion
                .Codtasador = pCodTasador
                .Usuarioregistro = GCCSession.CodigoUsuario
                .CodEstadoTasacion = "3"
                .VfechaProxTasacion = GCCUtilitario.ToStringyyyyMMdd(pFechaProxTasacion)
                'GCCUtilitario.ToStringyyyyMMdd(
                .Codmotivonotasacion = pMotivoNoTasacion
            End With

            strESolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(oEGccSolicitudcreditoestructuratasacion)
            Dim intResult As String = objAsignaTasadorTx.ActualizaTasador(strESolicitudcreditoestructuratasacion)
            If Convert.ToBoolean(intResult) Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class