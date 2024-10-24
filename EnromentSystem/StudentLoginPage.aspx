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
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator2" 
                        ControlToValidate="txtUserId"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        runat="server" 
                        ErrorMessage="This field is requited">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server" 
                        ControlToValidate="txtUserId"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Student ID not in required fromat"
                        ValidationExpression="I\d+">
                    </asp:RegularExpressionValidator>
                </div>
                <p>Password</p>
                <div>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" 
                        ControlToValidate="txtPassword"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        runat="server" 
                        ErrorMessage="This field is requited">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator 
                        ID="cvdLoginFall" 
                        ForeColor="red"
                        Display="dynamic"
                        runat="server" 
                        ErrorMessage="Incorrect password, please re-enter"
                        CssClass="validator"
                        OnServerValidate="cvdLoginFall_ServerValidate">
                    </asp:CustomValidator><br />
                    <a href="#">Forget Password?</a>
                </div>
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login to Online Enrolment Portal" OnClick="btnLogin_Click" />

            <div class="terms">
                <p>
                    By signing onto this website, you agree to abide by its 
                    <span><a href="#" id="termsOfUse">Terms of Use</a></span>.<br />
                    Violations could lead to restriction of website privileges and/or disciplinary action.
                </p>
            </div>

            <div id="popUpWindows" class="pop-up-windows">
                <div class="windows-contain">
                    <asp:Button ID="btnPopWindowsClose" runat="server" Text="Close" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
