<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HOPReviewDropCourse.aspx.cs" Inherits="HOPReviewDropCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Course</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/HOPReviewAdd.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <h2>Drop Course</h2>
    
            <asp:GridView ID="gvRequests" runat="server" AutoGenerateColumns="False"
                CssClass="gridview"
                OnRowCommand="gvRequests_RowCommand"
                OnRowDataBound="gvRequests_RowDataBound"
                EmptyDataText="No pending requests to display.">
                <Columns>

                    <asp:BoundField DataField="rid" HeaderText="Request ID" SortExpression="rid" />
                    <asp:BoundField DataField="sid" HeaderText="Student ID" SortExpression="sid" />
                    <asp:BoundField DataField="cid" HeaderText="Course ID" SortExpression="cid" />
                    <asp:BoundField DataField="reason" HeaderText="Reason" SortExpression="reason" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:BoundField DataField="create_time" HeaderText="Create Time" SortExpression="create_time" />

                    <asp:TemplateField>
                        <ItemTemplate>

                            <asp:Button ID="btnAccept" runat="server" Text="Accept" 
                                CommandName="Accept" CommandArgument="<%# 
                                Container.DataItemIndex %>" CssClass="action-button" />

                            <asp:Button ID="btnNotAccept" runat="server" Text="Not Accept" 
                                CommandName="NotAccept" CommandArgument="<%# 
                                Container.DataItemIndex %>" CssClass="action-button" />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="action-container">
                <asp:Button ID="Return" runat="server" Text="Return to Homepage" CssClass="return-button" OnClick="Return_Click" />
            </div>

        </div>
    </form>
</body>
</html>
