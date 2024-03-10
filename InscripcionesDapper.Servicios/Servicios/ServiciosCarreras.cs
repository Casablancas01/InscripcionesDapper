using InscripcionesDapper.Datos;
using InscripcionesDapper.Entidades;
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
    public class ServiciosCarreras : IServicioCarrera
    {
        public void Guardar(Carrera carrera)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    // Realiza operaciones en el repositorio
                    if (carrera.CarreraId == 0)
                    {
                        unitOfWork.Carreras.Agregar(carrera);

                    }
                    else
                    {
                        //unitOfWork.Carreras.Editar(carrera);

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

        public void Borrar(int carreraId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Carreras.Borrar(carreraId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();
                    throw;
                }
            }
        }

        public bool Existe(Carrera carrera)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    bool existe = unitOfWork.Carreras.Existe(carrera);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Carrera> GetAll()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    var lista = unitOfWork.Carreras.GetAll();
                    return lista;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool EstaRelacionada(Carrera carrera)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    bool existe = unitOfWork.Carreras.EstaRelacionada(carrera);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
