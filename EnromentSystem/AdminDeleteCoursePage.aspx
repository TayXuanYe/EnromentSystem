<%@ Page 
    Title="Delete Course Page"
    MasterPageFile="~/AdminSite.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminDeleteCoursePage.aspx.cs" 
    Inherits="AdminDeleteCoursePage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/adminDeleteProgramPage.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-contain">
        <table class="program-details-table">
            <tr>
                <th colspan="4">Course</th>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:Label ID="lblProgram" runat="server" ></asp:Label>
                </td>
                <td>Major</td>
                <td>
                    <asp:Label ID="lblMajor" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Course ID</td>
                <td>
                    <asp:Label ID="lblCourseId" runat="server"></asp:Label>
                </td>
                <td>Course Name</td>
                <td>
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Credit Hours</td>
                <td>
                    <asp:Label ID="lblCreditHours" runat="server"></asp:Label>
                </td>
                <td>Price (RM)</td>
                <td>
                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Pre-requisite course</td>
            </tr>
            <tr>
                <asp:GridView ID="gvPrerequisite" runat="server" 
                    AutoGenerateColumns="false" 
                    CssClass="grid-view-prerequisite"
                    DataKeyNames="cid" 
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField HeaderText="Course ID" DataField="cid" SortExpression="cid"/>
                        <asp:BoundField HeaderText="Course Name" DataField="name" SortExpression="name"/>
                    </Columns>

                    <EmptyDataTemplate>
                        <p class="center">No pre-requisite course</p>
                    </EmptyDataTemplate>
                </asp:GridView>
            </tr>
        </table>
    </div>

    <h2>Section</h2>
    <div class="section-table-container">
        <asp:GridView ID="gvSectionInfo" runat="server" 
            AutoGenerateColumns="false" 
            CssClass="grid-view"
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
                            ToolTip="Click to edit course details"
                            CausesValidation="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <EmptyDataTemplate>
                <p class="center">No section</p>
            </EmptyDataTemplate>
        </asp:GridView>

    </div>

    <div class="button-container">
        <asp:Button ID="btnDeleteProgram" runat="server" Text="Delete Program" OnClick="btnDeleteProgram_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
    </div>

    <asp:Panel ID="successfulWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Delete Successful</h1>
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
            <h1>Delete Fail</h1>
            <br />
            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/not-available.png" CssClass="successful-image"/><br />
            <div class="button-container">
                <asp:Button runat="server" Text="Exit" OnClick="btnCancel_Click" CausesValidation="false"/>
            </div>
        </div>
    </asp:Panel>  
    
    <asp:Panel ID="conformWindow" runat="server" CssClass="pop-up-windows">
        <div class="windows-contain">
            <br />
            <h1>Conform Delete?</h1>
            <p>Once deleted, it cannot be recovered<br />All majors under it will also be deleted.</p>
            <br />
            <p>To delete this program pleas enter following word</p>
            <asp:Label ID="lblConformText" runat="server"></asp:Label>
            <asp:TextBox ID="txtConformText" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="ConformTextCustomValidator" runat="server" 
                ErrorMessage="Conform Text Not Match"
                CssClass="validator"
                ControlToValidate="txtConformText"
                OnServerValidate="ConformTextCustomValidator_ServerValidate"
                ValidationGroup="conformWindow"></asp:CustomValidator>
            <div class="button-container">
                <asp:Button ID="btnCancelDeleteConform" runat="server" 
                    Text="Cancel" 
                    CausesValidation="false"
                    OnClick="btnCancelDeleteConform_Click"/>
                <asp:Button runat="server" Text="Conform" 
                    OnClick="btnConformDeleteProgram_Click" 
                    ValidationGroup="conformWindow"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>


