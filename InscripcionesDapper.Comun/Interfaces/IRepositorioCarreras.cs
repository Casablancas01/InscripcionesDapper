using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Comun.Interfaces
{
    public interface IRepositorioCarreras
    {
        void Agregar(Carrera carrera);
        void Borrar(int carreraId);
        bool Existe(Carrera carrera);
        List<Carrera> GetAll();
        bool EstaRelacionada(Carrera carrera);
    }
}
