<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnrolmentPage.aspx.cs" Inherits="EnrolmentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Enrolment Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/enrolmentPage.css") %>" />
</head>
<body>
<form id="form1" runat="server">
<div class ="box">
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
    
    <div></div>
</div>
</form>
</body>
</html>
