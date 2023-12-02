Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_MultaVehicular_frmMultaVehicularRegistro
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMultaVehicularRegistro.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 09/11/2012
    '''  </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            If Not Page.IsPostBack Then
                'GCCUtilitario.CargarMunicipalidad(Me.ddlMunicipalidad)
                txtPlaca.Value = Request.QueryString("placa")
                hddTipo.Value = Request.QueryString("tipo")
                hddEstadoPago.Value = Request.QueryString("EstPago")
                hddFecTransferencia.Value = Request.QueryString("FecTrans")
                hddNroLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("strNroLote")), "", Request.QueryString("strNroLote"))
                hddCodMunicipalidad.Value = IIf(String.IsNullOrEmpty(Request.QueryString("codMunicipalidad")), "", Request.QueryString("codMunicipalidad"))
                If hddTipo.Value = "" Then
                    hddTipo.Value = "0"
                End If
                If hddTipo.Value = "1" Then
                    hddCodMulta.Value = Request.QueryString("codMulta")
                    'hddCodMunicipalidad.Value = Request.QueryString("codMunicipalidad")
                    'GCCUtilitario.SeleccionaCombo(Me.ddlMunicipalidad, hddCodMunicipalidad.Value)
                End If
                'Inicio IBK - AAE - Agrego info de lote
                If Not String.IsNullOrEmpty(hddNroLote.Value) Then
                    txtNroLoteCarga.Value = hddNroLote.Value
                    hidTengoLote.Value = hddNroLote.Value
                    'Obtengo info del lote
                    Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
                    Dim flag As String = GCCConstante.C_LOTE_MULT_VEHICULAR
                    Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(hddNroLote.Value, flag))
                    hidCodEstadoLote.Value = dtImpuestoVehicular.Rows(0).Item("CodEstadoLote").ToString()
                    txtEstadoLote.Value = dtImpuestoVehicular.Rows(0).Item("DescCodEstadoLote").ToString()
                    txtMontoCheque.Value = dtImpuestoVehicular.Rows(0).Item("MontoCheque").ToString()
                    txtTotalLote.Value = dtImpuestoVehicular.Rows(0).Item("TotalLote").ToString()
                    txtDevuelto.Value = dtImpuestoVehicular.Rows(0).Item("DevueltoLote").ToString()
                    txtReembolsar.Value = dtImpuestoVehicular.Rows(0).Item("ReembolsoLote").ToString()
                    hddCodMunicipalidad.Value = IIf(String.IsNullOrEmpty(Request.QueryString("codMunicipalidad")), dtImpuestoVehicular.Rows(0).Item("Municipalidad").ToString(), Request.QueryString("codMunicipalidad"))
                Else
                    txtNroLoteCarga.Value = GenerarLoteLoad2("", hddNroLote.Value)
                    hidTengoLote.Value = "N"
                    txtEstadoLote.Value = "Ingresado"
                    txtMontoCheque.Value = "0"
                    txtTotalLote.Value = "0"
                    txtDevuelto.Value = "0"
                    txtReembolsar.Value = "0"
                End If
                'Fin IBK
                If Not String.IsNullOrEmpty(hddCodMunicipalidad.Value) Then
                    ListadoMunicipalidadPaginado(hddCodMunicipalidad.Value, "") 'JJM IBK
                    'GCCUtilitario.SeleccionaCombo(cmbMunicipalidad, strMunicipalidad)
                End If
                hddPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                'GCCUtilitario.SeleccionaCombo(Me.ddlMunicipalidad, hddCodMunicipalidad.Value.Trim)
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

#Region "Web Métodos"
    <WebMethod()> _
    Public Shared Function ListaBienesVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pCodMunicipalidad As String, _
                                           ByVal pPlaca As String, _
                                           ByVal pTipo As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX


        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarBienMultaVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pCodMunicipalidad, _
                                                                                                            pPlaca, pTipo, _
                                                                           pNroMotor, pCUCliente, pCodContrato))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtImpuestoVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpuestoVehicular)

    End Function
    <WebMethod()> _
    Public Shared Function ListarLoteMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pCodMunicipalidad As String, _
                                   ByVal pTipo As String, _
                                   ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX


        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarLoteMultaVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pPlaca.Trim, pCodMunicipalidad.Trim, CType(pTipo, Integer), pNroLote.Trim))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtImpuestoVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpuestoVehicular)

    End Function
    ''' Inicio IBK - AAE - Agrego paráemtro
    <WebMethod()> _
    Public Shared Function EliminarMultaVehicular(ByVal pstrCodigosImpuestos As String, ByVal pstrNroLote As String) As String

        Try

            Dim objLMultaVehicular As New LImpuestoVehicularTX


            Dim blnResult As Boolean = objLMultaVehicular.EliminarMultaVehicular(pstrCodigosImpuestos, GCCSession.CodigoUsuario, pstrNroLote)


            If blnResult Then
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
    Public Shared Function EliminarLote(ByVal pLote As String) As String

        Try

            Dim objLMultaVehicular As New LImpuestoVehicularTX


            Dim blnResult As Boolean = objLMultaVehicular.EliminarLote(pLote)



            If blnResult Then
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

    ''' <summary>
    ''' Generar Nro de Lote
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GenerarLote(ByVal strCodigoImpuesto As String, _
                                                    ByVal strNroLote As String, _
                                                    ByVal strMunicipalidad As String) As String

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .CodigosImpuesto = strCodigoImpuesto
                    .CodNroLote = strNroLote
                    .Municipalidad = strMunicipalidad
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteMultaVehicular(pEImpuestoVehicular)

            Return strNroLoteGenerado.Trim.PadLeft(8, "0"c)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    'Inicio IBK - AAE
    <WebMethod()> _
   Public Shared Function ReGenerarLote(ByVal strNroLote As String) As String

        Try

            Dim objLImpuestoVehicular As New LImpuestoVehicularTX


            Dim strNroLoteGenerado As String = objLImpuestoVehicular.ReGenerarLote(strNroLote)

            Return strNroLoteGenerado

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    'Inicio IBK - AAE - Agrego funcion
    <WebMethod()> _
    Public Shared Function AnularLote(ByVal pLote As String) As String

        Try

            Dim objLMultaVehicular As New LImpuestoVehicularTX


            Dim strResult As String = objLMultaVehicular.AnularLote(pLote)


            Return strResult
        Catch ex As Exception
            Dim myException As String
            myException = ManageException(ex)
            Return myException
        End Try
    End Function
    'Fin IBK
    <WebMethod()> _
   Public Shared Function ObtenerInfoLote(ByVal pNroLote As String) As JQGridJsonResponse

        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            
        Dim flag As String = GCCConstante.C_LOTE_IMP_VEHICULAR
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(pNroLote, flag))


        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtImpuestoVehicular)

    End Function
    'Fin IBK
    ''' <summary>
    ''' BusquedaRapida
    ''' Creacion : 21/02/2013
    ''' Creador : JJM IBK
    ''' </summary>
    ''' <remarks></remarks>
    ''' Public Shared Function ConsultaMunicipalidad(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As JQGridJsonResponse
    <WebMethod()> _
    Public Shared Function ConsultaMunicipalidad(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As String
        Dim objUtilNTX As New LUtilNTX
        Dim strRes As String = ""
        Dim dtUtil As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTX.ListadoMunicipalidadPaginado(100, 100, "CLAVE1", "desc", _
                                                                                                                    pCodMunicipalidad, _
                                                                                                                      pMunicipalidad))
        If dtUtil.Rows.Count = 1 Then
            strRes = "0|" + dtUtil.Rows(0).Item("CLAVE1").ToString() + "|" + dtUtil.Rows(0).Item("VALOR1").ToString
        Else
            strRes = "1|"
        End If
        'Dim objJQGridJsonResponse As New JQGridJsonResponse
        'Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtUtil)
        ' Número total de páginas
        Return strRes
    End Function
#End Region
#Region "Metodos"
    Public Shared Function GenerarLoteLoad(ByVal strCodigoImpuesto As String, _
                                                   ByVal strNroLote As String, _
                                                   ByVal strMunicipalidad As String) As String

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .CodigosImpuesto = strCodigoImpuesto
                    .CodNroLote = strNroLote
                    .Municipalidad = strMunicipalidad
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteMultaVehicular(pEImpuestoVehicular)

            Return strNroLoteGenerado.Trim.PadLeft(8, "0"c)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    'Inicio IBK - AAE
    Public Shared Function GenerarLoteLoad2(ByVal strCodigoImpuesto As String, _
                                                   ByVal strNroLote As String) As String

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .CodigosImpuesto = strCodigoImpuesto
                    .CodNroLote = strNroLote
                    .RegeneraLote = IIf(String.IsNullOrEmpty(strNroLote), "0", "1")
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)
            'Inicio IBK - AAE - Obtener el siguiente nro de lote para visualizar
            'Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteImpuestoVehicular(pEImpuestoVehicular)
            Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteImpuestoVehicular2(pEImpuestoVehicular)
            'Fin IBK
            Return strNroLoteGenerado.Trim.PadLeft(8, "0"c)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Fin IBK
    Public Function ListadoMunicipalidadPaginado(ByVal pCodMunicipalidad As String, ByVal pMunicipalidad As String) As String
        Dim objUtilNTX As New LUtilNTX

        Dim dtUtil As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTX.ListadoMunicipalidadPaginado(10, 1, "CLAVE1", "desc", _
                                                                                                                    pCodMunicipalidad, _
                                                                                                                    pMunicipalidad))

        If dtUtil.Rows.Count > 0 Then
            txtCodMunicipalidad.Value = dtUtil.Rows(0).Item("CLAVE1").ToString
            txtMunicipalidad.Value = dtUtil.Rows(0).Item("VALOR1").ToString
        End If
        Return ""

    End Function
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function

#End Region
End Class
