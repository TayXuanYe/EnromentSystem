<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Paybyother.aspx.cs" Inherits="Paybyother" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/Paybyother.css") %>" />

    <style type="text/css">
        .auto-style1 {
            width: 256px;
        }
        h1{
          text-decoration-color:#000000;
          font-size:24px;
          font-weight:bold;
        }
        .container{
            background-color:white;
        }
        .textbox{
            margin:10px;
            padding:8px;
        }
        .auto-style2 td:hover {
        background-color: #f0f0f0; 
        cursor: pointer;
        }
        .calendar-container {
    display: inline-block;
    vertical-align: middle;
    margin-left: 10px; 
}

.calendar-container img {
    vertical-align: middle; 
}

    </style>


</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>Pay by Other</h1>
            <asp:Panel ID="Panel2" runat="server" CssClass="panel1">
                <asp:Label ID="Label1" runat="server" Text="Other Payment Details"></asp:Label>
            </asp:Panel>
            <div>
                <table>
                    <tr>
                        <td>Transaction Date</td>
                        <td>
                            <asp:TextBox ID="txtTransactionDate" runat="server" TextMode="Date" ></asp:TextBox>
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
                        <td class="auto-style1"><asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn-secondary" OnClick="Button1_Click" /></td>
                    </tr>
                   
                </table>
                <br />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill in the amount paid" ControlToValidate="txtAmountPaid" ForeColor="Red"></asp:RequiredFieldValidator><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Fill up the reference number" ControlToValidate="txtReferenceNo" ForeColor="Red"></asp:RequiredFieldValidator><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please fill up remarks" ControlToValidate="txtRemarks" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactNo" ErrorMessage="Please fill up contact no" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTransactionDate" ErrorMessage="Please Choose the date" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
            </div>
    
    </div>
</asp:Content>
