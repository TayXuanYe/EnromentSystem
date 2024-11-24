<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Paybyother.aspx.cs" Inherits="Paybyother" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/Paybyother.css") %>" />

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Panel ID="Panel1" runat="server" CssClass="panel">
            <asp:Panel ID="Panel2" runat="server" CssClass="panel1">
                <asp:Label ID="Label1" runat="server" Text="Other Payment Details"></asp:Label>
            </asp:Panel>
            <div>
                <table>
                    <tr>
                        <td>Transaction Date</td>
                        <td>
                            <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton 
                                ID="imgCalendar" 
                                runat="server" 
                                ImageUrl="~/Images/calendar-icon.png" 
                                OnClick="imgCalendar_Click" 
                                AlternateText="Calendar" />
                            <asp:Calendar 
                                ID="Calendar1" 
                                runat="server" 
                                Visible="false" 
                                OnSelectionChanged="Calendar1_SelectionChanged">
                            </asp:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td>Currency</td>
                        <td><asp:DropDownList ID="ddlCurrency" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Amount Paid</td>
                        <td><asp:TextBox ID="txtAmountPaid" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Reference No</td>
                        <td><asp:TextBox ID="txtReferenceNo" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Remarks</td>
                        <td><asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Upload Slip</td>
                        <td><asp:FileUpload ID="FileUpload1" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Contact No</td>
                        <td><asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary" OnClick="btnSubmit_Click" /></td>
                        <td><asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn-secondary" /></td>
                    </tr>
                   
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
