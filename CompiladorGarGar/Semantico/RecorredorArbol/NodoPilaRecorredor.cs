using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos;
using System.Diagnostics;

namespace CompiladorGargar.Semantico.RecorredorArbol
{
    class NodoPilaRecorredor
    {
        public NodoArbolSemantico Nodo { get; set; }
        public int Actual { get; set; }

        public NodoPilaRecorredor(NodoArbolSemantico nod)
        {
            Debug.Assert(nod != null);

            this.Nodo = nod;
            this.Actual = 0;
        }

        public override string ToString()
        {
            return this.Nodo.ToString();
        }
    }
}
