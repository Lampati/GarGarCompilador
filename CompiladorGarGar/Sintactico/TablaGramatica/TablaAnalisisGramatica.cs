using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Auxiliares;


namespace CompiladorGargar.Sintactico.TablaGramatica
{
    class TablaAnalisisGramatica
    {

        private List<NodoTablaAnalisisGramatica> produccionesDeLaTabla;



        public TablaAnalisisGramatica()
        {
            this.produccionesDeLaTabla = new List<NodoTablaAnalisisGramatica>();
        }




        public void AgregarPrimeros(NoTerminal nt, List<Terminal> termsPrim, Produccion prod)
        {
            foreach (Terminal t in termsPrim)
           {
               if (t.NoEsLambda() && prod.Der != null)
               {
                   this.produccionesDeLaTabla.Add(new NodoTablaAnalisisGramatica(nt, t, prod));
               }
           }            
        }

        public void AgregarSiguientes(NoTerminal nt, List<Terminal> termsSig)
        {
            foreach (Terminal t in termsSig)
            {
                Produccion prod = new Produccion();
                prod.Izq = nt;

                Terminal term = new Terminal();
                term.Componente = new ComponenteLexico();
                term.Componente.Token = ComponenteLexico.TokenType.Ninguno;
                prod.Der.Add(term);

                this.produccionesDeLaTabla.Add(new NodoTablaAnalisisGramatica(nt, t,prod));                
            }
        }

        internal void AgregarSincronizacion(NoTerminal nt, List<Terminal> termsSig)
        {
            foreach (Terminal t in termsSig)
            {
                StringBuilder strBldr = new StringBuilder("Descarto el no terminal ").Append(nt.ToString());
                strBldr.Append(" de la pila ").Append(" pq viene el terminal ").Append(t.ToString());

                if (this.BuscarNodo(nt, t) == null)
                {
                    NodoTablaAnalisisGramatica nodo = new NodoTablaAnalisisGramatica(nt, t, strBldr.ToString());

                    this.produccionesDeLaTabla.Add(nodo);                    
                }
            }
        }

        public Produccion BuscarEnTablaProduccion(NoTerminal x, Terminal y, bool reportarErrores, bool generaProdVacia)
        {
            NodoTablaAnalisisGramatica nodo = this.EncontrarNodo(x,y);

            if (nodo != null)
            {
                if (!nodo.EsSinc)
                {
                    return nodo.Produccion;
                }
                else
                {
                    if (reportarErrores)
                    {
                        this.ErrorSintacticoSinMostrar(y.Componente.Fila, y.Componente.Columna,y, nodo.DescripcionError, false, true );
                    }
                    return null;
                }
            }
            else
            {
                if (reportarErrores)
                {
                    if (!generaProdVacia)
                    {
                        this.ErrorSintactico(y.Componente.Fila, y.Componente.Columna, y, true, false);
                    }
                    else
                    {
                        Produccion prod = new Produccion();
                        prod.Izq = x;

                        Terminal term = new Terminal();
                        term.Componente = new ComponenteLexico();
                        term.Componente.Token = ComponenteLexico.TokenType.Ninguno;
                        prod.Der.Add(term);

                        return prod;
                    }
                }
                return null;
            }
        }



        private void ErrorSintactico(int fila, int columna, Terminal t, bool descartarCadena, bool descartarPila)
        {
            //StringBuilder bldr = new StringBuilder("Error Sintactico en el token ");
            //bldr.Append(EnumUtils.stringValueOf( t.Componente.Token));
            //bldr.Append(".");

            StringBuilder bldr = new StringBuilder("Error Sintactico. La linea no esta correctamente formada."); 
            this.ErrorSintactico(fila,columna,t, bldr.ToString(),descartarCadena,descartarPila);
        }

        private void ErrorSintactico(int fila, int columna, Terminal t, string error, bool descartarCadena, bool descartarPila)
        {
            bool parar = true;
            if (t.Equals(Terminal.ElementoEOF()))
            {
                parar = true;
                //error = "La cadena de entrada termino y la pila no puede vaciarse. El analisis termina con error.";
                error = "Error sintactico. El programa esta mal formado.";
                //throw new ErrorSintacticoException("La cadena de entrada termino y la pila no puede vaciarse. El analisis termina con error.", fila, columna, false, true,true);
            }
            throw new ErrorSintacticoException(error, fila, columna, descartarPila, descartarCadena, parar);
        }

        private void ErrorSintacticoSinMostrar(int fila, int columna, Terminal t, string error, bool descartarCadena, bool descartarPila)
        {
            bool parar = false;
            if (t.Equals(Terminal.ElementoEOF()))
            {
                parar = true;
                //error = "La cadena de entrada termino y la pila no puede vaciarse. El analisis termina con error.";
                error = "Error sintactico. El programa esta mal formado.";
                //throw new ErrorSintacticoException("La cadena de entrada termino y la pila no puede vaciarse. El analisis termina con error.", fila, columna, false, true,true);
            }
            throw new ErrorSintacticoException(error, fila, columna, descartarPila, descartarCadena, parar,true);
        }

        private NodoTablaAnalisisGramatica EncontrarNodo(NoTerminal x, Terminal y)
        {
            return this.produccionesDeLaTabla.Find(

                delegate(NodoTablaAnalisisGramatica _nodo)
                {
                    if (_nodo.Terminal.Equals(y) && _nodo.NoTerminal.Equals(x))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            );
        }





        internal NodoTablaAnalisisGramatica BuscarNodo(NoTerminal nt, Terminal t)
        {
            return this.EncontrarNodo(nt,t);
        }


    }
}
