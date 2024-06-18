function showPage(pageNumber) {
    // Get all content elements
    var contents = document.querySelectorAll('.content');

    // Hide all content elements
    contents.forEach(function (content) {
        content.classList.remove('active');
    });

    // Show the selected page content
    document.getElementById('page' + pageNumber).classList.add('active');
}

// Initialize the first page as visible
document.addEventListener('DOMContentLoaded', function () {
    showPage(1);
});