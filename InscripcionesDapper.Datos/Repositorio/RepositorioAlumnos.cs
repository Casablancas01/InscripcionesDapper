using InscripcionesDapper.Comun.Interfaces;
using InscripcionesDapper.Entidades;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Collections.Generic;
using System;
using System.Linq;

namespace InscripcionesDapper.Datos.Repositorio
{
    public class RepositorioAlumnos : IRepositorioAlumnos
    {
        private readonly IDbTransaction _transaction;
        public RepositorioAlumnos(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Agregar(Alumno alumno)
        {
            string insertQuery = "INSERT INTO Alumnos (NombreCompleto,NumeroDocumento,Edad,Genero,FechaNacimiento) " +
                "VALUES(@NombreCompleto,@NumeroDocumento,@Edad,@Genero,@FechaNacimiento); SELECT SCOPE_IDENTITY()";
            int id = _transaction.Connection.QuerySingle<int>(insertQuery, alumno, transaction: _transaction);
            alumno.AlumnoId = id;

        }

        public void Borrar(int alumnoId)
        {

            string deleteQuery = "DELETE FROM Alumnos WHERE AlumnoId=@AlumnoId";
            _transaction.Connection.Execute(deleteQuery, new { AlumnoId = alumnoId }, transaction: _transaction);

        }

        public void Editar(Alumno alumno)
        {

            string updateQuery = "UPDATE Alumnos SET NombreCompleto=@NombreCompleto WHERE AlumnoId=@AlumnoId";
            _transaction.Connection.Execute(updateQuery, alumno, transaction: _transaction);
        }

        public bool EstaRelacionada(Alumno alumno)
        {
            int cantidad = 0;
            string selectQuery = @"SELECT COUNT(*) FROM Alumnos WHERE AlumnoId=@alumnoId";
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, new { alumnoId = alumno.AlumnoId }, transaction: _transaction);
            return cantidad > 0;
        }

        public bool Existe(Alumno alumno)
        {

            var cantidad = 0;
            string selectQuery;
            if (alumno.AlumnoId == 0)
            {
                selectQuery = "SELECT COUNT(*) FROM Alumnos WHERE NumeroDocumento=@NumeroDocumento";

            }
            else
            {
                selectQuery = "SELECT COUNT(*) FROM Categorias WHERE NumeroDocumento=@NumeroDocumento AND AlumnoId<>@AlumnoId";

            }
            cantidad = _transaction.Connection.ExecuteScalar<int>(selectQuery, alumno, transaction: _transaction);
            return cantidad > 0;
        }

        public List<Alumno> GetAll()
        {
        List<Alumno> lista = new List<Alumno>();
        string selectQuery = "SELECT*FROM Alumnos ORDER BY NumeroDocumento";
        lista = _transaction.Connection.Query<Alumno>(selectQuery, transaction: _transaction).ToList();
            return lista;
        }
        
    }
}
