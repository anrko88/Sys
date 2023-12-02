Imports Microsoft.VisualBasic
Imports System.DirectoryServices
Imports System.Data
Imports GCC.UI
Imports GCC.LogicWS
Imports GCC.Entity
Imports System.Xml
Imports System.IO
Imports System.Net.Mime

Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Collections.Generic

Namespace GCC.UI

    Public Class GCCBase
        Inherits System.Web.UI.Page

#Region "Constantes"
        Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
        Private Const C_NOMBRE_CLASE As String = "GCCBase"
#End Region

#Region "Constantes"

        ''' <summary>
        ''' Evento al cargar la Página
        ''' </summary>    
        ''' <remarks></remarks>
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Dim strPaginaActual As String = Request.ServerVariables("URL").ToString()
                Dim strPaginaReves As String = StrReverse(strPaginaActual)
                Dim intIndiceSlash As Int32 = strPaginaReves.IndexOf("/")
                strPaginaActual = strPaginaActual.Substring(strPaginaReves.Length - intIndiceSlash, intIndiceSlash)

                GCCSession.URLAplicacion = GCCUtilitario.fstrObtieneKeyWebConfig("URLAplicacion")

                If strPaginaActual.ToUpper() = GCCConstante.C_PAGINA_INICIAL.ToString().ToUpper() Then
                    If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                        Dim urlAnt As String = Request.QueryString("urlant")
                        If String.IsNullOrEmpty(urlAnt) Then
                            pInicializarAplicacion()
                        Else
                            If urlAnt.ToUpper.IndexOf("FRMLOGIN.ASPX") = -1 Then
                                pInicializarAplicacion()
                            Else
                                pInicializarAplicacionPruebas()
                            End If
                        End If
                    End If
                Else
                    If GCCSession.CodigoUsuario Is Nothing Then
                        Throw New Exception(GCCConstante.C_MENSAJE_ERROR_NO_LOGIN)
                    End If
                End If
            Catch ex As System.Threading.ThreadAbortException

            Catch ex As Exception
                GCCUtilitario.pGuardarLogWeb(C_NOMBRE_APLICATIVO, C_NOMBRE_CLASE, EConstante.C_SUCESO_ERROR, C_NOMBRE_CLASE, ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' CurrentUser
        ''' </summary>    
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentUser() As String
            Get
                Dim strDomainUser As String = Request.ServerVariables("LOGON_USER")
                Dim strUser As String = GCCUtilitario.fstrObtieneKeyWebConfig("UsuarioIdPrueba")
                If strUser = "" Then
                    strUser = Trim(Mid(strDomainUser, InStr(1, strDomainUser, "\", vbTextCompare) + 1, 20))
                End If
                If Not (GCCSession.NombreUsuario Is Nothing) Then
                    strUser = CType(GCCSession.NombreUsuario, String)
                End If
                If strUser <> "" Then
                    strUser = strUser.ToUpper
                    GCCSession.NombreUsuario = strUser
                End If

                Return strUser
            End Get
        End Property

        ''' <summary>
        ''' CurrentDominio
        ''' </summary>    
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentDominio() As String
            Get
                Dim strDominio As String = GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
                Return strDominio
            End Get
        End Property

        ''' <summary>
        ''' CurrentTerminal
        ''' </summary>    
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentTerminal() As String
            Get
                Return HttpContext.Current.Request.UserHostAddress
            End Get
        End Property

        ''' <summary>
        ''' pInicializarAplicacionPruebas
        ''' </summary>    
        ''' <remarks></remarks>
        Private Sub pInicializarAplicacionPruebas()
            GCCSession.SD = ""
            'El ambiente para las pruebas se saca del WebConfig
            GCCSession.Ambiente = GCCUtilitario.fstrObtieneKeyWebConfig("ArgAmbiente")
            GCCSession.AmbienteDesarrollo = ""
            GCCSession.DominioUsuario = "grupoib.local"
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                If Not String.IsNullOrEmpty(Request.QueryString("usu")) Then
                    GCCSession.CodigoUsuario = CType(Request.QueryString("usu"), String)
                Else
                    GCCSession.CodigoUsuario = "B15117"
                End If
            End If

            BusquedaActiveDirectory(GCCSession.CodigoUsuario, "NOMBRE", GCCSession.NombreUsuario, "")

            If String.IsNullOrEmpty(GCCSession.NombreUsuario) Then
                GCCSession.NombreUsuario = "Usuario de Prueba"
            Else
                GCCSession.NombreUsuario += " *"
            End If
            Dim oLwsConfiguracion As New LConfiguracion
            If String.IsNullOrEmpty(GCCSession.PerfilUsuario) Then
                If Not String.IsNullOrEmpty(Request.QueryString("rol")) Then
                    Dim odtRol As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsConfiguracion.ObtenerRolSDA(CType(Request.QueryString("rol"), String)))
                    For Each dr As DataRow In odtRol.Rows
                        GCCSession.PerfilUsuario = CType(dr("CodigoPerfil"), String)
                        GCCSession.DescripcionPerfil = CType(dr("NOMBRE"), String)
                    Next
                Else
                    GCCSession.PerfilUsuario = "1"
                    GCCSession.DescripcionPerfil = "Administrador"
                End If
            End If

            GCCSession.AccesoUsuario = GCCUtilitario.fstrObtieneKeyWebConfig("AccesoSDA")
            GCCSession.ServidorBD = ""
            GCCSession.NombreBD = ""
            GCCSession.Pin = "0000"
        End Sub

        ''' <summary>
        ''' pInicializarAplicacion
        ''' </summary>    
        ''' <remarks></remarks>
        Private Sub pInicializarAplicacion()
            Try
                Dim itUsuario As ESeguridad = Nothing
                Dim strXML As String = ""
                Dim sUser As String = ""
                Dim sPin As String = ""
                Dim sDbServer As String = ""
                Dim sDbName As String = ""
                Dim sPerfil As String = String.Empty

                Dim iRes As Integer = -1
                'Lectura del Pin y demas datos enviados por SDA
                sUser = Request.QueryString("arg01")
                sPin = Request.QueryString("arg02")
                sDbServer = Request.QueryString("arg03")
                sDbName = Request.QueryString("arg04")
                sPerfil = Request.QueryString("arg05")

                strXML = ValidarAccesoUsuario(sUser, sPin, sDbServer, sDbName, sPerfil)

                itUsuario = CType(GCC.UI.GCCUtilitario.DeserializeObject(Of ESeguridad)(strXML), ESeguridad)

                If Not itUsuario Is Nothing Then
                    If itUsuario.MensajeError = "OK" Then
                        GCC.UI.GCCSession.SD = itUsuario.SD
                        GCC.UI.GCCSession.Ambiente = itUsuario.Ambiente
                        GCC.UI.GCCSession.AmbienteDesarrollo = itUsuario.AmbienteDesarrollo
                        GCC.UI.GCCSession.DominioUsuario = GCC.UI.GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
                        GCC.UI.GCCSession.CodigoUsuario = itUsuario.CodigoUsuario.Trim.ToUpper
                        GCC.UI.GCCSession.NombreUsuario = itUsuario.NombreUsuario

                        'Obtiene el Codigo Interno 
                        Dim oLwsConfiguracion As New LConfiguracion
                        Dim odtRol As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsConfiguracion.ObtenerRolSDA(CType(itUsuario.PerfilUsuario, Integer)))
                        For Each dr As DataRow In odtRol.Rows
                            GCCSession.PerfilUsuario = CType(dr("CodigoPerfil"), String)
                            GCCSession.DescripcionPerfil = CType(dr("NOMBRE"), String)
                        Next

                        GCC.UI.GCCSession.AccesoUsuario = itUsuario.AccesoUsuario
                        GCC.UI.GCCSession.ServidorBD = itUsuario.ServidorBD
                        GCC.UI.GCCSession.NombreBD = itUsuario.NombreBD
                        GCC.UI.GCCSession.Pin = itUsuario.PIN

                        If odtRol IsNot Nothing Then
                            odtRol.Dispose()
                            odtRol = Nothing
                        End If
                    Else
                        Throw New Exception(itUsuario.MensajeError.Split("|"c)(0))
                    End If
                Else
                    Throw New Exception(GCC.UI.GCCConstante.C_MENSAJE_ERROR_NO_LOGIN)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' GetDirectoryEntry
        ''' </summary>    
        ''' <remarks></remarks>
        Protected Function GetDirectoryEntry() As DirectoryEntry
            Dim de As DirectoryEntry
            de = New DirectoryEntry
            de.Path = "LDAP://DC=grupoib,DC=local"
            de.AuthenticationType = AuthenticationTypes.Secure
            Return (de)
        End Function

        ''' <summary>
        ''' BusquedaActiveDirectory
        ''' </summary>    
        ''' <remarks></remarks>
        Protected Function BusquedaActiveDirectory(ByVal argsCodigo As String, _
                                                   ByVal argsTipo As String, _
                                                   ByRef argsRespuesta As String, _
                                                   ByRef argsError As String) As Boolean
            'Dim account As String = argsCodigo
            Dim sFuncion As String = ""
            BusquedaActiveDirectory = False
            sFuncion = "BusquedaActiveDirectory"
            argsCodigo = Trim(argsCodigo)
            Try
                Dim entry As DirectoryEntry = GetDirectoryEntry()
                Dim search As DirectorySearcher = New DirectorySearcher(entry)
                search.Filter = "(SAMAccountName=" + argsCodigo + ")"
                Select Case argsTipo
                    Case "NOMBRE"
                        search.PropertiesToLoad.Add("displayName")
                    Case "CORREO"
                        search.PropertiesToLoad.Add("mail")
                End Select

                Dim result As SearchResult = search.FindOne()
                If Not IsNothing(result) Then
                    Select Case argsTipo
                        Case "NOMBRE"
                            argsRespuesta = CStr(result.Properties("displayName")(0))
                        Case "CORREO"
                            argsRespuesta = CStr(result.Properties("mail")(0))
                    End Select
                    If Trim(argsRespuesta) <> "" Then
                        BusquedaActiveDirectory = True
                    Else
                        argsError = "La búsqueda en Active Directory devolvió cadena vacía, Usuario: " + argsCodigo
                        GCCUtilitario.pGuardarLogWeb(C_NOMBRE_APLICATIVO, C_NOMBRE_CLASE, EConstante.C_SUCESO_INFORMATIVO, "BusquedaActiveDirectory", argsError)
                    End If
                End If
            Catch ex As Exception
                GCCUtilitario.pGuardarLogWeb(C_NOMBRE_APLICATIVO, C_NOMBRE_CLASE, EConstante.C_SUCESO_ERROR, "BusquedaActiveDirectory", ex.Message)
            End Try
        End Function

#End Region

#Region "Validar Accesos"
        '-----------------------------------------------------------------------------
        'Nombre             : ValidarAccesoUsuario
        'Objetivo           : Permite obtener los datos de una Rolbandeja determinada
        'Parámetros         : 
        'Creado Por         : TSF - JAM
        'Fecha de Creación  : 17/04/2011 
        '-----------------------------------------------------------------------------
        Public Function ValidarAccesoUsuario(ByVal strUsuario As String, _
                                                    ByVal strPin As String, _
                                                    ByVal strDBServer As String, _
                                                    ByVal strDBName As String, _
                                                    ByVal strDbPerfil As String) As String
            Try
                Dim objSeguridad As New LConfiguracion
                Return objSeguridad.ValidarAccesoUsuario(strUsuario, strPin, strDBServer, strDBName, strDbPerfil)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Adjuntar Archivo"

        ''' <summary>
        ''' pEliminarArchivoFileServerBase
        ''' </summary>    
        ''' <remarks></remarks>
        Public Sub pEliminarArchivoFileServerBase(ByVal psrRutaArchivo As String)
            Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")

            If File.Exists(rutaVirtual + psrRutaArchivo) Then
                File.Delete(rutaVirtual + psrRutaArchivo)
            End If
        End Sub

        ''' <summary>
        ''' pGuardarArchivoFileServerBase
        ''' </summary>    
        ''' <remarks></remarks>
        Public Function pGuardarArchivoFileServerBase(ByVal archivo As FileUpload, _
                                                      ByVal reemplazo As Boolean, _
                                                      ByVal pstrNombAccion As String, _
                                                      ByVal pHidden1 As HtmlInputHidden, _
                                                      Optional ByRef numError As Integer = 0) As Boolean
            Dim guardo As Boolean = False

            Try
                If archivo.HasFile Then
                    Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") 'wbcConstante.C_RUTASERVIDOR_FILESERVER

                    If IsNumeric(GCCUtilitario.fstrObtieneKeyWebConfig("TamanoArchivosBytes")) Then
                        Dim tamano As Integer = CInt(GCCUtilitario.fstrObtieneKeyWebConfig("TamanoArchivosBytes"))
                        Dim scriptString As String = String.Empty
                        'Archivos sin contenido
                        If archivo.PostedFile.ContentLength = 0 Then
                            scriptString += " alert('El archivo esta vacio en su contenido. \nVerifique para continuar.');" + Environment.NewLine
                        ElseIf archivo.PostedFile.ContentLength > tamano Then
                            'Archivos muy grandes
                            tamano = tamano / 1000000
                            scriptString += " alert('El tamaño del archivo sobrepasa los " + tamano.ToString() + " MB.');" + Environment.NewLine
                            Return False
                        End If

                        If scriptString.Length > 0 Then
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ConfirmScript", scriptString, True)
                            Exit Function
                        End If
                    End If

                    ViewState("NomArchivo") = Replace(archivo.FileName, Chr(39), Chr(96))
                    Dim mNombAccion As String = pstrNombAccion '"Documentos\"
                    Dim mNomCarpAnno As String = Now.Year.ToString & "\"
                    Dim mNomCarpMes As String = MonthName(Now.Month).ToUpper & "\"
                    'Dim vNumProp As Integer = 0 'GCCUtilitario.CheckInt(GCCSession.NumeroInstruccion)

                    Dim nombreAr As String = Today.ToShortDateString.Replace("/", "-") & "_" & Now.TimeOfDay.ToString.Replace(":", ".") & "_" & archivo.FileName
                    ViewState("NomArchivo") = nombreAr

                    Dim mRutaTotal As String = rutaVirtual & mNombAccion & mNomCarpAnno & mNomCarpMes
                    If Not Directory.Exists(mRutaTotal) Then Directory.CreateDirectory(mRutaTotal)
                    Dim ruta As String = rutaVirtual & mNombAccion & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96)) 'ViewState("RutaCarpeta")

                    'Ruta de Archivo Adjunto
                    'ViewState("RutaArchivoAdj") = mNombAccion & mNomCarpAnno & mNomCarpMes & nombreAr 'ruta 
                    ViewState("RutaArchivoAdj") = mNombAccion & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96)) 'ruta 
                    'ViewState("RutaArchivoAdj") = rutaVirtual & mNombAccion & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96)) 'ruta 

                    If reemplazo Then
                        archivo.SaveAs(ruta)
                        guardo = True
                    Else
                        If System.IO.File.Exists(ruta) Then
                            Dim scriptString As String = String.Empty
                            ' Store the confirm's return in the hidden control...
                            scriptString += "document.getElementById('" + pHidden1.ClientID + "').value = "
                            scriptString += " confirm('El archivo ya existe en el servidor. Desea Reemplazarlo?');" + Environment.NewLine
                            ' Do a new PostBack...
                            scriptString += ClientScript.GetPostBackEventReference(pHidden1, String.Empty) + ";" + Environment.NewLine
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ConfirmScript", scriptString, True)
                            guardo = False
                        Else
                            archivo.SaveAs(ruta)
                            guardo = True
                        End If
                    End If
                End If
            Catch ex As ObjectDisposedException
                Throw New Exception("Los valores configurados para cargar archivos no permiten cargar el contenido de este archivo.")
            Catch ex As Exception
                ViewState("NomArchivo") = ""
                Throw ex
            End Try
            Return guardo
        End Function

        ''' <summary>
        ''' EliminarArchivoFileServer
        ''' </summary>    
        ''' <remarks></remarks>
        Public Sub EliminarArchivoFileServer(ByVal nombreArchivo As String)
            Try
                Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
                Dim ruta As String = rutaVirtual & nombreArchivo
                If System.IO.File.Exists(ruta) Then
                    System.IO.File.Delete(ruta)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Estructura Mail GCC"
        Public Function EnviarMail(ByVal pstrNroCotizacion As String, _
                                   ByVal pstrCorreo As String, _
                                   ByVal pstrRutaCarta As String, _
                                   ByVal pstrKeyXml As String, _
                                   ByVal pstrNroContrato As String, _
                                   ByVal pstrListaDoc As String, _
                                   ByVal pstrNombreCliente As String, _
                                   ByVal pstrBien As String, _
                                   ByVal pstrClasificacionBien As String, _
                                   ByVal pstrTipoBien As String, _
                                   ByVal pstrTipoContrato As String, _
                                   ByVal pstrImporte As String, _
                                   ByVal pstrMotivo As String, _
                                   ByVal pstrObservacion As String) As Boolean

            Dim strUsuarioEmisor As String = ConfigurationManager.AppSettings("MailUsuarioDe")

            Try
                pEnvioCorreo(pstrNroCotizacion, _
                             strUsuarioEmisor, _
                             pstrCorreo, _
                             pstrKeyXml, _
                             pstrRutaCarta, _
                             pstrNroContrato, _
                             pstrListaDoc, _
                             pstrNombreCliente, _
                             pstrBien, _
                             pstrClasificacionBien, _
                             pstrTipoBien, _
                             pstrTipoContrato, _
                             pstrImporte, _
                             pstrMotivo, _
                             pstrObservacion)
            Catch ex As Exception
                'GCC.UI.GCCUtilitario.pGuardarLogWeb(C_NOMBRE_APLICATIVO, "GCCBASE", 1, "EnviarMail", ex.Message)
                'Return False
            End Try
            Return True
        End Function

        Private Sub pEnvioCorreo(ByVal pstrNroCotizacion As String, _
                                  ByVal pstrUsuarioEmisor As String, _
                                  ByVal pstrCorreo As String, _
                                  ByVal pstrKeyXml As String, _
                                  ByVal pstrRutaCarta As String, _
                                  ByVal pstrNroContrato As String, _
                                  ByVal pstrListaDoc As String, _
                                  ByVal pstrNombreCliente As String, _
                                  ByVal pstrBien As String, _
                                  ByVal pstrClasificacionBien As String, _
                                  ByVal pstrTipoBien As String, _
                                  ByVal pstrTipoContrato As String, _
                                  ByVal pstrImporte As String, _
                                  ByVal pstrMotivo As String, _
                                  ByVal pstrObservacion As String)

            Dim strEnvioCorreo As String = GCCUtilitario.fstrObtieneKeyWebConfig("EnvioCorreo")
            Try
                Dim pUsuarioEmisor As String = pstrUsuarioEmisor
                Dim pAsunto As String = ""
                Dim pBody As String = ""
                Dim pUsuarioPara As String = ""
                Dim pCodUsuario1 As String = ""
                Dim pCodUsuario2 As String = ""
                Dim pCodUsuario3 As String = ""
                Dim pUsuarioCopia As String = ""
                Dim mCompletaBody As String = ""
                'AAE - 14/09/2012
                Dim strCodCotizacionContrato As String = ""
                Dim oLwsCotizacionNTx As LCotizacionNTx = New LCotizacionNTx
                Dim dtCodUsuarios As DataTable
                Dim strCorreoEjecutivo As String = ""
                Dim strCorreoAdministradores As String = ""
                Dim strCodUsuario As String
                Dim strCodigo As String = ""
                Dim strCorreo As String = ""
                Dim nbrEsCotizacion As Integer = 0
                'Fin AAE    

                pCodUsuario1 = GCCUtilitario.fstrObtieneKeyWebConfig("CodigoUsuario1")
                pCodUsuario2 = GCCUtilitario.fstrObtieneKeyWebConfig("CodigoUsuario2")
                pCodUsuario3 = GCCUtilitario.fstrObtieneKeyWebConfig("CodigoUsuario3")

                'Inicio AAE - Obtengo Ejecutivo de Leasing
                If pstrKeyXml = "MailGenerarCartaCotizacion" Or pstrKeyXml = "MailSolicitudCliente" Or pstrKeyXml = "MailSolicitudProveedor" Or pstrKeyXml = "MailChecklistLegal" Or pstrKeyXml = "MailContratoNotaria" Or pstrKeyXml = "MailContratoFirmado" Or pstrKeyXml = "MailCotizacionSupervisor" Or pstrKeyXml = "MailCotizacionSupervisorDevuelta" Or pstrKeyXml = "MailCotizacionSupervisorAprobada" Or pstrKeyXml = "MailChecklistComercial" Then
                    ' Chequeo si tengo parametro cotizacion
                    If pstrKeyXml = "MailGenerarCartaCotizacion" Or pstrKeyXml = "MailSolicitudCliente" Or pstrKeyXml = "MailSolicitudProveedor" Or pstrKeyXml = "MailCotizacionSupervisor" Or pstrKeyXml = "MailCotizacionSupervisorDevuelta" Or pstrKeyXml = "MailCotizacionSupervisorAprobada" Then
                        nbrEsCotizacion = 0
                        strCodCotizacionContrato = pstrNroCotizacion
                    Else
                        If pstrKeyXml = "MailChecklistLegal" Or pstrKeyXml = "MailContratoNotaria" Or pstrKeyXml = "MailContratoFirmado" Or pstrKeyXml = "MailChecklistComercial" Then
                            strCodCotizacionContrato = pstrNroContrato
                        Else
                            strCodCotizacionContrato = pstrNroCotizacion
                        End If
                        nbrEsCotizacion = 1
                    End If

                    dtCodUsuarios = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacionNTx.GetCodUsuarioEjecutivo(strCodCotizacionContrato, nbrEsCotizacion))
                    If dtCodUsuarios.Rows.Count > 0 Then
                        strCodUsuario = dtCodUsuarios.Rows(0).Item(0).ToString().Trim()
                        BusquedaActiveDirectory(strCodUsuario, "CORREO", strCorreoEjecutivo, "")
                        If strCorreoEjecutivo = "" Then
                            strCorreoEjecutivo = strCodUsuario + "@inercorp.com.pe"
                        End If
                    End If

                    pUsuarioCopia = strCorreoEjecutivo
                End If
                'AAE - Obtengo Supervisores para Leasing
                If pstrKeyXml = "MailCotizacionSupervisor" Then
                    dtCodUsuarios = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacionNTx.GetCodUsuarioAdministradoresComercial())
                    If dtCodUsuarios.Rows.Count > 0 Then
                        For i As Integer = 0 To dtCodUsuarios.Rows.Count - 1
                            strCodigo = dtCodUsuarios.Rows(i)(0).ToString().Trim()
                            strCorreo = ""
                            BusquedaActiveDirectory(strCodigo, "CORREO", strCorreo, "")
                            If strCorreo = "" Then
                                strCorreo = strCodigo + "@intercorp.com.pe"
                            End If
                            If i = 0 Then
                                strCorreoAdministradores = strCorreo
                            Else
                                strCorreoAdministradores = strCorreoAdministradores + ";" + strCorreo
                            End If
                        Next
                    End If
                    pUsuarioCopia = pUsuarioCopia + ";" + strCorreoAdministradores
                End If
                'AAE - Obtengo administradores de Leasing
                If pstrKeyXml = "MailContratoFirmado" Then
                    dtCodUsuarios = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacionNTx.GetCodUsuarioAdministradoresLeasing())
                    If dtCodUsuarios.Rows.Count > 0 Then
                        For i As Integer = 0 To dtCodUsuarios.Rows.Count - 1
                            strCodigo = dtCodUsuarios.Rows(i)(0).ToString().Trim()
                            strCorreo = ""
                            BusquedaActiveDirectory(strCodigo, "CORREO", strCorreo, "")
                            If strCorreo = "" Then
                                strCorreo = strCodigo + "@intercorp.com.pe"
                            End If
                            If i = 0 Then
                                strCorreoAdministradores = strCorreo
                            Else
                                strCorreoAdministradores = strCorreoAdministradores + ";" + strCorreo
                            End If
                        Next
                    End If
                    pUsuarioCopia = pUsuarioCopia + ";" + strCorreoAdministradores
                End If

                'Fin AAE

                pAsunto = fCargarVariablesGCCMail(pstrKeyXml, "Subject")
                pBody = fCargarVariablesGCCMail(pstrKeyXml, "Body")

                pReemplazarDatosXml(pAsunto, pBody, pstrNroCotizacion, pstrCorreo, pstrNroContrato, pstrListaDoc, pstrNombreCliente, pstrBien, pstrClasificacionBien, pstrTipoBien, pstrTipoContrato, pstrImporte, pstrMotivo, pstrObservacion)

                If strEnvioCorreo.ToUpper = "SI" Then
                    Dim strUsuarioPara As String = ""
                    Dim strUsuarioPara1 As String = ""
                    Dim strUsuarioPara2 As String = ""
                    If pstrKeyXml = "MailChecklistComercial" Then
                        BusquedaActiveDirectory(pCodUsuario1, "CORREO", strUsuarioPara, "")
                        BusquedaActiveDirectory(pCodUsuario2, "CORREO", strUsuarioPara1, "")
                        BusquedaActiveDirectory(pCodUsuario3, "CORREO", strUsuarioPara2, "")
                        If strUsuarioPara = "" Then
                            strUsuarioPara = pCodUsuario1 + "@intercorp.com.pe"
                        End If
                        If strUsuarioPara2 = "" Then
                            strUsuarioPara2 = pCodUsuario3 + "@intercorp.com.pe"
                        End If
                        If strUsuarioPara1 = "" Then
                            strUsuarioPara1 = pCodUsuario2 + "@intercorp.com.pe"
                        End If

                        pUsuarioPara = strUsuarioPara + ";" + strUsuarioPara1 + ";" + strUsuarioPara2
                    Else
                        BusquedaActiveDirectory(GCCSession.CodigoUsuario, "CORREO", strUsuarioPara, "")
                        If strUsuarioPara = "" Then
                            strUsuarioPara = GCCSession.CodigoUsuario + "@intercorp.com.pe"
                        End If
                        pUsuarioPara = strUsuarioPara
                    End If

                ElseIf strEnvioCorreo.ToUpper = "NO" Then
                    pUsuarioPara = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioPara")
                    pUsuarioCopia = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioCC")
                    'mCompletaBody = "<br/>Usuario Para : " & GCCSession.CodigoUsuario
                End If

                If String.IsNullOrEmpty(pAsunto) Or String.IsNullOrEmpty(pUsuarioPara) Then Throw New Exception("Asunto o Usuario Destino vacio.")
                If String.IsNullOrEmpty(pUsuarioPara) And String.IsNullOrEmpty(pUsuarioCopia) Then Throw New Exception("Usuario Destino y Usuario Copia vacio.")

                EnviarSMTP(pUsuarioPara, pUsuarioEmisor, pAsunto.Trim, pUsuarioCopia, pBody & mCompletaBody, pstrRutaCarta)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub pReemplazarDatosXml(ByRef pstSubjectXML As String, _
                                      ByRef pstBodyXML As String, _
                                      ByVal pstrCodigoCotizacion As String, _
                                      ByVal pstrCorreoCliente As String, _
                                      ByVal pstrNroContrato As String, _
                                      ByVal pstrListaDoc As String, _
                                      ByVal pstrNombreCliente As String, _
                                      ByVal pstrBien As String, _
                                      ByVal pstrClasificacionBien As String, _
                                      ByVal pstrTipoBien As String, _
                                      ByVal pstrTipoContrato As String, _
                                      ByVal pstrImporte As String, _
                                      ByVal pstrMotivo As String, _
                                      ByVal pstrObservacion As String)

            Dim NombreMetodo As String = "pReemplazarDatosXml"
            Dim strElemento As String = "GCCBase"

            Try
                If String.IsNullOrEmpty(pstSubjectXML) And String.IsNullOrEmpty(pstBodyXML) Then
                    Exit Sub
                End If

                pstBodyXML = pstBodyXML.Replace("[", "<")
                pstBodyXML = pstBodyXML.Replace("]", ">")

                'Setear Correo Cliente
                pstBodyXML = pstBodyXML.Replace("@CorreoCliente@", pstrCorreoCliente)

                'Setear Codigo Cotizacion
                pstBodyXML = pstBodyXML.Replace("@CodigoCotizacion@", pstrCodigoCotizacion)
                pstSubjectXML = pstSubjectXML.Replace("@CodigoCotizacion@", pstrCodigoCotizacion)
                'Seteo Nro Contrato
                pstBodyXML = pstBodyXML.Replace("@NumContrato@", pstrNroContrato)

                'Seteo Documentos
                pstBodyXML = pstBodyXML.Replace("@Documentos@", pstrListaDoc.Replace("|", "<br/>"))
                'Setear Codigo Cotizacion
                pstSubjectXML = pstSubjectXML.Replace("@CodigoCotizacion@", pstrCodigoCotizacion)

                'Seteo Nro Contrato
                pstSubjectXML = pstSubjectXML.Replace("@NumContrato@", pstrNroContrato)

                'Seteo Nombre Cliente
                pstBodyXML = pstBodyXML.Replace("@NombreCliente@", pstrNombreCliente)
                pstSubjectXML = pstSubjectXML.Replace("@NombreCliente@", pstrNombreCliente)
                'Seteo Bien
                pstBodyXML = pstBodyXML.Replace("@bien@", pstrBien.Replace("|", "<br/>"))
                pstSubjectXML = pstSubjectXML.Replace("@bien@", pstrBien)
                'Seteo ClasificacionBien
                pstBodyXML = pstBodyXML.Replace("@ClasificacionBien@", pstrClasificacionBien)
                pstSubjectXML = pstSubjectXML.Replace("@ClasificacionBien@", pstrClasificacionBien)
                'Seteo TipoBien
                pstBodyXML = pstBodyXML.Replace("@TipoBien@", pstrTipoBien)
                pstSubjectXML = pstSubjectXML.Replace("@TipoBien@", pstrTipoBien)
                'Seteo TipoContrato
                pstBodyXML = pstBodyXML.Replace("@TipoContrato@", pstrTipoContrato)
                pstSubjectXML = pstSubjectXML.Replace("@TipoContrato@", pstrTipoContrato)
                'Seteo Importe
                pstBodyXML = pstBodyXML.Replace("@Importe@", pstrImporte)
                pstSubjectXML = pstSubjectXML.Replace("@Importe@", pstrImporte)
                'Seteo Motivo
                pstBodyXML = pstBodyXML.Replace("@Motivo@", pstrMotivo)
                pstSubjectXML = pstSubjectXML.Replace("@Motivo@", pstrMotivo)
                'Seteo Observacion
                pstBodyXML = pstBodyXML.Replace("@Observacion@", pstrObservacion)
                pstSubjectXML = pstSubjectXML.Replace("@Observacion@", pstrObservacion)


                pstSubjectXML = pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
                pstBodyXML = fReplaceLetrasHtml(pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))

            Catch ex As Exception
                'Dim objLog As New Log
                'objLog.toWrite("", 4, strElemento, NombreMetodo, 3, "-1", "Falló " & NombreMetodo, ex.Source, ex.Message, "")
                'objLog = Nothing
            End Try
        End Sub

        Private Function fCargarVariablesGCCMail(ByVal argSeccion As String, _
                                           ByVal argNomClave As String) As String


            Dim NombreMetodo As String = "fCargarVariablesGCCMail"
            Dim strElemento As String = "GCCMail"
            Dim strMenRuta As String = ""
            '0.Definir variables
            Try
                Dim xmlSeccionList As XmlNodeList
                Dim xmlSeccion, xmlChild As XmlNode
                Dim XmlDoc As New XmlDocument()
                Dim Ruta As String = String.Concat(Server.MapPath(".."), "\MailGCC\xmlMailGCC.xml")
                strMenRuta = Ruta

                XmlDoc.Load(Ruta)
                Dim mNodo As String = "MailGCC/" & argSeccion & "/" & argNomClave
                xmlSeccion = XmlDoc.SelectSingleNode(mNodo)
                xmlSeccionList = xmlSeccion.ChildNodes

                For Each xmlChild In xmlSeccionList
                    Return (xmlChild.InnerText)
                Next

            Catch ex As Exception
                GCC.UI.GCCUtilitario.pGuardarLogWeb(C_NOMBRE_APLICATIVO, C_NOMBRE_CLASE, 1, NombreMetodo, ex.Message & " " & strMenRuta)
            End Try

            Return ""
        End Function

        Private Sub EnviarSMTP(ByVal pstrUsuarioMailReceptor As String, _
                                  ByVal pstrUsuarioEmisor As String, _
                                  ByVal pstrAsuntoAEnviar As String, _
                                  ByVal pstrCC As String, _
                                  ByVal pstrBody As String, _
                                  ByVal pRutaDocAdjunto As String)

            Dim arrstrCorreoCC As String() = Nothing
            Dim arrstrCorreoPara As String() = Nothing
            Dim SMTP_SERVER As String = ConfigurationManager.AppSettings("ServidorSMTP")

            Dim correo As New System.Net.Mail.MailMessage()
            correo.From = New System.Net.Mail.MailAddress(pstrUsuarioEmisor)

            If Not pstrUsuarioMailReceptor.Trim = "" Then
                arrstrCorreoPara = pstrUsuarioMailReceptor.Split(";")
                If arrstrCorreoPara IsNot Nothing Then
                    For intIndex As Integer = 0 To arrstrCorreoPara.Length - 1
                        correo.To.Add(arrstrCorreoPara(intIndex))
                    Next
                End If
            End If

            If Not pstrCC.Trim = "" Then
                arrstrCorreoCC = pstrCC.Split(";")
                If arrstrCorreoCC IsNot Nothing Then
                    For intIndex As Integer = 0 To arrstrCorreoCC.Length - 1
                        correo.CC.Add(arrstrCorreoCC(intIndex))
                    Next
                End If
            End If

            If Not String.IsNullOrEmpty(pRutaDocAdjunto) Then
                If System.IO.File.Exists(pRutaDocAdjunto) Then
                    Dim attachement As New System.Net.Mail.Attachment(pRutaDocAdjunto)
                    correo.Attachments.Add(attachement)
                End If
            End If

            correo.Subject = pstrAsuntoAEnviar
            correo.Body = pstrBody
            correo.IsBodyHtml = True
            correo.Priority = System.Net.Mail.MailPriority.Normal
            '
            Dim smtp As New System.Net.Mail.SmtpClient(SMTP_SERVER)

            Try
                smtp.Send(correo)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Function fReplaceLetrasHtml(ByVal argTextoaReemplazar As String) As String
            argTextoaReemplazar = Replace(argTextoaReemplazar, "Á", "&Aacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "É", "&Eacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "Í", "&Iacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "Ó", "&Oacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "Ú", "&Uacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "Ñ", "&Ntilde;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "á", "&aacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "é", "&eacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "í", "&iacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "ó", "&oacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "ú", "&uacute;")
            argTextoaReemplazar = Replace(argTextoaReemplazar, "ñ", "&ntilde;")
            Return argTextoaReemplazar
        End Function

        'Tres niveles de carptas
        Private Function fCargarVariablesGCCMailTresNivel(ByVal argSeccion As String, _
                                           ByVal argNomClave As String) As String


            Dim NombreMetodo As String = "fCargarVariablesGCCMail"
            Dim strElemento As String = "GCCMail"
            Dim strMenRuta As String = ""
            '0.Definir variables
            Try
                Dim xmlSeccionList As XmlNodeList
                Dim xmlSeccion, xmlChild As XmlNode
                Dim XmlDoc As New XmlDocument()
                Dim Ruta As String = String.Concat(Server.MapPath("../.."), "\MailGCC\xmlMailGCC.xml")
                strMenRuta = Ruta

                XmlDoc.Load(Ruta)
                Dim mNodo As String = "MailGCC/" & argSeccion & "/" & argNomClave
                xmlSeccion = XmlDoc.SelectSingleNode(mNodo)
                xmlSeccionList = xmlSeccion.ChildNodes

                For Each xmlChild In xmlSeccionList
                    Return (xmlChild.InnerText)
                Next

            Catch ex As Exception
                GCC.UI.GCCUtilitario.pGuardarLogWeb(C_NOMBRE_APLICATIVO, C_NOMBRE_CLASE, 1, NombreMetodo, ex.Message & " " & strMenRuta)
            End Try

            Return ""
        End Function

#End Region

#Region "Estructura Mail Alertas"
        Public Function EnviarMailAlertas(ByVal pstrKeyXml As String, _
                                          ByVal pobjEGCCAlertas As EGCC_Alertas) As Boolean

            Const mNombAccion As String = GCCConstante.C_DIRECTORIO_ALERTAS & "\"
            Dim pAsunto As String = ""
            Dim pBody As String = ""
            Dim pBodyPdf As String = ""
            Dim pUsuarioPara As String = ""
            Dim pUsuarioCopia As String = ""
            Dim pUsuarioEmisor As String = ""
            Dim strSiglas As String = String.Empty

            'Validar si la carpeta de Alartas Existe
            Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
            pUsuarioEmisor = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioDe")

            Dim mNomCarpAnno As String = Now.Year.ToString & "\"
            Dim mNomCarpMes As String = MonthName(Now.Month).ToUpper & "\"
            Dim mRutaTotal As String = rutaVirtual & mNombAccion & mNomCarpAnno & mNomCarpMes
            If Not Directory.Exists(mRutaTotal) Then Directory.CreateDirectory(mRutaTotal)

            Select Case pstrKeyXml
                Case "MailImpuestoVehicular"
                    strSiglas = "IMP"
                    pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailImpuestoVehicular
                Case "MailImpuestoMunicipal"
                    strSiglas = "IMP"
                    pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailImpuestoMunicipal
                Case "MailMultasVehicular"
                    strSiglas = "MUL"
                    pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailMultasVehicular
                Case "MailAsignacionTasador"
                    strSiglas = "TAS"
                    pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailAsignacionTasador
                Case "MailRegistroTasador"
                    strSiglas = "TAS"
                    pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailRegistroTasador
                Case "MailConceptoCobros"
                    strSiglas = "COB"
                    pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailConceptoCobros
            End Select

            Dim nombreAr As String = String.Concat(strSiglas.ToString(), pobjEGCCAlertas.NumContrato, "_", Today.ToShortDateString.Replace("/", "-"), "_", Now.TimeOfDay.ToString.Replace(":", "."), ".pdf")

            Dim strRutaPdf As String = String.Concat(mRutaTotal, nombreAr)

            Try
            'Extraer datos del XML
            pAsunto = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "Subject")
            pBody = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "Body")
            pBodyPdf = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "BodyPdf")

            pReemplazarDatosXmlAlertas(pAsunto, _
                                       pBody, _
                                       pBodyPdf, _
                                       pobjEGCCAlertas)
                'Crar PDF desde HTML
                If (pstrKeyXml <> "MailRegistroTasador") Then
                    HTMLToPdf(pBodyPdf, strRutaPdf)
                Else
                    strRutaPdf = ""
                End If

                'Concatenar correos a enviar
                EnviarMailA(pobjEGCCAlertas.Tipo, pobjEGCCAlertas.Correo, pUsuarioPara)

                Dim strEnvioCorreo As String = GCCUtilitario.fstrObtieneKeyWebConfig("EnvioCorreo")

                If strEnvioCorreo.ToUpper = "NO" Then
                    pUsuarioPara = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioPara")
                    pUsuarioCopia = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioCC")
                End If

                'Enviar Alerta Adjunto
                EnviarSMTP(pUsuarioPara, pUsuarioEmisor, pAsunto.Trim, pUsuarioCopia, pBody, strRutaPdf)

                'Registrar Historial Alertas
                Dim objLAlertasTx As New LAlertasTX
                Dim blnResult As Boolean = objLAlertasTx.fInsertarAlertas(GCCUtilitario.SerializeObject(Of EGCC_Alertas)(pobjEGCCAlertas))


            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function


        Public Sub EnviarMailCarta(ByVal pstrTipo As String, _
                                       ByVal pstrCodSolicitudCredito As String, _
                                       ByVal pstrRazonSocial As String, _
                                       ByVal pstrDireccion As String, _
                                       ByVal pstrUbigeo As String, _
                                       ByVal pstrRutaDoc As String)


            Dim pstBodyXML As String = String.Empty
            Dim pstSubjectXML As String = String.Empty
            Dim pUsuarioEmisor As String = String.Empty
            Dim pUsuarioPara As String = String.Empty
            Dim pUsuarioCopia As String = String.Empty
            Dim pstrKeyXml As String = String.Empty
            Dim pstrDias As String = String.Empty
            Dim pstrCarta As String = String.Empty
            Dim pstrMensaje As String = String.Empty

            'Identificar tipo de opcion de compra

            Select Case CInt(pstrTipo)
                Case "1"
                    pstrKeyXml = "MailOpcionComprasxvencer"
                    pstrCarta = "CARTA-OpcionCompra-xVencer.dot"
                    pstrDias = "60"
                Case "2"
                    pstrKeyXml = "MailOpcionComprasxvencer"
                    pstrCarta = "CARTA-OpcionCompra-xVencer.dot"
                    pstrDias = "30"
                Case "3"
                    pstrKeyXml = "MailOpcionComprassoldocumentos"
                    pstrCarta = "CARTA-OpcionCompra-SolDocumentos.dot"
                Case "4"
                    pstrKeyXml = "MailOpcionComprasvencidos"
                    pstrCarta = "CARTA-OpcionCompra-Vencidos.dot"
            End Select

            'Extrae el Emisor para el correo
            pUsuarioEmisor = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioDe")

            'Extrae los correos para quienes enviar
            EnviarMailA(GCCConstante.eTipoAlerta.C_MailOpcionCompras, pstrCodSolicitudCredito, pUsuarioPara)

            'Extraer datos del XML
            pstSubjectXML = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "Subject")
            pstBodyXML = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "Body")

            'Reemplazar en Subject XML
            pstSubjectXML = pstSubjectXML.Replace("[", "<")
            pstSubjectXML = pstSubjectXML.Replace("]", ">")
            pstSubjectXML = pstSubjectXML.Replace("@GCC_Dias@", pstrDias)
            pstSubjectXML = pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
            'pstSubjectXML = fReplaceLetrasHtml(pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))

            'Reemplazar en Body XML
            pstBodyXML = pstBodyXML.Replace("[", "<")
            pstBodyXML = pstBodyXML.Replace("]", ">")
            pstBodyXML = pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
            pstBodyXML = fReplaceLetrasHtml(pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))


            Dim strEnvioCorreo As String = GCCUtilitario.fstrObtieneKeyWebConfig("EnvioCorreo")
            If strEnvioCorreo.ToUpper = "NO" Then
                pUsuarioPara = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioPara")
                pUsuarioCopia = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioCC")
            End If

            EnviarSMTP(pUsuarioPara, pUsuarioEmisor, pstSubjectXML.Trim, pUsuarioCopia, pstBodyXML, GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + pstrRutaDoc)


            'Registrar Historial Alertas
            Dim objLAlertasTx As New LAlertasTX
            Dim pobjEGCCAlertas As New EGCC_Alertas
            pobjEGCCAlertas.NumContrato = pstrCodSolicitudCredito.ToString()
            pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailOpcionCompras
            pobjEGCCAlertas.AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()

            Dim blnResult As Boolean = objLAlertasTx.fInsertarAlertas(GCCUtilitario.SerializeObject(Of EGCC_Alertas)(pobjEGCCAlertas))


        End Sub

        'BK-Pruebas
        Public Sub EnviarMailCarta1(ByVal pstrTipo As String, _
                                       ByVal pstrCodSolicitudCredito As String, _
                                       ByVal pstrRazonSocial As String, _
                                       ByVal pstrDireccion As String, _
                                       ByVal pstrUbigeo As String)


            Dim pstBodyXML As String = String.Empty
            Dim pstSubjectXML As String = String.Empty
            Dim strRutaDoc As String = String.Empty
            Dim pUsuarioEmisor As String = String.Empty
            Dim pUsuarioPara As String = String.Empty
            Dim pUsuarioCopia As String = String.Empty
            Dim pstrKeyXml As String = String.Empty
            Dim pstrDias As String = String.Empty
            Dim pstrCarta As String = String.Empty
            Dim pstrMensaje As String = String.Empty

            'Identificar tipo de opcion de compra

            Select Case CInt(pstrTipo)
                Case "1"
                    pstrKeyXml = "MailOpcionComprasxvencer"
                    pstrCarta = "CARTA-OpcionCompra-xVencer.dot"
                Case "2"
                    pstrKeyXml = "MailOpcionComprasxvencer"
                    pstrCarta = "CARTA-OpcionCompra-xVencer.dot"
                Case "3"
                    pstrKeyXml = "MailOpcionComprassoldocumentos"
                    pstrCarta = "CARTA-OpcionCompra-SolDocumentos.dot"
                Case "4"
                    pstrKeyXml = "MailOpcionComprasvencidos"
                    pstrCarta = "CARTA-OpcionCompra-Vencidos.dot"
            End Select


            'Reemplazar datos por las variables y retorna la ruta del documento
            strRutaDoc = CartaOpcionCompra(pstrTipo, _
                                           pstrCodSolicitudCredito, _
                                           pstrRazonSocial, _
                                           pstrDireccion, _
                                           pstrUbigeo, _
                                           pstrCarta, _
                                           pstrMensaje)

            'Extrae el Emisor para el correo
            pUsuarioEmisor = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioDe")

            'Extrae los correos para quienes enviar
            EnviarMailA(GCCConstante.eTipoAlerta.C_MailOpcionCompras, pstrCodSolicitudCredito, pUsuarioPara)

            'Extraer datos del XML
            pstSubjectXML = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "Subject")
            pstBodyXML = fCargarVariablesGCCMailTresNivel(pstrKeyXml, "Body")

            'Reemplazar en Subject XML
            pstSubjectXML = pstSubjectXML.Replace("[", "<")
            pstSubjectXML = pstSubjectXML.Replace("]", ">")
            pstSubjectXML = pstSubjectXML.Replace("@GCC_Dias@", pstrMensaje)
            pstSubjectXML = pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
            'pstSubjectXML = fReplaceLetrasHtml(pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))

            'Reemplazar en Body XML
            pstBodyXML = pstBodyXML.Replace("[", "<")
            pstBodyXML = pstBodyXML.Replace("]", ">")
            pstBodyXML = pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
            pstBodyXML = fReplaceLetrasHtml(pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))


            Dim strEnvioCorreo As String = GCCUtilitario.fstrObtieneKeyWebConfig("EnvioCorreo")
            If strEnvioCorreo.ToUpper = "NO" Then
                pUsuarioPara = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioPara")
                pUsuarioCopia = GCCUtilitario.fstrObtieneKeyWebConfig("MailUsuarioCC")
            End If

            EnviarSMTP(pUsuarioPara, pUsuarioEmisor, pstSubjectXML.Trim, pUsuarioCopia, pstBodyXML, strRutaDoc)


            'Registrar Historial Alertas
            Dim objLAlertasTx As New LAlertasTX
            Dim pobjEGCCAlertas As New EGCC_Alertas
            pobjEGCCAlertas.NumContrato = pstrCodSolicitudCredito.ToString()
            pobjEGCCAlertas.Tipo = GCCConstante.eTipoAlerta.C_MailOpcionCompras
            pobjEGCCAlertas.AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()

            Dim blnResult As Boolean = objLAlertasTx.fInsertarAlertas(GCCUtilitario.SerializeObject(Of EGCC_Alertas)(pobjEGCCAlertas))


        End Sub

        Private Sub pReemplazarDatosXmlAlertas(ByRef pstSubjectXML As String, _
                                                      ByRef pstBodyXML As String, _
                                                      ByRef pBodyPdfXML As String, _
                                                      ByVal pobEGCCAlertas As EGCC_Alertas)

            Dim NombreMetodo As String = "pReemplazarDatosXmlAlertas"
            Dim strElemento As String = "GCCBase"

            Try
                If String.IsNullOrEmpty(pstSubjectXML) And String.IsNullOrEmpty(pstBodyXML) And String.IsNullOrEmpty(pBodyPdfXML) Then
                    Exit Sub
                End If

                'Reemplazar en Subject XML
                pstSubjectXML = pstSubjectXML.Replace("[", "<")
                pstSubjectXML = pstSubjectXML.Replace("]", ">")
                pstSubjectXML = pstSubjectXML.Replace("@GCC_Concepto@", pobEGCCAlertas.Periodo)
                pstSubjectXML = pstSubjectXML.Replace("@GCC_Periodo@", pobEGCCAlertas.Periodo)
                pstSubjectXML = pstSubjectXML.Replace("@GCC_Contrato@", pobEGCCAlertas.NumContrato)
                pstSubjectXML = pstSubjectXML.Replace("@Mes@", MonthName(Convert.ToDateTime(pobEGCCAlertas.FechaActivacion).Date.Month))
                pstSubjectXML = pstSubjectXML.Replace("@Anio@", Convert.ToDateTime(pobEGCCAlertas.FechaActivacion).Date.Year.ToString)
                pstSubjectXML = pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
                'pstSubjectXML = fReplaceLetrasHtml(pstSubjectXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))

                'Reemplazar en Body XML
                pstBodyXML = pstBodyXML.Replace("[", "<")
                pstBodyXML = pstBodyXML.Replace("]", ">")
                pstBodyXML = pstBodyXML.Replace("@GCC_Contrato@", pobEGCCAlertas.NumContrato)
                pstBodyXML = pstBodyXML.Replace("@GCC_Concepto@", pobEGCCAlertas.Periodo)
                pstBodyXML = pstBodyXML.Replace("@GCC_Periodo@", pobEGCCAlertas.Periodo)
                pstBodyXML = pstBodyXML.Replace("@Mes@", MonthName(Convert.ToDateTime(pobEGCCAlertas.FechaActivacion).Date.Month))
                pstBodyXML = pstBodyXML.Replace("@Anio@", Convert.ToDateTime(pobEGCCAlertas.FechaActivacion).Date.Year.ToString)
                pstBodyXML = pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
                pstBodyXML = fReplaceLetrasHtml(pstBodyXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))

                'Reemplazar en Body Pdf XML
                pBodyPdfXML = pBodyPdfXML.Replace("[", "<")
                pBodyPdfXML = pBodyPdfXML.Replace("]", ">")
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_RutaWeb@", pobEGCCAlertas.RutaWeb)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Periodo@", pobEGCCAlertas.Periodo)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Concepto@", pobEGCCAlertas.Periodo)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Fecha@", pobEGCCAlertas.FechaActual)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_RazonSocial@", pobEGCCAlertas.RazonSocial)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Direccion@", pobEGCCAlertas.Direccion)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Distrito@", pobEGCCAlertas.Distrito)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Provincia@", pobEGCCAlertas.Provincia)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Departamento@", pobEGCCAlertas.Departamento)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_NumContrato@", pobEGCCAlertas.NumContrato)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_FechaPago@", pobEGCCAlertas.FechaPago)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_SimMoneda@", pobEGCCAlertas.SimMoneda)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Importe@", pobEGCCAlertas.Importe)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_TipoCambio@", pobEGCCAlertas.TipoCambio)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Monto@", pobEGCCAlertas.Monto)

                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Comision@", pobEGCCAlertas.Comision)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_IGV@", pobEGCCAlertas.Igv)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_ImporteDetalle@", pobEGCCAlertas.Importe)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_FechaCobro@", pobEGCCAlertas.FechaCobro)
                pBodyPdfXML = pBodyPdfXML.Replace("@tablaDetalle@", pobEGCCAlertas.TablaDetalle)

                'FechaActivacion
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_EmpresaTasador@", pobEGCCAlertas.EmpresaTasador)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Telefono@", pobEGCCAlertas.Telefono)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Tasador@", pobEGCCAlertas.Tasador)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Celular@", pobEGCCAlertas.Celular)
                pBodyPdfXML = pBodyPdfXML.Replace("@GCC_Correo@", pobEGCCAlertas.Correo)

                pBodyPdfXML = pBodyPdfXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, "")
                pBodyPdfXML = fReplaceLetrasHtml(pBodyPdfXML.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbLf, ""))

            Catch ex As Exception
                'Dim objLog As New Log
                'objLog.toWrite("", 4, strElemento, NombreMetodo, 3, "-1", "Falló " & NombreMetodo, ex.Source, ex.Message, "")
                'objLog = Nothing
            End Try
        End Sub

        Private Sub HTMLToPdf(ByVal pstrHml As String, ByVal pstrFilePath As String)

            Dim strRutaPdf As String = pstrFilePath 'String.Concat(pstrFilePath, "Chap0101.pdf") 'C:\\TEMP\\Chap0101.pdf"
            Dim document As Document = New Document(PageSize.A4, 80, 60, 20, 20) ' R,L,T,B
            Dim strPDFWriter As PdfWriter = PdfWriter.GetInstance(document, New FileStream(strRutaPdf, FileMode.Create))
            document.Open()
            Dim styles As iTextSharp.text.html.simpleparser.StyleSheet = New iTextSharp.text.html.simpleparser.StyleSheet()
            Dim dicStylesPrincipal As New Dictionary(Of String, String)
            Dim dicStylesDetalle As New Dictionary(Of String, String)
            Dim dicStylesDetalleMultas As New Dictionary(Of String, String)
            dicStylesPrincipal.Add("border", "0")
            dicStylesPrincipal.Add("cellspacing", "0")
            dicStylesPrincipal.Add("cellpadding", "0")
            styles.LoadStyle("tabela", dicStylesPrincipal)

            dicStylesDetalle.Add("border", "0")
            dicStylesDetalle.Add("cellspacing", "0")
            dicStylesDetalle.Add("cellpadding", "0")
            dicStylesDetalle.Add("width", "450")
            styles.LoadStyle("tabDetalle", dicStylesDetalle)

            dicStylesDetalleMultas.Add("border", "0.5")
            dicStylesDetalleMultas.Add("cellspacing", "0")
            dicStylesDetalleMultas.Add("cellpadding", "1")
            dicStylesDetalleMultas.Add("width", "450")
            styles.LoadStyle("tabDetalleMultas", dicStylesDetalleMultas)

            styles.LoadTagStyle("div", "style", "background-color:red;font-size:9px;color:#000;font-family:Arial Narrow;")
            styles.LoadTagStyle("span", "style", "color: #ffffff;")
            styles.LoadTagStyle("p", "style", "text-align: justify;padding:0")
            styles.LoadTagStyle("p", "style", "text-align: justify;padding:0")
            Dim hw As iTextSharp.text.html.simpleparser.HTMLWorker = New iTextSharp.text.html.simpleparser.HTMLWorker(document)
            hw.Style = styles
            hw.Parse(New StringReader(pstrHml))
            document.Close()
            'ShowPdf("C:\\TEMP\\Chap0101.pdf")
        End Sub


        Public Function CartaOpcionCompra(ByVal pstrTipo As String, _
                                         ByVal pstrCodSolicitudCredito As String, _
                                         ByVal pstrRazonSocial As String, _
                                         ByVal pstrDireccion As String, _
                                         ByVal pstrUbigeo As String, _
                                         ByVal pstrCarta As String, _
                                         ByRef pstrMensaje As String) As String
            Dim oRtfHelper As RtfHelper = Nothing
            Dim sRutaParcial As String = String.Empty
            Dim strMensaje As String = String.Empty

            Const str60Vcto As String = "001"
            Const str30Vcto As String = "002"
            Const str60Mensaje As String = "de los siguientes dos meses"
            Const str30Mensaje As String = "del siguiente mes"

            ' Define la ruta de archivos origen y destino
            Try
                Dim nombreArchivo As String = GCCUtilitario.Concatenar("Empresa-", pstrCodSolicitudCredito, ".doc")
                sRutaParcial = RutaParcial(nombreArchivo, GCCConstante.C_DIRECTORIO_ALERTAS)

                Dim rutaAlerta As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

                oRtfHelper = New RtfHelper(HttpContext.Current.Server.MapPath("../../Util/Plantillas/" & pstrCarta), rutaAlerta)

                If str60Vcto = pstrTipo Then
                    strMensaje = str60Mensaje
                    pstrMensaje = "60"
                ElseIf str30Vcto = pstrTipo Then
                    strMensaje = str30Mensaje
                    pstrMensaje = "30"
                End If

                oRtfHelper.Reemplazar("@GCC_Fecha@", Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString())
                oRtfHelper.Reemplazar("@GCC_RazonSocial@", pstrRazonSocial)
                oRtfHelper.Reemplazar("@GCC_Direccion@", pstrDireccion)
                oRtfHelper.Reemplazar("@GCC_Ubigeo@", pstrUbigeo)
                oRtfHelper.Reemplazar("@GCC_NumContrato@", pstrCodSolicitudCredito)
                oRtfHelper.Reemplazar("@GCC_Mensaje@", strMensaje)

                oRtfHelper.SaveAndClose()

                Return sRutaParcial 'sRutaParcial
            Catch ex As Exception
                oRtfHelper.Close()
                Return ex.ToString()
            End Try
        End Function


        Public Function CartaOpcionCompra1(ByVal pstrTipo As String, _
                                         ByVal pstrCodSolicitudCredito As String, _
                                         ByVal pstrRazonSocial As String, _
                                         ByVal pstrDireccion As String, _
                                         ByVal pstrUbigeo As String, _
                                         ByVal pstrCarta As String, _
                                         ByRef pstrMensaje As String) As String
            Dim oRtfHelper As RtfHelper = Nothing
            Dim sRutaParcial As String = String.Empty
            Dim strMensaje As String = String.Empty

            Const str60Vcto As String = "001"
            Const str30Vcto As String = "002"
            Const str60Mensaje As String = "de los siguientes dos meses"
            Const str30Mensaje As String = "del siguiente mes"

            ' Define la ruta de archivos origen y destino
            Try
                Dim nombreArchivo As String = GCCUtilitario.Concatenar("Empresa-", pstrCodSolicitudCredito, ".doc")
                sRutaParcial = RutaParcial(nombreArchivo, GCCConstante.C_DIRECTORIO_ALERTAS)

                Dim rutaAlerta As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

                oRtfHelper = New RtfHelper(HttpContext.Current.Server.MapPath("../../Util/Plantillas/" & pstrCarta), rutaAlerta)

                If str60Vcto = pstrTipo Then
                    strMensaje = str60Mensaje
                    pstrMensaje = "60"
                ElseIf str30Vcto = pstrTipo Then
                    strMensaje = str30Mensaje
                    pstrMensaje = "30"
                End If

                oRtfHelper.Reemplazar("@GCC_Fecha@", Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString())
                oRtfHelper.Reemplazar("@GCC_RazonSocial@", pstrRazonSocial)
                oRtfHelper.Reemplazar("@GCC_Direccion@", pstrDireccion)
                oRtfHelper.Reemplazar("@GCC_Ubigeo@", pstrUbigeo)
                oRtfHelper.Reemplazar("@GCC_NumContrato@", pstrCodSolicitudCredito)
                oRtfHelper.Reemplazar("@GCC_Mensaje@", strMensaje)

                oRtfHelper.SaveAndClose()

                Return rutaAlerta 'sRutaParcial
            Catch ex As Exception
                oRtfHelper.Close()
                Return ex.ToString()
            End Try
        End Function


        ''' <summary>
        ''' Establece la ruta en el sistema de archivos, excluyendo el directorio raíz.
        ''' Si el directorio no existe, lo crea.
        ''' </summary>
        ''' <param name="FileName">Nombre del archivo a crearse</param>
        ''' <param name="Directorio">Ruta (carpeta) donde se va a crear el archivo</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 22/02/2011
        ''' </remarks>
        Private Function RutaParcial(ByVal FileName As String, _
                                     ByVal Directorio As String) As String
            Try

                Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")

                Dim mNombAccion As String = Directorio & "\"
                Dim mNomCarpAnno As String = Now.Year.ToString & "\"
                Dim mNomCarpMes As String = MonthName(Now.Month).ToUpper & "\"

                Dim nombreAr As String = Today.ToShortDateString.Replace("/", "-") & "_" & Now.TimeOfDay.ToString.Replace(":", ".") & "_" & FileName

                Dim mRutaTotal As String = rutaVirtual & mNombAccion & mNomCarpAnno & mNomCarpMes
                If Not Directory.Exists(mRutaTotal) Then
                    Directory.CreateDirectory(mRutaTotal)
                End If

                Return mNombAccion & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96))
            Catch ex As Exception
                Throw New Exception("Los valores configurados para cargar archivos no permiten cargar el contenido de este archivo.")
            End Try
        End Function


        Private Sub EnviarMailA(ByVal pintTipoAlerta As Int32, ByVal pstrCadena As String, ByRef pUsuarioPara As String)

            Dim strCodCotizacionContrato As String = ""
            Dim oLwsCotizacionNTx As LCotizacionNTx = New LCotizacionNTx
            Dim dtCodUsuarios As DataTable
            Dim strCorreoEjecutivo As String = ""
            Dim strCorreoAdministradores As String = ""
            Dim strCorreoCliente As String = ""
            Dim strCodUsuario As String
            Dim strCodigo As String = ""
            Dim strCorreo As String = ""
            Dim nbrEsCotizacion As Integer = 0

            strCodCotizacionContrato = pstrCadena 'pobjEGCCAlertas.NumContrato ' pstrNroCotizacion

            'Inicio AAE - Obtengo Cliente
            If pintTipoAlerta = GCCConstante.eTipoAlerta.C_MailImpuestoMunicipal OrElse _
                pintTipoAlerta = GCCConstante.eTipoAlerta.C_MailImpuestoVehicular OrElse _
                pintTipoAlerta = GCCConstante.eTipoAlerta.C_MailMultasVehicular OrElse _
                pintTipoAlerta = GCCConstante.eTipoAlerta.C_MailConceptoCobros Then
                If Not String.IsNullOrEmpty(pstrCadena) Then
                    pUsuarioPara = pstrCadena
                End If
            End If

            'Inicio AAE - Obtengo Ejecutivo de Leasing
            If pintTipoAlerta <> GCCConstante.eTipoAlerta.C_MailRegistroTasador Then
                'AndAlso Not String.IsNullOrEmpty(strCodCotizacionContrato)
                dtCodUsuarios = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacionNTx.GetCodUsuarioEjecutivo(strCodCotizacionContrato, nbrEsCotizacion))
                If dtCodUsuarios.Rows.Count > 0 Then
                    strCodUsuario = dtCodUsuarios.Rows(0).Item(0).ToString().Trim()
                    BusquedaActiveDirectory(strCodUsuario, "CORREO", strCorreoEjecutivo, "")
                    If strCorreoEjecutivo = "" Then
                        strCorreoEjecutivo = strCodUsuario + "@inercorp.com.pe"
                    End If
                End If
                pUsuarioPara = pUsuarioPara + ";" + strCorreoEjecutivo
            End If

            If pintTipoAlerta = GCCConstante.eTipoAlerta.C_MailOpcionCompras AndAlso _
               pintTipoAlerta = GCCConstante.eTipoAlerta.C_MailRegistroTasador Then


                'AAE - Obtengo Supervisores para Leasing

                dtCodUsuarios = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacionNTx.GetCodUsuarioAdministradoresComercial())
                If dtCodUsuarios.Rows.Count > 0 Then
                    For i As Integer = 0 To dtCodUsuarios.Rows.Count - 1
                        strCodigo = dtCodUsuarios.Rows(i)(0).ToString().Trim()
                        strCorreo = ""
                        BusquedaActiveDirectory(strCodigo, "CORREO", strCorreo, "")
                        If strCorreo = "" Then
                            strCorreo = strCodigo + "@intercorp.com.pe"
                        End If
                        If i = 0 Then
                            strCorreoAdministradores = strCorreo
                        Else
                            strCorreoAdministradores = strCorreoAdministradores + ";" + strCorreo
                        End If
                    Next
                End If
                pUsuarioPara = pUsuarioPara + ";" + strCorreoAdministradores


                'AAE - Obtengo administradores de Leasing
                dtCodUsuarios = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacionNTx.GetCodUsuarioAdministradoresLeasing())
                If dtCodUsuarios.Rows.Count > 0 Then
                    For i As Integer = 0 To dtCodUsuarios.Rows.Count - 1
                        strCodigo = dtCodUsuarios.Rows(i)(0).ToString().Trim()
                        strCorreo = ""
                        BusquedaActiveDirectory(strCodigo, "CORREO", strCorreo, "")
                        If strCorreo = "" Then
                            strCorreo = strCodigo + "@intercorp.com.pe"
                        End If
                        If i = 0 Then
                            strCorreoAdministradores = strCorreo
                        Else
                            strCorreoAdministradores = strCorreoAdministradores + ";" + strCorreo
                        End If
                    Next
                End If
                pUsuarioPara = pUsuarioPara + ";" + strCorreoAdministradores

                'Fin AAE

            End If

        End Sub

#End Region


    End Class

End Namespace
