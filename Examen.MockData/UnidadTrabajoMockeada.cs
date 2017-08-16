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
                    Corp_Name = "Corporación 1",
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
                    Corp_Name = "Corporación 2",
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
                    Corp_Name = "Corporación 3",
                    Street = string.Empty,
                    City = string.Empty,
                    State_Prov = string.Empty,
                    Country = string.Empty,
                    Mail_Code = string.Empty,
                    Phone_No = string.Empty,
                    Expr_Dt = DateTime.Now.AddYears(7),
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

    }
}
