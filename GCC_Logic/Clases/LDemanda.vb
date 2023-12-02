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
<Guid("0E528EF6-1F41-4931-A1B4-A5F27E4D02D7") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDemandaTx")> _
Public Class LDemandaTx
    Inherits ServicedComponent
    Implements IDemandaTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LDemandaTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarDemanda(ByVal pEDemanda As String, ByVal pESiniestro As String) As String Implements IDemandaTx.InsertarDemanda
        Dim objDDemandaTx As DDemandaTx = Nothing
        Dim objDSiniestroTx As DSiniestroTx = Nothing
        Dim blnResultado As Boolean
        Dim intCodSiniestro As Integer
        Dim strCodDemanda As String
        Try

            Dim objEDemanda As EGcc_Demanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

            'Valida TipoSiniestro
            Dim strTipoSiniestro As String = objEDemanda.TipoSiniestro
            If strTipoSiniestro.Trim.Equals("002") Then
                'Ingrese Siniestro
                objDSiniestroTx = New DSiniestroTx
                intCodSiniestro = objDSiniestroTx.InsertarSiniestro(pESiniestro)
                objEDemanda.CodSiniestro = intCodSiniestro
            End If

            'Ingresa Demanda
            Dim strEDemanda As String = CFunciones.SerializeObject(Of EGcc_Demanda)(objEDemanda)
            objDDemandaTx = New DDemandaTx
            strCodDemanda = objDDemandaTx.InsertarDemanda(strEDemanda)

        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaTx.Dispose()
            objDDemandaTx = Nothing
        End Try
        Return strCodDemanda
    End Function

    ''' <summary>
    ''' Modificar el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarDemanda(ByVal pEDemanda As String, ByVal pESiniestro As String) As Boolean Implements IDemandaTx.ModificarDemanda
        Dim objDDemandaTx As DDemandaTx = Nothing
        Dim objDSiniestroTx As DSiniestroTx = Nothing
        Dim blnResultado As Boolean
        Try
            Dim objEDemanda As EGcc_Demanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

            'Valida TipoSiniestro
            Dim strTipoSiniestro As String = objEDemanda.TipoSiniestro
            If strTipoSiniestro.Trim.Equals("002") Then
                'Actualiza Siniestro
                objDSiniestroTx = New DSiniestroTx
                blnResultado = objDSiniestroTx.ModificarSiniestro(pESiniestro)
            End If

            'Ingresa Demanda
            objDDemandaTx = New DDemandaTx
            blnResultado = objDDemandaTx.ModificarDemanda(pEDemanda)

        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaTx.Dispose()
            objDDemandaTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarDemanda(ByVal pEDemanda As String) As Boolean Implements IDemandaTx.EliminarDemanda
        Dim objDDemandaTx As DDemandaTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDDemandaTx = New DDemandaTx
            blnResultado = objDDemandaTx.EliminarDemanda(pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaTx.Dispose()
            objDDemandaTx = Nothing
        End Try
        Return blnResultado
    End Function

#Region "Implicados"

    ''' <summary>
    ''' Inserta el Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarDemandaImplicado(ByVal pEImplicado As String) As Boolean Implements IDemandaTx.InsertarDemandaImplicado
        Dim objDDemandaTx As DDemandaTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDDemandaTx = New DDemandaTx
            blnResultado = objDDemandaTx.InsertarDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaTx.Dispose()
            objDDemandaTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar el Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarDemandaImplicado(ByVal pEImplicado As String) As Boolean Implements IDemandaTx.ModificarDemandaImplicado
        Dim objDDemandaTx As DDemandaTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDDemandaTx = New DDemandaTx
            blnResultado = objDDemandaTx.ModificarDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaTx.Dispose()
            objDDemandaTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarDemandaImplicado(ByVal pEImplicado As String) As Boolean Implements IDemandaTx.EliminarDemandaImplicado
        Dim objDDemandaTx As DDemandaTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDDemandaTx = New DDemandaTx
            blnResultado = objDDemandaTx.EliminarDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaTx.Dispose()
            objDDemandaTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#End Region

End Class

#End Region


#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LDemandaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("DCA2A532-DA8B-4124-A449-E0426ABB8137") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDemandaNTx")> _
Public Class LDemandaNTx
    Inherits ServicedComponent
    Implements IDemandaNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LDemandaNTx"
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
    Function ListadoDemandaContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEDemanda As String _
                                                ) As String Implements IDemandaNTx.ListadoDemandaContrato
        Dim objDDemandaNTx As DDemandaNTx = Nothing
        Dim strResultado As String

        Try
            objDDemandaNTx = New DDemandaNTx
            strResultado = objDDemandaNTx.ListadoDemandaContrato(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetDemandaContrato(ByVal pEDemanda As String) As String Implements IDemandaNTx.GetDemandaContrato
        Dim objDDemandaNTx As DDemandaNTx = Nothing
        Dim strResultado As String

        Try
            objDDemandaNTx = New DDemandaNTx
            strResultado = objDDemandaNTx.GetDemandaContrato(pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaNTx.Dispose()
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
    Function ListadoDemanda(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEDemanda As String _
                                                ) As String Implements IDemandaNTx.ListadoDemanda
        Dim objDDemandaNTx As DDemandaNTx = Nothing
        Dim strResultado As String

        Try
            objDDemandaNTx = New DDemandaNTx
            strResultado = objDDemandaNTx.ListadoDemanda(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetDemanda(ByVal pEDemanda As String) As String Implements IDemandaNTx.GetDemanda
        Dim objDDemandaNTx As DDemandaNTx = Nothing
        Dim strResultado As String

        Try
            objDDemandaNTx = New DDemandaNTx
            strResultado = objDDemandaNTx.GetDemanda(pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaNTx.Dispose()
        End Try

        Return strResultado
    End Function

#Region "Implicados"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Function ListadoDemandaImplicado(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEImplicado As String _
                                                ) As String Implements IDemandaNTx.ListadoDemandaImplicado
        Dim objDDemandaNTx As DDemandaNTx = Nothing
        Dim strResultado As String

        Try
            objDDemandaNTx = New DDemandaNTx
            strResultado = objDDemandaNTx.ListadoDemandaImplicado(pPageSize, _
                                                                   pCurrentPage, _
                                                                   pSortColumn, _
                                                                   pSortOrder, _
                                                                   pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Demanda
    ''' </summary>
    ''' <param name="pEImplicado">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Function GetDemandaImplicado(ByVal pEImplicado As String) As String Implements IDemandaNTx.GetDemandaImplicado
        Dim objDDemandaNTx As DDemandaNTx = Nothing
        Dim strResultado As String

        Try
            objDDemandaNTx = New DDemandaNTx
            strResultado = objDDemandaNTx.GetDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objDDemandaNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#End Region

End Class

#End Region

