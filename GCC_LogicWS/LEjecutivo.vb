Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

#Region "Clase Transaccional"

Public Class LEjecutivoTx

    ''' <summary>
    ''' Ingresa Ejecutivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>    
    Public Function InsertarEjecutivo(ByVal pEEjecutivo As String) As Boolean
        Dim objLEjecutivoTx As Object = CreateObject("GCC.Logic.LEjecutivoTx")
        Try
            Return objLEjecutivoTx.InsertarEjecutivo(pEEjecutivo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica Ejecutivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>    
    Public Function ModificarEjecutivo(ByVal pEEjecutivo As String) As Boolean
        Dim objLEjecutivoTx As Object = CreateObject("GCC.Logic.LEjecutivoTx")
        Try
            Return objLEjecutivoTx.ModificarEjecutivo(pEEjecutivo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina Ejecutivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>    
    Public Function EliminarEjecutivo(ByVal pEEjecutivo As String) As Boolean
        Dim objLEjecutivoTx As Object = CreateObject("GCC.Logic.LEjecutivoTx")
        Try
            Return objLEjecutivoTx.EliminarEjecutivo(pEEjecutivo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "Clase NO Transaccional"

Public Class LEjecutivoNTx

    ''' <summary>
    ''' Listado de Ejecutivos
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>
    Public Function ListadoEjecutivo(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pEEjecutivo As String) As String
        Dim objLEjecutivoNTx As Object = CreateObject("GCC.Logic.LEjecutivoNTx")

        Try
            Return objLEjecutivoNTx.ListadoEjecutivo(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEEjecutivo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener Ejecutivo
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>
    Public Function ObtenerEjecutivo(ByVal pstrCodTabla As String, _
                                     ByVal pstrCodigo As String) As String
        Dim objLEjecutivoNTx As Object = CreateObject("GCC.Logic.LEjecutivoNTx")

        Try
            Return objLEjecutivoNTx.ObtenerEjecutivo(pstrCodTabla, pstrCodigo)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class

#End Region


