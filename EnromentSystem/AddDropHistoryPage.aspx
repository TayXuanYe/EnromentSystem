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
            <asp:GridView ID="GridViewAddDropHistory" runat="server" CssClass="course-table" AutoGenerateColumns="false" Encode=false>
                <Columns>
                    <asp:BoundField DataField="no" HeaderText="No." />
                    <asp:TemplateField HeaderText="Course">
                        <ItemTemplate>
                            <%# Eval("courseInfo") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="date" HeaderText="Date" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <h3>@Copyright 2014 INTI International University &amp; Collages. All Rights Reserved.</h3>
    <script src="Scripts/courseAddAndDrop.js" type="text/javascript"></script>
    </asp:Content>
