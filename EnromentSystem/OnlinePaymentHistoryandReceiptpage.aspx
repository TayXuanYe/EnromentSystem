<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OnlinePaymentHistoryandReceiptpage.aspx.cs" Inherits="OnlinePaymentHistoryandReceiptpage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/OnlinePaymentHistoryandReceipt.css") %>" />
    <style>
        .box{
            display:flex;
            justify-content:center;
            align-items:center;
        }
        .container{
            width:80%;
            border-radius:10px;
            background-color:white;
            margin:20px;
            padding:10px;
            justify-content:center;
        }
    </style>
</asp:Content>
<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
         <div class="box"> 
       <br />
       <h1>Online Payment History / Receipt </h1>
   </div>
       <div class="container">
     <br />
  
        <asp:Panel ID="Panel1" runat="server" CssClass="Panel1">
            <div class="date-range-container">
                <strong>
                    <asp:Label ID="Label1" runat="server" Text="Transaction Date"></asp:Label>
                </strong>&nbsp; From
                <asp:TextBox ID="TextBox1" runat="server" CssClass="text-box" TextMode="Date"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp; To
                <asp:TextBox ID="TextBox2" runat="server" CssClass="text-box" TextMode="Date"></asp:TextBox>
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
    </div>
    <h3>@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved.</h3>

    </asp:Content>