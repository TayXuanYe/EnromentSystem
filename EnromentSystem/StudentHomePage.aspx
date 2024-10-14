<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentHomePage.aspx.cs" Inherits="StudentHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/home.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
</head>
<body>
<form id="form1" runat="server">
<div class="box">
    <div class="header">
        <div id="intiLogo"></div>
        <div id="identityCard">
            <asp:Button ID="btnHomeButton" runat="server" Text="" />
            <div id="studentDetails">
                <asp:Label runat="server" Text="Tay Xuan Ye<br>I23024312<br>SCSI - BACHELOR OF COMPUTER SCIENCE (HONS)"></asp:Label>
            </div>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" />
        </div>
    </div>
    <div>

    </div>
</div>
</form>
</body>
</html>
