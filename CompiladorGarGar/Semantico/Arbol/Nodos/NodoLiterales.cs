using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLiterales : NodoArbolSemantico
    {
        public NodoLiterales(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }


        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.ListaElementosVisualizar.AddRange(this.hijosNodo[0].ListaElementosVisualizar);
            this.ListaElementosVisualizar.AddRange(this.hijosNodo[1].ListaElementosVisualizar);

            this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);

            return this;
        }

      

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}