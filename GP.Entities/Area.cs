using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Area
    {
        public int? Area_Id { get; set; }
        public string Descripcion { get; set; }
        public int Asistencias { get; set; }
        public int Tardanzas { get; set; }
        public int Faltas { get; set; }
        public int Estado { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}
