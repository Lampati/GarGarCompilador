using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico;
using System.IO;
using System.Diagnostics;
using Utilidades;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Resultado;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Sintactico.ErroresManager;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar
{
    public class Compilador
    {
        private AnalizadorSintactico analizadorSintactico;

        public string DirectorioTemporales { get; set; }
        public string DirectorioEjecutables { get; set; }

        public string NombreEjecutable { get; set; }

        private bool modoDebug { get; set; }


        public Compilador()
            : this(false, Path.GetTempPath(), Path.GetTempPath(), Path.GetRandomFileName())
        {

        }

        public Compilador(string fileName)
            : this(false, Path.GetTempPath(), Path.GetTempPath(), fileName)
        {

        }

        public Compilador(bool modo, string dirTemp, string dirEjec, string nombre)
        {
            modoDebug = modo;
            //this.ArchivoGramatica = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.Configuration.ConfigurationManager.AppSettings["archGramatica"].ToString());
            DirectorioTemporales = dirTemp;
            DirectorioEjecutables = dirEjec;
            NombreEjecutable = nombre;

            DirectoriosManager.CrearDirectorioSiNoExiste(DirectorioTemporales,false);
            DirectoriosManager.CrearDirectorioSiNoExiste(DirectorioEjecutables,false);

            GeneracionCodigoHelpers.DirectorioTemporales = dirTemp;

            analizadorSintactico = new AnalizadorSintactico(GlobalesCompilador.NOMBRE_ARCH_GRAMATICA);
            analizadorSintactico.HabilitarSemantico = true;
        }
      

        private void CargarAnalizadorLexico(string texto)
        {
            analizadorSintactico.CargarAnalizadorLexicografico(texto);
        }

        public ResultadoCompilacion Compilar(string texto)
        {
            Stopwatch temporizador = Stopwatch.StartNew();
            ResultadoCompilacion res = new ResultadoCompilacion();

            if (texto != string.Empty)
            {
                try
                {
                    long timeStamp = Stopwatch.GetTimestamp();
                    long timeStampPaso;

                    ReiniciarCompilador();

                    res.TiempoGeneracionAnalizadorSintactico = temporizador.Elapsed.TotalSeconds;

                    List<PasoDebugTiempos> tiempos = new List<PasoDebugTiempos>();

                    int i = 1;

                    long timeStampLex = Stopwatch.GetTimestamp();
                    CargarAnalizadorLexico(texto);
                    float tiempoCargarLexico = ((float)(Stopwatch.GetTimestamp() - timeStampLex)) / ((float)Stopwatch.Frequency);
                   
                    bool pararComp = false;
                    GlobalesCompilador.TipoError tipoError = GlobalesCompilador.TipoError.Ninguno;

                    while (!this.analizadorSintactico.EsFinAnalisisSintactico() && !pararComp)
                    {

                        timeStampPaso = Stopwatch.GetTimestamp();
                        List<PasoAnalizadorSintactico> retorno = this.analizadorSintactico.AnalizarSintacticamenteUnPaso();
                        float tiempoAnalizSint = ((float)(Stopwatch.GetTimestamp() - timeStampPaso)) / ((float)Stopwatch.Frequency);

                        if (retorno.Count > 0)
                        {
                            foreach (var item in retorno)
                            {
                                switch (item.TipoError)
                                {
                                    case GlobalesCompilador.TipoError.Sintactico:
                                        tipoError = item.TipoError;
                                        res.ListaErrores.Add(item);
                                        pararComp = pararComp || item.PararCompilacion;
                                        break;
                                    case GlobalesCompilador.TipoError.Semantico:
                                        tipoError = item.TipoError;
                                        res.ListaErrores.Add(item);
                                        pararComp = pararComp || item.PararCompilacion;
                                        break;
                                    case GlobalesCompilador.TipoError.Ninguno:
                                        tipoError = item.TipoError;
                                        break;

                                }
                            }
                        }

                        if (modoDebug)
                        {

                            PasoCompilacion paso = new PasoCompilacion(this.analizadorSintactico.Pila.ToString(),
                                this.analizadorSintactico.CadenaEntrada.ToString(),
                                tipoError);

                            res.ListaDebugSintactico.Add(paso);
                        }

                        float numPaso = ((float)(Stopwatch.GetTimestamp() - timeStampPaso)) / ((float)Stopwatch.Frequency);

                        tiempos.Add(new PasoDebugTiempos() { NumPaso = i, TiempoAnalizadorSint = tiempoAnalizSint, TiempoAnalizadorTot = numPaso }); ;
                        i++;
                    }

                    if (this.analizadorSintactico.EsFinAnalisisSintactico() && res.ListaErrores.Count == 0)
                    {
                        res.CompilacionGarGarCorrecta = true;
                    }

                    CompilarCodigoIntermedio(res);
                }          
                catch (Exception ex)
                {
                    string error = "Ha habido un error en la compilacion. Por favor reporte el problema" ;

                    if (modoDebug)
                    {
                        error = string.Format("{0}: \r\n {1}", ex.Message, ex.StackTrace);
                    }

                    res.CompilacionGarGarCorrecta = false;
                    res.ListaErrores.Add(new PasoAnalizadorSintactico(error,GlobalesCompilador.TipoError.Ninguno,0,0));
                }
                
                res.TiempoCompilacionTotal = temporizador.Elapsed.TotalSeconds;
            }
            else
            {
                //No hay nada para compilar
                res.ListaErrores.Add(new PasoAnalizadorSintactico(
                    "No se ha ingresado programa para compilar",
                    GlobalesCompilador.TipoError.Sintactico,
                    GlobalesCompilador.UltFila,
                    GlobalesCompilador.UltCol,
                    false)
                );
            }
            
            return res;
        }

        private void ReiniciarCompilador()
        {
            GlobalesCompilador.ReiniciarFilaYColumna();

            GeneracionCodigoHelpers.ReiniciarValoresVariablesAleatorias();

            analizadorSintactico.ReiniciarAnalizadorSintactico();
        }

        private void CompilarCodigoIntermedio(ResultadoCompilacion res)
        {
            if (res.CompilacionGarGarCorrecta)
            {
                res.ArbolSemanticoResultado = this.analizadorSintactico.ArbolSemantico;
                res.TablaSimbolos = res.ArbolSemanticoResultado.TablaDeSimbolos;

                long timeStampCod = Stopwatch.GetTimestamp();
                res.CodigoPascal = res.ArbolSemanticoResultado.CalcularCodigo();

                Dictionary<int, int> bindeoLineasEntrePascalYGarGar = BindearLineas(res.CodigoPascal.Split(new string[] { "\r\n" }, StringSplitOptions.None));


                res.TiempoGeneracionCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                timeStampCod = Stopwatch.GetTimestamp();
                res.ArchTemporalCodigoPascal = CrearArchivoTemporal(res.CodigoPascal);
                res.ArchTemporalCodigoPascalConRuta = Path.Combine(DirectorioTemporales, res.ArchTemporalCodigoPascal);
                res.TiempoGeneracionTemporalCodigo = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                try
                {
                    timeStampCod = Stopwatch.GetTimestamp();

                

                    ResultadoCompilacionPascal resPas = CompilarPascal(res.ArchTemporalCodigoPascalConRuta, bindeoLineasEntrePascalYGarGar);

                    res.ResultadoCompPascal = resPas;
                    res.ArchEjecutable = resPas.NombreEjecutable;
                    res.ArchEjecutableConRuta = Path.Combine(DirectorioEjecutables, res.ArchEjecutable);
                    res.TiempoGeneracionEjecutable = ((float)(Stopwatch.GetTimestamp() - timeStampCod)) / ((float)Stopwatch.Frequency);

                    res.GeneracionEjectuableCorrecto = resPas.CompilacionPascalCorrecta;

                }
                catch
                {
                    res.GeneracionEjectuableCorrecto = false;
                }

                BorrarTemporales();
            }
        }

        private Dictionary<int, int> BindearLineas(string[] lineas)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            int numLineaGargar = 0;

            for (int i = 0; i < lineas.Length; i++)
            {
                int numLineaPascal = i + 1;

                string linea = lineas[i].Trim('\t');

                dict.Add(numLineaPascal, numLineaGargar);

                if (linea.Contains(GeneracionCodigoHelpers.VariableContadoraLineas))
                {
                    string[] res = linea.Split(new string[] { ":=" }, StringSplitOptions.None);

                    if (res.Length > 1)
                    {
                        string num = res[1].TrimStart().TrimEnd().TrimEnd(';');
                        numLineaGargar = Convert.ToInt32(num);
                    }
                }
                         
            }

            return dict;
        }

        private void BorrarTemporales()
        {
            if (!string.IsNullOrWhiteSpace(DirectorioTemporales))
            {
                DirectoriosManager.BorrarArchivosDelDirPorExtension(DirectorioTemporales, "*.pas");
                DirectoriosManager.BorrarArchivosDelDirPorExtension(DirectorioTemporales, "*.o");
            }
        }

        private ResultadoCompilacionPascal CompilarPascal(string archTemporalPascal, Dictionary<int, int> bindeoLineas)
        {
            ResultadoCompilacionPascal res;

            try
            {
                archTemporalPascal = string.Format("{0}{1}{0}",'"',archTemporalPascal);

                string exe = Path.Combine(DirectorioEjecutables, NombreEjecutable);

                if (!exe.Contains(".exe"))
                {
                    exe = string.Concat(exe, ".exe");
                }

                string auxExe = string.Format("{0}{1}{0}", '"', exe);

                string pathIncludes = Path.Combine(GlobalesCompilador.PathEjecucionAplicacion, GlobalesCompilador.NOMBRE_DIR_UNITS_PASCAL);
                pathIncludes = string.Format("{0}{1}{0}", '"', pathIncludes);


                string argumentoInclude = string.Format("-Fu{0}", pathIncludes);
                string argumentoModoCompilacion = string.Format("-Mobjfpc");
                string argumentoUseAnsiStrings = string.Format("-Sh");
                string argumentoChequearIndicesDeArreglos = string.Format("-Cr");
                string argumentoNombreExe = string.Format("-o{0}", auxExe);

                string resultado = EjecucionManager.EjecutarSinVentana(GlobalesCompilador.NOMBRE_ARCH_COMPILADOR_PASCAL, new List<string>() { argumentoInclude, argumentoModoCompilacion, argumentoUseAnsiStrings, argumentoChequearIndicesDeArreglos, argumentoNombreExe, archTemporalPascal });
                res = new ResultadoCompilacionPascal(resultado, bindeoLineas);
                res.NombreEjecutable = exe;
            }
            catch (Exception)
            {
                res = new ResultadoCompilacionPascal();
                res.CompilacionPascalCorrecta = false;
            }

            return res;
        }

        private string CrearArchivoTemporal(string cod)
        {
            string nombreArch = string.Format("{0}{1}",RandomManager.RandomStringConPrefijo("tempPas",20,true),".pas");

            using (StreamWriter strWriter = new StreamWriter(Path.Combine(this.DirectorioTemporales, nombreArch), false))
            {
                strWriter.WriteLine(cod);

                strWriter.Flush();

            }

            return nombreArch;
        }
    }
}
