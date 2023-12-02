Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_frmGestionBienDocListado
    Inherits GCCBase
    Dim objLog As New GCCLog("frmSiniestroListado.aspx.vb")

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

            If Not Page.IsPostBack Then

                'Verifica Edicion
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                Dim strCodRelacionado As String = Request.QueryString("hddCodRelacionado")
                Dim strCodTipo As String = Request.QueryString("hddCodTipo")
                Dim strVer As String = Request.QueryString("hddVer")

                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodRelacionado.Value = strCodRelacionado
                Me.hddCodTipo.Value = strCodTipo
                Me.hddVer.Value = strVer
            
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
    ''' Listado de Documentos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/12/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoDocumentos(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pstrCodContrato As String, _
                                                 ByVal pstrCodBien As String, _
                                                 ByVal pstrCodRelacionado As String, _
                                                 ByVal pstrCodTipo As String _
                                               ) As JQGridJsonResponse

        'Variables
        Dim objLGestionBienDocNTx As New LGestionBienDocNTx

        Try

            'Inicializa Objeto
            Dim objEGestionBienDoc As New EGCC_GestionBienDoc
            Dim strEGestionBienDoc As String
            With objEGestionBienDoc
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .SecFinanciamiento = GCCUtilitario.CheckInt(pstrCodBien)
                .CodRelacionado = GCCUtilitario.NullableString(pstrCodRelacionado)
                .CodTipoModulo = GCCUtilitario.NullableString(pstrCodTipo)
            End With
            strEGestionBienDoc = GCCUtilitario.SerializeObject(objEGestionBienDoc)

            'Ejecuta Consulta
            Dim dtDocumentos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLGestionBienDocNTx.ListadoGestionBienDoc(pPageSize, _
                                                                                                                                       pCurrentPage, _
                                                                                                                                       pSortColumn, _
                                                                                                                                       pSortOrder, _
                                                                                                                                       strEGestionBienDoc))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtDocumentos.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(dtDocumentos.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDocumentos)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Eliminar DocumentoCometario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminarGestionBienDoc(ByVal pstrCodContrato As String, _
                                                     ByVal pstrCodBien As String, _
                                                     ByVal pstrCodRelacionado As String, _
                                                     ByVal pstrCodDocumento As String, _
                                                     ByVal pstrCodTipo As String _
                                                   ) As String

        ''Variables
        Dim objLGestionBienDocTx As New LGestionBienDocTx

        Try

            'Inicializa Objeto
            Dim objEGestionBienDoc As New EGCC_GestionBienDoc
            Dim strEGestionBienDoc As String
            With objEGestionBienDoc
                .CodSolicitudCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .SecFinanciamiento = GCCUtilitario.CheckInt(pstrCodBien)
                .CodRelacionado = GCCUtilitario.NullableString(pstrCodRelacionado)
                .Codigodocumento = GCCUtilitario.CheckInt(pstrCodDocumento)
                .CodTipoModulo = GCCUtilitario.NullableString(pstrCodTipo)

                .Audestadologico = 0
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            strEGestionBienDoc = GCCUtilitario.SerializeObject(objEGestionBienDoc)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objLGestionBienDocTx.EliminarGestionBienDoc(strEGestionBienDoc)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Metodos"

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region

End Class
