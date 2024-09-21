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