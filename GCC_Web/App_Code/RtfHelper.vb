Imports System.IO

Imports Microsoft.VisualBasic

Imports GCC.UI


Public Class RtfHelper


#Region "Campos"

    Public oFileStream As FileStream
    Public oFileContent As StringBuilder
    Public sFileName As String

    Private _width As Integer

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Ancho de la tabla.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Property Width() As Integer
        Get
            Return _width
        End Get
        Set(ByVal value As Integer)
            _width = value
        End Set
    End Property

#End Region

#Region "Métodos y Funciones"

    ''' <summary>
    ''' Identifica si el caracter enviado es un caracter reservado en los documentos rtf, si es asi lo códifica 
    ''' en su equivalente seguro.
    ''' </summary>
    ''' <param name="c"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Shared Function RtfEncoding(ByVal c As Char) As String
        If c = "\"c Then
            Return "\\"
        End If
        If c = "{"c Then
            Return "\{"
        End If
        If c = "}"c Then
            Return "\}"
        End If
        If c = ControlChars.Lf Then
            Return vbCr & vbLf & "\line "
        End If
        Dim code As Integer = Convert.ToInt32(c)

        If Char.IsLetter(c) AndAlso code < &H80 Then
            Return c.ToString()
        End If

        Return "\u" + code.ToString() + "?"
    End Function

    ''' <summary>
    ''' Transforma cada caracter no ANSI o de uso reservado en RTF a su equivalente hexadecimal
    ''' </summary>
    ''' <param name="texto"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function TextoRtf(ByVal texto As String) As String
        Dim returned As New StringBuilder()

        For Each c As Char In texto
            returned.Append(RtfEncoding(c))
        Next c

        Return returned.ToString()
    End Function

    ''' <summary>
    ''' Permite reemplazar palabras o caracteres específicos dentro de todas las secciones del documento.
    ''' </summary>
    ''' <param name="etiqueta">Texto a buscar en el documento</param>
    ''' <param name="texto">Texo con el cual reemplazar</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub Reemplazar(ByVal etiqueta As String, _
                          ByVal texto As String)

        oFileContent.Replace(etiqueta, _
                             TextoRtf(texto))
    End Sub

    ''' <summary>
    ''' Inserta una tabla en el lugar indicado por la palabra clave. Inserta una columna adicional a la izquierda de la tabla,
    ''' con un margen indicado, que sirva como desplazamiento.
    ''' Sólo puede haber una palabra clave por cada tabla a reemplazar.
    ''' </summary>
    ''' <param name="margenIzquierdo">Ancho de la columna o margen de desplazamiento a la izquierda.</param>
    ''' <param name="PalabraClave">Texto a buscar y reemplazar por la tabla.</param>
    ''' <param name="cuerpo">Matriz con el contenido a ingresar en la tabla.</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub Tabla(ByVal margenIzquierdo As Integer, _
                     ByVal PalabraClave As String, _
                     ByVal cuerpo As String())
        Dim rows As Integer
        Dim oTablaBasica As New TablaBasica

        ' Leer cuantas filas y columnas va a tener la tabla
        rows = cuerpo.GetLength(0)

        For i As Integer = 0 To rows - 1
            oTablaBasica.sbTabla.AppendLine(TablaBasica.InicioFila)

            Dim definicionFila As New StringBuilder
            definicionFila.AppendLine(TablaBasica.PrefijoFila + margenIzquierdo.ToString())
            definicionFila.AppendLine(TablaBasica.PrefijoFila + (_width - margenIzquierdo).ToString())
            oTablaBasica.sbTabla.Append(definicionFila)

            oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
            If Not (IsNothing(cuerpo(i)) OrElse String.IsNullOrEmpty(cuerpo(i))) Then
                oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i))))
            Else
                oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
            End If
            oTablaBasica.sbTabla.Append(TablaBasica.FilaFin)
        Next i

        oFileContent.Replace(PalabraClave, _
                             oTablaBasica.sbTabla.ToString)
    End Sub

    ''' <summary>
    ''' Inserta una tabla en el lugar indicado por la palabra clave. Inserta una columna adicional a la izquierda de la tabla,
    ''' con un margen indicado, que sirva como desplazamiento.
    ''' Sólo puede haber una palabra clave por cada tabla a reemplazar.
    ''' </summary>
    ''' <param name="PalabraClave">Texto a buscar y reemplazar por la tabla.</param>
    ''' <param name="cuerpo">Matriz con el contenido a ingresar en la tabla.</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub Tabla(ByVal PalabraClave As String, _
                     ByVal cuerpo As String())
        Dim rows As Integer
        Dim oTablaBasica As New TablaBasica

        ' Leer cuantas filas y columnas va a tener la tabla
        rows = cuerpo.GetLength(0)

        For i As Integer = 0 To rows - 1
            oTablaBasica.sbTabla.AppendLine(TablaBasica.InicioFila)

            Dim definicionFila As New StringBuilder
            definicionFila.AppendLine(TablaBasica.PrefijoFila + (_width).ToString())
            oTablaBasica.sbTabla.Append(definicionFila)

            If Not (IsNothing(cuerpo(i)) OrElse String.IsNullOrEmpty(cuerpo(i))) Then
                oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i))))
            Else
                oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
            End If
            oTablaBasica.sbTabla.Append(TablaBasica.FilaFin)
        Next i

        oFileContent.Replace(PalabraClave, _
                             oTablaBasica.sbTabla.ToString)
    End Sub

    ''' <summary>
    ''' Inserta una tabla en el lugar indicado por la palabra clave.
    ''' Sólo puede haber una palabra clave por cada tabla a reemplazar.
    ''' </summary>
    ''' <param name="PalabraClave">Texto a buscar y reemplazar por la tabla</param>
    ''' <param name="cuerpo">Matriz con el contenido a ingresar en la tabla</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub Tabla(ByVal margenIzquierdo As Integer, _
                     ByVal PalabraClave As String, _
                     ByVal cuerpo As String(,))
        Dim rows As Integer, cols As Integer
        Dim anchoColumna As Integer

        Dim oTablaBasica As New TablaBasica

        ' Leer cuantas filas y columnas va a tener la tabla
        rows = cuerpo.GetLength(0)
        cols = cuerpo.GetLength(1)
        anchoColumna = (_width - margenIzquierdo) \ (cols - 1)

        For i As Integer = 0 To rows - 1
            oTablaBasica.sbTabla.AppendLine(TablaBasica.InicioFila)

            Dim definicionFilas As New StringBuilder
            definicionFilas.AppendLine(TablaBasica.PrefijoFila + margenIzquierdo.ToString())
            For j As Integer = 1 To cols - 1
                definicionFilas.AppendLine(TablaBasica.PrefijoFila + (j * anchoColumna).ToString())
            Next j
            oTablaBasica.sbTabla.Append(definicionFilas)

            For j As Integer = 0 To cols - 1
                If Not (IsNothing(cuerpo(i, j)) OrElse String.IsNullOrEmpty(cuerpo(i, j))) Then
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i, j))))
                Else
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
                End If
            Next j
            oTablaBasica.sbTabla.Append(TablaBasica.FilaFin)
        Next i

        oFileContent.Replace(PalabraClave, _
                             oTablaBasica.sbTabla.ToString)

    End Sub

    ''' <summary>
    ''' Inserta una tabla en el lugar indicado por la palabra clave.
    ''' Sólo puede haber una palabra clave por cada tabla a reemplazar.
    ''' </summary>
    ''' <param name="PalabraClave">Texto a buscar y reemplazar por la tabla</param>
    ''' <param name="cuerpo">Matriz con el contenido a ingresar en la tabla</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub Tabla(ByVal PalabraClave As String, _
                     ByVal cuerpo As String(,))
        Dim rows As Integer, cols As Integer
        Dim anchoColumna As Integer

        Dim oTablaBasica As New TablaBasica

        ' Leer cuantas filas y columnas va a tener la tabla
        rows = cuerpo.GetLength(0)
        cols = cuerpo.GetLength(1)
        anchoColumna = _width \ cols

        For i As Integer = 0 To rows - 1
            oTablaBasica.sbTabla.AppendLine(TablaBasica.InicioFila)

            Dim definicionFilas As New StringBuilder
            For j As Integer = 1 To cols
                definicionFilas.AppendLine(TablaBasica.PrefijoFila + (j * anchoColumna).ToString())
            Next j
            oTablaBasica.sbTabla.Append(definicionFilas)

            For j As Integer = 0 To cols - 1
                If Not (IsNothing(cuerpo(i, j)) OrElse String.IsNullOrEmpty(cuerpo(i, j))) Then
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i, j))))
                Else
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
                End If
            Next j
            oTablaBasica.sbTabla.Append(TablaBasica.FilaFin)
        Next i

        oFileContent.Replace(PalabraClave, _
                             oTablaBasica.sbTabla.ToString)

    End Sub

    ''' <summary>
    ''' Inserta una tabla RTF en la ubicación indicada en PalabraClave.
    ''' </summary>
    ''' <param name="PalabraClave"></param>
    ''' <param name="cuerpo"></param>
    ''' <remarks></remarks>
    Public Sub Tabla(ByVal PalabraClave As String, _
                     ByVal cuerpo As Celda(,))
        Dim rows As Integer, cols As Integer
        Dim anchoColumna As Integer

        Dim oTablaBasica As New TablaBasica

        ' Leer cuantas filas y columnas va a tener la tabla
        rows = cuerpo.GetLength(0)
        cols = cuerpo.GetLength(1)
        anchoColumna = _width \ cols

        For i As Integer = 0 To rows - 1
            oTablaBasica.sbTabla.AppendLine(TablaBasica.InicioFila)

            ' incluye la definición de los bordes de cada celda.
            Dim definicionFilas As New StringBuilder
            For j As Integer = 1 To cols
                definicionFilas.AppendLine(GetBordes(cuerpo(i, j - 1)) + TablaBasica.PrefijoFila + (j * anchoColumna).ToString())
            Next j
            oTablaBasica.sbTabla.Append(definicionFilas)

            For j As Integer = 0 To cols - 1
                ' Si contiene algun texto se le asigna el formato
                If Not ((cuerpo(i, j).TextoCelda.Text.Length = 0) OrElse _
                    String.IsNullOrEmpty(cuerpo(i, j).TextoCelda.Text.ToString())) Then
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i, j).TextoCelda.Text.ToString()), _
                                                                   cuerpo(i, j).TextoCelda.Alignment, _
                                                                   cuerpo(i, j).TextoCelda.Bold))
                Else
                    ' Sino sólo texto vacío
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
                End If
            Next j
            oTablaBasica.sbTabla.Append(TablaBasica.FilaFin)
        Next i

        oFileContent.Replace(PalabraClave, _
                             oTablaBasica.sbTabla.ToString)

    End Sub

    ''' <summary>
    ''' Devuelve la descripción de los bordes (formato RTF) de la celda.
    ''' </summary>
    ''' <param name="oCelda"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function GetBordes(ByVal oCelda As Celda) As String
        Dim bordes As String
        Dim celltop As String = ""
        Dim cellleft As String = ""
        Dim cellbottom As String = ""
        Dim cellright As String = ""

        ' Bordes de la celda: "\clbrdrt\brdrs\clbrdrl\brdrs\clbrdrb\brdrs\clbrdrr\brdrs "
        If oCelda.Bordes.BorderTop = LineStyle.LineStyleSingle Then
            celltop = "\clbrdrt\brdrs"
        End If
        If oCelda.Bordes.BorderLeft = LineStyle.LineStyleSingle Then
            cellleft = "\clbrdrl\brdrs"
        End If
        If oCelda.Bordes.BorderRight = LineStyle.LineStyleSingle Then
            cellright = "\clbrdrr\brdrs"
        End If
        If oCelda.Bordes.BorderBottom = LineStyle.LineStyleSingle Then
            cellbottom = "\clbrdrb\brdrs"
        End If
        bordes = celltop + cellleft + cellbottom + cellright

        Return bordes + " "
    End Function

    ''' <summary>
    ''' Inserta una tabla en el lugar indicado por la palabra clave.
    ''' Sólo puede haber una palabra clave por cada tabla a reemplazar.
    ''' </summary>
    ''' <param name="PalabraClave">Texto a buscar y reemplazar por la tabla</param>
    ''' <param name="cuerpo">Matriz con el contenido a ingresar en la tabla</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub TablaCronograma(ByVal PalabraClave As String, _
                     ByVal cuerpo As String(,))
        Dim rows As Integer, cols As Integer
        Dim anchoColumna As Integer
        Dim intContador As Integer = 0

        Dim oTablaBasica As New TablaBasica

        ' Leer cuantas filas y columnas va a tener la tabla
        rows = cuerpo.GetLength(0)
        cols = cuerpo.GetLength(1)
        anchoColumna = _width \ cols

        For i As Integer = 0 To rows - 1
            oTablaBasica.sbTabla.AppendLine(TablaBasica.InicioFila)

            Dim definicionFilas As New StringBuilder
            For j As Integer = 1 To cols
                definicionFilas.AppendLine(TablaBasica.PrefijoFila + (j * anchoColumna).ToString())
            Next j
            oTablaBasica.sbTabla.Append(definicionFilas)

            For j As Integer = 0 To cols - 1
                If Not (IsNothing(cuerpo(i, j)) OrElse String.IsNullOrEmpty(cuerpo(i, j))) Then
                    If intContador < cols Then
                        oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i, j)), 2, False))
                    Else
                        oTablaBasica.sbTabla.Append(oTablaBasica.Celda(TextoRtf(cuerpo(i, j)), 2, False))
                    End If
                Else
                    oTablaBasica.sbTabla.Append(oTablaBasica.Celda(""))
                End If
                intContador = intContador + 1
            Next j
            oTablaBasica.sbTabla.Append(TablaBasica.FilaFin)
        Next i

        oFileContent.Replace(PalabraClave, _
                             oTablaBasica.sbTabla.ToString)

    End Sub

    ''' <summary>
    ''' Guarda y cierra el documento, contenido en un StreamWriter.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub SaveAndClose()
        Dim oStreamWriter As New StreamWriter(sFileName)

        oStreamWriter.Write(oFileContent)

        ' had fs.Flush which flushed the FileStream, but you needed to Flush the StreamWriter.
        oStreamWriter.Flush()
        ' had fs.Close which closed the FileStream, but you needed to Close the StreamWriter.
        oStreamWriter.Close()

        oFileStream.Close()
    End Sub

    Public Sub Close()
        oFileStream.Close()
    End Sub



    Public Function fRetornarStringDocumento() As String
        Return oFileContent.ToString
    End Function

#End Region

#Region "Constructores"

    ''' <summary>
    ''' Constructor predeterminado.
    ''' </summary>
    ''' <param name="template">Nombre de la plantilla a utilizar para generar el documento</param>
    ''' <param name="fileName">Archivo y ruta en la que se va a generar el documento</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub New(ByVal template As String, _
                   ByVal fileName As String)
        ' Ancho predefinido de la tabla
        Width = 9000

        sFileName = fileName
        ' Elimina cualquier versión anterior del archivo
        Call GCCUtilitario.Eliminar(sFileName)

        ' Hacer una copia de la plantilla
        File.Copy(template, sFileName)

        ' Quitar la propiedad de sólo lectura al nuevo archivo.
        Dim oFileInfo As New FileInfo(sFileName)
        oFileInfo.IsReadOnly = False

        ' Crear un flujo de archivo en modo lectura-escritura.
        oFileStream = New FileStream(sFileName, _
                                     FileMode.Open, _
                                     FileAccess.ReadWrite)

        Dim oStreamReader As New StreamReader(oFileStream)

        ' Leer el contenido predefinido de la plantilla y cargarlo en memoria.
        oFileContent = New StringBuilder(oStreamReader.ReadToEnd())

        ' Close the Reader
        oStreamReader.Close()
    End Sub

#End Region

#Region "Clase internas"

    ''' <summary>
    ''' Representa una tabla básica de un documento Rich Text Format, sin bordes.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Class TablaBasica

        Public Const InicioFila As String = "\trowd \trautofit1"
        Public Const FilaFin As String = "\row"

        Public Const PrefijoFila As String = "\cellx"

        Private Const CeldaInicio As String = "\pard\intbl"
        Private Const CeldaFin As String = "\cell"

        Public ReadOnly sbTabla As StringBuilder

        ''' <summary>
        ''' Permite ingresar un texto y retorna una celda rtf, con el texto indicado (sin ningún formato en el texto o la celda).
        ''' </summary>
        ''' <param name="dato"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 22/02/2011
        ''' </remarks>
        Public Function Celda(ByVal dato As String) As String
            Return CeldaInicio + "{" + dato + "}" + CeldaFin
        End Function

        ''' <summary>
        ''' Permite ingresar un texto y retorna una celda rtf, con el texto y la configuración indicada.
        ''' </summary>
        ''' <param name="dato">Texto a ingresar en la celda</param>
        ''' <param name="alinear">Alineación del texto dentro de la celda.</param>
        ''' <param name="Negrita">El texto estará en negrita.</param>
        ''' <param name="BorderInf">Tipo de línea del borde inferior.</param>
        ''' <param name="BorderSup">Tipo de línea del borde superior.</param>
        ''' <param name="BorderIzq">Tipo de línea del borde izquierdo.</param>
        ''' <param name="BorderDer">Tipo de línea del borde derecho.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Creado Por         : TSF - EBL
        ''' Fecha de Creación  : 22/02/2011
        ''' </remarks>
        Public Function Celda(ByVal dato As String, _
                              ByVal alinear As ParagraphAlignment, _
                              Optional ByVal Negrita As Boolean = False, _
                              Optional ByVal BorderInf As LineStyle = LineStyle.LineStyleNone, _
                              Optional ByVal BorderSup As LineStyle = LineStyle.LineStyleNone, _
                              Optional ByVal BorderIzq As LineStyle = LineStyle.LineStyleNone, _
                              Optional ByVal BorderDer As LineStyle = LineStyle.LineStyleNone) As String
            Dim etiquetaAlineado As String

            Dim etiquetaNegritaCierre As String = ""
            Dim etiquetaNegrita As String = ""

            ' Alineación del texto dentro de la celda
            Select Case alinear
                Case ParagraphAlignment.AlignParagraphLeft
                    etiquetaAlineado = "\ql "
                Case ParagraphAlignment.AlignParagraphCenter
                    etiquetaAlineado = "\qc "
                Case ParagraphAlignment.AlignParagraphRight
                    etiquetaAlineado = "\qr "
                Case Else
                    etiquetaAlineado = "\ql "
            End Select

            If Negrita Then
                etiquetaNegrita = "\b "
                etiquetaNegritaCierre = "\b0 "
            End If


            Return CeldaInicio + etiquetaAlineado + etiquetaNegrita + "{" + dato + "}" + etiquetaNegritaCierre + CeldaFin
        End Function

        ''' <summary>
        ''' Constructor por defecto
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.new()

            sbTabla = New StringBuilder

        End Sub

    End Class

#End Region

End Class


''' <summary>
''' Representa un texto, configurado en formato RTF
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Class Texto


#Region "Campos"

    Public Bold As Boolean
    Public Alignment As ParagraphAlignment
    Public ForegroundColor As ForegroundPatternColor

    Public Text As StringBuilder

#End Region

#Region "Constructores"

    ''' <summary>
    ''' Configura los valores predeterminados de un texto RTF.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub New()
        MyBase.new()

        Bold = False
        Alignment = ParagraphAlignment.AlignParagraphLeft
        ForegroundColor = ForegroundPatternColor.ColorAutomatic
        Text = New StringBuilder
    End Sub

#End Region

End Class

''' <summary>
''' Paleta de colores de fondo disponible
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Enum ForegroundPatternColor

    ColorAutomatic
    Oscuro15Porc
End Enum

''' <summary>
''' Estilos de línea disponibles
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Enum LineStyle

    LineStyleNone
    LineStyleSingle
End Enum

''' <summary>
''' Lista de bordes de una celda
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Structure Borders

    Dim BorderBottom As LineStyle
    Dim BorderTop As LineStyle
    Dim BorderLeft As LineStyle
    Dim BorderRight As LineStyle
End Structure

''' <summary>
''' Lista de alineamientos disponibles.
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Enum ParagraphAlignment

    AlignParagraphCenter
    AlignParagraphLeft
    AlignParagraphRight
End Enum

''' <summary>
''' Representa una celda de documento RTF.
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Class Celda

    Public Bordes As Borders
    Public TextoCelda As Texto


    Public Sub New()

        Bordes = New Borders
        TextoCelda = New Texto
    End Sub

    ''' <summary>
    ''' Devuelve el texto celda con el texto indicado en el parámetro.
    ''' </summary>
    ''' <param name="texto"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Shared Function Asignar(ByVal texto As String) As Celda
        Dim oCelda As New Celda

        oCelda.TextoCelda.Text.Append(texto)

        Return oCelda
    End Function

    ''' <summary>
    ''' Devuelve un objeto celda con la configuración indicada en el parámetro
    ''' </summary>
    ''' <param name="texto"></param>
    ''' <param name="negrita"></param>
    ''' <param name="alinear"></param>
    ''' <param name="BorderInf"></param>
    ''' <param name="BorderSup"></param>
    ''' <param name="BorderIzq"></param>
    ''' <param name="BorderDer"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Shared Function Asignar(ByVal texto As String, _
                                   ByVal negrita As Boolean, _
                                   ByVal alinear As ParagraphAlignment, _
                                   Optional ByVal BorderInf As LineStyle = LineStyle.LineStyleNone, _
                                   Optional ByVal BorderSup As LineStyle = LineStyle.LineStyleNone, _
                                   Optional ByVal BorderIzq As LineStyle = LineStyle.LineStyleNone, _
                                   Optional ByVal BorderDer As LineStyle = LineStyle.LineStyleNone) As Celda
        Dim oCelda As New Celda

        oCelda.TextoCelda.Text.Append(texto)

        oCelda.TextoCelda.Alignment = alinear
        oCelda.TextoCelda.Bold = negrita

        oCelda.Bordes.BorderBottom = BorderInf
        oCelda.Bordes.BorderTop = BorderSup
        oCelda.Bordes.BorderLeft = BorderIzq
        oCelda.Bordes.BorderRight = BorderDer

        Return oCelda
    End Function

    ''' <summary>
    ''' Elimina el contenido de la celda (texto)
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub Clear()

        TextoCelda.Text.Length = 0
        TextoCelda.Text.Capacity = 0
    End Sub

    ''' <summary>
    ''' Devuelve una cadena con el texto del objeto Texto contenido en la celda.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Overloads Function ToString() As String
        Return TextoCelda.ToString
    End Function

End Class
