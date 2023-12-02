Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Consultas_Siniestro_frmSiniestroConsulta
    Inherits GCCBase

    Dim objLog As New GCCLog("frmSiniestroConsulta.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2011
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

                'Verifica Edicion
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                If Not strCodBien Is Nothing Then

                    CargaEditarSiniestroContrato(strCodContrato, strCodBien)

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

#End Region

#Region "WebMethods"

    ''' <summary>
    ''' ListaSiniestro
    ''' </summary>
    ''' <returns>Listado de Siniestro</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaSiniestro(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pstrCodContrato As String, _
                                         ByVal pstrCodBien As String) As JQGridJsonResponse

        'Variables
        Dim objSiniestroNTx As New LSiniestroNTx

        Try

            'Inicializa Objeto
            Dim objESiniestro As New ESiniestro
            Dim strESiniestro As String
            With objESiniestro
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.StringToInteger(pstrCodBien)
                .CodTipoSiniestro = "001"
            End With
            strESiniestro = GCCUtilitario.SerializeObject(Of ESiniestro)(objESiniestro)

            'Ejecuta Consulta
            Dim dtSiniestro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestroNTx.ListadoSiniestro(pPageSize, _
                                                                                                                           pCurrentPage, _
                                                                                                                           pSortColumn, _
                                                                                                                           pSortOrder, _
                                                                                                                           strESiniestro))
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

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Get Siniestro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 13/11/2012
    ''' </remarks>
    Protected Sub CargaEditarSiniestroContrato(ByVal pstrCodContrato As String, ByVal strCodBien As String)

        Try

            Dim objSiniestroNTx As New LSiniestroNTx
            Dim msgError As String = ""

            'Inicializa Objeto
            Dim objESiniestro As New ESiniestro
            Dim strESiniestro As String
            With objESiniestro
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.CheckInt(strCodBien)
            End With
            strESiniestro = GCCUtilitario.SerializeObject(objESiniestro)

            'Ejecuta Consulta
            Dim dtSiniestro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestroNTx.GetSiniestroContrato(strESiniestro))

            'Valida si existe
            If dtSiniestro.Rows.Count > 0 Then

                Me.hddCodContrato.Value = dtSiniestro.Rows(0).Item("CodSolicitudCredito").ToString
                Me.hddCodBien.Value = dtSiniestro.Rows(0).Item("SecFinanciamiento").ToString
                Me.hddCodUnico.Value = dtSiniestro.Rows(0).Item("CodUnico").ToString

                Me.tdNroContrato.InnerHtml = dtSiniestro.Rows(0).Item("CodSolicitudCredito").ToString
                Me.tdCUCliente.InnerHtml = dtSiniestro.Rows(0).Item("CodUnico").ToString
                Me.tdRazonSocial.InnerHtml = dtSiniestro.Rows(0).Item("ClienteRazonSocial").ToString
                Me.tdEstadoContrato.InnerHtml = dtSiniestro.Rows(0).Item("EstadoContrato").ToString
                Me.tdClasificacionBien.InnerHtml = dtSiniestro.Rows(0).Item("ClasificacionBien").ToString
                Me.tdTipoBien.InnerHtml = dtSiniestro.Rows(0).Item("TipoBien").ToString
                Me.tdMoneda.InnerHtml = dtSiniestro.Rows(0).Item("NombreMoneda").ToString
                Me.tdValorBien.InnerHtml = GCCUtilitario.CheckDecimal(dtSiniestro.Rows(0).Item("ValorBien").ToString).ToString(GCCConstante.C_FormatMiles)
                Me.tdFecTransferencia.InnerHtml = dtSiniestro.Rows(0).Item("FechaTransferencia").ToString

                Me.tdPlaca.InnerHtml = dtSiniestro.Rows(0).Item("Placa").ToString
                Me.tdDescripcion.InnerHtml = dtSiniestro.Rows(0).Item("Comentario").ToString

                Me.tdUbicacionCab.InnerHtml = dtSiniestro.Rows(0).Item("Ubicacion").ToString
                Me.tdEjecutivoBanca.InnerHtml = dtSiniestro.Rows(0).Item("DesEjecutivoBanca").ToString
                Me.tdEjecutivoLeasing.InnerHtml = dtSiniestro.Rows(0).Item("NombreEjecutivoleasing").ToString

            End If

        Catch ex As Exception
            Throw ex
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

End Class

