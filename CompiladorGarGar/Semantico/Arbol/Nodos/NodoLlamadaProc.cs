using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLlamadaProc: NodoArbolSemantico
    {
        public NodoLlamadaProc(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
            this.LlamaProcs = true;
        }

      
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {

            List<Firma> listaFirmaComparar = this.hijosNodo[3].ListaFirma;  
            this.ListaFirma = listaFirmaComparar;
            string nombre = this.hijosNodo[1].Lexema;
            this.Lexema = nombre;


            LineaCorrespondiente = GlobalesCompilador.UltFila;

            StringBuilder strbldr;

            if (this.TablaSimbolos.ExisteProcedimiento(nombre))
            {
                List<FirmaProc> firmaFuncion = this.TablaSimbolos.ObtenerFirma(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento);

                if (firmaFuncion.Count == listaFirmaComparar.Count)
                {
                    int i = 0;
                    bool igual = true;
                    bool igualReferencia = true;
                    bool noEsConstanteYPorRef = true;


                    while (i < firmaFuncion.Count && igual && igualReferencia && noEsConstanteYPorRef)
                    {
                        igual = firmaFuncion[i].TipoDato == listaFirmaComparar[i].Tipo
                            && firmaFuncion[i].EsArreglo == listaFirmaComparar[i].EsArreglo;

                        igualReferencia = !firmaFuncion[i].EsPorReferencia || firmaFuncion[i].EsPorReferencia == listaFirmaComparar[i].EsReferencia;
                       
                        noEsConstanteYPorRef = !(firmaFuncion[i].EsPorReferencia && listaFirmaComparar[i].EsConstante);

                        i++;
                    }

                    if (igual && igualReferencia && noEsConstanteYPorRef)
                    {
                        this.TipoDato = this.TablaSimbolos.ObtenerTipoProcedimiento(nombre);
                        //this.Valor = 1;
                       
                      
                    }                 
                    else
                    {
                        List<ErrorSemanticoException> listaExcepciones = new List<ErrorSemanticoException>();

                        strbldr = new StringBuilder();
                        if (firmaFuncion[i - 1].TipoDato != listaFirmaComparar[i - 1].Tipo)
                        {
                            listaExcepciones.Add(new ErrorSemanticoException(new ErrorParametroTipoIncorrectoEnProc(firmaFuncion[i - 1].Lexema, nombre, firmaFuncion[i - 1].TipoDato)));
                        }

                        if (firmaFuncion[i - 1].EsPorReferencia != listaFirmaComparar[i - 1].EsReferencia)
                        {

                            listaExcepciones.Add(new ErrorSemanticoException(new ErrorParametroExpresionPorRefEnProc(firmaFuncion[i - 1].Lexema, nombre)));


                        }

                        if (firmaFuncion[i - 1].EsPorReferencia && listaFirmaComparar[i - 1].EsConstante)
                        {

                            listaExcepciones.Add(new ErrorSemanticoException(new ErrorParametroConstantePorRefEnProc(firmaFuncion[i - 1].Lexema, nombre)));
                        }

                        if (firmaFuncion[i - 1].EsArreglo != listaFirmaComparar[i - 1].EsArreglo)
                        {
                            if (firmaFuncion[i - 1].EsArreglo)
                            {
                                listaExcepciones.Add(new ErrorSemanticoException(new ErrorParametroVariableEnLugarDeArregloEnProc(firmaFuncion[i - 1].Lexema, nombre)));
                            }
                            else
                            {
                                listaExcepciones.Add(new ErrorSemanticoException(new ErrorParametroArregloEnLugarDeVariableEnProc(firmaFuncion[i - 1].Lexema, nombre)));

                            }
                        }

                        if (listaExcepciones.Count > 0)
                        {
                            throw new AggregateException(listaExcepciones);
                        }
                    }

                }
                else
                {
                    throw new ErrorSemanticoException(new ErrorCantidadIncorrectaParamentrosProcedimiento(nombre));
                }
            }
            else
            {
                throw new ErrorSemanticoException(new ErrorUsoProcedimientoNoDeclarado(nombre));
            }

            return this;
        }


    

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(LineaCorrespondiente));

            strBldr.Append(this.hijosNodo[1].Codigo);
            strBldr.Append("(");
            strBldr.Append(this.hijosNodo[3].Codigo);
            strBldr.Append(")");
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();

        }
    }


   
}
