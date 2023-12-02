<%@ WebHandler Language="VB" Class="whObtenerContrato" %>

Imports System
Imports System.Web
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Public Class whObtenerContrato : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim strNumeroContrato As String = context.Request.Form("pstrNumeroContrato")
        
        Dim objCobroNTx As New LCobroNTx
        Dim intExiste As Integer = 0        
        Dim strResultado As String = String.Empty
        Dim sbResultado As New StringBuilder
        Dim sbDatoAdicional As New StringBuilder
        Try
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.ObtenerContratoCobro(strNumeroContrato))
            
            strResultado = "No se encontro el número de contrato"
            If dtContrato IsNot Nothing Then
                If dtContrato.Rows.Count > 0 Then
                    With dtContrato.Rows(0)
                        If GCCUtilitario.CheckInt(.Item("EstadoIncorrecto").ToString()) = 1 Then
                            Dim strFechaActivacion As String = String.Empty
                            Dim strFechaVencimientoOperacion As String = String.Empty
                            
                            strFechaActivacion = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(.Item("FechaActivacion").ToString()))
                            strFechaVencimientoOperacion = GCCUtilitario.fSetearFecha(GCCUtilitario.CheckDate(.Item("FechaVencimientoOperacion").ToString()))
                            
                            intExiste = 1
                            strResultado = GCCUtilitario.Concatenar(.Item("CodSolicitudCredito").ToString(), "|", _
                                                                    .Item("CodigoTipoDocumento").ToString(), "|", _
                                                                    .Item("TipoDocumento").ToString(), "|", _
                                                                    .Item("NumeroDocumento").ToString(), "|", _
                                                                    .Item("ClienteRazonSocial").ToString(), "|", _
                                                                    .Item("CodUnico").ToString(), "|", _
                                                                    .Item("CodMoneda").ToString(), "|", _
                                                                    .Item("DescripCortoMoneda").ToString(), "|", _
                                                                    .Item("NombreMoneda").ToString(), "|", _
                                                                    .Item("CodigoEstadoContrato").ToString(), "|", _
                                                                    .Item("EstadoContrato").ToString(), "|", _
                                                                    strFechaActivacion, "|", strFechaVencimientoOperacion, "|", _
                                                                    .Item("PorcentajeComisionSC").ToString())
                        Else
                            strResultado = GCCUtilitario.Concatenar("No se puede seleccionar contrato en estado ", .Item("EstadoContrato").ToString().ToLower)
                        End If
                        
                    End With
              
                End If
            End If           
        Catch ex As Exception
            intExiste = 0
            strResultado = ex.Message
        Finally
            objCobroNTx = Nothing
        End Try
        
        If intExiste = 1 Then
            context.Response.Write(GCCUtilitario.Concatenar("0|", strResultado))
        Else
            context.Response.Write(GCCUtilitario.Concatenar("1|", strResultado))
        End If
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class