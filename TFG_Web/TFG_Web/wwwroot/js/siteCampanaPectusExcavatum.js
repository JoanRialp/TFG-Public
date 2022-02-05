$(document).ready(function () {
    //listPacientes();
});

// Formulario Control
$("#SelectCampanaPectusExcavatumIdPaciente").change(function () {
    var SelectCampanaPectusExcavatumIdPaciente = document.getElementById("SelectCampanaPectusExcavatumIdPaciente");
    var FormCampanaPectusExcavatumIdPaciente = document.getElementById("FormCampanaPectusExcavatumIdPaciente");
    var indicarPacienteError = document.getElementById("indicarPacienteError");
    indicarPacienteError.setAttribute('hidden','hidden');

    FormCampanaPectusExcavatumIdPaciente.value = SelectCampanaPectusExcavatumIdPaciente.value;

});

// Formulario Control
$("#BtnNextForm1ControlCampanaPectus").click(function () {
    var resultadoForm1 = formularioControlform1();

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

//Popup Form1
$("#buttonPopupForm1").click(function () {
    $("#form1ModalEnviar").modal('show');
});

$("#buttonPopupCerrar").click(function () {
    $("#form1ModalEnviar").modal('hide');
});

$("#popupConfirmarFormControlCampanaPectusExcavatum").click(function () {
    var FormSistemaCompresorIdPaciente = document.getElementById("FormCampanaPectusExcavatumIdPaciente");
    var indicarPacienteError = document.getElementById("indicarPacienteError");
    var error_ModalSubmitFormCampanaPectusExcavatum = document.getElementById("error_ModalSubmitFormCampanaPectusExcavatum");
    var spinner_ModalSubmitFormCampanaPectusExcavatum = document.getElementById("spinner_ModalSubmitFormCampanaPectusExcavatum");

    if (FormSistemaCompresorIdPaciente.value != null && FormSistemaCompresorIdPaciente.value != "")
    {

        if (formularioControlform1()) {
            error_ModalSubmitFormCampanaPectusExcavatum.setAttribute('hidden', 'hidden');
            spinner_ModalSubmitFormCampanaPectusExcavatum.removeAttribute('hidden');

            //Para que no salga la alert del refresh pages
            window.onbeforeunload = function (event) { };

            $("#submitForm1").trigger('click');
        }
        else {
            $("#form1ModalEnviar").modal('hide');
            error_ModalSubmitFormCampanaPectusExcavatum.removeAttribute('hidden');
        }
    }
       
    else {
        $("#form1ModalEnviar").modal('hide');
        spinner_ModalSubmitFormCampanaPectusExcavatum.setAttribute('hidden', 'hidden');
        indicarPacienteError.removeAttribute('hidden')
    }
});

//Control validar volores input null
function formularioControlform1() {
    var resultadoNext = true;

    var ControlForm1CampanaPectusExcavatumFecha = document.getElementById("ControlForm1CampanaPectusExcavatumFecha");
    var ControlForm1CampanaPectusExcavatumFechaInvalid = document.getElementById("ControlForm1CampanaPectusExcavatumFechaInvalid");
    if (!nextForm1Control(ControlForm1CampanaPectusExcavatumFecha, ControlForm1CampanaPectusExcavatumFechaInvalid)) { resultadoNext = false }

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