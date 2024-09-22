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
            <!-- Original Image Modal -->
            <div class="modal fade" id="photoModal" tabindex="-1" role="dialog" aria-labelledby="photoModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Photo</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideModal();">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Image ID="uploadedImage" runat="server" CssClass="img-fluid" style="display: none;" />
                            <canvas id="editingCanvas" style="border: 1px solid black;"></canvas>
                            <div class="mt-3">
                                <asp:Button ID="btnCrop" runat="server" Text="Crop" CssClass="btn btn-secondary" OnClientClick="showCropModal(); return false;" />
                                <asp:Button ID="btnFlip" runat="server" Text="Flip" CssClass="btn btn-secondary" OnClientClick="showFlipModal(); return false;" />
                                <asp:Button ID="btnRotate" runat="server" Text="Rotate" CssClass="btn btn-secondary" OnClientClick="showRotateModal(); return false;" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnApplyChanges" runat="server" Text="OK" CssClass="btn btn-primary" OnClientClick="applyEdits(); return false;" />
                            <asp:Button ID="btnCancelChanges" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClientClick="hideModal(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Crop Modal -->
            <div class="modal fade" id="cropModal" tabindex="-1" role="dialog" aria-labelledby="cropModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Crop Image</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideCropModal();">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <canvas id="cropCanvas" style="border: 1px solid black;"></canvas>
                            <p>Select the area to crop and click OK.</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCropOk" runat="server" Text="OK" CssClass="btn btn-primary" OnClientClick="cropImage(); return false;" />
                            <asp:Button ID="btnCropCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClientClick="hideCropModal(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

        <!-- Rotate Modal -->
        <div class="modal fade" id="rotateModal" tabindex="-1" role="dialog" aria-labelledby="rotateModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Rotate Image</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideRotateModal();">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>Select the rotation angle:</p>
                        <input type="range" id="rotationAngleSlider" min="-180" max="180" value="0" step="1" oninput="updateRotationAngle(this.value)">
                        <p>Angle: <span id="rotationAngleDisplay">0</span>°</p>
                        <canvas id="modalImageCanvas" style="border: 1px solid #000;"></canvas>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnApplyRotation" runat="server" CssClass="btn btn-primary" Text="Apply Rotation" OnClientClick="applyRotation(); return false;" />
                        <asp:Button ID="btnCancelRotation" runat="server" Text="Cancel" class="btn btn-secondary" OnClientClick="hideRotateModal();  return false;" />
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Flip Modal -->
        <div id="flipModal" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Choose Flip Direction</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>Select the direction to flip:</p>
                        <canvas id="imageCanvas" style="width: 100%; height: auto;"></canvas>
                        <div>
                            <asp:Button ID="btnFlipHorizontal" runat="server" Text="Flip Horizontally" CssClass="btn btn-primary" OnClientClick="flipMedia('horizontal'); return false;" />
                            <asp:Button ID="btnFlipVertical" runat="server" Text="Flip Vertically" CssClass="btn btn-primary" OnClientClick="flipMedia('vertical'); return false;" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnApplyFlip" runat="server" CssClass="btn btn-primary" Text="Apply Flip" OnClientClick="applyFlip(); return false;" />
                        <asp:Button ID="btnCancelFlip" runat="server" Text="Cancel" class="btn btn-secondary" OnClientClick="hideFlipModal();  return false;" />
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
            <asp:Button ID="btnReview" runat="server" class="submit-btn" Text="Submit Review" OnClick="btnReview_Click" OnClientClick="return false;" />
        </div>
    </div>


<!-- Script for Rating Stars -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
<script>
    document.addEventListener('DOMContentLoaded', function () {
        const ratingStars = document.querySelectorAll('.rating-stars .fa-star');

        ratingStars.forEach(star => {
            star.addEventListener('mouseover', function () {
                const ratingValue = this.getAttribute('data-value');
                ratingStars.forEach(star => {
                    star.classList.toggle('hover', star.getAttribute('data-value') <= ratingValue);
                });
            });

            star.addEventListener('mouseout', function () {
                ratingStars.forEach(star => {
                    star.classList.remove('hover');
                });
            });

            star.addEventListener('click', function () {
                const ratingValue = this.getAttribute('data-value');
                // Clear previous selections
                ratingStars.forEach(star => {
                    star.classList.remove('selected');
                });
                // Highlight only the clicked star
                this.classList.add('selected');
            });
        });
    });
</script>
   

<!-- Script for Preview Media -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    let selectedImageFile = null;
    let isModalOpen = false;

    // Show modal for image editing
    function showModal(imageFile) {
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>'); // Replace with correct client-side ID for the modal image
        const fileURL = URL.createObjectURL(imageFile); // Create a temporary URL for the image file

        uploadedImage.src = fileURL; // Display the image in the modal window
        selectedImageFile = imageFile;

        $('#photoModal').modal('show'); // Show the modal window
        isModalOpen = true;
    }


    // Hide modal
    function hideModal() {
        $('#photoModal').modal('hide');
        isModalOpen = false;
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
                updateCount(file.type, -1);
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
                    img.style.objectFit = 'contain';
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
        const canvas = document.getElementById('editingCanvas');
        const croppedImage = canvas.toDataURL('image/png'); // Get the cropped image data as a base64-encoded PNG

        // Get the media preview container
        const mediaPreview = document.getElementById('mediaPreview');

        // Clear all existing previews before adding the edited image
        mediaPreview.innerHTML = ''; // This removes all existing previews

        // Create a new preview box for the cropped (edited) image
        const previewBox = document.createElement('div');
        previewBox.classList.add('image-preview');
        previewBox.classList.add('edited-image-preview'); // Mark this as an edited image
        previewBox.style.position = 'relative';

        // Create the remove button
        const removeButton = document.createElement('span');
        removeButton.textContent = 'x';
        removeButton.classList.add('remove-button');
        removeButton.onclick = function () {
            previewBox.remove(); // Remove the preview box when clicked
        };
        previewBox.appendChild(removeButton);

        // Create and append the edited image
        const img = document.createElement('img');
        img.src = croppedImage; // Set the source to the cropped image
        img.style.width = '100%';
        img.style.height = '100%';
        img.style.objectFit = 'cover';
        previewBox.appendChild(img);

        // Append the edited image preview box to the media preview container
        mediaPreview.appendChild(previewBox);

        // Close the modal
        hideModal();
    }

    // Attach event listeners after DOM is fully loaded
    document.addEventListener('DOMContentLoaded', function () {
        const btnApplyChanges = document.getElementById('<%= btnApplyChanges.ClientID %>');
        if (btnApplyChanges) {
            btnApplyChanges.onclick = function (e) {
                e.preventDefault(); // Prevent default action
                applyEdits(); // Apply edits to image
                $('#photoModal').modal('hide');
            };
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
<!-- End Script for Preview Media -->

<!-- Script for Crop Image -->
<script>
    let cropping = false;
    let startX, startY, endX, endY;
    const canvas = document.getElementById('editingCanvas');
    const cropCanvas = document.getElementById('cropCanvas');
    const ctx = canvas.getContext('2d');
    const cropCtx = cropCanvas.getContext('2d');
    let uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>'); // Adjust if necessary

    function showModal(imageFile) {
        const fileURL = URL.createObjectURL(imageFile);
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>');

        uploadedImage.src = fileURL;

        uploadedImage.onload = function () {
            const canvas = document.getElementById('editingCanvas');
            const ctx = canvas.getContext('2d');
            canvas.width = 600; // Fixed width for the editing canvas
            canvas.height = 400; // Fixed height for the editing canvas
            ctx.drawImage(uploadedImage, 0, 0, canvas.width, canvas.height);
        };

        $('#photoModal').modal('show');
    }

    function hideModal() {
        $('#photoModal').modal('hide');
    }

    function showCropModal() {
        const canvas = document.getElementById('editingCanvas');
        const cropCanvas = document.getElementById('cropCanvas');
        const cropCtx = cropCanvas.getContext('2d');

        cropCanvas.width = 600; // Match the size of the editing canvas
        cropCanvas.height = 400; // Match the size of the editing canvas

        // Draw the image onto the crop canvas
        cropCtx.drawImage(canvas, 0, 0, cropCanvas.width, cropCanvas.height);
        $('#cropModal').modal('show');
    }

    // Crop image logic
    cropCanvas.addEventListener('mousedown', function (e) {
        const rect = cropCanvas.getBoundingClientRect();
        startX = e.clientX - rect.left;
        startY = e.clientY - rect.top;
        cropping = true;
    });

    cropCanvas.addEventListener('mousemove', function (e) {
        if (cropping) {
            const rect = cropCanvas.getBoundingClientRect();
            endX = e.clientX - rect.left;
            endY = e.clientY - rect.top;
            drawCropSelectionRect();
        }
    });

    cropCanvas.addEventListener('mouseup', function () {
        cropping = false;
    });

    function drawCropSelectionRect() {
        cropCtx.clearRect(0, 0, cropCanvas.width, cropCanvas.height);

        // Redraw the uploaded image on the crop canvas
        cropCtx.drawImage(uploadedImage, 0, 0, cropCanvas.width, cropCanvas.height);

        cropCtx.setLineDash([6]);
        cropCtx.strokeStyle = 'red';
        cropCtx.strokeRect(startX, startY, endX - startX, endY - startY);
    }

    function cropImage() {
        const cropWidth = endX - startX;
        const cropHeight = endY - startY;

        if (cropWidth > 0 && cropHeight > 0) {
            const croppedImage = cropCtx.getImageData(startX, startY, cropWidth, cropHeight);
            canvas.width = cropWidth;
            canvas.height = cropHeight;
            ctx.putImageData(croppedImage, 0, 0);
        } else {
            alert("Please select an area to crop.");
        }

        hideCropModal(); // Hide crop modal
    }

    // Hide crop modal
    function hideCropModal() {
        $('#cropModal').modal('hide');
    }
</script>
<!-- End Script for Crop Image -->

<!-- Script for Rotate Image -->
<script>
    let rotationAngle = 0; // Track the current rotation angle
    let rotatedImageDataUrl = ''; // Store the rotated image data URL

    function rotateImage(angle, canvasId) {
        const canvas = document.getElementById(canvasId);
        const ctx = canvas.getContext('2d');
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>');

    // Set fixed canvas dimensions
    const fixedWidth = 600; // Set desired width
    const fixedHeight = 400; // Set desired height
    canvas.width = fixedWidth;
    canvas.height = fixedHeight;

    // Clear the canvas
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Save the context state
    ctx.save();

    // Calculate the aspect ratio and scale the image to fit within the fixed dimensions
    const imgAspectRatio = uploadedImage.naturalWidth / uploadedImage.naturalHeight;
    let drawWidth, drawHeight;

    if (fixedWidth / fixedHeight < imgAspectRatio) {
        drawWidth = fixedWidth;
        drawHeight = fixedWidth / imgAspectRatio;
    } else {
        drawHeight = fixedHeight;
        drawWidth = fixedHeight * imgAspectRatio;
    }

    // Calculate center positions
    const centerX = (canvas.width - drawWidth) / 2;
    const centerY = (canvas.height - drawHeight) / 2;

    // Translate and rotate the context
    ctx.translate(centerX + drawWidth / 2, centerY + drawHeight / 2);
    ctx.rotate(angle * Math.PI / 180);

    // Draw the image at the center, maintaining its aspect ratio
    ctx.drawImage(uploadedImage, -drawWidth / 2, -drawHeight / 2, drawWidth, drawHeight);

    // Restore the context state
    ctx.restore();

    // Store the data URL for later use
    rotatedImageDataUrl = canvas.toDataURL();
}

// Update rotation angle based on slider value
function updateRotationAngle(value) {
    rotationAngle = parseInt(value); // Update the current rotation angle
    document.getElementById('rotationAngleDisplay').textContent = rotationAngle; // Update display
    rotateImage(rotationAngle, 'modalImageCanvas'); // Rotate image with the new angle in modal
}

// Show rotate modal
    function showRotateModal() {
        $('#rotateModal').modal('show');
        document.getElementById('rotationAngleSlider').value = rotationAngle; // Assuming you have a slider with this ID
        rotateImage(rotationAngle, 'modalImageCanvas'); // Show the image with the current rotation angle
    }

// Hide rotate modal
function hideRotateModal() {
    $('#rotateModal').modal('hide');
}

    // Apply rotation and save to preview box
    function applyRotation() {
        // Create a new image element for the rotated preview
        const newPreviewBox = document.createElement('div');
        newPreviewBox.classList.add('image-preview');
        newPreviewBox.style.position = 'relative';

        const rotatedImage = document.createElement('img');
        rotatedImage.src = rotatedImageDataUrl; // Use the stored data URL

        // Set styles to maintain original aspect ratio
        const img = new Image();
        img.src = rotatedImageDataUrl;
        img.onload = function () {
            rotatedImage.style.width = `${img.naturalWidth}px`;
            rotatedImage.style.height = `${img.naturalHeight}px`;
        };

        const removeButton = document.createElement('span');
        removeButton.textContent = 'x';
        removeButton.classList.add('remove-button');
        removeButton.onclick = function () {
            newPreviewBox.remove(); // Remove the preview box when clicked
        };

        newPreviewBox.appendChild(removeButton);
        newPreviewBox.appendChild(rotatedImage);

        // Append to the media preview section without clearing previous images
        document.getElementById('mediaPreview').appendChild(newPreviewBox);

        // Save the rotated image to the first modal window
        const firstModalImage = document.getElementById('<%= uploadedImage.ClientID %>');
    firstModalImage.src = rotatedImageDataUrl; // Update the original image in the first modal

    // Close the rotate modal
    hideRotateModal();
}
</script>
<!-- End Script for Rotate Image -->

<!-- Script for Flip Image -->
<script>
    let originalImage = new Image();
    let flippedImage = new Image();
    let flipDirection = '';

    function showFlipModal() {
        $('#flipModal').modal('show');
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>'); // Replace with the correct client ID
    originalImage.src = uploadedImage.src; // Get the current image source
    originalImage.onload = function() {
        loadImageToCanvas(originalImage);
    };
}

function loadImageToCanvas(image) {
    const canvas = document.getElementById('imageCanvas');
    const ctx = canvas.getContext('2d');
    canvas.width = image.width; // Set canvas width
    canvas.height = image.height; // Set canvas height
    ctx.drawImage(image, 0, 0); // Draw the image on canvas
}

function flipMedia(direction) {
    flipDirection = direction; // Store the direction
    const canvas = document.getElementById('imageCanvas');
    const ctx = canvas.getContext('2d');

    // Clear the canvas
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    
    // Check if we need to flip based on the current flippedImage
    if (flippedImage.src) {
        // Use the flipped image if it exists
        originalImage = flippedImage;
    }

    // Perform the flip
    if (direction === 'horizontal') {
        ctx.save();
        ctx.scale(-1, 1); // Flip horizontally
        ctx.drawImage(originalImage, -canvas.width, 0);
        ctx.restore();
    } else if (direction === 'vertical') {
        ctx.save();
        ctx.scale(1, -1); // Flip vertically
        ctx.drawImage(originalImage, 0, -canvas.height);
        ctx.restore();
    }

    // Update the flipped image source
    flippedImage.src = canvas.toDataURL();
}

function applyFlip() {
    const canvas = document.getElementById('imageCanvas');
        const uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>'); // Replace with the correct client ID
        uploadedImage.src = canvas.toDataURL(); // Update the original image with the flipped version
        $('#flipModal').modal('hide'); // Close the modal
    }

    function hideFlipModal() {
        $('#flipModal').modal('hide'); // Simply hide the modal
    }
</script>
</asp:Content>
