﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoIdAsign: NodoArbolSemantico
    {
        public NodoIdAsign(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

     

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.EsArreglo = true;
                this.TipoDato = this.hijosNodo[1].TipoDato;

                this.Gargar = string.Format("[{0}]", this.hijosNodo[1].Gargar);
                
                
            }
            else
            {
                this.EsArreglo = false;
                this.Gargar = string.Empty;
            }
            return this;
        }

   
        public override void ChequearAtributos(Terminal t)
        {
            if (this.EsArreglo)
            {
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    throw new ErrorSemanticoException(new ErrorIndiceArregloNoNumerico());
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Numero;
            return this;
        }

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            //if (this.hijosNodo.Count > 1)
            //{
            //    strBldr.Append("[");
            //    strBldr.Append(string.Format("FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo({0},{1})", this.hijosNodo[1].Codigo));
            //    strBldr.Append("]");
            //}
            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
