<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentUpdateProfilePage.aspx.cs" Inherits="StudentUpdateProfilePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Profile Page</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentUpdateProfilePage.css") %>" />
</head>
<body>
<form id="form1" runat="server">
<div class="box">
    <div class="header">
    <div id="intiLogo"></div>
    <div id="identityCard">
        <asp:Button ID="btnHomeButton" runat="server" Text="" />
        <div id="studentDetails">
            <asp:Label runat="server" Text="Tay Xuan Ye<br>I23024312<br>SCSI - BACHELOR OF COMPUTER SCIENCE (HONS)"></asp:Label>
        </div>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" />
    </div>

    <div class="navigation-bar">
    <table>
    <tr>
    <td style=" text-align:left;">
        <ul id="nav-one" class="dropmenu">
            <!--Home-->
            <li>
                <p><a href="#">Home</a></p>
            </li>

            <!--Enrolment-->
            <li> 
	                    <p>Enrolment</p>
                <ul>
                    <li><a href="#">Course Enrolment</a></li>
                </ul>
            </li>

            <!--Add and Drop-->
            <li> 
	                    <p>Add & Drop</p>
                <ul>
                    <li><a href="#">Course Add / Drop</a></li>
                    <li><a href="#">Add / Drop History</a></li>
                </ul>
            </li>

            <!--Enquiry-->
            <li> 
	                    <p>Enquiry</p>
                <ul>
                    <li><a href="#">Timetable Matching</a></li>
                    <li><a href="#">Contact Us</a></li>
                </ul>
            </li>

            <!--Statement-->
            <li> 
	                    <p>Statement</p>
                <ul>
                    <li><a href="#">Student Statement</a></li>
                    <li><a href="#">Registration Summary</a></li>
                </ul>
            </li>

            <!--Payment-->
            <li> 
	                    <p>Payment</p>
                <ul>
                    <li><a href="#">Payment</a></li>
                    <li><a href="#">Online Payment History <br /> Receipt</a></li>
                    <li><a href="#">Invoice and Adjustment <br /> Note</a></li>
                </ul>
            </li>

            <!--Account-->
            <li> 
	                    <p>Account</p>
                <ul>
                    <li><a href="#">Change Password</a></li>
                    <li><a href="#">Update Profile</a></li>
                    <li><a href="#">Update Bank Details</a></li>
                </ul>
            </li>
    </ul>
    </td>
    </tr>
    </table>
    </div>
</div>
    <div class="body">
        <div>
            <h1>Update Profile</h1>

            <table>
                <thead>
				    <tr>
					    <th><h2>Contact Information</h2></th>
                    </tr>
                </thead>
                <tbody>
                    <tr><td>
                        <table class="personal-address">
                            <tr>
                                <th colspan="2">
                                    <h3>Permanent Home Address</h3>
                                </th>
                                <th colspan="2">
                                    <h3>Current Mailing Address</h3>
                                </th>
                            </tr>
                            <!--Address-->
                            <tr>
                                <td>
                                    <p>Address</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <p>Address</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <!--Postcode-->
                            <tr>
                                <td>
                                    <p>Postcode</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPermanentPostcode" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <p>Postcode</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentPostcode" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <!--City-->
                            <tr>
                                <td>
                                    <p>City</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPermanentCity" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <p>City</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentCity" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <!--State-->
                            <tr>
                                <td>
                                    <p>State</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPermanentState" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                                <td>
                                    <p>State</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrentState" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                            </tr>
                            <!--Country-->
                            <tr>
                                <td>
                                    <p>Country</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPermanentCountry" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                                <td>
                                    <p>Country</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrentCountry" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td></tr>

                    <tr><td>
                        <table class="personal-contact">
                            <tr>
                                <th colspan="2">
                                    <h3>Personal Contact</h3>
                                </th>
                            </tr>
                            <!--Primary email and tel no.-->
                            <tr>
                                <td>
                                    <p>Primary Email</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrimaryEmail" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <p>Tel No.</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTelNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <!--Alternative Email and HP no.-->
                            <tr>
                                <td>
                                    <p>Alternative Email</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAlternativeEmail" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <p>HP No.</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHpNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td></td>
                                <th></th>
                                <td>
                                    <span>
                                        Input HP No. with country code
                                        <br />
                                        [eg:60111111111]
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td></tr>

                    <tr><td>
                        <table class="emergency-contact">
                           <tr>
                                <th colspan="2">
                                    <h3>Emergency Contact</h3>
                                </th>
                            </tr>
                            <!--Relationship-->
                            <tr>
                                <td>
                                    <p>Relationship</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRelationship" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Text="Father" Value="Father"></asp:ListItem>
                                        <asp:ListItem Text="Mother" Value="Mother"></asp:ListItem>
                                        <asp:ListItem Text="Guardian" Value="Guardian"></asp:ListItem>
                                        <asp:ListItem Text="Friend" Value="Friend"></asp:ListItem>
                                        <asp:ListItem Text="Relative" Value="Relative"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th></th><td></td>
                            </tr>
                            <!--Contact Person-->
                            <tr>
                                <td>
                                    <p>Contact Person</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox>
                                </td>
                                <th></th><td></td>
                            </tr>
                            <!--Contact Person HP NO-->
                            <tr>
                                <td>
                                    <p>HP No.</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactPersonHpNo" runat="server"></asp:TextBox>
                                </td>
                                <th></th><td></td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <span>
                                        Input HP No. with country code
                                        <br />
                                        [eg:60111111111]
                                    </span>
                                </td>
                                <th></th>
                                <td></td>
                            </tr>
                        </table>
                    </td></tr>

                    <tr><td class="footer-button">
                        <asp:Button ID="btnSave" runat="server" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    </td></tr>
                </tbody>
			</table>
        </div>
    </div>
</div>
</form>
</body>
</html>
