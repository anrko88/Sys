Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports Microsoft.VisualBasic

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity



#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoRepresentanteTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("35F61BCB-A657-4fd3-BD51-55E1DB5FD4AE") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoRepresentanteTx")> _
Public Class DContratoRepresentanteTx
    Inherits ServicedComponent
    Implements IContratoRepresentanteTx


#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoRepresentanteTx"

#End Region

#Region "Métodos"


    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : IJM - 
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteContratoIns(ByVal pEGcc_contratoRepresentante As String) As String Implements IContratoRepresentanteTx.ContratoRepresentanteIns

        'Deserealiza la Entidad
        Dim odtbContratoRepresentante As DataTable = Nothing
        Dim oEGccContratoRepresentante As New EGcc_contratorepresentante

        oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratoRepresentante)

        'Deserealiza la Entidad
        Dim prmParameter(5) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int32, 4, oEGccContratoRepresentante.Codigorepresentante, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEGccContratoRepresentante.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoTipoRepresentante", DbType.String, 100, oEGccContratoRepresentante.Codigotiporepresentante, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int32, 4, oEGccContratoRepresentante.Audestadologico, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccContratoRepresentante.Audfecharegistro, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratoRepresentante.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepresentanteContrato_ins"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoRepresentanteIns", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return 1
    End Function

    Public Function RepresentanteContratoItemDel(ByVal pEGcc_contratoRepresentante As String) As String Implements IContratoRepresentanteTx.RepresentanteContratoItemDel

        'Deserealiza la Entidad
        Dim odtbContratoRepresentante As DataTable = Nothing
        Dim oEGccContratoRepresentante As New EGcc_contratorepresentante
        oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratoRepresentante)

        'Deserealiza la Entidad
        Dim prmParameter(2) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int32, 4, oEGccContratoRepresentante.Codigorepresentante, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEGccContratoRepresentante.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoTipoRepresentante", DbType.String, 100, oEGccContratoRepresentante.Codigotiporepresentante, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepresentanteContrato_Item_del"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            objRequest.Factory.ExecuteNonQuery(objRequest)
            'ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentanteContratoItemDel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return 1
    End Function

    ''' <summary>
    ''' Elimina Todos los Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RepresentanteContratoDel(ByVal pEGcc_contratoRepresentante As String) As String Implements IContratoRepresentanteTx.RepresentanteContratoDel

        'Deserealiza la Entidad
        Dim odtbContratoRepresentante As DataTable = Nothing
        Dim oEGccContratoRepresentante As New EGcc_contratorepresentante
        oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratoRepresentante)

        'Deserealiza la Entidad
        Dim prmParameter(1) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEGccContratoRepresentante.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoTipoRepresentante", DbType.String, 3, oEGccContratoRepresentante.Codigotiporepresentante, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepresentanteContrato_del"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            objRequest.Factory.ExecuteNonQuery(objRequest)
            'ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentanteContratoDel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return 1
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DContratoRepresentanteNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("ECB24440-810A-415a-9669-79AF02D4C54F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoRepresentanteNTx")> _
Public Class DContratoRepresentanteNTx
    Inherits ServicedComponent
    Implements IContratoRepresentanteNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DContratoRepresentanteNTx"
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoRepresentanteSel(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pEGcc_contratoRepresentante As String, _
                                             ByVal pFirma As String) As String Implements IContratoRepresentanteNTx.ContratoRepresentanteSel

        'Deserealiza la Entidad
        Dim odtbContratoRepresentante As DataTable = Nothing
        Dim oEGccContratoRepresentante As New EGcc_contratorepresentante
        oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratoRepresentante)

        'Deserealiza la Entidad
        Dim prmParameter(6) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 4, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 4, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@numeroContrato", DbType.String, 8, oEGccContratoRepresentante.Numerocontrato, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@codigotiporepresentante", DbType.String, 3, oEGccContratoRepresentante.Codigotiporepresentante, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@sefirmaen", DbType.String, 3, pFirma, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoRepresentante_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoRepresentante = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocumentoSel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoRepresentante)
    End Function


#End Region

End Class

#End Region
