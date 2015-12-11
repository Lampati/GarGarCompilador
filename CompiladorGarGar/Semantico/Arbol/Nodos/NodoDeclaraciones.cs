using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoDeclaraciones : NodoArbolSemantico
    {
        public NodoDeclaraciones(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
               
        }

       

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
           
        

            return this;
        }

     

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            

            if (!(this.hijosNodo[0].Codigo == string.Empty))
            {
                strBldr.AppendLine("VAR");
                strBldr.Append(this.hijosNodo[0].Codigo);
            }
            

            this.Codigo = strBldr.ToString();
        }
    }
}
