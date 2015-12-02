using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoYDec : NodoArbolSemantico
    {
        public NodoYDec(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.VariablesACrear = new List<Variable>();
        }


        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1) //No es lambda en ese caso
            {
                this.EsArreglo = true;
                this.TipoDato = this.hijosNodo[1].TipoDato;
            }
            else
            {
                this.EsArreglo = false;
            }

            return this;
        }

      
        public override void ChequearAtributos(Terminal t)
        {
            if (this.EsArreglo)
            {
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    throw new ErrorSemanticoException(new StringBuilder("El subindice del arreglo debe ser natural.").ToString());
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Numero;
            return this;
        }
    }
}
