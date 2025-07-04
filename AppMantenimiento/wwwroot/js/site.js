//Scripts de el layout principal
function setActive(element) {
    // Remover clase active de todos los enlaces
    document.querySelectorAll('.nav-link').forEach(link => {
        link.classList.remove('active');
    });

    // Agregar clase active al enlace clickeado
    element.classList.add('active');
}

// Restaurar estado del sidebar al cargar la página
document.addEventListener('DOMContentLoaded', function () {
    const sidebarExpanded = localStorage.getItem('sidebarExpanded') === 'true';
    if (sidebarExpanded) {
        document.getElementById('sidebar').classList.add('expanded');
    }
});


function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('expanded');

    // Mostrar u ocultar el overlay si existe
    const overlay = document.querySelector('.sidebar-overlay');
    if (overlay) {
        overlay.style.display = sidebar.classList.contains('expanded') ? 'block' : 'none';
    }

    // Guardar estado en localStorage si quieres persistencia
    localStorage.setItem('sidebarExpanded', sidebar.classList.contains('expanded'));
}

// Cerrar sidebar al hacer clic en un enlace
document.querySelectorAll('.nav-link').forEach(link => {
    link.addEventListener('click', function () {
        if (window.innerWidth <= 768) {
            const sidebar = document.getElementById('sidebar');
            sidebar.classList.remove('expanded');

            const overlay = document.querySelector('.sidebar-overlay');
            if (overlay) {
                overlay.style.display = 'none';
            }
        }
    });
});

// Opcional: cerrar si se hace clic en el overlay
document.addEventListener('DOMContentLoaded', function () {
    const overlay = document.querySelector('.sidebar-overlay');
    if (overlay) {
        overlay.addEventListener('click', function () {
            document.getElementById('sidebar').classList.remove('expanded');
            overlay.style.display = 'none';
        });
    }
});




// Efectos de entrada para los campos
document.addEventListener('DOMContentLoaded', function () {
    const formGroups = document.querySelectorAll('.form-group');

    formGroups.forEach((group, index) => {
        group.style.opacity = '0';
        group.style.transform = 'translateY(20px)';

        setTimeout(() => {
            group.style.transition = 'all 0.5s ease';
            group.style.opacity = '1';
            group.style.transform = 'translateY(0)';
        }, index * 100);
    });
});

// Validación visual básica
document.querySelectorAll('.form-control').forEach(input => {
    input.addEventListener('blur', function () {
        if (this.value.trim() === '') {
            this.style.borderColor = '#ff6b6b';
        } else {
            this.style.borderColor = '#28a745';
        }
    });

    input.addEventListener('focus', function () {
        this.style.borderColor = '#667eea';
    });
});
