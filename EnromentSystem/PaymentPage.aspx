<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentPage.aspx.cs" Inherits="PaymentPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Payment Page</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <h2>Student Information</h2>
                <asp:Label ID="lblName" runat="server" Text="Name: " /><br />
                <asp:Label ID="lblSid" runat="server" Text="Student ID: " /><br />
                <asp:Label ID="lblIC" runat="server" Text="IC or Passport: " /><br />
                <asp:Label ID="lblProgram" runat="server" Text="Program: " /><br />
                <asp:Label ID="lblIntake" runat="server" Text="Intake: " /><br />
                <asp:Label ID="lblSemester" runat="server" Text="Semester: " /><br />
            </div>
        </form>
    </body>
</html>
