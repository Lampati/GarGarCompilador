using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico
{
    class ErrorSemanticoException : ErrorCompilacionException
    {
        public ErrorSemanticoException(string desc, int f, int c) 
            : base(desc, f, c)
        {
            this.Tipo = "Semantico";         
        }

        public ErrorSemanticoException(string desc)
            : base(desc)
        {
            this.Tipo = "Semantico";

            //Cambiado a partir de ahora, toma de global
            this.Fila = GlobalesCompilador.UltFila;
            this.Columna = GlobalesCompilador.UltCol;


        }
    }
}
