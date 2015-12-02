using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoReferencia : NodoArbolSemantico
    {
        public bool EsConRef { get; set; }

        public NodoReferencia(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (EsConRef)
            {
                strBldr.Append(this.Lexema);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
