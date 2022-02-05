//Formulario Nuevo Usuario

$(document).ready(function () {
    //Scroll
    window.onscroll = function () { scrollFunction(); };

    //Alert refresh pages
    window.onbeforeunload = function (event) {
        return confirm("Confirm refresh");
    };
});

//Mostrar el button de subir pages depende la posicion del scroll
function scrollFunction() {
    var mybutton = document.getElementById("btnSubirFormNuevoUsuario");
    if (document.body.scrollTop > 500 || document.documentElement.scrollTop > 500) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

//Button para subir la page
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

// #region Form1 Nuevo Usuario (Ha empeorado últimamente? Cuando?)
$("#customRadioFormNuevoUsuarioEmpeorado1").change(function () {
    var customRadioFormNuevoUsuarioEmpeorado1 = document.getElementById("customRadioFormNuevoUsuarioEmpeorado1");
    var inputEmpeoradoUltimamenteForm1Cuando = document.getElementById("inputEmpeoradoUltimamenteForm1Cuando");

    if (customRadioFormNuevoUsuarioEmpeorado1.checked) {
        inputEmpeoradoUltimamenteForm1Cuando.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioEmpeorado2").change(function () {
    var customRadioFormNuevoUsuarioEmpeorado2 = document.getElementById("customRadioFormNuevoUsuarioEmpeorado2");
    var inputEmpeoradoUltimamenteForm1Cuando = document.getElementById("inputEmpeoradoUltimamenteForm1Cuando");

    if (customRadioFormNuevoUsuarioEmpeorado2.checked) {
        inputEmpeoradoUltimamenteForm1Cuando.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form1 Nuevo Usuario (Ha empeorado últimamente? Cuando?)

// #region Form1 Nuevo Usuario (Antecedentes de malformaciones en la familia.)
$("#customRadioFormNuevoUsuarioAntMalforFam1").change(function () {
    var customRadioFormNuevoUsuarioAntMalforFam1 = document.getElementById("customRadioFormNuevoUsuarioAntMalforFam1");
    var form1NuevoUsuarioAntecedentesMalformacionesFamilia = document.getElementById("form1NuevoUsuarioAntecedentesMalformacionesFamilia");
    var form1NuevoUsuarioAntecedentesMalformacionesFamiliaOtros = document.getElementById("form1NuevoUsuarioAntecedentesMalformacionesFamiliaOtros");
    
    if (customRadioFormNuevoUsuarioAntMalforFam1.checked) {
        form1NuevoUsuarioAntecedentesMalformacionesFamilia.removeAttribute('hidden');
        form1NuevoUsuarioAntecedentesMalformacionesFamiliaOtros.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioAntMalforFam2").change(function () {
    var customRadioFormNuevoUsuarioAntMalforFam2 = document.getElementById("customRadioFormNuevoUsuarioAntMalforFam2");
    var form1NuevoUsuarioAntecedentesMalformacionesFamilia = document.getElementById("form1NuevoUsuarioAntecedentesMalformacionesFamilia");
    var form1NuevoUsuarioAntecedentesMalformacionesFamiliaOtros = document.getElementById("form1NuevoUsuarioAntecedentesMalformacionesFamiliaOtros");

    if (customRadioFormNuevoUsuarioAntMalforFam2.checked) {
        form1NuevoUsuarioAntecedentesMalformacionesFamilia.setAttribute('hidden', 'hidden');
        form1NuevoUsuarioAntecedentesMalformacionesFamiliaOtros.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form1 Nuevo Usuario (Antecedentes de malformaciones en la familia.)


// #region Form1 Nuevo Usuario (Ha consultado anteriormente por su pectus?)
$("#customRadioFormNuevoUsuarioConsultado1").change(function () {
    var customRadioFormNuevoUsuarioConsultado1 = document.getElementById("customRadioFormNuevoUsuarioConsultado1");
    var form1NuevoUsuarioConsultadoPectusCuando = document.getElementById("form1NuevoUsuarioConsultadoPectusCuando");
    var form1NuevoUsuarioConsultadoPectusDonde = document.getElementById("form1NuevoUsuarioConsultadoPectusDonde");

    if (customRadioFormNuevoUsuarioConsultado1.checked) {
        form1NuevoUsuarioConsultadoPectusCuando.removeAttribute('hidden');
        form1NuevoUsuarioConsultadoPectusDonde.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioConsultado2").change(function () {
    var customRadioFormNuevoUsuarioConsultado2 = document.getElementById("customRadioFormNuevoUsuarioConsultado2");
    var form1NuevoUsuarioConsultadoPectusCuando = document.getElementById("form1NuevoUsuarioConsultadoPectusCuando");
    var form1NuevoUsuarioConsultadoPectusDonde = document.getElementById("form1NuevoUsuarioConsultadoPectusDonde");

    if (customRadioFormNuevoUsuarioConsultado2.checked) {
        form1NuevoUsuarioConsultadoPectusCuando.setAttribute('hidden', 'hidden');
        form1NuevoUsuarioConsultadoPectusDonde.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form1 Nuevo Usuario  (Ha consultado anteriormente por su pectus?)

// #region Form2 Nuevo Usuario (Cirugía Previa)
$("#customRadioFormNuevoUsuarioCirugia1").change(function () {
    var customRadioFormNuevoUsuarioCirugia1 = document.getElementById("customRadioFormNuevoUsuarioCirugia1");
    var form2NuevoUsuarioInputsCirugiaPrevia = document.getElementById("form2NuevoUsuarioInputsCirugiaPrevia");

    if (customRadioFormNuevoUsuarioCirugia1.checked) {
        form2NuevoUsuarioInputsCirugiaPrevia.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioCirugia2").change(function () {
    var customRadioFormNuevoUsuarioCirugia2 = document.getElementById("customRadioFormNuevoUsuarioCirugia2");
    var form2NuevoUsuarioInputsCirugiaPrevia = document.getElementById("form2NuevoUsuarioInputsCirugiaPrevia");

    if (customRadioFormNuevoUsuarioCirugia2.checked) {
        form2NuevoUsuarioInputsCirugiaPrevia.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form2 Nuevo Usuario  (Anamnesis)

// #region Form3 Nuevo Usuario (Anamnesis)
$("#customRadioFormNuevoUsuarioEnfermedadPre1").change(function () {
    var customRadioFormNuevoUsuarioEnfermedadPre1 = document.getElementById("customRadioFormNuevoUsuarioEnfermedadPre1");
    var form4NuevoUsuarioInputsEnfermedadPreexistente = document.getElementById("form4NuevoUsuarioInputsEnfermedadPreexistente");

    if (customRadioFormNuevoUsuarioEnfermedadPre1.checked) {
        form4NuevoUsuarioInputsEnfermedadPreexistente.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioEnfermedadPre2").change(function () {
    var customRadioFormNuevoUsuarioEnfermedadPre2 = document.getElementById("customRadioFormNuevoUsuarioEnfermedadPre2");
    var form4NuevoUsuarioInputsEnfermedadPreexistente = document.getElementById("form4NuevoUsuarioInputsEnfermedadPreexistente");

    if (customRadioFormNuevoUsuarioEnfermedadPre2.checked) {
        form4NuevoUsuarioInputsEnfermedadPreexistente.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form3 Nuevo Usuario  (Anamnesis)

// #region Form5 Nuevo Usuario (Anamnesis)
$("#customRadioForm5NuevoUsuarioAlergia1").change(function () {
    var customRadioForm5NuevoUsuarioAlergia1 = document.getElementById("customRadioForm5NuevoUsuarioAlergia1");
    var inputForm5NuevoUsuarioAlergiasCuales = document.getElementById("inputForm5NuevoUsuarioAlergiasCuales");

    if (customRadioForm5NuevoUsuarioAlergia1.checked) {
        inputForm5NuevoUsuarioAlergiasCuales.removeAttribute('hidden');
    }
});

$("#customRadioForm5NuevoUsuarioAlergia2").change(function () {
    var customRadioForm5NuevoUsuarioAlergia2 = document.getElementById("customRadioForm5NuevoUsuarioAlergia2");
    var inputForm5NuevoUsuarioAlergiasCuales = document.getElementById("inputForm5NuevoUsuarioAlergiasCuales");

    if (customRadioForm5NuevoUsuarioAlergia2.checked) {
        inputForm5NuevoUsuarioAlergiasCuales.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form5 Nuevo Usuario  (Anamnesis)

// #region Form5 Nuevo Usuario (Deporte)
$("#customRadioForm5NuevoUsuarioDeportes1").change(function () {
    var customRadioForm5NuevoUsuarioDeportes1 = document.getElementById("customRadioForm5NuevoUsuarioDeportes1");
    var inputsNuevoUsuarioForm6RealizaDeporte = document.getElementById("inputsNuevoUsuarioForm6RealizaDeporte");

    if (customRadioForm5NuevoUsuarioDeportes1.checked) {
        inputsNuevoUsuarioForm6RealizaDeporte.removeAttribute('hidden');
    }
});

$("#customRadioForm5NuevoUsuarioDeportes2").change(function () {
    var customRadioForm5NuevoUsuarioDeportes2 = document.getElementById("customRadioForm5NuevoUsuarioDeportes2");
    var inputsNuevoUsuarioForm6RealizaDeporte = document.getElementById("inputsNuevoUsuarioForm6RealizaDeporte");

    if (customRadioForm5NuevoUsuarioDeportes2.checked) {
        inputsNuevoUsuarioForm6RealizaDeporte.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form5 Nuevo Usuario  (Deporte)

// #region Form6 Nuevo Usuario (Deporte)
$("#customRadioForm6NuevoUsuarioDejado1").change(function () {
    var customRadioForm6NuevoUsuarioDejado1 = document.getElementById("customRadioForm6NuevoUsuarioDejado1");
    var NuevoUsuarioForm6DejadoDeportePorque = document.getElementById("NuevoUsuarioForm6DejadoDeportePorque");

    if (customRadioForm6NuevoUsuarioDejado1.checked) {
        NuevoUsuarioForm6DejadoDeportePorque.removeAttribute('hidden');
    }
});

$("#customRadioForm6NuevoUsuarioDejado2").change(function () {
    var customRadioForm6NuevoUsuarioDejado2 = document.getElementById("customRadioForm6NuevoUsuarioDejado2");
    var NuevoUsuarioForm6DejadoDeportePorque = document.getElementById("NuevoUsuarioForm6DejadoDeportePorque");

    if (customRadioForm6NuevoUsuarioDejado2.checked) {
        NuevoUsuarioForm6DejadoDeportePorque.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form6 Nuevo Usuario  (Deporte)

// #region Form7 Nuevo Usuario (Anamnesis)
$("#customRadioForm7NuevoUsuarioMedicacion1").change(function () {
    var customRadioForm7NuevoUsuarioMedicacion1 = document.getElementById("customRadioForm7NuevoUsuarioMedicacion1");
    var inputsNuevoUsuarioForm7MedicacionCual = document.getElementById("inputsNuevoUsuarioForm7MedicacionCual");

    if (customRadioForm7NuevoUsuarioMedicacion1.checked) {
        inputsNuevoUsuarioForm7MedicacionCual.removeAttribute('hidden');
    }
});

$("#customRadioForm7NuevoUsuarioMedicacion2").change(function () {
    var customRadioForm7NuevoUsuarioMedicacion2 = document.getElementById("customRadioForm7NuevoUsuarioMedicacion2");
    var inputsNuevoUsuarioForm7MedicacionCual = document.getElementById("inputsNuevoUsuarioForm7MedicacionCual");

    if (customRadioForm7NuevoUsuarioMedicacion2.checked) {
        inputsNuevoUsuarioForm7MedicacionCual.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form7 Nuevo Usuario (Anamnesis)

// #region Form8 Nuevo Usuario (Dolor pecho)
$("#customRadioForm7NuevoUsuarioDolor1").change(function () {
    var customRadioForm7NuevoUsuarioDolor1 = document.getElementById("customRadioForm7NuevoUsuarioDolor1");
    var inputsNuevoUsuarioForm8DolorPecho = document.getElementById("inputsNuevoUsuarioForm8DolorPecho");

    if (customRadioForm7NuevoUsuarioDolor1.checked) {
        inputsNuevoUsuarioForm8DolorPecho.removeAttribute('hidden');
    }
});

$("#customRadioForm7NuevoUsuarioDolor2").change(function () {
    var customRadioForm7NuevoUsuarioDolor2 = document.getElementById("customRadioForm7NuevoUsuarioDolor2");
    var inputsNuevoUsuarioForm8DolorPecho = document.getElementById("inputsNuevoUsuarioForm8DolorPecho");

    if (customRadioForm7NuevoUsuarioDolor2.checked) {
        inputsNuevoUsuarioForm8DolorPecho.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form8 Nuevo Usuario (Dolor pecho)


// #region Form9 Nuevo Usuario (Signos y síntomas clínicos)
$("#customRadioFormNuevoUsuarioSignosSintomasClinicos1").change(function () {
    var customRadioFormNuevoUsuarioSignosSintomasClinicos1 = document.getElementById("customRadioFormNuevoUsuarioSignosSintomasClinicos1");
    var inputsNuevoUsuarioForm9SignosSintomasClinicos = document.getElementById("inputsNuevoUsuarioForm9SignosSintomasClinicos");

    if (customRadioFormNuevoUsuarioSignosSintomasClinicos1.checked) {
        inputsNuevoUsuarioForm9SignosSintomasClinicos.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioSignosSintomasClinicos2").change(function () {
    var customRadioFormNuevoUsuarioSignosSintomasClinicos2 = document.getElementById("customRadioFormNuevoUsuarioSignosSintomasClinicos2");
    var inputsNuevoUsuarioForm9SignosSintomasClinicos = document.getElementById("inputsNuevoUsuarioForm9SignosSintomasClinicos");

    if (customRadioFormNuevoUsuarioSignosSintomasClinicos2.checked) {
        inputsNuevoUsuarioForm9SignosSintomasClinicos.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form9 Nuevo Usuario (Signos y síntomas clínicos)

// #region Form10 Nuevo Usuario (Clasificacion Pectus)
$("#customRadioFormNuevoUsuarioClasificacionEstrias1").change(function () {
    var customRadioFormNuevoUsuarioClasificacionEstrias1 = document.getElementById("customRadioFormNuevoUsuarioClasificacionEstrias1");
    var inputsNuevoUsuarioForm10ClasificacionPectus = document.getElementById("inputsNuevoUsuarioForm10ClasificacionPectus");

    if (customRadioFormNuevoUsuarioClasificacionEstrias1.checked) {
        inputsNuevoUsuarioForm10ClasificacionPectus.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioClasificacionEstrias2").change(function () {
    var customRadioFormNuevoUsuarioClasificacionEstrias2 = document.getElementById("customRadioFormNuevoUsuarioClasificacionEstrias2");
    var inputsNuevoUsuarioForm10ClasificacionPectus = document.getElementById("inputsNuevoUsuarioForm10ClasificacionPectus");

    if (customRadioFormNuevoUsuarioClasificacionEstrias2.checked) {
        inputsNuevoUsuarioForm10ClasificacionPectus.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form10 Nuevo Usuario (Clasificacion Pectus)

// #region Form11 Nuevo Usuario (Pectus Carinatum)
$("#customRadioFormNuevoUsuarioPectusCarinatumRotacion1").change(function () {
    var customRadioFormNuevoUsuarioPectusCarinatumRotacion1 = document.getElementById("customRadioFormNuevoUsuarioPectusCarinatumRotacion1");
    var NuevoUsuarioForm11PectusCarinatumRotacionSi = document.getElementById("NuevoUsuarioForm11PectusCarinatumRotacionSi");

    if (customRadioFormNuevoUsuarioPectusCarinatumRotacion1.checked) {
        NuevoUsuarioForm11PectusCarinatumRotacionSi.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioPectusCarinatumRotacion2").change(function () {
    var customRadioFormNuevoUsuarioPectusCarinatumRotacion2 = document.getElementById("customRadioFormNuevoUsuarioPectusCarinatumRotacion2");
    var NuevoUsuarioForm11PectusCarinatumRotacionSi = document.getElementById("NuevoUsuarioForm11PectusCarinatumRotacionSi");

    if (customRadioFormNuevoUsuarioPectusCarinatumRotacion2.checked) {
        NuevoUsuarioForm11PectusCarinatumRotacionSi.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form11 Nuevo Usuario (Pectus Carinatum)

// #region Form12 Nuevo Usuario (Pectus Excavatum)
$("#customRadioFormNuevoUsuarioExcavatumRotacion1").change(function () {
    var customRadioFormNuevoUsuarioExcavatumRotacion1 = document.getElementById("customRadioFormNuevoUsuarioExcavatumRotacion1");
    var NuevoUsuarioForm12PectusExcavatumRotacionSi = document.getElementById("NuevoUsuarioForm12PectusExcavatumRotacionSi");

    if (customRadioFormNuevoUsuarioExcavatumRotacion1.checked) {
        NuevoUsuarioForm12PectusExcavatumRotacionSi.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioExcavatumRotacion2").change(function () {
    var customRadioFormNuevoUsuarioExcavatumRotacion2 = document.getElementById("customRadioFormNuevoUsuarioExcavatumRotacion2");
    var NuevoUsuarioForm12PectusExcavatumRotacionSi = document.getElementById("NuevoUsuarioForm12PectusExcavatumRotacionSi");

    if (customRadioFormNuevoUsuarioExcavatumRotacion2.checked) {
        NuevoUsuarioForm12PectusExcavatumRotacionSi.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form12 Nuevo Usuario (Pectus Excavatum)

// #region Form13 Nuevo Usuario (Pectus Mixto)
$("#customRadioFormNuevoUsuarioMixtoRotacion1").change(function () {
    var customRadioFormNuevoUsuarioMixtoRotacion1 = document.getElementById("customRadioFormNuevoUsuarioMixtoRotacion1");
    var NuevoUsuarioForm13PectusMixtoRotacionSi = document.getElementById("NuevoUsuarioForm13PectusMixtoRotacionSi");

    if (customRadioFormNuevoUsuarioMixtoRotacion1.checked) {
        NuevoUsuarioForm13PectusMixtoRotacionSi.removeAttribute('hidden');
    }
});

$("#customRadioFormNuevoUsuarioMixtoRotacion2").change(function () {
    var customRadioFormNuevoUsuarioMixtoRotacion2 = document.getElementById("customRadioFormNuevoUsuarioMixtoRotacion2");
    var NuevoUsuarioForm13PectusMixtoRotacionSi = document.getElementById("NuevoUsuarioForm13PectusMixtoRotacionSi");

    if (customRadioFormNuevoUsuarioMixtoRotacion2.checked) {
        NuevoUsuarioForm13PectusMixtoRotacionSi.setAttribute('hidden', 'hidden');
    }
});
// #endregion Form13 Nuevo Usuario (Pectus Mixto)



//Control boton siguiente Form1 - Si checked
$("#customRadioFormNuevoUsuarioCirugia1").change(function () {
    var navPagina2FormUsuario = document.getElementById("navPagina2FormUsuario");
    navPagina2FormUsuario.classList.remove('disabled')
});

//Control boton siguiente Form1 - No checked
$("#customRadioFormNuevoUsuarioCirugia2").change(function () {
    var navPagina2FormUsuario = document.getElementById("navPagina2FormUsuario");
    navPagina2FormUsuario.classList.add('disabled')
});

//Control boton siguiente Form1
$("#BtnNextForm1Usuario").click(function () {
    var resultadoForm1 = formularioNuevoUsuarioform1();

    //Comprobamos si el checked es Si o No para mostrar el nav 3
    if (resultadoForm1) {
        $("#navPagina3FormUsuario").trigger('click');      
    }
});

//Control boton siguiente Form3
$("#BtnNextForm3Usuario").click(function () {
    $("#navPagina9FormUsuario").trigger('click');
});

//Back Form3
$("#BtnBackForm3Usuario").click(function () {
    $("#navPagina1FormUsuario").trigger('click');
});

//Next Form9
$("#BtnNextForm9Usuario").click(function () {
    selectNavTipoPectus();
});

//Back Form9
$("#BtnBackForm9Usuario").click(function () {
    $("#navPagina3FormUsuario").trigger('click');
});

//Form6 Checkbox Other: (Disable)
$("#customRadioForm6NuevoUsuarioDeportesCual6").click(function () {
    var otherDeporte = document.getElementById("customRadioForm6NuevoUsuarioDeportesCual6");

    if (otherDeporte.checked) {
        $("#otherForm6NuevoUsuarioDeportesCual7").removeAttr('disabled');
    }
    else {
        $("#otherForm6NuevoUsuarioDeportesCual7").attr("disabled","disabled");
    }
});

//Control boton siguiente Form7 - Si checked
$("#customRadioForm7NuevoUsuarioDolor1").change(function () {
    var navPagina7FormUsuario = document.getElementById("navPagina8FormUsuario");
    navPagina7FormUsuario.classList.remove('disabled')
});

//Control boton siguiente Form7 - No checked
$("#customRadioForm7NuevoUsuarioDolor2").change(function () {
    var navPagina7FormUsuario = document.getElementById("navPagina8FormUsuario");
    navPagina7FormUsuario.classList.add('disabled')
});


function selectNavTipoPectus() {
    var customRadioForm9NuevoUsuarioPectus1 = document.getElementById("customRadioForm9NuevoUsuarioPectus1");
    var customRadioForm9NuevoUsuarioPectus2 = document.getElementById("customRadioForm9NuevoUsuarioPectus2");
    var customRadioForm9NuevoUsuarioPectus3 = document.getElementById("customRadioForm9NuevoUsuarioPectus3");
    var customRadioForm9NuevoUsuarioPectus4 = document.getElementById("customRadioForm9NuevoUsuarioPectus4");
    var customRadioForm9NuevoUsuarioPectus5 = document.getElementById("customRadioForm9NuevoUsuarioPectus5");
    
    if (customRadioForm9NuevoUsuarioPectus1.checked) {
        $("#dropdownPectusCarinatum").trigger('click');
    }
    else if (customRadioForm9NuevoUsuarioPectus2.checked) {
        $("#dropdownPectusExcavatum").trigger('click');
    }
    else if (customRadioForm9NuevoUsuarioPectus3.checked) {
        $("#dropdownPectusMixto").trigger('click');
    }
    else if (customRadioForm9NuevoUsuarioPectus4.checked) {
        $("#dropdownSindromePoland").trigger('click');
    }
    else if (customRadioForm9NuevoUsuarioPectus5.checked) {
        $("#dropdownOtros").trigger('click');
    }
}

//Form9 Select Tipo Pectus - Checkbox Other: (Disable)
function desabledAllTipoPectus(){
    var dropdownPectusCarinatum = document.getElementById("dropdownPectusCarinatum");
    var dropdownPectusExcavatum = document.getElementById("dropdownPectusExcavatum");
    var dropdownPectusMixto = document.getElementById("dropdownPectusMixto");
    var dropdownSindromePoland = document.getElementById("dropdownSindromePoland");
    var dropdownOtros = document.getElementById("dropdownOtros");
    dropdownPectusCarinatum.classList.add('disabled');
    dropdownPectusExcavatum.classList.add('disabled');
    dropdownPectusMixto.classList.add('disabled');
    dropdownSindromePoland.classList.add('disabled');
    dropdownOtros.classList.add('disabled');

    //Checkbox Other: (Disable)
    var customRadioForm9NuevoUsuarioPectus5 = document.getElementById("customRadioForm9NuevoUsuarioPectus5");
    if (customRadioForm9NuevoUsuarioPectus5.checked) {
        $("#customRadioForm9NuevoUsuarioPectus5Input").removeAttr("disabled");
    }
    else {
        $("#customRadioForm9NuevoUsuarioPectus5Input").attr("disabled", "disabled");
    }
}

$("#customRadioForm9NuevoUsuarioPectus1").change(function () {
    var dropdownPectusCarinatum = document.getElementById("dropdownPectusCarinatum");
    desabledAllTipoPectus();
    dropdownPectusCarinatum.classList.remove('disabled');
});

$("#customRadioForm9NuevoUsuarioPectus2").change(function () {
    var dropdownPectusExcavatum = document.getElementById("dropdownPectusExcavatum");
    desabledAllTipoPectus();
    dropdownPectusExcavatum.classList.remove('disabled');
});

$("#customRadioForm9NuevoUsuarioPectus3").change(function () {
    var dropdownPectusMixto = document.getElementById("dropdownPectusMixto");
    desabledAllTipoPectus();
    dropdownPectusMixto.classList.remove('disabled');
});

$("#customRadioForm9NuevoUsuarioPectus4").change(function () {
    var dropdownSindromePoland = document.getElementById("dropdownSindromePoland");
    desabledAllTipoPectus();
    dropdownSindromePoland.classList.remove('disabled');
});

$("#customRadioForm9NuevoUsuarioPectus5").change(function () {
    var dropdownOtros = document.getElementById("dropdownOtros");
    desabledAllTipoPectus();
    dropdownOtros.classList.remove('disabled');
});

//Control boton siguiente Form10
$("#buttonPopupFormNuevoUsuario").click(function () {
    //$("#navPagina10FormUsuario").trigger('click');
});

//Back Form10
$("#BtnBackForm10Usuario").click(function () {
    selectNavTipoPectus();
});

//Control boton siguiente Form11
$("#BtnNextForm11Usuario").click(function () {
    $("#navPagina10FormUsuario").trigger('click');
});

//Back Form11
$("#BtnBackForm11Usuario").click(function () {
    $("#navPagina9FormUsuario").trigger('click');
});

//Control boton siguiente Form12
$("#BtnNextForm12Usuario").click(function () {
    $("#navPagina10FormUsuario").trigger('click');
});

//Back Form12
$("#BtnBackForm12Usuario").click(function () {
    $("#navPagina9FormUsuario").trigger('click');
});

//Control boton siguiente Form13
$("#BtnNextForm13Usuario").click(function () {
    $("#navPagina10FormUsuario").trigger('click');
});

//Back Form13
$("#BtnBackForm13Usuario").click(function () {
    $("#navPagina9FormUsuario").trigger('click');
});

//Control boton siguiente Form14
$("#BtnNextForm14Usuario").click(function () {
    $("#navPagina10FormUsuario").trigger('click');
});

//Back Form14
$("#BtnBackForm14Usuario").click(function () {
    $("#navPagina9FormUsuario").trigger('click');
});

//Popup Enviar Form Nuevo Usuario - Show
$("#buttonPopupFormNuevoUsuario").click(function () {
    var error_ModalSubmitFormNuevoUsuario = document.getElementById("error_ModalSubmitFormNuevoUsuario");
    error_ModalSubmitFormNuevoUsuario.setAttribute('hidden', 'hidden');

    var spinner_ModalSubmitFormNuevoUsuario = document.getElementById("spinner_ModalSubmitFormNuevoUsuario");
    spinner_ModalSubmitFormNuevoUsuario.hidden = true;
    
    $("#formNuevoUsuarioModalEnviar").modal('show');
});

//Popup Enviar Form Nuevo Usuario - Hide
$("#buttonPopupCerrarFormNuevoUsuario").click(function () {
    $("#formNuevoUsuarioModalEnviar").modal('hide');
});

//Popup Enviar Form Nuevo Usuario - Submit
$("#popupConfirmarFormNuevoUsuario").click(function () {
    var error_ModalSubmitFormNuevoUsuario = document.getElementById("error_ModalSubmitFormNuevoUsuario");
    var spinner_ModalSubmitFormNuevoUsuario = document.getElementById("spinner_ModalSubmitFormNuevoUsuario");
    spinner_ModalSubmitFormNuevoUsuario.hidden = false;

    //Para que no salga la alert del refresh pages
    window.onbeforeunload = function (event) { };

    if (formularioNuevoUsuarioform1()) {
        error_ModalSubmitFormNuevoUsuario.setAttribute('hidden', 'hidden');
        $("#submitForm10NuevoUsuario").trigger('click');
    }
    else {
        spinner_ModalSubmitFormNuevoUsuario.hidden = true;
        error_ModalSubmitFormNuevoUsuario.removeAttribute('hidden');
    }
});

//Functions
function formularioNuevoUsuarioform1() {
    var resultadoNext = true;

    var form1NuevoPacienteNombre = document.getElementById("form1NuevoPacienteNombre");
    var form1NuevoPacienteNombreInvalid = document.getElementById("form1NuevoPacienteNombreInvalid");
    if (!nextFormNuevoUsuario(form1NuevoPacienteNombre, form1NuevoPacienteNombreInvalid)) { resultadoNext = false }

    var form1NuevoPacienteApellidos = document.getElementById("form1NuevoPacienteApellidos");
    var form1NuevoPacienteApellidosInvalid = document.getElementById("form1NuevoPacienteApellidosInvalid");
    if (!nextFormNuevoUsuario(form1NuevoPacienteApellidos, form1NuevoPacienteApellidosInvalid)) { resultadoNext = false }

    var form1NuevoPacienteCorreo = document.getElementById("form1NuevoPacienteCorreo");
    var form1NuevoPacienteCorreoInvalid = document.getElementById("form1NuevoPacienteCorreoInvalid");
    if (!ValidateEmail(form1NuevoPacienteCorreo, form1NuevoPacienteCorreoInvalid)) { resultadoNext = false }

    return resultadoNext;
}

function formularioNuevoUsuarioform3() {
    var resultadoNext = true;

    var NuevoUsuarioForm3EnfermedadPree = document.getElementById("NuevoUsuarioForm3EnfermedadPree");
    var NuevoUsuarioForm3EnfermedadPreeInvalid = document.getElementById("NuevoUsuarioForm3EnfermedadPreeInvalid");
    if (!nextFormNuevoUsuarioGroup(NuevoUsuarioForm3EnfermedadPree, NuevoUsuarioForm3EnfermedadPreeInvalid)) { resultadoNext = false }

    return resultadoNext;
}

function formularioNuevoUsuarioform5() {
    var resultadoNext = true;

    var NuevoUsuarioForm5RealizaDeporte = document.getElementById("NuevoUsuarioForm5RealizaDeporte");
    var NuevoUsuarioForm5RealizaDeporteInvalid = document.getElementById("NuevoUsuarioForm5RealizaDeporteInvalid");
    if (!nextFormNuevoUsuarioGroup(NuevoUsuarioForm5RealizaDeporte, NuevoUsuarioForm5RealizaDeporteInvalid)) { resultadoNext = false }

    return resultadoNext;
}

function formularioNuevoUsuarioform7() {
    var resultadoNext = true;

    var NuevoUsuarioForm7Dolor = document.getElementById("NuevoUsuarioForm7Dolor");
    var NuevoUsuarioForm7DolorInvalid = document.getElementById("NuevoUsuarioForm7DolorInvalid");
    if (!nextFormNuevoUsuarioGroup(NuevoUsuarioForm7Dolor, NuevoUsuarioForm7DolorInvalid)) { resultadoNext = false }

    return resultadoNext;
}

function formularioNuevoUsuarioform9() {
    var resultadoNext = true;

    var NuevoUsuarioForm9Pectus = document.getElementById("NuevoUsuarioForm9Pectus");
    var NuevoUsuarioForm9PectusInvalid = document.getElementById("NuevoUsuarioForm9PectusInvalid");
    if (!nextFormNuevoUsuarioGroup(NuevoUsuarioForm9Pectus, NuevoUsuarioForm9PectusInvalid)) { resultadoNext = false }

    return resultadoNext;
}

//Metode para comprobar/validar correo electronico
function ValidateEmail(input, alert) {
    var mailformat = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (input.value.match(mailformat)) {
        if (alert.style.visibility = 'visible') {
            alert.style.visibility = 'hidden';
            input.classList.remove('is-invalid')
        }
        return true;
    }
    else {
        alert.style.visibility = 'visible';
        input.classList.add('is-invalid')
        return false;
    }
}


//Metode para comprobar si se ha rellenado o no los inputs del form
function nextFormNuevoUsuario(input, alert) {
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

//Metode para comprobar si se ha seleccionado o no los inputs del form
function nextFormNuevoUsuarioGroup(group, alert) {
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
