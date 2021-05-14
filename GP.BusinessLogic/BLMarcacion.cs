using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionPlanilla.Common;
using GestionPlanilla.DataAccess;
using GestionPlanilla.Entities;

namespace GestionPlanilla.BusinessLogic
{
    public class BLMarcacion
    {
        private DAMarcacion repository;

        public BLMarcacion()
        {
            repository = new DAMarcacion();
        }

        public Response<Turno> GetHorarios(int Trabajador_Id)
        {
            try
            {
                var result = repository.GetHorarios(Trabajador_Id);
                return new Response<Turno>(result);
            }
            catch (Exception ex)
            {
                return new Response<Turno>(ex);
            }
        }

        public Response<Turno> GetMarcaHorarios(int Trabajador_Id)
        {
            try
            {
                var result = repository.GetMarcaHorarios(Trabajador_Id);
                return new Response<Turno>(result);
            }
            catch (Exception ex)
            {
                return new Response<Turno>(ex);
            }
        }

        public Response<DateTime> MarcarIngreso(Trabajador obj)
        {
            try
            {
                var result = repository.MarcarIngreso(obj);
                return new Response<DateTime>(result);
            }
            catch (Exception ex)
            {
                return new Response<DateTime>(ex);
            }
        }

        public Response<DateTime> MarcarSalida(Trabajador obj)
        {
            try
            {
                var result = repository.MarcarSalida(obj);
                return new Response<DateTime>(result);
            }
            catch (Exception ex)
            {
                return new Response<DateTime>(ex);
            }
        }



    }
}
