using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico.RecorredorArbol
{
    class ColaIndices
    {
        private List<int> indices;

        public int Count
        {
            get
            {
                return indices.Count;
            }
        }

        public ColaIndices()
        {
            this.indices = new List<int>();
        }

        public int ObtenerPrimerIndice()
        {
            return this.indices.First();
        }

        public int ObtenerIndice(int i)
        {
            return this.indices[i];
        }

        public void EliminarPrimerIndice()
        {
            this.indices.RemoveAt(0);
        }

        public void EliminarUltimoIndice()
        {
            this.indices.RemoveAt(this.indices.Count - 1);
        }

        public void InsertarIndice(int i)
        {
            this.indices.Add(i);
        }



        internal bool esVacia()
        {
            return this.indices.Count == 0;
        }


    }
}
