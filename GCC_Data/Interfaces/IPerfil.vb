Imports System.Runtime.InteropServices

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IPerfilNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - KCC
''' Fecha de Creación  : 25/05/2012
''' </remarks>
<Guid("9E9A5A3A-C2B9-49ef-BD29-5D6F92A240DC")> _
Public Interface IPerfilNTx
    Function ListarPerfil() As String
    Function ObtenerPerfilxSda(ByVal intCodSda As Integer) As String
End Interface

#End Region
