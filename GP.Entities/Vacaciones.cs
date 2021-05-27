using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Vacaciones
    {
        public int? Vacaciones_Id { get; set; }
        public string NombreApellido { get; set; }
        public int DiasDisponibles { get; set; }
        public int DiasTotales { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}
