using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoL: NodoArbolSemantico
    {
        public NodoL(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }

    

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo[0].GetType() == typeof(NodoTerminal))
            {
                this.Lexema = this.hijosNodo[0].Lexema.Replace("'", string.Empty);

                this.Gargar = this.hijosNodo[0].Lexema;
               
            }
            else
            {
                this.Gargar = this.hijosNodo[0].Gargar;
            }
            

            //this.ListaElementosVisualizar.Add(string.Copy(this.Lugar));

            return this;
        }

    

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo[0].GetType() != typeof(NodoTerminal))
            {
                if (this.hijosNodo[0].TipoDato == NodoTablaSimbolos.TipoDeDato.Booleano)
                {
                    strBldr.Append(GeneracionCodigoHelpers.EscribirValorBooleano(this.hijosNodo[0].Codigo));
                }
                else if (this.hijosNodo[0].TipoDato == NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    strBldr.Append(GeneracionCodigoHelpers.EscribirValorNumerico(this.hijosNodo[0].Codigo));
                }
                else
                {
                    strBldr.Append(this.hijosNodo[0].Codigo);    
                }
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Lexema);
            }


            this.Codigo = strBldr.ToString();
        }
    }
}
