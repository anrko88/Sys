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
Imports System.Data
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3053.
'
Namespace wsWIODesembolso
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="wiodesembolsoSoap", [Namespace]:="http://tempuri.org/wioWsDesembolso/wiodesembolso")>  _
    Partial Public Class wiodesembolso
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ListaxDesembolsarOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ListaOperacionesxPasoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ListaxEntregarOperationCompleted As System.Threading.SendOrPostCallback
        
        Private DesembolsarOperationCompleted As System.Threading.SendOrPostCallback
        
        Private EntregarOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ActualizarEstadoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private EstadoxNumeroInstruccionOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.GCC.LogicWS.My.MySettings.Default.GCC_LogicWS_wsWIODesembolso_wiodesembolso
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
        Public Event ListaxDesembolsarCompleted As ListaxDesembolsarCompletedEventHandler
        
        '''<remarks/>
        Public Event ListaOperacionesxPasoCompleted As ListaOperacionesxPasoCompletedEventHandler
        
        '''<remarks/>
        Public Event ListaxEntregarCompleted As ListaxEntregarCompletedEventHandler
        
        '''<remarks/>
        Public Event DesembolsarCompleted As DesembolsarCompletedEventHandler
        
        '''<remarks/>
        Public Event EntregarCompleted As EntregarCompletedEventHandler
        
        '''<remarks/>
        Public Event ActualizarEstadoCompleted As ActualizarEstadoCompletedEventHandler
        
        '''<remarks/>
        Public Event EstadoxNumeroInstruccionCompleted As EstadoxNumeroInstruccionCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/ListaxDesembolsar", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ListaxDesembolsar(ByVal argProducto As Integer, ByRef argdsDesembolsar As System.Data.DataSet, ByRef argsError As String) As Boolean
            Dim results() As Object = Me.Invoke("ListaxDesembolsar", New Object() {argProducto, argdsDesembolsar, argsError})
            argdsDesembolsar = CType(results(1),System.Data.DataSet)
            argsError = CType(results(2),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ListaxDesembolsarAsync(ByVal argProducto As Integer, ByVal argdsDesembolsar As System.Data.DataSet, ByVal argsError As String)
            Me.ListaxDesembolsarAsync(argProducto, argdsDesembolsar, argsError, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ListaxDesembolsarAsync(ByVal argProducto As Integer, ByVal argdsDesembolsar As System.Data.DataSet, ByVal argsError As String, ByVal userState As Object)
            If (Me.ListaxDesembolsarOperationCompleted Is Nothing) Then
                Me.ListaxDesembolsarOperationCompleted = AddressOf Me.OnListaxDesembolsarOperationCompleted
            End If
            Me.InvokeAsync("ListaxDesembolsar", New Object() {argProducto, argdsDesembolsar, argsError}, Me.ListaxDesembolsarOperationCompleted, userState)
        End Sub
        
        Private Sub OnListaxDesembolsarOperationCompleted(ByVal arg As Object)
            If (Not (Me.ListaxDesembolsarCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ListaxDesembolsarCompleted(Me, New ListaxDesembolsarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/ListaOperacionesxPaso", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ListaOperacionesxPaso(ByVal argProducto As Integer, ByVal argsPaso As String, ByRef argdsDesembolsar As System.Data.DataSet, ByRef argsError As String) As Boolean
            Dim results() As Object = Me.Invoke("ListaOperacionesxPaso", New Object() {argProducto, argsPaso, argdsDesembolsar, argsError})
            argdsDesembolsar = CType(results(1),System.Data.DataSet)
            argsError = CType(results(2),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ListaOperacionesxPasoAsync(ByVal argProducto As Integer, ByVal argsPaso As String, ByVal argdsDesembolsar As System.Data.DataSet, ByVal argsError As String)
            Me.ListaOperacionesxPasoAsync(argProducto, argsPaso, argdsDesembolsar, argsError, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ListaOperacionesxPasoAsync(ByVal argProducto As Integer, ByVal argsPaso As String, ByVal argdsDesembolsar As System.Data.DataSet, ByVal argsError As String, ByVal userState As Object)
            If (Me.ListaOperacionesxPasoOperationCompleted Is Nothing) Then
                Me.ListaOperacionesxPasoOperationCompleted = AddressOf Me.OnListaOperacionesxPasoOperationCompleted
            End If
            Me.InvokeAsync("ListaOperacionesxPaso", New Object() {argProducto, argsPaso, argdsDesembolsar, argsError}, Me.ListaOperacionesxPasoOperationCompleted, userState)
        End Sub
        
        Private Sub OnListaOperacionesxPasoOperationCompleted(ByVal arg As Object)
            If (Not (Me.ListaOperacionesxPasoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ListaOperacionesxPasoCompleted(Me, New ListaOperacionesxPasoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/ListaxEntregar", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ListaxEntregar(ByVal argProducto As Integer, ByRef argdsEntregar As System.Data.DataSet, ByRef argsError As String) As Boolean
            Dim results() As Object = Me.Invoke("ListaxEntregar", New Object() {argProducto, argdsEntregar, argsError})
            argdsEntregar = CType(results(1),System.Data.DataSet)
            argsError = CType(results(2),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ListaxEntregarAsync(ByVal argProducto As Integer, ByVal argdsEntregar As System.Data.DataSet, ByVal argsError As String)
            Me.ListaxEntregarAsync(argProducto, argdsEntregar, argsError, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ListaxEntregarAsync(ByVal argProducto As Integer, ByVal argdsEntregar As System.Data.DataSet, ByVal argsError As String, ByVal userState As Object)
            If (Me.ListaxEntregarOperationCompleted Is Nothing) Then
                Me.ListaxEntregarOperationCompleted = AddressOf Me.OnListaxEntregarOperationCompleted
            End If
            Me.InvokeAsync("ListaxEntregar", New Object() {argProducto, argdsEntregar, argsError}, Me.ListaxEntregarOperationCompleted, userState)
        End Sub
        
        Private Sub OnListaxEntregarOperationCompleted(ByVal arg As Object)
            If (Not (Me.ListaxEntregarCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ListaxEntregarCompleted(Me, New ListaxEntregarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/Desembolsar", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Desembolsar(ByVal argsNumDesembolso As String, ByVal argiProducto As Integer, ByVal argiNumInstruccion As Integer, ByVal argiNumIncidente As Integer, ByVal argsCodUsuario As String, ByVal argsNombreUsuario As String, ByVal argdFechaHoraLlegada As Date, ByRef argsError As String) As Boolean
            Dim results() As Object = Me.Invoke("Desembolsar", New Object() {argsNumDesembolso, argiProducto, argiNumInstruccion, argiNumIncidente, argsCodUsuario, argsNombreUsuario, argdFechaHoraLlegada, argsError})
            argsError = CType(results(1),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub DesembolsarAsync(ByVal argsNumDesembolso As String, ByVal argiProducto As Integer, ByVal argiNumInstruccion As Integer, ByVal argiNumIncidente As Integer, ByVal argsCodUsuario As String, ByVal argsNombreUsuario As String, ByVal argdFechaHoraLlegada As Date, ByVal argsError As String)
            Me.DesembolsarAsync(argsNumDesembolso, argiProducto, argiNumInstruccion, argiNumIncidente, argsCodUsuario, argsNombreUsuario, argdFechaHoraLlegada, argsError, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub DesembolsarAsync(ByVal argsNumDesembolso As String, ByVal argiProducto As Integer, ByVal argiNumInstruccion As Integer, ByVal argiNumIncidente As Integer, ByVal argsCodUsuario As String, ByVal argsNombreUsuario As String, ByVal argdFechaHoraLlegada As Date, ByVal argsError As String, ByVal userState As Object)
            If (Me.DesembolsarOperationCompleted Is Nothing) Then
                Me.DesembolsarOperationCompleted = AddressOf Me.OnDesembolsarOperationCompleted
            End If
            Me.InvokeAsync("Desembolsar", New Object() {argsNumDesembolso, argiProducto, argiNumInstruccion, argiNumIncidente, argsCodUsuario, argsNombreUsuario, argdFechaHoraLlegada, argsError}, Me.DesembolsarOperationCompleted, userState)
        End Sub
        
        Private Sub OnDesembolsarOperationCompleted(ByVal arg As Object)
            If (Not (Me.DesembolsarCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent DesembolsarCompleted(Me, New DesembolsarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/Entregar", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Entregar(ByVal argiProducto As Integer, ByVal argiNumInstruccion As Integer, ByVal argiNumIncidente As Integer, ByVal argsCodUsuario As String, ByVal argsNombreUsuario As String, ByRef argsError As String) As Boolean
            Dim results() As Object = Me.Invoke("Entregar", New Object() {argiProducto, argiNumInstruccion, argiNumIncidente, argsCodUsuario, argsNombreUsuario, argsError})
            argsError = CType(results(1),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub EntregarAsync(ByVal argiProducto As Integer, ByVal argiNumInstruccion As Integer, ByVal argiNumIncidente As Integer, ByVal argsCodUsuario As String, ByVal argsNombreUsuario As String, ByVal argsError As String)
            Me.EntregarAsync(argiProducto, argiNumInstruccion, argiNumIncidente, argsCodUsuario, argsNombreUsuario, argsError, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub EntregarAsync(ByVal argiProducto As Integer, ByVal argiNumInstruccion As Integer, ByVal argiNumIncidente As Integer, ByVal argsCodUsuario As String, ByVal argsNombreUsuario As String, ByVal argsError As String, ByVal userState As Object)
            If (Me.EntregarOperationCompleted Is Nothing) Then
                Me.EntregarOperationCompleted = AddressOf Me.OnEntregarOperationCompleted
            End If
            Me.InvokeAsync("Entregar", New Object() {argiProducto, argiNumInstruccion, argiNumIncidente, argsCodUsuario, argsNombreUsuario, argsError}, Me.EntregarOperationCompleted, userState)
        End Sub
        
        Private Sub OnEntregarOperationCompleted(ByVal arg As Object)
            If (Not (Me.EntregarCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent EntregarCompleted(Me, New EntregarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/ActualizarEstado", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ActualizarEstado(ByVal argNumIncidente As Integer) As Boolean
            Dim results() As Object = Me.Invoke("ActualizarEstado", New Object() {argNumIncidente})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ActualizarEstadoAsync(ByVal argNumIncidente As Integer)
            Me.ActualizarEstadoAsync(argNumIncidente, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ActualizarEstadoAsync(ByVal argNumIncidente As Integer, ByVal userState As Object)
            If (Me.ActualizarEstadoOperationCompleted Is Nothing) Then
                Me.ActualizarEstadoOperationCompleted = AddressOf Me.OnActualizarEstadoOperationCompleted
            End If
            Me.InvokeAsync("ActualizarEstado", New Object() {argNumIncidente}, Me.ActualizarEstadoOperationCompleted, userState)
        End Sub
        
        Private Sub OnActualizarEstadoOperationCompleted(ByVal arg As Object)
            If (Not (Me.ActualizarEstadoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ActualizarEstadoCompleted(Me, New ActualizarEstadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/wioWsDesembolso/wiodesembolso/EstadoxNumeroInstruccion", RequestNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", ResponseNamespace:="http://tempuri.org/wioWsDesembolso/wiodesembolso", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function EstadoxNumeroInstruccion(ByVal argsNumeroInstruccion As String, ByRef argsEstado As String, ByRef argsError As String) As Boolean
            Dim results() As Object = Me.Invoke("EstadoxNumeroInstruccion", New Object() {argsNumeroInstruccion, argsEstado, argsError})
            argsEstado = CType(results(1),String)
            argsError = CType(results(2),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub EstadoxNumeroInstruccionAsync(ByVal argsNumeroInstruccion As String, ByVal argsEstado As String, ByVal argsError As String)
            Me.EstadoxNumeroInstruccionAsync(argsNumeroInstruccion, argsEstado, argsError, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub EstadoxNumeroInstruccionAsync(ByVal argsNumeroInstruccion As String, ByVal argsEstado As String, ByVal argsError As String, ByVal userState As Object)
            If (Me.EstadoxNumeroInstruccionOperationCompleted Is Nothing) Then
                Me.EstadoxNumeroInstruccionOperationCompleted = AddressOf Me.OnEstadoxNumeroInstruccionOperationCompleted
            End If
            Me.InvokeAsync("EstadoxNumeroInstruccion", New Object() {argsNumeroInstruccion, argsEstado, argsError}, Me.EstadoxNumeroInstruccionOperationCompleted, userState)
        End Sub
        
        Private Sub OnEstadoxNumeroInstruccionOperationCompleted(ByVal arg As Object)
            If (Not (Me.EstadoxNumeroInstruccionCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent EstadoxNumeroInstruccionCompleted(Me, New EstadoxNumeroInstruccionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
    Public Delegate Sub ListaxDesembolsarCompletedEventHandler(ByVal sender As Object, ByVal e As ListaxDesembolsarCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ListaxDesembolsarCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argdsDesembolsar() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),System.Data.DataSet)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsError() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(2),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub ListaOperacionesxPasoCompletedEventHandler(ByVal sender As Object, ByVal e As ListaOperacionesxPasoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ListaOperacionesxPasoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argdsDesembolsar() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),System.Data.DataSet)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsError() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(2),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub ListaxEntregarCompletedEventHandler(ByVal sender As Object, ByVal e As ListaxEntregarCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ListaxEntregarCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argdsEntregar() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),System.Data.DataSet)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsError() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(2),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub DesembolsarCompletedEventHandler(ByVal sender As Object, ByVal e As DesembolsarCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class DesembolsarCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsError() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub EntregarCompletedEventHandler(ByVal sender As Object, ByVal e As EntregarCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class EntregarCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsError() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub ActualizarEstadoCompletedEventHandler(ByVal sender As Object, ByVal e As ActualizarEstadoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ActualizarEstadoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")>  _
    Public Delegate Sub EstadoxNumeroInstruccionCompletedEventHandler(ByVal sender As Object, ByVal e As EstadoxNumeroInstruccionCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class EstadoxNumeroInstruccionCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsEstado() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),String)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property argsError() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(2),String)
            End Get
        End Property
    End Class
End Namespace