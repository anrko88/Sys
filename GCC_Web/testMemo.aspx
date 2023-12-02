<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testMemo.aspx.vb" Inherits="testMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="LabelCont" runat="server" Text="NroContrato:"></asp:Label>
        <asp:TextBox ID="TextBoxNroContrato" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelWIO" runat="server" Text="NroWIO:"></asp:Label>
        <asp:TextBox ID="TextBoxNroWIO" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="ButtonTestMemo" runat="server" Text="Memo" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Resultado"></asp:Label>
        :<br />
        <br />
        <asp:TextBox ID="TextConection" runat="server" Visible="False"></asp:TextBox>
        <br />
        <asp:TextBox ID="TextBoxResultado" runat="server" Height="176px" 
            TextMode="MultiLine" Visible="False" Width="531px"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonTest1" runat="server" Text="TestS" Visible="False" />
&nbsp;
        <asp:Button ID="ButtonTest2" runat="server" Text="TestQ" Visible="False" />
        &nbsp;
        <asp:Button ID="ButtonTest3" runat="server" Text="Resultado" Visible="False" />
        &nbsp;
        <asp:GridView ID="gdvVista" runat="server" Visible="False">
        </asp:GridView>
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
