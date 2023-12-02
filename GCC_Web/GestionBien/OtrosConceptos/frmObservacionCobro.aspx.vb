
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class OtrosConceptos_frmObservacionCobro
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmObservacionCobro.aspx.vb")

#Region "   Eventos     "

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
                pCargarQueryString()
                pObtenerCobro()
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
    ''' Graba Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GrabaCobro(ByVal pstrNumeroContrato As String, _
                                      ByVal pstrTipoRubroFinanciamiento As String, _
                                      ByVal pstrCodIfi As String, _
                                      ByVal pstrTipoRecuperacion As String, _
                                      ByVal pstrNumSecRecuperacion As String, _
                                      ByVal pstrNumSecRecupComi As String, _
                                      ByVal pstrCodComisionTipo As String, _
                                      ByVal pstrObservaciones As String) As String

        Try
            Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision

            With objECreditoRecuperacionComision
                .CodOperacionActiva = pstrNumeroContrato
                .TipoRubroFinanciamiento = pstrTipoRubroFinanciamiento
                .CodIfi = pstrCodIfi
                .TipoRecuperacion = pstrTipoRecuperacion
                .NumSecRecuperacion = GCCUtilitario.CheckInt(pstrNumSecRecuperacion)
                .NumSecRecupComi = GCCUtilitario.CheckInt(pstrNumSecRecupComi)
                .CodComisionTipo = pstrCodComisionTipo
                .Observaciones = pstrObservaciones
                .FlagIndividual = "2"
            End With

            'Ejecuta Transaccion
            Dim objLCobroTx As New LCobroTx

            Dim blnResult As Boolean = False
            blnResult = objLCobroTx.fblnModificarCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision))

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

#Region "   Web Metodos "
    ''' <summary>
    ''' Función que Carga lo parametros de la url
    ''' </summary>    
    ''' <remarks>Creado por: TSF-WCR | Fecha:27/10/2011</remarks>
    Private Sub pCargarQueryString()        
        If Request.QueryString("csc") IsNot Nothing Then hidCodSolicitudCredito.Value = Request.QueryString("csc")
        If Request.QueryString("trf") IsNot Nothing Then hidTipoRubroFinanciamiento.Value = Request.QueryString("trf")
        If Request.QueryString("ci") IsNot Nothing Then hidCodIfi.Value = Request.QueryString("ci")
        If Request.QueryString("tre") IsNot Nothing Then hidTipoRecuperacion.Value = Request.QueryString("tre")
        If Request.QueryString("nsr") IsNot Nothing Then hidNumSecRecuperacion.Value = Request.QueryString("nsr")
        If Request.QueryString("nsrc") IsNot Nothing Then hidNumSecRecupComi.Value = Request.QueryString("nsrc")
        If Request.QueryString("cct") IsNot Nothing Then hidCodComisionTipo.Value = Request.QueryString("cct")        
    End Sub

    ''' <summary>
    ''' Obtener Cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Sub pObtenerCobro()
        Dim objLCobro As New LCobroNTx
        Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision
        Dim odtbCobro As DataTable

        Try
            With objECreditoRecuperacionComision
                .CodOperacionActiva = hidCodSolicitudCredito.Value
                .TipoRubroFinanciamiento = hidTipoRubroFinanciamiento.Value
                .CodIfi = hidCodIfi.Value
                .TipoRecuperacion = hidTipoRecuperacion.Value
                .NumSecRecuperacion = GCCUtilitario.CheckInt(hidNumSecRecuperacion.Value)
                .NumSecRecupComi = GCCUtilitario.CheckInt(hidNumSecRecupComi.Value)
                .CodComisionTipo = hidCodComisionTipo.Value
            End With

            odtbCobro = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobro.ObtenerCobro(GCCUtilitario.SerializeObject(objECreditoRecuperacionComision)))
            If odtbCobro IsNot Nothing Then
                If odtbCobro.Rows.Count > 0 Then
                    With odtbCobro.Rows(0)
                        txtObservaciones.Value = .Item("Observaciones").ToString
                    End With
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objLCobro = Nothing
        End Try
    End Sub

#End Region

End Class
