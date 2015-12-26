using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoV : NodoArbolSemantico
    {
        public NodoV(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            List<Variable> variables = this.hijosNodo[0].VariablesACrear;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;
            this.RangoArregloSinPrefijo = this.hijosNodo[2].RangoArregloSinPrefijo;

            StringBuilder textoParaArbol = new StringBuilder();

            if (DeclaracionesPermitidas == TipoDeclaracionesPermitidas.Variables)
            {
                List<string> nombresVariables = new List<string>();

                if (variables.Count > 0)
                {

                    foreach (Variable v in variables)
                    {
                        if (!this.hijosNodo[2].EsArreglo)
                        //if (!v.EsArreglo)
                        {
                            if (!this.TablaSimbolos.ExisteVariableEnEsteContexto(v.Lexema, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.TablaSimbolos.AgregarVariable(v.Lexema, tipo, this.EsConstante, this.ContextoActual, this.NombreContextoLocal);
                                textoParaArbol.Append("Declaracion de variable ").Append(v.Lexema).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                                textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));

                                nombresVariables.Add(v.Lexema);
                            }
                            else
                            {
                                throw new ErrorSemanticoException(new ErrorVariableRepetida(v.Lexema));
                            }
                        }
                        else
                        {

                            if (!this.TablaSimbolos.ExisteArregloEnEsteContexto(v.Lexema, this.ContextoActual, this.NombreContextoLocal))
                            {


                                bool res = this.TablaSimbolos.AgregarArreglo(v.Lexema, tipo, this.ContextoActual, this.NombreContextoLocal, this.RangoArregloSinPrefijo, false);

                                if (!res)
                                {
                                    throw new ErrorSemanticoException(new ErrorArregloTopeDecimal());
                                }

                                nombresVariables.Add(v.Lexema);
                            }
                            else
                            {
                                throw new ErrorSemanticoException(new ErrorArregloRepetido(v.Lexema));
                            }
                        }
                    }

                    //if (!this.hijosNodo[2].EsArreglo)
                    //{
                    //    AgregarVariableViewModel(string.Join(",", nombresVariables.ToArray()), tipo);
                    //}
                    //else
                    //{
                    //    AgregarArregloViewModel(string.Join(",", nombresVariables.ToArray()), tipo, this.RangoArregloSinPrefijo);
                    //}
                }
            }
            else
            {
                throw new ErrorSemanticoException(new ErrorDeclaracionVariableFueraLugar());
            }

            return this;
        }


        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(" ");
            strBldr.Append(":");
            strBldr.Append(" ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            

            this.Codigo = strBldr.ToString();
        }
    }
}
