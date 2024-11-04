<%@ Page 
    Title="Student Enrolment Page" 
    MasterPageFile="~/Site.master"
    Language="C#" AutoEventWireup="true" 
    CodeFile="EnrolmentPage.aspx.cs" 
    Inherits="EnrolmentPage" %>
<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


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
        <asp:Button ID="btnAddEnrolledCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click"/>
    </div>
    <div>
        <h2>Fee Summary</h2>
        <asp:Table ID="tblFeeSummary" runat="server" CssClass="FeeSummary"></asp:Table>
    </div>
    <div class="footer-button">
        <asp:Button ID="btnEnrol" runat="server" Text="Enrol" OnClick="btnEnrol_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel"  OnClick="btnCancel_Click"/>
    </div>
</div>


<asp:Panel ID="addCoursePopUpWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <h2>Course Code Listing</h2>
        <asp:DropDownList 
            ID="ddlCourseCodeListing" runat="server"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlCourseCodeListing_SelectedIndexChanged"
            CssClass="dropDownList"></asp:DropDownList>
        <h2>Course Section</h2>
        <asp:DropDownList ID="ddlCourseSection" runat="server" CssClass="dropDownList">
        </asp:DropDownList>
        <h2>Pre Requisite Course</h2>
        <asp:Table ID="tblPreRequisite" runat="server" CssClass="PreRequisiteCourse"></asp:Table>
        <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="validator"></asp:Label>
        <asp:Button 
            ID="btnAddCourse" 
            runat="server" 
            Text="Add Course" 
            OnClick="btnAddCourse_Click1" 
            ValidationGroup="popUpWindows"/>
        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click"/>
    </div>
</asp:Panel>

<asp:Panel ID="notOpenPopUpWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <br />
        <h1>Course Enrolment are not available</h1>
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/not-available.png" CssClass="not-available-image"/><br />
        <asp:Button ID="notOpenPopUpWindowExitButton" runat="server" Text="Exit" OnClick="btnCancel_Click"/>
    </div>
</asp:Panel>
    
<asp:Panel ID="multipleEnrolWaringWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <br />
        <h1>You have enrol course in this semester</h1>
        <br />
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/not-available.png" CssClass="not-available-image"/><br />
        <asp:Button ID="multipleEnrolWaringWindowExitButton" runat="server" Text="Exit" OnClick="btnCancel_Click"/>
    </div>
</asp:Panel>
</asp:Content>
