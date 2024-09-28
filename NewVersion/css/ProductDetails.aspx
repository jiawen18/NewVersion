<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="NewVersion.css.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:SqlDataSource
    ID="SqlDataSource1"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:productConnectionString %>"
    SelectCommand="SELECT r.ReviewID, r.ReviewDate, r.ReviewRating, r.ReviewImage, r.ReviewDescription, p.ProductName, p.Price, p.ProductImageURL 
                   FROM Review r
                   INNER JOIN Product p ON r.ProductID = p.ProductID WHERE p.ProductID = @ProductID">
</asp:SqlDataSource>

    <style>
        #divSuccessMessage {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: rgba(0, 128, 0, 0.7);
            color: white;
            padding: 20px;
            border-radius: 5px;
            z-index: 1000;
        }

        .add-to-cart-btn {
    background-color: #28a745;
    color: #fff;
    border: none;
    padding: 10px 20px;
    font-size: 16px;
    cursor: pointer;
}

.add-to-cart-btn:hover {
    background-color: #218838;
}

    </style>



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
        <asp:HyperLink ID="hplBackHome1" runat="server" CssClass="no-underline" style="transform: translate(100px, 60px);" NavigateUrl="Smartphones.aspx">< </asp:HyperLink>
        <div class="container">
            <div class="row">

<div class="product-detail">
    <div class="container">
        <div class="row" style="display: flex; flex-wrap: wrap; gap: 20px;"> 

            <!-- Product Image Carousel -->
            <div class="col-lg-5" style="flex: 1; min-width: 300px;"> 
                <div class="product-image-carousel" style="display: flex; flex-direction: column; gap: 10px;"> 
                    <div><asp:Image ID="ProductImg1" runat="server" style="width: 100%; height: auto;" /></div> 
                    <div><asp:Image ID="ProductImg2" runat="server" style="width: 100%; height: auto;" /></div>
                    <div><asp:Image ID="ProductImg3" runat="server" style="width: 100%; height: auto;" /></div>
                </div>
            </div>


<!-- Product Information -->
<div class="col-lg-7">
    <div class="product-info" style="padding: 20px; background-color: #ffffff;">
        <br />
        <!-- Product Name -->
        <div class="product-name-container" style="margin-bottom: 15px;">
            <h3 class="heading-large" style="font-size: 20px; margin-bottom: 8px; color: #333;">Product Name:</h3>
            <asp:Label ID="lblProductName" runat="server" CssClass="product-name-label" style="font-size: 18px; color: #555;"></asp:Label>
        </div>

        <!-- Storage Selection -->
        <div class="storage-container" style="margin-bottom: 20px;">
            <h3 class="heading-large" style="font-size: 20px; margin-bottom: 5px; color: #333;">Please select your storage:</h3>
            <h3 class="heading-small" style="font-size: 16px; margin-bottom: 8px; color: #666;">Select Storage</h3>
            <div class="storage-selection" style="display: flex; gap: 10px; flex-wrap: wrap;">
                <asp:Button ID="btnStorage1" runat="server" Text="256GB | 12GB" CssClass="storage-button" OnClick="btnStorage_Click" style="padding: 10px 15px; border: 1px solid #ddd; border-radius: 5px; background-color: #f4f4f4; color: #333; cursor: pointer;" />
                <asp:Button ID="btnStorage2" runat="server" Text="128GB | 12GB" CssClass="storage-button" OnClick="btnStorage_Click" style="padding: 10px 15px; border: 1px solid #ddd; border-radius: 5px; background-color: #f4f4f4; color: #333; cursor: pointer;" />
                <asp:Button ID="btnStorage3" runat="server" Text="64GB | 12GB" CssClass="storage-button" OnClick="btnStorage_Click" style="padding: 10px 15px; border: 1px solid #ddd; border-radius: 5px; background-color: #f4f4f4; color: #333; cursor: pointer;" />
            </div>
<<<<<<< Updated upstream
        </div>

        <!-- Color Selection -->
        <div id="colorContainer" class="color-container" runat="server"  style="display: none;">
            <h3 class="heading-large">Now select your color:</h3>
            <h3 class="heading-small">Select Color</h3>
            <div class="color-selection">
                <asp:Button ID="ColorButton1" runat="server" CssClass="color-button" OnClick="ColorButton_Click"  style="background-color: #dcf5fc;" value="Blue"/>
                <asp:Button ID="ColorButton2" runat="server" CssClass="color-button" OnClick="ColorButton_Click"  style="background-color: #fffdcf;" value="Yellow"/>
                <asp:Button ID="ColorButton3" runat="server" CssClass="color-button" OnClick="ColorButton_Click"  style="background-color: #000000;" value="Black"/>
=======
            <asp:Label ID="lblSelectedStorage" runat="server" CssClass="storage-label" style="display: block; margin-top: 10px; font-size: 14px; color: #666;"></asp:Label>
        </div>

        <!-- Color Selection -->
        <div id="colorContainer" class="color-container" runat="server" style="display: none; margin-bottom: 20px;">
            <h3 class="heading-large" style="font-size: 20px; margin-bottom: 5px; color: #333;">Now select your color:</h3>
            <h3 class="heading-small" style="font-size: 16px; margin-bottom: 8px; color: #666;">Select Color</h3>
            <div class="color-selection" style="display: flex; gap: 10px; flex-wrap: wrap;">
                <asp:Button ID="ColorButton1" runat="server" CssClass="color-button" OnClick="ColorButton_Click" style="padding: 10px 15px; border: 1px solid #ddd; border-radius: 5px; background-color: #dcf5fc; color: #333; cursor: pointer;" value="Blue" />
                <asp:Button ID="ColorButton2" runat="server" CssClass="color-button" OnClick="ColorButton_Click" style="padding: 10px 15px; border: 1px solid #ddd; border-radius: 5px; background-color: #fffdcf; color: #333; cursor: pointer;" value="Yellow" />
                <asp:Button ID="ColorButton3" runat="server" CssClass="color-button" OnClick="ColorButton_Click" style="padding: 10px 15px; border: 1px solid #ddd; border-radius: 5px; background-color: #000000; color: #fff; cursor: pointer;" value="Black" />
            </div>
            <asp:Label ID="lblSelectedColor" runat="server" CssClass="color-label" style="display: block; margin-top: 10px; font-size: 14px; color: #666;"></asp:Label>
        </div>

        <!-- Price Container -->
        <div class="price-container" style="margin-bottom: 15px;">
            <asp:Label ID="lblPrice" runat="server" style="font-size: 18px; font-weight: bold; color: #333;"></asp:Label>
        </div>

        <!-- Quantity Container -->
        <div class="quantity-container" style="margin-bottom: 15px;">
            <h3 class="heading-large" style="font-size: 20px; margin-bottom: 8px; color: #333;">Select Quantity:</h3>
            <div style="display: flex; align-items: center; gap: 10px;">
                <asp:Label ID="lblQuantity" runat="server" Text="Quantity:" style="font-size: 16px; color: #555;"></asp:Label>
                <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="quantity-dropdown" style="padding: 8px; font-size: 16px; border: 1px solid #ddd; border-radius: 5px; color: #333;">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                </asp:DropDownList>
>>>>>>> Stashed changes
            </div>
        </div>

        <!-- Add to Cart Button -->
        <div class="AddToCart" style="text-align: center; margin-top: 20px;">
            <asp:Button ID="btnAddToCart" runat="server" CssClass="add-to-cart-btn" Text="Add to Cart" OnClick="btnAddToCart_Click" style="padding: 12px 20px; font-size: 16px; color: #fff; background-color: #6c757d; border: none; border-radius: 5px; cursor: pointer; transition: background-color 0.3s;" />
        </div>

        <!-- Debug Information -->
        <asp:Label ID="lblDebugInfo" runat="server" ForeColor="Red" style="display: block; margin-top: 15px; font-size: 14px;"></asp:Label>

        <!-- Success Message -->
        <div id="divSuccessMessage" runat="server" style="margin-top: 10px;">
            <span>
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" style="font-size: 14px; color: green;"></asp:Label>
            </span>
        </div>
    </div>
</div>                

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

    <script type="text/javascript">
        
        function updateSelection(storage, color) {
            
            console.log('Selected Storage: ' + storage);
            console.log('Selected Color: ' + color);
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

    <script>setTimeout(function () {
            console.log('Hiding the success message'); // 检查是否到达此处
            div.style.display = 'none';
        }, 10000);
    </script>


</asp:Content>
