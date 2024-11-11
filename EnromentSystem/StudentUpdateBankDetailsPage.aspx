<%@ Page 
    Title="Update Bank Details Page"
    MasterPageFile="~/Site.master"
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="StudentUpdateBankDetailsPage.aspx.cs" 
    Inherits="StudentUpdateBankDetailsPage" %>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentUpdateBankDetailsPage.css") %>" />
</asp:Content>

<asp:Content  ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
    <div class="body">
        <div>
            <h1>Update Bank Details Page</h1>
            <table>
                <thead>
			        <tr>
				        <th><h2>Bank Information</h2></th>
                    </tr>
                </thead>
                <tbody>
                    <tr><td>
                        <table class="bank-details">
                            <!--Bank name-->
                            <tr>
                                <td>
                                    <p>Bank Name</p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                            </tr>
                            <!--Bank Account No.-->
                            <tr>
                                <td>
                                    <p>Bank Account No.</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcountNo" runat="server"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator2" 
                                        ControlToValidate="txtAcountNo"
                                        ForeColor="red"
                                        Display="dynamic"
                                        CssClass="validator"
                                        runat="server" 
                                        ErrorMessage="This field is requited">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="RegularExpressionValidator1" 
                                        runat="server" 
                                        ControlToValidate="txtAcountNo"
                                        ForeColor="red"
                                        Display="dynamic"
                                        CssClass="validator"
                                        ErrorMessage="This field only accepts number"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <!--Bank Holder Name-->
                            <tr>
                                <td>
                                    <p>Bank Holder Name</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHolderName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" 
                                        ControlToValidate="txtHolderName"
                                        ForeColor="red"
                                        Display="dynamic"
                                        CssClass="validator"
                                        runat="server" 
                                        ErrorMessage="This field is requited">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="RegularExpressionValidator2" 
                                        runat="server" 
                                        ControlToValidate="txtHolderName"
                                        ForeColor="red"
                                        Display="dynamic"
                                        CssClass="validator"
                                        ErrorMessage="This field only accepts letter and space"
                                        ValidationExpression="[A-Za-z\s]+">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        
                            <!--Account verification documents-->
                            <tr>
                                <td>
                                    <p>Account Verification Documents</p>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileUploadVerificationDocument" runat="server" />
                                    <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator3" 
                                        ControlToValidate="fileUploadVerificationDocument"
                                        ForeColor="red"
                                        Display="dynamic"
                                        CssClass="validator"
                                        runat="server" 
                                        ErrorMessage="This field is requited">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:Literal ID="Literal1" runat="server" Text="Download"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td></tr>
                    <tr><td class="footer-button">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload and Save Bank Details" OnClick="btnUpload_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="Exit without Saving" OnClick="btnExit_Click" CausesValidation="false"/>
                    </td></tr>
                </tbody>
		    </table>
        </div>
    </div>
</div>
</asp:Content>