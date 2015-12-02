using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares
{
    class Variable
    {
        public string Lexema { get; set; }
        public bool EsArreglo { get; set; }
        public int IndiceArreglo { get; set; }

        public Variable(string lexema, bool arreglo, int indice)
        {
            this.Lexema = lexema;
            this.EsArreglo = arreglo;
            this.IndiceArreglo = indice;
        }
    }
}
