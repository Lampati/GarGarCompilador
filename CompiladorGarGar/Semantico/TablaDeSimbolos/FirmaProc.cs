using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico.TablaDeSimbolos
{
    public class FirmaProc
    {

        public NodoTablaSimbolos.TipoDeDato TipoDato { get; set; }
        public bool EsArreglo { get; set; }
        public string Lexema { get; set; }
        public bool EsPorReferencia { get; set; }

        public FirmaProc(string nombre, NodoTablaSimbolos.TipoDeDato tipo, bool arr, bool esRef)
        {
            TipoDato = tipo;
            EsArreglo = arr;
            Lexema = nombre;
            EsPorReferencia = esRef;
        }
    }
}
