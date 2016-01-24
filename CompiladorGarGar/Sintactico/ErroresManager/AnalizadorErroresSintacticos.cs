using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class AnalizadorErroresSintacticos
    {

        private Tipos.TipoBase tipoError;

        public AnalizadorErroresSintacticos(List<Terminal> lineaHastaAhora, ContextoGlobal contextoGlobal, ContextoLinea contextoLinea, List<Terminal> cadenaEntradaFaltante)
        {

            if (TerminalesHelpers.EsTerminalFinBloque(cadenaEntradaFaltante[0]) && lineaHastaAhora.Count == 0)
            {
                ChequearErrorCierreBloqueIncorrecta(cadenaEntradaFaltante[0], contextoGlobal);
            }


            tipoError = TipoFactory.CrearTipo(lineaHastaAhora, contextoGlobal, contextoLinea, cadenaEntradaFaltante);

            if (tipoError == null)
            {
                ConstruirYArrojarExcepcion(cadenaEntradaFaltante[0], contextoGlobal);
            }
            

        
        }

        private void ChequearErrorCierreBloqueIncorrecta(Terminal terminal, ContextoGlobal contextoGlobal)
        {
            MensajeError mensajeError = null;

            if (contextoGlobal == ContextoGlobal.Cuerpo)
            {
                switch (terminal.Componente.Token)
                {
                    case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasFin:
                        if (EstadoSintactico.TopePilaLlamados != ElementoPila.Mientras)
                        {
                            mensajeError = new ErrorCierreMientrasFueraLugar(EstadoSintactico.TopePilaLlamados.ToString());
                        }
                        break;
                    case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiSino:
                        if (EstadoSintactico.TopePilaLlamados != ElementoPila.Si)
                        {
                            mensajeError = new ErrorSinoFueraLugar(EstadoSintactico.TopePilaLlamados.ToString());
                        }
                        break;
                    case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiFin:
                        if (EstadoSintactico.TopePilaLlamados != ElementoPila.Si && EstadoSintactico.TopePilaLlamados != ElementoPila.Sino)
                        {
                            mensajeError = new ErrorCierreSiFueraLugar(EstadoSintactico.TopePilaLlamados.ToString());
                        }
                        break;
                    case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin:
                        if (EstadoSintactico.TopePilaLlamados != ElementoPila.Procedimiento)
                        {
                            mensajeError = new ErrorCierreProcFueraLugar(EstadoSintactico.TopePilaLlamados.ToString());
                        }
                        break;
                    case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionFin:
                        if (EstadoSintactico.TopePilaLlamados != ElementoPila.Funcion)
                        {
                            mensajeError = new ErrorCierreFuncFueraLugar(EstadoSintactico.TopePilaLlamados.ToString());
                        }
                        break;
                    default:
                        break;
                }

                if (mensajeError != null)
                {
                    throw new AnalizadorErroresException(mensajeError) 
                        { Fila = terminal.Componente.Fila, Columna = terminal.Componente.Columna, Parar = true };
                }
            }
        }

        private void ConstruirYArrojarExcepcion(Terminal terminal, ContextoGlobal contextoGlobal)
        {

            MensajeError mensajeError;


            ContextoLinea cont = EstadoSintactico.ContextoPerteneceTerminal(terminal);

            switch (cont)
            {
                case ContextoLinea.Asignacion:
                    mensajeError = new ErrorVariableSinLugarEnAsignacion(terminal.Componente.Lexema);
                    break;
                case ContextoLinea.Leer:
                    mensajeError = new ErrorLeerFueraDeLugar();
                    break;
                case ContextoLinea.LlamadaProc:
                    mensajeError = new ErrorLlamadaProcFueraLugar();
                    break;
                case ContextoLinea.Mientras:
                    mensajeError = new ErrorBloqueMientrasFueraLugar();
                    break;
                case ContextoLinea.Si:
                    mensajeError = new ErrorBloqueSiFueraLugar();
                    break;
                case ContextoLinea.Sino:
                    mensajeError = new ErrorBloqueSinoFueraLugar();
                    break;
                case ContextoLinea.DeclaracionFuncion:
                    mensajeError = new ErrorDeclaracionFuncionFueraLugar();
                    break;
                case ContextoLinea.DeclaracionProc:
                    mensajeError = new ErrorDeclaracionProcFueraLugar();
                    break;
                case ContextoLinea.DeclaracionConstante:
                    switch (contextoGlobal)
	                {
                        case ContextoGlobal.GlobalDeclaracionesVariables:
                            mensajeError = new ErrorDeclaracionConstanteFueraLugarEnVariables();
                            break;
                        case ContextoGlobal.DeclaracionLocal:
                            mensajeError = new ErrorDeclaracionConstanteFueraLugarEnEspacioDecLocal();
                            break;
                        case ContextoGlobal.Cuerpo:
                            mensajeError = new ErrorDeclaracionConstanteFueraLugarCuerpoProc();
                            break;
                        default:
                            mensajeError = new ErrorDeclaracionConstanteFueraLugar2();
                            break;
	                }                    
                    break;
                case ContextoLinea.DeclaracionVariable:
                    switch (contextoGlobal)
                    {
                        case ContextoGlobal.GlobalDeclaracionesConstantes:
                            mensajeError = new ErrorDeclaracionVariableFueraLugarConstantes();
                            break;                        
                        case ContextoGlobal.Cuerpo:
                            mensajeError = new ErrorDeclaracionVariableFueraLugarCuerpo();
                            break;
                        default:
                            mensajeError = new ErrorDeclaracionVariableFueraLugar2();
                            break;
                    }  
                    break;
                case ContextoLinea.FinFuncion:
                    mensajeError = new ErrorFinFuncFueraLugar();
                    break;
                case ContextoLinea.FinProc:
                    mensajeError = new ErrorFinProcFueraLugar();
                    break;
                case ContextoLinea.FinMientras:
                    mensajeError = new ErrorFinMientrasFueraLugar();
                    break;
                case ContextoLinea.FinSi:
                    mensajeError = new ErrorFinSiFueraLugar();
                    break;
                case ContextoLinea.Mostrar:
                    mensajeError = new ErrorMostrarFueraLugar();
                    break;
                default:
                    mensajeError = new ErrorLineaComienzaIncorrecta( terminal.Componente.Lexema);
                    break;
            }

            throw new AnalizadorErroresException(mensajeError) 
                    { Fila = terminal.Componente.Fila, Columna = terminal.Componente.Columna, Parar = true  };
        }

        internal void Validar()
        {
            if (tipoError != null)
            {
                tipoError.Validar();
            }
        }
    }
}
