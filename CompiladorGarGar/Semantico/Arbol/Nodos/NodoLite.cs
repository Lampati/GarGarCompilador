using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLite : NodoArbolSemantico
    {
        public NodoLite(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }


        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1 )
            {
                this.ListaElementosVisualizar.AddRange(this.hijosNodo[1].ListaElementosVisualizar);
                this.ListaElementosVisualizar.AddRange(this.hijosNodo[2].ListaElementosVisualizar);

                this.Gargar = string.Format(", {0} {1}", this.hijosNodo[1].Gargar, this.hijosNodo[2].Gargar);
            }
            else
            {
                this.Gargar = string.Empty;
            }

            return this;
        }

      
         public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1 )
            {
                strBldr.Append(", ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(this.hijosNodo[2].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
