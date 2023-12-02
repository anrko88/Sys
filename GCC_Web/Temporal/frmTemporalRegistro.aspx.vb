Imports GCC.UI
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS


Partial Class Temporal_frmTemporalRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmTemporalRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            ' Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                Dim strId As String
                strId = Request.QueryString("hddCodigo")

                hddCodigo.Value = strId
            End If
        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                If Not IsPostBack Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                End If
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub

#End Region

#Region "WebMethods"

    <WebMethod()> _
    Public Shared Function BuscaDatosWM(ByVal pCodigo As String) As ETemporal
        Dim oETemporal1 As New ETemporal
        Dim oETemporal As New ETemporal
        Dim objTemporalNTx As New LTemporalNTx
        Dim pETemporal As String

        oETemporal1.Codigo = Integer.Parse(pCodigo)
        pETemporal = GCCUtilitario.SerializeObject(oETemporal1)
        oETemporal = GCCUtilitario.DeserializeObject(Of ETemporal)(objTemporalNTx.LeerTemporal(pETemporal))

        Return oETemporal
    End Function

    <WebMethod()> _
    Public Shared Function GuardarEditar(ByVal strCodigo As String, _
                                         ByVal strFecha As String, _
                                         ByVal strNumero As String, _
                                         ByVal strCombo As String, _
                                         ByVal strCombo1 As String, _
                                         ByVal strTexto As String, _
                                         ByVal strFlag As String, _
                                         ByVal strDecimales As String, _
                                         ByVal strComentario As String) As String
        Try
            Dim objTemporal As New ETemporal
            Dim objTemporalTx As New LTemporalTx
            Dim pETemporal As String


            With objTemporal
                .Codigo = CInt(strCodigo)

                .Texto = GCCUtilitario.NullableString(strTexto)
                .Comentario = GCCUtilitario.NullableString(strComentario)
                .Decimales = GCCUtilitario.StringToDecimal(strDecimales)
                .Fecha = GCCUtilitario.StringToDateTime(strFecha)
                .Flag = strFlag
                .Numero = GCCUtilitario.StringToInteger(strNumero)
            End With

            pETemporal = GCCUtilitario.SerializeObject(objTemporal)

            Dim blnResult As Boolean = objTemporalTx.fblnModificarTemporal(pETemporal)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function GuardarNuevo(ByVal strFecha As String, _
                                        ByVal strNumero As String, _
                                        ByVal strCombo As String, _
                                        ByVal strCombo1 As String, _
                                        ByVal strTexto As String, _
                                        ByVal strFlag As String, _
                                        ByVal strDecimales As String, _
                                        ByVal strComentario As String) As String
        Try
            Dim objTemporal As New ETemporal
            Dim objTemporalTx As New LTemporalTx
            Dim pETemporal As String

            With objTemporal
                .Texto = GCCUtilitario.NullableString(strTexto)
                .Comentario = GCCUtilitario.NullableString(strComentario)
                .Decimales = GCCUtilitario.StringToDecimal(strDecimales)
                .Fecha = GCCUtilitario.StringToDateTime(strFecha)
                .Flag = strFlag
                .Numero = GCCUtilitario.StringToInteger(strNumero)
            End With

            pETemporal = GCCUtilitario.SerializeObject(objTemporal)
            Dim intResult As Integer = objTemporalTx.fintInsertarTemporal(pETemporal)

            If intResult = 0 Then
                Return "0"
            Else
                Return intResult.ToString()
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        End Try
    End Function

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function

#End Region

End Class
