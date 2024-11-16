<%@ Page 
    Title="Home Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminSettingPage.aspx.cs" 
    Inherits="AdminSettingPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/settingPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfCurrenSemester" runat="server" />
    <div class="table-contain">
        <table class="semester-details-table">
            <tr>
                <th colspan="2"><span>Semester</span></th>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                    <asp:TextBox ID="txtSemesterName" runat="server" ValidationGroup="semester"></asp:TextBox>
                </td>
                <td>Credit Hours</td>
                <td>
                    <asp:TextBox ID="txtCreditHours" runat="server" ValidationGroup="semester" TextMode="Number"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="This field is requited"
                        ValidationGroup="semester"
                        CssClass="validator"
                        ControlToValidate="txtSemesterName"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ErrorMessage="This field only accept alphabet"
                        CssClass="validator"
                        ControlToValidate="txtSemesterName"
                        Display="Dynamic"
                        ValidationExpression="[A-Z]+"></asp:RegularExpressionValidator>
                </td>
                <th></th>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="This field is requited"
                        ValidationGroup="semester"
                        CssClass="validator"
                        ControlToValidate="txtCreditHours"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th></th>
                <th></th>
                <th></th>
                <th>
                    <div class="update-button">
                        <asp:Button ID="btnUpdateSemester" runat="server" 
                            Text="Update" 
                            OnClick="btnUpdateSemester_Click" 
                            ValidationGroup="semester"/>
                    </div>
                </th>
            </tr>
            <tr>
                <th colspan="2"><span>Enable Option</span></th>
            </tr>
            <tr>
                <td>Enroll Available</td>
                <td>
                    <div class="toggle-btn">
                        <asp:Button ID="btnEnroll" runat="server" OnClick="btnEnroll_Click" CausesValidation="false"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>Add and Drop Available</td>
                <td>
                    <div class="toggle-btn">
                        <asp:Button ID="btnAddDrop" runat="server" OnClick="btnAddDrop_Click" CausesValidation="false"/>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Update Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>