using InscripcionesDapper.Entidades;
using InscripcionesDapper.Servicios.Interfaces;
using InscripcionesDapper.Servicios.Servicios;
using InscripcionesDapper.Windows.Helpers;
using System;
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
    public partial class frmAlumnos : Form
    {
        public frmAlumnos(bool mostrarBoton)
        {
            InitializeComponent();
            _servicioesAlumnos = new ServiciosAlumnos();
            this.mostrarBoton = mostrarBoton;

        }

        private bool mostrarBoton;
       
        private readonly ServiciosAlumnos _servicioesAlumnos;
        private List<Alumno> lista;
        private Alumno alumn;
        //Para paginación
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;
        List<Alumno> alumno;
        bool filtroOn = false;
        string textoFiltro = null;
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlumnoAE frm = new frmAlumnoAE(_servicioesAlumnos) { Text = "Agregar Alumno" };
            DialogResult dr = frm.ShowDialog(this);
            MostrarDatosEnGrilla();
        }

        private void frmAlumnos_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
            MostrarOcultarBotones();
        }
        void MostrarOcultarBotones()
        {
            if (mostrarBoton)
            {
                button1.Visible = true;
                btnAgregar.Visible = false;
                btnBorrar.Visible = false;
            }
            else
            {
                button1.Visible = false;
                btnAgregar.Visible = true;
                btnBorrar.Visible = true;
            }
           
        }
        private void MostrarDatosEnGrilla()
        {
            lista = _servicioesAlumnos.GetAll();
            GridHelper.LimpiarGrilla(dataGridView1);
            foreach (var cliente in lista)
            {
                var r = GridHelper.ConstruirFila(dataGridView1);
                GridHelper.SetearFila(r, cliente);
                GridHelper.AgregarFila(dataGridView1, r);
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dataGridView1.SelectedRows[0];
            Alumno alumno = (Alumno)r.Tag;
            try
            {
                if (!_servicioesAlumnos.EstaRelacionada(alumno))
                {
                    //TODO: Se debe controlar que no este relacionado
                    DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No) { return; }
                    _servicioesAlumnos.Borrar(alumno.AlumnoId);
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
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }
        public Alumno GetAlumno() => alumn;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
                if (e.RowIndex < 0)
                {
                    return;
                }
                var r = dataGridView1.Rows[e.RowIndex];
                alumn = (Alumno)r.Tag;
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            if (alumn is null)
            {
                valido = false;
                MessageBox.Show("Debe seleccionar un Alumno", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return valido;
        }
    }
}
