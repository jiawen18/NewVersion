<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="NewVersion.css.Order" %>

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
                    <asp:HyperLink ID="hplBackHome1" runat="server" CssClass="no-underline" NavigateUrl="UserProfile.aspx">< </asp:HyperLink>
                    To Ship <img src="images/truck.png" /></h2>
    <%-- Content Section --%>
        <div class="row d-flex justify-content-center align-items-center h-100" >
            <div class="col-lg-10 col-xl-8">
                
                <asp:Repeater ID="rptOrders" runat="server">
                    <HeaderTemplate>
                        <div class="card" style="border-radius: 10px;">
                            <div class="card-header px-4 py-5">
                                <h5 class="text-muted mb-0">Hansumg.</h5>
                            </div>
                     </HeaderTemplate>
                    <ItemTemplate>
                            <div class="card-body p-4">
                                <div class="d-flex justify-content-between align-items-center mb-4">
                                    <p class="lead fw-normal mb-0" style="color: #a8729a;">Order</p>
                                    <p > <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%# "Order ID: " + Eval("OrderID") %>' CssClass="small text-muted mb-0"></asp:Label></p>
                                </div>

                                <div class="border">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <img src='<%# Eval("ProductImage") %>' style="max-width: 80px; max-height: 175px;" />

                                            </div>
                                            <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                                <p> <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' CssClass="text-muted mb-0"></asp:Label></p>
                                            </div>
                                            <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                                <p><asp:Label ID="lblColor" runat="server" Text='<%# "Color: " + Eval("Color") %>' CssClass="text-muted mb-0"></asp:Label></p>
                                            </div>
                                            <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                                <p> <asp:Label ID="lblCapacity" runat="server" Text='<%# "Capacity:" + Eval("storage") %>' CssClass="text-muted mb-0"></asp:Label></p>
                                            </div>
                                            <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                                <p> <asp:Label ID="lblQuantity" runat="server" Text='<%# "Qty: " + Eval("Quantity") %>' CssClass="text-muted mb-0"></asp:Label></p>
                                            </div>
                                            <div class="col-md-2 text-center d-flex justify-content-center align-items-center">
                                                <p> <asp:Label ID="lblPrice" runat="server" Text='<%# "RM " + Eval("Price") %>' CssClass="text-muted mb-0"></asp:Label></p>
                                            </div>
                                        </div>
                                        <hr class="mb-4" style="background-color: #e0e0e0; opacity: 1;">
                                        <div class="row d-flex align-items-center">

                                            <div class="trackAndReview">
                                                <asp:Button class="text-muted mb-0 small" ID="btnCancel" runat="server" Text="Cancel" CommandArgument='<%# Eval("OrderID") %>' OnClick="btnCancel_Click" />
                                                &nbsp&nbsp&nbsp&nbsp
                                                <asp:Button class="text-muted mb-0 small" ID="btnTrack" runat="server" Text="Track Order"  OnClick="btnTrack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                  </ItemTemplate>

                                  <FooterTemplate>
                                    <!-- Collapsible section -->
                                    <div class="card-details">
                                        <div class="d-flex justify-content-between pt-2">
                                            <p class="text-muted mb-0" style="position: relative; left: 30px;">
                                                <asp:Label class="fw-bold mb-0" ID="lblOrderDetails" runat="server" Text="Order Details"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="d-flex justify-content-between pt-2">
                                            <p>
                                            <asp:Label ID="Label1" runat="server" Text='<%# " Invoice Number : " + Eval("InvoiceID") %>' CssClass="text-muted mb-0" style="position: relative; left: 30px;"></asp:Label>
                                            </p>
                                            <p >
                                            <asp:Label ID="lblPrice" runat="server" CssClass="text-muted mb-0" style="position: relative; right: 30px;"><span class="fw-bold me-4">Total</span>'<%# " RM " + Eval("TotalPrice") %>'</asp:Label>
                                        </p>
                                        </div>

                                        <div class="d-flex justify-content-between">
                                            <p>
                                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# "Invoice Date: " + Convert.ToDateTime(Eval("InvoiceDate")).ToString("MM dd yyyy") %>' CssClass="text-muted mb-0" style="position: relative; left: 30px;"></asp:Label>
                                            </p>
                                            <p>
                                            <asp:Label ID="lblDeliveryFee" runat="server" CssClass="text-muted mb-0" style="position: relative; right: 30px;"><span class="fw-bold me-4">Delivery Charges</span>'<%# " RM " + Eval("DeliveryFee") %>'</asp:Label>
                                        </p>
                                        </div>
                                        <br />


                                        <div class="card-footer border-0 px-4 py-5"
                                            style="background-color: #a8729a; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px;">
                                                <asp:Label ID="lblTotalPrice" runat="server" CssClass="d-flex align-items-center justify-content-end text-white text-uppercase mb-0"><span class="h2 mb-0 ms-2">'<%# "Total Paid: RM " + (Convert.ToDecimal(Eval("TotalPrice")) + Convert.ToDecimal(Eval("DeliveryFee"))).ToString("N2") %>'</span></asp:Label>
                                            </h5>
                                        </div>
                                    </div>

                                  </FooterTemplate>
                                 </asp:Repeater>
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
