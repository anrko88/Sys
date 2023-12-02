Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmInscripcionMunicipalDetalle
    Inherits GCCBase

    Dim objLog As New GCCLog("frmInscripcionMunicipalDetalle.aspx.vb")

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
                hddCodigoInafectacion.Value = Request.QueryString("codigo")
                If hddCodigoInafectacion.Value <> "" Then
                    hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR
                    txtPartidaRegistral.Value = Request.QueryString("partida").Trim()
                    txtAsientoRegitral.Value = Request.QueryString("asiento").Trim()
                    txtActo.Value = Request.QueryString("acto").Trim()
                    hddEstado.Value = Request.QueryString("estado").Trim()
                    hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR
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

    <WebMethod()> _
    Public Shared Function GuardarInscripcionMunicipal(ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pPartida As String, _
                                         ByVal pAsiento As String, _
                                         ByVal pActo As String, _
                                         ByVal pEstado As String)

        Try
            Dim objESolicitudCreditoCarc As New ESolicitudcreditoestructuracarac
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoCarc As String

            '.FechaProbableFinObra = IIf(IsNothing(pFechaProbableFinObra), "", CDate(GCCUtilitario.StringToDateTime(pFechaProbableFinObra)).ToString("yyyy-MM-dd")).ToString()
            If objESolicitudCreditoCarc IsNot Nothing Then
                With objESolicitudCreditoCarc
                    .Codsolicitudcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .PartidaRegistral = pPartida
                    .AsientoRegistral = pAsiento
                    .Acto = pActo
                    .EstadoInscripcion = pEstado
                End With
            End If
            pESolicitudCreditoCarc = GCCUtilitario.SerializeObject(objESolicitudCreditoCarc)

            Dim blnResult As Boolean = objLBien.fblnInsertarInscripcionMunicipal(pESolicitudCreditoCarc)

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
    Public Shared Function ModificarInscripcionMunicipal(ByVal pCodigoInscripcion As String, _
                                         ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pPartida As String, _
                                         ByVal pAsiento As String, _
                                         ByVal pActo As String, _
                                         ByVal pEstado As String)

        Try
            Dim objESolicitudCreditoCarc As New ESolicitudcreditoestructuracarac
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoCarc As String

            If objESolicitudCreditoCarc IsNot Nothing Then
                With objESolicitudCreditoCarc
                    .CodigoInscripcion = pCodigoInscripcion
                    .Codsolicitudcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .PartidaRegistral = pPartida
                    .AsientoRegistral = pAsiento
                    .Acto = pActo
                    .EstadoInscripcion = pEstado
                End With
            End If
            pESolicitudCreditoCarc = GCCUtilitario.SerializeObject(objESolicitudCreditoCarc)

            Dim blnResult As Boolean = objLBien.fblnModificarInscripcionMunicipal(pESolicitudCreditoCarc)

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
    Public Shared Function ValidarDatosPartida(ByVal strNroContrato As String, _
                                         ByVal strPartida As String, _
                                         ByVal strAsiento As String, _
                                         ByVal strActo As String, _
                                         ByVal strTipo As String, _
                                         ByVal intCodigoInscripcion As String)

        Try
            Dim objESolicitudCreditoEstructuraCarc As New ESolicitudcreditoestructuracarac
            Dim objLBienNTX As New LBienNTx
            Dim pESolicitudCreditoEstructura As String

            If objESolicitudCreditoEstructuraCarc IsNot Nothing Then
                With objESolicitudCreditoEstructuraCarc
                    .Codsolicitudcredito = strNroContrato.ToString()
                    .PartidaRegistral = strPartida.ToString()
                    .AsientoRegistral = strAsiento.ToString()
                    .Acto = strActo.ToString()
                    .CodigoInscripcion = IIf(intCodigoInscripcion = "", "0", intCodigoInscripcion).ToString()
                End With
            End If
            pESolicitudCreditoEstructura = GCCUtilitario.SerializeObject(objESolicitudCreditoEstructuraCarc)

            Dim dtValidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLBienNTX.ValidarDatosPartida(pESolicitudCreditoEstructura, strTipo))
            Dim intContPartida As Integer
            Dim intContAsiento As Integer
            Dim intContActo As Integer

            intContPartida = Convert.ToInt32(dtValidacion.Rows(0)("cantidadPartida"))
            intContAsiento = Convert.ToInt32(dtValidacion.Rows(0)("cantidadAsiento"))
            intContActo = Convert.ToInt32(dtValidacion.Rows(0)("cantidadActo"))

            If intContPartida > 0 Then
                Return "Partida"
            ElseIf intContAsiento > 0 Then
                Return "Asiento"
            ElseIf intContActo > 0 Then
                Return "Acto"
            Else
                Return ""
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
End Class
