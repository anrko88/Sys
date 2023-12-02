
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class GestionBien_OpcionCompra_frmOpcionCompraRegistro
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmOpcionCompraRegistro.aspx.vb")

#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                pCargarQueryString()
                pInicializaPagina()

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

#Region "   Métodos     "

    ''' <summary>
    ''' Función que Carga lo parametros de la url
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:27/10/2011</remarks>
    Private Sub pCargarQueryString()
        hidCodOpcionCompra.Value = Request.QueryString("coc")
        hidNumeroContrato.Value = Request.QueryString("csc")
    End Sub

    ''' <summary>
    ''' Inicializa Combos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Protected Sub pInicializaPagina()
        hddFechaActual.Value = Date.Now.ToShortDateString
        GCCUtilitario.CargarComboValorGenerico(ddlPago, GCCConstante.C_TABLAGENERICA_OpcionCompra_TipoPagoBien)
        pObtenerDatos()
    End Sub

    ''' <summary>
    ''' Metodo que obtiene los datos para la opción de compra
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:08/01/2013</remarks>
    Private Sub pObtenerDatos()
        Dim objLOpcionCompra As New LOpcionCompraNTx
        Dim objEOpcionCompra As New EGCC_OpcionCompra
        Dim odtbOpcionCompra As New DataTable

        With objEOpcionCompra
            .NumeroContrato = hidNumeroContrato.Value
            .CodOpcionCompra = GCCUtilitario.CheckInt64(hidCodOpcionCompra.Value)
        End With

        odtbOpcionCompra = GCCUtilitario.DeserializeObject(Of DataTable)(objLOpcionCompra.fobjLeerOpcionCompra(GCCUtilitario.SerializeObject(objEOpcionCompra)))
        If odtbOpcionCompra IsNot Nothing Then
            If odtbOpcionCompra.Rows.Count > 0 Then
                With odtbOpcionCompra.Rows(0)
                    txtNroContrato.Value = .Item("CodSolicitudCredito").ToString()
                    txtCUCliente.Value = .Item("CodUnico").ToString()
                    txtRazonSocialProveedor.Value = .Item("ClienteRazonSocial").ToString()
                    txtClasificacionBien.Value = .Item("NombreClasificacionbien").ToString().ToUpper()
                    txtEstadoContrato.Value = .Item("EstadoContrato").ToString()
                    txtFechaActivacion.Value = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(.Item("FechaActivacion").ToString()))
                    txtBanca.Value = .Item("NombreBanca").ToString()
                    txtEjecutivoBanca.Value = .Item("DesEjecutivoBanca").ToString()
                    txtMoneda.Value = .Item("NombreMoneda").ToString()
                    txtPorcentajeOC.Value = GCCUtilitario.CheckDecimal(.Item("OpcionCompraPorc").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtImporteOC.Value = GCCUtilitario.CheckDecimal(.Item("ImporteOpcionCompra").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtComisionOC.Value = GCCUtilitario.CheckDecimal(.Item("PorcentajeC").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtPorcentajeCGT.Value = GCCUtilitario.CheckDecimal(.Item("PorcentajeCGT").ToString()).ToString(GCCConstante.C_FormatMiles)
                    hidCodEstadoContrato.Value = .Item("CodigoEstadoContrato").ToString()
                End With
            End If
        End If

    End Sub

#End Region

#Region "   Web Metodos "

    ''' <summary>
    ''' Lista Bien
    ''' </summary>
    ''' <returns>Listado de Bienes</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function BuscarBien(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pNumeroContraro As String) As JQGridJsonResponse
        Dim objOpcionCompraNTx As New LOpcionCompraNTx
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        With oEOpcionCompra
            .NumeroContrato = pNumeroContraro
        End With

        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.fobjListadoBienOpcionCompra(pPageSize, _
                                                                                                                                    pCurrentPage, _
                                                                                                                                    pSortColumn, _
                                                                                                                                    pSortOrder, _
                                                                                                                                    GCCUtilitario.SerializeObject(Of EGCC_OpcionCompra)(oEOpcionCompra)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtContrato.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtContrato.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtContrato.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtContrato)

    End Function

    ''' <summary>
    ''' Lista Bien
    ''' </summary>
    ''' <returns>Listado de Documento</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListadoDocumento(ByVal pNumeroContraro As String) As JQGridJsonResponse
        Dim objOpcionCompraNTx As New LOpcionCompraNTx
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        With oEOpcionCompra
            .NumeroContrato = pNumeroContraro
        End With

        Dim dtDocumento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.ListadoOpcionCompraDocumento(GCCUtilitario.SerializeObject(Of EGCC_OpcionCompra)(oEOpcionCompra)))

        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 99, dtDocumento)

    End Function

    ''' <summary>
    ''' Obtener Bien
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ObtenerBien(ByVal pstrNroContrato As String, _
                                       ByVal pstrSecFinanciamiento As String) As EGCC_OpcionCompra
        Dim objLOpcionCompra As New LOpcionCompraNTx
        Dim objEOpcionCompra As New EGCC_OpcionCompra
        Dim odtbBien As New DataTable

        Try
            With objEOpcionCompra
                .NumeroContrato = pstrNroContrato
                .SecFinanciamiento = GCCUtilitario.CheckInt(pstrSecFinanciamiento)
            End With

            odtbBien = GCCUtilitario.DeserializeObject(Of DataTable)(objLOpcionCompra.fobjLeerBienOpcionCompra(GCCUtilitario.SerializeObject(objEOpcionCompra)))
            If odtbBien IsNot Nothing Then
                If odtbBien.Rows.Count > 0 Then

                    With objEOpcionCompra
                        .CodSolicitudCredito = odtbBien.Rows(0).Item("CodSolicitudCredito").ToString
                        .SecFinanciamiento = odtbBien.Rows(0).Item("SecFinanciamiento").ToString
                        .DescripcionBien = odtbBien.Rows(0).Item("DescripcionBien").ToString
                        .TipoBien = odtbBien.Rows(0).Item("TipoBien").ToString
                        .Ubicacion = odtbBien.Rows(0).Item("Ubicacion").ToString
                        .DepartamentoNombre = odtbBien.Rows(0).Item("DepartamentoNombre").ToString
                        .ProvinciaNombre = odtbBien.Rows(0).Item("ProvinciaNombre").ToString
                        .DistritoNombre = odtbBien.Rows(0).Item("DistritoNombre").ToString
                        .PlacaActual = odtbBien.Rows(0).Item("PlacaAnterior").ToString
                        .NroMotor = odtbBien.Rows(0).Item("NroMotor").ToString
                        .Marca = odtbBien.Rows(0).Item("Marca").ToString
                        .FlagAceptacionCliente = odtbBien.Rows(0).Item("FlagAceptacionCliente").ToString
                        .StringFechaAceptacionCliente = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbBien.Rows(0).Item("FechaAceptacionCliente").ToString()))
                        .StringFechaTransferencia = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbBien.Rows(0).Item("FechaTransferencia").ToString()))
                        .StringFechaTransferenciaRRPP = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbBien.Rows(0).Item("FechaTransferenciaRRPP").ToString()))
                        .StringFechaPagoOC = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(odtbBien.Rows(0).Item("FechaPagoOC").ToString()))
                        .CodTipoPagoOC = odtbBien.Rows(0).Item("CodTipoPagoOC").ToString
                        .Item = odtbBien.Rows(0).Item("TotalBienes").ToString
                        .CodOpcionCompra = odtbBien.Rows(0).Item("CodOpcionCompra").ToString
                    End With

                End If
            End If

            Return objEOpcionCompra
        Catch ex As Exception
            Throw ex
        Finally
            odtbBien.Dispose()
            objLOpcionCompra = Nothing
            'objECreditoRecuperacionComision = Nothing
        End Try
    End Function



    ''' <summary>
    ''' Graba Bien
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 09/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaBien(ByVal pstrNumeroContrato As String, _
                                     ByVal pstrCodOpcionCompra As String, _
                                     ByVal pstrSecFinanciamiento As String, _
                                     ByVal pstrFlagAceptacion As String, _
                                     ByVal pstrFechaAceptacion As String, _
                                     ByVal pstrFechaTransferencia As String, _
                                     ByVal pstrFechaTransferenciaRRPP As String, _
                                     ByVal pstrFechaPagoOC As String, _
                                     ByVal pstrTipoPago As String, _
                                     ByVal pstrTotalBienes As String) As String

        Try
            Dim objEOpcionCompra As New EGCC_OpcionCompra

            With objEOpcionCompra
                .NumeroContrato = pstrNumeroContrato
                .CodOpcionCompra = GCCUtilitario.CheckInt64(pstrCodOpcionCompra)
                .SecFinanciamiento = GCCUtilitario.CheckInt(pstrSecFinanciamiento)
                .FlagAceptacionCliente = pstrFlagAceptacion
                .FechaAceptacionCliente = GCCUtilitario.CheckDate(pstrFechaAceptacion)
                .FechaTransferencia = GCCUtilitario.CheckDate(pstrFechaTransferencia)
                .FechaTransferenciaRRPP = GCCUtilitario.CheckDate(pstrFechaTransferenciaRRPP)
                .FechaPagoOC = GCCUtilitario.CheckDate(pstrFechaPagoOC)
                .CodTipoPagoOC = pstrTipoPago
                .Item = GCCUtilitario.CheckInt(pstrTotalBienes)
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
            End With

            'Ejecuta Transaccion
            Dim objLOpcionCompraTx As New LOpcionCompraTx

            Dim blnResult As Boolean = False
            blnResult = objLOpcionCompraTx.fblnModificarBienOpcionCompra(GCCUtilitario.SerializeObject(objEOpcionCompra))

            'Valida Resultado
            If blnResult Then
                Return "1"
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function


    ''' <summary>
    ''' Graba Opcion Compra Documento
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaDocumento(ByVal pstrNumeroContrato As String, _
                                          ByVal pstrCodOpcionCompraDocumento As String, _
                                          ByVal pstrCodCheckList As String, _
                                          ByVal pstrFlagCheckList As String) As String

        Try
            Dim objEOpcionCompra As New EGCC_OpcionCompra

            With objEOpcionCompra
                .NumeroContrato = pstrNumeroContrato
                .CodOperacionDocumento = GCCUtilitario.CheckInt(pstrCodOpcionCompraDocumento)
                .CodCheckList = pstrCodCheckList
                .FlagCheckList = pstrFlagCheckList
                If pstrFlagCheckList = "1" Then
                    .FechaCheckList = DateTime.Now
                Else
                    .FechaCheckList = New DateTime(1900, 1, 1)
                End If
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
                .Item = 1
            End With

            'Ejecuta Transaccion
            Dim objLOpcionCompraTx As New LOpcionCompraTx

            Dim blnResult As Boolean = False
            blnResult = objLOpcionCompraTx.fblnInsertarOpcionCompraDocumento(GCCUtilitario.SerializeObject(objEOpcionCompra))

            'Valida Resultado
            If blnResult Then
                Return "1"
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

    ''' <summary>
    ''' Listar Envios
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarOpcionCompraEnvio(ByVal pNumeroContraro As String) As JQGridJsonResponse
        Dim objOpcionCompraNTx As New LOpcionCompraNTx
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        With oEOpcionCompra
            .NumeroContrato = pNumeroContraro
        End With

        Dim dtEnvio As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.fobjListadoOpcionCompraEnvio(GCCUtilitario.SerializeObject(Of EGCC_OpcionCompra)(oEOpcionCompra)))

        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 20, dtEnvio)

    End Function

#End Region

End Class
