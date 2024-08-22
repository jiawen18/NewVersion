<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Accessories.aspx.cs" Inherits="NewVersion.css.Accessories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!-- Start Hero Section -->
<div class="hero">
	<div class="container">
		<div class="row justify-content-between">
				<div class="col-lg-5">
					<div class="intro-excerpt">
						<h1>Accessories</h1>
					</div>
				</div>
				<div class="col-lg-7">
					
				</div>
		</div>
	</div>
</div>
<!-- End Hero Section -->


<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy Buds</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyBuds3Pro.png" class="img-fluid product-thumbnail" alt="Galaxy Buds 3 Pro</">
        <h3 class="product-title">Galaxy Buds 3 Pro</h3>
        <strong class="product-price">RM 999.00</strong>
        <del class="product-old-price">RM 1100.00</del>
       
        <br />
        
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.9 (214)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyBuds3.png" class="img-fluid product-thumbnail" alt="Galaxy Buds 3">
        <h3 class="product-title">Galaxy Buds 3</h3>
        <strong class="product-price">RM 599.00</strong>
        <del class="product-old-price">RM 799.00</del>
        
        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.7 (397)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyBudsFE.png" class="img-fluid product-thumbnail" alt="Galaxy Buds FE+">
        <h3 class="product-title">Galaxy Buds F+</h3>
        <strong class="product-price">RM 399.00</strong>
        <del class="product-old-price">RM 599.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.8 (578)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->

<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy Watch</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyWatchUltra.png" class="img-fluid product-thumbnail" alt="Galaxy Watch Ultra">
        <h3 class="product-title">Galaxy Watch Ultra (LTE) 47mm</h3>
        <strong class="product-price">RM 3399.00</strong>
        <del class="product-old-price">RM 3500.80</del>
       
        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.8 (126)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->


<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyWatch7.png" class="img-fluid product-thumbnail" alt="Galaxy Watch 7">
        <h3 class="product-title">Galaxy Watch 7 </h3>
        <strong class="product-price">RM 1299.00</strong>
        <del class="product-old-price">RM 1599.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.9 (652)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton5" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyWatch6.png" class="img-fluid product-thumbnail" alt="Galaxy Watch 6">
        <h3 class="product-title">Galaxy Watch 6</h3>
        <strong class="product-price">RM 1399.00</strong>
        <del class="product-old-price">RM 1599.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.9 (294)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton6" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->


<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Other Accessories</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/watchStrap7.png" class="img-fluid product-thumbnail" alt="Galaxy Watch 7's Strap">
        <h3 class="product-title">Galaxy Watch 7's Strap</h3>
        <strong class="product-price">RM 3399.00</strong>
        <del class="product-old-price">RM 3500.80</del>
       
        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.8 (346)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton7" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->


<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/wirelessCharger.png" class="img-fluid product-thumbnail" alt="Wireless Charger">
        <h3 class="product-title">Wireless Charger </h3>
        <strong class="product-price">RM 355.00</strong>
        <del class="product-old-price">RM 490.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i>
        <i class="fa fa-star-o"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.4 (325)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton8" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/powerAdaptor.png" class="img-fluid product-thumbnail" alt="Power Adaptor">
        <h3 class="product-title">Power Adaptor </h3>
        <strong class="product-price">RM 89.00</strong>
        <del class="product-old-price">RM 120.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.9 (344)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton9" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyTab9_Cover.png" class="img-fluid product-thumbnail" alt="Galaxy S9 Tab Cover">
        <h3 class="product-title">Galaxy S9 Tab Cover</h3>
        <strong class="product-price">RM 120.00</strong>
        <del class="product-old-price">RM 140.00</del>
       
        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.9 (34)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton10" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->


<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyTab9_SPen.png" class="img-fluid product-thumbnail" alt="Galaxy S Pen">
        <h3 class="product-title">Galaxy S Pen </h3>
        <strong class="product-price">RM 565.00</strong>
        <del class="product-old-price">RM 620.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star-o"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.8 (652)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton11" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/starWarsClearCase.png" class="img-fluid product-thumbnail" alt="Galaxy Buds Case (Star Wars edition)">
        <h3 class="product-title">Galaxy Buds Case (Star Wars edition)</h3>
        <strong class="product-price">RM 120.00</strong>
        <del class="product-old-price">RM 154.00</del>

        <br />
        <!-- Product Rating -->
        <div class="product-ratings">
            <div class="star-rating">
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i>
        <i class="fa fa-star"></i> <!-- Empty star for incomplete rating -->
            </div>
            <span class="rating-text">4.9 (54)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:LinkButton ID="LinkButton12" runat="server" CssClass="buyNow-btn" OnClick="btnBuyNow_Click">
                 <i class="fa fa-shopping-cart"></i> Buy Now
            </asp:LinkButton>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->
</asp:Content>
