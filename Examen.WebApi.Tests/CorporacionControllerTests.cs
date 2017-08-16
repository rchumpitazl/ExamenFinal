using Examen.MockData;
using Examen.Modelos;
using Examen.PruebaDatos;
using Examen.UnidadDeTrabajo;
using Examen.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examen.WebApi.Tests
{
    public class CorporacionControllerTests
    {
        private readonly CorporacionController _controlador;

        public CorporacionControllerTests()
        {
            //_controlador = new CorporacionController(new UnidadTrabajo(Configuracion.ConnectionString));
            _controlador = new CorporacionController(UnidadTrabajoMockeada.ObtenerUnidadDeTrabajo());
        }

        [Fact(DisplayName = "[CorporacionController] Listar_Corporacion")]
        public void Listar_Test()
        {
            var resultado = _controlador.ListarTodo() as OkObjectResult;

            resultado.Should().NotBeNull();

            var modelo = resultado.Value as List<Corporation>;

            modelo.Count().Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[CorporacionController] TraerPorId_Corporacion")]
        public void TraerPorId_Test()
        {
            var resultado = _controlador.TraerPorId(1) as OkObjectResult;

            resultado.Should().NotBeNull();
        }


        [Fact(DisplayName = "[CorporacionController] ListarPaginado_Corporacion")]
        public void ListarPaginado_Test()
        {
            var resultado = _controlador.ObtenerCorporacionesPaginadas(2, 25) as OkObjectResult;

            resultado.Should().NotBeNull();

            var modelo = resultado.Value as List<Corporation>;

            modelo.Count().Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[CorporacionController] Insertar_Corporacion")]
        public void Insertar_Test()
        {
            Corporation corporacion = new Corporation
            {                
                Corp_Name = "Nueva corporación",
                Street = string.Empty,
                City = string.Empty,
                State_Prov = string.Empty,
                Country = string.Empty,
                Mail_Code = string.Empty,
                Phone_No = string.Empty,
                Expr_Dt = DateTime.Now.AddYears(5),
                Corp_Code = string.Empty
            };

            var resultado = _controlador.Insertar(corporacion) as OkObjectResult;

            resultado.Should().NotBeNull();

            int modelo = (int)resultado.Value;

            modelo.Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[CorporacionController] Actualizar_Corporacion")]
        public void Actualizar_Test()
        {
            int corporacionPrueba = ((_controlador.ListarTodo() as OkObjectResult).Value as List<Corporation>).Max(x => x.Corp_No);

            Corporation ultimaCorporacion = (_controlador.TraerPorId(corporacionPrueba) as OkObjectResult).Value as Corporation;

            ultimaCorporacion.Corp_Name += "Actualizado";

            var resultado = _controlador.Actualizar(ultimaCorporacion) as OkObjectResult;

            resultado.Should().NotBeNull();

            resultado.Value.Should().Be(true);
        }


        [Fact(DisplayName = "[CorporacionController] Eliminar_Corporacion")]
        public void Eliminar_Test()
        {
            int corporacionPrueba = ((_controlador.ListarTodo() as OkObjectResult).Value as List<Corporation>).Max(x => x.Corp_No);

            Corporation ultimaCorporacion = (_controlador.TraerPorId(corporacionPrueba) as OkObjectResult).Value as Corporation;

            var resultado = _controlador.Eliminar(ultimaCorporacion) as OkObjectResult;

            resultado.Should().NotBeNull();

            resultado.Value.Should().Be(true);
        }

        [Fact(DisplayName = "[CorporacionController] Contar_Registros_Corporacion")]
        public void Contar_Registros_Test()
        {
            var resultado = _controlador.ContarRegistros() as OkObjectResult;

            resultado.Should().NotBeNull();

            int modelo = (int)resultado.Value;

            modelo.Should().BeGreaterOrEqualTo(1);
        }

    }
}
