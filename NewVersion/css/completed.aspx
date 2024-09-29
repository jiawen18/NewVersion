<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="completed.aspx.cs" Inherits="NewVersion.css.completed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource 
    ID="SqlDataSource1" 
    runat="server" 
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    ProviderName="System.Data.SqlClient"
    SelectCommand="
           SELECT 
            o.OrderID,
            od.ProductName,
            od.Quantity,
            od.Storage,
            od.Color,
            od.Price,
            od.ProductImage,
            t.TransactionStatus
        FROM 
            [dbo].[Transaction] t
        INNER JOIN 
            [dbo].[Order] o ON t.OrderID = o.OrderID
        INNER JOIN 
            [dbo].[OrderDetails] od ON o.OrderID = od.OrderID
        WHERE 
            t.TransactionStatus = 'Success'">
</asp:SqlDataSource>
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

.card-details {
    z-index: 1;
}
</style>

            <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>My Orders</h1>
                        <%--<p class="mb-4">Discover the latest gadgets designed to elevate your digital experience. Shop now for cutting-edge devices that keep you ahead of the curve.</p>
                        <p>
                            <a href="Smartphones.aspx" class="btn btn-secondary me-2">Shop Now</a>
                            <a href="AboutUs.aspx" class="btn btn-white-outline">Explore</a>
                        </p>--%>
                    </div>
                </div>
                <%--<div class="col-lg-7">
                    <div class="hero-img-wrap">
                        <img src="images/slide_image1.png" class="images">
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

                  <div class="container-fluid">
    <div class="row">
        <!-- navigation bar (left menu) -->
        <nav class="col-md-2 sidebar">
    <ul class="order">
     <li><a href="Order.aspx" class="<%= Request.FilePath.Contains("Order.aspx") ? "active" : "" %>">To Ship</a></li>
    <li><a href="completed.aspx" class="<%= Request.FilePath.Contains("completed.aspx") ? "active" : "" %>">Completed</a></li>
    <li><a href="cancelled.aspx" class="<%= Request.FilePath.Contains("cancelled.aspx") ? "active" : "" %>">Cancelled</a></li>
</ul>

</nav>


         <!-- main content -->
         <main class="col-md-10 content">
             <h2 style="transform: translate(100px, 60px);">
                 <asp:HyperLink ID="hplBackHome1" runat="server" CssClass="no-underline" NavigateUrl="UserProfile.aspx">< </asp:HyperLink>
                 Completed <img src="images/package.png" /></h2>
            
             <%-- Content Section --%>
    <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
            <div class="col-lg-10 col-xl-8">
                <asp:Repeater ID="rptOrderDetails" runat="server" >
                    <HeaderTemplate>
                        <div class="card" style="border-radius: 10px;">
                            <div class="card-header px-4 py-5">
                                <h5 class="text-muted mb-0">HanSumg.</h5>
                            </div>
                            <div class="card-body p-4">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <p class="lead fw-normal mb-0" style="color: #a8729a;">Order</p>
                            <p class="small text-muted mb-0">Order ID : <%# Eval("OrderID") %></p>
                        </div>

                        <div class="border">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-2">
                                        <img src='<%# Eval("ProductImage") %>' style="max-width: 80px; max-height: 175px;" />
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0"><%# Eval("ProductName") %></p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Color: <%# Eval("Color") %></p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Capacity: <%# Eval("Storage") %></p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Qty: <%# Eval("Quantity") %></p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">RM<%# Eval("Price", "{0:N2}") %></p>
                                    </div>
                                </div>
                                <hr class="mb-4" style="background-color: #e0e0e0; opacity: 1;">
                                <div class="row d-flex align-items-center">
                                    <div class="trackAndReview">
                                        <asp:Button class="text-muted mb-0 small" ID="btnReview" runat="server" Text="Review" OnClick="btnReview_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button class="text-muted mb-0 small" ID="btnTrack" runat="server" Text="Track Order" OnClick="btnTrack_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>


                            <!-- Collapsible section -->
                            <div class="card-details">
                                <div class="d-flex justify-content-between pt-2">
                                    <p class="text-muted mb-0" style="position: relative; left: 30px;">
                                        <asp:Label class="fw-bold mb-0" ID="lblOrderDetails" runat="server" Text="Order Details"></asp:Label>
                                    </p>
                                </div>

                                <div class="d-flex justify-content-between pt-2">
                                    <p class="text-muted mb-0" style="position: relative; left: 30px;">Invoice Number : 788152</p>
                                    <p class="text-muted mb-0" style="position: relative; right: 30px;"><span class="fw-bold me-4">Total</span> RM 1999.90</p>
                                </div>

                                <div class="d-flex justify-content-between">
                                    <p class="text-muted mb-0" style="position: relative; left: 30px;">Invoice Date : 23 Aug,2024</p>
                                    <p class="text-muted mb-0" style="position: relative; right: 30px;"><span class="fw-bold me-4">Delivery Charges</span> RM 5.90</p>
                                </div>
                                <br />


                                <div class="card-footer border-0 px-4 py-5"
                                    style="background-color: #a8729a; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px;">
                                    <h5 class="d-flex align-items-center justify-content-end text-white text-uppercase mb-0">Total paid: <span class="h2 mb-0 ms-2">RM 2005.80</span></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</main>
        </div>
    </div>

   <script>
       document.addEventListener('DOMContentLoaded', function () {
           var menuItems = document.querySelectorAll('.sidebar a');

           menuItems.forEach(function (item) {
               var currentPage = window.location.pathname;
               var linkPage = item.getAttribute('href');

               if (currentPage.includes(linkPage)) {
                   item.classList.add('active');
               } else {
                   item.classList.remove('active');
               }
           });
       });


   </script>

</asp:Content>
