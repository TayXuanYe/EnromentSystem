<%@ Page 
    Title="Change Password Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentChangePasswordPage.aspx.cs" 
    Inherits="StudentChangePasswordPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentChangePasswordPage.css") %>" />
     <script type="text/javascript">
        function validatePassword(sender, args) {
           
            var newPassword = document.getElementById('<%= txtNewPassword.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= txtConfirmNewPassword.ClientID %>').value;

            // Regex for validating password: 8-20 characters, at least one letter and one digit
            var regex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$/;

            // Validate new password
            if (!regex.test(newPassword)) {
                args.IsValid = false;  
                args.ErrorMessage = "Password must be between 8 to 20 alphanumeric characters and include at least one letter and one numeric digit.";
                return;
            }

            // Check if the passwords match
            if (newPassword !== confirmPassword) {
                args.IsValid = false; 
                args.ErrorMessage = "New password and confirm password must match.";
                return;
            }

            // If all checks pass, validation is successful
            args.IsValid = true;
        }
    </script>
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
<div class="body">
    <h1>Change Password</h1>
    <p>
       Password must be between 8 to 20 alphanumeric characters. 
       It must include at least one letter and one numeric digit.
       Symbols are not allowed in the password.
    </p>

    <div>
        <div id="midBody">
            <!--Current password-->
            <div class="text-box">
                <p>Existing Password</p>
                <asp:TextBox ID="txtExistingPassword" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator 
                    ControlToValidate="txtExistingPassword"
                    ID="RequiredFieldValidator4" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="This field is requited"
                    CssClass="validator"> </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator4" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="CustomValidator"
                    CssClass="validator"
                    ClientValidationFunction=""
                    OnServerValidate="PasswordFormat_ServerValidate"> </asp:CustomValidator>
            </div>
            <!--New Password-->
            <div class="text-box">
                <p>New Password</p>
                <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator
                    ControlToValidate="txtNewPassword"
                    ID="RequiredFieldValidator5" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="This field is requited"
                    CssClass="validator"> </asp:RequiredFieldValidator>
                <asp:CustomValidator 
                    ID="CustomValidator5" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="CustomValidator"
                    CssClass="validator"
                    ClientValidationFunction="validatePassword"
                    OnServerValidate="PasswordFormat_ServerValidate"> </asp:CustomValidator>
            </div>
            <!--Confirm New Password-->
            <div class="text-box">
                <p>Confirm New Password</p>
                <asp:TextBox ID="txtConfirmNewPassword" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator 
                    ControlToValidate="txtConfirmNewPassword"
                    ID="RequiredFieldValidator6" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="This field is requited"
                    CssClass="validator"> </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator6" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="CustomValidator"
                    CssClass="validator"
                    ClientValidationFunction="validatePassword"
                    OnServerValidate="PasswordFormat_ServerValidate"> </asp:CustomValidator>
            </div>
            
            <div class="button">
                <asp:Button ID="Button1" runat="server" Text="Update Password" CssClass="button" OnClick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="button" OnClick="Button2_Click"/>
            </div>
        </div>
        <div id="verificationPopUp" runat="server" style="display:none;">
    <div class="popup-content">
        <h3>Enter Verification Code</h3>
        <asp:TextBox ID="txtVerificationCode" runat="server" CssClass="input" Required="true" />
        <asp:Button ID="btnVerifyCode" runat="server" Text="Verify" OnClick="btnVerifyCode_Click" CssClass="button" />
        <asp:Label ID="lblVerificationMessage" runat="server" ForeColor="Red" />
    </div>
</div>
  </div>
 <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
    </div>
</asp:Content>