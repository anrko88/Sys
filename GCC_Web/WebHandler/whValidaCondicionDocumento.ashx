<%@ WebHandler Language="VB" Class="whValidaCondicionDocumento" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whValidaCondicionDocumento : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim sbResul As New StringBuilder
      
        Dim strNumeroContrato As String = context.Request.Form("pstrNumeroContrato")
        Dim strCodTipoCondicion As String = context.Request.Form("pstrCodigoTipoCondicion")
        Dim intFlagEnvioCarta as Integer= Convert.ToInt32(context.Request.Form("pintFlagCartaEnvio"))
        Dim oLwsCheckListNTx As New LCheckListNTx
        Dim objEContratoDocumento As New EGcc_contratodocumento
        Dim intExiste As Integer = 0
        Dim strEEContratoDocumento As String = ""
        Dim sbDatoAdicional As New StringBuilder
        
        
        If strNumeroContrato.Trim <> "" And strCodTipoCondicion.Trim <> "" Then
            Try
                With objEContratoDocumento
                    .Numerocontrato = strNumeroContrato
                    .Codigotipocondicion = strCodTipoCondicion
                    .Flagcartaenvio = intFlagEnvioCarta
                End With
            
            
                strEEContratoDocumento = GCCUtilitario.SerializeObject(Of EGcc_contratodocumento)(objEContratoDocumento)
                strEEContratoDocumento = oLwsCheckListNTx.SelCondicionesDocumentoCli(strEEContratoDocumento)
                
                If strEEContratoDocumento IsNot Nothing Then
                    If strEEContratoDocumento <> "" Then
                        objEContratoDocumento = GCCUtilitario.DeserializeObject(Of EGcc_contratodocumento)(strEEContratoDocumento)
                        With objEContratoDocumento
                            sbResul.Append(GCCUtilitario.Concatenar("1|", .Existe.ToString))
                        End With
                    Else
                        sbResul.Append("0|")
                    End If
                Else
                    sbResul.Append("0|")
                End If
                
                
            Catch ex As Exception
                sbResul.Append("0|Se ha producido un error el obtener el registro")
            End Try
        End If
        context.Response.Write(sbResul.ToString())
            
            
            
            
                
        '    Dim dtContratoProveedor As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objProveedorNTx.ValidarContratoProveedor(GCCUtilitario.SerializeObject(objEProveedor)))
        '    If dtContratoProveedor IsNot Nothing Then
        '        Dim strMensaje As String = dtContratoProveedor.Rows(0).Item("Mensaje").ToString().Trim
        '        If strMensaje = String.Empty Then
        '            sbResultado.Append("0|")
        '        Else
        '            sbResultado.Append(GCCUtilitario.Concatenar("1|", strMensaje))
        '        End If
        '    End If
        'Catch ex As Exception
        '    sbResultado = New StringBuilder
        '    sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message, "|"))
        'Finally
        '    objProveedorNTx = Nothing
        'End Try
        'context.Response.Write(sbResultado.ToString())
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class