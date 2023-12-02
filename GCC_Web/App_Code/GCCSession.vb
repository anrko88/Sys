Imports System.Data
Imports GCC.Entity

Namespace GCC.UI

    Public Class GCCSession

        ''' <summary>
        ''' CodigoUsuario
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property CodigoUsuario() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("CodigoUsuario") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("CodigoUsuario"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("CodigoUsuario")
                Else
                    HttpContext.Current.Session("CodigoUsuario") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' DominioUsuario
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property DominioUsuario() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("DominioUsuario") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("DominioUsuario"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("DominioUsuario")
                Else
                    HttpContext.Current.Session("DominioUsuario") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' URLAplicacion
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property URLAplicacion() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("URLAplicacion") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("URLAplicacion"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("URLAplicacion")
                Else
                    HttpContext.Current.Session("URLAplicacion") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' SD
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property SD() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("sd") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("sd"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("sd")
                Else
                    HttpContext.Current.Session("sd") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Ambiente
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property Ambiente() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("Ambiente") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("Ambiente"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("Ambiente")
                Else
                    HttpContext.Current.Session("Ambiente") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' AmbienteDesarrollo
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property AmbienteDesarrollo() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("AmbienteDesarrollo") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("AmbienteDesarrollo"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("AmbienteDesarrollo")
                Else
                    HttpContext.Current.Session("AmbienteDesarrollo") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' NombreUsuario
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property NombreUsuario() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("NombreUsuario") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("NombreUsuario"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("NombreUsuario")
                Else
                    HttpContext.Current.Session("NombreUsuario") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' PerfilUsuario
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property PerfilUsuario() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("PerfilUsuario") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("PerfilUsuario"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("PerfilUsuario")
                Else
                    HttpContext.Current.Session("PerfilUsuario") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' AccesoUsuario
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property AccesoUsuario() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("AccesoUsuario") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("AccesoUsuario"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("AccesoUsuario")
                Else
                    HttpContext.Current.Session("AccesoUsuario") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' ServidorBD
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property ServidorBD() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("dbServer") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("dbServer"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("dbServer")
                Else
                    HttpContext.Current.Session("dbServer") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' NombreBD
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property NombreBD() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("dbName") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("dbName"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("dbName")
                Else
                    HttpContext.Current.Session("dbName") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Pin
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property Pin() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("PIN") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("PIN"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("PIN")
                Else
                    HttpContext.Current.Session("PIN") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' DescripcionPerfil
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property DescripcionPerfil() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("DescripcionPerfil") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("DescripcionPerfil"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("DescripcionPerfil")
                Else
                    HttpContext.Current.Session("DescripcionPerfil") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' CodigoTienda
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property CodigoTienda() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("CodigoTienda") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("CodigoTienda"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("CodigoTienda")
                Else
                    HttpContext.Current.Session("CodigoTienda") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' NombreTienda
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Property NombreTienda() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("NombreTienda") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("NombreTienda"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("NombreTienda")
                Else
                    HttpContext.Current.Session("NombreTienda") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' FlagBloqueo
        ''' </summary>    
        ''' <remarks>Para la pagina de instruccion</remarks>
        Public Shared Property FlagBloqueo() As Boolean
            Get
                Dim salida As Boolean = False
                If Not HttpContext.Current.Session("FlagBloqueo") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("FlagBloqueo"), Boolean)
                End If
                Return salida
            End Get
            Set(ByVal Value As Boolean)
                If Not Value Then
                    HttpContext.Current.Session.Remove("FlagBloqueo")
                Else
                    HttpContext.Current.Session("FlagBloqueo") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' ArchivoLog
        ''' </summary>    
        ''' <remarks>Ruta del archivo Log</remarks>
        Public Shared Property ArchivoLog() As String
            Get
                Dim salida As String = ""
                If Not HttpContext.Current.Session("GCCArchivoLog") Is Nothing Then
                    salida = CType(HttpContext.Current.Session("GCCArchivoLog"), String)
                End If
                Return salida
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    HttpContext.Current.Session.Remove("GCCArchivoLog")
                Else
                    HttpContext.Current.Session("GCCArchivoLog") = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' LimpiarTotalSesiones
        ''' </summary>    
        ''' <remarks></remarks>
        Public Shared Sub LimpiarTotalSesiones()
            CodigoUsuario = Nothing
            DominioUsuario = Nothing
            URLAplicacion = Nothing
            SD = Nothing
            Ambiente = Nothing
            AmbienteDesarrollo = Nothing
            NombreUsuario = Nothing
            PerfilUsuario = Nothing
            AccesoUsuario = Nothing
            ServidorBD = Nothing
            NombreBD = Nothing
            Pin = Nothing
            FlagBloqueo = False
            CodigoTienda = Nothing
            NombreTienda = Nothing
        End Sub

    End Class

End Namespace
