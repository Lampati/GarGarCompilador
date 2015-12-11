using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExprBoolExtra : NodoArbolSemantico
    {


        public NodoExprBoolExtra(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

      

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {

            if (this.hijosNodo.Count > 1)
            {
                this.NoEsAptaPasajeReferencia = true;

                this.Gargar = string.Format("{0} {1} {2}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar, this.hijosNodo[2].Gargar);

                this.Comparacion = TipoComparacion.None;
                this.Operacion = this.hijosNodo[0].Operacion;

                //Si le pongo un and, o le pongo un or, termina siendo siempre un tipo booleano el resultante.
                //this.TipoDato = this.hijosNodo[0].TipoDato;
                this.TipoDato = NodoTablaSimbolos.TipoDeDato.Booleano;

                this.EsArregloEnParametro = this.hijosNodo[1].EsArregloEnParametro;

                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Booleano)
                {
                    StringBuilder strbldr = new StringBuilder("Los operadores logicos and y or ");
                    strbldr.Append("solo pueden ser usados con expresiones del tipo booleanas");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }

                if (this.Operacion != TipoOperatoria.Ninguna && this.EsArregloEnParametro)
                {
                    StringBuilder strbldr = new StringBuilder("No se puede realizar operaciones logicas o aritmeticas con un ");
                    strbldr.Append(" arreglo. Las operaciones logicas y aritmenticas se pueden realizar únicamente con las posiciones de un arreglo");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
           


                this.AsignaParametros = this.hijosNodo[1].AsignaParametros || this.hijosNodo[2].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales || this.hijosNodo[2].UsaVariablesGlobales;
            }
            else
            {
                this.Operacion = TipoOperatoria.Ninguna;
            }
            
            return this;
        }

   

        public override void CalcularCodigo(bool modoDebug)
        {
            

            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(" ( ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(" ) ");
                strBldr.Append(this.hijosNodo[2].Codigo);

                
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
