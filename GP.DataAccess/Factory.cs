using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.Common;

namespace GP.DataAccess
{
    public static class Factory
    {
        private static string GetConnectionString()
        {
            try
            {
                var cnxKey = Common.ConfigurationUtilities.GetConnectionString();

                var cnx = cnxKey;//GetConnectionDecode(cnxKey);

                return cnx;
            }
            catch (Exception)
            {
                throw new Exception("Error al obtener la cadena de conexión.");
            }
        }

        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(GetConnectionString());

        private static string GetConnectionDecode(string conexion) {

            var stringarray = conexion.Split(';');

            string Conexion = string.Empty;

            for (int i = 0; i < stringarray.Length; i++)
            {
                string cnxAux = stringarray[i];

                if (i > 1)
                {
                    int intusu = cnxAux.IndexOf('=');

                    if (intusu > 0)
                    {
                        string strsub = cnxAux.Substring(intusu + 1);
                        string strdecode = cnxAux.Replace(strsub, EncriptacionBase64.Base64Decode(strsub));
                        cnxAux = strdecode;
                    }
                }
                Conexion = Conexion + cnxAux + ";";
            }
            return Conexion;
        }


        
    }
}
