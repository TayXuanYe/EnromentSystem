<%@ Page 
    Title="Update Bank Details Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentStatementPage.aspx.cs" 
    Inherits="StudentStatementPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentStatementPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <h1>Student Statement</h1>
    <div class="table-contain">
        <table>
            <tr>
                <td>
                    <b>Transaction Date</b>
                </td>
                <td>
                    From <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    To <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:RequiredFieldValidator runat="server" 
                        ErrorMessage="This field is requited"
                        CssClass="validator"
                        ControlToValidate="txtStartDate"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RequiredFieldValidator runat="server" 
                        ErrorMessage="This field is requited"
                        CssClass="validator"
                        ControlToValidate="txtEndDate"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnDisplayPdf" runat="server"></asp:Panel>
</asp:Content>