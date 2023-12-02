<%@ WebHandler Language="VB" Class="whProveedor" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whProveedor : Implements IHttpHandler
    
    ''' <summary>
    ''' Evento ProcessRequest
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>   
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        'Recoge Parametros del AJAX
        Dim strRuc As String = context.Request.Form("pstrRuc")
        Dim strTipoDocumento As String = context.Request.Form("pstrTipoDocumento")
        
        Dim objProveedorNTx As New LProveedorNTx
        Dim intExiste As Integer = 0
        Dim strResultado As String = String.Empty
        Dim dtProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objProveedorNTx.ListadoProveedor(1, 1, "", "", "", strRuc, ""))
        Try
            strResultado = "No se encontro el número de documento|"
            If dtProveedor IsNot Nothing Then
                If dtProveedor.Rows.Count > 0 Then
                    If dtProveedor.Rows.Count = 1 Then
                        intExiste = 1
                        strResultado = GCCUtilitario.Concatenar(dtProveedor.Rows(0).Item("CodProveedor").ToString(), "|", _
                                                                dtProveedor.Rows(0).Item("RazonSocial").ToString(), "|", _
                                                                dtProveedor.Rows(0).Item("CodigoTipoDocumento").ToString())
                    Else
                        intExiste = 1
                        strResultado = GCCUtilitario.Concatenar(dtProveedor.Rows(0).Item("CodProveedor").ToString(), "|", _
                                                                dtProveedor.Rows(0).Item("RazonSocial").ToString(), "|", _
                                                                dtProveedor.Rows(0).Item("CodigoTipoDocumento").ToString())
                    End If
                End If
            End If
        Catch ex As Exception
            strResultado = GCCUtilitario.Concatenar(ex.Message, "|")
        Finally
            objProveedorNTx = Nothing
        End Try

        If intExiste = 1 Then
            context.Response.Write(GCCUtilitario.Concatenar("0|", strResultado, "||"))
        Else
            context.Response.Write(GCCUtilitario.Concatenar("1|", strResultado, "||"))
        End If
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class