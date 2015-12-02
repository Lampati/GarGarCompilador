using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico
{
    class PilaProduccionesAtributos
    {

        private List<NodoPilaProduccionesAtributos> elementosPila = new List<NodoPilaProduccionesAtributos>();

        public PilaProduccionesAtributos()
        {
            this.elementosPila = new List<NodoPilaProduccionesAtributos>();
        }

        public NodoPilaProduccionesAtributos ObtenerTope()
        {
            return this.elementosPila.Last();
        }

        public void InsertarElemento(NodoPilaProduccionesAtributos elem)
        {
            this.elementosPila.Add(elem);
            //this.elementosPila.Insert(this.elementosPila.Count - 1, elem);
        }

        public void CalcularAtributos()
        {
            bool continuar = true;
            while (!this.esVacia() && continuar)
            {
                if (this.ObtenerTope().CalculadoAtributos)
                {
                    NoTerminal nt = this.ObtenerTope().obtenerNoTerminalConAtributosCalculados();
                    this.DescartarTope();
                    this.ObtenerTope().ActualizarAtributos(nt);
                }
                else
                {
                    continuar = false;
                }

            }
        }

        

        public void DescartarTope()
        {
            
            this.elementosPila.RemoveAt(this.elementosPila.Count - 1);
        }

        private bool AtributosCalculadosEnParteDerecha()
        {
            bool retorno = true;
            int i = 0;
            while (i < ObtenerTope().Prod.Der.Count && retorno == true)
            {
                retorno = elementosPila[i].CalculadoAtributos;
                i++;
            }

            return retorno;
        }



        internal bool esVacia()
        {
            return this.elementosPila.Count == 0;
        }




    }
}
