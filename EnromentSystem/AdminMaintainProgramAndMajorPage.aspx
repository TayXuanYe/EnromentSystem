<%@ Page 
    Title="Maintain Program & Major Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminMaintainProgramAndMajorPage.aspx.cs" 
    Inherits="AdminMaintainProgramAndMajorPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminMaintainLecturerMainPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="search-bar">
        <asp:TextBox ID="txtSearchInput" runat="server"></asp:TextBox>
        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search.png" OnClick="btnSearch_Click"/>
        <asp:DropDownList ID="ddlSearch" runat="server">
            <asp:ListItem Text="All" Value="all"></asp:ListItem>
            <asp:ListItem Text="Program" Value="program"></asp:ListItem>
            <asp:ListItem Text="School" Value="school"></asp:ListItem>
            <asp:ListItem Text="Level" Value="level"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <h1>Program</h1>
    <asp:GridView ID="gvProgramInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="grid-view"
        OnRowCommand="gvProgramInfo_RowCommand" 
        DataKeyNames="program" 
        ShowHeaderWhenEmpty="True"
        OnSelectedIndexChanged="gvProgramInfo_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Program" DataField="program" SortExpression="program"/>
            <asp:BoundField HeaderText="School" DataField="school" SortExpression="school"/>
            <asp:BoundField HeaderText="Level" DataField="level" SortExpression="level"/>
        
            <asp:TemplateField HeaderText="Operate">
                <ItemTemplate>
                    <asp:ImageButton 
                        ID="btnEdit" 
                        runat="server" 
                        ImageUrl="~/Images/edit.png"
                        CommandName="Edit"
                        CommandArgument='<%# Eval("program") %>'
                        ToolTip="Click to edit program details"/>
                    <asp:ImageButton 
                        ID="btnDelete" 
                        runat="server" 
                        ImageUrl="~/Images/delete.png"
                        CommandName="Delete"
                        CommandArgument='<%# Eval("program") %>'
                        ToolTip="Click to program student"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No program data found</p>
        </EmptyDataTemplate>
    </asp:GridView>
    
    <h1>Major</h1>
    <asp:GridView ID="gvMajorInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="grid-view"
        OnRowCommand="gvMajorInfo_RowCommand" 
        DataKeyNames="major" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField HeaderText="Major" DataField="major" SortExpression="major"/>
        
            <asp:TemplateField HeaderText="Operate">
                <ItemTemplate>
                    <asp:ImageButton 
                        ID="btnEdit" 
                        runat="server" 
                        ImageUrl="~/Images/edit.png"
                        CommandName="Edit"
                        CommandArgument='<%# Eval("major") %>'
                        ToolTip="Click to edit program details"/>
                    <asp:ImageButton 
                        ID="btnDelete" 
                        runat="server" 
                        ImageUrl="~/Images/delete.png"
                        CommandName="Delete"
                        CommandArgument='<%# Eval("major") %>'
                        ToolTip="Click to program student"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No major found</p>
        </EmptyDataTemplate>
    </asp:GridView>

    <div class="button-container">
        <asp:Button ID="btnAddMajor" runat="server" Text="Add New Major" OnClick="btnAddMajor_Click" />
    </div>
</asp:Content>
