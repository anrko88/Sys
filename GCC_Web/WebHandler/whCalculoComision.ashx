<%@ WebHandler Language="VB" Class="whCalculoComision" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whCalculoComision : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim strCodigoConcepto As String = IIf(context.Request.Form("pstrCodigoConcepto") Is Nothing, "", context.Request.Form("pstrCodigoConcepto"))
        Dim strImporte As String = IIf(context.Request.Form("pstrImporte") Is Nothing, "0", context.Request.Form("pstrImporte"))
        Dim strCodMoneda As String = IIf(context.Request.Form("pstrCodMoneda") Is Nothing, "", context.Request.Form("pstrCodMoneda"))
        
        Dim objCobroNTx As New LCobroNTx
        strImporte = strImporte.Replace(",", "")
        Dim decImporte As Decimal = GCCUtilitario.CheckDecimal(strImporte)
        Dim strResultado As String = String.Empty
        Dim sbResultado As New StringBuilder
        Dim sbDatoAdicional As New StringBuilder
        Try
            Dim dtCalculo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.CalculoComision(strCodigoConcepto, decImporte, strCodMoneda))
                        
            If dtCalculo IsNot Nothing Then
                If dtCalculo.Rows.Count > 0 Then
                    With dtCalculo.Rows(0)                        
                        strResultado = GCCUtilitario.Concatenar("0|", _
                                                                .Item("MontoComision").ToString(), "|", _
                                                                .Item("Mensaje").ToString(), "|", _
                                                                .Item("PorcentajeIGV").ToString(), "|", _
                                                                .Item("MontoMinimo").ToString(), "|", _
                                                                .Item("MontoMaximo").ToString())
                        
                    End With
              
                End If
            End If
        Catch ex As Exception            
            strResultado = GCCUtilitario.Concatenar("1|", ex.Message)
        Finally
            objCobroNTx = Nothing
        End Try

        context.Response.Write(strResultado)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class