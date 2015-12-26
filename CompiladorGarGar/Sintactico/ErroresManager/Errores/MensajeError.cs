using CompiladorGargar.Auxiliares;
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
        public string Mensaje { get; set; }
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
            Mensaje = "";
            EsErrorBienDefinido = false;
        }
    }



    public class ErrorDeclaracionConstanteGenerico : MensajeError
    {
        public ErrorDeclaracionConstanteGenerico()
            : base()
        {
            CodigoGlobal = 1;
            Mensaje = "La declaración de la constante contiene un error sintactico. La manera correcta de declarar una constante es: \"const EJEMPLO : TIPO = VALOR;\"\r\n(TIPO = \"numero|texto|booleano\")";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorTipoDatoDefRepetido : MensajeError
    {
        public ErrorTipoDatoDefRepetido()
            : base()
        {
            CodigoGlobal = 2;
            Mensaje = "El : esta especificado mas de una vez en la declaración";
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
            Mensaje = ": faltante en la declaración";
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
            Mensaje = "El = esta especificado mas de una vez en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorAsignarValorFaltante : MensajeError
    {
        public ErrorAsignarValorFaltante()
            : base()
        {
            CodigoGlobal = 5;
            Mensaje = "= faltante en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorConstanteTipoDatoSinArreglo : MensajeError
    {
        public ErrorConstanteTipoDatoSinArreglo()
            : base()
        {
            CodigoGlobal = 6;
            Mensaje = "Las constantes no pueden ser arreglos";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorTipoDatoRepetido : MensajeError
    {
        public ErrorTipoDatoRepetido()
            : base()
        {
            CodigoGlobal = 7;
            Mensaje = "El tipo de dato esta especificado mas de una vez en la declaración";
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
            Mensaje = "Tipo de dato faltante en la declaración";
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
            Mensaje = "El valor de constante esta especificado mas de una vez en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
            
        }
    }

    public class ErrorConstanteValorFaltante : MensajeError
    {
        public ErrorConstanteValorFaltante()
            : base()
        {
            CodigoGlobal = 10;
            Mensaje = "Valor de constante faltante en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);
        }
    }

    public class ErrorConstanteElementoQueSobraErroneo : MensajeError
    {
        public ErrorConstanteElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 11;
            Mensaje = string.Format("{0} no tiene lugar en una declaración de constante", elementoErroneo);
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
            Mensaje = "El = esta especificado mas de una vez en la asignacion";

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
            Mensaje = "= faltante en la asignacion";

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionParteDerechaCorrecta : MensajeError
    {
        public ErrorAsignacionParteDerechaCorrecta()
            : base()
        {
            CodigoGlobal = 14;
            Mensaje = "Error sintactico en la parte derecha de la asignación";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionParteIzqCorrecta : MensajeError
    {
        public ErrorAsignacionParteIzqCorrecta()
            : base()
        {
            CodigoGlobal = 15;
            Mensaje = "Error sintactico en la parte izq de la asignación";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 16;
            Mensaje = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 17;
            Mensaje = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 18;
            Mensaje = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 19;
            Mensaje = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteIzq : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteIzq()
            : base()
        {
            CodigoGlobal = 20;
            Mensaje = "La asignacion contiene una expresión mal formada en su parte izquierda";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteDer : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteDer()
            : base()
        {
            CodigoGlobal = 21;
            Mensaje = "La asignacion contiene una expresión mal formada en su parte derecha";
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
            Mensaje = "La asignacion contiene un error sintactico. La manera correcta de usar una asignación es la siguiente: \"VARIABLE = EXPRESION;\"\r\n(VARIABLE = variable o posición de arreglo)\r\n(EXPRESION = expresión de cualquier tipo)";
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
        }
    }

    public class ErrorDeclaracionFuncionValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionFuncionValidacionPorDefault()
            : base()
        {

            CodigoGlobal = 23;
            Mensaje = "La declaración de la función contiene un error sintactico. La manera correcta de declarar una función es la siguiente: \"funcion EJEMPLO ( PARAMETROS ) : TIPO comenzar BLOQUE finfunc RETORNO;\"\r\n(PARAMETROS = \"param1 : TIPO, param2 : arreglo[MAX] de TIPO\")\r\n(TIPO = \"numero|texto|booleano\")\r\n(BLOQUE = contenido de la función)\r\n(RETORNO = expresión conteniendo el retorno de la función)";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);
        }
    }

    public class ErrorDeclaracionProcedimientoValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionProcedimientoValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 24;
            Mensaje = "La declaración del procedimiento contiene un error sintactico. La manera correcta de declarar un procedimiento es la siguiente: \"procedimiento EJEMPLO ( PARAMETROS ) comenzar BLOQUE finproc;\"\r\n(PARAMETROS = \"param1 : TIPO, param2 : arreglo[MAX] de TIPO\")\r\n(TIPO = \"numero|texto|booleano\")\r\n(BLOQUE = contenido del procedimiento)";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionProcedimiento);
        }
    }

    public class ErrorDeclaracionVariableValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionVariableValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 25;
            Mensaje = "La declaración de la variable contiene un error sintactico. La manera correcta de declarar una variable es la siguiente: \"var LISTA : TIPO\" o \"var LISTA : arreglo[MAX] de TIPO\"\r\n(LISTA = \"nombre1,nombre2,nombre3...\")\r\n(TIPO = \"numero|texto|booleano\")";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableParteIzquierdaValida : MensajeError
    {
        public ErrorDeclaracionVariableParteIzquierdaValida()
            : base()
        {
            CodigoGlobal = 26;
            Mensaje = "La declaración de variables es incorrecta. Debe ser una lista de identificadores separados por comas o un identificador solo";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableCantArregloNoRepetido : MensajeError
    {
        public ErrorDeclaracionVariableCantArregloNoRepetido()
            : base()
        {
            CodigoGlobal = 27;
            Mensaje = "El arreglo esta especificado mas de una vez en la declaración";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorDeclaracionVariableCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 28;
            Mensaje = "Los corchetes no estan balanceados en la declaracion del arreglo";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorDeclaracionVariableElementoQueSobraErroneo : MensajeError
    {
        public ErrorDeclaracionVariableElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 29;
            Mensaje = string.Format("Error sintactico: {0} es incorrecto en la declaración de un arreglo. La forma correcta es arreglo [MAX] de TIPO", elementoErroneo);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);
        }
    }

    public class ErrorMientrasValidacionPorDefault : MensajeError
    {
        public ErrorMientrasValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 30;
            Mensaje = "El mientras contiene un error sintactico. La manera correcta de usar un mientras es la siguiente: \"mientras ( EXPRBOOLEANA ) hacer BLOQUE finmientras;\"\r\n(EXPRBOOLEANA = expresión del tipo booleana)\r\n(BLOQUE = contenido del mientras)";
            SentenciasQueTienenElError.Add(Sentencias.Mientras);
        }
    }

    public class ErrorFinSiValidacionFin : MensajeError
    {
        public ErrorFinSiValidacionFin()
            : base()
        {
            CodigoGlobal = 31;
            Mensaje = "El fin de un bloque si debe especificarse de la siguiente manera: finsi;";
            SentenciasQueTienenElError.Add(Sentencias.Si);
        }
    }

    public class ErrorLlamadoProcValidacionPorDefault : MensajeError
    {
        public ErrorLlamadoProcValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 32;
            Mensaje = "La llamada al procedimiento contiene un error sintactico. La manera correcta de usar una llamada a procedimiento es la siguiente: \"llamar NOMBREPROC ( EXPRESIONES );\"\r\n(EXPRESIONES = expresiones de cualquier tipo, separadas por coma)";
            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);
        }
    }

    public class ErrorLeerValidacionPorDefault : MensajeError
    {
        public ErrorLeerValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 33;
            Mensaje = "La llamada a leer contiene un error sintactico. La manera correcta de usar una llamada a leer es la siguiente: \"leer VARIABLE;\"\r\n(VARIABLE = variable o posición de arreglo)";
        }
    }

    public class ErrorFinMientrasValidacionFin : MensajeError
    {
        public ErrorFinMientrasValidacionFin()
            : base()
        {
            CodigoGlobal = 34;
            Mensaje = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
            SentenciasQueTienenElError.Add(Sentencias.Mientras);
        }
    }

    public class ErrorSiValidacionPorDefault : MensajeError
    {
        public ErrorSiValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 35;
            Mensaje = "La declaracion del bloque si contiene un error sintactico.  La manera correcta de declarar un si es la siguiente: \"si ( EXPRBOOLEANA ) entonces BLOQUE sino BLOQUE finsi;\"\r\n(EXPRBOOLEANA = expresión del tipo booleana)\r\n(BLOQUE = contenido del si/sino)";
            SentenciasQueTienenElError.Add(Sentencias.Si);
        }
    }

    public class ErrorMostrarValidacionPorDefault : MensajeError
    {
        public ErrorMostrarValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 36;
            Mensaje = "La llamada a mostrar contiene un error sintactico. La manera correcta de usar una llamada a mostrar es la siguiente: \"mostrar ( EXPRESIONES );\" o \"mostrarp ( EXPRESIONES );\"\r\n(EXPRESIONES = expresiones de cualquier tipo, separadas por coma)";
            SentenciasQueTienenElError.Add(Sentencias.Mostrar);
        }
    }

    public class ErrorFinProcValidacionFin : MensajeError
    {
        public ErrorFinProcValidacionFin()
            : base()
        {
            CodigoGlobal = 37;
            Mensaje = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionProcedimiento);
        }
    }

    public class ErrorFinFuncValidacionFin : MensajeError
    {
        public ErrorFinFuncValidacionFin()
            : base()
        {
            CodigoGlobal = 38;
            Mensaje = "El fin de la declaración de una función debe especificarse de la siguiente manera: finfunc RETORNO;";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);
        }
    }

    public class ErrorFinFuncValidacionPorDefault : MensajeError
    {
        public ErrorFinFuncValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 39;
            Mensaje = "El fin de la declaracion de la funcion contiene un error sintactico.";
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);
        }
    }

    public class ErrorLeerRepetido : MensajeError
    {
        public ErrorLeerRepetido()
            : base()
        {
            CodigoGlobal = 40;
            Mensaje = "Hay llamadas a leer repetidas en la misma linea";
            SentenciasQueTienenElError.Add(Sentencias.Leer);
        }
    }

    public class ErrorLeerSolo : MensajeError
    {
        public ErrorLeerSolo()
            : base()
        {
            CodigoGlobal = 41;
            Mensaje = "La llamada a leer debe estar acompañada de una variable o una posición de un arreglo";
            SentenciasQueTienenElError.Add(Sentencias.Leer);
        }
    }

    public class ErrorLeerNoIdentificador : MensajeError
    {
        public ErrorLeerNoIdentificador()
            : base()
        {
            CodigoGlobal = 42;
            Mensaje = "La llamada a leer puede estar acompañada unicamente de una variable o una posición de un arreglo";
            SentenciasQueTienenElError.Add(Sentencias.Leer);
        }
    }

    public class ErrorLexemaInvalido : MensajeError
    {
        public ErrorLexemaInvalido(string mensaje) 
            : base()
        {
            CodigoGlobal = 43;
            Mensaje = mensaje;
        }
    }


    public class ErrorArregloAsignarTipoInvalido : MensajeError
    {
        public ErrorArregloAsignarTipoInvalido(string nombreArr, 
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoArr,  
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoAsignado)
            : base()
        {
            CodigoGlobal = 1001;
            
            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El arreglo ").Append(nombreArr).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipoArr));
            strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoAsignado));

            Mensaje = strbldr.ToString();
        }
    }


    public class ErrorUsoVariableNoDeclarada : MensajeError
    {
        public ErrorUsoVariableNoDeclarada(string nombre)
            : base()
        {
            CodigoGlobal = 1002;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorUsoVariableComoArreglo : MensajeError
    {
        public ErrorUsoVariableComoArreglo(string nombre)
            : base()
        {
            CodigoGlobal = 1003;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" esta declarada como variable y se intento usar como arreglo");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorUsoConstanteComoVariable : MensajeError
    {
        public ErrorUsoConstanteComoVariable(string nombre)
            : base()
        {
            CodigoGlobal = 1004;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" esta declarada como una constante, no puede modificarse su valor.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorVariableAsignarTipoInvalido : MensajeError
    {
        public ErrorVariableAsignarTipoInvalido(string nombre,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoAsignado)
            : base()
        {
            CodigoGlobal = 1005;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
            strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoAsignado));

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorUsoArregloSinIndice : MensajeError
    {
        public ErrorUsoArregloSinIndice(string nombre)
            : base()
        {
            CodigoGlobal = 1006;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es un arreglo. Debe usar un indice para asignarle el contenido");
            strbldr.Append(" a una de sus posiciones. No se puede asignar el contenido total de un arreglo a otro. ");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorCondicionMientrasNoBooleana : MensajeError
    {
        public ErrorCondicionMientrasNoBooleana()
            : base()
        {
            CodigoGlobal = 1007;

            SentenciasQueTienenElError.Add(Sentencias.Mientras);

            StringBuilder strbldr = new StringBuilder("La condicion resultante del bloque mientras debe ser booleana");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorCondicionSiNoBooleana : MensajeError
    {
        public ErrorCondicionSiNoBooleana()
            : base()
        {
            CodigoGlobal = 1008;

            SentenciasQueTienenElError.Add(Sentencias.Si);

            StringBuilder strbldr = new StringBuilder("La condicion resultante del bloque si debe ser booleana");

            Mensaje = strbldr.ToString();
        }
    }


    public class ErrorTipoInvalidoEnConstante : MensajeError
    {
        public ErrorTipoInvalidoEnConstante(string nombre,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoAsignado)            
            : base()
        {
            CodigoGlobal = 1009;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);

            StringBuilder strbldr = new StringBuilder("La constante ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
            strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoAsignado));

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorConstanteRepetida : MensajeError
    {
        public ErrorConstanteRepetida(string nombre)
            : base()
        {
            CodigoGlobal = 1010;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);

            StringBuilder strbldr = new StringBuilder("La constante ").Append(nombre).Append(" ya existia en ese contexto");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorVariableRepetida : MensajeError
    {
        public ErrorVariableRepetida(string nombre)
            : base()
        {
            CodigoGlobal = 1011;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" ya existia en ese contexto");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorIntentarLeerVariableBooleana : MensajeError
    {
        public ErrorIntentarLeerVariableBooleana(string nombre)
            : base()
        {
            CodigoGlobal = 1012;

            SentenciasQueTienenElError.Add(Sentencias.Leer);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es booleana, no se le puede asignar un valor desde el teclado, porque sus unicos valores son verdadero y falso.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorIndiceArregloNoNumerico : MensajeError
    {
        public ErrorIndiceArregloNoNumerico()
            : base()
        {
            CodigoGlobal = 1013;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El indice del arreglo debe ser un numero.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorArregloRepetido : MensajeError
    {
        public ErrorArregloRepetido(string nombre)
            : base()
        {
            CodigoGlobal = 1014;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);

            StringBuilder strbldr = new StringBuilder("El arreglo ").Append(nombre).Append(" ya existia.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorArregloTopeDecimal : MensajeError
    {
        public ErrorArregloTopeDecimal()
            : base()
        {
            CodigoGlobal = 1015;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);

            StringBuilder strbldr = new StringBuilder("El tope de un arreglo no puede ser decimal.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorValorNumericoMuyGrande : MensajeError
    {
        public ErrorValorNumericoMuyGrande(double valor)
            : base()
        {
            CodigoGlobal = 1016;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);

            Mensaje = string.Format("No se pueden manejar valores numericos mas grandes que {0} en tiempo de compilacion", valor);
        }
    }

    public class ErrorValorNumericoMuyChico : MensajeError
    {
        public ErrorValorNumericoMuyChico(double valor)
            : base()
        {
            CodigoGlobal = 1017;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);

            Mensaje = string.Format("No se pueden manejar valores numericos mas chicos que {0} en tiempo de compilacion", valor);
        }
    }

    public class ErrorFuncionRepetido : MensajeError
    {
        public ErrorFuncionRepetido(string nombre)
            : base()
        {
            CodigoGlobal = 1018;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);

            StringBuilder strbldr = new StringBuilder("Ya existe la funcion ").Append(nombre);

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorProcedimientoRepetido : MensajeError
    {
        public ErrorProcedimientoRepetido(string nombre)
            : base()
        {
            CodigoGlobal = 1019;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionProcedimiento);

            StringBuilder strbldr = new StringBuilder("Ya existe el procedimiento ").Append(nombre);

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorProcedimientoPrincipalComoFuncion : MensajeError
    {
        public ErrorProcedimientoPrincipalComoFuncion()
            : base()
        {
            CodigoGlobal = 1020;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);

            StringBuilder strbldr = new StringBuilder("Principal no puede ser una funcion. Debe ser forzosamente un procedimiento");

            Mensaje = strbldr.ToString();
        }
    }


    public class ErrorFuncionDevuelveExpresionOtroTipo : MensajeError
    {
        public ErrorFuncionDevuelveExpresionOtroTipo(string nombre,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoExp)
            : base()
        {
            CodigoGlobal = 1021;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);

            StringBuilder strbldr = new StringBuilder("La funcion ").Append(nombre).Append(" esta declarada como ").Append(EnumUtils.stringValueOf(tipo)).Append(" pero la expresion que devuelve es ").Append(EnumUtils.stringValueOf(tipoExp));

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorUsoFuncionNoDeclarada : MensajeError
    {
        public ErrorUsoFuncionNoDeclarada(string nombre)
            : base()
        {
            CodigoGlobal = 1022;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La funcion ").Append(nombre).Append(" no esta declarada.");

            Mensaje = strbldr.ToString();
        }
    }
    public class ErrorUsoProcedimientoNoDeclarado : MensajeError
    {
        public ErrorUsoProcedimientoNoDeclarado(string nombre)
            : base()
        {
            CodigoGlobal = 1023;

            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("El procedimiento ").Append(nombre).Append(" no esta declarado.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorCantidadIncorrectaParamentrosFuncion : MensajeError
    {
        public ErrorCantidadIncorrectaParamentrosFuncion(string nombre)
            : base()
        {
            CodigoGlobal = 1024;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La cantidad de parametros para la funcion ").Append(nombre).Append(" es incorrecta.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorCantidadIncorrectaParamentrosProcedimiento : MensajeError
    {
        public ErrorCantidadIncorrectaParamentrosProcedimiento(string nombre)
            : base()
        {
            CodigoGlobal = 1025;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La cantidad de parametros para el procedimiento ").Append(nombre).Append(" es incorrecta.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorDeclaracionConstanteFueraLugar : MensajeError
    {
        public ErrorDeclaracionConstanteFueraLugar()
            : base()
        {
            CodigoGlobal = 1026;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionConstante);

            StringBuilder strbldr = new StringBuilder("No se permiten declarar constantes aqui. Las constantes deben ser creadas en el contexto global, al principio del programa.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorOperacionesConArreglo : MensajeError
    {
        public ErrorOperacionesConArreglo()
            : base()
        {
            CodigoGlobal = 1027;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("No se puede realizar operaciones logicas o aritmeticas con un ");
            strbldr.Append(" arreglo. Las operaciones logicas y aritmenticas se pueden realizar únicamente con las posiciones de un arreglo");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorOperadoresBooleanosConExprNoBooleanas : MensajeError
    {
        public ErrorOperadoresBooleanosConExprNoBooleanas()
            : base()
        {
            CodigoGlobal = 1028;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("Los operadores logicos and y or ");
            strbldr.Append("solo pueden ser usados con expresiones del tipo booleanas");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorOperadoresComparacionConExprNoNumericas : MensajeError
    {
        public ErrorOperadoresComparacionConExprNoNumericas()
            : base()
        {
            CodigoGlobal = 1029;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("Los comparadores mayor, mayor igual, menor y menor igual ");
            strbldr.Append("solo pueden ser usados con expresiones del tipo numericas");
                        

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorCompararExpresionesTipoIncorrecto : MensajeError
    {
        public ErrorCompararExpresionesTipoIncorrecto(CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo1,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo2)
            : base()
        {
            CodigoGlobal = 1030;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);

            StringBuilder strbldr = new StringBuilder("Se esta intentando comparar una expresion del tipo ").Append(EnumUtils.stringValueOf(tipo1));
            strbldr.Append(" con una del tipo ").Append(EnumUtils.stringValueOf(tipo2));

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorOperarExpresionesTipoIncorrecto : MensajeError
    {
        public ErrorOperarExpresionesTipoIncorrecto(CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo1,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo2)
            : base()
        {
            CodigoGlobal = 1031;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionFuncion);

            StringBuilder strbldr = new StringBuilder("Se esta intentando operar entre una expresion del tipo ").Append(EnumUtils.stringValueOf(tipo1));
            strbldr.Append(" y otra del tipo ").Append(EnumUtils.stringValueOf(tipo2));

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametrosConMismoNombre : MensajeError
    {
        public ErrorParametrosConMismoNombre()
            : base()
        {
            CodigoGlobal = 1032;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);
            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("Existen parametros con el mismo nombre.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorConstanteNumericaInvalidaEnTopeArreglo : MensajeError
    {
        public ErrorConstanteNumericaInvalidaEnTopeArreglo(string nombre)
            : base()
        {
            CodigoGlobal = 1033;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La constante numerica ").Append(nombre).Append(" no es mayor a 0. Solo se pueden usar constantes o numeros mayores a 0 al especificar el rango de los arreglos.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorConstanteNoNumericaEnTopeArreglo : MensajeError
    {
        public ErrorConstanteNoNumericaEnTopeArreglo(string nombre)
            : base()
        {
            CodigoGlobal = 1034;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La constante ").Append(nombre).Append(" no es del tipo numero. Solo se pueden usar constantes numericas o numeros al especificar el rango de los arreglos.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorNoConstanteEnTopeArreglo : MensajeError
    {
        public ErrorNoConstanteEnTopeArreglo(string nombre)
            : base()
        {
            CodigoGlobal = 1035;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no es una constante. Solo se pueden usar constantes o numeros al especificar el rango de los arreglos.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorNumeroInvalidoEnTopeArreglo : MensajeError
    {
        public ErrorNumeroInvalidoEnTopeArreglo(string nombre)
            : base()
        {
            CodigoGlobal = 1036;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El numero ").Append(nombre).Append(" no es mayor a 0. Solo se pueden usar constantes o numeros mayores a 0 al especificar el rango de los arreglos.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorNoNumeroEnTopeArreglo : MensajeError
    {
        public ErrorNoNumeroEnTopeArreglo(string nombre)
            : base()
        {
            CodigoGlobal = 1037;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("").Append(nombre).Append(" no es del tipo numero. Solo se pueden usar constantes numericas o numeros al especificar el rango de los arreglos.");

            Mensaje = strbldr.ToString();
        }
    }


    public class ErrorDeclaracionVariableFueraLugar : MensajeError
    {
        public ErrorDeclaracionVariableFueraLugar()
            : base()
        {
            CodigoGlobal = 1038;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionVariable);

            StringBuilder strbldr = new StringBuilder("No se permiten declarar variables aqui. Las variables deben ser creadas en el contexto global al principio del programa o en la zona de declaraciones de un procedimiento o funcion");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorNegarExprNoBooleana : MensajeError
    {
        public ErrorNegarExprNoBooleana()
            : base()
        {
            CodigoGlobal = 1039;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("Unicamente se pueden negar expresiones booleanas");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorProcedimientoPrincipalMalUbicado : MensajeError
    {
        public ErrorProcedimientoPrincipalMalUbicado()
            : base()
        {
            CodigoGlobal = 1040;

            SentenciasQueTienenElError.Add(Sentencias.DeclaracionProcedimiento);

            StringBuilder strbldr = new StringBuilder("Error en el procedimiento principal: Debe haber unicamente un procedimiento principal y debe ser el ultimo.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorDivisionPorCero : MensajeError
    {
        public ErrorDivisionPorCero()
            : base()
        {
            CodigoGlobal = 1041;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("No se puede dividir por cero.");

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroArregloEnLugarDeVariableEnFuncion : MensajeError
    {
        public ErrorParametroArregloEnLugarDeVariableEnFuncion(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1042;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" pasado a la funcion ");
            strbldr.Append(funcion).Append(" debe ser una variable, y se paso una arreglo.");
            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroVariableEnLugarDeArregloEnFuncion : MensajeError
    {
        public ErrorParametroVariableEnLugarDeArregloEnFuncion(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1043;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" pasado a la funcion ");
            strbldr.Append(funcion).Append(" debe ser un arreglo, y se paso una variable.");
            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroConstantePorRefEnFuncion : MensajeError
    {
        public ErrorParametroConstantePorRefEnFuncion(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1044;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" de la función ");
            strbldr.Append(funcion).Append(" esta definido por referencia, y se le intento pasar una constante. No se pueden pasar constantes por referencia.");
            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroExpresionPorRefEnFuncion : MensajeError
    {
        public ErrorParametroExpresionPorRefEnFuncion(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1045;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" de la función ");
            strbldr.Append(funcion).Append(" esta especificado como por referencia y se le intento pasar una expresion. El parametro no puede ser el resultado de una expresion o un valor constante o una función. De ser necesario, asigne el valor a una nueva variable para pasarla por parametro.");
            Mensaje = strbldr.ToString();
        }
    }


    public class ErrorParametroTipoIncorrectoEnFuncion : MensajeError
    {
        public ErrorParametroTipoIncorrectoEnFuncion(string param,
            string funcion,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo)
            : base()
        {
            CodigoGlobal = 1046;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("Se intento pasar un tipo incorrecto al parametro ").Append(param).Append(" de la función ");
            strbldr.Append(funcion).Append(". Debe ser de tipo ").Append(EnumUtils.stringValueOf(tipo));

            Mensaje = strbldr.ToString();
        }
    }

    //


    public class ErrorParametroArregloEnLugarDeVariableEnProc : MensajeError
    {
        public ErrorParametroArregloEnLugarDeVariableEnProc(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1047;

            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" pasado al procedimiento ");
            strbldr.Append(funcion).Append(" debe ser una variable, y se paso una arreglo.");
            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroVariableEnLugarDeArregloEnProc : MensajeError
    {
        public ErrorParametroVariableEnLugarDeArregloEnProc(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1048;

            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" pasado al procedimiento ");
            strbldr.Append(funcion).Append(" debe ser un arreglo, y se paso una variable.");
            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroConstantePorRefEnProc : MensajeError
    {
        public ErrorParametroConstantePorRefEnProc(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1049;

            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" del procedimiento ");
            strbldr.Append(funcion).Append(" esta definido por referencia, y se le intento pasar una constante. No se pueden pasar constantes por referencia.");
            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorParametroExpresionPorRefEnProc : MensajeError
    {
        public ErrorParametroExpresionPorRefEnProc(string param,
            string funcion)
            : base()
        {
            CodigoGlobal = 1050;

            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("El parametro ").Append(param).Append(" del procedimiento ");
            strbldr.Append(funcion).Append(" esta especificado como por referencia y se le intento pasar una expresion. El parametro no puede ser el resultado de una expresion o un valor constante o una función. De ser necesario, asigne el valor a una nueva variable para pasarla por parametro.");
            Mensaje = strbldr.ToString();
        }
    }


    public class ErrorParametroTipoIncorrectoEnProc : MensajeError
    {
        public ErrorParametroTipoIncorrectoEnProc(string param,
            string funcion,
            CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipo)
            : base()
        {
            CodigoGlobal = 1051;

            SentenciasQueTienenElError.Add(Sentencias.LlamarProcedimiento);

            StringBuilder strbldr = new StringBuilder("Se intento pasar un tipo incorrecto al parametro ").Append(param).Append(" del procedimiento ");
            strbldr.Append(funcion).Append(". Debe ser de tipo ").Append(EnumUtils.stringValueOf(tipo));

            Mensaje = strbldr.ToString();
        }
    }

    public class ErrorCantMaxCaracteresEnCadenaTexto : MensajeError
    {
        public ErrorCantMaxCaracteresEnCadenaTexto(double cantMax)
            : base()
        {
            CodigoGlobal = 1052;

            SentenciasQueTienenElError.Add(Sentencias.Asignacion);

            StringBuilder strbldr = new StringBuilder("No se pueden manejar cadenas de texto mayores a ").Append(cantMax).Append(" caracteres en tiempo de compilación");

            Mensaje = strbldr.ToString();
        }
    }
}
