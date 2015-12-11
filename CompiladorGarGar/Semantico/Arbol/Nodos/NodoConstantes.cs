using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoConstantes : NodoArbolSemantico
    {
        public NodoConstantes(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.EsConstante = true;
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Constantes;
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
