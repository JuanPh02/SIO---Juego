using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_OCA
{
    class Castigo
    {
        Random rnd = new Random();
        public int numCastigo;
        public string tipo;
        public string descripcion;

        public Castigo()
        {
            numCastigo = rnd.Next(1, 6);
            switch (numCastigo)
            {
                case 1:
                    tipo = "PUENTE";
                    descripcion = "Retrocederás tres casillas ";
                    break;
                case 2:
                    tipo = "POSADA";
                    descripcion = "Perderás un turno ";
                    break;
                case 3:
                    tipo = "DADO";
                    descripcion = "Tira el dado y retrocede el número de casillas que correspondan al dado ";
                    break;
                case 4:
                    tipo = "RESBALÓN";
                    descripcion = "Retrocederás dos casillas ";
                    break;
                case 5:
                    tipo = "CALAVERA";
                    descripcion = "Te devolverás a la casilla 1 (INICIO) ";
                    break;
            }
        }

        public void puente(FormTablero fT)
        {
            fT.moverFicha(-3);
        
        }

        public void posada(FormTablero fT)
        {
            fT.cambiarTurno();
            
        }

        public void dado(FormTablero fT)
        {
            fT.btnLanzarDado.Enabled = true;
       
        }

        public void resbalon(FormTablero fT )
        {
            fT.moverFicha(-2);
        }

        public void calavera(FormTablero fT)
        {
            fT.moverFicha(-30);
           
        }
    }
}
