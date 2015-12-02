using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExprProc: NodoArbolSemantico
    {
        public NodoExprProc(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
        }

   

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                /*
                if (this.hijosNodo[1].Temporal == null)
                {
                    this.ListaFirma.Add(new Firma(this.hijosNodo[1].Lexema, this.hijosNodo[1].TipoDato, this.hijosNodo[1].Valor));
                }
                else
                {
                    this.ListaFirma.Add(new Firma(this.hijosNodo[1].Temporal.Nombre, this.hijosNodo[1].TipoDato, this.hijosNodo[1].Valor));
                }
                 * */

                this.ListaFirma.Add(new Firma(this.hijosNodo[1].Lexema, this.hijosNodo[1].EsArregloEnParametro, this.hijosNodo[1].TipoDato, !this.hijosNodo[1].NoEsAptaPasajeReferencia, this.hijosNodo[0].EsConstante));
                this.ListaFirma.AddRange(this.hijosNodo[2].ListaFirma);

                this.Gargar = string.Format(", {0} {1}", this.hijosNodo[1].Gargar, this.hijosNodo[2].Gargar);
            }
            return this;
        }

      

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(", ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(this.hijosNodo[2].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
