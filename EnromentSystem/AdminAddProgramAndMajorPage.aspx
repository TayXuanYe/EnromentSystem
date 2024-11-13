<%@ Page 
    Title="Add New Program & Major Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminAddProgramAndMajorPage.aspx.cs" 
    Inherits="AdminAddProgramAndMajorPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminAddProgramAndMajorPage.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-contain">
        <table class="program-details-table">
            <tr>
                <th colspan="4">Program</th>
            </tr>
            <tr>
                <td>School</td>
                <td>
                    <asp:DropDownList ID="ddlSchool" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program Name</td>
                <td>
                    <asp:TextBox ID="txtProgramName" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtProgramName"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server" 
                        ControlToValidate="txtProgramName"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Name not in requited format, example: BCSI - ACHELER OF COMPUTER SCIENCES (HONS)"
                        ValidationExpression="[A-Z]{2,}\s-\s[A-Z]+(\s*\([A-Z]*\))?"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ErrorMessage="This name is already exits"
                        CssClass="validator"
                        ControlToValidate="txtProgramName"
                        Display="Dynamic"
                        OnServerValidate="CheckProgramNameIsExist_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server">
                        <asp:ListItem Text="Foundation" Value="Foundation"></asp:ListItem>
                        <asp:ListItem Text="Certificate" Value="Certificate"></asp:ListItem>
                        <asp:ListItem Text="Diploma" Value="Diploma"></asp:ListItem>
                        <asp:ListItem Text="Degree" Value="Degree"></asp:ListItem>
                        <asp:ListItem Text="Master" Value="Master"></asp:ListItem>
                        <asp:ListItem Text="PhD" Value="PhD"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>

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
                        ToolTip="Click to edit major details"/>
                    <asp:ImageButton 
                        ID="btnDelete" 
                        runat="server" 
                        ImageUrl="~/Images/delete.png"
                        CommandName="Delete"
                        CommandArgument='<%# Eval("major") %>'
                        ToolTip="Click to delete major"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No major</p>
        </EmptyDataTemplate>
    </asp:GridView>
    
    <div class="button-container">
        <asp:Button ID="btnAddMajor" runat="server" Text="Add New Major" OnClick="btnAddMajor_Click" />
    </div>

    <div class="button-container">
        <asp:Button ID="btnAddProgram" runat="server" Text="Add Program" OnClick="btnAddProgram_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Add Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
