Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmMantBienInafectacion
    Inherits GCCBase

    Dim objLog As New GCCLog("frmMantBienInafectacion.aspx.vb")

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

                GCCUtilitario.CargarComboValorGenerico(ddlEstado, GCCConstante.C_TABLAGENERICA_Estado_Resolucion)
                hddCodigoInafectacion.Value = Request.QueryString("codigo")
                If hddCodigoInafectacion.Value <> "" Then
                    hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR
                    txtPeriodo.Value = Request.QueryString("periodo")
                    txtFecEnvioCarta.Value = Request.QueryString("fecEnvio")
                    txtFecPresentacionSat.Value = Request.QueryString("fecPre")
                    txtFecRecepDocumentos.Value = Request.QueryString("fecRec")
                    txtFecNotificacion.Value = Request.QueryString("fecNot")
                    txtNroResolucion.Value = Request.QueryString("nrores").Trim()
                    ddlEstado.Value = Request.QueryString("codestadores").Trim()
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

    <WebMethod()> _
    Public Shared Function GuardarInafectacion(ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pPeriodo As String, _
                                         ByVal pFechaEnvioCarta As String, _
                                         ByVal pFechaRecepcionDocumentos As String, _
                                         ByVal pFechaPresentacionSat As String, _
                                         ByVal pFechaNotificacion As String, _
                                         ByVal pNroResolucion As String, _
                                         ByVal pEstadoResolucion As String, _
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
                    .Periodo = pPeriodo
                    .FecEnvioCarta = GCCUtilitario.StringToDateTime(pFechaEnvioCarta)
                    .FecRecepcionDocumento = GCCUtilitario.StringToDateTime(pFechaRecepcionDocumentos)
                    .FecPresentacionSat = GCCUtilitario.StringToDateTime(pFechaPresentacionSat)
                    .FechaNotificacion = GCCUtilitario.StringToDateTime(pFechaNotificacion)
                    .NroResolucion = pNroResolucion
                    .EstadoResolucion = pEstadoResolucion
                    .EstadoInafectacion = pEstado
                End With
            End If
            pESolicitudCreditoCarc = GCCUtilitario.SerializeObject(objESolicitudCreditoCarc)

            Dim blnResult As Boolean = objLBien.fblnInsertarInafectacion(pESolicitudCreditoCarc)

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
    Public Shared Function ModificarInafectacion(ByVal pCodigoInafectacion As String, _
                                                 ByVal pNumeroContrato As String, _
                                         ByVal pCodigoBien As String, _
                                         ByVal pPeriodo As String, _
                                         ByVal pFechaEnvioCarta As String, _
                                         ByVal pFechaRecepcionDocumentos As String, _
                                         ByVal pFechaPresentacionSat As String, _
                                          ByVal pFechaNotificacion As String, _
                                         ByVal pNroResolucion As String, _
                                         ByVal pEstadoResolucion As String, _
                                         ByVal pEstado As String)

        Try
            Dim objESolicitudCreditoCarc As New ESolicitudcreditoestructuracarac
            Dim objLBien As New LBienTx
            Dim pESolicitudCreditoCarc As String

            '.FechaProbableFinObra = IIf(IsNothing(pFechaProbableFinObra), "", CDate(GCCUtilitario.StringToDateTime(pFechaProbableFinObra)).ToString("yyyy-MM-dd")).ToString()
            If objESolicitudCreditoCarc IsNot Nothing Then
                With objESolicitudCreditoCarc
                    .CodInafectacion = pCodigoInafectacion
                    .Codsolicitudcredito = pNumeroContrato
                    .Secfinanciamiento = pCodigoBien
                    .Periodo = pPeriodo
                    .FecEnvioCarta = GCCUtilitario.StringToDateTime(pFechaEnvioCarta)
                    .FecRecepcionDocumento = GCCUtilitario.StringToDateTime(pFechaRecepcionDocumentos)
                    .FecPresentacionSat = GCCUtilitario.StringToDateTime(pFechaPresentacionSat)
                    .FechaNotificacion = GCCUtilitario.StringToDateTime(pFechaNotificacion)
                    .NroResolucion = pNroResolucion
                    .EstadoResolucion = pEstadoResolucion
                    .EstadoInafectacion = pEstado
                End With
            End If
            pESolicitudCreditoCarc = GCCUtilitario.SerializeObject(objESolicitudCreditoCarc)

            Dim blnResult As Boolean = objLBien.fblnModificarInafectacion(pESolicitudCreditoCarc)

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
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
End Class
