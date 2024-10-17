<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentChangePasswordPage.aspx.cs" Inherits="StudentChangePasswordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentUpdateProfilePage.css") %>" />
</head>
<body>
<form id="form1" runat="server">
<div class="box">
    <div class="header">
    <div id="intiLogo"></div>
    <div id="identityCard">
        <asp:Button ID="btnHomeButton" runat="server" Text="" />
        <div id="studentDetails">
            <asp:Label runat="server" Text="Tay Xuan Ye<br>I23024312<br>SCSI - BACHELOR OF COMPUTER SCIENCE (HONS)"></asp:Label>
        </div>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" />
    </div>

    <div class="navigation-bar">
    <table>
    <tr>
    <td style=" text-align:left;">
        <ul id="nav-one" class="dropmenu">
            <!--Home-->
            <li>
                <p><a href="#">Home</a></p>
            </li>

            <!--Enrolment-->
            <li> 
 	                <p>Enrolment</p>
                <ul>
                    <li><a href="#">Course Enrolment</a></li>
                </ul>
            </li>

            <!--Add and Drop-->
            <li> 
 	                <p>Add & Drop</p>
                <ul>
                    <li><a href="#">Course Add / Drop</a></li>
                    <li><a href="#">Add / Drop History</a></li>
                </ul>
            </li>

            <!--Enquiry-->
            <li> 
 	                <p>Enquiry</p>
                <ul>
                    <li><a href="#">Timetable Matching</a></li>
                    <li><a href="#">Contact Us</a></li>
                </ul>
            </li>

            <!--Statement-->
            <li> 
 	                <p>Statement</p>
                <ul>
                    <li><a href="#">Student Statement</a></li>
                    <li><a href="#">Registration Summary</a></li>
                </ul>
            </li>

            <!--Payment-->
            <li> 
 	                <p>Payment</p>
                <ul>
                    <li><a href="#">Payment</a></li>
                    <li><a href="#">Online Payment History <br /> Receipt</a></li>
                    <li><a href="#">Invoice and Adjustment <br /> Note</a></li>
                </ul>
            </li>

            <!--Account-->
            <li> 
 	                <p>Account</p>
                <ul>
                    <li><a href="#">Change Password</a></li>
                    <li><a href="#">Update Profile</a></li>
                    <li><a href="#">Update Bank Details</a></li>
                </ul>
            </li>
    </ul>
    </td>
    </tr>
    </table>
    </div>
</div>
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
                    <asp:TextBox ID="txtExistingPassword" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ControlToValidate="txtExistingPassword"
                        ID="RequiredFieldValidator1" 
                        runat="server" 
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="This field is requited"
                        CssClass="validator">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator
                        ID="CustomValidator1" 
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
                    <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ControlToValidate="txtExistingPassword"
                        ID="RequiredFieldValidator2" 
                        runat="server" 
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="This field is requited"
                        CssClass="validator">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator 
                        ID="CustomValidator2" 
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
                    <asp:TextBox ID="txtConfirmNewPassword" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ControlToValidate="txtExistingPassword"
                        ID="RequiredFieldValidator3" 
                        runat="server" 
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="This field is requited"
                        CssClass="validator">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator
                        ID="CustomValidator3" 
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
                    <asp:Button ID="btnUpdatePassword" runat="server" Text="Update Password" CssClass="button"/>
                    <asp:Button ID="vtnCancel" runat="server" Text="Cancel" CssClass="button"/>
                </div>
            </div>
        </div>
    </div>
</div>
</form>
</body>
</html>
