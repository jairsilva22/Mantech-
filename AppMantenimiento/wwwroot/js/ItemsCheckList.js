function manejarClickAgregarItem() {
    checkListEditViewModel.items.push(new itemsViewModel({ modoEdicion: true }));

    setTimeout(() => {
        $("[name=txtItemsDescripcion]:visible").last().focus();
    }, 0);
}

function manejarClickCancelarItem(item) {
	if (item.esNuevo()) {
		checkListEditViewModel.items.pop();

	} else {
		item.modoEdicion(false);
        item.descripcion(item.descrpcionAnterior); 
	}
}   


async function manejarClickSalvarItem(item) {
	const descripcion = item.descripcion();

	if (!descripcion) {
		item.descripcion(item.descripcionAnterior); // restauramos si estaba editando
		if (item.esNuevo()) {
			checkListEditViewModel.items.remove(item); // si era nuevo, lo quitamos
		}
		return;
	}

	const idCheckList = checkListEditViewModel.id;

	const data = JSON.stringify({
		descripcion: descripcion,
		checkListId: idCheckList
	});

	if (item.esNuevo()) {
		await insertarItem(item, data);
	} else {
		await actualizarItem(item, data);
	}
}


async function insertarItem(item, data) {
	const respuesta = await fetch(`${urlItems}`, {
		method: 'POST',
		body: data,
		headers: {
			'Content-type': 'application/json'
		}
	});

	if (respuesta.ok) {
		const json = await respuesta.json();
		item.id(json.id); // asignamos el nuevo ID
		item.modoEdicion(false);
	} else {
		manejarErrorApi(respuesta);
	}
}



async function cargarItemsDelChecklist(idCheckList) {
	try {
		const respuesta = await fetch(`${urlItems}/${idCheckList}`);
		if (!respuesta.ok) {
			if (respuesta.status === 404) {
				checkListEditViewModel.items([]); // No hay ítems, limpiamos
				return;
			}
			throw new Error("Error al cargar ítems.");
		}

		const data = await respuesta.json();

		const items = data.map(item => new itemsViewModel({
			id: item.id,
			descripcion: item.descripcion,
			modoEdicion: false
		}));

		checkListEditViewModel.items(items);

	} catch (error) {
		console.error(error);
		alert("Ocurrió un error al cargar los ítems.");
	}
}




function manejarClickitem(item) {
	item.modoEdicion(true);
    item.descrpcionAnterior = item.descripcion(); 

   $("[name=txtItemsDescripcion]:visible").focus();
   
}



async function actualizarItem(item, data) {
	const respuesta = await fetch(`${urlItems}/${item.id()}`, {
		method: 'PUT',
		body: data,
		headers: {
			'Content-type': 'application/json'
		}
	});

	if (respuesta.ok) {
		item.modoEdicion(false);
	} else {
		manejarErrorApi(respuesta);
	}
}




function manejarClickBorrarItem(item) {
	modalEditarCheckListBootstrap.hide();
	confirmarEliminacion({
		nombre: item.descripcion(),
		callbackAceptar: async function () {
            modalEditarCheckListBootstrap.show();
			await eliminarItem(item);

		},
		callbackCancelar: function () {
			// No hacer nada
			modalEditarCheckListBootstrap.show();
		}
	});
}

async function eliminarItem(item) {
	const respuesta = await fetch(`${urlItems}/${item.id()}`, {
		method: 'DELETE',
		headers: {
			'Content-type': 'application/json'
		}
	});
	if (!respuesta.ok) {
		manejarErrorApi(respuesta);
		return;
	}

    checkListEditViewModel.items.remove(item); // Eliminamos el item de la lista
		
}