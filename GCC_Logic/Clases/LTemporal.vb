Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LTemporalTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("6F7C94D9-6E0B-4401-AB65-B4973C162722") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LTemporalTx")> _
Public Class LTemporalTx
    Inherits ServicedComponent
    Implements ITemporalTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LTemporalTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Ingresa nueva Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarTemporal(ByVal pETemporal As String) As Integer Implements ITemporalTx.InsertarTemporal
        Dim objDTemporalTx As DTemporalTx = Nothing
        Dim iResultado As Integer

        Try
            objDTemporalTx = New DTemporalTx
            iResultado = objDTemporalTx.InsertarTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalTx.Dispose()
            objDTemporalTx = Nothing
        End Try

        Return iResultado
    End Function

    ''' <summary>
    ''' Modifica Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarTemporal(ByVal pETemporal As String) As Boolean Implements ITemporalTx.ModificarTemporal
        Dim objDTemporalTx As DTemporalTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDTemporalTx = New DTemporalTx
            blnResultado = objDTemporalTx.ModificarTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalTx.Dispose()
            objDTemporalTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnEliminarTemporal(ByVal pETemporal As String) As Boolean Implements ITemporalTx.EliminarTemporal
        Dim objDTemporalTx As DTemporalTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDTemporalTx = New DTemporalTx
            blnResultado = objDTemporalTx.EliminarTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalTx.Dispose()
            objDTemporalTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LTemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("9E4CF500-C0B9-4363-835B-D48D253C5E91") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LTemporalNTx")> _
Public Class LTemporalNTx
    Inherits ServicedComponent
    Implements ITemporalNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LTemporalNTx"
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Obtiene un registro de Temporal
    ''' </summary>
    ''' <returns>String con la entidad ETemporal serializada.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function LeerTemporal(ByVal pETemporal As String) As String Implements ITemporalNTx.LeerTemporal
        Dim objDTemporalNTx As DTemporalNTx = Nothing
        Dim strResultado As String

        Try
            objDTemporalNTx = New DTemporalNTx
            strResultado = objDTemporalNTx.LeerTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalNTx.Dispose()
            objDTemporalNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Temporal
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoTemporal(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pCodigo As String, _
                                    ByVal pFecha As String, _
                                    ByVal pNumero As String, _
                                    ByVal pDecimales As String, _
                                    ByVal pComentario As String, _
                                    ByVal pTexto As String, _
                                    ByVal pFlag As String) As String Implements ITemporalNTx.ListadoTemporal
        Dim objDTemporalNTx As DTemporalNTx = Nothing
        Dim strResultado As String

        Try
            objDTemporalNTx = New DTemporalNTx
            strResultado = objDTemporalNTx.ListadoTemporal(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pCodigo, _
                                                           pFecha, _
                                                           pNumero, _
                                                           pDecimales, _
                                                           pComentario, _
                                                           pTexto, _
                                                           pFlag)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalNTx.Dispose()
        End Try

        Return strResultado
    End Function
    'IBK - RPH
    Public Function ListarSeguros(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pESeguros As String) As String Implements ITemporalNTx.ListarSeguros

        Dim objDTemporalNTx As DTemporalNTx = Nothing
        Dim strResultado As String

        Try
            objDTemporalNTx = New DTemporalNTx
            strResultado = objDTemporalNTx.ListarSeguros(pPageSize, _
                                                        pCurrentPage, _
                                                        pSortColumn, _
                                                        pSortOrder, _
                                                        pESeguros)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalNTx.Dispose()
            objDTemporalNTx = Nothing
        End Try

        Return strResultado
    End Function
    Public Function ListarSegurosDetalle(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pCodigoContrato As String) As String Implements ITemporalNTx.ListarSegurosDetalle

        Dim objDTemporalNTx As DTemporalNTx = Nothing
        Dim strResultado As String

        Try
            objDTemporalNTx = New DTemporalNTx
            strResultado = objDTemporalNTx.ListarSegurosDetalle(pPageSize, _
                                                                pCurrentPage, _
                                                                pSortColumn, _
                                                                pSortOrder, _
                                                                pCodigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDTemporalNTx.Dispose()
            objDTemporalNTx = Nothing
        End Try

        Return strResultado
    End Function
#End Region

End Class

#End Region
