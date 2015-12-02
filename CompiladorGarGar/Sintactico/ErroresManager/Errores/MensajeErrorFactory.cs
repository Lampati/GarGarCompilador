using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager.Errores
{
    public static class MensajeErrorFactory
    {
        public static MensajeError ObtenerMensajeError(int codigoGlobal)
        {
            switch (codigoGlobal)
            {
                case 1:
                    return new ErrorDeclaracionConstanteGenerico();
                case 2:
                    return new ErrorTipoDatoDefRepetido();
                case 3:
                    return new ErrorTipoDatoDefFaltante();
                case 4:
                    return new ErrorAsignarValorRepetido();
                case 5:
                    return new ErrorAsignarValorFaltante();
                case 6:
                    return new ErrorConstanteTipoDatoSinArreglo();
                case 7:
                    return new ErrorTipoDatoRepetido();
                case 8:
                    return new ErrorTipoDatoFaltante();
                case 9:
                    return new ErrorConstanteValorRepetido();
                case 10:
                    return new ErrorConstanteValorFaltante();
                case 11:
                    return new ErrorConstanteElementoQueSobraErroneo(string.Empty);
                case 12:
                    return new ErrorAsignacionRepetido();
                case 13:
                    return new ErrorAsignacionFaltante();
                case 14:
                    return new ErrorAsignacionTerminaCorrectamente();
                case 15:
                    return new ErrorAsignacionParteIzqCorrecta();
                case 16:
                    return new ErrorAsignacionParentesisBalanceadosParteIzq();
                case 17:
                    return new ErrorAsignacionParentesisBalanceadosParteDer();
                case 18:
                    return new ErrorAsignacionCorchetesBalanceadosParteIzq();
                case 19:
                    return new ErrorAsignacionCorchetesBalanceadosParteDer();
                case 20:
                    return new ErrorAsignacionElementosConValorNoContiguosParteIzq();
                case 21:
                    return new ErrorAsignacionElementosConValorNoContiguosParteDer();
                case 22:
                    return new ErrorAsignacionValidacionPorDefault();
                case 23:
                    return new ErrorDeclaracionFuncionValidacionPorDefault();
                case 24:
                    return new ErrorAsignacionElementosConValorNoContiguosParteIzq();
                case 25:
                    return new ErrorDeclaracionVariableValidacionPorDefault();
                case 26:
                    return new ErrorDeclaracionVariableParteIzquierdaValida();
                case 27:
                    return new ErrorDeclaracionVariableCantArregloNoRepetido();
                case 28:
                    return new ErrorDeclaracionVariableCorchetesBalanceadosParteIzq();
                case 29:
                    return new ErrorDeclaracionVariableElementoQueSobraErroneo(string.Empty);
                case 30:
                    return new ErrorMientrasValidacionPorDefault();
                case 31:
                    return new ErrorFinSiValidacionFin();
                case 32:
                    return new ErrorLlamadoProcValidacionPorDefault();
                case 33:
                    return new ErrorLeerValidacionPorDefault();
                case 34:
                    return new ErrorFinMientrasValidacionFin();
                case 35:
                    return new ErrorSiValidacionPorDefault();
                case 36:
                    return new ErrorMostrarValidacionPorDefault();
                case 37:
                    return new ErrorFinProcValidacionFin();
                case 38:
                    return new ErrorFinFuncValidacionFin();
                case 39:
                    return new ErrorFinFuncValidacionPorDefault();
                default:
                    return null;
            }

        }
    }
}
