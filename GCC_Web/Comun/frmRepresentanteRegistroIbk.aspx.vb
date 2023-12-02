Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Comun_frmRepresentanteRegistroIbk
    Inherits GCCBase
    Public Const C_CODIGO_TIPO_REPRESENTANTE As String = "001"
    Dim objLog As New GCCLog("frmRepresentanteRegistroIbk.aspx.vb")

#Region "Eventos"
    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - IJM
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
                GCCUtilitario.CargarComboValorGenerico(Me.cmbcontratofirma, GCCConstante.C_TABLAGENERICA_FIRMA_EN)
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)
                hFirmaen.Value = Request.QueryString("hFirmaen").ToString.Trim()
                hubigeo.Value = Request.QueryString("hubigeo").ToString.Trim()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "cmbcontratofirma", "fn_util_SeteaComboServidor('cmbcontratofirma','" + RTrim(hFirmaen.Value) + "');", True)
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
    Protected Sub cmdguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdguardar.Click
        Dim objrepresentante As New EGcc_representante
        Dim objLCheckListTx As New LCheckListTx
        Dim pETContratoDocumento As String
        Try
            With objrepresentante
                .Codigotiporepresentante = C_CODIGO_TIPO_REPRESENTANTE
                .Nrodocumento = GCCUtilitario.NullableString(txtNroDocumento.Value)
                .Nombrerepresentante = GCCUtilitario.NullableString(txtNombreRepresentante.Value)
                .Partidaregistral = GCCUtilitario.NullableString(txtPartidaRegistral.Value)
                .Oficinaregistral = GCCUtilitario.NullableString(txtOficinaRegistral.Value)
                .Codigoubigeo = hddCadenaUbigeo.Value
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            pETContratoDocumento = GCCUtilitario.SerializeObject(objrepresentante)

            Dim objLCheckListNTx As New LCheckListNTx

            Dim dtRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLCheckListNTx.RepresentantesItem(GCCUtilitario.NullableString(txtNroDocumento.Value)))

            If dtRepresentante.Rows.Count = 0 Then
                Dim blnResult As Integer = objLCheckListTx.RepresentanteIns(pETContratoDocumento)
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Mensaje", "fn_MensajeValidaDni();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "cmbcontratofirma", "fn_util_SeteaComboServidor('cmbcontratofirma','" + RTrim(hFirmaen.Value) + "');", True)
            End If

        Catch ex As Exception
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub
#End Region

#Region "Web Metodos"

#End Region
   
End Class

