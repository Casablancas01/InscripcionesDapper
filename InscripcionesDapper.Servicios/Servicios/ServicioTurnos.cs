using InscripcionesDapper.Datos;
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
    public class ServicioTurnos : IServicioTurnos
    {
        public void Guardar(Turno turno)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    // Realiza operaciones en el repositorio
                    if (turno.TurnoId == 0)
                    {
                        unitOfWork.Turnos.Agregar(turno);

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

        public void Borrar(int turnoId)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {

                try
                {
                    unitOfWork.Turnos.Borrar(turnoId);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork?.Rollback();
                    throw;
                }
            }
        }

        public bool Existe(Turno turno)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    bool existe = unitOfWork.Turnos.Existe(turno);
                    return existe;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Turno> GetAll()
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    var lista = unitOfWork.Turnos.GetAll();
                    return lista;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool EstaRelacionada(Turno turno)
        {
            using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["MiConexion"].ToString()))
            {
                try
                {
                    bool existe = unitOfWork.Turnos.EstaRelacionada(turno);
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
