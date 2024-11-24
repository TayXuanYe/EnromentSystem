<%@ Page 
    Title="Take Attendent page"
    MasterPageFile="~/LecturerSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="LecturerTakeAttendentPage.aspx.cs" 
    Inherits="LecturerTakeAttendentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/lecturerTakeAttendentPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <h1>Class Attendance</h1>
        <table class = "lecturer-info-table">
            <tr>
                <td>Semester</td>
                <td>
                    <asp:Label ID="lblSemester" runat="server"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>Lecturer</td>
                <td>
                    <asp:Label ID="lblLecturer" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Course</td>
                <td>
                    <asp:Label ID="lblCourse" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Section</td>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="dropdownlist" 
                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                        AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Class Type</td>
                <td>
                    <asp:DropDownList ID="ddlClassType" runat="server" CssClass="dropdownlist"
                        OnSelectedIndexChanged="ddlClassType_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Class</td>
                <td>
                    <asp:DropDownList ID="ddlClassTime" runat="server" CssClass="dropdownlist"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Date</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="This field is requited"
                        CssClass="validator"
                        ControlToValidate="txtDate"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th colspan="2">
                    <asp:Button ID="btnGenerateQr" runat="server" Text="Generate QR Code" CssClass="generateQRbutton" OnClick="btnGenerateQr_Click"/>
                </th>
                <th colspan="2">
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="exitButton" OnClick="btnExit_Click" CausesValidation="false"/>
                </th>
            </tr>
            <tr>
                <th>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/QR.png" CssClass="QR"/>
                </th>
            </tr>
        </table>
        <asp:Panel ID="qrPart" runat="server">
            <h1>Attendent QR code</h1>
            <asp:Image ID="imgQrCode" runat="server"/>
        </asp:Panel>
    </div>
</asp:Content>


