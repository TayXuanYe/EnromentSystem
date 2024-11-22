<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HOPReviewHomePage.aspx.cs" Inherits="HOPReviewPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/HOPReviewHome.css") %>"/>
</head>
<body>
    <div class="layout-container">
        <div class="welcome-container">
            <asp:Label ID="welcome" runat="server" Text="Welcome 'Full Name'"></asp:Label>
        </div>

        <div class="main-content">
            <div class="content-box" id="add-course-box">
                <asp:Image ID="addcourse" runat="server" ImageUrl="~/Images/addcourse.png"/>
                <h2>Request Add Course</h2>
                <a href="HOPReviewAddCourse.aspx" class="action-button">Go to Page</a>
            </div>

            <div class="content-box" id="change-section-box">
                <asp:Image ID="changesection" runat="server" ImageUrl="~/Images/changesection.png"/>
                <h2>Request Change Section</h2>
                <a href="HOPReviewChangeSection.aspx" class="action-button">Go to Page</a>
            </div>

            <div class="content-box" id="drop-course-box">
                <img src="deletecourse.png" alt="Drop Course" class="box-image"/>
                <asp:Image ID="deletecourse" runat="server" ImageUrl="~/Images/deletecourse.png"/>
                <h2>Request Drop Course</h2>
                <a href="HOPReviewDropCourse.aspx" class="action-button">Go to Page</a>
            </div>
        </div>
    </div>
</body>
</html>

