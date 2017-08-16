using Examen.UnidadDeTrabajo;
using System.Linq;
using Xunit;

namespace Examen.PruebaDatos
{
    public class MemberPruebas
    {
        private readonly IUnidadTrabajo _unidad;

        public MemberPruebas()
        {
            _unidad = new UnidadTrabajo(Configuracion.ConnectionString);
        }

        [Fact(DisplayName = "[Miembro] Prueba Traer Todos")]
        public void Member_Prueba_TraerTodos()
        {
            var resultado = _unidad.Miembros.ListarTodo().ToList();

            Assert.True(resultado.Count > 0);
        }
    }
}
