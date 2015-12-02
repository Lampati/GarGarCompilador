using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Auxiliares;
using System.Globalization;

namespace CompiladorGargar.Sintactico.Gramatica
{
    internal class Terminal : ElementoGramatica
    {
        #region estaticas EOF, lambda y ERROR

        internal static Terminal ElementoVacio()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Ninguno;

            return t;
        }

        internal static Terminal ElementoEntonces()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.SiEntonces;
            t.componente.Lexema = "entonces";
            return t;
        }

        internal static Terminal ElementoHacer()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.MientrasHacer;
            t.componente.Lexema = "hacer";
            return t;
        }

        internal static Terminal ElementoTipoNumero()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.TipoNumero;
            t.componente.Lexema = "numero";
            return t;
        }

        internal static Terminal ElementoTipoTexto()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.TipoTexto;
            t.componente.Lexema = "texto";
            return t;
        }

        internal static Terminal ElementoTipoBooleano()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.TipoBooleano;
            t.componente.Lexema = "booleano";
            return t;
        }

        internal static Terminal ElementoFinSentencia()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.FinSentencia;
            t.componente.Lexema = ";";
            return t;
        }

        internal static Terminal ElementoAsignacion()
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion y modifico su lexema
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Igual;
            t.componente.Lexema = "=";
            return t;
        }

        internal static Terminal ElementoParentesisClausura()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.ParentesisClausura;
            t.componente.Lexema = ")";
            return t;
        }

        internal static Terminal ElementoEOF()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.EOF;

            return t;
        }

        internal static Terminal ElementoError()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Error;

            return t;
        }
        #endregion

        private ComponenteLexico componente;
        public ComponenteLexico Componente
        {
            get { return componente; }
            set { componente = value; }
        }

        public Terminal()
        {
        }


        public Terminal(string lexema)
        {
            this.componente = new ComponenteLexico();
            this.componente.Token = (ComponenteLexico.TokenType)EnumUtils.enumValueOf(lexema, typeof(ComponenteLexico.TokenType));
            this.componente.Lexema = lexema;
        }


        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // safe because of the GetType check
            Terminal t = (Terminal)obj;

            // use this pattern to compare reference members
            if (Componente.Equals(t.Componente))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return EnumUtils.stringValueOf(componente.Token);
        }

        internal bool NoEsLambda()
        {
            return (this.componente.Token != ComponenteLexico.TokenType.Ninguno);
        }

        public override double ObtenerValor()
        {
            double retorno = 0;
            bool resultado;
            switch (this.Componente.Token)
            {
                case ComponenteLexico.TokenType.Numero:
                    resultado = double.TryParse(this.Componente.Lexema, System.Globalization.NumberStyles.Number, new CultureInfo("en-US"),out retorno);
                    break;

                

                case ComponenteLexico.TokenType.Identificador:
                    //Buscarlo en la tabla de simbolos
                    break;

                default:

                    break;
                    //Error?
            }

            return retorno;
        }

        internal static Terminal ElementoSi()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.SiComienzo;
            t.componente.Lexema = "si";
            return t;
        }

        internal static Terminal ElementoMientras()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.MientrasComienzo;
            t.componente.Lexema = "mientras";
            return t;
        }

        internal static Terminal ElementoFuncion()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.FuncionComienzo;
            t.componente.Lexema = "funcion";
            return t;
        }

        internal static Terminal ElementoLlamar()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Llamar;
            t.componente.Lexema = "llamar";
            return t;
        }

        internal static Terminal ElementoVar()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Var;
            t.componente.Lexema = "var";
            return t;
        }

        internal static Terminal ElementoConst()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Const;
            t.componente.Lexema = "const";
            return t;
        }

        internal static Terminal ElementoMostrar()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Mostrar;
            t.componente.Lexema = "mostrar";
            return t;
        }

        internal static Terminal ElementoMostrarP()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.MostrarConPausa;
            t.componente.Lexema = "mostrarp";
            return t;
        }
    }
}
