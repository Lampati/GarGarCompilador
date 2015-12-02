using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Semantico.RecorredorArbol;
using CompiladorGargar.Semantico.Arbol.Nodos;
using System.Diagnostics;

namespace CompiladorGargar.Resultado
{
    public class ResultadoCompilacion
    {
        public bool CompilacionGarGarCorrecta { get; set; }
        public bool GeneracionEjectuableCorrecto { get; set; }
        public string Error { get; set; }

        public List<PasoAnalizadorSintactico> ListaErrores { get; set; }
        public List<PasoCompilacion> ListaDebugSintactico { get; set; }
        internal ArbolSemantico ArbolSemanticoResultado { get; set; }
        public TablaSimbolos TablaSimbolos { get; set; }
        public List<int> ListaLineasValidas { get; set; }
        public List<int> ListaLineasContenidoProcSalida { get; set; }

        public string CodigoGarGar { get; set; }
        public string CodigoPascal { get; set; }
        public string ArchTemporalCodigoPascal { get; set; }
        public string ArchTemporalCodigoPascalConRuta { get; set; }

        public string ArchTemporalResultadosEjecucionConRuta { get; set; }

        public string ArchEjecutable { get; set; }
        public string ArchEjecutableConRuta { get; set; }
        public ResultadoCompilacionPascal ResultadoCompPascal { get; set; }

        public double TiempoCompilacionTotal { get; set; }
        public double TiempoGeneracionAnalizadorLexico { get; set; }
        public double TiempoGeneracionAnalizadorSintactico { get; set; }
        public double TiempoGeneracionCodigo { get; set; }
        public double TiempoGeneracionTemporalCodigo { get; set; }
        public double TiempoGeneracionEjecutable { get; set; }

        

        public ResultadoCompilacion()
        {
            ListaDebugSintactico = new List<PasoCompilacion>();
            ListaErrores = new List<PasoAnalizadorSintactico>();
            CompilacionGarGarCorrecta = false;

            
        }

        public string ArmarArbol()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.ArbolSemanticoResultado != null)
            {
                NodoArbolSemantico nodoActual = this.ArbolSemanticoResultado.ObtenerRaiz();

                
                strBldr.AppendLine(nodoActual.ToString());

                for (int i = 0; i < nodoActual.ObtenerCantidadHijos(); i++)
                {
                    NodoArbolSemantico nodo = nodoActual.ObtenerHijo(i);
                    strBldr.AppendLine(ObtenerStringNodo(nodo));
                }
            }

            return strBldr.ToString();
        }

        private string ObtenerStringNodo(NodoArbolSemantico nodoActual)
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.AppendLine(nodoActual.ToString());

            for (int i = 0; i < nodoActual.ObtenerCantidadHijos(); i++)
            {
                NodoArbolSemantico nodo = nodoActual.ObtenerHijo(i);
                strBldr.AppendLine(ObtenerStringNodo(nodo));
            }

            return strBldr.ToString();
        }

        public void FiltrarListaErroresSintacticos()
        {
            List<int> listaPosicionesRemover = new List<int>();
            List<PosicionErrorSintactico> listaErroresYaVistos = new List<PosicionErrorSintactico>();

            for (int i = 0; i < this.ListaErrores.Count; i++)
            {
                if ( this.ListaErrores[i].TipoError == GlobalesCompilador.TipoError.Sintactico && this.ListaErrores[i].MensajeError != null)
                {
                    //Pregunto si el error ya aparece en la linea. Si aparece, con distinta columna, lo saco.
                    if (listaErroresYaVistos.Find(x => x.CodigoGlobal == this.ListaErrores[i].MensajeError.CodigoGlobal && x.Fila == this.ListaErrores[i].Fila) == null)
                    {
                        listaErroresYaVistos.Add(new PosicionErrorSintactico() { CodigoGlobal = this.ListaErrores[i].MensajeError.CodigoGlobal, Fila = this.ListaErrores[i].Fila });
                    }
                    else
                    {
                        listaPosicionesRemover.Add(i);
                    }
                }
            }

            listaPosicionesRemover.Sort();
            listaPosicionesRemover.Reverse();

            foreach (var item in listaPosicionesRemover)
            {
                this.ListaErrores.RemoveAt(item);
            }
        }
    }

    public class PosicionErrorSintactico
    {
        public int Fila { get; set; }
        public int CodigoGlobal { get; set; }

    }

    

   

  
}
