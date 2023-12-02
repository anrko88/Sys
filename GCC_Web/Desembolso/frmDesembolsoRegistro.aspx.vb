Imports System.Data
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Web.Services
Imports System.Collections.Generic

Partial Class Desembolso_frmDesembolsoRegistro
    Inherits GCCBase

    Dim mstrNroContrato As String
    Dim objLog As New GCCLog("frmDesembolsoRegistro.aspx.vb")

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not IsPostBack Then
                mstrNroContrato = Request.QueryString("hcontrato")

                'Fecha Hoy
                Dim dtFecha As Date = Now
                Me.hddFechaActual.Value = dtFecha.ToString("dd/MM/yyyy")

                pInicializarControles()

                'Eventos
                txtNumeroTipo.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) { fn_NumeroTipo1();return false;}} else {return true}; ")

                'Valida Bloqueo
                GestionBloqueo(mstrNroContrato)

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
    ''' Guardar
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Sub GrabarDesembolso(ByVal pstrCodContrato As String, _
                                    ByVal pstrCodProveedor As String, _
                                    ByVal pstrNumeroTipo As String, _
                                    ByVal pstrSerieDoc1 As String, _
                                    ByVal pstrNumeroDoc1 As String, _
                                    ByVal pstrFecVenc As String, _
                                    ByVal pstrAduana As String, _
                                    ByVal pstrAnioAduana As String, _
                                    ByVal pstrNumAduana As String, _
                                    ByVal pstrMoneda As String, _
                                    ByVal pstrProcedencia As String, _
                                    ByVal pstrFecEmsion As String, _
                                    ByVal pstrGravado As String, _
                                    ByVal pstrPorcIGV As String, _
                                    ByVal pstrIgv As String, _
                                    ByVal pstrNoGravado As String, _
                                    ByVal pstrTotal As String, _
                                    ByVal pstrchkAdelantoProveedor As String, _
                                    ByVal pstrAdelantoProveedor As String, _
                                    ByVal pstrPorDesembolsar As String, _
                                    ByVal pstrTipoCambioDia As String, _
                                    ByVal pstrchkTipoCambioEspecial As String, _
                                    ByVal pstrTipoCambioEspecial As String, _
                                    ByVal pstrchkTipoCambioSunat As String, _
                                    ByVal pstrTipoCambioSunat As String, _
                                    ByVal pstrchkDetraccion As String, _
                                    ByVal pstrchkRetencion As String, _
                                    ByVal pstrCodigoTipoBien As String, _
                                    ByVal pstrPorcServicio As String, _
                                    ByVal pstrMontoServicioSoles As String, _
                                    ByVal pstrMontoServicioDolar As String, _
                                    ByVal pstrNumeroConstancia As String, _
                                    ByVal pstrFecEmisionConst As String, _
                                    ByVal pstrNumeroTipo2 As String, _
                                    ByVal pstrSerieDoc2 As String, _
                                    ByVal pstrNroDoc2 As String, _
                                    ByVal pstrFecVenc2 As String, _
                                    ByVal pstrCodEstadoContrato As String, _
                                    ByVal pstrOpcion As String, _
                                    ByVal pstrKeyTipoComprobante As String, _
                                    ByVal pstrKeyNumeroComprobante As String, _
                                    ByVal pstrKeyFechaEmision As String, _
                                    ByVal pstrKeyCodProveedor As String, _
                                    ByVal pstrCodEstadoDoc As String, _
                                    ByVal pstrCodSolicitudCreditoAdd As String, _
                                    ByVal pstrPorc4ta As String, _
                                    ByVal pstrMonto4taSoles As String, _
                                    ByVal pstrMonto4taDolares As String)

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim oLwsDesembolso As New LDesembolsoTx
        Try
            With oESolCredEstructDoc
                .Codsolicitudcredito = pstrCodContrato
                .CodProveedor = pstrCodProveedor
                .Tipodocumento = pstrNumeroTipo
                .Numeroseriedocumento = pstrSerieDoc1
                If pstrNumeroTipo = GCCConstante.C_COD_TIPOCOMPROBANTE_DUA Then
                    .Nrodocumento = pstrNumAduana
                Else
                    .Nrodocumento = GCCUtilitario.Concatenar(pstrSerieDoc1.PadLeft(4, "0"), pstrNumeroDoc1)
                End If
                .FechaVencimiento = GCCUtilitario.StringToDateTime(pstrFecVenc)
                .CodigoTipoAduana = pstrAduana


                .AnioDUA = GCCUtilitario.StringToInteger(pstrAnioAduana)
                .NroComprobanteDUA = pstrNumAduana
                .Monedaoriginal = pstrMoneda
                .Codigoprocedencia = pstrProcedencia
                .Fechaemision = GCCUtilitario.StringToDateTime(pstrFecEmsion)
                .StringFechaEmision = CDate(GCCUtilitario.StringToDateTime(pstrFecEmsion)).ToString("yyyy-MM-dd")
                .MontoGravado = GCCUtilitario.StringToDecimal(pstrGravado)  'Monto  Grabado
                .Igvoriginal = GCCUtilitario.StringToDecimal(pstrPorcIGV)
                .Montoigv = GCCUtilitario.StringToDecimal(pstrIgv)
                .MontoNoGravado = GCCUtilitario.StringToDecimal(pstrNoGravado)  'Monto No Grabado
                .Total = GCCUtilitario.StringToDecimal(pstrTotal)

                'Adelanto Comentado
                .FlagAdelantoProveedor = GCCUtilitario.StringToInteger("0")
                .MontoAdelantoProveedor = GCCUtilitario.StringToDecimal("0")
                .MontoPendienteProveedor = GCCUtilitario.StringToDecimal("0")

                .Tcutilizado = GCCUtilitario.StringToDecimal(pstrTipoCambioDia)
                .FlagTipoCambioEspecial = GCCUtilitario.StringToInteger(pstrchkTipoCambioEspecial)
                .Tipocambioespecial = GCCUtilitario.StringToDecimal(pstrTipoCambioEspecial)
                .FlagTipoCambioSunat = GCCUtilitario.StringToInteger(pstrchkTipoCambioSunat)
                .Tcsbs = GCCUtilitario.StringToDecimal(pstrTipoCambioSunat)

                .Indicedetraccion = pstrchkDetraccion
                .Indiceretencion = pstrchkRetencion
                .CodigoTipoServicio = pstrCodigoTipoBien
                .ServicioPorc = GCCUtilitario.StringToDecimal(pstrPorcServicio)
                .MontoServicioSoles = GCCUtilitario.StringToDecimal(pstrMontoServicioSoles)
                .MontoServicioDolar = GCCUtilitario.StringToDecimal(pstrMontoServicioDolar)
                .NroConstancia = pstrNumeroConstancia
                .FechaEmisionServicio = GCCUtilitario.StringToDateTime(pstrFecEmisionConst)

                .CodigoTipoComprobante = pstrNumeroTipo2
                .NumeroSerieDocumentoAdd = pstrSerieDoc2
                .NroDocumentoAdd = pstrNroDoc2
                .FechaEmisionAdd = GCCUtilitario.StringToDateTime(pstrFecVenc2)
                .CodSolicitudcreditoAdd = pstrCodSolicitudCreditoAdd

                If pstrCodEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_FORMALIZADO Or pstrCodEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ENPROCESO Then
                    If GCCUtilitario.CheckInt(pstrCodEstadoDoc) = GCCConstante.eEstadoDoc.Desembolsado Then
                        .Estadodocumento = GCCConstante.eEstadoDoc.Desembolsado
                    Else
                        .Estadodocumento = GCCConstante.eEstadoDoc.Formalizado
                    End If
                Else
                    .Estadodocumento = GCCConstante.eEstadoDoc.Pendiente
                End If

                '.Estadodocumento = CInt(IIf(pstrCodEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_FORMALIZADO, GCCConstante.eEstadoDoc.Formalizado, GCCConstante.eEstadoDoc.Pendiente))
                If pstrOpcion = "M" Then
                    .KeyTipoComprobante = pstrKeyTipoComprobante
                    .KeyNumeroComprobante = pstrKeyNumeroComprobante
                    .KeyFechaEmision = CDate(GCCUtilitario.StringToDateTime(pstrKeyFechaEmision)).ToString("yyyy-MM-dd")
                    .KeyCodProveedor = pstrKeyCodProveedor
                End If

                'Datos de 4ta
                .Porc4ta = GCCUtilitario.StringToDecimal(pstrPorc4ta)
                .Monto4taSoles = GCCUtilitario.StringToDecimal(pstrMonto4taSoles)
                .Monto4taDolares = GCCUtilitario.StringToDecimal(pstrMonto4taDolares)

            End With
            If pstrOpcion = "N" Then
                oLwsDesembolso.InsertarContratoEstructDoc(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oESolCredEstructDoc))
            ElseIf pstrOpcion = "M" Then

                oLwsDesembolso.ModificarContratoEstructDoc(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oESolCredEstructDoc))
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDesembolso = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' ListaContratoEstructDocumentos
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaContratoEstructDocumentos(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pCodContrato As String) As JQGridJsonResponse


        Dim oLwsDesembolsoNTx As New LDesembolsoNTx
        Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
        Dim odtbLista As New DataTable
        Try
            With oEContratoEstructDoc
                .Codsolicitudcredito = GCCUtilitario.NullableString(pCodContrato)
            End With
            odtbLista = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDesembolsoNTx.ListarContratoEstructDoc(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc) _
                                                                           ))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage

            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If odtbLista.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(odtbLista.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, pCurrentPage, totalRecords, odtbLista)

        Catch ex As Exception
            Throw ex
        Finally
            odtbLista.Dispose()
            oLwsDesembolsoNTx = Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function ObtenerDocumentoEspecifico(ByVal pstrCodContrato As String, _
                                                      ByVal pstrCodProveedor As String, _
                                                      ByVal pstrNumeroTipo As String, _
                                                      ByVal pstrNumeroDoc1 As String, _
                                                      ByVal pstrFecEmision As String) As ESolicitudcreditoestructuradoc
        Dim oLwsDesembolsoNTx As New LDesembolsoNTx
        Dim odtbObtener As New DataTable
        Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
        Try
            With oEContratoEstructDoc
                .Codsolicitudcredito = pstrCodContrato
                .Tipodocumento = pstrNumeroTipo
                .Nrodocumento = pstrNumeroDoc1
                .Fechaemision = GCCUtilitario.StringToDateTime(pstrFecEmision)
                .CodProveedor = pstrCodProveedor
            End With

            odtbObtener = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDesembolsoNTx.ObtenerContratoEstructDoc(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc)))

            For Each odr As DataRow In odtbObtener.Rows
                With oEContratoEstructDoc
                    .Codsolicitudcredito = odr("CodSolicitudCredito").ToString.Trim
                    .CodProveedor = odr("CodProveedor").ToString.Trim
                    .CodigoTipoDocumentoProveedor = GCCUtilitario.CheckInt(odr("CodigoTipoDocProveedor").ToString)
                    .NumeroDocumentoProveedor = odr("NumeroDocumentoProveedor").ToString.Trim
                    .NombreProveedor = odr("NombreProveedor").ToString.Trim
                    .Tipodocumento = odr("TipoDocumento").ToString.Trim
                    If .Tipodocumento = GCCConstante.C_COD_TIPOCOMPROBANTE_DUA Then
                        .Nrodocumento = ""
                    Else
                        .Nrodocumento = odr("NroDocumento").ToString.Trim
                    End If
                    .NombreTipoComprobDoc1 = odr("NombreTipoDocumento").ToString.Trim
                    .Numeroseriedocumento = odr("NumeroSerieDocumento").ToString.Trim
                    '.FechaVencimiento = GCCUtilitario.CheckDate(odr("FechaVencimiento").ToString)
                    .StringFechaVencimiento = odr("StringFechaVencimiento").ToString
                    .CodigoTipoAduana = odr("CodigoTipoAduana").ToString.Trim
                    .NombreTipoAduana = odr("NombreTipoAduana").ToString.Trim
                    .AnioDUA = GCCUtilitario.CheckInt(odr("AnioDUA").ToString)
                    .NroComprobanteDUA = odr("NroComprobanteDUA").ToString.Trim
                    .Monedaoriginal = odr("MonedaOriginal").ToString.Trim
                    .Codigoprocedencia = odr("CodigoProcedencia").ToString.Trim
                    '.Fechaemision = GCCUtilitario.CheckDate(odr("FechaEmision").ToString)
                    .StringFechaEmision = odr("StringFechaEmision").ToString
                    .MontoGravado = GCCUtilitario.CheckDecimal(odr("MontoGravado").ToString)
                    .Igvoriginal = GCCUtilitario.CheckDecimal(odr("IGVOriginal").ToString)
                    .Montoigv = GCCUtilitario.CheckDecimal(odr("MontoIGV").ToString)
                    .MontoNoGravado = GCCUtilitario.CheckDecimal(odr("MontoNoGravado").ToString)
                    .Total = GCCUtilitario.CheckDecimal(odr("Total").ToString)
                    .FlagAdelantoProveedor = GCCUtilitario.CheckInt(odr("FlagAdelantoProveedor"))
                    .MontoAdelantoProveedor = GCCUtilitario.CheckDecimal(odr("MontoAdelantoProveedor").ToString)
                    .MontoPendienteProveedor = GCCUtilitario.CheckDecimal(odr("MontoPendienteProveedor").ToString)
                    .Tcutilizado = GCCUtilitario.CheckDecimal(odr("TCUtilizado").ToString)
                    .FlagTipoCambioEspecial = GCCUtilitario.CheckInt(odr("FlagTipoCambioEspecial"))
                    .Tipocambioespecial = GCCUtilitario.CheckDecimal(odr("TipoCambioEspecial").ToString)
                    .FlagTipoCambioSunat = GCCUtilitario.CheckInt(odr("FlagTipoCambioSunat"))
                    .Tcsbs = GCCUtilitario.CheckDecimal(odr("TCSBS").ToString)

                    'If (odr("IndiceDetraccion").ToString = "S") Then
                    '    .Indicedetraccion = "1"
                    'Else
                    '    .Indicedetraccion = "0"
                    'End If
                    .Indicedetraccion = GCCUtilitario.CheckInt(odr("IndiceDetraccion").ToString)

                    'If (odr("IndiceRetencion").ToString = "S") Then
                    '    .Indiceretencion = "1"
                    'Else
                    '    .Indiceretencion = "0"
                    'End If
                    .Indiceretencion = GCCUtilitario.CheckInt(odr("IndiceRetencion").ToString)
                    .CodigoTipoServicio = odr("CodigoTipoServicio").ToString.Trim
                    .NombreTipoServicio = odr("NombreTipoServicio").ToString.Trim
                    .ServicioPorc = GCCUtilitario.CheckDecimal(odr("ServicioPorc").ToString)
                    .MontoServicioSoles = GCCUtilitario.CheckDecimal(odr("MontoServicioSoles").ToString)
                    .MontoServicioDolar = GCCUtilitario.CheckDecimal(odr("MontoServicioDolar").ToString)
                    .NroConstancia = odr("NroConstancia").ToString.Trim
                    '.FechaEmisionServicio = GCCUtilitario.CheckDate(odr("FechaEmisionServicio").ToString)
                    .StringFechaEmisionServicio = odr("StringFechaEmisionServicio").ToString

                    .CodigoTipoComprobante = odr("CodigoTipoComprobante").ToString.Trim
                    .NombreTipoComprobDoc2 = odr("NombreTipoComprobante").ToString.Trim
                    .NumeroSerieDocumentoAdd = odr("NumeroSerieDocumentoAdd").ToString.Trim
                    .NroDocumentoAdd = odr("NroDocumentoAdd").ToString.Trim
                    '.FechaEmisionAdd = GCCUtilitario.CheckDate(odr("FechaEmisionAdd").ToString)
                    .StringFechaEmisionAdd = odr("StringFechaEmisionAdd").ToString

                    .Monto4taDolares = GCCUtilitario.CheckDecimal(odr("Monto4taDolares").ToString)
                    .Monto4taSoles = GCCUtilitario.CheckDecimal(odr("Monto4taSoles").ToString)
                    .Porc4ta = GCCUtilitario.CheckDecimal(odr("Monto4taPorc").ToString)

                    .Estadodocumento = odr("EstadoDocumento").ToString

                End With
                Exit For
            Next

            Return oEContratoEstructDoc
        Catch ex As Exception
            Throw ex
        Finally
            odtbObtener.Dispose()
            oLwsDesembolsoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Logicamente
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 06/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Sub EliminarDesembolso(ByVal pstrCodContrato As String, _
                                          ByVal pstrCodProveedor As String, _
                                          ByVal pstrNumeroTipo As String, _
                                          ByVal pstrNumeroDoc1 As String, _
                                          ByVal pstrFecEmision As String)

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim oLwsDesembolso As New LDesembolsoTx
        Try
            With oESolCredEstructDoc
                .Codsolicitudcredito = pstrCodContrato
                .Tipodocumento = pstrNumeroTipo
                .Nrodocumento = pstrNumeroDoc1
                .Fechaemision = GCCUtilitario.StringToDateTime(pstrFecEmision)
                .CodProveedor = pstrCodProveedor
                .Estadodocumento = GCCConstante.eEstadoDoc.Anulado
            End With
            oLwsDesembolso.EliminarContratoEstructDoc(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oESolCredEstructDoc))
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDesembolso = Nothing
        End Try
    End Sub

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

    ''' <summary>
    ''' Actualiza Bloquero
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ActualizaBloqueo(ByVal pstrCodBloqueo As String) As String

        Try
            Dim objEBloqueo As New EGCC_Bloqueo
            With objEBloqueo
                .CodigoBloqueo = GCCUtilitario.CheckInt(pstrCodBloqueo)
                .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .NombreUsuario = GCCUtilitario.NullableString(GCCSession.NombreUsuario)
                .Activo = "1"
            End With
            Dim objLUtilTX As New LUtilTX
            objLUtilTX.ModificarBloqueo(GCCUtilitario.SerializeObject(objEBloqueo))

            Return "0"
        Catch ex As Exception
            Return "1"
        End Try

    End Function

    ''' <summary>
    ''' Recalcula IGV Documentos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/09/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RecalculaIGVDocumentos(ByVal pstrNumeroContrato As String) As String

        Dim intResultado As Integer = 0
        Try
            Dim objLDesembolsoTx As New LDesembolsoTx
            intResultado = objLDesembolsoTx.ActualizarIGVDesembolso(pstrNumeroContrato)
            Return intResultado
        Catch ex As Exception
            Return intResultado
        End Try

    End Function

    ''' <summary>
    ''' Generar la Instrucción de Desembolso
    ''' </summary>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <param name="pstrChekeados">Todos los Chekeados</param>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RegistrarID(ByVal pstrCodContrato As String, _
                                       ByVal pstrChekeados As String, _
                                       ByVal pstrChekeados1 As String, _
                                       ByVal pstrActivacion As String, _
                                       ByVal pstrTipoLeasing As String) As String

        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim oESolicitudcredito As New ESolicitudcredito
        Dim oLwsDesembolsoNtx As New LDesembolsoNTx
        Dim oESolCredEstructDoc As ESolicitudcreditoestructuradoc

        Dim strResp As String = ""
        Dim strAsociacionBienes As String = "0|"
        Dim strResAsoc As String()
        Dim oLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx

        Dim flagVerificaBienes As Boolean
        flagVerificaBienes = True

        Try

            If Not pstrChekeados1.Trim.Equals("") And pstrActivacion.Trim().Equals("1") Then

                'VERIFICA EN EL LISTADO DE DOCUMENTOS CUAL TIENE VIENES ASOCIADOS
                Dim decTotalWio As Decimal = 0
                Dim decTotalGrilla As Decimal = 0

                Dim pChkTotal As String() = pstrChekeados1.TrimEnd("|").Split("|")
                Dim pValores As String()
                Dim odtbContratoEstruct As New DataTable
                Dim listaContratoEstruct As New ArrayList

                Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
                Dim odtbLista As New DataTable

                With oEContratoEstructDoc
                    .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                End With
                odtbLista = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                   oLwsDesembolsoNtx.ListarContratoEstructDoc(999, _
                                                                               1, _
                                                                               "CodSolicitudCredito", _
                                                                               "asc", _
                                                                               GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc) _
                                                                               ))

                For x As Integer = 0 To pChkTotal.Length - 1
                    pValores = pChkTotal(x).Split("$")
                    oESolCredEstructDoc = New ESolicitudcreditoestructuradoc
                    With oESolCredEstructDoc
                        .Codsolicitudcredito = pstrCodContrato
                        .CodProveedor = pValores(0)
                        .Tipodocumento = pValores(1)
                        .Nrodocumento = pValores(2)
                        .StringFechaEmision = pValores(3)
                        .Estadodocumento = GCCConstante.eEstadoDoc.Desembolsado
                        decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                        decTotalWio = decTotalWio + decTotalGrilla
                    End With
                    listaContratoEstruct.Add(oESolCredEstructDoc)

                    Dim strfechaemision As String = ""
                    For Each oRow As DataRow In odtbLista.Rows
                        strfechaemision = GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim())
                        If (oRow.Item("CodSolicitudCredito").ToString().Trim().Equals(pstrCodContrato.Trim()) _
                            And oRow.Item("TipoDocumento").ToString().Trim().Equals(pValores(1).Trim()) _
                            And oRow.Item("NroDocumento").ToString().Trim().Equals(pValores(2).Trim()) _
                            And oRow.Item("CodProveedor").ToString().Trim().Equals(pValores(0).Trim()) _
                            And GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim()).Equals(pValores(3).Trim())) Then
                            If (GCCUtilitario.CheckInt(oRow.Item("contadorbienes").ToString()) <= 0) Then
                                flagVerificaBienes = False
                            End If
                        End If
                    Next

                Next

            End If


            If flagVerificaBienes = True Then
                'Inicio IBK - AAE 
                ' si el leasing es TOTAL o voy a Activar un parcial/total ---> chequeo que todos los bienes y documentos estén asociados
                If pstrTipoLeasing.Trim() = "001" Or pstrActivacion.Trim().Equals("1") Then
                    strAsociacionBienes = oLInstruccionDesembolsoTx.CheckRelacionesDocBienes(pstrCodContrato.Trim())
                End If
                strResAsoc = strAsociacionBienes.Split("|")
                If strResAsoc(0) = "0" Then
                    'Activacion
                    With oESolicitudcredito
                        .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                        .FlagActivacionLeasing = GCCUtilitario.NullableString(pstrActivacion)
                    End With
                    Dim strESolicitudcredito As String = GCCUtilitario.SerializeObject(oESolicitudcredito)

                    'Instrucion Desembolso
                    With oEGCC_InsDesembolso
                        .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                        .Documentos = pstrChekeados + "''"
                        .UsuarioRegistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        'Inicio IBK - AAE - Activacion Parcial
                        .FlagActivacion = GCCUtilitario.NullableString(pstrActivacion)
                        'Fin IBK - Activación Parcial
                    End With
                    Dim strEGCC_InsDesembolso As String = GCCUtilitario.SerializeObject(oEGCC_InsDesembolso)
                    Dim strNumeroID As String = oLInstruccionDesembolsoTx.InsertarInsDesembolso(strEGCC_InsDesembolso, strESolicitudcredito)

                    strResp = String.Concat("0|", strNumeroID)
                Else
                    strResp = strAsociacionBienes
                End If
                'fin IBK
            Else
                strResp = String.Concat("1|", "Debe asociar todos los documentos a un bien")
            End If

        Catch ex As Exception
            strResp = String.Concat("1|", ex.Message)
        End Try

        Return strResp

    End Function

    'Inicio IBK - AAE
    ''' <summary>
    ''' Obtiene la info de bienes
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pNroContrato"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaBienesContrato(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pNroContrato As String) As JQGridJsonResponse
        Dim oLDesembolsoNTx As New LDesembolsoNTx()
        Dim dtBienes As DataTable

        Try
            dtBienes = GCCUtilitario.DeserializeObject(Of DataTable)(oLDesembolsoNTx.ListaBienesContrato(pPageSize, _
                                                                                                       pCurrentPage, _
                                                                                                       pSortColumn, _
                                                                                                       pSortOrder, _
                                                                                                       pNroContrato))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtBienes.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtBienes.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtBienes.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienes)
        Catch ex As Exception
            Throw ex
        Finally
            oLDesembolsoNTx = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Obtiene la info de Documentos
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pNroContrato"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pNroContrato As String) As JQGridJsonResponse
        Dim oLDesembolsoNTx As New LDesembolsoNTx()
        Dim dtDocs As DataTable

        Try
            dtDocs = GCCUtilitario.DeserializeObject(Of DataTable)(oLDesembolsoNTx.ListaDocumentosContrato(pPageSize, _
                                                                                                       pCurrentPage, _
                                                                                                       pSortColumn, _
                                                                                                       pSortOrder, _
                                                                                                       pNroContrato))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtDocs.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtDocs.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtDocs.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDocs)
        Catch ex As Exception
            Throw ex
        Finally
            oLDesembolsoNTx = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Obtiene la info de Relaciones entre Bienes y documentos
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pNroContrato"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaRelacionesContrato(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pNroContrato As String) As JQGridJsonResponse
        Dim oLDesembolsoNTx As New LDesembolsoNTx()
        Dim dtRels As DataTable

        Try
            dtRels = GCCUtilitario.DeserializeObject(Of DataTable)(oLDesembolsoNTx.ListaRelacionesContrato(pPageSize, _
                                                                                                       pCurrentPage, _
                                                                                                       pSortColumn, _
                                                                                                       pSortOrder, _
                                                                                                       pNroContrato))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtRels.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtRels.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtRels.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtRels)
        Catch ex As Exception
            Throw ex
        Finally
            oLDesembolsoNTx = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Actualiza las relaciones
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="strSecBien"></param>
    ''' <param name="strArrayDocs"></param>    
    ''' <remarks>Array Docs viene de la forma: "";"";""|"";"";""</remarks>
    <WebMethod()> _
    Public Shared Sub AgregarRelacion(ByVal strNroContrato As String, _
                                                   ByVal strSecBien As String, _
                                                   ByVal strArrayDocs As String)

        Dim oLwsDesembolso As New LDesembolsoTx
        Dim strDocs() As String = Split(strArrayDocs, "|")
        Dim strDetDoc() As String
        Dim strArrayDet As String = ""
        Dim strFecha As String = ""
        Dim cantDocs As Integer = strDocs.Length

        Try
            For i As Integer = 0 To strDocs.Length - 1
                'obtengo los detalles del documento
                strDetDoc = Split(strDocs(i), ";")
                If (i = 0) Then
                    strArrayDet = Trim(strDetDoc(0))
                Else

                    strArrayDet = strArrayDet + "|" + Trim(strDetDoc(0))
                End If
                strArrayDet = strArrayDet + ";" + Trim(strDetDoc(1))
                strArrayDet = strArrayDet + ";" + Trim(strDetDoc(2))
                strFecha = Trim(strDetDoc(3)).Substring(0, 2) + Trim(strDetDoc(3)).Substring(3, 2) + Trim(strDetDoc(3)).Substring(6, 4)
                strArrayDet = strArrayDet + ";" + strFecha

            Next
            oLwsDesembolso.AgregarRelacion(strNroContrato, strSecBien, strArrayDet, strDocs.Length)
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDesembolso = Nothing
        End Try



    End Sub
    'Fin IBK

#End Region

#Region "Métodos"

    Private Sub pInicializarControles()
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim objContratoNTx As New LContratoNTx

        Try
            txtNumeroContrato.Value = mstrNroContrato
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(txtNumeroContrato.Value))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtclasificacion.Value = dr("NOMBRECLASIFICACIONBIEN").ToString.ToUpper
                        txtcu.Value = dr("CODUNICO").ToString
                        txtRazonSocial.Value = dr("NOMBRECLIENTE").ToString
                        txtEstado.Value = dr("ESTADOCONTRATO").ToString.ToUpper
                        txtmoneda.Value = dr("NOMBREMONEDA").ToString


                        hddSubtipoContrato.Value = dr("CODIGOSUBTIPOCONTRATO").ToString.Trim

                        If (GCCUtilitario.CheckInt(dr("FLAGACTIVACIONLEASING")) = 0) Then
                            ChkActivacionLeasing.Checked = 0  'CBool(dr("FLAGTERMINORECEPDOCUMENTOCLIE").ToString)
                        Else
                            ChkActivacionLeasing.Checked = 1
                        End If

                        txtFechaAct.Value = dr("FECHAACTIVACION").ToString
                        hidCodClasificacion.Value = dr("CODIGOCLASIFICACIONBIEN").ToString

                        hidCodEstadoContrato.Value = dr("CODIGOESTADOCONTRATO").ToString.Trim
                        hidCodSubContrato.Value = dr("CODIGOSUBTIPOCONTRATO").ToString.Trim
                        hidCodMoneda.Value = dr("CODMONEDA").ToString.Trim
                        hidNroLineaOp.Value = dr("NROLINEA").ToString
                        hidCodProductoActivo.Value = dr("CODPRODUCTOFINANCIEROACTIVO").ToString

                        hidCodProcedenciaCotizacion.Value = dr("CODIGOPROCEDENCIA").ToString

                        'GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString()).ToString(GCCConstante.C_FormatMiles)
                        hidMontoFinanciado.Value = dr("PRECIOVENTA").ToString
                        txtMontoFinanciado.Value = GCCUtilitario.CheckDecimal(hidMontoFinanciado.Value.ToString()).ToString(GCCConstante.C_FormatMiles)

                        hidMontoIgv.Value = dr("VALORVENTAIGV").ToString
                        txtMontoIgv.Value = GCCUtilitario.CheckDecimal(hidMontoIgv.Value.ToString()).ToString(GCCConstante.C_FormatMiles)

                        hidImporteInicial.Value = dr("IMPORTECUOTAINICIAL").ToString
                        hidCapitalFinanciado.Value = dr("VALORVENTA").ToString
                        txtCapitalFinanciado.Value = GCCUtilitario.CheckDecimal(hidCapitalFinanciado.Value.ToString()).ToString(GCCConstante.C_FormatMiles)

                        hidRiesgoAsumido.Value = dr("RIESGONETO").ToString
                        hidNroCuotas.Value = dr("NUMEROCUOTAS").ToString
                        'hidPeriocidad.Value = dr("CODIGOPERIODICIDAD")
                        hidPeriocidad.Value = dr("CODIGOFRECUENCIAPAGO").ToString
                        hidImporteOpCompra.Value = dr("IMPORTEOPCIONCOMPRA").ToString
                        hidGastoActivacion.Value = dr("IMPORTECOMISIONACTIVACION").ToString
                        hidPolizaSeguro.Value = dr("CODIGOBIENTIPOSEGURO").ToString

                        hidTipoCambioDia.Value = "0"
                        hidTipoCambioSunat.Value = "0"

                        hidNroInstruccion.Value = IIf(dr("NUMEROINSTRUCCION") = 0, "", dr("NUMEROINSTRUCCION"))

                        hdiCostoFondos.Value = GCCUtilitario.CheckDecimal(dr("COSTOFONDOPORC"))
                        hdiTasa.Value = GCCUtilitario.CheckDecimal(dr("TEAPORC"))
                        hdiSpread.Value = GCCUtilitario.CheckDecimal(dr("SPREADPORC"))


                        hidTipoCambioDesembolso.Value = ObtenerTipoCambio(IIf(hidCodMoneda.Value = GCCConstante.C_COD_MONEDA_SOLES, GCCConstante.C_COD_MONEDA_DOLARES, hidCodMoneda.Value), _
                                    GCCUtilitario.ToStringyyyyMMdd(Today.ToString), "SBS")

                        Dim strresultadotcprf As String
                        ' Dim strresultadotcprfvalor As String = ""


                        'si el credito es soles tipo de cambio venta si no compra
                        '002 ES EL VALOR POR DEFECTO EN MONEDATIPODECAMBIO 
                        'MODIFICADO 11/09/2012
                        strresultadotcprf = ObtenerTipoCambio("002", GCCUtilitario.ToStringyyyyMMdd(Today.ToString), "PRF")
                        Dim strresultadotcprfvalor As String() = strresultadotcprf.Split("$"c)


                        If dr("CODMONEDA").ToString.Trim = GCCConstante.C_COD_MONEDA_SOLES Then
                            Me.hddtxttcdia.Value = strresultadotcprfvalor(0)
                            Me.txttcdia.Value = strresultadotcprfvalor(0)  'VENTA
                        Else
                            Me.hddtxttcdia.Value = strresultadotcprfvalor(1)
                            Me.txttcdia.Value = strresultadotcprfvalor(1) 'COMPRA
                        End If
                        Me.hddtcdiaVenta.Value = strresultadotcprfvalor(0)  'VENTA
                        Me.hddtcdiaCompra.Value = strresultadotcprfvalor(1)  'COMPRA
                        Me.txtMontoRegistrado.Value = GCCUtilitario.CheckDecimal(dr("TOTALDESEMBOLSADO").ToString()).ToString(GCCConstante.C_FormatMiles)
                        Dim nbrMontoPorDesembolsar As Decimal = GCCUtilitario.CheckDecimal(dr("VALORVENTA").ToString()) - GCCUtilitario.CheckDecimal(dr("TOTALDESEMBOLSADO").ToString())
                        Me.txtMontoPorDesembolsar.Value = nbrMontoPorDesembolsar.ToString(GCCConstante.C_FormatMiles)

                        dr = Nothing
                        Exit For
                    Next
                End If
                odtbDatos.Dispose()
            End If


            Dim odtbDatos1 As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratosDesembolso(10, 1, "CodSolicitudCredito", "ASC", _
                                                                                                                    txtNumeroContrato.Value, _
                                                                                                                    "", "", "", "", "", "", "", ""))
            If odtbDatos1 IsNot Nothing Then
                If odtbDatos1.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos1.Rows
                        txtcu.Value = dr("CODUNICO").ToString
                        txtRazonSocial.Value = dr("NombreSubprestatario").ToString
                        dr = Nothing
                        Exit For
                    Next
                End If
                odtbDatos.Dispose()
            End If




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
            GCCUtilitario.CargarComboValorGenerico(cmbProcedencia, GCCConstante.C_TABLAGENERICA_PROCEDENCIA)

            txtNroDocProveed.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {fn_buscarProveedor();return false;}} else {return true}; ")
            txtNumeroTipo.Attributes.Add("onblur", "javascript:fn_NumeroTipo1();")
            txtAduana.Attributes.Add("onblur", "javascript:fn_AduanaDUA();")
            txtNumeroTipo2.Attributes.Add("onblur", "javascript:fn_NumeroTipo2();")
            txtTipoBien.Attributes.Add("onblur", "javascript:fn_TipoBien();")
            txtNumeroDoc1.Attributes.Add("onblur", "javascript:fn_SetearTipoComprobante();")
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocClienteNtx = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Gestión Bloquero
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/07/2012
    ''' </remarks>
    Protected Sub GestionBloqueo(ByVal strCodigoContrato As String)

        Try
            'Variables
            Dim objEBloqueo As New EGCC_Bloqueo
            Dim blnNuevoBloqueo As New Boolean
            blnNuevoBloqueo = False

            'Pregunta Bloqueo
            With objEBloqueo
                .TipoDocumento = GCCConstante.C_BLOQUEO_DOC_CONTRATO
                .Modulo = GCCConstante.C_BLOQUEO_MODULO_DESEMBOLSO
                .NumeroDocumento = strCodigoContrato
                .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            Dim objLUtilNTX As New LUtilNTX
            Dim dtBloqueo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTX.ObtenerBloqueo(GCCUtilitario.SerializeObject(objEBloqueo)))

            'Valida Bloqueo Existente
            If Not dtBloqueo Is Nothing Then
                If dtBloqueo.Rows.Count > 0 Then

                    Dim strUsuarioBloqueo As String = dtBloqueo.Rows(0).Item("CodigoUsuario").ToString
                    If strUsuarioBloqueo.Trim().Equals(GCCUtilitario.NullableString(GCCSession.CodigoUsuario)) Then
                        Me.hddBloqueoExistente.Value = "0"
                    Else
                        Me.hddBloqueoExistente.Value = "1"
                        Me.hddBloqueoCodigo.Value = dtBloqueo.Rows(0).Item("CodigoBloqueo").ToString
                        Me.hddBloqueoCodUsuario.Value = dtBloqueo.Rows(0).Item("CodigoUsuario").ToString
                        Me.hddBloqueoNomUsuario.Value = dtBloqueo.Rows(0).Item("NombreUsuario").ToString
                        Me.hddBloqueoFecha.Value = dtBloqueo.Rows(0).Item("FechaInicio").ToString
                    End If

                Else
                    blnNuevoBloqueo = True
                End If
            Else
                blnNuevoBloqueo = True
            End If

            'Ingresa Nuevo Bloqueo
            If blnNuevoBloqueo Then
                Me.hddBloqueoExistente.Value = "0"
                With objEBloqueo
                    .TipoDocumento = GCCConstante.C_BLOQUEO_DOC_CONTRATO
                    .Modulo = GCCConstante.C_BLOQUEO_MODULO_DESEMBOLSO
                    .NumeroDocumento = strCodigoContrato
                    .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    .NombreUsuario = GCCUtilitario.NullableString(GCCSession.NombreUsuario)
                    .Activo = "1"
                End With
                Dim objLUtilTX As New LUtilTX
                objLUtilTX.InsertarBloqueo(GCCUtilitario.SerializeObject(objEBloqueo))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Registro WIO"

    ''' <summary>
    ''' Registrar un nuevo WIO y Cambiar estado del Contrato
    ''' </summary>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <param name="pstrChekeados">Todos los Chekeados</param>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RegistrarWIO(ByVal pstrCodContrato As String, _
                                   ByVal pstrCodUnico As String, _
                                   ByVal pstrNroLinea As String, _
                                   ByVal pstrProducto As String, _
                                   ByVal pstrChekeados As String, _
                                   ByVal pstrMonedaContrato As String, _
                                   ByVal pstrTipoCambioDia As String, _
                                   ByVal pstrMontoFinanciado As String, _
                                   ByVal pstrMontoIgv As String, _
                                   ByVal pstrImporteInicial As String, _
                                   ByVal pstrCapitalFinanciado As String, _
                                   ByVal pstrRiesgoAsumido As String, _
                                   ByVal pstrNroCuotas As String, _
                                   ByVal pstrPeriocidad As String, _
                                   ByVal pstrImporteOpCompra As String, _
                                   ByVal pstrGastoActivacion As String, _
                                   ByVal pstrPolizaSeguro As String, _
                                   ByVal pstrCodProcedenciaCotizacion As String, _
                                   ByVal pstrTipoDesembolso As String, _
                                   ByVal pstrNroIOAsociado As String, _
                                   ByVal pstrCostoFondos As String, _
                                   ByVal pstrTasa As String, _
                                   ByVal pstrSpread As String _
                            ) As String

        Dim oESolCredEstructDoc As ESolicitudcreditoestructuradoc
        Dim oLwsDesembolso As New LDesembolsoTx
        Dim oLwsDesembolsoNtx As New LDesembolsoNTx
        Dim strResp As String = ""
        Try




            Dim decTotalWio As Decimal = 0
            Dim decTotalGrilla As Decimal = 0

            Dim decTcVenta As Decimal = pstrTipoCambioDia.Split("$")(0)
            Dim decTcCompra As Decimal = pstrTipoCambioDia.Split("$")(1)

            Dim pChkTotal As String() = pstrChekeados.TrimEnd("|").Split("|")
            Dim pValores As String()
            Dim odtbContratoEstruct As New DataTable
            Dim listaContratoEstruct As New ArrayList

            Dim flagVerificaBienes As Boolean
            flagVerificaBienes = True

            'VERIFICA EN EL LISTADO DE DOCUMENTOS CUAL TIENE VIENES ASOCIADOS

            Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
            Dim odtbLista As New DataTable

            With oEContratoEstructDoc
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
            End With
            odtbLista = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDesembolsoNtx.ListarContratoEstructDoc(999, _
                                                                           1, _
                                                                           "CodSolicitudCredito", _
                                                                           "asc", _
                                                                           GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc) _
                                                                           ))

            For x As Integer = 0 To pChkTotal.Length - 1
                pValores = pChkTotal(x).Split("$")
                oESolCredEstructDoc = New ESolicitudcreditoestructuradoc
                With oESolCredEstructDoc
                    .Codsolicitudcredito = pstrCodContrato
                    .CodProveedor = pValores(0)
                    .Tipodocumento = pValores(1)
                    .Nrodocumento = pValores(2)
                    .StringFechaEmision = pValores(3)
                    .Estadodocumento = GCCConstante.eEstadoDoc.Desembolsado

                    'If pstrMonedaContrato = GCCConstante.C_CODMON_SOLES Then
                    '    If pValores(4) = GCCConstante.C_CODMON_DOLARES Then
                    '        decTotalGrilla = (GCCUtilitario.CheckDecimal(pValores(5)) * GCCUtilitario.CheckDecimal(decTcCompra))
                    '    Else
                    '        decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                    '    End If
                    'Else
                    '    If pValores(4) = GCCConstante.C_CODMON_SOLES Then
                    '        decTotalGrilla = (GCCUtilitario.CheckDecimal(pValores(5)) / GCCUtilitario.CheckDecimal(decTcVenta))
                    '    Else
                    '        decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                    '    End If
                    'End If

                    decTotalGrilla = GCCUtilitario.CheckDecimal(pValores(5))
                    decTotalWio = decTotalWio + decTotalGrilla
                End With
                listaContratoEstruct.Add(oESolCredEstructDoc)

                'Dim strNumeroDocumento As String = ""
                Dim strfechaemision As String = ""
                For Each oRow As DataRow In odtbLista.Rows

                    strfechaemision = GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim())

                    If (oRow.Item("CodSolicitudCredito").ToString().Trim().Equals(pstrCodContrato.Trim()) _
                        And oRow.Item("TipoDocumento").ToString().Trim().Equals(pValores(1).Trim()) _
                        And oRow.Item("NroDocumento").ToString().Trim().Equals(pValores(2).Trim()) _
                        And oRow.Item("CodProveedor").ToString().Trim().Equals(pValores(0).Trim()) _
                        And GCCUtilitario.ToStringyyyyMMdd(oRow.Item("FechaEmision").ToString().Trim()).Equals(pValores(3).Trim())) Then

                        If (GCCUtilitario.CheckInt(oRow.Item("contadorbienes").ToString()) <= 0) Then
                            flagVerificaBienes = False
                            'strNumeroDocumento = strNumeroDocumento + oRow.Item("NroDocumento").ToString().Trim() + "|"
                        End If
                    End If

                Next


            Next


            If flagVerificaBienes = True Then

                odtbContratoEstruct = GCCUtilitario.ToDataTable(listaContratoEstruct)
                Dim xmlContratoEstruct As String = GCCUtilitario.fstrConvertirDataTableAXML(odtbContratoEstruct)

                'Obtener Cuenta Cargo
                Dim odtbCtaCargoLeasing As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDesembolsoNtx.ObtenerCtaLeasingWioSel(pstrCodContrato))
                Dim xmlCtaContratoLeasing As String = GCCUtilitario.fstrConvertirDataTableAXML(odtbCtaCargoLeasing)

                'Obtener Leasing para enviar a WIO
                Dim odtbBienleasing As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDesembolsoNtx.ObtenerBienLeasingWIO(xmlContratoEstruct))
                Dim xmlBienleasing As String = GCCUtilitario.fstrConvertirDataTableAXML(odtbBienleasing)

                'Declaracion de Objetos donde se serializan 
                Dim strEClienteWIO As String = ""
                Dim strEInstruccionWio As String = ""
                Dim strELineaWio As String = ""
                Dim strECaracteristicaWio As String = ""
                Dim strTasaLineaWio As String = ""
                Dim strSeguimientoWio As String = ""
                Dim strCodTiendaRm As String = ""
                pCargarDatosWIO(pstrCodContrato, _
                                pstrCodUnico, _
                                pstrProducto, _
                                pstrMonedaContrato, _
                                decTotalWio, _
                                decTcVenta.ToString, _
                                pstrMontoFinanciado, _
                                pstrMontoIgv, _
                                pstrImporteInicial, _
                                pstrCapitalFinanciado, _
                                pstrRiesgoAsumido, _
                                pstrNroCuotas, _
                                pstrPeriocidad.Trim, _
                                pstrImporteOpCompra, _
                                pstrGastoActivacion, _
                                pstrPolizaSeguro.Trim, _
                                pstrCodProcedenciaCotizacion, _
                                pstrTipoDesembolso, _
                                pstrNroIOAsociado, _
                                strCodTiendaRm, _
                                strEClienteWIO, strEInstruccionWio, strECaracteristicaWio, strSeguimientoWio, pstrCostoFondos)

                Dim strNroIO As String = oLwsDesembolso.RegistrarWIO(strEClienteWIO, _
                                             strEInstruccionWio, _
                                             strECaracteristicaWio, _
                                             strSeguimientoWio, _
                                             xmlCtaContratoLeasing, _
                                             xmlBienleasing, _
                                             xmlContratoEstruct, _
                                             pstrCodContrato, _
                                             pstrNroLinea, _
                                             "0")


                pCargarDatosLinea(pstrNroLinea, pstrMonedaContrato, strCodTiendaRm, strNroIO, strELineaWio, strTasaLineaWio, pstrTasa, pstrSpread)
                oLwsDesembolso.RegistrarLineaWIO(strELineaWio, strTasaLineaWio, strNroIO)

                strResp = String.Concat("0|", strNroIO)
            Else
                strResp = String.Concat("1|", "Debe asociar todos los documentos a un bien")
            End If  'fin

        Catch ex As Exception
            strResp = String.Concat("1|", ex.Message)
        End Try

        Return strResp
    End Function

    Private Shared Sub pCargarDatosWIO(ByVal pstrCodContrato As String, _
                                       ByVal pstrCodUnico As String, _
                                       ByVal pstrProducto As String, _
                                       ByVal pstrMonedaContrato As String, _
                                       ByVal pImporte As Decimal, _
                                       ByVal pstrTipocambio As String, _
                                       ByVal pstrMontoFinanciado As String, _
                                       ByVal pstrMontoIgv As String, _
                                       ByVal pstrImporteInicial As String, _
                                       ByVal pstrCapitalFinanciado As String, _
                                       ByVal pstrRiesgoAsumido As String, _
                                       ByVal pstrNroCuotas As String, _
                                       ByVal pstrPeriocidad As String, _
                                       ByVal pstrImporteOpCompra As String, _
                                       ByVal pstrGastoActivacion As String, _
                                       ByVal pstrPolizaSeguro As String, _
                                       ByVal pstrCodProcedenciaCotizacion As String, _
                                       ByVal pstrTipoDesembolso As String, _
                                       ByVal pstrNroIOAsociado As String, _
                                       ByRef pstrCodTiendaRm As String, _
                                       ByRef pEClienteWIO As String, ByRef pEInstruccionWio As String, _
                                       ByRef pECaracteristicaWio As String, ByRef pESeguimientoWio As String, _
                                       ByRef pstrCostoFondos As String)
        Dim oLWebservice As New LWebService
        Dim secuencia As Integer = 1
        Try
            Dim strCodUnico As String = pstrCodUnico
            Dim strError As String = ""
            Dim oEClienteRM As New EClienteRM
            oEClienteRM = GCCUtilitario.fObtenerDatosRMCliente(1, strCodUnico, "2", "0", strError)

            If oEClienteRM Is Nothing Then
                Throw New Exception(strError)
                'oEClienteRM.Codigounico = strCodUnico
            End If
            Dim strUsuariosACLS As String = ObtMiembrosGpoLogico(GCCUtilitario.fstrObtieneKeyWebConfig("NeumonicoACLS"), _
                                                                 GCCUtilitario.fstrObtieneKeyWebConfig("wsUltimusWBC"))
            Dim oEClienteWIO As New F15.Entity.ECLIENTE
            With oEClienteWIO
                .Codigounico = GCCUtilitario.fFormatoCodCliente(oEClienteRM.Codigounico, 10)
                .Razonsocial = oEClienteRM.Razonsocialcliente
                .Ciiu = GCCUtilitario.CheckInt(oEClienteRM.Ciiu)
                .Banca = oEClienteRM.Banca
                .Codigoejecutivo = oEClienteRM.Codigoejecutivo
                .Codigotienda = oEClienteRM.Codigotienda
                .Nombretienda = oEClienteRM.Nombretienda
                .Ratingempresa = GCCUtilitario.CheckDecimal(oEClienteRM.Ratingempresa)
                .Fecharatingempresa = Now
                .Segmento = IIf(oEClienteRM.Segmento = "1", "1", "0")
                .Fechaibk = Now
                .Clasificacionsbs = oEClienteRM.Clasificacionsbs
                .Fechasbs = Now
                .Clasificacionfeve = oEClienteRM.Clasificacionfeve
                .Fechafeve = Now
                .Numerodocumento = oEClienteRM.Numerodocumento
                .Nombreejecutivo = oEClienteRM.Nombreejecutivo
                .Nombregrupo = oEClienteRM.Nombregrupo
                .Codigogrupo = GCCUtilitario.CheckInt(oEClienteRM.Codigogrupo)
                .Codigotipodocumento = fObtenerCodigoTipoDoc(oEClienteRM.Codigotipodocumento)
                .CODIGOUSUARIOCREACION = GCCSession.CodigoUsuario
                .CODIGOUSUARIOMODIFICACION = GCCSession.CodigoUsuario
                .Zona = "Zona 1"
            End With
            pstrCodTiendaRm = oEClienteWIO.Codigotienda

            If Not String.IsNullOrEmpty(pstrNroIOAsociado) Then
                secuencia = oLWebservice.fintObtenerSecuenciaLs(pstrNroIOAsociado.PadLeft(18, "0"))
            End If

            'INSTRUCCION OPERATIVA WIO
            Dim pintForOperacion As Integer = 0
            Dim oEInstOperativa As New F15.Entity.EINSTRUCCIONOPERATIVA
            With oEInstOperativa
                .Numeroinstruccion = 0
                .Codigounico = oEClienteWIO.Codigounico
                .Codigoproducto = fObtenerProductoWio(pstrProducto, pintForOperacion)
                .Codigoformaoperacion = pintForOperacion
                .CODIGOUSUARIOCREACION = strUsuariosACLS
                .Secuencia = secuencia
                .Codigomoneda = fObtenerCodigoMonedaWio(pstrMonedaContrato)
                .Contratomarco = 0
                .Importebase = pImporte
                .Importefinal = .Importebase
                .Tipocambio = GCCUtilitario.CheckDecimal(pstrTipocambio)
                .CODIGOWORKFLOW = 0
                .ESTACIONULTIMUS = F15.Entity.EConstante.C_vRECEPCION_SOLICITUD
                .Codigoestado = 221
                .Modalidad = fObtenerProcedenciaWio(pstrCodProcedenciaCotizacion.Trim)
                .TipoDesembolso = fObtenerTipoDesembolsoWio(pstrTipoDesembolso)
                .ActivaOP = 0
                .Numerooperacionasociada = IIf(secuencia = 1, "", pstrNroIOAsociado)
                .Numerooperacion = pstrCodContrato
                .NumeroregistroN1 = pstrCodContrato
                .Aplicacostofondo = 1
                .Valorcostofondo = pstrCostoFondos
            End With

            'Caracteristicas Leasing
            Dim oECaractOperLeasing As New F15.Entity.ECARACTERISTICAOPERACIONLEASING
            With oECaractOperLeasing
                .NumeroInstruccion = 0
                .CodMoneda = oEInstOperativa.Codigomoneda
                .CapitalFinanciado = GCCUtilitario.CheckDecimal(pstrCapitalFinanciado)
                .IGV = pstrMontoIgv
                .MontoFinanciado = pstrMontoFinanciado
                .CuotaInicial = pstrImporteInicial
                .RiesgoAsumido = pstrRiesgoAsumido
                .NumeroCuotas = pstrNroCuotas
                .Periodicidad = fObtenerPeriocidadWio(pstrPeriocidad)
                .PeriodicidadGracia = 0
                .OpcionCompra = pstrImporteOpCompra
                .Gastos = pstrGastoActivacion
                .PolizaSeguro = fObtenerPolizaSeguroWio(pstrPolizaSeguro)
                .EspecificacionesDesembolso = ""
            End With
            pECaracteristicaWio = GCCUtilitario.SerializeObject(Of F15.Entity.ECARACTERISTICAOPERACIONLEASING)(oECaractOperLeasing)

            'SEGUIMEINTO
            Dim oESeguimiento As New F15.Entity.ESEGUIMIENTO
            With oESeguimiento
                .Numeroinstruccion = 0
                .Codigoseguimiento = 0
                .Fechainicio = CType(Now, Date)
                .Fechainicioproceso = CType(Now, Date)
                .Fechafin = CType(Now, Date)
                .Codigousuarioregistro = GCCSession.CodigoUsuario
                .Vigenciausuario = 1
                .Nivel = 0
                .Codigoaccion = F15.Entity.EConstante.eAccion.nGuardar
                .Codigoestacion = F15.Entity.EConstante.C_nRECEPCION_SOLICITUD
                .Secuencia = 1
                .NombreUsuario = GCCSession.NombreUsuario
                .Observacion = "Se envia desde " & GCC.Entity.EConstante.C_NOMBRE_APLICATIVO & " en Desembolso."
                .CodigoRol = GCCSession.PerfilUsuario
            End With

            pEClienteWIO = GCCUtilitario.SerializeObject(Of F15.Entity.ECLIENTE)(oEClienteWIO)
            pEInstruccionWio = GCCUtilitario.SerializeObject(Of F15.Entity.EINSTRUCCIONOPERATIVA)(oEInstOperativa)
            pESeguimientoWio = GCCUtilitario.SerializeObject(Of F15.Entity.ESEGUIMIENTO)(oESeguimiento)

        Catch ex As Exception
            Throw ex
        Finally
            oLWebservice = Nothing
        End Try
    End Sub

    Private Shared Sub pCargarDatosLinea(ByVal pstrNroLinea As String, _
                                  ByVal pstrMonedaContrato As String, _
                                  ByVal pstrCodTiendaRm As String, _
                                  ByVal pstrNroInstruccion As String, _
                                  ByRef pELineaWio As String, _
                                  ByRef pTasaLineaWio As String, _
                                  ByRef pstrTasa As String, _
                                  ByRef pstrSpread As String)
        Dim oLWebservice As New LWebService
        Try
            Dim stMonedaWIO As String = fObtenerCodigoMonedaWio(pstrMonedaContrato)

            'LINEAS
            Dim odtbLODet As DataTable = GCCUtilitario.DeserializeObject2(Of DataTable)(oLWebservice.fObtenerDatosLineaOP(pstrNroLinea))

            If odtbLODet.Rows.Count > 0 Then
                Dim oELineaCredito As New F15.Entity.ELINEACREDITO
                With oELineaCredito
                    .Numeroinstruccion = pstrNroInstruccion
                    .Codigotipocredito = 87
                    .Nuevalineaoperacion = 0
                    .Numerolineaoperacion = pstrNroLinea
                    .NroLinea = pstrNroLinea

                    .Riesgomaximocliente = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("RIESGOMAXIMOCLIENTE").ToString)
                    .Riesgomaximogrupo = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("RIESGOMAXIMOGRUPO").ToString)

                    .RiesgomaximoclienteOri = .Riesgomaximocliente
                    .RiesgomaximogrupoOri = .Riesgomaximogrupo

                    .Montoaprobado = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("MONTOAPROBADO").ToString)
                    .Saldodisponible = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("SALDOANTESOPERACION").ToString)
                    .Saldoreservado = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("SALDORESERVADO").ToString)
                    .Saldolocacion = GCCUtilitario.CheckDecimal(odtbLODet.Rows(0).Item("MONTORECIBIDO").ToString)
                    .Codigoestado = GCCUtilitario.CheckInt(odtbLODet.Rows(0).Item("CODIGOESTADO").ToString)
                    .CodigoTienda = pstrCodTiendaRm

                    .Fechavencimiento = GCCUtilitario.CheckDate(odtbLODet.Rows(0).Item("FECHAVENCIMIENTO").ToString)
                End With

                'Obtener Tasa
                Dim odtbTasa As DataTable = GCCUtilitario.DeserializeObject2(Of DataTable)(oLWebservice.fObtenerTasasLineas("0", pstrNroLinea))
                Dim tblMoneda As New DataTable
                Dim tblTipo As New DataTable
                Dim tblSubtipo As New DataTable
                For Each DR As DataRow In odtbTasa.Rows
                    tblMoneda = GCCUtilitario.DeserializeObject(Of DataTable)(oLWebservice.fObtenerParamDomWio(7, CType(DR("CODIGOMONEDA"), Integer), 1))
                    tblTipo = GCCUtilitario.DeserializeObject(Of DataTable)(oLWebservice.fObtenerParamDomWio(32, CType(DR("CODIGOTIPOTASA"), Integer), 1))
                    tblSubtipo = GCCUtilitario.DeserializeObject(Of DataTable)(oLWebservice.fObtenerParamDomWio(33, CType(DR("CODIGOSUBTIPOTASA"), Integer), 1))
                    DR("CODIGOMONEDA") = tblMoneda.Rows(0).Item("CODIGO").ToString
                    DR("MONEDA") = tblMoneda.Rows(0).Item("DESCRIPCION").ToString
                    DR("CODIGOTIPOTASA") = tblTipo.Rows(0).Item("CODIGO").ToString
                    DR("TIPOTASA") = tblTipo.Rows(0).Item("DESCRIPCION").ToString
                    DR("CODIGOSUBTIPOTASA") = tblSubtipo.Rows(0).Item("CODIGO").ToString
                    DR("SUBTIPOTASA") = tblSubtipo.Rows(0).Item("DESCRIPCION").ToString

                    tblMoneda = Nothing
                    tblTipo = Nothing
                    tblSubtipo = Nothing
                Next

                Dim dvwfilterTasaComisionWIO As DataView = odtbTasa.DefaultView
                dvwfilterTasaComisionWIO.RowFilter = " CODIGOMONEDA = " & stMonedaWIO
                odtbTasa = New DataTable
                odtbTasa = dvwfilterTasaComisionWIO.ToTable

                If odtbTasa.Rows.Count = 0 Then

                    '-------------------------------------
                    'INICIO :: IF
                    '-------------------------------------
                    Dim FilaTasaComision As DataRow = odtbTasa.NewRow

                    '******************************
                    'Tasa
                    '******************************
                    FilaTasaComision("CODIGOTIPOTASA") = "226"
                    FilaTasaComision("TIPOTASA") = "T.I.R."
                    FilaTasaComision("CODIGOSUBTIPOTASA") = "237"
                    FilaTasaComision("SUBTIPOTASA") = "Tasa"

                    If stMonedaWIO = "95" Then
                        FilaTasaComision("CODIGOMONEDA") = "95"
                        FilaTasaComision("MONEDA") = "Nuevos Soles"

                    ElseIf stMonedaWIO = "96" Then
                        FilaTasaComision("CODIGOMONEDA") = "96"
                        FilaTasaComision("MONEDA") = "Dólares USA"
                    End If

                    FilaTasaComision("VALORLINEAS") = "-0.0100"
                    FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrTasa).ToString(GCCConstante.C_FormatMiles)
                    FilaTasaComision("CODIGOTASACOMISION") = "1"

                    'Aumenta FILA
                    odtbTasa.Rows.Add(FilaTasaComision)

                    '******************************
                    'Spread Mínimo
                    '******************************
                    FilaTasaComision = odtbTasa.NewRow
                    FilaTasaComision("CODIGOTIPOTASA") = "227"
                    FilaTasaComision("TIPOTASA") = "Spread Mínimo"
                    FilaTasaComision("CODIGOSUBTIPOTASA") = "238"
                    FilaTasaComision("SUBTIPOTASA") = "Spread"

                    If stMonedaWIO = "95" Then
                        FilaTasaComision("CODIGOMONEDA") = "95"
                        FilaTasaComision("MONEDA") = "Nuevos Soles"

                    ElseIf stMonedaWIO = "96" Then
                        FilaTasaComision("CODIGOMONEDA") = "96"
                        FilaTasaComision("MONEDA") = "Dólares USA"
                    End If

                    FilaTasaComision("VALORLINEAS") = "-0.0100"
                    FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrSpread).ToString(GCCConstante.C_FormatMiles)
                    FilaTasaComision("CODIGOTASACOMISION") = "2"

                    'Aumenta FILA
                    odtbTasa.Rows.Add(FilaTasaComision)

                    '-------------------------------------
                    'FIN :: IF
                    '-------------------------------------

                Else

                    '-------------------------------------
                    'INICIO :: ELSE
                    '-------------------------------------

                    'Filtro
                    dvwfilterTasaComisionWIO.RowFilter = ""
                    dvwfilterTasaComisionWIO.RowFilter = "CODIGOSUBTIPOTASA=237 AND CODIGOTIPOTASA=226"


                    If dvwfilterTasaComisionWIO.Count = 0 Then
                        Dim FilaTasaComision As DataRow = odtbTasa.NewRow
                        FilaTasaComision("CODIGOTIPOTASA") = "226"
                        FilaTasaComision("TIPOTASA") = "T.I.R."
                        FilaTasaComision("CODIGOSUBTIPOTASA") = "237"
                        FilaTasaComision("SUBTIPOTASA") = "Tasa"

                        If stMonedaWIO = "95" Then
                            FilaTasaComision("CODIGOMONEDA") = "95"
                            FilaTasaComision("MONEDA") = "Nuevos Soles"

                        ElseIf stMonedaWIO = "96" Then
                            FilaTasaComision("CODIGOMONEDA") = "96"
                            FilaTasaComision("MONEDA") = "Dólares USA"
                        End If

                        FilaTasaComision("VALORLINEAS") = "-0.0100"
                        FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrTasa).ToString(GCCConstante.C_FormatMiles)
                        FilaTasaComision("CODIGOTASACOMISION") = "1"

                        odtbTasa.Rows.Add(FilaTasaComision)

                    Else

                        For Each oRow As DataRow In odtbTasa.Rows
                            If oRow.Item("CODIGOSUBTIPOTASA").ToString().Trim().Equals("237") And oRow.Item("CODIGOTIPOTASA").ToString().Trim().Equals("226") Then
                                oRow.Item("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrTasa).ToString(GCCConstante.C_FormatMiles)
                            End If
                        Next
                        odtbTasa.AcceptChanges()

                    End If


                    'Filtro
                    dvwfilterTasaComisionWIO.RowFilter = ""
                    dvwfilterTasaComisionWIO.RowFilter = "CODIGOSUBTIPOTASA=238 AND CODIGOTIPOTASA=227"

                    If dvwfilterTasaComisionWIO.Count = 0 Then
                        Dim FilaTasaComision As DataRow = odtbTasa.NewRow
                        FilaTasaComision("CODIGOTIPOTASA") = "227"
                        FilaTasaComision("TIPOTASA") = "Spread Mínimo"
                        FilaTasaComision("CODIGOSUBTIPOTASA") = "238"
                        FilaTasaComision("SUBTIPOTASA") = "Spread"

                        If stMonedaWIO = "95" Then
                            FilaTasaComision("CODIGOMONEDA") = "95"
                            FilaTasaComision("MONEDA") = "Nuevos Soles"
                        ElseIf stMonedaWIO = "96" Then
                            FilaTasaComision("CODIGOMONEDA") = "96"
                            FilaTasaComision("MONEDA") = "Dólares USA"
                        End If

                        FilaTasaComision("VALORLINEAS") = "-0.0100"
                        FilaTasaComision("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrSpread).ToString(GCCConstante.C_FormatMiles)
                        FilaTasaComision("CODIGOTASACOMISION") = "2"
                        odtbTasa.Rows.Add(FilaTasaComision)

                    Else

                        For Each oRow As DataRow In odtbTasa.Rows
                            If oRow.Item("CODIGOSUBTIPOTASA").ToString().Trim().Equals("238") And oRow.Item("CODIGOTIPOTASA").ToString().Trim().Equals("227") Then
                                oRow.Item("VALOROPERACION") = GCCUtilitario.CheckDecimal(pstrSpread).ToString(GCCConstante.C_FormatMiles)
                            End If
                        Next
                        odtbTasa.AcceptChanges()

                    End If

                    '-------------------------------------
                    'FIN :: ELSE
                    '-------------------------------------

                End If

                pELineaWio = GCCUtilitario.SerializeObject(Of F15.Entity.ELINEACREDITO)(oELineaCredito)
                pTasaLineaWio = GCCUtilitario.fstrConvertirDataTableAXML(odtbTasa)
            Else
                pELineaWio = Nothing
                pTasaLineaWio = Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLWebservice = Nothing
        End Try
    End Sub

    Private Shared Function ObtMiembrosGpoLogico(ByVal pstrGroupName As String, _
                                                 ByVal sURLwsUltimus As String) As String

        Dim ObjStructGroupMembers() As wsUltimus.structGroupMembers = Nothing
        Dim strUserName As String = ""
        Dim strUsuarios As String = ""
        Dim strDominioUsuario As String = GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
        Dim objUltimus As New LWebService
        Dim bok As Boolean = False
        Try
            bok = objUltimus.fblnObtenerMemberGroup(pstrGroupName, ObjStructGroupMembers, sURLwsUltimus, "")
            If ObjStructGroupMembers IsNot Nothing Then
                For intIndex As Int32 = 0 To ObjStructGroupMembers.Length - 1
                    strUserName = ObjStructGroupMembers(intIndex).strMemberName.ToUpper
                    strUserName = strUserName.Replace(strDominioUsuario.ToUpper & "/", "").Trim()
                    strUsuarios += strUserName + ","
                Next
            End If

            strUsuarios = strUsuarios.TrimEnd(",")
        Catch ex As Exception
            Throw ex
        Finally
            objUltimus = Nothing
        End Try
        Return strUsuarios
    End Function

    Private Shared Function fObtenerCodigoTipoDoc(ByVal pstrTipoDoc As String) As Integer
        Dim intTipoDoc As Integer = 0
        Select Case pstrTipoDoc
            Case GCC.Entity.EConstante.C_TRX_TIPDOC_DNI
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_DNI
            Case GCC.Entity.EConstante.C_TRX_TIPDOC_CE
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_CE
            Case GCC.Entity.EConstante.C_TRX_TIPDOC_PA
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_PAS
            Case Else
                intTipoDoc = GCCConstante.C_WIO_TIPODOC_RUC
        End Select

        Return intTipoDoc
    End Function

    Private Shared Function fObtenerCodigoMonedaWio(ByVal pstrCodigoMoneda As String) As Integer
        Select Case pstrCodigoMoneda
            Case GCCConstante.C_CODMON_SOLES
                Return 95
            Case GCCConstante.C_CODMON_DOLARES
                Return 96
        End Select
    End Function

    Private Shared Function fObtenerProductoWio(ByVal pstrCodigoProducto As String, ByRef pintForOperacion As Integer) As Integer
        Select Case pstrCodigoProducto
            Case GCCConstante.C_CODLPC_PROD_LEASING
                pintForOperacion = 26
                Return 11
            Case GCCConstante.C_CODLPC_PROD_LEASEBACK
                pintForOperacion = 64
                Return 19
        End Select
    End Function

    Private Shared Function fObtenerPeriocidadWio(ByVal pstrCodigoPeriocidad As String) As Integer
        Select Case pstrCodigoPeriocidad.Trim
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_FIJA
                Return 456
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_VARIABLE
                Return 816
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_FIJA
                Return 151
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_VARIABLE
                Return 813
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_FIJA
                Return 150
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_VARIABLE
                Return 812
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_FIJA
                Return 455
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_VARIABLE
                Return 815
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_FIJA
                Return 152
            Case GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_VARIABLE
                Return 814
            Case Else
                Return 457
        End Select
    End Function

    Private Shared Function fObtenerPolizaSeguroWio(ByVal pstrCodigoPoliza As String) As Integer
        Select Case pstrCodigoPoliza
            Case "001"
                Return 490
            Case "002"
                Return 489
        End Select
    End Function

    Private Shared Function fObtenerProcedenciaWio(ByVal pstrCodigoProcedencia As String) As Integer
        Select Case pstrCodigoProcedencia
            Case "001"
                Return 219
            Case "002"
                Return 220
        End Select
    End Function

    Private Shared Function fObtenerTipoDesembolsoWio(ByVal psrtTipoDesembolso As String) As Integer
        Select Case psrtTipoDesembolso
            Case "001"
                Return 181
            Case "002"
                Return 180
        End Select
    End Function

#End Region

End Class
