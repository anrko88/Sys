Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LCesionContratoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("D93F30CA-9521-477b-A4CB-2E9DECD67C20") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCesionContratoTx")> _
Public Class LCesionContratoTx
    Inherits ServicedComponent
    Implements ICesionContratoTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LCesionContratoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Modificar el CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCesionContrato(ByVal pECesionContrato As String) As Boolean Implements ICesionContratoTx.ModificarCesionContrato
        Dim objDCesionContratoTx As DCesionContratoTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCesionContratoTx = New DCesionContratoTx
            blnResultado = objDCesionContratoTx.ModificarCesionContrato(pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionContratoTx.Dispose()
            objDCesionContratoTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Realizar el CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RealizarCesionContrato(ByVal pECesionContrato As String) As Boolean Implements ICesionContratoTx.RealizarCesionContrato
        Dim objDCesionContratoTx As DCesionContratoTx = Nothing
        Dim objDSubPrestatarioTx As DSubPrestatarioTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCesionContratoTx = New DCesionContratoTx
            objDSubPrestatarioTx = New DSubPrestatarioTx

            Dim objECesionario As EGCC_CesionContrato = CFunciones.DeserializeObject(Of EGCC_CesionContrato)(pECesionContrato)

            'Valida SubPrestatario
            Dim strCodSubPrestatario As String = ""
            Dim objESubPrestatario As New ESubprestatario
            objDSubPrestatarioTx = New DSubPrestatarioTx

            'Instancia Clase
            With objESubPrestatario
                .Codsubprestatario = objECesionario.CodSubprestatario
                .Codunico = objECesionario.CodUnico
                .Nombresubprestatario = objECesionario.RazonSocial
                .Coddocidentificaciontipo = objECesionario.TipoDocumento
                .Numdocidentificacion = objECesionario.NroDocumento
            End With
            strCodSubPrestatario = objDSubPrestatarioTx.InsertarSubPrestatario(CFunciones.SerializeObject(Of ESubprestatario)(objESubPrestatario))
            objECesionario.CodSubprestatario = strCodSubPrestatario

            Dim strNewCesionContrato As String = CFunciones.SerializeObject(Of EGCC_CesionContrato)(objECesionario)
            blnResultado = objDCesionContratoTx.RealizarCesionContrato(strNewCesionContrato)

        Catch ex As Exception
            Throw ex
        Finally
            objDCesionContratoTx.Dispose()
            objDCesionContratoTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

End Class

#End Region


#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LCesionContratoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 11/01/2013
''' </remarks>
<Guid("C101227C-630E-462d-B2CD-079014F28015") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCesionContratoNTx")> _
Public Class LCesionContratoNTx
    Inherits ServicedComponent
    Implements ICesionContratoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LCesionContratoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    Function ListadoCesionContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECesionContrato As String _
                                                ) As String Implements ICesionContratoNTx.ListadoCesionContrato
        Dim objDCesionContratoNTx As DCesionContratoNTx = Nothing
        Dim strResultado As String

        Try
            objDCesionContratoNTx = New DCesionContratoNTx
            strResultado = objDCesionContratoNTx.ListadoCesionContrato(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionContratoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/01/2013
    ''' </remarks>
    Function GetCesionContrato(ByVal pECesionContrato As String) As String Implements ICesionContratoNTx.GetCesionContrato
        Dim objDCesionContratoNTx As DCesionContratoNTx = Nothing
        Dim strResultado As String

        Try
            objDCesionContratoNTx = New DCesionContratoNTx
            strResultado = objDCesionContratoNTx.GetCesionContrato(pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDCesionContratoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todas las cesiones por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 17/01/2013 10:32:54 a.m. 
    ''' </remarks>
    Function ListadoCesionContratoReporte(ByVal pFechaInicio As String, _
                                          ByVal pFechaTermino As String) As String Implements ICesionContratoNTx.ListadoCesionContratoReporte
        Dim objDCesionContratoNTx As DCesionContratoNTx = Nothing
        Dim strResultado As String

        Try
            objDCesionContratoNTx = New DCesionContratoNTx
            strResultado = objDCesionContratoNTx.ListadoCesionContratoReporte(pFechaInicio, _
                                                                                pFechaTermino)
        Catch ex As Exception
            Throw ex
        Finally
            If objDCesionContratoNTx IsNot Nothing Then
                objDCesionContratoNTx.Dispose()
                objDCesionContratoNTx = Nothing
            End If
        End Try

        Return strResultado
    End Function
#End Region

End Class

#End Region

