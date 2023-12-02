Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Formalizacion_CesionContrato_frmRepresentanteRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCesionarioRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 04/01/2013
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
                Dim strCodCesionario As String = Request.QueryString("hddCodCesionario")

                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodCesionario.Value = strCodCesionario

                Me.hddTipoTx.Value = GCCConstante.C_TX_NUEVO

                GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoDoc, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)

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
    ''' Graba Representante
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaRepresentante(ByVal pstrTipoTx As String, _
                                            ByVal pstrCodContrato As String, _
                                            ByVal pstrCodCesionario As String, _
                                            ByVal pstrCodRepresentante As String, _
                                            ByVal pstrNombreCompleto As String, _
                                            ByVal pstrTipoDoc As String, _
                                            ByVal pstrNroDocumento As String, _
                                            ByVal pstrNroPartida As String, _
                                            ByVal pstrOfRegistral As String) As String

        Try
            Dim objERepresentanteCes As New EGCC_RepresentanteCes

            With objERepresentanteCes
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .CodCesionario = GCCUtilitario.CheckInt(pstrCodCesionario)
                .Codigorepresentante = GCCUtilitario.CheckInt(pstrCodRepresentante)
                .Nombrerepresentante = GCCUtilitario.NullableString(pstrNombreCompleto)
                .CodigoTipoDocumento = GCCUtilitario.NullableString(pstrTipoDoc)
                .Nrodocumento = GCCUtilitario.NullableString(pstrNroDocumento)
                .Partidaregistral = GCCUtilitario.NullableString(pstrNroPartida)
                .Oficinaregistral = GCCUtilitario.NullableString(pstrOfRegistral)
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .EstadoLogico = "1"
            End With

            'Ejecuta Transaccion
            Dim objLCesionarioTx As New LCesionarioTx

            Dim blnResult As Boolean = False
            Dim strCodRepresentante As String = ""
            If pstrTipoTx.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strCodRepresentante = objLCesionarioTx.InsertarRepresentante(GCCUtilitario.SerializeObject(objERepresentanteCes))
                blnResult = True
            Else
                objLCesionarioTx.ModificarRepresentante(GCCUtilitario.SerializeObject(objERepresentanteCes))
                strCodRepresentante = pstrCodCesionario
                blnResult = True
            End If

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
    ''' ListaRepresentates
    ''' </summary>
    ''' <returns>Listado de Representantes</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaRepresentates(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pstrCodContrato As String, _
                                                 ByVal pstrCodCesionario As String) As JQGridJsonResponse

        'Variables
        Dim objCesionarioNTx As New LCesionarioNTx

        Try

            'Inicializa Objeto
            Dim objRepresentanteCes As New EGCC_RepresentanteCes
            Dim strERepresentanteCes As String
            With objRepresentanteCes
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .CodCesionario = GCCUtilitario.CheckInt(pstrCodCesionario)
            End With
            strERepresentanteCes = GCCUtilitario.SerializeObject(Of EGCC_RepresentanteCes)(objRepresentanteCes)

            'Ejecuta Consulta
            Dim dtRepresentates As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCesionarioNTx.ListadoRepresentante(pPageSize, _
                                                                                                                               pCurrentPage, _
                                                                                                                               pSortColumn, _
                                                                                                                               pSortOrder, _
                                                                                                                               strERepresentanteCes))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtRepresentates.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtRepresentates.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtRepresentates.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtRepresentates)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Eliminar Representate
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 09/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaRepresentate(ByVal hddCodContrato As String, _
                                                ByVal hddCodCesionario As String, _
                                                ByVal hddCodRepresentante As String) As String

        ''Variables
        Dim objCesionarioTx As New LCesionarioTx

        Try

            'Inicializa Objeto
            Dim objERepresentanteCes As New EGCC_RepresentanteCes
            Dim strERepresentanteCes As String
            With objERepresentanteCes
                .Codsolcredito = GCCUtilitario.NullableString(hddCodContrato)
                .CodCesionario = GCCUtilitario.CheckInt(hddCodCesionario)
                .Codigorepresentante = GCCUtilitario.CheckInt(hddCodRepresentante)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .EstadoLogico = "0"
            End With
            strERepresentanteCes = GCCUtilitario.SerializeObject(Of EGCC_RepresentanteCes)(objERepresentanteCes)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objCesionarioTx.EliminarRepresentante(strERepresentanteCes)

            'Valida Resultado
            If blnResultado Then
                Return "1"
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
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
