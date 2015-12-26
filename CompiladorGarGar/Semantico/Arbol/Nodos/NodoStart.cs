using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;


using System.IO;
using System.Configuration;
using CompiladorGargar.Sintactico.ErroresManager.Errores;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoStart : NodoArbolSemantico 
    {
        public NodoStart(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ContextoActual = NodoTablaSimbolos.TipoContexto.Global;
            this.NombreContextoLocal = EnumUtils.stringValueOf(NodoTablaSimbolos.TipoContexto.Global);
            this.ProcPrincipalYaCreadoyCorrecto = false;
            this.ProcPrincipalCrearUnaVez = true;

            
        }


        public override void ChequearAtributos(Terminal t)
        {
            
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                throw new ErrorSemanticoException(new ErrorProcedimientoPrincipalMalUbicado());
            }

         
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                this.ProcPrincipalYaCreadoyCorrecto = true;
            }


            return this;
        }

   
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            //{
            //    ArmarActividadViewModel();
            //}
            return this;
        }

     
        public override void CalcularCodigo(bool modoDebug)
        {
			// flanzani 15/11/2012
            // IDC_APP_6
            // Agregar funciones matematicas al framework
            // Agrego la excepcion en pascal para cuando la tangente es invalida y da excepcion
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("program temporal;");
            strBldr.AppendLine("uses crt, Sysutils;");
            strBldr.AppendLine("");

            if (!string.IsNullOrWhiteSpace(this.hijosNodo[0].ConstantesGlobales))
            {
                strBldr.AppendLine("Const");
                strBldr.AppendLine(this.hijosNodo[0].ConstantesGlobales);
            }

            strBldr.AppendLine("Type");
            strBldr.AppendLine("EIteracionInfinitaException = class(Exception);");
            strBldr.AppendLine("EIndiceArregloInvalido = class(Exception);");
            strBldr.AppendLine("EMatematicaRaizException = class(Exception);");      
            strBldr.AppendLine("EMatematicaTangenteException = class(Exception);");      
            strBldr.AppendLine(ArmarTiposDeArreglo(this.TablaSimbolos.ListaTiposArreglos));
            strBldr.AppendLine("Var");
            strBldr.AppendLine("UserFile : Text;");
            
            strBldr.AppendLine(string.Format("{0} : integer;",GeneracionCodigoHelpers.VariableContadoraLineas));
            strBldr.AppendLine(this.hijosNodo[0].VariablesGlobales);
            strBldr.AppendLine(GeneracionCodigoHelpers.DefinirVariablesAuxiliares(this.TablaSimbolos));
            //strBldr.AppendLine(this.hijosNodo[1].VariablesProcPrincipal);
            strBldr.AppendLine("");
            strBldr.AppendLine(GeneracionCodigoHelpers.DefinirFuncionesBasicas());
            
            // flanzani 8/11/2012
            // IDC_APP_2
            // Agregar funciones por defecto en el framework
            // Agrego la definicion de las funciones del framework para que aparezcan en pascal
            strBldr.AppendLine(GeneracionCodigoHelpers.DefinirFuncionesFramework(this.TablaSimbolos));       


            
            strBldr.AppendLine(this.hijosNodo[1].Codigo);

            strBldr.AppendLine("begin");
            strBldr.AppendLine(@"Assign(UserFile,'resultado.txt');");
            strBldr.AppendLine("Rewrite(UserFile);");
             
            

            strBldr.Append(GeneracionCodigoHelpers.InicializarVariablesGlobales(this.TablaSimbolos));
            strBldr.AppendLine("try");       
            strBldr.Append("\t").AppendLine(string.Format("{0}PRINCIPAL();",GlobalesCompilador.PREFIJO_VARIABLES));            
            strBldr.AppendLine("except");
            strBldr.AppendLine("on E: SysUtils.EDivByZero do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine(string.Format("WriteLn('ERROR FATAL: Se intento dividir por cero en la linea ',{0});",GeneracionCodigoHelpers.VariableContadoraLineas));
            strBldr.AppendLine("end;");
            strBldr.AppendLine("on EIteracion: EIteracionInfinitaException do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine(string.Format("WriteLn('ERROR FATAL: Iteracion infinita posible encontrada en la linea ',{0});", GeneracionCodigoHelpers.VariableContadoraLineas));
            strBldr.AppendLine("end;");
            strBldr.AppendLine("on ERango: SysUtils.ERangeError do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine(string.Format("WriteLn('Error Fatal: Se intento acceder a una posicion invalida de un arreglo en la linea ',{0});", GeneracionCodigoHelpers.VariableContadoraLineas));
            strBldr.AppendLine("end;");
            strBldr.AppendLine("on EIndiceInvalido: EIndiceArregloInvalido do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("WriteLn(EIndiceInvalido.Message);");
            strBldr.AppendLine("end;");
            strBldr.AppendLine("on ERaiz: EMatematicaRaizException do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("WriteLn(ERaiz.Message);");
            strBldr.AppendLine("end;");            
            strBldr.AppendLine("on ETangente: EMatematicaTangenteException do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("WriteLn(ETangente.Message);");
            strBldr.AppendLine("end;"); 
            strBldr.AppendLine("on ETotal: Exception do");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("end;");
            strBldr.AppendLine("end;");
            strBldr.AppendLine(string.Format("WriteLn('<<presione cualquier tecla para finalizar el programa>>');"));
            strBldr.AppendLine("Close(UserFile);"); 
            strBldr.AppendLine(GeneracionCodigoHelpers.PausarHastaEntradaTeclado());
            strBldr.AppendLine("end.");
            

            
            
            this.Codigo = strBldr.ToString();
        }

        private string ArmarTiposDeArreglo(List<NodoTipoArreglo> list)
        {
            StringBuilder strBlder = new StringBuilder();
            foreach (NodoTipoArreglo item in list)
            {
                strBlder.Append(item.Nombre).Append(" = ").Append("Array [1..").Append(item.Rango).Append("] of ");

                string tipo;
                switch (item.TipoDato)
                {
                    case NodoTablaSimbolos.TipoDeDato.Texto:
                        tipo = "string";
                        break;
                    case NodoTablaSimbolos.TipoDeDato.Numero:
                        //tipo = "integer";
                        tipo = "real";
                        break;
                    case NodoTablaSimbolos.TipoDeDato.Booleano:
                        tipo = "boolean";
                        break;
                    case NodoTablaSimbolos.TipoDeDato.Ninguno:               
                    default:
                        tipo = string.Empty;
                        break;
                }

                strBlder.Append(tipo).AppendLine(";");
                
            }

            return strBlder.ToString();
        }
    }
}
