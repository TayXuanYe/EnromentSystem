<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentHomePage.aspx.cs" Inherits="StudentHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/home.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
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
    </div>
    <h1>Welcome to Online Enrolment Portal!</h1>
    <div class="card-container">
        <!-- Enrolment -->
        <div class="card" id="cardEnrolment">
            <div class="card-header">
                <span class="card-title">Enrolment</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="#">Course Enrolment</a></li>
                </ul>
            </div>
        </div>
        <!-- Add and Drop -->
        <div class="card" id="cardAddDrop">
            <div class="card-header">
                <span class="card-title">Add & Drop</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="#">Course Add & Drop</a></li>
                    <li><a href="#">Add & Drop History</a></li>
                </ul>
            </div>
        </div>
        <!-- Enquiry -->
        <div class="card" id="cardEnquiry">
            <div class="card-header">
                <span class="card-title">Enquiry</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="#">Contact Us</a></li>
                    <li><a href="#">Timetable Matching</a></li>
                    <li><a href="#">Student Evaluation of Teaching</a></li>
                </ul>
            </div>
        </div>
        <!--Statement-->
        <div class="card" id="cardStatement">
            <div class="card-header">
                <span class="card-title">Statement</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="#">Student Statement</a></li>
                    <li><a href="#">Registration Summary / Class Timetable</a></li>
                </ul>
            </div>
        </div>
        <!--Payment-->
        <div class="card" id="cardPayment">
            <div class="card-header">
                <span class="card-title">Payment</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="#">Payment</a></li>
                    <li><a href="#">Online Payment History / Receipt</a></li>
                    <li><a href="#">Invoice and Adjustment Note</a></li>
                </ul>
            </div>
        </div>
        <!--Account-->
        <div class="card" id="cardAccount">
            <div class="card-header">
                <span class="card-title">Account</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="#">Change Password</a></li>
                    <li><a href="#">Update Profile</a></li>
                    <li><a href="#">Update Bank Details</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
</form>
</body>
</html>
