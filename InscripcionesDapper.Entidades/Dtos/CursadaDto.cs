using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Entidades.Dtos
{
    public class CursadaDto
    {
        public int CursadaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int CantidadAlumnos { get; set; }
        public string DescripcionTurno { get; set; }
        public string NombreCarrera { get; set; }
    }
}
