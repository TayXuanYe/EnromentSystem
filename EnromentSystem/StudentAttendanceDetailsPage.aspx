<%@ Page 
    Title="Take Attendent page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentAttendanceDetailsPage.aspx.cs" 
    Inherits="StudentAttendanceDetailsPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/lecturerTakeAttendentPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <h1>Class Attendance</h1>
        <table class = "lecturer-info-table">
            <tr>
                <td>Semester</td>
                <td>
                    <asp:Label ID="lblSemester" runat="server"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>Lecturer</td>
                <td>
                    <asp:Label ID="lblLecturer" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Course</td>
                <td>
                    <asp:Label ID="lblCourse" runat="server"></asp:Label>
                </td>
            </tr>
         </table>
        <asp:GridView ID="gvHistory" runat="server" 
            AutoGenerateColumns="false" 
            CssClass="grid-view"
            DataKeyNames="cid" 
            ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField HeaderText="Course Id" DataField="cid" SortExpression="cid"/>
                <asp:BoundField HeaderText="Course Name" DataField="name" SortExpression="name"/>
                <asp:BoundField HeaderText="Date" DataField="date" SortExpression="date"/>
                <asp:BoundField HeaderText="Attendance" DataField="attendance" SortExpression="attendance"/>
            </Columns>

            <EmptyDataTemplate>
                <p class="center">No history</p>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>


