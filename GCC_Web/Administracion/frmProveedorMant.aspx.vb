Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Mantenimiento_frmProveedorMant
    Inherits GCCBase

    Dim objLog As New GCCLog("frmProveedorMant.aspx.vb")

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
                HidPerfil.Value = IIf(String.IsNullOrEmpty(GCCSession.PerfilUsuario), "", GCCSession.PerfilUsuario)
                hidCodProveedor.Value = Request.QueryString("cp")
                hidOpcion.Value = Request.QueryString("co")
                GCCUtilitario.CargarComboValorGenerico(Me.ddlProcedencia, GCCConstante.C_TABLAGENERICA_NACIONALIDAD)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoPersona, GCCConstante.C_TABLAGENERICA_TIPO_PERSONA)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta1, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta2, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta3, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.ddlCargo, GCCConstante.C_TABLAGENERICA_CARGO)
                GCCUtilitario.CargarComboPais(ddlPais)
                GCCUtilitario.CargarComboMoneda(cmbMoneda1)
                GCCUtilitario.CargarComboMoneda(cmbMoneda2)
                GCCUtilitario.CargarComboMoneda(cmbMoneda3)
                GCCUtilitario.CargarDepartamento(ddlDepartamento)
                pInicializarControles()

                'txtNroDocumento.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imgBsqClienteRM.UniqueID + "').click();return false;}} else {return true}; ")


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
            If hidCodProveedor.Value <> 0 Then

                Dim strProveedor As String = oLwsDocProveedorNtx.ObtenerProveedor(hidCodProveedor.Value)
                If Not String.IsNullOrEmpty(strProveedor) Then
                    Dim oEProveedor As EProveedor = GCCUtilitario.DeserializeObject(Of EProveedor)(strProveedor)
                    Dim ctd As String = oEProveedor.CodTipoDocumento.Trim
                    With oEProveedor
                        ddlProcedencia.Value = .CodProcedencia.Trim
                        strProcedencia = .CodProcedencia.Trim
                        cmbTipoPersona.Value = .Codigotipopersona.Trim
                        'cmbTipoDocumento.Value = .CodTipoDocumento.Trim
                        GCCUtilitario.SeleccionaCombo(cmbTipoDocumento, ctd)
                        txtNroDocumento.Value = .NumeroDocumento.Trim
                        txtRazonSocial.Value = .RazonSocial.Trim
                        txtDireccion.Value = .TextoDomicilioLegal.Trim
                        ddlPais.Value = .CodPais.Trim
                        hidCodDepartamento.Value = .CodDepartamento.Trim
                        hidCodProvincia.Value = .CodProvincia.Trim
                        hidCodDistrito.Value = .CodDistrito.Trim
                        HidCodigoUnico.Value = .CodUnico.Trim
                        'cmbTipoPersona.Value = .Codigotipopersona.Trim

                    End With

                End If

                'Dim oEClienteRM As EClienteRM = GCCUtilitario.fObtenerDatosRMCliente(2, "", cmbTipoDocumento.Value, txtNroDocumento.Value, "")

                ''Verifica Existencia en RM
                'If Not oEClienteRM Is Nothing Then
                '    oEClienteRM.CodError = 0
                '    HidCodigoUnico.Value = oEClienteRM.Codigounico
                'Else
                '    HidCodigoUnico.Value = ""
                'End If

                Dim dtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocProveedorNtx.ListadoProveedorCuenta(hidCodProveedor.Value))
                Dim intIndicador As Integer = 0
                If dtbDatos IsNot Nothing Then
                    If dtbDatos.Rows.Count > 0 Then
                        For Each dr As DataRow In dtbDatos.Rows
                            If dr("CodigoTipoBanco").ToString().Trim = GCCConstante.C_TIPOBANCO_NACION Then
                                hidCodProveedorCuenta3.Value = dr("CodProveedorCuenta").ToString().Trim
                                cmbTipoCuenta3.Value = dr("CodigoTipoCuenta").ToString().Trim
                                cmbMoneda3.Value = dr("CodMoneda").ToString().Trim
                                txtNumCuenta3.Value = dr("NumeroCuenta").ToString().Trim
                            Else
                                intIndicador += 1
                                If intIndicador = 1 Then
                                    hidCodProveedorCuenta1.Value = dr("CodProveedorCuenta").ToString().Trim
                                    cmbTipoCuenta1.Value = dr("CodigoTipoCuenta").ToString().Trim
                                    cmbMoneda1.Value = dr("CodMoneda").ToString().Trim
                                    txtNumCuenta1.Value = dr("NumeroCuenta").ToString().Trim
                                End If
                                If intIndicador = 2 Then
                                    hidCodProveedorCuenta2.Value = dr("CodProveedorCuenta").ToString().Trim
                                    cmbTipoCuenta2.Value = dr("CodigoTipoCuenta").ToString().Trim
                                    cmbMoneda2.Value = dr("CodMoneda").ToString().Trim
                                    txtNumCuenta2.Value = dr("NumeroCuenta").ToString().Trim
                                End If
                            End If
                        Next
                    End If
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "setearUbigeo", "fn_SeteaUbigeo();", True)

            End If
        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocProveedorNtx = Nothing
        End Try
    End Sub

    <WebMethod()> _
    Public Shared Function ListadoContacto(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pCodProveedor As String) As JQGridJsonResponse
        Dim objContactoNTx As New LProveedorNTx

        Dim dtContacto As New DataTable

        If pCodProveedor <> "0" Then
            dtContacto = GCCUtilitario.DeserializeObject(Of DataTable)(objContactoNTx.ListadoContacto(pPageSize, pCurrentPage, _
                                                                                                              pSortColumn, pSortOrder, _
                                                                                                              pCodProveedor))
        End If


        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtContacto.Rows.Count = 0 Then
            totalRecords = 0
        Else
            totalRecords = Convert.ToInt32(dtContacto.Rows(0)("RecordCount"))
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtContacto)



    End Function

    <WebMethod()> _
    Public Shared Function GuardarRegistro(ByVal pCodProveedor As String, _
                                         ByVal pCodProcedencia As String, _
                                         ByVal pCodTipoPersona As String, _
                                         ByVal pCodTipoDocumento As String, _
                                         ByVal pNumeroDocumento As String, _
                                         ByVal pRazonSocial As String, _
                                         ByVal pDireccion As String, _
                                         ByVal pCodPais As String, _
                                         ByVal pCodDepartamento As String, _
                                         ByVal pCodProvincia As String, _
                                         ByVal pCodDistrito As String, _
                                         ByVal parrContactos As String, _
                                         ByVal parrContactosEliminados As String, _
                                         ByVal parrCuentas As String, _
                                         ByVal parrCuentasEliminadas As String, _
                                         ByVal pOpcion As String, _
                                         ByVal pCodUnico As String) As String

        Try
            Dim objEProveedor As New EProveedor
            Dim objLProveedor As New LProveedorTx
            Dim pstrEProveedor As String = String.Empty
            Dim strXML As String = fGenerarXML(parrContactos, parrContactosEliminados, parrCuentas, parrCuentasEliminadas)

            With objEProveedor
                .Codproveedor = pCodProveedor
                .Codusuario = GCCSession.CodigoUsuario
                .CodProcedencia = pCodProcedencia
                .Codigotipopersona = pCodTipoPersona
                .CodTipoDocumento = pCodTipoDocumento
                .NumeroDocumento = pNumeroDocumento
                .RazonSocial = pRazonSocial
                .TextoDomicilioLegal = pDireccion
                .CodPais = pCodPais
                .CodDepartamento = pCodDepartamento
                .CodProvincia = pCodProvincia
                .CodDistrito = pCodDistrito
                .CodUnico = pCodUnico
                .XMLEntity = strXML

            End With
            pstrEProveedor = GCCUtilitario.SerializeObject(objEProveedor)
            Dim blnResultado As Boolean = False
            If pOpcion = "0" Then
                blnResultado = objLProveedor.fblnInsertarProveedor(pstrEProveedor)
            Else
                blnResultado = objLProveedor.fblnModificarProveedor(pstrEProveedor)
            End If

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

    Private Shared Function fGenerarXML(ByVal parrContactos As String, _
                                 ByVal parrContactosEliminados As String, _
                                 ByVal parrCuentas As String, _
                                 ByVal parrCuentasEliminadas As String) As String
        Dim sbXML As New StringBuilder
        sbXML.Append("<Root>")
        If parrContactos.Length > 0 Then
            Dim arrContactos() As String = parrContactos.Split("|")
            For i As Integer = LBound(arrContactos) To UBound(arrContactos)
                If arrContactos(i).Trim <> String.Empty Then
                    Dim arrDatos() As String = arrContactos(i).Split(";")
                    sbXML.Append(GCCUtilitario.Concatenar("<Contactos CodigoContacto=", Chr(39), arrDatos(0), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" Nombre=", Chr(39), arrDatos(1), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" Correo=", Chr(39), arrDatos(2), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" Telefono=", Chr(39), arrDatos(3), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" CodCargo=", Chr(39), arrDatos(4), Chr(39), " />"))
                End If
            Next
        End If

        If parrContactosEliminados.Length > 0 Then
            Dim arrContactosDel() As String = parrContactosEliminados.Split("|")
            For j As Integer = LBound(arrContactosDel) To UBound(arrContactosDel)
                If arrContactosDel(j).Trim <> String.Empty Then
                    sbXML.Append(GCCUtilitario.Concatenar("<ContactoEliminado CodigoContacto=", Chr(39), arrContactosDel(j), Chr(39), "/>"))
                End If
            Next
        End If

        If parrCuentas.Length > 0 Then
            Dim arrCuentas() As String = parrCuentas.Split("|")
            For k As Integer = LBound(arrCuentas) To UBound(arrCuentas)
                If arrCuentas(k).Trim <> String.Empty Then
                    Dim arrDatosC() As String = arrCuentas(k).Split(";")
                    sbXML.Append(GCCUtilitario.Concatenar("<Cuentas CodProveedorCuenta=", Chr(39), arrDatosC(0), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" CodTipoBanco=", Chr(39), arrDatosC(1), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" CodTipoCuenta=", Chr(39), arrDatosC(2), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" CodMoneda=", Chr(39), arrDatosC(3), Chr(39)))
                    sbXML.Append(GCCUtilitario.Concatenar(" NumeroCuenta=", Chr(39), arrDatosC(4), Chr(39), " />"))
                End If
            Next
        End If

        If parrCuentasEliminadas.Length > 0 Then
            Dim arrCuentasDel() As String = parrCuentasEliminadas.Split("|")
            For l As Integer = LBound(arrCuentasDel) To UBound(arrCuentasDel)
                If arrCuentasDel(l).Trim <> String.Empty Then
                    sbXML.Append(GCCUtilitario.Concatenar("<CuentaEliminada CodProveedorCuenta=", Chr(39), arrCuentasDel(l), Chr(39), "/>"))
                End If
            Next
        End If

        sbXML.Append("</Root>")
        Return sbXML.ToString()

    End Function

    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
    ''' <summary>
    ''' Consulta Datos RM
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 30/07/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ConsultaClienteRM(ByVal pstrTipoDoc As String, ByVal pstrNroRuc As String) As EClienteRM
        '-------------------------------------------
        ' Consulta Cliente RM
        '-------------------------------------------
        Dim intNumConsulta As Integer = 2
        Dim strCodUnico As String = ""
        Dim strTipoDoc As String = pstrTipoDoc
        Dim strNroRuc As String = pstrNroRuc
        Dim strMensaje As String = ""

        Try
            'Consulta RM
            Dim oEClienteRM As EClienteRM = GCCUtilitario.fObtenerDatosRMCliente(intNumConsulta, strCodUnico, strTipoDoc, strNroRuc, strMensaje)

            'Verifica Existencia en RM
            If Not oEClienteRM Is Nothing Then
                oEClienteRM.CodError = 0
                Return oEClienteRM
            Else
                oEClienteRM = New EClienteRM()
                If strMensaje.Trim().Equals("") Then
                    oEClienteRM.CodError = 1
                    oEClienteRM.MsgError = "El Código Unico ingresado no Existe."
                    Return oEClienteRM
                Else
                    oEClienteRM.CodError = 1
                    oEClienteRM.MsgError = strMensaje
                    Return oEClienteRM
                End If
            End If
        Catch ex As Exception
            Dim oEClienteRM As New EClienteRM
            oEClienteRM.CodError = 1
            oEClienteRM.MsgError = "No se pudo cargar los datos de RM."
            Return oEClienteRM
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function EliminarRegistro(ByVal pCodProveedor As String, _
                                         ByVal pCodEstadoLogico As String) As String

        Try

            Dim objLProveedor As New LProveedorTx
            Dim pstrEProveedor As String = String.Empty
            Dim pCodUsuario As String = GCCSession.NombreUsuario
            Dim blnResultado As Boolean = False
            blnResultado = objLProveedor.fblnEliminarProveedor(pCodProveedor, pCodEstadoLogico, pCodUsuario)

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
    Public Shared Function ListaProveedorDocumento(ByVal pCodProveedor As String) As String

        Try

            Dim objLProveedor As New LProveedorNTx
            Dim pstrEProveedor As String = String.Empty

            Dim blnResultado As DataTable
            blnResultado = GCCUtilitario.DeserializeObject(Of DataTable)(objLProveedor.ListaProveedorDocumento(pCodProveedor))
            Return blnResultado.Rows(0).Item("Total").ToString()

        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        End Try
    End Function
#End Region


End Class

