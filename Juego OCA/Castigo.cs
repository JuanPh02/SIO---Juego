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
        int numCastigo;
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

        public void Puente(FormTablero fT)
        {
            fT.moverFicha(-3);
        }

        public void Posada(FormTablero fT)
        {
            
        }

        public void Dado()
        {

        }
    }
}
