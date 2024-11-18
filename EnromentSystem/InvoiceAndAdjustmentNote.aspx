<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="InvoiceAndAdjustmentNote.aspx.cs" Inherits="InvoiceAndAdjustmentNote" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function loadPdf(particulars) {
            // Call server-side to generate the PDF by setting the iframe src
            var iframe = document.getElementById('pdfViewer');
            iframe.src = "InvoiceAndAdjustmentNote.aspx?pdf=" + encodeURIComponent(particulars);
        }
    </script>


        <h2>Invoice and Adjustment Note for:
                <asp:DropDownList ID="ddlMonthYear" runat="server" >
                    <asp:ListItem Text="AUG2024" Value="AUG2024" />
                </asp:DropDownList>
        </h2>
        <asp:GridView ID="gvInvoices" width="100%" height="100px" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInvoices_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Particular" HeaderText="Particular" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="DocumentDate" HeaderText="Document Date" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                <asp:BoundField DataField="AmountSettled" HeaderText="Amount Settled" />
            </Columns>
        </asp:GridView>

        <h3>PDF Preview</h3>
        <iframe id="pdfViewer" width="100%" height="600px" style="border: 1px solid black;"></iframe>
</asp:Content>


