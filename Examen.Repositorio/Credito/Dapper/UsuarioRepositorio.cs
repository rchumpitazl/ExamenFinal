using Dapper;
using Examen.Modelos;
using System.Data.SqlClient;

namespace Examen.Repositorios.Credito.Dapper
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(string cadenaDeConexion) : base(cadenaDeConexion)
        {

        }

        public Usuario ValidarUsuario(string email, string contrasena)
        {
            using (var connection = new SqlConnection(_cadenaDeConexion))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@contrasena", contrasena);

                return connection.QueryFirst<Usuario>("dbo.ValidarUsuario", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
