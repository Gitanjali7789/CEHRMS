<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mail.aspx.cs" Inherits="CEHRMS.Mail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:50px;">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
            <br /><br />
            <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" OnClick="btnSendMail_Click" />
        </div>
    </form>
</body>
</html>
