using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EconoterAPI.Models
{
    public class Calculo
    {
        public Tipo Tipo { get; set; }
        public decimal Diametro { get; set; }
        public decimal Ambiental { get; set; }
        public decimal Operacion { get; set; }
        public decimal Superficial { get; set; }
        public decimal Viento { get; set; }
        public decimal Emisividad { get; set; }
        public Aislante Aislante { get; set; }
    }
    
    public class Resultado
    {

        public string Espesor { get; set; }
        public string Flux { get; set; }
        public string SupMaxima { get; set; }

    }

    public class DetallesViewModel
    {

        public ArrayList Lista { get; set; } 
        public int TransfMax { get; set; }
        public bool tuberia { get; set; }
    }

    public enum Aislante
    {
        
        [Display(Name = "COLCHA DE FIBRA MINERAL ROLAN® CP/CA - 96 ")]
        CPCA96,
        [Display(Name = "COLCHA DE FIBRA MINERAL ROLAN® CP/CA - 144 ")]
        CPCA144,
        [Display(Name = "COLCHA DE FIBRA MINERAL ROLAN® CP/CA - 192")]
        CPCA192,
        [Display(Name = "TERMOAISLANTE PREFORMADO DE FIBRA MINERAL ROLAN®")]
        TERMOAISLANTE,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 32")]
        FF32,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 48")]
        FF48,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 64")]
        FF64,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 96")]
        FF96,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 128")]
        FF128
        

    }

    public enum Tipo
    {
       
        Deposito,
        Esfera,
        Pared,
        Tapa,
        [Display(Name="Tuberia Horizontal")]
        Tuberia1,
        [Display(Name="Tuberia Vertical")]
        Tuberia2
        
    }
}