using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Dtos;
using InscripcionesDapper.Entidades.Entidades;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InscripcionesDapper.Windows
{
    public partial class frmCursadaAE : Form
    {
        private readonly ServiciosCursadas _servicioCursadas;
        private Cursada cursada;
        private CursadaDto cursadaDto;
        private bool esEdicion = false;
        public frmCursadaAE(ServiciosCursadas servicioCursadas)
        {
            InitializeComponent();
            _servicioCursadas = servicioCursadas;
        }
        public CursadaDto GetProveedor()
        {
            return cursadaDto;
        }
        public void SetCursada(CursadaDto cursadaDto)
        {
            this.cursadaDto = cursadaDto;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (ValidarDatos())
            {
                if (cursada == null)
                {
                    cursada = new Cursada();

                }
                cursada.FechaInicio = dateTimePicker1.Value.Date;
                cursada.FechaFinal= dateTimePicker2.Value.Date;
                cursada.CantidadAlumnos = Convert.ToInt32(textBox1.Text);
                cursada.TurnoId = (int)comboBox1.SelectedValue;
                cursada.CarreraId  = (int)comboBox2.SelectedValue;

                try
                {

                    if (!_servicioCursadas.Existe(cursada))
                    {
                        

                        if (!esEdicion)
                        {
                            _servicioCursadas.Agregar(cursada);
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
                            cursada = null;
                            InicializarControles();

                        }
                        else
                        {
                            cursada.CursadaId = cursadaDto.CursadaId;
                            _servicioCursadas.Guardar(cursada);
                            MessageBox.Show("Registro editado", "Mensaje",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Registro duplicado",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cursada = null;
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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }

        private bool ValidarDatos()
        {
            string cantidadMaximaAlumnos = @"^(3[0]|[12]?[0-9]|[1-9])$";
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(textBox1.Text )|| !Regex.IsMatch(textBox1.Text, cantidadMaximaAlumnos))
            {
                valido = false;
                errorProvider1.SetError(textBox1, "Los cursos no puede pasar los 30 alumnos");
            }
            if (comboBox1.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboBox1, "Debe seleccionar un Turno");
            }
            if (comboBox2.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboBox1, "Debe seleccionar una Carrera");
            }
            return valido;
        }

        protected override void OnLoad(EventArgs e)
        {
            CombosHelper.CargarComboTurnos(ref comboBox1);
            CombosHelper.CargarComboCarreras(ref comboBox2);
            base.OnLoad(e);
            if (cursadaDto != null)
            {
                esEdicion = true;
                dateTimePicker1.Value = cursadaDto.FechaInicio;
                dateTimePicker2.Value = cursadaDto.FechaFinal;
                textBox1.Text = Convert.ToString(cursadaDto.CantidadAlumnos);
                comboBox1.SelectedValue = cursadaDto.TurnoId;
                comboBox2.SelectedValue = cursadaDto.CarreraId;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
