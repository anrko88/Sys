
Imports GCC.UI
Imports System.Data
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Formalizacion_frmGenerarCartaContrato
    Inherits GCCBase

    Dim mstrCodigoContrato As String
    Dim mstrCorreoCliente As String
    Dim mstrRutaCarta As String
    Dim mstrNombreCliente As String
    Dim StrListaBienes As String
    Dim objLog As New GCCLog("frmGenerarCartaContrato.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            mstrCodigoContrato = Request.QueryString("cc")
            mstrCorreoCliente = Request.QueryString("mail")
            mstrRutaCarta = Request.QueryString("rutacarta")
            mstrNombreCliente = Request.QueryString("nombreCliente")

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

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            'Dim strRutaCarta As String = String.Concat(GCCUtilitario.fstrObtieneKeyWebConfig("FileServer"), fObtenerCarta())

            EnviaCartaContrato(mstrCodigoContrato, mstrCorreoCliente, mstrRutaCarta)
            Page.ClientScript.RegisterStartupScript(Me.GetType, "Retornar", "fnRetornar('" & mstrCodigoContrato & "');", True)
        Catch ex As Exception
            GCCUtilitario.Show(ex.Message, Me)
        End Try
    End Sub

    Private Function fObtenerCarta() As String
        Dim oGCC_Anexo As New GCC_Anexo
        Dim strNameFile As String = oGCC_Anexo.CartaCotizacionLeasing(mstrCodigoContrato)
        Return strNameFile
    End Function

    'Private Function EnviaCarta(ByVal pstrCodigoCotizacion As String, _
    '                            ByVal pstrCorreo As String, _
    '                            ByVal pstrRutaDoc As String) As Boolean

    '    Dim objEGcc_cotizacion As New EGcc_cotizacion
    '    Dim objLCotizacionTx As New LCotizacionTx
    '    Dim strEGcc_cotizacion As String

    '    Try
    '        Dim mbool As Boolean = EnviarMail(pstrCodigoCotizacion, pstrCorreo, pstrRutaDoc, "MailGenerarCartaCotizacion")

    '        'If mbool Then
    '        With objEGcc_cotizacion
    '            .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
    '            .Audestadologico = 1
    '            .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
    '            .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
    '        End With

    '        strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
    '        mbool = objLCotizacionTx.ModificarCotizacionCarta(strEGcc_cotizacion)
    '        If Not mbool Then
    '            Throw New Exception("Error al Intentar de Cambiar de Estado. Vuelva a Intentarlo.")
    '        End If
    '        'Else
    '        'Throw New Exception("No se logro generar la Carta con el envio de mail.\n Intentelo mas tarde.")
    '        'End If

    '        Return mbool
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Private Function EnviaCartaContrato(ByVal pstrCodigoContrato As String, _
                                        ByVal pstrCorreo As String, _
                                        ByVal pstrRutaDoc As String) As Boolean

        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim objEGCC_Carta As New EGcc_carta
        Dim pSolicitudCredito As String
        Dim pEGCC_Carta As String

        'ListadoBienes

        StrListaBienes = ListarBienesDescripcion(pstrCodigoContrato)

        'Enviar Carta
        Dim strRutaServidor As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
        Dim mbool As Boolean = EnviarMail(pstrCodigoContrato, pstrCorreo, strRutaServidor + pstrRutaDoc, "MailGenerarCartaContrato", "", "", mstrNombreCliente, StrListaBienes, "", "", "", "", "", "")

        objESolicitudCredito.Codsolicitudcredito = pstrCodigoContrato
        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ENVIADOCLIENTE
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        objEGCC_Carta.Codsolicitudcredito = pstrCodigoContrato
        objEGCC_Carta.Usuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        objEGCC_Carta.Numerocarta = Nothing

        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
        pEGCC_Carta = GCCUtilitario.SerializeObject(objEGCC_Carta)

        Dim blnResult As Boolean = objContratoTx.EnviarCarta(pSolicitudCredito, _
                                                             pEGCC_Carta)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If

        'Dim objEGcc_cotizacion As New EGcc_cotizacion
        'Dim objLCotizacionTx As New LCotizacionTx
        'Dim strEGcc_cotizacion As String

        'Try
        '    Dim mbool As Boolean = EnviarMail(pstrCodigoCotizacion, pstrCorreo, pstrRutaDoc, "MailGenerarCartaContrato")

        '    'If mbool Then
        '    With objEGcc_cotizacion
        '        .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
        '        .Audestadologico = 1
        '        .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        '        .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        '    End With

        '    strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
        '    mbool = objLCotizacionTx.ModificarCotizacionCarta(strEGcc_cotizacion)
        '    If Not mbool Then
        '        Throw New Exception("Error al Intentar de Cambiar de Estado. Vuelva a Intentarlo.")
        '    End If
        '    'Else
        '    'Throw New Exception("No se logro generar la Carta con el envio de mail.\n Intentelo mas tarde.")
        '    'End If

        '    Return mbool
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Function

    Public Function ListarBienesDescripcion(ByVal pContrato As String) As String
        Dim objContratoNTx As New LContratoNTx
        Dim sListaBienes As New StringBuilder

        Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                              1, _
                                                                                                              "SecFinanciamiento", _
                                                                                                              "desc", _
                                                                                                              pContrato, _
                                                                                                              Nothing))

        For Each oRow As DataRow In dtBienes.Rows
            If Not oRow.Item("Comentario") Is DBNull.Value Then
                StrListaBienes = StrListaBienes + " - " + oRow.Item("Comentario").ToString() + "|"
            End If
        Next oRow

        Return StrListaBienes.ToString
    End Function

End Class


