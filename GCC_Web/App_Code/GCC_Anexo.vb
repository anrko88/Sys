Imports System.Data
Imports System.Globalization
Imports System.IO

Imports Microsoft.VisualBasic

Imports GCC.LogicWS
Imports GCC.UI
Imports GCC.Entity


Public Class GCC_Anexo

    ' Clasificación del producto financiero, leasing, leaseback.
    Dim _codProductoFinancieroActivo As String = ""
    ' Clasificación del bien.
    Dim _tipoRubroFinanciamiento As String = ""
    ' Nacional o importado.
    Dim _procedencia As String = ""
    ' Cesión de Posición Contractual, documentos del contrato.
    Dim _cpc As String = ""
    Dim _subtipoDeContrato As String = ""
    Dim _nombreArchivoAnexos As String = ""
    Dim _codigoClasificacionContrato As String = ""

    Private Const EspaciosIniciales As String = "             "
    Private Const MargenIzquierdo As Integer = 575

    ''' <summary>
    ''' Genera un documento de anexo (.doc) y retorna el nombre del archivo
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function Generar(ByVal pCodigoContrato As String, _
                            ByVal pCodigoCotizacion As String) As String
        Dim objContratoNTx As New LContratoNTx

        ' Datos del contrato
        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))

        For Each oRow As DataRow In dtContrato.Rows
            If Not oRow.Item("CodProductoFinancieroActivo") Is DBNull.Value Then
                _codProductoFinancieroActivo = oRow.Item("CodProductoFinancieroActivo").ToString()
            Else
                Throw New Exception("Se requiere el definir el tipo de contrato del crédito.")
            End If
            If Not oRow.Item("TipoRubroFinanciamiento") Is DBNull.Value Then
                _tipoRubroFinanciamiento = oRow.Item("TipoRubroFinanciamiento").ToString()
            Else
                Throw New Exception("Se requiere el definir la clasificación del bien.")
            End If
            ' Cesión de Posición Contractual
            If Not oRow.Item("CPC") Is DBNull.Value Then
                _cpc = oRow.Item("CPC").ToString()
            End If
            ' Nacional o importado
            If Not oRow.Item("CodigoProcedencia") Is DBNull.Value Then
                _procedencia = oRow.Item("CodigoProcedencia").ToString()
            End If
            ' Subtipo de contrato, total o parcial
            If Not oRow.Item("CodigoSubTipoContrato") Is DBNull.Value Then
                'Inicio IBK - AAE - Se hace un trim
                '_subtipoDeContrato = oRow.Item("CodigoSubTipoContrato").ToString()
                _subtipoDeContrato = oRow.Item("CodigoSubTipoContrato").ToString().Trim()
                'Fin IBK
            End If
            ' Si el bien inmueble es embarcación
            If Not oRow.Item("CodigoClasificacionContrato") Is DBNull.Value Then
                _codigoClasificacionContrato = oRow.Item("CodigoClasificacionContrato").ToString()
            End If
        Next oRow

        ' Selecciona una de las quince plantillas, correspondiente con las caracteristicas del contrato.
        ' Vehículo
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, _tipoRubroFinanciamiento) <> -1) _
            And _cpc = "" Then
            _nombreArchivoAnexos = LeasingDirectoVehicular(pCodigoContrato, _
                                                           pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, _tipoRubroFinanciamiento) <> -1) _
            And _cpc <> "" Then
            _nombreArchivoAnexos = LeasingDirectoVehicularConCesion(pCodigoContrato, _
                                                                    pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
            And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, _tipoRubroFinanciamiento) <> -1) _
            And _cpc = "" Then
            _nombreArchivoAnexos = LeaseBackVehicular(pCodigoContrato, _
                                                      pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
            And (Array.IndexOf(GCCConstante.Anexo_Vehiculo, _tipoRubroFinanciamiento) <> -1) _
            And _cpc <> "" Then
            _nombreArchivoAnexos = LeaseBackVehicularConCesion(pCodigoContrato, _
                                                               pCodigoCotizacion)
        End If

        ' Mueble
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc = "" _
            And _procedencia = GCCConstante.PROCEDENCIA_LOCAL _
            And _subtipoDeContrato = GCCConstante.SUBTIPO_TOTAL Then
            _nombreArchivoAnexos = LeasingDirectoMueble(pCodigoContrato, _
                                                        pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc = "" _
            And _procedencia = GCCConstante.PROCEDENCIA_LOCAL _
            And _subtipoDeContrato <> GCCConstante.SUBTIPO_TOTAL Then
            _nombreArchivoAnexos = LeasingDirectoMueblePagoParcial(pCodigoContrato, _
                                                                   pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc <> "" _
            And _procedencia <> GCCConstante.PROCEDENCIA_IMPORTACION Then
            _nombreArchivoAnexos = LeasingDirecto_MuebleConCesion(pCodigoContrato, _
                                                                  pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc = "" _
            And _procedencia <> GCCConstante.PROCEDENCIA_IMPORTACION Then
            _nombreArchivoAnexos = LeaseBackMueble(pCodigoContrato, _
                                                   pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc <> "" _
            And _procedencia <> GCCConstante.PROCEDENCIA_IMPORTACION Then
            _nombreArchivoAnexos = LeaseBackMuebleConCesion(pCodigoContrato, _
                                                            pCodigoCotizacion)
        End If
        ' Mueble - importación
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc = "" _
            And _procedencia = GCCConstante.PROCEDENCIA_IMPORTACION Then
            _nombreArchivoAnexos = LeasingDirecto_MuebleImportacion(pCodigoContrato, _
                                                                    pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Mueble, _tipoRubroFinanciamiento) <> -1) _
            And _cpc <> "" _
            And _procedencia = GCCConstante.PROCEDENCIA_IMPORTACION Then
            _nombreArchivoAnexos = LeasingDirecto_MuebleImportacionConCesion(pCodigoContrato, _
                                                                             pCodigoCotizacion)
        End If

        ' Inmueble
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Inmueble, _tipoRubroFinanciamiento) <> -1) _
            And _codigoClasificacionContrato <> GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
            And _cpc = "" Then
            _nombreArchivoAnexos = LeasingDirectoInmueble(pCodigoContrato, _
                                                          pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Inmueble, _tipoRubroFinanciamiento) <> -1) _
            And _codigoClasificacionContrato <> GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
            And _cpc <> "" Then
            _nombreArchivoAnexos = LeasingInmuebleConCesion(pCodigoContrato, _
                                                            pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Inmueble, _tipoRubroFinanciamiento) <> -1) _
            And _codigoClasificacionContrato = GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
            And _cpc = "" Then
            _nombreArchivoAnexos = LeasingDirectoEmbarcacion(pCodigoContrato, _
                                                             pCodigoCotizacion)
        End If
        If (_codProductoFinancieroActivo = GCCConstante.C_CODLPC_PROD_LEASING) _
            And (Array.IndexOf(GCCConstante.Anexo_Inmueble, _tipoRubroFinanciamiento) <> -1) _
            And _codigoClasificacionContrato = GCCConstante.C_CLASIF_CONTRATO_EMBARCACION_PESQUERA _
            And _cpc <> "" Then
            _nombreArchivoAnexos = LeasingEmbarcacionConCesion(pCodigoContrato, _
                                                               pCodigoCotizacion)
        End If

        Return _nombreArchivoAnexos
    End Function

    'IBK - RPH
    Public Function CronogramaCotizacionLeasing(ByVal pstrCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = "Cronograma-" + pstrCodigoCotizacion + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial
        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/CARTA-Cronograma-Leasing.dot"), rutaAnexo)
        Dim strVersionCotizacion As String

        Try
            Dim objCotizacionNTx As New LCotizacionNTx
            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String


            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)


            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))

            oRtfHelper.Reemplazar("SGL_NUMEROCOTIZACION", pstrCodigoCotizacion)
            oRtfHelper.Reemplazar("SGL_archivo", pstrCodigoCotizacion)


            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))



            If dtCotizacion.Rows.Count > 0 Then
                With dtCotizacion.Rows(0)
                    strVersionCotizacion = .Item("versioncotizacion").ToString
                End With
            End If

            Dim objECotizacion As New EGcc_cotizacion
            Dim strECotizacion As String

            With objECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
                .Versioncotizacion = GCCUtilitario.StringToInteger(strVersionCotizacion)
            End With
            strECotizacion = GCCUtilitario.SerializeObject(objECotizacion)

            Dim decMontoCuotaComun As Decimal = 0

            'Valida Cronograma
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)
            Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.CotizacionCronogramaGet(strECotizacion))

            'GestionCronograma
            Dim mCronograma(1, 5) As String
            ReDim mCronograma(dtCronograma.Rows.Count + 1, 5)
            mCronograma = Cronograma(dtCronograma, True, decMontoCuotaComun, decCuotaInicial)

            'oRtfHelper.Reemplazar("SGL_MontoCuota", decMontoCuotaComun.ToString())
            'oRtfHelper.Reemplazar("SGL_MontoCuota", Format(CDbl(decMontoCuotaComun.ToString()), "#,##0.00"))
            'If decMontoCuotaComun > 0 Then
            '    oRtfHelper.Reemplazar("US_7", sDescripCortoMoneda)
            'Else
            '    oRtfHelper.Reemplazar("US_7", "")
            'End If

            ' Datos del cronograma
            oRtfHelper.TablaCronograma("SGL_TablaCronograma", mCronograma)

            oRtfHelper.SaveAndClose()

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()
            Return ex.ToString()
        End Try
    End Function
    'Fin

    Public Function CartaCotizacionLeasing(ByVal pstrCodigoCotizacion As String) As String

        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = "Cotizacion-" + pstrCodigoCotizacion + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial
        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/CARTA-Cotizacion-Leasing.dot"), rutaAnexo)

        Try

            Dim objCotizacionNTx As New LCotizacionNTx
            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String
            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim strVersionCotizacion As String
            Dim sDescripCortoMoneda As String

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))

            oRtfHelper.Reemplazar("SGL_NUMEROCOTIZACION", pstrCodigoCotizacion)
            oRtfHelper.Reemplazar("SGL_archivo", pstrCodigoCotizacion)

            ' Valida si existe
            If dtCotizacion.Rows.Count > 0 Then
                With dtCotizacion.Rows(0)
                    'If .Item("CodigoCotizacion") IsNot DBNull.Value Then
                    '    oRtfHelper.Reemplazar("SGL_NUMEROCOTIZACION", .Item("CodigoCotizacion").ToString())
                    'Else
                    '    oRtfHelper.Reemplazar("SGL_NUMEROCOTIZACION", "")
                    'End If

                    If .Item("DescripCortoMoneda") IsNot DBNull.Value Then
                        sDescripCortoMoneda = .Item("DescripCortoMoneda").ToString()
                    Else
                        sDescripCortoMoneda = ""
                    End If


                    If .Item("NombreCliente") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_RazonSocial", .Item("NombreCliente").ToString())
                    Else
                        oRtfHelper.Reemplazar("SGL_RazonSocial", "")
                    End If
                    oRtfHelper.Reemplazar("SGL_GetDate", DateTime.Now.ToString("dd/MM/yyyy"))


                    If .Item("FechaOfertaValida") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_OfertaValida", GCCUtilitario.Concatenar(.Item("FechaOfertaValida").ToString().Substring(6, 2), "/", .Item("FechaOfertaValida").ToString().Substring(4, 2), "/", .Item("FechaOfertaValida").ToString().Substring(0, 4)))
                    Else
                        oRtfHelper.Reemplazar("SGL_OfertaValida", "")
                    End If

                    If .Item("NombreMoneda") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_Moneda", Trim(.Item("NombreMoneda").ToString()))
                    Else
                        oRtfHelper.Reemplazar("SGL_Moneda", "")
                    End If

                    If .Item("Precioventa") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_PrecioVenta", Format(CDbl(.Item("Precioventa").ToString()), "#,##0.00"))
                        oRtfHelper.Reemplazar("US_6", sDescripCortoMoneda)
                    Else
                        oRtfHelper.Reemplazar("SGL_PrecioVenta", "")
                        oRtfHelper.Reemplazar("US_6", "")
                    End If


                    If .Item("Importecuotainicial") IsNot DBNull.Value Then
                        If CDbl(.Item("Importecuotainicial").ToString()) > 0 Then

                            If .Item("Cuotainicialporc") IsNot DBNull.Value Then
                                oRtfHelper.Reemplazar("SGL_PorcentajeCuotaInicial", "Cuota Inicial " + Format(CDbl(.Item("Cuotainicialporc").ToString()), "#,##0.00") + "%")
                            Else
                                oRtfHelper.Reemplazar("SGL_PorcentajeCuotaInicial", "")
                            End If

                            oRtfHelper.Reemplazar("SGL_CuotaInicial", Format(CDbl(.Item("Importecuotainicial").ToString()), "#,##0.00"))
                            oRtfHelper.Reemplazar("US_4", ":")
                            oRtfHelper.Reemplazar("SGL_Obl_1", "(*)")
                        Else
                            oRtfHelper.Reemplazar("SGL_PorcentajeCuotaInicial", "")
                            oRtfHelper.Reemplazar("US_4", "")
                            oRtfHelper.Reemplazar("SGL_CuotaInicial", "")
                            oRtfHelper.Reemplazar("SGL_Obl_1", "")
                        End If
                    Else
                        oRtfHelper.Reemplazar("SGL_PorcentajeCuotaInicial", "")
                        oRtfHelper.Reemplazar("US_4", "")
                        oRtfHelper.Reemplazar("SGL_CuotaInicial", "")
                        oRtfHelper.Reemplazar("SGL_Obl_1", "")
                    End If


                    If .Item("Numerocuotas") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_Cuotas", .Item("Numerocuotas").ToString())
                    Else
                        oRtfHelper.Reemplazar("SGL_Cuotas", "")
                    End If



                    If .Item("Plazograciacuota") IsNot DBNull.Value Then
                        If CInt(.Item("Plazograciacuota").ToString()) > 0 Then
                            oRtfHelper.Reemplazar("SGL_NombrePlazoGracia", "Periodo de gracia")
                            oRtfHelper.Reemplazar("SGL_PlazoGracia", .Item("Plazograciacuota").ToString())
                            oRtfHelper.Reemplazar("US_5", ":")
                        Else
                            oRtfHelper.Reemplazar("SGL_NombrePlazoGracia", "")
                            oRtfHelper.Reemplazar("SGL_PlazoGracia", "")
                            oRtfHelper.Reemplazar("US_5", "")
                        End If
                    Else
                        oRtfHelper.Reemplazar("SGL_NombrePlazoGracia", "")
                        oRtfHelper.Reemplazar("SGL_PlazoGracia", "")
                        oRtfHelper.Reemplazar("US_5", "")
                    End If



                    If .Item("NombrePeriodicidad") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_Periodicidad", .Item("NombrePeriodicidad").ToString())
                    Else
                        oRtfHelper.Reemplazar("SGL_Periodicidad", "")
                    End If

                    If .Item("NombreClasificacionbien") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_ClasificacionBien", .Item("NombreClasificacionbien").ToString())
                    Else
                        oRtfHelper.Reemplazar("SGL_ClasificacionBien", "")
                    End If

                    If .Item("NombreFrecuenciapago") IsNot DBNull.Value Then
                        oRtfHelper.Reemplazar("SGL_Frecuencia", .Item("NombreFrecuenciapago").ToString())
                    Else
                        oRtfHelper.Reemplazar("SGL_Frecuencia", "")
                    End If

                    If .Item("Mostrarteacartas") IsNot DBNull.Value Then
                        If CBool(.Item("Mostrarteacartas")) Then
                            If .Item("Teaporc") IsNot DBNull.Value Then
                                oRtfHelper.Reemplazar("SGL_NombreTasa", "Tasa Efectiva Anual")
                                oRtfHelper.Reemplazar("SGL_Tasa", Format(CDbl(.Item("Teaporc").ToString()), "#,##0.00") + "%")
                                'oRtfHelper.Reemplazar("US$_0", "US$ :")
                                oRtfHelper.Reemplazar("US$_0", sDescripCortoMoneda)
                                oRtfHelper.Reemplazar("SGL_Tea", "TEA")
                            Else
                                oRtfHelper.Reemplazar("SGL_Tasa", "")
                                oRtfHelper.Reemplazar("US$_0", "")
                                oRtfHelper.Reemplazar("SGL_NombreTasa", "")
                                oRtfHelper.Reemplazar("SGL_Tea", "")
                            End If
                        Else
                            oRtfHelper.Reemplazar("SGL_Tasa", "")
                            oRtfHelper.Reemplazar("US$_0", "")
                            oRtfHelper.Reemplazar("SGL_NombreTasa", "")
                            oRtfHelper.Reemplazar("SGL_Tea", "")
                        End If
                    Else
                        oRtfHelper.Reemplazar("SGL_Tasa", "")
                        oRtfHelper.Reemplazar("US$_0", "")
                        oRtfHelper.Reemplazar("SGL_NombreTasa", "")
                        oRtfHelper.Reemplazar("SGL_Tea", "")
                    End If

                    '*********************************************
                    ' Mostrar comisiones
                    '*********************************************
                    If .Item("Mostrarmontocomision") IsNot DBNull.Value Then
                        If CBool(.Item("Mostrarmontocomision")) Then

                            'Importeopcioncompra
                            If .Item("Importeopcioncompra") IsNot DBNull.Value Then
                                If CDbl(.Item("Importeopcioncompra").ToString) > 0 Then
                                    oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "Opción de Compra (sin IGV)")
                                    oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", Format(CDbl(.Item("Importeopcioncompra").ToString()), "#,##0.00"))
                                    oRtfHelper.Reemplazar("US$_1", sDescripCortoMoneda)
                                Else
                                    oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", "")
                                    oRtfHelper.Reemplazar("US$_1", "")
                                    oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "")
                                End If
                            Else
                                oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", "")
                                oRtfHelper.Reemplazar("US$_1", "")
                                oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "")
                            End If

                            'Importecomisionactivacion
                            If .Item("Importecomisionactivacion") IsNot DBNull.Value Then
                                If CDbl(.Item("Importecomisionactivacion").ToString) > 0 Then
                                    oRtfHelper.Reemplazar("SGL_NombreComisionAct", "Comisión Activación (sin IGV)")
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionAct", Format(CDbl(.Item("Importecomisionactivacion").ToString()), "#,##0.00"))
                                    oRtfHelper.Reemplazar("US$_2", sDescripCortoMoneda)
                                Else
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionAct", "")
                                    oRtfHelper.Reemplazar("US$_2", "")
                                    oRtfHelper.Reemplazar("SGL_NombreComisionAct", "")
                                End If
                            Else
                                oRtfHelper.Reemplazar("SGL_ImporteComisionAct", "")
                                oRtfHelper.Reemplazar("US$_2", "")
                                oRtfHelper.Reemplazar("SGL_NombreComisionAct", "")
                            End If

                            'Importecomisionestructuracion
                            If .Item("Importecomisionestructuracion") IsNot DBNull.Value Then
                                If CDbl(.Item("Importecomisionestructuracion").ToString) > 0 Then
                                    oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "Comisión de Estructuración (sin IGV)")
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", Format(CDbl(.Item("Importecomisionestructuracion").ToString()), "#,##0.00"))
                                    oRtfHelper.Reemplazar("US$_3", sDescripCortoMoneda)
                                Else
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", "")
                                    oRtfHelper.Reemplazar("US$_3", "")
                                    oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "")
                                End If
                            Else
                                oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", "")
                                oRtfHelper.Reemplazar("US$_3", "")
                                oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "")
                            End If

                        Else

                            'Importeopcioncompra PORCENTAJE
                            If .Item("Opcioncompraporc") IsNot DBNull.Value Then
                                If CDbl(.Item("Opcioncompraporc").ToString) > 0 Then
                                    oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "Opción de Compra (sin IGV)")
                                    oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", Format(CDbl(.Item("Opcioncompraporc").ToString()), "#,##0.00") + "%")
                                    oRtfHelper.Reemplazar("US$_1", sDescripCortoMoneda)
                                Else
                                    oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", "")
                                    oRtfHelper.Reemplazar("US$_1", "")
                                    oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "")
                                End If
                            Else
                                oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", "")
                                oRtfHelper.Reemplazar("US$_1", "")
                                oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "")
                            End If

                            'Importecomisionactivacion PORCENTAJE
                            If .Item("Comisionactivacionporc") IsNot DBNull.Value Then
                                If CDbl(.Item("Comisionactivacionporc").ToString) > 0 Then
                                    oRtfHelper.Reemplazar("SGL_NombreComisionAct", "Comisión Activación (sin IGV)")
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionAct", Format(CDbl(.Item("Comisionactivacionporc").ToString()), "#,##0.00") + "%")
                                    oRtfHelper.Reemplazar("US$_2", sDescripCortoMoneda)
                                Else
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionAct", "")
                                    oRtfHelper.Reemplazar("US$_2", "")
                                    oRtfHelper.Reemplazar("SGL_NombreComisionAct", "")
                                End If
                            Else
                                oRtfHelper.Reemplazar("SGL_ImporteComisionAct", "")
                                oRtfHelper.Reemplazar("US$_2", "")
                                oRtfHelper.Reemplazar("SGL_NombreComisionAct", "")
                            End If

                            'Importecomisionestructuracion PORCENTAJE
                            If .Item("Comisionestructuracionporc") IsNot DBNull.Value Then
                                If CDbl(.Item("Comisionestructuracionporc").ToString) > 0 Then
                                    oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "Comisión de Estructuración (sin IGV)")
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", Format(CDbl(.Item("Comisionestructuracionporc").ToString()), "#,##0.00") + "%")
                                    oRtfHelper.Reemplazar("US$_3", sDescripCortoMoneda)
                                Else
                                    oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", "")
                                    oRtfHelper.Reemplazar("US$_3", "")
                                    oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "")
                                End If
                            Else
                                oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", "")
                                oRtfHelper.Reemplazar("US$_3", "")
                                oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "")
                            End If

                        End If
                    Else
                        oRtfHelper.Reemplazar("SGL_ImporteOpcionCompra", "")
                        oRtfHelper.Reemplazar("US$_1", "")
                        oRtfHelper.Reemplazar("SGL_NombreOpcionCompra", "")
                        oRtfHelper.Reemplazar("SGL_ImporteComisionAct", "")
                        oRtfHelper.Reemplazar("US$_2", "")
                        oRtfHelper.Reemplazar("SGL_NombreComisionAct", "")
                        oRtfHelper.Reemplazar("SGL_ImporteComisionEstruc", "")
                        oRtfHelper.Reemplazar("US$_3", "")
                        oRtfHelper.Reemplazar("SGL_NombreComisionEstruc", "")
                    End If


                    strVersionCotizacion = .Item("versioncotizacion").ToString
                End With

                Dim objECotizacion As New EGcc_cotizacion
                Dim strECotizacion As String
                With objECotizacion
                    .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
                    .Versioncotizacion = GCCUtilitario.StringToInteger(strVersionCotizacion)
                End With
                strECotizacion = GCCUtilitario.SerializeObject(objECotizacion)

                Dim decMontoCuotaComun As Decimal = 0

                'Valida Cronograma
                Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)
                Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.CotizacionCronogramaGet(strECotizacion))
                'If decCuotaInicial <= 0 Then
                '    dtCronograma.Rows.RemoveAt(0)
                'End If

                'GestionCronograma
                Dim mCronograma(1, 5) As String
                ReDim mCronograma(dtCronograma.Rows.Count + 1, 5)
                mCronograma = Cronograma(dtCronograma, True, decMontoCuotaComun, decCuotaInicial)

                'oRtfHelper.Reemplazar("SGL_MontoCuota", decMontoCuotaComun.ToString())
                oRtfHelper.Reemplazar("SGL_MontoCuota", Format(CDbl(decMontoCuotaComun.ToString()), "#,##0.00"))
                If decMontoCuotaComun > 0 Then
                    oRtfHelper.Reemplazar("US_7", sDescripCortoMoneda)
                Else
                    oRtfHelper.Reemplazar("US_7", "")
                End If

                ' Datos del cronograma
                oRtfHelper.TablaCronograma("SGL_TablaCronograma", mCronograma)

                oRtfHelper.SaveAndClose()
            End If
            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()
            Return ex.ToString()
        End Try
    End Function

    Public Function CartaProveedor(ByVal pdwProveedor As DataView, ByVal pstrNumeroContrato As String) As String
        Dim oRtfHelper As RtfHelper = Nothing
        Dim sRutaParcial As String = String.Empty
        ' Define la ruta de archivos origen y destino
        Try
            If pdwProveedor IsNot Nothing Then
                If pdwProveedor.Count = 1 Then
                    With pdwProveedor(0)

                        Dim nombreArchivo As String = GCCUtilitario.Concatenar("Proveedor-", pstrNumeroContrato, "-", .Item("CodProveedor").ToString(), ".doc")
                        sRutaParcial = RutaParcial(nombreArchivo, GCCConstante.C_DIRECTORIO_ANEXOS)
                        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial
                        If .Item("TipoProductoFinancieroActivo").ToString() = GCCConstante.C_COD_CLASIFICACION_BIEN_BIENES_INMUEBLES Then
                            oRtfHelper = New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/CARTA_Proveedor_OtroBien.dot"), rutaAnexo)
                        Else
                            oRtfHelper = New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/CARTA_Proveedor.dot"), rutaAnexo)
                        End If

                        If .Item("NombreInstitucion") IsNot DBNull.Value Then
                            oRtfHelper.Reemplazar("SGL_NombreProveedor", .Item("NombreInstitucion").ToString().Trim)
                        Else
                            oRtfHelper.Reemplazar("SGL_NombreProveedor", "")
                        End If

                        If .Item("CodProductoFinancieroActivo") IsNot DBNull.Value Then
                            If .Item("CodProductoFinancieroActivo").ToString = GCCConstante.C_CODLPC_PROD_LEASING Then
                                oRtfHelper.Reemplazar("SGL_TipoContrato", GCCConstante.C_DESGCC_PROD_LEASING)
                            Else
                                oRtfHelper.Reemplazar("SGL_TipoContrato", GCCConstante.C_DESGCC_PROD_LEASEBACK)
                            End If
                        Else
                            oRtfHelper.Reemplazar("SGL_TipoContrato", "")
                        End If
                        oRtfHelper.Reemplazar("SGL_NumContrato", pstrNumeroContrato)


                        If .Item("NombreCliente") IsNot DBNull.Value Then
                            oRtfHelper.Reemplazar("SGL_NombreCliente", .Item("NombreCliente").ToString().Trim)
                        Else
                            oRtfHelper.Reemplazar("SGL_NombreCliente", "")
                        End If

                        If .Item("NombreMoneda") IsNot DBNull.Value Then
                            oRtfHelper.Reemplazar("SGL_NombreMoneda", .Item("NombreMoneda").ToString().Trim)
                        Else
                            oRtfHelper.Reemplazar("SGL_NombreMoneda", "")
                        End If

                        If .Item("DescripCortoMoneda") IsNot DBNull.Value Then
                            oRtfHelper.Reemplazar("SGL_DescripCortoMoneda", .Item("DescripCortoMoneda").ToString().Trim)
                        Else
                            oRtfHelper.Reemplazar("SGL_DescripCortoMoneda", "")
                        End If

                        If .Item("Importe") IsNot DBNull.Value Then
                            oRtfHelper.Reemplazar("SGL_ImporteFacturar", Math.Round(GCCUtilitario.CheckDecimal(.Item("Importe").ToString()), 2).ToString("#,###,###,##0.00"))
                        Else
                            oRtfHelper.Reemplazar("SGL_ImporteFacturar", "0.00")
                        End If

                        If .Item("DescripcionBien") IsNot DBNull.Value Then
                            oRtfHelper.Reemplazar("SGL_DescripcionBien", .Item("DescripcionBien").ToString().Trim)
                        Else
                            oRtfHelper.Reemplazar("SGL_DescripcionBien", "")
                        End If

                        oRtfHelper.Reemplazar("SGL_EjecutivoLeasing", GCCSession.NombreUsuario.ToString.Replace("*", "").TrimEnd())

                    End With

                    oRtfHelper.SaveAndClose()
                End If
            End If

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()
            Return ex.ToString()
        End Try
    End Function


#Region "Tipos de Anexos"

    ''' <summary>
    ''' Producto financiero: Leasing directo.
    ''' Clasificación del contrato: Embarcación pesquera.
    ''' TipoRubroFinanciamiento: Bien inmueble.
    ''' Con cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingEmbarcacionConCesion(ByVal pCodigoContrato As String, _
                                                ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Embarcacion-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Inmueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Texto del cesionario.
            Dim cesionario(0) As String
            cesionario = TextoPredefinido(dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: Leasing directo.
    ''' Clasificación del contrato: Embarcación pesquera.
    ''' TipoRubroFinanciamiento: Bien inmueble.
    ''' Sin cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirectoEmbarcacion(ByVal pCodigoContrato As String, _
                                              ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Embarcacion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 7 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 ' Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Inmueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                              bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                              mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: Leasing directo.
    ''' Tipo de anexo: Inmueble.
    ''' TipoRubroFinanciamiento:  Inmueble.
    ''' Con cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingInmuebleConCesion(ByVal pCodigoContrato As String, _
                                             ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Inmueble-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Inmueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Texto del cesionario.
            Dim cesionario(0) As String
            cesionario = TextoPredefinido(dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                              bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Tipo de anexo: Inmueble.
    ''' Producto financiero: Leasing directo.
    ''' TipoRubroFinanciamiento:  Inmueble.
    ''' Sin cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirectoInmueble(ByVal pCodigoContrato As String, _
                                           ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Inmuebles.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 7 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 ' Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Inmueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                              bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                              mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: LeaseBack.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Sin cesión de posición contractual.
    ''' Procedencia: Local.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeaseBackMueble(ByVal pCodigoContrato As String, _
                                    ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Lease-Back-Bienes-Muebles.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda ' As String
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                             representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                             representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                              mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: LeaseBack.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Con cesión de posición contractual.
    ''' Procedencia: Local.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeaseBackMuebleConCesion(ByVal pCodigoContrato As String, _
                                             ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Lease-Back-Mueble-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            Dim cesionario(0) As String
            ' Texto del cesionario.
            cesionario = TextoPredefinido(dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                              mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto: Leasing directo.
    ''' Tipo de anexo: Vehículo.
    ''' TipoRubroFinanciamiento: Unidad de transporte terrestre liviano o pesado.
    ''' Sin cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirectoVehicular(ByVal pCodigoContrato As String, _
                                            ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Vehicular.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1, 1) As String
            bienes = Vehicular(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            ' Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                             representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                             representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                             proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: Leasing directo.
    ''' Tipo de anexo: Vehículo.
    ''' TipoRubroFinanciamiento: Unidad de transporte terrestre liviano o pesado.
    ''' Con cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirectoVehicularConCesion(ByVal pCodigoContrato As String, _
                                                     ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Vehicular-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1, 1) As String
            bienes = Vehicular(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Texto del cesionario.
            Dim cesionario(0) As String
            cesionario = TextoPredefinido(dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: LeaseBack.
    ''' Tipo de anexo: Vehiculo.
    ''' TipoRubroFinanciamiento: Unidad de transporte terrestre ligero y pesado.
    ''' Sin cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeaseBackVehicular(ByVal pCodigoContrato As String, _
                                       ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = "" + pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Lease-Back-Vehicular.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1, 1) As String
            bienes = Vehicular(dtBienes)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            ' Cotización
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                             representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                             representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: LeaseBack.
    ''' Tipo de anexo: Vehículo.
    ''' TipoRubroFinanciamiento: Unidad de transporte terrestre liviano o pesado.
    ''' Con cesión de posición contractual
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeaseBackVehicularConCesion(ByVal pCodigoContrato As String, _
                                                ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Lease-Back-Vehicular-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1, 2) As String
            bienes = Vehicular(dtBienes)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Texto del cesionario.
            Dim cesionario(0) As String
            cesionario = TextoPredefinido(dtContrato)
            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto: Leasing directo.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Sin cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirectoMueble(ByVal pCodigoContrato As String, _
                                         ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Bienes-Muebles.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 7 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 ' Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda ' As String
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                             representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                             representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                             proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto: Leasing directo.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Subtipo de contrato: Pago parcial.
    ''' Procedencia: Local.
    ''' Sin cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirectoMueblePagoParcial(ByVal pCodigoContrato As String, _
                                                    ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Bienes-Muebles-PagoParcial.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 7 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            ' Cotización
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto: Leasing directo.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Con cesión de posición contractual.
    ''' Procedencia: Local.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirecto_MuebleConCesion(ByVal pCodigoContrato As String, _
                                                   ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Bienes-Muebles-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Texto del cesionario.
            Dim cesionario(0) As String
            cesionario = TextoPredefinido(dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                             representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                             representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                             bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                             proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto financiero: Leasing.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Sin cesión de posición contractual.
    ''' Procedencia: Importación.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirecto_MuebleImportacion(ByVal pCodigoContrato As String, _
                                                     ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Bienes-Muebles-Importacion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 1, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 1, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGccCotizacion As New EGcc_cotizacion
            Dim eGccCotizacion As String

            With objEGccCotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            eGccCotizacion = GCCUtilitario.SerializeObject(objEGccCotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(eGccCotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                              bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Producto: Leasing directo.
    ''' Tipo de anexo: Mueble.
    ''' TipoRubroFinanciamiento: Maquinaria y equipo industrial, equipo de oficina, equipo de movimiento de tierra, sistema
    ''' de procesamiento y otros.
    ''' Procedencia: Importación.
    ''' Con cesión de posición contractual.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodigoCotizacion">Número de cotización</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function LeasingDirecto_MuebleImportacionConCesion(ByVal pCodigoContrato As String, _
                                                              ByVal pCodigoCotizacion As String) As String
        ' Define la ruta de archivos origen y destino
        Dim nombreArchivo As String = pCodigoContrato + ".doc"
        Dim sRutaParcial As String = RutaParcial(nombreArchivo, _
                                                 GCCConstante.C_DIRECTORIO_ANEXOS)
        Dim rutaAnexo As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer") + sRutaParcial

        Dim oRtfHelper As New RtfHelper(HttpContext.Current.Server.MapPath("../Util/Plantillas/ANEXO-Leasing-Directo-Bienes-Muebles-Importacion-con-Cesion.dot"), _
                                        rutaAnexo)

        Try

            Dim objContratoNTx As New LContratoNTx
            Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pCodigoContrato))

            Dim objRepresentanteContratoNTx As New LCheckListNTx()
            Dim oEGccRepresentanteBanco As New EGcc_contratorepresentante
            Dim strEGccontratoBanco As String

            With oEGccRepresentanteBanco
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_BANCO)
            End With
            strEGccontratoBanco = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteBanco)
            Dim dtRepresentanteBanco As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                        1, _
                                                                                                                                                        "CodigoRepresentante", _
                                                                                                                                                        "desc", _
                                                                                                                                                        strEGccontratoBanco, _
                                                                                                                                                        Nothing))

            Dim representanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 2, 1) As String
            representanteBanco = RepresentantesBanco(dtRepresentanteBanco)

            Dim oEGccRepresentanteCliente As New EGcc_contratorepresentante
            Dim strEGccontratoCliente As String
            With oEGccRepresentanteCliente
                .Numerocontrato = GCCUtilitario.NullableString(pCodigoContrato)
                .Codigotiporepresentante = GCCUtilitario.NullableString(GCCConstante.C_REPRESENTANTE_CLIENTE)
            End With
            strEGccontratoCliente = GCCUtilitario.SerializeObject(Of EGcc_contratorepresentante)(oEGccRepresentanteCliente)
            Dim dtRepresentanteCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objRepresentanteContratoNTx.RepresentantesContratoSel(1000, _
                                                                                                                                                          1, _
                                                                                                                                                          "CodigoRepresentante", _
                                                                                                                                                          "desc", _
                                                                                                                                                          strEGccontratoCliente, _
                                                                                                                                                          Nothing))
            Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 2, 1) As String
            representanteCliente = RepresentantesCliente(dtRepresentanteCliente)

            Call FirmaContratoYJuridiccion(oRtfHelper, _
                                           dtRepresentanteCliente)

            Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                                  1, _
                                                                                                                  "SecFinanciamiento", _
                                                                                                                  "desc", _
                                                                                                                  pCodigoContrato, _
                                                                                                                  Nothing))
            ' Número de filas
            Dim f As Integer
            Dim esMismaUbicacion As Boolean
            Dim totalFilas As Integer
            esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
            ' Se le agrega un espacio adicional como elemento divisor
            If esMismaUbicacion Then
                f = 6 + 1
                totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            Else
                f = 8 + 1
                totalFilas = f * dtBienes.Rows.Count
            End If

            Dim bienes(totalFilas - 1) As String
            bienes = Mueble(dtBienes)

            ' Lista de proveedores
            Dim dtProveedores As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratoProveedor(10000, _
                                                                                                                                   1, _
                                                                                                                                   "NombreInstitucion", _
                                                                                                                                   "desc", _
                                                                                                                                   pCodigoContrato))
            Dim proveedores(dtProveedores.Rows.Count - 1) As String
            proveedores = Proveedor(dtProveedores)

            ' Datos del contrato
            Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContrato(pCodigoContrato))
            Call Contrato(oRtfHelper, _
                          dtContrato)

            ' Texto del cesionario.
            Dim cesionario(0) As String
            cesionario = TextoPredefinido(dtContrato)

            ' Datos de la cotización
            Dim objCotizacionNTx As New LCotizacionNTx

            ' Datos de las tarifas
            Dim dtTarifas As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarTarifarioContrato(pCodigoContrato))
            Call Tarifas(oRtfHelper, _
                         dtTarifas)

            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String

            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(dtCotizacion.Rows(0).Item("Importecuotainicial").ToString)

            Dim oLwsCotizacion As New LCotizacionNTx
            Dim tbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(pCodigoCotizacion))
            Dim btieneSeguro As Boolean = TieneSeguro(tbCronograma)
            Dim mCronograma(1, 5) As Celda
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 5)
            Else
                ReDim mCronograma(tbCronograma.Rows.Count + 1, 4)
            End If
            mCronograma = CronogramaRtf(tbCronograma, , , decCuotaInicial)

            oRtfHelper.Reemplazar("SGL_NumContrato", pCodigoContrato)

            ' Datos del cliente
            Call Cliente(oRtfHelper, _
                         dtCliente)

            ' Representantes del cliente
            oRtfHelper.Tabla("SGL_RepresentanteCliente", _
                              representanteCliente)

            ' Representantes del banco
            oRtfHelper.Tabla("SGL_RepresentanteBanco", _
                              representanteBanco)

            ' Cesionario - texto predefinido
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_TextoPredefinidoCesion", _
                             cesionario)

            ' Datos de los bienes
            oRtfHelper.Tabla(MargenIzquierdo, _
                             "SGL_Bienes", _
                              bienes)

            ' Datos del proveedor
            oRtfHelper.Tabla("SGL_RazonSocialProveedor", _
                              proveedores)

            ' Datos de la cotización
            Call Cotizacion(oRtfHelper, _
                            dtCotizacion)

            ' Fecha actual
            Dim myCIintl As New CultureInfo("es-PE", False)
            oRtfHelper.Reemplazar("SGL_FechaActual", Now.ToString("dd' de 'MMMM' de 'yyyy", myCIintl))
            ' Nombre del archivo
            oRtfHelper.Reemplazar("SGL_archivo", nombreArchivo)

            ' Datos del cronograma
            oRtfHelper.Tabla("SGL_Cronograma", _
                             mCronograma)

            oRtfHelper.SaveAndClose()

            Dim objContratoTx As New LContratoTx
            Dim objESolicitudCredito As New ESolicitudcredito
            Dim stroESolicitudCredito As String

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.ArchivoContratoAdjunto = sRutaParcial
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            stroESolicitudCredito = GCCUtilitario.SerializeObject(Of ESolicitudcredito)(objESolicitudCredito)
            objContratoTx.ActualizaArchivoContratoAdjunto(stroESolicitudCredito)

            Return sRutaParcial
        Catch ex As Exception
            oRtfHelper.Close()

            Return ex.ToString()
        End Try
    End Function

#End Region

#Region "Métodos privados"

    ''' <summary>
    ''' Lista los datos del cliente, recorre el documento reemplazando las etiquetas predefinidas con los datos correspondientes.
    ''' </summary>
    ''' <param name="oRtfHelper"></param>
    ''' <param name="dtCliente"></param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub Cliente(ByRef oRtfHelper As RtfHelper, _
                        ByVal dtCliente As DataTable)
        For Each oRow As DataRow In dtCliente.Rows
            If Not oRow.Item("NombreSubprestatario") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_RazonSocialCliente", oRow.Item("NombreSubprestatario").ToString().Trim())
            Else
                oRtfHelper.Reemplazar("SGL_RazonSocialCliente", "")
            End If
            If Not oRow.Item("DocIdentificacion") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_TipoDocumento", oRow.Item("DocIdentificacion").ToString().Trim())
            Else
                oRtfHelper.Reemplazar("SGL_TipoDocumento", "")
            End If
            If Not oRow.Item("Direccion") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_Direccion", oRow.Item("Direccion").ToString().Trim())
            Else
                oRtfHelper.Reemplazar("SGL_Direccion", "")
            End If
            If Not oRow.Item("NumDocIdentificacion") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_NumDocumento", oRow.Item("NumDocIdentificacion").ToString().Trim())
            Else
                oRtfHelper.Reemplazar("SGL_NumDocumento", "")
            End If
            If Not oRow.Item("ProvinciaNombre") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_Provincia", oRow.Item("ProvinciaNombre").ToString().Trim())
            Else
                oRtfHelper.Reemplazar("SGL_Provincia", "")
            End If
            If Not oRow.Item("DepartamentoNombre") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_Dpto", oRow.Item("DepartamentoNombre").ToString().Trim())
            Else
                oRtfHelper.Reemplazar("SGL_Dpto", "")
            End If
        Next oRow
    End Sub

    Private Sub Cotizacion(ByRef oRtfHelper As RtfHelper, _
                           ByVal dtCotizacion As DataTable)
        For Each oRow As DataRow In dtCotizacion.Rows
            If Not oRow.Item("Precuotaporc") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_TasaPrecuota", Format(CDbl(oRow.Item("Precuotaporc")), "#,##0.00"))
            Else
                oRtfHelper.Reemplazar("SGL_TasaPrecuota", "0")
            End If
            Dim periodoDisponible As Integer
            Dim fechaOfertaValida As Date
            If Not oRow.Item("PeriodoDisponible") Is DBNull.Value Then
                periodoDisponible = CInt(oRow.Item("PeriodoDisponible"))
            Else
                periodoDisponible = 0
            End If
            If Not oRow.Item("FechaOfertaValida") Is DBNull.Value Then
                fechaOfertaValida = GCCUtilitario.StringToDateTime(oRow.Item("FechaOfertaValida").ToString).Value
                'Inicio IBK - AAE - Comento el adicionar días
                'fechaOfertaValida = fechaOfertaValida.AddDays(periodoDisponible)
                ' Fin IBK

                oRtfHelper.Reemplazar("SGL_PlazoDisponibilidad", fechaOfertaValida.ToString("dd/MM/yyyy"))
            Else
                oRtfHelper.Reemplazar("SGL_PlazoDisponibilidad", "")
            End If

            If oRow.Item("ImporteCuotaInicial") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_CuotaInicial", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_CuotaInicial", Format(CDbl(oRow.Item("ImporteCuotaInicial")), "#,##0.00"))
            End If
            If oRow.Item("ValorVenta") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_ValorVenta", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_ValorVenta", Format(CDbl(oRow.Item("ValorVenta")), "#,##0.00"))
            End If
            If oRow.Item("ValorVentaIGV") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_MontoIGV", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_MontoIGV", Format(CDbl(oRow.Item("ValorVentaIGV")), "#,##0.00"))
            End If
            If oRow.Item("PrecioVenta") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_PrecioVenta", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_PrecioVenta", Format(CDbl(oRow.Item("PrecioVenta")), "#,##0.00"))
            End If
            If oRow.Item("ImporteOpcionCompra") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_ValorOpcionCompra", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_ValorOpcionCompra", Format(CDbl(oRow.Item("ImporteOpcionCompra")), "#,##0.00"))
            End If
            If Not oRow.Item("NombrePeriodicidad") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_PeriodicidadCuota", oRow.Item("NombrePeriodicidad").ToString.Trim())
            Else
                oRtfHelper.Reemplazar("SGL_PeriodicidadCuota", "0")
            End If
            If Not oRow.Item("NombrePeriodicidadCuota") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_FrecuenciaPagoCuota", oRow.Item("NombrePeriodicidadCuota").ToString.Trim())
            Else
                oRtfHelper.Reemplazar("SGL_FrecuenciaPagoCuota", "0")
            End If
            If Not oRow.Item("Fechamaxactivacion") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_FechaMaxActivacion", CDate(GCCUtilitario.StringToDateTime(oRow.Item("Fechamaxactivacion").ToString)).ToString("dd/MM/yyyy"))
            Else
                oRtfHelper.Reemplazar("SGL_FechaMaxActivacion", "")
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Recorre el documento reemplazando la etiqueta de texto predefinido por su respectiva tabla.
    ''' </summary>
    ''' <param name="dtContrato"></param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function TextoPredefinido(ByVal dtContrato As DataTable) As String()
        Dim textoPredef(dtContrato.Rows.Count - 1) As String

        Dim j As Integer = 0
        For i As Integer = 0 To dtContrato.Rows.Count - 1
            If Not dtContrato.Rows(i).Item("CPCTextoPredefinido") Is DBNull.Value Then
                Dim texto As String
                texto = dtContrato.Rows(i).Item("CPCTextoPredefinido").ToString().Trim()
                If Not RepiteElemento(textoPredef, texto) Then
                    textoPredef(j) = texto
                    j = j + 1
                End If
            End If
        Next i

        Return textoPredef
    End Function

    ''' <summary>
    ''' Recorre el documento reemplazando las etiquetas predefinidas con los datos correspondientes al contrato y a GCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <param name="oRtfHelper"></param>
    ''' <param name="dtContrato"></param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub Contrato(ByRef oRtfHelper As RtfHelper, _
                         ByVal dtContrato As DataTable)
        For Each oRow As DataRow In dtContrato.Rows
            Dim oNumToLetras As New NumToLetras

            ' Se reconfigura para que soporte ahora sólo dos decimales
            If oRow.Item("ImportePendiente") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_ImporteAtrasoPorc", "0.000")
            Else
                oRtfHelper.Reemplazar("SGL_ImporteAtrasoPorc", Format(CDbl(oRow.Item("ImportePendiente")), "#,##0.000"))
            End If
            ' Días de vencimiento
            If Not oRow.Item("DiasVencimiento") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_Letras_DiasVencimiento", oNumToLetras.Numero2Letra(oRow.Item("DiasVencimiento").ToString))

                Dim diasVencimientoDosDigitos As Integer = CInt(oRow.Item("DiasVencimiento"))
                Dim sDiasVencimientoDosDigitos As String
                ' Si el número es de un dígito, se completa con "0" (debe ser al menos dos dígitos).
                If diasVencimientoDosDigitos > 9 Then
                    sDiasVencimientoDosDigitos = diasVencimientoDosDigitos.ToString()
                Else
                    sDiasVencimientoDosDigitos = "0" + diasVencimientoDosDigitos.ToString()
                End If
                oRtfHelper.Reemplazar("SGL_DiasVencimiento_DosDigitos", sDiasVencimientoDosDigitos)
            Else
                oRtfHelper.Reemplazar("SGL_Letras_DiasVencimiento", "")
                oRtfHelper.Reemplazar("SGL_DiasVencimiento_DosDigitos", "0")
            End If
            ' % De la cuota
            If oRow.Item("PorcentajeCuota") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_PorcDeCuota", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_PorcDeCuota", Format(CDbl(oRow.Item("PorcentajeCuota")), "#,##0.00"))
            End If

            If oRow.Item("ComisionActivacion") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_ComisionActivacion", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_ComisionActivacion", Format(CDbl(oRow.Item("ComisionActivacion")), "#,##0.00"))
            End If
            If Not oRow.Item("NombreMoneda") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_MonedaContrato", oRow.Item("NombreMoneda").ToString)
                oRtfHelper.Reemplazar("SGL_NombreMoneda", oRow.Item("NombreMoneda").ToString)
            Else
                oRtfHelper.Reemplazar("SGL_MonedaContrato", "")
                oRtfHelper.Reemplazar("SGL_NombreMoneda", "")
            End If
            If Not oRow.Item("DescripCortoMoneda") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_DescripCortoMoneda", oRow.Item("DescripCortoMoneda").ToString)
            Else
                oRtfHelper.Reemplazar("SGL_DescripCortoMoneda", "")
            End If
            If Not oRow.Item("LugarFirmaContrato") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_OficinaRegistral", oRow.Item("LugarFirmaContrato").ToString)
            Else
                oRtfHelper.Reemplazar("SGL_OficinaRegistral", "")
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Recorre el documento reemplazando las etiquetas predefinidas con los datos correspondientes a las tarifas que afectan el anexo.
    ''' </summary>
    ''' <param name="oRtfHelper"></param>
    ''' <param name="dtTarifas"></param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub Tarifas(ByRef oRtfHelper As RtfHelper, _
                        ByVal dtTarifas As DataTable)
        For Each oRow As DataRow In dtTarifas.Rows
            Dim oNumToLetras As New NumToLetras

            If Not oRow.Item("TarifarioMoneda") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_TarifarioMoneda", oRow.Item("TarifarioMoneda").ToString)
            Else
                oRtfHelper.Reemplazar("SGL_TarifarioMoneda", "")
            End If
            If Not oRow.Item("MontoAtrasoPago") Is DBNull.Value Then
                Dim montoAtrasoPago As String = Format(CDbl(oRow.Item("MontoAtrasoPago")), "#,##0.00")
                oRtfHelper.Reemplazar("SGL_TarifarioMontoAtrasoPago", montoAtrasoPago)
                oRtfHelper.Reemplazar("SGL_LetrasMontoAtrasoPago", oNumToLetras.NumeroALetraBancaria(montoAtrasoPago))
            Else
                oRtfHelper.Reemplazar("SGL_TarifarioMontoAtrasoPago", "")
                oRtfHelper.Reemplazar("SGL_LetrasMontoAtrasoPago", "")
            End If
            If Not oRow.Item("MontoAtrasoDevolucion") Is DBNull.Value Then
                Dim montoAtrasoDevolucion As String = Format(CDbl(oRow.Item("MontoAtrasoDevolucion")), "#,##0.00")
                oRtfHelper.Reemplazar("SGL_TarifarioMontoAtrasoDevolucion", montoAtrasoDevolucion)
                oRtfHelper.Reemplazar("SGL_LetrasMontoAtrasoDevolucion", oNumToLetras.NumeroALetraBancaria(montoAtrasoDevolucion))
            Else
                oRtfHelper.Reemplazar("SGL_TarifarioMontoAtrasoDevolucion", "")
                oRtfHelper.Reemplazar("SGL_LetrasMontoAtrasoDevolucion", "")
            End If

            If oRow.Item("MontoMaxSentencia") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_TarifarioMontoMaxSentencia", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_TarifarioMontoMaxSentencia", Format(CDbl(oRow.Item("MontoMaxSentencia")), "#,##0.00"))
            End If

            If oRow.Item("MontoPenalidadActivacion") Is DBNull.Value Then
                oRtfHelper.Reemplazar("SGL_TarifarioPenalidadActivacion", "0.00")
            Else
                oRtfHelper.Reemplazar("SGL_TarifarioPenalidadActivacion", Format(CDbl(oRow.Item("MontoPenalidadActivacion")), "#,##0.00"))
            End If
        Next oRow
    End Sub

    ''' <summary>
    ''' Lista los representantes del cliente incluidos en el contrato.
    ''' </summary>
    ''' <param name="dtRepresentanteCliente"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function RepresentantesCliente(ByVal dtRepresentanteCliente As DataTable) As String(,)
        Dim representanteCliente(dtRepresentanteCliente.Rows.Count * 4 - 2, 1) As String

        For i As Integer = 0 To dtRepresentanteCliente.Rows.Count - 1
            representanteCliente(0 + 4 * i, 0) = EspaciosIniciales + "Nombre(s) y Apellido(s)"
            If Not dtRepresentanteCliente.Rows(i).Item("NombreRepresentante") Is DBNull.Value Then
                representanteCliente(0 + 4 * i, 1) = dtRepresentanteCliente.Rows(i).Item("NombreRepresentante").ToString().Trim()
            Else
                representanteCliente(0 + 4 * i, 1) = ""
            End If
            representanteCliente(1 + 4 * i, 0) = EspaciosIniciales + "Documento de identidad"

            Dim tipoDocumento, nroDocumento As String
            If Not dtRepresentanteCliente.Rows(i).Item("TipoDocumento") Is DBNull.Value Then
                tipoDocumento = dtRepresentanteCliente.Rows(i).Item("TipoDocumento").ToString().Trim() + " "
            Else
                tipoDocumento = ""
            End If
            If Not dtRepresentanteCliente.Rows(i).Item("NroDocumento") Is DBNull.Value Then
                nroDocumento = dtRepresentanteCliente.Rows(i).Item("NroDocumento").ToString().Trim()
            Else
                nroDocumento = ""
            End If
            representanteCliente(1 + 4 * i, 1) = tipoDocumento + "Nro. " + nroDocumento

            representanteCliente(2 + 4 * i, 0) = EspaciosIniciales + "Datos de inscripción del poder"

            Dim partida As String = "", registro As String = ""
            If Not dtRepresentanteCliente.Rows(i).Item("NroDocumento") Is DBNull.Value Then
                partida = dtRepresentanteCliente.Rows(i).Item("PartidaRegistral").ToString().Trim()
            End If
            If Not dtRepresentanteCliente.Rows(i).Item("NroDocumento") Is DBNull.Value Then
                registro = dtRepresentanteCliente.Rows(i).Item("OficinaRegistral").ToString().Trim()
            End If

            representanteCliente(2 + 4 * i, 1) = "Partida Nº " + partida + " del Registro de Personas Jurídicas de " + registro
            If i <> dtRepresentanteCliente.Rows.Count - 1 Then
                representanteCliente(3 + 4 * i, 0) = ""
                representanteCliente(3 + 4 * i, 1) = ""
            End If
        Next i

        Return representanteCliente
    End Function

    Public Sub FirmaContratoYJuridiccion(ByRef oRtfHelper As RtfHelper, _
                                         ByVal dtRepresentanteCliente As DataTable)
        If dtRepresentanteCliente.Rows.Count > 0 Then

            Dim oRow As DataRow = dtRepresentanteCliente.Rows(0)
            ' AAE - 10/09/2012 - Cambio Departamento por provincia
            'If Not oRow.Item("Departamento") Is DBNull.Value Then
            If Not oRow.Item("Provincia") Is DBNull.Value Then
                ' Departamento de la firma del contrato.
                'oRtfHelper.Reemplazar("SGL_DepartamentoContrato", oRow.Item("Departamento").ToString().Trim())
                oRtfHelper.Reemplazar("SGL_DepartamentoContrato", oRow.Item("Provincia").ToString().Trim())
                ' Jurisdicción aplicable
                'oRtfHelper.Reemplazar("SGL_DepartamentoJuridiccion", oRow.Item("Departamento").ToString().Trim())
                oRtfHelper.Reemplazar("SGL_DepartamentoJuridiccion", oRow.Item("Provincia").ToString().Trim())

            Else
                If Not oRow.Item("Departamento") Is DBNull.Value Then
                    ' Departamento de la firma del contrato.
                    'oRtfHelper.Reemplazar("SGL_DepartamentoContrato", oRow.Item("Departamento").ToString().Trim())
                    oRtfHelper.Reemplazar("SGL_DepartamentoContrato", oRow.Item("Departamento").ToString().Trim())
                    ' Jurisdicción aplicable
                    'oRtfHelper.Reemplazar("SGL_DepartamentoJuridiccion", oRow.Item("Departamento").ToString().Trim())
                    oRtfHelper.Reemplazar("SGL_DepartamentoJuridiccion", oRow.Item("Departamento").ToString().Trim())
                Else
                    oRtfHelper.Reemplazar("SGL_DepartamentoContrato", "")
                    oRtfHelper.Reemplazar("SGL_DepartamentoJuridiccion", "")
                End If
            End If
            ' FIN AAE
        Else
            oRtfHelper.Reemplazar("SGL_DepartamentoContrato", "")
            oRtfHelper.Reemplazar("SGL_DepartamentoJuridiccion", "")
        End If
    End Sub

    ''' <summary>
    ''' Lista los representantes del banco que incluidos en el contrato
    ''' </summary>
    ''' <param name="dtRepresentanteBanco"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function RepresentantesBanco(ByVal dtRepresentanteBanco As DataTable) As String(,)
        Dim mRepresentanteBanco(dtRepresentanteBanco.Rows.Count * 4 - 2, 1) As String

        For i As Integer = 0 To dtRepresentanteBanco.Rows.Count - 1
            mRepresentanteBanco(0 + 4 * i, 0) = EspaciosIniciales + "Nombre(s) y Apellido(s)"
            If Not dtRepresentanteBanco.Rows(i).Item("NombreRepresentante") Is DBNull.Value Then
                mRepresentanteBanco(0 + 4 * i, 1) = dtRepresentanteBanco.Rows(i).Item("NombreRepresentante").ToString().Trim()
            Else
                mRepresentanteBanco(0 + 4 * i, 1) = ""
            End If
            mRepresentanteBanco(1 + 4 * i, 0) = EspaciosIniciales + "Documento de identidad"
            If Not dtRepresentanteBanco.Rows(i).Item("NroDocumento") Is DBNull.Value Then
                mRepresentanteBanco(1 + 4 * i, 1) = "DNI Nº " + dtRepresentanteBanco.Rows(i).Item("NroDocumento").ToString().Trim()
            Else
                mRepresentanteBanco(1 + 4 * i, 1) = ""
            End If
            mRepresentanteBanco(2 + 4 * i, 0) = EspaciosIniciales + "Datos de inscripción del poder"

            Dim partida As String = "", registro As String = ""
            If Not dtRepresentanteBanco.Rows(i).Item("NroDocumento") Is DBNull.Value Then
                partida = dtRepresentanteBanco.Rows(i).Item("PartidaRegistral").ToString().Trim()
            End If
            If Not dtRepresentanteBanco.Rows(i).Item("NroDocumento") Is DBNull.Value Then
                registro = dtRepresentanteBanco.Rows(i).Item("OficinaRegistral").ToString().Trim()
            End If
            mRepresentanteBanco(2 + 4 * i, 1) = "Partida Nº " + partida + " del Registro de Personas Jurídicas de " + registro
            If i <> dtRepresentanteBanco.Rows.Count - 1 Then
                mRepresentanteBanco(3 + 4 * i, 0) = ""
                mRepresentanteBanco(3 + 4 * i, 1) = ""
            End If
        Next i

        Return mRepresentanteBanco
    End Function

    ''' <summary>
    ''' Lista los proveedores que incluidos en el contrato.
    ''' Los proveedores sólo se listan una vez. La dimensión del vector es actualizada con el número de elementos no repetidos.
    ''' </summary>
    ''' <param name="dtProveedores"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function Proveedor(ByVal dtProveedores As DataTable) As String()
        Dim vProveedores(dtProveedores.Rows.Count - 1) As String

        Dim j As Integer = 0
        For i As Integer = 0 To dtProveedores.Rows.Count - 1
            If Not dtProveedores.Rows(i).Item("NombreInstitucion") Is DBNull.Value Then
                Dim prov As String
                prov = dtProveedores.Rows(i).Item("NombreInstitucion").ToString().Trim()
                If Not RepiteElemento(vProveedores, EspaciosIniciales + prov) Then
                    vProveedores(j) = EspaciosIniciales + prov
                    j = j + 1
                End If
            End If
        Next i

        ReDim Preserve vProveedores(j - 1)

        Return vProveedores
    End Function

    ''' <summary>
    ''' Inidica si un determinado elemento se encuantra en el vector enviado por parámetro.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <param name="texto"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function RepiteElemento(ByVal vector As String(), _
                                    ByVal texto As String) As Boolean
        Dim bRepite As Boolean = False

        For i As Integer = 0 To vector.GetLength(0) - 1
            If vector(i) IsNot Nothing AndAlso vector(i) = texto Then
                bRepite = True
                Exit For
            End If
        Next i

        Return bRepite
    End Function

    ''' <summary>
    ''' Devuelve la lista de bienes inmuebles descritos en el contrato.
    ''' </summary>
    ''' <param name="dtBienes"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function Inmueble(ByVal dtBienes As DataTable) As String()
        'RF1_1 - JRC - 04/09/2012	
        'Motivo :: Se aumentó el Distrito en Bienes

        ' Número de filas
        Dim provinciaNombre As String = "", departamentoNombre As String = "", DistritoNombre As String = ""

        Dim bienes(dtBienes.Rows.Count * 5 - 2) As String
        For i As Integer = 0 To dtBienes.Rows.Count - 1
            Dim bien As New StringBuilder
            If Not dtBienes.Rows(i).Item("CantidadProducto") Is DBNull.Value Then
                bien.Append(dtBienes.Rows(i).Item("CantidadProducto").ToString().Trim())
                bien.Append(" ")
            End If
            If Not dtBienes.Rows(i).Item("Comentario") Is DBNull.Value Then
                bien.Append(dtBienes.Rows(i).Item("Comentario").ToString().Trim())
                bien.Append(" ")
            End If
            If Not dtBienes.Rows(i).Item("Ubicacion") Is DBNull.Value Then
                bien.Append("ubicado en ")
                bien.Append(dtBienes.Rows(i).Item("Ubicacion").ToString().Trim)
                bien.Append(", ")
            Else
                bien.Append("ubicado en , ")
            End If
            If Not dtBienes.Rows(i).Item("DistritoNombre") Is DBNull.Value Then
                DistritoNombre = dtBienes.Rows(i).Item("DistritoNombre").ToString().Trim()
                bien.Append(DistritoNombre)
            End If
            bien.Append(", ")
            If Not dtBienes.Rows(i).Item("ProvinciaNombre") Is DBNull.Value Then
                provinciaNombre = dtBienes.Rows(i).Item("ProvinciaNombre").ToString().Trim()
                bien.Append(provinciaNombre)
            End If
            bien.Append(", ")
            If Not dtBienes.Rows(i).Item("DepartamentoNombre") Is DBNull.Value Then
                departamentoNombre = dtBienes.Rows(i).Item("DepartamentoNombre").ToString().Trim()
                bien.Append(departamentoNombre)
            End If
            bien.Append(", ")
            If Not dtBienes.Rows(i).Item("PartidaRegistral") Is DBNull.Value Then
                bien.Append("inscrito en partida número ")
                bien.Append(dtBienes.Rows(i).Item("PartidaRegistral").ToString().Trim)
                bien.Append(", ")
            Else
                bien.Append("inscrito en partida número , ")
            End If
            If Not dtBienes.Rows(i).Item("OficinaRegistral") Is DBNull.Value Then
                bien.Append("del registro de la propiedad inmueble ")
                bien.Append(dtBienes.Rows(i).Item("OficinaRegistral").ToString().Trim)
                bien.Append(".")
            Else
                bien.Append("del registro de la propiedad inmueble .")
            End If
            bienes(0 + 5 * i) = bien.ToString

            Dim uso As New StringBuilder
            uso.Append("Uso: ")
            If Not dtBienes.Rows(i).Item("Uso") Is DBNull.Value Then
                uso.Append(dtBienes.Rows(i).Item("Uso").ToString().Trim)
            End If
            bienes(2 + 5 * i) = uso.ToString

            Dim ubicacion As String = ""
            If Not dtBienes.Rows(i).Item("Ubicacion") Is DBNull.Value Then
                ubicacion = dtBienes.Rows(i).Item("Ubicacion").ToString().Trim()
            End If
            ' AAE - 10/09/2012 - Cambio órden de ubicacion
            'bienes(3 + 5 * i) = "Ubicación: " + ubicacion + _
            '                                ", Provincia de " + provinciaNombre + _
            '                                ", Departamento de " + departamentoNombre + _
            '                                " y Distrito de " + DistritoNombre
            bienes(3 + 5 * i) = "Ubicación: " + ubicacion + _
                                        " , Distrito de " + DistritoNombre + _
                                        ", Provincia de " + provinciaNombre + _
                                        " y Departamento de " + departamentoNombre

            ' FIN AAE
        Next i

        Return bienes
    End Function

    ''' <summary>
    ''' Devuelve la lista de bienes muebles descritos en el contrato.
    ''' </summary>
    ''' <param name="dtBienes"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function Mueble(ByVal dtBienes As DataTable) As String()
        'RF1_1 - JRC - 04/09/2012	
        'Motivo :: Se aumentó el Distrito en Bienes

        Dim objContratoNTx As New LContratoNTx

        ' Número de filas
        Dim f As Integer
        Dim esMismaUbicacion As Boolean
        Dim totalFilas As Integer
        esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)

        Dim bienes() As String
        ' Se le agrega un espacio adicional como elemento divisor
        If esMismaUbicacion Then
            f = 2
            totalFilas = f * dtBienes.Rows.Count + 2 '  Se adiciona la fila final para el uso y ubicación.
            ReDim bienes(totalFilas + 1)
        Else
            f = 4 + 1
            totalFilas = f * dtBienes.Rows.Count
            ReDim bienes(totalFilas + 1)
        End If

        For i As Integer = 0 To dtBienes.Rows.Count - 1
            Dim bien As New StringBuilder
            If Not dtBienes.Rows(i).Item("CantidadProducto") Is DBNull.Value Then
                bien.Append(dtBienes.Rows(i).Item("CantidadProducto").ToString().Trim())
                bien.Append(" ")
            End If
            If Not dtBienes.Rows(i).Item("Comentario") Is DBNull.Value Then
                bien.Append(dtBienes.Rows(i).Item("Comentario").ToString().Trim())
                bien.Append(" ")
            End If
            If Not dtBienes.Rows(i).Item("Marca") Is DBNull.Value Then
                bien.Append("de la marca ")
                bien.Append(dtBienes.Rows(i).Item("Marca").ToString().Trim)
                bien.Append(", ")
            Else
                bien.Append("de la marca , ")
            End If
            If Not dtBienes.Rows(i).Item("Modelo") Is DBNull.Value Then
                bien.Append("modelo ")
                bien.Append(dtBienes.Rows(i).Item("Modelo").ToString().Trim)
                bien.Append(", ")
            Else
                bien.Append("modelo , ")
            End If
            If Not dtBienes.Rows(i).Item("NroSerie") Is DBNull.Value Then
                bien.Append("con número de serie ")
                bien.Append(dtBienes.Rows(i).Item("NroSerie").ToString().Trim)
                bien.Append(", ")
            Else
                bien.Append("con número de serie , ")
            End If
            If Not dtBienes.Rows(i).Item("NroMotor") Is DBNull.Value Then
                bien.Append("número de motor ")
                bien.Append(dtBienes.Rows(i).Item("NroMotor").ToString().Trim)
                bien.Append(" ")
            Else
                bien.Append("número de motor ")
            End If
            If Not dtBienes.Rows(i).Item("Anio") Is DBNull.Value Then
                bien.Append("y año de fabricación ")
                bien.Append(dtBienes.Rows(i).Item("Anio").ToString().Trim)
            Else
                bien.Append("y año de fabricación ")
            End If
            bienes(0 + f * i) = bien.ToString

            If esMismaUbicacion Then
                ' Si es la última fila.
                If i = dtBienes.Rows.Count - 1 Then
                    Dim uso As New StringBuilder
                    uso.Append("Uso: ")
                    If Not dtBienes.Rows(i).Item("Uso") Is DBNull.Value Then
                        uso.Append(dtBienes.Rows(i).Item("Uso").ToString().Trim)
                    End If
                    bienes(2 + f * i) = uso.ToString

                    Dim ubicacion As String = "", provinciaNombre As String = "", departamentoNombre As String = "", DistritoNombre As String = ""
                    If Not dtBienes.Rows(i).Item("Ubicacion") Is DBNull.Value Then
                        ubicacion = dtBienes.Rows(i).Item("Ubicacion").ToString().Trim()
                    End If
                    If Not dtBienes.Rows(i).Item("ProvinciaNombre") Is DBNull.Value Then
                        provinciaNombre = dtBienes.Rows(i).Item("ProvinciaNombre").ToString().Trim()
                    End If
                    If Not dtBienes.Rows(i).Item("DepartamentoNombre") Is DBNull.Value Then
                        departamentoNombre = dtBienes.Rows(i).Item("DepartamentoNombre").ToString().Trim()
                    End If
                    If Not dtBienes.Rows(i).Item("DistritoNombre") Is DBNull.Value Then
                        DistritoNombre = dtBienes.Rows(i).Item("DistritoNombre").ToString().Trim()
                    End If
                    ' AAE - 10/09/2012 - Cambio órden de ubicacion
                    'bienes(3 + f * i) = "Ubicación: " + ubicacion + _
                    '                        ", Provincia de " + provinciaNombre + _
                    '                        ", Departamento de " + departamentoNombre + _
                    '                        " y Distrito de " + DistritoNombre
                    bienes(3 + f * i) = "Ubicación: " + ubicacion + _
                                            " , Distrito de " + DistritoNombre + _
                                            ", Provincia de " + provinciaNombre + _
                                            " y Departamento de " + departamentoNombre

                    ' FIN AAE
                End If
            Else
                Dim uso As New StringBuilder
                uso.Append("Uso: ")
                If Not dtBienes.Rows(i).Item("Uso") Is DBNull.Value Then
                    uso.Append(dtBienes.Rows(i).Item("Uso").ToString().Trim)
                End If
                bienes(2 + f * i) = uso.ToString

                Dim ubicacion As String = "", provinciaNombre As String = "", departamentoNombre As String = "", DistritoNombre As String = ""
                If Not dtBienes.Rows(i).Item("Ubicacion") Is DBNull.Value Then
                    ubicacion = dtBienes.Rows(i).Item("Ubicacion").ToString().Trim()
                End If
                If Not dtBienes.Rows(i).Item("ProvinciaNombre") Is DBNull.Value Then
                    provinciaNombre = dtBienes.Rows(i).Item("ProvinciaNombre").ToString().Trim()
                End If
                If Not dtBienes.Rows(i).Item("DepartamentoNombre") Is DBNull.Value Then
                    departamentoNombre = dtBienes.Rows(i).Item("DepartamentoNombre").ToString().Trim()
                End If
                If Not dtBienes.Rows(i).Item("DistritoNombre") Is DBNull.Value Then
                    DistritoNombre = dtBienes.Rows(i).Item("DistritoNombre").ToString().Trim()
                End If
                ' AAE - 10/09/2012 - Cambio órden de ubicacion
                'bienes(3 + f * i) = "Ubicación: " + ubicacion + _
                '                            ", Provincia de " + provinciaNombre + _
                '                            ", Departamento de " + departamentoNombre + _
                '                            " y Distrito de " + DistritoNombre
                bienes(3 + f * i) = "Ubicación: " + ubicacion + _
                                            " , Distrito de " + DistritoNombre + _
                                            ", Provincia de " + provinciaNombre + _
                                            " y Departamento de " + departamentoNombre

                ' FIN AAE
            End If
        Next i

        If esMismaUbicacion Then
            ReDim Preserve bienes(totalFilas - 1)
        Else
            ReDim Preserve bienes(totalFilas - 2)
        End If

        Return bienes
    End Function

    ''' <summary>
    ''' Devuelve la lista de vehículos descritos en el contrato.
    ''' </summary>
    ''' <param name="dtBienes"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function Vehicular(ByVal dtBienes As DataTable) As String(,)
        'RF1_1 - JRC - 04/09/2012	
        'Motivo :: Se aumentó el Distrito en Bienes

        Dim objContratoNTx As New LContratoNTx

        ' Número de filas
        Dim f As Integer
        Dim esMismaUbicacion As Boolean
        Dim totalFilas As Integer
        Dim total As Integer

        esMismaUbicacion = objContratoNTx.EsMismaUbicacionYUso(dtBienes)
        ' Se le agrega un espacio adicional como elemento divisor
        If esMismaUbicacion Then
            f = 6 + 1
            totalFilas = f * dtBienes.Rows.Count + 2 ' Se adiciona la fila final para el uso y ubicación.
            total = totalFilas - 1
        Else
            f = 8 + 1
            totalFilas = f * dtBienes.Rows.Count
            total = totalFilas - 2
        End If

        ' La columna cero ira vacía, sera usada para el margen izquierdo.
        Dim bienes(total, 2) As String

        For i As Integer = 0 To dtBienes.Rows.Count - 1
            bienes(0 + f * i, 1) = "a. Clase"
            Dim cantidad, descripcion As String
            If Not dtBienes.Rows(i).Item("CantidadProducto") Is DBNull.Value Then
                cantidad = dtBienes.Rows(i).Item("CantidadProducto").ToString().Trim()
            Else
                cantidad = ""
            End If
            If Not dtBienes.Rows(i).Item("Comentario") Is DBNull.Value Then
                descripcion = dtBienes.Rows(i).Item("Comentario").ToString().Trim()
            Else
                descripcion = ""
            End If
            bienes(0 + f * i, 2) = cantidad + " " + descripcion

            bienes(1 + f * i, 1) = "b. Marca"
            If Not dtBienes.Rows(i).Item("Marca") Is DBNull.Value Then
                bienes(1 + f * i, 2) = dtBienes.Rows(i).Item("Marca").ToString().Trim()
            Else
                bienes(1 + f * i, 2) = ""
            End If
            bienes(2 + f * i, 1) = "c. Modelo"
            If Not dtBienes.Rows(i).Item("Modelo") Is DBNull.Value Then
                bienes(2 + f * i, 2) = dtBienes.Rows(i).Item("Modelo").ToString().Trim()
            Else
                bienes(2 + f * i, 2) = ""
            End If
            bienes(3 + f * i, 1) = "d. Número de serie"
            If Not dtBienes.Rows(i).Item("NroSerie") Is DBNull.Value Then
                bienes(3 + f * i, 2) = dtBienes.Rows(i).Item("NroSerie").ToString().Trim()
            Else
                bienes(3 + f * i, 2) = ""
            End If
            bienes(4 + f * i, 1) = "e. Número de motor"
            If Not dtBienes.Rows(i).Item("NroMotor") Is DBNull.Value Then
                bienes(4 + f * i, 2) = dtBienes.Rows(i).Item("NroMotor").ToString().Trim()
            Else
                bienes(4 + f * i, 2) = ""
            End If
            bienes(5 + f * i, 1) = "f. Año de fabricación"
            If Not dtBienes.Rows(i).Item("Anio") Is DBNull.Value Then
                bienes(5 + f * i, 2) = dtBienes.Rows(i).Item("Anio").ToString().Trim()
            Else
                bienes(5 + f * i, 2) = ""
            End If
            If esMismaUbicacion Then
                ' Si es la última fila.
                If i = dtBienes.Rows.Count - 1 Then
                    bienes(6 + f * i, 1) = ""
                    bienes(6 + f * i, 2) = ""
                    bienes(7 + f * i, 1) = "Uso:"
                    If Not dtBienes.Rows(i).Item("Uso") Is DBNull.Value Then
                        bienes(7 + f * i, 2) = dtBienes.Rows(i).Item("Uso").ToString().Trim()
                    Else
                        bienes(7 + f * i, 2) = ""
                    End If
                    bienes(8 + f * i, 1) = "Ubicación:"

                    Dim ubicacion As String = "", provinciaNombre As String = "", departamentoNombre As String = "", DistritoNombre As String = ""
                    If Not dtBienes.Rows(i).Item("Ubicacion") Is DBNull.Value Then
                        ubicacion = dtBienes.Rows(i).Item("Ubicacion").ToString().Trim()
                    End If
                    If Not dtBienes.Rows(i).Item("ProvinciaNombre") Is DBNull.Value Then
                        provinciaNombre = dtBienes.Rows(i).Item("ProvinciaNombre").ToString().Trim()
                    End If
                    If Not dtBienes.Rows(i).Item("DepartamentoNombre") Is DBNull.Value Then
                        departamentoNombre = dtBienes.Rows(i).Item("DepartamentoNombre").ToString().Trim()
                    End If
                    If Not dtBienes.Rows(i).Item("DistritoNombre") Is DBNull.Value Then
                        DistritoNombre = dtBienes.Rows(i).Item("DistritoNombre").ToString().Trim()
                    End If
                    ' AAE - 10/09/2012 - Cambio órden de ubicacion
                    'bienes(8 + f * i, 2) = ubicacion + _
                    '                        ", Provincia de " + provinciaNombre + _
                    '                        ", Departamento de " + departamentoNombre + _
                    '                        " y Distrito de " + DistritoNombre
                    bienes(8 + f * i, 2) = ubicacion + _
                                            " , Distrito de " + DistritoNombre + _
                                            ", Provincia de " + provinciaNombre + _
                                            " y Departamento de " + departamentoNombre

                    ' FIN AAE
                Else
                    bienes(6 + f * i, 1) = ""
                    bienes(6 + f * i, 2) = ""
                End If
            Else
                bienes(6 + f * i, 1) = "Uso:"
                If Not dtBienes.Rows(i).Item("Uso") Is DBNull.Value Then
                    bienes(6 + f * i, 2) = dtBienes.Rows(i).Item("Uso").ToString().Trim()
                Else
                    bienes(6 + f * i, 2) = ""
                End If
                bienes(7 + f * i, 1) = "Ubicación:"
                Dim ubicacion As String = "", provinciaNombre As String = "", departamentoNombre As String = "", DistritoNombre As String = ""
                If Not dtBienes.Rows(i).Item("Ubicacion") Is DBNull.Value Then
                    ubicacion = dtBienes.Rows(i).Item("Ubicacion").ToString().Trim()
                End If
                If Not dtBienes.Rows(i).Item("ProvinciaNombre") Is DBNull.Value Then
                    provinciaNombre = dtBienes.Rows(i).Item("ProvinciaNombre").ToString().Trim()
                End If
                If Not dtBienes.Rows(i).Item("DepartamentoNombre") Is DBNull.Value Then
                    departamentoNombre = dtBienes.Rows(i).Item("DepartamentoNombre").ToString().Trim()
                End If
                If Not dtBienes.Rows(i).Item("DistritoNombre") Is DBNull.Value Then
                    DistritoNombre = dtBienes.Rows(i).Item("DistritoNombre").ToString().Trim()
                End If
                ' AAE - 10/09/2012 - Cambio órden de ubicacion
                'bienes(7 + f * i, 2) = ubicacion + _
                '                            ", Provincia de " + provinciaNombre + _
                '                            ", Departamento de " + departamentoNombre + _
                '                            " y Distrito de " + DistritoNombre
                bienes(7 + f * i, 2) = ubicacion + _
                                        " , Distrito de " + DistritoNombre + _
                                        ", Provincia de " + provinciaNombre + _
                                        " y Departamento de " + departamentoNombre

                ' FIN AAE
                If i <> dtBienes.Rows.Count - 1 Then
                    bienes(8 + f * i, 1) = ""
                    bienes(8 + f * i, 2) = ""
                End If
            End If
        Next i

        Return bienes
    End Function

    Private Function Cronograma(ByVal tbCronograma As DataTable, _
                                Optional ByVal pCartaCotizacion As Boolean = False, _
                                Optional ByRef pdecMonto As Decimal = 0, _
                                Optional ByRef pdecCuotaInicial As Decimal = 1) As String(,)
        Dim btieneSeguro As Boolean = True
        Dim mCronograma(1, 5) As String
        Dim principal As Double, interes As Double, seguro As Double
        Dim intContador As Integer = 1

        ' Si es contrato
        If pCartaCotizacion = False Then

            btieneSeguro = TieneSeguro(tbCronograma)
            ' Si tiene seguro
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count, 5)

                ' Fila de títulos
                mCronograma(0, 0) = "PERIODO"
                mCronograma(0, 1) = "Nº CUOTA"
                mCronograma(0, 2) = "CAPITAL"
                mCronograma(0, 3) = "INTERES"
                mCronograma(0, 4) = "SEGURO BIEN"
                mCronograma(0, 5) = "CUOTA SIN IGV(*)"
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 Then
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = i.ToString()
                            mCronograma(i + 1, 1) = "CI"
                        Else
                            mCronograma(i + 1, 0) = (i + 1).ToString()
                            mCronograma(i + 1, 1) = (i + 1).ToString()
                        End If
                    Else
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = i.ToString()
                            mCronograma(i + 1, 1) = i.ToString()
                        Else
                            mCronograma(i + 1, 0) = (i + 1).ToString()
                            mCronograma(i + 1, 1) = (i + 1).ToString()
                        End If
                    End If

                    If Not tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = Format(CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN")), "#,##0.00")
                        principal = CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN"))
                    Else
                        mCronograma(i + 1, 2) = "0.00"
                        principal = 0.0
                    End If
                    If tbCronograma.Rows(i).Item("MONTOINTERESBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = "0.00"
                        interes = 0.0
                    Else
                        mCronograma(i + 1, 3) = Format(CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN")), "#,##0.00")
                        interes = CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN"))
                    End If
                    If tbCronograma.Rows(i).Item("MONTOCUOTASEGUROBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 4) = "0.00"
                        seguro = 0.0
                    Else
                        mCronograma(i + 1, 4) = Format(CDbl(tbCronograma.Rows(i).Item("MONTOCUOTASEGUROBIEN")), "#,##0.00")
                        seguro = CDbl(tbCronograma.Rows(i).Item("MONTOCUOTASEGUROBIEN"))
                    End If

                    mCronograma(i + 1, 5) = Format(principal + interes + seguro, "#,##0.00")
                Next i
            Else
                ReDim mCronograma(tbCronograma.Rows.Count, 4)

                ' Fila de títulos
                mCronograma(0, 0) = "PERIODO"
                mCronograma(0, 1) = "Nº CUOTA"
                mCronograma(0, 2) = "CAPITAL"
                mCronograma(0, 3) = "INTERES"
                mCronograma(0, 4) = "CUOTA SIN IGV(*)"
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 Then
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = i.ToString()
                            mCronograma(i + 1, 1) = "CI"
                        Else
                            mCronograma(i + 1, 0) = (i + 1).ToString()
                            mCronograma(i + 1, 1) = (i + 1).ToString()
                        End If
                    Else
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = i.ToString()
                            mCronograma(i + 1, 1) = i.ToString()
                        Else
                            mCronograma(i + 1, 0) = (i + 1).ToString()
                            mCronograma(i + 1, 1) = (i + 1).ToString()
                        End If
                    End If

                    'If i = 1 And pdecCuotaInicial > 0 Then
                    '    mCronograma(i + 1, 0) = "CI"
                    'Else
                    '    mCronograma(i + 1, 0) = i.ToString()
                    'End If
                    'mCronograma(i + 1, 1) = (i + 1).ToString()
                    If Not tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = Format(CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN")), "#,##0.00")
                        principal = CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN"))
                    Else
                        mCronograma(i + 1, 2) = "0.00"
                        principal = 0.0
                    End If
                    If tbCronograma.Rows(i).Item("MONTOINTERESBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = "0.00"
                        interes = 0.0
                    Else
                        mCronograma(i + 1, 3) = Format(CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN")), "#,##0.00")
                        interes = CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN"))
                    End If

                    mCronograma(i + 1, 4) = Format(principal + interes, "#,##0.00")
                Next i
            End If
        Else

            intContador = 1

            btieneSeguro = TieneSeguroCotizacion(tbCronograma)
            Dim decSaldoCapital As Decimal = 0
            Dim decAmortizacion As Decimal = 0
            Dim decInteres As Decimal = 0
            Dim decSeguro As Decimal = 0
            Dim decMontoCuota As Decimal = 0

            If btieneSeguro Then

                ReDim mCronograma(tbCronograma.Rows.Count, 5)

                mCronograma(0, 0) = "Cuota"
                mCronograma(0, 1) = "Saldo Capital"
                mCronograma(0, 2) = "Amortización"
                mCronograma(0, 3) = "Interés"
                mCronograma(0, 4) = "Seguro"
                mCronograma(0, 5) = "Cuota sin IGV(*)"
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 And pdecCuotaInicial > 0 Then
                        mCronograma(i + 1, 0) = "CI"
                    Else
                        mCronograma(i + 1, 0) = intContador.ToString()
                        intContador = intContador + 1
                    End If
                    'cronograma(i + 1, 1) = (i + 1).ToString()
                    If tbCronograma.Rows(i).Item("Im_saldo") Is DBNull.Value Then
                        mCronograma(i + 1, 1) = "0.00"
                    Else
                        decSaldoCapital = CDec(tbCronograma.Rows(i).Item("Im_saldo").ToString())
                        mCronograma(i + 1, 1) = decSaldoCapital.ToString("###,###,##0.00")
                    End If
                    If tbCronograma.Rows(i).Item("Im_principal") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = "0.00"
                    Else
                        decAmortizacion = CDec(tbCronograma.Rows(i).Item("Im_principal").ToString())
                        mCronograma(i + 1, 2) = decAmortizacion.ToString("###,###,##0.00")
                    End If
                    If tbCronograma.Rows(i).Item("Im_interes") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = "0.00"
                    Else
                        decInteres = CDec(tbCronograma.Rows(i).Item("Im_interes").ToString())
                        mCronograma(i + 1, 3) = decInteres.ToString("###,###,##0.00")
                    End If
                    If tbCronograma.Rows(i).Item("Im_cuota_seguro") Is DBNull.Value Then
                        mCronograma(i + 1, 4) = "0.00"
                    Else
                        decSeguro = CDec(tbCronograma.Rows(i).Item("Im_cuota_seguro").ToString())
                        mCronograma(i + 1, 4) = decSeguro.ToString("###,###,##0.00")
                    End If

                    decMontoCuota = decAmortizacion + decInteres + decSeguro
                    mCronograma(i + 1, 5) = decMontoCuota.ToString("###,###,##0.00")

                    If i = tbCronograma.Rows.Count - 3 Then
                        pdecMonto = decMontoCuota
                    End If
                Next i


            Else


                ReDim mCronograma(tbCronograma.Rows.Count, 4)

                mCronograma(0, 0) = "Cuota"
                mCronograma(0, 1) = "Saldo Capital"
                mCronograma(0, 2) = "Amortización"
                mCronograma(0, 3) = "Interés"
                mCronograma(0, 4) = "Cuota sin IGV(*)"
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 And pdecCuotaInicial > 0 Then
                        mCronograma(i + 1, 0) = "CI"
                    Else
                        mCronograma(i + 1, 0) = intContador.ToString()
                        intContador = intContador + 1
                    End If
                    'cronograma(i + 1, 1) = (i + 1).ToString()
                    If tbCronograma.Rows(i).Item("Im_saldo") Is DBNull.Value Then
                        mCronograma(i + 1, 1) = "0.00"
                    Else
                        decSaldoCapital = CDec(tbCronograma.Rows(i).Item("Im_saldo").ToString())
                        mCronograma(i + 1, 1) = decSaldoCapital.ToString("###,###,##0.00")
                    End If
                    If tbCronograma.Rows(i).Item("Im_principal") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = "0.00"
                    Else
                        decAmortizacion = CDec(tbCronograma.Rows(i).Item("Im_principal").ToString())
                        mCronograma(i + 1, 2) = decAmortizacion.ToString("###,###,##0.00")
                    End If
                    If tbCronograma.Rows(i).Item("Im_interes") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = "0.00"
                    Else
                        decInteres = CDec(tbCronograma.Rows(i).Item("Im_interes").ToString())
                        mCronograma(i + 1, 3) = decInteres.ToString("###,###,##0.00")
                    End If

                    decMontoCuota = decAmortizacion + decInteres + decSeguro
                    mCronograma(i + 1, 4) = decMontoCuota.ToString("###,###,##0.00")

                    If i = tbCronograma.Rows.Count - 3 Then
                        pdecMonto = decMontoCuota
                    End If
                Next i


            End If





        End If

        Return mCronograma
    End Function

    ''' <summary>
    ''' Crea una matriz de objetos Celda, conteniendo la configuración del cronograma, incluyendo el dato y el formato del mismo.
    ''' el formato incluye: alineado (izquierda, derecha, centrado), negrita (true, false)
    ''' </summary>
    ''' <param name="tbCronograma"></param>
    ''' <param name="pCartaCotizacion"></param>
    ''' <param name="pdecMonto"></param>
    ''' <param name="pdecCuotaInicial"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function CronogramaRtf(ByVal tbCronograma As DataTable, _
                                Optional ByVal pCartaCotizacion As Boolean = False, _
                                Optional ByRef pdecMonto As Decimal = 0, _
                                Optional ByRef pdecCuotaInicial As Decimal = 1) As Celda(,)
        Dim btieneSeguro As Boolean
        Dim mCronograma(1, 5) As Celda
        Dim principal As Double, interes As Double, seguro As Double
        Dim intContador As Integer = 1

        ' Si es contrato ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If pCartaCotizacion = False Then

            btieneSeguro = TieneSeguro(tbCronograma)
            ' Si tiene seguro
            If btieneSeguro Then
                ReDim mCronograma(tbCronograma.Rows.Count, 5)

                mCronograma(0, 0) = Celda.Asignar("PERIODO", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 1) = Celda.Asignar("Nº CUOTA", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 2) = Celda.Asignar("CAPITAL", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 3) = Celda.Asignar("INTERES", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 4) = Celda.Asignar("SEGURO BIEN", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 5) = Celda.Asignar("CUOTA SIN IGV(*)", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 Then
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = Celda.Asignar(i.ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar("CI")
                        Else
                            mCronograma(i + 1, 0) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        End If
                    Else
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = Celda.Asignar(i.ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar(i.ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        Else
                            mCronograma(i + 1, 0) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        End If
                    End If
                    If Not tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = Celda.Asignar(Format(CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN")), "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        principal = CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN"))
                    Else
                        mCronograma(i + 1, 2) = Celda.Asignar("0.00", False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        principal = 0.0
                    End If
                    If tbCronograma.Rows(i).Item("MONTOINTERESBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = Celda.Asignar("0.00", False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        interes = 0.0
                    Else
                        mCronograma(i + 1, 3) = Celda.Asignar(Format(CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN")), "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        interes = CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN"))
                    End If
                    If tbCronograma.Rows(i).Item("MONTOCUOTASEGUROBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 4) = Celda.Asignar("0.00")
                        seguro = 0.0
                    Else
                        mCronograma(i + 1, 4) = Celda.Asignar(Format(CDbl(tbCronograma.Rows(i).Item("MONTOCUOTASEGUROBIEN")), "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        seguro = CDbl(tbCronograma.Rows(i).Item("MONTOCUOTASEGUROBIEN"))
                    End If

                    mCronograma(i + 1, 5) = Celda.Asignar(Format(principal + interes + seguro, "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                Next i
            Else
                ReDim mCronograma(tbCronograma.Rows.Count, 4)

                mCronograma(0, 0) = Celda.Asignar("PERIODO", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 1) = Celda.Asignar("Nº CUOTA", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 2) = Celda.Asignar("CAPITAL", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 3) = Celda.Asignar("INTERES", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 4) = Celda.Asignar("CUOTA SIN IGV(*)", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)

                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 Then
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = Celda.Asignar(i.ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar("CI", False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        Else
                            mCronograma(i + 1, 0) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        End If
                    Else
                        If pdecCuotaInicial > 0 Then
                            mCronograma(i + 1, 0) = Celda.Asignar(i.ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar(i.ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        Else
                            mCronograma(i + 1, 0) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                            mCronograma(i + 1, 1) = Celda.Asignar((i + 1).ToString(), False, ParagraphAlignment.AlignParagraphLeft, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        End If
                    End If

                    If Not tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = Celda.Asignar(Format(CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN")), "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        principal = CDbl(tbCronograma.Rows(i).Item("MONTOPRINCIPALBIEN"))
                    Else
                        mCronograma(i + 1, 2) = Celda.Asignar("0.00", False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        principal = 0.0
                    End If

                    If tbCronograma.Rows(i).Item("MONTOINTERESBIEN") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = Celda.Asignar("0.00", False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        interes = 0.0
                    Else
                        mCronograma(i + 1, 3) = Celda.Asignar(Format(CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN")), "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                        interes = CDbl(tbCronograma.Rows(i).Item("MONTOINTERESBIEN"))
                    End If

                    mCronograma(i + 1, 4) = Celda.Asignar(Format(principal + interes, "#,##0.00"), False, ParagraphAlignment.AlignParagraphRight, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                Next i
            End If
        Else ' Sino es cotización '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            btieneSeguro = TieneSeguroCotizacion(tbCronograma)
            Dim decSaldoCapital As Decimal
            Dim decAmortizacion As Decimal = 0
            Dim decInteres As Decimal = 0
            Dim decSeguro As Decimal = 0
            Dim decMontoCuota As Decimal

            If btieneSeguro Then

                ReDim mCronograma(tbCronograma.Rows.Count, 5)

                mCronograma(0, 0) = Celda.Asignar("Cuota")
                mCronograma(0, 1) = Celda.Asignar("Saldo Capital")
                mCronograma(0, 2) = Celda.Asignar("Amortización")
                mCronograma(0, 3) = Celda.Asignar("Interés")
                mCronograma(0, 4) = Celda.Asignar("Seguro")
                mCronograma(0, 5) = Celda.Asignar("Cuota sin IGV(*)")
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 Then
                        mCronograma(i + 1, 0) = Celda.Asignar("CI")
                    Else
                        mCronograma(i + 1, 0) = Celda.Asignar(i.ToString())
                    End If
                    If tbCronograma.Rows(i).Item("Im_saldo") Is DBNull.Value Then
                        mCronograma(i + 1, 1) = Celda.Asignar("0.00")
                    Else
                        decSaldoCapital = CDec(tbCronograma.Rows(i).Item("Im_saldo").ToString())
                        mCronograma(i + 1, 1) = Celda.Asignar(decSaldoCapital.ToString("###,###,##0.00"))
                    End If
                    If tbCronograma.Rows(i).Item("Im_principal") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = Celda.Asignar("0.00")
                    Else
                        decAmortizacion = CDec(tbCronograma.Rows(i).Item("Im_principal").ToString())
                        mCronograma(i + 1, 2) = Celda.Asignar(decAmortizacion.ToString("###,###,##0.00"))
                    End If
                    If tbCronograma.Rows(i).Item("Im_interes") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = Celda.Asignar("0.00")
                    Else
                        decInteres = CDec(tbCronograma.Rows(i).Item("Im_interes").ToString())
                        mCronograma(i + 1, 3) = Celda.Asignar(decInteres.ToString("###,###,##0.00"))
                    End If
                    If tbCronograma.Rows(i).Item("Im_cuota_seguro") Is DBNull.Value Then
                        mCronograma(i + 1, 4) = Celda.Asignar("0.00")
                    Else
                        decSeguro = CDec(tbCronograma.Rows(i).Item("Im_cuota_seguro").ToString())
                        mCronograma(i + 1, 4) = Celda.Asignar(decSeguro.ToString("###,###,##0.00"))
                    End If

                    decMontoCuota = decAmortizacion + decInteres + decSeguro
                    mCronograma(i + 1, 5) = Celda.Asignar(decMontoCuota.ToString("###,###,##0.00"))

                    If i = tbCronograma.Rows.Count - 3 Then
                        pdecMonto = decMontoCuota
                    End If
                Next i
            Else
                ReDim mCronograma(tbCronograma.Rows.Count, 4)

                mCronograma(0, 0) = Celda.Asignar("Cuota", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 1) = Celda.Asignar("Saldo Capital", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 2) = Celda.Asignar("Amortización", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 3) = Celda.Asignar("Interés", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                mCronograma(0, 4) = Celda.Asignar("Cuota sin IGV(*)", True, ParagraphAlignment.AlignParagraphCenter, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle, LineStyle.LineStyleSingle)
                For i As Integer = 0 To tbCronograma.Rows.Count - 1
                    If i = 0 Then
                        mCronograma(i + 1, 0) = Celda.Asignar("CI")
                    Else
                        mCronograma(i + 1, 0) = Celda.Asignar(i.ToString())
                    End If
                    If tbCronograma.Rows(i).Item("Im_saldo") Is DBNull.Value Then
                        mCronograma(i + 1, 1) = Celda.Asignar("0.00")
                    Else
                        decSaldoCapital = CDec(tbCronograma.Rows(i).Item("Im_saldo").ToString())
                        mCronograma(i + 1, 1) = Celda.Asignar(decSaldoCapital.ToString("###,###,##0.00"))
                    End If
                    If tbCronograma.Rows(i).Item("Im_principal") Is DBNull.Value Then
                        mCronograma(i + 1, 2) = Celda.Asignar("0.00")
                    Else
                        decAmortizacion = CDec(tbCronograma.Rows(i).Item("Im_principal").ToString())
                        mCronograma(i + 1, 2) = Celda.Asignar(decAmortizacion.ToString("###,###,##0.00"))
                    End If
                    If tbCronograma.Rows(i).Item("Im_interes") Is DBNull.Value Then
                        mCronograma(i + 1, 3) = Celda.Asignar("0.00")
                    Else
                        decInteres = CDec(tbCronograma.Rows(i).Item("Im_interes").ToString())
                        mCronograma(i + 1, 3) = Celda.Asignar(decInteres.ToString("###,###,##0.00"))
                    End If

                    decMontoCuota = decAmortizacion + decInteres + decSeguro
                    mCronograma(i + 1, 4) = Celda.Asignar(decMontoCuota.ToString("###,###,##0.00"))

                    If i = tbCronograma.Rows.Count - 3 Then
                        pdecMonto = decMontoCuota
                    End If
                Next i

            End If

        End If

        Return mCronograma
    End Function

    ''' <summary>
    ''' Establece la ruta en el sistema de archivos, excluyendo el directorio raíz.
    ''' Si el directorio no existe, lo crea.
    ''' </summary>
    ''' <param name="FileName">Nombre del archivo a crearse</param>
    ''' <param name="Directorio">Ruta (carpeta) donde se va a crear el archivo</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function RutaParcial(ByVal FileName As String, _
                                 ByVal Directorio As String) As String
        Try

            Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")

            Dim mNombAccion As String = Directorio
            Dim mNomCarpAnno As String = Now.Year.ToString & "\"
            Dim mNomCarpMes As String = MonthName(Now.Month).ToUpper & "\"

            Dim nombreAr As String = Today.ToShortDateString.Replace("/", "-") & "_" & Now.TimeOfDay.ToString.Replace(":", ".") & "_" & FileName

            Dim mRutaTotal As String = rutaVirtual & mNombAccion & mNomCarpAnno & mNomCarpMes
            If Not Directory.Exists(mRutaTotal) Then
                Directory.CreateDirectory(mRutaTotal)
            End If

            Return mNombAccion & mNomCarpAnno & mNomCarpMes & Replace(nombreAr, Chr(39), Chr(96))
        Catch ex As Exception
            Throw New Exception("Los valores configurados para cargar archivos no permiten cargar el contenido de este archivo.")
        End Try
    End Function

    ''' <summary>
    ''' Indica si el cronograma tbCronograma tiene una cuota de seguro
    ''' </summary>
    ''' <param name="tbCronograma">Un datatable conteniendo el cronograma</param>
    ''' <returns>True: Cuenta con cuota, False: Otro caso</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Function TieneSeguro(ByVal tbCronograma As DataTable) As Boolean
        Dim bTieneSeguro As Boolean = False
        Dim value As Double

        Try
            For Each oRow As DataRow In tbCronograma.Rows
                If Not oRow.Item("MONTOCUOTASEGUROBIEN") Is DBNull.Value Then
                    If Double.TryParse(oRow.Item("MONTOCUOTASEGUROBIEN").ToString(), value) Then
                        If CDbl(oRow.Item("MONTOCUOTASEGUROBIEN")) > 0 Then
                            bTieneSeguro = True
                        End If
                    End If
                End If
            Next oRow

            Return bTieneSeguro
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function TieneSeguroCotizacion(ByVal tbCronograma As DataTable) As Boolean
        Dim bTieneSeguro As Boolean = False
        Dim value As Double

        Try
            For Each oRow As DataRow In tbCronograma.Rows
                If Not oRow.Item("Im_cuota_seguro") Is DBNull.Value Then
                    If Double.TryParse(oRow.Item("Im_cuota_seguro").ToString(), value) Then
                        If CDbl(oRow.Item("Im_cuota_seguro")) > 0 Then
                            bTieneSeguro = True
                        End If
                    End If
                End If
            Next oRow

            Return bTieneSeguro
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

End Class
