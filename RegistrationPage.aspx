<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="CEHRMS.RegistrationPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REGISTER</title>
    <link rel="stylesheet" type="text/css" href="assets2/css/style.css" />
    <script src="https://kit.fontawesome.com/a81368914c.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <%--    <script type="text/javascript">
        $(function () {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $('#datepicker').datepicker({
                maxDate: new Date(currentYear, currentMonth, currentDate)
            });
        });
    </script>--%>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var today = new Date();
            var month = ('0' + (today.getMonth() + 1)).slice(-2);
            var day = ('0' + today.getDate()).slice(-2);
            var year = today.getFullYear();
            var date = year + '-' + month + '-' + day;
            $('[id*=txtDob]').attr('max', date);
        });
    </script>
    <%-- <script type="text/javascript">
        $(function () {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $('#txtDob').datepicker({
                maxDate: new Date(currentYear, currentMonth, currentDate)
            });
        });
    </script>--%>
    <style>
        btnn {
            border: none;
        }
    </style>
</head>
<body>
    <img class="wave" src="assets2/img/wave.png" />
    <div class="container">
        <div class="img">
            <img src="assets2/img/bg.svg" />
        </div>
        <div class="login-content">
            <form runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <img src="assets2/img/LogoNewBetter.png" style="width: 102px; height: 100px;" />
                <h2 class="title">Register</h2>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            Picture:<asp:FileUpload ID="FileUpload1" runat="server" Class="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            First Name:<asp:TextBox ID="txtFName" runat="server" class="form-control" ValidationGroup="Patreg"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            Last Name:<asp:TextBox ID="txtLName" runat="server" Class="form-control" ValidationGroup="Patreg"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            DOB:<asp:TextBox ID="txtDob" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                            <%--                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDob" Format="dd/MM/yyyy" Enabled="true"></asp:CalendarExtender>--%>
                        </div>
                        <div class="col-md-6">
                            Gender:<asp:DropDownList ID="ddlGen" runat="server" class="form-control">
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            Blood Group:<asp:DropDownList ID="ddlBl" runat="server" class="form-control">
                                <asp:ListItem>O+</asp:ListItem>
                                <asp:ListItem>O-</asp:ListItem>
                                <asp:ListItem>A+</asp:ListItem>
                                <asp:ListItem>A-</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>B-</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                                <asp:ListItem>AB-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            Adhaar No.:<asp:TextBox ID="txtAdhaarno" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            E-mail:<asp:TextBox ID="txtMail" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            Mobile No.:<asp:TextBox ID="txtMob" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            Address:<asp:TextBox ID="txtAdd" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            Password:<asp:TextBox ID="txtPass" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnPatientRegister" runat="server" Text="Register" Class="btn btn-success" OnClick="BtnPatientRegister_Click" ValidationGroup="Patreg" />
                        </div>
                    </div>
                    <a href="LoginPage.aspx">Already have an account?</a>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </form>
        </div>
    </div>
    <script type="text/javascript" src="assets2/js/main.js"></script>
</body>
</html>
