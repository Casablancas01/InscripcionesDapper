using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Enum;
using InscripcionesDapper.Servicios.Interfaces;
using InscripcionesDapper.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InscripcionesDapper.Windows
{
    public partial class frmAlumnoAE : Form
    {
        private IServiciosAlumnos _servicioesAlumnos;
        public frmAlumnoAE(IServiciosAlumnos servicioesAlumnos)
        {
            InitializeComponent();
            _servicioesAlumnos = servicioesAlumnos;
        }

        private Alumno alumno;
        private bool esEdicion = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboGeneros(ref comboBox1);
            if (alumno != null)
            {
                esEdicion = true;
                txtDocumento.Text = alumno.NumeroDocumento;
            }
        }
        private void frmAlumnoAE_Load(object sender, EventArgs e)
        {

        }
        public Alumno GetAlumno()
        {
            return alumno;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
                DialogResult = DialogResult.Cancel;
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (alumno == null)
                {
                    alumno = new Alumno();

                }
                alumno.NombreCompleto = textBox1.Text;
                alumno.NumeroDocumento=txtDocumento.Text;
                alumno.Edad=Convert.ToInt32(  textBox3.Text);
                var valorSeleccionado = CombosHelper.ObtenerValorSeleccionado(comboBox1);
                alumno.Genero =valorSeleccionado;
                alumno.FechaNacimiento = dateTimePicker1.Value.Date;

                try
                {

                    if (!_servicioesAlumnos.Existe(alumno))
                    {
                        _servicioesAlumnos.Guardar(alumno);

                        if (!esEdicion)
                        {
                            MessageBox.Show("Registro agregado",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult dr = MessageBox.Show("¿Desea agregar otro registro?",
                                "Pregunta",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.No)
                            {
                                DialogResult = DialogResult.OK;

                            }
                            alumno = null;
                            InicializarControles();

                        }
                        else
                        {
                            MessageBox.Show("Registro editado", "Mensaje",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Registro duplicado",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alumno = null;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void InicializarControles()
        {
            textBox1.Clear();
            txtDocumento.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = 0;
        }

        private bool ValidarDatos()
        {
            string patron = @"^(0?[1-9]|[1-9][0-9])$";
            string patronDocumento = @"^(111111|[1-9][0-9]{5,7}|[1-8][0-9]{6}|9[0-8][0-9]{5}|99999999)$";
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                valido = false;
                errorProvider1.SetError(textBox1, "Los nombres son requeridos");
            }
            if (string.IsNullOrEmpty(txtDocumento.Text) || !Regex.IsMatch(txtDocumento.Text, patronDocumento))
            {
                valido = false;
                errorProvider1.SetError(txtDocumento, "El Documento es requerido");
            }
            if (string.IsNullOrEmpty(textBox3.Text) || !Regex.IsMatch(textBox3.Text, patron))
            {
                valido = false;    
                errorProvider1.SetError(textBox3, "La Edad es requerida");
            }
            if (comboBox1.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboBox1, "Debe seleccionar un Genero");
            }
            return valido;
        }

        public void SetPais(Alumno alumno)
        {
            this.alumno = alumno;
        }
    }
}
