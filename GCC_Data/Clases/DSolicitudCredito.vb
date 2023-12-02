Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("10959006-8C71-4985-B068-0828755B8BF7") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoProveedorTx")> _
Public Class DSolicitudCreditoTx
    Inherits ServicedComponent
    Implements ISolicitudCreditoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DSolicitudCreditoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Modifica un registro existente de la tabla SolicitudCredito
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificaSolicitudDocumentoProveedor(ByVal pESolicitudCredito As String) As Boolean Implements ISolicitudCreditoTx.ModificaSolicitudDocumentoProveedor

        Dim oESolicitudCredito As New ESolicitudcredito

        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)

        'Campos TEMPORAL 

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_FlagTerminoRecepDocProv", DbType.Int16, 0, oESolicitudCredito.FlagTerminoRecepDocumentoProv, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pid_FechaTerminoRecepDocProv", DbType.Date, 8, oESolicitudCredito.FechaTerminoRecepDocumentoProv, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_DescripcionBien", DbType.String, 500, oESolicitudCredito.DescripcionBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoDocProveedor_upd"
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
    ''' Modifica Activacion de Contrato
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 15/10/2012
    ''' </remarks>
    Public Function ModificaActivacionContrato(ByVal pESolicitudCredito As String) As Boolean Implements ISolicitudCreditoTx.ModificaActivacionContrato

        Dim oESolicitudCredito As New ESolicitudcredito
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_FlagActivacionLeasing", DbType.String, 1, oESolicitudCredito.FlagActivacionLeasing, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoActivacion_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificaActivacionContrato", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
<Guid("97E4C390-E64D-4dbf-B4E1-FB05B425D11E") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionNTx")> _
Public Class DSolicitudCreditoNTx
    Inherits ServicedComponent
    Implements ISolicitudCreditoNTx
#Region "constantes"

    Private Const C_NOMBRE_CLASE As String = "DContratoNTx"

#End Region

    Function ListadoCheckListComercial(ByVal pESolicitudCredito As String) As String Implements ISolicitudCreditoNTx.ListadoCheckListComercial
        Dim oESolicitudCredito As ESolicitudcredito = Nothing

        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)
        prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CodigoCotizacion", DbType.String, 8, oESolicitudCredito.Codigocotizacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CheckListComercial_sel"
            objRequest.Parameters.AddRange(prmParameter)

            Using oReader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                If oReader.Read Then
                    oESolicitudCredito = New ESolicitudcredito
                    With oESolicitudCredito
                        .Codsolicitudcredito = CFunciones.CheckStr(oReader("CodSolicitudCredito"))
                        .Codigocotizacion = CFunciones.CheckStr(oReader("Codigocotizacion"))
                        .ClasificacionBien = CFunciones.CheckStr(oReader("ClasificacionBien"))
                        '.codigo()
                        '.Texto = CFunciones.CheckStr(oReader("texto"))

                        'If oReader("fecha") Is System.DBNull.Value Then
                        '.Fecha = Nothing
                        'Else
                        '.Fecha = CFunciones.CheckDate(oReader("fecha"))
                        'End If

                        '.Numero = CFunciones.CheckInt(oReader("numero"))
                        '.Decimales = CFunciones.CheckDecimal(oReader("decimales"))
                        '.Comentario = CFunciones.CheckStr(oReader("comentario"))
                        '.Flag = CFunciones.CheckStr(oReader("flag"))
                    End With
                End If
            End Using

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCheckListComercial", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of ESolicitudcredito)(oESolicitudCredito)
    End Function

    'Inicio IBK
    ''' <summary>
    ''' Lista bienes del contrato
    ''' </summary>
    ''' <param name="pNroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaBienesContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNroContrato As String) As String Implements ISolicitudCreditoNTx.ListaBienesContrato

        Dim odtbBienes As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, pNroContrato, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienesContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            'Obtiene el ResulSet
            odtbBienes = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "BienesContrato", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbBienes)

    End Function
    'Fin IBK
End Class

#End Region
