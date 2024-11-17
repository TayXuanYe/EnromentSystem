<%@ Page 
    Title="Take Attendent Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentTakeAttendentPage.aspx.cs" 
    Inherits="StudentTakeAttendentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentTakeAttendentPage.css") %>" />

</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <h1>Take Attendent</h1>
    <div class="qr-contain">
        <div id="qr-reader" class="reader" style="width: 500px;"></div>
        <div id="qr-result">Scanned Code: <span id="result"></span></div>
    </div>

    
    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Attendent Take Successful</h1>
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
            <h1>
                <asp:Label ID="txtErrorMessage" runat="server" CssClass="errorLabel"></asp:Label></h1>
            <br />
            <asp:Image runat="server" ImageUrl="~/Images/not-available.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
