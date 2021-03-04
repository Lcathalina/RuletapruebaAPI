using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuletapruebaAPI.Entities
{
    public class Historial
    {
        [Key]
        public  int IdHistorial { get; set; }
        public int IdApuesta { get; set; }
        public int MontoApuesta { get; set; }
        public int NumeroApostado { get; set; }
        public string Color { get; set; }
       




    }
}
