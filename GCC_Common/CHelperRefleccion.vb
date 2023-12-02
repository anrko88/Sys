Imports System
Imports System.Reflection

Public Class CHelperRefleccion
    Public Const MemberAccess As BindingFlags = BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.[Static] Or BindingFlags.Instance Or BindingFlags.IgnoreCase

    Public Shared Function GetProperty(ByVal vObject As Object, ByVal vPropertyName As String) As Object
        Dim hasProperty As Boolean = False
        Dim properties As PropertyInfo() = vObject.[GetType]().GetProperties()
        For i As Integer = 0 To properties.Length - 1
            If properties(i).Name.ToUpper() = vPropertyName.ToUpper() Then
                hasProperty = True
                Exit For
            End If
        Next
        If hasProperty = True Then
            Return vObject.[GetType]().GetProperty(vPropertyName, MemberAccess).GetValue(vObject, Nothing)
        End If
        Return Nothing
    End Function

    ''' 
    ''' Object to retreve Field from
    ''' name of the field to retrieve
    ''' 
    Public Shared Function GetField(ByVal [Object] As Object, ByVal [Property] As String) As Object
        Return [Object].[GetType]().GetField([Property], MemberAccess).GetValue([Object])
    End Function

    ''' 
    ''' Returns a property or field value using a base object and sub members including . syntax.
    ''' For example, you can access: this.oCustomer.oData.Company with (this,"oCustomer.oData.Company")
    ''' 
    ''' Parent object to 'start' parsing from.
    ''' The property to retrieve. Example: 'oBus.oData.Company'
    ''' 
    Public Shared Function GetPropertyEx(ByVal Parent As Object, ByVal [Property] As String) As Object
        Dim Member As MemberInfo = Nothing
        Dim Type As Type = Parent.[GetType]()
        Dim lnAt As Integer = [Property].IndexOf(".")
        If lnAt < 0 Then
            If [Property] = "this" OrElse [Property] = "me" Then
                Return Parent
            End If

            ' *** Get the member
            Member = Type.GetMember([Property], MemberAccess)(0)
            If Member.MemberType = MemberTypes.[Property] Then
                Return DirectCast(Member, PropertyInfo).GetValue(Parent, Nothing)
            Else
                Return DirectCast(Member, FieldInfo).GetValue(Parent)
            End If
        End If

        ' *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
        Dim Main As String = [Property].Substring(0, lnAt)
        Dim Subs As String = [Property].Substring(lnAt + 1)
        ' *** Retrieve the current property

        Member = Type.GetMember(Main, MemberAccess)(0)

        Dim [Sub] As Object

        If Member.MemberType = MemberTypes.[Property] Then
            ' *** Get its value
            [Sub] = DirectCast(Member, PropertyInfo).GetValue(Parent, Nothing)
        Else
            [Sub] = DirectCast(Member, FieldInfo).GetValue(Parent)
        End If
        ' *** Recurse further into the sub-properties (Subs)
        Return GetPropertyEx([Sub], Subs)
    End Function

    ''' 
    ''' Sets the property on an object.
    ''' 
    ''' Object to set property on
    ''' Name of the property to set
    ''' value to set it to
    Public Shared Sub SetProperty(ByVal vObject As Object, ByVal vPropertyName As String, ByVal vValue As Object)
        Dim hasProperty As Boolean = False
        Dim properties As PropertyInfo() = vObject.[GetType]().GetProperties()
        For i As Integer = 0 To properties.Length - 1
            If properties(i).Name.ToUpper() = vPropertyName.ToUpper() Then
                hasProperty = True
                Exit For
            End If
        Next
        If hasProperty = True Then
            vObject.[GetType]().GetProperty(vPropertyName, MemberAccess).SetValue(vObject, vValue, Nothing)
        End If
    End Sub

    ''' 
    ''' Sets the field on an object.
    ''' 
    ''' Object to set property on
    ''' Name of the field to set
    ''' value to set it to
    Public Shared Sub SetField(ByVal [Object] As Object, ByVal [Property] As String, ByVal Value As Object)
        [Object].[GetType]().GetField([Property], MemberAccess).SetValue([Object], Value)
    End Sub

    Public Shared Function [GetType](ByVal vObject As Object, ByVal vPropertyName As String) As String

        Dim hasProperty As Boolean = False
        Dim properties As PropertyInfo() = vObject.[GetType]().GetProperties()
        For i As Integer = 0 To properties.Length - 1
            If properties(i).Name.ToUpper() = vPropertyName.ToUpper() Then
                hasProperty = True
                Exit For
            End If
        Next
        If hasProperty = True Then
            Return vObject.[GetType]().GetProperty(vPropertyName, MemberAccess).PropertyType.Name
        End If
        Return ""
    End Function

    ''' 
    ''' Sets the value of a field or property via Reflection. This method alws 
    ''' for using '.' syntax to specify objects multiple levels down.
    ''' 
    ''' wwUtils.SetPropertyEx(this,"Invoice.LineItemsCount",10)
    ''' 
    ''' which would be equivalent of:
    ''' 
    ''' this.Invoice.LineItemsCount = 10;
    ''' 
    ''' 
    ''' Object to set the property on.
    ''' 
    ''' 
    ''' Property to set. Can be an object hierarchy with . syntax.
    ''' 
    ''' 
    ''' Value to set the property to
    ''' 
    Public Shared Function SetPropertyEx(ByVal Parent As Object, ByVal [Property] As String, ByVal Value As Object) As Object
        Dim Type As Type = Parent.[GetType]()
        Dim Member As MemberInfo = Nothing

        ' *** no more .s - we got our final object
        Dim lnAt As Integer = [Property].IndexOf(".")
        If lnAt < 0 Then
            Member = Type.GetMember([Property], MemberAccess)(0)
            If Member.MemberType = MemberTypes.[Property] Then
                DirectCast(Member, PropertyInfo).SetValue(Parent, Value, Nothing)
                Return Nothing
            Else
                DirectCast(Member, FieldInfo).SetValue(Parent, Value)
                Return Nothing
            End If
        End If

        ' *** Walk the . syntax
        Dim Main As String = [Property].Substring(0, lnAt)
        Dim Subs As String = [Property].Substring(lnAt + 1)
        Member = Type.GetMember(Main, MemberAccess)(0)
        Dim [Sub] As Object
        If Member.MemberType = MemberTypes.[Property] Then
            [Sub] = DirectCast(Member, PropertyInfo).GetValue(Parent, Nothing)
        Else
            [Sub] = DirectCast(Member, FieldInfo).GetValue(Parent)
        End If
        ' *** Recurse until we get the lowest ref
        SetPropertyEx([Sub], Subs, Value)
        Return Nothing
    End Function

    ''' 
    ''' Wrapper method to call a 'dynamic' (non-typelib) method
    ''' on a COM object
    ''' 
    ''' 1st - Method name, 2nd - 1st parameter, 3rd - 2nd parm etc.
    ''' 
    Public Shared Function CallMethod(ByVal [Object] As Object, ByVal Method As String, ByVal ParamArray Params As Object()) As Object

        Return [Object].[GetType]().InvokeMember(Method, MemberAccess Or BindingFlags.InvokeMethod, Nothing, [Object], Params)
    End Function
End Class
'End Namespace