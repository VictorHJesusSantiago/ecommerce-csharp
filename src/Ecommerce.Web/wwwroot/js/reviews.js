document.addEventListener('DOMContentLoaded', function() {
    const reviewForm = document.getElementById('reviewForm');
    if (reviewForm) {
        reviewForm.addEventListener('submit', function(e) {
            const rating = document.querySelector('input[name="Rating"]:checked');
            if (!rating) { e.preventDefault(); showToast('Please select a rating.', 'error'); return; }
            const title = document.getElementById('reviewTitle');
            const comment = document.getElementById('reviewComment');
            if (!title.value.trim() || !comment.value.trim()) { e.preventDefault(); showToast('Please fill in all required fields.', 'error'); }
        });
    }

    const starRatings = document.querySelectorAll('.star-rating input');
    starRatings.forEach(star => {
        star.addEventListener('change', function() {
            const value = this.value;
            document.querySelectorAll('.star-rating label').forEach(label => {
                const labelFor = parseInt(label.getAttribute('for').replace('star', ''));
                label.classList.toggle('active', labelFor <= value);
            });
        });
    });

    const helpfulButtons = document.querySelectorAll('.helpful-btn');
    helpfulButtons.forEach(button => {
        button.addEventListener('click', function() {
            const reviewId = this.dataset.reviewId;
            const isHelpful = this.dataset.helpful === 'true';
            fetch(`/api/reviews/${reviewId}/helpful`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ isHelpful: isHelpful })
            }).then(r => r.json()).then(d => {
                showToast('Thank you for your feedback!', 'success');
                this.disabled = true;
            });
        });
    });

    const reportButtons = document.querySelectorAll('.report-btn');
    reportButtons.forEach(button => {
        button.addEventListener('click', function() {
            const reviewId = this.dataset.reviewId;
            const reason = prompt('Why are you reporting this review?');
            if (reason) {
                fetch(`/api/reviews/${reviewId}/report`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ reason: reason })
                }).then(r => r.json()).then(d => { showToast('Review reported. Thank you.', 'success'); });
            }
        });
    });

    const reviewImages = document.querySelectorAll('.review-image');
    reviewImages.forEach(img => {
        img.addEventListener('click', function() { window.open(this.src, '_blank'); });
    });
});

function showToast(message, type) {
    const toast = document.createElement('div');
    toast.className = `alert alert-${type === 'error' ? 'danger' : type} position-fixed`;
    toast.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
    toast.textContent = message;
    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), 3000);
}
