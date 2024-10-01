
<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="NewVersion.css.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:SqlDataSource 
        ID="SqlDataSource1" 
        runat="server" 
        ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
        ProviderName="System.Data.SqlClient"
        SelectCommand="
            SELECT 
                p.ProductID, 
                p.ProductName, 
                p.ProductImageURL, 
                p.Price, 
                AVG(r.ReviewRating) AS AverageRating 
            FROM 
                Product p 
            LEFT JOIN 
                Review r ON p.ProductID = r.ProductID 
            GROUP BY 
                p.ProductID, 
                p.ProductName, 
                p.ProductImageURL, 
                p.Price">
    </asp:SqlDataSource>
    <div class="user-container">


        <!-- User Profile -->
        <div class="hero">
            <div class="wrapper-user" style="padding: 0px;">
                <div style="display: flex; align-items:center">
                    <div class="user_pic" style="padding: 0px;">
                        <div class="pro-img">
                             <img src='<%= GetProfilePictureUrl() %>' alt="User Profile" class="avatar-img rounded-circle" />
                        </div>
                    </div>
                    <div class="user_details" style ="margin-left:30px">
                        <h3 style="margin:0"> <asp:Label ID="lbl_user_name" runat="server" Text=""></asp:Label></h3>
                        <p style="margin:0"><asp:Label ID="lbl_user_email" runat="server" Text=""></asp:Label></p>                                        
                    </div>
                </div>         
            </div>
        </div>
    </div>



    <!-- User Menu -->
    <div class="user-menu">
        <a href="Order.aspx">
            <div class="menu-col">
                <img src="images/order-icon.png" />
                <strong style="font-size: 15px; padding: 5px;">My Order</strong>
                <p>Track, Modify or Cancel an order</p>
            </div>
        </a>


        <a href="Account.aspx">
            <div class="menu-col">
                <img src="images/user-icon.png" />
                <strong style="font-size: 15px; padding: 5px;">My Account</strong>
                <p>Signing in or reset password</p>
            </div>
        </a>
    </div>


    <!-- Recommneded Products -->
    <h1 style="text-align: center">Recommended For You</h1>
    <!-- SECTION -->
    <div class="section">
        <!-- container -->
        <div class="container">
            <!-- row -->
            <div class="row">
                <!-- /section title -->

                <!-- Products tab & slick -->
                <div class="row">
                    <div class="products-tabs">
                        <div id="tab2" class="tab-pane active">
                            <div class="products-slick" data-nav="#slick-nav-1" style="display: flex; flex-wrap: wrap; justify-content: space-between;">
                                <div class="row" style="display: flex; flex-wrap: wrap; width: 100%; margin-bottom: 40px;">
                                    <asp:Repeater ID="productRepeater" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-4" style="flex: 1 1 30%; box-sizing: border-box; margin-bottom: 20px; position: relative;">
                                                <a href='ProductDetails.aspx?ProductID=<%# Eval("ProductID") %>' style="text-decoration: none; color: inherit; height: 100%;">
                                                    <div class="product" style="height: 100%; background-color: transparent;"> <!-- Added background-color -->
                                                        <div class="product-img">
                                                            <img src='<%# Eval("ProductImageURL") %>' alt="image" style="width: 100%; height: 200px; object-fit: cover;">
                                                        </div>
                                                        <div class="product-body" style="flex-grow: 1; display: flex; flex-direction: column; justify-content: space-between; background-color: transparent;"> <!-- Added background-color -->
                                                            <h3 class="product-name"><%# Eval("ProductName") %></h3>
                                                            <h4 class="product-price">RM <%# Eval("Price") %></h4>
                                                            <div class="product-rating">
                                                                <%# GetRatingStars(Eval("AverageRating")) %>
                                                            </div>      
                                                            <div class="add-to-cart" style="opacity: 0; transition: opacity 0.3s ease; margin-top: 10px; background-color: transparent;"> <!-- Added background-color -->
                                                                <asp:Button ID="Button1" runat="server" Text="Buy Now" CssClass="buyNow-btn" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnBuyNow_Click" style="background-color: darkred; color: white; border: none; padding: 10px 20px; cursor: pointer; width: 100%;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- /Products tab & slick -->
            </div>
            <!-- /row -->
        </div>
    </div>
    <!-- /container -->
    <!-- /SECTION -->
    
    <script>
    // Show button on hover
    const productColumns = document.querySelectorAll('.col-md-4');
    productColumns.forEach(column => {
        column.addEventListener('mouseover', () => {
            column.querySelector('.add-to-cart').style.opacity = '1';
        });
        column.addEventListener('mouseout', () => {
            column.querySelector('.add-to-cart').style.opacity = '0';
        });
    });
    </script>
    
</asp:Content>
