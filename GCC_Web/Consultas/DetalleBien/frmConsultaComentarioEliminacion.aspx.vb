Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_frmConsultaComentarioEliminacion
    'Inherits System.Web.UI.Page
    Inherits GCCBase

    Dim objLog As New GCCLog("frmConsultaComentarioEliminacion.aspx.vb")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
            'pInicializarControles()

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Dim strCodigoContrato As String = Request.QueryString("ccf")
                Dim strSecFinanciamiento As String = Request.QueryString("csf")
                Dim Opcion As String = Request.QueryString("Add")
                Me.hddCodigoContrato.Value = strCodigoContrato
                Me.hddCodigoSecFinanciamiento.Value = strSecFinanciamiento
                hidOpcion.Value = Opcion
                If Opcion <> "" Then
                    InicializarCampos()
                Else
                    Me.txtFecha.InnerText = Now.ToString("dd/MM/yyyy")
                    HidFecha.Value = Me.txtFecha.InnerText
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
    Private Sub InicializarCampos()
        Dim oLwsDocBienNtx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraInmueble As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraMaquinaria As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraVehiculo As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraSistema As New ESolicitudcreditoestructura

        oESolicitudCreditoEstructura = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosBienContrato(hddCodigoContrato.Value))
        If oESolicitudCreditoEstructura IsNot Nothing Then
            With oESolicitudCreditoEstructura
                hidCodClasificacion.Value = .Tiporubrofinanciamiento

            End With
        End If

        If (Array.IndexOf(GCCConstante.DestinoCredito_Inmueble, hidCodClasificacion.Value) <> -1) Then
            oESolicitudCreditoEstructuraInmueble = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosInmueble(hddCodigoContrato.Value, Convert.ToInt32(hddCodigoSecFinanciamiento.Value)))
            If oESolicitudCreditoEstructuraInmueble IsNot Nothing Then
                With oESolicitudCreditoEstructuraInmueble
                    txtFecha.InnerText = GCCUtilitario.CheckDateString2(.FechaEliminacion.ToString(), "C")
                    txtComentario.Value = .ComentarioBaja
                End With
            End If
        ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Maquinaria, hidCodClasificacion.Value) <> -1) Then
            oESolicitudCreditoEstructuraMaquinaria = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosMaquinarias(hddCodigoContrato.Value, Convert.ToInt32(hddCodigoSecFinanciamiento.Value)))
            If oESolicitudCreditoEstructuraMaquinaria IsNot Nothing Then
                With oESolicitudCreditoEstructuraMaquinaria
                    txtFecha.InnerText = GCCUtilitario.CheckDateString2(.FechaEliminacion.ToString(), "C")
                    txtComentario.Value = .ComentarioBaja
                End With
            End If
        ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Vehiculo, hidCodClasificacion.Value) <> -1) Then
            oESolicitudCreditoEstructuraVehiculo = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosVehiculo(hddCodigoContrato.Value, Convert.ToInt32(hddCodigoSecFinanciamiento.Value)))
            If oESolicitudCreditoEstructuraVehiculo IsNot Nothing Then
                With oESolicitudCreditoEstructuraVehiculo
                    txtFecha.InnerText = GCCUtilitario.CheckDateString2(.FechaEliminacion.ToString(), "C")
                    txtComentario.Value = .ComentarioBaja
                End With
            End If

        ElseIf (Array.IndexOf(GCCConstante.DestinoCredito_Otros, hidCodClasificacion.Value) <> -1) Then
            oESolicitudCreditoEstructuraSistema = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosSistemas(hddCodigoContrato.Value, Convert.ToInt32(hddCodigoSecFinanciamiento.Value)))
            If oESolicitudCreditoEstructuraSistema IsNot Nothing Then
                With oESolicitudCreditoEstructuraSistema
                    txtFecha.InnerText = GCCUtilitario.CheckDateString2(.FechaEliminacion.ToString(), "C")
                    txtComentario.Value = .ComentarioBaja
                End With
            End If
        End If
    End Sub

 

End Class
