Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LGestionBienDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 05/12/2012
''' </remarks>
<Guid("76A24A93-3173-46f2-9B67-BF6857F82DAD") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LGestionBienDocTx")> _
Public Class LGestionBienDocTx
    Inherits ServicedComponent
    Implements IGestionBienDocTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LGestionBienDocTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el Documento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializado de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 05/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean Implements IGestionBienDocTx.InsertarGestionBienDoc
        Dim objDGestionBienDocTx As DGestionBienDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDGestionBienDocTx = New DGestionBienDocTx
            blnResultado = objDGestionBienDocTx.InsertarGestionBienDoc(pEGestionBienDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDGestionBienDocTx.Dispose()
            objDGestionBienDocTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar el Documento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializada de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 05/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean Implements IGestionBienDocTx.ModificarGestionBienDoc
        Dim objDGestionBienDocTx As DGestionBienDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDGestionBienDocTx = New DGestionBienDocTx
            blnResultado = objDGestionBienDocTx.ModificarGestionBienDoc(pEGestionBienDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDGestionBienDocTx.Dispose()
            objDGestionBienDocTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Documento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializada de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 05/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean Implements IGestionBienDocTx.EliminarGestionBienDoc
        Dim objDGestionBienDocTx As DGestionBienDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDGestionBienDocTx = New DGestionBienDocTx
            blnResultado = objDGestionBienDocTx.EliminarGestionBienDoc(pEGestionBienDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDGestionBienDocTx.Dispose()
            objDGestionBienDocTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LGestionBienDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("C081CEA6-3739-4700-AC88-7FE07506ABD9") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LGestionBienDocNTx")> _
Public Class LGestionBienDocNTx
    Inherits ServicedComponent
    Implements IGestionBienDocNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LGestionBienDocNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos los Documento de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/12/2012
    ''' </remarks>
    Function ListadoGestionBienDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGestionBienDoc As String _
                                                ) As String Implements IGestionBienDocNTx.ListadoGestionBienDoc
        Dim objDGestionBienDocNTx As DGestionBienDocNTx = Nothing
        Dim strResultado As String

        Try
            objDGestionBienDocNTx = New DGestionBienDocNTx
            strResultado = objDGestionBienDocNTx.ListadoGestionBienDoc(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pEGestionBienDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDGestionBienDocNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el GestionBienDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 05/12/2012 
    ''' </remarks>
    Function ObtenerGestionBienDoc(ByVal pEGestionBienDoc As String) As String Implements IGestionBienDocNTx.ObtenerGestionBienDoc
        Dim objDGestionBienDocNTx As DGestionBienDocNTx = Nothing
        Dim strResultado As String

        Try
            objDGestionBienDocNTx = New DGestionBienDocNTx
            strResultado = objDGestionBienDocNTx.ObtenerGestionBienDoc(pEGestionBienDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDGestionBienDocNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

End Class

#End Region

