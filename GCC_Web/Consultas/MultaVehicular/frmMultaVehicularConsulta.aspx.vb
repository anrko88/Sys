Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_MultaVehicular_frmMultaVehicularConsulta
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMultaVehicularConsulta.aspx.vb")

#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 09/11/2012
    '''  </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                GCCUtilitario.CargarMunicipalidad(Me.ddlMunicipalidad)

                tdPlaca.InnerHtml = Request.QueryString("placa")
                hddPlaca.Value = Request.QueryString("placa")
                hddTipo.Value = Request.QueryString("tipo")
                hddEstadoPago.Value = Request.QueryString("EstPago")
                hddFecTransferencia.Value = Request.QueryString("FecTrans")
                If hddTipo.Value = "" Then
                    hddTipo.Value = "0"
                End If
                If hddTipo.Value = "1" Then
                    hddCodMulta.Value = Request.QueryString("codMulta")
                    hddCodMunicipalidad.Value = Request.QueryString("codMunicipalidad")
                End If
                GCCUtilitario.SeleccionaCombo(Me.ddlMunicipalidad, hddCodMunicipalidad.Value.Trim)
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

#Region "   Métodos     "
    <WebMethod()> _
    Public Shared Function ListaBienesVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pCodMunicipalidad As String, _
                                           ByVal pPlaca As String, _
                                           ByVal pTipo As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX


        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarBienMultaVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pCodMunicipalidad, _
                                                                                                            pPlaca, pTipo, _
                                                                                                            "", "", ""))


        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtImpuestoVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpuestoVehicular)

    End Function
    <WebMethod()> _
    Public Shared Function ListarLoteMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pCodMunicipalidad As String, _
                                   ByVal pTipo As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX


        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarLoteMultaVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pPlaca, pCodMunicipalidad, Convert.ToInt32(pTipo), ""))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtImpuestoVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpuestoVehicular)

    End Function
    '<WebMethod()> _
    'Public Shared Function EliminarMultaVehicular(ByVal pstrCodigosImpuestos As String)

    '    Try

    '        Dim objLMultaVehicular As New LImpuestoVehicularTX


    '        Dim blnResult As Boolean = objLMultaVehicular.EliminarMultaVehicular(pstrCodigosImpuestos, GCCSession.CodigoUsuario)


    '        If blnResult Then
    '            Return "0"
    '        Else
    '            Return "1"
    '        End If
    '    Catch ex As Exception
    '        Dim myException As String
    '        myException = ManageException(ex)
    '        Return myException
    '    End Try
    'End Function

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
    ''' <summary>
    ''' Generar Nro de Lote
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GenerarLote(ByVal strCodigoImpuesto As String, _
                                                    ByVal strNroLote As String) As String

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .CodigosImpuesto = strCodigoImpuesto
                    .CodNroLote = strNroLote
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteMultaVehicular(pEImpuestoVehicular)

            Return strNroLoteGenerado.Trim.PadLeft(8, "0"c)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region
End Class
