<%@ Page 
    Title="Delete Program Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminDeleteProgramPage.aspx.cs" 
    Inherits="AdminDeleteProgramPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminDeleteProgramPage.css") %>" />
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
                    <asp:Label ID="lblSchool" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Program Name</td>
                <td>
                    <asp:Label ID="lblProgramName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Level</td>
                <td>
                    <asp:Label ID="lblLevel" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <h2>Major</h2>
    <asp:GridView ID="gvMajorInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="grid-view"
        DataKeyNames="major" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField HeaderText="Major" DataField="major" SortExpression="major"/>
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No major</p>
        </EmptyDataTemplate>
    </asp:GridView>

    <div class="button-container">
        <asp:Button ID="btnDeleteProgram" runat="server" Text="Delete Program" OnClick="btnDeleteProgram_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Delete Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="failWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Delete Fail</h1>
            <br />
            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/not-available.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>  
    
    <asp:Panel ID="conformWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Conform Delete?</h1>
            <p>Once deleted, it cannot be recovered<br />All majors under it will also be deleted.</p>
            <br />
            <p>To delete this program pleas enter following word</p>
            <asp:Label ID="lblConformText" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="txtConformText" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="ConformTextCustomValidator" runat="server" 
                ErrorMessage="Conform Text Not Match"
                CssClass="validator"
                ControlToValidate="txtConformText"
                OnServerValidate="ConformTextCustomValidator_ServerValidate"
                ValidationGroup="conformWindow"></asp:CustomValidator>
            <div class="button-container">
                <asp:Button runat="server" Text="Conform" 
                    OnClick="btnConformDeleteProgram_Click" 
                    ValidationGroup="conformWindow"/>
                <asp:Button ID="btnCancelDeleteConform" runat="server" 
                    Text="Cancel" 
                    CausesValidation="false"
                    OnClick="btnCancelDeleteConform_Click"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

