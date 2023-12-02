
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICobroTx
''' </summary>
''' <remarks>TSF-WCR | 27/11/2012</remarks>
<Guid("9103BD92-A9D2-4497-B06D-8589184714FB")> _
Public Interface ICobroTx

    ''' <summary>
    ''' Inserta un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Function fblnInsertarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean

    ''' <summary>
    ''' Actualiza un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Function fblnModificarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean

    ''' <summary>
    ''' Elimina un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Function fblnEliminarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEListFraccionarCobro">Lista generica de la entidad EGCC_Fraccionar serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Function fblnInsertarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean

    ''' <summary>
    ''' Actualiza un nuevo cobro en las tablas GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEListFraccionarCobro">Lista generica de la entidad EGCC_Fraccionar serializada</param>      
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Function fblnModificarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean

    ''' <summary>
    ''' Elimina un cobro en las tablas GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad EGCC_FraccionarCobro serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/12/2012
    ''' </remarks>
    Function fblnEliminarFraccionarCobro(ByVal pstrEFraccionarCobro As String) As Boolean

    ''' <summary>
    ''' Eliminar un conjunto de registros de la tabla GCC_FraccionarCobro y registra fraccionamiento de cobro en las tabla GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEListFraccionarCobro">Lista generica de la entidad EGCC_Fraccionar serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>    
    Function fblnEliminarInsertarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICobroNTx
''' </summary>
''' <remarks>TSF-WCR | 27/11/2012</remarks>
<Guid("1FF987F7-B59D-4ADC-9156-0396319F3F83")> _
Public Interface ICobroNTx

    ''' <summary>
    ''' Obtener dato del cobro   
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Código concepto</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Function ObtenerCobro(ByVal pECreditoRecuperacionComision As String) As String

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
    Function ListadoCobro(ByVal pPageSize As Integer, _
                          ByVal pCurrentPage As Integer, _
                          ByVal pSortColumn As String, _
                          ByVal pSortOrder As String, _
                          ByVal pECreditoRecuperacionComision As String) As String

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
    Function ListadoCobroPaginar(ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pECreditoRecuperacionComision As String) As String

    ''' <summary>
    ''' Devuelve el conjunto de conceptos    
    ''' </summary>
    ''' <param name="pstrIDTabla">ID Tabla valor generica</param>
    ''' <returns>Listado de Concepto(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Function ListadoConcepto(ByVal pstrIDTabla As String) As String

    ''' <summary>
    ''' Devuelve los datos de un contrato    
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Número de contrato</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 28/11/2012
    ''' </remarks>
    Function ObtenerContratoCobro(ByVal pstrNumeroContrato As String) As String

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
    Function CalculoComision(ByVal pstrCodigoConcepto As String, _
                             ByVal pdecImporte As Decimal, _
                             ByVal pstrCodMoneda As String) As String

    ''' <summary>
    ''' Realiza validación de la fecha ingresado no sea feriado 
    ''' </summary>
    ''' <param name="pstrFecha">Fecha</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Function ValidarFeriado(ByVal pstrFecha As String) As String

    ''' <summary>
    ''' Devuelve instancia para el registro
    ''' </summary>
    ''' <param name="pstrUsuario">Registro de usuario</param>
    ''' <returns>String</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/12/2012
    ''' </remarks>
    Function InstanciaRegistro(ByVal pstrUsuario As String) As String

    ''' <summary>
    ''' Devuelve el conjunto de datos de fraccionamiento    
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad serializada del objeto GCC_FraccionarCobro</param>
    ''' <returns>Listado de Fraccionamiento(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Function ListadoFraccionamiento(ByVal pstrEFraccionarCobro As String) As String
End Interface


#End Region

