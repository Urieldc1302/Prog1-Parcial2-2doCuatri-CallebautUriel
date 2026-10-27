const API_URL = "http://localhost:5050/api/talleres";

document.addEventListener("DOMContentLoaded", () => {
    cargarTalleres();
});

async function cargarTalleres() {
    const estadoCarga = document.getElementById("estado-carga");
    const mensajeError = document.getElementById("mensaje-error");
    const tabla = document.getElementById("tabla-talleres");
    const cuerpoTabla = document.getElementById("cuerpo-tabla");

    try {
        const respuesta = await fetch(API_URL);
        if (!respuesta.ok) {
            throw new Error(`Error en el servidor: ${respuesta.status}`);
        }

        const talleres = await respuesta.json();
        cuerpoTabla.innerHTML = "";

        talleres.forEach(taller => {
            const fila = document.createElement("tr");

            const tdNombre = document.createElement("td");
            tdNombre.textContent = taller.nombre;

            const tdDia = document.createElement("td");
            tdDia.textContent = taller.dia;

            const tdHorario = document.createElement("td");
            tdHorario.textContent = `${taller.horaInicio}:00 - ${taller.horaFin}:00`;

            const tdCupos = document.createElement("td");
            tdCupos.textContent = taller.cuposDisponibles;

            fila.appendChild(tdNombre);
            fila.appendChild(tdDia);
            fila.appendChild(tdHorario);
            fila.appendChild(tdCupos);

            cuerpoTabla.appendChild(fila);
        });

        estadoCarga.classList.add("oculto");
        tabla.classList.remove("oculto");
    } catch (error) {
        estadoCarga.classList.add("oculto");
        mensajeError.textContent = `No se pudieron cargar los talleres: ${error.message}`;
        mensajeError.classList.remove("oculto");
    }
}
