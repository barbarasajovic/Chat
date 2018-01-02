<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="WcfService1.Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td><asp:Label ID="CurrentUser" runat="server"></asp:Label></td><td><asp:Button ID="Logout" runat="server" Text="Odjava" Width="90px" OnClick="Logout_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Refresh" runat="server" Text="Osveži" Width="90px" /></td></tr>
        <tr><td><asp:ListBox ID="Messages" runat="server" Width="200px"></asp:ListBox></td><td>
            
            <asp:ListBox ID="Users" runat="server" Width="199px"></asp:ListBox>
            </td></tr>
        <tr><td><asp:TextBox ID="Message" runat="server" Width="191px"></asp:TextBox></td><td><asp:Button ID="Send" runat="server" Text="Pošlji" OnClick="Send_Click" Width="200px" /></td></tr>
    </table>
        
        
        
        
        
        
        
    </div>
    </form>
</body>
</html>