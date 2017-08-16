using Examen.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WebApi.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IUnidadTrabajo _unidad;

        public BaseController(IUnidadTrabajo unidad)
        {
            _unidad = unidad;
        }
    }
}
