using Dapper;
using Examen.Modelos;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Examen.Repositorios.Credito.Dapper
{
    public class CorporacionRepositorio : Repositorio<Corporation>, ICorporacionRepositorio
    {
        public CorporacionRepositorio(string cadenaDeConexion) : base(cadenaDeConexion)
        {

        }

        public IEnumerable<Corporation> ObtenerCorporacionesPaginadas(int filaInicial, int filaFinal)
        {
            using (var conexion = new SqlConnection(_cadenaDeConexion))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@filaInicial", filaInicial);
                parametros.Add("@filaFinal", filaFinal);

                return conexion.Query<Corporation>("dbo.ListaPaginadaDeCorporaciones",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        public int ContarRegistros()
        {
            using (var conexion = new SqlConnection(_cadenaDeConexion))
            {
                return conexion.ExecuteScalar<int>("select Count(corp_no) from dbo.Corporation");
            }
        }

    }
}
