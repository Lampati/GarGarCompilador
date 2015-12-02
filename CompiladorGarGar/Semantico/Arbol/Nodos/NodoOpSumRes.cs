using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoOpSumRes : NodoArbolSemantico
    {
        public NodoOpSumRes(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            switch (hijoASintetizar.Lexema)
            {
                case "+":
                case "++":
                    this.Operacion = TipoOperatoria.Suma;
                    break;

                case "-":
                case "--":
                    this.Operacion = TipoOperatoria.Resta;
                    break;

                case "&":
                    this.Operacion = TipoOperatoria.Concatenacion;
                    break;

            }

            this.NoEsAptaPasajeReferencia = true;
            this.Gargar = hijoASintetizar.Gargar;

        }

     


        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            string tipoDato;
            switch (this.hijosNodo[0].Lexema)
            {
                case "+":
                    tipoDato = "+";
                    break;
                case "-":
                    tipoDato = "-";
                    break;
                case "&":
                    tipoDato = "+";
                    break;
                default:
                    tipoDato = string.Empty;
                    break;
            }            

            strBldr.Append(" ").Append(tipoDato).Append(" ");
            this.Codigo = strBldr.ToString();
        }
    }
}
