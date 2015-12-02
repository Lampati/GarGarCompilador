using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    [Serializable]
    internal class AnalizadorErroresException : Exception
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Parar { get; set; }
        public string MensajeModoGrafico { get; set; }

        public AnalizadorErroresException(string m, string mensajeErrorModoGrafico)
            : base(m)
        {
            if (string.IsNullOrEmpty(mensajeErrorModoGrafico))
            {
                MensajeModoGrafico = m;
            }
            else
            {
                MensajeModoGrafico = mensajeErrorModoGrafico;
            }
          
        }
    }
}
