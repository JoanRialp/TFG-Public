$(document).ready(function () {
    //Cuando se carga la paginia de inicio se muestra la animacion de reload
    window.onbeforeunload = function (event) { reloadButton(true) };
});

//Fuction que controla el boton de login animacion
function reloadButton(activo) {
    var spinnerLogIn = document.getElementById("spinnerLogIn");
    if (activo) {
        spinnerLogIn.removeAttribute("hidden");
    }
    else {
        spinnerLogIn.setAttribute("hidden");
    } 
}

//Boton LogIn
$("#buttonLogIn").on("click", function (event) {
    event.preventDefault();
    $("#submitFormLogIn").trigger('click');
});

//Boton LogOut
$("#buttonLogOutClinics").click(function () {
    var spinnerLogIn = document.getElementById("spinnerLogIn");
    spinnerLogIn.setAttribute("hidden");
});

//Boton Recuperar contraseña
$("#buttonRecuperar").on("click", function (event) {
    event.preventDefault();

    var inputPasswordChanges = document.getElementById("inputPasswordChanges");
    var inputPasswordChanges2 = document.getElementById("inputPasswordChanges2");

    if (inputPasswordChanges != null && inputPasswordChanges2 != null) {

        if (inputPasswordChanges.value == inputPasswordChanges2.value) {
            $("#submitFormRecuperar").trigger('click');
        }
        else
        {
            var changesPasswordError = document.getElementById("changesPasswordError");
            changesPasswordError.removeAttribute('hidden');
        }
    }
    else
    {
        $("#submitFormRecuperar").trigger('click');
    }
});
