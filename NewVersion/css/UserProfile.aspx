<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="NewVersion.css.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="user-container">


        <!-- User Profile -->
        <div class="hero">
            <div class="wrapper-user" style="padding: 0px;">
                <div style="display: flex;">
                    <div class="user_pic" style="padding: 0px;">
                        <div class="pro-img">
                            <img src="https://i.imgur.com/8RKXAIV.jpg" alt="user">
                        </div>
                    </div>
                    <div class="user_details">
                        <h3>Shen</h3>
                        <p>Email: Kelvinchong0457@gmail.com</p>
                        <a href="UserProfile.aspx">Edit Information ></a>
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

        <a href="Address.aspx">
            <div class="menu-col">
                <img src="images/location-icon.png" />
                <strong style="font-size: 15px; padding: 5px;">Address Book</strong>
                <p>Manager our address for delivery</p>
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
                <div class="col-md-12">
                    <div class="row">
                        <div class="products-tabs">
                            <!-- tab -->
                            <div id="tab2" class="tab-pane fade in active">
                                <div class="products-slick" data-nav="#slick-nav-1">
                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <div class="product-label">
                                                <span class="sale">-30%</span>
                                                <span class="new">NEW</span>
                                            </div>
                                            <a href="#">
                                                <img src="images/zflips.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy Z flip 3</a></h3>
                                            <h4 class="product-price">RM 895.95 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 1200.89</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/galaxyA55.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="new">NEW</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy A55 5G</a></h3>
                                            <h4 class="product-price">RM1999.90 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 2550.79</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/galaxyS23Ultra.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="sale">-20%</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy S23 Ultra</a></h3>
                                            <h4 class="product-price">RM 5299.00 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 5700.89</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/galaxyS21Ultra.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy S21 Ultra 5G</a></h3>
                                            <h4 class="product-price">RM 3400.95</h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 3790.79</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/galaxyNote20.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy Note 20 5G</a></h3>
                                            <h4 class="product-price">RM 2250.98</h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 2509.87</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->
                                </div>
                            </div>
                            <!-- /tab -->

                            <!-- tab -->
                            <div id="tab3" class="tab-pane fade">
                                <div class="products-slick" data-nav="#slick-nav-1">
                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/tab_S9.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="sale">-30%</span>
                                                <span class="new">NEW</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Samsung Galaxy Tab S9 FE (Wi-Fi)</a></h3>
                                            <h4 class="product-price">RM 2099.98 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 2309.78</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/samsung_tab9.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="sale">-30%</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy Tab A9</a></h3>
                                            <h4 class="product-price">RM 699.00 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 812.89</del></h4>
                                        </div>
                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/tab_S9plus.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="new">NEW</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#"></a>Galaxy Tab S9 FE+></h3>
                                            <h4 class="product-price">RM3399.00 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 3500.98</del></h4>

                                        </div>

                                    </div>
                                    <!-- /product -->

                                </div>
                            </div>
                            <!-- /tab -->


                            <!-- tab -->
                            <div id="tab4" class="tab-pane fade">
                                <div class="products-slick" data-nav="#slick-nav-1">
                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/Samsung_Galaxy_Buds_Pro.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="sale">-30%</span>
                                                <span class="new">NEW</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Samsung Galaxy Buds Pro </a></h3>
                                            <h4 class="product-price">RM 799.95 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 1050.89</del></h4>


                                        </div>

                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/Samsung_Galaxy_Buds_Live.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="new">NEW</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Samsung Galaxy Buds Live</a></h3>
                                            <h4 class="product-price">RM269.90 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 279.79</del></h4>

                                        </div>

                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/Samsung_Galaxy_Watch_5.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                            <div class="product-label">
                                                <span class="sale">-30%</span>
                                            </div>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Samsung Galaxy Watch 5</a></h3>
                                            <h4 class="product-price">RM 1299.00 </h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 1500.89</del></h4>

                                        </div>

                                    </div>
                                    <!-- /product -->

                                    <!-- product -->
                                    <div class="product">
                                        <div class="product-img">
                                            <a href="#">
                                                <img src="images/GalaxyS24Case.jpg" alt="image" style="width: 350px; height: 300px;"></a>
                                        </div>
                                        <div class="product-body">
                                            <h3 class="product-name"><a href="#">Galaxy S24+ Case</a></h3>
                                            <h4 class="product-price">RM 189.95</h4>
                                            <h4 class="product-price"><del class="product-old-price">RM 220.79</del></h4>

                                        </div>
                                    </div>

                                    <!-- /product -->
                                </div>
                            </div>
                            <!-- /tab -->
                            <div id="slick-nav-2" class="products-slick-nav">
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
    </div>
</asp:Content>
