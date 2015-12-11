using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos;
using System.IO;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar
{

    class GeneracionCodigoHelpers
    {
        public static string DirectorioTemporales { get; set; }

        public static string VariableContadoraLineas {get; private set;}

        

        public static string DefinirFuncionesBasicas()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("function FrameworkProgramArProgramAr0000001EscribirBooleano( x : boolean) : string ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("retorno : string; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("if ( x ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tretorno := 'verdadero'; ");
            strBldr.AppendLine("end ");
            strBldr.AppendLine("else ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tretorno := 'falso'; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine("\tFrameworkProgramArProgramAr0000001EscribirBooleano := retorno; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            strBldr.AppendLine("function FrameworkProgramArProgramAr0000001EscribirReal( x : real) : string ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("aux : string; ");
            strBldr.AppendLine("longitud : integer; ");
            strBldr.AppendLine("begin ");
            //strBldr.AppendLine("aux := FloatToStrF(x, ffFixed,45, 15);");
            strBldr.AppendLine("aux := FormatFloat('#.###################################',x);");
              
            strBldr.AppendLine("longitud :=    Length(aux);");
            strBldr.AppendLine("while ((longitud > 0) and (aux[longitud] = '0')) do");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tDelete(aux, longitud, 1);");
            strBldr.AppendLine("\tlongitud := longitud - 1;");            
            strBldr.AppendLine("end; ");
            strBldr.AppendLine("longitud :=    Length(aux);");
            strBldr.AppendLine("if (aux[longitud] = ',' ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tDelete(aux, longitud, 1);");            
            strBldr.AppendLine("end");
            strBldr.AppendLine("else");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("\tif (aux[longitud] = '.') then");
            strBldr.AppendLine("\tbegin");
            strBldr.AppendLine("\t\tDelete(aux, longitud, 1);");
            strBldr.AppendLine("\tend;");
            
            strBldr.AppendLine("end;");
            
            strBldr.AppendLine("FrameworkProgramArProgramAr0000001EscribirReal := aux; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

         
            strBldr.AppendLine(GeneracionCodigoHelpers.DefinirConversionYChequeoIndiceArreglo());


            return strBldr.ToString();
        }

        private static string DefinirConversionYChequeoIndiceArreglo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("function FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo( num : real ; arregloAccedido : string ) : integer;");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("if (  num > trunc(num) ) then");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("raise EIndiceArregloInvalido.Create('El arreglo '+ arregloAccedido +' tenia un indice decimal. No se admiten arreglos con indices decimales.');");
            strBldr.AppendLine("end;");

            strBldr.AppendLine("FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo := trunc(num);");
            strBldr.AppendLine("end;");

            return strBldr.ToString();
        }

        public static string EscribirValorBooleano(string codigo)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("FrameworkProgramArProgramAr0000001EscribirBooleano( ");
            strBldr.Append(codigo);
            strBldr.Append(") ");
            

            return strBldr.ToString();
        }

        public static string EscribirValorNumerico(string codigo)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("FrameworkProgramArProgramAr0000001EscribirReal( ");
            strBldr.Append(codigo);
            strBldr.Append(") ");


            return strBldr.ToString();
        }

        public static string PausarHastaEntradaTeclado()
        {

            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(" readkey; ");

            return strBldr.ToString();
        }

        public static string AsignarLinea(int linea)
        {
            return string.Format("{0} := {1};",VariableContadoraLineas,linea);
        }


        internal static void ReiniciarValoresVariablesAleatorias()
        {
            VariableContadoraLineas = Utilidades.RandomManager.RandomStringConPrefijo("ProgramAr_ContLineas",20,true);
        }

        internal static string InicializarVariablesGlobales(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            return InicializarVariables(tablaSimbolos, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto.Global, string.Empty);
        }

        internal static string InicializarVariablesProc(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos, string nombreProc)
        {
            return InicializarVariables(tablaSimbolos, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto.Local, nombreProc);
        }

        private static string InicializarVariables(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto cont, string nombreProc)
        {
            StringBuilder strBldr = new StringBuilder();            

            List<CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos> listaVars = tablaSimbolos.ObtenerVariablesDeclaradasEnProcedimiento(cont, nombreProc);

            foreach (CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos item in listaVars)
            {
        
                switch (item.TipoDato)
                {
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Texto:
                        if (item.EsArreglo)
                        {
                            for (double i = 1; i <= item.Valor; i++)
                            {
                                strBldr.AppendLine(string.Format("{0}[{1}] := '';", item.NombreParaCodigo,i));
                            }
                        }
                        else
                        {
                            if (!item.EsConstante)
                            {
                                strBldr.AppendLine(string.Format("{0} := '';", item.NombreParaCodigo));
                            }
                        }
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero:
                        if (item.EsArreglo)
                        {
                            for (double i = 1; i <= item.Valor; i++)
                            {
                                strBldr.AppendLine(string.Format("{0}[{1}] := 0;", item.NombreParaCodigo,i));
                            }
                        }
                        else
                        {
                            if (!item.EsConstante)
                            {
                                strBldr.AppendLine(string.Format("{0} := 0;", item.NombreParaCodigo));
                            }
                        }
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano:
                        if (item.EsArreglo)
                        {
                            for (double i = 1; i <= item.Valor; i++)
                            {
                                strBldr.AppendLine(string.Format("{0}[{1}] := true;", item.NombreParaCodigo,i));
                            }
                        }
                        else
                        {
                            if (!item.EsConstante)
                            {
                                strBldr.AppendLine(string.Format("{0} := true;", item.NombreParaCodigo));
                            }
                        }
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Ninguno:
                        break;
                    default:
                        break;
                }

            }

            return strBldr.ToString();
        }

        internal static string DefinirVariablesAuxiliares(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldr = new StringBuilder();

            List<CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos> listaVars = tablaSimbolos.ObtenerAuxilairesParaCodIntermedio();

            foreach (CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos item in listaVars)
            {

                switch (item.TipoDato)
                {
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Texto:
                       
                        if (!item.EsConstante)
                        {
                            strBldr.AppendLine(string.Format("var {0} : string;", item.Nombre));
                        }
                        
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero:
                       
                        if (!item.EsConstante)
                        {
                            //strBldr.AppendLine(string.Format("var {0} : integer;", item.Nombre));
                            strBldr.AppendLine(string.Format("var {0} : real;", item.Nombre));
                        }
                        
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano:
                      
                        
                        if (!item.EsConstante)
                        {
                            strBldr.AppendLine(string.Format("var {0} : boolean;", item.Nombre));
                        }
                        
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Ninguno:
                        break;
                    default:
                        break;
                }

            }

            return strBldr.ToString();
        }




        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion del codigo pascal para la funcion potencia
        internal static string ArmarFuncionPotencia(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ; exponente : real) : real ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("res : real; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("if ( x = 0 ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tres := 0; ");
            strBldr.AppendLine("end ");
            strBldr.AppendLine("else ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tres :=  Exp(exponente*Ln(Abs(x)));");
            strBldr.AppendLine("end; ");
            strBldr.Append(nombreFunc).AppendLine(" := res;");
            //strBldr.Append(nombreFunc).AppendLine(" := power(x,exponente);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion del codigo pascal para la funcion raiz
        internal static string ArmarFuncionRaiz(string nombreFunc)
        {

            //Si el exponetne es 0, tiene que dar error

            //Si la base es negativa, tirar error
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ; exponente : real) : real ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("res : real; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("if ( exponente = 0 ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\traise EMatematicaRaizException.Create('Se paso 0 como raiz a aplicar. No se admite 0 en el exponente de la raiz');");
            //excepcion por exponente = 0
            strBldr.AppendLine("end; ");
            strBldr.AppendLine("if ( x > 0 ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tres :=  Exp((1/exponente)*Ln(Abs(x)));");
            strBldr.AppendLine("end ");
            strBldr.AppendLine("else ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\traise EMatematicaRaizException.Create('Se paso una base negativa en la raiz. El resultado seria un numero complejo y no estan admitidos en el lenguaje GarGar');");
            //excepcion por base negativa            
            strBldr.AppendLine("end; ");
            strBldr.Append(nombreFunc).AppendLine(" := res;");
            //strBldr.Append(nombreFunc).AppendLine(" := power(x,exponente);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion del codigo pascal para la funcion esPar
        internal static string ArmarFuncionEsPar(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : boolean ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("aux : integer; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("aux := trunc(x); ");

            strBldr.Append(nombreFunc).AppendLine(" := Not Odd(aux);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion del codigo pascal para la funcion EsImpar
        internal static string ArmarFuncionEsImpar(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : boolean ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("aux : integer; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("aux := trunc(x); ");

            strBldr.Append(nombreFunc).AppendLine(" := Odd(aux);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion del codigo pascal para la funcion truncar
        internal static string ArmarFuncionTruncar(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : real ;");
            strBldr.AppendLine("begin ");
            strBldr.Append(nombreFunc).AppendLine(" := trunc(x);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Creacion del codigo pascal para la funcion redondearAEntero
        internal static string ArmarFuncionRedondearAEntero(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : real ;");
            strBldr.AppendLine("begin ");
            strBldr.Append(nombreFunc).AppendLine(" := round(x);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }


        // flanzani 8/11/2012
        // IDC_APP_2
        // Agregar funciones por defecto en el framework
        // Agregado de las funciones del framework para ser creadas en codigo pascal
        internal static string DefinirFuncionesFramework(Semantico.TablaDeSimbolos.TablaSimbolos tabla)
        {
            StringBuilder strBldr = new StringBuilder();

            foreach (NodoTablaSimbolos item in tabla.ListaNodos.FindAll(x => x.EsDelFramework))
            {
                strBldr.AppendLine(item.CodigoPascalParaElFramework);
                strBldr.AppendLine();
            }

            return strBldr.ToString();
        }

       

        // flanzani 15/11/2012
        // IDC_APP_6
        // Agregar funciones matematicas al framework
        // Agregado de las funciones del framework para ser creadas en codigo pascal
        internal static string ArmarFuncionPI(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("() : real ;");
            strBldr.AppendLine("begin ");
            strBldr.Append(nombreFunc).AppendLine(" := Pi();");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 15/11/2012
        // IDC_APP_6
        // Agregar funciones matematicas al framework
        // Agregado de las funciones del framework para ser creadas en codigo pascal
        internal static string ArmarFuncionValorAbsoluto(string nombreFunc)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : real ;");
            strBldr.AppendLine("begin ");
            strBldr.Append(nombreFunc).AppendLine(" := abs(x);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 15/11/2012
        // IDC_APP_6
        // Agregar funciones matematicas al framework
        // Agregado de las funciones del framework para ser creadas en codigo pascal
        internal static string ArmarFuncionSeno(string nombreFunc, bool esRadianes)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : real ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("aux : real; ");
            strBldr.AppendLine("begin ");
            if (esRadianes)
            {
                strBldr.AppendLine("aux := x ;");
            }
            else
            {
                strBldr.AppendLine("aux := x / 57.2957795 ;");
            }
            
            strBldr.Append(nombreFunc).AppendLine(" := sin(aux);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 15/11/2012
        // IDC_APP_6
        // Agregar funciones matematicas al framework
        // Agregado de las funciones del framework para ser creadas en codigo pascal
        internal static string ArmarFuncionCoseno(string nombreFunc, bool esRadianes)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : real ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("aux : real; ");
            strBldr.AppendLine("begin ");
            if (esRadianes)
            {
                strBldr.AppendLine("aux := x ;");
            }
            else
            {
                strBldr.AppendLine("aux := x / 57.2957795 ;");
            }
            strBldr.Append(nombreFunc).AppendLine(" := cos(aux);");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }

        // flanzani 15/11/2012
        // IDC_APP_6
        // Agregar funciones matematicas al framework
        // Agregado de las funciones del framework para ser creadas en codigo pascal
        internal static string ArmarFuncionTangente(string nombreFunc, bool esRadianes)
        {
            //Si el coseno de x es 0, tiene que dar error
            
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("function ").Append(nombreFunc).AppendLine("( x : real ) : real ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("aux : real; ");
            strBldr.AppendLine("res : real; ");
            strBldr.AppendLine("resCos : real; ");
            strBldr.AppendLine("begin ");
            if (esRadianes)
            {
                strBldr.AppendLine("aux := x ;");
            }
            else
            {
                strBldr.AppendLine("aux := x / 57.2957795 ;");
            }
            strBldr.AppendLine("resCos := cos(aux);");
            strBldr.AppendLine("if ( resCos = 0 ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\traise EMatematicaTangenteException.Create('Se paso 0 como raiz a aplicar. No se admite 0 en el exponente de la raiz');");
            //excepcion por exponente = 0
            strBldr.AppendLine("end; ");
            strBldr.Append(nombreFunc).AppendLine(" := sin (aux) / resCos;");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            return strBldr.ToString();
        }
    }
}
