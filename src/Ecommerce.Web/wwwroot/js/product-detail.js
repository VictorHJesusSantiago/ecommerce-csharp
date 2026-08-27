document.addEventListener('DOMContentLoaded', function() {
    const mainImage = document.getElementById('mainImage');
    const thumbnails = document.querySelectorAll('.product-gallery .thumbnails img');

    thumbnails.forEach(thumb => {
        thumb.addEventListener('click', function() {
            if (mainImage) { mainImage.src = this.src; }
            thumbnails.forEach(t => t.classList.remove('active'));
            this.classList.add('active');
        });
    });

    const variantOptions = document.querySelectorAll('.variant-option');
    variantOptions.forEach(option => {
        option.addEventListener('click', function() {
            variantOptions.forEach(o => o.classList.remove('active'));
            this.classList.add('active');
            const variantId = this.dataset.variantId;
            const price = this.dataset.price;
            const stock = this.dataset.stock;
            if (price) { document.querySelector('.product-info .price').textContent = formatCurrency(price); }
            if (stock) { document.getElementById('quantity').max = stock; }
            if (variantId) { document.getElementById('selectedVariantId').value = variantId; }
        });
    });

    const zoomOverlay = document.createElement('div');
    zoomOverlay.className = 'zoom-overlay';
    zoomOverlay.style.cssText = 'display:none;position:fixed;top:0;left:0;width:100%;height:100%;background:rgba(0,0,0,0.8);z-index:9999;cursor:pointer;justify-content:center;align-items:center;';
    document.body.appendChild(zoomOverlay);

    if (mainImage) {
        mainImage.style.cursor = 'zoom-in';
        mainImage.addEventListener('click', function() {
            const zoomImg = document.createElement('img');
            zoomImg.src = this.src;
            zoomImg.style.cssText = 'max-width:90%;max-height:90%;border-radius:8px;';
            zoomOverlay.innerHTML = '';
            zoomOverlay.appendChild(zoomImg);
            zoomOverlay.style.display = 'flex';
        });
        zoomOverlay.addEventListener('click', function() { this.style.display = 'none'; });
    }
});

function formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(amount);
}
