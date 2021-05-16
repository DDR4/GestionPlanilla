using GP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class HorasTrabajadas
    {
        public int Horas_Trabajadas { get; set; }
        public int Horas_Tardanzas { get; set; }
        public int Faltas { get; set; }
        public string Periodo { get; set; }
        public Operacion Operacion { get; set; }
    }
}
