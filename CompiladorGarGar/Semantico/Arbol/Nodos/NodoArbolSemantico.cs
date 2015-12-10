using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Reflection;
using System.Collections;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using System.Diagnostics;


using System.ComponentModel;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    abstract class NodoArbolSemantico 
    {
        public enum TipoOperatoria
        {
            Suma,
            Resta,
            Multiplicacion,
            Division,
            Concatenacion,
            And,
            Or,
            Ninguna
        }

        public enum TipoComparacion
        {
            //Entero
            Mayor,
            MayorIgual,            
            Menor,
            MenorIgual,            
            Igual,
            Distinto,            
            None
        }

        public enum TipoDeclaracionesPermitidas
        {
            //Entero
            Variables,
            Constantes,
            Ninguno
        }

    

        public TablaSimbolos TablaSimbolos { get; set; }
        public NodoTablaSimbolos.TipoDeDato TipoDato { get; set; }
        public bool EsConstante { get; set; }
        public NodoTablaSimbolos.TipoContexto ContextoActual { get; set; }
        public bool ProcPrincipalCrearUnaVez { get; set; }
        public bool ProcPrincipalYaCreadoyCorrecto {get; set;}

        public string LexemaVariable
        {
            get
            {
                return string.Format("{0}{1}", GlobalesCompilador.PREFIJO_VARIABLES, Lexema);
            }   
        }


        public string Lexema { get; set; }
        public string NombreContextoLocal { get; set; }
        public bool EsArreglo { get; set; }
        public bool EsReferencia { get; set; }
        public string RangoArreglo { get; set; }
        public string RangoArregloSinPrefijo { get; set; }
        public string NombreTipoArreglo { get; set; }
        public bool EsFuncion { get; set; }
        public List<Variable> VariablesACrear { get; set; }
        public List<Firma> ListaFirma { get; set; }
        public TipoOperatoria Operacion { get; set; }
        public double ValorConstanteNumerica { get; set; }
        public string ValorConstanteTexto { get; set; }
        public bool NoEsAptaPasajeReferencia { get; set; }

        public bool ModificaParametros { get; set; }
        public bool AsignaParametros { get; set; }
        public bool LlamaProcs { get; set; }
        public bool TieneLecturas { get; set; }
        public bool UsaVariablesGlobales { get; set; }
        public bool EsPasajeParametrosAProcOFunc { get; set; }
        public bool EsArregloEnParametro { get; set; }

        public TipoDeclaracionesPermitidas DeclaracionesPermitidas { get; set; }

        //Entrega 4

        //sintetizado
        public bool EsFirma { get; set; }
        public string VariablesProcPrincipal { get; set; }
        public string VariablesGlobales { get; set; }
        public string ConstantesGlobales { get; set; }
        
        public List<string> ListaElementosVisualizar { get; set; }
        public TipoComparacion Comparacion { get; set; }
        public bool EsSino { get; set; }

        public int LineaCorrespondiente { get; set; }

       
        public string Codigo { get; set; }


        public string Gargar { get; set; }



        protected ElementoGramatica elemento;

        protected NodoArbolSemantico padreNodo;
        public NodoArbolSemantico PadreNodo
        {
            get
            {
                return padreNodo;
            }
        }

        protected List<NodoArbolSemantico> hijosNodo;


        protected bool inicializado;
        public bool CalculadoAtributosHijos
        {
            get
            {
                return AtributosCalculadosEnNodo() && inicializado;
            }

        }


        //public bool ChequeoDeCalculadosEnNodo

        protected bool[] atributosCalculados;

        public int ObtenerCantidadHijos()
        {
            return this.hijosNodo.Count();
        }

        public NodoArbolSemantico ObtenerHijo(int i)
        {
            if (i < this.hijosNodo.Count)
            {
                return this.hijosNodo[i];
            }
            else
            {
                return null;
            }
        }

        internal void CrearTablaSimbolos()
        {
            this.TablaSimbolos = new TablaSimbolos();


            // flanzani 8/11/2012
            // IDC_APP_2
            // Agregar funciones por defecto en el framework
            // Se agregan las librerias
            GlobalesCompilador.AgregarLibreriasFramework(this.TablaSimbolos);	
        }

        public NodoArbolSemantico(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
        {
            this.elemento = elem;
            this.padreNodo = nodoPadre;
            this.hijosNodo = new List<NodoArbolSemantico>();

            inicializado = false;

            //Sintetizados
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Ninguno;
            this.Comparacion = TipoComparacion.None;
            
            this.EsFuncion = false;
            this.EsArreglo = false;
            this.Operacion = TipoOperatoria.Ninguna;
            this.Codigo = string.Empty;

            this.EsArregloEnParametro = false;

            this.ValorConstanteNumerica = double.MinValue;
            
            this.ListaElementosVisualizar = new List<string>();

            this.LlamaProcs = false;
            this.ModificaParametros = false;
            this.TieneLecturas = false;
            this.AsignaParametros = false;
            this.UsaVariablesGlobales = false;

            //Heredados
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Ninguno;
            this.EsFirma = false;
            this.EsPasajeParametrosAProcOFunc = false;

            if (nodoPadre != null)
            {
                this.EsConstante = nodoPadre.EsConstante;
                this.ContextoActual = nodoPadre.ContextoActual;
                this.TablaSimbolos = nodoPadre.TablaSimbolos;
                this.NombreContextoLocal = nodoPadre.NombreContextoLocal;
                this.ProcPrincipalYaCreadoyCorrecto = nodoPadre.ProcPrincipalYaCreadoyCorrecto;
                this.ProcPrincipalCrearUnaVez = nodoPadre.ProcPrincipalCrearUnaVez;
                this.DeclaracionesPermitidas = nodoPadre.DeclaracionesPermitidas;
                this.EsPasajeParametrosAProcOFunc = nodoPadre.EsPasajeParametrosAProcOFunc;
            }
        }

        public void AgregarHijos(Produccion prod)
        {
            atributosCalculados = new bool[prod.Der.Count];

            inicializado = true;

            for (int i = 0; i < prod.Der.Count; i++)
            {
                //atributosCalculados[i] = (prod.Der[i].GetType() == typeof(Terminal)) ? true : false;
                atributosCalculados[i] = false;

                if (prod.Der[i].GetType() == typeof(Terminal))
                {
                    this.hijosNodo.Add(new NodoTerminal(this,(Terminal)prod.Der[i]));
                }
                else
                {
                    try
                    {
                        Type t = Type.GetType("CompiladorGARGAR.Semantico.Arbol.Nodos.Nodo" + prod.Der[i].ToString(), true, true);

                        ConstructorInfo ci = t.GetConstructor(new Type[] { typeof(NodoArbolSemantico), typeof(ElementoGramatica) });

                        this.hijosNodo.Add((NodoArbolSemantico)ci.Invoke(new object[] { this, prod.Der[i] }));
                        //this.hijosNodo.Add(new NodoPasaManos(this, prod.Der[i]));
                    }
                    catch (Exception ex)
                    {
                        Utils.Log.AddError(ex.Message);
                        this.hijosNodo.Add((NodoArbolSemantico)new NodoPasaManos(this, prod.Der[i]));
                    }
                }

                
            }
        }


        private bool AtributosCalculadosEnNodo()
        {
            bool retorno = true;
            int i = 0;
            while (i < this.hijosNodo.Count && retorno == true)
            {
                retorno = atributosCalculados[i];
                i++;
            }
            return retorno;
        }

        private bool ChequeoDeCalculadosEnNodo(bool[] arregloAControlar)
        {
            bool retorno = true;
            int i = 0;
            while (i < this.hijosNodo.Count && retorno == true)
            {
                retorno = arregloAControlar[i];
                i++;
            }
            return retorno;
        }

        internal NodoArbolSemantico ObtenerPrimerHijo()
        {
            this.hijosNodo[0].TablaSimbolos = this.TablaSimbolos;
            return this.hijosNodo[0];
        }
    

        internal void ActualizarAtributos(NodoArbolSemantico nodo)
        {
            this.TablaSimbolos = nodo.TablaSimbolos;
            this.NombreContextoLocal = nodo.NombreContextoLocal;
            this.ProcPrincipalYaCreadoyCorrecto = nodo.ProcPrincipalYaCreadoyCorrecto;
            this.ProcPrincipalCrearUnaVez = nodo.ProcPrincipalCrearUnaVez;

 	        int i = this.hijosNodo.IndexOf(nodo);
            this.atributosCalculados[i] = true;

            this.hijosNodo[i] = nodo;
        }
    
        internal NodoArbolSemantico ProximoNodoACalcular()
        {
            bool retorno = true;
            int i = 0;
            while (i < this.hijosNodo.Count && retorno == true)
            {
                if (atributosCalculados[i] == false)
                {
                    retorno = false;
                }
                else
                {
                    i++;
                }
            }

            if (i < this.hijosNodo.Count)
            {
                this.HeredarAtributosANodo(this.hijosNodo[i]);
                
                return this.hijosNodo[i];
            }
            else
            {
                return this;
            }
 	       
        }


        public virtual NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            foreach (NodoArbolSemantico nodo in this.hijosNodo)
            {
                int i = this.hijosNodo.IndexOf(nodo);

                if (this.atributosCalculados[i])
                {
                    //poner los sintetizados aca
                    this.SintetizarAtributosANodo(this.hijosNodo[i]);
                    
                }
            }
            return this;
        }

        public virtual void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public virtual void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
        }

        public virtual void ChequearAtributos(Terminal t)
        {
            
        }

        public virtual NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override string ToString()
        {
            return this.elemento.ToString();
        }
        
        public virtual void CalcularCodigo()
        {
            
        }
    }
}
