<%@ Page 
    Title="Modify Student Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminModifyStudentPage.aspx.cs" 
    Inherits="AdminModifyStudentPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminModifyStudentPage.css") %>" />
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
            </tr>
            <tr>
                <td>Student ID</td>
                <td>
                    <asp:Label ID="lblId" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <!--Admission information-->
        <table class="student-details-table">
            <tr>
                <th colspan="4">Admission information</th>
            </tr>
            <tr>
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
        </table>
    </div>

    <div class="button-container">
        <asp:Button ID="btnAdd" runat="server" Text="Update" OnClick="btnAdd_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="confirmationWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h2>Confirm to update the following data:</h2>
            <table class="student-confirm-details-table">
                <tr>
                    <th colspan="2">Current Information</th>
                    <th></th>
                    <th></th>
                    <th colspan="2">Update Information</th>
                </tr>
                <tr>
                    <td>Scholarship (%)</td>
                    <td>
                        <asp:Label ID="lblCurrenScholarship" runat="server"></asp:Label>
                    </td>
                    <th></th>
                    <th></th>
                    <td>Scholarship (%)</td>
                    <td>
                        <asp:Label ID="lblUpdateScholarship" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Mode of Study</td>
                    <td>
                        <asp:Label ID="lblCurrenStudyMode" runat="server"></asp:Label>                        
                    </td>                    
                    <th></th>
                    <th></th>
                    <td>Mode of Study</td>
                    <td>
                        <asp:Label ID="lblUpdateStudyMode" runat="server"></asp:Label>                        
                    </td>
                </tr>
                <tr>
                    <td>School</td>
                    <td>
                        <asp:Label ID="lblCurrenSchool" runat="server"></asp:Label>                        
                    </td>
                    <th></th>
                    <th></th>
                    <td>School</td>
                    <td>
                        <asp:Label ID="lblUpdateSchool" runat="server"></asp:Label>                        
                    </td>
                </tr>
                <tr>
                    <td>Level</td>
                    <td>
                        <asp:Label ID="lblCurrenLevel" runat="server"></asp:Label>                                                
                    </td>
                    <th></th>
                    <th></th>
                    <td>Level</td>
                    <td>
                        <asp:Label ID="lblUpdateLevel" runat="server"></asp:Label>                                                
                    </td>
                </tr>
                <tr>
                    <td>Program</td>
                    <td>
                        <asp:Label ID="lblCurrenProgram" runat="server"></asp:Label>                                                
                    </td>
                    <th></th>
                    <th></th>
                    <td>Program</td>
                    <td>
                        <asp:Label ID="lblUpdateProgram" runat="server"></asp:Label>                                                
                    </td>
                </tr>
                <tr>
                    <td>Major</td>
                    <td>
                        <asp:Label ID="lblCurrenMajor" runat="server"></asp:Label>                                                
                    </td>
                    <th></th>
                    <th></th>
                    <td>Major</td>
                    <td>
                        <asp:Label ID="lblUpdateMajor" runat="server"></asp:Label>                                                
                    </td>
                </tr>
            </table>
            
            <div class="button-container">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
                <asp:Button ID="btnCancelUpdate" runat="server" Text="Cancel" OnClick="btnCancelUpdate_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Update Student Details Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
