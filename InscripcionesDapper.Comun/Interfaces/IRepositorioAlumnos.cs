using InscripcionesDapper.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Comun.Interfaces
{
    public interface IRepositorioAlumnos
    {
        void Agregar(Alumno alumno);
        void Borrar(int alumnoId);
        void Editar(Alumno alumno);
        bool Existe(Alumno alumno);
        List<Alumno> GetAll();
        bool EstaRelacionada(Alumno alumno);
        //int GetCantidad(string textoFiltro = null);


    }
}
