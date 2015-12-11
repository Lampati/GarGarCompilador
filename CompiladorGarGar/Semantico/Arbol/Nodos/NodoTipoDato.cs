using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoTipoDato : NodoArbolSemantico
    {
        public NodoTipoDato(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

      
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
        }

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();
            string tipoDato;
            switch (this.hijosNodo[0].Lexema.ToLower())
            {
                case "numero":
                    tipoDato =  "real";
                    break;
                case "texto":
                    tipoDato = "string";
                    break;
                case "booleano":
                    tipoDato = "boolean";
                    break;
                default:
                    tipoDato = string.Empty;
                    break;
            }
            strBldr.Append(tipoDato);
            this.Codigo = strBldr.ToString();
        }
    }
}
