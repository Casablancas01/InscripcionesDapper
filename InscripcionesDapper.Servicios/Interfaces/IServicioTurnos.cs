using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Servicios.Interfaces
{
    public interface IServicioTurnos
    {
        void Guardar(Turno turno);
        void Borrar(int turnod);
        //void Editar(int carreraId);
        bool Existe(Turno turno);
        List<Turno> GetAll();
        bool EstaRelacionada(Turno turno);
    }
}
