<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentHomePage.aspx.cs" Inherits="StudentHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentHomePage.css") %>" />
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
                <asp:Label ID="lblStudentDetails" runat="server" Text=""></asp:Label>
            </div>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click"/>
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
                    <li><a href="EnrolmentPage.aspx">Course Enrolment</a></li>
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
                    <li><a href="CourseAddAndDropPage.aspx">Course Add & Drop</a></li>
                    <li><a href="AddDropHistoryPage.aspx">Add & Drop History</a></li>
                </ul>
            </div>
        </div>
        <!-- Attendance -->
        <div class="card" id="cardAttendance">
            <div class="card-header">
                <span class="card-title">Attendance</span>
            </div>
            <div class="card-content">
                <ul>
                    <li><a href="StudentTakeAttendentPage.aspx">Take Attendance</a></li>
                    <li><a href="StudentViewAttendentPage.aspx">Attendance</a></li>
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
                    <li><a href="StudentStatementPage.aspx">Student Statement</a></li>
                    <li><a href="StudentClassTimetablePage.aspx">Registration Summary / Class Timetable</a></li>
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
                    <li><a href="PaymentPage.aspx">Payment</a></li>
                    <li><a href="OnlinePaymentHistoryandReceiptpage.aspx">Online Payment History / Receipt</a></li>
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
                    <li><a href="StudentChangePasswordPage.aspx">Change Password</a></li>
                    <li><a href="StudentUpdateProfilePage.aspx">Update Profile</a></li>
                    <li><a href="StudentUpdateBankDetailsPage.aspx">Update Bank Details</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
</form>
</body>
</html>
