using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoId: NodoArbolSemantico
    {
        public NodoId(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

    

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {            
            StringBuilder strbldr;           
            
            bool esArreglo = this.hijosNodo[1].EsArreglo;
            bool esFuncion = this.hijosNodo[1].EsFuncion;

            string nombre = this.hijosNodo[0].Lexema;

            this.Gargar = string.Format("{0}{1}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar);

            if (esFuncion)
            {
                this.NoEsAptaPasajeReferencia = true;

                List<Firma> listaFirmaComparar = this.hijosNodo[1].ListaFirma;

                if (this.TablaSimbolos.ExisteFuncion(nombre))
                {
                    List < FirmaProc > firmaFuncion = this.TablaSimbolos.ObtenerFirma(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion);

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


                            igualReferencia = !firmaFuncion[i].EsPorReferencia ||  firmaFuncion[i].EsPorReferencia == listaFirmaComparar[i].EsReferencia;

                            noEsConstanteYPorRef = !(firmaFuncion[i].EsPorReferencia && listaFirmaComparar[i].EsConstante);

                            i++;
                        }

                        if (igual && igualReferencia && noEsConstanteYPorRef)
                        {
                            this.ListaFirma = this.hijosNodo[1].ListaFirma;

                            this.EsFuncion = true;
                            this.TipoDato = this.TablaSimbolos.ObtenerTipoFuncion(nombre);
                            //this.Valor = 1;


                            this.Lexema = nombre;

                            if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.UsaVariablesGlobales = true;
                            }

                            // flanzani 9/1/2012
                            // Uso de variables globales
                            // Me fijo si se esta usando una variable global como parametro.

                            this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales;
                            
                            
                        }
                        else
                        {
                            List<ErrorSemanticoException> listaExcepciones = new List<ErrorSemanticoException>();

                            strbldr = new StringBuilder();
                            if (firmaFuncion[i - 1].TipoDato != listaFirmaComparar[i - 1].Tipo)
                            {

                                strbldr = new StringBuilder("Se intento pasar un tipo incorrecto al parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" de la función ");
                                strbldr.Append(nombre).Append(". Debe ser de tipo ").Append(EnumUtils.stringValueOf(firmaFuncion[i - 1].TipoDato));
                                listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                                
                            }

                            if (firmaFuncion[i - 1].EsPorReferencia != listaFirmaComparar[i - 1].EsReferencia)
                            {

                                strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" de la función ");
                                strbldr.Append(nombre).Append(" esta especificado como por referencia y se le intento pasar una expresion. El parametro no puede ser el resultado de una expresion o un valor constante o una función. De ser necesario, asigne el valor a una nueva variable para pasarla por parametro.");
                                listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));


                            }

                            if (firmaFuncion[i - 1].EsPorReferencia && listaFirmaComparar[i - 1].EsConstante)
                            {

                                strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" de la función ");
                                strbldr.Append(nombre).Append(" esta definido por referencia, y se le intento pasar una constante. No se pueden pasar constantes por referencia.");
                                listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                            }

                            if (firmaFuncion[i - 1].EsArreglo != listaFirmaComparar[i - 1].EsArreglo)
                            {
                                if (firmaFuncion[i - 1].EsArreglo)
                                {
                                    strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la funcion ");
                                    strbldr.Append(nombre).Append(" debe ser un arreglo, y se paso una variable.");
                                    listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                                }
                                else
                                {
                                    strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la funcion ");
                                    strbldr.Append(nombre).Append(" debe ser una variable, y se paso una arreglo.");
                                    listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));

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
                        strbldr = new StringBuilder("La cantidad de parametros para la funcion ").Append(nombre).Append(" es incorrecta.");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }
                else
                {
                    strbldr = new StringBuilder("La funcion ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }

            }
            else if (esArreglo)
            {
                

                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    this.EsArreglo = true;
                    this.TipoDato = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);
                    //this.Valor = this.TablaSimbolos.ObtenerValorPosicionArreglo(nombre, indice);

                    //flanzani 21/03/2012
                    //Muy dificil de lograr... es neceario evaluar expresionaes en tiempo de ejecucion.

                    //string indice = this.hijosNodo[1].RangoArregloSinPrefijo;
                    //int indiceEntero;

                    //if (int.TryParse(indice, out indiceEntero))
                    //{
                    //    int tope = this.TablaSimbolos.ObtenerTopeArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);

                    //    if (indiceEntero < 1 || indiceEntero > tope)
                    //    {
                    //        strbldr = new StringBuilder("Se esta intentando acceder a una posicion invalida del arreglo ").Append(nombre);                            
                    //        throw new ErrorSemanticoException(strbldr.ToString());
                    //    }
                    //}

                    
              
                    


                    this.Lexema = nombre;
                    

                    if (this.TablaSimbolos.EsParametroDeEsteProc(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        this.AsignaParametros = true;
                    }

                    if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        this.UsaVariablesGlobales = true;
                    }
                }
                else
                {
                    //mejora de error. Me fijo si no ta declarada ya como arreglo o variable
                    if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" esta declarada como variable y se intento usar como arreglo");
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                    }

                    
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            else
            {
                if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    this.TipoDato = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.NombreContextoLocal);
                    //this.Valor = this.TablaSimbolos.ObtenerValorVariable(nombre);          
                    this.EsConstante =  !this.TablaSimbolos.EsModificableValorVarible(nombre, this.ContextoActual, this.NombreContextoLocal);

                    this.Lexema = nombre;
                        
                    if (this.TablaSimbolos.EsParametroDeEsteProc(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        this.AsignaParametros = true;
                    }

                    if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        this.UsaVariablesGlobales = true;
                    }
                }
                else
                {
                    // Pq puede ser que haya puesto el arreglo sin el subindice
                    if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        if (this.EsPasajeParametrosAProcOFunc)
                        {
                            this.EsArregloEnParametro = true;
                            this.TipoDato = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);
                            //this.Valor = this.TablaSimbolos.ObtenerValorPosicionArreglo(nombre, indice);
                            this.Lexema = nombre;

                            if (this.TablaSimbolos.EsParametroDeEsteProc(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.AsignaParametros = true;
                            }

                            if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.UsaVariablesGlobales = true;
                            }
                        }
                        else
                        {
                            strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es un arreglo. Debe usar un indice para asignarle el contenido");
                            strbldr.Append(" a una de sus posiciones. No se puede asignar el contenido total de un arreglo a otro. ");
                            throw new ErrorSemanticoException(strbldr.ToString());
                        }
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }
            }
            


            return this;
        }

      
        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {            
            //Por defecto coloco entero y un 1 para no alterar demasiado
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Numero;

            return this;
        }


        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();
            

            if (this.hijosNodo[1].ObtenerCantidadHijos() > 1)
            {
                if (this.EsArreglo)
                {
                    strBldr.Append(this.hijosNodo[0].LexemaVariable);
                    strBldr.Append("[");
                    strBldr.Append(string.Format("FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo({0},'{1}')", this.hijosNodo[1].Codigo, this.hijosNodo[0].Lexema));
                    strBldr.Append("]");
                }
                else
                {
                    string nombre = this.hijosNodo[0].LexemaVariable;

                    
                    if (this.TablaSimbolos.EsNombreFuncionFramework(this.hijosNodo[0].Lexema))
                    {
                        nombre = this.TablaSimbolos.ObtenerNombrePascalFuncionFramework(this.hijosNodo[0].Lexema);
                    }

                    strBldr.Append(nombre);

                    strBldr.Append("(");
                    strBldr.Append(this.hijosNodo[1].Codigo);
                    strBldr.Append(")");
                }
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].LexemaVariable);
            }

            //strBldr.Append(this.hijosNodo[1].Codigo);
            this.Codigo = strBldr.ToString();
        }
    }
}
