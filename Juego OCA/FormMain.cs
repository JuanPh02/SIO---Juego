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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        FormInicio fInicio = new FormInicio();
        
        private void FormMain_Load(object sender, EventArgs e)
        {
            fInicio.MdiParent = this;
            fInicio.Show();
        }
    }
}
