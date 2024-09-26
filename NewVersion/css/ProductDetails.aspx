<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="NewVersion.css.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:SqlDataSource
    ID="SqlDataSource1"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    SelectCommand="SELECT r.ReviewID, r.ReviewDate, r.ReviewRating, r.ReviewImage, r.ReviewDescription, p.ProductName, p.Price, p.ProductImageURL 
                   FROM Review r
                   INNER JOIN Product p ON r.ProductID = p.ProductID">
</asp:SqlDataSource>
    <!-- Start Hero Section -->
<div class="hero">
	<div class="container">
		<div class="row justify-content-between">
				<div class="col-lg-5">
					<div class="intro-excerpt">
						<h1>Product Details</h1>
					</div>
				</div>
				<div class="col-lg-7">
					
				</div>
		</div>
	</div>
</div>
<!-- End Hero Section -->




    <div class="product-detail">
        <!-- Product Image Carousel -->
        <div class="product-image-carousel">
            <div>
                <asp:Image ID="ProductImg" alt="Product Image 1" runat="server" ImageUrl="images/z-flip_blue.jpg"/></div>
            <div><img src="images/z-flip_blue.jpg" alt="Product Image 2"></div>
            <div><img src="images/z-flip_blue.jpg" alt="Product Image 3"></div>
        </div>

        <!-- Product Information -->
        <div class="product-info">
            <div class="product-desc">
                <h1 class="product-name">
                    <asp:Label ID="lblProductName" runat="server" Text="Z-Flip">Z-Flip</asp:Label>
                </h1>
                <p class="product-description">Product description goes here. This section provides details about the product, its features, and benefits.</p>
            </div>

        <!-- Storage Selection -->
        <div class="storage-container">
            <h3 class="heading-large">Please select your storage:</h3>
            <h3 class="heading-small">Select Storage</h3>
            <div class="storage-selection">
                <asp:Button ID="Button1" runat="server" Text="256GB | 12GB" CssClass="storage-button" OnClientClick="return selectStorage(this);" />
                <asp:Button ID="Button2" runat="server" Text="128GB | 12GB" CssClass="storage-button" OnClientClick="return selectStorage(this);" />
                <asp:Button ID="Button3" runat="server" Text="64GB | 12GB" CssClass="storage-button" OnClientClick="return selectStorage(this);" />
            </div>
        </div>

        <!-- Color Selection -->
        <div class="color-container" style="display: none;">
            <h3 class="heading-large">Now select your color:</h3>
            <h3 class="heading-small">Select Color</h3>
            <div class="color-selection">
                <asp:Button ID="ColorButton1" runat="server" CssClass="color-button" OnClientClick="return selectColor(this);" style="background-color: #dcf5fc;" Text="Blue"/>
                <asp:Button ID="ColorButton2" runat="server" CssClass="color-button" OnClientClick="return selectColor(this);" style="background-color: #fffdcf;" />
                <asp:Button ID="ColorButton3" runat="server" CssClass="color-button" OnClientClick="return selectColor(this);" style="background-color: #000000;" />
            </div>
        </div>

            <div class="price-container"> <asp:Label ID="lblPrice" runat="server" Text="200.50"></asp:Label></div>

            <div class="quantity-container"> <asp:Label ID="lblQuantity" runat="server" Text="1"></asp:Label></div>

            <asp:HiddenField ID="hiddenProductId" runat="server" Value="1" /> 

            <!-- Add to Cart Button -->
            <div class="AddToCart">
                <asp:Button ID="Button4" runat="server" CssClass="add-to-cart-btn" Text="Add to Cart" OnClick="Button4_Click"/>
            </div>
        </div>
    </div>

    <br />
    <br />

<!-- Customer Ratings -->
<div class="customer-ratings">
    <h2>Customer Ratings</h2>

    <asp:Panel ID="PanelFirstRating" runat="server">
    <div class="rating-box">
        <!-- Rating Entry -->
        <div class="rating-entry">
            <!-- Username -->
            <div class="rating-username">
                <strong>John Doe</strong>
            </div>

            <!-- Star Rating -->
            <div class="rating-star-fixed">
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star-half-alt"></i>
            </div>

            <!-- Product Model -->
            <h4 class="rating-model">Z-Flip 2024, Blue</h4>

            <!-- Comments -->
            <div class="rating-comments">
                The product is good. I really like the new features and the color.
            </div>

            <!-- Product Image Posted by User -->
            <div class="rating-image">
                <img src="images/z-flip_blue.jpg" alt="Customer Image">
            </div>

            <!-- Date of Rating -->
            <div class="rating-date">
                <small>Reviewed on: August 22, 2024</small>
            </div>
        </div>
        <!-- Repeat rating blocks as needed -->
    </div>
    </asp:Panel>

<asp:Panel ID="PanelMoreRatings" runat="server" Visible ="false">
        <div class="rating-box">
        <!-- Rating Entry -->
        <div class="rating-entry">
            <!-- Username -->
            <div class="rating-username">
                <strong>John Doe</strong>
            </div>

            <!-- Star Rating -->
            <div class="rating-star-fixed">
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star-half-alt"></i>
            </div>

            <!-- Product Model -->
            <h4 class="rating-model">Z-Flip 2024, Blue</h4>

            <!-- Comments -->
            <div class="rating-comments">
                The product is good. I really like the new features and the color.
            </div>

            <!-- Product Image Posted by User -->
            <div class="rating-image">
                <img src="images/z-flip_blue.jpg" alt="Customer Image">
            </div>

            <!-- Date of Rating -->
            <div class="rating-date">
                <small>Reviewed on: August 22, 2024</small>
            </div>
        </div>
        <!-- Repeat rating blocks as needed -->
    </div>


        <div class="rating-box">
        <!-- Rating Entry -->
        <div class="rating-entry">
            <!-- Username -->
            <div class="rating-username">
                <strong>John Doe</strong>
            </div>

            <!-- Star Rating -->
            <div class="rating-star-fixed">
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star"></i>
                <i class="fa fa-star-half-alt"></i>
            </div>

            <!-- Product Model -->
            <h4 class="rating-model">Z-Flip 2024, Blue</h4>

            <!-- Comments -->
            <div class="rating-comments">
                The product is good. I really like the new features and the color.
            </div>

            <!-- Product Image Posted by User -->
            <div class="rating-image">
                <img src="images/z-flip_blue.jpg" alt="Customer Image">
            </div>

            <!-- Date of Rating -->
            <div class="rating-date">
                <small>Reviewed on: August 22, 2024</small>
            </div>
        </div>
        <!-- Repeat rating blocks as needed -->
    </div>
</asp:Panel>
       
    <div class="view-more-container">
        <asp:Button ID="btnViewMore" runat="server" Text="View More Ratings" class="view-more-btn" OnClick="btnViewMore_Click" />
    </div>

</div>




    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.css">
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.js"></script>
    <!-- Scripts -->
    <script>
        $(document).ready(function () {
            $('.product-image-carousel').slick({
                dots: true,
                infinite: true,
                speed: 500,
                slidesToShow: 1,
                slidesToScroll: 1,
                prevArrow: '<button type="button" class="slick-prev">Previous</button>',
                nextArrow: '<button type="button" class="slick-next">Next</button>'
            });
        });
    </script>

<script type="text/javascript">
    function selectStorage(button) {
        event.preventDefault();

        var storageButtons = document.querySelectorAll('.storage-button');
        storageButtons.forEach(function (btn) {
            btn.classList.remove('selected');
        });

        button.classList.add('selected');

        document.querySelector('.color-container').style.display = 'block';
        return false;
    }

    function selectColor(button) {
        event.preventDefault();

        var colorButtons = document.querySelectorAll('.color-button');
        colorButtons.forEach(function (btn) {
            btn.classList.remove('selected');
        });

        button.classList.add('selected');
        return false; // Prevent further action
    }
</script>

<script>
    function toggleRatings(event) {
        event.preventDefault(); 

        var moreRatings = document.querySelectorAll('.more-ratings');
        moreRatings.forEach(function (rating) {
            rating.classList.toggle('hidden');
        });

        var button = document.querySelector('.view-more-btn');
        if (button.textContent === 'View More Ratings') {
            button.textContent = 'View Less Ratings';
        } else {
            button.textContent = 'View More Ratings';
        }
    }
</script>


</asp:Content>
