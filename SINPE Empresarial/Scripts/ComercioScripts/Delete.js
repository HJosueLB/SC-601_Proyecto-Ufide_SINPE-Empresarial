window.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll(".btn-eliminar").forEach(function (btn) {
        btn.addEventListener("click", function (e) {
            e.preventDefault();

            const id = this.dataset.id;

            Swal.fire({
                title: '¿Eliminar comercio?',
                text: "Esta acción no se puede deshacer.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    fetch('/Comercio/Eliminar', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ id: id })
                    })
                        .then(response => response.json())
                        .then(data => {
                            if (data.success) {
                                Swal.fire('¡Eliminado!', data.mensaje, 'success')
                                    .then(() => location.reload());
                            } else {
                                Swal.fire('Error', data.mensaje, 'error');
                            }
                        })
                        .catch(() => Swal.fire('Error', 'No se pudo eliminar el comercio.', 'error'));
                }
            });
        });
    });
});
