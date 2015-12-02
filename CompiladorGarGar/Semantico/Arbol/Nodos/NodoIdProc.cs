using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoIdProc : NodoArbolSemantico
    {
        public NodoIdProc(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

     

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.Lexema = hijoASintetizar.Lexema;
            this.NombreContextoLocal = hijoASintetizar.Lexema;
        }

     

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            
            //strBldr.Append(this.hijosNodo[0].LexemaVariable);
            strBldr.Append(this.LexemaVariable);
            strBldr.Append(" ");


            this.Codigo = strBldr.ToString();
        }
    }
}
