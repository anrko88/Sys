Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity


#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DBienNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("3AC87D47-6095-40b1-AE50-D42499737335") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DBienTx")> _
Public Class DBienTx
    Inherits ServicedComponent
    Implements IBienTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DBienTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inserta un nuevo bien en las tablas ESolicitudCreditoEstructura y ESolicitudCreditoEstructuraCarac
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>
    ''' <param name="pESolicitudCreditoEstructuraCarac">Entidad ESolicitudCreditoEstructuraCarac serializada</param>
    ''' <returns>String con el número de Temporal</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnInsertar(ByVal pESolicitudCreditoEstructura As String, _
                                 ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean Implements IBienTx.fblnInsertar

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

        'Dim prmParameter(29) As DAABRequest.Parameter
        Dim prmParameter(31) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)
        oESolicitudCreditoEstructuraCarac = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pESolicitudCreditoEstructuraCarac)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_Tiporubrofinanciamiento", DbType.String, 3, oESolicitudCreditoEstructura.Tiporubrofinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Codusuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_Tipoproducto", DbType.String, 3, oESolicitudCreditoEstructura.Tipoproducto, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pis_Cantidadproducto", DbType.Int16, 0, oESolicitudCreditoEstructura.Cantidadproducto, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_Codproveedor", DbType.String, 4, oESolicitudCreditoEstructura.Codproveedor, ParameterDirection.Input)

        prmParameter(6) = New DAABRequest.Parameter("@pid_Tipocambio", DbType.Decimal)
        prmParameter(6).Precision = 20
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oESolicitudCreditoEstructura.Tipocambio

        prmParameter(7) = New DAABRequest.Parameter("@pid_Monedabien", DbType.String, 5, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_Clase", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Clase, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_Marca", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Marca, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_Modelo", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Modelo, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_Carroceria", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Carroceria, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pis_Anio", DbType.Int16, 0, oESolicitudCreditoEstructuraCarac.Anio, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_Color", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Color, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_Nroserie", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Nroserie, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_Nromotor", DbType.String, 15, oESolicitudCreditoEstructuraCarac.Nromotor, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_Placa", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Placa, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pic_Distrito", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Distrito, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_Provincia", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Provincia, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_Departamento", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Departamento, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 250, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_Ubicacion", DbType.String, 250, oESolicitudCreditoEstructura.Ubicacion, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_PartidaRegistral", DbType.String, 50, oESolicitudCreditoEstructuraCarac.PartidaRegistral, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_OficinaRegistral", DbType.String, 50, oESolicitudCreditoEstructuraCarac.OficinaRegistral, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_Medidas", DbType.String, 100, oESolicitudCreditoEstructuraCarac.Medidas, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@piv_CodigoEstadoBien", DbType.String, 100, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_ValorBien", DbType.String, 250, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oESolicitudCreditoEstructuraCarac.Comentario, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@piv_CodigoPredio", DbType.String, 100, oESolicitudCreditoEstructuraCarac.CodigoPredio, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@flag_origen", DbType.Int32, 8, oESolicitudCreditoEstructura.Flag_origen, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@piv_UsuarioCreacion", DbType.String, 40, oESolicitudCreditoEstructura.AudUsuarioCreacion, ParameterDirection.Input)

        'IBK - RPH
        prmParameter(31) = New DAABRequest.Parameter("@piv_CodTipobien", DbType.String, 100, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        'Fin
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bien_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertar", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.
    ''' Soporta los tipos de producto Inmueble = , Inmueble y otros.
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>
    ''' <param name="pESolicitudCreditoEstructuraCarac">Entidad ESolicitudCreditoEstructuraCarac serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnModificar(ByVal pESolicitudCreditoEstructura As String, _
                                  ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean Implements IBienTx.fblnModificar

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim oESolicitudCreditoEstructuraCarac As New ESolicitudcreditoestructuracarac

        Dim prmParameter(28) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)
        oESolicitudCreditoEstructuraCarac = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pESolicitudCreditoEstructuraCarac)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Tiporubrofinanciamiento", DbType.String, 3, oESolicitudCreditoEstructura.Tiporubrofinanciamiento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_Codusuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Tipoproducto", DbType.String, 3, oESolicitudCreditoEstructura.Tipoproducto, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pis_Cantidadproducto", DbType.Int16, 0, oESolicitudCreditoEstructura.Cantidadproducto, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_Codproveedor", DbType.String, 4, oESolicitudCreditoEstructura.Codproveedor, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pid_Tipocambio", DbType.Decimal)
        prmParameter(7).Precision = 20
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oESolicitudCreditoEstructura.Tipocambio

        prmParameter(8) = New DAABRequest.Parameter("@pid_Monedabien", DbType.String, 5, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_Clase", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Clase, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_Marca", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Marca, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_Modelo", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Modelo, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_Carroceria", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Carroceria, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pis_Anio", DbType.Int16, 0, oESolicitudCreditoEstructuraCarac.Anio, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_Color", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Color, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_Nroserie", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Nroserie, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_Nromotor", DbType.String, 15, oESolicitudCreditoEstructuraCarac.Nromotor, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pic_Placa", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Placa, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_Distrito", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Distrito, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_Provincia", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Provincia, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pic_Departamento", DbType.String, 20, oESolicitudCreditoEstructuraCarac.Departamento, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 250, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_Ubicacion", DbType.String, 250, oESolicitudCreditoEstructura.Ubicacion, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_PartidaRegistral", DbType.String, 50, oESolicitudCreditoEstructuraCarac.PartidaRegistral, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_OficinaRegistral", DbType.String, 50, oESolicitudCreditoEstructuraCarac.OficinaRegistral, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@piv_Medidas", DbType.String, 100, oESolicitudCreditoEstructuraCarac.Medidas, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_CodigoEstadoBien", DbType.String, 100, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_ValorBien", DbType.String, 250, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oESolicitudCreditoEstructuraCarac.Comentario, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Bien_upd"
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
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function fblnModificarBienDesembolso(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnModificarBienDesembolso

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim prmParameter(12) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 100, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 10, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaTransferencia), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oESolicitudCreditoEstructura.Comentario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_Color", DbType.String, 20, oESolicitudCreditoEstructura.Color, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_Distrito", DbType.String, 20, oESolicitudCreditoEstructura.Distrito, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_ValorBien", DbType.String, 20, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_PartidaRegistral", DbType.String, 12, oESolicitudCreditoEstructura.PartidaRegistral, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_OficinaRegistral", DbType.String, 50, oESolicitudCreditoEstructura.OficinaRegistral, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 4, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pii_Flag", DbType.Int16, 0, oESolicitudCreditoEstructura.Flag, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionBienDesembolso_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function


    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function fblnModificarBien(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnModificarBien

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        'Iniccio IBK
        'Dim prmParameter(31) As DAABRequest.Parameter
        Dim prmParameter(32) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Departamento", DbType.String, 10, oESolicitudCreditoEstructura.Departamento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_Distrito", DbType.String, 10, oESolicitudCreditoEstructura.Distrito, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Provincia", DbType.String, 10, oESolicitudCreditoEstructura.Provincia, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_ubicacion", DbType.String, 100, oESolicitudCreditoEstructura.Ubicacion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_cantidad", DbType.String, 20, oESolicitudCreditoEstructura.Cantidadproducto, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 500, oESolicitudCreditoEstructura.Comentario, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_CodigoEstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.EstadoBien, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 4, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_ValorBien", DbType.String, 20, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaTransferencia), ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_FechaAdquisicion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaAdquisicion), ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_CodigoPredio", DbType.String, 40, oESolicitudCreditoEstructura.CodigoPredio, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_observacion", DbType.String, 500, oESolicitudCreditoEstructura.ObservacionBien, ParameterDirection.Input)

        prmParameter(16) = New DAABRequest.Parameter("@pic_FechaProbableFinObra", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaProbableFinObra), ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pic_FechaRealFinObra", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaRealFinObra), ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_FechaInscripcionMunicipal", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionMunicipal), ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_FechaEnvioNotaria", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEnvioNotaria), ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pic_FechaPropiedad", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaPropiedad), ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pic_FechaInscripcionRegistral", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionRegistral), ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pic_OficinaRegistral", DbType.String, 40, oESolicitudCreditoEstructura.OficinaRegistral, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 10, oESolicitudCreditoEstructura.CodigoNotaria, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@pic_CodEstadoInscripcionRRPP", DbType.String, 10, oESolicitudCreditoEstructura.CodEstadoInscripcionRrPp, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@pic_CodEstadoMunicipal", DbType.String, 10, oESolicitudCreditoEstructura.CodEstadoMunicipal, ParameterDirection.Input)

        prmParameter(26) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@pic_flagOrigen", DbType.Int32, 8, oESolicitudCreditoEstructura.Flag_origen, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pic_CodEstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@pii_Flag", DbType.Int16, 0, oESolicitudCreditoEstructura.Flag, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 100, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)
        prmParameter(31) = New DAABRequest.Parameter("@pic_FechaBaja", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaBaja), ParameterDirection.Input)
        'Inicio IBK
        prmParameter(32) = New DAABRequest.Parameter("@pic_Municipalidad", DbType.String, 3, oESolicitudCreditoEstructura.Municipalidad, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionBien_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function fblnModificarMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnModificarMaquinaria

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim prmParameter(17) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 100, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 10, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaTransferencia), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oESolicitudCreditoEstructura.Comentario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_Color", DbType.String, 20, oESolicitudCreditoEstructura.Color, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_Distrito", DbType.String, 20, oESolicitudCreditoEstructura.Distrito, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_ValorBien", DbType.String, 20, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_PlacaActual", DbType.String, 10, oESolicitudCreditoEstructura.Placa, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_PlacaAnterior", DbType.String, 10, oESolicitudCreditoEstructura.PlacaAntigua, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_AnioFabricacion", DbType.Int32, 4, oESolicitudCreditoEstructura.Anio, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_NroMotor", DbType.String, 50, oESolicitudCreditoEstructura.NroMotor, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_Carroceria", DbType.String, 100, oESolicitudCreditoEstructura.Carroceria, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_Medidas", DbType.String, 100, oESolicitudCreditoEstructura.Medidas, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 4, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pii_Flag", DbType.Int16, 0, oESolicitudCreditoEstructura.Flag, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 100, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionBienMaquinaria_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 16/10/2012
    ''' </remarks>
    Public Function fblnModificarDetalleMaquinaria(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnModificarDetalleMaquinaria

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim prmParameter(30) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_TipoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaTransferencia), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Color", DbType.String, 50, oESolicitudCreditoEstructura.Color, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 500, oESolicitudCreditoEstructura.ObservacionBien, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_CodDepartamento", DbType.String, 10, oESolicitudCreditoEstructura.Departamento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_CodProvincia", DbType.String, 10, oESolicitudCreditoEstructura.Provincia, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 10, oESolicitudCreditoEstructura.Distrito, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_ValorBien", DbType.String, 20, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_PlacaActual", DbType.String, 40, oESolicitudCreditoEstructura.Placa, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_PlacaAnterior", DbType.String, 40, oESolicitudCreditoEstructura.PlacaAntigua, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pii_AnioFabricacion", DbType.Int32, 4, oESolicitudCreditoEstructura.Anio, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_NroMotor", DbType.String, 40, oESolicitudCreditoEstructura.NroMotor, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Medidas", DbType.String, 40, oESolicitudCreditoEstructura.Medidas, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 10, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@piv_Ubicacion", DbType.String, 100, oESolicitudCreditoEstructura.Ubicacion, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pii_Cantidad", DbType.Int32, 8, oESolicitudCreditoEstructura.Cantidadproducto, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_Descripcion", DbType.String, 250, oESolicitudCreditoEstructura.Comentario, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_EstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.EstadoBien, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pid_FechaAdquisicion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaAdquisicion), ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pic_CodEstado", DbType.String, 10, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_NroSerie", DbType.String, 20, oESolicitudCreditoEstructura.NroSerie, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_Marca", DbType.String, 20, oESolicitudCreditoEstructura.Marca, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_Modelo", DbType.String, 20, oESolicitudCreditoEstructura.Modelo, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@piv_Carroceria", DbType.String, 100, oESolicitudCreditoEstructura.Carroceria, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@pic_CodEstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pic_flagOrigen", DbType.Int16, 0, oESolicitudCreditoEstructura.Flag_origen, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@pid_FechaBaja", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaBaja), ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 100, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionBienDetalleMaquinaria_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/05/2012
    ''' </remarks>
    Public Function fblnModificarVehiculo(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnModificarVehiculo

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        'Dim prmParameter(63) As DAABRequest.Parameter
        'Inicio IBK
        Dim prmParameter(64) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_TipoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaTransferencia), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Color", DbType.String, 50, oESolicitudCreditoEstructura.Color, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 500, oESolicitudCreditoEstructura.ObservacionBien, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_CodDepartamento", DbType.String, 10, oESolicitudCreditoEstructura.Departamento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_CodProvincia", DbType.String, 10, oESolicitudCreditoEstructura.Provincia, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 10, oESolicitudCreditoEstructura.Distrito, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_ValorBien", DbType.String, 20, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_PlacaActual", DbType.String, 40, oESolicitudCreditoEstructura.Placa, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_PlacaAnterior", DbType.String, 40, oESolicitudCreditoEstructura.PlacaAntigua, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pii_AnioFabricacion", DbType.Int32, 4, oESolicitudCreditoEstructura.Anio, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_NroMotor", DbType.String, 40, oESolicitudCreditoEstructura.NroMotor, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Medidas", DbType.String, 40, oESolicitudCreditoEstructura.Medidas, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 10, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@piv_Ubicacion", DbType.String, 100, oESolicitudCreditoEstructura.Ubicacion, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pii_Cantidad", DbType.Int32, 8, oESolicitudCreditoEstructura.Cantidadproducto, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_Descripcion", DbType.String, 250, oESolicitudCreditoEstructura.Comentario, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_EstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.EstadoBien, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pid_FechaAdquisicion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaAdquisicion), ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pid_FechaBaja", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaBaja), ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pii_FlagOrigen", DbType.Int16, 0, oESolicitudCreditoEstructura.Flag_origen, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@pic_CodEstado", DbType.String, 10, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_Transmision", DbType.String, 40, oESolicitudCreditoEstructura.CodTransmision, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@piv_Traccion", DbType.String, 40, oESolicitudCreditoEstructura.CodTraccion, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@pic_TipoMotor", DbType.String, 10, oESolicitudCreditoEstructura.CodTipoMotor, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_PotenciaMotor", DbType.String, 10, oESolicitudCreditoEstructura.CodPotenciaMotor, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pic_Combustible", DbType.String, 40, oESolicitudCreditoEstructura.CodCombustible, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@piv_Cilindros", DbType.String, 40, oESolicitudCreditoEstructura.Cilindros, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@piv_Longitud", DbType.String, 40, oESolicitudCreditoEstructura.Longitud, ParameterDirection.Input)
        prmParameter(31) = New DAABRequest.Parameter("@piv_Pasajeros", DbType.String, 40, oESolicitudCreditoEstructura.Pasajeros, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pie_PesoNeto", DbType.String, 40, oESolicitudCreditoEstructura.PesoNeto, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pie_CargaUtil", DbType.String, 40, oESolicitudCreditoEstructura.CargaUtil, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@pie_PesoBruto", DbType.String, 40, oESolicitudCreditoEstructura.PesoBruto, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@piv_Asientos", DbType.Int32, 8, oESolicitudCreditoEstructura.Asientos, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@pii_Ejes", DbType.Int32, 8, oESolicitudCreditoEstructura.Ejes, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pie_Ancho", DbType.String, 40, oESolicitudCreditoEstructura.Ancho, ParameterDirection.Input)
        prmParameter(38) = New DAABRequest.Parameter("@pii_Puertas", DbType.Int32, 8, oESolicitudCreditoEstructura.Puertas, ParameterDirection.Input)
        prmParameter(39) = New DAABRequest.Parameter("@pie_Alto", DbType.String, 40, oESolicitudCreditoEstructura.Alto, ParameterDirection.Input)
        prmParameter(40) = New DAABRequest.Parameter("@pii_Ruedas", DbType.Int32, 8, oESolicitudCreditoEstructura.Ruedas, ParameterDirection.Input)
        prmParameter(41) = New DAABRequest.Parameter("@piv_FormulaRodante", DbType.String, 40, oESolicitudCreditoEstructura.FormulaRodante, ParameterDirection.Input)
        prmParameter(42) = New DAABRequest.Parameter("@piv_NroSerie", DbType.String, 20, oESolicitudCreditoEstructura.NroSerie, ParameterDirection.Input)
        prmParameter(43) = New DAABRequest.Parameter("@piv_Marca", DbType.String, 20, oESolicitudCreditoEstructura.Marca, ParameterDirection.Input)
        prmParameter(44) = New DAABRequest.Parameter("@piv_Modelo", DbType.String, 20, oESolicitudCreditoEstructura.Modelo, ParameterDirection.Input)
        prmParameter(45) = New DAABRequest.Parameter("@piv_Carroceria", DbType.String, 100, oESolicitudCreditoEstructura.Carroceria, ParameterDirection.Input)
        prmParameter(46) = New DAABRequest.Parameter("@pic_CodClase", DbType.String, 10, oESolicitudCreditoEstructura.CodClase, ParameterDirection.Input)
        prmParameter(47) = New DAABRequest.Parameter("@piv_Clase", DbType.String, 20, oESolicitudCreditoEstructura.Clase, ParameterDirection.Input)
        prmParameter(48) = New DAABRequest.Parameter("@piv_Cilindraje", DbType.String, 40, oESolicitudCreditoEstructura.Cilindraje, ParameterDirection.Input)
        prmParameter(49) = New DAABRequest.Parameter("@pid_FechaEnvioSat", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEnvioSat), ParameterDirection.Input)
        prmParameter(50) = New DAABRequest.Parameter("@pid_FechaInscripcionMunicipal", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionMunicipal), ParameterDirection.Input)
        prmParameter(51) = New DAABRequest.Parameter("@pid_FechaEnvioNotaria", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEnvioNotaria), ParameterDirection.Input)
        prmParameter(52) = New DAABRequest.Parameter("@pid_FechaEnvioRrpp", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEnvioRrPp), ParameterDirection.Input)
        prmParameter(53) = New DAABRequest.Parameter("@pid_FechaPropiedad", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaPropiedad), ParameterDirection.Input)
        prmParameter(54) = New DAABRequest.Parameter("@pid_FechaInscripcionRegistral", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionRegistral), ParameterDirection.Input)
        prmParameter(55) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(56) = New DAABRequest.Parameter("@pEstadoInscripcionMunicipal", DbType.String, 10, oESolicitudCreditoEstructura.CodEstadoMunicipal, ParameterDirection.Input)
        prmParameter(57) = New DAABRequest.Parameter("@pEstadoInscripcionRrpp", DbType.String, 10, oESolicitudCreditoEstructura.CodEstadoInscripcionRrPp, ParameterDirection.Input)
        prmParameter(58) = New DAABRequest.Parameter("@pic_CodEstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(59) = New DAABRequest.Parameter("@pic_flagOrigen", DbType.Int16, 0, oESolicitudCreditoEstructura.Flag_origen, ParameterDirection.Input)
        prmParameter(60) = New DAABRequest.Parameter("@pid_FechaEmisionTarjeta", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEmisionTarjeta), ParameterDirection.Input)
        prmParameter(61) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 100, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)
        prmParameter(62) = New DAABRequest.Parameter("@pCodImpuesto", DbType.String, 10, oESolicitudCreditoEstructura.CodPagoImpuesto, ParameterDirection.Input)
        prmParameter(63) = New DAABRequest.Parameter("@pCodInafectacion", DbType.String, 10, oESolicitudCreditoEstructura.CodInafectacion, ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(64) = New DAABRequest.Parameter("@piv_usuariomodificacion", DbType.String, 40, oESolicitudCreditoEstructura.Audusuariomodificacion, ParameterDirection.Input)
        'Fin IBK - AAE

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionBienVehiculo_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    Public Function fblnModificarOtros(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnModificarDetalleOtros

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim prmParameter(26) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Departamento", DbType.String, 10, oESolicitudCreditoEstructura.Departamento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_Distrito", DbType.String, 10, oESolicitudCreditoEstructura.Distrito, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Provincia", DbType.String, 10, oESolicitudCreditoEstructura.Provincia, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NroSerie", DbType.String, 20, oESolicitudCreditoEstructura.NroSerie, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NroMotor", DbType.String, 40, oESolicitudCreditoEstructura.NroMotor, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Color", DbType.String, 50, oESolicitudCreditoEstructura.Color, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_ubicacion", DbType.String, 100, oESolicitudCreditoEstructura.Ubicacion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Marca", DbType.String, 20, oESolicitudCreditoEstructura.Marca, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Modelo", DbType.String, 20, oESolicitudCreditoEstructura.Modelo, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_PartidaRegistral", DbType.String, 40, oESolicitudCreditoEstructura.PartidaRegistral, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_OficinaRegistral", DbType.String, 40, oESolicitudCreditoEstructura.OficinaRegistral, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_cantidad", DbType.String, 20, oESolicitudCreditoEstructura.Cantidadproducto, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 500, oESolicitudCreditoEstructura.Comentario, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CodigoEstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.EstadoBien, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 4, oESolicitudCreditoEstructura.Monedabien, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_ValorBien", DbType.String, 20, oESolicitudCreditoEstructura.ValorBien, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaTransferencia), ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pic_FechaBaja", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaBaja), ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pic_FechaAdquisicion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaAdquisicion), ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pic_observacion", DbType.String, 500, oESolicitudCreditoEstructura.ObservacionBien, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCreditoEstructura.Codusuario, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@pic_flagOrigen", DbType.Int32, 8, oESolicitudCreditoEstructura.Flag_origen, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@pic_CodEstadoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 100, oESolicitudCreditoEstructura.Uso, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionOtros_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    ''' <summary>
    ''' Elimina un bien cuyas claves coinciden con las enviadas por parámetro, en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnEliminar(ByVal pESolicitudCreditoEstructura As String) As Boolean Implements IBienTx.fblnEliminar

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        Dim prmParameter(1) As DAABRequest.Parameter

        ' Deserealiza la Entidad
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos clave del objeto para poder eliminarlos
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Bien_del"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnEliminar", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function


    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 04/05/2012
    ''' </remarks>
    Public Function fblnModificarRRPPBienContrato(ByVal pESolicitudCredito As String) As Boolean Implements IBienTx.fblnModificarRRPPBienContrato

        Dim oESolicitudCredito As New ESolicitudcredito
        Dim prmParameter(12) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_FechaProbableFinObra", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaProbableFinObra), ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_FechaRealFinObra", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaRealFinObra), ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaInscripcionMunicipal", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaInscripcionMunicipal), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_FechaEnvioNotaria", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaEnvioNotaria), ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_FechaPropiedad", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaPropiedad), ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_FechaInscripcionRegistral", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaInscripcionRegistral), ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_OficinaRegistral", DbType.String, 40, oESolicitudCredito.OficinaRegistral, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 10, oESolicitudCredito.CodigoNotaria, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_CodEstadoInscripcionRRPP", DbType.String, 10, oESolicitudCredito.CodEstadoInscripcionRrPp, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_CodEstadoMunicipal", DbType.String, 10, oESolicitudCredito.CodEstadoMunicipal, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_CodEstadoTransferencia", DbType.String, 10, oESolicitudCredito.CodEstadoTransferencia, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_ObservacionContrato", DbType.String, 500, oESolicitudCredito.ObservacionContrato, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_CONTRATO_BIEN_INMUEBLE_RRPP_UPD"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 04/05/2012
    ''' </remarks>
    Public Function fblnDehabilitarBien(ByVal CodSolicitudCredito As String, ByVal SecFinanciamiento As String, ByVal ComentarioBaja As String) As Boolean Implements IBienTx.fblnDehabilitarBien

        Dim oESolicitudCredito As New ESolicitudcredito
        Dim prmParameter(2) As DAABRequest.Parameter

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_secFinanciamiento", DbType.Int32, 8, SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_comentario_baja", DbType.String, 500, ComentarioBaja, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_BIEN_DESACTIVAR_UPD"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Insertar Documentos
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 12/10/2012
    ''' </remarks>
    Public Function ContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean Implements IBienTx.ContratoDocumentoIns
        Dim blnResultado As Boolean
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim prmParameter(9) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_Numerocontrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int32, 8, oEGccContratodocumento.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_EstadoDocumento", DbType.Int32, 8, oEGccContratodocumento.EstadoDocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEGccContratodocumento.Nombrearchivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Adjunto", DbType.String, 255, oEGccContratodocumento.Adjunto, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 250, oEGccContratodocumento.Observaciones, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_EstadoDocContrato", DbType.Int32, 8, oEGccContratodocumento.EstadoDocContrato, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pii_EstadoDocBien", DbType.Int32, 8, oEGccContratodocumento.EstadoDocBien, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccContratodocumento.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratodocumento.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_BienContratoDocumento_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoChecklistUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    ''' <summary>
    ''' Modificar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializada de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function ModificarBienContratoDocumento(ByVal pEBienContratoDocumento As String) As Boolean Implements IBienTx.ModificarBienContratoDocumento

        Dim oEContratoDocumento As New EGcc_contratodocumento
        Dim prmParameter(9) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEBienContratoDocumento)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigoContrato", DbType.String, 8, oEContratoDocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEContratoDocumento.Codigodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEContratoDocumento.Nombrearchivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oEContratoDocumento.Adjunto, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oEContratoDocumento.Observaciones, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_EstadoDocumento", DbType.Int16, 0, oEContratoDocumento.EstadoDocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_EstadoDocContrato", DbType.Int16, 0, oEContratoDocumento.EstadoDocContrato, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_EstadoDocBien", DbType.Int16, 0, oEContratoDocumento.EstadoDocBien, ParameterDirection.Input)

        'Auditoria
        prmParameter(8) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEContratoDocumento.Audestadologico, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEContratoDocumento.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_BienContratoDocumento_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarBienContratoDocumento", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function


    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pEInafectacion">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnInsertarInafectacion(ByVal pEInafectacion As String) As Boolean Implements IBienTx.fblnInsertarInafectacion

        Dim oEInafectacion As New ESolicitudcreditoestructuracarac
        Dim prmParameter(9) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEInafectacion = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pEInafectacion)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEInafectacion.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoBien", DbType.String, 4, oEInafectacion.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Periodo", DbType.Int16, 0, oEInafectacion.Periodo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FechaEnvioCarta", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FecEnvioCarta), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_FechaRecepcionDocumentos", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FecRecepcionDocumento), ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_FechaPresentacionSAT", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FecPresentacionSat), ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_FechaNotificacion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FechaNotificacion), ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_NroResolucion", DbType.String, 20, oEInafectacion.NroResolucion, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_EstadoResolucion", DbType.String, 40, oEInafectacion.EstadoResolucion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Estado", DbType.String, 10, oEInafectacion.EstadoInafectacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Bien_Inafectacion_Ins"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pEInafectacion">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnModificarInafectacion(ByVal pEInafectacion As String) As Boolean Implements IBienTx.fblnModificarInafectacion

        Dim oEInafectacion As New ESolicitudcreditoestructuracarac
        Dim prmParameter(10) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEInafectacion = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pEInafectacion)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEInafectacion.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoBien", DbType.String, 4, oEInafectacion.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Periodo", DbType.Int16, 0, oEInafectacion.Periodo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FechaEnvioCarta", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FecEnvioCarta), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_FechaRecepcionDocumentos", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FecRecepcionDocumento), ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_FechaPresentacionSAT", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FecPresentacionSat), ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_FechaNotificacion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEInafectacion.FechaNotificacion), ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_NroResolucion", DbType.String, 20, oEInafectacion.NroResolucion, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_EstadoResolucion", DbType.String, 40, oEInafectacion.EstadoResolucion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Estado", DbType.String, 10, oEInafectacion.EstadoInafectacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_codigoInafectacion", DbType.Int16, 0, oEInafectacion.CodInafectacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Bien_Inafectacion_Upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pEInafectacion">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnInsertarInscripcionMunicipal(ByVal pEInscripcionMunicipal As String) As Boolean Implements IBienTx.fblnInsertarInscripcionMunicipal

        Dim oEInscripcionMunicipal As New ESolicitudcreditoestructuracarac
        Dim prmParameter(5) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEInscripcionMunicipal = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pEInscripcionMunicipal)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEInscripcionMunicipal.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoBien", DbType.String, 4, oEInscripcionMunicipal.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@Acto", DbType.String, 40, oEInscripcionMunicipal.Acto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@AsientoRegistral", DbType.String, 40, oEInscripcionMunicipal.AsientoRegistral, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@PartidaRegistral", DbType.String, 40, oEInscripcionMunicipal.PartidaRegistral, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Estado", DbType.String, 10, oEInscripcionMunicipal.EstadoInscripcion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_BIEN_INSCRIPCION_MUNICIPAL_INS"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.    
    ''' </summary>
    ''' <param name="pEInafectacion">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 18/10/2012
    ''' </remarks>
    Public Function fblnModificarInscripcionMunicipal(ByVal pEInscripcionMunicipal As String) As Boolean Implements IBienTx.fblnModificarInscripcionMunicipal

        Dim oEInscripcionMunicipal As New ESolicitudcreditoestructuracarac
        Dim prmParameter(6) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEInscripcionMunicipal = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pEInscripcionMunicipal)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEInscripcionMunicipal.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoBien", DbType.String, 4, oEInscripcionMunicipal.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@Acto", DbType.String, 40, oEInscripcionMunicipal.Acto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@AsientoRegistral", DbType.String, 40, oEInscripcionMunicipal.AsientoRegistral, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@PartidaRegistral", DbType.String, 40, oEInscripcionMunicipal.PartidaRegistral, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Estado", DbType.String, 10, oEInscripcionMunicipal.EstadoInscripcion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_codInscripcionMunicipal", DbType.String, 10, oEInscripcionMunicipal.CodigoInscripcion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_BIEN_INSCRIPCION_MUNICIPAL_UPD"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBien", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DBienNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("584A278D-F4DC-4b03-B126-77743F98F039") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DBienNTx")> _
Public Class DBienNTx
    Inherits ServicedComponent
    Implements IBienNTx


#Region "constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DBienNTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Obtiene los valores de un registro de la tabla T_CLIENTE
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fobjLeer(ByVal pESolicitudCreditoEstructura As String) As String Implements IBienNTx.fobjLeer
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim odtbListadoBien As DataTable

        Dim prmParameter(0) As DAABRequest.Parameter

        ' Deserealiza la Entidad
        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pESolicitudCreditoEstructura)

        ' Campos para la elaboración de la consulta
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oESolicitudCreditoEstructura.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(0) = New DAABRequest.Parameter("@pis_Secfinanciamiento", DbType.Int16, 0, oESolicitudCreditoEstructura.Secfinanciamiento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_ret"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjLeer", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de bienes asociados a un proveedor hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function Lista(ByVal pPageSize As Integer, _
                          ByVal pCurrentPage As Integer, _
                          ByVal pSortColumn As String, _
                          ByVal pSortOrder As String, _
                          ByVal pCodsolicitudcredito As String, _
                          ByVal pCodProveedor As String) As String Implements IBienNTx.Lista
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodProveedor", DbType.String, 4, pCodProveedor, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de bienes hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 14/06/2012
    ''' </remarks>
    Public Function ListadoBien(ByVal pPageSize As Integer, _
                          ByVal pCurrentPage As Integer, _
                          ByVal pSortColumn As String, _
                          ByVal pSortOrder As String, _
                          ByVal pstrSolicitudCreditoEstructura As String) As String Implements IBienNTx.ListadoBien
        Dim odtbListadoBien As DataTable
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(11) As DAABRequest.Parameter


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            ' Deserealiza la Entidad
            oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pstrSolicitudCreditoEstructura)

            ' Campos para la elaboración de la consulta
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8000, oESolicitudCreditoEstructura.NumeroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 8000, oESolicitudCreditoEstructura.CodUnico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, oESolicitudCreditoEstructura.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_TipoRubroFinanciamiento", DbType.String, 8000, oESolicitudCreditoEstructura.Tiporubrofinanciamiento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 8000, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_CodigoEstadoBien", DbType.String, 8000, oESolicitudCreditoEstructura.Codigoestadobien, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_Departamento", DbType.String, 8000, oESolicitudCreditoEstructura.Departamento, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_FechaTransferencia", DbType.String, 8000, oESolicitudCreditoEstructura.FechaTransferencia, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionBien_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoBien", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve una lista de contratos (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/09/2012
    ''' </remarks>
    Public Function ListadoContratoBien(ByVal pPageSize As Integer, _
                          ByVal pCurrentPage As Integer, _
                          ByVal pSortColumn As String, _
                          ByVal pSortOrder As String, _
                          ByVal pstrSolicitudCreditoEstructura As String) As String Implements IBienNTx.ListadoContratoBien
        Dim odtbListadoBien As DataTable
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(12) As DAABRequest.Parameter


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            ' Deserealiza la Entidad
            oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pstrSolicitudCreditoEstructura)

            ' Campos para la elaboración de la consulta
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8000, oESolicitudCreditoEstructura.NumeroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 8000, oESolicitudCreditoEstructura.CodUnico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, oESolicitudCreditoEstructura.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_TipoRubroFinanciamiento", DbType.String, 8000, oESolicitudCreditoEstructura.Tiporubrofinanciamiento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 8000, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_EstadoContrato", DbType.String, 8000, oESolicitudCreditoEstructura.EstadoContrato, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_TipoDocumento", DbType.String, 8000, oESolicitudCreditoEstructura.TipoDocumento, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 8000, oESolicitudCreditoEstructura.NumeroDocumento, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_Kardex", DbType.String, 8000, oESolicitudCreditoEstructura.Kardex, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionBien_ListaContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoBien", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los datos del bien
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 15/06/2012 
    ''' </remarks>
    Public Function ObtenerBien(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerBien

        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionBien_get"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Codsolicitudcredito = Reader("CodSolicitudCredito").ToString()
                        .EstadoContrato = Reader("EstadoContrato").ToString()
                        .Moneda = Reader("Moneda").ToString()
                        .DescripcionBien = Reader("DescripcionBien").ToString()
                        .RazonSocial = Reader("RazonSocial").ToString()
                        .Tiporubrofinanciamiento = Reader("Tiporubrofinanciamiento").ToString()
                        .ClasificacionBien = Reader("ClasificacionBien").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        'CFunciones.CheckDecimal(Reader("ValorBien").ToString().Replace(",", "")) / 100
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .CodUnico = Reader("CodUnico").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Uso = Reader("Uso").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .PartidaRegistral = Reader("PartidaRegistral").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .Comentario = Reader("Comentario").ToString()
                        .Placa = Reader("Placa").ToString()
                        .PlacaAntigua = Reader("PlacaAntigua").ToString()
                        .Anio = CFunciones.CheckInt(Reader("Anio").ToString())
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Color = Reader("Color").ToString()
                        .Carroceria = Reader("Carroceria").ToString()
                        .Medidas = Reader("Medidas").ToString()
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    ''' <summary>
    ''' Obtiene lista de proveedores asociado al bien
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 15/06/2012 
    ''' </remarks>
    Public Function ListadoBienProveedor(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ListadoBienProveedor
        Dim odtbListadoBien As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            ' Campos para la elaboración de la consulta
            prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionBienpProveedor_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoBienProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' ObtenerDatosBienContrato
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Código del contrato</param>    
    ''' <param name="pCodBien">Código del bien</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 27/09/2012 
    ''' </remarks>
    Public Function ObtenerDatosBienContrato(ByVal pstrNumeroContrato As String) As String Implements IBienNTx.ObtenerDatosBienContrato
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionContratoBien_get"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Codsolicitudcredito = Reader("CodSolicitudCredito").ToString()
                        .Tiporubrofinanciamiento = Reader("Tiporubrofinanciamiento").ToString()
                        .ClasificacionBien = Reader("ClasificacionBien").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .TipoBien = Reader("TipoBien").ToString()
                        .Moneda = Reader("Moneda").ToString()
                        .EstadoContrato = Reader("EstadoContrato").ToString()
                        .ObservacionContrato = Reader("ObservacionContrato").ToString()
                        .FechaProbableFinObra = Reader("FechaProbableFinObra").ToString()
                        .FechaRealFinObra = Reader("FechaRealFinObra").ToString()
                        .FechaInscripcionMunicipal = Reader("FechaInscripcionMunicipal").ToString()
                        .CodEstadoMunicipal = Reader("CodEstadoMunicipal").ToString()
                        .FechaEnvioNotaria = Reader("FechaEnvioNotaria").ToString()
                        .FechaPropiedad = Reader("FechaPropiedad").ToString()
                        .FechaInscripcionRegistral = Reader("FechaInscripcionRegistral").ToString()
                        .CodEstadoInscripcionRrPp = Reader("CodEstadoInscripcionRRPP").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .CodigoNotaria = Reader("CodigoNotaria").ToString()
                        .CodEstadoTransferencia = Reader("CodEstadoTransferencia").ToString()
                        .Banca = Reader("Banca").ToString()
                        .EjecutivoBanca = Reader("EjecutivoBanca").ToString()
                        .CodUnico = Reader("CodUnico").ToString()
                        .RazonSocial = Reader("RazonSocial").ToString()
                        .TipoDocumento = Reader("TipoDocumento").ToString()
                        .NumeroDocumento = Reader("NumeroDocumento").ToString()
                        .Kardex = Reader("Kardex").ToString()
                        .Precioventa = Reader("Precioventa")
                        '.CodigoSubTipoContrato = Reader("CodigoSubTipoContrato").ToString()
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ListaBienContratoInmuebles(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IBienNTx.ListaBienContratoInmuebles
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_EstadoBienLogico", DbType.String, 10, pCodEstadoLogico, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_INMUEBLE_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ListaBienContratoInscripcionResgistral(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pStrEBienInscripcion As String) As String Implements IBienNTx.ListaBienContratoInscripcionResgistral
        Dim odtbListadoBien As DataTable
        Dim prmParameter(22) As DAABRequest.Parameter
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pStrEBienInscripcion)

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 10, oESolicitudCreditoEstructura.NumeroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 10, oESolicitudCreditoEstructura.CodUnico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 50, oESolicitudCreditoEstructura.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_TipoDocumento", DbType.String, 10, oESolicitudCreditoEstructura.TipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 10, oESolicitudCreditoEstructura.NumeroDocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_CodigoClasificacion", DbType.String, 10, oESolicitudCreditoEstructura.Tiporubrofinanciamiento, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_CodigoTipoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_CodigoEstadoContrato", DbType.String, 10, oESolicitudCreditoEstructura.EstadoContrato, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_NroKardex", DbType.String, 20, oESolicitudCreditoEstructura.Kardex, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pic_FechaProbableFinObra", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaProbableFinObra), ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_FechaRealFinObra", DbType.String, 10, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaRealFinObra), ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pic_CodEstadoMunicipal", DbType.String, 10, oESolicitudCreditoEstructura.CodEstadoMunicipal, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@pic_FechaInsMunicipal", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionMunicipal), ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 10, oESolicitudCreditoEstructura.CodigoNotaria, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@pic_FechaEnvioNotaria", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEnvioNotaria), ParameterDirection.Input)
            prmParameter(19) = New DAABRequest.Parameter("@pic_CodEstadoRegistral", DbType.String, 20, oESolicitudCreditoEstructura.CodEstadoInscripcionRrPp, ParameterDirection.Input)
            prmParameter(20) = New DAABRequest.Parameter("@pic_FechaInsRegistral", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionRegistral), ParameterDirection.Input)
            prmParameter(21) = New DAABRequest.Parameter("@pic_OficinaRegistral", DbType.String, 50, oESolicitudCreditoEstructura.OficinaRegistral, ParameterDirection.Input)
            prmParameter(22) = New DAABRequest.Parameter("@pic_FechaPropiedad", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaPropiedad), ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_INSCRIPCION_REGISTRAL_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ListaBienContratoInscripcionResgistralReporte(ByVal pStrEBienInscripcion As String) As String Implements IBienNTx.ListaBienContratoInscripcionResgistralReporte
        Dim odtbListadoBien As DataTable
        Dim prmParameter(18) As DAABRequest.Parameter
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        oESolicitudCreditoEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pStrEBienInscripcion)

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 10, oESolicitudCreditoEstructura.NumeroContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 10, oESolicitudCreditoEstructura.CodUnico, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 50, oESolicitudCreditoEstructura.RazonSocial, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_TipoDocumento", DbType.String, 10, oESolicitudCreditoEstructura.TipoDocumento, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 10, oESolicitudCreditoEstructura.NumeroDocumento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodigoClasificacion", DbType.String, 10, oESolicitudCreditoEstructura.Tiporubrofinanciamiento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_CodigoTipoBien", DbType.String, 10, oESolicitudCreditoEstructura.Codigotipobien, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_CodigoEstadoContrato", DbType.String, 10, oESolicitudCreditoEstructura.EstadoContrato, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_NroKardex", DbType.String, 20, oESolicitudCreditoEstructura.Kardex, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_FechaProbableFinObra", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaProbableFinObra), ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_FechaRealFinObra", DbType.String, 10, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaRealFinObra), ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_CodEstadoMunicipal", DbType.String, 10, oESolicitudCreditoEstructura.CodEstadoMunicipal, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_FechaInsMunicipal", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionMunicipal), ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 10, oESolicitudCreditoEstructura.CodigoNotaria, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_FechaEnvioNotaria", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaEnvioNotaria), ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pic_CodEstadoRegistral", DbType.String, 20, oESolicitudCreditoEstructura.CodEstadoInscripcionRrPp, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@pic_FechaInsRegistral", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaInscripcionRegistral), ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@pic_OficinaRegistral", DbType.String, 50, oESolicitudCreditoEstructura.OficinaRegistral, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@pic_FechaPropiedad", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCreditoEstructura.FechaPropiedad), ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_INSCRIPCION_REGISTRAL_RPT"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ListaBienContratoVehiculos(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IBienNTx.ListaBienContratoVehiculos
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_EstadoBienLogico", DbType.String, 10, pCodEstadoLogico, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_VEHICULO_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ListadoBienContratoMaquinariayOtros(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IBienNTx.ListadoBienContratoMaquinariayOtros
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_EstadoBienLogico", DbType.String, 10, pCodEstadoLogico, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_MAQUINARIA_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ListadoBienContratoSistemas(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, ByVal pSortColumn As String, ByVal pSortOrder As String, ByVal pCodsolicitudcredito As String, ByVal pCodEstadoLogico As String) As String Implements IBienNTx.ListadoBienContratoSistemasyOtros
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_EstadoBienLogico", DbType.String, 10, pCodEstadoLogico, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_SISTEMAS_OTROS_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ObtenerDatosInmuebles(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosInmuebles
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_INMUEBLE_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .Uso = Reader("Uso").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Codigoestadobien = Reader("CodigoEstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .ComentarioBaja = Reader("comentariobaja")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .FechaProbableFinObra = Reader("FechaProbableFinObra").ToString()
                        .FechaRealFinObra = Reader("FechaRealFinObra").ToString()
                        .FechaInscripcionMunicipal = Reader("FechaInscripcionMunicipal").ToString()
                        .CodEstadoMunicipal = Reader("CodEstadoMunicipal").ToString()
                        .FechaEnvioNotaria = Reader("FechaEnvioNotaria").ToString()
                        .FechaPropiedad = Reader("FechaPropiedad").ToString()
                        .FechaInscripcionRegistral = Reader("FechaInscripcionRegistral").ToString()
                        .CodEstadoInscripcionRrPp = Reader("CodEstadoInscripcionRRPP").ToString()
                        .CodigoNotaria = Reader("CodigoNotaria").ToString()
                        .CodEstadoTransferencia = Reader("CodEstadoTransferencia").ToString()
                        .FechaBaja = Reader("FechaBaja")
                        .PartidaRegistral = Reader("PartidaRegistral").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .CodEstadoInscripcionRrPp = Reader("CodEstadoInscripcionRRPP").ToString()
                        .CodigoPredio = Reader("CodigoPredio").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                        .Municipalidad = Reader("Municipalidad")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerDatosMaquinarias(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosMaquinarias
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_MAQUINARIA_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Uso = Reader("Uso").ToString()
                        .Placa = Reader("Placa").ToString()
                        .PlacaAntigua = Reader("PlacaAnterior").ToString()
                        .Anio = CFunciones.CheckInt(Reader("anio").ToString())
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Color = Reader("Color").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .CodTipoCarroceria = Reader("codTipoCarroceria").ToString()
                        .Medidas = Reader("Medidas").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Codigoestadobien = Reader("CodigoEstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .FechaBaja = Reader("FechaBaja")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .ComentarioBaja = Reader("comentariobaja")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerDatosSistemas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosSistemas
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_SISTEMAS_OTROS_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Uso = Reader("Uso").ToString()
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .PartidaRegistral = Reader("PartidaRegistral").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .Color = Reader("Color").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("Cantidad").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .TipoBien = Reader("TipoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Codigoestadobien = Reader("CodigoEstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaBaja = Reader("FechaBaja")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .ComentarioBaja = Reader("comentariobaja")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerDatosSistemasConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosSistemasConsulta
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_SISTEMAS_OTROS_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .DepartamentoNombre = Reader("DepartamentoNombre").ToString()
                        .ProvinciaNombre = Reader("ProvinciaNombre").ToString()
                        .DistritoNombre = Reader("DistritoNombre").ToString()
                        .Uso = Reader("Uso").ToString()
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .PartidaRegistral = Reader("PartidaRegistral").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .Color = Reader("Color").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("Cantidad").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .TipoBien = Reader("TipoBien").ToString()
                        .EstadoBien = Reader("EstadoBienT").ToString()
                        .Codigoestadobien = Reader("EstadoBienN").ToString()
                        .Monedabien = Reader("MonedaBienNombre").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaBaja = Reader("FechaBaja")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .ComentarioBaja = Reader("comentariobaja")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function
    Public Function ObtenerDatosVehiculo(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosVehiculo
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_VEHICULO_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Uso = Reader("Uso").ToString()
                        .CodTransmision = Reader("codTransmision").ToString()
                        .CodTraccion = Reader("codTraccion").ToString()
                        .CodTipoMotor = Reader("CodTipoMotor").ToString()
                        .CodPotenciaMotor = Reader("PotenciaMotor").ToString()
                        .CodCombustible = Reader("Cumbustible").ToString()
                        .Cilindros = Reader("Cilindros").ToString()
                        .Longitud = Reader("Longitud").ToString()
                        .PesoNeto = Reader("PesoNeto").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Pasajeros = CFunciones.CheckInt(Reader("Pasajeros").ToString())
                        .CargaUtil = Reader("CargaUtil").ToString()
                        .PesoBruto = Reader("PesoBruto").ToString()
                        .Asientos = CFunciones.CheckInt(Reader("Asientos").ToString())
                        .Ejes = CFunciones.CheckInt(Reader("Ejes").ToString())
                        .Ruedas = CFunciones.CheckInt(Reader("Ruedas").ToString())
                        .Ancho = Reader("Ancho").ToString()
                        .Puertas = CFunciones.CheckInt(Reader("Puertas").ToString())
                        .Alto = Reader("Alto").ToString()
                        .FormulaRodante = Reader("FormulaRodante").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .Codigoestadobien = Reader("CodEstadoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        'CFunciones.CheckDecimal(Reader("ValorBien").ToString().Replace(",", "")) / 100
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .FechaEmisionTarjeta = Reader("FechaEmisionTarjetaPropiedad")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .PlacaAntigua = Reader("PlacaAnterior").ToString()
                        .Placa = Reader("Placa").ToString()
                        .Anio = CFunciones.CheckInt(Reader("anio").ToString())
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .PlacaAntigua = Reader("PlacaAnterior").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Medidas = Reader("Medidas").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Color = Reader("Color").ToString()
                        .CodClase = Reader("codClase").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .Cilindraje = Reader("cilindraje").ToString()
                        .Carroceria = Reader("Carroceria").ToString()
                        .Clase = Reader("Clase").ToString()
                        .FechaEnvioSat = Reader("FechaEnvioSAT")
                        .FechaInscripcionMunicipal = Reader("FechaInscripcionMunicipal")
                        .FechaEnvioNotaria = Reader("FechaEnvioNotaria")
                        .FechaEnvioRrPp = Reader("FechaEnvioRRPP")
                        .FechaPropiedad = Reader("FechaPropiedad")
                        .FechaInscripcionRegistral = Reader("FechaInscripcionRegistral")
                        .CodInafectacion = Reader("CodInafectacion").ToString()
                        .FechaBaja = Reader("FechaBaja")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .CodEstadoMunicipal = Reader("CodEstadoMunicipal").ToString()
                        .CodEstadoInscripcionRrPp = Reader("CodEstadoInscripcionRRPP").ToString()
                        .ComentarioBaja = Reader("comentariobaja").ToString()
                        .CodPagoImpuesto = Reader("CodPagoImpuestos").ToString()
                        .FlagInafectacion = Reader("FlagInafectacion").ToString()
                        .FlagPagoImpuestos = Reader("FlagPagoImpuestos").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerMaxFilas(ByVal pstrNumeroContrato As String, ByVal pstrEstado As String) As String Implements IBienNTx.ObtenerMaxFilas
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodEstadoBien", DbType.String, 5, pstrEstado, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_SEL_Max"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Secfinanciamiento = Reader("SecFinanciamiento").ToString()
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ListaBienContratoDocumento(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String Implements IBienNTx.ListaBienContratoDocumento
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pii_secfinanciamiento", DbType.Int32, 8, Convert.ToInt32(pCodbien), ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienContratoDocumento_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el BienContratoDocumento de un Contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 17/10/2012 
    ''' </remarks>
    Public Function ObtenerBienContratoDocumento(ByVal pEBienContratoDocumento As String) As String Implements IBienNTx.BienContratoDocumento

        Dim odtbListadoBienContratoDoc As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim oEContratoBienDocumento As New EGcc_contratodocumento
        oEContratoBienDocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEBienContratoDocumento)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigoContrato", DbType.String, 8, oEContratoBienDocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEContratoBienDocumento.Codigodocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienContratoDocumento_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBienContratoDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCotizacionDocumento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbListadoBienContratoDoc Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBienContratoDoc)
        End If
    End Function

    ''' <summary>
    ''' Obtiene los datos del bien
    ''' </summary>
    ''' <param name="pEBienSolicitudEstructura">Código del contrato</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 15/06/2012 
    ''' </remarks>
    Public Function ValidarDatosVehiculo(ByVal pEBienSolicitudEstructura As String) As String Implements IBienNTx.ValidarDatosVehiculo

        Dim odtbListadoBien As DataTable

        Dim prmParameter(3) As DAABRequest.Parameter
        Dim oESolicitudEstructura As New ESolicitudcreditoestructuracarac
        oESolicitudEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pEBienSolicitudEstructura)
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            'Deserealiza la Entidad
            prmParameter(0) = New DAABRequest.Parameter("@NroSerie", DbType.String, 20, oESolicitudEstructura.Nroserie, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@NroMotor", DbType.String, 20, oESolicitudEstructura.Nromotor, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@Placa", DbType.String, 20, oESolicitudEstructura.Placa, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@PlacaAnterior", DbType.String, 20, oESolicitudEstructura.Placaanterior, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_GESTIONBIEN_VALIDACAMPOS_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Obtiene los datos de la inafectación
    ''' </summary>
    ''' <param name="pCodSolitudCredito">Código del contrato</param>
    ''' <param name="pCodBien">Código del contrato</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 18/10/2012 
    ''' </remarks>
    Public Function ListaDatosInafectación(ByVal pCodSolitudCredito As String, ByVal pCodBien As String) As String Implements IBienNTx.ListaDatosInafectación

        Dim odtbListadoBien As DataTable

        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            'Deserealiza la Entidad
            prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodSolitudCredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoBien", DbType.String, 4, pCodBien, ParameterDirection.Input)



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bien_Inafectacion_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista Inafectación", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ListaBienInscripcionMunicipal(ByVal pPageSize As Integer, ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, ByVal pSortOrder As String, _
                                               ByVal pCodsolicitudcredito As String, ByVal pCodbien As String) As String Implements IBienNTx.ListaBienInscripcionMunicipal
        Dim odtbListadoBien As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, pCodsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pii_secfinanciamiento", DbType.Int32, 8, Convert.ToInt32(pCodbien), ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_INSCRIPCION_MUNICIPAL_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' Obtiene los datos del bien
    ''' </summary>
    ''' <param name="pEBienSolicitudEstructura">Código del contrato</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - AEP
    ''' Fecha de Creacion : 23/10/2012 
    ''' </remarks>
    Public Function ValidarDatosPartida(ByVal pEBienSolicitudEstructura As String, ByVal Tipo As String) As String Implements IBienNTx.ValidarDatosPartida

        Dim odtbListadoBien As DataTable

        Dim prmParameter(5) As DAABRequest.Parameter
        Dim oESolicitudEstructura As New ESolicitudcreditoestructuracarac
        oESolicitudEstructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuracarac)(pEBienSolicitudEstructura)
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            'Deserealiza la Entidad
            prmParameter(0) = New DAABRequest.Parameter("@NumeroContrato", DbType.String, 20, oESolicitudEstructura.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@Partida", DbType.String, 20, oESolicitudEstructura.PartidaRegistral, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@Asiento", DbType.String, 20, oESolicitudEstructura.AsientoRegistral, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@Acto", DbType.String, 20, oESolicitudEstructura.Acto, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@Tipo", DbType.Int32, Tipo, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@CodigoAsiento", DbType.Int32, 8, Convert.ToInt32(oESolicitudEstructura.CodigoInscripcion), ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_GESTIONBIEN_VALIDACAMPOSPARTIDA_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Lista", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

    End Function

    Public Function ObtenerDatosBienContratoConsulta(ByVal pstrNumeroContrato As String) As String Implements IBienNTx.ObtenerDatosBienContratoConsulta
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ConsultasContratoBien_get"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Codsolicitudcredito = Reader("CodSolicitudCredito").ToString()
                        .Tiporubrofinanciamiento = Reader("Tiporubrofinanciamiento").ToString()
                        .ClasificacionBien = Reader("ClasificacionBien").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .TipoBien = Reader("TipoBien").ToString()
                        .Moneda = Reader("Moneda").ToString()
                        .EstadoContrato = Reader("EstadoContrato").ToString()
                        .ObservacionContrato = Reader("ObservacionContrato").ToString()
                        .FechaProbableFinObra = Reader("FechaProbableFinObra").ToString()
                        .FechaRealFinObra = Reader("FechaRealFinObra").ToString()
                        .FechaInscripcionMunicipal = Reader("FechaInscripcionMunicipal").ToString()
                        .CodEstadoMunicipal = Reader("EstadoMunicipal").ToString()
                        .FechaEnvioNotaria = Reader("FechaEnvioNotaria").ToString()
                        .FechaPropiedad = Reader("FechaPropiedad").ToString()
                        .FechaInscripcionRegistral = Reader("FechaInscripcionRegistral").ToString()
                        .CodEstadoInscripcionRrPp = Reader("EstadoInscripcionRRPP").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .CodigoNotaria = Reader("EstadoNotaria").ToString()
                        .CodEstadoTransferencia = Reader("EstadoTransferencia").ToString()
                        .Banca = Reader("Banca").ToString()
                        .EjecutivoBanca = Reader("EjecutivoBanca").ToString()
                        .CodUnico = Reader("CodUnico").ToString()
                        .RazonSocial = Reader("RazonSocial").ToString()
                        .TipoDocumento = Reader("TipoDocumento").ToString()
                        .NumeroDocumento = Reader("NumeroDocumento").ToString()
                        .Kardex = Reader("Kardex").ToString()
                        .Precioventa = Reader("Precioventa")
                        '.CodigoSubTipoContrato = Reader("CodigoSubTipoContrato").ToString()
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerDatosInmueblesConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosInmueblesConsulta
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CONSULTA_BIEN_INMUEBLE_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .Uso = Reader("Uso").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("TipoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Codigoestadobien = Reader("CodigoEstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .ComentarioBaja = Reader("comentariobaja")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .FechaProbableFinObra = Reader("FechaProbableFinObra").ToString()
                        .FechaRealFinObra = Reader("FechaRealFinObra").ToString()
                        .FechaInscripcionMunicipal = Reader("FechaInscripcionMunicipal").ToString()
                        .CodEstadoMunicipal = Reader("EstadoMunicipal").ToString()
                        .FechaEnvioNotaria = Reader("FechaEnvioNotaria").ToString()
                        .FechaPropiedad = Reader("FechaPropiedad").ToString()
                        .FechaInscripcionRegistral = Reader("FechaInscripcionRegistral").ToString()
                        .CodEstadoInscripcionRrPp = Reader("CodEstadoInscripcionRRPP").ToString()
                        .CodigoNotaria = Reader("Notaria").ToString()
                        .CodEstadoTransferencia = Reader("EstadoTransferencia").ToString()
                        .FechaBaja = Reader("FechaBaja")
                        .PartidaRegistral = Reader("PartidaRegistral").ToString()
                        .OficinaRegistral = Reader("OficinaRegistral").ToString()
                        .CodEstadoInscripcionRrPp = Reader("EstadoInscripcionRRPP").ToString()
                        .CodigoPredio = Reader("CodigoPredio").ToString()
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerDatosMaquinariasConsultas(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosMaquinariasConsulta
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CONSULTA_BIEN_MAQUINARIA_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Uso = Reader("Uso").ToString()
                        .Placa = Reader("Placa").ToString()
                        .PlacaAntigua = Reader("PlacaAnterior").ToString()
                        .Anio = CFunciones.CheckInt(Reader("anio").ToString())
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Color = Reader("Color").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .CodTipoCarroceria = Reader("codTipoCarroceria").ToString()
                        .Medidas = Reader("Medidas").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("TipoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Codigoestadobien = Reader("CodigoEstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .FechaBaja = Reader("FechaBaja")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .ComentarioBaja = Reader("comentariobaja")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ObtenerDatosVehiculoConsulta(ByVal pstrNumeroContrato As String, ByVal pCodBien As Integer) As String Implements IBienNTx.ObtenerDatosVehiculoConsulta
        Dim oESolicitudCreditoEstructura As ESolicitudcreditoestructura = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinancimiento", DbType.Int16, 0, pCodBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CONSULTA_BIEN_VEHICULO_GET"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oESolicitudCreditoEstructura = New ESolicitudcreditoestructura
                    With oESolicitudCreditoEstructura
                        .Departamento = Reader("Departamento").ToString()
                        .Provincia = Reader("Provincia").ToString()
                        .Distrito = Reader("Distrito").ToString()
                        .Uso = Reader("Uso").ToString()
                        .CodTransmision = Reader("codTransmision").ToString()
                        .CodTraccion = Reader("codTraccion").ToString()
                        .CodTipoMotor = Reader("CodTipoMotor").ToString()
                        .CodPotenciaMotor = Reader("PotenciaMotor").ToString()
                        .CodCombustible = Reader("Cumbustible").ToString()
                        .Cilindros = Reader("Cilindros").ToString()
                        .Longitud = Reader("Longitud").ToString()
                        .PesoNeto = Reader("PesoNeto").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Pasajeros = CFunciones.CheckInt(Reader("Pasajeros").ToString())
                        .CargaUtil = Reader("CargaUtil").ToString()
                        .PesoBruto = Reader("PesoBruto").ToString()
                        .Asientos = CFunciones.CheckInt(Reader("Asientos").ToString())
                        .Ejes = CFunciones.CheckInt(Reader("Ejes").ToString())
                        .Ruedas = CFunciones.CheckInt(Reader("Ruedas").ToString())
                        .Ancho = Reader("Ancho").ToString()
                        .Puertas = CFunciones.CheckInt(Reader("Puertas").ToString())
                        .Alto = Reader("Alto").ToString()
                        .FormulaRodante = Reader("FormulaRodante").ToString()
                        .Cantidadproducto = CFunciones.CheckInt(Reader("CantidadProducto").ToString())
                        .Comentario = Reader("Descripcion").ToString()
                        .Codigotipobien = Reader("CodigoTipoBien").ToString()
                        .Codigoestadobien = Reader("CodEstadoBien").ToString()
                        .EstadoBien = Reader("EstadoBien").ToString()
                        .Monedabien = Reader("MonedaBien").ToString()
                        .MonedabienNombre = Reader("MonedaBienNombre").ToString()
                        '.MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString())
                        .MontoValorBien = CFunciones.CheckDecimal(Reader("ValorBien").ToString().Replace(",", "")) / 100
                        .FechaTransferencia = Reader("FechaTransferencia")
                        .FechaAdquisicion = Reader("FechaAdquisicion")
                        .FechaEmisionTarjeta = Reader("FechaEmisionTarjetaPropiedad")
                        .ObservacionBien = Reader("Observacion").ToString()
                        .PlacaAntigua = Reader("PlacaAnterior").ToString()
                        .Placa = Reader("Placa").ToString()
                        .Anio = CFunciones.CheckInt(Reader("anio").ToString())
                        .NroSerie = Reader("NroSerie").ToString()
                        .NroMotor = Reader("NroMotor").ToString()
                        .PlacaAntigua = Reader("PlacaAnterior").ToString()
                        .Marca = Reader("Marca").ToString()
                        .Medidas = Reader("Medidas").ToString()
                        .Modelo = Reader("Modelo").ToString()
                        .Color = Reader("Color").ToString()
                        .CodClase = Reader("codClase").ToString()
                        .Ubicacion = Reader("Ubicacion").ToString()
                        .Cilindraje = Reader("cilindraje").ToString()
                        .Carroceria = Reader("Carroceria").ToString()
                        .Clase = Reader("Clase").ToString()
                        .FechaEnvioSat = Reader("FechaEnvioSAT")
                        .FechaInscripcionMunicipal = Reader("FechaInscripcionMunicipal")
                        .FechaEnvioNotaria = Reader("FechaEnvioNotaria")
                        .FechaEnvioRrPp = Reader("FechaEnvioRRPP")
                        .FechaPropiedad = Reader("FechaPropiedad")
                        .FechaInscripcionRegistral = Reader("FechaInscripcionRegistral")
                        .CodInafectacion = Reader("CodInafectacion").ToString()
                        .FechaBaja = Reader("FechaBaja")
                        .FechaEliminacion = Reader("fechaeliminacion")
                        .CodEstadoMunicipal = Reader("CodEstadoMunicipal").ToString()
                        .CodEstadoInscripcionRrPp = Reader("CodEstadoInscripcionRRPP").ToString()
                        .ComentarioBaja = Reader("comentariobaja").ToString()
                        .CodPagoImpuesto = Reader("CodPagoImpuestos").ToString()
                        .FlagInafectacion = Reader("FlagInafectacion").ToString()
                        .FlagPagoImpuestos = Reader("FlagPagoImpuestos").ToString()
                        .PrecioTotal = Reader("PrecioTotal")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oESolicitudCreditoEstructura Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)
        End If
    End Function

    Public Function ListarDetalleBienConsulta(ByVal pstrEBien As String) As String Implements IBienNTx.ListarDetalleBienConsulta
        'Variables
        Dim odtbObtener As New DataTable

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim oeSolicitudEsctructura As ESolicitudcreditoestructura = Nothing
        oeSolicitudEsctructura = CFunciones.DeserializeObject(Of ESolicitudcreditoestructura)(pstrEBien)

        Dim prmParameter(8) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8000, oeSolicitudEsctructura.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 8000, oeSolicitudEsctructura.CodUnico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, oeSolicitudEsctructura.RazonSocial, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_TipoRubroFinanciamiento", DbType.String, 8000, oeSolicitudEsctructura.Tiporubrofinanciamiento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodTipoBien", DbType.String, 8000, oeSolicitudEsctructura.Codigotipobien, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_EstadoContrato", DbType.String, 8000, oeSolicitudEsctructura.EstadoContrato, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_TipoDocumento", DbType.String, 8000, oeSolicitudEsctructura.TipoDocumento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 8000, oeSolicitudEsctructura.NumeroDocumento, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_Kardex", DbType.String, 8000, oeSolicitudEsctructura.Kardex, ParameterDirection.Input)


        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_DETALLE_BIEN_CONSULTA_SEL"
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
#End Region


End Class

#End Region
