using Dapper;
using InscripcionesDapper.Comun.Interfaces;
using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Datos.Repositorio
{
    public class RepositorioCursadas : IRepositorioCursadas
    {

        private readonly IDbTransaction _transaction;
        public RepositorioCursadas(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Agregar(Cursada cursada)
        {
            string insertQuery = "INSERT INTO Cursadas (FechaInicio,FechaFinal,CantidadAlumnos,TurnoId,CarreraId) " +
                "VALUES(@FechaInicio,@FechaFinal,@CantidadAlumnos,@TurnoId,@CarreraId); SELECT SCOPE_IDENTITY()";
            int id = _transaction.Connection.QuerySingle<int>(insertQuery, cursada, transaction: _transaction);
            cursada.CursadaId = id;
        }

        public void Borrar(int cursadaId)
        {
            string deleteQueryRel = "DELETE FROM AlumnosCursadas WHERE CursadaId=@CursadaId";
            _transaction.Connection.Execute(deleteQueryRel, new { CursadaId = cursadaId }, transaction: _transaction);
            string deleteQuery = "DELETE FROM Cursadas WHERE CursadaId=@CursadaId";
            _transaction.Connection.Execute(deleteQuery, new { CursadaId = cursadaId }, transaction: _transaction);
        }

        public bool EstaRelacionada(Cursada cursada)
        {
                int cantidad = 0;
                string selectQuery = @"SELECT COUNT(*) FROM Cursadas WHERE CarreraId=@carreraId";
                cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { carreraId = cursada.CarreraId }, transaction: _transaction);
                return cantidad > 0;    
        }

        public bool Existe(Cursada cursada)
        {
            var cantidad = 0;
            string selectQuery;
            if (cursada.CursadaId == 0)
            {
                selectQuery = "SELECT COUNT(*) FROM Cursadas WHERE TurnoId=@TurnoId and CarreraId=@CarreraId";

            }
            else
            {
                selectQuery = "SELECT COUNT(*) FROM Cursadas WHERE  CursadasId<>@CursadasId";

            }
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, cursada, transaction: _transaction);
            return cantidad > 0;
        }

        public CursadaDto Get(int cursadaId)
        {
            string Query = "SELECT c.CursadaId,c.FechaInicio,c.FechaFinal,c.CantidadAlumnos,t.TurnoId,t.DescripcionTurno,cc.CarreraId,cc.NombreCarrera FROM Cursadas c " +
                   "INNER JOIN Turnos t ON c.TurnoId=t.TurnoId " +
                   "INNER JOIN Carreras cc on c.CarreraId= cc.CarreraId " +
                   "WHERE c.CursadaId = @cursadaId";
           var cursada= _transaction.Connection.QuerySingle<CursadaDto>(Query, new { CursadaId = cursadaId }, transaction: _transaction);
            return cursada;
        }

        public List<CursadaDto> GetAll()
        {
              List<CursadaDto> lista = new List<CursadaDto>();           
                string Query = "SELECT c.CursadaId,c.FechaInicio,c.FechaFinal,c.CantidadAlumnos,t.TurnoId,t.DescripcionTurno,cc.CarreraId,cc.NombreCarrera FROM Cursadas c " +
                   "INNER JOIN Turnos t ON c.TurnoId=t.TurnoId " +
                   "INNER JOIN Carreras cc on c.CarreraId= cc.CarreraId";
                 lista = _transaction.Connection.Query<CursadaDto>(Query, transaction: _transaction).ToList();
            
            
            return lista;
        }

        public int GetCantidadAlumnos(int cursadaId)
        {
            string selectQuery = @"SELECT COUNT(*) FROM AlumnosCursadas WHERE CursadaId=@CursadaId";
            var cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { CursadaId = cursadaId }, transaction: _transaction);
            return cantidad;
                
        }

        public void Guardar(Cursada cursada)
        {
            string insertQuery = "UPDATE Cursadas " +
                     "SET FechaInicio = @FechaInicio, " +
                     "    FechaFinal = @FechaFinal, " +
                     "    CantidadAlumnos = @CantidadAlumnos, " +
                     "    TurnoId = @TurnoId, " +
                     "    CarreraId = @CarreraId " +
                     "WHERE CursadaId = @CursadaId";
            
            _transaction.Connection.Execute(insertQuery, cursada, transaction: _transaction);
            
        }
    }
}
