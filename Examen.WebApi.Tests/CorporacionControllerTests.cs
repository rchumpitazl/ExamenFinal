using Examen.WebApi.Controllers;
using System;
using Xunit;
using Examen.MockData;
using Microsoft.AspNetCore.Mvc;
using Examen.Modelos;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Examen.WebApi.Tests
{
    public class CorporacionControllerTests
    {
        private readonly CorporacionController _controlador;

        public CorporacionControllerTests()
        {
            _controlador = new CorporacionController(UnidadTrabajoMockeada.ObtenerUnidadDeTrabajo());
        }

        [Fact(DisplayName = "[CorporacionController] ListarTodo")]
        public void Listar_Test()
        {
            var resultado = _controlador.ListarTodo() as OkObjectResult;

            resultado.Should().NotBeNull();

            var modelo = resultado.Value as List<Corporation>;

            modelo.Count().Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[CorporacionController] TraerPorId")]
        public void TraerPorId_Test()
        {
            var resultado = _controlador.TraerPorId(1) as OkObjectResult;

            resultado.Should().NotBeNull();
        }

        [Fact(DisplayName = "[CorporacionController] Insertar")]
        public void Insertar_Test()
        {
            Corporation corporacion = new Corporation
            {
                Corp_Name = "Corporación X",
                Street = string.Empty,
                City = string.Empty,
                State_Prov = string.Empty,
                Country = string.Empty,
                Mail_Code = string.Empty,
                Phone_No = string.Empty,
                Expr_Dt = DateTime.Now.AddYears(10),
                Corp_Code = string.Empty
            };

            var resultado = _controlador.Insertar(corporacion) as OkObjectResult;

            resultado.Should().NotBeNull();

            int modelo = (int)resultado.Value;

            modelo.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[CorporacionController] Actualizar")]
        public void Actualizar_Test()
        {
            int corporacionPrueba = ((_controlador.ListarTodo() as OkObjectResult).Value as List<Corporation>).Max(x => x.Corp_No);

            Corporation ultimaCorporacion = (_controlador.TraerPorId(corporacionPrueba) as OkObjectResult).Value as Corporation;

            ultimaCorporacion.Corp_Name += "Actualizado";

            var resultado = _controlador.Actualizar(ultimaCorporacion) as OkObjectResult;

            resultado.Should().NotBeNull();

            resultado.Value.Should().Be(true);
        }

        [Fact(DisplayName = "[CorporacionController] Eliminar")]
        public void Eliminar_Test()
        {
            int corporacionPrueba = ((_controlador.ListarTodo() as OkObjectResult).Value as List<Corporation>).Max(x => x.Corp_No);

            var resultado = _controlador.Eliminar(corporacionPrueba) as OkObjectResult;

            resultado.Should().NotBeNull();

            resultado.Value.Should().Be(true);
        }

        [Fact(DisplayName = "[CorporacionController] ObtenerCorporacionesPaginadas")]
        public void ListarPaginado_Test()
        {
            var resultado = _controlador.ObtenerCorporacionesPaginadas(1, 25) as OkObjectResult;

            resultado.Should().NotBeNull();

            var modelo = resultado.Value as List<Corporation>;

            modelo.Count().Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[CorporacionController] ContarRegistros")]
        public void Contar_Registros_Test()
        {
            var resultado = _controlador.ContarRegistros() as OkObjectResult;

            resultado.Should().NotBeNull();

            int modelo = (int)resultado.Value;

            modelo.Should().BeGreaterOrEqualTo(1);
        }
    }
}
