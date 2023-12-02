Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.IO
Imports System.Diagnostics
Imports GCC.UI

Public Class GCCLog

    Dim strNombreASPX As String = ""

    ''' <summary>
    ''' Constructor
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/02/2011
    ''' </remarks>
    Public Sub New(ByVal pStrNombreASPX As String)
        strNombreASPX = pStrNombreASPX
    End Sub

    ''' <summary>
    ''' Inicializa Log
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/02/2011
    ''' </remarks>
    Public Sub InicializarLog(ByVal strRuta As String, ByVal argAmbiente As Integer)

        '1.Inicilializar
        Dim strSistema As String = "GCC"
        Dim strAmbiente As String = ""
        Dim strCarpeta As String = MonthName(Month(Today)) & "_" & Year(Today)
        strCarpeta = strRuta & "GCC\" & strCarpeta.ToUpper

        Select Case argAmbiente
            Case 0 : strAmbiente = "PRD"
            Case 1 : strAmbiente = "UAT"
            Case 2 : strAmbiente = "SIT"
            Case 3 : strAmbiente = "DES"
            Case 4 : strAmbiente = ""
            Case Else : strAmbiente = "NONE"
        End Select

        Dim strArchivo As String = strSistema & "_" & strAmbiente & "_" & Format(Today, "ddMMyyyy") & ".log"
        strArchivo = strCarpeta.ToUpper & "\" & strArchivo
        GCCSession.ArchivoLog = strArchivo

        '2.Ejecutar
        '2.1.Crear ruta
        If Not System.IO.Directory.Exists(strCarpeta) Then
            System.IO.Directory.CreateDirectory(strCarpeta)
        End If

        '2.2.Crear archivo
        Dim strOriginal As String = Nothing
        Dim swLog As StreamWriter
        If Not File.Exists(strArchivo) Then
            swLog = New IO.StreamWriter(strArchivo)
            swLog.WriteLine(System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") & " | INFO | " & strNombreASPX & " | Mensaje :: Inicialización de Log correcta.")
            swLog.Close()
        End If

    End Sub

    ''' <summary>
    ''' Escribe Log
    ''' </summary>
    ''' <param name="strNivel">Nivel del Log: DEBUG, INFO, WARNING, ERROR, FATAL</param>
    ''' <param name="strMensaje">Mensaje para el Log</param>
    ''' <param name="strMetodo">Estructura: ([NroLinea]::[Metodo_Accion])</param>
    ''' <remarks></remarks>
    Public Sub escribe(ByVal strNivel As String, ByVal strMensaje As String, ByVal strMetodo As String)
        Dim strArchivo As String = GCCSession.ArchivoLog
        Dim strLogNivel As String = GCCUtilitario.fstrObtieneKeyWebConfig("LogNivel")

        Debug.WriteLine(System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") & " | " & strNivel & " | " & strNombreASPX & "::" & strMetodo & " | Mensaje :: " & strMensaje)

        'Using srLog As IO.TextWriter = IO.File.AppendText(strArchivo)
        '    srLog.WriteLine(System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") & " | " & strNivel & " | " & strNombreASPX & "::" & strMetodo & " | Mensaje :: " & strMensaje)
        'End Using
    End Sub

    ''' <summary>
    ''' toWrite
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - ???
    ''' Fecha de Creación  : ??/??/????
    ''' </remarks>
    Public Function toWrite(ByVal strRuta As String, _
                           ByVal argSd As String, _
                           ByVal argAmbiente As Integer, _
                           ByVal argElemento As String, _
                           ByVal argFuncion As String, _
                           ByVal argTipo As Integer, _
                           ByVal argErrApp As String, _
                           ByVal argMsgApp As String, _
                           ByVal argErrOri As String, _
                           ByVal argMsgOri As String) As Integer
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

        '1.Inicializar variables
        toWrite = -1
        Dim strFuncion As String = "toWrite"
        'Dim strRuta As String = System.Reflection.Assembly.GetExecutingAssembly.Location
        'strRuta = System.IO.Path.GetDirectoryName(strRuta) & "\"
        'Dim strRuta As String = "c:\"

        Select Case argAmbiente
            Case 0 : strAmbiente = "Prd"
            Case 1 : strAmbiente = "Uat"
            Case 2 : strAmbiente = "Sit"
            Case 3 : strAmbiente = "Des"
            Case 4 : strAmbiente = ""
            Case Else : toWrite = -2 : Exit Function
        End Select
        Dim strCarpeta As String = MonthName(Month(Today)) & Year(Today)
        strRuta = strRuta & strCarpeta.ToUpper & "\"
        strFile = strRuta & LCase(argSd) & strAmbiente & Format(Today, "ddMMyyyy") & ".log"
        Select Case argTipo
            Case 0 : strTipo = "Exito"
            Case 1 : strTipo = "Informativo"
            Case 2 : strTipo = "Advertencia"
            Case 3 : strTipo = "Error"
            Case 4 : strTipo = "Fatal"
            Case Else : toWrite = -3 : Exit Function
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
            Dim xmlReader As XmlTextReader = New XmlTextReader(strFile)
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
        xmlWriter.WriteElementString("Fecha", System.DateTime.Now.ToString("yyyymmdd"))
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
        toWrite = 0
    End Function

End Class
