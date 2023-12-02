Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Xml
Imports System.Configuration

Public Class CLog
    Public Function toWrite(ByVal argSd As String, ByVal argAmbiente As Integer, ByVal argElemento As String, ByVal argFuncion As String, ByVal argTipo As Integer, ByVal argErrApp As String, _
     ByVal argMsgApp As String, ByVal argErrOri As String, ByVal argMsgOri As String) As Integer
        '---------------------------------------------------------------------------------------------------
        ' Finalidad    : Escribir log
        ' Parámetros   : argSd       - Sistema donde ocurrio el evento
        '              : argAmbiente - Ambiente donde ocurrio el evento
        '              : argElemento - Elemento donde ocurrio el evento
        '              : argFuncion  - Función donde ocurrio el evento
        '              : argTipo     - Nivel de Error :
        '                              0-Exito
        '                              1-Informativo
        '                              2-Advertencia
        '                              3-Error
        '                              4-Falla general
        '              : argErrApp   - Código de Error de la Aplicación
        '              : argMsgApp   - Mensaje de Error de la Aplicación
        '              : argErrOri   - Grupo de Error Original
        '              : argMsgOri   - Mensaje de Error Original
        ' Resultados   :  0          - Log se escribio correctamente
        '              : -1          - Ocurrio un problema al escribir log
        '              : -2          - Ambiente no válido
        '              : -3          - Código de tipo de log no valido
        ' Modificación : &0001 * FO0000 01/12/04 B8936  B8936 ESTANDARES SDA
        '---------------------------------------------------------------------------------------------------

        '0.Declarar variables
        Dim strAmbiente As String
        Dim strFile As String
        Dim strTipo As String
        Dim iRetorno As Integer

        Try
            '1.Inicializar variables
            iRetorno = -1
            'string strFuncion = "toWrite";
            'Dim strRuta As String = System.Reflection.Assembly.GetExecutingAssembly.Location
            'strRuta = System.IO.Path.GetDirectoryName(strRuta) & "\"
            'string strRuta = @"D:\sda\com\wbc\";
            Dim strRuta As String = ConfigurationManager.AppSettings("RutaLog")

            Select Case argAmbiente
                Case 0
                    strAmbiente = "Prd"
                    Exit Select
                Case 1
                    strAmbiente = "Uat"
                    Exit Select
                Case 2
                    strAmbiente = "Sit"
                    Exit Select
                Case 3
                    strAmbiente = "Des"
                    Exit Select
                Case 4
                    strAmbiente = ""
                    Exit Select
                Case Else
                    iRetorno = -2
                    Return iRetorno
            End Select
            strFile = CFunciones.fConcatenar(strRuta.ToString(), argSd.ToLower(), strAmbiente.ToString(), ".log")
            Select Case argTipo
                Case 0
                    strTipo = "Exito"
                    Exit Select
                Case 1
                    strTipo = "Informativo"
                    Exit Select
                Case 2
                    strTipo = "Advertencia"
                    Exit Select
                Case 3
                    strTipo = "Error"
                    Exit Select
                Case 4
                    strTipo = "Fatal"
                    Exit Select
                Case Else
                    iRetorno = -3
                    Return iRetorno
            End Select

            '2.Ejecutar
            '2.1.Crear ruta
            If Not System.IO.Directory.Exists(strRuta) Then
                System.IO.Directory.CreateDirectory(strRuta)
            End If

            '2.2.Crear archivo
            Dim strOriginal As String = Nothing
            Dim xmlWriter As XmlTextWriter
            If Not System.IO.File.Exists(strFile) Then
                xmlWriter = New XmlTextWriter(strFile, System.Text.Encoding.UTF8)
                xmlWriter.WriteStartDocument()
                xmlWriter.WriteStartElement("Logs")
                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndDocument()
                xmlWriter.Close()
            Else
                Dim xmlReader As New XmlTextReader(strFile)
                xmlReader.WhitespaceHandling = WhitespaceHandling.None
                xmlReader.MoveToContent()
                strOriginal = xmlReader.ReadInnerXml()
                xmlReader.Close()
            End If

            '2.3.Escribir log
            xmlWriter = New XmlTextWriter(strFile, System.Text.Encoding.UTF8)
            xmlWriter.WriteStartDocument()
            xmlWriter.WriteStartElement("Logs")
            xmlWriter.WriteRaw(strOriginal)
            xmlWriter.WriteStartElement("Log")
            xmlWriter.WriteElementString("Fecha", System.DateTime.Now.ToString("yyyyMMdd"))
            xmlWriter.WriteElementString("Hora", System.DateTime.Now.ToString("hh:mm:ss"))
            xmlWriter.WriteElementString("Elemento", argElemento)
            xmlWriter.WriteElementString("Funcion", argFuncion)
            xmlWriter.WriteElementString("Tipo", strTipo)
            xmlWriter.WriteElementString("ErrApp", argErrApp)
            xmlWriter.WriteElementString("MsgApp", argMsgApp)
            xmlWriter.WriteElementString("ErrOri", argErrOri)
            xmlWriter.WriteElementString("MsgOri", argMsgOri)
            xmlWriter.WriteEndElement()
            xmlWriter.Close()
            iRetorno = 0
        Catch
            ' (Exception ex)
            iRetorno = -1
        End Try

        Return iRetorno
    End Function
End Class
'End Namespace