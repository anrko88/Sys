﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3053
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3053.
'
Namespace bseFCDO002
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="bseFCDO002Soap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class bseFCDO002
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private fConsultarClienteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.GCC.LogicWS.My.MySettings.Default.GCC_LogicWS_bseFCDO002_bseFCDO002
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event fConsultarClienteCompleted As fConsultarClienteCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/fConsultarCliente", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function fConsultarCliente(ByVal argFCDPARAMETRO As String, ByVal argFCDCODUNICO As String, ByVal argFCDTIPODOC As String, ByVal argFCDNUMDOC As String, ByVal argFCDCODPROD As String) As String
            Dim results() As Object = Me.Invoke("fConsultarCliente", New Object() {argFCDPARAMETRO, argFCDCODUNICO, argFCDTIPODOC, argFCDNUMDOC, argFCDCODPROD})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub fConsultarClienteAsync(ByVal argFCDPARAMETRO As String, ByVal argFCDCODUNICO As String, ByVal argFCDTIPODOC As String, ByVal argFCDNUMDOC As String, ByVal argFCDCODPROD As String)
            Me.fConsultarClienteAsync(argFCDPARAMETRO, argFCDCODUNICO, argFCDTIPODOC, argFCDNUMDOC, argFCDCODPROD, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub fConsultarClienteAsync(ByVal argFCDPARAMETRO As String, ByVal argFCDCODUNICO As String, ByVal argFCDTIPODOC As String, ByVal argFCDNUMDOC As String, ByVal argFCDCODPROD As String, ByVal userState As Object)
            If (Me.fConsultarClienteOperationCompleted Is Nothing) Then
                Me.fConsultarClienteOperationCompleted = AddressOf Me.OnfConsultarClienteOperationCompleted
            End If
            Me.InvokeAsync("fConsultarCliente", New Object() {argFCDPARAMETRO, argFCDCODUNICO, argFCDTIPODOC, argFCDNUMDOC, argFCDCODPROD}, Me.fConsultarClienteOperationCompleted, userState)
        End Sub
        
        Private Sub OnfConsultarClienteOperationCompleted(ByVal arg As Object)
            If (Not (Me.fConsultarClienteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent fConsultarClienteCompleted(Me, New fConsultarClienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub fConsultarClienteCompletedEventHandler(ByVal sender As Object, ByVal e As fConsultarClienteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class fConsultarClienteCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
End Namespace