using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLinea: NodoArbolSemantico
    {
        public NodoLinea(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
        }

       

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.TieneLecturas = this.hijosNodo[0].TieneLecturas ;
            this.LlamaProcs = this.hijosNodo[0].LlamaProcs ;
            this.ModificaParametros = this.hijosNodo[0].ModificaParametros;
            this.AsignaParametros = this.hijosNodo[0].AsignaParametros ;
            this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales ;


            return this;
        }

      

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.AppendLine(this.hijosNodo[0].Codigo);

      
            this.Codigo = strBldr.ToString();
        }
    }
}
