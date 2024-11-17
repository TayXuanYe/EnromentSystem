<%@ Page 
    Title="Student Time Table Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentClassTimetablePage.aspx.cs" 
    Inherits="StudentClassTimetablePage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentStatementPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <h1>Registration Summary / Class Timetable</h1>
    <h1>&nbsp</h1>
    <asp:Panel ID="pnDisplayPdf" runat="server" CssClass="pdfPanel">
        <iframe id="pdfFrame" runat="server" class="pdfPanel"></iframe>
    </asp:Panel>
</asp:Content>