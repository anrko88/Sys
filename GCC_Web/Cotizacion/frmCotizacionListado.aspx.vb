Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Cotizacion_frmCotizacionListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCotizacionListado.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            'Inicio IBK - RPH Obtengo el Nro de Cotizacion al Generar la Carta
            If Not String.IsNullOrEmpty(Request.QueryString("cod")) Then
                hddNroCotizacion.Value = Request.QueryString("cod")
            End If
            'Fin

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_COTIZACION)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionbien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                'ConsultaEjecutivoLeasing()
            End If

            'txtCodCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {fCapturarText(this); document.getElementById('" + btnAgregarGeneral.UniqueID + "').click();return false;}} else {return true}; ")

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

    <WebMethod()> _
    Public Shared Function ListaCotizacion(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pstrNroCotizacion As String, _
                                         ByVal pstrCuCliente As String, _
                                         ByVal pstrRazonSocial As String, _
                                         ByVal pstrEjecutivo As String, _
                                         ByVal pstrFechaIngresoIni As String, _
                                         ByVal pstrFechaIngresoFin As String, _
                                         ByVal pstrClasificacionbien As String, _
                                         ByVal pstrEstado As String) As JQGridJsonResponse

        'Variables
        Dim objCotizacionNTx As New LCotizacionNTx

        Try

            'Valida Campos
            If pstrFechaIngresoIni <> "" Then
                pstrFechaIngresoIni = CDate(pstrFechaIngresoIni).ToString("yyyyMMdd")
            End If
            If pstrFechaIngresoFin <> "" Then
                pstrFechaIngresoFin = CDate(pstrFechaIngresoFin).ToString("yyyyMMdd")
            End If
            Dim strCodUnico As String = GCCUtilitario.NullableString(pstrCuCliente)
            If Not strCodUnico Is Nothing Then
                strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
            End If

            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String
            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrNroCotizacion)
                .CodUnico = strCodUnico
                .NombreCliente = GCCUtilitario.NullableString(pstrRazonSocial)
                .Codigoejecutivoleasing = GCCUtilitario.NullableStringCombo(pstrEjecutivo)
                .FechaInicio = GCCUtilitario.StringToDateTime(pstrFechaIngresoIni)
                .FechaFin = GCCUtilitario.StringToDateTime(pstrFechaIngresoFin)
                .Codigoclasificacionbien = GCCUtilitario.NullableStringCombo(pstrClasificacionbien)
                .Codigoestadocotizacion = GCCUtilitario.NullableStringCombo(pstrEstado)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.ListadoCotizacion(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       GCCUtilitario.SerializeObject(Of EGcc_cotizacion)(objEGcc_cotizacion)))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCotizacion.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtCotizacion.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtCotizacion.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCotizacion)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    '''' <summary>
    '''' Enviar carta
    '''' </summary>    
    '''' <remarks>
    '''' Creado Por         : TSF - JRC
    '''' Fecha de Creación  : 18/05/2012
    '''' </remarks>
    '<WebMethod()> _
    'Public Shared Function EnviaCarta(ByVal pstrCodigoCotizacion As String, _
    '                                  ByVal pstrCorreo As String, _
    '                                  ByVal pstrRutaDoc As String) As String

    '    Dim objEGcc_cotizacion As New EGcc_cotizacion
    '    Dim objLCotizacionTx As New LCotizacionTx
    '    Dim strEGcc_cotizacion As String

    '    Try
    '        EnviarMail(pstrCodigoCotizacion, pstrCorreo, pstrRutaDoc, "MailGenerarCartaCotizacion")




    '        With objEGcc_cotizacion
    '            .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
    '            .Audestadologico = 1
    '            .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
    '            .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
    '        End With

    '        strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
    '        Dim booResultado As Boolean = objLCotizacionTx.ModificarCotizacionCarta(strEGcc_cotizacion)



    '        If booResultado Then
    '            Return "0"
    '        Else
    '            Return "1"
    '        End If

    '    Catch ex As Exception
    '        Return ex.ToString()
    '    End Try

    'End Function

    ''' <summary>
    ''' Aprobar Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function AprobarCotizacion(ByVal pstrCodigoCotizacion As String) As String

        Dim objEGcc_cotizacion As New EGcc_cotizacion
        ' Inicio IBK - AAE - 03/10/2012 - Agrego nueva cotización
        Dim objEGcc_cotizacion2 As New EGcc_cotizacion
        Dim strEGcc_cotizacion2 As String
        ' fin IBK
        Dim objLCotizacionTx As New LCotizacionTx
        Dim objLCotizacionNTx As New LCotizacionNTx
        Dim strEGcc_cotizacion As String

        Try

            Dim strFlujoF10 As String = ConfigurationManager.AppSettings("FlujoF10")

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

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim intFlagLinea As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("FlagLinea"))

            ' Inicio IBK - AAE - 03/10/2012 - Siempre se aprueba la cotización
            ' comento código original

            'If intFlagLinea = 0 And strFlujoF10.Trim().Equals("SI") Then

            '    objEGcc_cotizacion = New EGcc_cotizacion
            '    With objEGcc_cotizacion
            '        .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
            '        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_PENDIENTE_F10
            '        .Audestadologico = 1
            '        .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            '        .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            '    End With
            '    strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

            '    Dim booResultado As Boolean = objLCotizacionTx.ModificarCotizacionEstado(strEGcc_cotizacion)
            '    If booResultado Then
            '        Return "0"
            '    Else
            '        Return "1"
            '    End If

            'Else

            '    Dim booResultado As Boolean = objLCotizacionTx.CotizacionAprobar(strEGcc_cotizacion)
            '    If booResultado Then
            '        Return "0"
            '    Else
            '        Return "1"
            '    End If

            'End If
            ' Siempre apruebo la cotización, si no tengo líneas le cambio el estado a pendiente F10
            Dim booResultado As Boolean = objLCotizacionTx.CotizacionAprobar(strEGcc_cotizacion)
            If booResultado Then
                If intFlagLinea = 0 And strFlujoF10.Trim().Equals("SI") Then
                    objEGcc_cotizacion2 = New EGcc_cotizacion
                    With objEGcc_cotizacion2
                        .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_PENDIENTE_F10
                        .Audestadologico = 1
                        .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    End With
                    strEGcc_cotizacion2 = GCCUtilitario.SerializeObject(objEGcc_cotizacion2)
                    Dim booResultado2 As Boolean = objLCotizacionTx.ModificarCotizacionEstado(strEGcc_cotizacion2)
                    If booResultado2 Then
                        Return "0"
                    Else
                        Return "1"
                    End If
                Else
                    Return "0"
                End If
            Else
                Return "1"
            End If
            ' Fin IBK - AAE

        Catch ex As Exception
            Return ex.ToString()
        End Try

    End Function

    ''' <summary>
    ''' Get Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 20/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function CartaCotizacionLeasing(ByVal pstrCodigoCotizacion As String) As String
        Try
            Dim oGCC_Anexo As New GCC_Anexo
            Dim strNameFile As String = oGCC_Anexo.CartaCotizacionLeasing(pstrCodigoCotizacion)
            Return "0|" + strNameFile
        Catch ex As Exception
            Return "1|" + ex.ToString()
        End Try

    End Function

    ''' <summary>
    ''' Consulta Ejecutivo Leasing
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Public Sub ConsultaEjecutivoLeasing()

        Dim objLWebservice As New LWebService
        Dim strError As String = ""
        Dim strMensaje As String = ""
        Dim strDominioUsuario As String = GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
        Dim strWsUltimusWbc As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsUltimusWBC")
        Dim strNemonicoEjecutivo As String = GCCUtilitario.fstrObtieneKeyWebConfig("NemonicoEN")

        Try

            'Ejecutivos
            Dim sbResultado As New StringBuilder
            Dim objListEUsuarioUltimus As New ListEUsuarioUltimus
            Dim strGrpLogico As String = "WIO_GL_EL"
            objLWebservice.fboolObtenerUsuariosxGrupo(strGrpLogico, objListEUsuarioUltimus, strWsUltimusWbc, strError)
            If objListEUsuarioUltimus IsNot Nothing Then
                cmbEjecutivo.DataSource = objListEUsuarioUltimus
                cmbEjecutivo.DataTextField = "Nombreusuario"
                cmbEjecutivo.DataValueField = "Codigousuario"
                cmbEjecutivo.DataBind()
                GCCUtilitario.pInsertarPrimerItemHtmlSelect(cmbEjecutivo, "[-Seleccione-]", "0")
            End If

        Catch ex As Exception
            hddError.Value = "No se pudo cargar los Ejecutivos de Ultimus"
        End Try

    End Sub


#End Region

#Region "Metodos"

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

    Public Shared Function ListadoCotizacionCronograma(ByVal pstrNroCotizacion As String, _
                                                       ByVal pstrVersionCotizacion As String _
                                                       ) As String

        'Variables
        Dim objCotizacionNTx As New LCotizacionNTx

        Try
            'Inicializa Objeto
            Dim objECotizacion As New EGcc_cotizacion
            Dim strECotizacion As String
            With objECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrNroCotizacion)
                .Versioncotizacion = GCCUtilitario.StringToInteger(pstrVersionCotizacion)
            End With
            strECotizacion = GCCUtilitario.SerializeObject(objECotizacion)

            'Ejecuta Consulta
            Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.CotizacionCronogramaGet(strECotizacion))

        Catch ex As Exception
            Return Nothing
        End Try
        Return ""
    End Function

#End Region

End Class
