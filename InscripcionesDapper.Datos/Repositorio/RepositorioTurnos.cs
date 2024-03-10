using Dapper;
using InscripcionesDapper.Comun.Interfaces;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Datos.Repositorio
{
    public class RepositorioTurnos : IRepositorioTurnos
    {
        private readonly IDbTransaction _transaction;
        public RepositorioTurnos(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Agregar(Turno turno)
        {
            string insertQuery = "INSERT INTO Turnos (DescripcionTurno) " +
                "VALUES(@DescripcionTurno); SELECT SCOPE_IDENTITY()";
            int id = _transaction.Connection.QuerySingle<int>(insertQuery, turno, transaction: _transaction);
            turno.TurnoId = id;
        }

        public void Borrar(int turnoId)
        {

            string deleteQuery = "DELETE FROM Turnos WHERE TurnoId=@TurnoId";
            _transaction.Connection.Execute(deleteQuery, new { TurnoId = turnoId }, transaction: _transaction);

        }

        public bool EstaRelacionada(Turno turno)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM Cursadas WHERE TurnoId=@turnoId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { turnoId = turno.TurnoId }, transaction: _transaction);
            return cantidad > 0;
        }

        public bool Existe(Turno turno)
        {
            var cantidad = 0;
            string selectQuery;
            if (turno.TurnoId == 0)
            {
                selectQuery = "SELECT COUNT(*) FROM Turnos WHERE DescripcionTurno=@DescripcionTurno";

            }
            else
            {
                selectQuery = "SELECT COUNT(*) FROM Turnos WHERE DescripcionTurno=@DescripcionTurno AND TurnoId<>@TurnoId";

            }
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, turno, transaction: _transaction);
            return cantidad > 0;
        }

        public List<Turno> GetAll()
        {
            List<Turno> lista = new List<Turno>();
            string selectQuery = "SELECT*FROM Turnos ORDER BY DescripcionTurno";
            lista = _transaction.Connection.Query<Turno>(selectQuery, transaction: _transaction).ToList();
            return lista;
        }
    }
}
