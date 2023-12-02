Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class InsDesembolso_frmAbonoRegisto
    Inherits GCCBase

    Dim objLog As New GCCLog("frmAbonoRegisto.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
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

                Dim strNroContrato As String = Request.QueryString("cc")
                Dim strCodInsDesembolso As String = Request.QueryString("cid")
                Dim strCodProveedor As String = Request.QueryString("cpr")
                Dim strCodMoneda As String = Request.QueryString("cmo")


                Dim strImporteAgrupa As String = Request.QueryString("ImporteAgrupa")
                hddImporteAgrupa.Value = strImporteAgrupa
                hddCodigoContrato.Value = strNroContrato
                hddCodigoInsDesembolso.Value = strCodInsDesembolso
                hddCodProveedor.Value = strCodProveedor
                hddCodMoneda.Value = strCodMoneda


                'Combos
                GCCUtilitario.CargarComboMoneda(cmbMoneda)

                'Combo Proveedor
                Dim dtbInstruccionDesembolso As DataTable = New DataTable
                Dim objInstruccionDesembolso As Object = HttpContext.Current.Session("DTB_LISTACARGOABONO")
                If Not objInstruccionDesembolso Is Nothing Then
                    objInstruccionDesembolso = CType(objInstruccionDesembolso, DataTable)

                    'Proveedores
                    Dim dvwfilterInsDesembolso As DataView = objInstruccionDesembolso.DefaultView
                    dvwfilterInsDesembolso.RowFilter = " CODAGRUPACION = '02'"
                    dtbInstruccionDesembolso = New DataTable
                    dtbInstruccionDesembolso = dvwfilterInsDesembolso.ToTable

                    GCCUtilitario.pCargarHtmlSelect(cmbProveedor, dtbInstruccionDesembolso, "RAZONSOCIAL", "CodProveedor", "[-Seleccione-]", "0")

                End If

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
    ''' GrabaAbono
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaAbono(ByVal pstrCodContrato As String, _
                                      ByVal pstrCodInsDesembolso As String, _
                                      ByVal pstrCodProveedor As String, _
                                      ByVal pstrCodMoneda As String, _
                                      ByVal pstrCodMonto As String) As String

        Try
            Dim objInsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion

            With objInsDesembolsoAgrupacion
                .Codagrupacion = GCCConstante.C_COD_TIPOAGRUPACION_ID_ADELANTO
                .Codproveedor = GCCUtilitario.NullableString(pstrCodProveedor)
                .Codmonedadocumento = GCCUtilitario.NullableString(pstrCodMoneda)
                .Codmonedapago = GCCUtilitario.NullableString(pstrCodMoneda)
                .Codtipooperacion = GCCConstante.C_COD_TIPOOPERACION_ID_ABONO
                .Numerodocumento = ""
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrCodInsDesembolso)
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Montoretencion = 0
                .Montodetraccion = 0
                .Monto4ta = 0
                .Montototalpago = GCCUtilitario.StringToDecimal(pstrCodMonto)
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .PorcCalculo = GCCUtilitario.StringToDecimal("0")
            End With

            Dim objLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
            objLInstruccionDesembolsoTx.InsertarInsDesembolsoAgrupacion(GCCUtilitario.SerializeObject(objInsDesembolsoAgrupacion))

            'Actualiza Listado
            ListaCargoAbono(pstrCodContrato, pstrCodInsDesembolso)

            Return "0"

        Catch ex As Exception
            Return "1"
        End Try

    End Function

#End Region

#Region "Métodos"

    Public Shared Sub ListaCargoAbono(ByVal pstrNroContrato As String, _
                                            ByVal pstrNroInstruccion As String)

        Try
            'Variables
            Dim objLInstruccionDesembolsoNTx As New LInstruccionDesembolsoNTx

            'Inicializa Objeto
            Dim objEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
            Dim strEGCC_InsDesembolsoAgrupacion As String
            With objEGCC_InsDesembolsoAgrupacion
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrNroContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrNroInstruccion)
            End With
            strEGCC_InsDesembolsoAgrupacion = GCCUtilitario.SerializeObject(objEGCC_InsDesembolsoAgrupacion)

            'Ejecuta Consulta
            Dim dtInstruccionDesembolso As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono(strEGCC_InsDesembolsoAgrupacion))
            HttpContext.Current.Session("DTB_LISTACARGOABONO") = dtInstruccionDesembolso

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region


End Class
