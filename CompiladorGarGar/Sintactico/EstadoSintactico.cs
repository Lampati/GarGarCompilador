using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico
{
    internal enum ContextoGlobal
    {
        Global,
        GlobalDeclaracionesConstantes,
        GlobalDeclaracionesVariables,
        DeclaracionLocal,
        Cuerpo
        
    }

   

    internal enum ContextoLinea
    {
        Asignacion,
        Leer,
        LlamadaProc,
        Mientras,
        Si,
        Sino,
        DeclaracionFuncion,
        DeclaracionProc,
        DeclaracionConstante,
        DeclaracionVariable,
        FinFuncion,
        FinProc,
        FinMientras,
        FinSi,
        Mostrar,
        
        Ninguno

    }


    internal enum ElementoPila
    {
        Mientras,
        Si,
        Sino,
        Procedimiento,
        Funcion
    }

    internal static class EstadoSintactico
    {
        private static bool esProcPrincipal = false;

        

        private static List<Terminal> listaLeidoHastaAhora = new List<Terminal>();

        private static List<Terminal> listaLineaActual = new List<Terminal>();

  
        private static ContextoGlobal contextoGlobal = Sintactico.ContextoGlobal.Global;
        internal static ContextoGlobal ContextoGlobal
        {
            get { return contextoGlobal; }
            set { contextoGlobal = value; }
        }

        private static ContextoLinea contextoLinea = ContextoLinea.Ninguno;
        internal static ContextoLinea ContextoLinea
        {
            get { return contextoLinea; }
            set { contextoLinea = value; }
        }

        

        private static Stack<ElementoPila> pila = new Stack<ElementoPila>();

        public static string LineaActual
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();
                foreach (var item in listaLineaActual)
                {
                    strBldr.Append(item.Componente.Lexema).Append(" ");
                }
                return strBldr.ToString();
            }
        }

        public static List<Terminal> ListaLineaActual
        {
            get
            {
                return listaLineaActual;
            }
        }

        public static ElementoPila TopePilaLlamados
        {
            get
            {
                return pila.Peek() ;
            }
        }

        internal static void Reiniciar()
        {
            listaLeidoHastaAhora.Clear();
            listaLineaActual.Clear();
            contextoGlobal = Sintactico.ContextoGlobal.Global;
            contextoLinea = Sintactico.ContextoLinea.Ninguno;


            esProcPrincipal = false;
        }
            

        internal static void AgregarTerminal(Terminal t)
        {
            if (contextoLinea == ContextoLinea.Ninguno)
            {
                NuevaLinea(t);
            }
            else
            {
                List<Terminal> terminalesQueTerminanLinea = new List<Terminal>();
                switch (contextoLinea)
                {
                    case ContextoLinea.Asignacion:                        
                    case ContextoLinea.Leer:                        
                    case ContextoLinea.LlamadaProc:
                    case ContextoLinea.DeclaracionConstante:
                    case ContextoLinea.DeclaracionVariable:
                    case ContextoLinea.Mostrar:
                    case ContextoLinea.FinProc:
                    case ContextoLinea.FinFuncion:
                    case ContextoLinea.FinSi:
                    case ContextoLinea.FinMientras:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoFinSentencia());
                        break;
                    case ContextoLinea.Mientras:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoHacer());
                        break;
                    case ContextoLinea.Si:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoEntonces());
                        break;                    
                    case ContextoLinea.DeclaracionFuncion:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoTipoBooleano());
                        terminalesQueTerminanLinea.Add(Terminal.ElementoTipoNumero());
                        terminalesQueTerminanLinea.Add(Terminal.ElementoTipoTexto());
                        break;
                    case ContextoLinea.DeclaracionProc:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoParentesisClausura());
                        break;
                        
                }

                if (terminalesQueTerminanLinea.Contains(t))
                {
                    FinLinea(t);
                }
                else
                {
                   
                    AgregarALinea(t);
                }
            }

        }

        private static void AgregarALinea(Terminal t)
        {            
            listaLineaActual.Add(t);
            listaLeidoHastaAhora.Add(t);

         
          

            if (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Principal)
            {
                esProcPrincipal = true;
            }

         

            if (esProcPrincipal && t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin)
            {
                esProcPrincipal = false;
            }

          
        }

        private static void NuevaLinea(Terminal t)
        {
            //ojo con el sino!
            switch (t.Componente.Token)
            {
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Mostrar:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MostrarConPausa:
                    contextoLinea = ContextoLinea.Mostrar;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasComienzo:
                    contextoLinea = ContextoLinea.Mientras;
                    pila.Push(ElementoPila.Mientras);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasFin:
                    contextoLinea = ContextoLinea.FinMientras;
                    pila.Pop();
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiComienzo:
                    contextoLinea = ContextoLinea.Si;
                    pila.Push(ElementoPila.Si);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiSino:
                    pila.Push(ElementoPila.Sino);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiFin:
                    contextoLinea = ContextoLinea.FinSi;
                    if (pila.Peek() == ElementoPila.Sino)
                    {
                        pila.Pop();
                    }
                    pila.Pop();
                    
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoComienzo:
                    contextoGlobal = Sintactico.ContextoGlobal.DeclaracionLocal;
                    contextoLinea = ContextoLinea.DeclaracionProc;
                    pila.Push(ElementoPila.Procedimiento);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin:
                    contextoGlobal = Sintactico.ContextoGlobal.Global;
                    contextoLinea = ContextoLinea.FinProc;
                    pila.Pop();
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionComienzo:
                    contextoGlobal = Sintactico.ContextoGlobal.DeclaracionLocal;
                    contextoLinea = ContextoLinea.DeclaracionFuncion;
                    pila.Push(ElementoPila.Funcion);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionFin:
                    contextoGlobal = Sintactico.ContextoGlobal.Global;
                    contextoLinea = ContextoLinea.FinFuncion;
                    pila.Pop();
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Comenzar:
                    contextoGlobal = ContextoGlobal.Cuerpo;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Constantes:
                    contextoGlobal = Sintactico.ContextoGlobal.GlobalDeclaracionesConstantes;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Variables:
                    contextoGlobal = Sintactico.ContextoGlobal.GlobalDeclaracionesVariables;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Const:
                    contextoLinea = ContextoLinea.DeclaracionConstante;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Var:
                    contextoLinea = ContextoLinea.DeclaracionVariable;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Llamar:
                    contextoLinea = ContextoLinea.LlamadaProc;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Leer:
                    contextoLinea = ContextoLinea.Leer;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Identificador:
                    contextoLinea = ContextoLinea.Asignacion;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Principal:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Numero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Verdadero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Falso:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoDato:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoNumero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoTexto:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoBooleano:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiEntonces:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Igual:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Concatenacion:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SumaEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.RestaEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MultiplicacionEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.DivisionEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Mayor:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MayorIgual:
                
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MenorIgual:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Menor:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Distinto:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Negacion:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.And:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Or:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ParentesisApertura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ParentesisClausura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.CorcheteApertura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.CorcheteClausura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Literal:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Coma:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.De:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Arreglo:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ComentarioApertura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ComentarioClausura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Ninguno:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Error:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.EOF:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FinSentencia:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasHacer:
                default:
                    contextoLinea = ContextoLinea.Ninguno;
                    break;
            }

            if (contextoLinea != Sintactico.ContextoLinea.Ninguno)
            {
                AgregarALinea(t);
            }

           

          
        }

        private static void FinLinea(Terminal t)
        {
            listaLeidoHastaAhora.Add(t);
            contextoLinea = ContextoLinea.Ninguno;
            listaLineaActual.Clear();
        }




        internal static ContextoLinea ContextoPerteneceTerminal(Terminal t)
        {
            //ojo con el sino!
            ContextoLinea contextoLinea = Sintactico.ContextoLinea.Ninguno;

            switch (t.Componente.Token)
            {
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Mostrar:
                    return  ContextoLinea.Mostrar;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasComienzo:
                    return  ContextoLinea.Mientras;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasFin:
                    return  ContextoLinea.FinMientras;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiComienzo:
                    return  ContextoLinea.Si;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiSino:
                    return ContextoLinea.Sino;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiFin:
                    return  ContextoLinea.FinSi;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoComienzo:
                    return  ContextoLinea.DeclaracionProc;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin:
                    return  ContextoLinea.FinProc;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionComienzo:
                    return  ContextoLinea.DeclaracionFuncion;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionFin:
                    return  ContextoLinea.FinFuncion;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Const:
                    return  ContextoLinea.DeclaracionConstante;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Var:
                    return  ContextoLinea.DeclaracionVariable;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Llamar:
                    return  ContextoLinea.LlamadaProc;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Leer:
                    return  ContextoLinea.Leer;
                    
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Identificador:
                    return  ContextoLinea.Asignacion;
                    
            }

            return contextoLinea;
        }   


        internal static bool EsContextoLineaCorrectoParaGlobal(ContextoLinea linea, ContextoGlobal global, Terminal t)
        {
            switch (global)
            {
                case ContextoGlobal.Global:
                    return linea == Sintactico.ContextoLinea.DeclaracionFuncion
                        || linea == Sintactico.ContextoLinea.DeclaracionProc
                        || (linea == Sintactico.ContextoLinea.Ninguno && t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Variables)
                        || (linea == Sintactico.ContextoLinea.Ninguno && t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Constantes);
                case ContextoGlobal.GlobalDeclaracionesConstantes:
                    return linea == Sintactico.ContextoLinea.DeclaracionFuncion
                        || linea == Sintactico.ContextoLinea.DeclaracionProc
                        || linea == Sintactico.ContextoLinea.DeclaracionConstante
                        || linea == Sintactico.ContextoLinea.DeclaracionVariable
                        || (linea == Sintactico.ContextoLinea.Ninguno && t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Variables);
                case ContextoGlobal.GlobalDeclaracionesVariables:
                    return linea == Sintactico.ContextoLinea.DeclaracionFuncion
                        || linea == Sintactico.ContextoLinea.DeclaracionProc
                        || linea == Sintactico.ContextoLinea.DeclaracionConstante
                        || linea == Sintactico.ContextoLinea.DeclaracionVariable;                        
                case ContextoGlobal.DeclaracionLocal:
                    return linea == Sintactico.ContextoLinea.DeclaracionConstante
                        || linea == Sintactico.ContextoLinea.DeclaracionVariable
                        || (linea == Sintactico.ContextoLinea.Ninguno && t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Comenzar);
                case ContextoGlobal.Cuerpo:
                    return linea == Sintactico.ContextoLinea.Mientras
                        || linea == Sintactico.ContextoLinea.FinMientras
                        || linea == Sintactico.ContextoLinea.Si
                        || linea == Sintactico.ContextoLinea.Sino
                        || linea == Sintactico.ContextoLinea.FinSi
                        || linea == Sintactico.ContextoLinea.Leer
                        || linea == Sintactico.ContextoLinea.LlamadaProc
                        || linea == Sintactico.ContextoLinea.Mostrar;
                default:
                    return false;
            }

        }

    }

    
}
