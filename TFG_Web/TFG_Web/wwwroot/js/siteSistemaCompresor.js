$(document).ready(function () {
    //listPacientes();
});

// Formulario Control
$("#SelectSistemaCompresorIdPaciente").change(function () {
    var SelectSistemaCompresorIdPaciente = document.getElementById("SelectSistemaCompresorIdPaciente");
    var FormSistemaCompresorIdPaciente = document.getElementById("FormSistemaCompresorIdPaciente");
    var indicarPacienteError = document.getElementById("indicarPacienteError");
    indicarPacienteError.setAttribute('hidden','hidden');

    FormSistemaCompresorIdPaciente.value = SelectSistemaCompresorIdPaciente.value;
});

// Formulario Control
$("#BtnNextForm1Control").click(function () {
    var resultadoForm1 = formularioControlSistemaCompresorForm();

    if (resultadoForm1) {
         $("#navPagina2FormControl").trigger('click');   
    }
});

$("#BtnNextForm2Control").click(function () {
    //var resultadoForm2 = formularioControlform2();
    var resultadoForm2 = true;

    if (resultadoForm2) {
        $("#navPagina3FormControl").trigger('click');
    }
});


$('#clearControlForm1GroupGrup').click(function () {
    var grupo = document.getElementById("ControlForm1GroupGrupo");
    var numeroRadio = grupo.children.length;

    for (i = 0; i < numeroRadio; i++) {
        grupo.children[i].children[0].checked = false;
    }
});

//Popup Form1
$("#buttonPopupForm1").click(function () {
    $("#form1ModalEnviar").modal('show');
});

$("#buttonPopupCerrar").click(function () {
    $("#form1ModalEnviar").modal('hide');
});

$("#popupConfirmarForm1SistemaCompresor").click(function () {
    var FormSistemaCompresorIdPaciente = document.getElementById("FormSistemaCompresorIdPaciente");
    var indicarPacienteError = document.getElementById("indicarPacienteError");
    var error_ModalSubmitFormSistemaCompresor = document.getElementById("error_ModalSubmitFormSistemaCompresor");
    var spinner_ModalSubmitFormSistemaCompresor = document.getElementById("spinner_ModalSubmitFormSistemaCompresor");

    if (FormSistemaCompresorIdPaciente.value != null && FormSistemaCompresorIdPaciente.value != "") {
        let result = formularioControlSistemaCompresorForm();
        if (result) {
            error_ModalSubmitFormSistemaCompresor.setAttribute('hidden', 'hidden');
            spinner_ModalSubmitFormSistemaCompresor.removeAttribute('hidden');

            //Para que no salga la alert del refresh pages
            window.onbeforeunload = function (event) { };

            $("#submitForm1").trigger('click');
        }
        else {
            $("#form1ModalEnviar").modal('hide');
            error_ModalSubmitFormSistemaCompresor.removeAttribute('hidden');
        }
    }
    else {
        $("#form1ModalEnviar").modal('hide');
        spinner_ModalSubmitFormSistemaCompresor.setAttribute('hidden', 'hidden');
        indicarPacienteError.removeAttribute('hidden')
    }
});


//Control validar volores input null
function formularioControlSistemaCompresorForm() {
    var resultadoNext = true;

    var ControlForm1Fecha = document.getElementById("ControlSistemaCompresorForm1Fecha");
    var ControlForm1FechaInvalid = document.getElementById("ControlSistemaCompresorForm1FechaInvalid");
    if (!nextForm1Control(ControlForm1Fecha, ControlForm1FechaInvalid)) { resultadoNext = false }

    var ControlForm1PC = document.getElementById("ControlSistemaCompresorForm1PC");
    var ControlForm1PCInvalid = document.getElementById("ControlSistemaCompresorForm1PCInvalid");
    if (!nextForm1Control(ControlForm1PC, ControlForm1PCInvalid)) { resultadoNext = false }

    return resultadoNext;
}

function nextForm1Control(input, alert) {
    if (input.value == "") {
        alert.style.visibility = 'visible';
        input.classList.add('is-invalid')
        return false;
    }
    else {
        if (alert.style.visibility = 'visible') {
            alert.style.visibility = 'hidden';
            input.classList.remove('is-invalid')
        }
        return true;
    }
}

function nextForm1GroupControl(group, alert) {
    var numeroRadio = group.children.length;
    var resultadoChecked = true;

    for (i = 0; i < numeroRadio; i++) {
        if (!group.children[i].children[0].checked) {
            resultadoChecked = false;
        }
        else {
            if (alert.style.visibility = 'visible') {
                alert.style.visibility = 'hidden';
                for (i = 0; i < numeroRadio; i++) {
                    group.children[i].children[0].classList.remove('is-invalid')
                }
            }
            return true;
        }
    }
    if (!resultadoChecked) {
        alert.style.visibility = 'visible';
        for (i = 0; i < numeroRadio; i++) {
            group.children[i].children[0].classList.add('is-invalid')
        }
    }
    return resultadoChecked;
}