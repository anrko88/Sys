
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports GCC.Entity.EGcc_contratodocumento

Partial Class SituacionCredito_frmSituacionObservacion
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmCheckListObservacion.aspx.vb")
    ReadOnly Titulo As String = "Mantenimiento Bien"
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

                If Request.QueryString("Add").Trim <> "" Then
                    'hflagtipoObs.Value = Request.QueryString("sflagtipoobs").Trim
                    Me.hddAdd.Value = Request.QueryString("Add").Trim
                End If

                hddCodConDoc.Value = Request.QueryString("hddCodConDoc").ToString.Trim()
                hddCodContrato.Value = Request.QueryString("hddCodContrato").ToString.Trim()
                pInicializarControles(CInt(hddCodConDoc.Value), hddCodContrato.Value, 1)
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

        If hdnTitulo.Value = Titulo Then
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
                Dim cadena As String = ""
                pEDocumentoObservacion = GCCUtilitario.SerializeObject(objDocumentoObservacion)
                dtObservacion = GCCUtilitario.DeserializeObject(Of DataTable)(objDocumentoObservacionNTx.RetornarObservacionContratoDocumentoInafectacion(pEDocumentoObservacion))

                If dtObservacion.Rows.Count > 0 Then
                    TxaObs.Value = dtObservacion.Rows(0).Item("Observacion").ToString()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "cmbTipoObservacion", "fn_util_SeteaComboServidor('cmbTipoObservacion','" + RTrim(dtObservacion.Rows(0).Item("Codigotipoobservacion").ToString()) + "');", True)


                    For Each row As DataRow In dtObservacion.Rows
                        If IsDBNull(row.Item("Audfecharegistro")) Then
                            cadena = cadena + " " + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"
                        Else
                            cadena = cadena + " " + "<strong>" + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortDateString() + " " + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortTimeString() + "</strong>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Item("AudUsuarioRegistro").ToString() + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"

                        End If

                        'cadena = cadena + " " + "<strong>" + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortDateString() + "</strong>" + " " + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"
                    Next
                    dv_historial.InnerHtml = "<p align=left>" + cadena + "</p>"
                End If


            Catch ex As Exception
                Throw ex
            Finally

            End Try
        Else

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
                Dim cadena As String = ""
                pEDocumentoObservacion = GCCUtilitario.SerializeObject(objDocumentoObservacion)
                dtObservacion = GCCUtilitario.DeserializeObject(Of DataTable)(objDocumentoObservacionNTx.RetornarDocumentoObservacion(pEDocumentoObservacion))

                If dtObservacion.Rows.Count > 0 Then
                    TxaObs.Value = dtObservacion.Rows(0).Item("Observacion").ToString()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "cmbTipoObservacion", "fn_util_SeteaComboServidor('cmbTipoObservacion','" + RTrim(dtObservacion.Rows(0).Item("Codigotipoobservacion").ToString()) + "');", True)


                    For Each row As DataRow In dtObservacion.Rows
                        If IsDBNull(row.Item("Audfecharegistro")) Then
                            cadena = cadena + " " + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"
                        Else
                            cadena = cadena + " " + "<strong>" + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortDateString() + " " + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortTimeString() + "</strong>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Item("AudUsuarioRegistro").ToString() + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"

                        End If

                        'cadena = cadena + " " + "<strong>" + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortDateString() + "</strong>" + " " + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"
                    Next
                    dv_historial.InnerHtml = "<p align=left>" + cadena + "</p>"
                End If


            Catch ex As Exception
                Throw ex
            Finally

            End Try


        End If
    End Sub

#End Region



End Class
