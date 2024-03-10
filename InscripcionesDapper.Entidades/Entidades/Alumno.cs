using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesDapper.Entidades
{
    public class Alumno
    {
        public int AlumnoId { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroDocumento { get; set; }
        public int Edad { get; set; }
        public int Genero { get; set; }//enum
        public DateTime FechaNacimiento { get; set; }
    }
}
