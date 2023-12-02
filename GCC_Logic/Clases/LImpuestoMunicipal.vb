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
<Guid("4B409FB1-61D9-4ddf-B2D2-DB610D750021") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LImpuestoMunicipalTx")> _
Public Class LImpuestoMunicipalTx
    Inherits ServicedComponent
    Implements IImpuestoMunicipalTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LImpuestoMunicipalTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    ''' 'Inicio IBK - AAE - Retorno un string
    <AutoComplete(True)> _
    Public Function InsertarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalTx.InsertarImpuestoMunicipal
        Dim objDImpuestoMunicipalTx As DImpuestoMunicipalTx = Nothing
        'Inicio IBK - AAE - Retorno un string
        'Dim blnResultado As Boolean
        Dim blnResultado As String
        Try
            objDImpuestoMunicipalTx = New DImpuestoMunicipalTx
            blnResultado = objDImpuestoMunicipalTx.InsertarImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalTx.Dispose()
            objDImpuestoMunicipalTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar el ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    ''' ''' 'Inicio IBK - AAE - Retorno un string
    <AutoComplete(True)> _
    Public Function ModificarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalTx.ModificarImpuestoMunicipal
        Dim objDImpuestoMunicipalTx As DImpuestoMunicipalTx = Nothing
        'Inicio IBK - AAE - Retorno un string
        'Dim blnResultado As Boolean
        Dim blnResultado As String
        Try
            objDImpuestoMunicipalTx = New DImpuestoMunicipalTx
            blnResultado = objDImpuestoMunicipalTx.ModificarImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalTx.Dispose()
            objDImpuestoMunicipalTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean Implements IImpuestoMunicipalTx.EliminarImpuestoMunicipal
        Dim objDImpuestoMunicipalTx As DImpuestoMunicipalTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDImpuestoMunicipalTx = New DImpuestoMunicipalTx
            blnResultado = objDImpuestoMunicipalTx.EliminarImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalTx.Dispose()
            objDImpuestoMunicipalTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Asignar Lote en ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function AsignarLoteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalTx.AsignarLoteImpuestoMunicipal
        Dim objDImpuestoMunicipalTx As DImpuestoMunicipalTx = Nothing
        Dim strResultado As String
        Try
            objDImpuestoMunicipalTx = New DImpuestoMunicipalTx
            strResultado = objDImpuestoMunicipalTx.AsignarLoteImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalTx.Dispose()
            objDImpuestoMunicipalTx = Nothing
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Asignar Cheque en ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 29/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function AsignarChequeImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean Implements IImpuestoMunicipalTx.AsignarChequeImpuestoMunicipal
        Dim objDImpuestoMunicipalTx As DImpuestoMunicipalTx = Nothing
        Dim blnResultado As String
        Try
            objDImpuestoMunicipalTx = New DImpuestoMunicipalTx
            blnResultado = objDImpuestoMunicipalTx.AsignarChequeImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalTx.Dispose()
            objDImpuestoMunicipalTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Lote ImpuestoMunicipal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : JJM IBK 
    ''' Fecha de Creacion : 13/02/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarLoteImpuestoMunicipal(ByVal pNroLote As String) As Boolean Implements IImpuestoMunicipalTx.EliminarLoteImpuestoMunicipal
        Dim objDImpuestoMunicipalTx As DImpuestoMunicipalTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDImpuestoMunicipalTx = New DImpuestoMunicipalTx
            blnResultado = objDImpuestoMunicipalTx.EliminarLoteImpuestoMunicipal(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalTx.Dispose()
            objDImpuestoMunicipalTx = Nothing
        End Try
        Return blnResultado
    End Function
    

    
#End Region

End Class

#End Region


#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LImpuestoMunicipalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("A6E41D06-E90E-4B86-A07E-3708BCD3276C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LImpuestoMunicipalNTx")> _
Public Class LImpuestoMunicipalNTx
    Inherits ServicedComponent
    Implements IImpuestoMunicipalNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LImpuestoMunicipalNTx"
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
    Function ListadoImpuestoMunicipal(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEImpuestoMunicipal As String _
                                        ) As String Implements IImpuestoMunicipalNTx.ListadoImpuestoMunicipal
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.ListadoImpuestoMunicipal(pPageSize, _
                                                                               pCurrentPage, _
                                                                               pSortColumn, _
                                                                               pSortOrder, _
                                                                               pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoMunicipal
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.GetImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
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
    Function ListadoImpuestoMunicipalBienes(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEImpuestoMunicipal As String _
                                                ) As String Implements IImpuestoMunicipalNTx.ListadoImpuestoMunicipalBienes
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.ListadoImpuestoMunicipalBienes(pPageSize, _
                                                                                   pCurrentPage, _
                                                                                   pSortColumn, _
                                                                                   pSortOrder, _
                                                                                   pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el ImpuestoMunicipal Bienes
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetImpuestoMunicipalBienes(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoMunicipalBienes
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.GetImpuestoMunicipalBienes(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListarImpuestoMunicipalLiquidar(ByVal pPageSize As Integer, _
                               ByVal pCurrentPage As Integer, _
                               ByVal pSortColumn As String, _
                               ByVal pSortOrder As String, _
                               ByVal pCodLote As String) As String Implements IImpuestoMunicipalNTx.ListarImpuestoMunicipalLiquidar
        Dim objDImpuestoVehicularNTX As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoMunicipalLiquidar(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    'Inicio IBK
    Function GetImpuestoMultasInmueble(ByVal pNroLote As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoMultasInmueble
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.GetImpuestoMultasInmueble(pNroLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function
    Function GetCodigoPredioBien(ByVal pCodSolicitud As String, ByVal pSecFinanciamiento As String) As String Implements IImpuestoMunicipalNTx.GetCodigoPredioBien
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.GetCodigoPredioBien(pCodSolicitud, pSecFinanciamiento)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Function MyFunc() As Integer

        Return 0
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - JJM
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Function ListadoImpuestoMunicipalxLote(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEImpuestoMunicipal As String _
                                        ) As String Implements IImpuestoMunicipalNTx.ListadoImpuestoMunicipalxLote
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.ListadoImpuestoMunicipalxLote(pPageSize, _
                                                                               pCurrentPage, _
                                                                               pSortColumn, _
                                                                               pSortOrder, _
                                                                               pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Function GetImpuestoTotalesInmueble(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoTotalesInmueble
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.GetImpuestoTotalesInmueble(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function
    'Fin IBK

    Public Function ListarImpuestoMunicipalLiquidarTodo(ByVal pCodLote As String) As String Implements IImpuestoMunicipalNTx.ListarImpuestoMunicipalLiquidarTodo
        Dim objDImpuestoVehicularNTX As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoMunicipalLiquidarTodo(pCodLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function

    Public Function ListarImpuestoMunicipalReporteLiquidar(ByVal pCodigoImpuesto As String) As String Implements IImpuestoMunicipalNTx.ListarImpuestoMunicipalReporteLiquidar
        Dim objDImpuestoVehicularNTX As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoMunicipalReporteLiquidar(pCodigoImpuesto)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function


    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - SCA
    ''' Fecha de Creación  : 28/01/2013
    ''' </remarks>
    Public Function ListadoReporteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.ListadoReporteImpuestoMunicipal
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String

        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.ListadoReporteImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
        End Try

        Return strResultado
    End Function
    ''' <summary>
    ''' Descuento ImpuestoMunicipal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : JJM IBK 
    ''' Fecha de Creacion : 19/02/2013
    ''' </remarks>
    Public Function DescuentoLoteImpuestoMunicipal(ByVal pNroLote As String, ByVal pUsuarioModificacion As String, ByVal pDescuento As Decimal) As String Implements IImpuestoMunicipalNTx.DescuentoLoteImpuestoMunicipal
        Dim objDImpuestoMunicipalNTx As DImpuestoMunicipalNTx = Nothing
        Dim strResultado As String
        Try
            objDImpuestoMunicipalNTx = New DImpuestoMunicipalNTx
            strResultado = objDImpuestoMunicipalNTx.DescuentoLoteImpuestoMunicipal(pNroLote, pUsuarioModificacion, pDescuento)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoMunicipalNTx.Dispose()
            objDImpuestoMunicipalNTx = Nothing
        End Try
        Return strResultado
    End Function
#End Region

End Class

#End Region

