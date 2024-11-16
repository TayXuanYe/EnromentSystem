<%@ Page 
    Title="Delete Student Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminDeleteStudentPage.aspx.cs" 
    Inherits="AdminDeleteStudentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminDeleteStudentPage.css") %>" />
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
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                <td>Passport / NRIC</td>
                <td>
                    <asp:Label ID="lblPassportIc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Date of Birth</td>
                <td>
                    <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
                </td>
                <td>Phone number</td>
                <td>
                    <asp:Label ID="lblPhoneNumber" runat="server"></asp:Label>
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
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                </td>
                <td>Postcode</td>
                <td>
                    <asp:Label ID="lblPostcode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>City</td>
                <td>
                    <asp:Label ID="lblCity" runat="server"></asp:Label>
                </td>
                <td>State</td>
                <td>
                    <asp:Label ID="lblState" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Country</td>
                <td>
                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
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
                    <asp:Label ID="lblStudentId" runat="server"></asp:Label>
                </td>
                <td>Mode of Study</td>
                <td>
                    <asp:Label ID="lblModeOfStudy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>School</td>
                <td>
                    <asp:Label ID="lblSchool" runat="server"></asp:Label>
                </td>
                <td>Level</td>
                <td>
                    <asp:Label ID="lblLevel" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:Label ID="lblProgram" runat="server"></asp:Label>
                </td>
                <td>Major</td>
                <td>
                    <asp:Label ID="lblMajor" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Student Email</td>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
                <td>Scholarship (%)</td>
                <td>
                    <asp:Label ID="lblScholarship" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Admission Date</td>
                <td>
                    <asp:Label ID="lblAdmissionDate" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <h1>Pleas check student details before delete</h1>
    <div class="button-container">
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Student Delete Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="failWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Student Delete Fail</h1>
            <br />
            <asp:Image runat="server" ImageUrl="~/Images/not-available.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>