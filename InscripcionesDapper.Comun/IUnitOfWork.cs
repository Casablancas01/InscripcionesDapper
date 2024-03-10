using InscripcionesDapper.Comun.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Comun
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IRepositorioAlumnos Alumnos { get; }
        IRepositorioCarreras Carreras { get; }
        IRepositorioTurnos Turnos { get; }
        IRepositorioCursadas Cursadas { get; }

    }
}
