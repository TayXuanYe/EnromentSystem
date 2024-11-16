<%@ Page 
    Title="Add Student Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminAddStudentPage.aspx.cs" 
    Inherits="AdminAddStudentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminAddStudentPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-contain">
        <!--student details-->
        <table class="student-details-table">
            <tr>
                <th colspan="4">Student Details</th>
            </tr>
            <tr>
                <td>Student Name</td>
                <td>
                    <asp:TextBox ID="txtStudentName" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtStudentName"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td>Passport / NRIC</td>
                <td>
                    <asp:TextBox ID="txtPassportOrIC" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtPassportOrIC"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Date of Birth</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" onfocus="ShowCalendar()" TextMode="Date"/><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtDate"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td>Phone number</td>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtPhoneNumber"
                        Display="Dynamic"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" 
                        ControlToValidate="txtPhoneNumber"
                        ErrorMessage="Phone number format not correct<br> Format: [country code]-XXX-XXX-XXX"
                        ForeColor="Red"
                        ValidationExpression="\+\d{1,3}[-]\d{1,4}[-]\d{1,4}[-]\d{1,9}">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>

        <!--student address-->
        <table class="student-details-table">
            <tr>
                <th colspan="4">Student Address</th>
            </tr>
            <tr>
                <td>Address</td>
                <td>
                    <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtPermanentAddress"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td>Postcode</td>
                <td>
                    <asp:TextBox ID="txtPermanentPostcode" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtPermanentPostcode"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>City</td>
                <td>
                    <asp:TextBox ID="txtPermanentCity" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtPermanentCity"
                        Display="Dynamic"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="This field only accept alphabet"
                        CssClass="validator"
                        ControlToValidate="txtPermanentCity"
                        Display="Dynamic"
                        ValidationExpression="[A-Za-z][A-Za-z\s]+"></asp:RegularExpressionValidator>
                </td>
                <td>State</td>
                <td>
                    <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtState"
                        Display="Dynamic"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ErrorMessage="This field only accept alphabet"
                        CssClass="validator"
                        ControlToValidate="txtState"
                        Display="Dynamic"
                        ValidationExpression="[A-Za-z][A-Za-z\s]+"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Country</td>
                <td>
                    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtCountry"
                        Display="Dynamic"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ErrorMessage="This field only accept alphabet"
                        CssClass="validator"
                        ControlToValidate="txtCountry"
                        Display="Dynamic"
                        ValidationExpression="[A-Za-z][A-Za-z\s]+"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>

        <!--Admission information-->
        <table class="student-details-table">
            <tr>
                <th colspan="4">Admission information</th>
            </tr>
            <tr>
                <td>Student ID</td>
                <td>
                    <asp:TextBox ID="txtStudentId" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtStudentId"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server" 
                        ControlToValidate="txtStudentId"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Student ID not in required format"
                        ValidationExpression="[Ii]\d{8}"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ErrorMessage="This student ID is already exits"
                        CssClass="validator"
                        ControlToValidate="txtStudentId"
                        Display="Dynamic"
                        OnServerValidate="CheckStudentIdIsExist_ServerValidate"></asp:CustomValidator>
                </td>
                <td>Mode of Study</td>
                <td>
                    <asp:DropDownList ID="ddlModeOfStudy" runat="server">
                        <asp:ListItem Text="Full-time Learning" Value="Full-time Learning"></asp:ListItem>
                        <asp:ListItem Text="Part-time Learning" Value="Part-time Learning"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>School</td>
                <td>
                    <asp:DropDownList ID="ddlSchool" runat="server" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Foundation" Value="Foundation"></asp:ListItem>
                        <asp:ListItem Text="Certificate" Value="Certificate"></asp:ListItem>
                        <asp:ListItem Text="Diploma" Value="Diploma"></asp:ListItem>
                        <asp:ListItem Text="Degree" Value="Degree"></asp:ListItem>
                        <asp:ListItem Text="Master" Value="Master"></asp:ListItem>
                        <asp:ListItem Text="PhD" Value="PhD"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <br />
                    <asp:CustomValidator ID="CustomValidator3" runat="server" 
                        ErrorMessage="No program can be select, pls change level or school"
                        CssClass="validator"
                        Display="Dynamic"
                        OnServerValidate="ProgramIsEmpty_ServerValidate"></asp:CustomValidator>
                </td>
                <td>Major</td>
                <td>
                    <asp:DropDownList ID="ddlMajor" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Student Email</td>
                <td>
                    <asp:TextBox ID="txtStudentEmail" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtStudentEmail"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator3" 
                        runat="server" 
                        ControlToValidate="txtStudentEmail"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Email not in required format"
                        ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ErrorMessage="This student ID is already exits"
                        CssClass="validator"
                        ControlToValidate="txtStudentEmail"
                        Display="Dynamic"
                        OnServerValidate="CheckStudentEmailIsExist_ServerValidate"></asp:CustomValidator>
                </td>
                <td>Scholarship (%)</td>
                <td>
                    <asp:TextBox ID="txtScholarship" runat="server" TextMode="Number" min="0" max="100" Text="0"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtScholarship"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ErrorMessage="The range of scholarship should between 0 to 100"
                        CssClass="validator"
                        ControlToValidate="txtScholarship"
                        Display="Dynamic"
                        MinimumValue="0"
                        MaximumValue="100"
                        Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
        </table>
    </div>

    <div class="button-container">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Add Student Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
