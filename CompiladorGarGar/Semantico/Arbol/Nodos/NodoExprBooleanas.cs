using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExprBooleanas : NodoArbolSemantico
    {
        public bool EsPar { get; set; }

        public NodoExprBooleanas(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

       

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.NoEsAptaPasajeReferencia = this.hijosNodo[0].NoEsAptaPasajeReferencia || this.hijosNodo[1].NoEsAptaPasajeReferencia;
            this.EsConstante = this.hijosNodo[0].EsConstante;
           
            //Si el tipo de dato de ExprBoolExtra es booleano, es pq tiene un and, o un or. Por ende, este tambien sera booleano.
            //Tambien le pongo que es ninguna el tipo de operatoria. Cualquiera de los dos sirve.

            this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);

            LineaCorrespondiente = GlobalesCompilador.UltFila;

            this.Comparacion = this.hijosNodo[1].Comparacion;
            this.Operacion = this.hijosNodo[1].Operacion;
            this.EsArregloEnParametro = this.hijosNodo[0].EsArregloEnParametro;
           
            this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;
            this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales || this.hijosNodo[1].UsaVariablesGlobales;

            if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            {
                this.TipoDato = NodoTablaSimbolos.TipoDeDato.Booleano;

                if (this.hijosNodo[0].TipoDato != NodoTablaSimbolos.TipoDeDato.Booleano || this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Booleano)
                {
                    StringBuilder strbldr = new StringBuilder("Los operadores logicos and y or ");
                    strbldr.Append("solo pueden ser usados con expresiones del tipo booleanas");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }

                

                if ( this.EsArregloEnParametro)
                {
                    StringBuilder strbldr = new StringBuilder("No se puede realizar operaciones logicas o aritmeticas con un ");
                    strbldr.Append(" arreglo. Las operaciones logicas y aritmenticas se pueden realizar únicamente con las posiciones de un arreglo");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            else
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
            }

            
            
            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {
            StringBuilder strbldr;

            if (this.hijosNodo.Count == 2)
            {
                //Si el tipo de dato de ExprBoolExtra es booleano, este tambien debera serlo para poder compararlos 
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Ninguno)
                {
                    if (this.hijosNodo[0].TipoDato != this.hijosNodo[1].TipoDato)
                    {
                        strbldr = new StringBuilder("Se esta intentando comparar una expresion del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[0].TipoDato));
                        strbldr.Append(" con una del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[1].TipoDato));
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }
            }
        }

     
        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(" ( ");
            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(" ) ");
            strBldr.Append(this.hijosNodo[1].Codigo);
            

            this.Codigo = strBldr.ToString();
        }
    }
}
