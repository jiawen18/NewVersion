<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="cancelled.aspx.cs" Inherits="NewVersion.css.cancelled" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
               <style>
    .no-underline {
        text-decoration: none; 
        color: inherit; 
    }
    .no-underline:hover {
        text-decoration: none; 
    }
    h2 {
        z-index: 10; 
        position: relative;
    }
    .header {
        font-size: 20px;
        font-weight: bold;
        text-align: center;
    }
    .order-item {
        padding: 10px 0;
    }
    .order-info {
        margin: 10px 0;
    }
    .button {
        margin-top: 10px;
    }
    .card {
        border-radius: 10px;
        margin-bottom: 20px;
        padding: 20px;
        border: 1px solid #ddd;
    }
    .product-info {
        display: flex;
        justify-content: space-between;
        margin: 10px 0;
        padding-top: 10px;
    }
    .product-image {
        max-width: 80px;
        max-height: 100px;
    }
    .order-summary {
        margin-top: 20px;
        border-top: 1px solid #e0e0e0;
        padding-top: 10px;
    }
    .details {
        margin-top: 10px;
    }
</style>

<div class="hero">
    <div class="container">
        <div class="row justify-content-between">
            <div class="col-lg-5">
                <div class="intro-excerpt">
                    <h1>My Orders</h1>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="container-fluid">
    <div class="row">
        <!-- menu navigation -->
        <nav class="col-md-2 sidebar">
            <ul class="order">
                <li><a href="Order.aspx" class="<%= Request.FilePath.Contains("Order.aspx") ? "active" : "" %>">To Ship</a></li>
                <li><a href="completed.aspx" class="<%= Request.FilePath.Contains("completed.aspx") ? "active" : "" %>">Completed</a></li>
                <li><a href="cancelled.aspx" class="<%= Request.FilePath.Contains("cancelled.aspx") ? "active" : "" %>">Cancelled</a></li>
            </ul>
        </nav>

        <!-- content -->
        <main class="col-md-10 content">
            <h2 style="transform: translate(100px, 60px);">
                <asp:HyperLink ID="hplBackHome2" runat="server" CssClass="no-underline" NavigateUrl="UserProfile.aspx">< </asp:HyperLink>
                To Ship <img src="images/truck.png" />
            </h2>

            <%-- Content Section --%>
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-lg-10 col-xl-8">
                    <asp:Repeater ID="rptCancelOrders" runat="server" OnItemDataBound="rptCancelOrders_ItemDataBound">
                        <ItemTemplate>
                            <div class="card" style="margin-bottom: 20px;">
                                <div class="header px-4 py-5">
                                    <h5 class="text-muted mb-0" style="font-size: 24px; text-align: left; margin-top: 10px;">Hamsung.</h5>
                                    <hr style="border-top: 1px solid #e0e0e0; margin: 10px 0;" />
                                    <div class="order-info d-flex justify-content-between align-items-center mb-1">
                                        <p class="lead fw-normal mb-0" style="color: #a8729a;">Order</p>
                                        <p>
                                            <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%# "Order ID: " + Eval("OrderID") %>' CssClass="small text-muted mb-0"></asp:Label>
                                        </p>
                                    </div>
                                </div>

                                <!-- View Details Button -->
                                <div class="card-body" style="padding: 0;">
                                    <div class="d-flex justify-content-end" style="margin-bottom: 10px;">
                                        <asp:Button ID="btnViewDetails" runat="server" Text="View Details" CssClass="btn btn-primary" OnClientClick="toggleDetails(this); return false;" />
                                    </div>
                                </div>

                                <!-- Product Details Section (collapsed by default) -->
                                <div class="details" style="display: none; padding: 10px;">
                                    <asp:Repeater ID="rptProducts" runat="server">
                                        <ItemTemplate>
                                            <div class="border" style="margin-bottom: 10px; padding: 10px;">
                                                <div class="product-info" style="display: flex; align-items: center; justify-content: space-between;">
                                                    <div class="product-image" style="flex: 0 0 auto;">
                                                        <img src='<%# Eval("ProductImage") %>' alt="Product Image" style="max-width: 100px; max-height: 200px;" />
                                                    </div>
                                                    <div style="flex: 1; padding-left: 20px;">
                                                        <div class="d-flex justify-content-between">
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' CssClass="text-muted mb-0" style="margin-right: 10px;"></asp:Label>
                                                            <asp:Label ID="lblColor" runat="server" Text='<%# "Color: " + Eval("Color") %>' CssClass="text-muted mb-0" style="margin-right: 10px;"></asp:Label>
                                                            <asp:Label ID="lblCapacity" runat="server" Text='<%# "Storage: " + Eval("Capacity") %>' CssClass="text-muted mb-0" style="margin-right: 10px;"></asp:Label>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# "Qty: " + Eval("Quantity") %>' CssClass="text-muted mb-0" style="margin-right: 10px;"></asp:Label>
                                                            <asp:Label ID="lblProductPrice" runat="server" Text='<%# "RM " + Eval("Price") %>' CssClass="text-muted mb-0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <div class="row d-flex align-items-center">
                                        <div class="button d-flex justify-content-end">
                                            <asp:Button class="text-muted mb-0 small" ID="btnCancel" runat="server" Text="Cancel" CommandArgument='<%# Eval("OrderID") %>' OnClick="btnCancel_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button class="text-muted mb-0 small" ID="btnTrack" runat="server" Text="Track Order" OnClick="btnTrack_Click" />
                                        </div>

                                        <div class="order-summary">
                                            <div>
                                                <asp:Label ID="Label1" runat="server" Text='<%# "Invoice Number: " + Eval("InvoiceNumber") %>' CssClass="text-muted mb-0"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# "Invoice Date: " + Convert.ToDateTime(Eval("InvoiceDate")).ToString("MM dd yyyy") %>' CssClass="text-muted mb-0"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblDeliveryFee" runat="server" CssClass="text-muted mb-0"><span class="fw-bold me-4">Delivery Fee:</span> <%# "RM " + Eval("DeliveryFee") %></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblTotalPrice" runat="server" CssClass="text-muted mb-0"><span class="fw-bold me-4">Total Price:</span> <%# "RM " + Eval("TotalPrice") %></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </main>
    </div>
</div>
    

<script>
    function toggleDetails(button) {
        var detailsDiv = button.closest('.card').querySelector('.details');
        if (detailsDiv.style.display === 'none' || detailsDiv.style.display === '') {
            detailsDiv.style.display = 'block';
            button.innerText = 'Hide Details';
        } else {
            detailsDiv.style.display = 'none';
            button.innerText = 'View Details';
        }
    }
</script>

</asp:Content>
