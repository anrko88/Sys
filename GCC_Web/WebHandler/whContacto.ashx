<%@ WebHandler Language="VB" Class="whContacto" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whContacto : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim strCodProveedor As String = context.Request.Form("pstrCodProveedor")
        
        Dim objProveedorNTx As New LProveedorNTx
        Dim intExiste As Integer = 0
        Dim sbResultado As New StringBuilder
        Dim sbDatoAdicional As New StringBuilder
        Try
            Dim dtContacto As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objProveedorNTx.ListadoContacto(100, 1, "", "", strCodProveedor))
            If dtContacto IsNot Nothing Then
                sbResultado.Append("0|")
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "- Seleccione -"))
                For Each oRow As DataRow In dtContacto.Rows
                    Dim strValue As String = String.Empty
                    Dim strText As String = String.Empty
                    Dim strCorreo As String = String.Empty
                    If Not oRow.Item("CodigoContacto") Is DBNull.Value Then
                        strValue = Trim(oRow.Item("CodigoContacto").ToString())
                    End If
                    If Not oRow.Item("Nombre") Is DBNull.Value Then
                        strText = Trim(oRow.Item("Nombre").ToString())
                    End If
                    If Not oRow.Item("Correo") Is DBNull.Value Then
                        strCorreo = Trim(oRow.Item("Correo").ToString())
                    End If
                    sbDatoAdicional.Append(GCCUtilitario.Concatenar(strValue, "*", strCorreo, ";"))
                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(strValue, strText))
                Next oRow
            End If
            sbResultado.Append(GCCUtilitario.Concatenar("|", sbDatoAdicional.ToString()))
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message, "|"))
        Finally
            objProveedorNTx = Nothing
        End Try
        context.Response.Write(sbResultado.ToString())
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class