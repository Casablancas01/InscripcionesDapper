using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Comun.Interfaces
{
    public interface IRepositorioTurnos
    {
        void Agregar(Turno turno);
        void Borrar(int turnoId);
        bool Existe(Turno turno);
        List<Turno> GetAll();
        bool EstaRelacionada(Turno turno);
    }
}
