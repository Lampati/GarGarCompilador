using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoT : NodoArbolSemantico
    {
        public NodoT(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

      
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {            

            if (this.hijosNodo.Count > 1)
            {
                if (this.hijosNodo.Count > 3)
                {
                    this.NoEsAptaPasajeReferencia = true;
                    this.TipoDato = this.hijosNodo[2].TipoDato;
                    this.Lexema = this.hijosNodo[2].Lexema;
                    this.Gargar = string.Format("! ({0})", this.hijosNodo[2].Gargar);

                    this.EsArregloEnParametro = this.hijosNodo[2].EsArregloEnParametro;
                    //this.Lugar = this.hijosNodo[2].Lugar;

                    

                    if (this.hijosNodo[2].TipoDato != TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano)
                    {                       
                        throw new ErrorSemanticoException(new ErrorNegarExprNoBooleana());
                    }

                    this.AsignaParametros = this.hijosNodo[2].AsignaParametros;
                    this.UsaVariablesGlobales = this.hijosNodo[2].UsaVariablesGlobales;
                }
                else
                {

                    this.NoEsAptaPasajeReferencia = this.hijosNodo[1].NoEsAptaPasajeReferencia;
                    
                    this.TipoDato = this.hijosNodo[1].TipoDato;
                    this.Lexema = this.hijosNodo[1].Lexema;
                    this.EsArregloEnParametro = this.hijosNodo[1].EsArregloEnParametro;

                    this.Gargar = string.Format("({0})", this.hijosNodo[1].Gargar);

                    this.AsignaParametros = this.hijosNodo[1].AsignaParametros;
                    this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales;
                }
            }
            else
            {

                this.NoEsAptaPasajeReferencia = this.hijosNodo[0].NoEsAptaPasajeReferencia;
                this.EsConstante = this.hijosNodo[0].EsConstante;
                this.TipoDato = this.hijosNodo[0].TipoDato;
                this.Lexema = this.hijosNodo[0].Lexema;
                this.ValorConstanteNumerica = this.hijosNodo[0].ValorConstanteNumerica;

                this.EsArregloEnParametro = this.hijosNodo[0].EsArregloEnParametro;

              
                this.AsignaParametros = this.hijosNodo[0].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales;

                this.Gargar = this.hijosNodo[0].Gargar;
            }

            return this;
        }


        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 3)
            {
                strBldr.Append("NOT ");
                strBldr.Append(" ( ");
                strBldr.Append(this.hijosNodo[2].Codigo);
                strBldr.Append(" ) ");
            }
            else if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(" ( ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(" ) ");
            }
            else
            {
                strBldr.Append(" ").Append(this.hijosNodo[0].Codigo).Append(" ");
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
