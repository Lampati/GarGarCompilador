using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico
{
    class NodoPilaProduccionesAtributos
    {
        private bool calculadoAtributos;
        public bool CalculadoAtributos 
        {
            get
            {
                return AtributosCalculadosEnProduccion();
            }

        }

        public Produccion Prod {get; set;}

        private bool[] atributosCalculados;


        public NodoPilaProduccionesAtributos(Produccion prod)
        {
            this.Prod = prod;
            this.calculadoAtributos = false;

            atributosCalculados = new bool[this.Prod.Der.Count];

            for (int i = 0; i < this.Prod.Der.Count; i++)
            {
                atributosCalculados[i] = (this.Prod.Der[i].GetType() == typeof(Terminal)) ? true : false;
            }
        }

        public void ActualizarAtributos(NoTerminal nt)
        {
            int i = this.Prod.Der.IndexOf(nt);

            this.atributosCalculados[i] = true;

            this.Prod.Der[i] = nt;

        }

        private bool AtributosCalculadosEnProduccion()
        {
            bool retorno = true;
            int i = 0;
            while (i < this.Prod.Der.Count && retorno == true)
            {
                retorno = atributosCalculados[i];
                i++;
            }

            return retorno;
        }

        public NoTerminal obtenerNoTerminalConAtributosCalculados()
        {
            //CAlcular los atributos aca
            return this.Prod.Izq; 
        }

        public override string ToString()
        {
            return this.Prod.ToString();
        }
    }
}
