using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Servicios.Interfaces
{
    public interface IServicioCursadas
    {
        List<CursadaDto> GetAll();
        void Borrar(int cursadaId);
        void Guardar(Cursada cursada);
        bool Existe(Cursada cursada);
        CursadaDto Get(int cursadaId);
        int GetCantidadAlumnos(int cursadaId);
    }
}
