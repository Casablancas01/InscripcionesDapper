using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Servicios.Interfaces
{
    public interface IServicioCarrera
    {
        void Guardar(Carrera carrera);
        void Borrar(int carreraId);
        //void Editar(int carreraId);
        bool Existe(Carrera carrera);
        List<Carrera> GetAll();
        bool EstaRelacionada(Carrera carrera);
    }
}
