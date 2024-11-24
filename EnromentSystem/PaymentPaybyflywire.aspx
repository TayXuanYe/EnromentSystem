<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PaymentPaybyflywire.aspx.cs" Inherits="PaymentPaybyflywire" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/paymentflywire.css") %>" />

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="payment-container">
        <h2>Payment Options</h2>
        <p>How would you like to pay?</p>

        <div class="payment-methods">
            <div class="method active">
                <img src="<%= ResolveUrl("~/Images/maybank-logo.png") %>" alt="Maybank Logo" />
                <p>Credit Or Debit Card Via Maybank Gateway</p>
            </div>
            <div class="method">
                <img src="<%= ResolveUrl("~/Images/flywire-logo.png") %>" alt="Flywire Logo" />
                <p>For International Students performing Peer-to-Peer transfer</p>
            </div>
        </div>

        <div class="payment-details">
            <asp:Label ID="Label1" runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>

            <asp:Label ID="Label2" runat="server" Text="Phone No"></asp:Label>
            <asp:TextBox ID="TextBoxPhone" runat="server" placeholder="Enter your contact number"></asp:TextBox>

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
            <asp:Button ID="ButtonPay" runat="server" Text="Pay" OnClick="Button1_Click" CssClass="btn-primary" />
            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="btn-secondary" />
        </div>
    </div>
</asp:Content>