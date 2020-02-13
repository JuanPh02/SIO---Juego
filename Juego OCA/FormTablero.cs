using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Juego_OCA
{
    public partial class FormTablero : Form
    {
        public FormTablero()
        {
            InitializeComponent();
            cargarCasillas();
            cargarDatosPreguntas();
            lblCasillaJ1.Text = j1.posCasilla.ToString();
            lblCasillaJ2.Text = j2.posCasilla.ToString();
            turno = 1;
            lblTurno.Text = Convert.ToString(turno);
        }

        public List<PictureBox> lstCasillas = new List<PictureBox>();
        public List<string> lstPreguntas = new List<string>();
        public List<string> lstRespuestas = new List<string>();
        public List<int> lstRespCorrectas = new List<int>();

        //SoundPlayer sFicha = new SoundPlayer(Application.StartupPath + @"\Sonido\ficha.wav");
        Random rnd = new Random();
        int turno;
 
        Jugador j1 = new Jugador(1, 1);
        Jugador j2 = new Jugador(1, 2);

        manejarBD bd = new manejarBD();

        public void cargarCasillas()
        {
            lstCasillas.Add(pb1);
            lstCasillas.Add(pb2);
            lstCasillas.Add(pb3);
            lstCasillas.Add(pb4);
            lstCasillas.Add(pb5);
            lstCasillas.Add(pb6);
            lstCasillas.Add(pbOca7);
            lstCasillas.Add(pb8);
            lstCasillas.Add(pb9);
            lstCasillas.Add(pb10);
            lstCasillas.Add(pb11);
            lstCasillas.Add(pbOca12);
            lstCasillas.Add(pb13);
            lstCasillas.Add(pb14);
            lstCasillas.Add(pb15);
            lstCasillas.Add(pb16);
            lstCasillas.Add(pb17);
            lstCasillas.Add(pb18);
            lstCasillas.Add(pbOca19);
            lstCasillas.Add(pb20);
            lstCasillas.Add(pb21);
            lstCasillas.Add(pb22);
            lstCasillas.Add(pb23);
            lstCasillas.Add(pbOca24);
            lstCasillas.Add(pb25);
            lstCasillas.Add(pb26);
            lstCasillas.Add(pb27);
            lstCasillas.Add(pb28);
            lstCasillas.Add(pb29);
            lstCasillas.Add(pb30);
        }

        public void cargarDatosPreguntas()
        {
            lstPreguntas = bd.cargarPreguntas();
            lstRespuestas = bd.cargarRespuestas();
            lstRespCorrectas = bd.cargarRespCorrectas();
        }

        private void txtPregunta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtRespuesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnLanzarDado_Click(object sender, EventArgs e)
        {
            int resultadoDado = rnd.Next(1, 7);
            MessageBox.Show("¡Sacaste " + resultadoDado + " en el tiro del dado!", "¡Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblDado.Text = Convert.ToString(resultadoDado);
            moverFicha(resultadoDado);
        }

        private void moverFicha( int resultadoDado)
        {
            if (turno == 1)
            {
                j1.posCasilla = j1.posCasilla + resultadoDado;
                pbFicha1.Location = lstCasillas[j1.posCasilla-1].Location;
                pbFicha1.Location = new Point(pbFicha1.Location.X + 10, pbFicha1.Location.Y + 30);
                turno++;
            } else 
            {
                j2.posCasilla = j2.posCasilla + resultadoDado;
                pbFicha2.Location = lstCasillas[j2.posCasilla-1].Location;
                pbFicha2.Location = new Point(pbFicha2.Location.X + 60, pbFicha2.Location.Y+30);
                turno--;
            }
            lblTurno.Text = turno.ToString();
            lblCasillaJ1.Text = j1.posCasilla.ToString();
            lblCasillaJ2.Text = j2.posCasilla.ToString();
            mostrarPregunta();
            //sFicha.Play();
        }

        private void mostrarPregunta()
        {
            int numAzar = rnd.Next(1, 50);
            txtPregunta.Text = lstPreguntas[numAzar] + lstRespuestas[numAzar];
            
        }

       
    }
}
