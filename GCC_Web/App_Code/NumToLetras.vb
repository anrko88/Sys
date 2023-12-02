﻿Option Strict Off
Option Explicit On
Option Compare Text

Imports Microsoft.VisualBasic


''' <summary>
''' Clase para convertir números a letras.
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 22/02/2011
''' </remarks>
Public Class NumToLetras


#Region "Campos"

#Region "Privados"

    Dim unidad(9) As String
    Dim decena(9) As String
    Dim centena(10) As String
    Dim deci(9) As String
    Dim otros(15) As String

    Private _mSexo1 As String
    Private _mSexo2 As String
    Private _mLenSexo1 As Long

#End Region

#Region "Protegidos"


#End Region

#Region "Publicos"

    Public Enum Sexo

        Femenino = 0
        Masculino = 1
    End Enum

#End Region

#End Region

#Region "Propiedades"


#End Region

#Region "Métodos y funciones"

    ''' <summary>
    ''' Convierte un número en su respectiva descripción en texto. Si el número tiene parte decimal lo concatena
    ''' con el infijo " y " junto a la parte decimal "/100", si el decimal es mayor a dos digitos, lo trunca a dos.
    ''' </summary>
    ''' <param name="strNum">Numeral a convertir en formato cadena</param>
    ''' <param name="Lo"></param>
    ''' <param name="NumDecimales"></param>
    ''' <param name="sMoneda"></param>
    ''' <param name="sCentimos"></param>
    ''' <param name="SexoMoneda"></param>
    ''' <param name="SexoCentimos"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function NumeroALetraBancaria(ByVal strNum As String, _
                                         Optional ByVal Lo As Integer = 0, _
                                         Optional ByVal NumDecimales As Integer = 2, _
                                         Optional ByVal sMoneda As String = "", _
                                         Optional ByVal sCentimos As String = "", _
                                         Optional ByVal SexoMoneda As Sexo = Sexo.Masculino, _
                                         Optional ByVal SexoCentimos As Sexo = Sexo.Masculino) As String
        Dim vPartesNumero() As String
        Dim parteEntera As String
        Dim parteDecimal As String

        vPartesNumero = strNum.Split(New Char() {"."c})

        parteEntera = Numero2Letra(CDbl(vPartesNumero(0)).ToString(), _
                                   Lo, _
                                   0, _
                                   sMoneda, _
                                   sCentimos, _
                                   SexoMoneda, _
                                   SexoCentimos)

        parteDecimal = vPartesNumero(1).PadRight(2, Convert.ToChar("0")) + "/100"

        Return parteEntera + " y " + parteDecimal
    End Function

    ''' <summary>
    ''' Convierte el número strNum en letras, permite indicar los centimos.
    ''' </summary>
    ''' <param name="strNum"></param>
    ''' <param name="Lo"></param>
    ''' <param name="NumDecimales">Se puede indicar el número de decimales a devolver, por defecto son dos.</param>
    ''' <param name="sMoneda">Nombre de la moneda</param>
    ''' <param name="sCentimos"></param>
    ''' <param name="SexoMoneda"></param>
    ''' <param name="SexoCentimos"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function Numero2Letra(ByVal strNum As String, _
                                 Optional ByVal Lo As Integer = 0, _
                                 Optional ByVal NumDecimales As Integer = 2, _
                                 Optional ByVal sMoneda As String = "", _
                                 Optional ByVal sCentimos As String = "", _
                                 Optional ByVal SexoMoneda As Sexo = Sexo.Femenino, _
                                 Optional ByVal SexoCentimos As Sexo = Sexo.Masculino) As String
        '----------------------------------------------------------
        ' Convierte el número strNum en letras          (28/Feb/91)
        ' Versión para Windows                          (25/Oct/96)
        ' Variables estáticas                           (15/May/97)
        ' Parche de "Esteve" <esteve@mur.hnet.es>       (20/May/97)
        ' Revisión para decimales                       (10/Jul/97)
        ' Permite indicar el sexo de la moneda          (06/Ene/99)
        ' y de los centimos... nunca se sabe...
        ' Corregido fallo de los decimales cuando       (13/Ene/99)
        ' tienen ceros a la derecha.
        '
        ' La moneda debe especificarse en singular, ya que la función
        ' se encarga de convertirla en plural.
        ' Se puede indicar el número de decimales a devolver
        ' por defecto son dos.
        '----------------------------------------------------------
        Dim iHayDecimal As Integer    ' Posición del signo decimal
        Dim sDecimal As String = ""   ' Signo decimal a usar
        Dim sDecimalNo As String = "" 'Signo no decimal
        Dim sEntero As String
        Dim sFraccion As String
        Dim fFraccion As Single
        Dim sNumero As String
        Static sexoAntMoneda As Sexo
        Dim sSexoCents As String
        '
        ' Para tener en cuenta el sexo de los céntimos                  (20/Jul/00)
        ' m_Sexo2 se usa para indicar el plural de las monedas,
        ' sSexoCents sustituirá a esa variable cuando se calculen los céntimos
        If SexoCentimos = Sexo.Femenino Then
            sSexoCents = "as"
        Else
            sSexoCents = "os"
        End If
        '
        'Dependiendo del "sexo" indicado, usar las terminaciones
        If SexoMoneda = Sexo.Femenino Then
            _mSexo1 = "a"
            _mSexo2 = "as"
        Else
            _mSexo1 = ""
            _mSexo2 = "os"
        End If
        'por si se cambia en el trascurso el sexo de la moneda
        If SexoMoneda <> sexoAntMoneda Then
            unidad(1) = "" ' Aquí ponía: unidad(2) = ""                 (08/Sep/01)
            sexoAntMoneda = SexoMoneda
        End If
        _mLenSexo1 = Len(_mSexo1)

        'Si se especifica, se usarán
        sMoneda = Trim(sMoneda)
        If Len(Trim(sMoneda)) Then
            sMoneda = " " & sMoneda & " "
        Else
            sMoneda = " "
        End If

        sCentimos = Trim(sCentimos)
        If Len(Trim(sCentimos)) Then
            sCentimos = " " & sCentimos & " "
        Else
            sCentimos = " "
        End If

        'Si no se especifica el ancho...
        '
        If Lo Then
            sNumero = Space(Lo)
        Else
            sNumero = ""
        End If

        'Comprobar el signo decimal y devolver los adecuados a la config. regional
        strNum = ConvDecimal(strNum, sDecimal, sDecimalNo)

        'Comprobar si tiene decimales
        iHayDecimal = InStr(strNum, sDecimal)
        If iHayDecimal Then
            sEntero = Left(strNum, iHayDecimal - 1)
            sFraccion = Mid(strNum, iHayDecimal + 1) & New String("0", NumDecimales)
            'obligar a que tenga dos cifras
            '
            'pero habría que redondear el resto...
            'por ejemplo:
            '   .256 sería .26 y
            '   .254 sería .25
            'Pero esto otro no se haría:
            '.25499 no pasaría a .255 y después a .26

            '
            ' NO hacer cálculos de redondeo ni nada de nada             (08/Jul/00)
            '
            ' De esta forma se dirá:
            '   ,06 con seis
            '   ,50 con cincuenta
            sFraccion = Left(sFraccion, NumDecimales)
            '
            '* En las fracciones los ceros a la derecha no tienen significado
            '----------------------------------------------------------------------
            ' Pero si tenemos: 125.50 si que tiene significado,         (08/Jul/00)
            ' ya que tal y como está ahora, diría con 5 en lugar de cincuenta
            ' Así que si se ponen NumDecimales mayor de 2,
            ' hay que ser consecuentes con los resultados.
            '----------------------------------------------------------------------
            fFraccion = Val(sFraccion)
            ' Si no hay decimales... no agregar nada...
            If fFraccion < 1 Then
                If Len(Trim(sMoneda)) Then
                    sMoneda = Pluralizar(sNumero, sMoneda)
                End If
                strNum = RTrim(UnNumero(sEntero, _mSexo1) & sMoneda)
                If Lo Then
                    sNumero = LSet(strNum, Len(sNumero))
                Else
                    sNumero = strNum
                End If
                Numero2Letra = sNumero
                Exit Function
            End If

            If Len(Trim(sMoneda)) Then
                sMoneda = Pluralizar(sEntero, sMoneda)
            End If

            sEntero = UnNumero(sEntero, _mSexo1)

            If Len(Trim(sCentimos)) Then
                sCentimos = Pluralizar(sFraccion, sCentimos)
            End If

            ' Para el sexo de los decimales
            ' no se si esto puede cambiar, pero por si ocurre...
            '
            ' Sustituimos el plural de las monedas,                     (20/Jul/00)
            ' para adecuarla a los céntimos,
            ' ya que en España, la moneda es femenino, pero los céntimos masculino.
            _mSexo2 = sSexoCents
            If SexoCentimos = Sexo.Masculino Then
                sFraccion = UnNumero(sFraccion, "")
            Else
                sFraccion = UnNumero(sFraccion, "a")
            End If
            '
            strNum = sEntero & sMoneda & "con " & sFraccion & sCentimos
            If Lo Then
                sNumero = LSet(RTrim(strNum), Len(sNumero))
            Else
                sNumero = RTrim(strNum)
            End If
            Numero2Letra = sNumero
        Else
            If Len(Trim(sMoneda)) Then
                sMoneda = Pluralizar(strNum, sMoneda)
            End If
            strNum = RTrim(UnNumero(strNum, _mSexo1) & sMoneda)
            If Lo Then
                sNumero = LSet(strNum, Len(sNumero))
            Else
                sNumero = strNum
            End If
            Numero2Letra = sNumero
        End If
    End Function

    Private Function UnNumero(ByVal strNum As String, _
                              ByVal Sexo1 As String) As String
        '----------------------------------------------------------
        'Esta es la rutina principal                    (10/Jul/97)
        'Está separada para poder actuar con decimales
        '----------------------------------------------------------
        Dim dblNumero As Double

        Dim negativo As Boolean
        Dim l As Short
        Dim Una As Boolean
        Dim millon As Boolean
        Dim millones As Boolean
        Dim vez As Integer
        Dim maxVez As Integer
        Dim k As Integer
        Dim strQ As String
        Dim strB As String
        Dim strU As String
        Dim strD As String
        Dim strC As String
        Dim iA As Integer

        Dim strN() As String
        Dim sexo1Ant As String

        'Si se amplia este valor... no se manipularán bien los números
        Const cAncho As Short = 12
        Const cGrupos As Short = cAncho \ 3

        'Por si se especifica el sexo, para el caso de los decimales
        'que siempre será masculino
        sexo1Ant = _mSexo1
        _mSexo1 = Sexo1

        _mLenSexo1 = Len(_mSexo1)
        '
        ' idea aportada por Harvey Triana
        ' para no tener que estar reinicializando continuamente los arrays
        '
        ' Se ve que lo anterior fallaba si se usaba varias veces seguidas (05/Mar/99)
        If unidad(1) <> "un" & Sexo1 Then
            InicializarArrays()
        End If

        'Si se produce un error que se pare el mundo!!!
        On Error GoTo 0

        If Len(strNum) = 0 Then
            strNum = "0"
        End If

        dblNumero = Math.Abs(CDbl(strNum))
        negativo = (dblNumero <> CDbl(strNum))
        strNum = LTrim(RTrim(Str(dblNumero)))
        l = Len(strNum)

        If dblNumero < 1 Then
            UnNumero = "cero"
            Exit Function
        End If
        '
        Una = True
        millon = False
        millones = False
        If l < 4 Then Una = False
        If dblNumero > 999999 Then millon = True
        If dblNumero > 1999999 Then millones = True
        strB = ""
        vez = 0

        ReDim strN(cGrupos)
        strQ = Right(New String("0", cAncho) & strNum, cAncho)
        For k = Len(strQ) To 1 Step -3
            vez = vez + 1
            strN(vez) = Mid(strQ, k - 2, 3)
        Next
        maxVez = cGrupos
        For k = cGrupos To 1 Step -1
            If strN(k) = "000" Then
                maxVez = maxVez - 1
            Else
                Exit For
            End If
        Next
        For vez = 1 To maxVez
            strU = ""
            strD = ""
            strC = ""
            strNum = strN(vez)
            l = Len(strNum)
            k = Val(Right(strNum, 2))
            If Right(strNum, 1) = "0" Then
                k = k \ 10
                strD = decena(k)
            ElseIf k > 10 And k < 16 Then
                k = Val(Mid(strNum, l - 1, 2))
                strD = otros(k)
            Else
                strU = unidad(Val(Right(strNum, 1)))
                If l - 1 > 0 Then
                    k = Val(Mid(strNum, l - 1, 1))
                    strD = deci(k)
                End If
            End If
            '---Parche de Esteve
            If l - 2 > 0 Then
                k = Val(Mid(strNum, l - 2, 1))
                'Con esto funcionará bien el 100100, por ejemplo...
                If k = 1 Then
                    If Val(strNum) = 100 Then
                        k = 10
                    End If
                End If
                strC = centena(k) & " "
            End If
            '------
            If strU = "uno" And Left(strB, 4) = " mil" Then strU = ""
            strB = strC & strD & strU & " " & strB

            If (vez = 1 Or vez = 3) Then
                If strN(vez + 1) <> "000" Then strB = " mil " & strB
            End If
            If vez = 2 And millon Then
                If millones Then
                    strB = " millones " & strB
                Else
                    strB = "un millón " & strB
                End If
            End If
        Next
        strB = Trim(strB)
        If Right(strB, 3) = "uno" Then
            strB = Left(strB, Len(strB) - 1) & _mSexo1 '"a"
        End If
        Do  'Quitar los espacios dobles que haya por medio
            iA = InStr(strB, "  ")
            If iA = 0 Then Exit Do
            strB = Left(strB, iA - 1) & Mid(strB, iA + 1)
        Loop
        '
        If Left(strB, 5 + _mLenSexo1) = "un" & _mSexo1 & " un" Then
            strB = Mid(strB, 4 + _mLenSexo1)
        End If
        '---Nueva comparación                                     (01:16 25/Ene/99)
        If Left(strB, 5) = "un un" Then
            strB = Mid(strB, 4)
        End If
        '
        ' Comprobar sólo si se especifica "un* mil ",                   (05/Mar/99)
        ' no "un* mil" ya que puede ser "un* millón"
        If Left(strB, 7 + _mLenSexo1) = "un" & _mSexo1 & " mil " Then
            strB = Mid(strB, 4 + _mLenSexo1)
            ' Puede que el importe sea sólo "un mil" o "una mil"        (19/Ago/00)
        ElseIf strB = "un" & _mSexo1 & " mil" Then
            strB = Mid(strB, 4 + _mLenSexo1)
        End If
        '
        '---Nueva comparación                                     (15:11 25/Ene/99)
        ' Que debe estar así, para que no quite "un millón"             (05/Mar/99)
        If Left(strB, 7) = "un mil " Then
            strB = Mid(strB, 4)
        End If
        '
        If Right(strB, 15 + _mLenSexo1) <> "millones mil un" & _mSexo1 Then
            iA = InStr(strB, "millones mil un" & _mSexo1)
            If iA Then strB = Left(strB, iA + 8) & Mid(strB, iA + 13)
        End If
        '---Nueva comparación                                      (15:13 25/Ene/99)
        If Right(strB, 15) <> "millones mil un" Then
            iA = InStr(strB, "millones mil un")
            If iA Then strB = Left(strB, iA + 8) & Mid(strB, iA + 13)
        End If
        '
        ' De algo sirve que la gente pruebe las rutinas...              (05/Mar/99)
        If millones Then
            ' Comprobación de -as ??? millones
            ' convertir en -os ??? millones
            ' Pero sólo si el sexo es femenino
            If _mSexo1 = "a" Then
                ' Usar un bucle Do por si hay varias coincidencias      (07/Dic/00)
                Do While (strB Like "*as * millones*")
                    ' Buscar la primera terminación "as " y cambiar por "os "
                    k = InStr(strB, "as ")
                    If k Then
                        Mid(strB, k) = "os "
                    End If
                Loop
                ' La comparación anterior no funciona con x00 millones  (30/Jun/00)
                ' Usar un bucle Do por si hay varias coincidencias      (07/Dic/00)
                Do While (strB Like "*as millones*")
                    ' Buscar la primera terminación "as " y cambiar por "os "
                    k = InStr(strB, "as millones")
                    If k Then
                        Mid(strB, k) = "os millones"
                    End If
                Loop

                '------------------------------------------------------------------
                ' Comprobar si dice algo así ...una millones            (08/Jul/00)
                ' Por ejemplo en 821.xxx.xxx decia ochocientos veintiuna millones
                '------------------------------------------------------------------
                k = InStr(strB, "una mill")
                If k Then
                    strB = Left(strB, k + 1) & Mid(strB, k + 3)
                End If
                '
            End If
        End If
        '
        '--------------------------------------------------------------------------
        ' Cambiar los veintiun por veintiún, etc por sus acentuadas     (08/Jul/00)
        Do
            k = InStr(strB, "veintiun ")
            If k Then
                Mid(strB, k) = "veintiún "
            End If
        Loop While k
        ' El veintidos creo que nunca lo he acentuado...                (08/Jul/00)
        ' pero en la enciclopedia consultada lo acentúa
        Do
            k = InStr(strB, "veintidos ")
            If k Then
                Mid(strB, k) = "veintidós "
            End If
        Loop While k
        Do
            k = InStr(strB, "veintitres ")
            If k Then
                Mid(strB, k) = "veintitrés "
            End If
        Loop While k
        Do
            k = InStr(strB, "veintiseis ")
            If k Then
                Mid(strB, k) = "veintiséis "
            End If
        Loop While k
        '--------------------------------------------------------------------------
        '
        '
        If Right(strB, 6) = "ciento" Then
            strB = Left(strB, Len(strB) - 2)
        End If
        If negativo Then strB = "menos " & strB

        UnNumero = Trim(strB)

        ' Restablecer el valor anterior
        _mSexo1 = sexo1Ant
        _mLenSexo1 = Len(_mSexo1)
    End Function

    Private Sub InicializarArrays()
        'Asignar los valores
        unidad(1) = "un" & _mSexo1
        unidad(2) = "dos"
        unidad(3) = "tres"
        unidad(4) = "cuatro"
        unidad(5) = "cinco"
        unidad(6) = "seis"
        unidad(7) = "siete"
        unidad(8) = "ocho"
        unidad(9) = "nueve"
        '
        decena(1) = "diez"
        decena(2) = "veinte"
        decena(3) = "treinta"
        decena(4) = "cuarenta"
        decena(5) = "cincuenta"
        decena(6) = "sesenta"
        decena(7) = "setenta"
        decena(8) = "ochenta"
        decena(9) = "noventa"
        '
        centena(1) = "ciento"
        centena(2) = "doscient" & _mSexo2
        centena(3) = "trescient" & _mSexo2
        centena(4) = "cuatrocient" & _mSexo2
        centena(5) = "quinient" & _mSexo2
        centena(6) = "seiscient" & _mSexo2
        centena(7) = "setecient" & _mSexo2
        centena(8) = "ochocient" & _mSexo2
        centena(9) = "novecient" & _mSexo2
        centena(10) = "cien"
        '
        deci(1) = "dieci"
        deci(2) = "veinti"
        deci(3) = "treinta y "
        deci(4) = "cuarenta y "
        deci(5) = "cincuenta y "
        deci(6) = "sesenta y "
        deci(7) = "setenta y "
        deci(8) = "ochenta y "
        deci(9) = "noventa y "
        '
        otros(1) = "1"
        otros(2) = "2"
        otros(3) = "3"
        otros(4) = "4"
        otros(5) = "5"
        otros(6) = "6"
        otros(7) = "7"
        otros(8) = "8"
        otros(9) = "9"
        otros(10) = "10"
        otros(11) = "once"
        otros(12) = "doce"
        otros(13) = "trece"
        otros(14) = "catorce"
        otros(15) = "quince"
    End Sub

    ''' <summary>
    ''' Pluraliza la moneda, si el valor de número es distinto de uno
    ''' </summary>
    ''' <param name="sNumero"></param>
    ''' <param name="sMoneda"></param>
    ''' <param name="bCadaPalabra"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Pluralizar(ByVal sNumero As String, _
                               ByVal sMoneda As String, _
                               Optional ByVal bCadaPalabra As Boolean = False) As String
        '--------------------------------------------------------------------------
        '
        ' Parámetros:
        '   sNumero         Importe, para saber si hay que pluralizar o no
        '   sMoneda         Cadena con la palabra a pluralizar
        '   bCadaPalabra    Si se pluralizan todas las palabras         (08/Jul/00)
        '--------------------------------------------------------------------------
        Dim dblTotal As Double
        Dim sTmp As String
        Dim i As Integer

        If Len(Trim(sMoneda)) Then
            ' He quitado el Val             (08/Jul/00)
            'dblTotal = Val(sNumero)
            '
            ' Si entra una cadena vacia, da error                       (08/Jul/00)
            If Len(sNumero) = 0 Then
                sNumero = "0"
            End If
            dblTotal = CDbl(sNumero)
            '
            If dblTotal <> 1.0# Then
                sMoneda = Trim(sMoneda)
                ' Si se pluralizan todas las palabras                   (08/Jul/00)
                If bCadaPalabra Then
                    sMoneda = sMoneda & " "
                    sTmp = ""
                    For i = 1 To Len(sMoneda)
                        If Mid(sMoneda, i, 1) = " " Then
                            ' pluralizar
                            If InStr("aeiou", Right(sTmp, 1)) Then
                                sTmp = sTmp & "s"
                            Else
                                sTmp = sTmp & "es"
                            End If
                        End If
                        sTmp = sTmp & Mid(sMoneda, i, 1)
                    Next
                    sMoneda = " " & Trim(sTmp) & " "
                Else
                    If InStr("aeiou", Right(sMoneda, 1)) Then
                        sMoneda = " " & sMoneda & "s "
                    Else
                        sMoneda = " " & sMoneda & "es "
                    End If
                End If
            End If
        End If
        Pluralizar = sMoneda
    End Function

    Public Function ConvDecimal(ByVal strNum As String, _
                                Optional ByRef sDecimal As String = ",", _
                                Optional ByRef sDecimalNo As String = ".") As String
        ' Asigna el signo decimal adecuado (o lo intenta)               (10/Ene/99)
        ' Devuelve una cadena con el signo decimal del sistema
        Dim sNumero As String
        Dim i As Integer
        Dim j As Integer

        On Error Resume Next ' Si se produce un error, continuar (07/Jul/00)

        ' Averiguar el signo decimal
        sNumero = Format(25.5, "#.#")
        If InStr(sNumero, ".") Then
            sDecimal = "."
            sDecimalNo = ","
        Else
            sDecimal = ","
            sDecimalNo = "."
        End If

        strNum = Trim(strNum)
        If Left(strNum, 1) = sDecimalNo Then
            Mid(strNum, 1, 1) = sDecimal
        End If

        ' Si el número introducido contiene signos no decimales
        j = 0
        i = 1
        Do
            i = InStr(i, strNum, sDecimalNo)
            If i Then
                j = j + 1
                i = i + 1
            End If
        Loop While i

        If j = 1 Then
            ' cambiar ese símbolo por un espacio, si sólo hay uno de esos signos
            i = InStr(strNum, sDecimalNo)
            If i Then
                If InStr(strNum, sDecimal) Then
                    Mid(strNum, i, 1) = " "
                Else
                    Mid(strNum, i, 1) = sDecimal
                End If
            End If
        Else
            'En caso de que tenga más de uno de estos símbolos
            'convertirlos de manera adecuada.
            'Por ejemplo:
            'si el signo decimal es la coma:
            '   1,250.45 sería 1.250,45 y quedaría en 1250,45
            'si el signo decimal es el punto:
            '   1.250,45 sería 1,250.45 y quedaría en 1250.45
            '
            'Aunque no se arreglará un número erróneo:
            'si el signo decimal es la coma:
            '   1,250,45 será lo mismo que 1,25
            '   12,500.25 será lo mismo que 12,50
            'si el signo decimal es el punto:
            '   1.250.45 será lo mismo que 1.25
            '   12.500,25 será lo mismo que 12.50
            '
            i = 1
            Do
                i = InStr(i, strNum, sDecimalNo)
                If i Then
                    j = j - 1
                    If j = 0 Then
                        Mid(strNum, i, 1) = sDecimal
                    Else
                        Mid(strNum, i, 1) = " "
                    End If
                    i = i + 1
                End If
            Loop While i
        End If

        ' Quitar los espacios que haya por medio
        Do
            i = InStr(strNum, " ")
            If i = 0 Then Exit Do
            strNum = Left(strNum, i - 1) & Mid(strNum, i + 1)
        Loop

        ConvDecimal = strNum

        Err.Clear()
    End Function

#End Region

#Region "Constructores"

    Public Sub New()
        MyBase.New()

        _mSexo1 = "a"
        _mSexo2 = "as"
    End Sub

#End Region

End Class
