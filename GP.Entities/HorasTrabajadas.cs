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
        public int? Horas_Trabajadas_Id { get; set; }
        public int Horas_Trabajadas { get; set; }
        public int Horas_Tardanzas { get; set; }
        public string Periodo { get; set; }
        public int Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime PrimerDia { get; set; }
        public DateTime UltimoDia { get; set; }
        public int DiasTrabajados { get; set; }
        public int DiasTardanzas { get; set; }
        public int DiasNoTrabajados { get; set; }
        public int HorasNoTrabajados { get; set; }
        public Operacion Operacion { get; set; }
    }
}
