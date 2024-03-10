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
    public partial class frmNavegacion : Form
    {
        public frmNavegacion()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)//salir
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmTurnos frm = new frmTurnos();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAlumnos frm = new frmAlumnos(false);
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCarreras frm = new frmCarreras();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmDetalleCursada frm = new frmDetalleCursada();
            frm.ShowDialog();
           
        }
    }
}
