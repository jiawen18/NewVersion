<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="SuccessPage.aspx.cs" Inherits="NewVersion.css.SuccessPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="container">
        <br />
        <h1>Payment Successful!</h1>
        <p>Thank you for your order. Your payment has been processed successfully.</p>
        
        <h2>Order Details</h2>
        <table class="table">
            <tr>
                <th>Order ID</th>
                <td><asp:Label ID="lblOrderId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Transaction ID</th>
                <td><asp:Label ID="lblTransactionId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Invoice ID</th>
                <td><asp:Label ID="lblInvoiceId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Amount</th>
                <td><asp:Label ID="lblAmount" runat="server"></asp:Label></td>
            </tr>
        </table>

        <div>
            <asp:Button ID="btnContinue" runat="server" Text="Continue Shopping" OnClick="btnContinue_Click" CssClass="btn btn-primary" />
        </div>
    </div>

    <script>
        // setTimeout("location = 'Home.aspx'", 3000);


    </script>

</asp:Content>
