using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Sintactico.Gramatica
{
    internal class NoTerminal : ElementoGramatica
    {


        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public NoTerminal()
        {

        }

        public NoTerminal(string nom)
        {
            nombre = nom;
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
            NoTerminal nt = (NoTerminal)obj;

            // use this pattern to compare reference members
            if (Nombre.Equals(nt.Nombre))
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
            return nombre;
        }

        #region Atributos

        public override double ObtenerValor()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
