document.addEventListener('DOMContentLoaded', function() {
    const notificationBell = document.getElementById('notificationBell');
    if (notificationBell) {
        notificationBell.addEventListener('click', function() {
            const dropdown = document.getElementById('notificationDropdown');
            if (dropdown) { dropdown.classList.toggle('show'); loadNotifications(); }
        });
    }

    const markAllRead = document.getElementById('markAllRead');
    if (markAllRead) {
        markAllRead.addEventListener('click', function() {
            fetch('/api/notifications/read-all', { method: 'PUT' })
            .then(r => r.json())
            .then(d => {
                document.querySelectorAll('.notification-item.unread').forEach(n => n.classList.remove('unread'));
                updateNotificationCount(0);
            });
        });
    }
});

function loadNotifications() {
    fetch('/api/notifications?pageSize=10')
    .then(r => r.json())
    .then(data => {
        const container = document.getElementById('notificationList');
        if (container && data.data) {
            container.innerHTML = data.data.map(n =>
                `<div class="notification-item ${n.isRead ? '' : 'unread'} p-3 border-bottom" onclick="window.location.href='${n.actionUrl || '#'}'">
                    <div class="d-flex justify-content-between"><strong>${n.title}</strong><small class="text-muted">${n.timeAgo}</small></div>
                    <p class="mb-0 text-muted small">${n.message}</p>
                </div>`
            ).join('');
            if (data.data.length === 0) { container.innerHTML = '<div class="p-3 text-center text-muted">No notifications</div>'; }
        }
    });
}

function updateNotificationCount(count) {
    const badge = document.querySelector('.notification-badge');
    if (badge) { badge.textContent = count; badge.style.display = count > 0 ? 'inline' : 'none'; }
}

function markAsRead(notificationId) {
    fetch(`/api/notifications/${notificationId}/read`, { method: 'PUT' });
}
