using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Empleador
    {
        public int? Empleador_Id { get; set; }
        public string Descripcion { get; set; }
        public string Ruc { get; set; }
        public string Rubro { get; set; }
        public string Sede { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Estado { get; set; }
    }
}
