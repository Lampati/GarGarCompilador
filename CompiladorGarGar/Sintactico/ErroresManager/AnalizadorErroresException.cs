using CompiladorGargar.Sintactico.ErroresManager.Errores;
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

        public MensajeError Mensaje { get; set; }

        public AnalizadorErroresException(MensajeError m)
            : base(m.Descripcion)
        {
            Mensaje = m;
          
        }
    }
}
