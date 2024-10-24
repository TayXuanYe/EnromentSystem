﻿<%@ Page 
    Title="Change Password Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentChangePasswordPage.aspx.cs" 
    Inherits="StudentChangePasswordPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentChangePasswordPage.css") %>" />
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
                    CssClass="validator">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator4" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="CustomValidator"
                    CssClass="validator"
                    ClientValidationFunction=""
                    OnServerValidate="PasswordFormat_ServerValidate">
                </asp:CustomValidator>
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
                    CssClass="validator">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator 
                    ID="CustomValidator5" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="CustomValidator"
                    CssClass="validator"
                    ClientValidationFunction=""
                    OnServerValidate="PasswordFormat_ServerValidate">
                </asp:CustomValidator>
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
                    CssClass="validator">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator6" 
                    runat="server" 
                    Display="Dynamic"
                    ForeColor="Red"
                    ErrorMessage="CustomValidator"
                    CssClass="validator"
                    ClientValidationFunction=""
                    OnServerValidate="PasswordFormat_ServerValidate">
                </asp:CustomValidator>
            </div>
            
            <div class="button">
                <asp:Button ID="Button1" runat="server" Text="Update Password" CssClass="button"/>
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="button"/>
            </div>
        </div>
    </div>
</div>
</asp:Content>