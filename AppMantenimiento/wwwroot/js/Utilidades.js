async function manejarErrorApi(respuesta) {
    let mensajeError = "";
    if (respuesta.status === 400) {
        mensajeError = await respuesta.text();
    } else if (respuesta.status === 404) {
        mensajeError = "Recurso no encontrado (404)";
    } else if (respuesta.status === 500) {
        mensajeError = "Error interno del servidor (500)";
    } else if (respuesta.status === 403) {
        mensajeError = "Acceso denegado (403)";
    }


    mostrarMensajeError(mensajeError);
}

function mostrarMensajeError(mensaje) {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: mensaje,
    });
}

//alerta para eliminar cualquier accion 
function confirmarEliminacion({ callbackAceptar, callbackCancelar, nombre }) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: `¿Deseas eliminar  "${nombre}"?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, eliminar',
        focusConfirm: true,
    }).then((result) => {
        if (result.isConfirmed) {
            callbackAceptar();
        } else if (callbackCancelar) {
            callbackCancelar();
        }
    });
}