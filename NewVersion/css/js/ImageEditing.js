let cropping = false;
let startX, startY, endX, endY;
let cropWidth, cropHeight;
const canvas = document.getElementById('editingCanvas');
const ctx = canvas.getContext('2d');
let uploadedImage = document.getElementById('<%= uploadedImage.ClientID %>'); // Replace with correct ID if needed

// Show modal for image editing
function showModal(imageFile) {
    const fileURL = URL.createObjectURL(imageFile); // Create a temporary URL for the image file
    uploadedImage.src = fileURL; // Display the image in the modal window

    uploadedImage.onload = function () {
        canvas.width = uploadedImage.width; // Set canvas size to image size
        canvas.height = uploadedImage.height;
        ctx.drawImage(uploadedImage, 0, 0); // Draw the uploaded image on the canvas
    };

    $('#photoModal').modal('show'); // Show the modal window
}

// Hide modal
function hideModal() {
    $('#photoModal').modal('hide');
}

// Mouse down event to start cropping selection
canvas.addEventListener('mousedown', function (e) {
    const rect = canvas.getBoundingClientRect();
    startX = e.clientX - rect.left;
    startY = e.clientY - rect.top;
    cropping = true;
});

// Mouse move event to update selection rectangle
canvas.addEventListener('mousemove', function (e) {
    if (cropping) {
        const rect = canvas.getBoundingClientRect();
        endX = e.clientX - rect.left;
        endY = e.clientY - rect.top;
        drawSelectionRect(); // Draw the rectangle during selection
    }
});

// Mouse up event to finalize selection
canvas.addEventListener('mouseup', function () {
    cropping = false;
    cropWidth = endX - startX;
    cropHeight = endY - startY;
});

// Function to draw the selection rectangle
function drawSelectionRect() {
    ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
    ctx.drawImage(uploadedImage, 0, 0); // Redraw the uploaded image
    ctx.setLineDash([6]); // Optional: dashed line for selection
    ctx.strokeStyle = 'red';
    ctx.strokeRect(startX, startY, endX - startX, endY - startY);
}

// Function to crop the selected area
function cropImage() {
    if (cropWidth > 0 && cropHeight > 0) {
        const croppedImage = ctx.getImageData(startX, startY, cropWidth, cropHeight);
        canvas.width = cropWidth;
        canvas.height = cropHeight;
        ctx.putImageData(croppedImage, 0, 0);
    } else {
        alert("Please select an area to crop.");
    }
}

// Button click handlers
document.getElementById('<%= btnCrop.ClientID %>').onclick = cropImage; // Attach crop function to the crop button

// Other button click handlers (e.g., flip and rotate) can be added similarly
function flipImage(direction) {
    ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
    ctx.save(); // Save the current state
    ctx.scale(direction === 'horizontal' ? -1 : 1, direction === 'horizontal' ? 1 : -1);
    ctx.drawImage(originalImage, direction === 'horizontal' ? -canvas.width : 0, 0);
    ctx.restore(); // Restore the state
}

function rotateImage(degrees) {
    const imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
    ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
    ctx.save(); // Save the current state
    ctx.translate(canvas.width / 2, canvas.height / 2);
    ctx.rotate(degrees * Math.PI / 180);
    ctx.drawImage(originalImage, -canvas.width / 2, -canvas.height / 2);
    ctx.restore(); // Restore the state
    ctx.putImageData(imageData, 0, 0); // Reapply image data if needed
}

function applyEdits() {
    const croppedDataUrl = canvas.toDataURL('image/png');
    // Use croppedDataUrl as needed
}