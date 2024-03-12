using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Comun.Interfaces
{
    public interface IRepositorioCursadas
    {
        List<CursadaDto> GetAll();
        CursadaDto Get(int cursadaId);
        void Borrar(int cursadaId);
        void Agregar(Cursada cursada);
        void Guardar(Cursada cursada);
        bool Existe(Cursada cursada);
        bool EstaRelacionada(Cursada cursada);
        int GetCantidadAlumnos(int cursadaId);
       

    }
}
