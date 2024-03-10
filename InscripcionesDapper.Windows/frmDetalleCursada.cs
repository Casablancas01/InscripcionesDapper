using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InscripcionesDapper.Windows
{
    public partial class frmDetalleCursada : Form
    {
        
        private List<CursadaDto> lista;
        private readonly ServiciosCursadas _servicioCursadas;
        public frmDetalleCursada()
        {
            InitializeComponent();
            
            _servicioCursadas= new ServiciosCursadas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCursadaAE frm = new frmCursadaAE(_servicioCursadas) { Text = "Agregar Alumno" };
            DialogResult dr = frm.ShowDialog(this);
            MostrarDatosEnGrilla();
        }

        private void frmDetalleCursada_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
        }
        private void MostrarDatosEnGrilla()
        {
            lista = _servicioCursadas.GetAll();
            GridHelper.LimpiarGrilla(dataGridView1);
            foreach (var cliente in lista)
            {
                var r = GridHelper.ConstruirFila(dataGridView1);
                GridHelper.SetearFila(r, cliente);
                GridHelper.AgregarFila(dataGridView1, r);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dataGridView1.SelectedRows[0];
            CursadaDto cursada = (CursadaDto)r.Tag;
            frmAlumnosCursadasAE frm = new frmAlumnosCursadasAE(cursada.CursadaId) { Text = "Agregar Alumno" };
            DialogResult dr = frm.ShowDialog(this);
            MostrarDatosEnGrilla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dataGridView1.SelectedRows[0];
            CursadaDto cursada = (CursadaDto)r.Tag;
            try
            {
               
                    //TODO: Se debe controlar que no este relacionado
                    DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No) { return; }
                    _servicioCursadas.Borrar(cursada.CursadaId);
                    GridHelper.QuitarFila(dataGridView1, r);

                    //lblCantidad.Text = _servicio.GetCantidad().ToString();
                    MessageBox.Show("Registro borrado", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
