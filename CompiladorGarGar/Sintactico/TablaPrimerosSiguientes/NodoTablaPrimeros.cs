using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.TablaPrimerosSiguientes
{
    class NodoTablaPrimeros
    {
        private Produccion prod;
        public Produccion Prod
        {
            get { return prod; }
            set { prod = value; }
        }

        private NoTerminal nt;
        public NoTerminal NT
        {
            get { return nt; }
            set { nt = value; }
        }

        private List<Terminal> terminales;
        public List<Terminal> Terminales
        {
            get { return terminales; }
            set { terminales = value; }
        }

        private bool modificado;
        public bool Modificado
        {
            get { return modificado; }
            set { modificado = value; }
        }

        public NodoTablaPrimeros()
        {
            this.terminales = new List<Terminal>();
        }


        public NodoTablaPrimeros(NoTerminal noTerminal, Produccion produccion,List<Terminal> terms)
        {
            this.prod = produccion;
            this.nt = noTerminal;
            this.terminales = new List<Terminal>();
            this.terminales = terms;

            this.modificado = true;
        }
    }
}
