<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentLoginPage.aspx.cs" Inherits="StudentLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentLogin.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <div>
                <asp:Image runat="server" ImageUrl="~/Images/INTI_IU_logo.png"/>
            </div>
            <div id="infoBox">
                <p>User ID</p>
                <div>
                    <asp:TextBox ID="txtUserId" runat="server" CssClass="textBox"></asp:TextBox>
                </div>
                <p>Password</p>
                <div>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                    <a href="#">Forget Password?</a>
                    <asp:CustomValidator 
                        ID="cvdLoginFall" 
                        ForeColor="red"
                        Dispaly="dynamic"
                        runat="server" 
                        ErrorMessage="CustomValidator"
                        CssClass="validator"
                        ClientValidationFunction=""
                        OnServerValidate="cvdLoginFall_ServerValidate">
                    </asp:CustomValidator>
                </div>
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login to Online Enrolment Portal" />

            <div class="terms">
                <p>
                    By signing onto this website, you agree to abide by its 
                    <span><a href="#">Terms of Use</a></span>.<br />
                    Violations could lead to restriction of website privileges and/or disciplinary action.
                </p>
            </div>
        </div>
    </form>
</body>
</html>
