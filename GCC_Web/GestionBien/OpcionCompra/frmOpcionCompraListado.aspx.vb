Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO

Partial Class GestionBien_OpcionCompra_frmOpcionCompraListado
    Inherits GCCBase
    Dim objLog As New GCCLog("frmOpcionCompraListado.aspx.vb")

#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/01/2013
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
                GCCUtilitario.CargarComboValorGenerico(ddlClasificacionbien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                CargarComboEnvio(ddlCartaEnviar)
                hidFechaActual.Value = DateTime.Now.ToShortDateString()
                'GCCUtilitario.CargarComboValorGenerico(ddlCartaEnviar, GCCConstante.C_TABLAGENERICA_OpcionCompra_TipoEnvio)
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

#Region "   Metodos     "
    Private Sub CargarComboEnvio(ByRef pDDLCombo As HtmlSelect)
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim odtbParam As DataTable

        Try
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_OpcionCompra_TipoEnvio))
            Dim dwParam As New DataView(odtbParam)
            dwParam.Sort = "VALOR5"


            For i As Integer = 0 To dwParam.Count - 1
                pDDLCombo.Items.Insert(i, New ListItem(dwParam(i).Item("DESCRIPCION").ToString(), GCCUtilitario.Concatenar(dwParam(i).Item("CODIGO").ToString(), "|", dwParam(i).Item("VALOR5").ToString())))
            Next
            pDDLCombo.Items.Insert(0, New ListItem("[-Seleccione-]", "0"))
        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub

#End Region

#Region "  Web Metodos  "

    ''' <summary>
    ''' Listar Opción de compra
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarOpcionCompra(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pNumeroContraro As String, _
                                               ByVal pCUCliente As String, _
                                               ByVal pRazonSocial As String, _
                                               ByVal pCodClasificacionBien As String, _
                                               ByVal pCodTipoBien As String, _
                                               ByVal pCodTipoEnvio As String, _
                                               ByVal pDemanda As String, _
                                               ByVal pPlacaActual As String, _
                                               ByVal pNroSerie As String, _
                                               ByVal pFechaFiltro As String) As JQGridJsonResponse
        Dim objOpcionCompraNTx As New LOpcionCompraNTx
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        With oEOpcionCompra
            .NumeroContrato = pNumeroContraro
            .CodigoUnico = pCUCliente
            .RazonSocial = pRazonSocial
            .ClasificacionBien = pCodClasificacionBien
            .TipoBien = pCodTipoBien
            .TipoEnvio = pCodTipoEnvio
            .Demanda = pDemanda
            .PlacaActual = pPlacaActual
            .NroSerie = pNroSerie
            .FechaFiltro = GCCUtilitario.CheckDate(pFechaFiltro)
        End With


        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.fobjListadoOpcionCompra(pPageSize, _
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
    ''' Listar todos los registros de opción de compra
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarOpcionCompraTodo(ByVal pNumeroContraro As String, _
                                                   ByVal pCUCliente As String, _
                                                   ByVal pRazonSocial As String, _
                                                   ByVal pCodClasificacionBien As String, _
                                                   ByVal pCodTipoBien As String, _
                                                   ByVal pCodTipoEnvio As String, _
                                                   ByVal pDemanda As String, _
                                                   ByVal pPlacaActual As String, _
                                                   ByVal pNroSerie As String, _
                                                   ByVal pFechaFiltro As String) As JQGridJsonResponse
        Dim objOpcionCompraNTx As New LOpcionCompraNTx
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        With oEOpcionCompra
            .NumeroContrato = pNumeroContraro
            .CodigoUnico = pCUCliente
            .RazonSocial = pRazonSocial
            .ClasificacionBien = pCodClasificacionBien
            .TipoBien = pCodTipoBien
            .TipoEnvio = pCodTipoEnvio
            .Demanda = pDemanda
            .PlacaActual = pPlacaActual
            .NroSerie = pNroSerie
            .FechaFiltro = GCCUtilitario.CheckDate(pFechaFiltro)
        End With


        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.fobjListadoOpcionCompraTodo(GCCUtilitario.SerializeObject(Of EGCC_OpcionCompra)(oEOpcionCompra)))

        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 20, dtContrato)

    End Function

    ''' <summary>
    ''' Graba Envio
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaEnvio(ByVal pstrCodTipoEnvio As String, _
                                      ByVal pstrOrdenEnvio As String, _
                                      ByVal pstrContratos As String) As String

        Try
            Dim sbXML As New StringBuilder
            Dim objEOpcionCompra As New EGCC_OpcionCompra

            Dim arrContratos As String() = pstrContratos.Split(New Char() {"*"c})
            For i As Integer = 0 To arrContratos.Length - 1
                If arrContratos(i) <> String.Empty Then
                    sbXML.Append(GCCUtilitario.Concatenar("<Contratos NumeroContrato=", Chr(39), arrContratos(i), Chr(39), " />"))
                End If
            Next

            With objEOpcionCompra
                .TipoEnvio = pstrCodTipoEnvio
                .OrdenEnvio = pstrOrdenEnvio
                .XMLEntity = GCCUtilitario.Concatenar("<Root>", sbXML.ToString(), "</Root>")
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
            End With

            'Ejecuta Transaccion
            Dim objLOpcionCompraTx As New LOpcionCompraTx
            Dim blnResult As Boolean = False

            blnResult = objLOpcionCompraTx.fblnInsertarOpcionCompraEnvio(GCCUtilitario.SerializeObject(objEOpcionCompra))

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
    ''' Enviar Carta
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - SCA
    ''' Fecha de Creación  : 16/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Sub EnviarCarta(ByVal pstrTipo As String, _
                                   ByVal pstrCodSolicitudCredito As String, _
                                   ByVal pstrRazonSocial As String, _
                                   ByVal pstrDireccion As String, _
                                   ByVal pstrUbigeo As String)


        Dim objgccbase As New GCCBase
        objgccbase.EnviarMailCarta1(pstrTipo, _
                                           pstrCodSolicitudCredito, _
                                           pstrRazonSocial, _
                                           pstrDireccion, _
                                           pstrUbigeo)


    End Sub


    ''' <summary>
    ''' Graba Envio carta
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaEnvioCarta(ByVal pstrCodTipoEnvio As String, _
                                          ByVal pstrOrdenEnvio As String, _
                                          ByVal pstrContratos As String) As String

        Try
            Dim sbXML As New StringBuilder
            Dim objEOpcionCompra As New EGCC_OpcionCompra
            Dim objOpcionCompraNTx As New LOpcionCompraNTx

            Dim arrContratos As String() = pstrContratos.Split(New Char() {"*"c})
            For i As Integer = 0 To arrContratos.Length - 1
                If arrContratos(i) <> String.Empty Then


                    'Consulta Datos
                    Dim oEOpcionCompra As New EGCC_OpcionCompra
                    With oEOpcionCompra
                        .NumeroContrato = arrContratos(i)
                    End With
                    Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.fobjListadoOpcionCompra(10, 1, "CodSolicitudCredito", "desc", GCCUtilitario.SerializeObject(Of EGCC_OpcionCompra)(oEOpcionCompra)))

                    'Genera Archivo
                    Dim objgccbase As New GCCBase
                    Dim strRutaDoc As String = String.Empty
                    Dim pstrMensaje As String = String.Empty
                    Dim pstrCarta As String = String.Empty

                    Dim pstrRazonSocial As String = String.Empty
                    Dim pstrDireccion As String = String.Empty
                    Dim pstrUbigeo As String = String.Empty

                    Select Case CInt(pstrCodTipoEnvio)
                        Case "1"
                            pstrCarta = "CARTA-OpcionCompra-xVencer.dot"
                        Case "2"
                            pstrCarta = "CARTA-OpcionCompra-xVencer.dot"
                        Case "3"
                            pstrCarta = "CARTA-OpcionCompra-SolDocumentos.dot"
                        Case "4"
                            pstrCarta = "CARTA-OpcionCompra-Vencidos.dot"
                    End Select

                    If dtContrato IsNot Nothing Then
                        If dtContrato.Rows.Count > 0 Then

                            pstrRazonSocial = dtContrato.Rows(0).Item("ClienteRazonSocial").ToString
                            pstrDireccion = dtContrato.Rows(0).Item("ClienteDomicilioLegal").ToString
                            pstrUbigeo = dtContrato.Rows(0).Item("Ubigeo").ToString


                        End If
                    End If

                    strRutaDoc = objgccbase.CartaOpcionCompra(pstrCodTipoEnvio, _
                                                           arrContratos(i), _
                                                           pstrRazonSocial, _
                                                           pstrDireccion, _
                                                           pstrUbigeo, _
                                                           pstrCarta, _
                                                           pstrMensaje)

                    sbXML.Append(GCCUtilitario.Concatenar("<Contratos NumeroContrato=", Chr(39), arrContratos(i), Chr(39), " Archivo=", Chr(39), strRutaDoc, Chr(39), "  />"))

                    objgccbase.EnviarMailCarta(pstrCodTipoEnvio, _
                                               arrContratos(i), _
                                               pstrRazonSocial, _
                                               pstrDireccion, _
                                               pstrUbigeo, _
                                               strRutaDoc)

                End If
            Next

            With objEOpcionCompra
                .TipoEnvio = pstrCodTipoEnvio
                .OrdenEnvio = pstrOrdenEnvio
                .XMLEntity = GCCUtilitario.Concatenar("<Root>", sbXML.ToString(), "</Root>")
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
            End With

            'Ejecuta Transaccion
            Dim objLOpcionCompraTx As New LOpcionCompraTx
            Dim blnResult As Boolean = False

            blnResult = objLOpcionCompraTx.fblnInsertarOpcionCompraEnvio(GCCUtilitario.SerializeObject(objEOpcionCompra))

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

#End Region
End Class
