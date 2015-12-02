using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CompiladorGargar.Auxiliares.AF
{
    class Estado
    {
        private String nombre;
        public String Nombre
        {
            get { return nombre; }
        }

        private bool esFinal;
        public bool EsFinal
        {
            get { return esFinal; }
            set { esFinal = value; }
        }

        private Lexicografico.ComponenteLexico.TokenType token;
        public Lexicografico.ComponenteLexico.TokenType Token
        {
            get { return token; }
            set { token = value; }
        }

        private bool esInicial;
        public bool EsInicial
        {
            get { return esInicial; }
            set { esInicial = value; }
        }

        public Estado(String nombre)
        {
            this.nombre = nombre;
            this.esFinal = false;
            this.esInicial = false;
        }

        public Estado(String nombre, bool esFinal)
        {
            this.nombre = nombre;
            this.esFinal = esFinal;
            this.esInicial = false;
        }

        public Estado(String nombre, bool esFinal,bool esInicial)
        {
            this.nombre = nombre;
            this.esFinal = esFinal;
            this.esInicial = esInicial;
        }
    }
}
