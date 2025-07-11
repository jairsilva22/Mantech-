function mostrarQrModal(qrPath, nombreMaquina) {
	document.getElementById('qrImagen').src = qrPath;
	document.getElementById('qrDescargar').href = qrPath;
	document.getElementById('qrNombreMaquina').textContent = nombreMaquina;

	const modal = new bootstrap.Modal(document.getElementById('qrModal'));
	modal.show();
}



async function mostrarCheckListModal(maquinaId) {
	maquinaIdSeleccionada = maquinaId;

	try {
		// Fetch de todos los checklists
		const todosResponse = await fetch(urlMaquinasCheckList, {
			method: 'GET',
			headers: {
				'Content-type': 'application/json'
			}
		});

		// Validar respuesta
		if (!todosResponse.ok) {
			throw new Error("Error al obtener todos los checklists");
		}

		const todos = await todosResponse.json(); // ahora es un array

		// Fetch de checklists asignados a la máquina
		const asignadosResponse = await fetch(`${urlMaquinasCheckList}/maquina/${maquinaId}`, {
			method: 'GET',
			headers: {
				'Content-type': 'application/json'
			}
		});

		if (!asignadosResponse.ok) {
			throw new Error("Error al obtener checklists asignados");
		}

		const asignados = await asignadosResponse.json(); // debe ser un array de IDs

		// Renderizar checklists
		const contenedor = document.getElementById("lista-checklists");
		contenedor.innerHTML = "";

		todos.forEach(c => {
			const checkbox = document.createElement("div");
			checkbox.classList.add("form-check");
			checkbox.innerHTML = `
				<input class="form-check-input" type="checkbox" value="${c.id}" id="chk_${c.id}"
					${asignados.includes(c.id) ? "checked" : ""}>
				<label class="form-check-label" for="chk_${c.id}">${c.nombre}</label>
			`;
			contenedor.appendChild(checkbox);
		});

		// Mostrar modal
		const modal = new bootstrap.Modal(document.getElementById("CheckListModal"));
		modal.show();
	} catch (error) {
		console.error("Error al cargar los checklists:", error);
		alert("Ocurrió un error al cargar los checklists.");
	}
}


document.getElementById("form-asignar-checklists").addEventListener("submit", async function (e) {
	e.preventDefault();

	const seleccionados = Array.from(document.querySelectorAll("#lista-checklists input:checked"))
		.map(chk => parseInt(chk.value));

	await fetch(`/api/MaquinasCheckLists/maquina/${maquinaIdSeleccionada}/asignar`, {
		method: "POST",
		headers: { "Content-Type": "application/json" },
		body: JSON.stringify(seleccionados)
	});

    mostrarMensajeExito("Checklists Asignados", "Los checklists se han asignado correctamente a la máquina.");
	bootstrap.Modal.getInstance(document.getElementById("CheckListModal")).hide();


});


