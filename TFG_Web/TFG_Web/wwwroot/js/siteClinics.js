$(document).ready(function () {
    //Sidebar elements selected
    //A partir de la url, depende de que controller estamos, se activa uno o otro
    var path = window.location.pathname;
    asignarButtonSidebarWindows(path.substring(path.lastIndexOf('/') + 1));  
});

// ------- NavdBar & SideBar -------
//CSS active
$('#sidebarCollapse').on('click', function () {
    $('#sidebar').toggleClass('active');
    $('#content').toggleClass('active');
});

function cerrarSideBar() {
    var sidebar = document.getElementById("sidebar");
    if (sidebar.classList.value == 'active') {
        $('#sidebar').toggleClass('active');
        $('#content').toggleClass('active');
    }
}

//El hacer click en cualquier parte del body se cierra el sidebar
$("#navbarGeneral").click(function () {
    var sidebar = document.getElementById("sidebar");
    if (sidebar.classList.value == 'active') {
        cerrarSideBar();
    }
});

//Click en el header del sidebar
$("#headerSideBar").click(function () {
    var sidebar = document.getElementById("sidebar");
    if (sidebar.classList.value != 'active') {
        cerrarSideBar();
    }
});

//Sidebar element selected
function asignarButtonSidebarWindows(elemento) {

    if (elemento == 'Inicio') {
        $('#itemsSidebarHomeClinics').toggleClass('active');
        $('#itemsSidebarHomeClinicsActive').toggleClass('active');
    }
    if (elemento == 'Estadistica') {
        $('#itemsSidebarEstadistica').toggleClass('active');
        $('#itemsSidebarEstadisticaActive').toggleClass('active');
    }
    if (elemento == 'ControlSistemaCompresor') {
        $('#itemsSidebarFormControlSistemaCompresor').toggleClass('active');
        $('#itemsSidebarFormControlSistemaCompresorActive').toggleClass('active');
    }
    if (elemento == 'ControlCampanaPectusExcavatum') {
        $('#itemsSidebarCampanaPectusExcavatum').toggleClass('active');
        $('#itemsSidebarCampanaPectusExcavatumActive').toggleClass('active');
    }
    if (elemento == 'NuevoPaciente') {
        $('#itemsSidebarNuevoUsuario').toggleClass('active');
        $('#itemsSidebarNuevoUsuarioActive').toggleClass('active');
    }
    if (elemento == 'FinalizarTratamiento') {
        $('#itemsSidebarPacienteFinalizado').toggleClass('active');
        $('#itemsSidebarPacienteFinalizadoActive').toggleClass('active');
    }
    if (elemento == 'AnadirUsuario') {
        $('#itemsSidebarAñadirUsuario').toggleClass('active');
        $('#itemsSidebarAñadirUsuarioActive').toggleClass('active');
    }
}


//Esconder el listado de pacientes del Search, cuando el usuario hace click en el docuemnto
$(document).click(function () {
    var listado_Pacientes = document.getElementById("listado_Pacientes");
    listado_Pacientes.style.display = 'none';
});

//Boton del Search
$("#search-button").click(function () {
    var input = document.getElementById("search_Pacientes");

    if (input != null) {
        var filter = input.value.toUpperCase();
        var listado_Pacientes = document.getElementById("listado_Pacientes");

        if (filter != "") {
            for (i = 0; i < listado_Pacientes.children.length; i++) {
                if (listado_Pacientes.children[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                    listado_Pacientes.children[i].click();
                    listado_Pacientes.style.display = 'none';
                    break;
                }
            }
        }
    }
});

//Funcion del seleccionar el Paciente (Enter o boton search)
function selectPacienteSearch() {
    var listado_Pacientes = document.getElementById("listado_Pacientes");
    var input = document.getElementById("search_Pacientes");
    var filter = input.value.toUpperCase();

    if (filter != "") {
        for (i = 0; i < listado_Pacientes.children.length; i++) {
            if (listado_Pacientes.children[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                input.value = listado_Pacientes.children[i].innerHTML.trim();
                listado_Pacientes.style.display = 'none';
                break;
            }
        }
    }
}

// Boton del desplegable del Search
$("#desplegable_Pacientes").click(function () {
    let listado_Pacientes = document.getElementById("listado_Pacientes");
    let input = document.getElementById("search_Pacientes");
    let contarCaracteres = input.value;

    //Indicar a partir de cuantos caracteres se miestra el listado de pacientes
    if (contarCaracteres.length > 2) {
        if (listado_Pacientes.style.display == 'none' || listado_Pacientes.style.display == "") {
            listado_Pacientes.style.display = 'block';
        }
        else {
            listado_Pacientes.style.display = 'none';
        }
    }
    else {
        listado_Pacientes.style.display = 'none';
    }
    
});

// #region ----------------- Buscar pacientes -----------------
//Funcion para buscar y mostrar los pacientes relacionados Search
function search_Pacientes() {

    var input = document.getElementById("search_Pacientes");
    var contarCaracteres = input.value;

    //Indicar a partir de cuantos caracteres se miestra el listado de pacientes
    if (contarCaracteres.length > 2) {
        var filter = input.value.toUpperCase();
        var array_pacientes = [];

        input.addEventListener("keyup", function (event) {
            if (event.keyCode == 13) {
                selectPacienteSearch();
                return;
            }
        });

        if (filter != "") {
            for (i = 0; i < listado_Pacientes.children.length; i++) {
                if (listado_Pacientes.children[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                    array_pacientes.push(listado_Pacientes.children[i]);
                    listado_Pacientes.children[i].style.display = 'block';
                }
                else {
                    listado_Pacientes.children[i].style.display = 'none';
                }
            }
            if (array_pacientes.length > 0) {
                listado_Pacientes.style.display = 'block';
            }
            else {
                listado_Pacientes.style.display = 'none';
            }
        }
        else {
            for (i = 0; i < listado_Pacientes.children.length; i++) {
                listado_Pacientes.children[i].style.display = 'block';
                listado_Pacientes.style.display = 'none';
            }
        }
    }
    else
    {
        listado_Pacientes.style.display = 'none';
    }
}

//Cuando selecciona un paciente del listado se guarda en el input value (Search)
function changes_Value_Search(value) {
    var search_Pacientes = document.getElementById("search_Pacientes");
    search_Pacientes.value = value;

    var listado_Pacientes = document.getElementById("listado_Pacientes");
    listado_Pacientes.style.display = 'none';

    selectPacienteSearch();
}

//Cuando limpia con la X el input del Search (Reset el listado)
$("#search_Pacientes").on("search", function () {
    var listado_Pacientes = document.getElementById("listado_Pacientes");
    listado_Pacientes.style.display = 'none';

    for (i = 0; i < listado_Pacientes.children.length; i++) {
        listado_Pacientes.children[i].style.display = 'block';
    }
});
// #endregion ----------------- Buscar pacientes -----------------