function confirmarEliminarUsuario(url) {
    if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
        window.location.href = url;
    }
    return false;
}