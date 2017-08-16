using Examen.Modelos;

namespace Examen.Repositorios.Credito
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario ValidarUsuario(string email, string contrasena);
    }
}
