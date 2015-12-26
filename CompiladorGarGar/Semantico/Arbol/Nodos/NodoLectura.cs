using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Sintactico.ErroresManager.Errores;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLectura : NodoArbolSemantico
    {
        public NodoLectura(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {
            this.TieneLecturas = true;
        }

     
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            string nombre = this.hijosNodo[1].Lexema;
            bool esArreglo = this.hijosNodo[1].EsArreglo;

            NodoTablaSimbolos.TipoDeDato tipo;
            StringBuilder strbldr;


            LineaCorrespondiente = GlobalesCompilador.UltFila;

            if (!esArreglo)
            {
                if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre, this.ContextoActual, this.NombreContextoLocal);

                    
                    if (this.TablaSimbolos.EsModificableValorVarible(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {                    

                        if (tipo == NodoTablaSimbolos.TipoDeDato.Booleano)
                        {
                            throw new ErrorSemanticoException(new ErrorIntentarLeerVariableBooleana(nombre));
                        }
               
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorUsoConstanteComoVariable(nombre));
                    }
                    
                    //else
                    //{
                    //    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                    //    strbldr.Append(" pero la funcion leer lee solo enteros.");
                    //    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    //}
                }
                else
                {
                    if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        throw new ErrorSemanticoException(new ErrorUsoArregloSinIndice(nombre));
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorUsoVariableNoDeclarada(nombre));
                    }
                }
            }
            else
            {
                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    //if (this.TablaSimbolos.ExisteArreglo(nombre, indice))
                    //{
                    tipo = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);

                    

                    this.Lexema = nombre;
                        

                    if (tipo == NodoTablaSimbolos.TipoDeDato.Booleano)
                    {
                        throw new ErrorSemanticoException(new ErrorIntentarLeerVariableBooleana(nombre));

                    }
                   
                }
                else
                {
                    //mejora de error. Me fijo si no ta declarada ya como arreglo o variable
                    if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        throw new ErrorSemanticoException(new ErrorUsoVariableComoArreglo(nombre));
                        
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorUsoVariableNoDeclarada(nombre));
                    }

                    
                }
            }

            return this;
        }


     

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(LineaCorrespondiente));

            strBldr.Append("Readln ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[1].Codigo);
            strBldr.Append(") ");
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();
        }
    }
}
