using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos;
using CompiladorGargar.Sintactico.Gramatica;
using System.Windows.Forms;
using CompiladorGargar.Semantico.RecorredorArbol;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Resultado.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol
{
    internal class ArbolSemantico
    {
        private NodoArbolSemantico nodoRaiz;
        private NodoArbolSemantico nodoActual;

        internal ArbolSemantico(NoTerminal nt)
        {
            nodoRaiz = new NodoStart(null, nt);
            nodoRaiz.CrearTablaSimbolos();
            
            nodoActual = nodoRaiz;
        }

        internal NodoArbolSemantico ObtenerRaiz()
        {
            return nodoRaiz;
        }

        internal void AgregarHijosNodoActual(Produccion prod)
        {
            this.nodoActual.AgregarHijos(prod);
            this.nodoActual = this.nodoActual.ObtenerPrimerHijo(); 
        }

        public TablaSimbolos TablaDeSimbolos 
        {
            get
            {
                return nodoRaiz.TablaSimbolos;
            }
        }

        internal List<PasoAnalizadorSintactico> CalcularAtributos(Terminal t)
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();       

            bool continuar = true;
            while (this.nodoActual != null && continuar)
            {
                if (this.nodoActual.CalculadoAtributosHijos)
                {
                    NodoArbolSemantico nodo = this.nodoActual;
                    try
                    {
                        nodo = this.nodoActual.CalcularAtributos(t);
                        nodo.ChequearAtributos(t);
                    }
                    catch (ErrorSemanticoException ex)
                    {

                        retorno.Add(new PasoAnalizadorSintactico(ex.Message, GlobalesCompilador.TipoError.Semantico, ex.Fila, ex.Columna, false));

                        //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
                        nodo = nodo.SalvarAtributosParaContinuar();
                    }
                    catch (AggregateException exs)
                    {
                        

                        foreach (ErrorSemanticoException ex in exs.InnerExceptions)
                        {
                            retorno.Add(new PasoAnalizadorSintactico(ex.Message, GlobalesCompilador.TipoError.Semantico, ex.Fila, ex.Columna, false));
                            //this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
                        }
                        nodo = nodo.SalvarAtributosParaContinuar();
                    }

                    if (this.nodoActual != this.nodoRaiz)
                    {
                        this.nodoActual = nodo.PadreNodo;
                        this.nodoActual.ActualizarAtributos(nodo);
                        this.nodoActual = this.nodoActual.ProximoNodoACalcular();
                    }
                    else
                    {
                        continuar = false;
                    }
                }
                else
                {
                    continuar = false;
                }

            }

            return retorno;

        }

        public string CalcularCodigo(bool modoDebug)
        {
            PilaRecorredor pilaRecorredor = new PilaRecorredor();
            pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(this.nodoRaiz));

            NodoArbolSemantico nodoActual = this.nodoRaiz;

            while (!pilaRecorredor.esVacia())
            {

                NodoPilaRecorredor nodoRecorredorActual = pilaRecorredor.ObtenerTope();

                if (nodoRecorredorActual.Actual < nodoRecorredorActual.Nodo.ObtenerCantidadHijos())
                {

                    pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(nodoRecorredorActual.Nodo.ObtenerHijo(nodoRecorredorActual.Actual)));
                    nodoRecorredorActual.Actual++;

                }
                else
                {
                    nodoRecorredorActual.Nodo.CalcularCodigo(modoDebug);

                    if (!pilaRecorredor.esVacia())
                    {
                        pilaRecorredor.DescartarTope();
                    }
                }

            }

            return this.nodoRaiz.Codigo;
        }



    }
}
