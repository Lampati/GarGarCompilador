using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Auxiliares.AF
{
    class FuncionTransicion
    {
        private String estadoInicial;
        public String EstadoInicial
        {
            get { return estadoInicial; }
        }

        private char caracter;
        public char Caracter
        {
            get { return caracter; }
        }

        private String estadoFinal;
        public String EstadoFinal
        {
            get { return estadoFinal; }
        }

        public FuncionTransicion(String estadoI, char caracter, String estadoF)
        {
            this.estadoInicial = estadoI;
            this.caracter = caracter;
            this.estadoFinal = estadoF;
        }
    }
}
