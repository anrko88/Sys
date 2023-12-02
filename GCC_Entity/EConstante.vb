'-----------------------------------------------------------------------------
'Nombre             : EConstante
'Objetivo           : Permite obtener los valores Globales
'Creado Por         : TSF - JRC
'Fecha de Creación  : 22/02/2012
'-----------------------------------------------------------------------------
Public Class EConstante


    'Valor constante para Configuración
    Public Const C_ARCHIVO_CONFIG_XML As String = "Config.xml"
    Public Const C_AMBIENTE_PRODUCCION As Integer = 0
    Public Const C_AMBIENTE_UAT As Integer = 1
    Public Const C_AMBIENTE_SIT As Integer = 2
    Public Const C_AMBIENTE_DESARROLLO As Integer = 3
    Public Const C_AMBIENTE_VACIO As Integer = 4
    Public Const C_NOMBRE_APLICATIVO As String = "GCC"

    'Valores constantes de NODO principal en el archivo de configuracion XML
    Public Const C_NODO_MOTOR_ORACLE As String = "motororacle"
    Public Const C_NODO_MOTOR_SQL As String = "motorsql"

    'Valores de Cifrado
    Public Const C_CIFRADO As String = "S"
    Public Const C_NO_CIFRADO As String = "N"

    'Constante de formato de Miles
    Public Const C_FormatMiles As String = "#,###.#0"
    Public Const C_FormatMiles4 As String = "#,###.#000"

    'Valor de Error de Aplicación
    Public Const C_ERROR_APLICACION As String = "-1"

    'Valores de los Tipos de Sucesos
    Public Const C_SUCESO_EXITO As Integer = 0
    Public Const C_SUCESO_INFORMATIVO As Integer = 1
    Public Const C_SUCESO_ADVERTENCIA As Integer = 2
    Public Const C_SUCESO_ERROR As Integer = 3
    Public Const C_SUCESO_FATAL As Integer = 4

    'Constante Tipo Doc (TRX)
    Public Const C_TRX_TIPDOC_DNI As Integer = 1
    Public Const C_TRX_TIPDOC_RUC As Integer = 2
    Public Const C_TRX_TIPDOC_CE As Integer = 3
    Public Const C_TRX_TIPDOC_CI As Integer = 4
    Public Const C_TRX_TIPDOC_PA As Integer = 5

    'Constantes de Tipo Docimento
    Public Const C_TIPODOCUMENTO_DNI As Integer = 20
    Public Const C_TIPODOCUMENTO_RUC As Integer = 21
    Public Const C_TIPODOCUMENTO_CE As Integer = 22
    Public Const C_TIPODOCUMENTO_OTROS As Integer = 145
    Public Const C_TIPODOCUMENTO_CI As Integer = 192
    Public Const C_TIPODOCUMENTO_PAS As Integer = 193

    'Estados Cotizacion
    Public Const C_ESTADOCOTIZACION_INGRESADO As String = "001"
    Public Const C_ESTADOCOTIZACION_PENDCARTA As String = "002"
    Public Const C_ESTADOCOTIZACION_EVASUPERV As String = "003"
    Public Const C_ESTADOCOTIZACION_EVACLIE As String = "004"
    Public Const C_ESTADOCOTIZACION_APROBADO As String = "005"
    Public Const C_ESTADOCOTIZACION_DESAPROBADO As String = "006"

End Class

