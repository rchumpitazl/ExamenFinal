using Examen.Modelos;
using System.Collections.Generic;

namespace Examen.Repositorios.Credito
{
    public interface IMiembroRepositorio : IRepositorio<Member>
    {
        IEnumerable<Member> ObtenerMiembrosPaginados(int filaInicial, int filaFinal);
        int ContarRegistros();
    }
}
