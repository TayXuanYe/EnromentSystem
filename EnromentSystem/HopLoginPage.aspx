<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HopLoginPage.aspx.cs" Inherits="HopLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin login page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/HOPloginPage.css") %>" />
</head>
<body>
    <div class="login-container">
        <form id="form1" runat="server">
            <div class="form-field">
                <label for="HOPID">HOP ID:</label>
                <asp:TextBox ID="HOPID" runat="server" CssClass="form-control textBox"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    ControlToValidate="HOPID"
                    ForeColor="red"
                    Display="dynamic"
                    CssClass="validator"
                    runat="server" 
                    ErrorMessage="Please enter your HOP ID">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator2" 
                    runat="server" 
                    ControlToValidate="HOPID"
                    ForeColor="red"
                    Display="dynamic"
                    CssClass="validator"
                    ErrorMessage="HOP ID not in required format"
                    ValidationExpression="[Hh]\d{8}">
                </asp:RegularExpressionValidator>
            </div>
            <div class="form-field">
                <label for="HOPPassword">Password:</label>
                <asp:TextBox ID="HOPPassword" runat="server" CssClass="form-control textBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator3" 
                    ControlToValidate="HOPPassword"
                    ForeColor="red"
                    Display="dynamic"
                    CssClass="validator"
                    runat="server" 
                    ErrorMessage="Please enter your password">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator 
                    ID="cvdLoginFall" 
                    ForeColor="red"
                    Display="dynamic"
                    runat="server" 
                    ErrorMessage="Your Password is incorrect, please re-enter"
                    CssClass="validator"
                    OnServerValidate="LoginFall">
                </asp:CustomValidator><br />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Login to HOP Review Page" OnClick="Login_Click" CssClass="aspButton" />
        </form>
    </div>
</body>
</html>



