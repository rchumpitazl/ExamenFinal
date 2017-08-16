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
    public class MiembrosControllerTests
    {
        private readonly MiembroController _controlador;

        public MiembrosControllerTests()
        {
            //_controlador = new MiembroController(new UnidadTrabajo(Configuracion.ConnectionString));
            _controlador = new MiembroController(UnidadTrabajoMockeada.ObtenerUnidadDeTrabajo());
        }

        [Fact(DisplayName = "[MiembroController] Listar_Miembro")]
        public void Listar_Test()
        {
            var resultado = _controlador.ListarTodo() as OkObjectResult;

            resultado.Should().NotBeNull();

            var modelo = resultado.Value as List<Member>;

            modelo.Count().Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[MiembroController] TraerPorId_Miembro")]
        public void TraerPorId_Test()
        {
            var resultado = _controlador.TraerPorId(1) as OkObjectResult;

            resultado.Should().NotBeNull();
        }


        [Fact(DisplayName = "[MiembroController] ListarPaginado_Miembro")]
        public void ListarPaginado_Test()
        {
            var resultado = _controlador.ObtenerMiembrosPaginadas(2, 25) as OkObjectResult;

            resultado.Should().NotBeNull();

            var modelo = resultado.Value as List<Member>;

            modelo.Count().Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "[MiembroController] Insertar_Miembro")]
        public void Insertar_Test()
        {
            Member miembro = new Member
            {
                LastName = "Quintana",
                FirstName = "Daniel",
                MiddleInitial = string.Empty,
                Street = string.Empty,
                City = string.Empty,
                State_Prov = string.Empty,
                Country = string.Empty,
                Mail_Code = string.Empty,
                Phone_No = string.Empty,
                Issue_Dt = DateTime.Now,
                Expr_Dt = DateTime.Now,
                Corp_No = 380,
                Prev_Balance = 0,
                Curr_Balance = 0,
                Member_Code = string.Empty
            };

            var resultado = _controlador.Insertar(miembro) as OkObjectResult;

            resultado.Should().NotBeNull();

            int modelo = (int)resultado.Value;

            modelo.Should().BeGreaterOrEqualTo(0);
        }


        [Fact(DisplayName = "[MiembroController] Actualizar_Miembro")]
        public void Actualizar_Test()
        {
            int miembroPrueba = ((_controlador.ListarTodo() as OkObjectResult).Value as List<Member>).Max(x => x.Member_No);

            Member ultimoMiembro = (_controlador.TraerPorId(miembroPrueba) as OkObjectResult).Value as Member;

            ultimoMiembro.MiddleInitial = "A";
            ultimoMiembro.Corp_No = 380;

            var resultado = _controlador.Actualizar(ultimoMiembro) as OkObjectResult;

            resultado.Should().NotBeNull();

            resultado.Value.Should().Be(true);
        }


        [Fact(DisplayName = "[MiembroController] Eliminar_Miembro")]
        public void Eliminar_Test()
        {
            int miembroPrueba = ((_controlador.ListarTodo() as OkObjectResult).Value as List<Member>).Max(x => x.Member_No);

            Member ultimoMiembro = (_controlador.TraerPorId(miembroPrueba) as OkObjectResult).Value as Member;

            var resultado = _controlador.Eliminar(ultimoMiembro) as OkObjectResult;

            resultado.Should().NotBeNull();

            resultado.Value.Should().Be(true);
        }

        [Fact(DisplayName = "[MiembroController] Contar_Registros_Miembro")]
        public void Contar_Registros_Test()
        {            
            var resultado = _controlador.ContarRegistros() as OkObjectResult;

            resultado.Should().NotBeNull();

            int modelo = (int)resultado.Value;

            modelo.Should().BeGreaterOrEqualTo(1);
        }

    }
}
