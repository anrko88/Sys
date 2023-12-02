
Public Class LConfiguracion
   
    ''' <summary>
    ''' Obtener Validacion de Rol por SDA
    ''' </summary>
    ''' <param name="strUsuario"></param>
    ''' <param name="strPin"></param>
    ''' <param name="strDBServer"></param>
    ''' <param name="strDBName"></param>
    ''' <param name="strPerfil"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC 
    ''' Fecha de Creación  : 11/07/2011 
    ''' </remarks>
    Public Function ValidarAccesoUsuario(ByVal strUsuario As String, _
                                         ByVal strPin As String, _
                                         ByVal strDBServer As String, _
                                         ByVal strDBName As String, _
                                         ByVal strPerfil As String) As String
        Try
            Dim objLRolNTx As Object = CreateObject("GCC.Logic.LConfiguracionNTx")
            Return objLRolNTx.ValidarAccesoUsuario(strUsuario, strPin, strDBServer, strDBName, strPerfil)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los datos del perfil ingresado desde SDA
    ''' </summary>
    ''' <param name="pintCodRolSDA"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 23/05/2012
    ''' </remarks>
    Public Function ObtenerRolSDA(ByVal pintCodRolSDA As Integer) As String
        Try
            Dim objLRolNTx As Object = CreateObject("GCC.Logic.LConfiguracionNTx")
            Return objLRolNTx.ObtenerRolSDA(pintCodRolSDA)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de todos los roles activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    '''  Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 23/05/2012
    ''' </remarks>
    Public Function ListarRolActivo() As String
        Try
            Dim objLRolNTx As Object = CreateObject("GCC.Logic.LConfiguracionNTx")
            Return objLRolNTx.ListarRolActivo()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
