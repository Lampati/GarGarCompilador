using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class TipoFactory
    {

        internal static Tipos.TipoBase CrearTipo(List<Terminal> lineaHastaAhora, ContextoGlobal contextoGlobal, ContextoLinea tipo, List<Terminal> cadenaEntradaFaltante)
        {
            int fila, col;

            if (cadenaEntradaFaltante.Count > 0)
            {
                fila = cadenaEntradaFaltante[0].Componente.Fila;
                col = cadenaEntradaFaltante[0].Componente.Columna;
            }
            else
            {
                if (lineaHastaAhora.Count > 0)
                {
                    fila = lineaHastaAhora[lineaHastaAhora.Count - 1].Componente.Fila;
                    col = lineaHastaAhora[lineaHastaAhora.Count - 1].Componente.Columna;
                }
                else
                {
                    fila = -1;
                    col = -1;
                }
            }
            
            bool errorLineaFueraContextoGlobal = false; 

            List<Terminal> lineaEntera = new List<Terminal>(lineaHastaAhora);

            List<Terminal> terminalesQueTerminanLinea = new List<Terminal>();
            List<Terminal> terminalesQueIndicanComienzoOtraSentencia = new List<Terminal>();

            //Estos son comunes a todos. Algunos tienen alguno mas.
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoSi());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoMientras());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoFuncion());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoLlamar());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoVar());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoConst());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoMostrar());
            terminalesQueIndicanComienzoOtraSentencia.Add(Terminal.ElementoMostrarP());
            switch (tipo)
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

                case ContextoLinea.Ninguno:
                    //Es el primer terminal se una linea fuera de contexto
                    errorLineaFueraContextoGlobal = ChequeoLineasFueraContexto(lineaHastaAhora, contextoGlobal, cadenaEntradaFaltante);
                    break;
            }

            if (!errorLineaFueraContextoGlobal)
            {
                

                int i = 0;
                while (i < cadenaEntradaFaltante.Count 
                        && !terminalesQueTerminanLinea.Contains(cadenaEntradaFaltante[i])
                        && !terminalesQueIndicanComienzoOtraSentencia.Contains(cadenaEntradaFaltante[i]))
                {
                    
                    lineaEntera.Add(cadenaEntradaFaltante[i]);
                    
                    i++;
                }

                if (i < cadenaEntradaFaltante.Count)
                {
                    bool lineaContieneTerminalDeOtraSentencia = terminalesQueIndicanComienzoOtraSentencia.Contains(cadenaEntradaFaltante[i]);

                    if (!lineaContieneTerminalDeOtraSentencia)
                    {
                        //salida normal, agarre la linea entera
                        lineaEntera.Add(cadenaEntradaFaltante[i]);
                    }



                    return Crear(lineaEntera, lineaHastaAhora, contextoGlobal, tipo, fila, col);
                }
                else
                {
                    //no agarre la linea entera. Error x default??
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

       

        private static bool ChequeoLineasFueraContexto(List<Terminal> linea, ContextoGlobal contextoGlobal, List<Terminal> cadenaEntradaFaltante)
        {
            if (linea.Count == 0)
            {
                ContextoLinea cont = EstadoSintactico.ContextoPerteneceTerminal(cadenaEntradaFaltante[0]);

                return EstadoSintactico.EsContextoLineaCorrectoParaGlobal(cont, contextoGlobal, cadenaEntradaFaltante[0]);

            }
            else
            {
                return false;
            }
        }

        private static Tipos.TipoBase Crear(List<Terminal> lineaEntera, List<Terminal> lineaHastaAhora, ContextoGlobal contextoGlobal, ContextoLinea tipo, int fila, int col)
        {
            Tipos.TipoBase retorno = null;

            switch (tipo)
            {
                case ContextoLinea.Asignacion:
                    retorno = new Tipos.Asignacion(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.Leer:
                    retorno = new Tipos.Leer(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.LlamadaProc:
                    retorno = new Tipos.LlamadoProc(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.Mientras:
                    retorno = new Tipos.Mientras(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.Si:
                    retorno = new Tipos.Si(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.Sino:
                    break;
                case ContextoLinea.DeclaracionFuncion:
                    retorno = new Tipos.DeclaracionFuncion(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.DeclaracionProc:
                    retorno = new Tipos.DeclaracionProc(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.DeclaracionConstante:
                    retorno = new Tipos.DeclaracionConstante(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.DeclaracionVariable:
                    retorno = new Tipos.DeclaracionVariable(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.FinFuncion:
                    retorno = new Tipos.FinFuncion(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.FinProc:
                    retorno = new Tipos.FinProc(lineaEntera, lineaHastaAhora,fila, col);  //terminada 16/3/2012
                    break;
                case ContextoLinea.FinMientras:
                    retorno = new Tipos.FinMientras(lineaEntera, lineaHastaAhora,fila, col);  //terminada 16/3/2012
                    break;
                case ContextoLinea.FinSi:
                    retorno = new Tipos.FinSi(lineaEntera, lineaHastaAhora,fila, col); //terminada 16/3/2012
                    break;
                case ContextoLinea.Mostrar:
                    retorno = new Tipos.Mostrar(lineaEntera, lineaHastaAhora, fila, col);
                    break;
                case ContextoLinea.Ninguno:
                    break;
                default:
                    break;
            }


            return retorno;
        }
    }
}
