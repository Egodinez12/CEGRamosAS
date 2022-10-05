$(document).ready(function () {
    GetAll();
    SemestreGetAll();
    
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5105/Api/Materia/GetAll',
        success: function (result) {
            $('#Materia tbody').empty();
            $.each(result.objects, function (i, materia) {
                var filas =
                    '<tr>'
                    + '<td style="text-align: center; vertical-align: middle;"><a class="btn btn-warning glyphicon"> '
                    + '<a href="#" onclick="GetById(' + materia.idMateria + ')">'
                    + '</a> '
                    + '</td>'
                    + "<td  id='id' class='text-center'>" + materia.idMateria + "</td>"
                    + "<td class='text-center'>" + materia.nombreMateria + "</td>"
                    + "<td class='text-center'>" + materia.creditos + "</ td>"
                    + "<td class='text-center'>" + materia.semestre.idSemestre + "</td>"                    
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + materia.idMateria + ')"><span class="glyphicon glyphicon-trash" style="color:#FFFFFF"></span></button></td>'

                    + "</tr>";
                $("#Materia tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};



function SemestreGetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5105/Api/Semestre/GetAll',
        success: function (result) {
            $("#ddlSemestres").append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, semestre) {
                $("#ddlSemestres").append('<option value="'
                    + semestre.idSemestre + '">'
                    + semestre.nombreSemestre + '</option>');
            });
        }
    });
}


function Add() {

    var materia = {
        idMateria: 0,
        nombreMateria: $('#txtNombreMateria').val(),
        creditos: $('#txtCreditos').val(),
        semestre: {
            idSemestre: $('#ddlSemestres').val()
        }
    }
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5105/Api/Materia/Add/',
        dataType: 'json',
        data: materia,
        success: function (result) {
            $('#myModal').modal();
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};


function GetById(IdMateria) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5105/Api/Materia/GetById/' + IdMateria,
        success: function (result) {
            $('#txtIdMateria').val(result.Object.idMateria);
            $('#txtNombreMateria').val(result.Object.nombreMateria);
            $('#txtCreditos').val(result.Object.creditos);
            $('#txtIdSemestre').val(result.Object.semestre.idSemestre);
            $('#ModalUpdate').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }


    });

}


function Update() {

    var materia = {
        idMateria: $('#txtIdMateria').val(),
        nombreMateria: $('#txtNombreMateria').val(),
        creditos: $('#txtCreditos').val(),
        idSemestre: {
            idSemestre: $('#ddlSemestres').val()
        }
    }

    $.ajax({
        type: 'POST',
        url: 'http://localhost:5105/Api/Materia/Update/',
        datatype: 'json',
        data: materia,
        success: function (result) {
            $('#myModal').modal();
            $('#ModalUpdate').modal('hide');
            GetAll();
            Console(respond);
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });

};



function Eliminar(Idmateria) {

    if (confirm("¿Estas seguro de eliminar la Materia seleccionada?")) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:5105/Api/Materia/Delete/' + Idmateria,
            success: function (result) {
                $('#myModal').modal();
                GetAll();
            },
            error: function (result) {
                alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
            }
        });

    };
};