using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Lexicografico;
using System.Diagnostics;

namespace CompiladorGargar.Sintactico.TablaPrimerosSiguientes
{
    class TablaPrimeros
    {
        private List<NodoTablaPrimeros> nodos;


        public TablaPrimeros()
        {            
            this.nodos = new List<NodoTablaPrimeros>();
        }

        public void AgregarNodo(NoTerminal nt, Produccion prod, List<Terminal> terms)
        {
            this.nodos.Add(new NodoTablaPrimeros(nt,prod,terms));
        }

        public RetornoPrimeros Primeros(NoTerminal nt, Produccion prod, bool enDerecha)
        {
            if (enDerecha)
            {
                //return PrimerosDerecha(nt, prod);
                return PrimerosParaSiguientes(nt, prod);
            }
            else
            {
                return PrimerosIzquierda(nt, prod);
            }
        }

        private RetornoPrimeros PrimerosParaSiguientes(NoTerminal nt, Produccion prod)
        {
            RetornoPrimeros retorno = new RetornoPrimeros();

            if (prod.Der != null)
            {
                retorno.Terminales.AddRange(this.PrimerosDe(nt));
            }
            else
            {
                retorno.Terminales.Add(Terminal.ElementoVacio());
            }

            if (retorno.Terminales.Contains(Terminal.ElementoVacio()))
            {
                retorno.EsNecesarioSiguiente = true;
                retorno.NoTerminal = nt;
            }

            return retorno;
        }

        private RetornoPrimeros PrimerosIzquierda(NoTerminal nt, Produccion prod)
        {
            RetornoPrimeros retorno = new RetornoPrimeros();
            if (prod.Der != null)
            {
                retorno.Terminales.AddRange(this.PrimerosDe(nt, prod));
            }
            else
            {
                retorno.Terminales.Add(Terminal.ElementoVacio());
            }

            return retorno;
        }

        //Metodo publico, que se fija el primero del NoTerminal elegido en la parte derecha produccion
        private RetornoPrimeros PrimerosDerecha(NoTerminal nt, Produccion prod)
        {
            RetornoPrimeros retorno = new RetornoPrimeros();            

            List<Terminal> terminales = new List<Terminal>();

            int i = prod.Der.IndexOf(nt);
           
            Debug.Assert(i >= 0, "El indice del metodo Primeros en TablePrimeros era menor a 0", "el terminal " + nt.ToString() + " no figura en la derecha de la prod " + prod.ToString());

            bool parar = false;

            while (i < prod.Der.Count && !parar)
            {
                if (prod.Der[i].GetType() == typeof(NoTerminal))
                {
                    List<Terminal> terminalesAux = new List<Terminal>();

                    terminalesAux = this.PrimerosDe((NoTerminal)prod.Der[i]);

                    terminales.AddRange(terminalesAux);

                    if (!terminales.Contains(Terminal.ElementoVacio()))
                    {
                        parar = true;
                    }
                }
                else
                {
                    terminales.Add((Terminal)prod.Der[i]);
                    parar = true;
                }
                i++;
            }

            if (!parar)
            {
                retorno.EsNecesarioSiguiente = true;
                retorno.NoTerminal = prod.Izq;
            }

            retorno.Terminales = terminales;

            return retorno;
        }

        private List<Terminal> PrimerosDe(NoTerminal nt)
        {
            List<Terminal> listaTerminales = new List<Terminal>();

            foreach (NodoTablaPrimeros nd in this.ObtenerNodos(nt))
            {
                foreach (Terminal t in nd.Terminales)
                {
                    if (!listaTerminales.Contains(t))
                    {
                        listaTerminales.Add(t);
                    }
                }
            }

            return listaTerminales;
        }

        private List<Terminal> PrimerosDe(NoTerminal nt, Produccion prod)
        {
            List<Terminal> listaTerminales = new List<Terminal>();

            NodoTablaPrimeros nd = this.ObtenerNodo(nt, prod);
            
            listaTerminales.AddRange(nd.Terminales);            

            return listaTerminales;
        }

        private NodoTablaPrimeros ObtenerNodo(NoTerminal nt, Produccion prod)
        {
            return this.nodos.Find(

                delegate(NodoTablaPrimeros _nd)
                {
                    return _nd.NT.Equals(nt) && _nd.Prod.Equals(prod);
                }

            );

        }

        private IEnumerable<NodoTablaPrimeros> ObtenerNodos(NoTerminal nt)
        {
            return this.nodos.FindAll(

                delegate(NodoTablaPrimeros _nd)
                {
                    return _nd.NT.Equals(nt);
                }

            );
        }

        public void CompletarTabla()
        {
            try
            {
                while (!this.tablaCompleta())
                {
                    foreach (NodoTablaPrimeros nodo in this.nodos)
                    {
                        //if (nodo.NT.Nombre == "MULT")
                        //{
                        //    Debugger.Break();
                        //}


                        List<Terminal> terminales = new List<Terminal>();

                        int i = 0;
                        bool parar = false;

                        if (nodo.Prod.Der != null)
                        {
                            while (i < nodo.Prod.Der.Count && !parar)
                            {
                                Debug.Assert(nodo.Prod != null);


                                List<Terminal> terminalesAux = new List<Terminal>();

                                if (nodo.Prod.Der[i].GetType() == typeof(NoTerminal))
                                {
                                    terminalesAux = this.PrimerosDe((NoTerminal)nodo.Prod.Der[i]);

                                    foreach (Terminal termAux in terminalesAux)
                                    {
                                        if ((!terminales.Contains(termAux)) && (!termAux.Equals(Terminal.ElementoVacio())))
                                        {
                                            terminales.Add(termAux);
                                        }
                                    }


                                    if (!terminalesAux.Contains(Terminal.ElementoVacio()))
                                    {
                                        parar = true;
                                    }
                                }
                                else
                                {
                                    terminales.Add((Terminal)nodo.Prod.Der[i]);
                                    parar = true;
                                }
                                i++;
                            }

                            //Para preguntar si nunca se paro por lambda, tengo que ver como se hace
                            if (!parar)
                            {
                                terminales.Add(Terminal.ElementoVacio());
                            }

                        }
                        else
                        {
                            terminales.Add(Terminal.ElementoVacio());
                        }

                        List<Terminal> listaSinRepetidos = terminales.Distinct().ToList();

                        if (nodo.Terminales.Count != listaSinRepetidos.Count)
                        {
                            nodo.Modificado = true;
                            nodo.Terminales = new List<Terminal>(listaSinRepetidos);
                        }
                        else
                        {
                            nodo.Modificado = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
            }
        }

        private bool tablaCompleta()
        {
            bool modificada = false;
            int i=0;

            while (!modificada && i < this.nodos.Count)
            {
                if (nodos[i].Modificado)
                {
                    modificada = true;
                }
                i++;
            }
            
            return !modificada;          
        }

    }
}
