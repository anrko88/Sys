Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Temporal_frmTemporalEditarGrilla
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarCombo(GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
        End If
    End Sub

    Public Sub CargarCombo(ByVal pstrTablaGenerica As String)
        Dim objLMantenimientolNTx As New LMantenimientoNTX
        Dim sbResultado As String


        Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLMantenimientolNTx.ListarTablaGenerica(pstrTablaGenerica))

        For Each oRow As DataRow In dtDatos.Rows
            Dim value As String = ""
            Dim text As String = ""
            If Not oRow.Item("CODIGO") Is DBNull.Value Then
                value = Trim(oRow.Item("CODIGO").ToString())
            End If
            If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                text = Trim(oRow.Item("DESCRIPCION").ToString())
            End If

            sbResultado = sbResultado + value + ":" + text + ";"
            hidCargarComboClasificacion.Value = "" + Mid(sbResultado, 1, sbResultado.Length - 1) + ""
        Next oRow

    End Sub
    <WebMethod()> _
   Public Shared Function GuardarRegistro(ByVal pstrNumeroContrato As String, _
                                      ByVal pstrFechaTerminoRecepDocProv As String, _
                                      ByVal pstrFlagFechaTerminoRecepDocProv As String, _
                                      ByVal pstrDescripcionBien As String) As String
        Dim oESolicitudCredito As New ESolicitudcredito
        Dim objLSolicitudCredito As New LProveedorTx
        Try

            With oESolicitudCredito
                .Codsolicitudcredito = pstrNumeroContrato
                If pstrFlagFechaTerminoRecepDocProv = "1" Then
                    .FlagTerminoRecepDocumentoProv = 1
                    .FechaTerminoRecepDocumentoProv = GCCUtilitario.StringToDateTime(pstrFechaTerminoRecepDocProv)
                Else
                    .FlagTerminoRecepDocumentoProv = 0
                    .FechaTerminoRecepDocumentoProv = GCCUtilitario.StringToDateTime("1/1/1900")
                End If
                .DescripcionBien = pstrDescripcionBien

            End With

            Dim blnResult As Boolean = False 'objLSolicitudCredito.ModificaSolicitudDocumentoProveedor(GCCUtilitario.SerializeObject(oESolicitudCredito))

            If blnResult Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Dim myException As String

            myException = ManageException(ex)
            Return myException
        Finally

        End Try
    End Function
    Private Shared Function ManageException(ByVal ex As Exception) As String
        Dim strErrorMessage As String = ex.ToString()

        Return strErrorMessage
    End Function
End Class
