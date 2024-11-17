﻿<%@ Page 
    Title="Home Page"
    MasterPageFile="~/LecturerSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="LecturerHomePage.aspx.cs" 
    Inherits="LecturerHomePage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/lecturerHomePage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <h1>Dashboard</h1>
        <table class = "lecturer-info-table">
            <tr>
                <td>Semester</td>
                <td>
                    <asp:Label ID="lblSemester" runat="server"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCourse" runat="server" 
            AutoGenerateColumns="false" 
            CssClass="grid-view"
            OnSelectedIndexChanged="gvCourse_SelectedIndexChanged" 
            DataKeyNames="cid" 
            ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField HeaderText="Course Id" DataField="cid" SortExpression="cid"/>
                <asp:BoundField HeaderText="Course Name" DataField="name" SortExpression="name"/>
                <asp:CommandField ShowSelectButton="true" HeaderText="Action" ButtonType="Button" SelectText=" "/>            
            </Columns>

            <EmptyDataTemplate>
                <p class="center">No course</p>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
