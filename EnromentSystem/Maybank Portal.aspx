<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maybank Portal.aspx.cs" Inherits="Maybank_Portal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/MaybankPaymentPortal.css") %>" />
     <script src="Scripts/Maybank.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Maybank Top Bar.png" CssClass="image"/>
        </div>
        <asp:Button ID="Button3" runat="server" Text="Button" />
         <div class="container">
            <asp:Panel ID="Panel1" runat="server" CssClass="panel1">
                <asp:Label ID="Label1" runat="server" Text="INTI INTERNATIONAL UNIVERSITY" CssClass="Label1"></asp:Label>
                <div class="timer-wrapper">
                    <div class="time-caption">Time Remaining</div>
                    <div id="timer" class="timer"></div>
                </div>
                <hr class="horizontal-line" />
                <div>
                  <table class="invisible-table">
                      <tr>
                         <th>
                             Order Referrence
                         </th>
                          <th>
                              Name
                          </th>
                          <th>
                              Payment Description
                          </th>
                          <th>
                              Total Amount
                          </th>
                      </tr>
                      <tr>
                      <td>
                  <asp:Label ID="orderreference" runat="server" CssClass="labels"></asp:Label>
                      </td>
                       <td>
                   <asp:Label ID="studentname" runat="server" CssClass="labels"></asp:Label>
                         </td>
                          <td>
                   <asp:Label ID="paymentdescription" runat="server" CssClass="labels"></asp:Label>
                         </td>
                          <td>
                   <asp:Label ID="amount" runat="server" CssClass="labels"></asp:Label>
                          </td>
                      </tr>
                  </table>
                </div>
        </asp:Panel>
        </div>
        <div class="container">
        <asp:Panel ID="Panel2" runat="server" CssClass="panel2">
            <div class="box">
            <table>
                <tr class="small-table">
                    <td><asp:Label ID="Label6" runat="server" Text="Select Payment Method" CssClass="labelselect"></asp:Label>  </td>
                    <td><div class="line-between"></div></td>
                    <td><asp:Label ID="Label7" runat="server" Text="Make Payment" CssClass="labelpay"></asp:Label> </td>
                </tr>

            </table>
       </div>
            <table>
                <tr>
                    <td>Card Number</td>
                    <td>
                        <asp:TextBox ID="cardnumber" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                   <td>Name on Card</td>
                    <td>
                        <asp:TextBox ID="nameoncard" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        Expiration date
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="ddlExpirationMonth" runat="server"></asp:DropDownList>
                    </td>
                   <td class="auto-style1">
                       <asp:DropDownList ID="ddlExpirationYear" runat="server"></asp:DropDownList>
                   </td>
                </tr>
                <tr>
                    <td>
                        Security Code (CVV/CVC)
                    </td>
                    <td>
                        <asp:TextBox ID="cvc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Pay" runat="server" Text="Pay" OnClick="Button1_Click" CausesValidation="true" />
                    </td>
                    <td>
                        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
            </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cardnumber" ErrorMessage="You Must Enter your Card Number" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="nameoncard" ErrorMessage="You must enter your card number" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cvc" ErrorMessage="You must enter the cvv code" ForeColor="Red"></asp:RequiredFieldValidator>
    </form>
</body>
</html>
