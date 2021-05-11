﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlanilla.Entities
{
    public class Cargo
    {
        public int? Cargo_Id { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public Area Area { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}