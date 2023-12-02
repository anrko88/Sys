Imports System.Data
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Imports Microsoft.VisualBasic

Imports GCC.Entity

Namespace GCC.UI

    Public Class GCCConstante
        Public Const C_TABLAGENERICA_Modalidad_TipoCambio As String = "TBL116"
        ' Constantes de mensajes por defecto
        Public Const C_MENSAJE_ERROR_NO_LOGIN As String = "Ud. no ha iniciado sesión correctamente o se ha perdido la sesión que tenía, vuelva a ingresar."
        Public Const C_MENSAJE_ERROR_TIMEOUT As String = "Su sesion ha caducado, debe volver a ingresar al sistema."
        Public Const C_ERROR As String = "ERROR"

        Public Const C_FECHANULA As String = "01/01/0001"

        'Paginacion
        Public Const C_PAGINADO As Integer = 10

        'Constante de formato de Entero
        Public Const C_FormatEntero As String = "#,##0"

        'Constante de formato de Miles
        Public Const C_FormatMiles As String = "#,###.#0"
        Public Const C_FormatMiles0 As String = "#,##0"
        Public Const C_FormatMiles2 As String = "#,##0.00"
        Public Const C_FormatMiles4 As String = "#,###.#000"
        Public Const C_FormatMiles6 As String = "#,###.#00000"

        'Constantes de Formato de Fecha
        Public Const C_FormatoFecha As String = "dd/MM/yyyy"

        ' Constantes de Páginas Invariables      
        Public Const C_PAGINA_INICIAL As String = "frmPrincipal.aspx"
        Public Const C_PAGINA_INDEX As String = "frmLogin.aspx"

        'Constantes varios de cadena
        Public Const C_CADENA_VACIA As String = ""
        Public Const C_CADENA_CERO As String = "0"
        Public Const C_CADENA_UNO As String = "1"
        Public Const C_CADENA_DOS As String = "2"
        Public Const C_CADENA_TRES As String = "3"

        'Constantes varios de numeros
        Public Const C_VALOR_MENOS_UNO As Integer = -1
        Public Const C_VALOR_CERO As Integer = 0

        'Transaccion
        Public Const C_TX_NUEVO As String = "NUEVO"
        Public Const C_TX_EDITAR As String = "EDITAR"
        Public Const C_TX_LABEL_NUEVO As String = "Nuevo"
        Public Const C_TX_LABEL_EDITAR As String = "Editar"
        'IBK RPR
        Public Const C_TX_CONSULTA As String = "CONSULTA"
        Public Const C_TX_EXTORNO As String = "EXTORNO"
        Public Const C_TX_LABEL_CONSULTA As String = "Consulta"
        Public Const C_TX_LABEL_EXTORNO As String = "Extorno"
        'FIN IBK

        'Texto correo
        Public Const C_MENSAJE_CORREO_FACTURA_PROVEEDOR As String = "Estimados:%0A%0ABuenos días, adjunto carta de aprobación por el leasing planteado según la referencia.%0A%0A" & _
                                                                    "Por favor, enviar facturas originales a mi atención, en el piso 15 de la Torre Interbank (Carlos Villarán 140 - La Victoria), adjuntando la carta de aprobación recepcionada dentro de las siguientes 48 horas.%0A%0A" & _
                                                                    "De existir algún adelanto de la cuota inicial del cliente, por favor enviar carta confirmando el importe recibido (Modelo de carta adjunta)%0A%0A" & _
                                                                    "Tener en cuenta que si no se da respuesta, dentro de los plazos establecidos en el presente email y en la carta de aprobación adjunta, la aprobación quedará sin efecto.%0A%0A" & _
                                                                    "Cualquier consulta adicional agradeceré se comunique a este mismo email,%0A%0A" & _
                                                                    "Saludos cordiales,"
        Public Const C_MENSAJE_CORREO_CLIENTE As String = "Estimado,%0A%0A" & _
                                                          "Su operación de leasing ha sido aprobada, le pedimos se sirva enviar los siguientes documentos para la formalización y desembolso:%0A%0A" & _
                                                          "Documentos para Formalización (firma de contrato)%0A%0A{0} %0A%0A" & _
                                                          "Requerimientos para Desembolso%0A%0A" & _
                                                          "-	Contrato de leasing firmado. La notaria se comunicará con ustedes para la toma de firmas respectivas.%0A" & _
                                                          "-	Factura del bien, la cual será solicitada al concesionario. %0A" & _
                                                          "-	Número de cuenta corriente relacionada al leasing.%0A" & _
                                                          "-	Depósito de cuota inicial y comisión de activación. De haber realizado un adelanto al proveedor, indicarlo para solicitar la carta de adelanto correspondiente. %0A" & _
                                                          "-	En caso de haber requerido póliza externa, brindarnos el nombre y dirección de correo electrónico de su bróker para la solicitud de documentación correspondiente. %0A%0A" & _
                                                          "El desembolso se realizará, siempre que usted haya cumplido con enviar lo solicitado; y si su póliza es externa, que haya enviado los documentos del seguro y éstos se encuentren conformes.%0A%0A " & _
                                                          "Saludos cordiales, "

        ' Texto correo enviar contrato
        Public Const C_MENSAJE_CORREO_CONTRATO As String = "Estimado(a) SGL_NombreContacto:%0A%0AEn adjunto envío el contrato de leasing, para tu revisión y conformidad.%0A%0A- SGL_DescripcionBien, SGL_Marca.%0A%0AQuedamos a la espera, para su envío a notaria.%0A%0AMuchas Gracias"

        '14/01/2013 IBK RPR
        Public Enum eTipoCuenta As Integer
            Cuenta_corriente = 1
            Ahorros = 2
        End Enum
        'FIN IBK

        'Tipo Util ASHX
        Public Enum eTipoUtilAshx As Integer
            Departamento = 0
            ValorGenerica = 1
            ValorProvincia = 2
            ValorDistrito = 3
            ValorGenericaAnidada = 4
            ValorGenericaEspecial = 5
            HtmlEncode = 6
            DescripcionValorGenerico = 7
            Notarias = 8
            ComisionActivacion = 9
            ValorPais = 10
            ContactoNotaria = 11 'IBK RPH
            Municipalidad = 12 'IBK JJM
        End Enum

        'Tabla Generica

        'Inicio IBK - RPR 02/01/2013
        Public Const C_TABLAGENERICA_COD_TIENDA As String = "TBL052"
        Public Const C_TABLAGENERICA_ESTADO_RECUPERACION As String = "TBL060"
        Public Const C_TABLAGENERICA_CONCEPTOS As String = "TBL115"
        Public Const C_TABLAGENERICA_TIPO_LIQUIDACION As String = "TBL360"
        Public Const C_TABLAGENERICA_ESTADO_LIQUIDACION As String = "TBL361"
        Public Const C_TABLAGENERICA_ESTADO_OPERACIONACTIVA As String = "TBL065"
        'Fin

        Public Const C_TABLAGENERICA_ESTADO_CONTRATO As String = "TBL318"
        Public Const C_TABLAGENERICA_EJECUTIVO As String = "TBL146"
        Public Const C_TABLAGENERICA_ESTADO_COTIZACION As String = "TBL192"
        Public Const C_TABLAGENERICA_CLASIFICACION_BIEN As String = "TBL014"
        Public Const C_TABLAGENERICA_TIPO_BIEN As String = "TBL104"
        Public Const C_TABLAGENERICA_TIPO_PERSONA As String = "TBL310"
        'Inicio IBK - RPH
        Public Const C_TABLAGENERICA_TIPO_REPRESENTANTE As String = "TBL321"
        'Fin
        Public Const C_TABLAGENERICA_TIPO_DOCUMENTO As String = "TBL041"
        Public Const C_TABLAGENERICA_TIPO_CONTRATO As String = "TBL129"
        Public Const C_TABLAGENERICA_SUB_TIPO_CONTRATO As String = "TBL198"
        Public Const C_TABLAGENERICA_PROCEDENCIA As String = "TBL199"
        Public Const C_TABLAGENERICA_TIPO_INMUEBLE As String = "TBL200"
        Public Const C_TABLAGENERICA_ESTADO_BIEN As String = "TBL201"
        Public Const C_TABLAGENERICA_TIPO_CRONOGRAMA As String = "TBL048"
        Public Const C_TABLAGENERICA_PERIOCIDAD As String = "TBL019"
        Public Const C_TABLAGENERICA_FRECUENCIA_PAGO As String = "TBL033"
        Public Const C_TABLAGENERICA_TIPO_GRACIA As String = "TBL202"
        Public Const C_TABLAGENERICA_TIPO_BIEN_SEGURO As String = "TBL203"
        Public Const C_TABLAGENERICA_TIPO_SEGURO As String = "TBL203"
        Public Const C_TABLAGENERICA_BANCA_ATENCION As String = "TBL168"
        Public Const C_TABLAGENERICA_TIPO_PROVEEDOR As String = "TBL205"
        Public Const C_TABLAGENERICA_CONDICIONES_ADICIONALES As String = "TBL204"
        Public Const C_TABLAGENERICA_FIRMA_EN As String = "TBL206"
        Public Const C_TABLAGENERICA_TIPO_CTA_CTE As String = "TBL028"
        Public Const C_TABLAGENERICA_CLASIFICACION_CONTRATO As String = "TBL207"
        Public Const C_TABLAGENERICA_NOTARIA_PUBLICA As String = "TBL111"
        Public Const C_TABLAGENERICA_NOMBRE_CONTRATO As String = "TBL152"
        Public Const C_TABLAGENERICA_ORIGEN_ADENDA As String = "TBL208"
        Public Const C_TABLAGENERICA_ESTADO_CIVIL As String = "TBL194"
        'Public Const C_TABLAGENERICA_EJECUTIVO_BANCA As String = "TBL313"
        Public Const C_TABLAGENERICA_ZONAL As String = "TBL311"
        Public Const C_TABLAGENERICA_ORIGEN_DOCUMENTO As String = "TBL313"
        Public Const C_TABLAGENERICA_TIPO_OBSERVACION_CHECKLIST As String = "TBL315"
        Public Const C_TABLAGENERICA_MOTIVO_RECHAZO_COTIZACION As String = "TBL316"
        Public Const C_TABLAGENERICA_NACIONALIDAD As String = "TBL046"
        Public Const C_TABLAGENERICA_CARGO As String = "TBL317"
        Public Const C_TABLAGENERICA_CONCEPTO_DETRACCION As String = "TBL180"

        Public Const C_TABLAGENERICA_MOTIVO_PIPELINE As String = "TBL319"
        Public Const C_TABLAGENERICA_ESTADO_PIPELINE As String = "TBL320"

        Public Const C_TABLAGENERICA_ESTA_BIEN_E As String = "TBL209"
        Public Const C_TABLAGENERICA_Estado_Inscripción_RRPP As String = "TBL210"
        Public Const C_TABLAGENERICA_Estado_Municipal As String = "TBL211"
        Public Const C_TABLAGENERICA_Propiedad As String = "TBL212"
        Public Const C_TABLAGENERICA_Opcion_Compra As String = "TBL213"
        Public Const C_TABLAGENERICA_Clase As String = "TBL214"
        Public Const C_TABLAGENERICA_Tipo_Carroceria As String = "TBL215"
        Public Const C_TABLAGENERICA_Transmision As String = "TBL216"
        Public Const C_TABLAGENERICA_Traccion As String = "TBL217"
        Public Const C_TABLAGENERICA_Tipo_Motor As String = "TBL218"
        Public Const C_TABLAGENERICA_Estado_Transferencia As String = "TBL219"
        Public Const C_TABLAGENERICA_Tipo_Combustible As String = "TBL221"


        Public Const C_TABLAGENERICA_Tipo_IDAgrupacion As String = "TBL220"
        Public Const C_TABLAGENERICA_Tipo_MedioPago As String = "TBL222"
        Public Const C_TABLAGENERICA_Tipo_Cuentas As String = "TBL224"
        Public Const C_TABLAGENERICA_Tipo_Pago_Comision As String = "TBL225"

        Public Const C_TABLAGENERICA_InsDesembolso_Estado As String = "TBL226"
        Public Const C_TABLAGENERICA_Estado_Resolucion As String = "TBL227"
        Public Const C_TABLAGENERICA_Tasador As String = "TBL135"
        Public Const C_TABLAGENERICA_MotivoNoTasacion As String = "TBL179"
        Public Const C_TABLAGENERICA_EstadoTasaciónContrato As String = "TBL321"
        Public Const C_TABLAGENERICA_Estado_Pago As String = "TBL228"
        Public Const C_TABLAGENERICA_Estado_Cobro As String = "TBL229"
        Public Const C_TABLAGENERICA_Numero_Cuota As String = "TBL230"

        Public Const C_TABLAGENERICA_Siniestro_Tipo As String = "TBL141"
        Public Const C_TABLAGENERICA_Siniestro_Situacion As String = "TBL142"
        Public Const C_TABLAGENERICA_Siniestro_Aplicacion As String = "TBL143"
        Public Const C_TABLAGENERICA_Siniestro_Transferencia As String = "TBL144"
        Public Const C_TABLAGENERICA_Siniestro_Contrato As String = "TBL145"
        Public Const C_TABLAGENERICA_Siniestro_Seguro As String = "TBL231"
        Public Const C_TABLAGENERICA_Siniestro_Origen As String = "TBL232"
        Public Const C_TABLAGENERICA_Siniestro_EstadoBien As String = "TBL233"
        Public Const C_TABLAGENERICA_Siniestro_TipoPoliza As String = "TBL234"
        Public Const C_TABLAGENERICA_Siniestro_BancoEmite As String = "TBL235"
        Public Const C_TABLAGENERICA_Siniestro_AplicaFondo As String = "TBL236"

        Public Const C_TABLAGENERICA_Demanda_Estado As String = "TBL237"
        Public Const C_TABLAGENERICA_Demanda_TipoImplicado As String = "TBL238"
        Public Const C_TABLAGENERICA_MultaVehicular_Concepto As String = "TBL239"
        Public Const C_TABLAGENERICA_MultaVehiculAR_Infraccion As String = "TBL240"
        Public Const C_TABLAGENERICA_Concepto As String = "TBL115"
        Public Const C_TABLAGENERICA_Estado_Cobro_LPC As String = "TBL060"
        Public Const C_TABLAGENERICA_Configuracion_Cobro As String = "TBL323"
        Public Const C_TABLAGENERICA_Impuestos_LPC As String = "TBL044"

        Public Const C_TABLAGENERICA_OpcionCompra_TipoEnvio As String = "TBL324"
        Public Const C_TABLAGENERICA_OpcionCompra_Estado As String = "TBL325"
        Public Const C_TABLAGENERICA_OpcionCompra_TipoPagoBien As String = "TBL326"

        'IBK - RPH
        Public Const C_TABLAGENERICA_Tipo_Valor As String = "TBL404"
        Public Const C_TABLAGENERICA_Tipo_Seguro_reporte As String = "TBL405"
        'Fin

        ' Tipo dato notarial
        Public Const ORIGEN_ADENDA_DatoNotarial As String = "001"
        Public Const ORIGEN_ADENDA_Adenda As String = "002"

        ' Arrendamiento Financiero
        Public Const MINUTA_ARRENDAMIENTO_FINANCIERO As String = "01"

        Public Const CODIGO_ESTADO_PIPELIN_EVALUACION As String = "001"
        Public Const CODIGO_ESTADO_PIPELIN_RIESGOS As String = "002"
        Public Const CODIGO_ESTADO_PIPELIN_PORDESEMBOLSAR As String = "003"
        Public Const CODIGO_ESTADO_PIPELIN_EVALUACION_C As String = "007"
        Public Const CODIGO_ESTADO_PIPELIN_RIESGOS_C As String = "008"
        Public Const CODIGO_ESTADO_PIPELIN_PORDESEMBOLSAR_C As String = "005"

        'Tìpo Documento
        Public Enum eTipoDocumento
            NoTiene = 0
            DNI = 1
            RUC = 2
            CarnetExtranjeria = 3
            CarnetIdentidad = 4
            Pasaporte = 5
            Otros = 6
        End Enum

        'Estados Cotizacion
        Public Const C_ESTADOCOTIZACION_INGRESADO As String = "001"
        Public Const C_ESTADOCOTIZACION_PENDCARTA As String = "002"
        Public Const C_ESTADOCOTIZACION_EVASUPERV As String = "003"
        Public Const C_ESTADOCOTIZACION_EVACLIE As String = "004"
        Public Const C_ESTADOCOTIZACION_APROBADO As String = "005"
        Public Const C_ESTADOCOTIZACION_DESAPROBADO As String = "006"
        Public Const C_ESTADOCOTIZACION_PENDIENTE_F10 As String = "007"
        Public Const C_ESTADOCOTIZACION_EVALUACION_F10 As String = "008"
        'Public Const C_ESTADOCOTIZACION_DESAPROBADO As String = "009"

        ' TIPO DE REPRESENTANTE
        Public Const C_REPRESENTANTE_BANCO As String = "001"
        Public Const C_REPRESENTANTE_CLIENTE As String = "002"

        'ORIGEN DE DOCUMENTO
        Public Const C_ORIGENDOC_DOC_CLIENTE As String = "001"
        Public Const C_ORIGENDOC_CHECKLIST_COMERCIAL As String = "002"
        Public Const C_ORIGENDOC_CHECKLIST_LEGAL As String = "003"
        Public Const C_ORIGENDOC_CONTRATO As String = "004"

        'TIPO OPERACION LINEAS
        Public Enum eTipoOperacion As Integer
            Operacion = 43
            Linea = 42
        End Enum

        'PRODUCTO LEASING (LINEAS)
        Public Const C_CODIGO_LEASING_LINEAS As String = "610"

        'PRODUCTO LEASING GCC
        Public Const C_CODGCC_PROD_LEASING As String = "LD"
        Public Const C_CODGCC_PROD_LEASEBACK As String = "LB"
        Public Const C_CODGCC_PROD_IMPORTACION As String = "IM"
        Public Const C_DESGCC_PROD_LEASING As String = "LEASING TOTAL"
        Public Const C_DESGCC_PROD_LEASEBACK As String = "LEASEBACK"
        Public Const C_DESGCC_PROD_IMPORTACION As String = "IMPORTACION"

        'PRODUCTO LEASING LPC
        Public Const C_CODLPC_PROD_LEASING As String = "000111"
        Public Const C_CODLPC_PROD_LEASEBACK As String = "000112"
        Public Const C_CODLPC_PROD_IMPORTACION As String = "000111"
        Public Const C_CODLPC_PROD_LEASING_PAS As String = "000501"
        Public Const C_CODLPC_PROD_LEASEBACK_PAS As String = "000505"
        Public Const C_CODLPC_PROD_IMPORTACION_PAS As String = "000601"

        'ESTADOS CONTRATO (SOLICITUDCREDITO)
        Public Const C_CODIGO_ESTADO_CONTRATO_INGRESADO As String = "01"
        Public Const C_CODIGO_ESTADO_CONTRATO_VERIFICADO As String = "02"
        Public Const C_CODIGO_ESTADO_CONTRATO_ELABORADO As String = "03"
        Public Const C_CODIGO_ESTADO_CONTRATO_PENDIENTEDECARTA As String = "04"
        Public Const C_CODIGO_ESTADO_CONTRATO_ENVIADOCLIENTE As String = "05"
        Public Const C_CODIGO_ESTADO_CONTRATO_PENDIENTEFIRMA As String = "06"
        Public Const C_CODIGO_ESTADO_CONTRATO_FORMALIZADO As String = "07"
        Public Const C_CODIGO_ESTADO_CONTRATO_VIGENTE As String = "08"
        Public Const C_CODIGO_ESTADO_CONTRATO_ENPROCESO As String = "09"
        Public Const C_CODIGO_ESTADO_CONTRATO_ANULADO As String = "10"
        Public Const C_CODIGO_ESTADO_CONTRATO_PENDIENTEENVIO As String = "15"

        'TIPO CRONOGRAMA
        Public Const C_CODIGO_TIPO_CRONOGRAMA_CAPITAL_CONSTANTE As String = "001"
        Public Const C_CODIGO_TIPO_CRONOGRAMA_CUOTA_CONSTANTE As String = "003"

        'TIPO PERIODICIDAD
        Public Const C_CODIGO_TIPO_PERIODICIDAD_ANUAL As String = "ANU"
        Public Const C_CODIGO_TIPO_PERIODICIDAD_MENSUAL As String = "MEN"
        Public Const C_CODIGO_TIPO_PERIODICIDAD_SEMESTRAL As String = "SEM"
        Public Const C_CODIGO_TIPO_PERIODICIDAD_TRIMESTRAL As String = "TRI"
        Public Const C_CODIGO_TIPO_PERIODICIDAD_BIMESTRAL As String = "BIM"

        'TIPO FRECUENCIAPAGO
        Public Const C_CODIGO_TIPO_FRECPAGO_ANUAL_FIJA As String = "ANU"
        Public Const C_CODIGO_TIPO_FRECPAGO_ANUAL_VARIABLE As String = "AND"
        Public Const C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_FIJA As String = "BIM"
        Public Const C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_VARIABLE As String = "BID"
        Public Const C_CODIGO_TIPO_FRECPAGO_MENSUAL_FIJA As String = "MEN"
        Public Const C_CODIGO_TIPO_FRECPAGO_MENSUAL_VARIABLE As String = "MED"
        Public Const C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_FIJA As String = "SEM"
        Public Const C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_VARIABLE As String = "SED"
        Public Const C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_FIJA As String = "TRI"
        Public Const C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_VARIABLE As String = "TRD"

        ' Tipo dato notarial
        Public Const C_COD_MONEDA_SOLES As String = "001"
        Public Const C_COD_MONEDA_DOLARES As String = "002"

        'ESTADO DOCUMENTO
        Public Enum eEstadoDoc
            Pendiente = 1
            Formalizado = 2
            Desembolsado = 3
            Anulado = 4
        End Enum

        'TIPO BANCO
        Public Const C_TIPOBANCO_INTERBANK As String = "001"
        Public Const C_TIPOBANCO_NACION As String = "002"

        'BANCA WIO
        Public Const C_DIV_BE As String = "BE"
        Public Const C_DIV_BC As String = "BC"
        Public Const C_DIV_BI As String = "BI"
        Public Const C_DIV_LS As String = "LS"
        Public Const C_DIV_IM As String = "IM"
        'Inicio IBK - AAE - Se agregan constantes para Banca Empresarial Provicia y Lima
        Public Const C_DIV_BEP As String = "BEP"
        Public Const C_DIV_BEL As String = "BEL"

        Public Const C_DIVISION_BANCA_EMPRESA_PROV As String = "Banca Empresa Provincia"
        Public Const C_DIVISION_BANCA_EMPRESA_LIMA As String = "Banca Empresa Lima"
        'Fin IBK
        Public Const C_DIVISION_BANCA_EMPRESA As String = "Banca Empresa"
        Public Const C_DIVISION_BANCA_CORPORATIVA As String = "Banca Corporativa"
        Public Const C_DIVISION_BANCA_INSTITUCIONAL As String = "Banca Institucional"
        Public Const C_DIVISION_LEASING As String = "Leasing"
        Public Const C_DIVISION_INMOBILIARIA As String = "Inmobiliaria"
        Public Const C_DIVISION_OTROS As String = "Otros"

        'BANCA SGL
        'Inicio IBK - AAE - Se agregan constantes para Banca Empresarial Provicia y Lima
        Public Const C_CODIGO_BANCA_BEP As String = "05"
        'Fin IBK
        Public Const C_CODIGO_BANCA_BE As String = "01"
        Public Const C_CODIGO_BANCA_BC As String = "03"
        Public Const C_CODIGO_BANCA_BI As String = "06"
        Public Const C_CODIGO_BANCA_LS As String = "LS"
        Public Const C_CODIGO_BANCA_IM As String = "80"
        Public Const C_CODIGO_BANCA_OTROS As String = "00"

        ' Destino del crédito
        Public Shared DestinoCredito_Inmueble() As String = {"002"}
        Public Shared DestinoCredito_Maquinaria() As String = {"003", "004", "005"}
        Public Shared DestinoCredito_Otros() As String = {"008", "007"}
        Public Shared DestinoCredito_Vehiculo() As String = {"006"}

        ' Clasificación contrato
        Public Const C_CLASIF_CONTRATO_INMUEBLE As String = "001"
        Public Const C_CLASIF_CONTRATO_MUEBLE As String = "002"
        Public Const C_CLASIF_CONTRATO_VEHICULO As String = "003"
        Public Const C_CLASIF_CONTRATO_EMBARCACION_PESQUERA As String = "004"

        ' TipoRubroFinanciamiento
        Public Const C_TIPO_RUBRO_FINANC_BIEN_INMUEBLE As String = "002"
        Public Const C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_INDUSTRIAL As String = "003"
        Public Const C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_OFICINA As String = "004"
        Public Const C_TIPO_RUBRO_FINANC_MAQ_Y_EQUIP_MOV_TIERRA As String = "005"
        Public Const C_TIPO_RUBRO_FINANC_UNID_TRANS_TER_LIV As String = "006"
        Public Const C_TIPO_RUBRO_FINANC_SISTEMA_PROC_DATOS As String = "007"
        Public Const C_TIPO_RUBRO_FINANC_OTROS As String = "008"
        Public Const C_TIPO_RUBRO_FINANC_UNID_TRANS_TER_PESADO As String = "011"

        ' Tipo de Bien: Embarcación
        Public Const C_TIPO_BIEN_EMBARCACION As String = "019"

        ' Tipos de anexo
        Public Shared Anexo_Vehiculo() As String = {"006", "011"}
        Public Shared Anexo_Inmueble() As String = {"002"}
        Public Shared Anexo_Mueble() As String = {"003", "004", "005", "007", "008"}

        'Tipo Documento WIO
        Public Const C_WIO_TIPODOC_RUC As Integer = 139
        Public Const C_WIO_TIPODOC_DNI As Integer = 140
        Public Const C_WIO_TIPODOC_CE As Integer = 141
        Public Const C_WIO_TIPODOC_PAS As Integer = 142

        'Tipo de Moneda
        Public Const C_CODMON_SOLES As String = "001"
        Public Const C_CODMON_DOLARES As String = "002"

        'TipoConcepto LSGConceptoTarifa
        Public Const C_COD_TIPOCONCEPTO_TASA As String = "01"
        Public Const C_COD_TIPOCONCEPTO_COMISION As String = "02"
        Public Const C_COD_TIPOCONCEPTO_CARTAS As String = "03"

        'TipoTarifa LSGConceptoTarifa
        Public Const C_COD_TIPOTARIFA_COMISION_ACTIVACION As String = "03"

        'Clasificacion Bien
        Public Const C_COD_CLASIFICACION_BIEN_BIENES_INMUEBLES As String = "002"

        ' Directorio Anexos
        Public Const C_DIRECTORIO_ANEXOS As String = "ANEXOS"

        ' Directorio Alertas
        Public Const C_DIRECTORIO_ALERTAS As String = "ALERTAS"


        ' Código de los textos predefinidos
        ' Fianza Solidaria
        Public Const C_TEXTO_PREDEF_FIANZA_SOL As String = "006"
        Public Const C_TEXTO_PREDEF_POSIC_CONTR As String = "010"

        ' Textos predefinidos.
        ' "006" - Fianza Solidaria
        Public Const TEXTO_PREDEF_FIANZA_SOLIDARIA As String = "_____________, identificado con DNI N° ____________, casado bajo el Régimen de Separación de Patrimonios inscrito en  la Partida N° ___________  del Registro Personal de Lima, con  domicilio en ___________, distrito de _________, provincia y departamento de Lima." + vbCrLf + _
                                                               vbCrLf + _
                                                               "_________, identificado con DNI N° ____________ casado con la señora ____________, identificada con DNI N° ____________, con domicilio común en ___________, distrito de _________, provincia y departamento de Lima." + vbCrLf + _
                                                               vbCrLf + _
                                                               "____________, con RUC Nº ___________, con domicilio en ___________, distrito de ___________, provincia  y departamento de Lima, debidamente representado por el señor _____________, identificado con DNI Nº ___________, y por el señor _____________, identificado con DNI Nº ___________, según poderes inscritos en la Partida Nº ________ del Registro de Personas Jurídicas de Lima."

        ' "010" - Cesión de Posición Contractual
        Public Const TEXTO_PREDEF_CESIONARIO As String = "Denominación o razón social      : ____________" + vbCrLf + _
                                                         "Registro Único de Contribuyente  : ____________" + vbCrLf + _
                                                         "Domicilio: ____________, Provincia y Departamento de Lima. " + vbCrLf + _
                                                         "Representante(s):     " + vbCrLf + _
                                                         vbCrLf + _
                                                         "Nombre(s) y Apellido(s)         : ____________" + vbCrLf + _
                                                         "Documento de identidad          : DNI Nº ____________" + vbCrLf + _
                                                         "Datos de inscripción del poder  : Partida Nº ____________ del Registro de Personas Jurídicas de __________."


        ' Procedencia
        Public Const PROCEDENCIA_IMPORTACION As String = "001"
        Public Const PROCEDENCIA_LOCAL As String = "002"

        ' SUBTIPO DE CONTRATO
        Public Const SUBTIPO_TOTAL As String = "001"
        Public Const SUBTIPO_PARCIAL As String = "002"

        Public Const CONTRATO_DIAS_VENCIMIENTO As Integer = 8

        'MODALIDAD DE TIPO CAMBIO
        Public Const C_vMODALIDAD_IBK As String = "IBK"
        Public Const C_vMODALIDAD_SBS As String = "SBS"
        Public Const C_vMODALIDAD_PRF As String = "PRF"

        'BLOQUEO TIPO DOCUMENTO
        Public Const C_BLOQUEO_DOC_COTIZACION As String = "001"
        Public Const C_BLOQUEO_DOC_CONTRATO As String = "002"

        'BLOQUEO TIPO MODULO
        Public Const C_BLOQUEO_MODULO_COTIZACION As String = "001"
        Public Const C_BLOQUEO_MODULO_SOLCLIENTE As String = "002"
        Public Const C_BLOQUEO_MODULO_SOLPROVEEDOR As String = "003"
        Public Const C_BLOQUEO_MODULO_CHECKCOMERCIAL As String = "004"
        Public Const C_BLOQUEO_MODULO_CHECKLEGAL As String = "005"
        Public Const C_BLOQUEO_MODULO_CONTRATO As String = "006"
        Public Const C_BLOQUEO_MODULO_DESEMBOLSO As String = "007"

        'Tipo de Comprobante
        Public Const C_COD_TIPOCOMPROBANTE_DUA As String = "04"

        'Tipo Agrupacion de Instruccion Desembolso
        Public Const C_COD_TIPOAGRUPACION_ID_ADELANTO As String = "07"
        Public Const C_COD_TIPOAGRUPACION_ID_CARGO As String = "08"

        'Tipo de Operacion
        Public Const C_COD_TIPOOPERACION_ID_CARGO As String = "002"
        Public Const C_COD_TIPOOPERACION_ID_ABONO As String = "001"

        'Estado de Instruccion
        Public Const C_ESTADO_INSDESEMBOLSO_ELABORACION As String = "001"
        Public Const C_ESTADO_INSDESEMBOLSO_WIO As String = "002"
        Public Const C_ESTADO_INSDESEMBOLSO_PENDEJECUCION As String = "003"
        Public Const C_ESTADO_INSDESEMBOLSO_DEVUELTA As String = "004"
        Public Const C_ESTADO_INSDESEMBOLSO_ANULADA As String = "005"
        Public Const C_ESTADO_INSDESEMBOLSO_APROBADA As String = "006"

        'Inicio IBK - AAE - Agrego Estados
        Public Const C_ESTADO_INSDESEMBOLSO_ERROR As String = "007"
        Public Const C_ESTADO_INSDESEMBOLSO_ADMINISTRATIVA As String = "008"
        Public Const C_ESTADO_INSDESEMBOLSO_EJECUCION As String = "009"
        'Fin IBK

        '******************************************
        'Constantes para Tramas
        '******************************************
        'Constante Cuenta (WIO - TRX)
        Public Const C_CODCTA_CORRIENTE As String = "IM"
        Public Const C_CODCTA_AHORROS As String = "ST"

        'Constante TX Pagos :: Moneda
        Public Const C_TX_MONEDA_SOLES As String = "001"
        Public Const C_TX_MONEDA_DOLARES As String = "010"

        Public Const C_TIPOCUENTA_CORRIENTE As String = "01"
        Public Const C_TIPOCUENTA_AHORROS As String = "02"

        Public Const C_TIPOMONEDA_SOLES As String = "001"
        Public Const C_TIPOMONEDA_DOLARES As String = "002"

        'Tipo de Siniestro 
        Public Const C_TIPO_SINIESTRO_SINIESTRO As String = "001"
        Public Const C_TIPO_SINIESTRO_DEMANDA As String = "002"

        ' Concepto
        Public Const C_CONCEPTO_IMPUESTO_MUNICIPAL As String = "O01"
        Public Const C_CONCEPTO_IMPUESTO_VEHICULAR As String = "O02"
        Public Const C_CONCEPTO_INFRACCION_TRANSITO As String = "O17"
        Public Const C_CONCEPTO_MULTA_INSCRIPCION As String = "O31"

        ' Estado Cobro
        Public Const C_ESTADO_COBRO_ENVIADO_A_HOST As String = "C"

        ' Cantidad de dias habiles para la fecha de cobro (multas vehiculares) 
        ' SE TIENE QUE COLOCAR UN DIA MAS DEL CALUCULO QUE SE REQUIERE HACER
        Public Const C_DIAS_HABILES_COBRO_MULTA_VEHICULAR As Integer = 2
        Public Const C_DIAS_HABILES_COBRO_IMPUESTO_VEHICULAR As Integer = 7
        Public Const C_DIAS_HABILES_COBRO_MULTA_MUNICIPAL As Integer = 7
        Public Const C_DIAS_HABILES_MULTA_VEHICULAR As Integer = 8



        'Modulos para Documento Global POP
        Public Const C_GESTIONBIEN_SINIESTRO As String = "001"
        Public Const C_GESTIONBIEN_DEMANDA As String = "002"
        Public Const C_GESTIONBIEN_IMPMUNICIPAL As String = "003"
        Public Const C_GESTIONBIEN_MULTAVEHICULAR As String = "004"
        Public Const C_GESTIONBIEN_IMPVEHICULAR As String = "005"
        Public Const C_GESTIONBIEN_TASACION As String = "006"
        Public Const C_GESTIONBIEN_OTROSCONCEPTOS As String = "007"
        Public Const C_GESTIONBIEN_BIEN As String = "008"
        Public Const C_GESTIONBIEN_OPCIONCOMPRA As String = "009"
        Public Const C_FORMALIZACION_CESIONCONTRATO As String = "010"

        'Gestion Bien :: Configuracion Cobros
        Public Const C_CONFIGURACION_COBRO_TASADEFECTO As String = "001"
        Public Const C_CONFIGURACION_COBRO_NROCUOTAFRACCIONAMIENTO As String = "002"
        Public Const C_CONFIGURACION_COBRO_TEA As String = "003"
        Public Const C_CONFIGURACION_COBRO_DIASANIO As String = "004"

        'Gestion Bien :: Impuestos LPC
        Public Const C_IMPUESTOS_LPC_IGV As String = "01"

        'Inicio IBK - AAE - 14/02/2013 - Agrego contantes para manejo de Lote de gestión de bien
        Public Const C_LOTE_IMP_VEHICULAR As String = "0"
        Public Const C_LOTE_MULT_VEHICULAR As String = "1"
        Public Const C_LOTE_IMP_MUNICIPAL As String = "2"

        'Inicio IBK - AAE - 23/04/2013 - Constantes Opcion Compra
        Public Const C_COD_CONCEPTO_AMORTIZACION As String = "O35"
        Public Const C_COD_CONCEPTO_OC As String = "O00"
        Public Const C_COD_CONCEPTO_COM_TRANSF As String = "O11"
        'Fin IBK

        'Tipo Alerta
        Public Enum eTipoAlerta As Integer
            C_MailImpuestoVehicular = 1
            C_MailImpuestoMunicipal = 2
            C_MailMultasVehicular = 3
            C_MailAsignacionTasador = 4
            C_MailRegistroTasador = 5
            C_MailConceptoCobros = 6
            C_MailOpcionCompras = 7
        End Enum

        '14/01/2013 IBK - RPR Equivalencia entre codigos de moneda
        Public Shared Function eq(ByVal strCodMoneda As String, ByVal strCodMonedaTx As String) As Boolean

            Dim codMoneda, codMonedaTx As Integer
            codMoneda = Val(strCodMoneda)
            codMonedaTx = Val(strCodMonedaTx)

            If codMoneda = 1 And codMonedaTx = 1 Then Return True
            If codMoneda = 2 And codMonedaTx = 10 Then Return True
            Return False








        End Function
        'FIN IBK

    End Class

End Namespace


