using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.TablaPrimerosSiguientes
{
    class RetornoPrimeros
    {
        private List<Terminal> terminales;
        public List<Terminal> Terminales
        {
            get { return terminales; }
            set { terminales = value; }
        }

        private NoTerminal noTerminal;
        public NoTerminal NoTerminal
        {
            get { return noTerminal; }
            set { noTerminal = value; }
        }

        private bool esNecesarioSiguiente;
        public bool EsNecesarioSiguiente
        {
            get { return esNecesarioSiguiente; }
            set { esNecesarioSiguiente = value; }
        }

        public RetornoPrimeros()
        {
            this.terminales = new List<Terminal>();
            this.esNecesarioSiguiente = false;
            this.noTerminal = null;
        }
    }
}
