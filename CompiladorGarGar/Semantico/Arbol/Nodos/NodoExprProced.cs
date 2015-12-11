using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExprProced: NodoArbolSemantico
    {
        public NodoExprProced(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
            this.EsPasajeParametrosAProcOFunc = true;
        }

    
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.ListaFirma.Add(new Firma(this.hijosNodo[0].Lexema, this.hijosNodo[0].EsArregloEnParametro, this.hijosNodo[0].TipoDato, !this.hijosNodo[0].NoEsAptaPasajeReferencia, this.hijosNodo[0].EsConstante));
                this.ListaFirma.AddRange(this.hijosNodo[1].ListaFirma);

                this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales || this.hijosNodo[1].UsaVariablesGlobales;

                this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);
            }
            return this;
        }

   

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
