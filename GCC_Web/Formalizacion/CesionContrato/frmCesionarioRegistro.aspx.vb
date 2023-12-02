Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Formalizacion_CesionContrato_frmCesionarioRegistro
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

                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Me.hddCodContrato.Value = strCodContrato

                'Setea Modo Transaccion
                Me.hddTipoTx.Value = GCCConstante.C_TX_NUEVO

                GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoDoc, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboMoneda(cmbMoneda)

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
    ''' Graba Cesionario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaCesionario(ByVal pstrTipoTx As String, _
                                            ByVal pstrCodContrato As String, _
                                            ByVal pstrCodCesionario As String, _
                                            ByVal pstrRazSocial As String, _
                                            ByVal pstrNroDocumento As String, _
                                            ByVal pstrDireccion As String, _
                                            ByVal pstrTipoDoc As String, _
                                            ByVal pstrDepartamento As String, _
                                            ByVal pstrProvincia As String, _
                                            ByVal pstrDistrito As String, _
                                            ByVal txtCodUnico As String, _
                                            ByVal cmbTipoCuenta As String, _
                                            ByVal cmbMoneda As String, _
                                            ByVal txtNumeroCuenta As String) As String

        Try
            Dim objECesionario As New EGCC_Cesionario

            With objECesionario
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .CodCesionario = GCCUtilitario.CheckInt(pstrCodCesionario)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazSocial)
                .TipoDocumento = GCCUtilitario.NullableString(pstrTipoDoc)
                .NroDocumento = GCCUtilitario.NullableString(pstrNroDocumento)
                .Direccion = GCCUtilitario.NullableString(pstrDireccion)
                .Departamento = GCCUtilitario.NullableString(pstrDepartamento)
                .Provincia = GCCUtilitario.NullableString(pstrProvincia)
                .Distrito = GCCUtilitario.NullableString(pstrDistrito)
                .UsuarioRegistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .UsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .CodUnico = GCCUtilitario.NullableString(txtCodUnico)
                .TipoCuenta = GCCUtilitario.NullableString(cmbTipoCuenta)
                .CodMoneda = GCCUtilitario.NullableString(cmbMoneda)
                .NroCuenta = GCCUtilitario.NullableString(txtNumeroCuenta)
                .EstadoLogico = "1"
            End With


            'Ejecuta Transaccion
            Dim objLCesionarioTx As New LCesionarioTx

            Dim blnResult As Boolean = False
            Dim strCodCesionario As String = ""
            If pstrTipoTx.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strCodCesionario = objLCesionarioTx.InsertarCesionario(GCCUtilitario.SerializeObject(objECesionario))
                blnResult = True
            Else
                objLCesionarioTx.ModificarCesionario(GCCUtilitario.SerializeObject(objECesionario))
                strCodCesionario = pstrCodCesionario
                blnResult = True
            End If

            'Valida Resultado
            If blnResult Then
                Return "1|" + strCodCesionario
            Else
                Return "0|0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

    ''' <summary>
    ''' ListaCesionContrato
    ''' </summary>
    ''' <returns>Listado de Cesionarios</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaCesionarios(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pstrNroContrato As String) As JQGridJsonResponse

        'Variables
        Dim objCesionarioNTx As New LCesionarioNTx

        Try

            'Inicializa Objeto
            Dim objECesionario As New EGCC_Cesionario
            Dim strECesionario As String
            With objECesionario
                .Codsolcredito = GCCUtilitario.NullableString(pstrNroContrato)
            End With
            strECesionario = GCCUtilitario.SerializeObject(Of EGCC_Cesionario)(objECesionario)

            'Ejecuta Consulta
            Dim dtCesionario As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCesionarioNTx.ListadoCesionario(pPageSize, _
                                                                                                                           pCurrentPage, _
                                                                                                                           pSortColumn, _
                                                                                                                           pSortOrder, _
                                                                                                                           strECesionario))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCesionario.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtCesionario.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtCesionario.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCesionario)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Eliminar Cesionario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 09/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaCesionario(ByVal hddCodContrato As String, _
                                             ByVal hddCodCesionario As String) As String

        ''Variables
        Dim objCesionarioTx As New LCesionarioTx

        Try

            'Inicializa Objeto
            Dim objECesionario As New EGCC_Cesionario
            Dim strECesionario As String
            With objECesionario
                .Codsolcredito = GCCUtilitario.NullableString(hddCodContrato)
                .CodCesionario = GCCUtilitario.CheckInt(hddCodCesionario)
                .UsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .EstadoLogico = "0"
            End With
            strECesionario = GCCUtilitario.SerializeObject(Of EGCC_Cesionario)(objECesionario)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objCesionarioTx.EliminarCesionario(strECesionario)

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

    ''' <summary>
    ''' Consulta RM
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ConsultaRM(ByVal pstrTipoDoc As String, _
                                            ByVal pstrNroDoc As String) As EClienteRM

        Try
            Dim strMensajeError As String = ""

            'Consulta RM
            Dim intNumConsulta As Integer = 2 '2=Busqueda por Tipo Documento - 1=Por CU
            Dim strCodUnico As String = ""
            Dim strTipoDoc As String = pstrTipoDoc.Trim()
            Dim strNroRuc As String = pstrNroDoc.Trim()
            Dim strMensaje As String = ""
            Dim oEClienteRM As EClienteRM = GCCUtilitario.fObtenerDatosRMCliente(intNumConsulta, strCodUnico, strTipoDoc, strNroRuc, strMensaje)
            strMensajeError = strMensaje

            Dim blnResultado As Boolean = False
            If Not oEClienteRM Is Nothing Then
                oEClienteRM.CodError = 0
                Return oEClienteRM
            Else
                oEClienteRM.CodError = 1
                oEClienteRM.MsgError = "El servicio de RM lanzó una excepción.(" + strMensaje + ")"
                Return oEClienteRM
            End If

        Catch ex As Exception
            Dim oEClienteRM As New EClienteRM
            oEClienteRM.CodError = 1
            oEClienteRM.MsgError = "No se pudo cargar los datos de RM."
            Return oEClienteRM
        End Try

    End Function

    ''' <summary>
    ''' ValidaCuentaST
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ValidaCuentaST(ByVal argFCDTIPOCUENTA As String, _
                                          ByVal argFCDCODMONEDA As String, _
                                          ByVal argFCDCODTIENDA As String, _
                                          ByVal argFCDCODCATEGORIA As String, _
                                          ByVal argFCDNUMCUENTA As String, _
                                          ByVal pCodUnico As String) As String
        Dim sTldDatosTran As String = String.Empty
        Dim sMensaje As String = ""
        Dim oLWebService As New LWebService
        Try
            Dim strUlrws As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsFCDCuenta")

            sTldDatosTran = oLWebService.fstrConsultarCuenta(argFCDTIPOCUENTA, argFCDCODMONEDA, argFCDCODTIENDA, argFCDCODCATEGORIA, argFCDNUMCUENTA, strUlrws)
            Dim resultado1 As String = ""
            Dim resultado2 As String = ""
            Dim resultadoSTServicio As String
            resultadoSTServicio = "1|(FCDO003)Función six_fastpartner retornar un Error: -10"

            If sTldDatosTran.Trim <> resultadoSTServicio.Trim Then

                Dim strTrans As String() = sTldDatosTran.Split(New Char() {"|"c}) 'sTldDatosTran.Split("|")
                If strTrans(0).ToString = "0" Then
                    If argFCDTIPOCUENTA = "IM" Then
                        resultado1 = "0|" + strTrans(16).ToString   '-- CORRIENTE 
                        If strTrans(16).ToString() <> pCodUnico Then
                            resultado1 = "1|" + strTrans(16).ToString()
                        End If
                    Else
                        resultado1 = "0|" + strTrans(17).ToString   '-- AHORROS
                        If strTrans(17).ToString() <> pCodUnico Then
                            resultado1 = "1|" + strTrans(17).ToString()
                        End If
                    End If

                Else
                    resultado1 = "1|" + strTrans(1).ToString
                End If



                If Left(resultado1, 1) = "1" Then
                    Return "1|La Cuenta ingresada está errada, por favor verifique"
                Else
                    Return resultado1
                End If


            Else
            Return "1|No hay conexión con el servidor para la validación de las cuentas"

            End If
        Catch ex As Exception
            'Throw ex
            Return "1|" + ex.ToString
        End Try

        Return "1|No se pudo validar la cuenta ST"

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
