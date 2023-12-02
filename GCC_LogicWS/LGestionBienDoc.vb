Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

#Region "Clase No Transaccional"

Public Class LGestionBienDocNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/12/2012
    ''' </remarks>
    Public Function ListadoGestionBienDoc(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEGestionBienDocDoc As String) As String

        Dim objLGestionBienDocNTx As Object = CreateObject("GCC.Logic.LGestionBienDocNTx")
        Try
            Return objLGestionBienDocNTx.ListadoGestionBienDoc(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pEGestionBienDocDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el GestionBienDocDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDocDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 05/12/2012 
    ''' </remarks>
    Public Function ObtenerGestionBienDoc(ByVal pEGestionBienDocDoc As String) As String

        Dim objLGestionBienDocNTx As Object = CreateObject("GCC.Logic.LGestionBienDocNTx")
        Try
            Return objLGestionBienDocNTx.ObtenerGestionBienDoc(pEGestionBienDocDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "Clase Transaccional"

Public Class LGestionBienDocTx
    Inherits LUtilitario

    ''' <summary>
    ''' Ingresar Documento
    ''' </summary>
    ''' <param name="pEGestionBienDocDoc">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/12/2012
    ''' </remarks>
    Public Function InsertarGestionBienDoc(ByVal pEGestionBienDocDoc As String) As Boolean
        Dim objLGestionBienDocTx As Object = CreateObject("GCC.Logic.LGestionBienDocTx")
        Try
            Return objLGestionBienDocTx.InsertarGestionBienDoc(pEGestionBienDocDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar Documento
    ''' </summary>
    ''' <param name="pEGestionBienDocDoc">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/12/2012
    ''' </remarks>
    Public Function ModificarGestionBienDoc(ByVal pEGestionBienDocDoc As String) As Boolean
        Dim objLGestionBienDocTx As Object = CreateObject("GCC.Logic.LGestionBienDocTx")
        Try
            Return objLGestionBienDocTx.ModificarGestionBienDoc(pEGestionBienDocDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Documento
    ''' </summary>
    ''' <param name="pEGestionBienDocDoc">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/12/2012
    ''' </remarks>
    Public Function EliminarGestionBienDoc(ByVal pEGestionBienDocDoc As String) As Boolean
        Dim objLGestionBienDocTx As Object = CreateObject("GCC.Logic.LGestionBienDocTx")
        Try
            Return objLGestionBienDocTx.EliminarGestionBienDoc(pEGestionBienDocDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region