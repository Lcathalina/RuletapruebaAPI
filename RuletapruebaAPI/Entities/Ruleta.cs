using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuletapruebaAPI.Entities
{
    public class Ruleta
    {
        [Key]
        public int IdApuesta { get; set; }
        public int MontoApuesta { get; set; }
        public int NumeroApostado { get; set; }
        public bool Estado { get; set; }
        public string Color { get; set; }
        public int IdUsuario { get; set; }
        public int Resultado { get; set; }


    }
}
