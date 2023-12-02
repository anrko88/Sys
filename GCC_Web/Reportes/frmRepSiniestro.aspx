<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRepSiniestro.aspx.vb"
    Inherits="Reportes_frmRepSiniestro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Util/css/css_global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .title
        {
            border-right: black 1px solid;
            border-top: black 1px solid;
            font-weight: bold;
            font-size: 11px;
            border-left: black 1px solid;
            color: #ffffff;
            border-bottom: black 1px solid;
            font-family: arial;
            height: 15px;
            background-color: black;
            text-align: Center;
        }
        .trCabecera
        {
            font-family: arial;
            font-size: 9px;
            text-align: center;
            background-color: silver;
            border-right: black 1px solid;
            border-top: black 1px solid;
            border-bottom: black 1px solid;
            border-left: black 1px solid;
        }
        .tdFilas
        {
            font-family: arial;
            font-size: 9px;
            text-align: center;
            background-color: #ffffff;
            border-right: black 1px solid;
            border-top: black 1px solid;
            border-bottom: black 1px solid;
            border-left: black 1px solid;
        }
        .tableblackgrilla
        {
            border: solid;
            border: black;
            border-collapse: collapse;
            border-bottom-color: black;
            border-bottom-style: solid;
            border-top: black:1px;
            border-top-color: black;
            border-top-style: solid;
            border-left: black:1px;
            border-left-color: black;
            border-left-style: solid;
            border-right: black:1px;
            border-right-color: black;
            border-right-style: solid;
            border-width: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
            <td style="text-align: left; height: 30px">
                <img src="C:\Files\logo.png" runat="server" id="imgLogo" style="width: 150px; height: 30px" />
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
                <asp:Label ID="lblTitle" Text="Siniestro" runat="server" Font-Size="Small" ForeColor="black"
                    Font-Bold="True" Font-Names="Tahoma, Helvetica, sans-serif"></asp:Label>
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
            <td valign="top" style="width: 5px">
            </td>
            <td valign="top">
                <asp:Table ID="tbReporte" runat="server" Width="100%" EnableTheming="False" CssClass="tableblackgrilla">
                </asp:Table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
