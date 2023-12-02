Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DBloqueoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("5922C39C-D3DB-4210-9C3B-89D3DBE636BD") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DAlertasTx")> _
Public Class DAlertasTx
    Inherits ServicedComponent
    Implements IAlertasTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DAlertasTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta bloqueo del sistema
    ''' </summary>
    ''' <param name="pAlertas">Entidad Alertas</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - SCA
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function fInsertarAlertas(ByVal pEAlertas As String) As Boolean Implements IAlertasTx.fInsertarAlertas

        Dim oEAlertas As New EGCC_Alertas
        Dim prmParameter(6) As DAABRequest.Parameter

        ''Deserealiza la Entidad
        oEAlertas = CFunciones.DeserializeObject(Of EGCC_Alertas)(pEAlertas)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@pTipo", DbType.Int32, 1, oEAlertas.Tipo, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pCodNroLote", DbType.String, 8, oEAlertas.NroLote, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pCodNroContrato", DbType.String, 8, oEAlertas.NumContrato, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pPeriodo", DbType.String, 10, oEAlertas.Periodo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pCodTasador", DbType.String, 8, oEAlertas.CodTasador, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pAudUsuarioRegistro", DbType.String, 12, oEAlertas.AudUsuarioRegistro, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pTipoConcepto", DbType.String, 8, oEAlertas.TipoConcepto, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_AlertaHistorial_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fInsertarAlertas", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DBloqueoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("8F9AB8A4-F602-4060-B2CB-55F4F52F1520") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DAlertasNTx")> _
Public Class DAlertasNTx
    Inherits ServicedComponent
    Implements IAlertasNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DAlertasNTx"
#End Region

#Region "Metodos"


#End Region

End Class

#End Region

