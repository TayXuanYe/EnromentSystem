<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassTimetable.aspx.cs" Inherits="ClassTimetable" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Class Timetable</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/timetable.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
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
        <div class="timetable">
            <h2>Class Timetable PDF Preview</h2>
            <asp:Literal ID="LiteralPdfPreview" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>

