
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

Imports System.Text
Imports System.Configuration

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LHostTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 09/01/2013
''' </remarks>
<Guid("8603CE20-8D67-43fd-96B2-FA182D348B7A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DHostTx")> _
Public Class DHostTx
    Inherits ServicedComponent
    Implements IHostTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DHostTx"
#End Region

#Region "Metodos"

    Private RutaCom As String

    <DllImport("NPCli.dll")> _
    Private Shared Function OpenServer(ByVal ServerName As String, ByVal CodTerm As String, ByVal CentralName As String) As Integer
    End Function

    <DllImport("NPCli.dll")> _
    Private Shared Function A_E(ByVal BufferLength As Integer, ByVal pbuffer As StringBuilder) As Integer
    End Function

    <DllImport("NPCli.dll")> _
    Private Shared Function E_A(ByVal BufferLength As Integer, ByVal pbuffer As StringBuilder) As Integer
    End Function

    <DllImport("NPCli.dll")> _
    Private Shared Function SendData(ByVal Hnd As Integer, ByVal pbuffer As String, ByVal BufferLength As Integer) As Boolean
    End Function

    <DllImport("NPCli.dll")> _
    Private Shared Function ReadData(ByVal Hnd As Integer, ByVal pbuffer As StringBuilder) As Integer
    End Function

    <DllImport("NPCli.dll")> _
    Private Shared Function MoreData(ByVal Hnd As Integer) As Boolean
    End Function

    <DllImport("NPCli.dll")> _
    Private Shared Function CloseServer(ByVal Hnd As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function SetDllDirectory(ByVal lpPathName As String) As Boolean
    End Function

    ''' <summary>
    ''' Consumir funciones de NPCli.dll para llamadas a Host
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function TransaccionGINA(ByVal strTramaIn As String, ByRef strTramaOut As String) As Boolean Implements IHostTx.TransaccionGINA

        Try
            Dim Hnd As Integer

            Try
                RutaCom = ConfigurationManager.AppSettings("RutaCom")
                SetDllDirectory(RutaCom)

                Hnd = OpenServer(ConfigurationManager.AppSettings("NPCliServerName"), ConfigurationManager.AppSettings("NPCliTerminal"), "LP00")

                If Hnd = 0 Or Hnd = 99999999 Then
                    strTramaOut = "Inteface Host no disponible"
                    Return False
                End If

                strTramaIn = strTramaIn + vbCr

                'Encriptar Trama
                Dim longTrama As Integer = strTramaIn.Length
                Dim refTramaEncriptar As New StringBuilder(strTramaIn)
                A_E(longTrama, refTramaEncriptar)

                'Enviar Trama Encriptada
                Dim strTramaEnriptada = refTramaEncriptar.ToString
                Dim bRespuesta As Boolean = SendData(Hnd, strTramaEnriptada, strTramaEnriptada.Length)

                If bRespuesta = False Then
                    strTramaOut = "Error al ejecutar la transacción"
                    CloseServer(Hnd)
                    Return False
                End If

                'Recibir Respuesta Encriptada
                Dim strRespuestaEncriptada As String = ""
                Do
                    Dim refTramaRespuesta As New StringBuilder(Space(1024))
                    Dim lread As Integer = ReadData(Hnd, refTramaRespuesta)

                    If lread < 0 Then bRespuesta = False

                    strRespuestaEncriptada = strRespuestaEncriptada + refTramaRespuesta.ToString
                Loop While MoreData(Hnd)

                If bRespuesta = False Then
                    strTramaOut = "No hay respuesta de host"
                    CloseServer(Hnd)
                    Return False
                End If

                'Desencriptar Respuesta
                Dim refRespuestaDesencriptar As New StringBuilder(strRespuestaEncriptada)
                E_A(strRespuestaEncriptada.Length, refRespuestaDesencriptar)

                strTramaOut = refRespuestaDesencriptar.ToString
                CloseServer(Hnd)

                If Mid(strTramaOut, 9, 2) = "00" Then
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                CloseServer(Hnd)
            End Try

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"


<Guid("50C51A35-2F6A-4a22-BB10-4D87C20491BF") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DHostNTx")> _
Public Class DHostNTx
    Inherits ServicedComponent
    Implements IHostNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DHostNTx"
#End Region

#Region "Metodos"


#End Region

End Class

#End Region

