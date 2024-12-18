﻿<%@ Page 
    Title="Home Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminHomePage.aspx.cs" 
    Inherits="AdminHomePage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminHomePage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <table class="card-contain">
            <tr>
                <td rowspan="2" colspan="2">
                    <div class="adminInfoCard">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/INTI_Malaysia_Logo.png"/>
                            <h1>Welcome Back Admin</h1>
                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    </div>
                </td>
                <td>
                    <a href="AdminMaintainStudentMainPage.aspx" >
                        <div class="functionCard">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/student (1).png"/>
                            <span>Student</span>
                        </div>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="AdminMaintainLecturerMainPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/lecturer (1).png"/>
                            <span>Lecture</span>
                        </div>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="AdminSettingPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/setting.png"/>
                            <span>Setting</span>
                        </div>
                    </a>
                </td>
                <td>
                    <a href="AdminMaintainProgramAndMajorPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/program.png"/>
                            <span>Program & Major</span>
                        </div>
                    </a>
                </td>
                <td>
                    <a href="AdminMaintainCourseAndSectionPage.aspx">
                        <div class="functionCard">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/course.png"/>
                            <span>Course & Section</span>
                        </div>
                    </a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

