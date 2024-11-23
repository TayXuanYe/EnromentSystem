<%@ Page 
    Title="Take Attendent Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentTakeAttendentPage.aspx.cs" 
    Inherits="StudentTakeAttendentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentTakeAttendentPage.css") %>" />
    <script src="https://cdn.jsdelivr.net/npm/html5-qrcode/minified/html5-qrcode.min.js"></script>
    <script src="https://unpkg.com/html5-qrcode/minified/html5-qrcode.min.js"></script>
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <h1>Take Attendent</h1>
    <div class="qr-contain">
        <div id="reader" class="reader"></div>
    </div>
    <script>
        let html5QrCode = null;

        window.onload = function () {
            html5QrCode = new Html5Qrcode("reader");

            html5QrCode.start(
                { facingMode: "environment" },
                {
                    fps: 1, 
                    qrbox: { width: 250, height: 250 }
                },
                qrCodeMessage => {
                    console.log("QR: ", qrCodeMessage);
                    window.location.href = "StudentTakeAttendentResultPage.aspx?qrData="+qrCodeMessage;
                },
                errorMessage => {
                    console.warn("sacn fail", errorMessage);
                }
            );
        };
    </script>
</asp:Content>
