using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar
{
    public static class GlobalesCompilador
    {
        public const string NOMBRE_ARCH_COMPILADOR_PASCAL = @"Configuracion\CompiladorPascal\Compilador\bin\fpc\ppc386.exe";
        public const string NOMBRE_DIR_UNITS_PASCAL = @"Configuracion\CompiladorPascal\Compilador\extra";
        public const string NOMBRE_ARCH_CONFIG_APLICACION = @"configApp.xml";
        public const string NOMBRE_ARCH_RUTINASPREDEF_APLICACION = @"rutinasPredef.xml";
        public const string NOMBRE_ARCH_GRAMATICA =  @"Gramatica.xml";

        public const string NOMBRE_APLICACION = @"GarGar Dev";

        private static string pathEjecucionAplicacion = null;
        public static string PathEjecucionAplicacion
        {
            get
            {
                if (pathEjecucionAplicacion == null)
                {
                    pathEjecucionAplicacion = AppDomain.CurrentDomain.BaseDirectory;
                }
                return pathEjecucionAplicacion;
            }
        }

        internal const string NOMBRE_PROC_PRINCIPAL = "principal";

        internal const string PREFIJO_VARIABLES = "ProgramArVariable__00__";

        internal const double MIN_VALOR_NUMERO = -2.9e39;
        internal const double MAX_VALOR_NUMERO = 1.7e38;

        internal const double MAX_LONG_CADENA = 250;


        internal const int CANT_MAX_ITERACIONES = 1000000;

        internal const short CANT_MAX_ERRORES_SINTACTICOS = 5;

        internal static int UltFila;
        internal static int UltCol;


        public enum TipoError
        {
            Compilacion,
            Semantico,
            Sintactico,            
            Ninguno
        }

        public static void ReiniciarFilaYColumna()
        {
            GlobalesCompilador.UltFila = 0;
            GlobalesCompilador.UltCol = 0;
        }

        public static string ObtenerProgramaConEstructuraVacia()
        {
            StringBuilder strBldr = new StringBuilder();
       
            strBldr.AppendLine("procedimiento PRINCIPAL()");
            strBldr.AppendLine("comenzar");
            strBldr.AppendLine();
            strBldr.AppendLine("finproc;");
            


            return strBldr.ToString();
        }

        public static void AgregarLibreriasFramework(TablaSimbolos tablaSimbolos)
        {
            AgregarLibreriaNormal(tablaSimbolos);
            AgregarLibreriaMatematica(tablaSimbolos);

        }

        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion de la libreria de funciones Normal
        private static void AgregarLibreriaNormal(TablaSimbolos tablaSimbolos)
        {
            string nombre;
            string nombreFunc;
            string codigo;
            List<FirmaProc> parametros;

            nombre = "EsPar";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionEsPar(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Booleano, codigo, nombreFunc);

            nombre = "EsImpar";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionEsImpar(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Booleano, codigo, nombreFunc);

            nombre = "Redondear";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionRedondearAEntero(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            nombre = "Truncar";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionTruncar(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);


        }


        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion de la libreria de funciones matematicas
        private static void AgregarLibreriaMatematica(TablaSimbolos tablaSimbolos)
        {
            string nombre;
            string nombreFunc;
            string codigo;
            List<FirmaProc> parametros;

            nombre = "Potencia";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            parametros.Add(new FirmaProc("exp", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionPotencia(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            nombre = "Raiz";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            parametros.Add(new FirmaProc("exp", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionRaiz(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "PI";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            codigo = GeneracionCodigoHelpers.ArmarFuncionPI(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "ValAbs";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));            
            codigo = GeneracionCodigoHelpers.ArmarFuncionValorAbsoluto(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "Seno";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionSeno(nombreFunc,false);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "Coseno";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionCoseno(nombreFunc, false);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "Tangente";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionTangente(nombreFunc, false);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "rSeno";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionSeno(nombreFunc, true);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "rCoseno";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionCoseno(nombreFunc, true);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            // flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la funcion a la libreria matematica
            nombre = "rTangente";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionTangente(nombreFunc, true);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

        }
    }
}
