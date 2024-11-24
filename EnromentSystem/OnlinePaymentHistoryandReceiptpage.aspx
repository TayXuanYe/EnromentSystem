<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OnlinePaymentHistoryandReceiptpage.aspx.cs" Inherits="OnlinePaymentHistoryandReceiptpage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/OnlinePaymentHistoryandReceipt.css") %>" />
</asp:Content>
<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
         <div> 
       <br />
       <h1>Online Payment History / Receipt </h1>
   </div>
       
     <br />
  
        <asp:Panel ID="Panel1" runat="server" CssClass="Panel1">
            <div class="date-range-container">
                <strong>
                    <asp:Label ID="Label1" runat="server" Text="Transaction Date"></asp:Label>
                </strong>&nbsp; From
                <asp:TextBox ID="TextBox1" runat="server" CssClass="text-box" TextMode="Date"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="image-button" OnClick="ImageButton1_Click" />
                <asp:Calendar ID="Calendar1" runat="server" Visible="false" OnSelectionChanged="Calendar1_SelectionChanged" />
                &nbsp;&nbsp;&nbsp;&nbsp; To
                <asp:TextBox ID="TextBox2" runat="server" CssClass="text-box"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" CssClass="image-button" OnClick="ImageButton2_Click" />
                <asp:Calendar ID="Calendar2" runat="server" Visible="false" OnSelectionChanged="Calendar2_SelectionChanged" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="View" CssClass="view-button" OnClick="Button1_Click1" />
            </div>
        </asp:Panel>
        <div>
         <asp:Panel ID="Panel2" runat="server">
     <!-- New Course Table -->
        <asp:Table ID="courseTable" class="course-table" runat="server">
       <asp:TableHeaderRow>
        <asp:TableHeaderCell>No</asp:TableHeaderCell>
        <asp:TableHeaderCell>Payment Type</asp:TableHeaderCell>
        <asp:TableHeaderCell>Transaction Date</asp:TableHeaderCell>
        <asp:TableHeaderCell>Amount</asp:TableHeaderCell>
        <asp:TableHeaderCell>Status</asp:TableHeaderCell>
        <asp:TableHeaderCell>Reprint Receipt</asp:TableHeaderCell>

       </asp:TableHeaderRow>
    <asp:TableRow>
    </asp:TableRow>
    </asp:Table>
   
 </asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <h3>@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved.</h3>

    </asp:Content>