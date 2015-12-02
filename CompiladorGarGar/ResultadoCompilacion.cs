using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorGarGar
{
    public class ResultadoCompilacion
    {
        public bool CompilacionGarGarCorrecta { get; set; }
        public bool GeneracionEjectuableCorrecto { get; set; }
        public string Error { get; set; }
    }
}
