$(document).ready(function () {
    calcularEdadPaciente();

    // #region DataTable
    $('.table_PerfilPacienteCampanaPectusExcavatum').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

    $('.table_PerfilPacienteSistemaCompresor').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

    // #endregion Datatable
});

//Popup Modal Eliminar Sistema Compresor - Abrir
$("#buttonModalEliminarSistemaCompresor").click(function () {
    $("#modalEliminarSistemaCompresor").modal('show');
});

//Popup Modal Eliminar Sistema Compresor - Cerrar
$("#buttonPopupCerrarEliminarSistemaCompreosr").click(function () {
    $("#modalEliminarSistemaCompresor").modal('hide');
});

//Popup Modal Eliminar Campana Pectus Excavatum - Abrir
$("#buttonModalEliminarCampanaPectusExcavatum").click(function () {
    $("#modalEliminarCampanaPectusExcavatum").modal('show');
});

//Popup Modal Eliminar Campana Pectus Excavatum - Cerrar
$("#buttonPopupCerrarEliminarCampanaPectusExcavatum").click(function () {
    $("#modalEliminarCampanaPectusExcavatum").modal('hide');
});


// --------- Finalizar Tratamiento -----------

//Popup Modal Enviar Informe - Abrir
$("#buttonModalEnviarInforme").click(function () {
    $("#modalEnviarInformePaciente").modal('show');
});

//Popup Modal Finalizar Tratamiento - Abrir
$("#buttonModalFinalizarTratamiento").click(function () {
    $("#modalFinalizarTratamiento").modal('show');
});

//Popup Modal Finalizar Tratamiento - Cerrar
$("#buttonPopupCerrarFinalizarTratamiento").click(function () {
    $("#modalFinalizarTratamiento").modal('hide');
});
//Popup Modal - Cerrar 
$("#buttonPopupCerrarEnviarInforme").click(function () {
    $("#modalEnviarInformePaciente").modal('hide');
});

// ------ Popup Modal Eliminar Paciente ------
$("#buttonModalEliminarPaciente").click(function () {
    $("#modalEliminarPaciente").modal('show');
});

$("#buttonCerrarModalEliminarPaciente").click(function () {
    $("#modalEliminarPaciente").modal('hide');
});


// #region Modal - Control Eliminacion Paciente
$("#buttonSubmitModalEliminarPaciente").click(function () {

    let inputModalEliminarPacienteControl = document.getElementById("inputModalEliminarPacienteControl");
    let modalEliminarPaciente_NombrePaciente = document.getElementById("modalEliminarPaciente_NombrePaciente");
    let submitModalEliminarPaciente = document.getElementById("submitModalEliminarPaciente");
    let modalEliminarPacienteErrorMensaje = document.getElementById("modalEliminarPacienteErrorMensaje");

    if (inputModalEliminarPacienteControl.value == modalEliminarPaciente_NombrePaciente.innerHTML) {
        submitModalEliminarPaciente.click();
    }
    else
    {
        modalEliminarPacienteErrorMensaje.removeAttribute('hidden');
    }
});

$("#inputModalEliminarPacienteControl").change(function () {
    let modalEliminarPacienteErrorMensaje = document.getElementById("modalEliminarPacienteErrorMensaje");
    modalEliminarPacienteErrorMensaje.setAttribute('hidden', 'hidden');
});
// #endregion ------ END Popup Modal Eliminar Paciente END ------


// #region ---------- Campana Pectus Excavatum - Table/Form -----------
var formCampanaPectusExcavatumInputs = document.getElementById("formCampanaPectusExcavatumInputs");
var formCampanaPectusExcavatumInputs2 = document.getElementById("formCampanaPectusExcavatumInputs2");
var buttonsSubmitFormControlPectusExcavatumEditar = document.getElementById("buttonsSubmitFormControlPectusExcavatumEditar");

$("#buttonCampanaPectusExcavatumEditar").click(function () {
    buttonsSubmitFormControlPectusExcavatumEditar.removeAttribute('hidden');

    for (let i = 0; i < formCampanaPectusExcavatumInputs.children.length; i++) {
        formCampanaPectusExcavatumInputs.children[i].children[0].removeAttribute('disabled');
    }

    for (let i = 0; i < formCampanaPectusExcavatumInputs2.children.length; i++) {
        formCampanaPectusExcavatumInputs2.children[i].children[0].removeAttribute('disabled');
    }
});

$("#buttonCampanaPectusExcavatumEditarHidden").click(function () {
    buttonsSubmitFormControlPectusExcavatumEditar.setAttribute('hidden','hidden');

    for (let i = 0; i < formCampanaPectusExcavatumInputs.children.length; i++) {
        formCampanaPectusExcavatumInputs.children[i].children[0].disabled = true;
    }

    for (let i = 0; i < formCampanaPectusExcavatumInputs2.children.length; i++) {
        formCampanaPectusExcavatumInputs2.children[i].children[0].disabled = true;
    }
});
// #endregion ---------- Campana Pectus Excavatum - Table/Form -----------


// #region ------------ Sistema Compresor - Table/Form --------------
var formSistemaCompresorInputs = document.getElementById("formSistemaCompresorInputs");
var formSistemaCompresorInputs2 = document.getElementById("formSistemaCompresorInputs2");
var buttonsSubmitFormSistemaCompresorEditar = document.getElementById("buttonsSubmitFormSistemaCompresorEditar");

$("#buttonSistemaCompresorEditar").click(function () {
    buttonsSubmitFormSistemaCompresorEditar.removeAttribute('hidden');

    for (let i = 0; i < formSistemaCompresorInputs.children.length; i++) {
        formSistemaCompresorInputs.children[i].children[0].removeAttribute('disabled');
        formSistemaCompresorInputs.children[i].children[0].setAttribute("style", "border:1px solid black;");
    }

    for (let i = 0; i < formSistemaCompresorInputs2.children.length; i++) {
        formSistemaCompresorInputs2.children[i].children[0].removeAttribute('disabled');
        formSistemaCompresorInputs2.children[i].children[0].setAttribute("style", "border:1px solid black;");
    }
});

$("#buttonSistemaCompresorEditarHidden").click(function () {
    buttonsSubmitFormSistemaCompresorEditar.setAttribute('hidden', 'hidden');

    for (let i = 0; i < formSistemaCompresorInputs.children.length; i++) {
        formSistemaCompresorInputs.children[i].children[0].disabled = true;
        formSistemaCompresorInputs.children[i].children[0].setAttribute("style", "text-align:center;");
    }

    for (let i = 0; i < formSistemaCompresorInputs2.children.length; i++) {
        formSistemaCompresorInputs2.children[i].children[0].disabled = true;
        formSistemaCompresorInputs2.children[i].children[0].setAttribute("style", "text-align:center;");
    }
});
// #endregion ------------ END Sistema Compresor - Table/Form --------------


// #region ------------ PerfilPaciente - Forms (HistorialClinico) ------------------

//Funcion para poder editar los inputs del formulario
function BotonEditarInputs_PerfilPaciente(historial, buttonForm, form) {
    //Buttons Guardar & Cancelar
    let buttonsSubmitEditar = document.querySelector("#".concat(historial).concat(" ").concat("#").concat(buttonForm));
    buttonsSubmitEditar.removeAttribute('hidden');

    //Inputs
    let formPerfilPacienteInputs = document.querySelector("#".concat(historial).concat(" ").concat("#").concat(form));
    let inputs = formPerfilPacienteInputs.getElementsByTagName("input");
    for (i = 0; i < inputs.length; i++) {
        inputs[i].removeAttribute('disabled');
        inputs[i].setAttribute("style", "border:1px solid black;");
    }   
}
// #endregion ------------ PerfilPaciente - Forms (HistorialClinico) ------------------


// #region ------- Boton de Editar Forms ----------
$("#buttonPerfilPacienteEditarInputs").click(function () {
    let buttonPerfilPacienteEditarInputs = document.getElementById("buttonPerfilPacienteEditarInputs");

    //Attribute button Editar
    if (buttonPerfilPacienteEditarInputs.hasAttribute("navActive"))
    {
        let nav = buttonPerfilPacienteEditarInputs.attributes.navactive.value;

        //-------- Historial 1 -----------

        //Form1 - Editar
        if (nav == "form1")
        {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitForm1SistemaCompresorEditar", "form1PerfilPacienteInputs");
            let edadPacienteDate = document.getElementsByClassName("edadPacienteDate");
            let edadPacienteNumber = document.getElementsByClassName("edadPacienteNumber");
            edadPacienteDate[0].removeAttribute('hidden');
            edadPacienteNumber[0].setAttribute("hidden", "hidden");
        }

        //Form2 - Editar
        if (nav == "form2")
        {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitForm2SistemaCompresorEditar", "form2PerfilPacienteInputs");
        }

        if (nav == "form3") {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitForm3SistemaCompresorEditar", "form3PerfilPacienteInputs");
        }

        if (nav == "form4") {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitForm4SistemaCompresorEditar", "form4PerfilPacienteInputs");
        }

        if (nav == "form5") {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitForm5SistemaCompresorEditar", "form5PerfilPacienteInputs");
        }

        if (nav == "form6") {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitForm6SistemaCompresorEditar", "form6PerfilPacienteInputs");
        }

        if (nav == "formTipoPectus") {
            BotonEditarInputs_PerfilPaciente("formHistorial1", "buttonsSubmitFormTipoPectusSistemaCompresorEditar", "formTipoPectusPerfilPacienteInputs");
        }

       //-------- Historial 2 -----------

        if (nav == "form1Historial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitForm1SistemaCompresorEditar", "form1PerfilPacienteInputs"); 
        }

        if (nav == "form2Historial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitForm2SistemaCompresorEditar", "form2PerfilPacienteInputs");
        }

        if (nav == "form3Historial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitForm3SistemaCompresorEditar", "form3PerfilPacienteInputs");
        }

        if (nav == "form4Historial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitForm4SistemaCompresorEditar", "form4PerfilPacienteInputs");
        }

        if (nav == "form5Historial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitForm5SistemaCompresorEditar", "form5PerfilPacienteInputs");
        }

        if (nav == "form6Historial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitForm6SistemaCompresorEditar", "form6PerfilPacienteInputs");
        }

        if (nav == "formTipoPectusHistorial2") {
            BotonEditarInputs_PerfilPaciente("formHistorial2", "buttonsSubmitFormTipoPectusSistemaCompresorEditar", "formTipoPectusPerfilPacienteInputs");
        }
    }
});
// #endregion ------- Boton de Editar Forms ----------


// #region ---------------- Historial 1 Forms EDITAR ------------------
var buttonPerfilPacienteEditarInputs = document.getElementById("buttonPerfilPacienteEditarInputs"); 

//Navs click Historial1 (Historial1) - Editar
$("#navPagina1FormPerfilHistorial").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form1");
});

//Navs click form1 (Historial1) - Editar
$("#navPagina1FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form1");
});

//Navs click form2 (Historial1) - Editar
$("#navPagina2FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form2");
});

//Navs click form3 (Historial1) - Editar
$("#navPagina3FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form3");
});

//Navs click form4 (Historial1) - Editar
$("#navPagina4FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form4");
});

//Navs click form5 (Historial1) - Editar
$("#navPagina5FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form5");
});

//Navs click form6 (Historial1) - Editar
$("#navPagina6FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form6");
});

//Navs click formTipoPectus (Historial1) - Editar
$("#navPagina7FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "formTipoPectus");
});

//Navs click formSistemaCompresor (Historial1) - Editar
$("#formHistorial1 #navPagina8FormUsuario").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "");
});

//Navs click formPectusExcavatum (Historial1) - Editar
$("#formHistorial1 #navPagina9FormUsuario").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "");
});
// #endregion ---------------- Historial 1 Forms EDITAR ------------------


// #region ---------------- Historial 2 Forms EDITAR ------------------
var buttonPerfilPacienteEditarInputs = document.getElementById("buttonPerfilPacienteEditarInputs"); 

//Navs click Historial2 - Editar
$("#navPagina2FormPerfilHistorial").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form1Historial2");
});

//Navs click form1 (Historial2) - Editar
$("#formHistorial2 #navPagina1FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form1Historial2");
});

//Navs click form2 (Historial2) - Editar
$("#formHistorial2 #navPagina2FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form2Historial2");
});

//Navs click form3 (Historial2) - Editar
$("#formHistorial2 #navPagina3FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form3Historial2");
});

//Navs click form4 (Historial2) - Editar
$("#formHistorial2 #navPagina4FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form4Historial2");
});

//Navs click form5 (Historial2) - Editar
$("#formHistorial2 #navPagina5FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form5Historial2");
});

//Navs click form6 (Historial2) - Editar
$("#formHistorial2 #navPagina6FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "form6Historial2");
});

//Navs click formTipoPectus (Historial2) - Editar
$("#formHistorial2 #navPagina7FormPerfilPacinet").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "formTipoPectusHistorial2");
});

//Navs click formSistemaCompresor (Historial2) - Editar
$("#formHistorial2 #navPagina8FormUsuario").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "");
});

//Navs click formPectusExcavatum (Historial2) - Editar
$("#formHistorial2 #navPagina9FormUsuario").click(function () {
    buttonPerfilPacienteEditarInputs.setAttribute("navActive", "");
});

// #endregion ---------------- Historial 2 Forms EDITAR ------------------


// #region -------------- Boton Cancelar - Editar -----------------
//Funcion del Boton Cancel Inputs Forms - Editar
function BotonCancelarEditarForm_PerfilPaciente(historial, botonCancelar, form) {
    //Buttons Guardar & Cancelar
    let querySelector = "#".concat(historial).concat(" ").concat("#").concat(botonCancelar);
    let buttonsSubmitFormEditar = document.querySelector(querySelector);
    buttonsSubmitFormEditar.setAttribute('hidden', 'hidden');

    //Inputs
    let formPerfilPacienteInputs = document.querySelector("#".concat(historial).concat(" ").concat("#").concat(form));
    let inputs = formPerfilPacienteInputs.getElementsByTagName("input");
    for (i = 0; i < inputs.length; i++) {
        inputs[i].disabled = true;
        inputs[i].removeAttribute('style');
    }
    topFunction();
}

//Button Cancel Inputs Form1 (Historial 1) - Editar
$("#formHistorial1 #buttonForm1SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitForm1SistemaCompresorEditar", "form1PerfilPacienteInputs");

    let edadPacienteDate = document.getElementsByClassName("edadPacienteDate");
    let edadPacienteNumber = document.getElementsByClassName("edadPacienteNumber");
    edadPacienteDate[0].setAttribute("hidden", "hidden");
    edadPacienteNumber[0].removeAttribute('hidden');
});

//Button Cancel Inputs Form1 (Historial 2) - Editar
$("#formHistorial2 #buttonForm1SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitForm1SistemaCompresorEditar", "form1PerfilPacienteInputs");
});

//Button Cancel Inputs Form2 (Historial 1) - Editar
$("#formHistorial1 #buttonForm2SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitForm2SistemaCompresorEditar", "form2PerfilPacienteInputs");
});

//Button Cancel Inputs Form2 (Historial 2) - Editar
$("#formHistorial2 #buttonForm2SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitForm2SistemaCompresorEditar", "form2PerfilPacienteInputs");
});

//Button Cancel Inputs Form3 (Historial 1) - Editar
$("#formHistorial1 #buttonForm3SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitForm3SistemaCompresorEditar", "form3PerfilPacienteInputs");
});

//Button Cancel Inputs Form3 (Historial 2) - Editar
$("#formHistorial2 #buttonForm3SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitForm3SistemaCompresorEditar", "form3PerfilPacienteInputs");
});

//Button Cancel Inputs Form4 (Historial 1) - Editar
$("#formHistorial1 #buttonForm4SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitForm4SistemaCompresorEditar", "form4PerfilPacienteInputs");
})

//Button Cancel Inputs Form4 (Historial 2) - Editar
$("#formHistorial2 #buttonForm4SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitForm4SistemaCompresorEditar", "form4PerfilPacienteInputs");
})

//Button Cancel Inputs Form5 (Historial 1)- Editar
$("#formHistorial1 #buttonForm5SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitForm5SistemaCompresorEditar", "form5PerfilPacienteInputs");
})

//Button Cancel Inputs Form5 (Historial 2)- Editar
$("#formHistorial2 #buttonForm5SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitForm5SistemaCompresorEditar", "form5PerfilPacienteInputs");
})

//Button Cancel Inputs Form6 (Historial 1) - Editar
$("#formHistorial1 #buttonForm6SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitForm6SistemaCompresorEditar", "form6PerfilPacienteInputs");
})

//Button Cancel Inputs Form6 (Historial 2) - Editar
$("#formHistorial2 #buttonForm6SistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitForm6SistemaCompresorEditar", "form6PerfilPacienteInputs");
})

//Button Cancel Inputs FormTipoPectus (Historial 1) - Editar
$("#formHistorial1 #buttonFormTipoPectusSistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial1", "buttonsSubmitFormTipoPectusSistemaCompresorEditar", "formTipoPectusPerfilPacienteInputs");
})

//Button Cancel Inputs FormTipoPectus (Historial 2) - Editar
$("#formHistorial2 #buttonFormTipoPectusSistemaCompresorEditarHidden").click(function () {
    BotonCancelarEditarForm_PerfilPaciente("formHistorial2", "buttonsSubmitFormTipoPectusSistemaCompresorEditar", "formTipoPectusPerfilPacienteInputs");
})
// #endregion -------------- Boton Cancelar - Editar -----------------


//Generar informe paciente - Nuevo tab
$("#perfilPacienteGenerarInformer").click(function () {
    let perfilPacienteGenerarInformer = document.getElementById("perfilPacienteGenerarInformer");
    let prova = perfilPacienteGenerarInformer.attributes.idPaciente;

    window.open("/InformePaciente?id=" + prova.value);
});

//Edad Paciente - Calcular

function calcularEdadPaciente() {
    let edadPacienteNumber = document.getElementsByClassName("edadPacienteNumber");

    var fechaNacimiento = new Date(edadPacienteNumber[0].defaultValue);
    var diff_ms = Date.now() - fechaNacimiento;
    var age_dt = new Date(diff_ms);
    var edad = Math.abs(age_dt.getUTCFullYear() - 1970);

    edadPacienteNumber[0].defaultValue = edad;
}

//Button para subir la page
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}