using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoEx : NodoArbolSemantico
    {
        public NodoEx(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }

      

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (hijosNodo.Count > 1)
            {
                this.NoEsAptaPasajeReferencia = true;

                this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);

                this.TipoDato = this.hijosNodo[1].TipoDato;
                this.Comparacion = this.hijosNodo[0].Comparacion;

                this.AsignaParametros = this.hijosNodo[1].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales;  

                if (!(this.Comparacion == TipoComparacion.Igual || this.Comparacion == TipoComparacion.Distinto))
                {
                    if (this.TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        StringBuilder strbldr = new StringBuilder("Los comparadores mayor, mayor igual, menor y menor igual ");
                        strbldr.Append("solo pueden ser usados con expresiones del tipo numericas");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }

                }

                this.EsArregloEnParametro = this.hijosNodo[1].EsArregloEnParametro;

                if (this.EsArregloEnParametro)
                {
                    StringBuilder strbldr = new StringBuilder("No se puede realizar operaciones logicas o aritmeticas con un ");
                    strbldr.Append(" arreglo. Las operaciones logicas y aritmenticas se pueden realizar únicamente con las posiciones de un arreglo");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }

            }
            return this;
        }

      

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Lexema);
                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
