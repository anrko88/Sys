Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

#Region "Clase No Transaccional"

Public Class LCesionarioNTx
    Inherits LUtilitario

#Region "Mnt Cesionario"

    ''' <summary>
    ''' Lista todos los cesionarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function ListadoCesionario(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pECesionario As String) As String

        Dim objLCesionarioNTx As Object = CreateObject("GCC.Logic.LCesionarioNTx")
        Try
            Return objLCesionarioNTx.ListadoCesionario(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pECesionario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 07/01/2013
    ''' </remarks>
    Public Function ObtenerCesionario(ByVal pECesionario As String) As String

        Dim objLCesionarioNTx As Object = CreateObject("GCC.Logic.LCesionarioNTx")
        Try
            Return objLCesionarioNTx.ObtenerCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Mnt Representante"

    ''' <summary>
    ''' Lista todos los representantes de un cesionario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function ListadoRepresentante(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pERepresentante As String) As String

        Dim objLCesionarioNTx As Object = CreateObject("GCC.Logic.LCesionarioNTx")
        Try
            Return objLCesionarioNTx.ListadoRepresentante(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pERepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el Representante de un cesionario
    ''' </summary>
    ''' <param name="pERepresentante">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 07/01/2013
    ''' </remarks>
    Public Function ObtenerRepresentante(ByVal pERepresentante As String) As String

        Dim objLCesionarioNTx As Object = CreateObject("GCC.Logic.LCesionarioNTx")
        Try
            Return objLCesionarioNTx.ObtenerRepresentante(pERepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

End Class

#End Region

#Region "Clase Transaccional"

Public Class LCesionarioTx
    Inherits LUtilitario

#Region "Mnt Cesionario"

    ''' <summary>
    ''' Ingresar Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function InsertarCesionario(ByVal pECesionario As String) As String
        Dim objLCesionarioTx As Object = CreateObject("GCC.Logic.LCesionarioTx")
        Try
            Return objLCesionarioTx.InsertarCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function ModificarCesionario(ByVal pECesionario As String) As Boolean
        Dim objLCesionarioTx As Object = CreateObject("GCC.Logic.LCesionarioTx")
        Try
            Return objLCesionarioTx.ModificarCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function EliminarCesionario(ByVal pECesionario As String) As Boolean
        Dim objLCesionarioTx As Object = CreateObject("GCC.Logic.LCesionarioTx")
        Try
            Return objLCesionarioTx.EliminarCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Mnt Representante"

    ''' <summary>
    ''' Ingresar Representante
    ''' </summary>
    ''' <param name="pERepresentante">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function InsertarRepresentante(ByVal pERepresentante As String) As Boolean
        Dim objLCesionarioTx As Object = CreateObject("GCC.Logic.LCesionarioTx")
        Try
            Return objLCesionarioTx.InsertarRepresentante(pERepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar Representante
    ''' </summary>
    ''' <param name="pERepresentante">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function ModificarRepresentante(ByVal pERepresentante As String) As Boolean
        Dim objLCesionarioTx As Object = CreateObject("GCC.Logic.LCesionarioTx")
        Try
            Return objLCesionarioTx.ModificarRepresentante(pERepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Representante
    ''' </summary>
    ''' <param name="pERepresentante">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function EliminarRepresentante(ByVal pERepresentante As String) As Boolean
        Dim objLCesionarioTx As Object = CreateObject("GCC.Logic.LCesionarioTx")
        Try
            Return objLCesionarioTx.EliminarRepresentante(pERepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region