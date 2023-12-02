Imports System.Data
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Web.Services
Imports System.Collections.Generic
Partial Class GestionBien_OtrosConceptos_frmGastoRegistro
    Inherits GCCBase

    Dim mstrNroContrato As String
    Dim objLog As New GCCLog("frmGastoRegistro.aspx.vb")

#Region "   Eventos     "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not IsPostBack Then

                'Fecha Hoy
                Dim dtFecha As Date = Now
                Me.hddFechaActual.Value = dtFecha.ToString("dd/MM/yyyy")

                pInicializarControles()
                'Eventos
                txtNumeroTipo.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_NumeroTipo1();return false;}} else {return true}; ")


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
#End Region

#Region "   Metodos     "
    Private Sub pInicializarControles()
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Try
            Dim odtbParam As New DataTable
            Dim dv As New DataView
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO))
            dv = odtbParam.DefaultView
            dv.RowFilter = "CODIGO IN (" & GCCConstante.eTipoDocumento.DNI & "," _
                                         & GCCConstante.eTipoDocumento.RUC & "," _
                                         & GCCConstante.eTipoDocumento.Otros & ")"
            GCCUtilitario.pCargarHtmlSelect(cmdTipoDoc, dv.ToTable, "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")

            GCCUtilitario.CargarComboMoneda(cmbMoneda)
            If hidCodMoneda.Value <> String.Empty Then cmbMoneda.Value = hidCodMoneda.Value


            txtNroDocProveed.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {fn_buscarProveedor();return false;}} else {return true}; ")
            txtNumeroTipo.Attributes.Add("onblur", "javascript:fn_NumeroTipo1();")
            txtTipoBien.Attributes.Add("onblur", "javascript:fn_TipoBien();")
            txtNumeroDoc1.Attributes.Add("onblur", "javascript:fn_SetearTipoComprobante();")
        Catch ex As Exception
            Throw ex
        Finally            
            oLwsMantenimiento = Nothing
        End Try
    End Sub

    Private Shared Function ObtenerTipoCambio(ByVal strMonedaBusq As String, _
                                               ByVal strFecha As String, _
                                               ByVal strTipoModalidaCambio As String) As String
        Dim oLwsTipoCambioNtx As New LUtilNTX
        Dim odtbDatos As New DataTable
        Dim strResult As String = ""
        Try
            odtbDatos = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTipoCambioNtx.ObtenerTipoCambio(strMonedaBusq, strTipoModalidaCambio, strFecha))
            If odtbDatos.Rows.Count = 0 Then
                strResult = String.Concat("0$0")
            Else
                strResult = String.Concat(odtbDatos.Rows(0).Item("MontoValorVenta").ToString, "$", odtbDatos.Rows(0).Item("MontoValorCompra").ToString)
            End If
            odtbDatos = Nothing

            Return strResult
        Catch ex As Exception
            Return "0"
        Finally
            oLwsTipoCambioNtx = Nothing
        End Try
    End Function
#End Region

#Region "   WebMethods  "

    <WebMethod()> _
    Public Shared Function ConsultarTipoCambio(ByVal pstrCodMoneda As String, _
                                           ByVal pstrFecha As String, _
                                           ByVal pstrModalidad As String) As String
        Dim strResult As String = ""
        Try
            Dim TipoCambio As String = ObtenerTipoCambio(IIf(pstrCodMoneda = GCCConstante.C_COD_MONEDA_SOLES, GCCConstante.C_COD_MONEDA_DOLARES, pstrCodMoneda), _
                                    pstrFecha, pstrModalidad)

            Return "0|" & TipoCambio
        Catch ex As Exception
            strResult = "1|" & ex.Message
        End Try
        Return strResult
    End Function

    <WebMethod()> _
    Public Shared Function AgenteRetencion(ByVal pNroDocumento As String) As Integer
        Dim oLwsDesembolsoNTx As New LDesembolsoNTx
        Dim intAgente As Integer = 0
        Try
            intAgente = oLwsDesembolsoNTx.ObtenerAgenteRetencion(pNroDocumento)

            Return intAgente
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDesembolsoNTx = Nothing
        End Try
    End Function

#End Region

End Class
