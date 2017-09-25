using System;
using System.Collections;
using EconoterAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EconoterAPI.Controllers
{
    public class Calculo : Controller
    {

        [HttpGet]
        [Route("api/transferencia")]
        public int CalculoTransferencia([FromQuery] double diametro, [FromQuery] double temperatura,
            [FromQuery] bool superficie)
        {

            temperatura = temperatura - 274;

            var matrizTemperatura = new[] {60, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650};
            var matrizDiametro = new[]
            {
                13, 19, 25, 38, 51, 64, 76, 102, 127, 152, 203, 254, 305, 356, 406, 457, 508, 559, 610, 660, 711, 762,
                10000
            };

            var matrizTransferencia = new[,]
            {
                {6, 12, 19, 29, 37, 49, 66, 75, 88, 97, 111, 125, 141},
                {7, 13, 21, 32, 41, 50, 68, 82, 96, 105, 121, 136, 153},
                {8, 15, 24, 36, 46, 56, 75, 90, 101, 116, 133, 142, 160},
                {10, 18, 29, 40, 50, 67, 85, 101, 119, 130, 148, 168, 188},
                {12, 21, 33, 45, 57, 70, 95, 113, 119, 138, 157, 178, 200},
                {13, 24, 37, 50, 63, 77, 104, 116, 130, 150, 172, 194, 218},
                {16, 26, 43, 57, 72, 87, 109, 131, 145, 168, 192, 207, 232},
                {19, 28, 44, 60, 77, 102, 119, 143, 167, 194, 210, 238, 267},
                {17, 30, 54, 70, 84, 101, 126, 153, 170, 202, 214, 253, 279},
                {20, 34, 58, 79, 95, 114, 134, 162, 174, 211, 239, 266, 310},
                {33, 43, 65, 94, 104, 138, 161, 171, 208, 234, 267, 315, 349},
                {40, 51, 77, 100, 123, 148, 174, 200, 243, 276, 311, 347, 405},
                {47, 59, 89, 115, 141, 169, 198, 227, 276, 293, 351, 391, 433},
                {51, 64, 96, 125, 152, 182, 213, 244, 291, 314, 355, 419, 463},
                {58, 72, 108, 140, 169, 203, 237, 271, 306, 347, 392, 438, 511},
                {57, 80, 120, 154, 187, 224, 260, 298, 336, 380, 429, 479, 530},
                {63, 88, 132, 169, 205, 245, 284, 324, 365, 388, 465, 519, 574},
                {69, 97, 143, 184, 222, 265, 273, 315, 395, 407, 502, 559, 618},
                {75, 105, 155, 199, 240, 251, 295, 339, 424, 449, 494, 553, 661},
                {82, 113, 167, 214, 223, 270, 325, 374, 454, 479, 528, 590, 705},
                {88, 121, 179, 229, 238, 296, 346, 398, 438, 510, 575, 642, 694},
                {94, 129, 190, 243, 261, 314, 368, 422, 466, 540, 605, 679, 736},
                {37, 49, 70, 88, 91, 107, 123, 126, 153, 157, 175, 192, 224}
            };

            var i = 0;
            var r = 0;

            if (superficie == false)
            {
                while (Convert.ToInt32(temperatura) > matrizTemperatura[i])
                {
                    i++;
                }
                r = 22;
            }

            else
            {
                while (Convert.ToInt32(diametro) > matrizDiametro[r])
                {
                    r++;
                }

                while (Convert.ToInt32(temperatura) > matrizTemperatura[i])
                {
                    i++;
                }
            }

            return matrizTransferencia[r, i];
        }

        // GET
        public IActionResult Norm(Calculo calculo)
        {
            return new ObjectResult(calculo);
        }

        [HttpGet]
        [Route("api/calculo")]
        public ActionResult Detalles([FromQuery]double ambiental, [FromQuery] double superficial,
            [FromQuery]double operacion, [FromQuery]double diametro,
            [FromQuery]double viento,
            [FromQuery]double emisividad, [FromQuery]int aislante)
        {

            var datagri = new ArrayList();
            var transfMax = 0;
            var tipo = false;

            for (var i = 0; i < 100; i++)
            {
                var ta = ambiental + 274;
                var tsup = superficial + 274;
                var top = operacion + 274;
                var c = (diametro <= 0) ? 1.79 : 1.016;
                var esp = (6.35 * i) * 0.001;
                double kais = 0;
                var v = viento * 0.621371;
                var emss = emisividad;
                var Do = diametro * 0.001;
                var key = false;


                double valorA ;
                double valorB ;
                double valorC ;
                double operacion2;

                if (aislante == (int) Aislante.CPCA96)
                {
                    valorA = 0.2187 / 6.935;
                    valorB = 0.0002148 / 6.935;
                    valorC = 0.0000006908 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.CPCA144)
                {
                    valorA = 0.2192 / 6.935;
                    valorB = 0.0001433 / 6.935;
                    valorC = 0.0000006312 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.CPCA192)
                {

                    valorA = 0.2331 / 6.935;
                    valorB = 0.0000679 / 6.935;
                    valorC = 0.0000006385 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.FF32)
                {
                    valorA = 0.212 / 6.935;
                    valorB = 0.0002696 / 6.935;
                    valorC = 0.000001367 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.FF48)
                {
                    valorA = 0.2035 / 6.935;
                    valorB = 0.000373 / 6.935;
                    valorC = 0.0000009124 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.FF64)
                {
                    valorA = 0.2033 / 6.935;
                    valorB = 0.0003868 / 6.935;
                    valorC = 0.0000006548 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.FF96)
                {
                    valorA = 0.2187 / 6.935;
                    valorB = 0.0002148 / 6.935;
                    valorC = 0.0000006908 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }
                if (aislante == (int) Aislante.FF128)
                {
                    valorA = 0.2114 / 6.935;
                    valorB = 0.0002836 / 6.935;
                    valorC = 0.0000004948 / 6.935;

                    operacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = valorA + valorB * operacion2 + valorC * Math.Pow(operacion2, 2);

                }

                if (c == 1.79)
                {
                    transfMax = CalculoTransferencia(Do * 1000, top, false);
                    tipo = true;
                    while (key == false)
                    {

                        var hc = 3.0075 * c * (Math.Pow(1.11 / (tsup + ta - 510.44), 0.181)) *
                                 (Math.Pow(1.8 * (tsup - ta), 0.266)) *
                                 (Math.Pow(1 + 7.9366 * Math.Pow(10, -4) * v, 0.5));
                        var hr = (0.9824 * Math.Pow(10, -8) * emss) * (Math.Pow(ta, 4) - Math.Pow(tsup, 4)) /
                                 (ta - tsup);
                        var hs = hc + hr;
                        var q = (top - ta) / ((esp / kais) + (1 / hs));
                        var tsc = ta + q / hs;


                        var tsc2 = Convert.ToInt32(tsc);
                        var tsup2 = Convert.ToInt32(tsup);

                        if (tsc2 == tsup2)
                        {
                            key = true;

                            var esp2 = Convert.ToInt32(esp * 1000);
                            var q2 = Convert.ToString(q);


                            var qint = Convert.ToInt32(q);
                            var tscint = Convert.ToInt32(tsc - 274);

                            datagri.Add(new Resultado
                            {
                                Espesor = esp2 + " mm",
                                Flux = qint + " w/m2",
                                SupMaxima = tscint + " °C"
                            });

                        }
                        else
                        {
                            key = false;
                            tsup = tsc;
                        }
                    }



                }
                else
                {

                    transfMax = CalculoTransferencia(Do * 1000, top, true);
                    tipo = false;

                    while (key == false)
                    {
                        var da = Do + 2 * esp;
                        var hc = 2.7241 * c * Math.Pow(da, -0.2) * Math.Pow(1.11 / (tsup + ta - 510.44), 0.181) *
                                 Math.Pow(1.8 * (tsup - ta), 0.266) * Math.Pow(1 + 7.9366 * Math.Pow(10, -4) * v, 0.5);
                        var hr = 0.9824 * Math.Pow(10, -8) * emss * (Math.Pow(ta, 4) - Math.Pow(tsup, 4)) / (ta - tsup);
                        var hs = hc + hr;
                        var q = Math.PI * (top - ta) / (1 / (2 * kais) * Math.Log(da / Do) + 1 / (hs * da));
                        var tsc = top - (q / (2 * Math.PI * kais) * Math.Log(da / Do));


                        var tsc2 = Convert.ToInt32(tsc);
                        var tsup2 = Convert.ToInt32(tsup);
                        if (tsc2 == tsup2)
                        {
                            key = true;
                            tsc += -274;

                            var esp2 = Convert.ToInt32(esp * 1000);

                            var qint = Convert.ToInt32(q);
                            var tscint = Convert.ToInt32(tsc);


                            datagri.Add(new Resultado
                            {
                                Espesor = esp2 + " mm",
                                Flux = qint + " w/m",
                                SupMaxima = tscint + " °C"
                            });


                        }

                        else
                        {
                            key = false;
                            tsup = tsc;

                        }
                    }
                }
                
            }
            
            var viewModel = new DetallesViewModel
            {
                Lista = datagri,
                TransfMax = transfMax,
                tuberia = tipo
            };
            
            return new ObjectResult(viewModel);
        }
    }
}