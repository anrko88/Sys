Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_Demanda_frmDemandaRegistro
    Inherits GCCBase


    Dim objLog As New GCCLog("frmSiniestroMnt.aspx.vb")

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

                    CargaEditarDemandaContrato(strCodContrato, strCodBien)

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
    ''' Eliminar Demanda
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 15/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaDemanda(ByVal hddCodContrato As String, _
                                          ByVal hddCodBien As String, _
                                          ByVal hddCodDemanda As String _
                                          ) As String

        ''Variables
        Dim objDemandaTx As New LDemandaTx

        Try

            'Inicializa Objeto
            Dim objEDemanda As New EGcc_Demanda
            Dim strEDemanda As String
            With objEDemanda
                .CodSolCredito = GCCUtilitario.NullableString(hddCodContrato)
                .SecFinanciamiento = GCCUtilitario.StringToInteger(hddCodBien)
                .CodDemanda = GCCUtilitario.StringToInteger(hddCodDemanda)
                .UsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .EstadoLogico = 0
            End With
            strEDemanda = GCCUtilitario.SerializeObject(Of EGcc_Demanda)(objEDemanda)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objDemandaTx.EliminarDemanda(strEDemanda)

            Return ""

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
    Protected Sub CargaEditarDemandaContrato(ByVal pstrCodContrato As String, ByVal strCodBien As String)

        Try

            Dim objDemandaNTx As New LDemandaNTx
            Dim msgError As String = ""

            'Inicializa Objeto
            Dim objEDemanda As New EGcc_Demanda
            Dim strEDemanda As String
            With objEDemanda
                .CodSolCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .SecFinanciamiento = GCCUtilitario.CheckInt(strCodBien)
            End With
            strEDemanda = GCCUtilitario.SerializeObject(objEDemanda)

            'Ejecuta Consulta
            Dim dtDemanda As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objDemandaNTx.GetDemandaContrato(strEDemanda))

            'Valida si existe
            If dtDemanda.Rows.Count > 0 Then

                Me.hddCodContrato.Value = dtDemanda.Rows(0).Item("CodSolicitudCredito").ToString
                Me.hddCodBien.Value = dtDemanda.Rows(0).Item("SecFinanciamiento").ToString
                Me.hddCodUnico.Value = dtDemanda.Rows(0).Item("CodUnico").ToString

                Me.txtNroContrato.Value = dtDemanda.Rows(0).Item("CodSolicitudCredito").ToString
                Me.txtCUCliente.Value = dtDemanda.Rows(0).Item("CodUnico").ToString
                Me.txtRazonSocial.Value = dtDemanda.Rows(0).Item("ClienteRazonSocial").ToString
                Me.txtEstadoContrato.Value = dtDemanda.Rows(0).Item("EstadoContrato").ToString
                Me.txtClasificacionBien.Value = dtDemanda.Rows(0).Item("ClasificacionBien").ToString
                Me.txtTipoBien.Value = dtDemanda.Rows(0).Item("TipoBien").ToString
                Me.txtMoneda.Value = dtDemanda.Rows(0).Item("NombreMoneda").ToString
                Me.txtValorBien.Value = dtDemanda.Rows(0).Item("ValorBien").ToString
                Me.txtFecTransferencia.Value = dtDemanda.Rows(0).Item("FechaTransferencia").ToString

                Me.txtPlaca.Value = dtDemanda.Rows(0).Item("Placa").ToString
                Me.txtDescripcion.Value = dtDemanda.Rows(0).Item("Comentario").ToString

                Me.txtUbicacionCab.Value = dtDemanda.Rows(0).Item("Ubicacion").ToString
                Me.txtEjecutivoBanca.Value = dtDemanda.Rows(0).Item("DesEjecutivoBanca").ToString
                Me.txtEjecutivoLeasing.Value = dtDemanda.Rows(0).Item("NombreEjecutivoleasing").ToString

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
