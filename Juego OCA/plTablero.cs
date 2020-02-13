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
    public partial class plTablero : Form
    {
        public plTablero()
        {
            InitializeComponent();
        }

        public void prepararTablero()
        {
            for(int ifilas = 0; ifilas < 6; ifilas++)
            {
                for(int icolumnas = 0; icolumnas < 5; icolumnas++)
                {
                    Control objControlPanel = pnlTablero.GetControlFromPosition(icolumnas, ifilas);
                }
            }
        }
        
    }
}
