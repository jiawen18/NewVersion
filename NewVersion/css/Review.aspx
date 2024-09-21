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
                <asp:Image ID="Image1" runat="server" src="images/z-flip_blue.jpg"  alt="Product Image"/>
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
        <input type="file" id="FileUploadMedia" name="FileUploadMedia" class="file-upload-input" accept="image/*,video/*" onchange="previewMedia(event)" multiple />
        <label for="FileUploadMedia" class="upload-btn">
            <asp:Image ID="Image2" runat="server" src="images/camera.png" alt="Upload Icon" class="upload-icon" />
            Add Photo / Video
        </label>
    </div>

    



    <!-- Media Container -->
    <div id="mediaContainer" class="media-container">
        <!-- Preview Image -->
        <div id="mediaPreview" class="media-preview"></div>

        <!-- Preview Image -->
        <div id="imagePreview" class="image-preview" style="display: none;">
            <asp:Image ID="previewImage" runat="server" class="preview-image" />
        </div>

        <!-- Add More Container -->
        <div id="addMoreContainer" class="add-more-container">
            <div id="addMoreBox" class="add-more-box" onclick="document.getElementById('mediaInput').click();">
                <input type="file" id="mediaInput" multiple accept="image/*,video/*" onchange="previewMedia(event)" style="display: none;">
                <span class="add-more-icon">+</span>
                <div id="mediaCount" class="media-count" >
                (0/2 photos, 0/1 video)
                </div>
            </div>

            <div id="errorMessage" class="error-message"></div>
        </div>
    </div>


        <!-- Image Modal -->
        <!-- Modal box -->
        <div class="modal fade" id="photoModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit Photo</h5>
                        <asp:Button ID="btnClose" runat="server" class="close" data-dismiss="modal" aria-label="Close" />
                            <span aria-hidden="true">&times;</span>
                    </div>
                    <div class="modal-body">
                        <!-- Display uploaded photo -->
                        <asp:Image ID="uploadedImage" runat="server" CssClass="img-fluid" />
                
                        <!-- Options for editing the photo -->
                        <div class="mt-3">
                            <asp:Button ID="btnCrop" runat="server" Text="Crop" CssClass="btn btn-secondary" />
                            <asp:Button ID="btnFlip" runat="server" Text="Flip" CssClass="btn btn-secondary" />
                            <asp:Button ID="btnRotate" runat="server" Text="Rotate" CssClass="btn btn-secondary" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnApplyChanges" runat="server" Text="OK" CssClass="btn btn-primary" OnClientClick="applyEdits(); return false;" />
                        <asp:Button ID="btnCancelChanges" runat="server" Text="Cancel" class="btn btn-secondary"  data-dismiss="modal"/>
                        <input type="file" id="imageUploadInput" onchange="previewImage(event);" style="display: none;"  />
                    </div>
                </div>
            </div>
        </div>

        <br />
        












        <!-- Description -->
        <div class="description-section">
            <h3>Description</h3>
            <textarea rows="5" placeholder="Write your review here..."></textarea>
        </div>

        <!-- Submit Button -->
        <div class="submit-section"> 
            <asp:Button ID="btnReview" runat="server" class="submit-btn" Text="Submit Review" OnClick="btnReview_Click" />
        </div>
    </div>


<!-- Script for Rating Stars -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
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


<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

<script>
    let editedImageDataUrl = null;

    // Show modal for image editing
    function showModal() {
        $('#photoModal').modal('show');
    }

    // Hide modal
    function hideModal() {
        $('#photoModal').modal('hide');
    }

    // Function to trigger the hidden file input when the "Add More" box is clicked
    function triggerFileUpload() {
        document.getElementById('mediaInput').click();
    }

    // Function to handle the media preview when files are selected
    function previewMedia(event) {
        const mediaPreview = document.getElementById('mediaPreview');
        const files = Array.from(event.target.files); // Get the uploaded files

        let imageCount = mediaPreview.querySelectorAll('img').length; // Count existing images
        let videoCount = mediaPreview.querySelectorAll('video').length; // Count existing videos

        // Clear previous error message
        const errorMessage = document.getElementById('errorMessage');
        errorMessage.style.display = 'none'; // Hide error message initially

        files.forEach(file => {
            const fileURL = URL.createObjectURL(file); // Create a URL for the file
            const previewBox = document.createElement('div');
            previewBox.classList.add('image-preview');

            if (file.type.startsWith('image/')) {
                if (imageCount < 2) {
                    const img = document.createElement('img');
                    img.src = fileURL;
                    img.style.width = '100%'; // Fit the image within the box
                    img.style.height = '100%'; // Maintain aspect ratio
                    img.style.objectFit = 'cover'; // Cover the box entirely
                    previewBox.appendChild(img);
                    mediaPreview.appendChild(previewBox); // Add the new preview box
                    imageCount++; // Increment image count
                } else {
                    errorMessage.textContent = "You can only upload a maximum of two images.";
                    errorMessage.style.display = 'block'; // Show the error message
                }
            } else if (file.type.startsWith('video/') && videoCount === 0) {
                const video = document.createElement('video');
                video.src = fileURL;
                video.controls = true; // Show video controls
                video.style.width = '100%'; // Fit the video within the box
                video.style.height = '100%'; // Maintain aspect ratio
                video.style.objectFit = 'cover'; // Cover the box entirely
                previewBox.appendChild(video);
                mediaPreview.appendChild(previewBox); // Add the new preview box
                videoCount++; // Increment video count
            } else if (file.type.startsWith('video/') && videoCount > 0) {
                errorMessage.textContent = "You can only upload one video.";
                errorMessage.style.display = 'block'; // Show the error message
            }
        });

        // Update the count display
        document.getElementById('mediaCount').textContent = `(${imageCount}/2 photos, ${videoCount}/1 video)`;
    }

    // Apply edits to the image and update preview
    function applyEdits() {
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>');
        const previewImage = document.getElementById('<%= previewImage.ClientID %>');
        const imagePreviewDiv = document.getElementById('imagePreview');

        // Update preview with the edited image
        previewImage.src = uploadedImage.src;
        imagePreviewDiv.style.display = 'block'; // Show preview div

        hideModal(); // Close modal after applying edits
    }

    // Attach event listeners after DOM is fully loaded
    document.addEventListener('DOMContentLoaded', function () {
        const btnApplyChanges = document.getElementById('btnApplyChanges'); // Use actual button ID
        if (btnApplyChanges) {
            btnApplyChanges.onclick = function (e) {
                e.preventDefault(); // Prevent default action
                applyEdits(); // Apply edits to image
                hideModal(); // Hide modal after applying edits
            };
        }

        // Attach event listener to the initial file input
        document.getElementById('mediaInput').onchange = previewMedia;
    });

    // Handle adding more media inputs dynamically
    function handleAddMoreMedia() {
        const newInput = document.createElement('input');
        newInput.type = 'file';
        newInput.classList.add('file-upload-input');
        newInput.accept = 'image/*,video/*';
        newInput.multiple = true; // Allow multiple selections
        newInput.onchange = previewMedia; // Attach preview handler to new input

        // Append the new input to the media upload section
        document.querySelector('.media-upload').appendChild(newInput);
    }
</script>
</asp:Content>
