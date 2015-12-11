using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoValorConst : NodoArbolSemantico
    {
        public NodoValorConst(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }
     
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            this.ValorConstanteNumerica = hijoASintetizar.ValorConstanteNumerica;
            this.ValorConstanteTexto = hijoASintetizar.Lexema;
        }
       
        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo[0].GetType() == typeof(NodoTerminal))
            {
                strBldr.Append(this.hijosNodo[0].Lexema);
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
            }
            
            this.Codigo = strBldr.ToString();
        }
    }
}
