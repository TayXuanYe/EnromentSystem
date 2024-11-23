<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PaymentPage.aspx.cs" Inherits="PaymentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/Payment.css") %>" />
     <style type="text/css">
     .auto-style1 {
         color: #FF1111;
     }
         .auto-style2 {
             width: 43px;
         }
 </style>
</asp:Content>
<asp:Content  ContentPlaceHolderID="MainContent" runat="server">

 <div> 
    <br />
    <h1>Payment</h1>
    <br />
</div>
<div class="Container-table">

<table>
                        <tr>
                            <td>Student Name</td>
                            <td>:</td>
                            <td ><asp:Label ID="Label1" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Matriculation No</td>
                            <td>:</td>
                            <td><asp:Label ID="Label2" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>IC/Passport No</td>
                            <td>:</td>
                            <td><asp:Label ID="Label3" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Program</td>
                            <td>:</td>
                            <td><asp:Label ID="Label4" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Intake</td>
                            <td>:</td>
                            <td><asp:Label ID="Label5" runat="server" /></td>
                        </tr>
                          <tr>
                            <td>Semester</td>
                            <td>:</td>
                            <td><asp:Label ID="Label6" runat="server" /></td>
                         </tr>
                    </table>

</div>
<br />
       <div>
            <!-- New Course Table -->
 <asp:Table ID="courseTable" class="course-table" runat="server">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell>Particulars</asp:TableHeaderCell>
        <asp:TableHeaderCell>Type</asp:TableHeaderCell>
        <asp:TableHeaderCell>Document Date/Instalment Due Date</asp:TableHeaderCell>
        <asp:TableHeaderCell>Amount (RM)</asp:TableHeaderCell>
        <asp:TableHeaderCell>Amount Settled (RM)</asp:TableHeaderCell>
    </asp:TableHeaderRow>
    <asp:TableRow>
    </asp:TableRow>
</asp:Table>
       </div>
       <div class="small-table">

                 <table>
                        <tr>
                            <td>Total Amount Payable</td>
                            <td>:</td>
                            <td><asp:Label ID="Label7" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Scholarship Deduction</td>
                            <td>:</td>
                            <td><asp:Label ID="Label8" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Amount Settled</td>
                            <td>:</td>
                            <td><asp:Label ID="Label13" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="blue">Net Amount Payable</td>
                            <td>:</td>
                            <td><asp:Label ID="Label9" runat="server" /></td>
                        </tr>
                       </table>
           <br />
           
           </div>
           <div class="second-table">
                    <table>
                        <tr>
                             <td><strong>I agree that the invoice(s) detail is correct.</strong></td>       
                        </tr>
                        <tr>
                            <td><em>- Click on </em><span class="auto-style1"><em>Pay By (Credit /Debit Card/Flywire)</em></span><em> Button if you plan to play using your </em><span class="auto-style1"><em>Credit card, Debit Card or Flywire.</em></span></td>
                        </tr>
                        <tr>
                            <td><em>- Click on </em><span class="auto-style1"><em>Pay By (Other)</em></span><em> button if you plan to pay using </em><span class="auto-style1"><em>interbank GIRO or other payment modes.</em></span></td>
                        </tr>
                    </table>

                  </div>
       <br />
       <div class="button-container" >
          <br />
        <asp:Button ID="Button1" runat="server" Text="PAY BY(CREDIT/DEBIT CARD/FLYWIRE)" CssClass="unique-button" OnClick="Button1_Click" />
           &nbsp;<asp:Button ID="Button2" runat="server" Text="PAY BY (OTHER)" CssClass="unique-button" OnClick="Button2_Click" />
           &nbsp;<asp:Button ID="Button3" runat="server" Text="REGISTRATION SUMMARY" CssClass="unique-button" />
           &nbsp;<asp:Button ID="Button4" runat="server" Text="CANCEL" CssClass="unique-button" />
         
      </div>
       <br />
        <asp:Label ID="Label10" runat="server" Text="Please specify your Student ID or IC/Passport as reference during payment transfer." CssClass="blinking-label"></asp:Label>
       <br />
       <asp:Label ID="Label11" runat="server" Text="*** For further clarification please visit FINANCE Office at your respective campus." CssClass="normal-label" ></asp:Label>
       <br />
       <asp:Label ID="Label12" runat="server" Text="@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved." CssClass="copywrite-label"></asp:Label>
       <br />

   </asp:Content>