using Examen.Repositorios.Credito;
using Examen.Repositorios.Credito.Dapper;

namespace Examen.UnidadDeTrabajo
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        public UnidadTrabajo(string cadenaConexion)
        {
            Corporaciones = new CorporacionRepositorio(cadenaConexion);
            Miembros = new MiembroRepositorio(cadenaConexion);
            Usuarios = new UsuarioRepositorio(cadenaConexion);
        }

        public ICorporacionRepositorio Corporaciones { get; private set; }
        public IMiembroRepositorio Miembros { get; private set; }
        public IUsuarioRepositorio Usuarios { get; private set; }

    }
}
