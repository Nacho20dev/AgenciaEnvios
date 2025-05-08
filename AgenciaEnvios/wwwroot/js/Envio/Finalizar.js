function confirmarFinalizarEnvio(url) {
    if (confirm("¿Estás seguro de que deseas finalizar este envío?")) {
        window.location.href = url;
    }
    return false;
}