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
        previewBox.style.position = 'relative'; // Make the preview box relative for absolute positioning of the remove button

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
               img.onclick = function () {
                    selectedImageFile = file; // Save the selected file for editing
                    showModal(file); // Show modal for editing the selected image

                };
                previewBox.appendChild(img);
                mediaPreview.appendChild(previewBox);
                imageCount++;
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