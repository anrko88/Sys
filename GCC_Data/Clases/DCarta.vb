Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCartaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("1A22A925-73F0-4ddd-B5AA-65B4F2E2B6BD") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCartaTx")> _
Public Class DCartaTx
    Inherits ServicedComponent
    Implements ICartaTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCartaTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Actualiza el estado del contrato
    ''' </summary>
    ''' <param name="pEGCC_Carta">Entidad EGCC_Carta serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function EnviarCarta(ByVal pEGCC_Carta As String) As Boolean Implements ICartaTx.EnviarCarta

        Dim oEGCC_Carta As New EGcc_carta

        Dim prmParameter(3) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEGCC_Carta = CFunciones.DeserializeObject(Of EGcc_carta)(pEGCC_Carta)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pii_Numerocarta", DbType.String, 10, oEGCC_Carta.Numerocarta, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGCC_Carta.Codigocotizacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, oEGCC_Carta.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Usuarioregistro", DbType.String, 12, oEGCC_Carta.Usuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Carta_ins"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EnviarCarta", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DCartaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("CD698DC3-C6B5-4044-B7D3-F4D7D42754D1") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCartaNTx")> _
Public Class DCartaNTx
    Inherits ServicedComponent
    Implements ICartaNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCartaNTx"
#End Region

#Region "Metodos"

#End Region

End Class

#End Region
