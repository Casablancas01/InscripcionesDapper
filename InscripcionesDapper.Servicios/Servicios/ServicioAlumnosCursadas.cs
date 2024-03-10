using InscripcionesDapper.Datos;
using InscripcionesDapper.Entidades;
using InscripcionesDapper.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Servicios.Servicios
{
    public class ServicioAlumnosCursadas : IServicioAlumnoCursadas
    {
        public void Agregar(int alumnoId, int carreraId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.AlumnosCursadas.Agregar(alumnoId, carreraId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();

                    throw;
                }
            }
        }

        public bool Existe(int alumnoId, int cursadaId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    bool existe = unitOfWork.AlumnosCursadas.Existe(alumnoId,cursadaId);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Alumno> GetAlumnos(int cursadaId)
        {
            var lista= new List<Alumno>();
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    lista = unitOfWork.AlumnosCursadas.GetAlumnos(cursadaId);
                    return lista;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public void Quitar(int alumnoId, int carreraId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.AlumnosCursadas.Quitar(alumnoId, carreraId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();

                    throw;
                }
            }
        }
    }
}
