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

        private static string variableContadoraLineas;
        public static string VariableContadoraLineas
        {
            get
            {
                return variableContadoraLineas;
            }
        }

        private static string archivoTemporalEstaEjecucion;
        public static string ArchivoTemporalEstaEjecucion
        {
            get
            {
                return archivoTemporalEstaEjecucion;
            }
        }

        private static string llamarCrearEntradaEnArchResultado;
        public static string LlamarCrearEntradaEnArchResultado
        {
            get
            {
                return llamarCrearEntradaEnArchResultado;
            }
        }

        private static string llamarCrearResSalidaEnArchResultado;
        public static string LlamarCrearResSalidaEnArchResultado
        {
            get
            {
                return llamarCrearResSalidaEnArchResultado;
            }
        }
        

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
            variableContadoraLineas = Utilidades.RandomManager.RandomStringConPrefijo("ProgramAr_ContLineas",20,true);
            archivoTemporalEstaEjecucion = Path.Combine(DirectorioTemporales,string.Format("{0}.xml",Utilidades.RandomManager.RandomStringConPrefijo("ProgramAr_ArchResName", 20, true)));
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


        internal static string CrearArchivoDeResultados()
        {
            return string.Format("CrearArchivoResultados('{0}'); ",archivoTemporalEstaEjecucion);
        }

        internal static string CrearProcedimientoResultadoCorrectoEnArchivo()
        {
            return string.Format("ColocarResultadoCorrectoEnArch('{0}'); ", archivoTemporalEstaEjecucion);
        }

        internal static string CrearProcedimientoResultadoIncorrectoEnArchivo()
        {
            return string.Format("ColocarResultadoIncorrectoEnArch('{0}'); ", archivoTemporalEstaEjecucion);
        }

        internal static string CrearErrorEnArch(string tipoError, string descError)
        {
            return string.Format("AgregarErrorArchivoResultados('{0}','{1}','{2}',{3}); ", 
                archivoTemporalEstaEjecucion,tipoError,descError,variableContadoraLineas);
        }

        internal static string CrearErrorEnArchConVariable(string tipoError, string variable)
        {
            return string.Format("AgregarErrorArchivoResultados('{0}','{1}',{2},{3}); ",
                archivoTemporalEstaEjecucion, tipoError, variable, variableContadoraLineas);
        }
        
        internal static string ArmarProcedimientoMarcarEntradaEnArchivo(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldrEncabezado = new StringBuilder();
            StringBuilder strBldrLlamado = new StringBuilder();
            StringBuilder strBldrTotal = new StringBuilder();

            string parametros = string.Empty;

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerVariablesDelProcPrincipal())
            {
                if (!item.EsConstante)
                {
                    string tipo = ObtenerTipoParaParametroPascal(item.EsArreglo, item.TipoDato);

                    strBldrEncabezado.AppendFormat("{0} : {1};", item.NombreParaCodigo, tipo);
                    strBldrLlamado.AppendFormat("{0},", item.NombreParaCodigo);
                }
            }

            llamarCrearEntradaEnArchResultado = string.Format("FrameworkProgramArProgramAr0000001InsertarEntradaEnArchResultados({0});",strBldrLlamado.ToString().TrimEnd(','));

            strBldrTotal.Append(" procedure FrameworkProgramArProgramAr0000001InsertarEntradaEnArchResultados( ").Append(strBldrEncabezado.ToString().TrimEnd(';')).Append(" ) ;").AppendLine();
            strBldrTotal.Append(" begin ").AppendLine();
            strBldrTotal.AppendFormat("  CrearNuevaEntradaParcial('{0}', {1}); ", archivoTemporalEstaEjecucion, variableContadoraLineas).AppendLine();

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerVariablesDelProcPrincipal())
            {
                if (!item.EsConstante)
                {
                    if (item.EsArreglo)
                    {
                        switch (item.TipoDato)
                        {
                            case NodoTablaSimbolos.TipoDeDato.Texto:
                                strBldrTotal.AppendFormat("  CrearNuevoArregloTextoEnEntradaEnLinea('{0}', {1},'{2}','{4}', {2},{3}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.NombreParaCodigo, item.Valor, item.NombreContextoLocal).AppendLine();
                                break;
                            case NodoTablaSimbolos.TipoDeDato.Numero:
                                strBldrTotal.AppendFormat("  CrearNuevoArregloNumeroEnEntradaEnLinea('{0}', {1},'{2}','{4}', {2},{3}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.NombreParaCodigo, item.Valor, item.NombreContextoLocal).AppendLine();
                                break;
                            case NodoTablaSimbolos.TipoDeDato.Booleano:
                                strBldrTotal.AppendFormat("  CrearNuevoArregloBooleanoEnEntradaEnLinea('{0}', {1},'{2}','{4}', {2},{3}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.NombreParaCodigo, item.Valor, item.NombreContextoLocal).AppendLine();
                                break;
                        }
                        

                    }
                    else
                    {
                        strBldrTotal.AppendFormat("  CrearNuevaVariableEnEntradaEnLinea('{0}', {1},'{2}','{3}',{2}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.NombreParaCodigo, item.NombreContextoLocal).AppendLine();
                    }
                }
            }

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerVariablesGlobales())
            {
                if (!item.EsConstante)
                {
                    if (item.EsArreglo)
                    {
                        switch (item.TipoDato)
                        {
                            case NodoTablaSimbolos.TipoDeDato.Texto:
                                strBldrTotal.AppendFormat("  CrearNuevoArregloTextoEnEntradaEnLinea('{0}', {1},'{2}','{5}',{3},{4}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreParaCodigo, item.Valor, item.Contexto.ToString()).AppendLine();
                                break;
                            case NodoTablaSimbolos.TipoDeDato.Numero:
                                strBldrTotal.AppendFormat("  CrearNuevoArregloNumeroEnEntradaEnLinea('{0}', {1},'{2}','{5}',{3},{4}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreParaCodigo, item.Valor, item.Contexto.ToString()).AppendLine();
                                break;
                            case NodoTablaSimbolos.TipoDeDato.Booleano:
                                strBldrTotal.AppendFormat("  CrearNuevoArregloBooleanoEnEntradaEnLinea('{0}', {1},'{2}','{5}',{3},{4}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreParaCodigo, item.Valor, item.Contexto.ToString()).AppendLine();
                                break;
                        }

                        

                    }
                    else
                    {
                        strBldrTotal.AppendFormat("  CrearNuevaVariableEnEntradaEnLinea('{0}', {1},'{2}','{4}',{3}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreParaCodigo, item.Contexto.ToString()).AppendLine();
                    }
                }
            }

            strBldrTotal.Append(" end; ").AppendLine();


            return strBldrTotal.ToString();
        }

        internal static string ArmarLlamadaResFinalEnArchivo(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldrLlamado = new StringBuilder();
            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerParametrosDelProcSalida())
            {
                if (!item.EsConstante)
                {
                    string tipo = ObtenerTipoParaParametroPascal(item.EsArreglo, item.TipoDato);

                    strBldrLlamado.AppendFormat("{0},", item.NombreParaCodigo);
                }
            }

            return string.Format("FrameworkProgramArProgramAr0000001InsertarResFinalEnArchResultados({0});", strBldrLlamado.ToString().TrimEnd(','));
        }

        internal static string ArmarProcedimientoResFinalEnArchivo(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldrEncabezado = new StringBuilder();
            StringBuilder strBldrTotal = new StringBuilder();

            string parametros = string.Empty;

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerParametrosDelProcSalida())
            {
                if (!item.EsConstante)
                {
                    string tipo = ObtenerTipoParaParametroPascal(item.EsArreglo, item.TipoDato);

                    strBldrEncabezado.AppendFormat("{0} : {1};", item.NombreParaCodigo, tipo);
                }
            }

            strBldrTotal.Append(" procedure FrameworkProgramArProgramAr0000001InsertarResFinalEnArchResultados( ").Append(strBldrEncabezado.ToString().TrimEnd(';')).Append(" ) ;").AppendLine();
            strBldrTotal.Append(" begin ").AppendLine();

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerParametrosDelProcSalida())
            {
                if (!item.EsConstante)
                {
                    if (item.EsArreglo)
                    {

                        strBldrTotal.AppendFormat("  CrearNuevoArregloEnResultado('{0}', '{1}','{3}',{1},{2}); ",
                                    archivoTemporalEstaEjecucion, item.NombreParaCodigo, item.Valor, item.NombreContextoLocal).AppendLine();

                    }
                    else
                    {
                        strBldrTotal.AppendFormat("  CrearNuevaVariableEnResultado('{0}', '{1}','{2}',{1}); ",
                                    archivoTemporalEstaEjecucion, item.NombreParaCodigo, item.NombreContextoLocal).AppendLine();
                    }
                }
            }

            strBldrTotal.Append(" end; ").AppendLine();


            return strBldrTotal.ToString();
        }

        private static string ObtenerTipoParaParametroPascal(bool esArreglo, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoDeDato)
        {
            string retorno = string.Empty;
            switch (tipoDeDato)
            {
                case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Texto:
                    if (esArreglo)
                    {
                        retorno = "array of string";
                    }
                    else
                    {
                        retorno = "string";
                    }
                    break;
                case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero:
                    if (esArreglo)
                    {
                        //retorno = "array of integer";
                        retorno = "array of real";
                    }
                    else
                    {
                        retorno = "real";
                    }
                    break;
                case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano:
                    if (esArreglo)
                    {
                        retorno = "array of boolean";
                    }
                    else
                    {
                        retorno = "boolean";
                    }
                    break;
                
            }

            return retorno;
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
