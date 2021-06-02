using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Calculos
    {
        public int? Calculo_Boleta_Id { get; set; }
        public string Descripcion { get; set; }
        
        public decimal Monto { get; set; }
        public int Estado { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}
