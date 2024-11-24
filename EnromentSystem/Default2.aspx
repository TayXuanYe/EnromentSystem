<%@ Page 
    Title="Student Details and Payment" 
    MasterPageFile="~/Site.master" 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Default2.aspx.cs" 
    Inherits="Default2" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentPage.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Student Details and Payment Summary</h1>
    <asp:Panel ID="pnDisplayPdf" runat="server" CssClass="pdfPanel">
        <iframe id="pdfFrame" runat="server" class="pdfPanel"></iframe>
    </asp:Panel>
</asp:Content>



