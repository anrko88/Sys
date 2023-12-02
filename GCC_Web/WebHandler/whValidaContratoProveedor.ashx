<%@ WebHandler Language="VB" Class="whValidaContratoProveedor" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whValidaContratoProveedor : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim strCodigoContratoProveedor As String = context.Request.Form("pstrCodigoContratoProveedor")
        Dim strCodProveedor As String = context.Request.Form("pstrCodProveedor")
        Dim strCodigoContacto As String = context.Request.Form("pstrCodigoContacto")
        Dim strNumeroContrato As String = context.Request.Form("pstrNumeroContrato")
        
        Dim objProveedorNTx As New LProveedorNTx
        Dim objEProveedor As New EGcc_contratoproveedor
        Dim intExiste As Integer = 0
        Dim sbResultado As New StringBuilder
        Dim sbDatoAdicional As New StringBuilder
        
        Try
            With objEProveedor
                .Codigocontratoproveedor = CInt(strCodigoContratoProveedor)
                .Codproveedor = strCodProveedor
                .Codigocontacto = strCodigoContacto
                .Numerocontrato = strNumeroContrato
            End With
                
            Dim dtContratoProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objProveedorNTx.ValidarContratoProveedor(GCCUtilitario.SerializeObject(objEProveedor)))
            If dtContratoProveedor IsNot Nothing Then
                Dim strMensaje As String = dtContratoProveedor.Rows(0).Item("Mensaje").ToString().Trim
                If strMensaje = String.Empty Then
                    sbResultado.Append("0|")
                Else
                    sbResultado.Append(GCCUtilitario.Concatenar("1|", strMensaje))
                End If
            End If
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