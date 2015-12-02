using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

using CompiladorGargar.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoBloqueSi: NodoArbolSemantico
    {
        public NodoBloqueSi(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
         


        }

      

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.EsSino = this.hijosNodo[4].EsSino;

            NodoTablaSimbolos.TipoDeDato tipoDato = this.hijosNodo[2].TipoDato;

            if (tipoDato != NodoTablaSimbolos.TipoDeDato.Booleano)
            {                
                throw new ErrorSemanticoException(new StringBuilder("La condicion resultante del bloque si debe ser booleana").ToString());                
            }


            return this;
        }

      

     

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(this.hijosNodo[2].LineaCorrespondiente));

            strBldr.Append("If ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(") ");
            strBldr.AppendLine("then");
            strBldr.AppendLine("begin");
            strBldr.Append("\t").AppendLine(this.hijosNodo[5].Codigo.Replace("\r\n", "\r\n\t"));
            strBldr.Append("end");

            if (this.hijosNodo[6].Codigo != string.Empty)
            {
                strBldr.AppendLine();
                strBldr.Append(this.hijosNodo[6].Codigo);
            }
            else
            {
                strBldr.AppendLine(";");
            }


            

            this.Codigo = strBldr.ToString().ToString().TrimEnd();
        }
    }
}
