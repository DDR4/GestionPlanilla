using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebGestionPlanilla.Core.Identity
{
    public class CustomApplicationUser : IUser
    {
        public string Id { get; }
        public string UserName { get; set; }
        public string Trabajador_Id { get; set; }

        public GestionPlanilla.Common.Response<GestionPlanilla.Entities.Trabajador> Trabajador { get; set; }

        public CustomApplicationUser() : this(new GestionPlanilla.Common.Response<GestionPlanilla.Entities.Trabajador>(default(GestionPlanilla.Entities.Trabajador)))
        {
        }


        public CustomApplicationUser(GestionPlanilla.Common.Response<GestionPlanilla.Entities.Trabajador> usuario)
        {
            Trabajador = usuario;

            if (usuario.InternalStatus == GestionPlanilla.Common.EnumTypes.InternalStatus.Success)
            {
                Id = usuario.Data.Tipo.ToString();
                UserName = usuario.Data.Nombres;
                Trabajador_Id = usuario.Data.Trabajador_Id.ToString();
            }
            else
            {
                // Valores por defecto, lo cuales no tendran relevancia, puesto que la aplicación no iniciará sesión.
                Id = "01";
                UserName = "USER";
                Trabajador_Id = "01";
            }
        }


    }
}