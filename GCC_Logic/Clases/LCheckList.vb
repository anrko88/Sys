Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LCheckListTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("73F9B21C-FC32-429d-8CA4-838E3509ED7B") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCheckListTx")> _
Public Class LCheckListTx
    Inherits ServicedComponent
    Implements ICheckListTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LCheckListTx"
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Modificar Contrato Cuenta
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoCuentaUpd(ByVal pESolicitudCredito As String) As Boolean Implements ICheckListTx.ContratoCuentaUpd
        Dim objDContratoCuentaTx As DContratoCuentaTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoCuentaTx = New DContratoCuentaTx
            blnResultado = objDContratoCuentaTx.ContratoCuentaIns(pESolicitudCredito)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoCuentaTx.Dispose()
            objDContratoCuentaTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Insertar Documentos/Condiciones para el Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean Implements ICheckListTx.ContratoDocumentoIns
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ContratoDocumentoIns(pEGcc_contratodocumento)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualizar Adjunto de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoDocumentoAdjuntoUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements ICheckListTx.ContratoDocumentoAdjuntoUpd
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ContratoDocumentoAdjuntoUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try

        Return blnResultado
    End Function

    '(pFlagAprobacionLegal, pNumeroContrato, pCodigoContratoDocumento)
    ''' <summary>
    ''' Actualizar Adjunto de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pFlagAprobacionLegal">Flag</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ActualizaFlagAprobacionLegal(ByVal pFlagAprobacionLegal As Integer, ByVal pNumeroContrato As String, ByVal pCodigoContratoDocumento As Integer) As Boolean Implements ICheckListTx.ActualizaFlagAprobacionLegal
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ActualizaFlagAprobacionLegal(pFlagAprobacionLegal, pNumeroContrato, pCodigoContratoDocumento)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualizar Observacion de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoDocumentoObsUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements ICheckListTx.ContratoDocumentoObsUpd
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ContratoDocumentoObsUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualizar Estado de Envio Carta de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoDocumentoEnviaCartaUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements ICheckListTx.ContratoDocumentoEnviaCartaUpd
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ContratoDocumentoEnviaCartaUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try

        Return blnResultado
    End Function
    ''' <summary>
    ''' GestionComercialEnviarUpd
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function GestionComercialEnviarUpd(ByVal pEGcc_Contrato As String) As Boolean Implements ICheckListTx.GestionComercialEnviarUpd
        Dim objDContratoGComercialTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoGComercialTx = New DContratoTx
            blnResultado = objDContratoGComercialTx.GestionComercialEnviarUpd(pEGcc_Contrato)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDContratoGComercialTx.Dispose()
            objDContratoGComercialTx = Nothing
        End Try

        Return blnResultado
    End Function


#Region "Representante"

    ''' <summary>
    ''' Insertar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteIns(ByVal pEGcc_Representante As String) As Integer Implements ICheckListTx.RepresentanteIns
        Dim objDRepresentanteTx As DRepresentanteTx = Nothing
        Dim intResultado As Integer

        Try
            objDRepresentanteTx = New DRepresentanteTx
            intResultado = objDRepresentanteTx.RepresentanteIns(pEGcc_Representante)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try

        Return intResultado
    End Function

    ''' <summary>
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteUpd(ByVal pEGcc_Representante As String) As Boolean Implements ICheckListTx.RepresentanteUpd
        Dim objDRepresentanteTx As DRepresentanteTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDRepresentanteTx = New DRepresentanteTx
            blnResultado = objDRepresentanteTx.RepresentanteUpd(pEGcc_Representante)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina un bien cuyas claves coinciden con las enviadas por parámetro, en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.
    ''' </summary>
    ''' <param name="pListEGcc_representante">Entidad ESolicitudCreditoEstructuraLst serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarRepresentante(ByVal pListEGcc_representante As String) As Boolean Implements ICheckListTx.EliminarRepresentante
        Dim objDRepresentanteTx As DRepresentanteTx = Nothing
        Dim blnResultado As Boolean

        Try
            Dim objListEGcc_representante As ListEGcc_representante = CFunciones.DeserializeObject(Of ListEGcc_representante)(pListEGcc_representante)

            objDRepresentanteTx = New DRepresentanteTx
            For Each objEGcc_representante In objListEGcc_representante
                Dim pEGcc_representante As String

                pEGcc_representante = CFunciones.SerializeObject(objEGcc_representante)
                blnResultado = objDRepresentanteTx.RepresentanteDel(pEGcc_representante)

            Next objEGcc_representante
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#Region "Representante Contrato"

    ''' <summary>
    ''' Insertar Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteContratoIns(ByVal pEGcc_contratorepresentante As String) As Integer Implements ICheckListTx.RepresentanteContratoIns


        Dim objDRepresentanteContratoTx As DContratoRepresentanteTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDRepresentanteContratoTx = New DContratoRepresentanteTx
            blnResultado = objDRepresentanteContratoTx.RepresentanteContratoIns(pEGcc_contratorepresentante)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDRepresentanteContratoTx.Dispose()
            objDRepresentanteContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Insertar una lista de Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentanteList">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteContratoListIns(ByVal pESolicitudCredito As String, _
                                                 ByVal pEGcc_contratorepresentanteList As String) As Boolean Implements ICheckListTx.RepresentanteContratoListIns
        Dim objDContratoRepresentanteTx As DContratoRepresentanteTx = Nothing

        Dim blnResultado As Boolean

        Try
            Dim objContratoRepresentanteList As ListEGcc_contratorepresentante = CFunciones.DeserializeObject(Of ListEGcc_contratorepresentante)(pEGcc_contratorepresentanteList)

            objDContratoRepresentanteTx = New DContratoRepresentanteTx
            For Each objEGcc_contratorepresentante In objContratoRepresentanteList
                Dim pEGcc_contratorepresentante As String

                pEGcc_contratorepresentante = CFunciones.SerializeObject(objEGcc_contratorepresentante)
                blnResultado = objDContratoRepresentanteTx.RepresentanteContratoIns(pEGcc_contratorepresentante)

            Next objEGcc_contratorepresentante

            If blnResultado Then
                Dim objDContratoTx As New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            Else
                Throw New Exception("No se pudo ingresar el/los representantes seleccionados.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDContratoRepresentanteTx.Dispose()
            objDContratoRepresentanteTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina un Representantes de un contrato
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteContratoItemDel(ByVal pEGcc_contratorepresentante As String) As Boolean Implements ICheckListTx.RepresentanteContratoItemDel
        Dim objDRepresentanteTx As DContratoRepresentanteTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDRepresentanteTx = New DContratoRepresentanteTx
            blnResultado = objDRepresentanteTx.RepresentanteContratoItemDel(pEGcc_contratorepresentante)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina Todos los Representante  de un contrato
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteContratoDel(ByVal pEGcc_contratorepresentante As String) As Boolean Implements ICheckListTx.RepresentanteContratoDel
        Dim objDRepresentanteTx As DContratoRepresentanteTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDRepresentanteTx = New DContratoRepresentanteTx
            blnResultado = objDRepresentanteTx.RepresentanteContratoDel(pEGcc_contratorepresentante)
        Catch ex As Exception
            blnResultado = False
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Insertar una lista de Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentanteList">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteContratoListDel(ByVal objESolicitudCredito As String, _
                                                 ByVal pEGcc_contratorepresentanteList As String) As Boolean Implements ICheckListTx.RepresentanteContratoListDel
        Dim objDContratoRepresentanteTx As DContratoRepresentanteTx = Nothing
        Dim blnResultado As Boolean

        Try
            Dim objContratoRepresentanteList As ListEGcc_contratorepresentante = CFunciones.DeserializeObject(Of ListEGcc_contratorepresentante)(pEGcc_contratorepresentanteList)

            objDContratoRepresentanteTx = New DContratoRepresentanteTx
            For Each objEGcc_contratorepresentante In objContratoRepresentanteList
                Dim pEGcc_contratorepresentante As String

                pEGcc_contratorepresentante = CFunciones.SerializeObject(objEGcc_contratorepresentante)
                blnResultado = objDContratoRepresentanteTx.RepresentanteContratoItemDel(pEGcc_contratorepresentante)

            Next objEGcc_contratorepresentante

            ' Actualizar el estado del contrato, indicando que se actualizó uno de sus elementos.
            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(objESolicitudCredito)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoRepresentanteTx.Dispose()
            objDContratoRepresentanteTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LCheckListNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("F8123F65-37B0-414a-8EF1-96E89BB137E3") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LCheckListNTx")> _
Public Class LCheckListNTx
    Inherits ServicedComponent
    Implements ICheckListNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LCheckListNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoCuentaSel(ByVal pEGcc_ContratoCuenta As String) As String Implements ICheckListNTx.ContratoCuentaSel

        Dim objDContratoCuentaNTx As DContratoCuentaNTx = Nothing
        Dim strResultado As String

        Try
            objDContratoCuentaNTx = New DContratoCuentaNTx
            strResultado = objDContratoCuentaNTx.ContratoCuentaSel(pEGcc_ContratoCuenta)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDContratoCuentaNTx.Dispose()
            objDContratoCuentaNTx = Nothing
        End Try

        Return strResultado

    End Function

    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoSel(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pEGcc_contratodocumento As String) As String Implements ICheckListNTx.ContratoDocumentoSel

        Dim objDContratoDocumentoNTx As DContratoDocumentoNTx = Nothing
        Dim strResultado As String

        Try
            objDContratoDocumentoNTx = New DContratoDocumentoNTx
            strResultado = objDContratoDocumentoNTx.ContratoDocumentoSel(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEGcc_contratodocumento)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDContratoDocumentoNTx.Dispose()
            objDContratoDocumentoNTx = Nothing
        End Try

        Return strResultado

    End Function

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pFirmaEn As String, _
                                                ByVal pEGcc_representante As String, _
                                                ByVal pNumeroContrato As String) As String Implements ICheckListNTx.RepresentantesSel

        Dim objDRepresentanteNTx As DRepresentanteNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteNTx = New DRepresentanteNTx
            strResultado = objDRepresentanteNTx.RepresentantesSel(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pFirmaEn, pEGcc_representante, pNumeroContrato)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDRepresentanteNTx.Dispose()
            objDRepresentanteNTx = Nothing
        End Try

        Return strResultado

    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RepresentantesItem(ByVal pNroDocumento As String) As String Implements ICheckListNTx.RepresentantesItem

        Dim objDRepresentanteNTx As DRepresentanteNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteNTx = New DRepresentanteNTx
            strResultado = objDRepresentanteNTx.RepresentantesItem(pNroDocumento)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDRepresentanteNTx.Dispose()
            objDRepresentanteNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Representantes por Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesContratoSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGcc_contratoRepresentante As String, _
                                                ByVal pFirma As String) As String Implements ICheckListNTx.RepresentantesContratoSel

        Dim objDRepresentanteContratoNTx As DContratoRepresentanteNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteContratoNTx = New DContratoRepresentanteNTx
            strResultado = objDRepresentanteContratoNTx.ContratoRepresentanteSel(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEGcc_contratoRepresentante, pFirma)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDRepresentanteContratoNTx.Dispose()
            objDRepresentanteContratoNTx = Nothing
        End Try

        Return strResultado

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pESolicitudCredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoCheckListComercial(ByVal pESolicitudCredito As String) As String Implements ICheckListNTx.ListadoCheckListComercial

        Dim objDSolicitudCreditoNTx As DSolicitudCreditoNTx = Nothing
        Dim strResultado As String

        Try
            objDSolicitudCreditoNTx = New DSolicitudCreditoNTx
            strResultado = objDSolicitudCreditoNTx.ListadoCheckListComercial(pESolicitudCredito)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDSolicitudCreditoNTx.Dispose()
            objDSolicitudCreditoNTx = Nothing
        End Try

        Return strResultado

    End Function

    ''' <summary>
    ''' Listado de Representantes del cliente para todos los contratos
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesCliente(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodUnico As String) As String Implements ICheckListNTx.RepresentantesCliente

        Dim objDRepresentanteNTx As DRepresentanteNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteNTx = New DRepresentanteNTx
            strResultado = objDRepresentanteNTx.RepresentantesCliente(pPageSize, _
                                                                      pCurrentPage, _
                                                                      pSortColumn, _
                                                                      pSortOrder, _
                                                                      pCodUnico)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDRepresentanteNTx.Dispose()
            objDRepresentanteNTx = Nothing
        End Try

        Return strResultado

    End Function

    ''' <summary>
    ''' Lista de Documentos y condiciones por numero de contrato
    ''' </summary>
    ''' <param name="p_numerocontrato">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/08/2012
    ''' </remarks>
    Public Function ContratoDocumentoGet(ByVal p_numerocontrato As String) As String Implements ICheckListNTx.ContratoDocumentoGet

        Dim objDContratoDocumentoNTx As DContratoDocumentoNTx = Nothing
        Dim strResultado As String

        Try
            objDContratoDocumentoNTx = New DContratoDocumentoNTx
            strResultado = objDContratoDocumentoNTx.ContratoDocumentoGet(p_numerocontrato)
        Catch ex As Exception
            strResultado = ""
            Throw ex
        Finally
            objDContratoDocumentoNTx.Dispose()
            objDContratoDocumentoNTx = Nothing
        End Try

        Return strResultado

    End Function

#End Region

End Class

#End Region
