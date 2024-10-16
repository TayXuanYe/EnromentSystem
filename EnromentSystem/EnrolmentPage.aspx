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
    
    <div class="body">
        <h1>Course Enrolment</h1>
        <table id="studentInfoPart">
            <tr>
                <td>
                    <p>Matriculation No</p>
                </td>
                <td>
                    <asp:Label ID="lblMatriculationNo" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <p>School</p>
                </td>
                <td>
                    <asp:Label ID="lblSchool" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>  
            
            <tr>
                <td>
                    <p>Student Name</p>
                </td>
                <td>
                    <asp:Label ID="lblStudentName" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <p>Level</p>
                </td>
                <td>
                    <asp:Label ID="lblLevel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p>IC/Passport No</p>
                </td>
                <td>
                    <asp:Label ID="lblIcPassportNo" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <p>Program</p>
                </td>
                <td>
                    <asp:Label ID="lblProgram" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p>Mode of Study</p>
                </td>
                <td>
                    <asp:Label ID="lblStudyMode" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <p>Major</p>
                </td>
                <td>
                    <asp:Label ID="lblMajor" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p>Session</p>
                </td>
                <td>
                    <asp:Label ID="lblSession" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="stdentCourseInfo">
            <tr>
                <td>
                    <h2>Previous Failed Course</h2>
                </td>
                <td>
                    <h2>Previous Compulsory Course - Not Taken</h2>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Table ID="tblPreviousFailedCourse" runat="server"></asp:Table>
                </td>
                <td>
                    <asp:Table ID="tblPreviousCompulsoryCourse" runat="server"></asp:Table>
                </td>
            </tr>
        </table>
        <div>
            <h2>Course Enrolled</h2>
            <asp:Table ID="tblCourseEnrolled" runat="server"></asp:Table>
             <asp:Button ID="btnCalculateFeeSummary" runat="server" Text="Fee Summary" />
        </div>
        <div class="footer-button">
            <asp:Button ID="btnEnrol" runat="server" Text="Enrol" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
    </div>
</div>
</form>
</body>
</html>
