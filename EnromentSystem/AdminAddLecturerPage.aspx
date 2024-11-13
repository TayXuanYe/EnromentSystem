<%@ Page 
    Title="Add New Lecturer Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminAddLecturerPage.aspx.cs" 
    Inherits="AdminAddLecturerPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminAddLecturerPage.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-contain">
        <table class="lecturer-details-table">
            <tr>
                <th colspan="4">Lecturer Details</th>
            </tr>
            <tr>
                <td>Lecturer Name</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtName"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Lecturer ID</td>
                <td>
                    <asp:TextBox ID="txtId" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtId"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server" 
                        ControlToValidate="txtId"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Lecturer ID not in required format"
                        ValidationExpression="[Ll]\d{8}"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ErrorMessage="This lecturer ID is already exits"
                        CssClass="validator"
                        ControlToValidate="txtId"
                        Display="Dynamic"
                        OnServerValidate="CheckIdIsExist_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
        </table>
    </div>

    <div class="button-container">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Add New Lecturer Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>