using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoDeclaracionesIni : NodoArbolSemantico
    {
        public NodoDeclaracionesIni(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
               
        }

        

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {          
            return this;
        }


        public override void CalcularCodigo(bool modoDebug)
        {
            // esto es asi pq las variables del proc, tienen que ser globales
            if (this.hijosNodo.Count > 1)
            {
                this.ConstantesGlobales = this.hijosNodo[0].Codigo;
                this.VariablesGlobales = this.hijosNodo[1].Codigo;
            }
            
        }
    }
}
