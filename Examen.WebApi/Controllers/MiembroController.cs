using Microsoft.AspNetCore.Mvc;
using Examen.Modelos;
using Examen.UnidadDeTrabajo;

namespace Examen.WebApi.Controllers
{
    [Route("miembro")]
    public class MiembroController : BaseController
    {
        public MiembroController(IUnidadTrabajo unidad) : base(unidad)
        {
        }

        [HttpGet]
        public IActionResult ListarTodo()
        {
            return Ok(_unidad.Miembros.ListarTodo());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult TraerPorId(int id)
        {
            return Ok(_unidad.Miembros.TraerPorId(id));
        }

        [HttpPost]
        public IActionResult Insertar([FromBody]Member Miembro)
        {
            return Ok(_unidad.Miembros.Insertar(Miembro));
        }

        [HttpPut]
        public IActionResult Actualizar([FromBody]Member Miembro)
        {
            return Ok(_unidad.Miembros.Actualizar(Miembro));
        }

        [HttpDelete]
        public IActionResult Eliminar([FromBody]Member Miembro)
        {
            return Ok(_unidad.Miembros.Eliminar(Miembro));
        }

        [HttpGet]
        [Route("{numeroPagina}/{cantRegistros}")]
        public IActionResult ObtenerMiembrosPaginadas(int numeroPagina, int cantRegistros)
        {
            int filaInicial = (numeroPagina - 1) * cantRegistros + 1;
            int filaFinal = numeroPagina * cantRegistros;

            return Ok(_unidad.Miembros.ObtenerMiembrosPaginados(filaInicial, filaFinal));
        }

        [HttpGet]
        [Route("contar")]
        public IActionResult ContarRegistros()
        {
            return Ok(_unidad.Miembros.ContarRegistros());
        }

    }
}
