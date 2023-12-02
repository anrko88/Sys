Imports System.IO

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class Formalizacion_frmSubirArchivo
    Inherits GCCBase


    ReadOnly objLog As New GCCLog("frmSubirArchivo.aspx.vb")

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
                hddCodContrato.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodContrato").ToString)
                If (Not IsNothing(Request.QueryString("hddCodConDoc"))) Then
                    hddCodConDoc.Value = GCCUtilitario.NullableString(Request.QueryString("hddCodConDoc").ToString)
                End If
                ' Si no hay un control en la página padre, usar la carpeta predeterminada.
                If IsNothing(Request.QueryString("hddControl")) Then
                    hddDirectorio.Value = "COTIZACION"
                Else
                    ' Nombre del control oculto en la página padre con el nombre del archivo
                    hddControl.Value = Request.QueryString("hddControl").ToString()

                    ' Dependiendo del nombre del control, se identifica la carpeta con la cual guardará el archivo adjuntado.
                    Select Case hddControl.Value
                        Case "btnAdjuntarArchivo"
                            hddDirectorio.Value = Directorio.Anexos
                        Case "btnAdjuntarArchivoDocumentoSeparacion"
                            hddDirectorio.Value = Directorio.DocumentoSeparacion
                        Case "btnAdjuntarArchivoOtroConcepto"
                            hddDirectorio.Value = Directorio.OtroConcepto
                        Case "btnAdjuntarArchivoNotarialNuevo", "btnAdjuntarArchivoNotarialEditar"
                            hddDirectorio.Value = Directorio.DocumentoNotarial
                    End Select
                End If

                If Not IsNothing(Request.QueryString("hddOp")) Then
                    hddNombreArchivo.Value = Request.QueryString("hddOp").ToString
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

    ''' <summary>
    ''' Selecciona el tipo de operación a ejecutar, dependiendo del tipo de documento a adjuntar.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub cmdguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdguardar.Click
        Try

            Select Case hddControl.Value
                Case "btnAdjuntarArchivo"
                    ' Contrato (Anexos)
                    Call SubirAnexos()
                    ' Datos del conyugue
                Case "btnAdjuntarArchivoDocumentoSeparacion"
                    Call AdjuntarArchivoDocumentoSeparacion()
                    ' Otros conceptos
                Case "btnAdjuntarArchivoOtroConcepto"
                    Call AdjuntarArchivoOtroConcepto()
                    ' Adenda - nuevo
                Case "btnAdjuntarArchivoNotarialNuevo"
                    Call AdjuntarArchivoNotarialNuevo()
                    ' Adenda - editar
                Case "btnAdjuntarArchivoNotarialEditar"
                    Call AdjuntarArchivoNotarialEditar()
            End Select

        Catch ex As Exception
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Adjunta un documento notarial, cuando la operación es la de edición sobre un número nuevo.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub AdjuntarArchivoNotarialNuevo()
        ' Sube Archivo
        Dim strArchivo As String = Me.txtArchivoDocumentos.FileName

        If Not strArchivo.Trim().Equals("") Then
            GuardarArchivoFileServerBase(Me.txtArchivoDocumentos, _
                                         False, _
                                         Directorio.DocumentoNotarial)
        End If

        hddFile.Value = ViewState("RutaArchivoAdj").ToString()

        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
    End Sub

    ''' <summary>
    ''' Adjunta un documento notarial, cuando la operación es la de edición sobre un número existente.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub AdjuntarArchivoNotarialEditar()
        ' Sube Archivo
        Dim strArchivo As String = txtArchivoDocumentos.FileName

        If Not strArchivo.Trim().Equals("") Then
            GuardarArchivoFileServerBase(Me.txtArchivoDocumentos, _
                                         False, _
                                         Directorio.DocumentoNotarial)
        End If

        hddFile.Value = ViewState("RutaArchivoAdj").ToString()

        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
    End Sub

    ''' <summary>
    ''' Adjunta un documento de separación de bienes, cuando el cliente es persona natural y su estado civil es
    ''' casado, al momento de solicitar el crédito.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub AdjuntarArchivoDocumentoSeparacion()
        Dim objContratoTx As New LContratoTx

        Dim oESolicitudCredito As New ESolicitudcredito
        Dim sESolicitudCredito As String

        ' Sube Archivo
        Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
        If Not strArchivo.Trim().Equals("") Then
            GuardarArchivoFileServerBase(Me.txtArchivoDocumentos, _
                                         False, _
                                         Directorio.DocumentoSeparacion)
        End If

        oESolicitudCredito.Codsolicitudcredito = hddCodContrato.Value
        oESolicitudCredito.Documentoseparacion = ViewState("RutaArchivoAdj").ToString()
        oESolicitudCredito.Modificado = True
        oESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        sESolicitudCredito = GCCUtilitario.SerializeObject(oESolicitudCredito)

        objContratoTx.ActualizaDocumentoSeparacion(sESolicitudCredito)

        hddFile.Value = ViewState("RutaArchivoAdj").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
    End Sub

    ''' <summary>
    ''' Actualiza el archivo de los anexos.
    ''' </summary>   
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub SubirAnexos()
        Dim objContratoTx As New LContratoTx
        Dim oESolicitudcredito As New ESolicitudcredito
        Dim sESolicitudcredito As String

        ' Sube Archivo
        Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
        If Not strArchivo.Trim().Equals("") Then
            Call GCCUtilitario.Eliminar(txtArchivoDocumentos.FileName)

            pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, _
                                          False, _
                                          Directorio.Anexos, Nothing)
        End If

        oESolicitudcredito.Codsolicitudcredito = hddCodContrato.Value
        oESolicitudcredito.ArchivoContratoAdjunto = ViewState("RutaArchivoAdj").ToString()
        oESolicitudcredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        sESolicitudcredito = GCCUtilitario.SerializeObject(oESolicitudcredito)

        objContratoTx.ActualizaArchivoContratoAdjunto(sESolicitudcredito)

        hddFile.Value = ViewState("RutaArchivoAdj").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)

    End Sub

    ''' <summary>
    ''' Actualiza la base de datos de otros conceptos.
    ''' </summary>   
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub AdjuntarArchivoOtroConcepto()
        Try
            Dim objContratoTx As New LContratoTx

            Dim oEgccContratoOtroConcepto As New EGcc_contratootroconcepto
            Dim egccContratoOtroConcepto As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            ' Sube Archivo
            Dim strArchivo As String = Me.txtArchivoDocumentos.FileName
            If Not strArchivo.Trim().Equals("") Then
                Call GuardarArchivoFileServerBase(txtArchivoDocumentos, _
                                                  chkValidarArchivoExistente.Checked, _
                                                  Directorio.OtroConcepto)
            End If

            oEgccContratoOtroConcepto.Numerocontrato = hddCodContrato.Value
            oEgccContratoOtroConcepto.Nombrearchivo = ViewState("RutaArchivoAdj").ToString()
            oEgccContratoOtroConcepto.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            objESolicitudCredito.Codsolicitudcredito = hddCodContrato.Value
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            egccContratoOtroConcepto = GCCUtilitario.SerializeObject(oEgccContratoOtroConcepto)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            objContratoTx.ContratoOtroConceptoAdjuntoUpd(pESolicitudCredito, _
                                                         egccContratoOtroConcepto)

            hddFile.Value = ViewState("RutaArchivoAdj").ToString()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Error", "fn_mensaje('" + ex.Message() + "');", True)
        End Try
    End Sub

    ''' <summary>
    ''' Permite adjuntar un archivo al servidor, en la ruta indicada.
    ''' </summary>
    ''' <param name="archivo">Objeto FileUpload que contiene el nombre del archivo que se va a adjuntar y todas
    ''' sus caracteristicas.
    ''' </param>
    ''' <param name="reemplazo">True: Se eliminará el archivo coincidente en el servidor.
    ''' Otro caso: No se realiza ninguna acción</param>
    ''' <param name="carpetaDestino"></param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub GuardarArchivoFileServerBase(ByRef archivo As FileUpload, _
                                            ByVal reemplazo As Boolean, _
                                            ByVal carpetaDestino As String)
        Try
            Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")

            If IsNumeric(GCCUtilitario.fstrObtieneKeyWebConfig("TamanoArchivosBytes")) Then
                Dim tamano As Integer = CInt(GCCUtilitario.fstrObtieneKeyWebConfig("TamanoArchivosBytes"))

                'Archivos sin contenido
                If archivo.PostedFile.ContentLength = 0 Then
                    Throw New Exception("El archivo esta vacío en su contenido. \nVerifique para continuar.")
                ElseIf archivo.PostedFile.ContentLength > tamano Then
                    'Archivos muy grandes
                    tamano = CType(((tamano / 1024) \ 1024), Integer)
                    Throw New Exception("El tamaño del archivo sobrepasa los " + tamano.ToString() + " MB.")
                End If
            End If

            ViewState("NomArchivo") = Replace(archivo.FileName, Chr(39), Chr(96))

            Dim mNomCarpAnno As String = Now.Year.ToString & "\"
            Dim mNomCarpMes As String = MonthName(Now.Month).ToUpper & "\"

            Dim nombreAr As String = Today.ToShortDateString.Replace("/", "-") & "_" & Now.TimeOfDay.ToString.Replace(":", ".") & "_" & archivo.FileName
            ViewState("NomArchivo") = nombreAr

            Dim mRutaTotal As String = rutaVirtual & carpetaDestino & mNomCarpAnno & mNomCarpMes
            If Not Directory.Exists(mRutaTotal) Then Directory.CreateDirectory(mRutaTotal)
            Dim ruta As String = rutaVirtual & carpetaDestino & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96))

            'Ruta de Archivo Adjunto
            ViewState("RutaArchivoAdj") = carpetaDestino & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96))

            If reemplazo Then
                archivo.SaveAs(ruta)
            Else
                If File.Exists(ruta) Then
                    Throw New Exception("El archivo ya existe en el servidor.")
                Else
                    GCCUtilitario.Eliminar(ruta)
                    archivo.SaveAs(ruta)
                End If
            End If
        Catch ex As ObjectDisposedException
            Throw New Exception("Los valores configurados para cargar archivos no permiten cargar el contenido de este archivo.")
        Catch ex As Exception
            ViewState("NomArchivo") = ""

            Throw ex
        End Try
    End Sub

#End Region

#Region "Web methods"


#End Region

#Region "Clases Privadas"

    ''' <summary>
    ''' Contiene las rutas de los subdirectorios para los docuemntos del contrato
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Class Directorio

        Public Const Anexos As String = GCCConstante.C_DIRECTORIO_ANEXOS

        Public Const DocumentoSeparacion As String = "DOCUMENTO_SEPARACION"
        Public Const OtroConcepto As String = "OTRO_CONCEPTO"
        Public Const DocumentoNotarial As String = "DOCUMENTO_NOTARIAL"

    End Class

#End Region

End Class