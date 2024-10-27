<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddDropHistoryPage.aspx.cs" 
  Inherits="AddDropHistoryPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/AddDropHistory.css") %>" />
</asp:Content>
<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
       <div> 
       <br />
       <h1>ADD / DROP HISTORY </h1>
   </div>
        <h2>Your Add / Drop history is as follows: </h2>
    <div>
         <asp:Panel ID="Panel2" runat="server">
     <!-- New Course Table -->
     <table class="course-table">
         <thead>
             <tr>
                 <th>No</th>
                 <th>Course</th>
                 <th>Date</th>
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td></td>
                 <td></td>
                 <td></td>
             </tr>
             <tr>
                 <td></td>
                 <td></td>
                 <td></td>
             </tr>
             <tr>
                <td></td>
                <td></td>
                <td></td>
                </tr>
              <tr>
                <td></td>
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td></td>
                <td></td>
                <td></td>
              </tr>
             <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
             <!-- Add more rows as needed -->
         </tbody>
     </table>
 </asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <h3>@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved.</h3>

    </asp:Content>
