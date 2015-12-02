using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Lexicografico
{


    internal class ComponenteLexico
    {
        //Enumerador que contiene todos los tipos de token aceptados por el lenguaje.
        public enum TokenType
        {
            [DescriptionAttribute("lambda")]
            Ninguno,
            
            Error,

            [DescriptionAttribute("$")]
            EOF,

            [DescriptionAttribute(";")]
            FinSentencia, // ;

            [DescriptionAttribute("mientras")]
            MientrasComienzo, // mientras
            [DescriptionAttribute("hacer")]
            MientrasHacer, // hacer
            [DescriptionAttribute("finmientras")]
            MientrasFin, // fin-mientras
            [DescriptionAttribute("si")]
            SiComienzo, // si
            [DescriptionAttribute("entonces")]
            SiEntonces, // entonces
            [DescriptionAttribute("sino")]
            SiSino, // sino
            [DescriptionAttribute("finsi")]
            SiFin, //fin-si

            [DescriptionAttribute("principal")]
            Principal, //procedimiento
          
            [DescriptionAttribute("procedimiento")]
            ProcedimientoComienzo, //procedimiento
            [DescriptionAttribute("finproc")]
            ProcedimientoFin, //fin-proc
            [DescriptionAttribute("funcion")]
            FuncionComienzo, //funcion
            [DescriptionAttribute("finfunc")]
            FuncionFin, //fin-funcion
            [DescriptionAttribute("comenzar")]
            Comenzar, //comenzar

            [DescriptionAttribute("constantes")]
            Constantes, //const
            [DescriptionAttribute("variables")]
            Variables, //var

            [DescriptionAttribute("const")]
            Const, //const
            [DescriptionAttribute("var")]
            Var, //var

            [DescriptionAttribute("llamar")]
            Llamar, //const
            [DescriptionAttribute("leer")]
            Leer, //const

            [DescriptionAttribute("id")]
            Identificador, //variable formada con una letra inicial y despues numeros o letras

            [DescriptionAttribute("int")]
            Numero,

            [DescriptionAttribute("verdadero")]
            Verdadero,
            [DescriptionAttribute("falso")]
            Falso,



            [DescriptionAttribute(":")]
            TipoDato, // :

            [DescriptionAttribute("ref")]
            Referencia, // :
            
            [DescriptionAttribute("numero")]
            TipoNumero, //entero

            [DescriptionAttribute("texto")]
            TipoTexto, //entero
            [DescriptionAttribute("booleano")]
            TipoBooleano, //entero
                    

         

            [DescriptionAttribute("mostrar")]
            Mostrar,

            [DescriptionAttribute("mostrarp")]
            MostrarConPausa,

            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Borro la asignacion, ya que va a ser lo mismo que el igual
            [DescriptionAttribute(":=")]
            Asignacion, // :=

            [DescriptionAttribute("&")]
            Concatenacion, // +
            
            [DescriptionAttribute("+")]
            SumaEntero, // +
            [DescriptionAttribute("-")]
            RestaEntero, // -
            [DescriptionAttribute("*")]
            MultiplicacionEntero, // *
            [DescriptionAttribute("/")]
            DivisionEntero, // /

            [DescriptionAttribute(">")]
            Mayor, // >
            [DescriptionAttribute(">=")]
            MayorIgual, // >=
            [DescriptionAttribute("=")]
            Igual, // =
            [DescriptionAttribute("<=")]
            MenorIgual, // <=
            [DescriptionAttribute("<")]
            Menor, // <
            [DescriptionAttribute("<>")]
            Distinto, // !=

            [DescriptionAttribute("!")]
            Negacion, // !=

            [DescriptionAttribute("and")]
            And, // !=
            [DescriptionAttribute("or")]
            Or, // !=

            [DescriptionAttribute("(")]
            ParentesisApertura, // (
            [DescriptionAttribute(")")]
            ParentesisClausura, // )
            [DescriptionAttribute("[")]
            CorcheteApertura, // [
            [DescriptionAttribute("]")]
            CorcheteClausura, // ]

            [DescriptionAttribute("literal")]
            Literal, // (
            [DescriptionAttribute(",")]
            Coma, // ,

            [DescriptionAttribute("de")]
            De, // de

            [DescriptionAttribute("arreglo")]
            Arreglo, // arreglo

            [DescriptionAttribute("{")]
            ComentarioApertura, // {
            [DescriptionAttribute("}")]
            ComentarioClausura, // }

             [DescriptionAttribute("comentario")]
            Comentario // 

            //Llamar, //llamar
            //Leer,
            //Visualizar,
            //VisualizarLN,
        }

        #region Properties

        private TokenType token;
        public TokenType Token
        {
            get { return token; }
            set { token = value; }
        }

        private String lexema;
        public String Lexema
        {
            get { return lexema; }
            set { lexema = value; }
        }

        public string CaracterErroneo { get; set; }

        private int fila;
        public int Fila
        {
            get { return fila; }
            set { fila = value; }
        }

        private int columna;
        public int Columna
        {
            get { return columna; }
            set { columna = value; }
        }

        private String descripcion;
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private bool antecedidoPorSeparador;
        public bool AntecedidoPorSeparador
        {
            get { return antecedidoPorSeparador; }
            set { antecedidoPorSeparador = value; }
        }

      

        #endregion

        public ComponenteLexico()
        {
            this.descripcion = string.Empty;
            this.token = TokenType.Ninguno;
            this.lexema = string.Empty;
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
            ComponenteLexico comp = (ComponenteLexico)obj;

            // use this pattern to compare reference members
            if (token == comp.token)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
