<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLoginPage.aspx.cs" Inherits="MainLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Login Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminHomePage.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="box">
        <table class="card-contain" style="height: auto">
            <tr>
                <th colspan="4" style="color:white">
                    <h1>Select ICON For LOGIN</h1>
                </th>
            </tr>
            <tr>
                <td>
                    <a href="StudentLoginPage.aspx" >
                        <div class="functionCard">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/student (1).png"/>
                            <span>Student</span>
                        </div>
                    </a>
                </td>
                <td>
                    <a href="LecturerLoginPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/lecturer (1).png"/>
                            <span>Lecture</span>
                        </div>
                    </a>
                </td>
                <td>
                    <a href="AdminLoginPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Administrator.png"/>
                            <span>Admin</span>
                        </div>
                    </a>
                </td>
                <td>
                    <a href="HopLoginPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/book.png"/>
                            <span>HOP</span>
                        </div>
                    </a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
