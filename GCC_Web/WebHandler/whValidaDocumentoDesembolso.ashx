<%@ WebHandler Language="VB" Class="whValidaDocumentoDesembolso" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whValidaDocumentoDesembolso : Implements IHttpHandler
    
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
        Dim strCodigoContrato As String = context.Request.Form("CodigoContrato")
        Dim strTipoDocumento As String = context.Request.Form("TipoDocumento")
        Dim strNroDocumento As String = context.Request.Form("NroDocumento")
        Dim strFechaEmision As String = context.Request.Form("FechaEmision")
        Dim strCodProveedor As String = context.Request.Form("CodProveedor")
        Dim strKeyTipoComprobante As String = context.Request.Form("KeyTipoComprobante")
        Dim strKeyNumeroComprobante As String = context.Request.Form("KeyNumeroComprobante")
        Dim strKeyFechaEmision As String = context.Request.Form("KeyFechaEmision")
        Dim strKeyCodProveedor As String = context.Request.Form("KeyCodProveedor")
        Dim strEstadoDocumento As String = context.Request.Form("EstadoDocumento")
        
               
        Dim objLDesembolsoNTx As New LDesembolsoNTx
        Dim objESolicitudCreditoEstructuraDoc As New ESolicitudcreditoestructuradoc
        With objESolicitudCreditoEstructuraDoc
            .Codsolicitudcredito = strCodigoContrato
            .Tipodocumento = strTipoDocumento
            .Nrodocumento = strNroDocumento
            .Fechaemision = GCCUtilitario.StringToDateTime(strFechaEmision)
            .CodProveedor = strCodProveedor
            .KeyTipoComprobante = strKeyTipoComprobante
            .KeyNumeroComprobante = strKeyNumeroComprobante
            If strKeyFechaEmision = "" Then strKeyFechaEmision = "19000101"
            .KeyFechaEmision = GCCUtilitario.StringToDateTime(strKeyFechaEmision)
            .KeyCodProveedor = strKeyCodProveedor
            .Estadodocumento = strEstadoDocumento
        End With
            
        Dim intExiste As Integer = 0
        Dim strResultado As String = String.Empty
        Dim dtDocumento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLDesembolsoNTx.ValidaContratoEstructDoc(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(objESolicitudCreditoEstructuraDoc)))
        Try            
            If dtDocumento IsNot Nothing Then
                If dtDocumento.Rows.Count > 0 Then
                    If dtDocumento.Rows.Count = 1 Then
                        strResultado = GCCUtilitario.Concatenar("0|", dtDocumento.Rows(0).Item("Existe").ToString())
                    End If
                Else
                    strResultado = GCCUtilitario.Concatenar("1|0")
                End If
            Else
                strResultado = GCCUtilitario.Concatenar("1|0")
            End If
        Catch ex As Exception
            strResultado = GCCUtilitario.Concatenar("1|", ex.Message)
        Finally
            objLDesembolsoNTx = Nothing
        End Try
        context.Response.Write(strResultado)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class