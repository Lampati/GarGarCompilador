using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using CommandLine;
using CommandLine.Text;
using System.IO;
using CompiladorGargar.Resultado;

namespace CompiladorGargar
{

    class Options
    {
        [Option('a', "archivo", Required = true, 
          HelpText = "Archivo GarGar a compilar.")]
        public string InputFile { get; set; }

        [Option('e', "ejecutable", Required = true,
         HelpText = "Nombre ejecutable.")]
        public string ExeFile { get; set; }

        [Option('v', "verbose", DefaultValue = false,
          HelpText = "Imprimir todos los mensajes.")]
        public bool Verbose { get; set; }
    }


    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            try
            {
                //Compilador comp = new Compilador();

                //Application.Run(comp);

                 var options = new Options();
                 var result = CommandLine.Parser.Default.ParseArguments<Options>(args);

                
                if (!result.Errors.Any())
                {

                    if (File.Exists(result.Value.InputFile))
                    {
                        using (StreamReader reader = new StreamReader(result.Value.InputFile))
                        {
                            string textoCompilar = reader.ReadToEnd();

                            //Compilador compilador = new Compilador(false, Globales.ConstantesGlobales.PathEjecucionAplicacion, Globales.ConstantesGlobales.PathEjecucionAplicacion, result.Value.ExeFile);
                            Compilador compilador = new Compilador(false, Path.GetTempPath(), Path.GetTempPath(), result.Value.ExeFile);
                            ResultadoCompilacion res = compilador.Compilar(textoCompilar);


                            if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null &&
                                res.ResultadoCompPascal.CompilacionPascalCorrecta)
                            {
                                Console.WriteLine("Compilacion exitosa");
                            }
                            else
                            {
                                Console.WriteLine("Error de compilacion");
                                foreach (var error in res.ListaErrores)
                                {
                                    Console.WriteLine(string.Format("<{0},{1}> {2}",error.Fila,error.Columna, error.Descripcion));
                                }
                                
                            }
                        }
                    }
                }

                //if (args != null)
                //{


                //    if (args.Length > 0)
                //    {
                //        List<string> argumentos = new List<string>(args);
                //    }
                //    else
                //    {
                //        Console.WriteLine("Coloque el parametro -? para ayuda");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Coloque el parametro -? para ayuda");
                //}
            }
            catch (Exception ex)
            {
                if (!Debugger.IsAttached)
                {
                    MessageBox.Show("Error fatal en el compilador.");
                }
                else
                {
                    MessageBox.Show(
                        new StringBuilder(ex.Message).AppendLine()
                                                     .AppendLine(ex.Source)
                                                     .AppendLine(ex.StackTrace)
                                                     .ToString());
                }

            }
        }

    }
}
