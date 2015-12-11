using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoVariables : NodoArbolSemantico
    {
        public NodoVariables(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.EsConstante = false;
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Variables;
        }

      
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
         

            return this;
        }

    

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[1].Codigo).Append(" ");
                strBldr.AppendLine(";");
                strBldr.Append(this.hijosNodo[3].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
