using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoOpMultDiv: NodoArbolSemantico
    {
        public NodoOpMultDiv(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

       
        
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            switch (hijoASintetizar.Lexema)
            {

                case "*":
                    this.Operacion = TipoOperatoria.Multiplicacion;
                    break;

                case "/":
                    this.Operacion = TipoOperatoria.Division;
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
                case "*":
                    tipoDato = "*";
                    break;
                case @"/":
                    tipoDato = @"/";
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
