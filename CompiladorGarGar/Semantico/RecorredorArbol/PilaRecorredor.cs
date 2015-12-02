using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico.RecorredorArbol
{
    class PilaRecorredor
    {
        private List<NodoPilaRecorredor> elementosPila;


        public PilaRecorredor()
        {
            this.elementosPila = new List<NodoPilaRecorredor>();         
        }

        public NodoPilaRecorredor ObtenerTope()
        {

            return this.elementosPila.Last();
        }

        public void InsertarElemento(NodoPilaRecorredor elem)
        {
            this.elementosPila.Add(elem);
            //this.elementosPila.Insert(this.elementosPila.Count - 1, elem);
        }

        public void DescartarTope()
        {
            this.elementosPila.RemoveAt(this.elementosPila.Count - 1);
        }

        


        internal bool esVacia()
        {
            return this.elementosPila.Count == 0;
        }

        

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            for (int i = this.elementosPila.Count - 1; i >= 0; i--)
            {
                strBldr.Append(this.elementosPila[i].ToString());
                strBldr.Append(" ");
            }

            return strBldr.ToString();
        }
    }
}
