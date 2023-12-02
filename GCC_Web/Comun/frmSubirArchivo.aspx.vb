Imports GCC.UI

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Comun_frmSubirArchivo
    Inherits GCCBase


    ReadOnly objLog As New GCCLog("frmSubirArchivo.aspx.vb")
    ReadOnly Titulo As String = "Mantenimiento Bien"
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
                If (Not String.IsNullOrEmpty(Request.QueryString("hddCodContrato").ToString())) Then
                    hddCodContrato.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodContrato").ToString)
                End If
                'hddCodContrato.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodContrato").ToString)
                If (Not IsNothing(Request.QueryString("hddCodConDoc"))) Then
                    hddCodConDoc.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodConDoc").ToString)
                End If
                If IsNothing(Request.QueryString("hddBoton")) Then
                    hddDirectorio.Value = "COTIZACION"
                Else
                    hddBoton.Value = GCCUtilitario.NullableString(Request.QueryString("hddBoton").ToString)
                    hddDirectorio.Value = "CONTRATO"
                End If
                If Not IsNothing(Request.QueryString("hddOp")) Then
                    hddOp.Value = Request.QueryString("hddOp").ToString
                End If
                If (Not String.IsNullOrEmpty(Request.QueryString("Titulo").ToString())) Then
                    hddTitulo.Value = GCCUtilitario.NullableString(Request.QueryString("Titulo").ToString)
                End If
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

    Protected Sub cmdguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdguardar.Click
        Dim objContratoDocumento As New EGcc_contratodocumento
        Dim objContratoDocumentoTx As New LCheckListTx
        Dim objContratoDocumentoAfectoTx As New LContratoTx
        Dim pETContratoDocumento As String

        If hddTitulo.Value = Titulo Then
            Try
                'Sube Archivo
                Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
                If Not strArchivo.Trim().Equals("") Then
                    pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "Inafectación", Nothing)
                End If

                With objContratoDocumento
                    .Codigodocumento = CInt(hddCodConDoc.Value)
                    .Adjunto = ViewState("RutaArchivoAdj")  'Me.txtArchivoDocumentos.FileName
                End With

                pETContratoDocumento = GCCUtilitario.SerializeObject(objContratoDocumento)
                'Actualizar ruta! Inafectacion
                 Dim blnResult As Boolean = objContratoDocumentoAfectoTx.ArchivoAdjuntoAfectoUpd(pETContratoDocumento)

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)

            Catch ex As Exception
                GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
            End Try
        Else
            Try
                'Sube Archivo
                Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
                If Not strArchivo.Trim().Equals("") Then
                    pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "CHECK LIST", Nothing)
                    'Else
                    '   ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
                End If

                With objContratoDocumento
                    .Numerocontrato = hddCodContrato.Value
                    .Codigocontratodocumento = CInt(hddCodConDoc.Value)
                    .Adjunto = ViewState("RutaArchivoAdj")  'Me.txtArchivoDocumentos.FileName
                    .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                End With

                pETContratoDocumento = GCCUtilitario.SerializeObject(objContratoDocumento)
                Dim blnResult As Boolean = objContratoDocumentoTx.ContratoDocumentoAdjuntoUpd(pETContratoDocumento)

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)

            Catch ex As Exception
                GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
            End Try

        End If



    End Sub

#End Region

End Class
