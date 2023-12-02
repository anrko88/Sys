<%@ WebHandler Language="VB" Class="whTemporal" %>

Imports System
Imports System.Web
Imports GCC.LogicWS
Imports GCC.Entity
Imports GCC.UI
Imports System.Data

Public Class whTemporal : Implements IHttpHandler
    
    ''' <summary>
    ''' Evento ProcessRequest
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/04/2011
    ''' </remarks>
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
                
        'Recoge Parametros del AJAX
        Dim strTipo As String = context.Request.Form("pstrTipo")
        'Dim strVariable As String = context.Request.Form("pstrVariable")
                
        'Ejecuta metodo segun Tipo
        Dim strResultado As String = ""
        If strTipo.Trim().Equals("1") Then
            strResultado = CargaCombo()
        ElseIf strTipo.Trim().Equals("2") then
            strResultado = CargaDatos()
        End If
        
        'Devuelve Resultado
        context.Response.Write(strResultado)
        
    End Sub
       

    ''' <summary>
    ''' Funcion CargaCombo
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/04/2011
    ''' </remarks>
    Private Function CargaCombo() As String
        
        Dim objETemporal As New ETemporal
        Dim objLTemporalNTx As New LTemporalNTx
        Dim sbResultado As New StringBuilder
        
        Try
            
            'Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLTemporalNTx.LeerTemporal(""))                        
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0","- Seleccione -"))
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("1","Datos AJAX 1"))
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("2","Datos AJAX 2"))
            
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            objLTemporalNTx = Nothing
        End Try
        
        Return sbResultado.ToString()
        
    End Function
 
 
    ''' <summary>
    ''' Funcion Carga Datos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/04/2011
    ''' </remarks>
    Private Function CargaDatos() As String
        
        Dim objETemporal As New ETemporal
        Dim objLTemporalNTx As New LTemporalNTx
        Dim sbResultado As New StringBuilder
        
        Try
            
            'Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLTemporalNTx.LeerTemporal(""))                                    
            sbResultado.Append(GCCUtilitario.Concatenar("0|", "20/04/2012"))
            sbResultado.Append(GCCUtilitario.Concatenar("|", "666"))
            sbResultado.Append(GCCUtilitario.Concatenar("|", "123.45"))
            sbResultado.Append(GCCUtilitario.Concatenar("|", "Este es un comentario"))
            sbResultado.Append(GCCUtilitario.Concatenar("|", "Texto"))
            sbResultado.Append(GCCUtilitario.Concatenar("|", "1"))
            
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            objLTemporalNTx = Nothing
        End Try
        
        Return sbResultado.ToString()
        
    End Function
    
    
    '***********************************************************
    ' Evento: IsReusable()
    '***********************************************************
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    
End Class
