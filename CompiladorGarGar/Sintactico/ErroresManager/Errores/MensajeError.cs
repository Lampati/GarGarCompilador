using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager.Errores
{
    public enum Sentencias
    {
        Mientras,
        Si,
        Leer,
        Mostrar,
        LlamarProcedimiento,
        Asignacion,
        DeclaracionVariable,
        DeclaracionConstante,
        DeclaracionFuncion,
        DeclaracionProcedimiento,
        Ninguno

    }

    public abstract class MensajeError
    {
        public string MensajeModoTexto { get; set; }
        public string MensajeModoGrafico { get; set; }
        public int CodigoGlobal { get; set; }
        public bool EsErrorBienDefinido { get; set; }
        public List<Sentencias> SentenciasQueTienenElError { get; set; }

        public MensajeError()
        {
            SentenciasQueTienenElError = new List<Sentencias>();
            EsErrorBienDefinido = true;
        }
    }

    public class ErrorVacio : MensajeError
    {
        public ErrorVacio()
            : base()
        {
            CodigoGlobal = 0;
            MensajeModoTexto = "";
            MensajeModoGrafico = "";
            EsErrorBienDefinido = false;
        }
    }



    public class ErrorDeclaracionConstanteGenerico : MensajeError
    {
        public ErrorDeclaracionConstanteGenerico()
            : base()
        {
            CodigoGlobal = 1;
            MensajeModoTexto = "La declaración de la constante contiene un error sintactico. La manera correcta de declarar una constante es: \"const EJEMPLO : TIPO = VALOR;\"\r\n(TIPO = \"numero|texto|booleano\")";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorTipoDatoDefRepetido : MensajeError
    {
        public ErrorTipoDatoDefRepetido()
            : base()
        {
            CodigoGlobal = 2;
            MensajeModoTexto = "El : esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorTipoDatoDefFaltante : MensajeError
    {
        public ErrorTipoDatoDefFaltante()
            : base()
        {
            CodigoGlobal = 3;
            MensajeModoTexto = ": faltante en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorAsignarValorRepetido : MensajeError
    {
        public ErrorAsignarValorRepetido()
            : base()
        {
            CodigoGlobal = 4;
            MensajeModoTexto = "El = esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorAsignarValorFaltante : MensajeError
    {
        public ErrorAsignarValorFaltante()
            : base()
        {
            CodigoGlobal = 5;
            MensajeModoTexto = "= faltante en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorConstanteTipoDatoSinArreglo : MensajeError
    {
        public ErrorConstanteTipoDatoSinArreglo()
            : base()
        {
            CodigoGlobal = 6;
            MensajeModoTexto = "Las constantes no pueden ser arreglos";
            MensajeModoGrafico = "Las constantes no pueden ser arreglos";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorTipoDatoRepetido : MensajeError
    {
        public ErrorTipoDatoRepetido()
            : base()
        {
            CodigoGlobal = 7;
            MensajeModoTexto = "El tipo de dato esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "El tipo de dato esta especificado mas de una vez en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorTipoDatoFaltante : MensajeError
    {
        public ErrorTipoDatoFaltante()
            : base()
        {
            CodigoGlobal = 8;
            MensajeModoTexto = "Tipo de dato faltante en la declaración";
            MensajeModoGrafico = "Tipo de dato faltante en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorConstanteValorRepetido : MensajeError
    {
        public ErrorConstanteValorRepetido()
            : base()
        {
            CodigoGlobal = 9;
            MensajeModoTexto = "El valor de constante esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "El valor de constante esta especificado mas de una vez en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
            
        }
    }

    public class ErrorConstanteValorFaltante : MensajeError
    {
        public ErrorConstanteValorFaltante()
            : base()
        {
            CodigoGlobal = 10;
            MensajeModoTexto = "Valor de constante faltante en la declaración";
            MensajeModoGrafico = "Valor de constante faltante en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorConstanteElementoQueSobraErroneo : MensajeError
    {
        public ErrorConstanteElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 11;
            MensajeModoTexto = string.Format("{0} no tiene lugar en una declaración de constante", elementoErroneo);
            MensajeModoGrafico = string.Format("{0} no tiene lugar en una declaración de constante", elementoErroneo);

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorAsignacionRepetido : MensajeError
    {
        public ErrorAsignacionRepetido()
            : base()
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion
            CodigoGlobal = 12;
            MensajeModoTexto = "El = esta especificado mas de una vez en la asignacion";
            MensajeModoGrafico = "El = esta especificado mas de una vez en la asignacion";

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionFaltante : MensajeError
    {
        public ErrorAsignacionFaltante()
            : base()
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion
            CodigoGlobal = 13;
            MensajeModoTexto = "= faltante en la asignacion";
            MensajeModoGrafico = "= faltante en la asignacion";

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionTerminaCorrectamente : MensajeError
    {
        public ErrorAsignacionTerminaCorrectamente()
            : base()
        {
            CodigoGlobal = 14;
            MensajeModoTexto = "La asignacion termina incorrectamente";
            MensajeModoGrafico = "La asignacion termina incorrectamente";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionParteIzqCorrecta : MensajeError
    {
        public ErrorAsignacionParteIzqCorrecta()
            : base()
        {
            CodigoGlobal = 15;
            MensajeModoTexto = "Error sintactico en la parte izq de la asignación";
            MensajeModoGrafico = "Error sintactico en la parte izq de la asignación";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 16;
            MensajeModoTexto = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
            MensajeModoGrafico = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 17;
            MensajeModoTexto = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
            MensajeModoGrafico = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 18;
            MensajeModoTexto = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
            MensajeModoGrafico = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 19;
            MensajeModoTexto = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
            MensajeModoGrafico = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteIzq : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteIzq()
            : base()
        {
            CodigoGlobal = 20;
            MensajeModoTexto = "La asignacion contiene una expresión mal formada en su parte izquierda";
            MensajeModoGrafico = "La asignacion contiene una expresión mal formada en su parte izquierda";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteDer : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteDer()
            : base()
        {
            CodigoGlobal = 21;
            MensajeModoTexto = "La asignacion contiene una expresión mal formada en su parte derecha";
            MensajeModoGrafico = "La asignacion contiene una expresión mal formada en su parte derecha";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionValidacionPorDefault : MensajeError
    {
        public ErrorAsignacionValidacionPorDefault()
            : base()
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion
            CodigoGlobal = 22;
            MensajeModoTexto = "La asignacion contiene un error sintactico. La manera correcta de usar una asignación es la siguiente: \"VARIABLE = EXPRESION;\"\r\n(VARIABLE = variable o posición de arreglo)\r\n(EXPRESION = expresión de cualquier tipo)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorDeclaracionFuncionValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionFuncionValidacionPorDefault()
            : base()
        {

            CodigoGlobal = 23;
            MensajeModoTexto = "La declaración de la función contiene un error sintactico. La manera correcta de declarar una función es la siguiente: \"funcion EJEMPLO ( PARAMETROS ) : TIPO comenzar BLOQUE finfunc RETORNO;\"\r\n(PARAMETROS = \"param1 : TIPO, param2 : arreglo[MAX] de TIPO\")\r\n(TIPO = \"numero|texto|booleano\")\r\n(BLOQUE = contenido de la función)\r\n(RETORNO = expresión conteniendo el retorno de la función)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);
        }
    }

    public class ErrorDeclaracionProcedimientoValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionProcedimientoValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 24;
            MensajeModoTexto = "La declaración del procedimiento contiene un error sintactico. La manera correcta de declarar un procedimiento es la siguiente: \"procedimiento EJEMPLO ( PARAMETROS ) comenzar BLOQUE finproc;\"\r\n(PARAMETROS = \"param1 : TIPO, param2 : arreglo[MAX] de TIPO\")\r\n(TIPO = \"numero|texto|booleano\")\r\n(BLOQUE = contenido del procedimiento)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionProcedimiento);
        }
    }

    public class ErrorDeclaracionVariableValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionVariableValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 25;
            MensajeModoTexto = "La declaración de la variable contiene un error sintactico. La manera correcta de declarar una variable es la siguiente: \"var LISTA : TIPO\" o \"var LISTA : arreglo[MAX] de TIPO\"\r\n(LISTA = \"nombre1,nombre2,nombre3...\")\r\n(TIPO = \"numero|texto|booleano\")";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableParteIzquierdaValida : MensajeError
    {
        public ErrorDeclaracionVariableParteIzquierdaValida()
            : base()
        {
            CodigoGlobal = 26;
            MensajeModoTexto = "La declaración de variables es incorrecta. Debe ser una lista de identificadores separados por comas o un identificador solo";
            MensajeModoGrafico = "La declaración de variables es incorrecta. Debe ser una lista de identificadores separados por comas o un identificador solo";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableCantArregloNoRepetido : MensajeError
    {
        public ErrorDeclaracionVariableCantArregloNoRepetido()
            : base()
        {
            CodigoGlobal = 27;
            MensajeModoTexto = "El arreglo esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "El arreglo esta especificado mas de una vez en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorDeclaracionVariableCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 28;
            MensajeModoTexto = "Los corchetes no estan balanceados en la declaracion del arreglo";
            MensajeModoGrafico = "Los corchetes no estan balanceados en la declaracion del arreglo";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableElementoQueSobraErroneo : MensajeError
    {
        public ErrorDeclaracionVariableElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 29;
            MensajeModoTexto = string.Format("Error sintactico: {0} es incorrecto en la declaración de un arreglo. La forma correcta es arreglo [MAX] de TIPO", elementoErroneo);
            MensajeModoGrafico = string.Format("Error sintactico: {0} es incorrecto en la declaración de un arreglo.", elementoErroneo);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorMientrasValidacionPorDefault : MensajeError
    {
        public ErrorMientrasValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 30;
            MensajeModoTexto = "El mientras contiene un error sintactico. La manera correcta de usar un mientras es la siguiente: \"mientras ( EXPRBOOLEANA ) hacer BLOQUE finmientras;\"\r\n(EXPRBOOLEANA = expresión del tipo booleana)\r\n(BLOQUE = contenido del mientras)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.Mientras);
        }
    }

    public class ErrorFinSiValidacionFin : MensajeError
    {
        public ErrorFinSiValidacionFin()
            : base()
        {
            CodigoGlobal = 31;
            MensajeModoTexto = "El fin de un bloque si debe especificarse de la siguiente manera: finsi;";
            MensajeModoGrafico = "El fin de un bloque si debe especificarse de la siguiente manera: finsi;";
            SentenciasQueTienenElError.Add(Sentencias.Si);
        }
    }

    public class ErrorLlamadoProcValidacionPorDefault : MensajeError
    {
        public ErrorLlamadoProcValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 32;
            MensajeModoTexto = "La llamada al procedimiento contiene un error sintactico. La manera correcta de usar una llamada a procedimiento es la siguiente: \"llamar NOMBREPROC ( EXPRESIONES );\"\r\n(EXPRESIONES = expresiones de cualquier tipo, separadas por coma)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);
        }
    }

    public class ErrorLeerValidacionPorDefault : MensajeError
    {
        public ErrorLeerValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 33;
            MensajeModoTexto = "La llamada a leer contiene un error sintactico. La manera correcta de usar una llamada a leer es la siguiente: \"leer VARIABLE;\"\r\n(VARIABLE = variable o posición de arreglo)";
            MensajeModoGrafico = MensajeModoTexto;
        }
    }

    public class ErrorFinMientrasValidacionFin : MensajeError
    {
        public ErrorFinMientrasValidacionFin()
            : base()
        {
            CodigoGlobal = 34;
            MensajeModoTexto = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
            MensajeModoGrafico = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
            SentenciasQueTienenElError.Add(Sentencias.Mientras);
        }
    }

    public class ErrorSiValidacionPorDefault : MensajeError
    {
        public ErrorSiValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 35;
            MensajeModoTexto = "La declaracion del bloque si contiene un error sintactico.  La manera correcta de declarar un mientras es la siguiente: \"si ( EXPRBOOLEANA ) entonces BLOQUE sino BLOQUE finsi;\"\r\n(EXPRBOOLEANA = expresión del tipo booleana)\r\n(BLOQUE = contenido del si/sino)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.Si);
        }
    }

    public class ErrorMostrarValidacionPorDefault : MensajeError
    {
        public ErrorMostrarValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 36;
            MensajeModoTexto = "La llamada a mostrar contiene un error sintactico. La manera correcta de usar una llamada a mostrar es la siguiente: \"mostrar ( EXPRESIONES );\" o \"mostrarp ( EXPRESIONES );\"\r\n(EXPRESIONES = expresiones de cualquier tipo, separadas por coma)";
            MensajeModoGrafico = MensajeModoTexto;
            SentenciasQueTienenElError.Add(Sentencias.Mostrar);
        }
    }

    public class ErrorFinProcValidacionFin : MensajeError
    {
        public ErrorFinProcValidacionFin()
            : base()
        {
            CodigoGlobal = 37;
            MensajeModoTexto = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
            MensajeModoGrafico = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionProcedimiento);
        }
    }

    public class ErrorFinFuncValidacionFin : MensajeError
    {
        public ErrorFinFuncValidacionFin()
            : base()
        {
            CodigoGlobal = 38;
            MensajeModoTexto = "El fin de la declaración de una función debe especificarse de la siguiente manera: finfunc;";
            MensajeModoGrafico = "El fin de la declaración de una función debe especificarse de la siguiente manera: finfunc;";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);
        }
    }

    public class ErrorFinFuncValidacionPorDefault : MensajeError
    {
        public ErrorFinFuncValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 39;
            MensajeModoTexto = "El fin de la declaracion de la funcion contiene un error sintactico.";
            MensajeModoGrafico = "El fin de la declaracion de la funcion contiene un error sintactico.";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);
        }
    }

    public class ErrorLeerRepetido : MensajeError
    {
        public ErrorLeerRepetido()
            : base()
        {
            CodigoGlobal = 40;
            MensajeModoTexto = "Hay llamadas a leer repetidas en la misma linea";
            MensajeModoGrafico = "Hay llamadas a leer repetidas en la misma linea";
            SentenciasQueTienenElError.Add(Sentencias.Leer);
        }
    }

    public class ErrorLeerSolo : MensajeError
    {
        public ErrorLeerSolo()
            : base()
        {
            CodigoGlobal = 41;
            MensajeModoTexto = "La llamada a leer debe estar acompañada de una variable o una posición de un arreglo";
            MensajeModoGrafico = "La llamada a leer debe estar acompañada de una variable o una posición de un arreglo";
            SentenciasQueTienenElError.Add(Sentencias.Leer);
        }
    }

    public class ErrorLeerNoIdentificador : MensajeError
    {
        public ErrorLeerNoIdentificador()
            : base()
        {
            CodigoGlobal = 42;
            MensajeModoTexto = "La llamada a leer puede estar acompañada unicamente de una variable o una posición de un arreglo";
            MensajeModoGrafico = "La llamada a leer puede estar acompañada unicamente de una variable o una posición de un arreglo";
            SentenciasQueTienenElError.Add(Sentencias.Leer);
        }
    }

    public class ErrorLexemaInvalido : MensajeError
    {
        public ErrorLexemaInvalido(string mensaje) 
            : base()
        {
            CodigoGlobal = 43;
            MensajeModoTexto = mensaje;
            MensajeModoGrafico = mensaje;
        }
    }
}
