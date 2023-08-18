$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja fazer essa operação?");
        if (resultado == false) {
            e.preventDefault();
        }
    });
});