<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WcfService1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<style>
    input[type=text], input[type=password] {
        width: 100%;
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: 1px solid #ccc;
        box-sizing: border-box;
    }

    #LoginBtn, #RegistrationBtn, #AdminBtn {
        background-color: #4CAF50;
        color: white;
        padding: 14px 20px;
        margin: 8px 0;
        border: none;
        cursor: pointer;
        width: 100%;
    }

    .cancelbtn {
        width: auto;
        padding: 10px 18px;
        background-color: #f44336;
    }

    .imgcontainer {
        text-align: center;
        margin: 24px 0 12px 0;
    }

    img.avatar {
        width: 40%;
        border-radius: 50%;
    }

    .container {
        padding: 16px;
    }

    span.psw {
        float: right;
        padding-top: 16px;
    }
</style>
<body>
    <form id="form1" runat="server">

        <h2>NoDB - Prijava</h2>


        <div class="imgcontainer">
            <img src="http://previews.123rf.com/images/qoolio/qoolio1603/qoolio160300049/53841864-Geek-Chat-Logo-Design-Logo-template-nerd-Forum-Community-society-talk-discuss-speak-bubble-conversat-Stock-Vector.jpg" alt="Avatar" class="avatar">
        </div>

        <div class="container">
            <label><b>Uporabniško ime</b></label>
            <br />
            <asp:TextBox ID="Username" runat="server"></asp:TextBox>
            <br />
            &nbsp;<label><b>Geslo</b></label>&nbsp;&nbsp;<input id="Password" type="password" runat="server" />

            <asp:Button ID="LoginBtn" runat="server" Text="Prijava" OnClick="LoginBtn_Click" />
            <asp:Button ID="AdminBtn" runat="server" Text="Prijava v administratorske strani" OnClick="AdminBtn_Click" />
            <asp:Button ID="RegistrationBtn" runat="server" Text="Registracija" OnClick="RegistrationBtn_Click" />

            <br />
            <asp:Label ID="Error" runat="server" Text="" Font-Bold="True" Font-Strikeout="False" ForeColor="Red"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:servicechatConnectionString %>" SelectCommand="SELECT * FROM [Uporabnik]"></asp:SqlDataSource>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>

    </form>
</body>
</html>
