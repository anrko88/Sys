Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"
''' <summary>
''' Implementación de la clase DPipelineTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Guid("1465C627-A083-45c8-913E-961CC8FE80C8") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DPipelineTX")> _
Public Class DPipelineTX
    Inherits ServicedComponent
    Implements IPipelineTX
#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DPipelineTX"
#End Region
#Region "Metodos"
    ''' <summary>
    ''' Insertar Representantes del cliente
    ''' </summary>
    ''' <param name="pEPipelien">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 28/08/2012
    ''' </remarks>
    Public Function GrabarPipelineIns(ByVal pEPipelien As String) As Boolean Implements IPipelineTX.GrabarPipelineIns
        Dim blnResultado As Boolean
        Dim oEGccPipeline As EGCC_Pipeline
        ' Inicio IBK - AAE - 22/10/2012 - Se agrega campo comentario al salvar el pipeline
        ' ocmento código original
        'Dim prmParameter(7) As DAABRequest.Parameter
        Dim prmParameter(8) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccPipeline = CFunciones.DeserializeObject(Of EGCC_Pipeline)(pEPipelien)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoCotizacion", DbType.String, 20, oEGccPipeline.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 200, oEGccPipeline.CodigoSolicitud, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodigoMotivo", DbType.String, 200, oEGccPipeline.CodigoMotivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodigoEstado", DbType.String, 120, oEGccPipeline.CodigoEstado, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_PorcentajeAnterior", DbType.Decimal, 20, oEGccPipeline.PorcentajeAnterior, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_PorcentajeMesActual", DbType.Decimal, 20, oEGccPipeline.PorcentajeMesActual, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_PorcentajeMesSiguiente", DbType.Decimal, 20, oEGccPipeline.PorcentajeMesSiguiente, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_PorcentajeAnioSiguiente", DbType.Decimal, 20, oEGccPipeline.PorcentajeAnioSiguiente, ParameterDirection.Input)
				prmParameter(8) = New DAABRequest.Parameter("@pic_Comentario", DbType.String, 127, oEGccPipeline.Comentario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_pipeline_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GrabarPipelineIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function
#End Region

    Public Function EliminarPipelineDel(ByVal pCodCotizacion As String) As Boolean Implements IPipelineTX.EliminarPipelineDel
        Dim blnResultado As Boolean
        Dim prmParameter(0) As DAABRequest.Parameter
        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoCotizacion", DbType.String, 20, pCodCotizacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_pipeline_Del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarPipeline", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function
End Class

#End Region

#Region "Clase NO Transaccional"
''' <summary>
''' Implementación de la clase DPipelineNTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Guid("515D007D-D067-41f4-924B-2FD3417CD6DB") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DPipelineNTX")> _
Public Class DPipelineNTX
    Inherits ServicedComponent
    Implements IPipelineNTX

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DPipelineNTX"
#End Region

#Region "Metodos"
    Public Function ListarPipeline(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEPipeline As String) As String Implements IPipelineNTX.ListarPipeline

        Dim oEPipeline As New EGCC_Pipeline
        oEPipeline = CFunciones.DeserializeObject(Of EGCC_Pipeline)(pEPipeline)
        'Deserealiza la Entidad
        Dim odtPipeline As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(9) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 8000, oEPipeline.CodUnico, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, oEPipeline.RazonSocial, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_EjecutivoLeasing", DbType.String, 20, oEPipeline.EjecutivoLeasing, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_CodCotizacion", DbType.String, 80, oEPipeline.Codigocotizacion, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_CodEstado", DbType.String, 3, oEPipeline.CodigoEstado, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_CodBanca", DbType.String, 2, oEPipeline.CodBanca, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Pipeline_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtPipeline = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarPipeline", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtPipeline)
    End Function

#End Region


End Class

#End Region
