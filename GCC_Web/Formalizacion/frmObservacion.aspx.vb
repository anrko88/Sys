
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports GCC.Entity.EGcc_contratodocumento

Partial Class Formalizacion_frmObservacion
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmObservacion.aspx.vb")

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

                If Request.QueryString("Titulo").Trim <> "" Then
                    hdnTitulo.Value = Request.QueryString("Titulo").Trim
                End If

                If Request.QueryString("sflagtipoobs").Trim <> "" Then
                    hflagtipoObs.Value = Request.QueryString("sflagtipoobs").Trim
                End If

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoObservacion, GCCConstante.C_TABLAGENERICA_TIPO_OBSERVACION_CHECKLIST)

                hddCodConDoc.Value = Request.QueryString("hddCodConDoc").ToString.Trim()
                hddCodContrato.Value = Request.QueryString("hddCodContrato").ToString.Trim()
                pInicializarControles(CInt(hddCodConDoc.Value), hddCodContrato.Value, 2)

                hddusuariosesion.value = GCCSession.CodigoUsuario

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

    Private Sub pInicializarControles(ByVal intCodigocontratodocumento As Integer, ByVal strNumerocontrato As String, ByVal sFlagOrigenObservacion As Integer)

        Try
            Dim objDocumentoObservacion As New EGcc_documentoobservacion
            Dim objDocumentoObservacionNTx As New LContratoNTx
            Dim pEDocumentoObservacion As String


            With objDocumentoObservacion
                .Codigocontratodocumento = GCCUtilitario.CheckInt(intCodigocontratodocumento)
                .Numerocontrato = GCCUtilitario.CheckStr(strNumerocontrato)
                .FlagOrigenObservacion = GCCUtilitario.CheckInt(sFlagOrigenObservacion)
             
            End With
            Dim dtObservacion As New DataTable
            pEDocumentoObservacion = GCCUtilitario.SerializeObject(objDocumentoObservacion)
            '  Dim odtbDatos As EGcc_documentoobservacion = GCCUtilitario.DeserializeObject(Of EGcc_documentoobservacion)(objDocumentoObservacionNTx.RetornarDocumentoObservacion(pEDocumentoObservacion))

            dtObservacion = GCCUtilitario.DeserializeObject(Of DataTable)(objDocumentoObservacionNTx.RetornarDocumentoObservacion(pEDocumentoObservacion))
            If dtObservacion.Rows.Count > 0 Then
                TxaObs.Value = dtObservacion.Rows(0).Item("Observacion").ToString()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "cmbTipoObservacion", "fn_util_SeteaComboServidor('cmbTipoObservacion','" + RTrim(dtObservacion.Rows(0).Item("Codigotipoobservacion").ToString()) + "');", True)

            End If

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

#End Region

#Region "Web Metodos"
    ''' <summary>
    ''' InsertaObservacionDocumento
    ''' </summary>
    ''' <param name="strCodigoContratoDocumento"></param>
    ''' <param name="strnumeroContrato"></param>
    ''' <param name="strObservacion"></param>
    ''' <param name="CodigoTipoObservacion"></param>
    ''' <param name="audUsuarioRegistro"></param>
    ''' <param name="intFlagOrigenObservacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function InsertaObservacionDocumento(ByVal strCodigoContratoDocumento As String, _
                                                       ByVal strnumeroContrato As String, _
                                                       ByVal strObservacion As String, _
                                                       ByVal CodigoTipoObservacion As String, _
                                                       ByVal audUsuarioRegistro As String, _
                                                       ByVal intFlagOrigenObservacion As Integer) As String

        Try
            Dim objObservacionDocumentoInsTx As New LContratoTx
            Dim oEGccObservacionDocuento As New EGcc_documentoobservacion
            Dim strEGccontratoDocObservacion As String
            With oEGccObservacionDocuento
                .Codigocontratodocumento = GCCUtilitario.CheckInt(strCodigoContratoDocumento)
                .Numerocontrato = GCCUtilitario.NullableString(strnumeroContrato)
                .Observacion = GCCUtilitario.NullableString(strObservacion)
                .Codigotipoobservacion = GCCUtilitario.NullableString(CodigoTipoObservacion)
                .Audusuarioregistro = GCCUtilitario.NullableString(audUsuarioRegistro)
                .FlagOrigenObservacion = GCCUtilitario.CheckInt(intFlagOrigenObservacion)

            End With


            strEGccontratoDocObservacion = GCCUtilitario.SerializeObject(oEGccObservacionDocuento)
            Dim intResult As Boolean = objObservacionDocumentoInsTx.InsertarContratoDocumentoObservacion(strEGccontratoDocObservacion)
            If intResult = 0 Then
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
