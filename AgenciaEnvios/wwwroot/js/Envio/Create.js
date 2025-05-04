document.querySelector("#Dto_TipoEnvio").addEventListener('change', MostrarDatosPropiosDelTipoEnvio);
MostrarDatosPropiosDelTipoEnvio();


function MostrarDatosPropiosDelTipoEnvio() {

    let tipoSel = document.querySelector("#Dto_TipoEnvio").value;
    console.log(tipoSel);

    if (tipoSel == "Urgente") {
        document.querySelector("#Comun").style.display = "none";
        document.querySelector("#Urgente").style.display = "block";
    } else {
        document.querySelector("#Comun").style.display = "block";
        document.querySelector("#Urgente").style.display = "none";
    }

}