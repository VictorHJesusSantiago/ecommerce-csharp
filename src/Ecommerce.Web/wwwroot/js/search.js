document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const suggestionsContainer = document.getElementById('searchSuggestions');
    let debounceTimer;

    if (searchInput) {
        searchInput.addEventListener('input', function() {
            clearTimeout(debounceTimer);
            const query = this.value.trim();
            if (query.length < 2) { if (suggestionsContainer) suggestionsContainer.innerHTML = ''; return; }
            debounceTimer = setTimeout(() => fetchSuggestions(query), 300);
        });

        searchInput.addEventListener('keydown', function(e) {
            if (e.key === 'Enter') { e.preventDefault(); performSearch(); }
        });

        document.addEventListener('click', function(e) {
            if (suggestionsContainer && !searchInput.contains(e.target) && !suggestionsContainer.contains(e.target)) {
                suggestionsContainer.style.display = 'none';
            }
        });
    }

    function fetchSuggestions(query) {
        fetch(`/api/search/suggestions?q=${encodeURIComponent(query)}`)
            .then(r => r.json())
            .then(data => {
                if (suggestionsContainer && data.suggestions) {
                    suggestionsContainer.innerHTML = data.suggestions.map(s =>
                        `<div class="search-suggestion" onclick="selectSuggestion('${s}')">${s}</div>`
                    ).join('');
                    suggestionsContainer.style.display = 'block';
                }
            })
            .catch(() => {});
    }

    function performSearch() {
        const query = searchInput ? searchInput.value.trim() : '';
        if (query) { window.location.href = `/search?q=${encodeURIComponent(query)}`; }
    }

    const filters = document.querySelectorAll('.filter-checkbox');
    filters.forEach(filter => {
        filter.addEventListener('change', function() { applyFilters(); });
    });

    function applyFilters() {
        const params = new URLSearchParams(window.location.search);
        filters.forEach(filter => {
            if (filter.checked) { params.set(filter.name, filter.value); }
            else { params.delete(filter.name); }
        });
        window.location.search = params.toString();
    }

    const sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', function() {
            const params = new URLSearchParams(window.location.search);
            params.set('sortBy', this.value);
            window.location.search = params.toString();
        });
    }
});
