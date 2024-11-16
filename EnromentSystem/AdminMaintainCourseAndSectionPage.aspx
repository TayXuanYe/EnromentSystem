<%@ Page 
    Title="Maintain Course & Section Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminMaintainCourseAndSectionPage.aspx.cs" 
    Inherits="AdminMaintainCourseAndSectionPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminMaintainCourseSectionPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="search-bar">
        <asp:TextBox ID="txtSearchInput" runat="server"></asp:TextBox>
        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search.png" OnClick="btnSearch_Click"/>
        <asp:DropDownList ID="ddlSearch" runat="server">
            <asp:ListItem Text="All" Value="all"></asp:ListItem>
            <asp:ListItem Text="Course ID" Value="cid"></asp:ListItem>
            <asp:ListItem Text="Name" Value="name"></asp:ListItem>
            <asp:ListItem Text="Credit hours" Value="credit_hours"></asp:ListItem>
            <asp:ListItem Text="Available" Value="available"></asp:ListItem>
            <asp:ListItem Text="Price" Value="price"></asp:ListItem>
            <asp:ListItem Text="Prerequisite" Value="prerequisite"></asp:ListItem>
            <asp:ListItem Text="Program" Value="program"></asp:ListItem>
            <asp:ListItem Text="Major" Value="major"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <h1>Course</h1>
    <asp:GridView ID="gvCourseInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="course-grid-view"
        OnRowCommand="gvCourseInfo_RowCommand" 
        DataKeyNames="cid" 
        OnSelectedIndexChanged="gvCourseInfo_SelectedIndexChanged"
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField HeaderText="Course ID" DataField="cid" SortExpression="cid"/>
            <asp:BoundField HeaderText="Name" DataField="name" SortExpression="name"/>
            <asp:BoundField HeaderText="Credit hours" DataField="credit_hours" SortExpression="credit_hours"/>
            <asp:BoundField HeaderText="Available" DataField="available" SortExpression="available"/>
            <asp:BoundField HeaderText="Price (RM)" DataField="price" SortExpression="price"/>
            <asp:BoundField HeaderText="Pre-requisite Course" DataField="prerequisite" SortExpression="prerequisite"/>
            <asp:BoundField HeaderText="Program" DataField="program" SortExpression="program"/>
            <asp:BoundField HeaderText="Major" DataField="major" SortExpression="major"/>
            <asp:TemplateField HeaderText="Operate">
                <ItemTemplate>
                    <asp:ImageButton 
                        ID="btnEdit" 
                        runat="server" 
                        ImageUrl="~/Images/edit.png"
                        CommandName="Edit"
                        CommandArgument='<%# Eval("program") %>'
                        ToolTip="Click to edit course details"/>
                    <asp:ImageButton 
                        ID="btnDelete" 
                        runat="server" 
                        ImageUrl="~/Images/delete.png"
                        CommandName="Delete"
                        CommandArgument='<%# Eval("program") %>'
                        ToolTip="Click to delete course"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowSelectButton="true" HeaderText="Show Section"  ButtonType="Button" SelectText="Display"/>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No course data found</p>
        </EmptyDataTemplate>
    </asp:GridView>
    <div class="button-container">
        <asp:Button ID="btnAddCourse" runat="server" Text="Add New Course" OnClick="btnAddCourse_Click" />
    </div>

    <h1>Section</h1>
    <div class="text-left-allign">
        <p>Semester: <asp:DropDownList ID="ddlSectionSemester" runat="server"></asp:DropDownList></p
    </div>
    <asp:GridView ID="gvSectionInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="grid-view-section"
        DataKeyNames="sid" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField HeaderText="Section ID" DataField="sid" SortExpression="sid"/>
            <asp:BoundField HeaderText="Name" DataField="name" SortExpression="name"/>
            <asp:BoundField HeaderText="Max enroll allow" DataField="max_enroll" SortExpression="max_enroll"/>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No section found</p>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
