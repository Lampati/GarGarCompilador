using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoOpBooleanas : NodoArbolSemantico
    {
        public NodoOpBooleanas(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            switch (hijoASintetizar.Lexema)
            {
                case "and":
                    this.Operacion = TipoOperatoria.And;
                    break;

                case "or":
                    this.Operacion = TipoOperatoria.Or;
                    break;
            }

            this.NoEsAptaPasajeReferencia = true;

            this.Gargar = hijoASintetizar.Gargar;
        }

      

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.Append(" ").Append(this.hijosNodo[0].Lexema).Append(" ");
            this.Codigo = strBldr.ToString();
        }
    
    
    }
}
