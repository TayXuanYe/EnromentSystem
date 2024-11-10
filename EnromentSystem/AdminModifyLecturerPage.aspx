<%@ Page 
    Title="Modify Lecturer Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminModifyLecturerPage.aspx.cs" 
    Inherits="AdminModifyLecturerPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminModifyLecturerPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-contain">     
        <table class="lecturer-details-table">
            <tr>
                <th colspan="4">Lecturer Details</th>
            </tr>
            <tr>
                <td>Lecturer Name</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
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
                    <asp:Label ID="lblId" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div class="button-container">
        <asp:Button ID="btnAdd" runat="server" Text="Update" OnClick="btnAdd_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="confirmationWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h2>Confirm to update the following data:</h2>
            <table class="lecturer-confirm-details-table">
                <tr>
                    <th colspan="2">Current Information</th>
                    <th></th>
                    <th></th>
                    <th colspan="2">Update Information</th>
                </tr>
                <tr>
                    <td>Name</td>
                    <td>
                        <asp:Label ID="lblCurrenName" runat="server"></asp:Label>
                    </td>
                    <th></th>
                    <th></th>
                    <td>Name</td>
                    <td>
                        <asp:Label ID="lblUpdateName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            
            <div class="button-container">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
                <asp:Button ID="btnCancelUpdate" runat="server" Text="Cancel" OnClick="btnCancelUpdate_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Update Student Details Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>