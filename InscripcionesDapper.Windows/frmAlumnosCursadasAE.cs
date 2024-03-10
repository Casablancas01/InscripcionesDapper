using InscripcionesDapper.Entidades;
using InscripcionesDapper.Servicios.Interfaces;
using InscripcionesDapper.Servicios.Servicios;
using InscripcionesDapper.Windows.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InscripcionesDapper.Windows
{
    public partial class frmAlumnosCursadasAE : Form
    {
        private readonly IServicioAlumnoCursadas _servicioAlumnosCursadas;
        private readonly IServicioCursadas _serviciosCursadas;

        public frmAlumnosCursadasAE(int cursadaId)
        {
            InitializeComponent();
           this.cursadaId = cursadaId;
            _servicioAlumnosCursadas = new ServicioAlumnosCursadas();
            _serviciosCursadas = new ServiciosCursadas();

        }
        int cursadaId;
        private List<Alumno> lista;
        private Alumno alumn;
        private void frmAlumnosCursadasAE_Load(object sender, EventArgs e)
        {
            CargarLabels();

            MostrarDatosEnGrilla();
        }
        private void CargarLabels()
        {
            var cursadaDto = _serviciosCursadas.Get(cursadaId);
            var cantidad = _serviciosCursadas.GetCantidadAlumnos(cursadaId);
            label1.Text = $@"Carrera:{cursadaDto.NombreCarrera}";
            label2.Text = $@"Cantidad de Alumnos:{cantidad}";

        }
        private void MostrarDatosEnGrilla()
        {
            lista = _servicioAlumnosCursadas.GetAlumnos(cursadaId);
            GridHelper.LimpiarGrilla(dataGridView1);
            foreach (var cliente in lista)
            {
                var r = GridHelper.ConstruirFila(dataGridView1);
                GridHelper.SetearFila(r, cliente);
                GridHelper.AgregarFila(dataGridView1, r);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAlumnos frm = new frmAlumnos(true) { Text = "Seleccionar Alumno" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            alumn = frm.GetAlumno();
            if (!_servicioAlumnosCursadas.Existe(alumn.AlumnoId,cursadaId))
            {
                _servicioAlumnosCursadas.Agregar(alumn.AlumnoId, cursadaId);
                MostrarDatosEnGrilla();
                CargarLabels();
            }
            else
            {
                MessageBox.Show("El alumno ya se encuentra en la cursada",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dataGridView1.SelectedRows[0];
            Alumno alumno = (Alumno)r.Tag;
            try
            {
                     _servicioAlumnosCursadas.Quitar(alumno.AlumnoId, cursadaId);
                    GridHelper.QuitarFila(dataGridView1, r);
                CargarLabels();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
