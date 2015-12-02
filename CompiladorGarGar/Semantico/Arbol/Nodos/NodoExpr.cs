using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Diagnostics;
using CompiladorGargar.Auxiliares;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExpr : NodoArbolSemantico
    {
        public NodoExpr(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

 
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            //Hago la operacion igual, si los tipos no eran iguales, simplemente tiro la excepcion.
            //Por defecto uso el tipo1 para asignar el tipo de este nodo.
            this.NoEsAptaPasajeReferencia = this.hijosNodo[0].NoEsAptaPasajeReferencia || this.hijosNodo[1].NoEsAptaPasajeReferencia;
            this.EsConstante = this.hijosNodo[0].EsConstante;

            this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);

            this.EsArregloEnParametro = this.hijosNodo[0].EsArregloEnParametro;
            
            TipoOperatoria operacion = this.hijosNodo[1].Operacion;

            this.TipoDato = this.hijosNodo[0].TipoDato;

            if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Ninguno)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            }

            this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;
            this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales || this.hijosNodo[1].UsaVariablesGlobales;

            if (operacion != TipoOperatoria.Ninguna)
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;

                if (this.EsArregloEnParametro)
                {
                    StringBuilder strbldr = new StringBuilder("No se puede realizar operaciones logicas o aritmeticas con un ");
                    strbldr.Append(" arreglo. Las operaciones logicas y aritmenticas se pueden realizar únicamente con las posiciones de un arreglo");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            
            }
            else
            {
              

                this.Lexema = this.hijosNodo[0].Lexema;
                //this.Temporal = this.hijosNodo[0].Temporal;

                
            }

            

            

            return this;
        }

     

        public override void ChequearAtributos(Terminal t)
        {
            TipoOperatoria operacion = this.hijosNodo[1].Operacion;

            NodoTablaSimbolos.TipoDeDato tipo1 = this.hijosNodo[0].TipoDato;        
            NodoTablaSimbolos.TipoDeDato tipo2 = this.hijosNodo[1].TipoDato;

            if (operacion != TipoOperatoria.Ninguna)
            {
                if (tipo1 != tipo2)
                {
                    StringBuilder strbldr = new StringBuilder("Se esta intentando operar con distintos tipos");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
        }

     

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(this.hijosNodo[1].Codigo);
           

            this.Codigo = strBldr.ToString();
        }


    }
}
