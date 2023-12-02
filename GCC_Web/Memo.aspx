<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Memo.aspx.vb" Inherits="pruebaMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Button" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger" 
            Text="Label"></asp:Label>
        &nbsp;
        <asp:Button ID="ButtonDetallesEx" runat="server" Text="Detalles..." 
            Visible="False" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <br />
        <br />
        <asp:Label ID="LabelError" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
