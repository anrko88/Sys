Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LCotizacionTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("06076709-A7B9-4950-9307-D126EE3C8A9B") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LSiniestroTx")> _
Public Class LSiniestroTx
    Inherits ServicedComponent
    Implements ISiniestroTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LSiniestroTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarSiniestro(ByVal pESiniestro As String) As Boolean Implements ISiniestroTx.InsertarSiniestro
        Dim objDSiniestroTx As DSiniestroTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSiniestroTx = New DSiniestroTx
            objDSiniestroTx.InsertarSiniestro(pESiniestro)
            blnResultado = True
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDSiniestroTx.Dispose()
            objDSiniestroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarSiniestro(ByVal pESiniestro As String) As Boolean Implements ISiniestroTx.ModificarSiniestro
        Dim objDSiniestroTx As DSiniestroTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSiniestroTx = New DSiniestroTx
            blnResultado = objDSiniestroTx.ModificarSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroTx.Dispose()
            objDSiniestroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarSiniestro(ByVal pESiniestro As String) As Boolean Implements ISiniestroTx.EliminarSiniestro
        Dim objDSiniestroTx As DSiniestroTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSiniestroTx = New DSiniestroTx
            blnResultado = objDSiniestroTx.EliminarSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroTx.Dispose()
            objDSiniestroTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

End Class

#End Region


#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LSiniestroNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("D7A7EA71-2EE9-4097-930D-028724A98F66") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LSiniestroNTx")> _
Public Class LSiniestroNTx
    Inherits ServicedComponent
    Implements ISiniestroNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LSiniestroNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Function ListadoSiniestroContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pESiniestro As String _
                                                ) As String Implements ISiniestroNTx.ListadoSiniestroContrato
        Dim objDSiniestroNTx As DSiniestroNTx = Nothing
        Dim strResultado As String

        Try
            objDSiniestroNTx = New DSiniestroNTx
            strResultado = objDSiniestroNTx.ListadoSiniestroContrato(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetSiniestroContrato(ByVal pESiniestro As String) As String Implements ISiniestroNTx.GetSiniestroContrato
        Dim objDSiniestroNTx As DSiniestroNTx = Nothing
        Dim strResultado As String

        Try
            objDSiniestroNTx = New DSiniestroNTx
            strResultado = objDSiniestroNTx.GetSiniestroContrato(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Function ListadoSiniestro(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pESiniestro As String _
                                                ) As String Implements ISiniestroNTx.ListadoSiniestro
        Dim objDSiniestroNTx As DSiniestroNTx = Nothing
        Dim strResultado As String

        Try
            objDSiniestroNTx = New DSiniestroNTx
            strResultado = objDSiniestroNTx.ListadoSiniestro(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetSiniestro(ByVal pESiniestro As String) As String Implements ISiniestroNTx.GetSiniestro
        Dim objDSiniestroNTx As DSiniestroNTx = Nothing
        Dim strResultado As String

        Try
            objDSiniestroNTx = New DSiniestroNTx
            strResultado = objDSiniestroNTx.GetSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Siniestro para exportar
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 21/01/2013
    ''' </remarks>
    Public Function ListadoReporteSiniestro(ByVal pESiniestro As String) As String Implements ISiniestroNTx.ListadoReporteSiniestro
        Dim objDSiniestroNTx As DSiniestroNTx = Nothing
        Dim strResultado As String

        Try
            objDSiniestroNTx = New DSiniestroNTx
            strResultado = objDSiniestroNTx.ListadoReporteSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 18/01/2013
    ''' </remarks>
    Function GetSiniestroConsulta(ByVal pESiniestro As String) As String Implements ISiniestroNTx.GetSiniestroConsulta
        Dim objDSiniestroNTx As DSiniestroNTx = Nothing
        Dim strResultado As String

        Try
            objDSiniestroNTx = New DSiniestroNTx
            strResultado = objDSiniestroNTx.GetSiniestroConsulta(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objDSiniestroNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

End Class

#End Region

