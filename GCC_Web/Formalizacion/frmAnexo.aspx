<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAnexo.aspx.vb" Inherits="Formalizacion_frmAnexo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <rsweb:ReportViewer ID="rpvReporte" runat="server" Width="100%" Height="520px">
     </rsweb:ReportViewer>
     </div>
    </form>
</body>
</html>