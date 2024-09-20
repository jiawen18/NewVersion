let uploadedImage = null;
let uploadedVideo = null;

function previewMedia(event) {
    const imagePreview = document.getElementById('imagePreview');
    const videoPreview = document.getElementById('videoPreview');
    const files = event.target.files;

    // Clear previous previews
    imagePreview.innerHTML = '';
    videoPreview.innerHTML = '';

    if (files.length > 0) {
        const file = files[0];

        if (file.type.startsWith('image/')) {
            const reader = new FileReader();
            reader.onload = function (e) {
                imagePreview.innerHTML = `<img src="${e.target.result}" alt="Image Preview" style="max-width: 100%; height: auto;" />`;
            };
            reader.readAsDataURL(file);
        } else if (file.type.startsWith('video/')) {
            const url = URL.createObjectURL(file);
            videoPreview.innerHTML = `<video controls style="max-width: 100%;"><source src="${url}" type="${file.type}" /></video>`;

            // Optional: Release the object URL after the video is loaded
            videoPreview.querySelector('video').onloadeddata = function () {
                URL.revokeObjectURL(url);
            };
        }
    }
}

function handleAddMoreMedia() {
    const fileInput = document.getElementById('FileUploadMedia');
    fileInput.value = ''; // Clear the file input value
    fileInput.click(); // Trigger the file input click
}
