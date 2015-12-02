using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    [Serializable]
    internal class ValidacionException : Exception
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public MensajeError MensjError { get; set; }

        public ValidacionException(MensajeError mensaje, string m) : base(m)
        {
            MensjError = mensaje;
        }
    }
}
