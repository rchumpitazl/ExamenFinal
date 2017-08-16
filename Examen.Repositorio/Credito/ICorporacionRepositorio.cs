using Examen.Modelos;
using System.Collections.Generic;

namespace Examen.Repositorios.Credito
{
    public interface ICorporacionRepositorio : IRepositorio<Corporation>
    {
        IEnumerable<Corporation> ObtenerCorporacionesPaginadas(int filaInicial, int filaFinal);
        int ContarRegistros();
    }
}
