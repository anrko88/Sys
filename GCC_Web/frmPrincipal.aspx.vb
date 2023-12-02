Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Xml

Partial Class frmPrincipal
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPrincipal.aspx.vb")
    Private acceso As String

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                acceso = GCCSession.AccesoUsuario
                lblCodeUser.Text = GCCSession.CodigoUsuario.ToString
                lblUserName.Text = GCCSession.NombreUsuario.ToString
                lblDescripRol.Text = GCCSession.DescripcionPerfil.ToString
                lblAmbiente.Text = fCargarAmbiente()
            End If
            pCrearMenu()
        Catch ex As Exception
            GCCSession.LimpiarTotalSesiones()
            HttpResponse.RemoveOutputCacheItem(Request.CurrentExecutionFilePath)
            Session.Abandon()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "CerrarMaster", "pCerrarMaster('" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA").ToString() & "');", True)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Function fCargarAmbiente() As String
        Try
            Dim mValor As String = String.Empty
            Select Case GCCUtilitario.fstrObtieneKeyWebConfig("ArgAmbienteIndex")
                Case 0 : mValor = "PRD"
                Case 1 : mValor = "UAT"
                Case 2 : mValor = "SIT"
                Case 3 : mValor = "DES"
            End Select
            Return mValor
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub lkbCerrarSession_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkbCerrarSession.Click
        Try
            GCCSession.LimpiarTotalSesiones()
            HttpResponse.RemoveOutputCacheItem(Request.CurrentExecutionFilePath)
            Session.Abandon()
            GC.Collect()

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "CerrarMaster", "pCerrarMaster('" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio").ToString() & "');", True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub pCrearMenu()
        Dim strVer As String = ""
        For x As Int32 = 0 To Len(acceso) - 2
            strVer = acceso.Substring(x, 1)
            Dim newLi As HtmlGenericControl
            newLi = CType(Me.FindControl("m" & (x + 1)), HtmlGenericControl)
            newLi.Visible = CBool(strVer)
        Next
    End Sub

    'Private Function fLeerXml() As String
    '    Dim xmlSeccionList As XmlNodeList
    '    Dim xmlSeccion, xmlChild As XmlNode
    '    Dim XmlDoc As New XmlDocument()
    '    Try
    '        Dim strVer As String = ""
    '        Dim i As Integer = 0
    '        Dim Ruta As String = HttpContext.Current.Server.MapPath("xmlMenu.xml") 'String.Concat(Server.MapPath("xmlMenu.xml"))
    '        XmlDoc.Load(Ruta)
    '        Dim strMenu As String = ""
    '        Dim mNodo As String = "MenuGeneral/menu"
    '        xmlSeccion = XmlDoc.SelectSingleNode(mNodo)
    '        xmlSeccionList = xmlSeccion.ChildNodes

    '        For Each xmlChild In xmlSeccionList
    '            strVer = acceso.Substring(i, 1)
    '            Dim newLi As New HtmlGenericControl
    '            newLi.ID = "m" & (i + 1).ToString
    '            strMenu = newLi.ID
    '            newLi.TagName = "li"
    '            newLi.Attributes.Add("class", "accessible")
    '            newLi.InnerText = xmlChild.ChildNodes(0).InnerText

    '            newLi.Visible = CBool(strVer)
    '            ULMenu.Controls.Add(newLi)
    '            i += (i + 1) + 1
    '            pCrearMenuSub(newLi, strMenu, i)
    '        Next

    '        Return ""
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Private Sub pCrearMenuSub(ByVal uAnt As HtmlGenericControl, ByVal pstrMenu As String, ByVal x As Integer)
    '    Dim xmlSeccionList As XmlNodeList
    '    Dim xmlSeccion, xmlChild As XmlNode
    '    Dim XmlDoc As New XmlDocument()
    '    Dim strVer As String = ""

    '    Dim Ruta As String = HttpContext.Current.Server.MapPath("xmlMenu.xml")
    '    XmlDoc.Load(Ruta)
    '    Dim strMenu As String = pstrMenu & "/m" & x.ToString
    '    Dim mNodo As String = "MenuGeneral/menu/" & strMenu
    '    xmlSeccion = XmlDoc.SelectSingleNode(mNodo)
    '    If xmlSeccion Is Nothing Then Exit Sub
    '    xmlSeccionList = xmlSeccion.ChildNodes

    '    For Each xmlChild In xmlSeccionList
    '        strVer = acceso.Substring(x, 1)
    '        Dim newLi As New HtmlGenericControl
    '        newLi.ID = "m" & (x).ToString
    '        newLi.TagName = "ul"
    '        newLi.InnerText = xmlChild.InnerText
    '        newLi.InnerHtml = xmlChild.InnerText
    '        '.Replace("[", "&#60;")
    '        'newLi.InnerText = newLi.InnerText.Replace("]", "&#62;")
    '        newLi.Attributes.Add("class", "accessible")
    '        newLi.Visible = CBool(strVer)
    '        uAnt.Controls.Add(newLi)
    '        x += 1
    '        If xmlSeccionList.Count > 1 Then pCrearMenuSub(uAnt, strMenu, x)
    '    Next
    'End Sub
#End Region

End Class
