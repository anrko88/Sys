
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LCobroTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 27/11/2012
''' </remarks>
<Guid("09A01A76-EB75-4328-BD8B-84AF68E8B3B2") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCobroTx")> _
Public Class LCobroTx
    Inherits ServicedComponent
    Implements ICobroTx

#Region "   Constantes  "
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LCobroTx"
#End Region

#Region "   Metodos     "

    ''' <summary>
    ''' Inserta un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnInsertarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean Implements ICobroTx.fblnInsertarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            objCobroTx = New DCobroTx
            blnResultado = objCobroTx.fblnInsertarCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean Implements ICobroTx.fblnModificarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            objCobroTx = New DCobroTx
            blnResultado = objCobroTx.fblnModificarCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnEliminarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean Implements ICobroTx.fblnEliminarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            objCobroTx = New DCobroTx
            blnResultado = objCobroTx.fblnEliminarCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEListFraccionarCobro">Lista generica de la entidad EGCC_Fraccionar serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnInsertarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean Implements ICobroTx.fblnInsertarFraccionarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            Dim objListEFraccionarCobro As ListEGCC_FraccionarCobro = CFunciones.DeserializeObject(Of ListEGCC_FraccionarCobro)(pstrEListFraccionarCobro)
            objCobroTx = New DCobroTx
            For Each objEFraccionarCobro In objListEFraccionarCobro
                Dim strEFraccionarCobro As String = String.Empty

                strEFraccionarCobro = CFunciones.SerializeObject(objEFraccionarCobro)
                blnResultado = objCobroTx.fblnInsertarFraccionarCobro(strEFraccionarCobro)

            Next objEFraccionarCobro

        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza un nuevo cobro en las tablas GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEListFraccionarCobro">Lista generica de la entidad EGCC_Fraccionar serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean Implements ICobroTx.fblnModificarFraccionarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            Dim objListEFraccionarCobro As ListEGCC_FraccionarCobro = CFunciones.DeserializeObject(Of ListEGCC_FraccionarCobro)(pstrEListFraccionarCobro)
            objCobroTx = New DCobroTx
            For Each objEFraccionarCobro In objListEFraccionarCobro
                Dim strEFraccionarCobro As String = String.Empty
                strEFraccionarCobro = CFunciones.SerializeObject(objEFraccionarCobro)
                blnResultado = objCobroTx.fblnModificarFraccionarCobro(strEFraccionarCobro)

            Next objEFraccionarCobro

        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina un cobro en las tablas GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad EGCC_FraccionarCobro serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnEliminarFraccionarCobro(ByVal pstrEFraccionarCobro As String) As Boolean Implements ICobroTx.fblnEliminarFraccionarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            objCobroTx = New DCobroTx
            blnResultado = objCobroTx.fblnEliminarFraccionarCobro(pstrEFraccionarCobro)
        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar un conjunto de registros de la tabla GCC_FraccionarCobro y registra fraccionamiento de cobro en las tabla GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEListFraccionarCobro">Lista generica de la entidad EGCC_Fraccionar serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnEliminarInsertarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean Implements ICobroTx.fblnEliminarInsertarFraccionarCobro
        Dim objCobroTx As DCobroTx = Nothing
        Dim blnResultado As Boolean

        Try
            Dim objListEFraccionarCobro As ListEGCC_FraccionarCobro = CFunciones.DeserializeObject(Of ListEGCC_FraccionarCobro)(pstrEListFraccionarCobro)
            objCobroTx = New DCobroTx

            Dim intEli As Short = 0
            Dim boolEli As Boolean = False
            For Each objEFraccionarCobro In objListEFraccionarCobro
                intEli += 1
                Dim strEFraccionarCobro As String = String.Empty
                strEFraccionarCobro = CFunciones.SerializeObject(objEFraccionarCobro)
                If intEli = 1 Then boolEli = objCobroTx.fblnEliminarFraccionarCobro(strEFraccionarCobro)
                If boolEli Then
                    blnResultado = objCobroTx.fblnModificarFraccionarCobro(strEFraccionarCobro)
                End If
            Next objEFraccionarCobro

        Catch ex As Exception
            Throw ex
        Finally
            objCobroTx.Dispose()
            objCobroTx = Nothing
        End Try
        Return blnResultado
    End Function
#End Region


End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LCobroNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 27/11/2012
''' </remarks>
<Guid("5B143052-ED7F-4B9C-ABBE-F728A2AA5D81") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCobroNTx")> _
Public Class LCobroNTx
    Inherits ServicedComponent
    Implements ICobroNTx

#Region "   Constantes  "
    Private Const C_NOMBRE_CLASE As String = "LCobroNTx"
#End Region

#Region "   Metodos     "

    ''' <summary>
    ''' Obtener dato del cobro   
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Código concepto</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function ObtenerCobro(ByVal pECreditoRecuperacionComision As String) As String Implements ICobroNTx.ObtenerCobro
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.ObtenerCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de cobros asociados a contratos hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pECreditoRecuperacionComision">Entidad serializada del objeto CreditoRecuperacionComision</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Public Function ListadoCobro(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pECreditoRecuperacionComision As String) As String Implements ICobroNTx.ListadoCobro
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.ListadoCobro(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de cobros asociados a contratos anterior, actual y siguiente 
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pECreditoRecuperacionComision">Entidad serializada del objeto CreditoRecuperacionComision</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 03/12/2012
    ''' </remarks>
    Public Function ListadoCobroPaginar(ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pECreditoRecuperacionComision As String) As String Implements ICobroNTx.ListadoCobroPaginar
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.ListadoCobroPaginar(pSortColumn, _
                                                           pSortOrder, _
                                                           pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de conceptos    
    ''' </summary>
    ''' <param name="pstrIDTabla">ID Tabla valor generica</param>
    ''' <returns>Listado de Concepto(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Public Function ListadoConcepto(ByVal pstrIDTabla As String) As String Implements ICobroNTx.ListadoConcepto
        Dim objDConceptoNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDConceptoNTx = New DCobroNTx
            strResultado = objDConceptoNTx.ListadoConcepto(pstrIDTabla)
        Catch ex As Exception
            Throw ex
        Finally
            objDConceptoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Devuelve los datos de un contrato    
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Número de contrato</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 28/11/2012
    ''' </remarks>
    Public Function ObtenerContratoCobro(ByVal pstrNumeroContrato As String) As String Implements ICobroNTx.ObtenerContratoCobro
        Dim objDConceptoNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDConceptoNTx = New DCobroNTx
            strResultado = objDConceptoNTx.ObtenerContratoCobro(pstrNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDConceptoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Realiza el calculo de la comisión del cobro   
    ''' </summary>
    ''' <param name="pstrCodigoConcepto">Código concepto</param>
    ''' <param name="pdecImporte">Importe</param>
    ''' <param name="pstrCodMoneda">Código moneda</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Public Function CalculoComision(ByVal pstrCodigoConcepto As String, _
                                   ByVal pdecImporte As Decimal, _
                                   ByVal pstrCodMoneda As String) As String Implements ICobroNTx.CalculoComision
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.CalculoComision(pstrCodigoConcepto, pdecImporte, pstrCodMoneda)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Realiza validación de la fecha ingresado no sea feriado 
    ''' </summary>
    ''' <param name="pstrFecha">Fecha</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Public Function ValidarFeriado(ByVal pstrFecha As String) As String Implements ICobroNTx.ValidarFeriado
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.ValidarFeriado(pstrFecha)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Devuelve instancia para el registro
    ''' </summary>
    ''' <param name="pstrUsuario">Registro de usuario</param>
    ''' <returns>String</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/12/2012
    ''' </remarks>
    Public Function InstanciaRegistro(ByVal pstrUsuario As String) As String Implements ICobroNTx.InstanciaRegistro
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.InstanciaRegistro(pstrUsuario)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function


    ''' <summary>
    ''' Devuelve el conjunto de datos de fraccionamiento    
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad serializada del objeto GCC_FraccionarCobro</param>
    ''' <returns>Listado de Fraccionamiento(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Public Function ListadoFraccionamiento(ByVal pstrEFraccionarCobro As String) As String Implements ICobroNTx.ListadoFraccionamiento
        Dim objDCobroNTx As DCobroNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objDCobroNTx = New DCobroNTx
            strResultado = objDCobroNTx.ListadoFraccionamiento(pstrEFraccionarCobro)
        Catch ex As Exception
            Throw ex
        Finally
            objDCobroNTx.Dispose()
        End Try

        Return strResultado
    End Function
#End Region



End Class

#End Region
