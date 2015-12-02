using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoTipoDatoConArreglo : NodoArbolSemantico
    {
        public NodoTipoDatoConArreglo(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }
   
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;          
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.TipoDato = this.hijosNodo[5].TipoDato;
                this.EsArreglo = true;
                this.RangoArreglo = this.hijosNodo[2].RangoArreglo;
                this.RangoArregloSinPrefijo = this.hijosNodo[2].RangoArregloSinPrefijo;

                this.NombreTipoArreglo = this.TablaSimbolos.AgregarTipoArreglo(this.TipoDato, this.RangoArreglo);
            }
            else
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
                this.EsArreglo = false;
            }

            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.NombreTipoArreglo);
                //strBldr.Append("Array ");
                
                //strBldr.Append("[ ");
                //strBldr.Append("1..");
                //strBldr.Append(this.hijosNodo[2].Codigo);
                //strBldr.Append("] ");
                
                //strBldr.Append(" of ");
                //strBldr.Append(this.hijosNodo[5].Codigo);
                
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
