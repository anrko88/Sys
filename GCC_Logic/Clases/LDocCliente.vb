Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LDocClienteTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("7AF50724-84EE-4054-8B25-F5125439BD9E") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocClienteTx")> _
Public Class LDocClienteTx
    Inherits ServicedComponent
    Implements IDocClienteTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LDocClienteTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Guardar de Verificacion Documento Cliente 
    ''' </summary>
    ''' <param name="pEContrato">Entidad de Contrato para valores del Flag y Fecha de Termino Recep Cliente</param>
    ''' <param name="pEContacto">Entidad Serializada de Contacto para la cotizacion y contrato especifico</param>
    ''' <param name="pstrOpcion">Opcion de Contacto Si es N : Nuevo; M : Modificar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    ''' 
    <AutoComplete(True)> _
    Public Function fblnGuardarVerificacionCliente(ByVal pEContrato As String, ByVal pEContacto As String, ByVal pstrOpcion As String) As Boolean Implements IDocClienteTx.fblnGuardarVerificacionCliente
        Try
            If Not String.IsNullOrEmpty(pEContrato) Then
                Using oDContratoTx As New DContratoTx
                    oDContratoTx.ContratoDocClienteUpd(pEContrato)
                End Using
            End If

            Using oDContactoTx As New DContactoTx
                If pstrOpcion = "N" Then
                    oDContactoTx.InsertarContacto(pEContacto)
                ElseIf pstrOpcion = "M" Then
                    oDContactoTx.ModificarContacto(pEContacto)
                End If
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Agregar un Documento/Condicion en Verificacion de Cliente 
    ''' </summary>
    ''' <param name="pstrContratoDoc">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function AgregarDocCondCliente(ByVal pstrContratoDoc As String) As Integer Implements IDocClienteTx.AgregarDocCondCliente
        Try
            Dim intCodigoDocumento As Integer
            Using oDContratoDoc As New DContratoDocumentoTx
                intCodigoDocumento = oDContratoDoc.InsertarContratoDocCliente(pstrContratoDoc)
            End Using

            Return intCodigoDocumento
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar un Documento/Condicion en Verificacion de Cliente de manera Logica
    ''' </summary>
    ''' <param name="pstrContratoDoc">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarDocCondCliente(ByVal pstrContratoDoc As String) As Boolean Implements IDocClienteTx.EliminarDocCondCliente
        Try
            Using oDContratoDoc As New DContratoDocumentoTx
                oDContratoDoc.EliminarContratoDocCliente(pstrContratoDoc)
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Evniar Carta Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EnviarCartaDocumentoCliente(ByVal pEGcc_contratodocumento As String) As Boolean Implements IDocClienteTx.EnviarCartaDocumentoCliente
        Try
            Dim oDContratoDoc As New DContratoDocumentoTx

            Dim oEContratoDoc As New EGcc_contratodocumento
            oEContratoDoc = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

            Dim arrRegistros As String = oEContratoDoc.Observaciones
            Dim strRegistrosSplit As String() = arrRegistros.Split("|")
            If strRegistrosSplit.Length > 0 Then
                For i As Integer = LBound(strRegistrosSplit) To UBound(strRegistrosSplit)
                    oEContratoDoc.Codigocontratodocumento = CFunciones.CheckInt(strRegistrosSplit(i))
                    If oEContratoDoc.Codigocontratodocumento > 0 Then
                        oDContratoDoc.EnviarCartaDocumentoCliente(CFunciones.SerializeObject(Of EGcc_contratodocumento)(oEContratoDoc))
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LDocClienteNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("CCCA0A51-0AD9-4bab-BB2F-470E58AA768D") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocClienteNTx")> _
Public Class LDocClienteNTx
    Inherits ServicedComponent
    Implements IDocClienteNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LDocClienteNTx"
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Obtiene el Contacto de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pstrCodCotizacion">Numero de Cotizacion</param>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012 
    ''' </remarks>
    Public Function ObtenerContacto(ByVal pstrCodCotizacion As String, ByVal pstrCodContrato As String) As String Implements IDocClienteNTx.ObtenerContacto
        Try
            Using oDContactoNTx As New DContactoNTx
                Return oDContactoNTx.ObtenerContacto(pstrCodCotizacion, pstrCodContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado General de Contrato y Cotizacion 
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String Implements IDocClienteNTx.ListadoContratoCotizacion
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ListadoContratoCotizacion(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene Datos de Contrato y Cotizacion por Numero de Contrato
    ''' </summary>
    ''' <param name="pNumeroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 17/05/2012
    ''' </remarks>
    Public Function ObtenerContratoCotizacion(ByVal pNumeroContrato As String) As String Implements IDocClienteNTx.ObtenerContratoCotizacion
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ObtenerContratoCotizacion(pNumeroContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' Inicio IBK - AAE - 03/10/2012 Se agrega método para listar contratocotización en sol Docs
    Function ListadoContratoCotizacionSolDoc(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String Implements IDocClienteNTx.ListadoContratoCotizacionSolDoc
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ListadoContratoCotizacionSolDoc(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' fin IBK
#End Region

End Class

#End Region
