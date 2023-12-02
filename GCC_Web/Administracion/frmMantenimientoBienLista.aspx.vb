﻿Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Administracion_frmMantenimientoBienLista
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMantenimientoBienLista.aspx.vb")

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
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(ddlClasificacionbien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarDepartamento(ddlDepartamento)
                GCCUtilitario.CargarComboValorGenerico(ddlEstado, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
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

#Region "Métodos"
    <WebMethod()> _
    Public Shared Function BuscarBien(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pCodClasificacionBien As String, _
                                           ByVal pCodTipoBien As String, _
                                           ByVal pCodEstadoBien As String, _
                                           ByVal pCodDepartamento As String, _
                                           ByVal pFechaTransferencia As String) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        With oESolicitudCreditoEstructura
            .NumeroContrato = pNumeroContraro
            .CodUnico = pCUCliente
            .RazonSocial = pRazonSocial
            .Tiporubrofinanciamiento = pCodClasificacionBien
            .Codigotipobien = pCodTipoBien
            .Codigoestadobien = pCodEstadoBien
            .Departamento = pCodDepartamento
            If pFechaTransferencia = String.Empty Then
                .FechaTransferencia = "19000101"
            Else
                .FechaTransferencia = pFechaTransferencia
            End If

        End With


        Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoBien(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtProveedor.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtProveedor.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtProveedor.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtProveedor)

    End Function
#End Region
End Class