using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CompiladorGargar.Lexicografico;

namespace CompiladorGargar.Sintactico.Gramatica
{
    internal class Produccion
    {
        private NoTerminal izq;
        public NoTerminal Izq
        {
            get { return izq; }
            set { izq = value; }
        }

        private List<ElementoGramatica> der;
        public List<ElementoGramatica> Der
        {
            get { return der; }
            set { der = value; }
        }

        public Produccion()
        {
            der = new List<ElementoGramatica>();
        }

        public bool ApareceEnParteDerecha(NoTerminal nt)
        {
            if (this.der != null)
            {
                return this.der.Exists(

                    delegate(ElementoGramatica _nt)
                    {
                        if (_nt.Equals(nt))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    );
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            strBldr.Append(izq.ToString());
            strBldr.Append(" --> ");

            if (der != null)
            {
                foreach (ElementoGramatica elem in der)
                {
                    strBldr.Append(elem.ToString());
                    strBldr.Append(" ");
                }
            }
            else
            {
                strBldr.Append("lambda");
            }
            return strBldr.ToString();
        }


        public ElementoGramatica ObtenerSiguienteDe(NoTerminal nt)
        {
           int i = this.der.FindIndex(

                delegate(ElementoGramatica _nt)
                {
                    if (_nt.Equals(nt))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                );

           if (i < this.der.Count-1)
           {
               return this.der[i + 1];
           }
           else
           {
               return null;
               //Terminal t = new Terminal();
               //t.Componente = new ComponenteLexico();
               //t.Componente.Token = ComponenteLexico.TokenType.EOF;
               //return t;
                
           }
        }

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
            Produccion prod = (Produccion)obj;

            // use this pattern to compare reference members
            if (Izq.Equals(prod.Izq) && this.IgualParteDerecha(prod))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool IgualParteDerecha(Produccion prod)
        {
            if (this.Der.Count != prod.Der.Count)
            {
                return false;
            }

            bool retorno = true;

            for (int i = 0; i < this.Der.Count; i++)
            {
                if (!this.Der[i].Equals(prod.Der[i]))
                {
                    retorno = false;
                }                
            }

            return retorno;
        }



        internal bool ProduceElementoVacio()
        {
            return this.Der[0].Equals(Terminal.ElementoVacio());
        }
    }    
}
