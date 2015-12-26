using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExprBool : NodoArbolSemantico
    {
        public bool EsPar { get; set; }

        public NodoExprBool(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

     

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.NoEsAptaPasajeReferencia = this.hijosNodo[0].NoEsAptaPasajeReferencia || this.hijosNodo[1].NoEsAptaPasajeReferencia;
            this.EsConstante = this.hijosNodo[0].EsConstante;

            this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);         

            this.TipoDato = this.hijosNodo[0].TipoDato;

            this.Comparacion = this.hijosNodo[1].Comparacion;

            this.EsArregloEnParametro = this.hijosNodo[0].EsArregloEnParametro;

            if ( this.Comparacion != TipoComparacion.None && this.EsArregloEnParametro)
            {
                throw new ErrorSemanticoException(new ErrorOperacionesConArreglo());
            }
           


            this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;
            this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales || this.hijosNodo[1].UsaVariablesGlobales;
            
            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {
            StringBuilder strbldr;

            if (this.hijosNodo.Count == 2)
            {
                //Si la parte EX no tiene valor pq es lambda, es cuestion de no comprobar nada, y tomar el tipo de dato de EXPR
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Ninguno)
                {

                    if (this.hijosNodo[0].TipoDato != this.hijosNodo[1].TipoDato)
                    {
                        throw new ErrorSemanticoException(new ErrorCompararExpresionesTipoIncorrecto(this.hijosNodo[0].TipoDato, this.hijosNodo[1].TipoDato));
                    }
                    else
                    {
                        //Entonces lo que se estaba haciendo era una comparacion, pongo que es un dato booleano.
                        this.TipoDato = NodoTablaSimbolos.TipoDeDato.Booleano;
                    }
                }
                else
                {
                    this.TipoDato = this.hijosNodo[0].TipoDato;
                }
            }
        }

    

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();
            
            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(this.hijosNodo[1].Codigo);            

            this.Codigo = strBldr.ToString();
        }
    }
}
