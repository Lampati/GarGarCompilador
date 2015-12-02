using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoC : NodoArbolSemantico
    {
        public NodoC(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            string nombre = this.hijosNodo[0].Lexema;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;
            

            if (this.DeclaracionesPermitidas == TipoDeclaracionesPermitidas.Constantes)
            {

                if (!this.TablaSimbolos.ExisteVariableEnEsteContexto(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    if (tipo != this.hijosNodo[4].TipoDato)
                    {
                        throw new ErrorSemanticoException(new StringBuilder("Se intento asignar un tipo invalido a ").Append(nombre).ToString());
                    }

                    this.ValorConstanteNumerica = this.hijosNodo[4].ValorConstanteNumerica;
                    this.ValorConstanteTexto = this.hijosNodo[4].ValorConstanteTexto;

                    if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        this.TablaSimbolos.AgregarConstante(nombre, tipo, this.ContextoActual, this.NombreContextoLocal, this.ValorConstanteNumerica);
                    }
                    else
                    {
                        this.TablaSimbolos.AgregarConstante(nombre, tipo, this.ContextoActual, this.NombreContextoLocal, this.ValorConstanteTexto);
                    }

                    //this.TablaSimbolos.AgregarVariable(nombre, tipo, this.EsConstante, this.ContextoActual, this.NombreContextoLocal);
                    
                   

                }
                else
                {
                    throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(nombre).Append(" ya existia en ese contexto").ToString());
                }
            }
            else
            {
                throw new ErrorSemanticoException(new StringBuilder("No se permiten declarar constantes aqui. Las constantes deben ser creadas en el contexto global, al principio del programa").ToString());
            }

            return this;
        }

      
     

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            string nombre = this.hijosNodo[0].Lexema;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;

            if (!this.TablaSimbolos.ExisteVariableEnEsteContexto(nombre, this.ContextoActual, this.NombreContextoLocal))
            {
                this.TablaSimbolos.AgregarVariable(nombre, tipo, this.EsConstante, this.ContextoActual, this.NombreContextoLocal);
            }

            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append(this.hijosNodo[0].LexemaVariable).Append(" "); // id
            //strBldr.Append(":").Append(" "); // :
            //strBldr.Append(this.hijosNodo[2].Codigo).Append(" "); // tipo
            strBldr.Append("=").Append(" "); // =
            strBldr.Append(this.hijosNodo[4].Codigo).Append(" "); // valor

            this.Codigo = strBldr.ToString();
        }

    }
}
