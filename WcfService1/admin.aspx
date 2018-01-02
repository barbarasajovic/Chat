<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="WcfService1.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1 style="align-content:center" class="auto-style1">ADMINISTRATORSKA STRAN</h1>
    </div>
    <div style="text-align: center">&nbsp;<asp:Label ID="CurrentUser" runat="server"></asp:Label>
        <br />
        &nbsp; <strong>Uporabniki z sporočili</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="LogoutAdmin" runat="server" OnClick="LogoutAdmin_Click" Text="Logout" />
        </div>
    <asp:ListBox ID="ListBox1" runat="server" Height="207px" style="text-align: left; margin-left: 440px;" Width="420px"></asp:ListBox>
    <asp:ListBox ID="UsersToEdit" runat="server" Height="207px" style="text-align: left; margin-left: 440px;" Width="420px"></asp:ListBox>
        <asp:Button ID="PostaneAdmin" runat="server" OnClick="PostaneAdmin_Click" Text="Postane Admin" />
        <asp:Button ID="Izbriši" runat="server" OnClick="Izbriši_Click" Text="Izbriši" />
    </form>
</body>
</html>
