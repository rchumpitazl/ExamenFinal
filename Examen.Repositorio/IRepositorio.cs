using System.Collections.Generic;

namespace Examen.Repositorios
{
    public interface IRepositorio<T> where T : class
    {
        bool Eliminar(T entidad);
        int Insertar(T entidad);
        bool Actualizar(T entidad);
        IEnumerable<T> ListarTodo();
        T TraerPorId(int id);        
    }
}
