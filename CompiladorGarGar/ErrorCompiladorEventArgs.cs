using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar
{
    class ErrorCompiladorEventArgs : EventArgs 
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool PararAnalisis { get; set; }

        public ErrorCompiladorEventArgs(string tipo, string desc, int f, int c,bool parar)
        {
            this.Tipo = tipo;
            this.Descripcion = desc;
            this.Fila = f;
            this.Columna = c;
            this.PararAnalisis = parar;
        }
    }
}
