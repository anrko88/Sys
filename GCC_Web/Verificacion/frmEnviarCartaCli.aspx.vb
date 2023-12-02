Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data

Partial Class Verificacion_frmEnviarCartaCli
    Inherits GCCBase

    Dim strFechaTerminoRecepCli As String
    Dim strFlagFechaTerminoRecepCli As String
    Dim strCodigoContacto As String
    Dim strCodigocotizacion As String
    Dim strNumeroContrato As String
    Dim strNombre As String
    Dim strCorreo As String
    Dim strTelefono As String
    Dim strAnexo As String
    Dim strCargo As String
    Dim strListaDoc As String
    Dim strRegistros As String
    Dim strNombreCliente As String
    Dim objLog As New GCCLog("frmEnviarCartaCli.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim oLwsCheckListNTx As New LCheckListNTx

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            strFechaTerminoRecepCli = Request.QueryString("p1")
            strFlagFechaTerminoRecepCli = Request.QueryString("p2")
            strCodigoContacto = Request.QueryString("p3")
            strCodigocotizacion = Request.QueryString("p4")
            strNumeroContrato = Request.QueryString("p5")
            strNombre = Request.QueryString("p6")
            strCorreo = Request.QueryString("p7")
            strTelefono = Request.QueryString("p8")
            strAnexo = Request.QueryString("p9")
            strCargo = Request.QueryString("p10")
            strNombreCliente = Request.QueryString("p11")
            strRegistros = Request.QueryString("p12")
            Dim dtDocumento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCheckListNTx.ContratoDocumentoGet(strNumeroContrato))
            Dim strCodDocumentos As String()

            strCodDocumentos = Split(strRegistros, "|")

            If dtDocumento.Rows.Count > 0 Then
                For i As Integer = 0 To dtDocumento.Rows.Count - 1
                    For j As Integer = 0 To strCodDocumentos.Length - 1
                        If dtDocumento.Rows.Item(i).Item("CodigoContratoDocumento").ToString().Trim() = strCodDocumentos(j).Trim() Then
                            ' Inicio IBK - AAE- 03/10/2012 - Cargo la descripción larga
                            'strListaDoc = strListaDoc + " - " + dtDocumento.Rows.Item(i).Item("Descripcion").ToString() + "|"
                            strListaDoc = strListaDoc + " - " + dtDocumento.Rows.Item(i).Item("DescripcionLarga").ToString() + "|"
                            ' fin IBK - AAE
                        End If
                    Next
                Next
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
    Private Sub ObtenerListaDocumentos()

    End Sub
    Private Sub Guardar()
        Dim oEContrato As New ESolicitudcredito
        Dim oEContacto As New EGcc_contacto
        Dim oLwsDocClienteTx As New LDocClienteTx
        Try
            Dim strEContrato As String = ""
            If strFlagFechaTerminoRecepCli = "1" Then
                With oEContrato
                    .FechaTerminoRecepDocumentoClie = GCCUtilitario.StringToDateTime(strFechaTerminoRecepCli)
                    .FlagTerminoRecepDocumentoClie = strFlagFechaTerminoRecepCli
                End With
                strEContrato = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(oEContrato)
            End If

            With oEContacto
                .Codigocontacto = GCCUtilitario.StringToInteger(strCodigoContacto)
                .Codigocotizacion = strCodigocotizacion
                .Codsolicitudcredito = strNumeroContrato
                .Nombre = GCCUtilitario.NullableString(strNombre)
                .Correo = GCCUtilitario.NullableString(strCorreo)
                .Telefono = GCCUtilitario.NullableString(strTelefono)
                .Anexo = GCCUtilitario.NullableString(strAnexo)
                .CodigoCargo = GCCUtilitario.NullableString(strCargo)
            End With

            Dim blnResult As Boolean = oLwsDocClienteTx.GuardarVerificacionCliente( _
                                                strEContrato, _
                                                GCCUtilitario.SerializeObject(Of EGcc_contacto)(oEContacto), _
                                                IIf(String.IsNullOrEmpty(strCodigoContacto), "N", "M"))

            blnResult = EnviaCarta()
            If Not blnResult Then
                Throw New Exception("Error al intentar de cambiar de estado. Vuelva a intentarlo.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteTx = Nothing
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim mbool As Boolean = EnviarMail(strCodigocotizacion, strCorreo, "", "MailSolicitudCliente", strNumeroContrato, strListaDoc, strNombreCliente, "", "", "", "", "", "", "")
            If mbool Then
                Guardar()
            Else
                Throw New Exception("No se logró enviar el correo. Inténtelo mas tarde.")
            End If

            Page.ClientScript.RegisterStartupScript(Me.GetType, "Retornar", "fnRetornar('" & strNumeroContrato & "');", True)
        Catch ex As Exception
            GCCUtilitario.Show(ex.Message, Me)
        End Try
    End Sub

    Private Function EnviaCarta() As Boolean
        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim oLwsDocClienteTx As New LDocClienteTx
        Try
            With oEContratoDoc
                .Observaciones = strRegistros
                .Numerocontrato = strNumeroContrato.Trim.PadLeft(8, "0"c)
                .Audusuarioregistro = GCCSession.CodigoUsuario
            End With

            Dim blnResult As Boolean = oLwsDocClienteTx.EnviarCartaDocumentoCliente(GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(oEContratoDoc))
            Return blnResult
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteTx = Nothing
        End Try

    End Function
End Class
