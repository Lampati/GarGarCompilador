using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Interfases
{
    interface IValorizable
    {
        int ObtenerValor(Terminal t);
    }
}
