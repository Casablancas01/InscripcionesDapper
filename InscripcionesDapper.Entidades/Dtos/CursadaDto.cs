using InscripcionesDapper.Entidades.Entidades;
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
        public int TurnoId { get; set; }
        public string DescripcionTurno { get; set; }
        public int CarreraId { get; set; }
        public string NombreCarrera { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Cursada ToEntity(Cursada cursada)
        {
            Cursada curs = new Cursada
            {
                CursadaId = cursada.CursadaId,
                FechaInicio = cursada.FechaInicio,
                FechaFinal = cursada.FechaFinal,
                CantidadAlumnos = cursada.CantidadAlumnos,
                TurnoId = cursada.TurnoId,
                CarreraId = cursada.CarreraId
            };


            return curs;
        }
    }
}
