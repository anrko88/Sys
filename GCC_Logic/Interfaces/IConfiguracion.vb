Imports System.Runtime.InteropServices

#Region "Interface No Transaccional"
'-----------------------------------------------------------------------------
'Nombre             : IConfiguracionNTx
'Objetivo           : Interfaz de métodos para la clase IConfiguracionNTx
'Creado Por         : TSF - KCC
'Fecha de Creación  : 22/05/2012 
'-----------------------------------------------------------------------------
<Guid("93DCC7E1-04B9-4117-8683-FD2D3BBAE858")> _
Public Interface IConfiguracionNTx
    Function ValidarAccesoUsuario(ByVal strUsuario As String, _
                                  ByVal strPin As String, _
                                  ByVal strDBServer As String, _
                                  ByVal strDBName As String, _
                                  ByVal strPerfil As String) As String
    Function ObtenerRolSDA(ByVal pintCodRolSDA As Integer) As String
    Function ListarRolActivo() As String
End Interface

#End Region
