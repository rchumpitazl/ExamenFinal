using Examen.Repositorios.Credito;

namespace Examen.UnidadDeTrabajo
{
    public interface IUnidadTrabajo
    {
        ICorporacionRepositorio Corporaciones { get; }
        IMiembroRepositorio Miembros { get; }
        IUsuarioRepositorio Usuarios { get; }
    }
}
