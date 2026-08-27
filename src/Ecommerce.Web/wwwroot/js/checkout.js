document.addEventListener('DOMContentLoaded', function() {
    const checkoutForm = document.getElementById('checkoutForm');
    if (checkoutForm) {
        checkoutForm.addEventListener('submit', function(e) {
            const cardNumber = document.getElementById('cardNumber');
            if (cardNumber) {
                const cleaned = cardNumber.value.replace(/\s/g, '');
                if (!/^\d{13,19}$/.test(cleaned)) {
                    e.preventDefault();
                    showToast('Please enter a valid card number.', 'error');
                    return;
                }
            }
        });
    }

    const cardNumberInput = document.getElementById('cardNumber');
    if (cardNumberInput) {
        cardNumberInput.addEventListener('input', function() {
            let value = this.value.replace(/\s/g, '').replace(/\D/g, '');
            let formatted = value.match(/.{1,4}/g)?.join(' ') || value;
            this.value = formatted;
        });
    }

    const expiryInput = document.getElementById('cardExpiry');
    if (expiryInput) {
        expiryInput.addEventListener('input', function(e) {
            let value = this.value.replace(/\D/g, '');
            if (value.length >= 2) { value = value.substring(0, 2) + '/' + value.substring(2); }
            this.value = value;
        });
    }
});

function showToast(message, type) {
    const toast = document.createElement('div');
    toast.className = `alert alert-${type === 'error' ? 'danger' : type} position-fixed`;
    toast.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
    toast.textContent = message;
    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), 3000);
}
