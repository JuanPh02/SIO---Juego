using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_OCA
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        FormTablero fTablero;

        private void btnJugar_Click(object sender, EventArgs e)
        {
            
            if (fTablero == null)
            {
                fTablero = new FormTablero();
                fTablero.MdiParent = this.MdiParent;
                //fTablero.prepararTablero();
                fTablero.Show();
            }
            else
            {
                fTablero.Activate();
            }
        }

        private void btnReglas_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
    }
}
