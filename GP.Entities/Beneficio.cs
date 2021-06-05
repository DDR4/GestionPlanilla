using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Beneficio
    {
        public decimal EsSalud { get; set; }
        public decimal AFPAporte { get; set; }
        public decimal AFPComision { get; set; }
        public decimal AFPSeguro { get; set; }

    }
}
