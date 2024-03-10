using InscripcionesDapper.Entidades;
using InscripcionesDapper.Entidades.Entidades;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InscripcionesDapper.Windows
{
    public partial class frmCarreraAe : Form
    {
        private IServicioCarrera _servicioesCarrera;
        public frmCarreraAe(IServicioCarrera servicioesCarrera)
        {
            InitializeComponent();
            _servicioesCarrera = servicioesCarrera;
        }



        private Carrera carrera;
        private bool esEdicion = false;
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (carrera == null)
                {
                    carrera = new Carrera();

                }
                carrera.NombreCarrera = textBox1.Text;

                try
                {

                    if (!_servicioesCarrera.Existe(carrera))
                    {
                        _servicioesCarrera.Guardar(carrera);

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
                            carrera = null;
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
                        carrera = null;
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
        }

        private bool ValidarDatos()
        {

            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                valido = false;
                errorProvider1.SetError(textBox1, "EL nombre es requerido");
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
