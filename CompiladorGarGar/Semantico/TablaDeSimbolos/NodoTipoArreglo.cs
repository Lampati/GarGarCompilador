using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Auxiliares;
using Utilidades;

namespace CompiladorGargar.Semantico.TablaDeSimbolos
{
    class NodoTipoArreglo
    {  

        private string nombre;
        public string Nombre 
        {
            get { return nombre; }
        }   

        private NodoTablaSimbolos.TipoDeDato tipoDato;
        public NodoTablaSimbolos.TipoDeDato TipoDato
        {
            get { return tipoDato; }
        }     

        private string rango;
        public string Rango
        {
            get { return rango; }
        }    

        public NodoTipoArreglo(NodoTablaSimbolos.TipoDeDato dato, string r)
        {
            StringBuilder strBldr = new StringBuilder();
            
            this.tipoDato = dato;
            this.rango = r;
            this.nombre = RandomManager.RandomStringConPrefijo(string.Format("{0}_{1}_",EnumUtils.stringValueOf(tipoDato),rango),6,true);
            
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
            NodoTipoArreglo nodo = (NodoTipoArreglo)obj;

            // use this pattern to compare reference members
            return Rango == nodo.Rango && TipoDato.Equals(nodo.TipoDato);
        }

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            strBldr.Append(Nombre).Append(", ");
            strBldr.Append(EnumUtils.stringValueOf(TipoDato)).Append(", ");
            strBldr.Append(Rango);

            return strBldr.ToString();
        }
    }
}
