window.onload = function () {
    var alt = localStorage.getItem('alert');
    if (alt == "1") {
        alertUpload();
    }
}

$(".btnfile").bind("click", function () {
    $("#ipt").click();
});

async function upload() {

    var file = document.getElementById("ipt");
    if (file.files.length > 0) {
        var url = "Home/upload";
        var frmGuardar = document.getElementById("frmfile");
        var frm = new FormData(frmGuardar);

        var raiz = document.getElementById("hdfOculto").value;
        var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;

        await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        window.location.href = '/Home/Index';
        localStorage.setItem('alert', 1);
    } else {
        alertShow("No File Selected");
    } 
}

function alertUpload() {
    Swal.fire({
        html: '<span class="bluc">Upload File Success!</span>',
        icon: 'success',
        showConfirmButton: false,
        backdrop: false,
        timer: 4000,
        toast: true,
        position: 'top-end'
    });
    localStorage.setItem('alert', 0);
}

function alertShow(msj) {
    Swal.fire({
        icon: 'error',
        Title: 'Oops....',
        text: msj,
        footer: 'Please Try Again'
    });
}

