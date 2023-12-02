Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Mantenimiento_frmMntClienteAgregarAsignar
    Inherits GCCBase
    Dim objLog As New GCCLog("frmImpuestoVehicularAsigCheque.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 30/11/2012
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
                hidCodigoSup.Value = Request.QueryString("codSup")

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
    ''' Asigna Cheque
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2012
    ''' </remarks>

    <WebMethod()> _
    Public Shared Function BuscarContacto(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pCodigoSuprestatario As String, _
                                           ByVal pNombreC As String, _
                                           ByVal pCorreoC As String, _
                                           ByVal pTelefonoC As String) As JQGridJsonResponse
        Dim objUtilNTx As New LUtilNTX

        Dim dtSuprestatario As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarContactoSuprestatario(pPageSize, _
                                                                                                                    pCurrentPage, _
                                                                                                                    pSortColumn, _
                                                                                                                    pSortOrder, _
                                                                                                                    pCodigoSuprestatario, pNombreC, pCorreoC, pTelefonoC))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtSuprestatario.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtSuprestatario.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtSuprestatario.Rows(0)("TOTAL_PAGINA"))
        End If
        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSuprestatario)

    End Function

    <WebMethod()> _
    Public Shared Function GuardarContactoSuprestatario(ByVal pCodSuprestatario As String, _
                                         ByVal pNombre As String, _
                                         ByVal pCorreo As String, _
                                         ByVal pTelefono As String) As String
        Try

            Dim objLContacto As New LUtilTX


            Dim blnResult As Boolean = objLContacto.InsertarContactoSuprestatario(pCodSuprestatario, pNombre, pCorreo, pTelefono)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
           
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function ModificarContactoSuprestatario(ByVal pCodSupContacto As String, ByVal pCodSuprestatario As String, _
                                         ByVal pNombre As String, _
                                         ByVal pCorreo As String, _
                                         ByVal pTelefono As String, ByVal pEstado As String) As String
        Try

            Dim objLContacto As New LUtilTX


            Dim blnResult As Boolean = objLContacto.ModificarContactoSuprestatario(Convert.ToInt32(pCodSupContacto), pCodSuprestatario, pNombre, pCorreo, pTelefono, pEstado)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

        End Try
    End Function

    <WebMethod()> _
Public Shared Function AsignarContactoPreferente(ByVal pCodSupContacto As String, ByVal pCodSuprestatario As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String
        Try

            Dim objLContacto As New LUtilTX


            Dim blnResult As Boolean = objLContacto.InsertarContactoPreferente(Convert.ToInt32(pCodSupContacto), pCodSuprestatario, pNombre, pCorreo, pTelefono)

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception

        End Try
    End Function
#End Region

End Class
