Imports GCC.UI

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_OpcionCompra_frmSubirArchivoOC
    Inherits GCCBase

    ReadOnly objLog As New GCCLog("frmSubirArchivoOC.aspx.vb")

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
                If (Not IsNothing(Request.QueryString("hddCodOpcComDoc"))) Then
                    hddCodOpcComDoc.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodOpcComDoc").ToString)
                End If
                If (Not IsNothing(Request.QueryString("hddCodCheck"))) Then
                    hddCodCheck.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodCheck").ToString)
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
        Dim objEOpcionCompra As New EGCC_OpcionCompra
        Dim objLOpcionCompra As New LOpcionCompraTx
        Dim strEOpcionCompra As String = String.Empty

        Try
            'Sube Archivo
            Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
            If Not strArchivo.Trim().Equals("") Then
                pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "OPCIONCOMPRA", Nothing)
            End If

            With objEOpcionCompra
                .NumeroContrato = hddCodContrato.Value
                .CodCheckList = hddCodCheck.Value
                .CodOperacionDocumento = GCCUtilitario.CheckInt(hddCodOpcComDoc.Value)
                .Adjunto = ViewState("RutaArchivoAdj")  'Me.txtArchivoDocumentos.FileName                
            End With

            strEOpcionCompra = GCCUtilitario.SerializeObject(objEOpcionCompra)
            Dim blnResult As Boolean = objLOpcionCompra.fblnInsertarOpcionCompraDocumento(strEOpcionCompra)

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)

        Catch ex As Exception
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub

#End Region

End Class
