using Examen.Modelos;
using Examen.UnidadDeTrabajo;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Examen.MockData
{
    public static class UnidadTrabajoMockeada
    {
        public static IUnidadTrabajo ObtenerUnidadDeTrabajo()
        {

            Mock<IUnidadTrabajo> unidad = new Mock<IUnidadTrabajo>();
            unidad.ConfigurarCorporacion();
            unidad.ConfigurarMiembros();
            return unidad.Object;
        }
    }

    public static class UnidadTrabajoMockeadaExtensiones
    {
        public static Mock<IUnidadTrabajo> ConfigurarCorporacion(this Mock<IUnidadTrabajo> mock)
        {
            List<Corporation> corporaciones = new List<Corporation>{
                new Corporation
                {
                    Corp_No = 1,
                    Corp_Name = "Nueva corp. 1",
                    Street = string.Empty,
                    City = string.Empty,
                    State_Prov = string.Empty,
                    Country = string.Empty,
                    Mail_Code = string.Empty,
                    Phone_No = string.Empty,
                    Expr_Dt = DateTime.Now.AddYears(5),
                    Corp_Code = string.Empty
                },
                new Corporation
                {
                    Corp_No = 2,
                    Corp_Name = "Nueva corp. 2",
                    Street = string.Empty,
                    City = string.Empty,
                    State_Prov = string.Empty,
                    Country = string.Empty,
                    Mail_Code = string.Empty,
                    Phone_No = string.Empty,
                    Expr_Dt = DateTime.Now.AddYears(6),
                    Corp_Code = string.Empty
                },
                new Corporation
                {
                    Corp_No = 3,
                    Corp_Name = "Nueva corp. 3",
                    Street = string.Empty,
                    City = string.Empty,
                    State_Prov = string.Empty,
                    Country = string.Empty,
                    Mail_Code = string.Empty,
                    Phone_No = string.Empty,
                    Expr_Dt = DateTime.Now.AddYears(7),
                    Corp_Code = string.Empty
                },
                new Corporation
                {
                    Corp_No = 4,
                    Corp_Name = "Nueva corp. 4",
                    Street = string.Empty,
                    City = string.Empty,
                    State_Prov = string.Empty,
                    Country = string.Empty,
                    Mail_Code = string.Empty,
                    Phone_No = string.Empty,
                    Expr_Dt = DateTime.Now.AddYears(8),
                    Corp_Code = string.Empty
                }
            };

            mock.Setup(c => c.Corporaciones.ListarTodo()).Returns(corporaciones);

            mock.Setup(c => c.Corporaciones.TraerPorId(It.IsAny<int>())).Returns((int id) =>
            {
                return corporaciones.FirstOrDefault(x => x.Corp_No == id);
            });

            mock.Setup(c => c.Corporaciones.Insertar(It.IsAny<Corporation>())).Returns(1);

            mock.Setup(c => c.Corporaciones.Actualizar(It.IsAny<Corporation>())).Returns(true);

            mock.Setup(c => c.Corporaciones.Eliminar(It.IsAny<Corporation>())).Returns(true);

            mock.Setup(c => c.Corporaciones.ObtenerCorporacionesPaginadas(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int filaInicial, int filaFinal) =>
                {
                    return corporaciones;
                });

            mock.Setup(c => c.Corporaciones.ContarRegistros()).Returns(2);

            return mock;
        }

        public static Mock<IUnidadTrabajo> ConfigurarMiembros(this Mock<IUnidadTrabajo> mock)
        {
            List<Member> miembros = new List<Member>{
                new Member
                {
                    Member_No = 1,
                    LastName = "Quintana 1",
                    FirstName = "Daniel 1",
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
                },
                new Member
                {
                    Member_No = 2,
                    LastName = "Quintana 2",
                    FirstName = "Daniel 2",
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
                },
                new Member
                {
                    Member_No = 3,
                    LastName = "Quintana 3",
                    FirstName = "Daniel 3",
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
                },
                new Member
                {
                    Member_No = 4,
                    LastName = "Quintana 4",
                    FirstName = "Daniel 4",
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
                }
            };

            mock.Setup(c => c.Miembros.ListarTodo()).Returns(miembros);

            mock.Setup(c => c.Miembros.TraerPorId(It.IsAny<int>())).Returns((int id) =>
            {
                return miembros.FirstOrDefault(x => x.Member_No == id);
            });

            mock.Setup(c => c.Miembros.Insertar(It.IsAny<Member>())).Returns(1);

            mock.Setup(c => c.Miembros.Actualizar(It.IsAny<Member>())).Returns(true);

            mock.Setup(c => c.Miembros.Eliminar(It.IsAny<Member>())).Returns(true);

            mock.Setup(c => c.Miembros.ObtenerMiembrosPaginados(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int filaInicial, int filaFinal) =>
                {
                    return miembros;
                });

            mock.Setup(c => c.Miembros.ContarRegistros()).Returns(2);

            return mock;
        }
    }
}
