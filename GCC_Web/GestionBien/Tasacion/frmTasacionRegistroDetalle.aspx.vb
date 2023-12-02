

Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_Tasacion_frmTasacionRegistroDetalle
    Inherits GCCBase
    Dim objLog As New GCCLog("frmTasacionRegistroDetalle.aspx.vb")

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
                'strNroContrato = Request.QueryString("cc")
                'GCCUtilitario.CargarComboValorGenerico(Me.cmbTasador, GCCConstante.C_TABLAGENERICA_Tasador)

                hddCodContrato.value = Request.QueryString("hddCodContrato")
                hddCodtasacion.value = Request.QueryString("hddCodtasacion")
                hddCodTasador.value = Request.QueryString("hddCodTasador")
                hddSecfinanciamiento.Value = Request.QueryString("hddSecfinanciamiento")
                ' Format(CDbl(dr("Capitalfinanciamiento").ToString), "#,##0.00")
                txtValorComercial.Value = Format(CDbl(Request.QueryString("hhdValorBien").ToString), "#,##0.00")
                txtvalorrealizacion.Value = Format(CDbl(Request.QueryString("hhvalorComercial").ToString), "#,##0.00")
                txtFechaasignacion.Value = Request.QueryString("hddfechaencargo")
                txtFechatasacion.Value = Request.QueryString("hddfechatasacion")

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTasador, GCCConstante.C_TABLAGENERICA_Tasador)

                Dim dtFecha As Date = Now
                Me.hddFechaHoy.Value = dtFecha.ToString("dd/MM/yyyy")

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
    ''' <param name="pCodTasacion"></param>
    ''' <param name="pvalorejecucion"></param>
    ''' <param name="pvalorComercial"></param>
    ''' <param name="pfechaencargo"></param>
    ''' <param name="pfechatasacion"></param>
    ''' <param name="pCodTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
        Public Shared Function ActualizaTasacion(ByVal pCodSolicitudcredito As String, _
                                                 ByVal pCodTasacion As String, _
                                                 ByVal pvalorejecucion As String, _
                                                 ByVal pvalorComercial As String, _
                                                 ByVal pfechaencargo As String, _
                                                 ByVal pfechatasacion As String, _
                                                 ByVal pCodTasador As String, _
                                                 ByVal pSecfinanciamiento As Integer) As String

        Try
            Dim objObservacionDocumentoInsTx As New LTasadorTx
            Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
            Dim strESolicitudcreditoestructuratasacion As String
            With oEGccSolicitudcreditoestructuratasacion

                .Codsolicitudcredito = pCodSolicitudcredito
                .Codtasacion = pCodTasacion
                .Codtasador = pCodTasador
                .Usuarioregistro = GCCSession.CodigoUsuario
                .Valorcomercial = GCCUtilitario.StringToDecimal(pvalorComercial)
                .Valorejecucion = GCCUtilitario.StringToDecimal(pvalorejecucion)
                .VFechaencargo = GCCUtilitario.ToStringyyyyMMdd(pfechaencargo)
                .VFechatasacion = GCCUtilitario.ToStringyyyyMMdd(pfechatasacion)
                .Secfinanciamiento = pSecfinanciamiento

      


            End With

            strESolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(oEGccSolicitudcreditoestructuratasacion)
            Dim intResult As String = objObservacionDocumentoInsTx.ActualizarTasacion(strESolicitudcreditoestructuratasacion)
            If intResult = "True" Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class