Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"
<Guid("28F2963A-1D02-4194-93DA-1F4E1B287987") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DTasadorTx")> _
Public Class DTasadorTx
    Inherits ServicedComponent
    Implements ITasadorTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DTasadorTx"
#End Region


    Public Function InsertarTasador(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.InsertarTasador
        Dim blnResultado As String
        ' Dim idpCodigoRepresentante As IDataParameter
        Dim oEGccSolicitudCreditoEstructuraTasacion As New ESolicitudcreditoestructuratasacion
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccSolicitudCreditoEstructuraTasacion = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuratasacion)(pEGCC_ContratoTasador)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGccSolicitudCreditoEstructuraTasacion.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodTasador", DbType.String, 3, oEGccSolicitudCreditoEstructuraTasacion.Codtasador, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_usuario", DbType.String, 8, oEGccSolicitudCreditoEstructuraTasacion.Usuarioregistro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_estadoTasacion", DbType.String, 1, oEGccSolicitudCreditoEstructuraTasacion.CodEstadoTasacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bien_Tasacion_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarTasador", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function


    Public Function ActualizaTasador(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.ActualizaTasador
        Dim blnResultado As String
        ' Dim idpCodigoRepresentante As IDataParameter
        Dim oEGccSolicitudCreditoEstructuraTasacion As New ESolicitudcreditoestructuratasacion
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccSolicitudCreditoEstructuraTasacion = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuratasacion)(pEGCC_ContratoTasador)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGccSolicitudCreditoEstructuraTasacion.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.String, 3, oEGccSolicitudCreditoEstructuraTasacion.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_Codtasacion", DbType.Int32, 0, oEGccSolicitudCreditoEstructuraTasacion.Codtasacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Codtasador", DbType.String, 3, oEGccSolicitudCreditoEstructuraTasacion.Codtasador, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_usuario", DbType.String, 1, oEGccSolicitudCreditoEstructuraTasacion.Usuarioregistro, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodMotivonoTasacion", DbType.String, 3, oEGccSolicitudCreditoEstructuraTasacion.Codmotivonotasacion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_FechaTasacion", DbType.String, 10, oEGccSolicitudCreditoEstructuraTasacion.VfechaProxTasacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bien_Tasacion_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizaTasador", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function


    ''' <summary>
    ''' EnviarCarta
    ''' </summary>
    ''' <param name="pEGCC_ContratoTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EnviarCarta(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.EnviarCarta
        Dim blnResultado As String
        ' Dim idpCodigoRepresentante As IDataParameter
        Dim oEGccSolicitudCreditoEstructuraTasacion As New ESolicitudcreditoestructuratasacion
        Dim prmParameter(2) As DAABRequest.Parameter
        'Deserealiza la Entidad
        oEGccSolicitudCreditoEstructuraTasacion = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuratasacion)(pEGCC_ContratoTasador)
        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGccSolicitudCreditoEstructuraTasacion.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_estadobientasado", DbType.String, 1, oEGccSolicitudCreditoEstructuraTasacion.CodEstadoTasacion, ParameterDirection.Input)

        prmParameter(2) = New DAABRequest.Parameter("@piv_usuario", DbType.String, 10, oEGccSolicitudCreditoEstructuraTasacion.Usuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bien_Tasacion_EstadoBien"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EnviarCarta", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    Public Function ActualizarTasacion(ByVal pEGCC_ContratoTasador As String) As String Implements ITasadorTx.ActualizarTasacion
        Dim blnResultado As String
        ' Dim idpCodigoRepresentante As IDataParameter
        Dim parSalida As IDataParameter
        Dim oEGccSolicitudCreditoEstructuraTasacion As New ESolicitudcreditoestructuratasacion
        Dim prmParameter(8) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccSolicitudCreditoEstructuraTasacion = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuratasacion)(pEGCC_ContratoTasador)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pii_CodTasacion", DbType.Int32, 4, oEGccSolicitudCreditoEstructuraTasacion.Codtasacion, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGccSolicitudCreditoEstructuraTasacion.Codsolicitudcredito, ParameterDirection.Input)

        prmParameter(2) = New DAABRequest.Parameter("@pii_ValorEjecucion", DbType.Decimal)
        prmParameter(2).Precision = 18
        prmParameter(2).Scale = 6
        prmParameter(2).Value = oEGccSolicitudCreditoEstructuraTasacion.Valorejecucion

        prmParameter(3) = New DAABRequest.Parameter("@pii_ValorComercial", DbType.Decimal)
        prmParameter(3).Precision = 18
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEGccSolicitudCreditoEstructuraTasacion.Valorcomercial

        prmParameter(4) = New DAABRequest.Parameter("@piv_FechaEncargo", DbType.String, 10, oEGccSolicitudCreditoEstructuraTasacion.VFechaencargo, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_FechaTasacion", DbType.String, 10, oEGccSolicitudCreditoEstructuraTasacion.VFechatasacion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodTasador", DbType.String, 3, oEGccSolicitudCreditoEstructuraTasacion.Codtasador, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int32, 1, oEGccSolicitudCreditoEstructuraTasacion.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Usuario", DbType.String, 12, oEGccSolicitudCreditoEstructuraTasacion.Usuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_bien_Tasacion_asigna"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            'blnResultado = "True"
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarTasacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'blnResultado = "False"
            Throw ex
        Finally
            parSalida = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return parSalida.Value.ToString()

    End Function
End Class

#End Region


#Region "Clase No Transaccional"
<Guid("87EC3B92-F36C-4230-B183-A41C380659B8") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DTasadorNTx")> _
Public Class DTasadorNTx
    Inherits ServicedComponent
    Implements ITasadorNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DTasadorNTx"
#End Region

    ''' <summary>
    ''' Obtiene Datos de Contrato para Tasacion
    ''' </summary>
    ''' <param name="pNumeroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Public Function ObtenerContratoTasacion(ByVal pstrNroContrato As String) As String Implements ITasadorNTx.ObtenerContratoTasacion
        'Variables
        Dim odtbObtener As New DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_NroContrato", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_Tasacion_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbObtener = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    Public Function ListadoContratoTasador(ByVal pPageSize, _
                                           ByVal pCurrentPage, _
                                           ByVal pSortColumn, _
                                           ByVal pSortOrder, _
                                           ByVal pCodSolicitudcredito, _
                                           ByVal pCuCliente, _
                                           ByVal pRazonsolcial, _
                                           ByVal pTipoDocumento, _
                                           ByVal pNumerodocumento, _
                                           ByVal pEstadoTasacion, _
                                           ByVal pClasificacionBien, _
                                           ByVal pBanca, _
                                           ByVal pEjecutivoBanca, _
                                           ByVal pPeriodo, _
                                           ByVal pFechadesde, _
                                           ByVal pFechaHasta, _
                                           ByVal pEstadoTasacionContrato) As String Implements ITasadorNTx.ListadoContratoTasador
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(16) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            '    'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int16, 4, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int16, 4, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodSolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_pCuCliente", DbType.String, 10, pCuCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_pRazonsolcial", DbType.String, 50, pRazonsolcial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_pTipoDocumento", DbType.String, 3, pTipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_pNumerodocumento", DbType.String, 20, pNumerodocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_pEstadoTasacion", DbType.String, 3, pEstadoTasacion, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_pClasificacionBien", DbType.String, 50, pClasificacionBien, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_pBanca", DbType.String, 100, pBanca, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_pEjecutivoBanca", DbType.String, 3, pEjecutivoBanca, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_pPeriodo", DbType.String, 4, pPeriodo, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@piv_pFechadesde", DbType.String, 10, pFechadesde, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@piv_pFechaHasta", DbType.String, 10, pFechaHasta, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@piv_EstadoTasacionContrato", DbType.String, 2, pEstadoTasacionContrato, ParameterDirection.Input)
            'prmParameter(17) = New DAABRequest.Parameter("@piv_EstadoTasacionContrato", DbType.String, 2, pEstadocontrato, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_Tasacion_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratoTasador", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)

    End Function

    Public Function ObtenerContratoCotizacionSaldoFinanciado(ByVal pstrNroContrato As String) As String Implements ITasadorNTx.ObtenerContratoCotizacionSaldoFinanciado
        'Variables
        Dim odtbObtener As New DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienTasacionRegistro_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbObtener = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista GCC_ContratoTasacion
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>    
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function ListaContratoBienTasacion(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodSolicitudcredito As String) As String Implements ITasadorNTx.ListaContratoBienTasacion

        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            '    'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int16, 4, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int16, 4, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, pCodSolicitudcredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienTasacionAsignacion_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListaContratoBienTasacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)

    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_HISTORICOTASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>
    ''' <param name="pCodContratoTasacion">Código de Contrato Tasación</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function ListaHistoricoContratoBienTasacion(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pCodSolicitudcredito As String, _
                                                      ByVal pCodContratoTasacion As Short) As String Implements ITasadorNTx.ListaHistoricoContratoBienTasacion

        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            '    'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int16, 4, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int16, 4, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, pCodSolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pis_CodContratoTasacion", DbType.Int16, 0, pCodContratoTasacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_Tasacion_Individual_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListaHistoricoContratoBienTasacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)

    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_TASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function ListaBienTasacion(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pCodSolicitudcredito As String) As String Implements ITasadorNTx.ListaBienTasacion

        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            '    'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int16, 4, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int16, 4, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, pCodSolicitudcredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienTasacion_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListaBienTasacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)

    End Function



    Public Function calculatotales(ByVal pCodSolicitudcredito) As String Implements ITasadorNTx.calculatotales

        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            '    'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 8, pCodSolicitudcredito, ParameterDirection.Input)
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_Tasacion_Individual_total"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "calculatotales", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)

    End Function
End Class

#End Region