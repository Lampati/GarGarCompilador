using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoF : NodoArbolSemantico
    {
        public bool EsConRef { get; set; }

        public NodoF(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();

            EsFirma = true;
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            Firma f;

            if (this.hijosNodo.Count > 3)
            {
                EsReferencia = true;

                f = new Firma(this.hijosNodo[1].Lexema, this.hijosNodo[3].TipoDato, EsReferencia, false);
                f.EsArreglo = this.hijosNodo[3].EsArreglo;
                f.RangoArregloSinPrefijo = this.hijosNodo[3].RangoArregloSinPrefijo;

                
            }
            else
            {
                EsReferencia = false;

                f = new Firma(this.hijosNodo[0].Lexema, this.hijosNodo[2].TipoDato, EsReferencia, false);
                f.EsArreglo = this.hijosNodo[2].EsArreglo;
                f.RangoArregloSinPrefijo = this.hijosNodo[2].RangoArregloSinPrefijo;
            }

            this.ListaFirma.Add(f);

            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.EsFirma = true;
    
        }


        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 3)
            {
                strBldr.Append("var").Append(" ");
                strBldr.Append(this.hijosNodo[1].LexemaVariable).Append(" "); // id
                strBldr.Append(":").Append(" "); // :
                strBldr.Append(this.hijosNodo[3].Codigo).Append(" "); // tipo
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].LexemaVariable).Append(" "); // id
                strBldr.Append(":").Append(" "); // :
                strBldr.Append(this.hijosNodo[2].Codigo).Append(" "); // tipo
            }
            

            this.Codigo = strBldr.ToString();
        }
    }
}
