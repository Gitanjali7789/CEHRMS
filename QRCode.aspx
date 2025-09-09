<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="CEHRMS.QRCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; margin-top:50px;">
            <asp:Label ID="lblText" runat="server" Text="Enter text to generate QR:" />
            <br /><br />
            <asp:TextBox ID="txtInput" runat="server" Width="250px" />
            <br /><br />
            <asp:Button ID="btnGenerate" runat="server" Text="Generate QR Code" OnClick="btnGenerate_Click" />
            <br /><br />
            <asp:Image ID="imgQR" runat="server" Visible="false" Width="200px" Height="200px" />
        </div>
    </form>
</body>
</html>
