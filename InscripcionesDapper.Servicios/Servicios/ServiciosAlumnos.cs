
using InscripcionesDapper.Datos;
using InscripcionesDapper.Entidades;
using InscripcionesDapper.Servicios.Interfaces;
using InscripcionesDapper.Comun;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace InscripcionesDapper.Servicios.Servicios
{
    public class ServiciosAlumnos:IServiciosAlumnos
    {
        public ServiciosAlumnos()
        {

        }
        public void Borrar(int alumonoId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Alumnos.Borrar(alumonoId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();
                    throw;
                }
            }
        }
        public List<Alumno> GetAll()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    var lista = unitOfWork.Alumnos.GetAll();
                    return lista;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public bool Existe(Alumno alumno)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    bool existe = unitOfWork.Alumnos.Existe(alumno);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Guardar(Alumno alumno)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    // Realiza operaciones en el repositorio
                    if (alumno.AlumnoId == 0)
                    {
                        unitOfWork.Alumnos.Agregar(alumno);

                    }
                    else
                    {
                        unitOfWork.Alumnos.Editar(alumno);

                    }

                    // Más operaciones si es necesario

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // Maneja errores y posiblemente realiza un rollback
                    unitOfWork.Rollback();
                    unitOfWork.Dispose();
                    // Log o manejo de errores
                }
            }
        }

        public bool EstaRelacionada(Alumno alumno)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Alumnos.EstaRelacionada(alumno);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


    }
}
