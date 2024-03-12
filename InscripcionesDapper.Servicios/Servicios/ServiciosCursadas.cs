using InscripcionesDapper.Datos;
using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
using InscripcionesDapper.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Servicios.Servicios
{
    public class ServiciosCursadas : IServicioCursadas
    {
        public void Guardar(Cursada cursada)
        {

            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    unitOfWork.Cursadas.Guardar(cursada);
                   
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
        public void Agregar(Cursada cursada)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    unitOfWork.Cursadas.Agregar(cursada);

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

        public void Borrar(int cursadaId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Cursadas.Borrar(cursadaId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();

                    throw;
                }
            }
        }

        public List<CursadaDto> GetAll()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Cursadas.GetAll();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Existe(Cursada cursada)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    bool existe = unitOfWork.Cursadas.Existe(cursada);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public CursadaDto Get(int cursadaId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Cursadas.Get(cursadaId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int GetCantidadAlumnos(int cursadaId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    return unitOfWork.Cursadas.GetCantidadAlumnos(cursadaId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        
    }
}
