<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentStatementPage.aspx.cs" Inherits="StudentStatementPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Statement Record PDF Generator</title>
        <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }

        h1 {
            text-align: center;
            color: #333;
        }

        .container {
            width: 80%;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        #GridView1 {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        #GridView1 th, #GridView1 td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        #GridView1 th {
            background-color: #4CAF50;
            color: white;
        }

        #GridView1 tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #GridView1 tr:hover {
            background-color: #ddd;
        }

        .button-container {
            text-align: center;
            margin-top: 20px;
        }

        .button-container input[type=submit], .button-container input[type=button] {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }

        .button-container input[type=submit]:hover, .button-container input[type=button]:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Statement Record PDF Generator</h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True"></asp:GridView>
            <div class="button-container">
                <asp:Button ID="btnGeneratePdf" runat="server" Text="Generate PDF" OnClick="btnGeneratePdf_Click" />
            </div>
        </div>
    </form>
</body>
</html>
