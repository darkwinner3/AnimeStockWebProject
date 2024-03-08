window.addEventListener('DOMContentLoaded', function () {
    adjustSidebarHeight();
});

function adjustSidebarHeight() {
    const sidebarContainer = document.querySelector('.sidebar-container');
    const footer = document.querySelector('footer');

    const sidebarHeight = footer.offsetTop - sidebarContainer.offsetTop;
    sidebarContainer.style.height = sidebarHeight + 'px';
}