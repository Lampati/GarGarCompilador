using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoMaxArregloDec: NodoArbolSemantico
    {

        private bool esID;

        public NodoMaxArregloDec(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }


      

        

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero ||
                t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Literal ||
                t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Verdadero ||
                t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Falso                 
                )

            {
                esID = false;

                if (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero)
                {
                    double valor = t.ObtenerValor();
                    if (valor > 0)
                    {

                        this.RangoArreglo = valor.ToString();
                        this.RangoArregloSinPrefijo = valor.ToString();
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorNumeroInvalidoEnTopeArreglo(valor.ToString()));                        
                    }
                }
                else
                {
                    throw new ErrorSemanticoException(new ErrorNoNumeroEnTopeArreglo(t.Componente.Lexema));                        
                    
                }
            }
            else
            {
                esID = true;

                Lexema = t.Componente.Lexema;

                if (this.TablaSimbolos.ExisteVariable(Lexema, this.ContextoActual, this.NombreContextoLocal))
                {
                    if (!this.TablaSimbolos.EsModificableValorVarible(Lexema, this.ContextoActual, this.NombreContextoLocal))
                    {
                        if (this.TablaSimbolos.ObtenerTipoVariable(Lexema, this.ContextoActual, this.NombreContextoLocal) == NodoTablaSimbolos.TipoDeDato.Numero)
                        {
                            if (this.TablaSimbolos.RetornarValorConstante(Lexema, this.ContextoActual, this.NombreContextoLocal) > 0)
                            {

                                this.RangoArreglo = LexemaVariable;
                                this.RangoArregloSinPrefijo = Lexema;
                            }
                            else
                            {
                                throw new ErrorSemanticoException(new ErrorConstanteNumericaInvalidaEnTopeArreglo(Lexema));
                            }
                        }
                        else
                        {
                            throw new ErrorSemanticoException(new ErrorConstanteNoNumericaEnTopeArreglo(Lexema));
                        }
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorNoConstanteEnTopeArreglo(Lexema));
                    }
                }
                else
                {
                    throw new ErrorSemanticoException(new ErrorUsoVariableNoDeclarada(Lexema));
                }
            }

            return this;
        }

    
        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            if (esID)
            {
                strBldr.Append(this.hijosNodo[0].LexemaVariable);
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Lexema);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
