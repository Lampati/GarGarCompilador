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
    class NodoAsignacion: NodoArbolSemantico
    {
        public NodoAsignacion(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            string nombre = this.hijosNodo[0].Lexema;
            bool esArreglo = this.hijosNodo[0].EsArreglo;

            Gargar = string.Format("{0} {1} {2}{3}", 
                this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar, this.hijosNodo[2].Gargar, this.hijosNodo[3].Gargar);


            NodoTablaSimbolos.TipoDeDato tipoExp = this.hijosNodo[2].TipoDato;

            NodoTablaSimbolos.TipoDeDato tipo;
            StringBuilder strbldr;

            LineaCorrespondiente = GlobalesCompilador.UltFila;


            if (!esArreglo)
            {
                if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.NombreContextoLocal);

                    if (tipo == tipoExp)
                    {
                        if (this.TablaSimbolos.EsModificableValorVarible(nombre, this.ContextoActual, this.NombreContextoLocal))
                        {

                            //esto es para agarrar que no se haga nada raro en el procedimiento salida
                            if (this.TablaSimbolos.EsParametroDeEsteProc(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.ModificaParametros = true;
                            }

                            if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.UsaVariablesGlobales = true;
                            }

                            this.AsignaParametros = this.hijosNodo[2].AsignaParametros;

                            this.UsaVariablesGlobales = this.UsaVariablesGlobales || this.hijosNodo[2].UsaVariablesGlobales;
                          
                        }
                        else
                        {
                            throw new ErrorSemanticoException(new ErrorUsoConstanteComoVariable(nombre));
                        }
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorVariableAsignarTipoInvalido(nombre, tipo, tipoExp));
                    }
                                      
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
                  
                    tipo = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);

                    if (tipo == tipoExp)
                    {
                        //this.TablaSimbolos.ModificarValorPosicionArreglo(nombre, indice, valorExp);

                        //esto es para agarrar que no se haga nada raro en el procedimiento salida
                        if (this.TablaSimbolos.EsParametroDeEsteProc(nombre, this.ContextoActual, this.NombreContextoLocal))
                        {
                            this.ModificaParametros = true;
                        }

                        if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                        {
                            this.UsaVariablesGlobales = true;
                        }

                        this.AsignaParametros = this.hijosNodo[2].AsignaParametros;                          
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new ErrorArregloAsignarTipoInvalido(nombre, tipo, tipoExp ));
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

    
       

        public override void ChequearAtributos(Terminal t)
        {
            
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(LineaCorrespondiente));

            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(" := ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();

        }


    }
}
