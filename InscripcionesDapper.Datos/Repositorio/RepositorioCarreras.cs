using Dapper;
using InscripcionesDapper.Comun.Interfaces;
using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Datos.Repositorio
{
    public class RepositorioCarreras : IRepositorioCarreras
    {

        private readonly IDbTransaction _transaction;
        public RepositorioCarreras(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Agregar(Carrera carrera)
        {
            string insertQuery = "INSERT INTO Carreras (NombreCarrera) " +
                "VALUES(@NombreCarrera); SELECT SCOPE_IDENTITY()";
            int id = _transaction.Connection.QuerySingle<int>(insertQuery, carrera, transaction: _transaction);
            carrera.CarreraId = id;
        }

        public void Borrar(int carreraId)
        {

            string deleteQuery = "DELETE FROM Carreras WHERE CarreraId=@CarreraId";
            _transaction.Connection.Execute(deleteQuery, new { CarreraId = carreraId }, transaction: _transaction);

        }

        public bool Existe(Carrera carrera)
        {
            var cantidad = 0;
            string selectQuery;
            if (carrera.CarreraId == 0)
            {
                selectQuery = "SELECT COUNT(*) FROM Carreras WHERE NombreCarrera=@NombreCarrera";

            }
            else
            {
                selectQuery = "SELECT COUNT(*) FROM Carreras WHERE NombreCarrera=@NombreCarrera AND CarreraId<>@CarreraId";

            }
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, carrera, transaction: _transaction);
            return cantidad > 0;
        }
        public List<Carrera> GetAll()
        {
            List<Carrera> lista = new List<Carrera>();
            string selectQuery = "SELECT*FROM Carreras ORDER BY NombreCarrera";
            lista = _transaction.Connection.Query<Carrera>(selectQuery, transaction: _transaction).ToList();
            return lista;
        }
        public bool EstaRelacionada(Carrera carrera)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM Cursadas WHERE CarreraId=@carreraId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { carreraId = carrera.CarreraId }, transaction: _transaction);
            return cantidad > 0;
        }
    }
}
