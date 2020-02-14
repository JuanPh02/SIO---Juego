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
            cargarLstPregUtilizadas();
            lblCasillaJ1.Text = j1.posCasilla.ToString();
            lblCasillaJ2.Text = j2.posCasilla.ToString();
            turno = 1;
            lblTurno.Text = Convert.ToString(turno);
        }

        public List<PictureBox> lstCasillas = new List<PictureBox>();
        public List<string> lstPreguntas = new List<string>();
        public List<string> lstRespuestas = new List<string>();
        public List<int> lstRespCorrectas = new List<int>();
        public List<bool> lstPreguntasUtilizadas = new List<bool>();

        //SoundPlayer sFicha = new SoundPlayer(Application.StartupPath + @"\Sonido\ficha.wav");
        Random rnd = new Random((int)DateTime.Now.Ticks);
        public int turno;
        public int turnoCastigo;
        int contCastigoPosada = 0;
        int respCorrecta;
        int indicePregMostrada;

        Castigo c = null;

        public bool hayCastigo;
        public bool esCastigoPuente = false;
        public bool esCastigoPosada = false;
        public bool esCastigoDado = false;
        public bool esCastigoResbalon = false;
        public bool esCastigoCalavera = false;

        bool puenteUtilizado;
        bool posadaUtilizado;
        bool dadoUtilizado;
        bool resbalonUtilizado;
        bool calaveraUtilizado;

        Jugador j1 = new Jugador(1, 1);
        Jugador j2 = new Jugador(1, 2);

        manejarBD bd = new manejarBD();

        private void cargarLstPregUtilizadas()
        {
            for (int i = 0; i < lstPreguntas.Count; i++)
            {
                lstPreguntasUtilizadas.Add(false);
            }
        }

        private void cargarCasillas()
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

        public void btnLanzarDado_Click(object sender, EventArgs e)
        {
            int resultadoDado = rnd.Next(1, 7);
            if (esCastigoDado)
            {
                MessageBox.Show("¡Sacaste " + resultadoDado + " en el tiro del dado! \n" + "Retrocederás " + resultadoDado + " casillas", "¡Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                hayCastigo = false;
                moverFicha(-(resultadoDado));
                esCastigoDado = false;
                //btnLanzarDado.Enabled = true;
                cambiarTurno();
            }
            else
            {
                MessageBox.Show("¡Sacaste " + resultadoDado + " en el tiro del dado!", "¡Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDado.Text = Convert.ToString(resultadoDado);
                moverFicha(resultadoDado);
                btnLanzarDado.Enabled = false;
                btnResp1.Enabled = true;
                btnResp2.Enabled = true;
                btnResp3.Enabled = true;
            }
        }

        public void moverFicha(int resultadoDado)
        {
            int posAnterior;
            if (turno == 1)
            {
                posAnterior = j1.posCasilla;
                j1.posCasilla = j1.posCasilla + resultadoDado;
                if (j1.posCasilla < 1)
                {
                    j1.posCasilla = 1;
                }
                if(j1.posCasilla > 30)
                {
                    j1.posCasilla = posAnterior;
                }
                pbFicha1.Location = lstCasillas[j1.posCasilla - 1].Location;
                estaEnOca();
                pbFicha1.Location = new Point(pbFicha1.Location.X + 10, pbFicha1.Location.Y + 30);
                if (j1.posCasilla == 30)
                {
                    ganoJugador(1);
                }
                if (hayCastigo == false || esCastigoPosada)
                {
                    mostrarPregunta();
                }
            } else
            {
                posAnterior = j2.posCasilla;
                j2.posCasilla = j2.posCasilla + resultadoDado;
                if (j2.posCasilla <= 1)
                {
                    j2.posCasilla = 1;
                }
                if (j2.posCasilla > 30)
                {
                    j2.posCasilla = posAnterior;
                    MessageBox.Show("Deberás sacar en el dado las casillas exactas para GANAR", "¡I N F O!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cambiarTurno();
                }
                pbFicha2.Location = lstCasillas[j2.posCasilla - 1].Location;
                pbFicha2.Location = new Point(pbFicha2.Location.X + 60, pbFicha2.Location.Y + 30);
                if (j2.posCasilla == 30)
                {
                    ganoJugador(2);
                }
                if (hayCastigo == false || esCastigoPosada)
                {
                    mostrarPregunta();
                }
            }
            lblTurno.Text = turno.ToString();
            lblCasillaJ1.Text = j1.posCasilla.ToString();
            lblCasillaJ2.Text = j2.posCasilla.ToString();
            //sFicha.Play();
        }

        private void estaEnOca()
        {
            if (j1.turno == turno)
            {
                if (lstCasillas[j1.posCasilla - 1] == pbOca7)
                {
                    j1.posCasilla = 12;
                    pbFicha1.Location = pbOca12.Location;
                    pbFicha1.Location = new Point(pbFicha1.Location.X + 60, pbFicha1.Location.Y + 30);
                }
                else if (lstCasillas[j1.posCasilla - 1] == pbOca12)
                {
                    j1.posCasilla = 19;
                    pbFicha1.Location = pbOca19.Location;
                    pbFicha1.Location = new Point(pbFicha1.Location.X + 60, pbFicha1.Location.Y + 30);
                }
                else if (lstCasillas[j1.posCasilla - 1] == pbOca19)
                {
                    j1.posCasilla = 24;
                    pbFicha1.Location = pbOca24.Location;
                    pbFicha1.Location = new Point(pbFicha1.Location.X + 60, pbFicha1.Location.Y + 30);
                }
            }
            else if (j2.turno == turno)
            {
                if (lstCasillas[j1.posCasilla - 1] == pbOca7)
                {
                    j1.posCasilla = 12;
                    pbFicha1.Location = pbOca12.Location;
                    pbFicha1.Location = new Point(pbFicha1.Location.X + 60, pbFicha1.Location.Y + 30);
                }
                else if (lstCasillas[j1.posCasilla - 1] == pbOca12)
                {
                    j1.posCasilla = 19;
                    pbFicha1.Location = pbOca19.Location;
                    pbFicha1.Location = new Point(pbFicha1.Location.X + 60, pbFicha1.Location.Y + 30);
                }
                else if (lstCasillas[j1.posCasilla - 1] == pbOca19)
                {
                    j1.posCasilla = 24;
                    pbFicha1.Location = pbOca24.Location;
                    pbFicha1.Location = new Point(pbFicha1.Location.X + 60, pbFicha1.Location.Y + 30);
                }
            }
        }

        public void mostrarPregunta()
        {
            indicePregMostrada = rnd.Next(0, 50);
            if (lstPreguntasUtilizadas[indicePregMostrada] == false)
            {
                txtPregunta.Text = indicePregMostrada + lstPreguntas[indicePregMostrada] + lstRespuestas[indicePregMostrada];
                lstPreguntasUtilizadas[indicePregMostrada] = true;
            }
            else
            {
                mostrarPregunta();
            }
        }

        public void restaurarCastigos()
        {
            if(puenteUtilizado && posadaUtilizado && resbalonUtilizado && dadoUtilizado && calaveraUtilizado)
            {
                puenteUtilizado = false;
                posadaUtilizado = false;
                resbalonUtilizado = false;
                dadoUtilizado = false;
                calaveraUtilizado = false;
            }
        }

        private Castigo crearCastigo()
        {
            int nCastigo = rnd.Next(1, 6);
            switch (nCastigo)
            {
                case 1:
                    if(puenteUtilizado == false)
                    {
                        c = new Castigo(1);
                        puenteUtilizado = true;
                    } else
                    {
                        crearCastigo();
                    }
                    break;
                case 2:
                    if(posadaUtilizado == false)
                    {
                        c = new Castigo(2);
                        posadaUtilizado = true;
                    }
                    else
                    {
                        crearCastigo();
                    }
                    break;
                case 3:
                    if (dadoUtilizado == false)
                    {
                        c = new Castigo(3);
                        dadoUtilizado = true;
                    }
                    else
                    {
                        crearCastigo();
                    }
                    break;
                case 4:
                    if (resbalonUtilizado == false)
                    {
                        c = new Castigo(4);
                        resbalonUtilizado = true;
                    }
                    else
                    {
                        crearCastigo();
                    }
                    break;
                case 5:
                    if (calaveraUtilizado == false)
                    {
                        c = new Castigo(5);
                        calaveraUtilizado = true;
                    }
                    else
                    {
                        crearCastigo();
                    }
                    break;
                default:
                    crearCastigo();
                    break;
            }
            restaurarCastigos();
            return c;
        }
            
        

        public void compararRespuesta(int n)
        {
            if (respCorrecta == lstRespCorrectas[n])
            {
                MessageBox.Show("Respuesta Correcta, quedate ahí", "¡ACERTASTE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                estaEnOca();
                hayCastigo = false;
                btnLanzarDado.Enabled = true;
                btnResp1.Enabled = true;
                btnResp2.Enabled = true;
                btnResp3.Enabled = true;
                cambiarTurno();
            }
            else
            {
                hayCastigo = true;
                crearCastigo();
                MessageBox.Show("Escogiste la equivocada tu CASTIGO es \n " + c.tipo + "\n" + c.descripcion, "¡OH OH CASTIGO!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (c.numCastigo)
                {
                    case 1:
                        c.puente(this);
                        esCastigoPuente = true;
                        break;
                    case 2:
                        c.posada(this);
                        esCastigoPosada = true;
                        break;
                    case 3:
                        c.dado(this);
                        esCastigoDado = true;
                        break;
                    case 4:
                        c.resbalon(this);
                        esCastigoResbalon = true;
                        break;
                    case 5:
                        c.calavera(this);
                        esCastigoCalavera = true;
                        break;
                }
            }
            
        }

        public void cambiarTurno()
        {
            if (esCastigoPosada)
            {
                contCastigoPosada++;
                if (contCastigoPosada == 2)
                {
                    esCastigoPosada = false;
                    hayCastigo = false;
                    contCastigoPosada = 0;
                }
            } else
            {
                if (turno == 1)
                {
                    turno++;
                }
                else if (turno == 2)
                {
                    turno--;
                }
            }
            btnLanzarDado.Enabled = true;
            btnResp1.Enabled = false;
            btnResp2.Enabled = false;
            btnResp3.Enabled = false;
            lblTurno.Text = Convert.ToString(turno);
            txtPregunta.Clear();
        }

        private void btnResp1_Click(object sender, EventArgs e)
        {
            btnResp2.Enabled = false;
            btnResp3.Enabled = false;
            respCorrecta = 1;
            compararRespuesta(indicePregMostrada);
        }

        private void btnResp2_Click(object sender, EventArgs e)
        {
            btnResp1.Enabled = false;
            btnResp3.Enabled = false;
            respCorrecta = 2;
            compararRespuesta(indicePregMostrada);
        }

        private void btnResp3_Click(object sender, EventArgs e)
        {
            btnResp1.Enabled = false;
            btnResp2.Enabled = false;
            respCorrecta = 3;
            compararRespuesta(indicePregMostrada);
        }

        private void ganoJugador(int j)
        {
            string jug;
            if(j == 1)
            {
                jug = "JUGADOR 1";
            } else
            {
                jug = "JUGADOR 2";
            }
            MessageBox.Show(jug + "\n H A S    G A N A D O", "¡G A N A D O R! ¡G A N A D O R!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            FormInicio fInicio = new FormInicio();
            this.Close();
            fInicio.Show();
        }
    }
}
