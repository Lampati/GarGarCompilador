using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoMostrarOp : NodoArbolSemantico
    {
        public bool ConPausa { get; set; }

        public NodoMostrarOp(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

     
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo[0].Lexema.ToUpper().Equals("MOSTRARP"))
            {
                ConPausa = true;
            }

            return this;
        }

     

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            

            this.Codigo = strBldr.ToString();
        }
    }
}
