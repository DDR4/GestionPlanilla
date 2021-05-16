using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Turno
    {
        public int? Turno_Id { get; set; }
        public string Descripcion { get; set; }
        public string Hora_Ingreso { get; set; }
        public string Hora_Salida { get; set; }
        public DateTime Marcar_Hora_Ingreso { get; set; }
        public DateTime Marcar_Hora_Salida { get; set; }
        public int Estado { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }

    }
}
