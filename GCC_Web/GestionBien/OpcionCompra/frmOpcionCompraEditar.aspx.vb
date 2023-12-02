
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_OpcionCompra_frmOpcionCompraEditar
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmOpcionCompraEditar.aspx.vb")

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
                pCargarQueryString()
                pObtenerDatos()
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

#Region "   Métodos     "

    ''' <summary>
    ''' Función que Carga lo parametros de la url
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:04/01/2013</remarks>
    Private Sub pCargarQueryString()
        hidCodOpcionCompra.Value = Request.QueryString("coc")
        hidNumeroContrato.Value = Request.QueryString("csc")
        hidOpcion.Value = Request.QueryString("op")
    End Sub

    ''' <summary>
    ''' Metodo que obtiene los datos para la opción de compra
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:08/01/2013</remarks>
    Private Sub pObtenerDatos()
        Dim objLOpcionCompra As New LOpcionCompraNTx
        Dim objEOpcionCompra As New EGCC_OpcionCompra
        Dim odtbOpcionCompra As New DataTable

        With objEOpcionCompra
            .NumeroContrato = hidNumeroContrato.Value
            .CodOpcionCompra = GCCUtilitario.CheckInt64(hidCodOpcionCompra.Value)
        End With

        odtbOpcionCompra = GCCUtilitario.DeserializeObject(Of DataTable)(objLOpcionCompra.fobjLeerOpcionCompra(GCCUtilitario.SerializeObject(objEOpcionCompra)))
        If odtbOpcionCompra IsNot Nothing Then
            If odtbOpcionCompra.Rows.Count > 0 Then
                With odtbOpcionCompra.Rows(0)
                    txtMoneda.Value = .Item("NombreMoneda").ToString()
                    txtPorcentajeOC.Value = GCCUtilitario.CheckDecimal(.Item("OpcionCompraPorc").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtImporteOC.Value = GCCUtilitario.CheckDecimal(.Item("ImporteOpcionCompra").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtComisionOC.Value = GCCUtilitario.CheckDecimal(.Item("PorcentajeC").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtPorcentajeCGT.Value = GCCUtilitario.CheckDecimal(.Item("PorcentajeCGT").ToString()).ToString(GCCConstante.C_FormatMiles)

                    txtComisionOCMonto.Value = GCCUtilitario.CheckDecimal(.Item("MontoComision").ToString()).ToString(GCCConstante.C_FormatMiles)
                    txtPorcentajeCGTMonto.Value = GCCUtilitario.CheckDecimal(.Item("MontoGastoTransferencia").ToString()).ToString(GCCConstante.C_FormatMiles)

                    hddComisionOC.Value = .Item("FlgComision").ToString()
                    hddGastosTransCGT.Value = .Item("FlgGasto").ToString()

                End With
            End If
        End If

    End Sub

#End Region

#Region "   Web Metodos "
    ''' <summary>
    ''' Graba Opcion de Compra
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaOpcionCompra(ByVal pstrNumeroContrato As String, _
                                              ByVal pstrCodOpcionCompra As String, _
                                              ByVal pstrComisionOC As String, _
                                              ByVal pstrPorcentajeCGT As String, _
                                              ByVal pstrHddComisionOC As String, _
                                              ByVal pstrHddGastosTransCGT As String, _
                                              ByVal pstrComisionOCMonto As String, _
                                              ByVal pstrPorcentajeCGTMonto As String) As String

        Try
            Dim objEOpcionCompra As New EGCC_OpcionCompra

            With objEOpcionCompra
                .NumeroContrato = pstrNumeroContrato
                .CodOpcionCompra = GCCUtilitario.CheckInt64(pstrCodOpcionCompra)
                .PorcentajeComision = GCCUtilitario.CheckDecimal(pstrComisionOC)
                .PorcentajeGastoTransferencia = GCCUtilitario.CheckDecimal(pstrPorcentajeCGT)
                .AudUsuarioRegistro = GCCSession.CodigoUsuario

                .MontoComision = GCCUtilitario.CheckDecimal(pstrComisionOCMonto)
                .MontoGastoTransferencia = GCCUtilitario.CheckDecimal(pstrPorcentajeCGTMonto)
                .FlgComision = GCCUtilitario.CheckInt64(pstrHddComisionOC)
                .FlgGasto = GCCUtilitario.CheckInt64(pstrHddGastosTransCGT)

            End With

            'Ejecuta Transaccion
            Dim objLOpcionCompraTx As New LOpcionCompraTx
            Dim blnResult As Boolean = False

            blnResult = objLOpcionCompraTx.fblnModificarOpcionCompra(GCCUtilitario.SerializeObject(objEOpcionCompra))

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
