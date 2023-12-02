Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Desembolso_frmBienBusqueda
    Inherits GCCBase

    Dim objLog As New GCCLog("frmBienBusqueda.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            If Not Page.IsPostBack Then

                Me.hddCodSolicitudCredito.Value = Request.QueryString("csc") '"00000001"
                'Me.hddCodigoDocumento.Value = Request.QueryString("cdc") '"0"
                hidCodProveedor.Value = Request.QueryString("cp")
                hidTipoDocumento.Value = Request.QueryString("ctd")
                hidNumeroDocumento.Value = Request.QueryString("cnd").ToString.Trim
                hidFecEmision.Value = Request.QueryString("cfe")
                Me.hddClasificacionBien.Value = Request.QueryString("clb") '"002"
                Me.hddEstadoDocumento.Value = Request.QueryString("CEstado")
                Me.hddTipoSubcontrato.Value = Request.QueryString("ctipoSubcontrato")

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

#Region "WebMethods"

    <WebMethod()> _
    Public Shared Function ListaBienesContrato(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pstrNroContrato As String, _
                                         ByVal pstrCodProveedor As String, _
                                         ByVal pstrNumeroTipo As String, _
                                         ByVal pstrNumeroDoc1 As String, _
                                         ByVal pstrFecEmision As String, _
                                         ByVal pstrCodigoTipoBien As String) As JQGridJsonResponse

        'Variables
        Dim objLDesembolsoNTx As New LDesembolsoNTx
        Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
        Try
            With oEContratoEstructDoc
                .Codsolicitudcredito = pstrNroContrato
                .CodProveedor = pstrCodProveedor
                .Tipodocumento = pstrNumeroTipo
                .Nrodocumento = pstrNumeroDoc1
                .StringFechaEmision = pstrFecEmision
                .CodigoTipoBien = pstrCodigoTipoBien
            End With

            'Ejecuta Consulta
            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                            objLDesembolsoNTx.ListadoDocumentoBienes(pPageSize, _
                                                                                   pCurrentPage, _
                                                                                   pSortColumn, _
                                                                                   pSortOrder, _
                                                                                   GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc)))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtBienes.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = dtBienes.Rows.Count 'Convert.ToInt32(dtBienes.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienes)

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' AsociaBien
    ''' </summary>
    ''' <param name="strNumContrato"></param>
    ''' <param name="pstrCodProveedor"></param>
    ''' <param name="pstrNumeroTipo"></param>
    ''' <param name="pstrNumeroDoc1"></param>
    ''' <param name="pstrFecEmision"></param>
    ''' <param name="strCodigoBien"></param>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Sub AsociaBien(ByVal strNumContrato As String, _
                                 ByVal pstrCodProveedor As String, _
                                 ByVal pstrNumeroTipo As String, _
                                 ByVal pstrNumeroDoc1 As String, _
                                 ByVal pstrFecEmision As String, _
                                 ByVal strCodigoBien As String)

        Dim oEContratoEstructDocDet As New ESolicitudcreditoestructuradocdet
        Dim oLDesembolsoTx As New LDesembolsoTx
        Try
            With oEContratoEstructDocDet
                .Codsolicitudcredito = strNumContrato
                .Codproveedor = pstrCodProveedor
                .Tipodocumento = pstrNumeroTipo
                .Nrodocumento = pstrNumeroDoc1
                '.Fechaemision = GCCUtilitario.StringToDateTime(pstrFecEmision)
                .Secfinanciamiento = strCodigoBien
                .StringFechaemision = pstrFecEmision
            End With

            Dim blnResult As Boolean = oLDesembolsoTx.InsertarDesembolsoBien(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradocdet)(oEContratoEstructDocDet))
        Catch ex As Exception
            Throw ex
        Finally
            oLDesembolsoTx = Nothing
        End Try

    End Sub

    <WebMethod()> _
    Public Shared Sub DesasociaBien(ByVal strNumContrato As String, ByVal pstrCodProveedor As String, _
                                 ByVal pstrNumeroTipo As String, _
                                 ByVal pstrNumeroDoc1 As String, _
                                 ByVal pstrFecEmision As String, _
                                 ByVal strCodigoBien As String)

        Dim oEContratoEstructDocDet As New ESolicitudcreditoestructuradocdet
        Dim oLDesembolsoTx As New LDesembolsoTx
        Try
            With oEContratoEstructDocDet
                .Codsolicitudcredito = strNumContrato
                .Secfinanciamiento = strCodigoBien
                .Codproveedor = pstrCodProveedor
                .Tipodocumento = pstrNumeroTipo
                .Nrodocumento = pstrNumeroDoc1
                '.Fechaemision = GCCUtilitario.StringToDateTime(pstrFecEmision)
                .StringFechaemision = pstrFecEmision
            End With

            Dim blnResult As Boolean = oLDesembolsoTx.EliminarDesembolsoBien(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradocdet)(oEContratoEstructDocDet))
        Catch ex As Exception
            Throw ex
        Finally
            oLDesembolsoTx = Nothing
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

#End Region

End Class
