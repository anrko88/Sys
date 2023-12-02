Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS



Partial Class Comun_frmCheckListSubirArchivo
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCheckListSubirArchivo.aspx.vb")

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

                GCCUtilitario.CargarComboValorGenerico(Me.cmbCondicionesAdicionales, GCCConstante.C_TABLAGENERICA_CONDICIONES_ADICIONALES)
                hddCodConDoc.Value = Request.QueryString("hddCodConDoc").ToString.Trim()
                hddCodContrato.Value = Request.QueryString("hddCodContrato").ToString.Trim()
                hddflaCondAdicional.Value = Request.QueryString("sflaCondAdicional").ToString.Trim()

                If (hddflaCondAdicional.Value = 1) Then
                    lbltitulo.Text = "Agregar Condiciones Adicionales"
                Else
                    lbltitulo.Text = "Documentos"
                End If


                hddOrigenCondicion.Value = Request.QueryString("hdOrigenCondicion").ToString.Trim()

                'hddFormulario.Value = Request.QueryString("sFormulario").ToString.Trim()


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

#End Region

    Protected Sub cmdguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdguardar.Click
        Dim objContratoDocumento As New EGcc_contratodocumento
        Dim objContratoDocumentoTx As New LCheckListTx
        Dim pETContratoDocumento As String

        Try

            Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "COTIZACION", Nothing)
                'Else
                '   ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
            End If

            With objContratoDocumento
                .Numerocontrato = Me.hddCodContrato.Value
                .Codigodocumento = Nothing
                If hddflaCondAdicional.Value = "1" Then
                    .Codigotipocondicion = Me.cmbCondicionesAdicionales.Value
                Else
                    .Codigotipocondicion = Nothing
                End If
                .Aprobarcomercial = 0
                .Aprobarlegal = 0
                If hddflaCondAdicional.Value = "1" Then
                    .Nombrearchivo = Me.cmbCondicionesAdicionales.Items(Me.cmbCondicionesAdicionales.SelectedIndex).Text
                Else
                    .Nombrearchivo = Me.txtDocumentos.Value

                End If

                'Verificar Datos de Ingreso
                .Adjunto = ViewState("RutaArchivoAdj")  'Me.txtArchivoDocumentos.FileName
                .Codigotipoincoterm = Nothing
                .Incotermmonto = 0
                .Codigoestadochecklist = Nothing
                .Flagcartaenvio = 1
                .Observaciones = Nothing
                .Codigoorigencondicion = hddOrigenCondicion.Value
                .Audestadologico = 1
                .Audfecharegistro = Now()
                .Audfechamodificacion = Now()
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)


            End With
            pETContratoDocumento = GCCUtilitario.SerializeObject(objContratoDocumento)

            Dim blnResult As Boolean = objContratoDocumentoTx.ContratoDocumentoIns(pETContratoDocumento)

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
            'If blnResult Then
            '    Return "0"
            'Else
            '    Return "1"
            'End If
        Catch ex As Exception
            'Dim myException As String
            'myException = ManageException(ex)
            'Return myException

        End Try




    End Sub
End Class
