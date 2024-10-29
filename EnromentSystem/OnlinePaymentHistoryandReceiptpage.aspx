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
  
       <div>
        <asp:Panel ID="Panel1" runat="server" CssClass="Panel1">
            <div class="date-range-container">
                <strong>
                    <asp:Label ID="Label1" runat="server" Text="Transaction Date"></asp:Label>
                </strong>&nbsp; From
                <asp:TextBox ID="TextBox1" runat="server" CssClass="text-box"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="image-button" />
                &nbsp;&nbsp;&nbsp;&nbsp; To
                <asp:TextBox ID="TextBox2" runat="server" CssClass="text-box"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" CssClass="image-button" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="View" CssClass="view-button" />
            </div>
        </asp:Panel>
    </div>
        <div>
         <asp:Panel ID="Panel2" runat="server">
     <!-- New Course Table -->
     <table class="course-table">
         <thead>
             <tr>
                 <th>No</th>
                 <th>Transaction Date</th>
                 <th>Time</th>
                 <th>Amount (RM)</th>
                 <th>Status</th>
                 <th>Responce Message</th>
                 <th>Reprint Receipt</th>
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
             </tr>
             <tr>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                  

             </tr>
             <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                </tr>
              <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
              </tr>
             <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
             <!-- Add more rows as needed -->
         </tbody>
     </table>
 </asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <h3>@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved.</h3>

    </asp:Content>