<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRepSunatContrato.aspx.vb"
    Inherits="Reportes_frmRepSunatContrato" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Util/css/css_global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center;">
                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btnStandar"
                    Width="100px" />
            </td>
        </tr>
    </table>
    <table id="tblReporte" border="0" cellpadding="0" cellspacing="0" style="width: 100%;"
        runat="server">
        <tr>
            <td style="width: 5px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
                <img src="C:\Files\logo.png" runat="server" id="imgLogo" style="width: 150px; height: 30px" />
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td >
                
            </td>
        </tr>        
        <tr>
            <td style="width: 5px">
            </td>
            <td id="tdTitulo" runat="server">
                <b>Sunat - Contratos</b>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
                 <b>Filtros</b>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
                <table>
                    <tr>
                        <td style="width: 210px" colspan="3">
                             <b>Fecha Celebración Contrato Desde</b>
                        </td>
                        <td id="tdFCCD" runat="server">
                        </td>
                        <td>
                            <b>Hasta</b>
                        </td>
                        <td id="tdFCCH" runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 210px" colspan="3">
                            <b>Fecha Activación Contrato Desde</b>
                        </td>
                        <td id="tdFACD" runat="server">
                        </td>
                        <td>
                            <b>Hasta</b>
                        </td>
                        <td id="tdFACH" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td valign="top">
                <asp:Table ID="tbReporte" runat="server" Width="100%" EnableTheming="False">
                </asp:Table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
