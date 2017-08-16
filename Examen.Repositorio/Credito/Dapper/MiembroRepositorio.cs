using Dapper;
using Examen.Modelos;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Examen.Repositorios.Credito.Dapper
{
    public class MiembroRepositorio : Repositorio<Member>, IMiembroRepositorio
    {
        public MiembroRepositorio(string cadenaDeConexion) : base(cadenaDeConexion)
        {

        }

        public IEnumerable<Member> ObtenerMiembrosPaginados(int filaInicial, int filaFinal)
        {
            using (var conexion = new SqlConnection(_cadenaDeConexion))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@filaInicial", filaInicial);
                parametros.Add("@filaFinal", filaFinal);

                return conexion.Query<Member>("dbo.ListaPaginadaDeMiembros",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public int ContarRegistros()
        {
            using (var conexion = new SqlConnection(_cadenaDeConexion))
            {
                return conexion.ExecuteScalar<int>("select Count(member_no) from dbo.Member");
            }
        }

    }
}
