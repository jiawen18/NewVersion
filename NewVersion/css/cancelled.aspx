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
                       
            </div>
        </div>
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
                 Cancelled <img src="images/cancel-order.png" /></h2>
    <%-- Content Section --%>
    <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
            <div class="col-lg-10 col-xl-8">
                <div class="card" style="border-radius: 10px;">
                    <div class="card-header px-4 py-5">
                        <h5 class="text-muted mb-0">HanSumg.</h5>
                    </div>

                    <div class="card-body p-4">
                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <p class="lead fw-normal mb-0" style="color: #a8729a;">Order</p>
                            <p class="small text-muted mb-0">Order ID : ORD12345</p>
                        </div>

                        <div class="border">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-2">
                                        <img src="images/GalaxyA.png" style="max-width: 80px; max-height: 175px;" />

                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Samsung Galaxy A55</p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Color：Mix</p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Capacity: 64GB</p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">Qty: 1</p>
                                    </div>
                                    <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                        <p class="text-muted mb-0">RM1999.90</p>
                                    </div>
                                </div>
                                <hr class="mb-4" style="background-color: #e0e0e0; opacity: 1;">
                                <div class="row d-flex align-items-center">

                                    <div class="trackAndReview">
                                        <asp:Button class="text-muted mb-0 small" ID="btnBuyAgain" runat="server" Text="Buy Again" OnClick="btnBuyAgain_Click" />
                                        &nbsp&nbsp&nbsp&nbsp
                                        <asp:Button class="text-muted mb-0 small" ID="btnTrackRefund" runat="server" Text="Track Refund" OnClick="btnTrackRefund_Click" />
                                    </div>
                                </div>
                            </div>


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
