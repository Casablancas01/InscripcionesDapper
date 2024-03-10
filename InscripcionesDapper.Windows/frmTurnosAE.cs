using InscripcionesDapper.Entidades.Entidades;
using InscripcionesDapper.Servicios.Interfaces;
using InscripcionesDapper.Servicios.Servicios;
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
    public partial class frmTurnosAE : Form
    {
        private IServicioTurnos _servicioTurnos;
        public frmTurnosAE(IServicioTurnos servicioTurnos)
        {
            InitializeComponent();
            _servicioTurnos = servicioTurnos;
        }

        private Turno turno;
        private bool esEdicion = false;
        private void button1_Click(object sender, EventArgs e)
        {

            if (ValidarDatos())
            {
                if (turno == null)
                {
                    turno = new Turno();

                }
                turno.DescripcionTurno = textBox1.Text;
                try
                {

                    if (!_servicioTurnos.Existe(turno))
                    {
                        _servicioTurnos.Guardar(turno);

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
                            turno = null;
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
                        turno = null;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        private bool ValidarDatos()
        {

            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                valido = false;
                errorProvider1.SetError(textBox1, "La descripcion es requerida");
            }
            return valido;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void InicializarControles()
        {
            textBox1.Clear();
        }
    }
}
