using InscripcionesDapper.Datos.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InscripcionesDapper.Comun;
using InscripcionesDapper.Comun.Interfaces;

namespace InscripcionesDapper.Datos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public IRepositorioAlumnos Alumnos { get; }
        public IRepositorioCarreras Carreras { get; }
        public IRepositorioTurnos Turnos { get; }
        public IRepositorioCursadas Cursadas { get; }
        public IRepositorioAlumnosCursadas AlumnosCursadas { get; }

        public UnitOfWork(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new ArgumentException("La cadena de conexión no puede estar vacía.", nameof(cadenaConexion));
            }

            _connection = new SqlConnection(cadenaConexion);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Alumnos = new RepositorioAlumnos(_transaction);
            Carreras = new RepositorioCarreras(_transaction);
            Turnos = new RepositorioTurnos(_transaction);
            Cursadas = new RepositorioCursadas(_transaction);
            AlumnosCursadas= new RepositorioAlumnosCursadas(_transaction);


        }


        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
