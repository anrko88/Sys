Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_OpcionCompra_frmListadoBienes
    Inherits GCCBase
    Dim objLog As New GCCLog("frmListadoBienes.aspx.vb")

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
                hddCodContrato.Value = Request.QueryString("csc")                
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

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region

#Region "  Web Metodos  "

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
    ''' ListaDemanda
    ''' </summary>
    ''' <returns>Listado de Demanda</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaDemanda(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pstrCodContrato As String, _
                                         ByVal pstrCodBien As String) As JQGridJsonResponse

        'Variables
        Dim objDemandaNTx As New LDemandaNTx

        Try

            'Inicializa Objeto
            Dim objDemanda As New EGcc_Demanda
            Dim strEDemanda As String
            With objDemanda
                .CodSolCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .SecFinanciamiento = GCCUtilitario.StringToInteger(pstrCodBien)
            End With
            strEDemanda = GCCUtilitario.SerializeObject(Of EGcc_Demanda)(objDemanda)

            'Ejecuta Consulta
            Dim dtSiniestro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objDemandaNTx.ListadoDemanda(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       strEDemanda))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtSiniestro.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtSiniestro.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtSiniestro.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSiniestro)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Graba Aprobación de Bienes
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaAprobacion(ByVal pstrNumeroContrato As String, _
                                           ByVal pstrBienes As String) As String

        Try
            Dim sbXML As New StringBuilder
            Dim objEOpcionCompra As New EGCC_OpcionCompra
            Dim arrBienes As String() = pstrBienes.Split(New Char() {"*"c})
            For i As Integer = 0 To arrBienes.Length - 1
                If arrBienes(i) <> String.Empty Then
                    sbXML.Append(GCCUtilitario.Concatenar("<Bienes SecFinanciamiento=", Chr(39), arrBienes(i), Chr(39), " />"))
                End If
            Next

            With objEOpcionCompra
                .NumeroContrato = pstrNumeroContrato
                .XMLEntity = GCCUtilitario.Concatenar("<Root>", sbXML.ToString(), "</Root>")
                .AudUsuarioRegistro = GCCSession.CodigoUsuario
            End With

            'Ejecuta Transaccion
            Dim objLOpcionCompraTx As New LOpcionCompraTx
            Dim blnResult As Boolean = False

            blnResult = objLOpcionCompraTx.fblnAprobacionBien(GCCUtilitario.SerializeObject(objEOpcionCompra))

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
