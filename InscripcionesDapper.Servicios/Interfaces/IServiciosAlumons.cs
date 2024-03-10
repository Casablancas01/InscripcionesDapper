using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Servicios.Interfaces
{
    public interface IServiciosAlumnos
    {
        void Guardar(Alumno alumno);
        void Borrar(int alumnoId);
        bool Existe(Alumno alumno);
        List<Alumno> GetAll();
        bool EstaRelacionada(Alumno alumno);
        //int GetCantidad(string textoFiltro);
        //List<Alumno> GetPaises(string textoFiltro);
        //Alumno GetPaisPorId(int alumnoId);
    }
}
