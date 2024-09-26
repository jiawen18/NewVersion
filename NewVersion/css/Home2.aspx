<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Home2.aspx.cs" Inherits="NewVersion.css.Home2" %>

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

        <!-- Start advertisement "hero" Section -->
    <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Next-Gen Tech at Your Fingertips! </h1>
                        <p class="mb-4">Discover the latest gadgets designed to elevate your digital experience. Shop now for cutting-edge devices that keep you ahead of the curve.</p>
                        <p>
                            <a href="Smartphones.aspx" class="btn btn-secondary me-2">Shop Now</a>
                            <a href="AboutUs.aspx" class="btn btn-white-outline">Explore</a>
                        </p>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="hero-img-wrap">
                        <img src="images/slide_image1.png" class="images">
                    </div>
                </div>
            </div>
        </div>
    </div>
	<!-- End advertisement Section -->
   

	<!-- SECTION -->
	<div class="section">
		<!-- container -->
		<div class="container">
			<!-- row -->
			<div class="row">

				<!-- section title -->
				<div class="col-md-12">
					<div class="section-title">
						<h3 class="title">Top selling</h3>
						<div class="section-nav">
							<ul class="section-tab-nav tab-nav">
								<li><a data-toggle="tab" href="#tab2">Smartphones</a></li>
							</ul>
						</div>
					</div>
				</div>
				<!-- /section title -->

                <div class="row">
                    <div class="products-tabs">
                        <div id="tab2" class="tab-pane active">
                            <div class="products-slick" data-nav="#slick-nav-1" style="display: flex; flex-wrap: wrap; justify-content: space-between;">
                                <div class="row" style="display: flex; flex-wrap: wrap; width: 100%;">
                                    <asp:Repeater ID="productRepeater" runat="server" >
                                        <ItemTemplate>
                                            <div class="col-md-4" style="flex: 1 1 30%; box-sizing: border-box; margin-bottom: 20px;">
                                                <div class="product" style="height: 100%;">
                                                    <div class="product-img">
                                                        <img src='<%# Eval("ProductImageURL") %>' alt="image" style="width: 100%; height: 200px; object-fit: cover;">
                                                        <div class="product-label">
                                                            <span class="sale">-30%</span>
                                                            <span class="new">NEW</span>
                                                        </div>
                                                    </div>
                                                    <div class="product-body" style="flex-grow: 1; display: flex; flex-direction: column; justify-content: space-between;">
                                                        <h3 class="product-name"><%# Eval("ProductName") %></h3>
                                                        <h4 class="product-price">RM <%# Eval("Price") %></h4>
                                                        <div class="product-rating">
                                                            <i class="fa fa-star"></i>
                                                            <i class="fa fa-star"></i>
                                                            <i class="fa fa-star"></i>
                                                            <i class="fa fa-star"></i>
                                                            <i class="fa fa-star"></i>
                                                        </div>
                                                    </div>
                                                    <div class="add-to-cart" style="margin-top: auto;">
                                                        <asp:HiddenField ID="hfProductId" runat="server" Value='<%# Eval("ProductID") %>' />
                                                        <asp:Button ID="btnBuyNow" runat="server" CssClass="add-to-cart-btn" 
                                                            Text="Buy Now" CommandArgument='<%# Eval("ProductID") %>' 
                                                            OnClick="btnBuyNow_Click" 
                                                            style="background-color: darkred; color: white; border: none; padding: 10px 20px; cursor: pointer;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>				

	<!-- HOT DEAL SECTION -->
	<div id="hot-deal" class="section">
		<!-- container -->
		<div class="container">
    <!-- row -->
		<div class="row">
						<div class="col-md-12">
							<div class="hot-deal">
								<ul class="hot-deal-countdown">
									<li>
										<div>
											<h3>02</h3>
											<span>Days</span>
										</div>
									</li>
									<li>
										<div>
											<h3>10</h3>
											<span>Hours</span>
										</div>
									</li>
									<li>
										<div>
											<h3>34</h3>
											<span>Mins</span>
										</div>
									</li>
									<li>
										<div>
											<h3>60</h3>
											<span>Secs</span>
										</div>
									</li>
								</ul>
								<h2 class="text-uppercase">hot deal this week</h2>
								<p>New Collection Up to 50% OFF</p>
								<a class="primary-btn cta-btn" href="Smartphones.aspx">Shop now</a>
							</div>
						</div>
		</div>
	<!-- /row -->
		</div>
		<!-- /container -->
	</div>
	<!-- /HOT DEAL SECTION -->
   

<!-- Start Product Section -->
<div class="product-section">
	<div class="container">
    <div class="row">

			    <!-- Start Column 1 -->
			    <div class="col-md-12 col-lg-3 mb-5 mb-lg-0">
				    <h2 class="mb-4 section-title">New Arrival</h2>
				    <p class="mb-4">Discover the latest arrivals from Hansumg, including the sleek Hansumg Z Flip, advanced Hansumg Ear Buds, and versatile Tabs. Elevate your tech experience with our cutting-edge products, designed to fit your lifestyle and enhance everyday moments. </p>
				    <p><a href="Smartphones.aspx" class="btn">Explore</a></p>
			    </div> 
			    <!-- End Column 1 -->

			    <!-- Start Column 2 -->
			    <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
				    <a class="product-item" href="ProductDetails.aspx">
					    <img src="images/z-flip_blue.jpg" class="image-fluid product-thumbnail">
					    <h3 class="product-title"> Hamsung Galaxy Z Flip 6</h3>
					    <strong class="product-price">RM 4499.00</strong>

					    <span class="icon-cross">
						    <img src="images/cross.svg" class="img-fluid">
					    </span>
				    </a>
			    </div> 
			    <!-- End Column 2 -->

			    <!-- Start Column 3 -->
			    <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
				    <a class="product-item" href="ProductDetails.aspx">
					    <img src="images/samsung_tabS9_FE.jpg" class="image-fluid product-thumbnail">
					    <h3 class="product-title"> Hansumg Tab S9 FE</h3>
					    <strong class="product-price">RM 1799.00</strong>

					    <span class="icon-cross">
						    <img src="images/cross.svg" class="img-fluid">
					    </span>
				    </a>
			    </div>
			    <!-- End Column 3 -->

			    <!-- Start Column 4 -->
			    <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
				    <a class="product-item" href="ProductDetails.aspx">
					    <img src="images/Galaxy_Buds3.jpg" class="image-fluid product-thumbnail">
					    <h3 class="product-title">Galaxy Buds 3</h3>
					    <strong class="product-price">RM 499.00</strong>

					    <span class="icon-cross">
						    <img src="images/cross.svg" class="img-fluid">
					    </span>
				    </a>
			    </div>
			    <!-- End Column 4 -->

</div>
	</div>
</div>
<!-- End Product Section -->


<!-- Start We Help Section -->
<div class="testimonial-section before-footer-section" style="padding:30px 0 !important">
	<div class="container">
     <div class="row justify-content-between">
     <div class="col-lg-7 mb-5 mb-lg-0">
         <div class="imgs-grid">
             <div class="grid grid-1"><img src="images/AI_image.png" alt="Hansumg.com" style="width: 99%; height: auto;" ></div>
         </div>
     </div>
     <div class="col-lg-5 ps-lg-5">
         <h2 class="section-title mb-4">How We Can Help You</h2>
         <p>We help you find the latest and most innovative electrical appliances, including phones, earbuds, and tablets. Discover cutting-edge technology and elevate your everyday life with our carefully selected range.</p>

         <ul class="list-unstyled custom-list my-4">
             <li><b>Product Consultation</b></li>
             <li><b>Tech Support</b></li>
			 <li><b>Personalized Recommendations</b></li>
             <li><b>Warranty and Repairs</b></li>
         </ul>
         <p><a href="AboutUs.aspx" class="btn">Explore</a></p>
     </div>
 </div>
   	</div>
</div>
<!-- End We Help Section -->

   
	    
<!-- Start Testimonial Slider -->
<div class="testimonial-section">
    <div class="container">
        <div class="row">
            <div class="col-lg-7 mx-auto text-center">
                <h2 class="section-title">Featured Reviews</h2>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="testimonial-slider-wrap text-center">

                    <div class="testimonial-slider">

                        <div class="item">
                            <div class="row justify-content-center">
                                <div class="col-lg-8 mx-auto">

                                    <div class="testimonial-block text-center">
                                        <blockquote class="mb-5">
                                            <p>&ldquo;I recently bought the latest smartphone from this store, and it’s exceeded all my expectations. The camera quality is incredible, and the battery life is fantastic. I couldn’t be happier with my purchase!&rdquo;</p>
                                        </blockquote>

                                        <div class="author-info">
                                            <h3 class="font-weight-bold">Maria Jones</h3>
                                            <span class="position d-block mb-3">CEO, Co-Founder, XYZ Inc.</span>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- END item -->

                        <div class="item">
                            <div class="row justify-content-center">
                                <div class="col-lg-8 mx-auto">

                                    <div class="testimonial-block text-center">
                                        <blockquote class="mb-5">
                                            <p>&ldquo;This tablet has been a game-changer for me. Whether it’s for reading, watching videos, or browsing the web, it handles everything with ease. I’m very pleased with my purchase.&rdquo;</p>
                                        </blockquote>

                                        <div class="author-info">
                                            <h3 class="font-weight-bold">John Doe</h3>
                                            <span class="position d-block mb-3">CTO, ABC Ltd.</span>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- END item -->

                        <div class="item">
                            <div class="row justify-content-center">
                                <div class="col-lg-8 mx-auto">

                                    <div class="testimonial-block text-center">
                                        <blockquote class="mb-5">
                                            <p>&ldquo;I’m very happy with the accessories I purchased. The charging cables and protective cases are high-quality and work great. The customer service was excellent, and shipping was fast.&rdquo;</p>
                                        </blockquote>

                                        <div class="author-info">
                                            <h3 class="font-weight-bold">Linda Smith</h3>
                                            <span class="position d-block mb-3">Manager, DEF Corp.</span>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- END item -->

                    </div>
                    <!-- END testimonial-slider -->

                </div>
            </div>
        </div>
    </div>
</div>
<!-- End Testimonial Slider -->

<!-- Include Slick CSS -->
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.css"/>
<link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.min.css"/>

<!-- Include jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<!-- Include Slick JS -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.js"></script>

<script>
    $(document).ready(function () {
        $('.testimonial-slider').slick({
            dots: true,
            infinite: true,
            speed: 300,
            slidesToShow: 1,
            slidesToScroll: 1,
            prevArrow: '<span class="prev" data-controls="prev"><span class="fa fa-chevron-left"></span></span>',
            nextArrow: '<span class="next" data-controls="next"><span class="fa fa-chevron-right"></span></span>',
        });
    });
</script>





<script>
    // Set the date and time we're counting down to
    var countDownDate = new Date("Oct 15, 2024 23:59:59").getTime();

    // Update the countdown every 1 second
    var countdownFunction = setInterval(function () {

        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the countdown date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes, and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Display the result in the respective HTML elements
        document.querySelector(".hot-deal-countdown li:nth-child(1) h3").innerHTML = days;
        document.querySelector(".hot-deal-countdown li:nth-child(2) h3").innerHTML = hours;
        document.querySelector(".hot-deal-countdown li:nth-child(3) h3").innerHTML = minutes;
        document.querySelector(".hot-deal-countdown li:nth-child(4) h3").innerHTML = seconds;

        // If the countdown is finished, display a message
        if (distance < 0) {
            clearInterval(countdownFunction);
            document.querySelector(".hot-deal-countdown").innerHTML = "EXPIRED";
        }
    }, 1000);
</script>
</asp:Content>
