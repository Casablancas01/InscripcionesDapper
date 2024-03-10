using InscripcionesDapper.Entidades;
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
    public partial class frmCarreras : Form
    {
        private readonly ServiciosCarreras _servicioCarrera;
        public frmCarreras()
        {
            InitializeComponent();
            _servicioCarrera = new ServiciosCarreras();
        }
        private List<Carrera> lista;
        bool filtroOn = false;
        string textoFiltro = null;
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmCarreraAe frm = new frmCarreraAe(_servicioCarrera) { Text = "Agregar Alumno" };
            DialogResult dr = frm.ShowDialog(this);
            MostrarDatosEnGrilla();
        }

        private void frmCarreras_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
        }
        private void MostrarDatosEnGrilla()
        {
            lista = _servicioCarrera.GetAll();
            GridHelper.LimpiarGrilla(dataGridView1);
            foreach (var cliente in lista)
            {
                var r = GridHelper.ConstruirFila(dataGridView1);
                GridHelper.SetearFila(r, cliente);
                GridHelper.AgregarFila(dataGridView1, r);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dataGridView1.SelectedRows[0];
            Carrera carrera = (Carrera)r.Tag;
            try
            {
                if (!_servicioCarrera.EstaRelacionada(carrera))
                {

                
                //TODO: Se debe controlar que no este relacionado
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicioCarrera.Borrar(carrera.CarreraId);
                GridHelper.QuitarFila(dataGridView1, r);

                //lblCantidad.Text = _servicio.GetCantidad().ToString();
                MessageBox.Show("Registro borrado", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("REGISTRO RELACIONADO", "Mensaje",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count == 0)
            //{
            //    return;
            //}
            //var r = dataGridView1.SelectedRows[0];
            //Carrera carrera = (Carrera)r.Tag;
            //frmDetalleCursada frm = new frmDetalleCursada(carrera) { Text = "Agregar Alumno" };
            //DialogResult dr = frm.ShowDialog(this);
            //MostrarDatosEnGrilla();
        }
    }
}
