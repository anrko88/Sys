Imports Microsoft.Reporting.WebForms
Imports System.Data
Imports GCC.UI
Imports GCC.LogicWS
Imports GCC.Entity

Partial Class Temporal_frmReporte
    Inherits System.Web.UI.Page

    Dim mstrContrato As String
    Dim mstrCotizacion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mstrContrato = Request.QueryString("ncontrato")
            mstrCotizacion = Request.QueryString("ncotizacion")
            If Not IsPostBack Then
                pCargarReporte()
            End If
        Catch ex As Exception
            GCCUtilitario.Show(ex.Message, Me)
        End Try
    End Sub

    Private Sub pCargarReporte()
        Dim oLwsDocClienteNtx As New LDocClienteNTx
        Dim oLwsCotizacion As New LCotizacionNTx
        Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsDocClienteNtx.ObtenerContratoCotizacion(mstrContrato))
        Dim odtbCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsCotizacion.ObtenerCronogramaActual(mstrCotizacion))


        Dim ds1 As New ReportDataSource("DataSet1_dtbContCotiz", odtbDatos)
        Dim ds2 As New ReportDataSource("DataSet1_dtbCronograma", odtbCronograma)

        rpvReporte.LocalReport.DataSources.Clear()
        rpvReporte.LocalReport.ReportPath = "Temporal/rptAnexo.rdlc"
        rpvReporte.LocalReport.DataSources.Add(ds1)
        rpvReporte.LocalReport.DataSources.Add(ds2)
        
        ViewState.Clear()
        rpvReporte.LocalReport.Refresh()
    End Sub


End Class
