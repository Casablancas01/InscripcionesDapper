using InscripcionesDapper.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Comun.Interfaces
{
    public interface IRepositorioAlumnosCursadas
    {
        void Agregar(int alumnoId,int carreraId);
        void Quitar(int alumnoId, int carreraId);
        List<Alumno> GetAlumnos(int carreraId);
        bool Existe(int alumnoId, int cursadaId);

    }
}
