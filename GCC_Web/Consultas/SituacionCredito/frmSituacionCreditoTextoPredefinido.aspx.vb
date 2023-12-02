Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class Consultas_frmSituacionCreditoTextoPredefinido
    Inherits GCCBase


    ReadOnly objLog As New GCCLog("frmTextoPredefinido.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
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
                hddCodigoContrato.Value = Request.QueryString("CodigoContrato")
                hddCodigoContratoDocumento.Value = Request.QueryString("CodigoContratoDocumento")
                hddEdita.Value = Request.QueryString("edita").Trim


                Call LeerTextoPredefinido(CType(hddCodigoContratoDocumento.Value, Integer))
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

    ''' <summary>
    ''' Guarda el texto que editó el usuario a partir del texto predefinido usado como plantilla.
    ''' </summary>
    ''' <param name="strNroContrato">Número del contrato</param>
    ''' <param name="intCodigoContratoDocumento">Número del documento del contrato</param>
    ''' <param name="strTextoPredefinido">Texto predefinido que editó el usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GuardarTextoPredefinido(ByVal strNroContrato As String, _
                                                   ByVal intCodigoContratoDocumento As Integer, _
                                                   ByVal strTextoPredefinido As String) As String
        Try
            Dim objContratoTx As New LContratoTx

            Dim objEgccContratoDocumento As New EGcc_contratodocumento
            Dim pEgccContratoDocumento As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String

            With objEgccContratoDocumento
                .Codigocontratodocumento = intCodigoContratoDocumento
                .TextoPredefinido = GCCUtilitario.NullableString(strTextoPredefinido)
            End With

            objESolicitudCredito.Codsolicitudcredito = strNroContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pEgccContratoDocumento = GCCUtilitario.SerializeObject(objEgccContratoDocumento)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim result As Boolean = objContratoTx.fblnModificarTextoPredefinido(pESolicitudCredito, _
                                                                                pEgccContratoDocumento)

            If result Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lee el texto predefinido de acuerdo a su tipo de condición.
    ''' </summary>
    ''' <param name="intCodigoContratoDocumento">Número del documento del contrato</param>
    ''' <remarks></remarks>
    Private Sub LeerTextoPredefinido(ByVal intCodigoContratoDocumento As Integer)

        Dim objLContratoNTx As New LContratoNTx

        Dim dtContratoDocumento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLContratoNTx.LeerTextoPredefinido(intCodigoContratoDocumento))

        For Each oRow As DataRow In dtContratoDocumento.Rows
            If Not oRow.Item("TextoPredefinido") Is DBNull.Value Then
                txaTextoPredefinido.Value = oRow.Item("TextoPredefinido").ToString()
                hddNuevo.Value = "N"
            Else
                hddNuevo.Value = "S"

                If Not oRow.Item("CodigoTipoCondicion") Is DBNull.Value Then
                    If oRow.Item("CodigoTipoCondicion").ToString().TrimEnd = GCCConstante.C_TEXTO_PREDEF_FIANZA_SOL Then
                        txaTextoPredefinido.Value = GCCConstante.TEXTO_PREDEF_FIANZA_SOLIDARIA
                    Else
                        txaTextoPredefinido.Value = GCCConstante.TEXTO_PREDEF_CESIONARIO
                    End If
                End If
            End If
        Next oRow
    End Sub

#End Region

End Class
