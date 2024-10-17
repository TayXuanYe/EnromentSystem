<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentUpdateBankDetailsPage.aspx.cs" Inherits="StudentUpdateBankDetailsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Bank Details Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentUpdateBankDetailsPage.css") %>" />
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
        <div>
            <h1>Update Bank Details Page</h1>

            <table>
                <thead>
				    <tr>
					    <th><h2>Bank Information</h2></th>
                    </tr>
                </thead>
                <tbody>
                    <tr><td>
                        <table class="bank-details">
                            <!--Bank name-->
                            <tr>
                                <td>
                                    <p>Bank Name</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                            </tr>
                            <!--Bank Account No.-->
                            <tr>
                                <td>
                                    <p>Bank Account No.</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankAccountNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <!--Bank Holder Name-->
                            <tr>
                                <td>
                                    <p>Bank Holder Name</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankHolderName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <!--Account verification documents-->
                            <tr>
                                <td>
                                    <p>Account Verification Documents</p>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fudAccountVerificationDocuments" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:Literal ID="lilDownloadFileLink" runat="server" Text="Download"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td></tr>
                    <tr><td class="footer-button">
                        <asp:Button ID="btnSave" runat="server" Text="Upload and Save Bank Details" />
                        <asp:Button ID="btnExit" runat="server" Text="Exit without Saving" />
                    </td></tr>
                </tbody>
			</table>
        </div>
    </div>
</div>
</form>
</body>
</html>
