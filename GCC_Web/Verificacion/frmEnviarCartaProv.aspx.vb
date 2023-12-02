Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data

Partial Class Verificacion_frmEnviarCartaProv
    Inherits GCCBase

    Dim pstrCodContrato As String
    Dim pstrRegistros As String
    Dim pstrNombreCliente As String
    Dim pstrNombreProveedor As String
    Dim objLog As New GCCLog("frmEnviarCartaProv.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            pstrCodContrato = Request.QueryString("p1")
            pstrRegistros = Request.QueryString("p2")
            pstrNombreCliente = Request.QueryString("p3")
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

    Private Sub EnviaCarta()

        Dim pEcontratoproveedor As String
        Dim oEContratoProv As New EGcc_contratoproveedor
        Dim oLwsDocProveedorTx As New LProveedorTx
        Try
            Dim strRegistrosSplit As String() = pstrRegistros.Split(New Char() {"|"c})

            Dim objContratoProveedorList As New ListEContratoProveedor

            If strRegistrosSplit.Length > 0 Then

                Dim objContratoProveedorNTx As New LProveedorNTx
                Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoProveedorNTx.ListadoContratoProveedor(pstrCodContrato))
                Dim dwProveedor As New DataView(dtProveedor)
                Dim oGCCAnexo As New GCC_Anexo

                Dim sbProveedor As New StringBuilder
                For i As Integer = LBound(strRegistrosSplit) To UBound(strRegistrosSplit)
                    oEContratoProv = New EGcc_contratoproveedor

                    oEContratoProv.Codigocontratoproveedor = GCCUtilitario.CheckInt(strRegistrosSplit(i))
                    oEContratoProv.Numerocontrato = pstrCodContrato



                    If oEContratoProv.Codigocontratoproveedor.Value > 0 Then
                        objContratoProveedorList.Add(oEContratoProv)

                        dwProveedor.RowFilter = GCCUtilitario.Concatenar("CodigoContratoProveedor=", oEContratoProv.Codigocontratoproveedor.Value.ToString())
                        If dwProveedor.Count = 1 Then
                            With dwProveedor
                                sbProveedor.Append(GCCUtilitario.Concatenar(oGCCAnexo.CartaProveedor(dwProveedor, pstrCodContrato), "*", dwProveedor(0).Item("Correo").ToString(), "*", "%", dwProveedor(0).Item("NombreInstitucion").ToString(), ";"))
                            End With
                        End If
                    End If
                Next

                Dim Proveedores As String() = sbProveedor.ToString.TrimEnd(";").Split(";")
                Dim ruta As String = ""
                For x As Int32 = 0 To Proveedores.Length - 1
                    ruta = String.Concat(GCCUtilitario.fstrObtieneKeyWebConfig("FileServer"), Proveedores(x).Split("*")(0))
                    Dim mbool As Boolean = EnviarMail("", Proveedores(x).Split("*")(1), ruta, "MailSolicitudProveedor", pstrCodContrato, "", pstrNombreCliente, Proveedores(x).Split("%")(1), "", "", "", "", "", "")
                Next

                pEcontratoproveedor = GCCUtilitario.SerializeObject(objContratoProveedorList)
                Dim blnResult As Boolean = oLwsDocProveedorTx.EnviarCartaDocumentoProveedor(pEcontratoproveedor)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocProveedorTx = Nothing
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            EnviaCarta
            Page.ClientScript.RegisterStartupScript(Me.GetType, "Retornar", "fnRetornar('" & pstrCodContrato & "');", True)
        Catch ex As Exception
            GCCUtilitario.Show(ex.Message, Me)
        End Try
    End Sub

End Class
