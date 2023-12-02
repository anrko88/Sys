
#Region "Clase Transaccional"

Public Class LCobroTx

    ''' <summary>
    ''' Inserta un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function fblnInsertarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean
        Dim objLCobroTx As Object = CreateObject("GCC.Logic.LCobroTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLCobroTx.fblnInsertarCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroTx = Nothing
        End Try
        Return boolResultado
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
    Public Function fblnModificarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean
        Dim objLCobroTx As Object = CreateObject("GCC.Logic.LCobroTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLCobroTx.fblnModificarCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Elimina un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function fblnEliminarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean
        Dim objLCobroTx As Object = CreateObject("GCC.Logic.LCobroTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLCobroTx.fblnEliminarCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroTx = Nothing
        End Try
        Return boolResultado
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
    Public Function fblnInsertarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean
        Dim objLCobroTx As Object = CreateObject("GCC.Logic.LCobroTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLCobroTx.fblnInsertarFraccionarCobro(pstrEListFraccionarCobro)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroTx = Nothing
        End Try
        Return boolResultado
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
    Public Function fblnModificarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean
        Dim objLCobroTx As Object = CreateObject("GCC.Logic.LCobroTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLCobroTx.fblnModificarFraccionarCobro(pstrEListFraccionarCobro)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroTx = Nothing
        End Try
        Return boolResultado
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
    Public Function fblnEliminarInsertarFraccionarCobro(ByVal pstrEListFraccionarCobro As String) As Boolean
        Dim objLCobroTx As Object = CreateObject("GCC.Logic.LCobroTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLCobroTx.fblnEliminarInsertarFraccionarCobro(pstrEListFraccionarCobro)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroTx = Nothing
        End Try
        Return boolResultado
    End Function
End Class


#End Region

#Region "Clase No Transaccional"

Public Class LCobroNTx

    ''' <summary>
    ''' Obtener dato del cobro   
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Código concepto</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function ObtenerCobro(ByVal pECreditoRecuperacionComision As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.ObtenerCobro(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
                                  ByVal pECreditoRecuperacionComision As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLCobroNTx.ListadoCobro(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
                                          ByVal pECreditoRecuperacionComision As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLCobroNTx.ListadoCobroPaginar(pSortColumn, _
                                                           pSortOrder, _
                                                           pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
    Public Function ListadoConcepto(ByVal pstrIDTabla As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.ListadoConcepto(pstrIDTabla)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
    Public Function ObtenerContratoCobro(ByVal pstrNumeroContrato As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.ObtenerContratoCobro(pstrNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
                                   ByVal pstrCodMoneda As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.CalculoComision(pstrCodigoConcepto, pdecImporte, pstrCodMoneda)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
    Public Function ValidarFeriado(ByVal pstrFecha As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.ValidarFeriado(pstrFecha)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
    Public Function InstanciaRegistro(ByVal pstrUsuario As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.InstanciaRegistro(pstrUsuario)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
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
    Public Function ListadoFraccionamiento(ByVal pstrEFraccionarCobro As String) As String
        Dim objLCobroNTx As Object = CreateObject("GCC.Logic.LCobroNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLCobroNTx.ListadoFraccionamiento(pstrEFraccionarCobro)
        Catch ex As Exception
            Throw ex
        Finally
            objLCobroNTx = Nothing
        End Try

        Return strResultado
    End Function
End Class

#End Region

