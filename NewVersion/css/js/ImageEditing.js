let currentImage = null; // Stores the image being edited
let canvas = null; // Canvas for editing

// Function to initialize the canvas with the uploaded image
function loadImageForEditing(imageElement) {
    currentImage = imageElement;
    canvas = document.getElementById('editingCanvas');
    const context = canvas.getContext('2d');

    const img = new Image();
    img.src = imageElement.src;

    img.onload = () => {
        // Adjust canvas size to image dimensions
        canvas.width = img.width;
        canvas.height = img.height;

        // Draw the image on the canvas
        context.drawImage(img, 0, 0, img.width, img.height);
    };
}

// Crop function
function cropImage(x, y, width, height) {
    const context = canvas.getContext('2d');
    const croppedData = context.getImageData(x, y, width, height);

    // Adjust canvas to new cropped size
    canvas.width = width;
    canvas.height = height;

    // Clear and redraw the cropped section
    context.clearRect(0, 0, canvas.width, canvas.height);
    context.putImageData(croppedData, 0, 0);
}

// Rotate function
function rotateImage(angle) {
    const context = canvas.getContext('2d');
    const img = new Image();
    img.src = currentImage.src;

    img.onload = () => {
        const width = img.width;
        const height = img.height;
        const radians = angle * (Math.PI / 180);

        canvas.width = width;
        canvas.height = height;

        context.clearRect(0, 0, canvas.width, canvas.height);

        // Translate to the center, rotate, and then translate back
        context.translate(width / 2, height / 2);
        context.rotate(radians);
        context.translate(-width / 2, -height / 2);

        context.drawImage(img, 0, 0);
    };
}

// Flip function (horizontal or vertical)
function flipImage(direction) {
    const context = canvas.getContext('2d');
    const img = new Image();
    img.src = currentImage.src;

    img.onload = () => {
        canvas.width = img.width;
        canvas.height = img.height;

        context.clearRect(0, 0, canvas.width, canvas.height);

        if (direction === 'horizontal') {
            context.scale(-1, 1); // Flip horizontally
            context.drawImage(img, -canvas.width, 0, canvas.width, canvas.height);
        } else if (direction === 'vertical') {
            context.scale(1, -1); // Flip vertically
            context.drawImage(img, 0, -canvas.height, canvas.width, canvas.height);
        }
    };
}

// Apply the changes from canvas to the actual image element
function applyCanvasToImage(imageElement) {
    imageElement.src = canvas.toDataURL(); // Update the image element with the edited canvas data
}