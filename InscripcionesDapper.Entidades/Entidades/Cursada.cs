using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Entidades.Entidades
{
    public class Cursada
    {
        public int CursadaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int CantidadAlumnos { get; set; }
        public int TurnoId { get; set; }
        public int CarreraId { get; set; }
    }
}
