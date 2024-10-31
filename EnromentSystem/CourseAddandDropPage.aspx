<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CourseAddandDropPage.aspx.cs" Inherits="CourseAddandDropPage" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%: ResolveUrl("~/Styles/CourseAddandDrop.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div class="panel-container">
            <asp:Panel ID="Panel1" runat="server" Height="340px">
                <h1>Course Add / Drop</h1>
                <br />

                <!-- Flex container for the tables -->
                <div class="table-container">
                    <!-- First table -->
                    <table class="table-left">
                        <tr>
                            <td>Matriculation No</td>
                            <td>:</td>
                            <td><asp:Label ID="lblMatricNo" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Student Name</td>
                            <td>:</td>
                            <td><asp:Label ID="Label2" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>IC/Passport No</td>
                            <td>:</td>
                            <td><asp:Label ID="lblPassport" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Mode Of Study</td>
                            <td>:</td>
                            <td><asp:Label ID="lblModeOfStudy" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Session</td>
                            <td>:</td>
                            <td><asp:Label ID="lblSession" runat="server" /></td>
                        </tr>
                    </table>

                    <!-- Second table (on the right) -->
                    <table class="table-right">
                        <tr>
                            <td>School</td>
                            <td>:</td>
                            <td><asp:Label ID="lblSchool" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Level</td>
                            <td>:</td>
                            <td><asp:Label ID="lblLevel" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Program</td>
                            <td>:</td>
                            <td><asp:Label ID="lblProgram" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Major</td>
                            <td>:</td>
                            <td><asp:Label ID="lblMajor" runat="server" /></td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <p class="underline-red">Fee Summary</p>
            <p>You are currently enrolled to the following course(s):</p>

            <asp:Panel ID="Panel2" runat="server">
                <!-- New Course Table -->
                <table class="course-table">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>Course</th>
                            <th>Credit</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <button type="button" class="round-button">
                                    <img src="path/to/your/image.png" alt="Add" /> <!-- Change the path to your image -->
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <button type="button" class="round-button">
                                    <img src="path/to/your/image.png" alt="Add" /> <!-- Change the path to your image -->
                                </button>
                            </td>
                        </tr>
                        <!-- Add more rows as needed -->
                    </tbody>
                </table>
            </asp:Panel>
        </div>
        <p>
            &nbsp;</p>
        <asp:Panel ID="Panel3" runat="server" CssClass ="panel-small" Height="107px" Width="500px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            &nbsp;
            <asp:Image ID="Image1" runat="server" Height="30px" ImageUrl="~/Icon for inti/Plus button.png" Width="34px" />
            &nbsp; Click to <span class="auto-style1">add</span> new course.&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="~/Icon for inti/Minus Button.png" Width="34px" />
            &nbsp;Click to <span class="auto-style1">drop</span> new course.<br /> &nbsp; Click on the &quot;Section Link&quot; highlighted in blue to change a section</asp:Panel>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <asp:Label ID="Label3" runat="server" Text="Request Changes:" CssClass="unique-label-style"></asp:Label>
  
    
    <asp:Panel ID="Panel4" runat="server">
     <!-- New Course Table -->
     <table class="course-table">
         <thead>
             <tr>
                 <th>No</th>
                 <th>Course</th>
                 <th>Credit</th>
                 <th>Action</th>
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td>
                     <button type="button" class="round-button">
                         <img src="path/to/your/image.png" alt="Add" /> <!-- Change the path to your image -->
                     </button>
                 </td>
             </tr>
             <tr>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td>
                     <button type="button" class="round-button">
                         <img src="path/to/your/image.png" alt="Add" /> <!-- Change the path to your image -->
                     </button>
                 </td>
             </tr>
             <!-- Add more rows as needed -->
         </tbody>
     </table>
 </asp:Panel>
       <p>
           &nbsp;</p>
        <p>
            &nbsp;<asp:Button ID="Button1" runat="server" Text="            Request for Approval" CssClass="custom-button" BackColor="Silver" BorderColor="#999999" BorderStyle="Solid" Height="45px"  Width="250px"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="Button2" runat="server" Text="                         Cancel" CssClass="custom-button" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" Height="45px" Width="250px" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;<asp:Label ID="Label4" runat="server" Text="@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved." CssClass="unique-label-style2"></asp:Label>
        </p>
<p>
    &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
 
   </asp:Content>