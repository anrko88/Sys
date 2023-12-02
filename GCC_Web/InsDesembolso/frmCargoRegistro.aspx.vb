Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class InsDesembolso_frmCargoRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCargoRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                Dim strNroContrato As String = Request.QueryString("cc")
                Dim strCodInsDesembolso As String = Request.QueryString("cid")
                Dim strCodMonedaContrato As String = Request.QueryString("mco")
                Dim strMontoTotal As String = Request.QueryString("mto")
                Dim strCodCotizacion As String = Request.QueryString("cct")

                hddCodigoContrato.Value = strNroContrato
                hddCodigoInsDesembolso.Value = strCodInsDesembolso
                hddCodMonedaContrato.Value = strCodMonedaContrato
                hddMontoTotal.Value = GCCUtilitario.StringToDecimal(strMontoTotal)

                'Obtiene Datos Cotizacion
                ObtenerCotizacion(strCodCotizacion)

                'Combos
                GCCUtilitario.CargarComboMoneda(cmbMoneda)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbConcepto, GCCConstante.C_TABLAGENERICA_Tipo_IDAgrupacion)

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

#Region "WebMethods"

    ''' <summary>
    ''' Graba Cargo
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaCargo(ByVal pstrCodContrato As String, _
                                      ByVal pstrCodInsDesembolso As String, _
                                      ByVal pstrCodConceptoCargo As String, _
                                      ByVal pstrCodMoneda As String, _
                                      ByVal pstrCodMonto As String, _
                                      ByVal pstrPorcMonto As String) As String

        Try
            Dim objInsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion

            If pstrPorcMonto Is Nothing Then
                pstrPorcMonto = "0"
            End If
            If pstrPorcMonto.Trim.Equals("") Then
                pstrPorcMonto = "0"
            End If

            With objInsDesembolsoAgrupacion
                .Codagrupacion = GCCUtilitario.NullableString(pstrCodConceptoCargo)
                .Codproveedor = ""
                '.CodConceptoCargo = GCCUtilitario.NullableString(pstrCodConceptoCargo)
                .Codmonedadocumento = GCCUtilitario.NullableString(pstrCodMoneda)
                .Codmonedapago = GCCUtilitario.NullableString(pstrCodMoneda)
                .Codtipooperacion = GCCConstante.C_COD_TIPOOPERACION_ID_CARGO
                .Numerodocumento = ""
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrCodInsDesembolso)
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Montoretencion = 0
                .Montodetraccion = 0
                .Monto4ta = 0
                .Montototalpago = GCCUtilitario.StringToDecimal(pstrCodMonto)
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .PorcCalculo = GCCUtilitario.StringToDecimal(pstrPorcMonto)
            End With

            Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
            objLInstruccionDesembolsoTx.InsertarInsDesembolsoAgrupacion(GCCUtilitario.SerializeObject(objInsDesembolsoAgrupacion))

            'Actualiza Listado
            ListaCargoAbono(pstrCodContrato, pstrCodInsDesembolso)

            Return "0"

        Catch ex As Exception
            Return "1"
        End Try

    End Function

#End Region

#Region "Métodos"

    Public Shared Sub ListaCargoAbono(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String)

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strEGCC_InsDesembolsoAgrupacion As String
            With objEGCC_InsDesembolsoAgrupacion
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoAgrupacion)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono(strEGCC_InsDesembolsoAgrupacion))
            HttpContext.Current.Session("DTB_LISTACARGOABONO") = dtInstruccionDesembolso

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ObtenerCotizacion(ByVal pstrCodCotizacion As String)

        Try
            Dim objCotizacionNTx As New LCotizacionNTx
            Dim msgError As String = ""
            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String
            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))

            'Valida si existe
            If dtCotizacion.Rows.Count > 0 Then

                hddPorcComActivacion.Value = dtCotizacion.Rows(0).Item("Comisionactivacionporc").ToString.Trim
                hddPorcComEstructuracion.Value = dtCotizacion.Rows(0).Item("Comisionestructuracionporc").ToString.Trim
                hddPorcCuotaInicial.Value = dtCotizacion.Rows(0).Item("Cuotainicialporc").ToString

                hddMontoComActivacion.Value = dtCotizacion.Rows(0).Item("Importecomisionactivacion").ToString.Trim
                hddMontoComEstructuracion.Value = dtCotizacion.Rows(0).Item("Importecomisionestructuracion").ToString.Trim
                hddMontoCuotaInicial.Value = dtCotizacion.Rows(0).Item("Importecuotainicial").ToString.Trim

                'Inicio IBK - AAE
                hddRdCuotaInicial.Value = dtCotizacion.Rows(0).Item("FlagCuotaInicial").ToString.Trim
                hddRdComActivacion.Value = dtCotizacion.Rows(0).Item("FlagComisionActivacion").ToString.Trim
                hddRdComEstructuracion.Value = dtCotizacion.Rows(0).Item("FlagComisionEstructuracion").ToString.Trim
                'Fin IBK - AAE

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
