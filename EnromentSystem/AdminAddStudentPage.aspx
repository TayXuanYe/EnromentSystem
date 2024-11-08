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
                    <asp:DropDownList ID="ddlPermanentState" runat="server" CssClass="dropDownList"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Country</td>
                <td>
                    <asp:DropDownList ID="ddlPermanentCountry" runat="server" CssClass="dropDownList"></asp:DropDownList>
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
                        ControlToValidate="txtUserId"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Student ID not in required format"
                        ValidationExpression="[Ii]\d{8}"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ErrorMessage="This student ID is already exits"
                        CssClass="validator"
                        ControlToValidate="txtStudentId"
                        Display="Dynamic"
                        OnServerValidate="CheckStudentIdIsExist"></asp:CustomValidator>
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
                    <asp:DropDownList ID="ddlSchool" runat="server">
                        <asp:ListItem Text="GBL110 INFORMATION TECHNOLOGY" Value="GBL110 INFORMATION TECHNOLOGY"></asp:ListItem>
                        <asp:ListItem Text="Part-time Learning" Value="Part-time Learning"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
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
                    <asp:DropDownList ID="ddlProgram" runat="server"></asp:DropDownList>
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
                        ValidationExpression="[Ii]\d{8}"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ErrorMessage="This student ID is already exits"
                        CssClass="validator"
                        ControlToValidate="txtStudentEmail"
                        Display="Dynamic"
                        OnServerValidate="CheckStudentEmailIsExist"></asp:CustomValidator>
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
                        MaximumValue="100"></asp:RangeValidator>
                </td>
            </tr>
        </table>
    </div>

    <div class="button-container">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
    </div>
</asp:Content>
