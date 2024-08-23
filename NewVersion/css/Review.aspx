<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="NewVersion.css.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Start Hero Section -->
<div class="hero">
	<div class="container">
		<div class="row justify-content-between">
				<div class="col-lg-5">
					<div class="intro-excerpt">
						<h1>Review</h1>
					</div>
				</div>
				<div class="col-lg-7">
					
				</div>
		</div>
	</div>
</div>
<!-- End Hero Section -->

	<div class="review-container">
        <!-- Product Details -->
        <div class="prod-details">
            <div class="prod-image">
                <img src="images/z-flip_blue.jpg" alt="Product Image">
            </div>
            <div class="prod-info">
                <h3 class="prod-name">Z-flip 2024</h3>
                <p class="prod-details">Model: 258GB, Color: Navy Blue</p>
            </div>
        </div>

        <!-- Product Quality Rating -->
        <div class="rating-section">
            <h3>Product Quality</h3>
            <div class="rating-stars">
                <i class="fa fa-star" data-value="1"></i>
                <i class="fa fa-star" data-value="2"></i>
                <i class="fa fa-star" data-value="3"></i>
                <i class="fa fa-star" data-value="4"></i>
                <i class="fa fa-star" data-value="5"></i>
            </div>
        </div>

        <br />


        <!-- Media Uploads -->
        <div class="media-upload">

        <!-- Image upload -->
            <input type="file" id="FileUploadImage" name="FileUploadImage" class="file-upload-input" />
            <label for="FileUploadImage" class="upload-btn">
                <img src="images/camera.png" alt="Camera Icon" class="upload-icon" />
                Add Photo</label>
    
            <!-- Video upload -->
            <input type="file" id="FileUploadVideo" name="FileUploadVideo" class="file-upload-input" />
            <label for="FileUploadVideo" class="upload-btn">
                <img src="images/video-camera-alt.png" alt="Video Camera Icon" class="upload-icon" />
                Add Video</label>
    
            <asp:Button ID="Button3" runat="server" CssClass="upload-btn" Text="Submit" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" CssClass="UploadStatus" Text=""></asp:Label>
        </div>

        <br />
        

        <!-- Description -->
        <div class="description-section">
            <h3>Description</h3>
            <textarea rows="5" placeholder="Write your review here..."></textarea>
        </div>

        <!-- Submit Button -->
        <div class="submit-section"> 
            <asp:Button ID="btnReview" runat="server" class="submit-btn" Text="Submit Review" />
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const ratingStars = document.querySelectorAll('.rating-stars .fa-star');

            ratingStars.forEach(star => {
                star.addEventListener('mouseover', function () {
                    // Light up stars from left to right
                    const ratingValue = this.getAttribute('data-value');
                    ratingStars.forEach(star => {
                        if (star.getAttribute('data-value') <= ratingValue) {
                            star.classList.add('hover');
                        } else {
                            star.classList.remove('hover');
                        }
                    });
                });

                star.addEventListener('mouseout', function () {
                    // Remove hover effect, keep selected stars highlighted
                    const selectedValue = document.querySelector('.rating-stars .fa-star.selected')?.getAttribute('data-value');
                    ratingStars.forEach(star => {
                        if (selectedValue && star.getAttribute('data-value') <= selectedValue) {
                            star.classList.add('selected');
                        } else {
                            star.classList.remove('hover');
                        }
                    });
                });

                star.addEventListener('click', function () {
                    // Set selected rating
                    const ratingValue = this.getAttribute('data-value');
                    ratingStars.forEach(star => {
                        if (star.getAttribute('data-value') <= ratingValue) {
                            star.classList.add('selected');
                        } else {
                            star.classList.remove('selected');
                        }
                    });
                });
            });
        });
    </script>

</asp:Content>
