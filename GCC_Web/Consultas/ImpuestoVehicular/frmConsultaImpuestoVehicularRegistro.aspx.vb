
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_ImpuestoVehicular_frmConsultaImpuestoVehicularRegistro
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultaImpuestoVehicularRegistro.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/11/2011
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

                txtPlaca.Value = Request.QueryString("placa")
                hddTipo.Value = Request.QueryString("tipo")
                hddFechaTransferencia.Value = Request.QueryString("fecTransferencia")
                hddCheque.Value = Request.QueryString("cheque")
                If hddTipo.Value = "" Then
                    hddTipo.Value = "0"
                End If
                hddCodImpuesto.Value = Request.QueryString("codImpuesto")
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
    Public Shared Function ListaBienesVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pPlaca As String, _
                                           ByVal pTipo As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As JQGridJsonResponse

        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarBienImpuestoVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
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
    Public Shared Function ListarLoteImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pTipo As Integer) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX

        'GCCAEP_20130212 - Se cambio el metodo de consulta.
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarLoteImpuestoVehicularConsulta(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pPlaca, Convert.ToInt32(pTipo)))

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
    ''' <summary>
    ''' Eliminar Impuesto Vehicular
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 15/11/2012
    ''' </remarks>
    '    <WebMethod()> _
    'Public Shared Function EliminarImpuestoVehicular(ByVal pstrCodigosImpuestos As String)

    '        Try

    '            Dim objLMultaVehicular As New LImpuestoVehicularTX


    '            Dim blnResult As Boolean = objLMultaVehicular.EliminarImpuestoVehicular(pstrCodigosImpuestos, GCCSession.CodigoUsuario)


    '            If blnResult Then
    '                Return "0"
    '            Else
    '                Return "1"
    '            End If
    '        Catch ex As Exception
    '            Dim myException As String
    '            myException = ManageException(ex)
    '            Return myException
    '        End Try
    '    End Function
    ''' <summary>
    ''' Generar Nro de Lote
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 16/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GenerarLote(ByVal strCodigoImpuesto As String, _
                                                    ByVal strNroLote As String) As String

        Try
            Dim objEImpuestoVehicular As New EImpuestovehicular
            Dim objLImpuestoVehicular As New LImpuestoVehicularTX
            Dim pEImpuestoVehicular As String

            If objEImpuestoVehicular IsNot Nothing Then
                With objEImpuestoVehicular
                    .CodigosImpuesto = strCodigoImpuesto
                    .CodNroLote = strNroLote
                End With
            End If
            pEImpuestoVehicular = GCCUtilitario.SerializeObject(objEImpuestoVehicular)

            Dim strNroLoteGenerado As String = objLImpuestoVehicular.AsignarLoteImpuestoVehicular(pEImpuestoVehicular)

            Return strNroLoteGenerado.Trim.PadLeft(8, "0"c)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    '<WebMethod()> _
    'Public Shared Function ValidarCuotas(ByVal strCodigoImpuesto As String, ByVal strCodigoBien As String, ByVal strPeriodos As String, ByVal strContratos As String)

    '    Try
    '        Dim StrPlaca As String = ""
    '        Dim strNroPeriodo As String
    '        Dim IntSumNroCuota As Integer = 0
    '        Dim IntCantNroCuota As Integer = 0
    '        Dim intTotalCuota As Integer = 0
    '        Dim StrMensaje As String
    '        Dim flag As Integer

    '        Dim objLImpuestoVehicularNTX As New LImpuestoVehicularNTX

    '        Dim dtTotalCuotas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoVehicularNTX.ObtenerTotalCuotas(strCodigoBien, strPeriodos, strContratos))


    '        Dim dtValidacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoVehicularNTX.ObtenerPeriodosValidacion(strCodigoImpuesto))

    '        If dtTotalCuotas.Rows.Count > 0 And dtValidacion.Rows.Count > 0 Then
    '            For Each dr As DataRow In dtValidacion.Rows
    '                'If (dr("SumNroCuota").ToString().Trim() <> 0) Then
    '                For Each dr2 As DataRow In dtTotalCuotas.Rows
    '                    If dr("Periodo").ToString().Trim() = dr2("periodo").ToString().Trim() Then
    '                        intTotalCuota = Convert.ToInt32(dr2("CantidadTotal").ToString().Trim())
    '                        IntCantNroCuota = Convert.ToInt32(dr("CantNroCuota").ToString().Trim())
    '                        IntSumNroCuota = Convert.ToInt32(dr("SumNroCuota").ToString().Trim())

    '                        If intTotalCuota <> IntCantNroCuota Then
    '                            StrMensaje = "Debe de seleccionar todas las cuotas del periodo " + dr("Periodo").ToString().Trim() + " de la placa " + dr("Placa").ToString().Trim()
    '                        Else

    '                            If IntSumNroCuota = 0 And IntCantNroCuota = 1 Then
    '                                StrMensaje = ""
    '                            ElseIf IntSumNroCuota = 1 And IntCantNroCuota = 2 Then
    '                                StrMensaje = ""
    '                            ElseIf IntSumNroCuota = 3 And IntCantNroCuota = 3 Then
    '                                StrMensaje = ""
    '                            ElseIf IntSumNroCuota = 6 And IntCantNroCuota = 4 Then
    '                                StrMensaje = ""
    '                            ElseIf IntSumNroCuota = 10 And IntCantNroCuota = 4 Then
    '                                StrMensaje = ""
    '                            Else
    '                                StrMensaje = "Debe de seleccionar todas las cuotas del periodo " + dr("Periodo").ToString().Trim() + " de la placa " + dr("Placa").ToString().Trim()
    '                            End If

    '                            'If intTotalCuota <> IntCantNroCuota Then
    '                            '    StrMensaje = "Debe de seleccionar todas las cuotas del periodo " + dr("Periodo").ToString().Trim() + " de la placa " + dr("Placa").ToString().Trim()
    '                            'End If
    '                        End If
    '                    End If
    '                Next
    '                '  End If
    '            Next
    '        End If

    '        'If StrMensaje = "" Then
    '        '    If dtValidacion.Rows.Count > 0 Then

    '        '        For Each dr As DataRow In dtValidacion.Rows

    '        '            If StrPlaca.Contains(dr("Placa").ToString().Trim()) Then
    '        '                StrPlaca = dr("Placa").ToString().Trim()
    '        '                flag = 1
    '        '            Else
    '        '                StrPlaca = StrPlaca + dr("Placa").ToString().Trim() + ","
    '        '                If dtValidacion.Rows.Count > 1 Then
    '        '                    flag = 2
    '        '                Else
    '        '                    flag = 1
    '        '                End If

    '        '            End If
    '        '            strNroPeriodo = strNroPeriodo + dr("Periodo").ToString().Trim() + ","
    '        '            IntSumNroCuota = Convert.ToInt32(dr("SumNroCuota").ToString().Trim())
    '        '            IntCantNroCuota = Convert.ToInt32(dr("CantNroCuota").ToString().Trim())

    '        '            If IntSumNroCuota = 0 And IntCantNroCuota = 1 Then
    '        '                StrMensaje = ""
    '        '            ElseIf IntSumNroCuota = 1 And IntCantNroCuota = 2 Then
    '        '                StrMensaje = ""
    '        '            ElseIf IntSumNroCuota = 3 And IntCantNroCuota = 3 Then
    '        '                StrMensaje = ""
    '        '            ElseIf IntSumNroCuota = 6 And IntCantNroCuota = 4 Then
    '        '                StrMensaje = ""
    '        '            ElseIf IntSumNroCuota = 10 And IntCantNroCuota = 4 Then
    '        '                StrMensaje = ""
    '        '            Else
    '        '                If flag = 1 Then
    '        '                    If dtValidacion.Rows.Count > 1 Then
    '        '                        StrMensaje = "Falta registrar cuotas para la placa " + StrPlaca + " de los periodos " + strNroPeriodo.Substring(0, strNroPeriodo.Length - 1)
    '        '                    Else
    '        '                        StrMensaje = "Falta registrar cuotas para la placa " + StrPlaca + " del periodo " + strNroPeriodo.Substring(0, strNroPeriodo.Length - 1)
    '        '                    End If
    '        '                Else
    '        '                    If dtValidacion.Rows.Count > 1 Then
    '        '                        StrMensaje = "Falta registrar cuotas para las placas " + StrPlaca.Substring(0, StrPlaca.Length - 1) + " de los periodos " + strNroPeriodo.Substring(0, strNroPeriodo.Length - 1)
    '        '                    Else
    '        '                        StrMensaje = "Falta registrar cuotas para las placas " + StrPlaca.Substring(0, StrPlaca.Length - 1) + " del periodo " + strNroPeriodo.Substring(0, strNroPeriodo.Length - 1)
    '        '                    End If
    '        '                End If
    '        '                ' Exit For
    '        '            End If
    '        '        Next
    '        '    Else
    '        '        Return ""
    '        '    End If
    '        'End If


    '        If StrMensaje <> "" Then
    '            Return StrMensaje
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Dim myException As String
    '        myException = ManageException(ex)
    '        Return myException
    '    End Try
    'End Function

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
#End Region
End Class
