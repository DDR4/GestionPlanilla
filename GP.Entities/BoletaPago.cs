using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class BoletaPago
    {   
        public int? BoletaPago_Id { get; set; }
        public Trabajador Trabajador { get; set; }
        public HorasTrabajadas HorasTrabajadas { get; set; }
        public int Indicador { get; set; }
        public byte[] Arraybytes { get; set; }
        public string Nombrearchivo { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}
