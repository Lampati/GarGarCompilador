using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico
{
    internal class ErrorSintacticoException : ErrorCompilacionException 
    {
        public bool DescartarTopePila { get; set; }
        public bool DescartarTopeCadena { get; set; }
        public bool PararAnalisis { get; set; }
        public bool Mostrar { get; set; }
        public Terminal TerminalHastaDondeDescartarPila { get; set; }


        public ErrorSintacticoException(string desc, int f, int c, bool descartarPila, bool descartarCadena, bool parar ) 
            : base(desc, f, c)
        {
            this.Tipo = "Sintactico";
            this.DescartarTopePila = descartarPila;
            this.DescartarTopeCadena = descartarCadena;
            this.PararAnalisis = parar;
            this.Mostrar = true;
        }

        public ErrorSintacticoException(string desc, int f, int c, bool descartarPila, bool descartarCadena, bool parar, bool mostrar)
            : base(desc, f, c)
        {
            this.Tipo = "Sintactico";
            this.DescartarTopePila = descartarPila;
            this.DescartarTopeCadena = descartarCadena;
            this.PararAnalisis = parar;
            this.Mostrar = mostrar;
        }

        public ErrorSintacticoException(string desc, int f, int c, bool descartarPila, bool descartarCadena, bool parar, bool mostrar, Terminal terminalHastaDondeDescartar)
            : base(desc, f, c)
        {
            this.Tipo = "Sintactico";
            this.DescartarTopePila = descartarPila;
            this.DescartarTopeCadena = descartarCadena;
            this.PararAnalisis = parar;
            this.Mostrar = mostrar;
            this.TerminalHastaDondeDescartarPila = terminalHastaDondeDescartar;
        }

    }
}
