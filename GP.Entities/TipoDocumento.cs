using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class TipoDocumento
    {
        public int? TipoDocumento_Id { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
    }
}
