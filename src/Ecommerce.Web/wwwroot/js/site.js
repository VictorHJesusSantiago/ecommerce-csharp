document.addEventListener('DOMContentLoaded', function() {
    const navbar = document.querySelector('.navbar');
    window.addEventListener('scroll', function() {
        if (window.scrollY > 50) {
            navbar.classList.add('shadow-sm');
        } else {
            navbar.classList.remove('shadow-sm');
        }
    });

    const tooltips = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltips.forEach(tooltip => new bootstrap.Tooltip(tooltip));

    const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
    addToCartButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const productId = this.dataset.productId;
            addToCart(productId);
        });
    });
});

function addToCart(productId, quantity = 1) {
    fetch('/api/cart/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': getAntiForgeryToken() },
        body: JSON.stringify({ productId: productId, quantity: quantity })
    })
    .then(response => {
        if (response.ok) {
            showToast('Product added to cart!', 'success');
            updateCartCount();
        } else {
            showToast('Failed to add product to cart.', 'error');
        }
    })
    .catch(() => showToast('An error occurred.', 'error'));
}

function removeFromCart(itemId) {
    fetch(`/api/cart/remove/${itemId}`, { method: 'DELETE', headers: { 'RequestVerificationToken': getAntiForgeryToken() } })
    .then(response => {
        if (response.ok) { location.reload(); }
    });
}

function updateCartItemQuantity(itemId, quantity) {
    fetch('/api/cart/update', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': getAntiForgeryToken() },
        body: JSON.stringify({ itemId: itemId, quantity: quantity })
    })
    .then(response => { if (response.ok) { location.reload(); } });
}

function addToWishlist(productId) {
    fetch(`/api/wishlist/add/${productId}`, {
        method: 'POST',
        headers: { 'RequestVerificationToken': getAntiForgeryToken() }
    })
    .then(response => {
        if (response.ok) {
            showToast('Product added to wishlist!', 'success');
        } else {
            showToast('Failed to add to wishlist.', 'error');
        }
    });
}

function removeFromWishlist(productId) {
    fetch(`/api/wishlist/remove/${productId}`, { method: 'DELETE', headers: { 'RequestVerificationToken': getAntiForgeryToken() } })
    .then(response => {
        if (response.ok) { location.reload(); }
    });
}

function showToast(message, type = 'info') {
    const toast = document.createElement('div');
    toast.className = `alert alert-${type === 'error' ? 'danger' : type} position-fixed`;
    toast.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
    toast.textContent = message;
    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), 3000);
}

function updateCartCount() {
    fetch('/api/cart/count')
    .then(r => r.json())
    .then(data => {
        const badge = document.querySelector('.cart-badge');
        if (badge) { badge.textContent = data.count; }
    });
}

function getAntiForgeryToken() {
    const token = document.querySelector('input[name="__RequestVerificationToken"]');
    return token ? token.value : '';
}

function confirmDelete(message = 'Are you sure you want to delete this?') {
    return confirm(message);
}

function formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(amount);
}
