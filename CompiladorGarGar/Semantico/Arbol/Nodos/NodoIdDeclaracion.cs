using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoIdDeclaracion : NodoIdent
    {
        public NodoIdDeclaracion(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.VariablesACrear = new List<Variable>();
        }

    

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.VariablesACrear.Add(new Variable(this.hijosNodo[0].Lexema, false, 0));
            return this;
        }

    

        public override void ChequearAtributos(Terminal t)
        {
            if (this.TablaSimbolos.ExisteVariableEnEsteContexto(this.VariablesACrear[0].Lexema, this.ContextoActual, this.NombreContextoLocal))
            {
                throw new ErrorSemanticoException(new ErrorVariableRepetida(this.VariablesACrear[0].Lexema));
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            this.VariablesACrear = new List<Variable>();
            return this;
        }

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();
           
            strBldr.Append(this.hijosNodo[0].LexemaVariable);          

            this.Codigo = strBldr.ToString();
        }
    }
}
