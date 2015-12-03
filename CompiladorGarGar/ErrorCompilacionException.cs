using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar
{
    [Serializable]
    internal class ErrorCompilacionException : Exception
    {
        public string Tipo { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }

        public ErrorCompilacionException(string desc, int f, int c ) 
            : base(desc)
        {
            this.Fila = f;
            this.Columna = c;
        }

        public ErrorCompilacionException(string desc)
            : base(desc)
        {
        }
    }
}
