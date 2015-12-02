using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoIdAsignacion: NodoArbolSemantico
    {
        public NodoIdAsignacion(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

      
        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.Lexema = this.hijosNodo[0].Lexema;
            this.EsArreglo = this.hijosNodo[1].EsArreglo;

            //if (this.EsArreglo)
            //{
            //    this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
            //    this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Entero);
            //}
            //if (this.EsArreglo)
            //{
            //    nombreContexto = "Global";
            //}
            //else
            //{
            //    nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.nombreContextoLocal);
            //}

            this.Gargar = string.Format("{0} {1}", this.hijosNodo[0].Lexema, this.hijosNodo[1].Gargar);
            
            

            return this;
        }

      

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.Append(this.hijosNodo[0].LexemaVariable);

            if (this.hijosNodo[1].EsArreglo)
            {
                strBldr.Append("[");
                strBldr.Append(string.Format("FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo({0},'{1}')", this.hijosNodo[1].Codigo, this.hijosNodo[0].Lexema));
                strBldr.Append("]");
            }

            //strBldr.Append(this.hijosNodo[1].Codigo);
            

            this.Codigo = strBldr.ToString();
        }

    }
}
