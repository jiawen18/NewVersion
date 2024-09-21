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
            <span class="upload-text">Add Photo / Video</span>
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
                <span class="add-more-icon" >+</span>
                <div id="mediaCount" class="media-count" >
                (0/2 photos, 0/1 video)
                </div>
            </div>         
        </div>
    </div>

        <br />

       <div id="errorMessage" class="error-message"></div>


        <!-- Image Modal -->
        <!-- Modal box -->
        <div class="modal fade" id="photoModal" tabindex="-1" role="dialog" aria-labelledby="photoModalLabel" aria-hidden="true">
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
                            <asp:Button ID="btnCrop" runat="server" Text="Crop" CssClass="btn btn-secondary" OnClientClick="cropImage(50, 50, 200, 200); return false;" CausesValidation="false"/>
                            <asp:Button ID="btnFlip" runat="server" Text="Flip" CssClass="btn btn-secondary" OnClientClick="flipImage('horizontal'); return false;" CausesValidation="false" />
                            <asp:Button ID="btnRotate" runat="server" Text="Rotate" CssClass="btn btn-secondary" OnClientClick="rotateImage(90); return false;" CausesValidation="false" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnApplyChanges" runat="server" Text="OK" CssClass="btn btn-primary"  OnClientClick="applyEdits(); return false;" />
                        <asp:Button ID="btnCancelChanges" runat="server" Text="Cancel" Cssclass="btn btn-secondary"  data-dismiss="modal"/>
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
<script src="js/Ratingstars.js"></script>
   

<!-- Script for Preview Media -->




<script src="js/ImageEditing.js"></script>

<script>
    let selectedImageFile = null;

    // Show modal for image editing
    function showModal(imageFile) {
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>'); // Replace with correct client-side ID for the modal image
        const fileURL = URL.createObjectURL(imageFile); // Create a temporary URL for the image file

        uploadedImage.src = fileURL; // Display the image in the modal window
        selectedImageFile = imageFile;
        $('#photoModal').modal('show'); // Show the modal window
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
        const files = Array.from(event.target.files);

        let imageCount = mediaPreview.querySelectorAll('img').length;
        let videoCount = mediaPreview.querySelectorAll('video').length;

        const errorMessage = document.getElementById('errorMessage');
        errorMessage.style.display = 'none';

        files.forEach(file => {
            const fileURL = URL.createObjectURL(file);
            const previewBox = document.createElement('div');
            previewBox.classList.add('image-preview');
            previewBox.style.position = 'relative';

            const removeButton = document.createElement('span');
            removeButton.textContent = 'x';
            removeButton.classList.add('remove-button');
            removeButton.onclick = function () {
                mediaPreview.removeChild(previewBox);
                if (file.type.startsWith('image/')) {
                    imageCount--;
                } else if (file.type.startsWith('video/')) {
                    videoCount--;
                }
                document.getElementById('mediaCount').textContent = `(${imageCount}/2 photos, ${videoCount}/1 video)`;
            };
            previewBox.appendChild(removeButton);

            if (file.type.startsWith('image/')) {
                if (imageCount < 2) {
                    const img = document.createElement('img');
                    img.src = fileURL;
                    img.style.width = '100%';
                    img.style.height = '100%';
                    img.style.objectFit = 'cover';
                    previewBox.appendChild(img);
                    mediaPreview.appendChild(previewBox);
                    imageCount++;

                    // Open the modal immediately after adding the image
                    showModal(file);
                } else {
                    errorMessage.textContent = "You can only upload a maximum of two images.";
                    errorMessage.style.display = 'block';
                }
            } else if (file.type.startsWith('video/') && videoCount === 0) {
                const video = document.createElement('video');
                video.src = fileURL;
                video.controls = true;
                video.style.width = '100%';
                video.style.height = '100%';
                video.style.objectFit = 'cover';
                previewBox.appendChild(video);
                mediaPreview.appendChild(previewBox);
                videoCount++;
            } else if (file.type.startsWith('video/') && videoCount > 0) {
                errorMessage.textContent = "You can only upload one video.";
                errorMessage.style.display = 'block';
            }
        });

        document.getElementById('mediaCount').textContent = `(${imageCount}/2 photos, ${videoCount}/1 video)`;
    }

    // Apply edits to the image and update preview
    function applyEdits() {
        const uploadedImage = document.getElementById('uploadedImage');
        const previewImage = document.getElementById('previewImage');
        const imagePreviewDiv = document.getElementById('imagePreview');

        if (uploadedImage.src) {
            // Update preview with the edited image
            previewImage.src = uploadedImage.src;
            imagePreviewDiv.style.display = 'block'; // Show preview div
        } else {
            console.error("No uploaded image found.");
        }

        // Close modal after applying edits (removed duplicate call)
        hideModal();
    }

    // Attach event listeners after DOM is fully loaded
    document.addEventListener('DOMContentLoaded', function () {
        const btnApplyChanges = document.getElementById('btnApplyChanges');
        if (btnApplyChanges) {
            btnApplyChanges.onclick = function (e) {
                e.preventDefault(); // Prevent default action
                applyEdits(); // Apply edits to image
                // Removed duplicate hideModal() call
            };
        }

        // Attach event listener to the initial file input
        const mediaInput = document.getElementById('mediaInput');
        if (mediaInput) {
            mediaInput.onchange = previewMedia;
        }
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
