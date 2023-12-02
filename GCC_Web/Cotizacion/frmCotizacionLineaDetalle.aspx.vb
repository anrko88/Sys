Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Cotizacion_frmCotizacionLineaDetalle
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCotizacionLineaDetalle.aspx.vb")

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Dim strLinea As String = Request.QueryString("hddLinea")
                Dim objLWebService As New LWebService
                Dim strLODet As String = objLWebService.fObtenerDatosLineaOP(strLinea.Trim)

                'Detalle Linea
                If Not strLODet Is Nothing Then
                    If Not strLODet.Trim().Equals("") Then
                        Dim odtbLODet As DataTable = GCCUtilitario.DeserializeObject2(Of DataTable)(strLODet)
                        If Not odtbLODet Is Nothing Then
                            If odtbLODet.Rows.Count > 0 Then
                                For Each oRow As DataRow In odtbLODet.Rows
                                    Me.txtMontoDisponible.Value = oRow.Item("SALDOANTESOPERACION").ToString()
                                    Me.txtMontoAprobado.Value = oRow.Item("MONTOAPROBADO").ToString()
                                    Me.txtMontoUtilizado.Value = oRow.Item("SALDORESERVADO").ToString()
                                    Me.txtEstado.Value = oRow.Item("ESTADO").ToString()
                                    'Me.txtFechaAprobacion.Value = oRow.Item("").ToString()
                                    Dim strFechaVenc As String = oRow.Item("FECHAVENCIMIENTO").ToString()
                                    Me.txtFechaVence.Value = strFechaVenc.Substring(0, 10)
                                Next oRow
                            End If
                        End If
                    End If
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

End Class
