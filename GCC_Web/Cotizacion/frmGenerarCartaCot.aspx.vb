Imports GCC.UI
Imports System.Data
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Cotizacion_frmGenerarCartaCot
    Inherits GCCBase

    Dim objLog As New GCCLog("frmGenerarCartaCot.aspx.vb")
    Dim mstrCodigoCotizacion As String
    Dim mstrCorreoCliente As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            mstrCodigoCotizacion = Request.QueryString("cc")
            mstrCorreoCliente = Request.QueryString("mail")

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
            Dim strRutaCarta As String = String.Concat(GCCUtilitario.fstrObtieneKeyWebConfig("FileServer"), fObtenerCarta())
            EnviaCarta(mstrCodigoCotizacion, mstrCorreoCliente, strRutaCarta)
            Page.ClientScript.RegisterStartupScript(Me.GetType, "Retornar", "fnRetornar('" & mstrCodigoCotizacion & "');", True)
        Catch ex As Exception
            GCCUtilitario.Show(ex.Message, Me)
        End Try
    End Sub

    Private Function fObtenerCarta() As String
        Dim oGCC_Anexo As New GCC_Anexo
        Dim strNameFile As String = oGCC_Anexo.CartaCotizacionLeasing(mstrCodigoCotizacion)
        Return strNameFile
    End Function

    Private Function EnviaCarta(ByVal pstrCodigoCotizacion As String, _
                                ByVal pstrCorreo As String, _
                                ByVal pstrRutaDoc As String) As Boolean

        Dim objEGcc_cotizacion As New EGcc_cotizacion
        Dim objLCotizacionTx As New LCotizacionTx
        Dim objCotizacionNTx As New LCotizacionNTx
        Dim strEGcc_cotizacion As String
        With objEGcc_cotizacion
            .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
        End With
        strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
        Try
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim strNombreCliente As String = dtCotizacion.Rows(0).Item("NombreCliente").ToString

            Dim mbool As Boolean = EnviarMail(pstrCodigoCotizacion, pstrCorreo, pstrRutaDoc, "MailGenerarCartaCotizacion", "", "", strNombreCliente, "", "", "", "", "", "", "")

            'If mbool Then
            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
                .Audestadologico = 1
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                'GCCTS_JRC_20120304-Seguimiento Datos acionales
                .CodigoUsuario = GCCSession.CodigoUsuario.ToString()
                .NombreUsuario = GCCSession.NombreUsuario.ToString()
                .PerfilUsuario = GCCSession.DescripcionPerfil.ToString()

            End With

            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            mbool = objLCotizacionTx.ModificarCotizacionCarta(strEGcc_cotizacion)
            If Not mbool Then
                Throw New Exception("Error al Intentar de Cambiar de Estado. Vuelva a Intentarlo.")
            End If
            'Else
            'Throw New Exception("No se logro generar la Carta con el envio de mail.\n Intentelo mas tarde.")
            'End If

            Return mbool
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
