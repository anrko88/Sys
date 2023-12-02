Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LDesembolsoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("19127616-2479-4b90-BD8E-881564D1399A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDesembolsoTx")> _
Public Class LDesembolsoTx
    Inherits ServicedComponent
    Implements IDesembolsoTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LDesembolsoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean Implements IDesembolsoTx.InsertarContratoEstructDoc
        Try
            Using oDContratoEstructDoc As New DSolicitudCreditoEstructuraDocTx
                Return oDContratoEstructDoc.InsertarContratoEstructDoc(pEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean Implements IDesembolsoTx.ModificarContratoEstructDoc
        Try
            Using oDContratoEstructDoc As New DSolicitudCreditoEstructuraDocTx
                Return oDContratoEstructDoc.ModificarContratoEstructDoc(pEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 06/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean Implements IDesembolsoTx.EliminarContratoEstructDoc
        Try
            Using oDContratoEstructDoc As New DSolicitudCreditoEstructuraDocTx
                Return oDContratoEstructDoc.EliminarContratoEstructDoc(pEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Insertar Documento Bien
    ''' </summary>
    ''' <param name="pEContratoEstructDocDet">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean Implements IDesembolsoTx.InsertarContratoEstructDocDet
        Dim objDSolCredEstructuraDocDetTx As DSolicitudCreditoEstructuraDocDetTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSolCredEstructuraDocDetTx = New DSolicitudCreditoEstructuraDocDetTx
            blnResultado = objDSolCredEstructuraDocDetTx.InsertarContratoEstructDocDet(pEContratoEstructDocDet)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolCredEstructuraDocDetTx.Dispose()
            objDSolCredEstructuraDocDetTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar Documento Bien
    ''' </summary>
    ''' <param name="pEContratoEstructDocDet">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean Implements IDesembolsoTx.EliminarContratoEstructDocDet
        Dim objDSolCredEstructuraDocDetTx As DSolicitudCreditoEstructuraDocDetTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSolCredEstructuraDocDetTx = New DSolicitudCreditoEstructuraDocDetTx
            blnResultado = objDSolCredEstructuraDocDetTx.EliminarContratoEstructDocDet(pEContratoEstructDocDet)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolCredEstructuraDocDetTx.Dispose()
            objDSolCredEstructuraDocDetTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Generar WIO - En desembolso (Formalizacion)
    ''' </summary>
    ''' <param name="pstrClienteWIO">Entidad Serializado del Cliente WIO</param>
    ''' <param name="pstrInstruccionWIO">Entidad serializada de la Instruccion WIO</param>   
    ''' <param name="pstrCaracteristicasWIO">Entidad serializada de la caracteristicas WIO</param>
    ''' <param name="pstrSeguimientoWIO">Entidad del segimiento WIO</param>
    ''' <param name="pXmlEContratoEstructDoc">Datatable serializado de COntrato Estructu doc</param>
    ''' <param name="pstrNroContrato">Numero de COntrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
   Public Function RegistrarWIO(ByVal pstrClienteWIO As String, _
                                ByVal pstrInstruccionWIO As String, _
                                ByVal pstrCaracteristicasWIO As String, _
                                ByVal pstrSeguimientoWIO As String, _
                                ByVal pxmlCtaCargoLeasing As String, _
                                ByVal pxmlBienLeasing As String, _
                                ByVal pXmlEContratoEstructDoc As String, _
                                ByVal pstrNroContrato As String, _
                                ByVal pstrNroLineaOp As String, _
                                ByVal pstrEsActivacion As String) As String Implements IDesembolsoTx.RegistrarWIO
        Dim objDSolCredEstructuraDocTx As New DSolicitudCreditoEstructuraDocTx
        'Inicio IBK - AAE - Activación Leasing Parcial
        Dim oDInstruccionDesembolsoTx As New DInstruccionDesembolsoTx
        'Fin IBK 
        Try
            'INSERTO WIO
            Dim objBLInfoGeneralTx As Object = CreateObject("F15.Logic.LInfoGeneralTx")
            Dim strNroInstruccion As String = objBLInfoGeneralTx.fblnInsertarInstruccionCliente(pstrClienteWIO, pstrInstruccionWIO)

            'Modifico Instruccion Operatiava WIO
            Dim oEInstruccion As New F15.Entity.EINSTRUCCIONOPERATIVA
            oEInstruccion = CFunciones.DeserializeObject(Of F15.Entity.EINSTRUCCIONOPERATIVA)(pstrInstruccionWIO)
            oEInstruccion.Numeroinstruccion = strNroInstruccion
            objBLInfoGeneralTx.fblnModificarInstruccionoperativa(CFunciones.SerializeObject(Of F15.Entity.EINSTRUCCIONOPERATIVA)(oEInstruccion))

            'Insertar de donde provino
            Dim objLInstruccionOpTx As Object = CreateObject("F15.Logic.LInstruccionOperativaTx")
            objLInstruccionOpTx.fblnModificarTablaEspecifica("INSTRUCCIONOPERATIVA", "FLAGGCC", "NUMEROINSTRUCCION", strNroInstruccion, "1")

            'Caracteristicas WIO
            Dim oECaracteristica As New F15.Entity.ECARACTERISTICAOPERACIONLEASING
            If Not String.IsNullOrEmpty(pstrCaracteristicasWIO) Then
                oECaracteristica = CFunciones.DeserializeObject(Of F15.Entity.ECARACTERISTICAOPERACIONLEASING)(pstrCaracteristicasWIO)
                oECaracteristica.NumeroInstruccion = strNroInstruccion

                Dim objLCaracteristicainstruccionTx As Object = CreateObject("F15.Logic.LCaracteristicaInstruccionTx")
                objLCaracteristicainstruccionTx.flstInsertarProductoCaractOperaLeasing(CFunciones.SerializeObject(Of F15.Entity.ECARACTERISTICAOPERACIONLEASING)(oECaracteristica), "M")
            End If

            'Cuenta Cargo Leasing
            If Not String.IsNullOrEmpty(pxmlCtaCargoLeasing) Then
                Dim objLCaracteristicainstruccionTx As Object = CreateObject("F15.Logic.LCaracteristicaInstruccionTx")
                objLCaracteristicainstruccionTx.flstInsertarProductoCuentaCargoLeasing(pxmlCtaCargoLeasing, strNroInstruccion, "M")
            End If

            'Bien Leasing WIO
            If Not String.IsNullOrEmpty(pxmlBienLeasing) Then
                Dim objLCaracteristicainstruccionTx As Object = CreateObject("F15.Logic.LCaracteristicaInstruccionTx")
                objLCaracteristicainstruccionTx.flstInsertarBienLeasing(pxmlBienLeasing, strNroInstruccion, "M")
            End If

            'Insertar Seguimiento WIO
            If Not String.IsNullOrEmpty(pstrSeguimientoWIO) Then
                Dim oESeguimiento As F15.Entity.ESEGUIMIENTO = CFunciones.DeserializeObject(Of F15.Entity.ESEGUIMIENTO)(pstrSeguimientoWIO)
                oESeguimiento.Numeroinstruccion = strNroInstruccion

                Dim LSeguimientoTx As Object = CreateObject("F15.Logic.LSeguimientoTx")
                LSeguimientoTx.fblnInsertarSeguimiento(CFunciones.SerializeObject(Of F15.Entity.ESEGUIMIENTO)(oESeguimiento))
            End If

            'Actualizar los documentos a desembolsados
            If pstrEsActivacion <> "1" Then
                objDSolCredEstructuraDocTx.ActualizarGrupoContratoEstruct(pXmlEContratoEstructDoc, strNroInstruccion, oEInstruccion.Secuencia)
                'Inicio IBK - AAE - Activación de leasing Parcial
            Else
                oDInstruccionDesembolsoTx.ActualizarNroWIOActivacionParcial(pstrNroContrato, strNroInstruccion)
                'Fin IBK
            End If

            'Insertar la Relacion Contrato WIO
            objDSolCredEstructuraDocTx.InsertarContratoWIO(pstrNroContrato, strNroInstruccion, pstrNroLineaOp)

            Return strNroInstruccion
        Catch ex As Exception
            Throw ex
        Finally
            objDSolCredEstructuraDocTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Registra LINEA Y TASA WIO - En desembolso (Formalizacion)
    ''' </summary>
    ''' <param name="pstrLineaOPWIO">Entidad Serializada de la Linea de credito WIO</param>
    ''' <param name="pstrTasaComisionWIO">Datatable serializado en Xml WIO</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 27/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RegistrarLineaWIO(ByVal pstrLineaOPWIO As String, _
                                    ByVal pstrTasaComisionWIO As String, _
                                    ByVal pstrNroInstruccion As String) As Boolean Implements IDesembolsoTx.RegistrarLineaWIO
        Try
            'Lineas Operaciones WIO
            If Not String.IsNullOrEmpty(pstrLineaOPWIO) Then
                Dim objLLineaOpTX As Object = CreateObject("F15.Logic.LLineaOPTx")
                objLLineaOpTX.flstInsertarLineaCredito(pstrLineaOPWIO, "M")
            End If

            'Tasa Comision WIO
            If Not String.IsNullOrEmpty(pstrTasaComisionWIO) Then
                Dim objLLineaOpTX As Object = CreateObject("F15.Logic.LLineaOPTx")
                objLLineaOpTX.flstInsertarTasaComision(pstrTasaComisionWIO, pstrNroInstruccion, "M")
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ModificaEstadoDocumentoWS
    ''' </summary>
    ''' <param name="strEGcc_desembolso"></param>
    ''' <param name="pTipoAprobacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
Public Function ModificaEstadoDocumentoWS(ByVal strEGcc_desembolso As String, ByVal pTipoAprobacion As String) As Boolean Implements IDesembolsoTx.ModificaEstadoDocumentoWS
        Dim objDSolCredEstructuraDocTx As DSolicitudCreditoEstructuraDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSolCredEstructuraDocTx = New DSolicitudCreditoEstructuraDocTx
            blnResultado = objDSolCredEstructuraDocTx.ModificaEstadoDocumentoWS(strEGcc_desembolso, pTipoAprobacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolCredEstructuraDocTx.Dispose()
            objDSolCredEstructuraDocTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' ActualizarIGVDesembolso
    ''' </summary>
    ''' <param name="strNumContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function ActualizarIGVDesembolso(ByVal strNumContrato As String) As Integer Implements IDesembolsoTx.ActualizarIGVDesembolso
        Dim objDSolCredEstructuraDocTx As DSolicitudCreditoEstructuraDocTx = Nothing
        Dim intResultado As Integer
        Try
            objDSolCredEstructuraDocTx = New DSolicitudCreditoEstructuraDocTx
            intResultado = objDSolCredEstructuraDocTx.ActualizarIGVDesembolso(strNumContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolCredEstructuraDocTx.Dispose()
            objDSolCredEstructuraDocTx = Nothing
        End Try
        Return intResultado
    End Function

    'Inicio IBK
    ''' <summary>
    ''' Actualiza las relaciones entre bienes y documentos
    ''' </summary>
    ''' <param name="pstrArrayDocs">Arreglo de tipodoc;nrodoc;codprov;fecha(ddmmyyyy)|....|.... </param>
    ''' <param name="nbrArraySize">Tamaño del arreglo, el areglo se divide por ;</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function AgregarRelacion(ByVal pstrNroContrato As String, _
                             ByVal pstrSecBien As String, _
                             ByVal pstrArrayDocs As String, _
                             ByVal nbrArraySize As Integer) As Boolean Implements IDesembolsoTx.AgregarRelacion
        Try
            Using oDContratoEstructDocDet As New DSolicitudCreditoEstructuraDocDetTx
                Return oDContratoEstructDocDet.AgregarRelacion(pstrNroContrato, pstrSecBien, pstrArrayDocs, nbrArraySize)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

#End Region

End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LDesembolsoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("B44E8B44-E529-4766-95AE-2D96022F1224") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDesembolsoNTx")> _
Public Class LDesembolsoNTx
    Inherits ServicedComponent
    Implements IDesembolsoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LDesembolsoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Listado de Desembolso Documento Bien
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratoEstructDocDet(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEContratoEstructDoc As String) As String Implements IDesembolsoNTx.ListadoContratoEstructDocDet
        Dim objDSolCredEstructuraDocDetNTx As DSolicitudCreditoEstructuraDocDetNTx = Nothing
        Dim strResultado As String
        Try
            objDSolCredEstructuraDocDetNTx = New DSolicitudCreditoEstructuraDocDetNTx
            strResultado = objDSolCredEstructuraDocDetNTx.ListadoContratoEstructDocDet(pPageSize, _
                                                                                       pCurrentPage, _
                                                                                       pSortColumn, _
                                                                                       pSortOrder, _
                                                                                       pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolCredEstructuraDocDetNTx.Dispose()
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Valida
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Contrato Estructura Documento serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ValidaContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String Implements IDesembolsoNTx.ValidaContratoEstructDoc
        Dim objDSolicitudCreditoEstructuraDoc As DSolicitudCreditoEstructuraDocNTx = Nothing
        Dim strResultado As String
        Try
            objDSolicitudCreditoEstructuraDoc = New DSolicitudCreditoEstructuraDocNTx
            strResultado = objDSolicitudCreditoEstructuraDoc.ValidaContratoEstructDoc(pEContratoEstructDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolicitudCreditoEstructuraDoc.Dispose()
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListarContratoEstructDoc(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String Implements IDesembolsoNTx.ListarContratoEstructDoc
        Try
            Using oDContratoEstructDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDContratoEstructDocNTx.ListarContratoEstructDoc(pPageSize, _
                                                                        pCurrentPage, _
                                                                        pSortColumn, _
                                                                        pSortOrder, _
                                                                        pEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 07/03/2013
    ''' </remarks>
    Public Function ListarContratoEstructDocConsulta(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String Implements IDesembolsoNTx.ListarContratoEstructDocConsulta
        Try
            Using oDContratoEstructDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDContratoEstructDocNTx.ListarContratoEstructDocConsulta(pPageSize, _
                                                                        pCurrentPage, _
                                                                        pSortColumn, _
                                                                        pSortOrder, _
                                                                        pEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListarContratoEstructDocAsociar(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String Implements IDesembolsoNTx.ListarContratoEstructDocAsociar
        Try
            Using oDContratoEstructDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDContratoEstructDocNTx.ListarContratoEstructDocAsociar(pPageSize, _
                                                                                pCurrentPage, _
                                                                                pSortColumn, _
                                                                                pSortOrder, _
                                                                                pEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el dato especifico de los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ObtenerContratoEstructDoc(ByVal pEContratoEstructDocc As String) As String Implements IDesembolsoNTx.ObtenerContratoEstructDoc
        Try
            Using oDContratoEstructDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDContratoEstructDocNTx.ObtenerContratoEstructDoc(pEContratoEstructDocc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Estructura para insertar en WIO los bienes
    ''' </summary>
    ''' <param name="pXmlEContratoEstructDoc"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 20/06/2012
    ''' </remarks>
    Public Function ObtenerBienLeasingWIO(ByVal pXmlEContratoEstructDoc As String) As String Implements IDesembolsoNTx.ObtenerBienLeasingWIO
        Try
            Using oDContratoEstructDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDContratoEstructDocNTx.ObtenerBienLeasingWIO(pXmlEContratoEstructDoc)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener las Cuentas de un contrato para WIO
    ''' </summary>
    ''' <param name="pstrNroContrato">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 20/06/2012
    ''' </remarks>
    Public Function ObtenerCtaLeasingWioSel(ByVal pstrNroContrato As String) As String Implements IDesembolsoNTx.ObtenerCtaLeasingWioSel
        Try
            Using oDContratoCuenta As New DContratoCuentaNTx
                Return oDContratoCuenta.ObtenerCtaLeasingWioSel(pstrNroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene si el Proveedor es Agente de retencion
    ''' </summary>
    ''' <param name="pstrNroDocumento"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 27/06/2012
    ''' </remarks>
    Public Function ObtenerAgenteRetencion(ByVal pstrNroDocumento As String) As Integer Implements IDesembolsoNTx.ObtenerAgenteRetencion
        Try
            Using oDSolicitudCreditoEstructuraDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDSolicitudCreditoEstructuraDocNTx.ObtenerAgenteRetencion(pstrNroDocumento)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
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
                                                ByVal pNroContrato As String) As String Implements IDesembolsoNTx.ListaBienesContrato

        Try
            Using oDSolicitudCreditoNTx As New DSolicitudCreditoNTx
                Return oDSolicitudCreditoNTx.ListaBienesContrato(pPageSize, _
                                                                pCurrentPage, _
                                                                pSortColumn, _
                                                                pSortOrder, _
                                                                pNroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista Documentos del contrato
    ''' </summary>
    ''' <param name="pNroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNroContrato As String) As String Implements IDesembolsoNTx.ListaDocumentosContrato

        Try
            Using oDSolicitudCreditoEstructuraDocNTx As New DSolicitudCreditoEstructuraDocNTx
                Return oDSolicitudCreditoEstructuraDocNTx.ListaDocumentosContrato(pPageSize, _
                                                                pCurrentPage, _
                                                                pSortColumn, _
                                                                pSortOrder, _
                                                                pNroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista Relaciones del contrato
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
                                                ByVal pNroContrato As String) As String Implements IDesembolsoNTx.ListaRelacionesContrato

        Try
            Using oDSolicitudCreditoEstructuraDocDetNTx As New DSolicitudCreditoEstructuraDocDetNTx
                Return oDSolicitudCreditoEstructuraDocDetNTx.ListaRelacionesContrato(pPageSize, _
                                                                pCurrentPage, _
                                                                pSortColumn, _
                                                                pSortOrder, _
                                                                pNroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

#End Region
End Class

#End Region
