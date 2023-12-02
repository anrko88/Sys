Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LCesionarioTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("196F7E43-2193-4eca-A7BB-C1E596CFB0EF") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCesionarioTx")> _
Public Class LCesionarioTx
    Inherits ServicedComponent
    Implements ICesionarioTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LCesionarioTx"
#End Region

#Region "Metodos"

#Region "Mnt Cesionario"

    ''' <summary>
    ''' Inserta el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarCesionario(ByVal pECesionario As String) As String Implements ICesionarioTx.InsertarCesionario
        Dim objDCesionarioTx As DCesionarioTx = Nothing
        Dim strResultado As String
        Try
            objDCesionarioTx = New DCesionarioTx
            strResultado = objDCesionarioTx.InsertarCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionarioTx.Dispose()
            objDCesionarioTx = Nothing
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Modificar el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCesionario(ByVal pECesionario As String) As Boolean Implements ICesionarioTx.ModificarCesionario
        Dim objDCesionarioTx As DCesionarioTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCesionarioTx = New DCesionarioTx
            blnResultado = objDCesionarioTx.ModificarCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionarioTx.Dispose()
            objDCesionarioTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarCesionario(ByVal pECesionario As String) As Boolean Implements ICesionarioTx.EliminarCesionario
        Dim objDCesionarioTx As DCesionarioTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCesionarioTx = New DCesionarioTx
            blnResultado = objDCesionarioTx.EliminarCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionarioTx.Dispose()
            objDCesionarioTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#Region "Mnt Representante"

    ''' <summary>
    ''' Inserta el Representante
    ''' </summary>
    ''' <param name="pERepresentante">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarRepresentante(ByVal pERepresentante As String) As String Implements ICesionarioTx.InsertarRepresentante
        Dim objDRepresentanteTx As DRepresentanteCesTx = Nothing
        Dim strResultado As Boolean
        Try
            objDRepresentanteTx = New DRepresentanteCesTx
            strResultado = objDRepresentanteTx.InsertarRepresentanteCes(pERepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Modificar el Representante
    ''' </summary>
    ''' <param name="pERepresentante">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarRepresentante(ByVal pERepresentante As String) As Boolean Implements ICesionarioTx.ModificarRepresentante
        Dim objDRepresentanteTx As DRepresentanteCesTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDRepresentanteTx = New DRepresentanteCesTx
            blnResultado = objDRepresentanteTx.ModificarRepresentanteCes(pERepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Representante
    ''' </summary>
    ''' <param name="pERepresentante">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarRepresentante(ByVal pERepresentante As String) As Boolean Implements ICesionarioTx.EliminarRepresentante
        Dim objDRepresentanteTx As DRepresentanteCesTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDRepresentanteTx = New DRepresentanteCesTx
            blnResultado = objDRepresentanteTx.EliminarRepresentanteCes(pERepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LCesionarioNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("C70D1CB4-2495-44fe-8E67-729891D05FBB") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCesionarioNTx")> _
Public Class LCesionarioNTx
    Inherits ServicedComponent
    Implements ICesionarioNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LCesionarioNTx"
#End Region

#Region "Metodos"

#Region "Mnt Cesionario"

    ''' <summary>
    ''' Lista todos los Cesionarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    Function ListadoCesionario(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECesionario As String _
                                                ) As String Implements ICesionarioNTx.ListadoCesionario
        Dim objDCesionarioNTx As DCesionarioNTx = Nothing
        Dim strResultado As String

        Try
            objDCesionarioNTx = New DCesionarioNTx
            strResultado = objDCesionarioNTx.ListadoCesionario(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pECesionario)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionarioNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Function ObtenerCesionario(ByVal pECesionario As String) As String Implements ICesionarioNTx.ObtenerCesionario
        Dim objDCesionarioNTx As DCesionarioNTx = Nothing
        Dim strResultado As String

        Try
            objDCesionarioNTx = New DCesionarioNTx
            strResultado = objDCesionarioNTx.ObtenerCesionario(pECesionario)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionarioNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#Region "Mnt Representante"

    ''' <summary>
    ''' Lista todos los Representantes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    Function ListadoRepresentante(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pERepresentante As String _
                                                ) As String Implements ICesionarioNTx.ListadoRepresentante
        Dim objDRepresentanteNTx As DRepresentanteCesNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteNTx = New DRepresentanteCesNTx
            strResultado = objDRepresentanteNTx.ListadoRepresentanteCes(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pERepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Representante
    ''' </summary>
    ''' <param name="pERepresentante">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Function ObtenerRepresentante(ByVal pERepresentante As String) As String Implements ICesionarioNTx.ObtenerRepresentante
        Dim objDRepresentanteNTx As DRepresentanteCesNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteNTx = New DRepresentanteCesNTx
            strResultado = objDRepresentanteNTx.ObtenerRepresentanteCes(pERepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#End Region

End Class

#End Region

