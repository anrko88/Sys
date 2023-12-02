Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LTasadorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - IJM
''' Fecha de Creación  : 13/10/2012
''' </remarks>
<Guid("EC308C61-5D58-4c3a-BE98-6BA0579C5FA0") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LTasadorTx")> _
Public Class LTasadorTx
    Inherits ServicedComponent
    Implements ITasadorTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LTasadorTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pEGCC_ContratoTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function InsertarTasador(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.InsertarTasador
        Dim objDTasadorTx As DTasadorTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDTasadorTx = New DTasadorTx

            blnResultado = objDTasadorTx.InsertarTasador(pEGCC_ContratoTasador)

        Catch ex As Exception
            Throw ex
        Finally
            objDTasadorTx.Dispose()
            objDTasadorTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pEGCC_ContratoTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function ActualizaTasador(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.ActualizaTasador
        Dim objDTasadorTx As DTasadorTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDTasadorTx = New DTasadorTx

            blnResultado = objDTasadorTx.ActualizaTasador(pEGCC_ContratoTasador)

        Catch ex As Exception
            Throw ex
        Finally
            objDTasadorTx.Dispose()
            objDTasadorTx = Nothing
        End Try

        Return blnResultado
    End Function
    <AutoComplete(True)> _
    Public Function EnviarCarta(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.EnviarCarta
        Dim objDTasadorTx As DTasadorTx = Nothing
        Dim blnResultado As String

        Try
            objDTasadorTx = New DTasadorTx

            blnResultado = objDTasadorTx.EnviarCarta(pEGCC_ContratoTasador)

        Catch ex As Exception
            Throw ex
        Finally
            objDTasadorTx.Dispose()
            objDTasadorTx = Nothing
        End Try

        Return blnResultado
    End Function
    <AutoComplete(True)> _
    Public Function ActualizarTasacion(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.ActualizarTasacion
        Dim objDTasadorTx As DTasadorTx = Nothing
        Dim blnResultado As String

        Try
            objDTasadorTx = New DTasadorTx

            blnResultado = objDTasadorTx.ActualizarTasacion(pEGCC_ContratoTasador)

        Catch ex As Exception
            Throw ex
        Finally
            objDTasadorTx.Dispose()
            objDTasadorTx = Nothing
        End Try

        Return blnResultado
    End Function


#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<Guid("4FC9ED80-BA82-44d7-AD9B-F7DF7DB6533C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LTasadorNTx")> _
Public Class LTasadorNTx
    Inherits ServicedComponent
    Implements ITasadorNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LTasadorNTx"
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Obtiene Datos de Contrato para Tasacion
    ''' </summary>
    ''' <param name="pNumeroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Public Function ObtenerContratoTasacion(ByVal pNumeroContrato As String) As String Implements ITasadorNTx.ObtenerContratoTasacion
        Try
            Using objContratoNTx As New DTasadorNTx
                Return objContratoNTx.ObtenerContratoTasacion(pNumeroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ListadoContratoTasador
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCuCliente"></param>
    ''' <param name="pRazonsolcial"></param>
    ''' <param name="pTipoDocumento"></param>
    ''' <param name="pNumerodocumento"></param>
    ''' <param name="pEstadoTasacion"></param>
    ''' <param name="pClasificacionBien"></param>
    ''' <param name="pBanca"></param>
    ''' <param name="pEjecutivoBanca"></param>
    ''' <param name="pPeriodo"></param>
    ''' <param name="pFechadesde"></param>
    ''' <param name="pFechaHasta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function ListadoContratoTasador(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pCodSolicitudcredito As String, _
                                            ByVal pCuCliente As String, _
                                            ByVal pRazonsolcial As String, _
                                            ByVal pTipoDocumento As String, _
                                            ByVal pNumerodocumento As String, _
                                            ByVal pEstadoTasacion As String, _
                                            ByVal pClasificacionBien As String, _
                                            ByVal pBanca As String, _
                                            ByVal pEjecutivoBanca As String, _
                                            ByVal pPeriodo As String, _
                                            ByVal pFechadesde As String, _
                                            ByVal pFechaHasta As String, _
                                            ByVal pEstadoTasacionContrato As String) As String Implements ITasadorNTx.ListadoContratoTasador
        Dim objContratoNTx As DTasadorNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DTasadorNTx
            resultado = objContratoNTx.ListadoContratoTasador(pPageSize, _
                                                                pCurrentPage, _
                                                                pSortColumn, _
                                                                pSortOrder, _
                                                                pCodSolicitudcredito, _
                                                                pCuCliente, _
                                                                pRazonsolcial, _
                                                                pTipoDocumento, _
                                                                pNumerodocumento, _
                                                                pEstadoTasacion, _
                                                                pClasificacionBien, _
                                                                pBanca, _
                                                                pEjecutivoBanca, _
                                                                pPeriodo, _
                                                                pFechadesde, _
                                                                pFechaHasta, pEstadoTasacionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    Public Function ObtenerContratoCotizacionSaldoFinanciado(ByVal pNumeroContrato As String) As String Implements ITasadorNTx.ObtenerContratoCotizacionSaldoFinanciado
        Try
            Using oDTasadorNTx As New DTasadorNTx
                Return oDTasadorNTx.ObtenerContratoCotizacionSaldoFinanciado(pNumeroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista GCC_ContratoTasacion
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>    
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ListaContratoBienTasacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pCodSolicitudcredito As String) As String Implements ITasadorNTx.ListaContratoBienTasacion
        Dim objContratoNTx As DTasadorNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DTasadorNTx
            resultado = objContratoNTx.ListaContratoBienTasacion(pPageSize, _
                                                                 pCurrentPage, _
                                                                 pSortColumn, _
                                                                 pSortOrder, _
                                                                 pCodSolicitudcredito)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_TASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>    
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/01/2013 06:41:54 p.m. 
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ListaBienTasacion(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodSolicitudcredito As String) As String Implements ITasadorNTx.ListaBienTasacion
        Dim objContratoNTx As DTasadorNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DTasadorNTx
            resultado = objContratoNTx.ListaBienTasacion(pPageSize, _
                                                         pCurrentPage, _
                                                         pSortColumn, _
                                                         pSortOrder, _
                                                         pCodSolicitudcredito)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_HISTORICOTASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>
    ''' <param name="pCodContratoTasacion">Código de Contrato Tasación</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ListaHistoricoContratoBienTasacion(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodSolicitudcredito As String, _
                                          ByVal pCodContratoTasacion As Short) As String Implements ITasadorNTx.ListaHistoricoContratoBienTasacion
        Dim objContratoNTx As DTasadorNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DTasadorNTx
            resultado = objContratoNTx.ListaHistoricoContratoBienTasacion(pPageSize, _
                                                                            pCurrentPage, _
                                                                            pSortColumn, _
                                                                            pSortOrder, _
                                                                            pCodSolicitudcredito, _
                                                                            pCodContratoTasacion)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' calculatotales
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
Public Function calculatotales(ByVal pCodSolicitudcredito As String) As String Implements ITasadorNTx.calculatotales
        Dim objContratoNTx As DTasadorNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DTasadorNTx
            resultado = objContratoNTx.calculatotales(pCodSolicitudcredito)

        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
#End Region

End Class

#End Region

