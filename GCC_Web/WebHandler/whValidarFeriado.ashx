<%@ WebHandler Language="VB" Class="whValidarFeriado" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whValidarFeriado : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim strFlag As String = IIf(context.Request.Form("pstrFlag") Is Nothing, "0", context.Request.Form("pstrFlag"))
        Dim strFecha As String = IIf(context.Request.Form("pstrFecha") Is Nothing, "1900-01-01", context.Request.Form("pstrFecha"))
        Dim strFechaFin As String = IIf(context.Request.Form("pstrFechaFin") Is Nothing, "1900-01-01", context.Request.Form("pstrFechaFin"))        
        Dim strAddMonth As String = IIf(context.Request.Form("pstrAddMonth") Is Nothing, "0", context.Request.Form("pstrAddMonth"))
        
        
        Dim objCobroNTx As New LCobroNTx        
        Dim strResultado As String = String.Empty
        Dim intMonth As Integer = 0
        Dim strFechaCobro As String = String.Empty
        Dim intDias As Integer = 0
        Dim strValidacion As String = String.Empty
        Try
            If strFlag = "1" Then
                strValidacion = objCobroNTx.ValidarFeriado(strFecha)   
            Else If strFlag = "2" then 
                intMonth = GCCUtilitario.CheckInt(strAddMonth)
                If intMonth > 0 Then
                    strFechaCobro = GCCUtilitario.CheckDate(strFecha).AddDays(intMonth).ToString("yyyy-MM-dd")
                    intDias = DateDiff(DateInterval.Day, GCCUtilitario.CheckDate(strFecha), GCCUtilitario.CheckDate(strFechaCobro))
                    strFecha = strFechaCobro
                End If
                strValidacion = objCobroNTx.ValidarFeriado(strFecha)   
            Else If strFlag = "3" then                 
                intDias = DateDiff(DateInterval.Day, GCCUtilitario.CheckDate(strFecha), GCCUtilitario.CheckDate(strFechaFin))
                strValidacion = objCobroNTx.ValidarFeriado(strFechaFin)    
                strFecha = strFechaFin           
            End if
            strResultado = GCCUtilitario.Concatenar("0|", strValidacion, "*", intDias.ToString(), "*", strFecha)
            
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