<%@ Page 
    Title="Add New Course & Section Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminAddCourseAndSectionPage.aspx.cs" 
    Inherits="AdminAddCourseAndSectionPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminAddCourseAndSectionPage.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-contain">
        <table class="course-details-table">
            <tr>
                <th colspan="4">Course</th>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" 
                        OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"
                        AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>Major</td>
                <td>
                    <asp:DropDownList ID="ddlMajor" runat="server"
                        OnSelectedIndexChanged="ddlMajor_SelectedIndexChanged"
                        AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Course ID</td>
                <td>
                    <asp:TextBox ID="txtCourseId" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtCourseId"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator2" 
                        runat="server" 
                        ControlToValidate="txtCourseId"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Name not in requited format, example: PRG3201"
                        ValidationExpression="[A-Z]{2,}\d+"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ErrorMessage="This course id is already exits"
                        CssClass="validator"
                        ControlToValidate="txtCourseId"
                        Display="Dynamic"
                        OnServerValidate="CheckCourseIdIsExist_ServerValidate"></asp:CustomValidator>
                </td>
                <td>Course Name</td>
                <td>
                    <asp:TextBox ID="txtCourseName" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtCourseName"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        runat="server" 
                        ControlToValidate="txtCourseName"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Name not in requited format, only allow capital letter and space"
                        ValidationExpression="[A-Z][A-Z\s]+"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Credit Hours</td>
                <td>
                    <asp:DropDownList ID="ddlCreditHours" runat="server">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Price (RM)</td>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" TextMode="Number"></asp:TextBox><br />
                    <asp:RequiredFieldValidator runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtPrice"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div>
            <table class="section-details-table">
                <tr>
                    <td>Pre-requisite course</td>
                    <td>
                        <div>
                            <asp:DropDownList ID="ddlPrerequisite" runat="server"></asp:DropDownList>
                            <asp:ImageButton ID="btnAddPrerequisite" runat="server" 
                                OnClick="btnAddPrerequisite_Click" 
                                ImageUrl="~/Images/add.png" 
                                CausesValidation="false"/>
                        </div>
                    </td>
                
                </tr>
            </table>
        </div>
        <asp:GridView ID="gvPrerequisite" runat="server" 
            AutoGenerateColumns="false" 
            CssClass="grid-view-prerequisite"
            OnSelectedIndexChanged="gvPrerequisite_SelectedIndexChanged" 
            DataKeyNames="cid" 
            ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField HeaderText="Course ID" DataField="cid" SortExpression="cid"/>
                <asp:BoundField HeaderText="Course Name" DataField="name" SortExpression="name"/>

                <asp:CommandField ShowSelectButton="true" HeaderText="Delete" ButtonType="Button" SelectText=" "/>            
            </Columns>

            <EmptyDataTemplate>
                <p class="center">No pre-requisite course</p>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>

    <h2>Section</h2>
    <div class="table-contain">
        <table class="section-details-table">
            <tr>
                <td>Section Name</td>
                <td>
                    <div>
                        <asp:TextBox ID="txtSectionName" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="btnAddSection" runat="server" 
                            OnClick="btnAddSection_Click" 
                            ImageUrl="~/Images/add.png" 
                            ValidationGroup="section"/>
                    </div>
                    <asp:RegularExpressionValidator runat="server" 
                        ErrorMessage="This field only accept capital alphabet and number"
                        CssClass="validator"
                        ControlToValidate="txtSectionName"
                        Display="Dynamic"
                        ValidationExpression="[A-Z][A-Z\d]+"
                        ValidationGroup="section"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" 
                        ErrorMessage="This field is require"
                        CssClass="validator"
                        ControlToValidate="txtSectionName"
                        Display="Dynamic"
                        ValidationGroup="section"></asp:RequiredFieldValidator>
                </td>
                
            </tr>
        </table>
    </div>
    <asp:GridView ID="gvSectionInfo" runat="server" 
        AutoGenerateColumns="false" 
        CssClass="grid-view"
        OnSelectedIndexChanged="gvSectionInfo_SelectedIndexChanged" 
        OnRowCommand="gvSectionInfo_RowCommand"
        DataKeyNames="name" 
        ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:BoundField HeaderText="Section Name" DataField="name" SortExpression="name"/>
            <asp:TemplateField HeaderText="Show Time Table">
                <ItemTemplate>
                    <asp:ImageButton 
                        ID="btnView" 
                        runat="server" 
                        ImageUrl="~/Images/send.png"
                        CommandName="view"
                        CommandArgument='<%# Eval("name") %>'
                        ToolTip="Click to edit course details"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowSelectButton="true" HeaderText="Delete" ButtonType="Button" SelectText=" "/>            
        </Columns>

        <EmptyDataTemplate>
            <p class="center">No section</p>
        </EmptyDataTemplate>
    </asp:GridView>

    <div class="button-container">
        <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Add Successful</h1>
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/successful.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="classWindows" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <div class="class-details-table-contain">
                <table class="class-details-table">
                    <tr>
                        <td>Section Name</td>
                        <td><asp:Label ID="lblClassWindowsSectionName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>Class Type</th>
                    </tr>
                    <tr>
                        <td>Lecture Class Lecturer Name</td>
                        <td>
                            <asp:DropDownList ID="ddlLectureClassLecturer" runat="server"
                                OnSelectedIndexChanged="ddlLectureClassLecturer_SelectedIndexChanged"
                                AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnAddLectureClass" runat="server" 
                                ValidationGroup="addLecturerClass"
                                OnClick="btnAddLectureClass_Click"
                                ImageUrl="~/Images/edit.png"
                                CssClass="add-class-button"/>
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Label ID="lblWarningLectureClassLecturer" runat="server" 
                                CssClass="warning-label"
                                Text="Pls select lecturer before add class"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Practical Class Lecturer Name</td>
                        <td>
                            <asp:DropDownList ID="ddlPracticalClassLecturer" runat="server"
                                OnSelectedIndexChanged="ddlPracticalClassLecturer_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnAddPracticalClass" runat="server" 
                                ValidationGroup="addPracticalClass"
                                OnClick="btnAddPracticalClass_Click"
                                ImageUrl="~/Images/edit.png"
                                CssClass="add-class-button"/>
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Label ID="lblWarningPracticalClassLecturer" runat="server" 
                                CssClass="warning-label"
                                Text="Pls select lecturer before add class"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="class-timetable-display-contain">
                <asp:GridView ID="gvClassTimetable" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Day" DataField="day" SortExpression="day"/>     
                        <asp:BoundField HeaderText="0800" DataField="0800" SortExpression="0800"/>     
                        <asp:BoundField HeaderText="0900" DataField="0900" SortExpression="0900"/>     
                        <asp:BoundField HeaderText="1000" DataField="1000" SortExpression="1000"/>     
                        <asp:BoundField HeaderText="1100" DataField="1100" SortExpression="1100"/>     
                        <asp:BoundField HeaderText="1200" DataField="1200" SortExpression="1200"/>     
                        <asp:BoundField HeaderText="1300" DataField="1300" SortExpression="1300"/>     
                        <asp:BoundField HeaderText="1400" DataField="1400" SortExpression="1400"/>     
                        <asp:BoundField HeaderText="1500" DataField="1500" SortExpression="1500"/>     
                        <asp:BoundField HeaderText="1600" DataField="1600" SortExpression="1600"/>     
                        <asp:BoundField HeaderText="1700" DataField="1700" SortExpression="1700"/>     
                        <asp:BoundField HeaderText="1800" DataField="1800" SortExpression="1800"/>     
                        <asp:BoundField HeaderText="1900" DataField="1900" SortExpression="1900"/>     
                        <asp:BoundField HeaderText="2000" DataField="2000" SortExpression="2000"/>     
                        <asp:BoundField HeaderText="2100" DataField="2100" SortExpression="2100"/>     
                        <asp:BoundField HeaderText="2200" DataField="2200" SortExpression="2200"/>          
                    </Columns>
                </asp:GridView>
            </div>
            <div class="warning-contain">
                <asp:Label ID="lblWarningNoClassAdded" runat="server" 
                CssClass="warning-label"
                Text="Class are requited!"></asp:Label>
            </div>
            <div class="button-container">
                <asp:Button runat="server" Text="Add" OnClick="btnAddClass_Click" ValidationGroup="class-windows"/>
                <asp:Button runat="server" Text="Exit" OnClick="btnCancelAddClass_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="selectClassWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <asp:Label ID="lblSelectingClass" runat="server" CssClass="lblSelectingClass"></asp:Label>
            <div class="class-timetable-display-contain">
                <asp:GridView ID="gvSelectClass" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Day" DataField="day" SortExpression="day"/>

                        <asp:TemplateField HeaderText="0800">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect08" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="0900">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect09" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="1000">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect10" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="1100">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect11" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="1200">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect12" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="1300">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect13" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="1400">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect14" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="1500">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect15" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="1600">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect16" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="1700">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect17" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="1800">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect18" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="1900">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect19" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="2000">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect20" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="2100">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect21" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                        <asp:TemplateField HeaderText="2200">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect22" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
      
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoTimeSelected" runat="server" Text="Pls select atleast one time" CssClass="warning-label"></asp:Label>
            </div>
            <div class="button-container">
                <asp:Button runat="server" Text="Continue" OnClick="btnContinueSelectClass_Click" CausesValidation="false"/>
                <asp:Button runat="server" Text="Cancel" OnClick="btnCancelSelectClass_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="assignClassroomWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <asp:Label ID="lblAssigningClassType" runat="server" CssClass="lblSelectingClass"></asp:Label>
            <div class="assign-classroom-contain">
                <asp:GridView ID="gvAssignClassroom" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Day & Time" DataField="timeDay" SortExpression="timeDay"/>

                        <asp:TemplateField HeaderText="Class room">
                            <ItemTemplate>
                                <asp:TextBox ID="txtClassRoomName" runat="server" ValidationGroup="assignClassroom"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ErrorMessage="This field is require"
                                    CssClass="validator"
                                    ControlToValidate="txtClassRoomName"
                                    Display="Dynamic"
                                    ValidationGroup="assignClassroom"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator2" 
                                    runat="server" 
                                    ControlToValidate="txtClassRoomName"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accept capital letter, number and space"
                                    ValidationExpression="[A-Z][A-Z\d\s]+"
                                    ValidationGroup="assignClassroom"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                    ErrorMessage="This class have been use in this time"
                                    CssClass="validator"
                                    ControlToValidate="txtClassRoomName"
                                    Display="Dynamic"
                                    OnServerValidate="CheckClassIsUsed_ServerValidate"
                                    ValidationGroup="assignClassroom"></asp:CustomValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="timeIndex"/>     
                    </Columns>
                </asp:GridView>
            </div>
            <div class="button-container">
                <asp:Button runat="server" Text="Save" OnClick="btnSaveAssignClassroom_Click" ValidationGroup="assignClassroom"/>
                <asp:Button runat="server" Text="Cancel" OnClick="btnCancelAssignClassroom_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>
    
    
</asp:Content>

