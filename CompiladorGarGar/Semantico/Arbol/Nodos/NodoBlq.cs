using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoBlq: NodoArbolSemantico
    {
        public NodoBlq(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

      public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {


                Gargar = new StringBuilder(this.hijosNodo[0].Gargar).AppendLine().AppendLine(this.hijosNodo[1].Gargar).ToString();

             

                this.TieneLecturas = this.hijosNodo[0].TieneLecturas || this.hijosNodo[1].TieneLecturas;
                this.LlamaProcs = this.hijosNodo[0].LlamaProcs || this.hijosNodo[1].LlamaProcs;
                this.ModificaParametros = this.hijosNodo[0].ModificaParametros || this.hijosNodo[1].ModificaParametros;
                this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales || this.hijosNodo[1].UsaVariablesGlobales;
            }
            else
            {
                Gargar = string.Empty;
            }

            return this;
        }


        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);

                this.Codigo = strBldr.ToString();
            }
            else
            {
                this.Codigo = string.Empty;
            }
        }

    }
}
