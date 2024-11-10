<%@ Page 
    Title="Maintain Student Main Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminMaintainLecturerMainPage.aspx.cs" 
    Inherits="AdminMaintainLecturerMainPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentChangePasswordPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="search-bar">
        <asp:TextBox ID="txtSearchInput" runat="server"></asp:TextBox>
        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search.png" OnClick="btnSearch_Click"/>
        <asp:DropDownList ID="ddlSearch" runat="server">
            <asp:ListItem Text="All" Value="all"></asp:ListItem>
            <asp:ListItem Text="Name" Value="name"></asp:ListItem>
            <asp:ListItem Text="ID" Value="sid"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:GridView ID="gvLecturerInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="grid-view"
        OnRowCommand="gvLecturerInfo_RowCommand" 
        DataKeyNames="lid" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField HeaderText="Name" DataField="name" SortExpression="name"/>
            <asp:BoundField HeaderText="Lecturer ID" DataField="lid" SortExpression="sid"/>
        
            <asp:TemplateField HeaderText="Operate">
                <ItemTemplate>
                    <asp:ImageButton 
                        ID="btnEdit" 
                        runat="server" 
                        ImageUrl="~/Images/edit.png"
                        CommandName="Edit"
                        CommandArgument='<%# Eval("sid") %>'
                        ToolTip="Click to edit student details"/>
                    <asp:ImageButton 
                        ID="btnDelete" 
                        runat="server" 
                        ImageUrl="~/Images/delete.png"
                        CommandName="Delete"
                        CommandArgument='<%# Eval("sid") %>'
                        ToolTip="Click to delete student"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No lecturer data found</p>
        </EmptyDataTemplate>
    </asp:GridView>

    <div class="button-container">
        <asp:Button ID="btnAddLecturer" runat="server" Text="Add Lecturer" OnClick="btnAddLecturer_Click" />
    </div>
</asp:Content>
