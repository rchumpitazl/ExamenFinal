using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Examen.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly string _cadenaDeConexion;

        public Repositorio(string cadenaDeConexion)
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"[{type.Name}]"; };
            _cadenaDeConexion = cadenaDeConexion;
        }

        public bool Eliminar(T entidad)
        {
            using (var connection = new SqlConnection(_cadenaDeConexion))
            {
                return connection.Delete(entidad);
            }
        }

        public IEnumerable<T> ListarTodo()
        {
            using (var connection = new SqlConnection(_cadenaDeConexion))
            {
                return connection.GetAll<T>();
            }
        }

        public T TraerPorId(int id)
        {
            using (var connection = new SqlConnection(_cadenaDeConexion))
            {
                return connection.Get<T>(id);
            }
        }

        public int Insertar(T entidad)
        {
            using (var connection = new SqlConnection(_cadenaDeConexion))
            {
                return (int)connection.Insert(entidad);
            }
        }

        public bool Actualizar(T entidad)
        {
            using (var connection = new SqlConnection(_cadenaDeConexion))
            {
                return connection.Update(entidad);
            }
        }
    }
}
