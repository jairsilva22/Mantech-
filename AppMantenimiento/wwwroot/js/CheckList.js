
function agregarNuevoCheckListAlListado() { 

    checklistListadoViewModel.checkLists.push(new checklistElementoViewModel({ id: 0, nombre: '' }));
    $("[name=titulo-checkList]").last().focus();


}


async function manejarFocusOutTituloTarea(checkList) {
    console.log('manejarFocusOutTituloTarea', checkList);
    const nombre = checkList.nombre();

    if (!nombre) {
        checklistListadoViewModel.checkLists.pop(checkList);
        return;
    }


    const data = JSON.stringify({ nombre: nombre });

    const respuesta = await fetch(urlCheckList, {
        method: 'POST',
        body: data,
        headers: {
            'Content-type': 'application/json'
        }
    });

    if (respuesta.ok) {
        const json = await respuesta.json();
        checkList.id(json.id); // actualizas el ID del observable
    } else {
        manejarErrorApi(respuesta);
    }
}



async function obtenerCheckLists() {

    checklistListadoViewModel.cargando(true);

    const respuesta = await fetch(urlCheckList, {
        method: 'GET',
        headers: {
            'Content-type': 'application/json'
        }

    });

    console.log(respuesta);
    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
    }
    const json = await respuesta.json();
    checklistListadoViewModel.checkLists([]);
    json.forEach(tarea => {
        checklistListadoViewModel.checkLists.push(new checklistElementoViewModel(tarea));
    });

    checklistListadoViewModel.cargando(false);
}




async function manejarClickCheckList(checkList) {
    console.log('manejarClickCheckList', checkList.id());
    if (checkList.esNuevo()) {
        return;
    }

    const respuesta = await fetch(`${urlCheckList}/${checkList.id()}`, {
        method: 'GET',
        headers: {
            'Content-type': 'application/json'
        }
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
        return;
    }

    const json = await respuesta.json();

    console.log(json);

    checkListEditViewModel.nombre(json.nombre);
    checkListEditViewModel.id = json.id;

    checkListEditViewModel.items([]);
    await cargarItemsDelChecklist(checkList.id());

    modalEditarCheckListBootstrap.show();


}


//Funcuiones para editar checklist

async function manejarEditarCheckList() {
    const obj = {
        id: checkListEditViewModel.id,
        nombre: checkListEditViewModel.nombre(),
    };


    if (!obj.nombre) {
       
        return;
    }

    await editarCheckList(obj);

    //aqui me busca el indice del checklist que se esta editando y lo actualiza en el listado
    const indice = checklistListadoViewModel.checkLists().findIndex(t => t.id() === checkListEditViewModel.id);
    const checkList = checklistListadoViewModel.checkLists()[indice];
    checkList.nombre(obj.nombre);
    checkListEditViewModel.limpiar();

}

async function editarCheckList(checkList) {

    const data = JSON.stringify(checkList);
    const respuesta = await fetch(`${urlCheckList}/${checkList.id}`, {
        method: 'PUT',
        body: data,
        headers: {
            'Content-type': 'application/json'
        }
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
        //throw "error al editar tarea";
    }

}


//Funciuones para eliminar checklist
function intentarBorrarCheckList(checkList) {
    modalEditarCheckListBootstrap.hide();
    confirmarEliminacion({
        nombre: checkList.nombre(),
        callbackAceptar: async function () {
            await eliminarCheckList(checkList);

        },
        callbackCancelar: function () {
            // No hacer nada
            modalEditarCheckListBootstrap.show();
        }
    });
}

async function eliminarCheckList(checkList) {
  
    const id = checkList.id;
    const respuesta = await fetch(`${urlCheckList}/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-type': 'application/json'
        }
    });
    if (respuesta.ok) {
        const indice = checklistListadoViewModel.checkLists().findIndex(t => t.id() === id);
        checklistListadoViewModel.checkLists.splice(indice, 1);
    }
}
