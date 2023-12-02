Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data.Odbc.OdbcConnection
Imports System
Imports System.IO
Imports System.Collections
Partial Class GestionBien_ImpuestoVehicular_frmImpuestoVehicularCargarMasivo
    Inherits GCCBase
    Dim objLog As New GCCLog("frmImpuestoVehicularCargarMasivo.aspx.vb")

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        objLog.escribe("DEBUG", "Metodo Load de la página", "btnGrabar_Click")
        Try

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            'Instancia Clases
         

            'Sube Archivo
            Dim rutaVirtual As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
            Dim strArchivo As String = rutaVirtual & txtArchivoDocumentos.FileName
            If Not strArchivo.Trim().Equals("") Then

                Dim objReader As New StreamReader(strArchivo)
                Dim sLine As String = ""
                Dim arrText As New ArrayList()
                Dim placa As String
                Dim NroMotor As String
                Dim FechaAdquisicion As String
                Dim FechaInscripcion As String
                Dim FechaDeclaracion As String
                Dim Marca As String
                Dim Modelo As String
                Dim clase As String
                Dim AnioFabricacion As String
                Dim AnioAfectacion As String
                Dim NroCuota As String
                Dim ImpuestoAnual As String
                Do
                    sLine = objReader.ReadLine()
                    If Not sLine Is Nothing Then
                        arrText.Add(sLine)
                    End If
                Loop Until sLine Is Nothing
                objReader.Close()

                For Each sLine In arrText
                   
                    placa = sLine.Substring(0, 9)
                    NroMotor = sLine.Substring(10, 19)
                    FechaAdquisicion = sLine.Substring(29, 9)
                    FechaInscripcion = sLine.Substring(38, 8)
                    FechaDeclaracion = sLine.Substring(46, 8)
                    Marca = sLine.Substring(54, 20)
                    Modelo = sLine.Substring(74, 20)
                    clase = sLine.Substring(94, 1)
                    AnioFabricacion = sLine.Substring(95, 4)
                    AnioAfectacion = sLine.Substring(99, 4)
                    NroCuota = sLine.Substring(103, 1)
                    ImpuestoAnual = sLine.Substring(104, 14)
                Next

                ' pGuardarArchivoFileServerBase(Me.txtArchivoDocumentos, False, "Mant.Bien", Nothing)
            Else
                ' ViewState("RutaArchivoAdj") = HttpUtility.UrlDecode(Me.hddAdjunto.Value)
            End If

            'Graba Documentos

            
            'Valida Resultado
            'If blnResult Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_cargaListado();", True)
            'End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGrabar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "'))", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGrabar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try


    End Sub
End Class
