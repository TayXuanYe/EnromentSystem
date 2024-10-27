<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentResetPasswordPage.aspx.cs" Inherits="StudentResetPasswordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/resetPasswordPage.css") %>" />
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
                        ID="rfvUserId" 
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
                        ValidationExpression="[Ii]\d{8}">
                    </asp:RegularExpressionValidator>
                </div>
                <p>New Password</p>
                <div>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="textBox" ViewStateMode="Enabled"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" 
                        ControlToValidate="txtNewPassword"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        runat="server" 
                        ErrorMessage="This field is requited">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server" 
                        ControlToValidate="txtNewPassword"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Password must be between 8 to 20 alphanumeric characters. Include at least one letter and one numeric digit. Symbols are not allowed."
                        ValidationExpression="(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}">
                    </asp:RegularExpressionValidator>
                </div>
                <p>Conform New Password</p>
                <div>
                    <asp:TextBox ID="txtConformNewPassword" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3" 
                        ControlToValidate="txtConformNewPassword"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        runat="server" 
                        ErrorMessage="This field is requited">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator 
                        ID="CompareValidator1" 
                        ControlToValidate="txtConformNewPassword"
                        ForeColor="red"
                        Display="dynamic"
                        ControlToCompare="txtNewPassword"
                        runat="server" 
                        ErrorMessage="The passwords entered do not match. <br>Please re-enter">
                    </asp:CompareValidator>
                </div>
                <p>Verification Code</p>
                <div>
                    <table class="verifcation-code-table">
                        <tr><td>
                            <asp:TextBox ID="txtVerifcationCode" runat="server" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button 
                                ID="btnSendVerificationCode" 
                                runat="server" 
                                OnClick="btnSendVerificationCode_Click"
                                CausesValidation="false"/>
                        </td></tr>
                    </table>
                    <asp:CustomValidator 
                        ID="cvdVerificationCodeMatch" 
                        ForeColor="red"
                        Display="dynamic"
                        runat="server" 
                        ErrorMessage="The Verification code not match"
                        CssClass="validator"
                        OnServerValidate="cvdVerificationCodeMatch_ServerValidate">
                    </asp:CustomValidator><br />
                </div>
            </div>
            <div class="button-bar">
                <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click"/>
                <asp:Button ID="btnExit" runat="server" Text="Exit" CausesValidation="false" OnClick="ExitPage"/>
            </div>
            <asp:Panel ID="successfulPopUpWindows" runat="server" CssClass="pop-up-windows">
                <div class="windows-contain">
                    <div class="checkmark-container">
                        <div class="checkmark">✔</div>
                    </div>
                    <h1>Password reset successful</h1>
                    <asp:Button 
                        ID="btnExitInPopUpWindows" 
                        runat="server" 
                        Text="Exit" 
                        OnClick="ExitPage"
                        CausesValidation="false"/>
                </div>
            </asp:Panel>

            <asp:Panel ID="failPopUpWindows" runat="server" CssClass="pop-up-windows">
                <div class="windows-contain-fail">
                    <div class="crosemark-container">
                        <div class="crosemark">✕</div>
                    </div>
                    <h1>Student ID not found</h1>
                    <asp:Button
                        ID="Button1" 
                        runat="server" 
                        Text="Exit" 
                        OnClick="ExitPage"
                        CausesValidation="false"
                        OnClientClick="document.getElementById('failPopUpWindows').style.display='none'; return false;" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
