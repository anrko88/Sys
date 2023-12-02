Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity


#Region "Clase Transaccional"


''' <summary>
''' Implementación de la clase DLSGConceptosTarifasTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("D9F93D8C-B0F2-4e7a-9AD9-866C7EE6E48A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DLSGConceptosTarifasTx")> _
Public Class DLSGConceptosTarifasTx
    Inherits ServicedComponent
    Implements ILSGConceptosTarifasTx


End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DLSGConceptosTarifasNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("25631218-E9DB-420b-8E6F-5F192979AFEE") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DLSGConceptosTarifasNTx")> _
Public Class DLSGConceptosTarifasNTx
    Inherits ServicedComponent
    Implements ILSGConceptosTarifasNTx


#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DLSGConceptosTarifasNTx"

#End Region

#Region "Métodos"

    Public Function RetornarTarifarioPredefContrato(ByVal CodProductoFinancieroActivo As String, _
                                                    ByVal CodMoneda As String) As String Implements ILSGConceptosTarifasNTx.RetornarTarifarioPredefContrato
        Dim odtbContrato As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter


        prmParameter(0) = New DAABRequest.Parameter("@CodProductoFinancieroActivo", DbType.String, 6, CodProductoFinancieroActivo, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CodMoneda", DbType.String, 3, CodMoneda, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_LSGConceptosTarifas_Contrato_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarTarifarioPredefinido", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' ConsultaConceptosTarifas
    ''' </summary>
    ''' <param name="pdblimporte"></param>
    ''' <param name="pstrCodtarifa"></param>
    ''' <param name="pstrCodmoneda"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultaConceptosTarifas(ByVal pdblimporte As Decimal, _
                                             ByVal pstrTipoConcepto As String, _
                                             ByVal pstrCodtarifa As String, _
                                             ByVal pstrCodmoneda As String) As String Implements ILSGConceptosTarifasNTx.ConsultaConceptosTarifas

        'Variables
        Dim odtbListado As DataTable
        Dim prmParameter(3) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            'prmParameter(0) = New DAABRequest.Parameter("@piv_importe", DbType.String, 8, pstrNumeroCotizacion, ParameterDirection.Input)
            prmParameter(0) = New DAABRequest.Parameter("@piv_importe", DbType.Decimal)
            prmParameter(0).Precision = 18
            prmParameter(0).Scale = 6
            prmParameter(0).Value = pdblimporte

            prmParameter(1) = New DAABRequest.Parameter("@piv_tipoConcepto", DbType.String, 2, pstrTipoConcepto, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_codtarifa", DbType.String, 2, pstrCodtarifa, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_codmoneda", DbType.String, 3, pstrCodmoneda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ConceptosTarifas_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ConsultaConceptosTarifas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

#End Region

End Class

#End Region
