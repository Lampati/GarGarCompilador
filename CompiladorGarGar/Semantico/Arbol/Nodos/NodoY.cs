using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoY: NodoArbolSemantico
    {
        public NodoY(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }
        
       
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {            
      

            this.EsFuncion = false;
            this.EsArreglo = false;

            this.Gargar = string.Empty;

            if (this.hijosNodo.Count > 1)
            {
                string tipoEntrada = this.hijosNodo[0].Lexema;

                //this.Lugar = this.hijosNodo[1].Lugar;

                switch (tipoEntrada)
                {
                    case "(":
                        this.EsFuncion = true;
                        this.ListaFirma = this.hijosNodo[1].ListaFirma;
                        this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales;
                        this.Gargar = string.Format("({0})", this.hijosNodo[1].Gargar);
                        break;

                    case "[":
                        this.EsArreglo = true;
                        this.TipoDato = this.hijosNodo[1].TipoDato;
                        this.Gargar = string.Format("[{0}]", this.hijosNodo[1].Gargar);
                        break;
                }
            }

            return this;
        }

       

        public override void ChequearAtributos(Terminal t)
        {
            if (this.EsArreglo)
            {
                if (this.TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    throw new ErrorSemanticoException(new StringBuilder("El indice del arreglo debe ser un numero.").ToString());
                }
            }
        }

        

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            //if (this.hijosNodo.Count > 1)
            //{
            //    if (this.EsArreglo)
            //    {
            //        strBldr.Append("[");
            //        strBldr.Append(this.hijosNodo[1].Codigo);
            //        strBldr.Append("]");
            //    }
            //    else
            //    {
            //        strBldr.Append("(");
            //        strBldr.Append(this.hijosNodo[1].Codigo);
            //        strBldr.Append(")");
            //    }
            //}

            if (this.hijosNodo.Count > 1)
            {
                

                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
