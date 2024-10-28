<%@ Page 
    Title="Student Enrolment Page" 
    MasterPageFile="~/Site.master"
    Language="C#" AutoEventWireup="true" 
    CodeFile="EnrolmentPage.aspx.cs" 
    Inherits="EnrolmentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/enrolmentPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
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
                <asp:Table ID="tblPreviousFailedCourse" runat="server" CssClass="PreviousFailedCourse"></asp:Table>
            </td>
            <td>
                <asp:Table ID="tblPreviousCompulsoryCourse" runat="server" CssClass="PreviousCompulsoryCourse"></asp:Table>
            </td>
        </tr>
    </table>
    <div>
        <h2>Course Enrolled</h2>
        <asp:Table ID="tblCourseEnrolled" runat="server" CssClass="CourseEnrolled"></asp:Table>
            <asp:Button ID="btnCalculateFeeSummary" runat="server" Text="Fee Summary" />
    </div>
    <div class="footer-button">
        <asp:Button ID="btnEnrol" runat="server" Text="Enrol" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</div>
</asp:Content>
