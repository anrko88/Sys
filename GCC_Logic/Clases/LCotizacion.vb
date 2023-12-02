Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LCotizacionTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("1113AAB6-E454-4e5f-B23B-80B43A79203E") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCotizacionTx")> _
Public Class LCotizacionTx
    Inherits ServicedComponent
    Implements ICotizacionTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LCotizacionTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Ingresa nueva Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarCotizacion(ByVal pECotizacion As String, ByVal pEListCronograma As String) As String Implements ICotizacionTx.InsertarCotizacion
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim objDContactoTx As DContactoTx = Nothing
        Dim objDSubPrestatarioTx As DSubPrestatarioTx = Nothing
        Dim objDCotizacionCronogramaTx As DCotizacionCronogramaTx = Nothing
        Dim strCodCotizacion As String

        Try
            objDCotizacionTx = New DCotizacionTx
            objDContactoTx = New DContactoTx
            objDCotizacionCronogramaTx = New DCotizacionCronogramaTx

            'Instancia ECotizacion
            Dim objECotizacion As EGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

            'Valida SubPrestatario
            Dim strCodSubPrestatario As String = objECotizacion.Codsubprestatario
            Dim objESubPrestatario As New ESubprestatario
            objDSubPrestatarioTx = New DSubPrestatarioTx
            If objECotizacion.Codsubprestatario Is Nothing Then

                'Instancia Clase
                With objESubPrestatario
                    .Codsubprestatario = objECotizacion.Codsubprestatario
                    .Codunico = objECotizacion.CodUnico
                    .Nombresubprestatario = objECotizacion.NombreCliente
                    .Coddocidentificaciontipo = objECotizacion.CodigoTipoDocumento
                    .Numdocidentificacion = objECotizacion.NumeroDocumento
                End With

                'Inserta Nuevo SubPrestatario
                strCodSubPrestatario = objDSubPrestatarioTx.InsertarSubPrestatario(CFunciones.SerializeObject(Of ESubprestatario)(objESubPrestatario))

            End If

            'Ingresa Cotizacion
            If strCodSubPrestatario.Trim().Equals("") Then
                strCodSubPrestatario = Nothing
            End If
            objECotizacion.Codsubprestatario = strCodSubPrestatario
            strCodCotizacion = objDCotizacionTx.InsertarCotizacion(CFunciones.SerializeObject(Of EGcc_cotizacion)(objECotizacion))

            'Ingresa Contacto
            Dim objEGcc_contacto As New EGcc_contacto
            With objEGcc_contacto
                .Codigocotizacion = strCodCotizacion
                .Codsolicitudcredito = Nothing
                .Nombre = objECotizacion.NombreContacto
                .Correo = objECotizacion.CorreoContacto
                .Telefono = Nothing
                .Anexo = Nothing
            End With
            objDContactoTx.InsertarContacto(CFunciones.SerializeObject(Of EGcc_contacto)(objEGcc_contacto))

            'Versionamiento
            Dim objEGcc_cotizacionVersion As New EGcc_cotizacion
            With objEGcc_cotizacionVersion
                .Codigocotizacion = strCodCotizacion
            End With
            objDCotizacionTx.CotizacionVersionamiento(CFunciones.SerializeObject(Of EGcc_cotizacion)(objEGcc_cotizacionVersion))

            'Ingresa Cronograma
            If Not pEListCronograma Is Nothing Then
                If Not pEListCronograma.Trim().Equals("") Then
                    Dim objEListCronograma As ListEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of ListEGcc_cotizacioncronograma)(pEListCronograma)
                    For Each objECronograma In objEListCronograma
                        objECronograma.Codigocotizacion = strCodCotizacion
                        objECronograma.Versioncotizacion = 1
                        objDCotizacionCronogramaTx.InsertarCronograma(CFunciones.SerializeObject(Of EGcc_cotizacioncronograma)(objECronograma))
                    Next
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
            objDContactoTx.Dispose()
            objDContactoTx = Nothing
            objDSubPrestatarioTx.Dispose()
            objDSubPrestatarioTx = Nothing
        End Try

        Return strCodCotizacion
    End Function

    ''' <summary>
    ''' Modifica Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCotizacion(ByVal pECotizacion As String, ByVal pEListCronograma As String) As Boolean Implements ICotizacionTx.ModificarCotizacion
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim objDContactoTx As DContactoTx = Nothing
        Dim objDCotizacionCronogramaTx As DCotizacionCronogramaTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDCotizacionTx = New DCotizacionTx
            objDContactoTx = New DContactoTx
            objDCotizacionCronogramaTx = New DCotizacionCronogramaTx

            'Actualizar Cotizacion
            blnResultado = objDCotizacionTx.ModificarCotizacion(pECotizacion)

            'Modifica Contacto
            Dim objECotizacion As EGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)
            Dim objEGcc_contacto As New EGcc_contacto
            With objEGcc_contacto
                .Codigocontacto = objECotizacion.CodigoContacto
                .Codigocotizacion = objECotizacion.Codigocotizacion
                .Codsolicitudcredito = Nothing
                .Nombre = objECotizacion.NombreContacto
                .Correo = objECotizacion.CorreoContacto
                .Telefono = Nothing
                .Anexo = Nothing
            End With
            objDContactoTx.ModificarContacto(CFunciones.SerializeObject(Of EGcc_contacto)(objEGcc_contacto))

            'Versionamiento
            If objECotizacion.AplicaVersionamiento = 1 Then
                Dim objEGcc_cotizacionVersion As New EGcc_cotizacion
                With objEGcc_cotizacionVersion
                    .Codigocotizacion = objECotizacion.Codigocotizacion
                End With
                objDCotizacionTx.CotizacionVersionamiento(CFunciones.SerializeObject(Of EGcc_cotizacion)(objEGcc_cotizacionVersion))
            End If

            'Gestion Cronograma
            Dim oECronograma As New EGcc_cotizacioncronograma
            oECronograma.Codigocotizacion = objECotizacion.Codigocotizacion
            oECronograma.Versioncotizacion = objECotizacion.Versioncotizacion

            If Not pEListCronograma Is Nothing Then
                If Not pEListCronograma.Trim().Equals("") Then
                    Dim objEListCronograma As ListEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of ListEGcc_cotizacioncronograma)(pEListCronograma)

                    'Elimina Cronograma Actual
                    If objECotizacion.AplicaVersionamiento = 0 Then
                        objDCotizacionCronogramaTx.EliminarCronograma(CFunciones.SerializeObject(Of EGcc_cotizacioncronograma)(oECronograma))
                    End If

                    For Each objECronograma In objEListCronograma
                        If objECotizacion.AplicaVersionamiento = 1 Then
                            objECronograma.Versioncotizacion = CFunciones.CheckInt(oECronograma.Versioncotizacion) + 1
                        Else
                            objECronograma.Versioncotizacion = CFunciones.CheckInt(oECronograma.Versioncotizacion)
                        End If
                        objDCotizacionCronogramaTx.InsertarCronograma(CFunciones.SerializeObject(Of EGcc_cotizacioncronograma)(objECronograma))
                    Next

                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try

        Return blnResultado
    End Function
    'Inicio IBK
    <AutoComplete(True)> _
       Public Function RegistrarRutaCronograma(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.RegistrarRutaCronograma
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.RegistrarRutaCronograma(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function
    'Fin IBK

    ''' <summary>
    ''' Modifica el estado de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCotizacionEstado(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacionEstado
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.ModificarCotizacionEstado(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modifica el estado de carta de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCotizacionCarta(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacionCarta
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.ModificarCotizacionCarta(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modifica el estado de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function CotizacionAprobar(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.CotizacionAprobar
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.CotizacionAprobar(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Rechazar la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacionSeguimiento">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function CotizacionRechazar(ByVal pECotizacionSeguimiento As String) As Boolean Implements ICotizacionTx.CotizacionRechazar
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.CotizacionRechazar(pECotizacionSeguimiento)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function

#Region "WEB SERVICES"
    ''' <summary>
    ''' Web Services ModificarEstadoCotizacionWS
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion"></param>
    ''' <param name="pstrCodigoEstado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function ModificarEstadoCotizacionWS(ByVal pstrNumeroCotizacion As String, ByVal pstrCodUnico As String, ByVal pstrCodigoEstado As String) As Boolean Implements ICotizacionTx.ModificarEstadoCotizacionWS
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.ModificarEstadoCotizacionWS(pstrNumeroCotizacion, pstrCodUnico, pstrCodigoEstado)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function
    ''' <summary>
    ''' ModificarCotizacionWS
    ''' </summary>
    ''' <param name="pECotizacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function ModificarCotizacionWS(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacionWS
        Dim objDCotizacionTx As DCotizacionTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionTx = New DCotizacionTx
            blnResultado = objDCotizacionTx.ModificarCotizacionWS(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionTx.Dispose()
            objDCotizacionTx = Nothing
        End Try
        Return blnResultado
    End Function


#End Region


#Region "SubPrestatario"

    ''' <summary>
    ''' Insert SubPrestatario
    ''' </summary>
    ''' <param name="pESubPrestatario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function InsertarSubPrestatario(ByVal pESubPrestatario As String) As Boolean Implements ICotizacionTx.InsertarSubPrestatario
        Dim objDSubPrestatarioTx As DSubPrestatarioTx = Nothing
        Dim strCodCotizacion As String

        Try
            objDSubPrestatarioTx = New DSubPrestatarioTx
            strCodCotizacion = objDSubPrestatarioTx.InsertarSubPrestatario(pESubPrestatario)
        Catch ex As Exception
            Throw ex
        Finally
            objDSubPrestatarioTx.Dispose()
            objDSubPrestatarioTx = Nothing
        End Try

        Return strCodCotizacion
    End Function

    ''' <summary>
    ''' Modifica SubPrestatario
    ''' </summary>
    ''' <param name="pESubPrestatario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ModificarSubPrestatario(ByVal pESubPrestatario As String) As Boolean Implements ICotizacionTx.ModificarSubPrestatario
        Dim objDSubPrestatarioTx As DSubPrestatarioTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDSubPrestatarioTx = New DSubPrestatarioTx
            blnResultado = objDSubPrestatarioTx.ModificarSubPrestatario(pESubPrestatario)
        Catch ex As Exception
            Throw ex
        Finally
            objDSubPrestatarioTx.Dispose()
            objDSubPrestatarioTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#Region "DocumentoComentario"

    ''' <summary>
    ''' Ingresar CotizacionDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements ICotizacionTx.InsertarCotizacionDocumento
        Dim DCotizacionDocumentoTx As DCotizacionDocumentoTx = Nothing
        Dim blnResultado As Boolean
        Try
            DCotizacionDocumentoTx = New DCotizacionDocumentoTx
            blnResultado = DCotizacionDocumentoTx.InsertarCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            DCotizacionDocumentoTx.Dispose()
            DCotizacionDocumentoTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar CotizacionDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements ICotizacionTx.ModificarCotizacionDocumento
        Dim DCotizacionDocumentoTx As DCotizacionDocumentoTx = Nothing
        Dim blnResultado As Boolean
        Try
            DCotizacionDocumentoTx = New DCotizacionDocumentoTx
            blnResultado = DCotizacionDocumentoTx.ModificarCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            DCotizacionDocumentoTx.Dispose()
            DCotizacionDocumentoTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar CotizacionDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements ICotizacionTx.EliminarCotizacionDocumento
        Dim DCotizacionDocumentoTx As DCotizacionDocumentoTx = Nothing
        Dim blnResultado As Boolean
        Try
            DCotizacionDocumentoTx = New DCotizacionDocumentoTx
            blnResultado = DCotizacionDocumentoTx.EliminarCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            DCotizacionDocumentoTx.Dispose()
            DCotizacionDocumentoTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#Region "Cronograma"

    ''' <summary>
    ''' Ingresar CotizacionCronograma
    ''' </summary>
    ''' <param name="pECotizacionCronograma">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarCotizacionCronograma(ByVal pECotizacionCronograma As String) As Boolean Implements ICotizacionTx.InsertarCotizacionCronograma
        Dim oDCotizacionCronogramaTx As DCotizacionCronogramaTx = Nothing
        Dim blnResultado As Boolean
        Try
            oDCotizacionCronogramaTx = New DCotizacionCronogramaTx
            blnResultado = oDCotizacionCronogramaTx.InsertarCronograma(pECotizacionCronograma)
        Catch ex As Exception
            Throw ex
        Finally
            oDCotizacionCronogramaTx.Dispose()
            oDCotizacionCronogramaTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar CotizacionCronograma
    ''' </summary>
    ''' <param name="pECotizacionCronograma">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarCotizacionCronograma(ByVal pECotizacionCronograma As String) As Boolean Implements ICotizacionTx.ModificarCotizacionCronograma
        Dim oDCotizacionCronogramaTx As DCotizacionCronogramaTx = Nothing
        Dim blnResultado As Boolean
        Try
            oDCotizacionCronogramaTx = New DCotizacionCronogramaTx
            blnResultado = oDCotizacionCronogramaTx.ModificarCronograma(pECotizacionCronograma)
        Catch ex As Exception
            Throw ex
        Finally
            oDCotizacionCronogramaTx.Dispose()
            oDCotizacionCronogramaTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar CotizacionCronograma
    ''' </summary>
    ''' <param name="pECotizacionCronograma">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarCotizacionCronograma(ByVal pECotizacionCronograma As String) As Boolean Implements ICotizacionTx.EliminarCotizacionCronograma
        Dim oDCotizacionCronogramaTx As DCotizacionCronogramaTx = Nothing
        Dim blnResultado As Boolean
        Try
            oDCotizacionCronogramaTx = New DCotizacionCronogramaTx
            blnResultado = oDCotizacionCronogramaTx.EliminarCronograma(pECotizacionCronograma)
        Catch ex As Exception
            Throw ex
        Finally
            oDCotizacionCronogramaTx.Dispose()
            oDCotizacionCronogramaTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LCotizacionNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("CAD00B18-DA30-4ae4-8D59-06A14A040310") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCotizacionNTx")> _
Public Class LCotizacionNTx
    Inherits ServicedComponent
    Implements ICotizacionNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LCotizacionNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Listado de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoCotizacion(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pECotizacion As String) As String Implements ICotizacionNTx.ListadoCotizacion
        Dim objDCotizacionNTx As DCotizacionNTx = Nothing
        Dim strResultado As String

        Try
            objDCotizacionNTx = New DCotizacionNTx
            strResultado = objDCotizacionNTx.ListadoCotizacion(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Get de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un registro de Cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function GetCotizacion(ByVal pECotizacion As String) As String Implements ICotizacionNTx.GetCotizacion
        Dim objDCotizacionNTx As DCotizacionNTx = Nothing
        Dim strResultado As String

        Try
            objDCotizacionNTx = New DCotizacionNTx
            strResultado = objDCotizacionNTx.GetCotizacion(pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista Los datos de la cotizcion por un nuemro o codigo unico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Public Function ConsultaCotizacion(ByVal pstrNumeroCotizacion As String, _
                                    ByVal pstrCodigoUnico As String) As String Implements ICotizacionNTx.ConsultaCotizacion
        Try
            Using oDCotizacionNtx As New DCotizacionNTx
                Return oDCotizacionNtx.ConsultaCotizacion(pstrNumeroCotizacion, pstrCodigoUnico)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'iniio IBK
#Region "Busqueda Clientes"
    Public Function ListadoCliente(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pRazonSocial As String) As String Implements ICotizacionNTx.ListadoCliente

        Dim objClienteNTx As DCotizacionNTx = Nothing
        Dim strResultado As String

        Try
            objClienteNTx = New DCotizacionNTx
            strResultado = objClienteNTx.ListadoCliente(pPageSize, _
                                                        pCurrentPage, _
                                                        pSortColumn, _
                                                        pSortOrder, _
                                                        pRazonSocial)
        Catch ex As Exception
            Throw ex
        Finally
            objClienteNTx.Dispose()
        End Try

        Return strResultado
    End Function
#End Region
    'Fin IBK
#Region "SubPrestatario"

    ''' <summary>
    ''' Obtiene el SubPrestatario de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pstrCodSuprestatario">Codigo Suprestatario</param>
    ''' <param name="pstrCodUnico">Codigo Unico</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012 
    ''' </remarks>
    Public Function ObtenerSubPrestatario(ByVal pstrCodSuprestatario As String, ByVal pstrCodUnico As String) As String Implements ICotizacionNTx.ObtenerSubPrestatario
        Dim objDSubPrestatarioNTx As DSubPrestatarioNTx = Nothing
        Dim strResultado As String

        Try
            objDSubPrestatarioNTx = New DSubPrestatarioNTx
            strResultado = objDSubPrestatarioNTx.ObtenerSubPrestatario(pstrCodSuprestatario, pstrCodUnico)
        Catch ex As Exception
            Throw ex
        Finally
            objDSubPrestatarioNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#Region "DocumentoComentario"

    ''' <summary>
    ''' Listado de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pECotizacionDocumento As String) As String Implements ICotizacionNTx.ListadoCotizacionDocumento
        Dim objDCotizacionDocumentoNTx As DCotizacionDocumentoNTx = Nothing
        Dim strResultado As String

        Try
            objDCotizacionDocumentoNTx = New DCotizacionDocumentoNTx
            strResultado = objDCotizacionDocumentoNTx.ListadoCotizacionDocumento(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionDocumentoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Get de CotizacionDocumento
    ''' </summary>
    ''' <returns>Devuelve un registro de Cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionDocumento(ByVal pECotizacionDocumento As String) As String Implements ICotizacionNTx.ObtenerCotizacionDocumento
        Dim objDCotizacionDocumentoNTx As DCotizacionDocumentoNTx = Nothing
        Dim strResultado As String

        Try
            objDCotizacionDocumentoNTx = New DCotizacionDocumentoNTx
            strResultado = objDCotizacionDocumentoNTx.ObtenerCotizacionDocumento(pECotizacionDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionDocumentoNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

#Region "Cotizacion Version"
    ''' <summary>
    ''' Listado General de Cotizacion Version
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 23/05/2012
    ''' </remarks>
    Public Function ListadoCotizacionVersion(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pECotizacion As String) As String Implements ICotizacionNTx.ListadoCotizacionVersion

        Try
            Using objDCotVerisionNTx As New DCotizacionVersionNTx
                Return objDCotVerisionNTx.ListadoCotizacionVersion(pPageSize, _
                                                                  pCurrentPage, _
                                                                  pSortColumn, _
                                                                  pSortOrder, _
                                                                  pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener Version especifica de una Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionVersion(ByVal pECotizacion As String) As String Implements ICotizacionNTx.ObtenerCotizacionVersion

        Try
            Using objDCotVerisionNTx As New DCotizacionVersionNTx
                Return objDCotVerisionNTx.ObtenerCotizacionVersion(pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Seguimiento Cotizacion"
    ''' <summary>
    ''' Listado del Seguimeinto de una Cotizacion especifica
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 23/05/2012
    ''' </remarks>
    Public Function ListadoSeguimientoCotizacion(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pECotizacion As String) As String Implements ICotizacionNTx.ListadoSeguimientoCotizacion
        Try
            Using objDCotSeguimientoNTx As New DCotizacionSeguimientoNTx
                Return objDCotSeguimientoNTx.ListadoSeguimientoCotizacion(pPageSize, _
                                                                          pCurrentPage, _
                                                                          pSortColumn, _
                                                                          pSortOrder, _
                                                                          pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Cotizacion Cronograma"
    ''' <summary>
    ''' Obtener Cronograma de la version y cotizacion especifica
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionCronograma(ByVal pECotizacion As String) As String Implements ICotizacionNTx.ObtenerCotizacionCronograma

        Try
            Using objDCotCronogramaNTx As New DCotizacionCronogramaNTx
                Return objDCotCronogramaNTx.ObtenerCotizacionCronograma(pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener Cronograma Actual cotizacion especifica
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion">Numero de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Public Function ObtenerCronogramaActual(ByVal pstrNumeroCotizacion As String) As String Implements ICotizacionNTx.ObtenerCronogramaActual

        Try
            Using objDCotCronogramaNTx As New DCotizacionCronogramaNTx
                Return objDCotCronogramaNTx.ObtenerCronogramaActual(pstrNumeroCotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener Cronograma de la version y cotizacion especifica
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function CotizacionCronogramaGet(ByVal pECotizacion As String) As String Implements ICotizacionNTx.CotizacionCronogramaGet

        Try
            Using objDCotCronogramaNTx As New DCotizacionCronogramaNTx
                Return objDCotCronogramaNTx.CotizacionCronogramaGet(pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    ' Inicio AAE 
#Region "Auxiliares"
    ' INICIO AAE - 14/09/2012 - Función que obtiene el correo del ejecutivo
    Public Function GetCodUsuarioEjecutivo(ByVal pstrNroCotizacion As String, ByVal nbrEsCotizacion As Integer) As String Implements ICotizacionNTx.GetCodUsuarioEjecutivo

        Try
            Using objDCotizacionNTx As New DCotizacionNTx
                Return objDCotizacionNTx.GetCodUsuarioEjecutivo(pstrNroCotizacion, nbrEsCotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCodUsuarioAdministradoresLeasing() As String Implements ICotizacionNTx.GetCodUsuarioAdministradoresLeasing

        Try
            Using objDCotizacionNTx As New DCotizacionNTx
                Return objDCotizacionNTx.GetCodUsuarioAdministradoresLeasing()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCodUsuarioAdministradoresComercial() As String Implements ICotizacionNTx.GetCodUsuarioAdministradoresComercial
        Try
            Using objDCotizacionNTx As New DCotizacionNTx
                Return objDCotizacionNTx.GetCodUsuarioAdministradoresComercial()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    ' FIN AAE

#Region "Wwb Services"
    <AutoComplete(True)> _
    Public Function ConsultarCotizacionWS(ByVal pECodigoCotizacion As String, ByVal pECodigoUnico As String) As Boolean Implements ICotizacionNTx.ConsultarCotizacionWS
        Dim objDCotizacionNTx As DCotizacionNTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDCotizacionNTx = New DCotizacionNTx
            blnResultado = objDCotizacionNTx.ConsultarCotizacionWS(pECodigoCotizacion, pECodigoUnico)
        Catch ex As Exception
            Throw ex
        Finally
            objDCotizacionNTx.Dispose()
            objDCotizacionNTx = Nothing
        End Try
        Return blnResultado
    End Function



#End Region



#End Region

End Class

#End Region
