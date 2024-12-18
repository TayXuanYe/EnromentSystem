﻿<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PaymentPaybyflywire.aspx.cs" Inherits="PaymentPaybyflywire" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/paymentflywire.css") %>" />

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="payment-container">
        <h2>Payment Options</h2>
        <p>How would you like to pay?</p>

        <div class="payment-methods">
            <div class="method active">
                &nbsp;<p><strong>Credit Or Debit Card Via Maybank Gateway</strong></p>
            </div>
            <div class="method active">
                &nbsp;<p><strong>Flywire For International Students performing Peer-to-Peer transfer</strong></p>
            </div>
        </div>

        <div class="payment-details">
            <asp:Label ID="Label1" runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="TextBoxAmount" runat="server" TextMode="Number" min="0"></asp:TextBox><br />
            <asp:RequiredFieldValidator runat="server" ErrorMessage="This field is requited" ForeColor="Red" ControlToValidate="TextBoxAmount"></asp:RequiredFieldValidator><br />
            <asp:Label ID="Label2" runat="server" Text="Phone No"></asp:Label>
            <asp:TextBox ID="TextBoxPhone" runat="server" placeholder="Enter your contact number"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="This field is requited" ForeColor="Red" ControlToValidate="TextBoxPhone" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" runat="server" 
                ControlToValidate="TextBoxPhone"
                ErrorMessage="Phone number format not correct<br> Format: [country code]-XXX-XXX-XXX"
                ForeColor="Red"
                ValidationExpression="\+\d{1,3}[-]\d{1,4}[-]\d{1,4}[-]\d{1,9}">
            </asp:RegularExpressionValidator>
            <br />
            <!-- Hidden Field for storing the passed Net Amount -->
            <asp:HiddenField ID="HiddenNetAmount" runat="server" />

            <p>
                This payment will be redirected to <strong>Maybank</strong> payment page.
                To keep your information safe and secure, all transactions are protected by SSL encryption.
            </p>
        </div>

        <div class="terms">
            <h3>Terms and Conditions</h3>
            <p>
                By paying using the Credit/Debit card option, you have agreed to our fee charges policy that apply. 
                <a href="<%= ResolveUrl("~/Documents/FeesPolicy.pdf") %>" target="_blank">Student Fees & Charges Policy</a>
            </p>
            <asp:CheckBox ID="CheckBoxAgreeTerms" runat="server" Text="I have read and agree to all the fee charges policy specified above." />
        </div>

        <div class="actions">
            <asp:Button ID="Paybutton" runat="server" Text="Pay" OnClick="Button1_Click" CssClass="btn-primary" />
            <asp:Button ID="Cancelbutton" runat="server" Text="Cancel" CssClass="btn-secondary" OnClick="Cancelbutton_Click" CausesValidation="false"/>
        </div>
    </div>
</asp:Content>