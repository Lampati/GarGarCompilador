using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol;
using CompiladorGargar.Resultado.Auxiliares;
using System.Text.RegularExpressions;

namespace CompiladorGargar.Resultado.Auxiliares
{
    public class ResultadoCompilacionPascal
    {
        public bool CompilacionPascalCorrecta { get; set; }

        public string NombreEjecutable { get; set; }

        public List<ResultadoCompilacionPascalLinea> ListaErrores { get; set; }
        public List<ResultadoCompilacionPascalLinea> ListaWarnings { get; set; }
        public List<ResultadoCompilacionPascalLinea> ListaNotes { get; set; }
        public List<ResultadoCompilacionPascalLinea> ListaHints { get; set; }

        public ResultadoCompilacionPascal()
        {
            ListaErrores = new List<ResultadoCompilacionPascalLinea>();
            ListaWarnings = new List<ResultadoCompilacionPascalLinea>();
            ListaNotes = new List<ResultadoCompilacionPascalLinea>();
            ListaHints = new List<ResultadoCompilacionPascalLinea>();
         
        }

        public ResultadoCompilacionPascal(string output, Dictionary<int, int> bindeoLineas)
        {
            ListaErrores = new List<ResultadoCompilacionPascalLinea>();
            ListaWarnings = new List<ResultadoCompilacionPascalLinea>();
            ListaNotes = new List<ResultadoCompilacionPascalLinea>();
            ListaHints = new List<ResultadoCompilacionPascalLinea>();

            string[] lineas = output.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in lineas)
            {
                string filaYCol = Regex.Match(item, @"\(([^)]*)\)").Groups[1].Value;
                int fila = 0;
                int col = 0;

                string[] resFilas = filaYCol.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (resFilas.Length > 0)
                {
                    int.TryParse(resFilas[0], out fila);
                    if (resFilas.Length > 1)
                    {
                        int.TryParse(resFilas[1], out col);

                    }
                }

                int lineaGarGar = fila;
                bindeoLineas.TryGetValue(fila, out lineaGarGar);

                string[] contenido = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                if (contenido[0].Contains("Fatal") || contenido[0].Contains("Error"))
                {
                    ListaErrores.Add(new ResultadoCompilacionPascalLinea(contenido[1], lineaGarGar, col));
                }                
                else if (contenido[0].Contains("Warning"))
                {
                    ListaWarnings.Add(new ResultadoCompilacionPascalLinea(contenido[1], lineaGarGar, col));
                }
                else if (contenido[0].Contains("Note"))
                {
                    ListaNotes.Add(new ResultadoCompilacionPascalLinea(contenido[1], lineaGarGar, col));
                }
                else if (contenido[0].Contains("Hint"))
                {
                    ListaHints.Add(new ResultadoCompilacionPascalLinea(contenido[1], lineaGarGar, col));
                }                
            }

            CompilacionPascalCorrecta = ListaErrores.Count == 0;           

        }
        
    }

    public class ResultadoCompilacionPascalLinea
    {
        public string ErrorNativo { get; set; }
        public string ErrorTraducido { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Mostrar { get; set; }

        public ResultadoCompilacionPascalLinea(string errorNativo, int f, int c)
        {
            ErrorNativo = errorNativo;
            Fila = f;
            Columna = c;
            Mostrar = false;

            if (errorNativo.Contains("range check"))
            {
                ErrorTraducido = string.Format("Error Fatal: Indice de arreglo fuera de rango en la linea {0}",Fila);
                Mostrar = true;
            }

            if (errorNativo.Contains("Overflow in arithmetic operation"))
            {
                ErrorTraducido = string.Format("Error Fatal: La operacion aritmetica en la linea {0} causa un desbordamiento de valores", Fila);
                Mostrar = true;
            }
        }
    }

    

   

  
}
