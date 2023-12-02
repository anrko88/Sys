
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports GCC.Entity.EGcc_contratodocumento

Partial Class GestionBien_OpcionCompra_frmObservacionOC
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmObservacionOC.aspx.vb")

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
                hddCodOpcComDoc.Value = Request.QueryString("hddCodOpcComDoc").ToString.Trim()
                hddCodContrato.Value = Request.QueryString("hddCodContrato").ToString.Trim()
                hddCodCheck.Value = Request.QueryString("hddCodCheck").ToString.Trim()
                pInicializarControles()
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

#Region "Métodos"

    Private Sub pInicializarControles()

        Try
            Dim objEOpcionCompra As New EGCC_OpcionCompra
            Dim objLOpcionCompra As New LOpcionCompraNTx
            Dim strEOpcionCompra As String = String.Empty


            With objEOpcionCompra
                .CodOperacionDocumento = GCCUtilitario.CheckInt(hddCodOpcComDoc.Value)
                .NumeroContrato = GCCUtilitario.CheckStr(hddCodContrato.Value)
            End With

            Dim dtObservacion As New DataTable
            Dim strCadena As String = String.Empty
            strEOpcionCompra = GCCUtilitario.SerializeObject(objEOpcionCompra)
            dtObservacion = GCCUtilitario.DeserializeObject(Of DataTable)(objLOpcionCompra.ListadoOpcionCompraDocumentoObservacion(strEOpcionCompra))

            If dtObservacion.Rows.Count > 0 Then
                TxaObs.Value = dtObservacion.Rows(0).Item("Observacion").ToString()

                For Each row As DataRow In dtObservacion.Rows
                    If IsDBNull(row.Item("AudFechaRegistro")) Then
                        strCadena = strCadena + " " + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"
                    Else
                        strCadena = strCadena + " " + "<strong>" + Convert.ToDateTime(row.Item("AudFechaRegistro")).ToShortDateString() + " " + Convert.ToDateTime(row.Item("AudFechaRegistro")).ToShortTimeString() + "</strong>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Item("AudUsuarioRegistro").ToString() + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"

                    End If

                    'cadena = cadena + " " + "<strong>" + Convert.ToDateTime(row.Item("Audfecharegistro")).ToShortDateString() + "</strong>" + " " + "</br>" + " " + row.Item("Observacion").ToString() + "</br>" + "</br>"
                Next
                dv_historial.InnerHtml = "<p align=left>" + strCadena + "</p>"
            End If


        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

#End Region

#Region "Web Metodos"
    ''' <summary>
    ''' Inserta Observacion Documento
    ''' </summary>
    ''' <param name="pstrNumeroContrato"></param>
    ''' <param name="pstrCodOpcionCompraDocumento"></param>
    ''' <param name="pstrCodCheckList"></param>
    ''' <param name="pstrObservacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function InsertaObservacionDocumento(ByVal pstrNumeroContrato As String, _
                                                       ByVal pstrCodOpcionCompraDocumento As String, _
                                                       ByVal pstrCodCheckList As String, _
                                                       ByVal pstrObservacion As String) As String


        Try
            Dim objLOpcionCompra As New LOpcionCompraTx
            Dim objEOpcionCompra As New EGCC_OpcionCompra
            Dim strEOpcionCompra As String = String.Empty

            With objEOpcionCompra
                .CodOperacionDocumento = GCCUtilitario.CheckInt(pstrCodOpcionCompraDocumento)
                .NumeroContrato = pstrNumeroContrato
                .CodCheckList = pstrCodCheckList
                .Observacion = GCCUtilitario.NullableString(pstrObservacion)                
                .AudUsuarioRegistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)                
            End With

            strEOpcionCompra = GCCUtilitario.SerializeObject(objEOpcionCompra)
            Dim intResult As Boolean = objLOpcionCompra.fblnInsertarOpcionCompraDocumentoObservacion(strEOpcionCompra)


            If intResult = 0 Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
