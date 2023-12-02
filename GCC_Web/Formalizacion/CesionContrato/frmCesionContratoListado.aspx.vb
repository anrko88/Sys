﻿Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Formalizacion_CesionContrato_frmCesionContratoListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCesionContratoListado.aspx.vb")


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

                GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoDoc, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoContrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmdClasificacion, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)

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
    ''' ListaCesionContrato
    ''' </summary>
    ''' <returns>Listado de CesionContrato</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaCesionContrato(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pstrNroContrato As String, _
                                                 ByVal pstrCUCliente As String, _
                                                 ByVal pstrRazonSocial As String, _
                                                 ByVal pstrTipoDoc As String, _
                                                 ByVal pstrNroDocumento As String, _
                                                 ByVal pstrClasificacion As String, _
                                                 ByVal pstrEstadoContrato As String, _
                                                 ByVal pstrCesionPosicion As String) As JQGridJsonResponse

        'Variables
        Dim objCesionContratoNTx As New LCesionContratoNTx

        Try

            'Valida Campos            
            Dim strCodUnico As String = GCCUtilitario.NullableString(pstrCUCliente)
            If Not strCodUnico Is Nothing Then
                strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
            End If


            'Inicializa Objeto
            Dim objECesionContrato As New EGCC_CesionContrato
            Dim strECesionContrato As String
            With objECesionContrato
                .NroContrato = GCCUtilitario.NullableString(pstrNroContrato)
                .EstadoContrato = GCCUtilitario.NullableStringCombo(pstrEstadoContrato)
                .CUCliente = GCCUtilitario.NullableString(pstrCUCliente)
                .TipoDocumento = GCCUtilitario.NullableStringCombo(pstrTipoDoc)
                .NroDocumento = GCCUtilitario.NullableString(pstrNroDocumento)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .ClasificacionBien = GCCUtilitario.NullableStringCombo(pstrClasificacion)
                .CesionPosicion = GCCUtilitario.NullableStringCombo(pstrCesionPosicion)
            End With
            strECesionContrato = GCCUtilitario.SerializeObject(Of EGCC_CesionContrato)(objECesionContrato)

            'Ejecuta Consulta
            Dim dtCesionContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCesionContratoNTx.ListadoCesionContrato(pPageSize, _
                                                                                                                                       pCurrentPage, _
                                                                                                                                       pSortColumn, _
                                                                                                                                       pSortOrder, _
                                                                                                                                       strECesionContrato))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCesionContrato.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtCesionContrato.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtCesionContrato.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCesionContrato)

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
