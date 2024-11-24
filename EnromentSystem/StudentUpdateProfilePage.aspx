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
                                <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator1" 
                                    ControlToValidate="txtPermanentAddress"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <p>Address</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentAddress" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator2" 
                                    ControlToValidate="txtCurrentAddress"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <!--Postcode-->
                        <tr>
                            <td>
                                <p>Postcode</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPermanentPostcode" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator7" 
                                    ControlToValidate="txtPermanentPostcode"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <p>Postcode</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentPostcode" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator8" 
                                    ControlToValidate="txtCurrentPostcode"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator8" 
                                    runat="server" 
                                    ControlToValidate="txtCurrentPostcode"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts number and have 5 number"
                                    ValidationExpression="\d{5}">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!--City-->
                        <tr>
                            <td>
                                <p>City</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPermanentCity" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator3" 
                                    ControlToValidate="txtPermanentCity"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator3" 
                                    runat="server" 
                                    ControlToValidate="txtPermanentCity"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <p>City</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentCity" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator4" 
                                    ControlToValidate="txtCurrentCity"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator4" 
                                    runat="server" 
                                    ControlToValidate="txtCurrentCity"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!--State-->
                        <tr>
                            <td>
                                <p>State</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPermanentState" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator5" 
                                    ControlToValidate="txtPermanentState"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator5" 
                                    runat="server" 
                                    ControlToValidate="txtPermanentState"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <p>State</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentState" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator16" 
                                    ControlToValidate="txtCurrentState"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator15" 
                                    runat="server" 
                                    ControlToValidate="txtCurrentState"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!--Country-->
                        <tr>
                            <td>
                                <p>Country</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPermanentCountry" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator6" 
                                    ControlToValidate="txtPermanentCountry"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator6" 
                                    runat="server" 
                                    ControlToValidate="txtPermanentCountry"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <p>Country</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentCountry" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator15" 
                                    ControlToValidate="txtCurrentCountry"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator14" 
                                    runat="server" 
                                    ControlToValidate="txtCurrentCountry"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
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
                                <asp:TextBox ID="txtPrimaryEmail" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator12" 
                                    ControlToValidate="txtPrimaryEmail"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator11" 
                                    runat="server" 
                                    ControlToValidate="txtPrimaryEmail"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="Not meet email format"
                                    ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <p>Tel No.</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelNo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator11" 
                                    ControlToValidate="txtTelNo"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator10" 
                                    runat="server" 
                                    ControlToValidate="txtTelNo"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts number"
                                    ValidationExpression="\d+">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!--Alternative Email and HP no.-->
                        <tr>
                            <td>
                                <p>Alternative Email</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlternativeEmail" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator10" 
                                    ControlToValidate="txtAlternativeEmail"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator9" 
                                    runat="server" 
                                    ControlToValidate="txtAlternativeEmail"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="Not meet email format"
                                    ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <p>HP No.</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHpNo" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                    ErrorMessage="This field is require"
                                    CssClass="validator"
                                    ControlToValidate="txtHpNo"
                                    Display="Dynamic"></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" 
                                    ControlToValidate="txtHpNo"
                                    ErrorMessage="Phone number format not correct<br> Format: [country code]-XXX-XXX-XXX"
                                    ForeColor="Red"
                                    ValidationExpression="\+\d{1,3}[-]\d{1,4}[-]\d{1,4}[-]\d{1,9}">
                                </asp:RegularExpressionValidator>
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
                                    [eg:+60-111-111-111]
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
                                <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator14" 
                                    ControlToValidate="txtContactPerson"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    runat="server" 
                                    ErrorMessage="This field is requited">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="RegularExpressionValidator13" 
                                    runat="server" 
                                    ControlToValidate="txtContactPerson"
                                    ForeColor="red"
                                    Display="dynamic"
                                    CssClass="validator"
                                    ErrorMessage="This field only accepts letter and space"
                                    ValidationExpression="[A-Za-z\s]+">
                                </asp:RegularExpressionValidator>
                            </td>
                            <th></th><td></td>
                        </tr>
                        <!--Contact Person HP NO-->
                        <tr>
                            <td>
                                <p>HP No.</p>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactPersonHpNo" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                    ErrorMessage="This field is require"
                                    CssClass="validator"
                                    ControlToValidate="txtContactPersonHpNo"
                                    Display="Dynamic"></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" 
                                    ControlToValidate="txtContactPersonHpNo"
                                    ErrorMessage="Phone number format not correct<br> Format: [country code]-XXX-XXX-XXX"
                                    ForeColor="Red"
                                    ValidationExpression="\+\d{1,3}[-]\d{1,4}[-]\d{1,4}[-]\d{1,9}">
                                </asp:RegularExpressionValidator>
                            </td>
                            <th></th><td></td>
                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <span>
                                    Input HP No. with country code
                                    <br />
                                    [eg:+60-111-111-111]
                                </span>
                            </td>
                            <th></th>
                            <td></td>
                        </tr>
                    </table>
                </td></tr>

                <tr><td class="footer-button">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"/>
                </td></tr>
            </tbody>
		</table>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>