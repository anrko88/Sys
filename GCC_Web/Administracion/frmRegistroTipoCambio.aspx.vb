Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Administracion_frmRegistroTipoCambio
    Inherits GCCBase

    Dim objLog As New GCCLog("frmRegistroTipoCambio.aspx.vb")
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
                hddPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                hidCodTipoCambio.Value = Request.QueryString("CodTipoCambio")
                hidOpcion.Value = Request.QueryString("co")
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoModalidad, GCCConstante.C_TABLAGENERICA_Modalidad_TipoCambio)
                pInicializarControles()
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

    Private Sub pInicializarControles()
        Dim oLwsDocProveedorNtx As New LProveedorNTx
        Dim strProcedencia As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(hidCodTipoCambio.Value) Then

                cmbTipoModalidad.Value = Request.QueryString("CodTipoCambio").ToString()
                txtFechaIni.Value = Request.QueryString("FechaInicio").ToString()
                txtFechaFin.Value = Request.QueryString("FechaFin").ToString()
                txtNombreMoneda.Value = Request.QueryString("CodMoneda").ToString()
                txtValorCompra.Value = Request.QueryString("ValorCompra").ToString()
                txtValorVenta.Value = Request.QueryString("ValorVenta").ToString()

            End If

        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocProveedorNtx = Nothing
        End Try
    End Sub

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

    Private Shared Function ConvierteFecha(ByVal StrFecha As String) As String
        Dim datofecha() As String
        Dim Dato1 As String = StrFecha.ToString()
        Dim Dia As String = String.Empty
        Dim Mes As String = String.Empty
        Dim Anio As String = String.Empty
        If Not String.IsNullOrEmpty(StrFecha) Then
            datofecha = Split(Dato1, "/")
            Dia = datofecha(0)
            Mes = datofecha(1)
            Anio = datofecha(2)
        End If
        Return Anio + "" + Mes + "" + Dia

    End Function
    Public Shared Function ValidatipoCambio(ByVal pCodTipoModalidad As String, _
                                         ByVal ptxtFechaIni As String, _
                                         ByVal ptxtFechaFin As String) As String

        Try
            Dim objTipoCambioNTx As New LTipoCambioNTx
            Dim ETipoCambio As New EMonedatipocambio
            With ETipoCambio
                .Tipomodalidadcambio = pCodTipoModalidad
                .Fechainiciovigencia = ConvierteFecha(ptxtFechaIni)
                .Fechafinalvigencia = ConvierteFecha(ptxtFechaFin)
                .Codmoneda = GCCConstante.C_COD_MONEDA_DOLARES
            End With

            Dim dtTipoCambio As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTipoCambioNTx.ValidaTipoCambio(GCCUtilitario.SerializeObject(Of EMonedatipocambio)(ETipoCambio)))
            Dim Result As String = dtTipoCambio.Rows.Count.ToString()
            Return Result
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()
        Return strErrorMessage
    End Function
    Private Shared Function AgregaDias(ByVal StrFecha As String) As String
        Dim fecha As Date = StrFecha
        Dim fechaxx As Date = fecha.AddDays(-15).ToShortDateString()
        Return fechaxx
    End Function
#End Region

#Region "WebMetods"
    <WebMethod()> _
    Public Shared Function GuardarRegistro(ByVal pCodTipoModalidad As String, _
                                         ByVal ptxtFechaIni As String, _
                                         ByVal ptxtFechaFin As String, _
                                         ByVal ptxtValorCompra As String, _
                                         ByVal ptxtValorVenta As String) As String

        Try
            Dim objEMonedatipocambio As New EMonedatipocambio
            Dim objLTipoCambio As New LTipoCambioTx
            Dim pstrEMonedatipocambio As String = String.Empty

            With objEMonedatipocambio
                .Codmoneda = GCCConstante.C_COD_MONEDA_DOLARES
                .Tipomodalidadcambio = pCodTipoModalidad
                .Fechainiciovigencia = ConvierteFecha(ptxtFechaIni)
                .Fechafinalvigencia = ConvierteFecha(ptxtFechaFin)
                .Montovalorcompra = GCCUtilitario.CheckDecimal(ptxtValorCompra)
                .Montovalorventa = GCCUtilitario.CheckDecimal(ptxtValorVenta)
                .Textoaudicreacion = GCCSession.CodigoUsuario
                .Codusuario = GCCSession.CodigoUsuario

            End With
            pstrEMonedatipocambio = GCCUtilitario.SerializeObject(objEMonedatipocambio)
            Dim blnResultado As Boolean = False
            Dim res As String = ValidatipoCambio(pCodTipoModalidad, ptxtFechaIni, ptxtFechaFin)
            If (res = "0") Then
                blnResultado = objLTipoCambio.InsertaTipoCambio(pstrEMonedatipocambio)
            End If

            If blnResultado = True Then
                Return "1" 'Si
            Else
                Return "0" 'No
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    <WebMethod()> _
   Public Shared Function ActualizaRegistro(ByVal pCodTipoModalidad As String, _
                                        ByVal ptxtFechaIni As String, _
                                        ByVal ptxtFechaFin As String, _
                                        ByVal ptxtValorCompra As String, _
                                        ByVal ptxtValorVenta As String) As String

        Try
            Dim objEMonedatipocambio As New EMonedatipocambio
            Dim objLTipoCambio As New LTipoCambioTx
            Dim pstrEMonedatipocambio As String = String.Empty

            With objEMonedatipocambio
                .Codmoneda = GCCConstante.C_COD_MONEDA_DOLARES
                .Tipomodalidadcambio = pCodTipoModalidad
                .Fechainiciovigencia = ConvierteFecha(ptxtFechaIni)
                .Fechafinalvigencia = ConvierteFecha(ptxtFechaFin)
                .Montovalorcompra = GCCUtilitario.CheckDecimal(ptxtValorCompra)
                .Montovalorventa = GCCUtilitario.CheckDecimal(ptxtValorVenta)
                .Fechacarga = Date.Now.ToString()
                .Textoaudicreacion = GCCSession.CodigoUsuario
                .Codusuario = GCCSession.CodigoUsuario

            End With
            pstrEMonedatipocambio = GCCUtilitario.SerializeObject(objEMonedatipocambio)
            Dim blnResultado As Boolean = False
            blnResultado = objLTipoCambio.ActualizaTipoCambio(pstrEMonedatipocambio)

            If blnResultado = True Then
                Return "1" 'Grabo
            Else
                Return "0" 'No grabo
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    <WebMethod()> _
  Public Shared Function EliminaRegistro(ByVal pCodTipoModalidad As String, _
                                       ByVal ptxtFechaIni As String, _
                                       ByVal ptxtFechaFin As String) As String

        Try
            Dim objEMonedatipocambio As New EMonedatipocambio
            Dim objLTipoCambio As New LTipoCambioTx
            Dim pstrEMonedatipocambio As String = String.Empty

            With objEMonedatipocambio
                .Codmoneda = GCCConstante.C_COD_MONEDA_DOLARES
                .Tipomodalidadcambio = pCodTipoModalidad
                .Fechainiciovigencia = ConvierteFecha(ptxtFechaIni)
                .Fechafinalvigencia = ConvierteFecha(ptxtFechaFin)
                .Fechacarga = Date.Now.ToString()
                .Textoaudicreacion = GCCSession.CodigoUsuario
                .Codusuario = GCCSession.CodigoUsuario

            End With
            pstrEMonedatipocambio = GCCUtilitario.SerializeObject(objEMonedatipocambio)
            Dim blnResultado As Boolean = False
            blnResultado = objLTipoCambio.EliminaTipoCambio(pstrEMonedatipocambio)

            If blnResultado = True Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    <WebMethod()> _
 Public Shared Function BuscarTipoCambio(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodigoTipoModalidad As String, _
                                          ByVal pFechaInicioVigencia As String, _
                                          ByVal pFechaFinalVigencia As String) As JQGridJsonResponse

        Dim objTipoCambioNTx As New LTipoCambioNTx
        Dim ETipoCambio As New EMonedatipocambio


        With ETipoCambio
            .Tipomodalidadcambio = pCodigoTipoModalidad
            .Fechainiciovigencia = GCCUtilitario.NullableString(ConvierteFecha(AgregaDias(pFechaInicioVigencia)))
            .Fechafinalvigencia = GCCUtilitario.NullableString(ConvierteFecha(pFechaFinalVigencia))
            .Tipooperacion = "1"
        End With

        Dim dtTipoCambio As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTipoCambioNTx.ListaTipoCambio(pPageSize, _
                                                                                                                    pCurrentPage, _
                                                                                                                    pSortColumn, _
                                                                                                                    pSortOrder, _
                                                                                                                    GCCUtilitario.SerializeObject(Of EMonedatipocambio)(ETipoCambio)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtTipoCambio.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtTipoCambio.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtTipoCambio.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtTipoCambio)

    End Function
#End Region

End Class
