<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CourseAddAndDropPage.aspx.cs" Inherits="CourseAddAndDropPage" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveUrl("~/Styles/CourseAddandDrop.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="body">
    <h1>Course Add & Drop</h1>
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

    <div>
        <h2>Current Enrolled Course</h2>
        <asp:Table ID="tblCurrentEnrolledCourse" runat="server" CssClass="CourseEnrolled"></asp:Table>
        <asp:Button ID="btnAddEnrolledCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click"/>
    </div>
    
    <div>
        <h2>Request Changes:</h2>
        <asp:Table ID="tblRequestChanges" runat="server" CssClass="CourseEnrolled"></asp:Table>
    </div>
    <div class="footer-button">
        <asp:Button ID="btnRequestToApprove" runat="server" Text="Request For Approval" OnClick="btnRequestToApprove_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel"  OnClick="btnCancel_Click"/>
    </div>
</div>

<asp:Panel ID="selectOperationpopUpWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <div class="label-operating-course-id">
            <span >Operating course: </span>
            <asp:Label ID="lblCourseId" runat="server" Text="" ></asp:Label><br />
        </div>
        <div class="operation-button-contain">
            <asp:ImageButton 
                ID="btnDropCourse" 
                runat="server" 
                CssClass="operationButton" 
                OnClick="btnDropCourse_Click"
                ImageUrl="~/Images/delete.png"/>
            <span class="iamge-btn-text">Drop Course</span>
        </div>
        <div class="operation-button-contain">
            <asp:ImageButton 
                ID="btnChangeSection" 
                runat="server" 
                CssClass="operationButton" 
                OnClick="btnChangeSection_Click"
                ImageUrl="~/Images/change.png"/>
            <span class="iamge-btn-text">Change Course</span>
        </div><br />
        <asp:Button ID="btnExitOperatingWindoiw" runat="server" Text="Exit" OnClick="btnExitOperatingWindoiw_Click"/><br />
    </div>
</asp:Panel>

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
        <h2>Reason</h2>
        <asp:TextBox ID="txtAddCourseReason" runat="server" TextMode="MultiLine" CssClass="text-area"></asp:TextBox><br />
        <asp:RequiredFieldValidator 
            ID="rfvAddCourseReason" 
            runat="server" 
            Display="Dynamic"
            ErrorMessage="This field are requited"
            ControlToValidate="txtAddCourseReason"
            CssClass="validator"
            ForeColor="Red"
            ValidationGroup="addCoursePopUpWindow">
        </asp:RequiredFieldValidator>
        <asp:CustomValidator 
            ID="csvAddCourseReasonLength" 
            runat="server" 
            Display="Dynamic"
            ControlToValidate="txtAddCourseReason"
            CssClass="validator"
            ErrorMessage="The length of input should not more than 1000"
            OnServerValidate="OperationReasonLengthValidate"
            ValidationGroup="addCoursePopUpWindow">
        </asp:CustomValidator><br />

        <asp:Button 
            ID="btnAddCourse" 
            runat="server" 
            Text="Add Course" 
            OnClick="btnAddCourse_Click1" 
            ValidationGroup="addCoursePopUpWindow"/>
        <asp:Button ID="btnExitAddCourseWindow" runat="server" Text="Exit" OnClick="btnExitAddCourseWindow_Click"/>
    </div>

</asp:Panel>

<asp:Panel ID="dropCoursePopUpWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <h2>Course Code : <asp:Label ID="lblDropingCourse" runat="server" Text=""></asp:Label></h2>

        <h2>Reason</h2>
        <asp:TextBox ID="txtDropCourseReason" runat="server" TextMode="MultiLine" CssClass="text-area"></asp:TextBox><br />
        <asp:RequiredFieldValidator 
            ID="rfvDropCourseReason" 
            runat="server" 
            Display="Dynamic"
            ErrorMessage="This field are requited"
            ControlToValidate="txtDropCourseReason"
            CssClass="validator"
            ForeColor="Red"
            ValidationGroup="dropCoursepopUpWindow">
        </asp:RequiredFieldValidator>
        <asp:CustomValidator 
            ID="CustomValidator1" 
            runat="server" 
            Display="Dynamic"
            ControlToValidate="txtDropCourseReason"
            CssClass="validator"
            ErrorMessage="The length of input should not more than 1000"
            OnServerValidate="OperationReasonLengthValidate"
            ValidationGroup="dropCoursepopUpWindow">
        </asp:CustomValidator><br />

        <asp:Button 
            ID="btnDropCourseApply" 
            runat="server" 
            Text="Drop Course" 
            OnClick="btnDropCourseApply_Click" 
            ValidationGroup="dropCoursepopUpWindow"/>
        <asp:Button ID="btnExitDropCourseWindow" runat="server" Text="Exit" OnClick="btnExitDropCourseWindow_Click"/>
    </div>

</asp:Panel>

<asp:Panel ID="changeSectionPopUpWindow" runat="server" CssClass="pop-up-windows">
    <asp:Label ID="lblCurrentSectionId" runat="server" Text="" Width="0px"></asp:Label>
    <div class="windows-contain">
        <h2>Course Code : <asp:Label ID="lblChangingSectionCourse" runat="server" Text=""></asp:Label></h2>
        <h2>Current Section : <asp:Label ID="lblCurrentSectionName" runat="server" Text=""></asp:Label></h2><br />
        <h2>Target Section</h2>
        <asp:DropDownList ID="ddlTargetChangeSection" runat="server" CssClass="dropDownList">
        </asp:DropDownList><br />
        <asp:Label ID="lblChangeSectionErrorMessage" runat="server" Text="" CssClass="validator"></asp:Label>
        <h2>Reason</h2>
        <asp:TextBox ID="txtChangeSectionReason" runat="server" TextMode="MultiLine" CssClass="text-area"></asp:TextBox><br />
        <asp:RequiredFieldValidator 
            ID="RequiredFieldValidator1" 
            runat="server" 
            Display="Dynamic"
            ErrorMessage="This field are requited"
            ControlToValidate="txtChangeSectionReason"
            CssClass="validator"
            ForeColor="Red"
            ValidationGroup="ddlTargetChangeSection">
        </asp:RequiredFieldValidator>
        <asp:CustomValidator 
            ID="CustomValidator2" 
            runat="server" 
            Display="Dynamic"
            ControlToValidate="txtChangeSectionReason"
            CssClass="validator"
            ErrorMessage="The length of input should not more than 1000"
            OnServerValidate="OperationReasonLengthValidate"
            ValidationGroup="ddlTargetChangeSection">
        </asp:CustomValidator><br />
        <asp:Button 
            ID="btnChangeSectionApply" 
            runat="server" 
            Text="Change Section" 
            OnClick="btnChangeSectionApply_Click" 
            ValidationGroup="ddlTargetChangeSection"/>
        <asp:Button ID="btnExitChangeSectionWindow" runat="server" Text="Exit" OnClick="btnExitChangeSectionWindow_Click"/>
    </div>
</asp:Panel>


<asp:Panel ID="notOpenPopUpWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <br />
        <h1>Add and Drop are not available</h1>
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/not-available.png" CssClass="not-available-image"/><br />
        <asp:Button ID="notOpenPopUpWindowExitButton" runat="server" Text="Exit" OnClick="btnCancel_Click"/>
    </div>
</asp:Panel>
    
<asp:Panel ID="haventEnrolWaringWindow" runat="server" CssClass="pop-up-windows">
    <div class="windows-contain">
        <br />
        <h1>Add and Drop are not available.<br /> You must enrol course first.</h1>
        <br />
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/not-available.png" CssClass="not-available-image"/><br />
        <asp:Button ID="haventEnrolWaringWindowExitButton" runat="server" Text="Exit" OnClick="btnCancel_Click"/>
    </div>
</asp:Panel>

    <script src="Scripts/courseAddAndDrop.js" type="text/javascript"></script>
</asp:Content>