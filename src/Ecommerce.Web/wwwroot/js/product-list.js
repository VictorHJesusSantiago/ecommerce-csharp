document.addEventListener('DOMContentLoaded', function() {
    const filterForm = document.getElementById('filterForm');
    if (filterForm) {
        const priceRange = document.getElementById('priceRange');
        if (priceRange) {
            noUiSlider.create(priceRange, {
                start: [0, 1000],
                connect: true,
                range: { min: 0, max: 1000 },
                step: 10
            });
            priceRange.noUiSlider.on('update', function(values) {
                document.getElementById('minPrice').value = Math.round(values[0]);
                document.getElementById('maxPrice').value = Math.round(values[1]);
            });
        }
    }

    const viewToggle = document.querySelectorAll('.view-toggle');
    viewToggle.forEach(toggle => {
        toggle.addEventListener('click', function() {
            const view = this.dataset.view;
            const productsContainer = document.getElementById('productsContainer');
            if (productsContainer) {
                productsContainer.className = view === 'grid' ? 'row' : 'list-view';
            }
            viewToggle.forEach(t => t.classList.remove('active'));
            this.classList.add('active');
        });
    });

    const quickViewButtons = document.querySelectorAll('.quick-view-btn');
    quickViewButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const productId = this.dataset.productId;
            showQuickView(productId);
        });
    });

    const compareButtons = document.querySelectorAll('.compare-btn');
    compareButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const productId = this.dataset.productId;
            addToCompare(productId);
        });
    });
});

function showQuickView(productId) {
    fetch(`/api/products/${productId}`)
    .then(r => r.json())
    .then(product => {
        const modal = document.getElementById('quickViewModal');
        if (modal) {
            document.getElementById('quickViewTitle').textContent = product.name;
            document.getElementById('quickViewImage').src = product.mainImageUrl;
            document.getElementById('quickViewPrice').textContent = `$${product.price}`;
            document.getElementById('quickViewDescription').textContent = product.shortDescription;
            const bootstrapModal = new bootstrap.Modal(modal);
            bootstrapModal.show();
        }
    });
}

function addToCompare(productId) {
    let compareList = JSON.parse(localStorage.getItem('compareList') || '[]');
    if (compareList.includes(productId)) {
        compareList = compareList.filter(id => id !== productId);
    } else if (compareList.length < 4) {
        compareList.push(productId);
    }
    localStorage.setItem('compareList', JSON.stringify(compareList));
    updateCompareCount();
}

function updateCompareCount() {
    const compareList = JSON.parse(localStorage.getItem('compareList') || '[]');
    const badge = document.querySelector('.compare-badge');
    if (badge) { badge.textContent = compareList.length; badge.style.display = compareList.length > 0 ? 'inline' : 'none'; }
}
