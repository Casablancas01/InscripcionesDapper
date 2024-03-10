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
    public class RepositorioAlumnosCursadas : IRepositorioAlumnosCursadas
    {
        private readonly IDbTransaction _transaction;
        public RepositorioAlumnosCursadas(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Agregar(int alumnoId, int cursadaId)
        {    
            string insertQuery = @"INSERT INTO AlumnosCursadas(AlumnoId, CursadaId)
                VALUES(@AlumnoId, @CursadaId)";

            _transaction.Connection.Execute(insertQuery, new { AlumnoId = alumnoId, CursadaId = cursadaId }, transaction: _transaction);
        }

        public bool Existe(int alumnoId, int cursadaId)
        {
            int cantidad;
            string queryBool = @"SELECT*FROM AlumnosCursadas WHERE AlumnoId=@AlumnoId and CursadaId=@CursadaId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(queryBool, new { AlumnoId = alumnoId, CursadaId = cursadaId }, transaction: _transaction);
            return cantidad > 0;
        }

        public List<Alumno> GetAlumnos(int cursadaId)
        {
            List<Alumno> lista = new List<Alumno>();
            string selectQuery = @"SELECT a.AlumnoId,a.NombreCompleto,a.NumeroDocumento, a.Edad, a.Genero, a.FechaNacimiento 
                        FROM AlumnosCursadas ac INNER JOIN Alumnos a
                        ON a.AlumnoId=ac.AlumnoId
                        WHERE ac.CursadaId=@cursadaId";

            lista = _transaction.Connection
               .Query<Alumno>(selectQuery,
                   new { cursadaId },
               transaction: _transaction).ToList();
            return lista;
        }

        public void Quitar(int alumnoId, int cursadaId)
        {
            string insertQuery = @"DELETE FROM AlumnosCursadas WHERE AlumnoId=@AlumnoId AND CursadaId=@CursadaId";
            var parametros = new { AlumnoId = alumnoId, CursadaId = cursadaId };

            _transaction.Connection.Execute(insertQuery, parametros, transaction: _transaction);
        }
    }
}
