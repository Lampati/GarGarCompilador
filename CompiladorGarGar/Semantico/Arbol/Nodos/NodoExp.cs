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
    class NodoExp: NodoArbolSemantico
    {
        public NodoExp(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

  

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            //Hago la operacion igual, si los tipos no eran iguales, simplemente tiro la excepcion.
            //Por defecto uso el tipo del primer hijo para asignar el tipo de este nodo.

            if (this.hijosNodo.Count > 1)
            {
                this.NoEsAptaPasajeReferencia = true;
                
                this.Gargar = string.Format("{0} {1} {2}", this.hijosNodo[0].Gargar, this.hijosNodo[1].Gargar, this.hijosNodo[2].Gargar);
              

                this.EsArregloEnParametro = this.hijosNodo[1].EsArregloEnParametro;

                TipoOperatoria operacion = this.hijosNodo[2].Operacion;

                this.Operacion = this.hijosNodo[0].Operacion;

                this.TipoDato = this.hijosNodo[0].TipoDato;

                this.AsignaParametros = this.hijosNodo[1].AsignaParametros || this.hijosNodo[2].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales || this.hijosNodo[2].UsaVariablesGlobales;


                if (operacion != TipoOperatoria.Ninguna)
                {

                    if (this.EsArregloEnParametro)
                    {
                        throw new ErrorSemanticoException(new ErrorOperacionesConArreglo());
                    }

                }
                else
                {
                    

                    this.Lexema = this.hijosNodo[0].Lexema;
                    

                            
                }
            }
            else
            {
                this.Gargar = string.Empty;
                this.Operacion = TipoOperatoria.Ninguna;
            }

            return this;
        }

    

        public override void ChequearAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                NodoTablaSimbolos.TipoDeDato tipo1 = this.hijosNodo[0].TipoDato;
                NodoTablaSimbolos.TipoDeDato tipo2 = this.hijosNodo[1].TipoDato;
                NodoTablaSimbolos.TipoDeDato tipo3 = this.hijosNodo[2].TipoDato;

                if (tipo3 != NodoTablaSimbolos.TipoDeDato.Ninguno)
                {
                   
                    if (tipo1 != tipo2)
                    {
                        throw new ErrorSemanticoException(new ErrorOperarExpresionesTipoIncorrecto(tipo1, tipo2));
                    }
                    else
                    {
                        if (tipo2 != tipo3)
                        {
                            throw new ErrorSemanticoException(new ErrorOperarExpresionesTipoIncorrecto(tipo2, tipo3));
                        }
                    }
                }
                else
                {
                    if (tipo1 != tipo2)
                    {
                        throw new ErrorSemanticoException(new ErrorOperarExpresionesTipoIncorrecto(tipo1, tipo2));
                    }
                }

                //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal,this.ToString());
                //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, tipo2);
            }
        }

    

       

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(this.hijosNodo[2].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
