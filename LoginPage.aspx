<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CEHRMS.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
    <link rel="stylesheet" type="text/css" href="assets2/css/style.css" />
    <link href="assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/a81368914c.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1" /></head>
<body>
    <img class="wave" src="assets2/img/wave.png" />
    <div class="container">
        <div class="img">
            <img src="assets2/img/bg.svg" />
        </div>
        <div class="login-content">
            <form runat="server">
                <%--<img src="assets2/img/cehms-logo1.png" />--%>
                <img src="assets2/img/LogoNewBetter.png" />
                <h2 class="title">Welcome</h2>
                <div class="i">
                    <i class="fa fa-users"></i>
                </div>
                <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                    <asp:ListItem Value="0">Select User Type</asp:ListItem>
                    <asp:ListItem Value="1">Patient</asp:ListItem>
                    <asp:ListItem Value="2">Doctor</asp:ListItem>
                    <asp:ListItem Value="3">Receptionist</asp:ListItem>
                    <asp:ListItem Value="4">Admin</asp:ListItem>
                </asp:DropDownList><!--UserType-->
                <div class="input-div one">
                    <div class="i">
                        <i class="fa fa-user"></i>
                    </div>
                    <div class="div">
                        <asp:TextBox ID="txtUser" runat="server" placeholder="UserId:"></asp:TextBox>
                    </div>
                </div>
                <!--username-->
                <div class="input-div pass">
                    <div class="i">
                        <i class="fa fa-lock"></i>
                    </div>
                    <div class="div">
                        <asp:TextBox ID="TxtPass" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <!--password-->
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                <a href="RegisterPage.aspx" style="float: left">Don't have an account?</a>
                <a href="ForgotPasswordPage.aspx">Forgot Password?</a>
                <asp:Button ID="btnLogin" runat="server" Text="LOG IN" class="btn btn-success" OnClick="btnLogin_Click" />
            </form>
        </div>
    </div>
    <script type="text/javascript" src="assets2/js/main.js"></script>
</body>
</html>
