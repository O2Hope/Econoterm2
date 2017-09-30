var ambiental1;
var superficial1;
var operacion1;
var diametro1;
var viento1;
var emisividad1;
var aislante1;

$('#diametro').prop('disabled',true)

$('#reset').click((function () {
    $('#ambiental').val('');
    $('#superficial').val('');
    $('#operacion').val('');
    $('#diametro').val('');
    $('#viento').val('');
    $('#emisividad').val('');
}));

$('#tipo').change(function () {
    if ($('#tipo').val() == 'Deposito'){
        $('#diametro').prop('disabled',true)
    }
});

$('#calcular').click(function () {

    ambiental1 = $('#ambiental').val();
    superficial1 = $('#superficial').val();
    operacion1 = $('#operacion').val();
    diametro1 = $('#diametro').val();
    viento1 = $('#viento').val();
    emisividad1 = $('#emisividad').val();
    aislante1 = $('#aislante').val();
    if (aislante1 == 'CPCA96')
        aislante1 = 0;
    if (aislante1 == 'CPCA144')
        aislante1 = 1;
    if (aislante1 == 'CPA192')
        aislante1 = 2;
    if (aislante1 == 'FF32')
        aislante1 = 3;
    if (aislante1 == 'FF48')
        aislante1 = 4;
    if (aislante1 == 'FF64')
        aislante1 = 5;
    if (aislante1 == 'FF76')
        aislante1 = 6;
    if (aislante1 == 'FF128')
        aislante1 = 7;

    $.getJSON('http://econoterm.coep.com.mx/api/calculo', {
        ambiental: ambiental1,
        superficial: superficial1,
        operacion: operacion1,
        diametro: diametro1,
        viento: viento1,
        emisividad: emisividad1,
        aislante: aislante1
    })
        .done (function (data) {
            $.each(data.lista, function (i, item) {
                $('#tablaCalc').find('tbody')
                    .append('<tr><td>' + i + '</td>' + '<td>' + item.espesor + '</td>' + '<td>' + item.flux + '</td>' +
                        '<td>' + item.supMaxima + '</td></tr>')
            });
            $('#tituloTrans').text('Transferencia para cumplir la norma: '+ data.transfMax + ' w/m');

        });

});
