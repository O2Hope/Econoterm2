var ambiental1;
var superficial1;
var operacion1;
var diametro1;
var viento1;
var emisividad1;
var aislante1;
var flux = [];
var espesor = [];
var supMaximus = [];

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
    if ($('#tipo').val() == 'Deposito' || $('#tipo').val() == 'Tapa' || $('#tipo').val() == 'Pared' )
        $('#diametro').prop('disabled', true);
    else $('#diametro').prop('disabled', false);

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
                    if (i == 10 || i == 20 || i == 30 || i == 40 || i == 50 || i == 60 || i == 70 || i == 80 || i == 90){
                        var flexC = item.flux.slice(0,-4);
                        var supC = item.supMaxima.slice(0,-2);

                        flux.push(flexC);
                        supMaximus.push(supC);
                    }



                $('#tablaCalc').find('tbody')
                    .append('<tr><td>' + i + '</td>' + '<td>' + item.espesor + '</td>' + '<td>' + item.flux + '</td>' +
                        '<td>' + item.supMaxima + '</td></tr>');

            });

            $('#tituloTrans').text('Transferencia para cumplir la norma: '+ data.transfMax + ' w/m');

        });

});

$('#descargaPDF').click(function () {
    pruebaDivAPdf()
});

function pruebaDivAPdf() {
    var pdf = new jsPDF('p', 'pt', 'letter');

    pdf.cellInitialize();
    pdf.setFontSize(10);
    pdf.text(30,20,$('#tituloTrans').text());

    $.each($('#tablaCalc tr'), function (i, row) {
        if ($(row).text().trim().length !== 0) {
            $.each($(row).find("td, th"), function (j, cell) {
                var txt = $(cell).text().trim() || " ";
                var width = 180;
                if (j != 1) {
                    pdf.cell(10, 50, width, 18, txt, i);
                }
            });
        }
    });

    pdf.save('Calculo-Econoterm.pdf');
}

var ctx = document.getElementById("myChart").getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: [64, 127, 190, 254, 318, 381, 444, 508, 572 ],
        datasets: [{
            label: 'Flux',
            data: flux,
            borderColor: [
                'rgba(255,99,132,1)'
            ],
            borderWidth: 1
        },{
            label: 'Temperatura Superficial Max',
            data: supMaximus,
            borderColor: '#1b1b1b'
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero:true
                }
            }]
        }
    }
});