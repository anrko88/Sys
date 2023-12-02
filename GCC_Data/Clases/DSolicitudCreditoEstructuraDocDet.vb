Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DSolicitudCreditoEstructuraDocDetTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/06/2012
''' </remarks>
<Guid("C50AA261-04F1-4886-8BA1-A838E10DDF43") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSolicitudCreditoEstructuraDocDetTx")> _
Public Class DSolicitudCreditoEstructuraDocDetTx
    Inherits ServicedComponent
    Implements ISolicitudCreditoEstructuraDocDetTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DSolicitudCreditoEstructuraDocDetTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta Contrato EstructDocDet
    ''' </summary>
    ''' <param name="pEContratoEstructDocDet">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function InsertarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean Implements ISolicitudCreditoEstructuraDocDetTx.InsertarContratoEstructDocDet

        Dim oESolEstDocDet As New ESolicitudcreditoestructuradocdet
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolEstDocDet = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradocdet)(pEContratoEstructDocDet)

        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolEstDocDet.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESolEstDocDet.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oESolEstDocDet.Tipodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oESolEstDocDet.Nrodocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.String, 10, oESolEstDocDet.StringFechaemision, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oESolEstDocDet.Codproveedor, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DocumentoBien_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoEstructDocDet", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar Contrato EstructDocDet
    ''' </summary>
    ''' <param name="pEContratoEstructDocDet">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function EliminarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean Implements ISolicitudCreditoEstructuraDocDetTx.EliminarContratoEstructDocDet

        Dim oESolEstDocDet As New ESolicitudcreditoestructuradocdet
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolEstDocDet = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradocdet)(pEContratoEstructDocDet)

        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolEstDocDet.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESolEstDocDet.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oESolEstDocDet.Tipodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oESolEstDocDet.Nrodocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.String, 10, oESolEstDocDet.StringFechaemision, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oESolEstDocDet.Codproveedor, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DocumentoBien_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "up_gcc_DocumentoBien_del", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    'inicio IBK
    ''' <summary>
    ''' Actualiza las relaciones entre bienes y documentos
    ''' </summary>
    ''' <param name="pstrArrayDocs">Arreglo de tipodoc;nrodoc;codprov;fecha(ddmmyyyy)|....|.... </param>
    ''' <param name="nbrArraySize">Tamaño del arreglo, el areglo se divide por ;</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function AgregarRelacion(ByVal pstrNroContrato As String, _
                             ByVal pstrSecBien As String, _
                             ByVal pstrArrayDocs As String, _
                             ByVal nbrArraySize As Integer) As Boolean Implements ISolicitudCreditoEstructuraDocDetTx.AgregarRelacion


        Dim prmParameter(3) As DAABRequest.Parameter
        Dim nbrlargoSecBien As Integer = pstrSecBien.Length
        Dim nbrlargoArray As Integer = pstrArrayDocs.Length

        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_SecFinanciamiento", DbType.String, nbrlargoSecBien, pstrSecBien, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Documentos", DbType.String, nbrlargoArray, pstrArrayDocs, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CantDocs", DbType.Int16, 0, nbrArraySize, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_rel_doc_bien_add"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "up_gcc_DocumentoBien_del", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function
    'Fin IBK

#End Region

End Class

#End Region


#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DSolicitudCreditoEstructuraDocDetNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/06/2012
''' </remarks>
<Guid("BE7009DE-C9B9-4d46-95AE-C2D5C2E2EED6") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSolicitudCreditoEstructuraDocDetTx")> _
Public Class DSolicitudCreditoEstructuraDocDetNTx
    Inherits ServicedComponent
    Implements ISolicitudCreditoEstructuraDocDetNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DSolicitudCreditoEstructuraDocDetNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function ListadoContratoEstructDocDet(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocDetNTx.ListadoContratoEstructDocDet

        Dim oEContratoEstrucDoc As New ESolicitudcreditoestructuradoc
        oEContratoEstrucDoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)
        'Deserealiza la Entidad
        Dim odtbBienes As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(9) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEContratoEstrucDoc.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oEContratoEstrucDoc.Tipodocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oEContratoEstrucDoc.Nrodocumento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.String, 10, oEContratoEstrucDoc.StringFechaEmision, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oEContratoEstrucDoc.CodProveedor, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodigoTipoBien", DbType.String, 100, oEContratoEstrucDoc.CodigoTipoBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DocumentoBien_sel"
            objRequest.Parameters.AddRange(prmParameter)
            'Obtiene el ResulSet
            odtbBienes = objRequest.Factory.ExecuteDataset(objRequest).Tables(1)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoDesembolsoDocumento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbBienes)
    End Function

    'Inicio IBK
    ''' <summary>
    ''' Lista relaciones del contrato
    ''' </summary>
    ''' <param name="pNroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaRelacionesContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNroContrato As String) As String Implements ISolicitudCreditoEstructuraDocDetNTx.ListaRelacionesContrato

        Dim odtbRelaciones As DataTable
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
            objRequest.Command = "up_gcc_RelacionesContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            'Obtiene el ResulSet
            odtbRelaciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RelacionesContrato", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbRelaciones)

    End Function
    'Fin IBK

#End Region

End Class


#End Region
