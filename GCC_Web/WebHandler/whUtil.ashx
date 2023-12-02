<%@ WebHandler Language="VB" Class="whUtil" %>

Imports System
Imports System.Web
Imports GCC.LogicWS
Imports GCC.Entity
Imports GCC.UI
Imports System.Data

Public Class whUtil : Implements IHttpHandler
    
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        'context.Response.ContentType = "text/plain"
        'context.Response.Write("Hello World")
        'Recoge Parametros del AJAX
        
        Dim strResultado As String = ""
        Dim strOperacion As String = Trim(context.Request.Form("pstrOp"))
        Dim intOperacion As Integer = GCCUtilitario.CheckInt(strOperacion)
        
        Select Case intOperacion
            
            Case GCCConstante.eTipoUtilAshx.Departamento
                strResultado = CargarDepartamento()
                
            Case GCCConstante.eTipoUtilAshx.ValorGenerica
                'Carga Valor Generica
                Dim pstrTablaGenerica As String = context.Request.Form("pstrTablaGenerica")
                strResultado = CargaComboValorGenerico(pstrTablaGenerica)
                
            Case GCCConstante.eTipoUtilAshx.valorProvincia
                'Carga Provincia
                Dim pstrProvincia As String = context.Request.Form("pstrDepartamento")
                strResultado = CargaComboProvincia(pstrProvincia)
                
            Case GCCConstante.eTipoUtilAshx.ValorDistrito
                'Carga Distrito
                Dim pstrDepartamento As String = context.Request.Form("pstrDepartamento")
                Dim pstrProvincia As String = context.Request.Form("pstrProvincia")
                strResultado = CargaComboDistrito(pstrDepartamento, pstrProvincia)
            
            Case GCCConstante.eTipoUtilAshx.ValorGenericaAnidada
                'Carga Valor Generica Anidada
                Dim pstrTablaGenerica As String = context.Request.Form("pstrTablaGenerica")
                Dim pstrCodigoGenerico As String = context.Request.Form("pstrCodigoGenerico")
                strResultado = CargaComboValorGenericoAnidado(pstrTablaGenerica, pstrCodigoGenerico)
                
            Case GCCConstante.eTipoUtilAshx.ValorGenericaEspecial
                'Carga Valor Generica Anidada
                Dim pstrTablaGenerica As String = context.Request.Form("pstrTablaGenerica")
                strResultado = CargaComboValorGenericoEspecial(pstrTablaGenerica)
                                      
            Case GCCConstante.eTipoUtilAshx.HtmlEncode
                'AgregaDias
                Dim pstrFecha As String = context.Request.Form("pstrFecha")
                Dim pstrDias As String = context.Request.Form("pstrDias")
                strResultado = AumentaDiasFecha(pstrFecha, pstrDias)                
                
            Case GCCConstante.eTipoUtilAshx.DescripcionValorGenerico
                Dim pstrDominio As String = context.Request.Form("pstrDominio")
                Dim pstrParametro As String = context.Request.Form("pstrParametro")
                strResultado = DescripcionParametro(pstrDominio, pstrParametro)
            
                'IBK - RPH se agrego un parametros mas para filtrar por provincias
            Case GCCConstante.eTipoUtilAshx.Notarias
                Dim pstrDepartamento As String = context.Request.Form("pstrDepartamento")
                Dim pstrProvincia As String = context.Request.Form("pstrProvincia")
                strResultado = CargaComboNotaria(pstrDepartamento, pstrProvincia)
            
            Case GCCConstante.eTipoUtilAshx.ComisionActivacion
                Dim pstrCodMoneda As String = context.Request.Form("pstrCodMoneda")
                strResultado = CargaComisionActivacion(pstrCodMoneda)
                
            Case GCCConstante.eTipoUtilAshx.ValorPais
                strResultado = CargaComboPais()
                'RPH
            Case GCCConstante.eTipoUtilAshx.ContactoNotaria
                Dim pstrCodNotaria As String = context.Request.Form("pstrCodNotaria")
                strResultado = ObtenerContactoNotaria(pstrCodNotaria)
                'JJM IBK 
            Case GCCConstante.eTipoUtilAshx.Municipalidad
                strResultado = CargarMuncipalidad()
        End Select
        context.Response.Write(strResultado)

        'Ejecuta metodo segun Tipo
        'context.Response.Write(strResultado)
        'Distrito
    End Sub
    
    ''' <summary>
    ''' Lista los Muncipalidad.
    ''' </summary>
    ''' <example></example>    
    '''<remarks>
    ''' Creado Por         : IBK JJM
    ''' Fecha de Creación  : 24/01/2013
    ''' </remarks>
    ''' </remarks>
    Private Function CargarMuncipalidad() As String
        Dim oLwsUtil As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsUtil.ListarMunicipalidad())
   
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "- Seleccione -"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("NOMBRE") Is DBNull.Value Then
                    text = Trim(oRow.Item("NOMBRE").ToString())
                End If

                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    'IBK - RPH
    Public Function ObtenerContactoNotaria(ByVal pstrCodNotaria As String) As String

        Dim objLUtilNTx As New LUtilNTX
        Dim sbResultado As String
        Dim value As String = ""
        Dim text As String = ""

        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTx.ObtenerContatoNotarias(pstrCodNotaria))

            If dtDatos.Rows.Count > 0 Then
                If Not dtDatos.Rows(0)("NOMBRECONTACTO") Is DBNull.Value Then
                    value = Trim(dtDatos.Rows(0)("NOMBRECONTACTO").ToString())
                End If
                If Not dtDatos.Rows(0)("CORREOCONTACTO") Is DBNull.Value Then
                    text = Trim(dtDatos.Rows(0)("CORREOCONTACTO").ToString())
                End If
            End If

        Catch ex As Exception
        Finally
        End Try
        sbResultado = value & "*" & text
        Return sbResultado
    End Function
        
    
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pstrTablaGenerica">Nombre de la tabla genérica</param>
    ''' <example>"TBL192"</example>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargaComboValorGenerico(ByVal pstrTablaGenerica As String) As String
        Dim objLMantenimientolNTx As New LMantenimientoNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLMantenimientolNTx.ListarTablaGenerica(pstrTablaGenerica))
   
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                    text = Trim(oRow.Item("DESCRIPCION").ToString())
                End If

                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    ''' <summary>
    ''' Lista los departamentos.
    ''' </summary>
    ''' <example></example>    
    '''<remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    ''' </remarks>
    Private Function CargarDepartamento() As String
        Dim oLwsUtil As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsUtil.ListarDepartamento())
   
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("NOMBRE") Is DBNull.Value Then
                    text = Trim(oRow.Item("NOMBRE").ToString())
                End If

                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pstrTablaGenerica">Nombre de la tabla genérica anidado</param>
    ''' <example>"TBL192"</example>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargaComboValorGenericoAnidado(ByVal pstrTablaGenerica As String, ByVal pstrCodigoGenerico As String) As String
        Dim objLMantenimientolNTx As New LMantenimientoNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLMantenimientolNTx.ListarTablaGenericaAnidada(pstrTablaGenerica, pstrCodigoGenerico))
   
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                    text = Trim(oRow.Item("DESCRIPCION").ToString())
                End If

                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    

    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pstrTablaGenerica">Nombre de la tabla genérica anidado</param>
    ''' <example>"TBL192"</example>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargaComboValorGenericoEspecial(ByVal pstrTablaGenerica As String) As String
        Dim objLMantenimientolNTx As New LMantenimientoNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLMantenimientolNTx.ListarTablaGenerica(pstrTablaGenerica))
   
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString()) + "*" + Trim(oRow.Item("VALOR2").ToString())
                End If
                If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                    text = Trim(oRow.Item("DESCRIPCION").ToString())
                End If

                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    
    Private Function CargaComboProvincia(ByVal pstrDepartamento As String) As String
        Dim objLUtilNTx As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTx.ListarProvincia(pstrDepartamento))
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                    text = Trim(oRow.Item("DESCRIPCION").ToString())
                End If
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    Private Function CargaComboPais() As String 'AEP-10/07/2012
        Dim objLUtilNTx As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTx.ListarPais())
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("codpais") Is DBNull.Value Then
                    value = Trim(oRow.Item("codpais").ToString())
                End If
                If Not oRow.Item("descrippais") Is DBNull.Value Then
                    text = Trim(oRow.Item("descrippais").ToString())
                End If
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally

        End Try
        
        Return sbResultado.ToString()
    End Function
        
    Private Function CargaComboDistrito(ByVal pstrDepartamento As String, ByVal pstrProvincia As String) As String
        Dim objLUtilNTx As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTx.ListarDistrito(pstrDepartamento, pstrProvincia))
            
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                    text = Trim(oRow.Item("DESCRIPCION").ToString())
                End If
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    'IBK - RPH
    Private Function CargaComboNotaria(ByVal pstrDepartamento As String, _
                                       ByVal pstrProvincia As String) As String
    
    
        
        Dim objLUtilNTx As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTx.ListarNotarias(pstrDepartamento, pstrProvincia))
            
            sbResultado.Append("0|")
            sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
            For Each oRow As DataRow In dtDatos.Rows
                Dim value As String = ""
                Dim text As String = ""
                If Not oRow.Item("CODIGO") Is DBNull.Value Then
                    value = Trim(oRow.Item("CODIGO").ToString())
                End If
                If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                    text = Trim(oRow.Item("DESCRIPCION").ToString())
                End If
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion(value, text))
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pstrFecha">Fecha</param>
    ''' <example>"TBL192"</example>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AumentaDiasFecha(ByVal pstrFecha As String, ByVal pstrDias As String) As String
        Dim objLMantenimientolNTx As New LMantenimientoNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim arrFecha() As String = Split(pstrFecha, "/")
            Dim dtFecha As Date = New Date(arrFecha(2), arrFecha(1), arrFecha(0))
                        
            dtFecha = dtFecha.AddDays(CInt(pstrDias))
            
            sbResultado.Append("0|")
            sbResultado.Append(dtFecha.ToString("dd/MM/yyyy"))
            
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
        
    ''' <summary>
    ''' Obtiene las descripciones solicitadas de la tabla valor generica
    ''' </summary>
    ''' <param name="pstrDominio">Valor de la Tabla a filtrar</param>
    ''' <param name="pstrParametro">Valor Especifico de la solicitud</param>
    ''' <returns>la descripcion de dichos filtros</returns>
    ''' <remarks>TSF - KCC : 04-05-2012</remarks>
    Private Function DescripcionParametro(ByVal pstrDominio As String, ByVal pstrParametro As String) As String
        Dim objLUtilNTx As New LUtilNTX
        Dim sbResultado As New StringBuilder
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTx.ObtenerValorGenerico(pstrDominio, pstrParametro))
            
            sbResultado.Append("0|")
            For Each oRow As DataRow In dtDatos.Rows
                sbResultado.Append(oRow("DESCRIPCION").ToString & "|")
                sbResultado.Append(oRow("VALOR3").ToString & "|")
                sbResultado.Append(oRow("VALOR4").ToString & "|")
                sbResultado.Append(oRow("VALOR2").ToString)
                Exit For
            Next oRow
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            objLUtilNTx = Nothing
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    
    ''' <summary>
    ''' CargaComisionActivacion
    ''' </summary>
    ''' <param name="pstrCodMoneda">Codigo de Moneda</param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargaComisionActivacion(ByVal pstrCodMoneda As String) As String
        Dim objLUtilNTX As New LUtilNTX
        Dim sbResultado As New StringBuilder
        
        Try
            Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTX.ConsultaConceptosTarifas(0, GCCConstante.C_COD_TIPOCONCEPTO_COMISION, GCCConstante.C_COD_TIPOTARIFA_COMISION_ACTIVACION, pstrCodMoneda))
            Dim decImporte As Decimal = 0
            
            If Not dtDatos Is Nothing Then
                If dtDatos.Rows.Count > 0 Then
                    decImporte = GCCUtilitario.CheckDecimal(dtDatos.Rows(0).Item("Minimo").ToString)
                End If
            End If
            
            sbResultado.Append("0|")
            sbResultado.Append(decImporte.ToString)
            
        Catch ex As Exception
            sbResultado = New StringBuilder
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        Finally
            
        End Try
        
        Return sbResultado.ToString()
    End Function
    
    
End Class