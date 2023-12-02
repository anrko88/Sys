Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity


#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoOtroConceptoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("1D8D9A2A-4484-4d21-8BDF-F2CD2463DF92") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoOtroConceptoTx")> _
Public Class DContratoOtroConceptoTx
    Inherits ServicedComponent
    Implements IContratoOtroConceptoTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoOtroConceptoTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Actualiza los datos de objeto EGCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <param name="pEGCC_ContratoOtroConcepto">Entidad EGCC_ContratoOtroConcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function fblnModificar(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean Implements IContratoOtroConceptoTx.fblnModificar

        Dim oEGCC_ContratoOtroConcepto As New EGcc_contratootroconcepto

        Dim prmParameter(4) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEGCC_ContratoOtroConcepto = CFunciones.DeserializeObject(Of EGcc_contratootroconcepto)(pEGCC_ContratoOtroConcepto)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEGCC_ContratoOtroConcepto.Numerocontrato, ParameterDirection.Input)

        prmParameter(1) = New DAABRequest.Parameter("@pid_ImportePendiente", DbType.Decimal)
        prmParameter(1).Precision = 18
        prmParameter(1).Scale = 6
        prmParameter(1).Value = oEGCC_ContratoOtroConcepto.Importependiente

        prmParameter(2) = New DAABRequest.Parameter("@piv_OtrasPenalidades", DbType.String, 500, oEGCC_ContratoOtroConcepto.Otraspenalidades, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_DiasVencimiento", DbType.Int32, 0, oEGCC_ContratoOtroConcepto.Diasvencimiento, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pid_PorcentajeCuota", DbType.Decimal)
        prmParameter(4).Precision = 18
        prmParameter(4).Scale = 6
        prmParameter(4).Value = oEGCC_ContratoOtroConcepto.Porcentajecuota

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoOtroConcepto_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de objeto EGCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <param name="pEGCC_ContratoOtroConcepto">Entidad EGCC_ContratoOtroConcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ContratoOtroConceptoAdjuntoUpd(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean Implements IContratoOtroConceptoTx.ContratoOtroConceptoAdjuntoUpd
        Dim oEGCC_ContratoOtroConcepto As New EGcc_contratootroconcepto

        Dim prmParameter(2) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEGCC_ContratoOtroConcepto = CFunciones.DeserializeObject(Of EGcc_contratootroconcepto)(pEGCC_ContratoOtroConcepto)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Numerocontrato", DbType.String, 8, oEGCC_ContratoOtroConcepto.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEGCC_ContratoOtroConcepto.Nombrearchivo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Audusuariomodificacion", DbType.String, 12, oEGCC_ContratoOtroConcepto.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoOtroConcepto_Archivoupd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoOtroConceptoAdjuntoUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    'Inicio IBK 
    Public Function fblnModificar2(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean Implements IContratoOtroConceptoTx.fblnModificar2

        Dim oEGCC_ContratoOtroConcepto As New EGcc_contratootroconcepto

        Dim prmParameter(7) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEGCC_ContratoOtroConcepto = CFunciones.DeserializeObject(Of EGcc_contratootroconcepto)(pEGCC_ContratoOtroConcepto)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEGCC_ContratoOtroConcepto.Numerocontrato, ParameterDirection.Input)

        prmParameter(1) = New DAABRequest.Parameter("@pid_ImportePendiente", DbType.Decimal)
        prmParameter(1).Precision = 18
        prmParameter(1).Scale = 6
        prmParameter(1).Value = oEGCC_ContratoOtroConcepto.Importependiente

        prmParameter(2) = New DAABRequest.Parameter("@piv_OtrasPenalidades", DbType.String, 500, oEGCC_ContratoOtroConcepto.Otraspenalidades, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_DiasVencimiento", DbType.Int32, 0, oEGCC_ContratoOtroConcepto.Diasvencimiento, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pid_PorcentajeCuota", DbType.Decimal)
        prmParameter(4).Precision = 18
        prmParameter(4).Scale = 6
        prmParameter(4).Value = oEGCC_ContratoOtroConcepto.Porcentajecuota

        prmParameter(5) = New DAABRequest.Parameter("@pid_ComisionActivacion", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEGCC_ContratoOtroConcepto.ComisionActivacion

        prmParameter(6) = New DAABRequest.Parameter("@pid_ComsionEstructuracion", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEGCC_ContratoOtroConcepto.ComisionEstructuracion

        prmParameter(7) = New DAABRequest.Parameter("@pid_OpcionCompra", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGCC_ContratoOtroConcepto.ImporteOpcionCompra

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoOtroConcepto_upd2"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    'Fin IBK

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DContratoOtroConceptoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("7EA79968-1B86-4bac-B02C-7DEFD3ECD1E0") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DBienNTx")> _
Public Class DContratoOtroConceptoNTx
    Inherits ServicedComponent
    Implements IContratoOtroConceptoNTx

#Region "constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoOtroConceptoNTx"

#End Region

#Region "Métodos"


#End Region

End Class

#End Region
