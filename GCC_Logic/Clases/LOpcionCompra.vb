
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LOpcionCompraTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013
''' </remarks>
<Guid("5234C33E-331A-4640-82FD-47539A2B5CB3") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LOpcionCompraTx")> _
Public Class LOpcionCompraTx
    Inherits ServicedComponent
    Implements IOpcionCompraTx

#Region "   Constantes  "
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LOpcionCompraTx"
#End Region

#Region "   Metodos     "

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    <AutoComplete(True)> _
   Public Function fblnInsertarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompra
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnInsertarOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompraEnvio
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 10/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    <AutoComplete(True)> _
    Public Function fblnInsertarOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompraEnvio
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnInsertarOpcionCompraEnvio(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function


    ''' <summary>
    ''' Actualiza los registros de la tabla SolicitudCreditoEstructuraCarax
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    <AutoComplete(True)> _
    Public Function fblnAprobacionBien(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnAprobacionBien
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnAprobacionBien(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnModificarOpcionCompra
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnModificarOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function


    ''' <summary>
    ''' Modifica un registro existente de la tabla SolicitudCreditoEstructuraCarac
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 09/01/2013 06:41:54 p.m.
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnModificarBienOpcionCompra
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnModificarBienOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnEliminarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnEliminarOpcionCompra
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnEliminarOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function


    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumento
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnInsertarOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompraDocumento
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnInsertarOpcionCompraDocumento(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumentoObservacion
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnInsertarOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompraDocumentoObservacion
        Dim objOpcionCompraTx As DOpcionCompraTx = Nothing
        Dim blnResultado As Boolean

        Try
            objOpcionCompraTx = New DOpcionCompraTx
            blnResultado = objOpcionCompraTx.fblnInsertarOpcionCompraDocumentoObservacion(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompraTx.Dispose()
            objOpcionCompraTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region


End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LOpcionCompraNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013
''' </remarks>
<Guid("C2AEA102-40AE-42F8-B187-6B38D64FEF0F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LOpcionCompraNTx")> _
Public Class LOpcionCompraNTx
    Inherits ServicedComponent
    Implements IOpcionCompraNTx

#Region "   Constantes  "
    Private Const C_NOMBRE_CLASE As String = "LOpcionCompraNTx"
#End Region

#Region "   Metodos     "

    ''' <summary>
    ''' Obtiene los valores de un registro de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjLeerOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjLeerOpcionCompra
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjLeerOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompra.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene los valores de un registro de la vista UV_GCC_BIENOPCIOMCOMPRA_SEL
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjLeerBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjLeerBienOpcionCompra
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjLeerBienOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompra.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompra(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoOpcionCompra
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjListadoOpcionCompra(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompra.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompraTodo(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoOpcionCompraTodo
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjListadoOpcionCompraTodo(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompra.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoOpcionCompraEnvio
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjListadoOpcionCompraEnvio(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompra.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoBienOpcionCompra(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoBienOpcionCompra
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjListadoBienOpcionCompra(pPageSize, _
                                                                       pCurrentPage, _
                                                                       pSortColumn, _
                                                                       pSortOrder, _
                                                                       pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objOpcionCompra.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos los bienes por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 16/01/2013 03:11:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoBienOpcionCompraReporte(ByVal pFechaInicio As String, _
                                                        ByVal pFechaTermino As String) As String Implements IOpcionCompraNTx.fobjListadoBienOpcionCompraReporte
        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.fobjListadoBienOpcionCompraReporte(pFechaInicio, _
                                                                                pFechaTermino)
        Catch ex As Exception
            Throw ex
        Finally
            If objOpcionCompra IsNot Nothing Then
                objOpcionCompra.Dispose()
                objOpcionCompra = Nothing
            End If
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Opción de Compra Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Public Function ListadoReporteOpcionCompra(ByVal pdFechaActivacionIni As DateTime, _
                                               ByVal pdFechaActivacionFin As DateTime) As String Implements IOpcionCompraNTx.ListadoReporteOpcionCompra

        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty
        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.ListadoReporteOpcionCompra(pdFechaActivacionIni, _
                                                                      pdFechaActivacionFin)
        Catch ex As Exception
            Throw ex
        Finally
            If objOpcionCompra IsNot Nothing Then
                objOpcionCompra.Dispose()
                objOpcionCompra = Nothing
            End If
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function ListadoOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.ListadoOpcionCompraDocumento

        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty
        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.ListadoOpcionCompraDocumento(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            If objOpcionCompra IsNot Nothing Then
                objOpcionCompra.Dispose()
                objOpcionCompra = Nothing
            End If
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Observaciones de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function ListadoOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.ListadoOpcionCompraDocumentoObservacion

        Dim objOpcionCompra As DOpcionCompraNTx = Nothing
        Dim strResultado As String = String.Empty
        Try
            objOpcionCompra = New DOpcionCompraNTx
            strResultado = objOpcionCompra.ListadoOpcionCompraDocumentoObservacion(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            If objOpcionCompra IsNot Nothing Then
                objOpcionCompra.Dispose()
                objOpcionCompra = Nothing
            End If
        End Try
        Return strResultado
    End Function

#End Region

End Class

#End Region
