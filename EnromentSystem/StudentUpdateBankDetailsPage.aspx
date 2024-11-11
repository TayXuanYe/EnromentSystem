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
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="dropDownList"></asp:DropDownList>
                                </td>
                            </tr>
                            <!--Bank Account No.-->
                            <tr>
                                <td>
                                    <p>Bank Account No.</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <!--Bank Holder Name-->
                            <tr>
                                <td>
                                    <p>Bank Holder Name</p>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        
                            <!--Account verification documents-->
                            <tr>
                                <td>
                                    <p>Account Verification Documents</p>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
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
                        <asp:Button ID="Button1" runat="server" Text="Upload and Save Bank Details" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" Text="Exit without Saving" OnClick="Button2_Click" />
                    </td></tr>
                </tbody>
		    </table>
        </div>
    </div>
</div>
</asp:Content>