<%@ Page 
    Title="Class Timetablee"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="ClassTimetable.aspx.cs" 
    Inherits="ClassTimetable" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/timetable.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="timetable">
        <h2>Class Timetable PDF Preview</h2>
        <asp:Literal ID="LiteralPdfPreview" runat="server"></asp:Literal>
    </div>
</asp:Content>

