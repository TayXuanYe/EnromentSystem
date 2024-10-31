<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact Us.aspx.cs" Inherits="Contact_Us" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Panel-move{
            position:absolute;
        }
        /* Style for the image button */
        .image-section {
            position: absolute;
            top: 10px; /* Align it vertically */
            left: 10px; /* Align it to the left */
        }

        /* Main container for the account section */
        .account-section {
            position: absolute;
            top: 10px; /* Same vertical alignment as the image */
            right: 20px; /* Align to the right */
            display: flex;
            align-items: center;
            border: 1px solid #ddd;
            padding: 10px;
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            width: auto;
            max-width: 450px;
        }

        /* Home icon */
        .account-icon {
            margin-right: 10px;
        }

        /* User info container */
        .user-info {
            flex-grow: 1;
            font-size: 14px;
            color: #555;
        }

        .user-info .name {
            font-weight: bold;
            font-size: 16px;
            color: #333;
        }

        .user-info .details {
            color: #888;
        }

        /* Logout button */
        .logout-button {
            background-color: #d9534f;
            color: white;
            border: none;
            padding: 8px 16px;
            font-size: 14px;
            cursor: pointer;
            text-transform: uppercase;
            border-radius: 5px;
        }

        .logout-button:hover {
            background-color: #c9302c;
        }
        .auto-style1 {
            position: absolute;
            top: 14px; /* Align it vertically */
            left: 333px; /* Align it to the left */
        }
        .auto-style2 {
            position: absolute;
            top: 16px; /* Same vertical alignment as the image */
            right: 258px; /* Align to the right */
            display: flex;
            align-items: center;
            border: 1px solid #ddd;
            padding: 10px;
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            width: auto;
            max-width: 450px;
        }
        /* Basic styling for navigation bar */
  .navbar {
      background-color: #333;
      overflow: hidden;
      width: 100%;
  }

  /* Styling for each link in the navbar */
  .navbar a {
      float: left;
      display: block;
      color: white;
      text-align: center;
      padding: 14px 16px;
      text-decoration: none;
      font-size: 17px;
  }

  /* Dropdown container - hidden initially */
  .dropdown {
      float: left;
      overflow: hidden;
  }

  .dropdown .dropbtn {
      font-size: 17px;
      border: none;
      outline: none;
      color: white;
      padding: 14px 16px;
      background-color: inherit;
      font-family: inherit;
      margin: 0;
  }

  /* Dropdown content (hidden by default) */
  .dropdown-content {
      display: none;
      position: absolute;
      background-color: #f9f9f9;
      min-width: 160px;
      box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
      z-index: 1;
  }

  /* Links inside the dropdown */
  .dropdown-content a {
      float: none;
      color: black;
      padding: 12px 16px;
      text-decoration: none;
      display: block;
      text-align: left;
  }

  /* Show the dropdown content on hover */
  .dropdown:hover .dropdown-content {
      display: block;
  }

  /* Change color of dropdown links on hover */
  .dropdown-content a:hover {
      background-color: #ddd;
  }
   /* Main content styles */
 .content {
     padding: 20px;
 }


        .auto-style3 {
            position: absolute;
            left: 453px;
            top: 324px;
            width: 558px;
            height: 603px;
        }


        .auto-style4 {
            position: absolute;
            left: 456px;
            top: 254px;
            margin-top: 9px;
        }
        .auto-style5 {
            position: absolute;
            left: 24px;
            top: 18px;
            width: 73px;
        }
        .auto-style6 {
            position: absolute;
            left: 20px;
            top: 48px;
            width: 532px;
            height: 28px;
        }
        .auto-style7 {
            position: absolute;
            left: 22px;
            top: 97px;
            width: 73px;
        }
        .auto-style8 {
            position: absolute;
            left: 19px;
            top: 141px;
            width: 516px;
            height: 30px;
        }
        .auto-style9 {
            position: absolute;
            left: 20px;
            top: 203px;
            width: 73px;
        }
        .auto-style10 {
            position: absolute;
            left: 16px;
            top: 251px;
            width: 516px;
            height: 265px;
        }
        .auto-style11 {
            position: absolute;
            left: 295px;
            top: 550px;
            width: 112px;
            right: 160px;
        }
        .auto-style12 {
            position: absolute;
            left: 880px;
            top: 875px;
            width: 103px;
            margin-top: 0px;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <!-- Image Button aligned to the top-left -->
        <div class="auto-style1">
            <asp:ImageButton ID="ImageButton1" runat="server" Height="95px" ImageUrl="~/Icon for inti/Screenshot 2024-10-20 193807.png" Width="451px" />
        </div>

        <!-- Account section aligned to the top-right, beside the image -->
        <div class="auto-style2">
            <!-- Home icon -->
            <div class="account-icon">
                <img src="Icon%20for%20inti/Home%20icon.png" alt="Home Icon" width="40" height="40"/>
            </div>

            <!-- User information -->
            <div class="user-info">
                <div class="name">Mizana Binti Mohamed Yahiya</div>
                <div class="details">I22023829</div>
                <div class="details">BCSI - Bachelor of Computer Science (HONS)</div>
            </div>

            <!-- Logout button -->
            <button type="submit" class="logout-button">Logout</button>
        </div>
         <div class="navbar">
     <!-- Home button -->
     <a href="#home">Home</a>

     <!-- Dropdown for Enrolment -->
     <div class="dropdown">
         <button class="dropbtn">Enrolment</button>
         <div class="dropdown-content">
             <a href="#">Course Enrolment</a>
         </div>
     </div>
      
      <!-- Dropdown for Enrolment -->
     <div class="dropdown">
    <button class="dropbtn">Add / Drop</button>
    <div class="dropdown-content">
        <a href="#">Course Add / Drop</a>
        <a href="#">Add / Drop History</a>
    </div>
     </div>
  
       <div class="dropdown">
<button class="dropbtn">Enquiry</button>
<div class="dropdown-content">
    <a href="#">Timetable Matching</a>
    <a href="#">Contact Us</a>
</div>
 </div>

        <div class="dropdown">
<button class="dropbtn">Statement</button>
<div class="dropdown-content">
    <a href="#">Student Statement</a>
    <a href="#">Registration Summary</a>
</div>
 </div>     

      <div class="dropdown">
<button class="dropbtn">Payment</button>
<div class="dropdown-content">
    <a href="#">Payment</a>
    <a href="#">Online Payment History / Receipt</a>
    <a href="#">Invoice and Adjustment Note</a>
</div>
 </div>
                   <div class="dropdown">
<button class="dropbtn">Account</button>
<div class="dropdown-content">
    <a href="#">Change Password</a>
    <a href="#">Update Profile</a>
    <a href="#">Update Bank Details</a>
</div>
 </div>
     
 </div>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="X-Large" Text="Contact Us" CssClass="auto-style4" ForeColor="#666666"></asp:Label>
        </p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        </p>
        <p>
        </p>
        <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" CssClass="auto-style3">
            <asp:Label ID="Label2" runat="server" CssClass="auto-style5" Text="Category"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="auto-style6" Width="524px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Registry - for any enrolment realted matters</asp:ListItem>
                <asp:ListItem>Scholarship - for any internal scholarship, PTPTN, MARA study loan related matters</asp:ListItem>
                <asp:ListItem>Fianace - for any payment related matters</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label3" runat="server" CssClass="auto-style9" Text="Message"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style8" Width="516px"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" CssClass="auto-style7" Text="Subject"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="auto-style10" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" CssClass="auto-style11" Height="30px" Text="Send" Width="103px" />
        </asp:Panel>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
            <!-- Main Content -->
 <div class="content">
     <asp:Button ID="Button2" runat="server" CssClass="auto-style12" Height="30px" Text="Back" Width="103px" />
 </div>
    </form>
</body>
</html>
