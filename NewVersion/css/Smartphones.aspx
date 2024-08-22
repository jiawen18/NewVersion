<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Smartphones.aspx.cs" Inherits="NewVersion.css.Smartphones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Start Hero Section -->
<div class="hero">
	<div class="container">
		<div class="row justify-content-between">
				<div class="col-lg-5">
					<div class="intro-excerpt">
						<h1>Smartphones</h1>
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
    <h2>Galaxy Z flip Series</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/z-flip_blue-removebg-preview.png" class="img-fluid product-thumbnail" alt="Galaxy Z flip 3">
        <h3 class="product-title">Galaxy Z flip 3</h3>
        <strong class="product-price">RM 1200.89</strong>
        <del class="product-old-price">RM 2200.89</del>
       
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
            <span class="rating-text">4.8 (634)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button1" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxy_z_fold6-removebg-preview.png" class="img-fluid product-thumbnail" alt="Galaxy Z fold 6">
        <h3 class="product-title">Galaxy Z fold 6</h3>
        <strong class="product-price">RM 7299.89</strong>
        <del class="product-old-price">RM 8100.89</del>
        
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
            <span class="rating-text">4.4 (334)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button2" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxy_z_fold5-removebg-preview.png" class="img-fluid product-thumbnail" alt="Galaxy Z fold 5">
        <h3 class="product-title">Galaxy Z fold 5</h3>
        <strong class="product-price">RM 8299.00</strong>
        <del class="product-old-price">RM 8500.89</del>

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
            <span class="rating-text">4.6 (234)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button3" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->

<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy S Series</h2>
</div>
<!-- End Section Title -->

<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxy_S24_Ultra.png" class="img-fluid product-thumbnail" alt="Galaxy S24 Ultra">
        <h3 class="product-title">Galaxy S24 Ultra</h3>
        <strong class="product-price">RM 3200.89</strong>
        <del class="product-old-price">RM 3400.89</del>
       
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
            <span class="rating-text">4.7 (4334)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
             <asp:Button ID="Button4" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->


<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxy_S24.png" class="img-fluid product-thumbnail" alt="Galaxy S24">
        <h3 class="product-title">Galaxy S24 </h3>
        <strong class="product-price">RM 2200.89</strong>
        <del class="product-old-price">RM 2400.89</del>

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
            <span class="rating-text">4.9 (3434)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button5" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyS22.png" class="img-fluid product-thumbnail" alt="Galaxy S22">
        <h3 class="product-title">Galaxy S22</h3>
        <strong class="product-price">RM 2360.89</strong>
        <del class="product-old-price">RM 2567.89</del>

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
            <span class="rating-text">4.9 (5634)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button6" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->


<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyS23.png" class="img-fluid product-thumbnail" alt="Galaxy S23 FE">
        <h3 class="product-title">Galaxy S23 FE</h3>
        <strong class="product-price">RM 3690.89</strong>
        <del class="product-old-price">RM 4350.89</del>
       
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
            <span class="rating-text">4.4 (454)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button7" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->


<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyS23plus.png" class="img-fluid product-thumbnail" alt="Galaxy S23+">
        <h3 class="product-title">Galaxy S23+</h3>
        <strong class="product-price">RM 4652.89</strong>
        <del class="product-old-price">RM 4786.09</del>

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
            <span class="rating-text">4.7 (234)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button8" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyS21plus.png" class="img-fluid product-thumbnail" alt="Galaxy S21+">
        <h3 class="product-title">Galaxy S21+ </h3>
        <strong class="product-price">RM 2340.89</strong>
        <del class="product-old-price">RM 2567.09</del>

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
            <span class="rating-text">4.9 (3234)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button9" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->


<br />
<!-- Start Section Title -->
<div class="section-title-container">
    <h2>Galaxy A55 | A35 5G Series</h2>
</div>
<!-- End Section Title -->
<!-- Start Products Row -->
<div class="products-row row">
<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyA35.png" class="img-fluid product-thumbnail" alt="Galaxy A35 5G">
        <h3 class="product-title">Galaxy A35 5G</h3>
        <strong class="product-price">RM 2312.89</strong>
        <del class="product-old-price">RM 2435.59</del>
       
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
            <span class="rating-text">4.7 (464)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button10" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->


<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyA55_5G.png" class="img-fluid product-thumbnail" alt="Galaxy A55 5G">
        <h3 class="product-title">Galaxy A55 5G</h3>
        <strong class="product-price">RM 3234.89</strong>
        <del class="product-old-price">RM 3542.89</del>

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
            <span class="rating-text">4.6 (234)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button11" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->

<!-- Start Grey Container -->
<div class="col-12 col-md-4 col-lg-3 mb-5 grey-container">

        <img src="images/galaxyA05.png" class="img-fluid product-thumbnail" alt="Galaxy A05">
        <h3 class="product-title">Galaxy A05</h3>
        <strong class="product-price">RM 1240.89</strong>
        <del class="product-old-price">RM 1321.69</del>

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
            <span class="rating-text">4.3 (294)</span> <!-- Rating score and number of ratings -->
        </div>

        <!-- Buy Now Button -->
        <div class="buyNow">
            <asp:Button ID="Button12" runat="server" Text="Buy Now"  CssClass="buyNow-btn" OnClick="btnBuyNow_Click"/>
        </div>

</div>
<!-- End Grey Container -->
</div>
<!-- End Products Row -->
</asp:Content>
