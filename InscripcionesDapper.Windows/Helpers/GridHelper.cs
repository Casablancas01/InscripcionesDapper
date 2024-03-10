using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
using InscripcionesDapper.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InscripcionesDapper.Windows.Helpers
{
    public static class GridHelper
    {
        public static void LimpiarGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }
        public static DataGridViewRow ConstruirFila(DataGridView dgv)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgv);
            return r;

        }
        public static void SetearFila(DataGridViewRow r, object obj)
        {
            switch (obj)
            {
                case Alumno alumno:
                    r.Cells[0].Value = alumno.NombreCompleto;
                    r.Cells[1].Value = alumno.NumeroDocumento;
                    r.Cells[2].Value = alumno.Edad;
                    r.Cells[3].Value = (Generos)alumno.Genero;
                    r.Cells[4].Value = alumno.FechaNacimiento.ToShortDateString();
                    break;
                case Carrera carrera:
                    r.Cells[0].Value = carrera.NombreCarrera;
                    break;
                case Turno turno:
                    r.Cells[0].Value = turno.DescripcionTurno;
                    break;
                case CursadaDto cursada:
                    r.Cells[0].Value = cursada.FechaInicio;
                    r.Cells[1].Value = cursada.FechaFinal;
                    r.Cells[2].Value = cursada.CantidadAlumnos;
                    r.Cells[3].Value = cursada.DescripcionTurno;
                    r.Cells[4].Value = cursada.NombreCarrera;
                    r.Cells[5].Value = cursada.CursadaId;
                    break;

            }
            r.Tag = obj;

        }
        public static void AgregarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Add(r);
        }

        public static void QuitarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Remove(r);
        }

        public static void MostrarDatosEnGrilla<T>(DataGridView dgv, List<T> lista) where T : class
        {
            GridHelper.LimpiarGrilla(dgv);
            foreach (var obj in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgv);
                GridHelper.SetearFila(r, obj);
                GridHelper.AgregarFila(dgv, r);
            }

        }

    }
}
