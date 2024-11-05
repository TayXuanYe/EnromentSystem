<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminHomePage.aspx.cs" Inherits="AdminHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminHomePage.css") %>" />

</head>
<body>
<form id="form1" runat="server">
    <div class="box">
        <table>
            <tr>
                <td>
                    <div class="adminInfoCard">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/INTI_Malaysia_Logo.png"/>
                            <h1>Welcome Back Admin</h1>
                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    </div>
                </td>
                <td>
                    <table class="card-contain">
                        <tr>
                            <td>
                                <a href="#" >
                                    <div class="functionCard">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/student (1).png"/>
                                        <span>Student</span>
                                    </div>
                                </a>
                            </td>
                            <td>
                                <a href="#">
                                    <div class="functionCard">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/lecturer (1).png"/>
                                        <span>Lecture</span>
                                    </div>
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="#">
                                    <div class="functionCard">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/program.png"/>
                                        <span>Program</span>
                                    </div>
                                </a>
                            </td>
                            <td>
                                <a href="#">
                                    <div class="functionCard">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/course.png"/>
                                        <span>Course</span>
                                    </div>
                                </a>
                            </td>
                        </tr>
                            
                    </table>
                </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>
