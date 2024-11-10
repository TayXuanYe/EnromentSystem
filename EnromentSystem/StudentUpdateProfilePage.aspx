<%@ Page 
    Title="Update Profile Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentUpdateProfilePage.aspx.cs" 
    Inherits="StudentUpdateProfilePage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentUpdateProfilePage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
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
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td></tr>
            </tbody>
		</table>
    </div>
</div>
</asp:Content>