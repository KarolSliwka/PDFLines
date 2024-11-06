$(document).ready(function () {
    // Tooltip activate 
    $('[data-bs-toggle=tooltip]').tooltip();

    // Toggle hamburger button
    const navToggle = document.getElementById('btn-hamburger');
    navToggle.addEventListener('click', () => {
        navToggle.classList.toggle('open')
    });

    $(".is-corrected").change(function () {
        let label = $(this).next();
        if (this.checked == false) {
            label.html("Nie");
        }
        else {
            label.html("Tak");
        }
    });
});

/**
 * This function will add active link class to an element
 */
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.nav-link').forEach(link => {
        if (link.getAttribute('href').toLowerCase() === location.pathname.toLowerCase()) {
            link.classList.add('active');
        } else {
            link.classList.remove('active');
        }
    });
});
