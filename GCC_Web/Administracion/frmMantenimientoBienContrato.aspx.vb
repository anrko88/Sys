Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmMantenimientoBienContrato
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMantenimientoBienContrato.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 20/09/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida(Sesión)
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                'hidSecFinanciamiento.Value = Request.QueryString("csf")
                hidNumeroContrato.Value = Request.QueryString("csc")
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienMaquinariaOtros, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienUnidadTransporte, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienSistemas, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoBienInmueble, GCCConstante.C_TABLAGENERICA_ESTA_BIEN_E)
                GCCUtilitario.CargarComboValorGenerico(ddlNotaria, GCCConstante.C_TABLAGENERICA_NOTARIA_PUBLICA)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionRRPP, GCCConstante.C_TABLAGENERICA_Estado_Inscripción_RRPP)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionMunicipal, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                'GCCUtilitario.CargarComboValorGenerico(ddlPropiedad, GCCConstante.C_TABLAGENERICA_Estado_Transferencia)
                pInicializarControles()

            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                If Not IsPostBack Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                End If
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"
    Private Sub pInicializarControles()
        Dim oLwsDocBienNtx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        'Dim pPageSize As Integer = "100"
        'Dim pCurrentPage As Integer = "1"
        'Dim pSortColumn As String = "CodSolicitudCredito"
        'Dim pSortOrder As String = "desc"
        'Dim pNumeroContrato As String
        'Dim pCodEstadoLogico As String = "001"




        Try
            oESolicitudCreditoEstructura = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosBienContrato(hidNumeroContrato.Value))
            If oESolicitudCreditoEstructura IsNot Nothing Then
                With oESolicitudCreditoEstructura
                    txtNumeroContrato.Value = .Codsolicitudcredito
                    txtEstadoContrato.Value = .EstadoContrato
                    txtclasificacion.Value = .ClasificacionBien
                    txtTipoBien.Value = .TipoBien
                    txtMoneda.Value = .Moneda
                    txtEstadoContrato.Value = .EstadoContrato
                    txtEjecutivo.Value = .EjecutivoBanca
                    txtBanca.Value = .Banca
                    txtKardex.Value = .Kardex
                    txtObservacionContrato.Value = .ObservacionContrato
                    txtFechaFinObra.Value = GCCUtilitario.CheckDateString2(.FechaProbableFinObra.ToString.Trim, "C")
                    txtFechaRealFinObra.Value = GCCUtilitario.CheckDateString2(.FechaRealFinObra.ToString.Trim, "C")
                    txtFechaInscripcionMunicipal.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString.Trim, "C")
                    ddlEstadoInscripcionMunicipal.Value = .CodEstadoMunicipal.Trim()
                    txtFechaEnvioNotaria.Value = GCCUtilitario.CheckDateString2(.FechaEnvioNotaria.ToString.Trim, "C")
                    txtFechaPropiedad.Value = GCCUtilitario.CheckDateString2(.FechaPropiedad.ToString.Trim, "C")
                    txtFechaInscripcionRegistral.Value = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString.Trim, "C")
                    ddlEstadoInscripcionRRPP.Value = .CodEstadoInscripcionRrPp.Trim()
                    txtOficinaRegistral.Value = .OficinaRegistral
                    ddlNotaria.Value = .CodigoNotaria.Trim()
                    'ddlPropiedad.Value = .CodEstadoTransferencia.Trim()
                    txtCUCliente.Value = .CodUnico
                    txtRazonSocial.Value = .RazonSocial
                    txtTipoDocumento.Value = .TipoDocumento
                    txtNumeroDocumento.Value = .NumeroDocumento
                    hidCodClasificacion.Value = .Tiporubrofinanciamiento
                    hidCodTipoBien.Value = .Codigotipobien.Trim
                    'IBK - RPH
                    hidPrecioVenta.Value = .Precioventa
                    'Fin
                End With
            End If


            'Dim objBienNTx As New LBienNTx
            'pNumeroContrato = txtNumeroContrato.Value
            'pCodEstadoLogico = "001"
            'Dim dtBienesContratoVehiculo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoVehiculos(pPageSize, _
            '                                                                                                                                pCurrentPage, _
            '                                                                                                                                pSortColumn, _
            '                                                                                                                                pSortOrder, _
            '                                                                                                                                pNumeroContrato, _
            '                                                                                                                                pCodEstadoLogico))
            'Dim dr As DataRow
            'Dim TotalValorBien As Integer
            'For Each dr In dtBienesContratoVehiculo.Rows
            '    TotalValorBien = TotalValorBien + IIf(dr("ValorBien") Is DBNull.Value, "0", dr("ValorBien"))
            'Next
            'hidTotal.Value = TotalValorBien


        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocBienNtx = Nothing
        End Try
    End Sub
    <WebMethod()> _
    Public Shared Function ListaBienContratoInmuebles(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCodEstadoLogico As String) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx
       


        Dim dtBienesContratoInmuebles As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoInmuebles(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pNumeroContraro, _
                                                                                                            pCodEstadoLogico))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtBienesContratoInmuebles.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtBienesContratoInmuebles.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtBienesContratoInmuebles.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienesContratoInmuebles)

    End Function
    <WebMethod()> _
    Public Shared Function ListaBienContratoVehiculos(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCodEstadoLogico As String) As JQGridJsonResponse

        Dim objBienNTx As New LBienNTx
        Dim dtBienesContratoVehiculo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoVehiculos(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pNumeroContraro, _
                                                                                                            pCodEstadoLogico))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtBienesContratoVehiculo.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtBienesContratoVehiculo.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtBienesContratoVehiculo.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienesContratoVehiculo)

    End Function
    <WebMethod()> _
    Public Shared Function ListadoBienContratoMaquinaria(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCodEstadoLogico As String) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx



        Dim dtBienesContratoMaqOtros As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoBienContratoMaquinaria(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pNumeroContraro, _
                                                                                                            pCodEstadoLogico))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtBienesContratoMaqOtros.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtBienesContratoMaqOtros.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtBienesContratoMaqOtros.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienesContratoMaqOtros)

    End Function
    <WebMethod()> _
Public Shared Function ListadoBienContratoSistemas(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pNumeroContraro As String, _
                                       ByVal pCodEstadoLogico As String) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx



        Dim dtBienesContratoOtros As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoBienContratoSistemas(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pNumeroContraro, _
                                                                                                            pCodEstadoLogico))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtBienesContratoOtros.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtBienesContratoOtros.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtBienesContratoOtros.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienesContratoOtros)

    End Function
    <WebMethod()> _
    Public Shared Function GuardarRrppBienContrato(ByVal pNumeroContrato As String, _
                                         ByVal pFechaProbableFinObra As String, _
                                         ByVal pFechaRealFinObra As String, _
                                         ByVal pFechaInscripcionMunicipal As String, _
                                         ByVal pFechaEnvioNotaria As String, _
                                         ByVal pFechaPropiedad As String, _
                                         ByVal pFechaInscripcionRegistral As String, _
                                         ByVal pOficinaRegistral As String, _
                                         ByVal pCodigoNotaria As String, _
                                         ByVal pCodEstadoInscripcionRrpp As String, _
                                         ByVal pCodEstadoMunicipal As String, _
                                         ByVal pCodEstadoTransferencia As String, _
                                         ByVal pObservacionContrato As String)

        Try
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim objLBien As New LBienTx
            Dim pESolicitudCredito As String

            '.FechaProbableFinObra = IIf(IsNothing(pFechaProbableFinObra), "", CDate(GCCUtilitario.StringToDateTime(pFechaProbableFinObra)).ToString("yyyy-MM-dd")).ToString()
            If objESolicitudCredito IsNot Nothing Then
                With objESolicitudCredito
                    .Codsolicitudcredito = pNumeroContrato
                    .FechaProbableFinObra = GCCUtilitario.StringToDateTime(pFechaProbableFinObra)
                    .FechaRealFinObra = GCCUtilitario.StringToDateTime(pFechaRealFinObra)
                    .FechaInscripcionMunicipal = GCCUtilitario.StringToDateTime(pFechaInscripcionMunicipal)
                    .FechaEnvioNotaria = GCCUtilitario.StringToDateTime(pFechaEnvioNotaria)
                    .FechaPropiedad = GCCUtilitario.StringToDateTime(pFechaPropiedad)
                    .FechaInscripcionRegistral = GCCUtilitario.StringToDateTime(pFechaInscripcionRegistral)
                    .OficinaRegistral = pOficinaRegistral
                    .CodigoNotaria = pCodigoNotaria
                    .CodEstadoInscripcionRrPp = pCodEstadoInscripcionRrpp
                    .CodEstadoMunicipal = pCodEstadoMunicipal
                    .CodEstadoTransferencia = pCodEstadoTransferencia
                    .ObservacionContrato = pObservacionContrato
                End With
            End If
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim blnResult As Boolean = objLBien.fblnModificarRRPPBienContrato(pESolicitudCredito)

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
        Public Shared Function ListaDocumentos(ByVal pPageSize As Integer, _
                                                          ByVal pCurrentPage As Integer, _
                                                          ByVal pSortColumn As String, _
                                                          ByVal pSortOrder As String, _
                                                          ByVal pCodigoContrato As String, _
                                                          ByVal pCodigoBien As Integer) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx

        Dim dtDocumentos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoDocumento(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   pCodigoContrato, _
                                                                                                                   pCodigoBien.ToString()))
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        'Dim total2 As Decimal
        If dtDocumentos.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtDocumentos.Rows(0)("RecordCount"))
            'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
            intTotalCurrent = Convert.ToInt32(dtDocumentos.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDocumentos)

    End Function

    ''' <summary>
    ''' Eliminar DocumentoCometario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaDocumentoComentario(ByVal pstrCodigoContrato As String, _
                                                         ByVal pstrCodigoDocumento As String _
                                                       ) As String

        ''Variables
        Dim objLBienTx As New LBienTx
        Dim strEGcc_biendocumento As String
        Try

            'Inicializa Objeto
            Dim objEGcc_contratodocumento As New EGcc_contratodocumento
            With objEGcc_contratodocumento
                .Numerocontrato = pstrCodigoContrato
                .Codigodocumento = pstrCodigoDocumento
                .SecFinanciamiento = 0
                .EstadoDocumento = 2
                .EstadoDocContrato = 1
                .EstadoDocBien = 0
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            strEGcc_biendocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(objEGcc_contratodocumento)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objLBienTx.ModificarBienContratoDocumento(strEGcc_biendocumento)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function

#End Region
End Class
