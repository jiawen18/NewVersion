
        let uploadedImage = null;
        let uploadedVideo = null;

        function previewImage(event) {
            const imagePreview = document.getElementById('imagePreview');
            uploadedImage = event.target.files[0];

            if (uploadedImage) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    imagePreview.innerHTML = '<img src="' + e.target.result + '" alt="Image Preview" class="preview-img">';
                };
                reader.readAsDataURL(uploadedImage);
            }
        }

        function previewVideo(event) {
            const videoPreview = document.getElementById('videoPreview');
            uploadedVideo = event.target.files[0];

            if (uploadedVideo) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    videoPreview.innerHTML = '<video controls class="preview-video"><source src="' + e.target.result + '" type="' + uploadedVideo.type + '">Your browser does not support the video tag.</video>';
                };
                reader.readAsDataURL(uploadedVideo);
            }
        }

        // The files stored in uploadedImage and uploadedVideo can be submitted later with the entire review form.

        // Function to preview the selected media (image or video)
        function previewMedia(event) {
            const imagePreview = document.getElementById('imagePreview');
            const videoPreview = document.getElementById('videoPreview');
            const file = event.target.files[0];

            if (file) {
                if (file.type.startsWith('image/')) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        imagePreview.innerHTML = `<img src="${e.target.result}" alt="Image Preview" />`;
                    };
                    reader.readAsDataURL(file);
                } else if (file.type.startsWith('video/')) {
                    const url = URL.createObjectURL(file);
                    videoPreview.innerHTML = `<video controls><source src="${url}" type="${file.type}" /></video>`;
                }
            }
        }

        // Function to handle "Add More" button click
        function handleAddMoreMedia() {
            const fileInput = document.getElementById('FileUploadMedia');
            fileInput.value = ''; // Clear the file input value
            fileInput.click(); // Trigger the file input click
        }

