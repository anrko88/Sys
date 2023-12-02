Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Consultas_frmConsultaInafectacion
    Inherits GCCBase

    Dim objLog As New GCCLog("frmConsultaInafectacion.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                hddCodigoContrato.Value = Request.QueryString("NumContrato")
                hddCodigoBien.Value = Request.QueryString("CodigoBien")
                hddAnioFabricacion.Value = Request.QueryString("AnioFabricacion")

                'GCCUtilitario.CargarComboValorGenerico(ddlEstado, GCCConstante.C_TABLAGENERICA_Estado_Resolucion)
                hddCodigoInafectacion.Value = Request.QueryString("codigo")
                If hddCodigoInafectacion.Value <> "" Then
                    hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR
                    txtPeriodo.InnerText = Request.QueryString("periodo")
                    txtFecEnvioCarta.InnerText = Request.QueryString("fecEnvio")
                    txtFecPresentacionSat.InnerText = Request.QueryString("fecPre")
                    txtFecRecepDocumentos.InnerText = Request.QueryString("fecRec")
                    txtFecNotificacion.InnerText = Request.QueryString("fecNot")
                    txtNroResolucion.InnerText = Request.QueryString("nrores").Trim()
                    ddlEstado.InnerText = Request.QueryString("codestadores").Trim()
                    hddEstado.Value = Request.QueryString("estado").Trim()
                    hddAnioFabricacion.Value = Request.QueryString("AnioFabricacion").Trim()
                Else
                    hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO
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
