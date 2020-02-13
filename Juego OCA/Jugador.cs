using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_OCA
{
    class Jugador
    {
        public int posCasilla;
        public int turno;

        public Jugador(int pCasilla, int t)
        {
            this.posCasilla = pCasilla;
            this.turno = t;
        }

    }
}
