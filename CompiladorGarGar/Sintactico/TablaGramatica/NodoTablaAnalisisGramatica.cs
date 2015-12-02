using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.TablaGramatica
{
    class NodoTablaAnalisisGramatica
    {
        public NoTerminal NoTerminal {get; set;}
        public Terminal Terminal { get; set; }
        public Produccion Produccion{get; set;}
        public bool EsSinc { get; set; }
        public string DescripcionError { get; set; }


        public NodoTablaAnalisisGramatica()
        {

        }

        public NodoTablaAnalisisGramatica(NoTerminal nt, Terminal t, Produccion prod)
        {
            this.NoTerminal = nt;
            this.Terminal = t;
            this.Produccion = prod;
            this.EsSinc = false;
            this.DescripcionError = string.Empty;
        }

        public NodoTablaAnalisisGramatica(NoTerminal nt, Terminal t, string sinc)
        {
            this.NoTerminal = nt;
            this.Terminal = t;
            this.EsSinc = true;
            this.Produccion = null;
            this.DescripcionError = sinc;
        }


        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            strBldr.Append(NoTerminal.ToString()); 
            strBldr.Append(",");
            strBldr.Append(Terminal.ToString());
            strBldr.Append("      ");
            if (EsSinc)
            {
                strBldr.Append("Sinc");
            }
            else
            {
                strBldr.Append(Produccion.ToString());
            }

            return strBldr.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // safe because of the GetType check
            NodoTablaAnalisisGramatica nodo = (NodoTablaAnalisisGramatica)obj;

            // use this pattern to compare reference members
            if (NoTerminal.Equals(nodo.NoTerminal.ToString()) && Terminal.Equals(nodo.Terminal.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }           
        }
    }
}
