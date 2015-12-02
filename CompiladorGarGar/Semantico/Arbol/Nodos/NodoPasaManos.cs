using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoPasaManos :NodoArbolSemantico
    {
        public NodoPasaManos(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
               
        }
    }
}
